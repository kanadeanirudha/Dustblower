using AERP.Base.DTO;
using AERP.Common;
using AERP.DataProvider;
using AERP.DTO;
using AERP.ExceptionManager;

using System;
namespace AERP.Business.BusinessAction
{
    public class EmployeeESICForm6ReportBA : IEmployeeESICForm6ReportBA
    {
        IEmployeeESICForm6ReportDataProvider _EmployeeESICForm6ReportDataProvider;
        private ILogger _logException;

        public EmployeeESICForm6ReportBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _EmployeeESICForm6ReportDataProvider = new EmployeeESICForm6ReportDataProvider();
        }

        public IBaseEntityCollectionResponse<EmployeeESICForm6Report> GetEmployeeESICForm6ReportDataList(EmployeeESICForm6ReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<EmployeeESICForm6Report> EmployeeESICForm6ReportCollection = new BaseEntityCollectionResponse<EmployeeESICForm6Report>();
            try
            {
                if (_EmployeeESICForm6ReportDataProvider != null)
                    EmployeeESICForm6ReportCollection = _EmployeeESICForm6ReportDataProvider.GetEmployeeESICForm6ReportDataList(searchRequest);
                else
                {
                    EmployeeESICForm6ReportCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    EmployeeESICForm6ReportCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                EmployeeESICForm6ReportCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                EmployeeESICForm6ReportCollection.CollectionResponse = null;
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return EmployeeESICForm6ReportCollection;
        }


    }
}