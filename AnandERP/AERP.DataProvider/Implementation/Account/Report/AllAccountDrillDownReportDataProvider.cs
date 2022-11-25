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
    public class AllAccountDrillDownReportDataProvider : DBInteractionBase, IAllAccountDrillDownReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public AllAccountDrillDownReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public AllAccountDrillDownReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_ActLedgerAll_Report";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccountSessionID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccountSessionID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtStartDate", SqlDbType.Date, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionFromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtEndDate", SqlDbType.Date, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionUptoDate));

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

                    baseEntityCollection.CollectionResponse = new List<AllAccountDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        AllAccountDrillDownReport item = new AllAccountDrillDownReport();

                        item.AccountMasterID = sqlDataReader["AccountMasterID"] is DBNull ? new Int16() : Convert.ToInt16(sqlDataReader["AccountMasterID"]);
                        item.AccountName = sqlDataReader["AccountName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["AccountName"]);
                        item.ActBalsheetMstID = sqlDataReader["ActBalsheetMstID"] is DBNull ? new Int16() : Convert.ToInt16(sqlDataReader["ActBalsheetMstID"]);
                        item.PersonID = sqlDataReader["PersonID"] is DBNull ? new int() : Convert.ToInt32(sqlDataReader["PersonID"]);
                        item.PersonType = sqlDataReader["PersonType"] is DBNull ? "0" : Convert.ToString(sqlDataReader["PersonType"]);
                        item.CurrentBalance = sqlDataReader["CurrentBalance"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["CurrentBalance"]); 

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.AccountSessionName = searchRequest.AccountSessionName;
                        item.SessionFromDate = searchRequest.SessionFromDate;
                        item.SessionUptoDate = searchRequest.SessionUptoDate;
                        item.AccountSessionID = searchRequest.AccountSessionID;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_AllAccountDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList2(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
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
                    if (searchRequest.PersonID == 0)
                    {
                        cmdToExecute.CommandText = "dbo.USP_ActGeneralLedger_Report";
                    }
                    else
                    {
                        cmdToExecute.CommandText = "dbo.USP_ActSubLedger_Report";
                    }
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtStartDate", SqlDbType.Date, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionFromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtEndDate", SqlDbType.Date, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionUptoDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccountId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccountMasterID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccSessionId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccountSessionID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccBalsheetMstId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.ActBalsheetMstID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    if(searchRequest.PersonID == 0)
                    {
                        cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                        //cmdToExecute.Parameters.Add(new SqlParameter("@sConsolidiateType", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, "NOTAPPLI"));
                        //cmdToExecute.Parameters.Add(new SqlParameter("@nsTransactionType", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ""));
                    }
                    else
                    {
                        cmdToExecute.Parameters.Add(new SqlParameter("@iPersonId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.PersonID));
                        cmdToExecute.Parameters.Add(new SqlParameter("@cPersonType", SqlDbType.Char, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.PersonType));
                        cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    }


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

                    baseEntityCollection.CollectionResponse = new List<AllAccountDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        AllAccountDrillDownReport item = new AllAccountDrillDownReport();

                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty: Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.NarrationDescription = sqlDataReader["NarrationDescription"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["NarrationDescription"]);
                        item.ChequeNo = sqlDataReader["ChequeNo"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["ChequeNo"]);
                        item.VoucherNoWithTranType = sqlDataReader["VoucherNoWithTranType"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["VoucherNoWithTranType"]);
                        item.DebitCreditFlag = sqlDataReader["DebitCreditFlag"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["DebitCreditFlag"]);
                        item.DebitTransactionAmount = sqlDataReader["DebitTransactionAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["DebitTransactionAmount"]);
                        item.CreditTransactionAmount = sqlDataReader["CreditTransactionAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["CreditTransactionAmount"]);
                        item.RunningTotal = sqlDataReader["RunningTotal"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["RunningTotal"]);
                        item.TransactionMainID = sqlDataReader["TransactionMainID"] is DBNull ? new int() : Convert.ToInt32(sqlDataReader["TransactionMainID"]);

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_AllAccountDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<AllAccountDrillDownReport> GetAllAccountDrillDownReportList3(AllAccountDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<AllAccountDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_ActGetVoucherByVoucherID";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iVoucherID", SqlDbType.Int, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.TransactionMainID));

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

                    baseEntityCollection.CollectionResponse = new List<AllAccountDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        AllAccountDrillDownReport item = new AllAccountDrillDownReport();

                        item.InvoiceNumber = sqlDataReader["InvoiceNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["InvoiceNumber"]);
                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.TransactionType = sqlDataReader["TransactionType"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionType"]);
                        item.AccountName = sqlDataReader["AccountName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["AccountName"]);
                        item.TransactionAmount = sqlDataReader["TransactionAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TransactionAmount"]);
                        item.DebitCreditFlag = sqlDataReader["DebitCreditFlag"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["DebitCreditFlag"]);

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.VoucherNoWithTranType = searchRequest.VoucherNoWithTranType;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_AllAccountDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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
