using AERP.Base.DTO;
using AERP.DTO;
using AERP.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DataProvider
{
    public class LoginDataProvider : DBInteractionBase, ILoginDataProvider
    {
        #region Login API
        public IBaseEntityResponse<UserMaster> UserLoginApi(UserMaster item)
        {
            IBaseEntityResponse<UserMaster> response = new BaseEntityResponse<UserMaster>();
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
                    _mainConnection.ConnectionString = item.ConnectionString;
                    cmdToExecute.Connection = _mainConnection;
                    cmdToExecute.CommandText = "dbo.USP_WEB_API_LoginVerification";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sMobileNumber", SqlDbType.VarChar, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ""));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sEmailID", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 100, 0, "", DataRowVersion.Proposed, item.EmailID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sPassword", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 100, 0, "", DataRowVersion.Proposed, item.Password));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sMachineName", SqlDbType.VarChar, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, item.MachinName));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sIP", SqlDbType.VarChar, 10, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, item.IP));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sVersionNumber", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, item.VersionNumber));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsDeviceToken", SqlDbType.VarChar, 250, ParameterDirection.Input, true, 100, 0, "", DataRowVersion.Proposed, item.DeviceToken));

                    if (_mainConnectionIsCreatedLocal)
                    {
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
                    UserMaster userMaster = new UserMaster();

                    if (sqlDataReader.Read())
                    {
                        userMaster.exists = sqlDataReader["exist"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["exist"]); 
                        if (userMaster.exists.ToUpper() == "EXIST")
                        {
                            userMaster.EmployeeMasterID = sqlDataReader["EmployeeMasterID"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["EmployeeMasterID"]);
                            userMaster.UserID = sqlDataReader["UserID"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["UserID"]);
                            userMaster.UserName = sqlDataReader["EmployeeName"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["EmployeeName"]);
                            userMaster.EmailID = sqlDataReader["EmailID"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["EmailID"]);
                            userMaster.MobileNumber = sqlDataReader["MobileNumber"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["MobileNumber"]);
                            userMaster.EmployeeDesignation = sqlDataReader["EmployeeDesignation"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["EmployeeDesignation"]);
                            userMaster.EmployeeDesignationID = sqlDataReader["EmployeeDesignationID"] is DBNull ? new Int32() : Convert.ToInt32(sqlDataReader["EmployeeDesignationID"]);
                            userMaster.IsServiceEnginner = sqlDataReader["ServiceEnginner"] is DBNull ? new Boolean() : Convert.ToBoolean(sqlDataReader["ServiceEnginner"]);
                            userMaster.IsServiceManager = sqlDataReader["ServiceManager"] is DBNull ? new Boolean() : Convert.ToBoolean(sqlDataReader["ServiceManager"]);
                            userMaster.IsCollectionExecutive = sqlDataReader["CollectionExecutive"] is DBNull ? new Boolean() : Convert.ToBoolean(sqlDataReader["CollectionExecutive"]);
                        }
                    }
                    sqlDataReader.Close();
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }

                    response.Entity = userMaster;
                    response.Entity.ErrorCode = (int)_errorCode;

                    if (_errorCode != (int)ErrorEnum.AllOk && _errorCode != (int)ErrorEnum.Success && _errorCode != (int)ErrorEnum.InvalidCredentials && _errorCode != (int)ErrorEnum.VersionUpgrade)
                    {
                        throw new Exception("Stored Procedure 'USP_LoginVerification Procedure' reported the ErrorCode: " + _errorCode);
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
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
            return response;
        }
        #endregion

        public IBaseEntityResponse<UserMaster> IsValidate(UserMaster item)
        {
            IBaseEntityResponse<UserMaster> response = new BaseEntityResponse<UserMaster>();
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
                    _mainConnection.ConnectionString = item.ConnectionString;
                    cmdToExecute.Connection = _mainConnection;
                    cmdToExecute.CommandText = "dbo.USP_WEB_API_LoginValidate";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@sEmailID", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 100, 0, "", DataRowVersion.Proposed, item.EmailID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sPassword", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 100, 0, "", DataRowVersion.Proposed, item.Password));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    if (_mainConnectionIsCreatedLocal)
                    {
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
                    UserMaster userMaster = new UserMaster();

                    if (sqlDataReader.Read())
                    {
                        userMaster.exists = sqlDataReader["exist"] is DBNull ? string.Empty : Convert.ToString(sqlDataReader["exist"]);
                    }
                    sqlDataReader.Close();
                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }

                    response.Entity = userMaster;
                    response.Entity.ErrorCode = (int)_errorCode;

                    if (_errorCode != (int)ErrorEnum.AllOk && _errorCode != (int)ErrorEnum.Success && _errorCode != (int)ErrorEnum.InvalidCredentials && _errorCode != (int)ErrorEnum.VersionUpgrade)
                    {
                        throw new Exception("Stored Procedure 'USP_USP_WEB_API_LoginValidate Procedure' reported the ErrorCode: " + _errorCode);
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
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
            return response;
        }

    }
}
