using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessAction
{
    public interface IEmployeeFormIIWagesSheetReportBA
    {
        IBaseEntityCollectionResponse<EmployeeFormIIWagesSheetReport> GetEmployeeFormIIWagesSheetReportDataList(EmployeeFormIIWagesSheetReportSearchRequest searchRequest);

    }
}
