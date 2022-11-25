using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class ContractSalaryAndInvoiceStatusReportViewModel
    {

        public ContractSalaryAndInvoiceStatusReportViewModel()
        {
            ContractSalaryAndInvoiceStatusReportDTO = new ContractSalaryAndInvoiceStatusReport();
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

        public ContractSalaryAndInvoiceStatusReport ContractSalaryAndInvoiceStatusReportDTO
        {
            get;
            set;
        }

        [Display(Name = "Contract Number")]
        public string ContractNumber
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.ContractNumber : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.ContractNumber = value;
            }
        }
        [Display(Name = "Salary Status")]
        public string SalaryStatus
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.SalaryStatus : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.SalaryStatus = value;
            }
        }

        public bool IsPosted { get; set; }
        [Display(Name = "Invoice Status")]
        public string InvoiceStatus
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.InvoiceStatus : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.InvoiceStatus = value;
            }
        }
        [Display(Name = "Month")]
        public string SalaryMonth
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.SalaryMonth : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.SalaryMonth = value;
            }
        }
        [Display(Name = "Year")]
        public string SalaryYear
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.SalaryYear : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.SalaryYear = value;
            }
        }

        [Display(Name = "Centre")]
        public string CentreCode
        {
            get
            {
                return (ContractSalaryAndInvoiceStatusReportDTO != null) ? ContractSalaryAndInvoiceStatusReportDTO.CentreCode : string.Empty;
            }
            set
            {
                ContractSalaryAndInvoiceStatusReportDTO.CentreCode = value;
            }
        }
    }
}

