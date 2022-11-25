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
    public class ContractWisePFESICChecklistReportDataProvider : DBInteractionBase, IContractWisePFESICChecklistReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public ContractWisePFESICChecklistReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public ContractWisePFESICChecklistReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> GetContractWisePFESICChecklistReportDataList(ContractWisePFESICChecklistReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<ContractWisePFESICChecklistReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_ContractWisePFESICChecklistReport_SelectAll_1";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsMonthName", SqlDbType.NVarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SalaryMonth));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsYear", SqlDbType.NVarChar, 5, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SalaryYear));
                    cmdToExecute.Parameters.Add(new SqlParameter("@siCurrentESICZoneID", SqlDbType.SmallInt, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CurrentESICZoneID));

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

                    baseEntityCollection.CollectionResponse = new List<ContractWisePFESICChecklistReport>();

                    while (sqlDataReader.Read())
                    {
                        ContractWisePFESICChecklistReport item = new ContractWisePFESICChecklistReport();
                        item.SiteName = sqlDataReader["SiteName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["SiteName"]);
                        item.PFWages = sqlDataReader["PFWages"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["PFWages"]);
                        item.PFWorkersShare = sqlDataReader["PFWorkersShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["PFWorkersShare"]);
                        item.PFTotalShare = sqlDataReader["PFTotalShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["PFTotalShare"]);
                        item.PFDifference = sqlDataReader["PFDifference"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["PFDifference"]);
                        item.ESICWages = sqlDataReader["ESICWages"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESICWages"]);
                        item.ESICWorkersShare = sqlDataReader["ESICWorkersShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESICWorkersShare"]);
                        item.ESICTotalShare = sqlDataReader["ESICTotalShare"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESICTotalShare"]);
                        item.ESICDifference = sqlDataReader["ESICDifference"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESICDifference"]);
                        item.Acc01 = sqlDataReader["Acc01"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Acc01"]);
                        item.Acc10 = sqlDataReader["Acc10"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Acc10"]);
                        item.Acc02 = sqlDataReader["Acc02"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Acc02"]);
                        item.Acc21 = sqlDataReader["Acc21"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Acc21"]);
                        item.Acc22 = sqlDataReader["Acc22"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["Acc22"]);
                        item.ESIC = sqlDataReader["ESIC"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["ESIC"]);

                        item.SalaryMonth = searchRequest.SalaryMonth;
                        item.SalaryYear = searchRequest.SalaryYear;
                        item.CentreCode = searchRequest.CentreCode;
                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_ContractWisePFESICChecklistReport_SelectAll' reported the ErrorCode: " + _errorCode);
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
