using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class EmployeeFormX : BaseDTO
    {
        public Int64 ID
        {
            get;
            set;
        }
       
        public string CentreName
        {
            get; set;
        }
        public string EmployeeName
        {
            get; set;
        }
       
        public string CentreAdress
        {
            get; set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }

        public int DeletedBy
        {
            get;
            set;
        }

        public DateTime? DeletedDate
        {
            get;
            set;
        }
        public string errorMessage { get; set; }
        public Int64 SaleContractMasterID
        {
            get; set;
        }
        public string ContractNumber
        {
            get; set;
        }
        public Int64 SaleContractBillingSpanID
        {
            get;
            set;
        }
        public string SaleContractBillingSpanName
        {
            get;
            set;
        }
        public string EmployeeCode    {get;set;}
        public string GenderCode      {get;set;}
        public string Designation     {get;set;}
        public string WagePeriod      {get;set;}
        public string CustomerName    {get;set;}
        public string Customeraddress { get; set; }
        public string TenureOfEmp { get; set; }
    }
}
