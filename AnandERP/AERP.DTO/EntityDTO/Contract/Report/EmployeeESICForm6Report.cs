using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class EmployeeESICForm6Report : BaseDTO
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
        public string EmployeeName
        {
            get; set;
        }
        public string EmployeeCode
        {
            get; set;
        }
        public string ESICNumber
        {
            get; set;
        }
        public string ZoneCode
        {
            get;
            set;
        }
        public string FirstJoiningDate
        {
            get; set;
        }
        public string Month
        {
            get; set;
        }
        public byte WorkingDays
        {
            get; set;
        }
        public string Year
        {
            get; set;
        }
        public decimal Wages
        {
            get; set;
        }

        public decimal TotalEmployeersShareESIC
        {
            get; set;
        }
        public decimal TotalWorkersShareESIC
        {
            get; set;
        }
        public string CentreAdress
        {
            get; set;
        }
        public string Designation
        {
            get; set;
        }
        public int ESICZoneID { get; set; }
        public string SerialNo { get; set; }
    }
}
