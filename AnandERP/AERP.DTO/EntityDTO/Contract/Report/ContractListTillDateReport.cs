using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class ContractListTillDateReport : BaseDTO
    {
        public string ContractNumber
        {
            get; set;
        }
        public string SalaryStatus
        {
            get; set;
        }
        public string InvoiceStatus
        {
            get; set;
        }
        public string SalaryMonth
        {
            get; set;
        }
        public string SalaryYear
        {
            get; set;
        }
        public string CentreCode
        {
            get; set;
        }
        public string StartDate
        {
            get; set;
        }
        public string EndDate
        {
            get; set;
        }
        public string CustomerName { get; set; }
        public string CustomerBranchName { get; set; }
        public string Narration { get; set; }
        public string PreviousContractNumber { get; set; }
        public string ContractStatus { get; set; }
    }
}
