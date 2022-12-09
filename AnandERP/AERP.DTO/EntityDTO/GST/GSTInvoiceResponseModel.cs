using AERP.Base.DTO;

namespace AERP.DTO
{
    public class GSTInvoiceResponseModel : BaseDTO
    {
        public int GSTEInvoiceMasterID { get; set; }
        public int SalesInvoiceMasterID { get; set; }
        public string AcknowledgementNo { get; set; }
        public string AcknowledgementDate { get; set; }
        public string Irn { get; set; }
        public string QrCodeImage { get; set; }
        public bool IsCancelledEInvoice { get; set; }
        public string CancelledEInvoiceDate { get; set; }
        public string CancelledEInvoiceReason { get; set; }
        public string CancelledEInvoiceDescription { get; set; }
        public string GSTEInvoiceDetails { get; set; }
        public int CreatedBy { get; set; }
    }
}

