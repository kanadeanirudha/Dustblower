﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.Business.BusinessRules
{
    public interface IUserModuleMasterBR
    {
        IValidateBusinessRuleResponse InsertUserModuleMasterValidate(UserModuleMaster item);
        IValidateBusinessRuleResponse UpdateUserModuleMasterValidate(UserModuleMaster item);
        IValidateBusinessRuleResponse DeleteUserModuleMasterValidate(UserModuleMaster item);
    }
}
