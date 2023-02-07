USE [Ver1]
GO
/****** Object:  UserDefinedFunction [dbo].[fGetTotalAttendance]    Script Date: 2/7/2023 9:13:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 ALTER   function [dbo].[fGetTotalAttendance]
(@EmployeeId int,@iSaleContractBillingSpanID bigint,@EndDate date) returns nvarchar(100) 
AS
BEGIN
Declare @TotalAttendance nVarchar(100);
;
with cteSaleContractBillingSpan as 
(
select 
	A.SaleContractMasterID, A.ID as SaleContractBillingSpanId , month(EndDate) as SalMonth, YEAR(EndDate) as SalYear 
from 
	SaleContractBillingSpan A
where  cast (datepart(YEAR, EndDate) as  varchar(5)) +  cast(datepart(Month, EndDate)as varchar(2) ) =cast (datepart(YEAR, @EndDate) as  varchar(5)) +  cast(datepart(Month, @EndDate)as varchar(2) )
      and A.ID != @iSaleContractBillingSpanID
--and isbillgenerated=1
), cteFianl as 
(
select
	A.SaleContractEmployeeMasterID, 
	B.SalMonth, 
	B.SalYear,
	isnull(sum(A.TotalAttendance),0) as TotalAttendance
from 
	SaleContractAttendance A 
inner join cteSaleContractBillingSpan B on (A.SaleContractBillingSpanID=b.SaleContractBillingSpanId and A.SaleContractEmployeeMasterID=@EmployeeId and A.ApprovalStatus=2) 
group by A.SaleContractEmployeeMasterID, SalYear,SalMonth
)
select @TotalAttendance= cast( TotalAttendance as  varchar(100)) +'~'
 from
cteFianl

return isnull(@TotalAttendance,'0~')
end