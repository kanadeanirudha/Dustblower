using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface ISalesRegisterDrillDownReportDataProvider
    {
        IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList(SalesRegisterDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList2(SalesRegisterDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList3(SalesRegisterDrillDownReportSearchRequest searchRequest);
    }
}
