using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessAction
{
    public interface IEmployeeESICSummaryReportBA
    {
        IBaseEntityCollectionResponse<EmployeeESICSummaryReport> GetEmployeeESICSummaryReportDataList(EmployeeESICSummaryReportSearchRequest searchRequest);

    }
}
