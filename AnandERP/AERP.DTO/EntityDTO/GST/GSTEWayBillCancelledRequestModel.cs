using AERP.Base.DTO;

namespace AERP.DTO
{
    public class GSTEWayBillCancelledRequestModel : BaseDTO
    {
        public string CentreCode { get; set; }
        public int SalesInvoiceMasterID { get; set; }
        public string ewbNo { get; set; }
        public string cancelRsnCode { get; set; } = "2"; //description": "Cancel Reason 1- Duplicate, 2 - Data entry mistake, 3- Order Cancelled, 4 - Others
        public string cancelRmrk { get; set; }
    }
}

