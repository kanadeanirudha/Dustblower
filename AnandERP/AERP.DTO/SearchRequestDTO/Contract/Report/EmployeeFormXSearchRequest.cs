﻿using AERP.Base.DTO;
using System;
namespace AERP.DTO
{
    public class EmployeeFormXSearchRequest : Request
    {
        public string CentreCode
        {
            get;
            set;
        }
        public Int64 ContractNumber
        {
            get;
            set;
        }
        public Int64 SaleContractBillingSpanID
        {
            get;
            set;
        }
        public int SaleContractEmployeeMasterID
        {
            get;
            set;
        }

        public string SearchWord
        {
            get;
            set;
        }
        public string SearchFor
        {
            get;
            set;
        }
        public bool IsDeleted
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

        public int EndRow
        {
            get;
            set;
        }

        public int RowLength
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
        public string UnitCode
        {
            get;
            set;
        }
    }
}
