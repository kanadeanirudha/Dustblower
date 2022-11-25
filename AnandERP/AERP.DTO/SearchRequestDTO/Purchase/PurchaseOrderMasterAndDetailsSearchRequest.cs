﻿using AERP.Base.DTO;
using System;
namespace AERP.DTO
{
    public class PurchaseOrderMasterAndDetailsSearchRequest : Request
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
        public int PurchaseRequisitionMasterID
        {
            get;
            set;
        }
        public string PurchaseOrderNumber
        {
            get;
            set;
        }
        public string PurchaseOrderDate
        {
            get;
            set;
        }
        public int VendorID
        {
            get;
            set;
        }
        public Int16 PurchaseOrderType
        {
            get;
            set;
        }

        public string PurchaseOrderFromDate
        {
            get;set;
        }
        public string PurchaseOrderUptoDate { get; set; }

        //------------------------------Purchase order details--------------------------------//
        public int PurchaseOrderDetailsID
        {
            get;
            set;
        }
        public int PurchaseRequisitionDetailsID
        {
            get;
            set;
        }       
        public int ItemID
        {
            get;
            set;
        }
        public decimal Quantity
        {
            get;
            set;
        }
        public decimal Rate
        {
            get;
            set;
        }
        public int DepartmentID
        {
            get;
            set;
        }
        public int StorageLocationID
        {
            get;
            set;
        }
        public int IssueFromLocationID
        {
            get;
            set;
        }
        public string ExpectedDeliveryDate
        {
            get;
            set;
        }
        public Int16 PriorityFlag
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
        #region -------- TaskNotification --------
        public int PersonID
        {
            get;
            set;
        }
        public int TaskNotificationMasterID
        {
            get;
            set;
        }
        public int GeneralTaskReportingDetailsID
        { get; set; }

        #endregion
    }
}
