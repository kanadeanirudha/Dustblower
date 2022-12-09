
using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTInvoiceCancelledResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
        public string status_cd { get; set; }
        public Error error { get; set; }
        public string ErrorMessage { get; set; }
        public string Irn { get; set; }
        public string CancelDate { get; set; }
    }

}