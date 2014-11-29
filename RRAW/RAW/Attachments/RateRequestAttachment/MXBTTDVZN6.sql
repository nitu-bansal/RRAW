select  * from SQLFeedsShipments where 
((ORIGIN='SGN' and ShipperAccountCode='TH120179' and ShipperName like 'GREYSTONE DATA SYSTEM%' and CustomerAccountCode='TH120179' and CustomerName='GREYSTONE DATA SYSTEM VIETNAM' and BillToAccountCode='TH868550')
or (origin='ICN' and Destination='SIN' and ConsigneeName like 'FLEXTRONICS MANUFACTURING%' and CustomerName='FLEXTRONICS' and BillToAccountCode='KRCIGUSA')
or ( origin='BKK' and ConsigneeCountryCode='BR' and CustomerAccountCode='TH100003' and CustomerName = 'WESTERN DIGITAL (THAILAND) CO.,' and BillToAccountCode='TH868550')
or(origin='KUL' and ShipperAccountCode='02842185' and ShipperName='WESTERN DIGITAL M SDN BHD' and ConsigneeCountryCode='BR' and BillToAccountCode='1868550')
or (origin='HKG' and CustomerAccountCode='3510344' and CustomerName='WESTERN DIGITAL')
or(origin='MNL' and Destination='SIN' and ConsigneeAccountCode='FLEMAN0102' and BillToAccountCode='1868550')
or(origin in ('SYD','MEL') and ConsigneeName='TELEPLAN TECHNOLOGY SERVICES' and BillToAccountCode='AUCIIUS')
or (origin ='BLR' and CustomerAccountCode='6284593' and CustomerName='FLEXTRONICS TECHNOLOGIE INDIA P LTD' and BillToAccountCode='1868550')
or (origin='NRT' and Destination='SIN' and ConsigneeName='FLEXTRONICS MANUFACTURING' and BillToAccountCode='JPEGLATC')
or(origin='TPE' and Destination='SIN' and ConsigneeAccountCode='FLEMAN0102' and CustomerAccountCode='2641' and BillToAccountCode='3821518')
or(origin='SIN' and CustomerAccountCode='2319251')
or(origin='PVG' and CustomerName='WESTERN DIGITAL' and BillToAccountCode='CNCIIUSAC'))
and Hawb not in (select Hawb from ShipmentMaster where hawb is not null and hawb<>'')


order by CustomerName
