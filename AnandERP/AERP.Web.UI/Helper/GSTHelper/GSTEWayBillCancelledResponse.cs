
using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTEWayBillCancelledResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public string ewayBillNo { get; set; }
        public string cancelDate { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; } = null;
        public object InfoDtls { get; set; }
        public string status_cd { get; set; }
        public Error error { get; set; }
        public string ErrorMessage { get; set; }
    }
}