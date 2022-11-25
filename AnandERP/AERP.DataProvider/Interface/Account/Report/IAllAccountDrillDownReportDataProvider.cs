using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface IAllAccountDrillDownReportDataProvider
    {
        IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList(AllAccountDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList2(AllAccountDrillDownReportSearchRequest searchRequest);
        IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList3(AllAccountDrillDownReportSearchRequest searchRequest);
    }
}
