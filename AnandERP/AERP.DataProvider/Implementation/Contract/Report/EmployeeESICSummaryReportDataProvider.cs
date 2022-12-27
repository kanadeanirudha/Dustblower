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
    public class EmployeeESICSummaryReportDataProvider : DBInteractionBase, IEmployeeESICSummaryReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public EmployeeESICSummaryReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public EmployeeESICSummaryReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<EmployeeESICSummaryReport> GetEmployeeESICSummaryReportDataList(EmployeeESICSummaryReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<EmployeeESICSummaryReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<EmployeeESICSummaryReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_EmployeesESICSummaryReport";
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

                    baseEntityCollection.CollectionResponse = new List<EmployeeESICSummaryReport>();
                    while (sqlDataReader.Read())
                    {
                        EmployeeESICSummaryReport item = new EmployeeESICSummaryReport();
                        item.Wages = sqlDataReader["Wages"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Wages"]);
                        item.WorkersShare = sqlDataReader["WorkersShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["WorkersShare"]);
                        item.ESIC = sqlDataReader["ESIC"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESIC"]);
                        item.TotalShare = sqlDataReader["TotalShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalShare"]);
                        item.WorkingDays = sqlDataReader["WorkingDays"] is DBNull ? new int() : Convert.ToInt32(sqlDataReader["WorkingDays"]);
                        item.cntEmployee = sqlDataReader["cntEmployee"] is DBNull ? new int() : Convert.ToInt32(sqlDataReader["cntEmployee"]);
                        item.SalaryMonth = sqlDataReader["SalaryMonth"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["SalaryMonth"]);
                        item.SalaryYear = sqlDataReader["SalaryYear"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["SalaryYear"]);

                        item.FromDate = searchRequest.FromDate;
                        item.UptoDate = searchRequest.UptoDate;
                        item.CentreCode = searchRequest.CentreCode;
                        item.CentreName = searchRequest.CentreName;
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
                        throw new Exception("Stored Procedure 'USP_EmployeeESICSummaryReport_SelectAll' reported the ErrorCode: " + _errorCode);
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
