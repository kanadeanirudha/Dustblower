using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessRules
{
   public interface ICCRMCallAllotmentMasterBR
    {
        //IValidateBusinessRuleResponse InsertCCRMCallAllotmentMasterValidate(CCRMCallAllotmentMaster item);
        IValidateBusinessRuleResponse UpdateCCRMCallAllotmentMasterValidate(CCRMCallAllotmentMaster item);
        IValidateBusinessRuleResponse DeleteCCRMCallAllotmentMasterValidate(CCRMCallAllotmentMaster item);
    }
}
