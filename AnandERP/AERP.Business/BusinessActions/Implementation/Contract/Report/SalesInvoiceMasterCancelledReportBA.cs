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
    public class SalesInvoiceMasterCancelledReportBA : ISalesInvoiceMasterCancelledReportBA
    {
        ISalesInvoiceMasterCancelledReportDataProvider _SalesInvoiceMasterCancelledReportDataProvider;
        private ILogger _logException;

        public SalesInvoiceMasterCancelledReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _SalesInvoiceMasterCancelledReportDataProvider = new SalesInvoiceMasterCancelledReportDataProvider();
        }

        public IBaseEntityCollectionResponse<SalesInvoiceMasterCancelledReport> GetSalesInvoiceMasterCancelledReportDataList(SalesInvoiceMasterCancelledReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesInvoiceMasterCancelledReport> SalesInvoiceMasterCancelledReportCollection = new BaseEntityCollectionResponse<SalesInvoiceMasterCancelledReport>();
            try
            {
                if (_SalesInvoiceMasterCancelledReportDataProvider != null)
                    SalesInvoiceMasterCancelledReportCollection = _SalesInvoiceMasterCancelledReportDataProvider.GetSalesInvoiceMasterCancelledReportDataList(searchRequest);
                else
                {
                    SalesInvoiceMasterCancelledReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    SalesInvoiceMasterCancelledReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                SalesInvoiceMasterCancelledReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                SalesInvoiceMasterCancelledReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return SalesInvoiceMasterCancelledReportCollection;
        }


    }
}