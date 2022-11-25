using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface IPurchaseRegisterDrillDownReportDataProvider
    {
        IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList(PurchaseRegisterDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList2(PurchaseRegisterDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList3(PurchaseRegisterDrillDownReportSearchRequest searchRequest);
    }
}
