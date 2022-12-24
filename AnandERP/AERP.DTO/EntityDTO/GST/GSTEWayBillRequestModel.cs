using AERP.Base.DTO;

namespace AERP.DTO
{
    public class GSTEWayBillRequestModel : BaseDTO
    {
        public string CentreCode { get; set; }
        public int SalesInvoiceMasterID { get; set; }
        public string Irn { get; set; }
        /// <summary>
        ///GSTIN
        /// </summary>
        public string TransId { get; set; }
        public string TransName { get; set; }
        /// <summary>
        ///Required Parameter, enum": ["1", "2", "3", "4"] description": "Mode of transport (Road-1, Rail-2, Air-3, Ship-4)
        /// </summary>
        public string TransMode { get; set; } = "1";
        /// <summary>
        ///Required Parameter, Distance between source and destination PIN codes
        /// </summary>
        public int Distance { get; set; } = 0;
        /// <summary>
        ///pattern": "^([0-9A-Z/-]){1,15}$" "description": "Tranport Document Number"
        /// </summary>
        public string TransDocNo { get; set; }
        /// <summary>
        /// "pattern": "[0-3][0-9]/[0-1][0-9]/[2][0][1-2][0-9]","description": "Transport Document Date"
        /// </summary>
        public string TransDocDt { get; set; }
        /// <summary>
        ///  "Vehicle Number"
        /// </summary>
        public string VehNo { get; set; }
        /// <summary>
        /// "enum": ["O", "R"], "description": "Whether O-ODC or R-Regular "
        /// </summary>
        public string VehType { get; set; } = "R";
       
    }
}

