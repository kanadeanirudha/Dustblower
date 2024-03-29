﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.Business.BusinessRules
{
    public interface IAccountCentreOpeningBalanceBR
    {
        IValidateBusinessRuleResponse InsertAccountCentreOpeningBalanceValidate(AccountCentreOpeningBalance item);
        IValidateBusinessRuleResponse UpdateAccountCentreOpeningBalanceValidate(AccountCentreOpeningBalance item);
        IValidateBusinessRuleResponse DeleteAccountCentreOpeningBalanceValidate(AccountCentreOpeningBalance item);
    }
}
