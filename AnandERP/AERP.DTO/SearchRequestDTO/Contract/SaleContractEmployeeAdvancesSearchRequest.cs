﻿using AERP.Base.DTO;

namespace AERP.DTO
{
    public class SaleContractEmployeeAdvancesSearchRequest : Request
    {
        public int ID
        {
            get;
            set;
        }
        public int ContractEmployeeMasterID
        {
            get;
            set;
        }
        public string SearchWord
        {
            get;set;
        }
        public int CustomerMasterID
        {
            get;set;
        }
        public int CustomerBranchMasterID
        {
            get;set;
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
    }
}
