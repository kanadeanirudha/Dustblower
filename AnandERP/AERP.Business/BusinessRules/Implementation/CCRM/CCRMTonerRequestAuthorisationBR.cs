using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;
using AERP.Common;
using AERP.DTO;

namespace AERP.Business.BusinessRules
{
    class CCRMTonerRequestAuthorisationBR :ICCRMTonerRequestAuthorisationBR
    {
        public CCRMTonerRequestAuthorisationBR()
        {
        }
        public IValidateBusinessRuleResponse UpdateCCRMTonerRequestAuthorisationValidate(CCRMTonerRequestAuthorisation item)
        {
            IValidateBusinessRuleResponse businessResponse = new ValidateBusinessRuleResponse();
            try
            {
                //Check null exception
                if (item == null)
                {
                    throw new ArgumentNullException(Resources.InvalidArgumentsError);
                }

                if (!ValidateUpdateCCRMTonerRequestAuthorisation(item))
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
        public IValidateBusinessRuleResponse DeleteCCRMTonerRequestAuthorisationValidate(CCRMTonerRequestAuthorisation item)
        {
            IValidateBusinessRuleResponse businessResponse = new ValidateBusinessRuleResponse();
            try
            {
                //Check null exception
                if (item == null)
                {
                    throw new ArgumentNullException(Resources.InvalidArgumentsError);
                }

                if (!ValidateDeleteCCRMTonerRequestAuthorisation(item))
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
        //private bool ValidateInsertCCRMTonerRequestAuthorisation(CCRMTonerRequestAuthorisation request)
        //{
        //    //We need to Implment this validation method properly
        //    return true;
        //}
        private bool ValidateUpdateCCRMTonerRequestAuthorisation(CCRMTonerRequestAuthorisation request)
        {
            //We need to Implment this validation method properly
            return (request.ID > 0);
        }
        private bool ValidateDeleteCCRMTonerRequestAuthorisation(CCRMTonerRequestAuthorisation request)
        {
            try
            {
                return (request.ID > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
