
using AERP.DTO;

using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTInvoiceResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
        public string status_cd { get; set; }
        public Error error { get; set; }
        public string ErrorMessage { get; set; }
        public DataResponse DataResponse { get; set; }
    }

    public class Error
    {
        public string error_cd { get; set; }
        public string message { get; set; }
    }
    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class DataResponse
    {
        public string AckNo { get; set; }
        public string AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public ExtractedSignedInvoiceData ExtractedSignedInvoiceData { get; set; }
        public ExtractedSignedQrCode ExtractedSignedQrCode { get; set; }
        public string QrCodeImage { get; set; }
        public string JwtIssuer { get; set; }
    }


    public class ExtractedSignedInvoiceData
    {
        public long AckNo { get; set; }
        public string AckDt { get; set; }
        public string Version { get; set; }
        public string Irn { get; set; }
        public TranDtls TranDtls { get; set; }
        public DocDtls DocDtls { get; set; }
        public SellerDtls SellerDtls { get; set; }
        public BuyerDtls BuyerDtls { get; set; }
        public DispDtls DispDtls { get; set; }
        public ShipDtls ShipDtls { get; set; }
        public ValDtls ValDtls { get; set; }
        public PayDtls PayDtls { get; set; }
        public ExpDtls ExpDtls { get; set; }
        public RefDtls RefDtls { get; set; }
        public List<AddlDocDtl> AddlDocDtls { get; set; }
        public object EwbDtls { get; set; }
        public List<ItemList> ItemList { get; set; }
    }

    public class ExtractedSignedQrCode
    {
        public string SellerGstin { get; set; }
        public string BuyerGstin { get; set; }
        public string DocNo { get; set; }
        public string DocTyp { get; set; }
        public string DocDt { get; set; }
        public double TotInvVal { get; set; }
        public string ItemCnt { get; set; }
        public string MainHsnCode { get; set; }
        public string Irn { get; set; }
    }
}