
using AERP.DTO;

using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTEWayBillResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
        public string status_cd { get; set; }
        public Error error { get; set; }
        public string ErrorMessage { get; set; }
        public EWayBillDataResponse DataResponse { get; set; }
    }

    public class EWayBillDataResponse
    {
        public long EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
    }
}