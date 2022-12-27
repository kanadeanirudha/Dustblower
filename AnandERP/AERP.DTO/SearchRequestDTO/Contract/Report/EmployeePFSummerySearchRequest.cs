using AERP.Base.DTO;
namespace AERP.DTO
{
    public class EmployeePFSummerySearchRequest : Request
    {
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
        public string FromDate
        {
            get;
            set;
        }
        public string UptoDate
        {
            get;
            set;
        }
    }
}
