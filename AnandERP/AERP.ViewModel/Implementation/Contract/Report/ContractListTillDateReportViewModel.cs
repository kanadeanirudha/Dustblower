using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class ContractListTillDateReportViewModel
    {

        public ContractListTillDateReportViewModel()
        {
            ContractListTillDateReportDTO = new ContractListTillDateReport();
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

        public ContractListTillDateReport ContractListTillDateReportDTO
        {
            get;
            set;
        }

        [Display(Name = "Contract Number")]
        public string ContractNumber
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.ContractNumber : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.ContractNumber = value;
            }
        }
        [Display(Name = "Salary Status")]
        public string SalaryStatus
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.SalaryStatus : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.SalaryStatus = value;
            }
        }

        public bool IsPosted { get; set; }
        [Display(Name = "Invoice Status")]
        public string InvoiceStatus
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.InvoiceStatus : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.InvoiceStatus = value;
            }
        }
        [Display(Name = "Month")]
        public string SalaryMonth
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.SalaryMonth : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.SalaryMonth = value;
            }
        }
        [Display(Name = "Year")]
        public string SalaryYear
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.SalaryYear : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.SalaryYear = value;
            }
        }

        [Display(Name = "Centre")]
        public string CentreCode
        {
            get
            {
                return (ContractListTillDateReportDTO != null) ? ContractListTillDateReportDTO.CentreCode : string.Empty;
            }
            set
            {
                ContractListTillDateReportDTO.CentreCode = value;
            }
        }
    }
}

