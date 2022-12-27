using AERP.Base.DTO;
using AERP.Common;
using AERP.DataProvider;
using AERP.DTO;
using AERP.ExceptionManager;

using System;
namespace AERP.Business.BusinessAction
{
    public class EmployeeESICSummaryReportBA : IEmployeeESICSummaryReportBA
    {
        IEmployeeESICSummaryReportDataProvider _EmployeeESICSummaryReportDataProvider;
        private ILogger _logException;

        public EmployeeESICSummaryReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _EmployeeESICSummaryReportDataProvider = new EmployeeESICSummaryReportDataProvider();
        }

        public IBaseEntityCollectionResponse<EmployeeESICSummaryReport> GetEmployeeESICSummaryReportDataList(EmployeeESICSummaryReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<EmployeeESICSummaryReport> EmployeeESICSummaryReportCollection = new BaseEntityCollectionResponse<EmployeeESICSummaryReport>();
            try
            {
                if (_EmployeeESICSummaryReportDataProvider != null)
                    EmployeeESICSummaryReportCollection = _EmployeeESICSummaryReportDataProvider.GetEmployeeESICSummaryReportDataList(searchRequest);
                else
                {
                    EmployeeESICSummaryReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    EmployeeESICSummaryReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                EmployeeESICSummaryReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                EmployeeESICSummaryReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return EmployeeESICSummaryReportCollection;
        }


    }
}