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
    public class SalesRegisterDrillDownReportDataProvider : DBInteractionBase, ISalesRegisterDrillDownReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public SalesRegisterDrillDownReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public SalesRegisterDrillDownReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_SalesRegisterFinancialYearWise";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccountSessionID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccountSessionID));

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

                    baseEntityCollection.CollectionResponse = new List<SalesRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        SalesRegisterDrillDownReport item = new SalesRegisterDrillDownReport();

                        item.TransMonth = sqlDataReader["TransMonth"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransMonth"]);
                        item.TransYear = sqlDataReader["TransYear"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransYear"]);
                        item.TotalInvoiceAmount = sqlDataReader["TotalIInvoiceAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalIInvoiceAmount"]);
                        item.TransMonthName = sqlDataReader["TransMonthName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransMonthName"]);

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.AccountSessionName = searchRequest.AccountSessionName;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_SalesRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList2(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_SalesRegisterDetailsFinancialYearMonthWise";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 35, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iMonth", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.TransMonth));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iYear", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.TransYear));

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

                    baseEntityCollection.CollectionResponse = new List<SalesRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        SalesRegisterDrillDownReport item = new SalesRegisterDrillDownReport();

                        item.SaleInvoiceMasterID = sqlDataReader["SaleInvoiceMasterID"] is DBNull ? new Int64(): Convert.ToInt64(sqlDataReader["SaleInvoiceMasterID"]);
                        item.CompanyName = sqlDataReader["CompanyName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["CompanyName"]);
                        item.BranchName = sqlDataReader["BranchName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["BranchName"]);
                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.CustomerInvoiceNumber = sqlDataReader["CustomerInvoiceNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["CustomerInvoiceNumber"]);
                        item.TransMonth = sqlDataReader["TransMonth"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransMonth"]);
                        item.TransYear = sqlDataReader["TransYear"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransYear"]);
                        item.TotalIInvoiceAmount = sqlDataReader["TotalIInvoiceAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalIInvoiceAmount"]);
                        item.Invoicetype = sqlDataReader["Invoicetype"] is DBNull ? new byte() : Convert.ToByte(sqlDataReader["Invoicetype"]);
                        item.CustomerBranchMasterID = sqlDataReader["CustomerBranchMasterID"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["CustomerBranchMasterID"]);
                        item.CustomerMasterID = sqlDataReader["CustomerMasterID"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["CustomerMasterID"]);

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.TransMonthName = searchRequest.TransMonthName;
                        item.CentreName = searchRequest.CentreName;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_SalesRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> GetSalesRegisterDrillDownReportList3(SalesRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<SalesRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<SalesRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_ActGetVoucherByInvoiceMasterID";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iSalesInvoiceMasterID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SalesInvoiceMasterID));

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

                    baseEntityCollection.CollectionResponse = new List<SalesRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        SalesRegisterDrillDownReport item = new SalesRegisterDrillDownReport();

                        item.InvoiceNumber = sqlDataReader["InvoiceNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["InvoiceNumber"]);
                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.TransactionType = sqlDataReader["TransactionType"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionType"]);
                        item.AccountName = sqlDataReader["AccountName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["AccountName"]);
                        item.TransactionAmount = sqlDataReader["TransactionAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TransactionAmount"]);
                        item.DebitCreditFlag = sqlDataReader["DebitCreditFlag"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["DebitCreditFlag"]);

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.CustomerInvoiceNumber = searchRequest.CustomerInvoiceNumber;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_SalesRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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
