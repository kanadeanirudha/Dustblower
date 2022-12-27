using AERP.Base.DTO;
namespace AERP.DTO
{
    public class EmployeeESICForm6ReportSearchRequest : Request
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
        public int ESICZoneID
        {
            get;set;
        }
        public string ESICZone
        {
            get;
            set;
        }
    }
}
