
using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTAuthTokenErrorResponse
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
    }
}