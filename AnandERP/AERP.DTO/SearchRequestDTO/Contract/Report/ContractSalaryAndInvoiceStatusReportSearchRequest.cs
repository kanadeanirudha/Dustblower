using AERP.Base.DTO;
namespace AERP.DTO
{
    public class ContractSalaryAndInvoiceStatusReportSearchRequest : Request
    {
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public string CentreCode { get; set; }
    }
}
