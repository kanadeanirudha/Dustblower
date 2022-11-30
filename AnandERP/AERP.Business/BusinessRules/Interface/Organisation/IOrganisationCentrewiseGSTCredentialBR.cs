using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.Business.BusinessRules
{
    public interface IOrganisationCentrewiseGSTCredentialBR
    {
        IValidateBusinessRuleResponse InsertOrganisationCentrewiseGSTCredentialValidate(OrganisationCentrewiseGSTCredential item);
        IValidateBusinessRuleResponse UpdateOrganisationCentrewiseGSTCredentialValidate(OrganisationCentrewiseGSTCredential item);
    }
}
