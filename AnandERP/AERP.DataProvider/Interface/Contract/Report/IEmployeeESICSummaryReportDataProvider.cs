using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.DataProvider
{
    public interface IEmployeeESICSummaryReportDataProvider
    {
        IBaseEntityCollectionResponse<EmployeeESICSummaryReport> GetEmployeeESICSummaryReportDataList(EmployeeESICSummaryReportSearchRequest searchRequest);

    }
}
