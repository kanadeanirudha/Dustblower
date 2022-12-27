using AERP.Base.DTO;

using System;

namespace AERP.DTO
{
    public class EmployeePFSummery : BaseDTO
    {
        public int EmployeeCnt { get; set; }
        public int EmployeeNotAgeCnt { get; set; }
        public string FromDate
        {
            get; set;
        }
        public string UptoDate
        {
            get; set;
        }
        public string CentreName
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
        public decimal TotalWagesAmount
        {
            get; set;
        }
        public decimal TotalNotAgedWagesAmount
        {
            get; set;
        }
        public decimal WorkersShare
        {
            get; set;
        }
        public string CentreAddress
        {
            get; set;
        }
        public decimal Acc01
        {
            get; set;
        }
        public decimal Acc02
        {
            get; set;
        }
        public decimal Acc10
        {
            get; set;
        }
        public decimal Acc21
        {
            get; set;
        }
        public decimal Acc22
        {
            get; set;
        }
    }
}
