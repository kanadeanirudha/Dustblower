
using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTErrorResponse
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
    }
    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}