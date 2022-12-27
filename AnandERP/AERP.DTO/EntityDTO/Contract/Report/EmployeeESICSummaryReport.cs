using AERP.Base.DTO;

namespace AERP.DTO
{
    public class EmployeeESICSummaryReport : BaseDTO
    {
        public string FromDate
        {
            get; set;
        }
        public string UptoDate
        {
            get; set;
        }
        public string ESICZone
        {
            get; set;
        }
        public string CentreName
        {
            get; set;
        }
        public string CentreCode { get; set; }
        public string SalaryMonth
        {
            get; set;
        }
        public int WorkingDays
        {
            get; set;
        }
        public int cntEmployee
        {
            get; set;
        }
        public string SalaryYear
        {
            get; set;
        }
        public decimal Wages
        {
            get; set;
        }

        public decimal ESIC
        {
            get; set;
        }
        public decimal WorkersShare
        {
            get; set;
        }
        public decimal TotalShare
        {
            get; set;
        }
        public int ESICZoneID { get; set; }
    }
}
