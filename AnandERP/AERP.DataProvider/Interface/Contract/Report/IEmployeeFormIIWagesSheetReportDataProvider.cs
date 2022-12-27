using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.DataProvider
{
    public interface IEmployeeFormIIWagesSheetReportDataProvider
    {
        IBaseEntityCollectionResponse<EmployeeFormIIWagesSheetReport> GetEmployeeFormIIWagesSheetReportDataList(EmployeeFormIIWagesSheetReportSearchRequest searchRequest);

    }
}
