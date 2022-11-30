using AERP.Base.DTO;
using AERP.DTO;
namespace AERP.DataProvider
{
    public interface IOrganisationCentrewiseGSTCredentialDataProvider
    {
        IBaseEntityResponse<OrganisationCentrewiseGSTCredential> InsertOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item);
        IBaseEntityResponse<OrganisationCentrewiseGSTCredential> UpdateOrganisationCentrewiseGSTCredential(OrganisationCentrewiseGSTCredential item);
        OrganisationCentrewiseGSTCredential GetOrganisationCentrewiseGSTCredentialByCentreCode(OrganisationCentrewiseGSTCredential item);
    }
}
