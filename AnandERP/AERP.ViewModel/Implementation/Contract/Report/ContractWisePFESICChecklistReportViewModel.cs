using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class ContractWisePFESICChecklistReportViewModel
    {

        public ContractWisePFESICChecklistReportViewModel()
        {
            ContractWisePFESICChecklistReportDTO = new ContractWisePFESICChecklistReport();
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

        public ContractWisePFESICChecklistReport ContractWisePFESICChecklistReportDTO
        {
            get;
            set;
        }

        [Display(Name = "Contract Number")]
        public string ContractNumber
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.ContractNumber : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.ContractNumber = value;
            }
        }
        [Display(Name = "Salary Status")]
        public string SalaryStatus
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.SalaryStatus : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.SalaryStatus = value;
            }
        }

        public bool IsPosted { get; set; }
        [Display(Name = "Invoice Status")]
        public string InvoiceStatus
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.InvoiceStatus : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.InvoiceStatus = value;
            }
        }
        [Display(Name = "Month")]
        public string SalaryMonth
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.SalaryMonth : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.SalaryMonth = value;
            }
        }
        [Display(Name = "Year")]
        public string SalaryYear
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.SalaryYear : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.SalaryYear = value;
            }
        }

        [Display(Name = "Centre")]
        public string CentreCode
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null) ? ContractWisePFESICChecklistReportDTO.CentreCode : string.Empty;
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.CentreCode = value;
            }
        }

        [Display(Name = "Zone")]
        public Int16 CurrentESICZoneID
        {
            get
            {
                return (ContractWisePFESICChecklistReportDTO != null && ContractWisePFESICChecklistReportDTO.CurrentESICZoneID > 0) ? ContractWisePFESICChecklistReportDTO.CurrentESICZoneID : new Int16();
            }
            set
            {
                ContractWisePFESICChecklistReportDTO.CurrentESICZoneID = value;
            }
        }
    }
}

