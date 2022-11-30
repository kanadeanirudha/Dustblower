using AERP.Base.DTO;

using System;

namespace AERP.DTO
{
    public class OrganisationCentrewiseGSTCredential : BaseDTO
    {
        public int ID { get; set; }
        public string Version { get; set; }
        public string GSTIN { get; set; }
        public string CentreCode { get; set; }
        public string Urls { get; set; }
        public string EInvoiceUserName { get; set; }
        public string EInvoicePassword { get; set; }
        public string AspId { get; set; }
        public string AspUserPassword { get; set; }
        public int QrCodeSize { get; set; }
        public string AuthToken { get; set; }
        public string TokenExpiry { get; set; }
        public string ClientId { get; set; }
        public bool IsLiveMode { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
