using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class SalesInvoiceMasterCancelledReportViewModel
    {

        public SalesInvoiceMasterCancelledReportViewModel()
        {
            SalesInvoiceMasterCancelledReport = new SalesInvoiceMasterCancelledReport();
            SalesInvoiceMasterCancelledReportList = new List<SalesInvoiceMasterCancelledReport>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }
        public List<SalesInvoiceMasterCancelledReport> SalesInvoiceMasterCancelledReportList { get; set; }

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

        public SalesInvoiceMasterCancelledReport SalesInvoiceMasterCancelledReport
        {
            get;
            set;
        }

        public Int64 ID
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.ID > 0) ? SalesInvoiceMasterCancelledReport.ID : new Int64();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.ID = value;
            }
        }
        [Display(Name = "From Date")]
        public string FromDate
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.FromDate : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.FromDate = value;
            }
        }
        [Display(Name = "Upto Date")]
        public string UptoDate
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.UptoDate : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.UptoDate = value;
            }
        }
        [Display(Name = "Month")]
        public string MonthName
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.MonthName : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.MonthName = value;
            }
        }
        [Display(Name = "Month")]
        public string MonthFullName
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.MonthFullName : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.MonthFullName = value;
            }
        }
        [Display(Name = "Year")]
        public string MonthYear
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.MonthYear : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.MonthYear = value;
            }
        }

        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.IsDeleted : false;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.CreatedBy > 0) ? SalesInvoiceMasterCancelledReport.CreatedBy : new int();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.CreatedDate : DateTime.Now;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.ModifiedBy > 0) ? SalesInvoiceMasterCancelledReport.ModifiedBy : new int();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.ModifiedDate.HasValue) ? SalesInvoiceMasterCancelledReport.ModifiedDate : DateTime.Now;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.DeletedBy > 0) ? SalesInvoiceMasterCancelledReport.DeletedBy : new int();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.DeletedDate.HasValue) ? SalesInvoiceMasterCancelledReport.DeletedDate : DateTime.Now;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.DeletedDate = value;
            }
        }
        [Display(Name = "Employee Name")]
        public Int32 SaleContractEmployeeMasterID
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.SaleContractEmployeeMasterID > 0) ? SalesInvoiceMasterCancelledReport.SaleContractEmployeeMasterID : new Int32();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.SaleContractEmployeeMasterID = value;
            }
        }
        [Display(Name = "Employee Name")]
        public string SaleContractEmployeeMasterName
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null) ? SalesInvoiceMasterCancelledReport.SaleContractEmployeeMasterName : string.Empty;
            }
            set
            {
                SalesInvoiceMasterCancelledReport.SaleContractEmployeeMasterName = value;
            }
        }

        [Display(Name = "Account Session")]
        public int AccountSessionID
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.AccountSessionID > 0) ? SalesInvoiceMasterCancelledReport.AccountSessionID : new int();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.AccountSessionID = value;
            }
        }

        [Display(Name = "ESIC Zone")]
        public int ESICZoneID
        {
            get
            {
                return (SalesInvoiceMasterCancelledReport != null && SalesInvoiceMasterCancelledReport.ESICZoneID > 0) ? SalesInvoiceMasterCancelledReport.ESICZoneID : new int();
            }
            set
            {
                SalesInvoiceMasterCancelledReport.ESICZoneID = value;
            }
        }
        
        public string errorMessage { get; set; }
    }
}

