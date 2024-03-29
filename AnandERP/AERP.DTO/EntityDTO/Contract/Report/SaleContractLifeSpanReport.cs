﻿using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class SaleContractLifeSpanReport : BaseDTO
    {
        public int ID
        {
            get;
            set;
        }

        public string CustomerMasterName
        {
            get; set;
        }
        public string CustomerBranchMasterName
        {
            get; set;
        }
        public string ContractNumber
        {
            get; set;
        }
        public string ContractStartDate
        {
            get; set;
        }
        public string ContractEndDate
        {
            get; set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }

        public int DeletedBy
        {
            get;
            set;
        }

        public DateTime? DeletedDate
        {
            get;
            set;
        }
        public string errorMessage { get; set; }
    }
}
