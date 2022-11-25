using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class AllAccountDrillDownReportViewModel
    {

        public AllAccountDrillDownReportViewModel()
        {
            AllAccountDrillDownReport = new AllAccountDrillDownReport();
            AllAccountDrillDownReportList = new List<AllAccountDrillDownReport>();
            AllAccountDrillDownReportDetailListForparticulars = new List<AllAccountDrillDownReport>();
            AllAccountDrillDownReportDTO = new AllAccountDrillDownReport();
            ListAccountSessionMaster = new List<AccountSessionMaster>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }
        public AllAccountDrillDownReport AllAccountDrillDownReportDTO
        { get; set; }

        public List<AllAccountDrillDownReport> AllAccountDrillDownReportList { get; set; }
        public List<AllAccountDrillDownReport> AllAccountDrillDownReportDetailListForparticulars { get; set; }

        public AllAccountDrillDownReport AllAccountDrillDownReport
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
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.ID > 0) ? AllAccountDrillDownReport.ID : new Int64();
            }
            set
            {
                AllAccountDrillDownReport.ID = value;
            }
        }
      
        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
        public string SessionFromDate
        {
            get;
            set;
        }
        public string SessionUptoDate
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
                return (AllAccountDrillDownReport != null) ? AllAccountDrillDownReport.IsDeleted : false;
            }
            set
            {
                AllAccountDrillDownReport.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.CreatedBy > 0) ? AllAccountDrillDownReport.CreatedBy : new int();
            }
            set
            {
                AllAccountDrillDownReport.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (AllAccountDrillDownReport != null) ? AllAccountDrillDownReport.CreatedDate : DateTime.Now;
            }
            set
            {
                AllAccountDrillDownReport.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.ModifiedBy > 0) ? AllAccountDrillDownReport.ModifiedBy : new int();
            }
            set
            {
                AllAccountDrillDownReport.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.ModifiedDate.HasValue) ? AllAccountDrillDownReport.ModifiedDate : DateTime.Now;
            }
            set
            {
                AllAccountDrillDownReport.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.DeletedBy > 0) ? AllAccountDrillDownReport.DeletedBy : new int();
            }
            set
            {
                AllAccountDrillDownReport.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (AllAccountDrillDownReport != null && AllAccountDrillDownReport.DeletedDate.HasValue) ? AllAccountDrillDownReport.DeletedDate : DateTime.Now;
            }
            set
            {
                AllAccountDrillDownReport.DeletedDate = value;
            }
        }



        public string errorMessage { get; set; }
    }
}

