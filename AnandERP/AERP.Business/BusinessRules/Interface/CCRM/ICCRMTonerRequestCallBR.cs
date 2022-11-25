using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.DTO;

namespace AERP.Business.BusinessRules
{
   public interface ICCRMTonerRequestCallBR
    {
        IValidateBusinessRuleResponse InsertCCRMTonerRequestCallValidate(CCRMTonerRequestCall item);
       // IValidateBusinessRuleResponse UpdateCCRMTonerRequestCallValidate(CCRMTonerRequestCall item);
       // IValidateBusinessRuleResponse DeleteCCRMTonerRequestCallValidate(CCRMTonerRequestCall item);
    }
}
