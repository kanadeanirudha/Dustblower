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
    public class PurchaseRegisterDrillDownReportBA : IPurchaseRegisterDrillDownReportBA
    {
        IPurchaseRegisterDrillDownReportDataProvider _PurchaseRegisterDrillDownReportDataProvider;
        private ILogger _logException;

        public PurchaseRegisterDrillDownReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _PurchaseRegisterDrillDownReportDataProvider = new PurchaseRegisterDrillDownReportDataProvider();
        }

        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> PurchaseRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
            try
            {
                if (_PurchaseRegisterDrillDownReportDataProvider != null)
                    PurchaseRegisterDrillDownReportCollection = _PurchaseRegisterDrillDownReportDataProvider.GetPurchaseRegisterDrillDownReportList(searchRequest);
                else
                {
                    PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return PurchaseRegisterDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList2(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> PurchaseRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
            try
            {
                if (_PurchaseRegisterDrillDownReportDataProvider != null)
                    PurchaseRegisterDrillDownReportCollection = _PurchaseRegisterDrillDownReportDataProvider.GetPurchaseRegisterDrillDownReportList2(searchRequest);
                else
                {
                    PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return PurchaseRegisterDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList3(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> PurchaseRegisterDrillDownReportCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
            try
            {
                if (_PurchaseRegisterDrillDownReportDataProvider != null)
                    PurchaseRegisterDrillDownReportCollection = _PurchaseRegisterDrillDownReportDataProvider.GetPurchaseRegisterDrillDownReportList3(searchRequest);
                else
                {
                    PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                PurchaseRegisterDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                PurchaseRegisterDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return PurchaseRegisterDrillDownReportCollection;
        }

    }
}