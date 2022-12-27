using AERP.Base.DTO;
using AERP.DTO;
using AERP.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace AERP.DataProvider
{
    public class EmployeeESICForm6ReportDataProvider : DBInteractionBase, IEmployeeESICForm6ReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public EmployeeESICForm6ReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public EmployeeESICForm6ReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<EmployeeESICForm6Report> GetEmployeeESICForm6ReportDataList(EmployeeESICForm6ReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<EmployeeESICForm6Report> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<EmployeeESICForm6Report>();
            SqlCommand cmdToExecute = new SqlCommand();
            SqlDataReader sqlDataReader = null;

            try
            {
                if (string.IsNullOrEmpty(searchRequest.ConnectionString))
                {
                    baseEntityCollection.Message.Add(new MessageDTO()
                    {
                        ErrorMessage = "Connection string is empty.",
                        MessageType = MessageTypeEnum.Error
                    });
                }
                else
                {
                    // Use base class' connection object
                    _mainConnection.ConnectionString = searchRequest.ConnectionString;

                    cmdToExecute.Connection = _mainConnection;
                    cmdToExecute.CommandText = "dbo.USP_EmployeesESICForm6Report";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtFromDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.FromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtUptoDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.UptoDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@siESICZoneID", SqlDbType.SmallInt, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.ESICZoneID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));

                    if (_mainConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _mainConnection.Open();
                    }
                    else
                    {
                        if (_mainConnectionProvider.IsTransactionPending)
                        {
                            cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                        }
                    }

                    sqlDataReader = cmdToExecute.ExecuteReader();

                    baseEntityCollection.CollectionResponse = new List<EmployeeESICForm6Report>();
                    while (sqlDataReader.Read())
                    {
                        EmployeeESICForm6Report item = new EmployeeESICForm6Report();
                        item.EmployeeName = sqlDataReader["EmployeeName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["EmployeeName"]);
                        item.EmployeeCode = sqlDataReader["EmployeeCode"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["EmployeeCode"]);
                        item.ESICNumber = sqlDataReader["ESICNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["ESICNumber"]);
                        item.ZoneCode = sqlDataReader["ZoneCode"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["ZoneCode"]);
                        item.FirstJoiningDate = sqlDataReader["FirstJoiningDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["FirstJoiningDate"]);
                        item.Month = sqlDataReader["Month"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["Month"]);
                        item.Year = sqlDataReader["Year"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["Year"]);
                        item.CentreName = sqlDataReader["CentreName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["CentreName"]);
                        item.CentreAdress = sqlDataReader["CentreAdress"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["CentreAdress"]);
                        item.Wages = sqlDataReader["Wages"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Wages"]);
                        item.TotalWorkersShareESIC = sqlDataReader["TotalWorkersShareESIC"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalWorkersShareESIC"]);
                        item.TotalEmployeersShareESIC = sqlDataReader["TotalEmployeersShareESIC"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalEmployeersShareESIC"]);
                        item.WorkingDays = sqlDataReader["WorkingDays"] is DBNull ? new byte() : Convert.ToByte(sqlDataReader["WorkingDays"]);
                        item.Designation = sqlDataReader["Designation"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["Designation"]);
                        item.SerialNo = sqlDataReader["SerialNo"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["SerialNo"]);
                        item.FromDate = searchRequest.FromDate;
                        item.UptoDate = searchRequest.UptoDate;
                        item.CentreCode = searchRequest.CentreCode;
                        item.ESICZone = searchRequest.ESICZone;
                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_EmployeeESICForm6Report_SelectAll' reported the ErrorCode: " + _errorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                baseEntityCollection.Message.Add(new MessageDTO()
                {
                    ErrorMessage = ex.InnerException.Message,
                    MessageType = MessageTypeEnum.Error
                });
                // _logException.Error(ex.Message);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
            return baseEntityCollection;
        }


        #endregion
    }
}
