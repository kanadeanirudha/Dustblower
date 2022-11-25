using AERP.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public class AllAccountDrillDownReport : BaseDTO
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
        public Int64 SaleInvoiceMasterID { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string TransactionDate { get; set; }
        public string CustomerInvoiceNumber { get; set; }
        public byte Invoicetype { get; set; }
        public Int32 CustomerBranchMasterID { get; set; }
        public Int32 CustomerMasterID { get; set; }
        public string InvoiceNumber { get; set; }
        public string TransactionType { get; set; }
        public string AccountName { get; set; }
        public decimal TransactionAmount { get; set; }
        public string DebitCreditFlag { get; set; }
        public string SessionFromDate { get; set; }
        public string SessionUptoDate { get; set; }
        public int AccountSessionID { get; set; }
        public Int16 AccountMasterID { get; set; }
        public Int16 ActBalsheetMstID { get; set; }
        public Int32 PersonID { get; set; }
        public string PersonType { get; set; }
        public decimal CurrentBalance { get; set; }
        public string NarrationDescription { get; set; }
        public string ChequeNo { get; set; }
        public string VoucherNoWithTranType { get; set; }
        public decimal DebitTransactionAmount { get; set; }
        public decimal CreditTransactionAmount { get; set; }
        public decimal RunningTotal { get; set; }
        public Int32 TransactionMainID { get; set; }
    }
}
