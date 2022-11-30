using AERP.Base.DTO;
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
    public class OrganisationCentrewiseGSTCredentialBA : IOrganisationCentrewiseGSTCredentialBA
    {
        IOrganisationCentrewiseGSTCredentialDataProvider _organisationCentrewiseGSTCredentialDataProvider;
        IOrganisationCentrewiseGSTCredentialBR _organisationCentrewiseGSTCredentialBR;
        private ILogger _logException;
        public OrganisationCentrewiseGSTCredentialBA()
        {
            _logException = new ExceptionManager.ExceptionManager(); //This need to change later
            _organisationCentrewiseGSTCredentialBR = new OrganisationCentrewiseGSTCredentialBR();
            _organisationCentrewiseGSTCredentialDataProvider = new OrganisationCentrewiseGSTCredentialDataProvider();
        }
        /// <summary>
        /// Create new record of OrganisationCentrewiseGSTCredential.
        /// <summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<OrganisationCentrewiseGSTCredential> InsertOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item)
        {
            IBaseEntityResponse<OrganisationCentrewiseGSTCredential> entityResponse = new BaseEntityResponse<OrganisationCentrewiseGSTCredential>();
            try
            {
                IValidateBusinessRuleResponse brResponse = _organisationCentrewiseGSTCredentialBR.InsertOrganisationCentrewiseGSTCredentialValidate(item);
                if (brResponse.Passed)
                {
                    entityResponse = _organisationCentrewiseGSTCredentialDataProvider.InsertOrganisationCentrewiseGSTCredential(item);
                }
                else
                {
                    entityResponse.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    entityResponse.Entity = null; ;
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
        /// Update a specific record  of OrganisationCentrewiseGSTCredential.
        /// <summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBaseEntityResponse<OrganisationCentrewiseGSTCredential> UpdateOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item)
        {
            IBaseEntityResponse<OrganisationCentrewiseGSTCredential> entityResponse = new BaseEntityResponse<OrganisationCentrewiseGSTCredential>();
            try
            {
                IValidateBusinessRuleResponse brResponse = _organisationCentrewiseGSTCredentialBR.UpdateOrganisationCentrewiseGSTCredentialValidate(item);
                if (brResponse.Passed)
                {
                    entityResponse = _organisationCentrewiseGSTCredentialDataProvider.UpdateOrganisationCentrewiseGSTCredential(item);
                }
                else
                {
                    entityResponse.Message.Add(new MessageDTO
                    {
                        ErrorMessage = Resources.Null_Object_Exception,
                        MessageType = MessageTypeEnum.Error
                    });
                    entityResponse.Entity = null; ;
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
        public OrganisationCentrewiseGSTCredential GetOrganisationCentrewiseGSTCredentialByCentreCode(OrganisationCentrewiseGSTCredential item)
        {
            OrganisationCentrewiseGSTCredential entityResponse = new OrganisationCentrewiseGSTCredential();
            try
            {
                entityResponse = _organisationCentrewiseGSTCredentialDataProvider.GetOrganisationCentrewiseGSTCredentialByCentreCode(item);
            }
            catch (Exception ex)
            {
                if (_logException != null)
                {
                    _logException.Error(ex.Message);
                }
            }
            return entityResponse;
        }
    }
}
