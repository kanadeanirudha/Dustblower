﻿using AERP.Base.DTO;
using AERP.DTO;
using AERP.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace AERP.DataProvider
{
    public class AccountProfitAndLossReportDataProvider : DBInteractionBase, IAccountProfitAndLossReportDataProvider
    {
        #region Variable Declaration
        private readonly string _connectionString;
        private readonly ILogger _logException;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public AccountProfitAndLossReportDataProvider()
        {
        }
        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public AccountProfitAndLossReportDataProvider(ILogger logException)
        {
            _connectionString = ""; //ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }
        #endregion
        #region Method Implementation
        /// <summary>
        /// Select all record from AccountProfitAndLossReport table with search parameters
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public IBaseEntityCollectionResponse<AccountProfitAndLossReport> GetAccountProfitAndLossReportBySearch(AccountProfitAndLossReportSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<AccountProfitAndLossReport> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<AccountProfitAndLossReport>();
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
                    cmdToExecute.CommandText = "dbo.USP_AccountProfitAndLoss_Report";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtStartDate", SqlDbType.Date, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionFromDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@dtEndDate", SqlDbType.Date, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SessionUptoDate));
                    cmdToExecute.Parameters.Add(new SqlParameter("@cProfitLossFlag", SqlDbType.Char, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, searchRequest.ProfitLossFlag));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccBalsheetMstId", SqlDbType.Int, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccBalsheetMstId));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAccSessionId", SqlDbType.Int, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AccountSessionID));
                    if (searchRequest.CentreCode != string.Empty && searchRequest.CentreCode != null)
                    {
                        cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 15, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.CentreCode));
                    }
                    else
                    {
                        cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 15, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    }
                    cmdToExecute.Parameters.Add(new SqlParameter("@cGroupBy", SqlDbType.Char, 2, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.GroupBy));
                    cmdToExecute.Parameters.Add(new SqlParameter("@bIsZeroBalance", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, searchRequest.IsZeroBalance));
                    cmdToExecute.Parameters.Add(new SqlParameter("@bIsSubLedger", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, searchRequest.IsSubLedger));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
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
                    //DataTable dt = new DataTable();
                    //dt.Load(sqlDataReader);
                    baseEntityCollection.CollectionResponse = new List<AccountProfitAndLossReport>();
                    while (sqlDataReader.Read())
                    {
                        AccountProfitAndLossReport item = new AccountProfitAndLossReport();

                        if (DBNull.Value.Equals(sqlDataReader["HeadName"]) == false)
                        {
                            item.HeadName = sqlDataReader["HeadName"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["HeadCode"]) == false)
                        {
                            item.HeadCode = sqlDataReader["HeadCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["AccountName"]) == false)
                        {
                            item.AccountName = sqlDataReader["AccountName"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["AccountCode"]) == false)
                        {
                            item.AccountCode = sqlDataReader["AccountCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["GroupDescription"]) == false)
                        {
                            item.GroupDescription = sqlDataReader["GroupDescription"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["GroupCode"]) == false)
                        {
                            item.GroupCode = sqlDataReader["GroupCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["CategoryDescription"]) == false)
                        {
                            item.CategoryDescription = sqlDataReader["CategoryDescription"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["CategoryCode"]) == false)
                        {
                            item.CategoryCode = sqlDataReader["CategoryCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["CentreCode"]) == false)
                        {
                            item.CentreCode = sqlDataReader["CentreCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["OpeningBalanceDebitAmount"]) == false)
                        {
                            item.OpeningBalanceDebit = Convert.ToDecimal(sqlDataReader["OpeningBalanceDebitAmount"].ToString());
                        }
                        if (DBNull.Value.Equals(sqlDataReader["OpeningBalanceCreditAmount"]) == false)
                        {
                            item.OpeningBalanceCerdit = Convert.ToDecimal(sqlDataReader["OpeningBalanceCreditAmount"].ToString());
                        }
                        if (DBNull.Value.Equals(sqlDataReader["OpeningBalance"]) == false)
                        {
                            item.OpeningBalance = Convert.ToDecimal(sqlDataReader["OpeningBalance"].ToString());
                        }
                        if (DBNull.Value.Equals(sqlDataReader["TotalDebit"]) == false)
                        {
                            item.TotalDebit = Convert.ToDecimal(sqlDataReader["TotalDebit"].ToString());
                        }
                        if (DBNull.Value.Equals(sqlDataReader["TotalCredit"]) == false)
                        {
                            item.TotalCredit = Convert.ToDecimal(sqlDataReader["TotalCredit"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ClosingBalance"]) == false)
                        {
                            item.ClosingBalance = Convert.ToDecimal(sqlDataReader["ClosingBalance"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ClosingDebitAmount"]) == false)
                        {
                            item.ClosingDebitAmount = Convert.ToDecimal(sqlDataReader["ClosingDebitAmount"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ClosingCreditAmount"]) == false)
                        {
                            item.ClosingCreditAmount = Convert.ToDecimal(sqlDataReader["ClosingCreditAmount"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["TotalBalanceSum"]) == false)
                        {
                            item.TotalBalanceSum = Convert.ToDecimal(sqlDataReader["TotalBalanceSum"]);
                        }
                        switch (searchRequest.GroupBy)
                        { 
                            case "A":
                                item.GroupBy = "Account Wise";
                                break;
                            case "G":
                                item.GroupBy = "Group Wise";
                                break;
                            case "C":
                                item.GroupBy = "Category Wise";
                                break;
                        }
                        item.AccBalsheetName = searchRequest.AccBalsheetName;
                        item.SessionFromDate = searchRequest.SessionFromDate;
                        item.SessionUptoDate = searchRequest.SessionUptoDate;

                        baseEntityCollection.CollectionResponse.Add(item);
                    }
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_AccountProfitAndLoss_Report' reported the ErrorCode: " + _errorCode);
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
        /// <summary>
        /// Select a record from table by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<AccountProfitAndLossReport> GetAccountProfitAndLossReportByID(AccountProfitAndLossReport item)
        {
            IBaseEntityResponse<AccountProfitAndLossReport> response = new BaseEntityResponse<AccountProfitAndLossReport>();
            SqlCommand cmdToExecute = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                if (string.IsNullOrEmpty(item.ConnectionString))
                {
                    response.Message.Add(new MessageDTO()
                    {
                        ErrorMessage = "Connection string is empty.",
                        MessageType = MessageTypeEnum.Error
                    });
                }
                else
                {
                    // Use base class' connection object
                    _mainConnection.ConnectionString = item.ConnectionString;
                    cmdToExecute.Connection = _mainConnection;
                    cmdToExecute.CommandText = "dbo.USP_AccountProfitAndLossReport_SelectOne";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.ID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
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
                    while (sqlDataReader.Read())
                    {
                        AccountProfitAndLossReport _item = new AccountProfitAndLossReport();
                        //_item.ID = Convert.ToInt32(sqlDataReader["ID"]);
                        //_item.SessionStartDatetime = Convert.ToDateTime(sqlDataReader["SessionStartDatetime"]);
                        //_item.SessionEndDatetime = Convert.ToDateTime(sqlDataReader["SessionEndDatetime"]);
                        //_item.DefaultFlag = Convert.ToBoolean(sqlDataReader["DefaultFlag"]);
                        //_item.Account_System = sqlDataReader["Account_System"].ToString();
                        //_item.IsActive = Convert.ToBoolean(sqlDataReader["IsActive"]);
                        //_item.CreatedBy = Convert.ToInt32(sqlDataReader["CreatedBy"]);

                        response.Entity = _item;
                    }
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'Select Procedure' reported the ErrorCode: " + _errorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageDTO()
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
            return response;
        }

        #endregion
    }
}
