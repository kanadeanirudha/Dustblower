using AERP.Base.DTO;
using AERP.Common;
using AERP.DTO;

using System;
namespace AERP.Business.BusinessRules
{
    public class OrganisationCentrewiseGSTCredentialBR : IOrganisationCentrewiseGSTCredentialBR
    {
        public OrganisationCentrewiseGSTCredentialBR()
        {
        }
        /// <summary>
        /// Validate method to insert record from OrganisationCentrewiseGSTCredential.
        /// <summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IValidateBusinessRuleResponse InsertOrganisationCentrewiseGSTCredentialValidate(OrganisationCentrewiseGSTCredential item)
        {
            IValidateBusinessRuleResponse businessResponse = new ValidateBusinessRuleResponse();
            try
            {
                //Check null exception
                if (item == null)
                {
                    throw new ArgumentNullException(Resources.InvalidArgumentsError);
                }
                if (!ValidateInsertOrganisationCentrewiseGSTCredential(item))
                {
                    businessResponse.Passed = false;
                    businessResponse.Message.Add(new MessageDTO
                    {
                        MessageType = MessageTypeEnum.Error,
                        ErrorMessage = "pass error message"
                    });
                }
                else
                {
                    businessResponse.Passed = true;
                }
            }
            catch (Exception ex)
            {
                businessResponse.Message.Add(new MessageDTO
                {
                    MessageType = MessageTypeEnum.Error,
                    ErrorMessage = ex.Message
                });
            }
            return businessResponse;
        }
        /// <summary>
        /// Validate method to update record from OrganisationCentrewiseGSTCredential.
        /// <summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IValidateBusinessRuleResponse UpdateOrganisationCentrewiseGSTCredentialValidate(OrganisationCentrewiseGSTCredential item)
        {
            IValidateBusinessRuleResponse businessResponse = new ValidateBusinessRuleResponse();
            try
            {
                //Check null exception
                if (item == null)
                {
                    throw new ArgumentNullException(Resources.InvalidArgumentsError);
                }
                if (!ValidateUpdateOrganisationCentrewiseGSTCredential(item))
                {
                    businessResponse.Passed = false;
                    businessResponse.Message.Add(new MessageDTO
                    {
                        MessageType = MessageTypeEnum.Error,
                        ErrorMessage = "pass error message"
                    });
                }
                else
                {
                    businessResponse.Passed = true;
                }
            }
            catch (Exception ex)
            {
                businessResponse.Message.Add(new MessageDTO
                {
                    MessageType = MessageTypeEnum.Error,
                    ErrorMessage = ex.Message
                });
            }
            return businessResponse;
        }
        private bool ValidateInsertOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential request)
        {
            //We need to Implment this validation method properly
            return true;
        }
        /// <summary>
        /// Validation on update OrganisationCentrewiseGSTCredentialproperty.
        /// <summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ValidateUpdateOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential request)
        {
            //We need to Implment this validation method properly
            return (request.ID > 0);
        }
    }
}
