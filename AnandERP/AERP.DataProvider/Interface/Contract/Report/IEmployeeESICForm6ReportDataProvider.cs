using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.DataProvider
{
    public interface IEmployeeESICForm6ReportDataProvider
    {
        IBaseEntityCollectionResponse<EmployeeESICForm6Report> GetEmployeeESICForm6ReportDataList(EmployeeESICForm6ReportSearchRequest searchRequest);

    }
}
