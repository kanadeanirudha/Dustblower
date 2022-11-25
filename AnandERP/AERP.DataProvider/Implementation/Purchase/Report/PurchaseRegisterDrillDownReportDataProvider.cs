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
    public class PurchaseRegisterDrillDownReportDataProvider : DBInteractionBase, IPurchaseRegisterDrillDownReportDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public PurchaseRegisterDrillDownReportDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public PurchaseRegisterDrillDownReportDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_PurchaseRegisterFinancialYearWise";
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

                    baseEntityCollection.CollectionResponse = new List<PurchaseRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        PurchaseRegisterDrillDownReport item = new PurchaseRegisterDrillDownReport();

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
                        throw new Exception("Stored Procedure 'USP_PurchaseRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList2(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_PurchaseRegisterDetailsFinancialYearMonthWise";
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

                    baseEntityCollection.CollectionResponse = new List<PurchaseRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        PurchaseRegisterDrillDownReport item = new PurchaseRegisterDrillDownReport();

                        item.PurchaseInvoiceMasterID = sqlDataReader["PurchaseInvoiceMasterID"] is DBNull ? new Int64(): Convert.ToInt64(sqlDataReader["PurchaseInvoiceMasterID"]);
                        item.VendorId = sqlDataReader["VendorId"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["VendorId"]);
                        item.Vender = sqlDataReader["Vender"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["Vender"]);
                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.VendorInvoiceNumber = sqlDataReader["VendorInvoiceNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["VendorInvoiceNumber"]);
                        item.TransMonth = sqlDataReader["TransMonth"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransMonth"]);
                        item.TransYear = sqlDataReader["TransYear"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransYear"]);
                        item.TotalIInvoiceAmount = sqlDataReader["TotalIInvoiceAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TotalIInvoiceAmount"]);
                        
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
                        throw new Exception("Stored Procedure 'USP_PurchaseRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList3(PurchaseRegisterDrillDownReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<PurchaseRegisterDrillDownReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_ActGetVoucherByPurchaseInvoiceMasterID";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.CommandTimeout = 0;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iPurchaseInvoiceMasterID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.PurchaseInvoiceMasterID));

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

                    baseEntityCollection.CollectionResponse = new List<PurchaseRegisterDrillDownReport>();
                    while (sqlDataReader.Read())
                    {
                        PurchaseRegisterDrillDownReport item = new PurchaseRegisterDrillDownReport();

                        item.InvoiceNumber = sqlDataReader["InvoiceNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["InvoiceNumber"]);
                        item.TransactionDate = sqlDataReader["TransactionDate"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionDate"]);
                        item.TransactionType = sqlDataReader["TransactionType"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["TransactionType"]);
                        item.AccountName = sqlDataReader["AccountName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["AccountName"]);
                        item.TransactionAmount = sqlDataReader["TransactionAmount"] is DBNull ? new decimal() : Convert.ToDecimal(sqlDataReader["TransactionAmount"]);
                        item.DebitCreditFlag = sqlDataReader["DebitCreditFlag"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["DebitCreditFlag"]);

                        item.CentreName = searchRequest.CentreName;
                        item.CentreCode = searchRequest.CentreCode;
                        item.VendorInvoiceNumber = searchRequest.VendorInvoiceNumber;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_PurchaseRegisterDrillDownReport_SelectAll' reported the ErrorCode: " + _errorCode);
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
