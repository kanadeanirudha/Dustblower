using AMS.Base.DTO;
using AMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Business.BusinessRules
{
    public interface IGeneralGroupMasterBR
    {
        IValidateBusinessRuleResponse InsertGeneralGroupMasterValidate(GeneralGroupMaster item);
        IValidateBusinessRuleResponse UpdateGeneralGroupMasterValidate(GeneralGroupMaster item);
        IValidateBusinessRuleResponse DeleteGeneralGroupMasterValidate(GeneralGroupMaster item);
        IValidateBusinessRuleResponse InsertGroupDetailsValidate(GeneralGroupMaster item);        
    }
}
