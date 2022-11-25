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
    public class ContractSalaryAndInvoiceStatusReportBA : IContractSalaryAndInvoiceStatusReportBA
    {
        IContractSalaryAndInvoiceStatusReportDataProvider _ContractSalaryAndInvoiceStatusReportDataProvider;
        //IContractSalaryAndInvoiceStatusReportBR _generalRegionMasterBR;
        private ILogger _logException;

        public ContractSalaryAndInvoiceStatusReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            //_generalRegionMasterBR = new ContractSalaryAndInvoiceStatusReportBR();
            _ContractSalaryAndInvoiceStatusReportDataProvider = new ContractSalaryAndInvoiceStatusReportDataProvider();
        }

        public IBaseEntityCollectionResponse<ContractSalaryAndInvoiceStatusReport> GetContractSalaryAndInvoiceStatusReportDataList(ContractSalaryAndInvoiceStatusReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<ContractSalaryAndInvoiceStatusReport> ContractSalaryAndInvoiceStatusReportCollection = new BaseEntityCollectionResponse<ContractSalaryAndInvoiceStatusReport>();
            try
            {
                if (_ContractSalaryAndInvoiceStatusReportDataProvider != null)
                    ContractSalaryAndInvoiceStatusReportCollection = _ContractSalaryAndInvoiceStatusReportDataProvider.GetContractSalaryAndInvoiceStatusReportDataList(searchRequest);
                else
                {
                    ContractSalaryAndInvoiceStatusReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    ContractSalaryAndInvoiceStatusReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                ContractSalaryAndInvoiceStatusReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                ContractSalaryAndInvoiceStatusReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return ContractSalaryAndInvoiceStatusReportCollection;
        }
       
    }
}