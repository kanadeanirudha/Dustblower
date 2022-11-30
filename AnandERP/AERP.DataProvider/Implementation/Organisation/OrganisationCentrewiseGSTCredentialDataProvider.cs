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
    public class OrganisationCentrewiseGSTCredentialDataProvider : DBInteractionBase, IOrganisationCentrewiseGSTCredentialDataProvider
    {
        #region Variable Declaration

        private readonly string _connectionString;
        private readonly ILogger _logException;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        public OrganisationCentrewiseGSTCredentialDataProvider()
        {
        }

        /// <summary>
        /// Constructor to initialized data member and member functions
        /// </summary>
        /// <param name="logException"></param>
        public OrganisationCentrewiseGSTCredentialDataProvider(ILogger logException)
        {
            _connectionString = "";//ConfigurationManager.ConnectionStrings["AERPEntities"].ToString();
            _logException = logException; // This should fix later
        }

        #endregion

        #region Method Implementation
        /// <summary>
        /// Select a record from OrganisationCentrewiseGSTCredential table by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public OrganisationCentrewiseGSTCredential GetOrganisationCentrewiseGSTCredentialByCentreCode(OrganisationCentrewiseGSTCredential item)
        {
            OrganisationCentrewiseGSTCredential response = new OrganisationCentrewiseGSTCredential();
            SqlCommand cmdToExecute = new SqlCommand();
            SqlDataReader sqlDataReader = null;

            try
            {
                if (string.IsNullOrEmpty(item.ConnectionString))
                {
                    response.ErrorMessage = "Connection string is empty.";
                }
                else
                {
                    // Use base class' connection object
                    _mainConnection.ConnectionString = item.ConnectionString;

                    cmdToExecute.Connection = _mainConnection;
                    cmdToExecute.CommandText = "dbo.USP_OrganisationCentrewiseGSTCredentialBy_CentreCode";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsCentreCode", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.CentreCode));
                    cmdToExecute.Parameters.Add(new SqlParameter("@bIsLiveMode", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.IsLiveMode));
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
                        response.ID = Convert.ToInt32(sqlDataReader["ID"]);
                        response.GSTIN = Convert.ToString(sqlDataReader["GSTINNumber"]);
                        response.CentreCode = Convert.ToString(sqlDataReader["CentreCode"]);
                        response.Version = Convert.ToString(sqlDataReader["Version"]);
                        response.Urls = Convert.ToString(sqlDataReader["Urls"]);
                        response.EInvoiceUserName = Convert.ToString(sqlDataReader["EInvoiceUserName"]);
                        response.EInvoicePassword = Convert.ToString(sqlDataReader["EInvoicePassword"]);
                        response.AspId = Convert.ToString(sqlDataReader["AspId"]);
                        response.AspUserPassword = Convert.ToString(sqlDataReader["AspUserPassword"]);
                        response.QrCodeSize = Convert.ToInt32(sqlDataReader["QrCodeSize"]);
                        response.AuthToken = Convert.ToString(sqlDataReader["AuthToken"]);
                        response.TokenExpiry = Convert.ToString(sqlDataReader["TokenExpiry"]);
                        response.ClientId = Convert.ToString(sqlDataReader["ClientId"]);
                    }

                    if (cmdToExecute.Parameters["@iErrorCode"].Value != null)
                    {
                        _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    }
                    if (_errorCode != (int)ErrorEnum.AllOk)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_OrganisationCentrewiseGSTCredentialBy_CentreCode' reported the ErrorCode: " + _errorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Please try again after sometime";
                //response.ErrorCode = MessageTypeEnum.Error;
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

        /// <summary>
        /// Create new record of OrganisationCentrewiseGSTCredential
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<OrganisationCentrewiseGSTCredential> InsertOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item)
        {
            IBaseEntityResponse<OrganisationCentrewiseGSTCredential> response = new BaseEntityResponse<OrganisationCentrewiseGSTCredential>();
            SqlCommand cmdToExecute = new SqlCommand();

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
                    cmdToExecute.CommandText = "dbo.USP_OrganisationCentrewiseGSTCredential_Insert";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;


                    //cmdToExecute.Parameters.Add(new SqlParameter("@nsDepartmentName", SqlDbType.NVarChar, 60,
                    //                                             ParameterDirection.Input, false, 10, 0, "",
                    //                                             DataRowVersion.Proposed, item.DepartmentName.Trim()));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@nsDeptShortCode", SqlDbType.NVarChar, 10,
                    //                                             ParameterDirection.Input, false, 0, 0, "",
                    //                                            DataRowVersion.Proposed, item.DeptShortCode.Trim()));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@nsPrintShortDesc", SqlDbType.NVarChar, 10,
                    //                                             ParameterDirection.Input, false, 0, 0, "",
                    //                                            DataRowVersion.Proposed, item.PrintShortDesc.Trim()));
                    ////cmdToExecute.Parameters.Add(new SqlParameter("@sAcademicNonacademic", SqlDbType.VarChar, 12,
                    ////                                             ParameterDirection.Input, false, 0, 0, "",
                    ////                                            DataRowVersion.Proposed, item.AcademicNonacademic));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@bTeachingActivity", SqlDbType.Bit, 1,
                    //                                             ParameterDirection.Input, true, 0, 0, "",
                    //                                             DataRowVersion.Proposed, item.TeachingActivity));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@iCreatedBy", SqlDbType.Int, 4,
                    //                                             ParameterDirection.Input, true, 10, 0, "",
                    //                                             DataRowVersion.Proposed, item.CreatedBy));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Output,
                    //                                             true, 10, 0, "", DataRowVersion.Proposed, item.ID));
                    //cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4,
                    //                                             ParameterDirection.Output, true, 10, 0, "",
                    //                                             DataRowVersion.Proposed, _errorCode));

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

                    // Execute query.
                    _rowsAffected = cmdToExecute.ExecuteNonQuery();
                    if (_rowsAffected > 0)
                    {
                        item.ID = (Int32)cmdToExecute.Parameters["@iID"].Value;
                    }
                    _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    item.ErrorCode = (Int32)_errorCode;
                    response.Entity = item;
                    if (_errorCode != (int)ErrorEnum.AllOk && _errorCode != (int)ErrorEnum.DuplicateEntry)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_OrganisationCentrewiseGSTCredential_Insert' reported the ErrorCode: " +
                                            _errorCode);
                    }

                    //if (_rowsAffected > 0)
                    //{
                    //    response.Entity = item;
                    //}
                    //else
                    //{
                    //    response.Message.Add(new MessageDTO
                    //    {
                    //        ErrorMessage = "Create failed"
                    //    });
                    //}
                }
            }
            catch (SqlException ex)
            {
                response.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                _logException.Error(ex.Message);
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                _logException.Error(ex.Message);
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

        /// <summary>
        /// Update a specific record of OrganisationCentrewiseGSTCredential
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<OrganisationCentrewiseGSTCredential> UpdateOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item)
        {
            IBaseEntityResponse<OrganisationCentrewiseGSTCredential> response = new BaseEntityResponse<OrganisationCentrewiseGSTCredential>();
            SqlCommand cmdToExecute = new SqlCommand();

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
                    cmdToExecute.CommandText = "dbo.USP_OrganisationCentrewiseGSTCredential_Update";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    cmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.ID));
                    cmdToExecute.Parameters.Add(new SqlParameter("@nsAuthToken", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, item.AuthToken.Trim()));
                    cmdToExecute.Parameters.Add(new SqlParameter("@sTokenExpiry", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, item.TokenExpiry.Trim()));
                    cmdToExecute.Parameters.Add(new SqlParameter("@iModifiedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, item.ModifiedBy));
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

                    // Execute query.
                    _rowsAffected = cmdToExecute.ExecuteNonQuery();
                    _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;
                    item.ErrorCode = (Int32)_errorCode;
                    response.Entity = item;
                    if (_errorCode != (int)ErrorEnum.AllOk && _errorCode != (int)ErrorEnum.DuplicateEntry)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'USP_OrganisationCentrewiseGSTCredential_Insert' reported the ErrorCode: " + _errorCode);
                    }

                    //if (_rowsAffected > 0)
                    //{
                    //    response.Entity = item;
                    //}
                    //else
                    //{
                    //    response.Message.Add(new MessageDTO
                    //    {
                    //        ErrorMessage = "Create failed"
                    //    });
                    //}
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });
                //_logException.Error(ex.Message);
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
