﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessRules
{
    public interface IAdminSnPostsTransactionBR
    {
        IValidateBusinessRuleResponse InsertAdminSnPostsTransactionValidate(AdminSnPostsTransaction item);

        IValidateBusinessRuleResponse UpdateAdminSnPostsTransactionValidate(AdminSnPostsTransaction item);

        IValidateBusinessRuleResponse DeleteAdminSnPostsTransactionValidate(AdminSnPostsTransaction item);
    }
}
