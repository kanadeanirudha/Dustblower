using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessRules
{
   public interface ICCRMCalenderMasterBR
    {
        IValidateBusinessRuleResponse InsertCCRMCalenderMasterValidate(CCRMCalenderMaster item);
        //IValidateBusinessRuleResponse UpdateCCRMCalenderMasterValidate(CCRMCalenderMaster item);
        //IValidateBusinessRuleResponse DeleteCCRMCalenderMasterValidate(CCRMCalenderMaster item);
    }
}
