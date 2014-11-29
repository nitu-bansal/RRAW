

declare @fromDate datetime
declare @ToDate datetime

set @fromDate='2012-07-11 00:00:00.000'
set @ToDate='2012-07-11 00:00:00.000'



SELECT Station,SettlementNumber,MIN([open])as [open],SUM([input]) as [input],SUM([output]) as [output],MIN(residual) as residual FROM
(
	select 
	a.SettlementNumber,
	a.Station,
	(select sum(opencount) from LIVE_SUMMARY..InterCompanyVolumeSummary where ReportDate=@fromDate and SettlementNumber=a.SettlementNumber and Station=a.Station group by Station,SettlementNumber )as [open],
	input,
	output,
	(select SUM(ResidualCount) from LIVE_SUMMARY..InterCompanyVolumeSummary where ReportDate=@ToDate and SettlementNumber=a.SettlementNumber and Station=a.Station group by Station,SettlementNumber )as residual,
	ReportDate
	 from 
		(

			select SettlementNumber,Station,ReportDate,SUM(OpenCount) as opencount,SUM(InputCount) as input,SUM(OutputCount)as output,
			SUM(ResidualCount)as Residual from LIVE_SUMMARY..InterCompanyVolumeSummary  
			where ReportDate between @fromDate and @ToDate		---------date range based on user input
			---and Station='atc' and SettlementNumber='1210'	---------add this condition based on filteration if needed.
			group by SettlementNumber,Station,ReportDate

		) a

)B
group by Station,SettlementNumber
order by Station,SettlementNumber
