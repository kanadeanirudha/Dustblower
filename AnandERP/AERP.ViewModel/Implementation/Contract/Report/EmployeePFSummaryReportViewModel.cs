using AERP.DTO;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class EmployeePFSummaryReportViewModel
    {

        public EmployeePFSummaryReportViewModel()
        {
            EmployeePFSummery = new EmployeePFSummery();
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

        public EmployeePFSummery EmployeePFSummery
        {
            get;
            set;
        }

        [Display(Name = "From Date")]
        public string FromDate
        {
            get
            {
                return (EmployeePFSummery != null) ? EmployeePFSummery.FromDate : string.Empty;
            }
            set
            {
                EmployeePFSummery.FromDate = value;
            }
        }
        [Display(Name = "Upto Date")]
        public string UptoDate
        {
            get
            {
                return (EmployeePFSummery != null) ? EmployeePFSummery.UptoDate : string.Empty;
            }
            set
            {
                EmployeePFSummery.UptoDate = value;
            }
        }
       
        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
    }
}

