select username,isnull([open],0)+isnull([close],0) as Total,[close] as Closed from
(
Select userid,UserName ,
      case when ICStatusID=5 then 'Close' else 'open' end as ICStatus,COUNT(*) [Ship Cnt] from                                                          (
      Select ICStatusID, ID,UserID from InterCompany where  CONVERT(date,LastActionDate)='25-Jun-2012' 
      union 
      Select IC.ICStatusID,MasterID ,H.UserID
      from InterCompany IC 
      join (Select MasterID,UserID,COUNT(*) [Cnt] from InterCompanyHistory where  CONVERT(date,LastActionDate)='25-Jun-2012' group by MasterID,UserID)
      H on (H.MasterID=IC.ID) 
      ) [Data] 
      join Users u on u.ID=DATA.UserID
      where UserID<>25
group by UserID,UserName,ICStatusID)data2
pivot (sum([Ship Cnt]) for ICStatus in ([open],[Close])) as data3
 
order by UserName
