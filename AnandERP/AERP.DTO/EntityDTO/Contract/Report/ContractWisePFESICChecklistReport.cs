using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class ContractWisePFESICChecklistReport : BaseDTO
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

        public string SiteName { get; set; }
        public decimal PFWages { get; set; }
        public decimal PFWorkersShare { get; set; }
        public decimal PFTotalShare { get; set; }
        public decimal PFDifference { get; set; }
        public decimal ESICWages { get; set; }
        public decimal ESICWorkersShare { get; set; }
        public decimal ESICTotalShare { get; set; }
        public decimal ESICDifference { get; set; }
        public decimal Acc01 { get; set; }
        public decimal Acc10 { get; set; }
        public decimal Acc02 { get; set; }
        public decimal Acc21 { get; set; }
        public decimal Acc22 { get; set; }
        public decimal ESIC { get; set; }
        public decimal TotalPFWorkerShare { get; set; }
        public decimal TotalESICWorkersShare { get; set; }
        public decimal TotalAcc01 { get; set; }
        public decimal TotalAcc10 { get; set; }
        public decimal TotalAcc02 { get; set; }
        public decimal TotalAcc21 { get; set; }
        public decimal TotalAcc22 { get; set; }
        public decimal TotalESICEmployerShare { get; set; }
        public decimal TotalPF { get; set; }
        public Int16 CurrentESICZoneID { get; set; }
    }
}
