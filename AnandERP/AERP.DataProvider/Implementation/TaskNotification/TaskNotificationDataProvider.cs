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
    public class TaskNotificationDataProvider : DBInteractionBase, ITaskNotificationDataProvider
    {
        #region Variable Declaration
        private readonly string _connectionString;
        private readonly ILogger _logException;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public TaskNotificationDataProvider()
        {
        }
        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public TaskNotificationDataProvider(ILogger logException)
        {
            _connectionString = ""; //ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }
        #endregion
        #region Method Implementation
        /// <summary>
        /// Select all record from TaskNotification table with search parameters
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public IBaseEntityCollectionResponse<TaskNotification> GetTaskNotificationBySearch(TaskNotificationSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<TaskNotification>();
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
                    if (searchRequest.TaskCode == "MP")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForManPower_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "PO")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForPO_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "SO")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForSO_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "SC")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForSC_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "CA")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForCA_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "CF")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForCF_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "SI")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForSI_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "CI")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForCI_SelectByPersonID";
                    }
                    else if (searchRequest.TaskCode == "CSR" || searchRequest.TaskCode == "CSD")
                    {
                        cmdToExecute.CommandText = "dbo.USP_TaskNotificationDetailsForCSR_SelectByPersonID";
                    }
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iPersonID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.PersonID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sTaskCode", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.TaskCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sSortBy", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, searchRequest.SortBy));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iStartRow", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.StartRow));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iEndRow", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.EndRow));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsSearchBy", SqlDbType.NVarChar, 200, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SearchBy));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sSortDirection", SqlDbType.VarChar, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.SortDirection));

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
                    baseEntityCollection.CollectionResponse = new List<TaskNotification>();
                    while (sqlDataReader.Read())
                    {
                        TaskNotification item = new TaskNotification();

                        if (DBNull.Value.Equals(sqlDataReader["Description"]) == false)
                        {
                            item.TaskDescription = sqlDataReader["Description"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["MenuCodeLink"]) == false)
                        {
                            item.MenuCodeLink = sqlDataReader["MenuCodeLink"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ApprovalStatus"]) == false)
                        {
                            item.ApprovalStatus = Convert.ToInt32(sqlDataReader["ApprovalStatus"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["TaskNotificationDetailsID"]) == false)
                        {
                            item.TaskNotificationDetailsID = Convert.ToInt32(sqlDataReader["TaskNotificationDetailsID"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["TaskNotificationMasterID"]) == false)
                        {
                            item.TaskNotificationMasterID = Convert.ToInt32(sqlDataReader["TaskNotificationMasterID"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["GeneralTaskReportingDetailsID"]) == false)
                        {
                            item.GeneralTaskReportingDetailsID = Convert.ToInt32(sqlDataReader["GeneralTaskReportingDetailsID"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["EntitytableName"]) == false)
                        {
                            item.EntitytableName = sqlDataReader["EntitytableName"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["EntityPKName"]) == false)
                        {
                            item.EntityPKName = sqlDataReader["EntityPKName"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["EntityPKValue"]) == false)
                        {
                            item.EntityPKValue = Convert.ToInt32(sqlDataReader["EntityPKValue"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["StageSequenceNumber"]) == false)
                        {
                            item.StageSequenceNumber = Convert.ToInt32(sqlDataReader["StageSequenceNumber"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["IsLastStage"]) == false)
                        {
                            item.IsLastRecordFlag = Convert.ToBoolean(sqlDataReader["IsLastStage"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ApplicationDate"]) == false)
                        {
                            item.ApplicationDate = sqlDataReader["ApplicationDate"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["IsEngaged"]) == false)
                        {
                            item.IsEngaged = Convert.ToBoolean(sqlDataReader["IsEngaged"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["EnagedByUserID"]) == false)
                        {
                            item.EngagedByUserID = Convert.ToInt32(sqlDataReader["EnagedByUserID"]);
                        }
                        if (DBNull.Value.Equals(sqlDataReader["CentreCode"]) == false)
                        {
                            item.CentreCode = Convert.ToString(sqlDataReader["CentreCode"]);
                        }
                        if (searchRequest.TaskCode == "MP")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["CompanyName"]) == false)
                            {
                                item.CompanyName = Convert.ToString(sqlDataReader["CompanyName"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["Designation"]) == false)
                            {
                                item.Designation = Convert.ToString(sqlDataReader["Designation"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["TotalAmount"]) == false)
                            {
                                item.TotalAmount = Convert.ToString(sqlDataReader["TotalAmount"]);
                            }
                        }
                        if (searchRequest.TaskCode == "PO")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["TransDate"]) == false)
                            {
                                item.TransDate = Convert.ToString(sqlDataReader["TransDate"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["PurchaseRequisitionNumber"]) == false)
                            {
                                item.PurchaseRequisitionNumber = Convert.ToString(sqlDataReader["PurchaseRequisitionNumber"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["Vendor"]) == false)
                            {
                                item.Vendor = Convert.ToString(sqlDataReader["Vendor"]);
                            }
                        }
                        if (searchRequest.TaskCode == "SC")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["ContractNumber"]) == false)
                            {
                                item.ContractNumber = Convert.ToString(sqlDataReader["ContractNumber"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["CompanyName"]) == false)
                            {
                                item.CompanyName = Convert.ToString(sqlDataReader["CompanyName"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["BillingType"]) == false)
                            {
                                item.BillingType = Convert.ToString(sqlDataReader["BillingType"]);
                            }
                        }
                        if (searchRequest.TaskCode == "CA" || searchRequest.TaskCode == "CF")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["SaleContractBillingSpanID"]) == false)
                            {
                                item.SaleContractBillingSpanID = Convert.ToInt32(sqlDataReader["SaleContractBillingSpanID"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["ContractNumber"]) == false)
                            {
                                item.ContractNumber = Convert.ToString(sqlDataReader["ContractNumber"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["SaleContractBillingSpan"]) == false)
                            {
                                item.SaleContractBillingSpan = Convert.ToString(sqlDataReader["SaleContractBillingSpan"]);
                            }
                        }
                        if (searchRequest.TaskCode == "SO")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["SalesOrderNumber"]) == false)
                            {
                                item.SalesOrderNumber = Convert.ToString(sqlDataReader["SalesOrderNumber"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["TransDate"]) == false)
                            {
                                item.TransDate = Convert.ToString(sqlDataReader["TransDate"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["CompanyName"]) == false)
                            {
                                item.CompanyName = Convert.ToString(sqlDataReader["CompanyName"]);
                            }
                        }
                        if (searchRequest.TaskCode == "SI" || searchRequest.TaskCode == "CI")
                        {
                            if (DBNull.Value.Equals(sqlDataReader["CustomerInvoiceNumber"]) == false)
                            {
                                item.CustomerInvoiceNumber = Convert.ToString(sqlDataReader["CustomerInvoiceNumber"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["TransDate"]) == false)
                            {
                                item.TransDate = Convert.ToString(sqlDataReader["TransDate"]);
                            }
                            if (DBNull.Value.Equals(sqlDataReader["CompanyName"]) == false)
                            {
                                item.CompanyName = Convert.ToString(sqlDataReader["CompanyName"]);
                            }
                        }
                        if (searchRequest.TaskCode == "CSR" || searchRequest.TaskCode == "CSD")
                        {
                            if (!DBNull.Value.Equals(sqlDataReader["CustomerName"]))
                                item.CustomerName = Convert.ToString(sqlDataReader["CustomerName"]);
                            if (!DBNull.Value.Equals(sqlDataReader["ItemName"]))
                                item.ItemName = Convert.ToString(sqlDataReader["ItemName"]);
                            if (!DBNull.Value.Equals(sqlDataReader["Location"]))
                                item.Location = Convert.ToString(sqlDataReader["Location"]);
                            if (!DBNull.Value.Equals(sqlDataReader["SalePrice"]))
                                item.SalePrice = Convert.ToString(sqlDataReader["SalePrice"]);
                        }
                        baseEntityCollection.CollectionResponse.Add(item);
                        baseEntityCollection.TotalRecords = Convert.ToInt32(sqlDataReader["TotalRecords"]);
                    }
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_TaskNotification_SelectAll' reported the ErrorCode: " + _errorCode);
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
        public IBaseEntityCollectionResponse<TaskNotification> GetDashboardContentListByAdminRoleID(TaskNotificationSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollection = baseEntityCollection = new BaseEntityCollectionResponse<TaskNotification>();
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
                    cmdToExecute.CommandText = "dbo.USP_DashboardContentDetails_SelectByRoleID";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iAdminRoleMasterID", SqlDbType.Int, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, searchRequest.AdminRoleMasterID));
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
                    baseEntityCollection.CollectionResponse = new List<TaskNotification>();
                    while (sqlDataReader.Read())
                    {
                        TaskNotification item = new TaskNotification();
                        if (DBNull.Value.Equals(sqlDataReader["ContentCode"]) == false)
                        {
                            item.ContentCode = sqlDataReader["ContentCode"].ToString();
                        }
                        if (DBNull.Value.Equals(sqlDataReader["ContentTitle"]) == false)
                        {
                            item.ContentTitle = sqlDataReader["ContentTitle"].ToString();
                        }
                        baseEntityCollection.CollectionResponse.Add(item);
                    }
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_GeneralTaskModel_SelectByRoleID' reported the ErrorCode: " + _errorCode);
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

        public IBaseEntityResponse<TaskNotification> GetNotificationCountEmployeewise(TaskNotification item)
        {
            //throw new NotImplementedException();
            IBaseEntityResponse<TaskNotification> response = new BaseEntityResponse<TaskNotification>();
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
                    cmdToExecute.CommandText = "dbo.USP_NotificationCount_SelectByPersonID";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iPersonID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.EmployeeID));
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
                        TaskNotification _item = new TaskNotification();
                        if (DBNull.Value.Equals(sqlDataReader["NotificationCount"]) == false)
                        {
                            _item.NotificationCount = Convert.ToInt32(sqlDataReader["NotificationCount"]);
                        }

                        response.Entity = _item;
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_TaskNotificationDetailsTotalCount_SelectByPersonID' reported the ErrorCode: " + _errorCode);
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
