using AERP.Base.DTO;

namespace AERP.DTO
{
    public class GSTInvoiceCancelledRequestModel : BaseDTO
    {
        public string Irn { get; set; }
        public string CnlRsn { get; set; } = "1"; //description": "Cancel Reason 1- Duplicate, 2 - Data entry mistake, 3- Order Cancelled, 4 - Others
        public string CnlRem { get; set; }
    }
}

