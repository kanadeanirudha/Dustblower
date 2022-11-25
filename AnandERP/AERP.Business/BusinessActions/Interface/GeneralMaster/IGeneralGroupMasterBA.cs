using AMS.Base.DTO;
using AMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Business.BusinessActions
{
    public interface IGeneralGroupMasterBA
    {
        IBaseEntityResponse<GeneralGroupMaster> InsertGeneralGroupMaster(GeneralGroupMaster item);
        IBaseEntityResponse<GeneralGroupMaster> UpdateGeneralGroupMaster(GeneralGroupMaster item);
        IBaseEntityResponse<GeneralGroupMaster> DeleteGeneralGroupMaster(GeneralGroupMaster item);
        IBaseEntityCollectionResponse<GeneralGroupMaster> GetBySearch(GeneralGroupMasterSearchRequest searchRequest);
        IBaseEntityResponse<GeneralGroupMaster> SelectByID(GeneralGroupMaster item);
        IBaseEntityResponse<GeneralGroupMaster> InsertGroupDetails(GeneralGroupMaster item);
        IBaseEntityCollectionResponse<GeneralGroupMaster> EmployeeGroupDetailsGetBySearch(GeneralGroupMasterSearchRequest searchRequest);
        IBaseEntityResponse<GeneralGroupMaster> SelectEmployeeGroupDetailsByID(GeneralGroupMaster item);
        IBaseEntityResponse<GeneralGroupMaster> UpdateGroupDetails(GeneralGroupMaster item);        
    }
}
