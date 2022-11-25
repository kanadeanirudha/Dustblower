﻿using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class SaleContractWisePNLReport : BaseDTO
    {
        public Int64 ID
        {
            get;
            set;
        }

        public string CentreName
        {
            get; set;
        }
        public string EmployeeName
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
        public Int64 SaleContractMasterID { get; set; }
        public string ContractNumber { get; set; }
        public Int64 SaleContractBillingSpanID { get; set; }
        public string SaleContractBillingSpanName { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalIInvoiceAmount { get; set; }
        public decimal NetPayable { get; set; }
        public decimal PFAmount { get; set; }
        public decimal ESICAmount { get; set; }
        public decimal PTAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal Profit { get; set; }
        public decimal TotalPosting { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal NetProfit { get; set; }
        public decimal FOODAmount { get; set; }
        public decimal NonReimAmount { get; set; }
    }
}
