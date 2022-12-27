using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessAction
{
    public interface IEmployeeESICForm6ReportBA
    {
        IBaseEntityCollectionResponse<EmployeeESICForm6Report> GetEmployeeESICForm6ReportDataList(EmployeeESICForm6ReportSearchRequest searchRequest);

    }
}
