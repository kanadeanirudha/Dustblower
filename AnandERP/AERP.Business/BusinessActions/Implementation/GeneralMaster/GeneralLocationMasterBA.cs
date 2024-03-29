﻿using AERP.Base.DTO;
using AERP.Business.BusinessRules;
using AERP.Common;
using AERP.DataProvider;
using AERP.DTO;
using AERP.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessAction
{
   public class GeneralLocationMasterBA : IGeneralLocationMasterBA
    {
        IGeneralLocationMasterDataProvider _generalLocationMasterDataProvider;
        IGeneralLocationMasterBR _generalLocationMasterBR;
        private ILogger _logException;

        public GeneralLocationMasterBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _generalLocationMasterBR = new GeneralLocationMasterBR();
            _generalLocationMasterDataProvider = new GeneralLocationMasterDataProvider();
        }

        /// <summary>
        /// Create new record of General Location Master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<GeneralLocationMaster> InsertGeneralLocationMaster(GeneralLocationMaster item)
        {
            IBaseEntityResponse<GeneralLocationMaster> entityResponse = new BaseEntityResponse<GeneralLocationMaster>();
            try
            {
                IValidateBusinessRuleResponse brResponse = _generalLocationMasterBR.InsertGeneralLocationMasterValidate(item);
                if (brResponse.Passed)
                {
                    entityResponse = _generalLocationMasterDataProvider.InsertGeneralLocationMaster(item);
                }
                else
                {
                    entityResponse.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    entityResponse.Entity = null;
                }
            }
            catch (Exception ex)
            {
                entityResponse.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                entityResponse.Entity = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return entityResponse;
        }

        /// <summary>
        /// Update a specific record of General Location Master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<GeneralLocationMaster> UpdateGeneralLocationMaster(GeneralLocationMaster item)
        {
            IBaseEntityResponse<GeneralLocationMaster> entityResponse = new BaseEntityResponse<GeneralLocationMaster>();
            try
            {
                IValidateBusinessRuleResponse brResponse = _generalLocationMasterBR.UpdateGeneralLocationMasterValidate(item);
                if (brResponse.Passed)
                {
                    entityResponse = _generalLocationMasterDataProvider.UpdateGeneralLocationMaster(item);
                }
                else
                {
                    entityResponse.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    entityResponse.Entity = null;
                }
            }
            catch (Exception ex)
            {
                entityResponse.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                entityResponse.Entity = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return entityResponse;
        }

        /// <summary>
        /// Delete a selected record from General Location Master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<GeneralLocationMaster> DeleteGeneralLocationMaster(GeneralLocationMaster item)
        {
            IBaseEntityResponse<GeneralLocationMaster> entityResponse = new BaseEntityResponse<GeneralLocationMaster>();
            try
            {
                IValidateBusinessRuleResponse brResponse = _generalLocationMasterBR.DeleteGeneralLocationMasterValidate(item);
                if (brResponse.Passed)
                {
                    entityResponse = _generalLocationMasterDataProvider.DeleteGeneralLocationMaster(item);
                }
                else
                {
                    entityResponse.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    entityResponse.Entity = null;
                }
            }
            catch (Exception ex)
            {
                entityResponse.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                entityResponse.Entity = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return entityResponse;
        }

        /// <summary>
        /// Select all record from General Location Master table with search parameters.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>

        public IBaseEntityCollectionResponse<GeneralLocationMaster> GetBySearch(GeneralLocationMasterSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<GeneralLocationMaster> categoryMasterCollection = new BaseEntityCollectionResponse<GeneralLocationMaster>();
            try
            {
                if (_generalLocationMasterDataProvider != null)
                {
                    categoryMasterCollection = _generalLocationMasterDataProvider.GetGeneralLocationMasterBySearch(searchRequest);
                }
                else
                {
                    categoryMasterCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    categoryMasterCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                categoryMasterCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                categoryMasterCollection.CollectionResponse = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return categoryMasterCollection;
        }
       


       /// <summary>
        /// Select all record from General Location Master table with search parameters.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>

        public IBaseEntityCollectionResponse<GeneralLocationMaster> GetBySearchList(GeneralLocationMasterSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<GeneralLocationMaster> categoryMasterCollection = new BaseEntityCollectionResponse<GeneralLocationMaster>();
            try
            {
                if (_generalLocationMasterDataProvider != null)
                {
                    categoryMasterCollection = _generalLocationMasterDataProvider.GetGeneralLocationMasterGetBySearchList(searchRequest);
                }
                else
                {
                    categoryMasterCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    categoryMasterCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                categoryMasterCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                categoryMasterCollection.CollectionResponse = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return categoryMasterCollection;
        }


        /// <summary>
        /// Select a record from General Location Master Master table by ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<GeneralLocationMaster> SelectByID(GeneralLocationMaster item)
        {

            IBaseEntityResponse<GeneralLocationMaster> entityResponse = new BaseEntityResponse<GeneralLocationMaster>();
            try
            {
                entityResponse = _generalLocationMasterDataProvider.GetGeneralLocationMasterByID(item);
            }
            catch (Exception ex)
            {
                entityResponse.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                entityResponse.Entity = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return entityResponse;
        }


        /// <summary>
        /// Select all record from General Location Master table with search parameters by cityID.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>

        public IBaseEntityCollectionResponse<GeneralLocationMaster> GetByCityID(GeneralLocationMasterSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<GeneralLocationMaster> categoryMasterCollection = new BaseEntityCollectionResponse<GeneralLocationMaster>();
            try
            {
                if (_generalLocationMasterDataProvider != null)
                {
                    categoryMasterCollection = _generalLocationMasterDataProvider.GetGeneralLocationMasterGetByCityID(searchRequest);
                }
                else
                {
                    categoryMasterCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    categoryMasterCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                categoryMasterCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                categoryMasterCollection.CollectionResponse = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return categoryMasterCollection;
        }


        public IBaseEntityCollectionResponse<GeneralLocationMaster> GetByRegionIDAndCityID(GeneralLocationMasterSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<GeneralLocationMaster> categoryMasterCollection = new BaseEntityCollectionResponse<GeneralLocationMaster>();
            try
            {
                if (_generalLocationMasterDataProvider != null)
                {
                    categoryMasterCollection = _generalLocationMasterDataProvider.GetByRegionIDAndCityID(searchRequest);
                }
                else
                {
                    categoryMasterCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    categoryMasterCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                categoryMasterCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                categoryMasterCollection.CollectionResponse = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return categoryMasterCollection;
        }

        public IBaseEntityCollectionResponse<GeneralLocationMaster> GetBySearchKeyWord(GeneralLocationMasterSearchRequest searchRequest)
        {
            IBaseEntityCollectionResponse<GeneralLocationMaster> categoryMasterCollection = new BaseEntityCollectionResponse<GeneralLocationMaster>();
            try
            {
                if (_generalLocationMasterDataProvider != null)
                {
                    categoryMasterCollection = _generalLocationMasterDataProvider.GetBySearchKeyWord(searchRequest);
                }
                else
                {
                    categoryMasterCollection.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    categoryMasterCollection.CollectionResponse = null;
                }
            }
            catch (Exception ex)
            {
                categoryMasterCollection.Message.Add(new MessageDTO
                {
                    ErrorMessage = ex.Message,
                    MessageType = MessageTypeEnum.Error
                });

                categoryMasterCollection.CollectionResponse = null;

                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return categoryMasterCollection;
        }
       
    }
}