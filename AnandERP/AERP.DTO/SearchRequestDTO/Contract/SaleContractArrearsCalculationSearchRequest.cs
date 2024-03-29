﻿using AERP.Base.DTO;

namespace AERP.DTO
{
    public class SaleContractArrearsCalculationSearchRequest : Request
    {
        public long ID
        {
            get;
            set;
        }
        public long SaleContractBillingSpanID
        {
            get; set;
        }
        public long SaleContractMasterID
        {
            get; set;
        }
        public long SaleContractEmployeeMasterID
        {
            get;set;
        }
        public int SaleContractManPowerItemID
        {
            get;set;
        }
        public string SearchWord
        {
            get; set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        public string SearchBy
        {
            get;
            set;
        }
        public string SortDirection
        {
            get;
            set;
        }
        public string SortOrder
        {
            get;
            set;
        }
        public string SortBy
        {
            get;
            set;
        }
        public int StartRow
        {
            get;
            set;
        }
        public int RowLength
        {
            get;
            set;
        }

        public int EndRow
        {
            get;
            set;
        }
        public byte SummaryFormat { get; set; }
    }
}
