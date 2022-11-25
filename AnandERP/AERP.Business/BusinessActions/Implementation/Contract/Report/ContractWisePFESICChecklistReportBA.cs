using AERP.Base.DTO;
using AERP.Business.BusinessRules;
using AERP.Common;
using AERP.DataProvider;
using AERP.DTO;
using AERP.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.Business.BusinessAction
{
    public class ContractWisePFESICChecklistReportBA : IContractWisePFESICChecklistReportBA
    {
        IContractWisePFESICChecklistReportDataProvider _ContractWisePFESICChecklistReportDataProvider;
        //IContractWisePFESICChecklistReportBR _generalRegionMasterBR;
        private ILogger _logException;

        public ContractWisePFESICChecklistReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            //_generalRegionMasterBR = new ContractWisePFESICChecklistReportBR();
            _ContractWisePFESICChecklistReportDataProvider = new ContractWisePFESICChecklistReportDataProvider();
        }

        public IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> GetContractWisePFESICChecklistReportDataList(ContractWisePFESICChecklistReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> ContractWisePFESICChecklistReportCollection = new BaseEntityCollectionResponse<ContractWisePFESICChecklistReport>();
            try
            {
                if (_ContractWisePFESICChecklistReportDataProvider != null)
                    ContractWisePFESICChecklistReportCollection = _ContractWisePFESICChecklistReportDataProvider.GetContractWisePFESICChecklistReportDataList(searchRequest);
                else
                {
                    ContractWisePFESICChecklistReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    ContractWisePFESICChecklistReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                ContractWisePFESICChecklistReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                ContractWisePFESICChecklistReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return ContractWisePFESICChecklistReportCollection;
        }
       
    }
}