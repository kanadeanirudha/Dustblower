using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.Business.BusinessAction
{
    public interface IOrganisationCentrewiseGSTCredentialBA
    {
        IBaseEntityResponse<OrganisationCentrewiseGSTCredential> InsertOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item);
        IBaseEntityResponse<OrganisationCentrewiseGSTCredential> UpdateOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item);
        OrganisationCentrewiseGSTCredential GetOrganisationCentrewiseGSTCredentialByCentreCode(OrganisationCentrewiseGSTCredential item);
    }
}
