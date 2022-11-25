using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class SalesRegisterDrillDownReportViewModel
    {

        public SalesRegisterDrillDownReportViewModel()
        {
            SalesRegisterDrillDownReport = new SalesRegisterDrillDownReport();
            SalesRegisterDrillDownReportList = new List<SalesRegisterDrillDownReport>();
            SalesRegisterDrillDownReportDetailListForparticulars = new List<SalesRegisterDrillDownReport>();
            SalesRegisterDrillDownReportDTO = new SalesRegisterDrillDownReport();
            ListAccountSessionMaster = new List<AccountSessionMaster>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }
        public SalesRegisterDrillDownReport SalesRegisterDrillDownReportDTO
        { get; set; }

        public List<SalesRegisterDrillDownReport> SalesRegisterDrillDownReportList { get; set; }
        public List<SalesRegisterDrillDownReport> SalesRegisterDrillDownReportDetailListForparticulars { get; set; }

        public SalesRegisterDrillDownReport SalesRegisterDrillDownReport
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
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.ID > 0) ? SalesRegisterDrillDownReport.ID : new Int64();
            }
            set
            {
                SalesRegisterDrillDownReport.ID = value;
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
                return (SalesRegisterDrillDownReport != null) ? SalesRegisterDrillDownReport.IsDeleted : false;
            }
            set
            {
                SalesRegisterDrillDownReport.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.CreatedBy > 0) ? SalesRegisterDrillDownReport.CreatedBy : new int();
            }
            set
            {
                SalesRegisterDrillDownReport.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (SalesRegisterDrillDownReport != null) ? SalesRegisterDrillDownReport.CreatedDate : DateTime.Now;
            }
            set
            {
                SalesRegisterDrillDownReport.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.ModifiedBy > 0) ? SalesRegisterDrillDownReport.ModifiedBy : new int();
            }
            set
            {
                SalesRegisterDrillDownReport.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.ModifiedDate.HasValue) ? SalesRegisterDrillDownReport.ModifiedDate : DateTime.Now;
            }
            set
            {
                SalesRegisterDrillDownReport.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.DeletedBy > 0) ? SalesRegisterDrillDownReport.DeletedBy : new int();
            }
            set
            {
                SalesRegisterDrillDownReport.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (SalesRegisterDrillDownReport != null && SalesRegisterDrillDownReport.DeletedDate.HasValue) ? SalesRegisterDrillDownReport.DeletedDate : DateTime.Now;
            }
            set
            {
                SalesRegisterDrillDownReport.DeletedDate = value;
            }
        }



        public string errorMessage { get; set; }
    }
}

