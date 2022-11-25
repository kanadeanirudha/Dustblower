
using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class PurchaseRegisterDrillDownReport : BaseDTO
    {
        public Int64 ID
        {
            get;
            set;
        }

        public string CentreName
        {
            get; set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }

        public int DeletedBy
        {
            get;
            set;
        }

        public DateTime? DeletedDate
        {
            get;
            set;
        }
        public string errorMessage { get; set; }
        public string TransMonth { get; set; }
        public string TransYear { get; set; }
        public string TransMonthName { get; set; }
        public decimal TotalIInvoiceAmount { get; set; }
        public decimal TotalInvoiceAmount { get; set; }

        public string AccountSessionName { get; set; }
        public string CentreCode { get; set; }
        public Int64 PurchaseInvoiceMasterID { get; set; }
        public string Vender { get; set; }
        public string TransactionDate { get; set; }
        public string VendorInvoiceNumber { get; set; }
        public byte Invoicetype { get; set; }
        public Int32 VendorId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TransactionType { get; set; }
        public string AccountName { get; set; }
        public decimal TransactionAmount { get; set; }
        public string DebitCreditFlag { get; set; }
    }
}
