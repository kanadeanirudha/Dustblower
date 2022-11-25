using AERP.Base.DTO;
namespace AERP.DTO
{
    public class PurchaseRegisterDrillDownReportSearchRequest : Request
    {
        public string CentreCode
        {
            get;
            set;
        }
        public string CentreName { get; set; }
        public string SearchWord
        {
            get;
            set;
        }
        public string SearchFor
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        public string SortOrder
        {
            get;
            set;
        }

        public string SortBy
        {
            get;
            set;
        }

        public int StartRow
        {
            get;
            set;
        }

        public int EndRow
        {
            get;
            set;
        }

        public int RowLength
        {
            get;
            set;
        }
        public string SearchBy
        {
            get;
            set;
        }
        public string SortDirection
        {
            get;
            set;
        }
        public string UnitCode
        {
            get;
            set;
        }
        public string FromDate { get; set; }
        public string UptoDate { get; set; }
        public int AccountSessionID { get; set; }
        public string AccountSessionName { get; set; }
        public string TransYear { get; set; }
        public string TransMonthName { get; set; }
        public string TransMonth { get; set; }
        public long PurchaseInvoiceMasterID { get; set; }
        public string VendorInvoiceNumber { get; set; }
    }
}
