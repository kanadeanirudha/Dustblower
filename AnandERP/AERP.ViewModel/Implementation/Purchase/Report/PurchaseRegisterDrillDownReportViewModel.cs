using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class PurchaseRegisterDrillDownReportViewModel
    {

        public PurchaseRegisterDrillDownReportViewModel()
        {
            PurchaseRegisterDrillDownReport = new PurchaseRegisterDrillDownReport();
            PurchaseRegisterDrillDownReportList = new List<PurchaseRegisterDrillDownReport>();
            PurchaseRegisterDrillDownReportDetailListForparticulars = new List<PurchaseRegisterDrillDownReport>();
            PurchaseRegisterDrillDownReportDTO = new PurchaseRegisterDrillDownReport();
            ListAccountSessionMaster = new List<AccountSessionMaster>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }
        public PurchaseRegisterDrillDownReport PurchaseRegisterDrillDownReportDTO
        { get; set; }

        public List<PurchaseRegisterDrillDownReport> PurchaseRegisterDrillDownReportList { get; set; }
        public List<PurchaseRegisterDrillDownReport> PurchaseRegisterDrillDownReportDetailListForparticulars { get; set; }

        public PurchaseRegisterDrillDownReport PurchaseRegisterDrillDownReport
        {
            get;
            set;
        }

        public List<AccountSessionMaster> ListAccountSessionMaster { get; set; }

        public IEnumerable<SelectListItem> AccountSessionMasterItems
        {
            get
            {
                return new SelectList(ListAccountSessionMaster, "ID", "SessionName");
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

        public Int64 ID
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.ID > 0) ? PurchaseRegisterDrillDownReport.ID : new Int64();
            }
            set
            {
                PurchaseRegisterDrillDownReport.ID = value;
            }
        }
      
        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
        public string FromDate
        {
            get;
            set;
        }
        public string UptoDate
        {
            get;
            set;
        }
        public int AccountSessionID { get; set; }
        public string AccountSessionName { get; set; }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null) ? PurchaseRegisterDrillDownReport.IsDeleted : false;
            }
            set
            {
                PurchaseRegisterDrillDownReport.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.CreatedBy > 0) ? PurchaseRegisterDrillDownReport.CreatedBy : new int();
            }
            set
            {
                PurchaseRegisterDrillDownReport.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null) ? PurchaseRegisterDrillDownReport.CreatedDate : DateTime.Now;
            }
            set
            {
                PurchaseRegisterDrillDownReport.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.ModifiedBy > 0) ? PurchaseRegisterDrillDownReport.ModifiedBy : new int();
            }
            set
            {
                PurchaseRegisterDrillDownReport.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.ModifiedDate.HasValue) ? PurchaseRegisterDrillDownReport.ModifiedDate : DateTime.Now;
            }
            set
            {
                PurchaseRegisterDrillDownReport.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.DeletedBy > 0) ? PurchaseRegisterDrillDownReport.DeletedBy : new int();
            }
            set
            {
                PurchaseRegisterDrillDownReport.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (PurchaseRegisterDrillDownReport != null && PurchaseRegisterDrillDownReport.DeletedDate.HasValue) ? PurchaseRegisterDrillDownReport.DeletedDate : DateTime.Now;
            }
            set
            {
                PurchaseRegisterDrillDownReport.DeletedDate = value;
            }
        }



        public string errorMessage { get; set; }
    }
}

