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
    public class AllAccountDrillDownReportBA : IAllAccountDrillDownReportBA
    {
        IAllAccountDrillDownReportDataProvider _AllAccountDrillDownReportDataProvider;
        private ILogger _logException;

        public AllAccountDrillDownReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _AllAccountDrillDownReportDataProvider = new AllAccountDrillDownReportDataProvider();
        }

        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> AllAccountDrillDownReportCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
            try
            {
                if (_AllAccountDrillDownReportDataProvider != null)
                    AllAccountDrillDownReportCollection = _AllAccountDrillDownReportDataProvider.GetAllAccountDrillDownReportList(searchRequest);
                else
                {
                    AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    AllAccountDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                AllAccountDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return AllAccountDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList2(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> AllAccountDrillDownReportCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
            try
            {
                if (_AllAccountDrillDownReportDataProvider != null)
                    AllAccountDrillDownReportCollection = _AllAccountDrillDownReportDataProvider.GetAllAccountDrillDownReportList2(searchRequest);
                else
                {
                    AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    AllAccountDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                AllAccountDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return AllAccountDrillDownReportCollection;
        }

        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList3(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> AllAccountDrillDownReportCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
            try
            {
                if (_AllAccountDrillDownReportDataProvider != null)
                    AllAccountDrillDownReportCollection = _AllAccountDrillDownReportDataProvider.GetAllAccountDrillDownReportList3(searchRequest);
                else
                {
                    AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    AllAccountDrillDownReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                AllAccountDrillDownReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                AllAccountDrillDownReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return AllAccountDrillDownReportCollection;
        }

    }
}