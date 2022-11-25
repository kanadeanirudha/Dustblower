using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.Business.BusinessAction
{
    public interface IDashboardBA
    {
        IBaseEntityCollectionResponse<Dashboard> GetDashboardContentListByAdminRoleID(DashboardSearchRequest searchRequest);
        IBaseEntityCollectionResponse<Dashboard> GetDeshboardAllocationBySearch(DashboardSearchRequest searchRequest);
        IBaseEntityResponse<Dashboard> DeleteContaintAllocateStatus(Dashboard item);
        IBaseEntityResponse<Dashboard> InsertContaintAllocateStatus(Dashboard item);
        IBaseEntityCollectionResponse<Dashboard> GetDashboardRoleCodeList(DashboardSearchRequest searchRequest);
        IBaseEntityCollectionResponse<Dashboard> GetGeneralTaskModelListByPersonID(DashboardSearchRequest searchRequest);
    }
}
