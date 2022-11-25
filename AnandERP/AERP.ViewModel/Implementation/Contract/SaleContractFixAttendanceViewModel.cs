using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class SaleContractFixAttendanceViewModel : ISaleContractFixAttendanceViewModel
    {

        public SaleContractFixAttendanceViewModel()
        {
            SaleContractFixAttendanceDTO = new SaleContractFixAttendance();
            SaleContractSpanList = new List<SaleContractAttendance>();
            SaleContractFixAttendanceList = new List<SaleContractFixAttendance>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }

        public List<SaleContractAttendance> SaleContractSpanList { get; set; }
        public List<SaleContractFixAttendance> SaleContractFixAttendanceList { get; set; }

        public IEnumerable<SelectListItem> ListGetContractSpans
        {
            get
            {
                return new SelectList(SaleContractSpanList, "SaleContractBillingSpanID", "SaleContractBillingSpanName");
            }
        }

        public List<AdminRoleApplicableDetails> ListGetAdminRoleApplicableCentre
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> ListGetAdminRoleApplicableCentreItems
        {
            get
            {
                return new SelectList(ListGetAdminRoleApplicableCentre, "CentreCode", "CentreName");
            }
        }

        public SaleContractFixAttendance SaleContractFixAttendanceDTO
        {
            get;
            set;
        }

        public Int64 ID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.ID > 0) ? SaleContractFixAttendanceDTO.ID : new Int64();
            }
            set
            {
                SaleContractFixAttendanceDTO.ID = value;
            }
        }
        [Display(Name ="Contract Number")]
        public Int64 SaleContractMasterID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.SaleContractMasterID > 0) ? SaleContractFixAttendanceDTO.SaleContractMasterID : new Int64();
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractMasterID = value;
            }
        }
        [Display(Name = "Customer Branch")]
        public Int32 CustomerBranchMasterID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.CustomerBranchMasterID > 0) ? SaleContractFixAttendanceDTO.CustomerBranchMasterID : new Int32();
            }
            set
            {
                SaleContractFixAttendanceDTO.CustomerBranchMasterID = value;
            }
        }
        [Display(Name = "Customer Branch")]
        public string CustomerBranchMasterName
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.CustomerBranchMasterName : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.CustomerBranchMasterName = value;
            }
        }
        [Display(Name = "Customer")]
        public Int32 CustomerMasterID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.CustomerMasterID > 0) ? SaleContractFixAttendanceDTO.CustomerMasterID : new Int32();
            }
            set
            {
                SaleContractFixAttendanceDTO.CustomerMasterID = value;
            }
        }
        [Display(Name = "Customer")]
        public string CustomerMasterName
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.CustomerMasterName : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.CustomerMasterName = value;
            }
        }
        [Display(Name = "Contract Number")]
        public string ContractNumber
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.ContractNumber : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.ContractNumber = value;
            }
        }
        [Display(Name = "Job Work Item")]
        public Int32 SaleContractFixItemID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.SaleContractFixItemID > 0) ? SaleContractFixAttendanceDTO.SaleContractFixItemID : new Int32();
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractFixItemID = value;
            }
        }
        [Display(Name = "Job Work Item")]
        public string SaleContractFixItemName
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.SaleContractFixItemName : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractFixItemName = value;
            }
        }
        [Display(Name = "Billing Span")]
        public Int64 SaleContractBillingSpanID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.SaleContractBillingSpanID > 0) ? SaleContractFixAttendanceDTO.SaleContractBillingSpanID : new Int64();
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractBillingSpanID = value;
            }
        }
        [Display(Name = "Billing Span")]
        public string SaleContractBillingSpanName
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.SaleContractBillingSpanName : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractBillingSpanName = value;
            }
        }
        [Display(Name = "Total Days")]
        public decimal SaleContractFixItemQuantity
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.SaleContractFixItemQuantity > 0) ? SaleContractFixAttendanceDTO.SaleContractFixItemQuantity : new decimal();
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractFixItemQuantity = value;
            }
        }
        [Display(Name = "Total Days")]
        public decimal SaleContractFixItemAttendance
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.SaleContractFixItemAttendance> 0) ? SaleContractFixAttendanceDTO.SaleContractFixItemAttendance : new decimal();
            }
            set
            {
                SaleContractFixAttendanceDTO.SaleContractFixItemAttendance = value;
            }
        }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.IsDeleted : false;
            }
            set
            {
                SaleContractFixAttendanceDTO.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.CreatedBy > 0) ? SaleContractFixAttendanceDTO.CreatedBy : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.CreatedDate : DateTime.Now;
            }
            set
            {
                SaleContractFixAttendanceDTO.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.ModifiedBy > 0) ? SaleContractFixAttendanceDTO.ModifiedBy : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.ModifiedDate.HasValue) ? SaleContractFixAttendanceDTO.ModifiedDate : DateTime.Now;
            }
            set
            {
                SaleContractFixAttendanceDTO.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.DeletedBy > 0) ? SaleContractFixAttendanceDTO.DeletedBy : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.DeletedDate.HasValue) ? SaleContractFixAttendanceDTO.DeletedDate : DateTime.Now;
            }
            set
            {
                SaleContractFixAttendanceDTO.DeletedDate = value;
            }
        }

        public string errorMessage { get; set; }
        public string XMLstringForFixItemData { get; set; }

        [Display(Name = "Centre")]
        public string CentreCode
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.CentreCode : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.CentreCode = value;
            }
        }

        public int PersonID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.PersonID > 0) ? SaleContractFixAttendanceDTO.PersonID : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.PersonID = value;
            }
        }
        public int TaskNotificationDetailsID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.TaskNotificationDetailsID > 0) ? SaleContractFixAttendanceDTO.TaskNotificationDetailsID : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.TaskNotificationDetailsID = value;
            }
        }
        public int TaskNotificationMasterID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.TaskNotificationMasterID > 0) ? SaleContractFixAttendanceDTO.TaskNotificationMasterID : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.TaskNotificationMasterID = value;
            }
        }
        public int GeneralTaskReportingDetailsID
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.GeneralTaskReportingDetailsID > 0) ? SaleContractFixAttendanceDTO.GeneralTaskReportingDetailsID : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.GeneralTaskReportingDetailsID = value;
            }
        }
        public int StageSequenceNumber
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.StageSequenceNumber > 0) ? SaleContractFixAttendanceDTO.StageSequenceNumber : new int();
            }
            set
            {
                SaleContractFixAttendanceDTO.StageSequenceNumber = value;
            }
        }
        public bool IsLastRecord
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.IsLastRecord : new bool();
            }
            set
            {
                SaleContractFixAttendanceDTO.IsLastRecord = value;
            }
        }
        public byte ApprovedStatus
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.ApprovedStatus > 0) ? SaleContractFixAttendanceDTO.ApprovedStatus : new byte();
            }
            set
            {
                SaleContractFixAttendanceDTO.ApprovedStatus = value;
            }
        }
        public byte ApprovalStatus
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null && SaleContractFixAttendanceDTO.ApprovalStatus > 0) ? SaleContractFixAttendanceDTO.ApprovalStatus : new byte();
            }
            set
            {
                SaleContractFixAttendanceDTO.ApprovalStatus = value;
            }
        }
        public string TaskCode
        {
            get
            {
                return (SaleContractFixAttendanceDTO != null) ? SaleContractFixAttendanceDTO.TaskCode : string.Empty;
            }
            set
            {
                SaleContractFixAttendanceDTO.TaskCode = value;
            }
        }
    }
}

