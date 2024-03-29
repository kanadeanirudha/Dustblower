﻿
using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessRules
{
    public interface IPurchaseReportMasterBR
    {
        // PurchaseReportMaster Method
        IValidateBusinessRuleResponse InsertPurchaseReportMasterValidate(PurchaseReportMaster item);
        IValidateBusinessRuleResponse UpdatePurchaseReportMasterValidate(PurchaseReportMaster item);
        IValidateBusinessRuleResponse DeletePurchaseReportMasterValidate(PurchaseReportMaster item);
    }
}
