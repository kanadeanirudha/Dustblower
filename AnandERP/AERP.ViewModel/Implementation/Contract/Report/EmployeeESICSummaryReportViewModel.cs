using AERP.DTO;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class EmployeeESICSummaryReportViewModel
    {

        public EmployeeESICSummaryReportViewModel()
        {
            EmployeeESICSummaryReport = new EmployeeESICSummaryReport();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
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

        public EmployeeESICSummaryReport EmployeeESICSummaryReport
        {
            get;
            set;
        }

        [Display(Name = "From Date")]
        public string FromDate
        {
            get
            {
                return (EmployeeESICSummaryReport != null) ? EmployeeESICSummaryReport.FromDate : string.Empty;
            }
            set
            {
                EmployeeESICSummaryReport.FromDate = value;
            }
        }
        [Display(Name = "Upto Date")]
        public string UptoDate
        {
            get
            {
                return (EmployeeESICSummaryReport != null) ? EmployeeESICSummaryReport.UptoDate : string.Empty;
            }
            set
            {
                EmployeeESICSummaryReport.UptoDate = value;
            }
        }
       
        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
       
        [Display(Name = "ESIC Zone")]
        public string ESICZone
        {
            get
            {
                return (EmployeeESICSummaryReport != null) ? EmployeeESICSummaryReport.ESICZone : string.Empty;
            }
            set
            {
                EmployeeESICSummaryReport.ESICZone = value;
            }
        }

        [Display(Name = "ESIC Zone")]
        public int ESICZoneID
        {
            get
            {
                return (EmployeeESICSummaryReport != null && EmployeeESICSummaryReport.ESICZoneID > 0) ? EmployeeESICSummaryReport.ESICZoneID : new int();
            }
            set
            {
                EmployeeESICSummaryReport.ESICZoneID = value;
            }
        }
    }
}

