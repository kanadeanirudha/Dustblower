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
    public class SalesRegisterDrillDownReportBA : ISalesRegisterDrillDownReportBA
    {
        ISalesRegisterDrillDownReportDataProvider _SalesRegisterDrillDownReportDataProvider;
        private ILogger _logException;

        public SalesRegisterDrillDownReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _SalesRegisterDrillDownReportDataProvider = new SalesRegisterDrillDownReportDataProvider();
        }

        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> SalesRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
            try
            {
                if (_SalesRegisterDrillDownReportDataProvider != null)
                    SalesRegisterDrillDownReportCollection = _SalesRegisterDrillDownReportDataProvider.GetSalesRegisterDrillDownReportList(searchRequest);
                else
                {
                    SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return SalesRegisterDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList2(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> SalesRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
            try
            {
                if (_SalesRegisterDrillDownReportDataProvider != null)
                    SalesRegisterDrillDownReportCollection = _SalesRegisterDrillDownReportDataProvider.GetSalesRegisterDrillDownReportList2(searchRequest);
                else
                {
                    SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return SalesRegisterDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList3(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> SalesRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
            try
            {
                if (_SalesRegisterDrillDownReportDataProvider != null)
                    SalesRegisterDrillDownReportCollection = _SalesRegisterDrillDownReportDataProvider.GetSalesRegisterDrillDownReportList3(searchRequest);
                else
                {
                    SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                SalesRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                SalesRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return SalesRegisterDrillDownReportCollection;
        }

    }
}