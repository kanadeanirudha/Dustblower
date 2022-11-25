using AERP.Base.DTO;
namespace AERP.DTO
{
    public class ContractWisePFESICChecklistReportSearchRequest : Request
    {
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public string CentreCode { get; set; }
        public short CurrentESICZoneID { get; set; }
    }
}
