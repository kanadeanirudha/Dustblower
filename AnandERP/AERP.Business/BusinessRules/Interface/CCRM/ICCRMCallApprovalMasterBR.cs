using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessRules
{
   public interface ICCRMCallApprovalMasterBR
    {
        //IValidateBusinessRuleResponse InsertCCRMCallApprovalMasterValidate(CCRMCallApprovalMaster item);
        IValidateBusinessRuleResponse UpdateCCRMCallApprovalMasterValidate(CCRMCallApprovalMaster item);
        IValidateBusinessRuleResponse DeleteCCRMCallApprovalMasterValidate(CCRMCallApprovalMaster item);
    }
}
