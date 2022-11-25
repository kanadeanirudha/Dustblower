using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface IContractWisePFESICChecklistReportDataProvider
    {
        IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> GetContractWisePFESICChecklistReportDataList(ContractWisePFESICChecklistReportSearchRequest searchRequest);
    }
}
