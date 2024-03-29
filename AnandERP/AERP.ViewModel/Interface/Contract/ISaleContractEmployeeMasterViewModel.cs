﻿using AERP.DTO;
using System;
using System.Collections.Generic;

namespace AERP.ViewModel
{
    public interface ISaleContractEmployeeMasterViewModel
    {
        SaleContractEmployeeMaster SaleContractEmployeeMasterDTO
        {
            get;
            set;
        }
         int ID { get; set; }
         bool IsDeleted { get; set; }
         int CreatedBy { get; set; }
         DateTime CreatedDate { get; set; }
         int ModifiedBy { get; set; }
         Nullable<System.DateTime> ModifiedDate { get; set; }
         int DeletedBy { get; set; }
         Nullable<System.DateTime> DeletedDate { get; set; }
          string errorMessage { get; set; }
    }
}
