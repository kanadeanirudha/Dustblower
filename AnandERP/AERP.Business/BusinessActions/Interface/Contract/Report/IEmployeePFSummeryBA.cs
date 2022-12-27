using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessAction
{
    public interface IEmployeePFSummeryBA
    {
        IBaseEntityCollectionResponse<EmployeePFSummery> GetEmployeePFSummeryDataList(EmployeePFSummerySearchRequest searchRequest);
    }
}
