
using System.Collections.Generic;

namespace AERP.Web.UI.Helper
{
    public class GSTInvoiceResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public object ErrorDetails { get; set; }
        public object InfoDtls { get; set; }
    }

    public class DataResponse
    {
        public string AckNo { get; set; }
        public string AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public object EwbNo { get; set; }
        public object EwbDt { get; set; }
        public object EwbValidTill { get; set; }
        public ExtractedSignedInvoiceData ExtractedSignedInvoiceData { get; set; }
        public ExtractedSignedQrCode ExtractedSignedQrCode { get; set; }
        public object QrCodeImage { get; set; }
        public string JwtIssuer { get; set; }
    }

    public class BuyerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public object TrdNm { get; set; }
        public string Pos { get; set; }
        public string Addr1 { get; set; }
        public object Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public int Stcd { get; set; }
        public object Ph { get; set; }
        public string Em { get; set; }
    }

    public class DocDtls
    {
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }
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
        public object DispDtls { get; set; }
        public object ShipDtls { get; set; }
        public ValDtls ValDtls { get; set; }
        public object PayDtls { get; set; }
        public object ExpDtls { get; set; }
        public object RefDtls { get; set; }
        public object AddlDocDtls { get; set; }
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
        public int TotInvVal { get; set; }
        public string ItemCnt { get; set; }
        public string MainHsnCode { get; set; }
        public string Irn { get; set; }
    }

    public class ItemList
    {
        public int ItemNo { get; set; }
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HsnCd { get; set; }
        public object Barcde { get; set; }
        public string Qty { get; set; }
        public string FreeQty { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double TotAmt { get; set; }
        public int Discount { get; set; }
        public int PreTaxVal { get; set; }
        public int AssAmt { get; set; }
        public int GstRt { get; set; }
        public double IgstAmt { get; set; }
        public int CgstAmt { get; set; }
        public int SgstAmt { get; set; }
        public int CesRt { get; set; }
        public int CesAmt { get; set; }
        public int CesNonAdvlAmt { get; set; }
        public int StateCesRt { get; set; }
        public int StateCesAmt { get; set; }
        public int StateCesNonAdvlAmt { get; set; }
        public int OthChrg { get; set; }
        public double TotItemVal { get; set; }
        public object OrdLineRef { get; set; }
        public object OrgCntry { get; set; }
        public object PrdSlNo { get; set; }
        public object BchDtls { get; set; }
        public object AttribDtls { get; set; }
    }

    public class SellerDtls
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public int Stcd { get; set; }
        public object Ph { get; set; }
        public string Em { get; set; }
    }

    public class TranDtls
    {
        public string TaxSch { get; set; }
        public string SupTyp { get; set; }
        public string RegRev { get; set; }
        public object EcmGstin { get; set; }
        public string IgstOnIntra { get; set; }
    }

    public class ValDtls
    {
        public int AssVal { get; set; }
        public int CgstVal { get; set; }
        public int SgstVal { get; set; }
        public double IgstVal { get; set; }
        public int CesVal { get; set; }
        public int StCesVal { get; set; }
        public int Discount { get; set; }
        public int OthChrg { get; set; }
        public double RndOffAmt { get; set; }
        public int TotInvVal { get; set; }
        public double TotInvValFc { get; set; }
    }


}