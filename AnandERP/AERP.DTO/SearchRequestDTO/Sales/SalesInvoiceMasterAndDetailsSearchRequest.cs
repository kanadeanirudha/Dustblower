﻿using AERP.Base.DTO;

namespace AERP.DTO
{
    public class SalesInvoiceMasterAndDetailsSearchRequest : Request
    {
        public int ID
        {
            get;
            set;
        }
        public int AdminRoleID
        {
            get;
            set;
        }
        public int SalesOrderMasterID
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }
        
        public bool IsDeleted
        {
            get;
            set;
        }
        public string SearchWord
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
        public byte InvoiceType { get; set; }
        public int PersonID { get; set; }
        public int TaskNotificationMasterID { get; set; }
        public int GeneralTaskReportingDetailsID { get; set; }
        public string MonthName
        {
            get; set;
        }
        public string MonthYear { get; set; }
    }
}
