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
    public class ContractListTillDateReportBA : IContractListTillDateReportBA
    {
        IContractListTillDateReportDataProvider _ContractListTillDateReportDataProvider;
        //IContractListTillDateReportBR _generalRegionMasterBR;
        private ILogger _logException;

        public ContractListTillDateReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            //_generalRegionMasterBR = new ContractListTillDateReportBR();
            _ContractListTillDateReportDataProvider = new ContractListTillDateReportDataProvider();
        }

        public IBaseEntityCollectionResponse<ContractListTillDateReport> GetContractListTillDateReportDataList(ContractListTillDateReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<ContractListTillDateReport> ContractListTillDateReportCollection = new BaseEntityCollectionResponse<ContractListTillDateReport>();
            try
            {
                if (_ContractListTillDateReportDataProvider != null)
                    ContractListTillDateReportCollection = _ContractListTillDateReportDataProvider.GetContractListTillDateReportDataList(searchRequest);
                else
                {
                    ContractListTillDateReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    ContractListTillDateReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                ContractListTillDateReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                ContractListTillDateReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return ContractListTillDateReportCollection;
        }
       
    }
}