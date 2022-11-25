using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class EmployeeFormXViewModel
    {

        public EmployeeFormXViewModel()
        {
            EmployeeFormX = new EmployeeFormX();
            EmployeeFormXList = new List<EmployeeFormX>();
            EmployeeFormXDetailListForparticulars = new List<EmployeeFormX>();
            EmployeeFormXDTO = new EmployeeFormX();

        }
        public EmployeeFormX EmployeeFormXDTO
        { get; set; }

        public List<EmployeeFormX> EmployeeFormXList { get; set; }
        public List<EmployeeFormX> EmployeeFormXDetailListForparticulars { get; set; }

        public EmployeeFormX EmployeeFormX
        {
            get;
            set;
        }

        public Int64 ID
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.ID > 0) ? EmployeeFormX.ID : new Int64();
            }
            set
            {
                EmployeeFormX.ID = value;
            }
        }
       

        public bool IsPosted { get; set; }
        public string CentreCode
        {
            get;
            set;
        }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get
            {
                return (EmployeeFormX != null) ? EmployeeFormX.IsDeleted : false;
            }
            set
            {
                EmployeeFormX.IsDeleted = value;
            }
        }

        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.CreatedBy > 0) ? EmployeeFormX.CreatedBy : new int();
            }
            set
            {
                EmployeeFormX.CreatedBy = value;
            }
        }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get
            {
                return (EmployeeFormX != null) ? EmployeeFormX.CreatedDate : DateTime.Now;
            }
            set
            {
                EmployeeFormX.CreatedDate = value;
            }
        }

        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.ModifiedBy > 0) ? EmployeeFormX.ModifiedBy : new int();
            }
            set
            {
                EmployeeFormX.ModifiedBy = value;
            }
        }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.ModifiedDate.HasValue) ? EmployeeFormX.ModifiedDate : DateTime.Now;
            }
            set
            {
                EmployeeFormX.ModifiedDate = value;
            }
        }

        [Display(Name = "Deleted By")]
        public int DeletedBy
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.DeletedBy > 0) ? EmployeeFormX.DeletedBy : new int();
            }
            set
            {
                EmployeeFormX.DeletedBy = value;
            }
        }

        [Display(Name = "DeletedDate")]
        public DateTime? DeletedDate
        {
            get
            {
                return (EmployeeFormX != null && EmployeeFormX.DeletedDate.HasValue) ? EmployeeFormX.DeletedDate : DateTime.Now;
            }
            set
            {
                EmployeeFormX.DeletedDate = value;
            }
        }
        [Display(Name = "Contract Number")]
        public Int64 SaleContractMasterID
        {
            get
            {
                return (EmployeeFormXDTO != null && EmployeeFormXDTO.SaleContractMasterID > 0) ? EmployeeFormXDTO.SaleContractMasterID : new Int64();
            }
            set
            {
                EmployeeFormXDTO.SaleContractMasterID = value;
            }
        }
        [Display(Name = "Contract Number")]
        public string ContractNumber
        {
            get
            {
                return (EmployeeFormXDTO != null) ? EmployeeFormXDTO.ContractNumber : string.Empty;
            }
            set
            {
                EmployeeFormXDTO.ContractNumber = value;
            }
        }
        [Display(Name = "Billing Span")]
        public Int64 SaleContractBillingSpanID
        {
            get
            {
                return (EmployeeFormXDTO != null && EmployeeFormXDTO.SaleContractBillingSpanID > 0) ? EmployeeFormXDTO.SaleContractBillingSpanID : new Int64();
            }
            set
            {
                EmployeeFormXDTO.SaleContractBillingSpanID = value;
            }
        }
        [Display(Name = "Billing Span")]
        public string SaleContractBillingSpanName
        {
            get
            {
                return (EmployeeFormXDTO != null) ? EmployeeFormXDTO.SaleContractBillingSpanName : string.Empty;
            }
            set
            {
                EmployeeFormXDTO.SaleContractBillingSpanName = value;
            }
        }


        public string errorMessage { get; set; }
    }
}

