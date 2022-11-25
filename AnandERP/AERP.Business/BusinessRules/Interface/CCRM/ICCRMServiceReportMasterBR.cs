using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.DTO;


namespace AERP.Business.BusinessRules
{
    public interface ICCRMServiceReportMasterBR
    {
        IValidateBusinessRuleResponse UpdateCCRMServiceReportMasterValidate(CCRMServiceReportMaster item);
        IValidateBusinessRuleResponse DeleteCCRMServiceReportMasterValidate(CCRMServiceReportMaster item);
    }
}
