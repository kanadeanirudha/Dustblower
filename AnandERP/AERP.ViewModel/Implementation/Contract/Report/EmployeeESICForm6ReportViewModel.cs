using AERP.DTO;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class EmployeeESICForm6ReportViewModel
    {

        public EmployeeESICForm6ReportViewModel()
        {
            EmployeeESICForm6Report = new EmployeeESICForm6Report();
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

        public EmployeeESICForm6Report EmployeeESICForm6Report
        {
            get;
            set;
        }

        [Display(Name = "From Date")]
        public string FromDate
        {
            get
            {
                return (EmployeeESICForm6Report != null) ? EmployeeESICForm6Report.FromDate : string.Empty;
            }
            set
            {
                EmployeeESICForm6Report.FromDate = value;
            }
        }
        [Display(Name = "Upto Date")]
        public string UptoDate
        {
            get
            {
                return (EmployeeESICForm6Report != null) ? EmployeeESICForm6Report.UptoDate : string.Empty;
            }
            set
            {
                EmployeeESICForm6Report.UptoDate = value;
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
                return (EmployeeESICForm6Report != null) ? EmployeeESICForm6Report.ESICZone : string.Empty;
            }
            set
            {
                EmployeeESICForm6Report.ESICZone = value;
            }
        }

        [Display(Name = "ESIC Zone")]
        public int ESICZoneID
        {
            get
            {
                return (EmployeeESICForm6Report != null && EmployeeESICForm6Report.ESICZoneID > 0) ? EmployeeESICForm6Report.ESICZoneID : new int();
            }
            set
            {
                EmployeeESICForm6Report.ESICZoneID = value;
            }
        }
    }
}

