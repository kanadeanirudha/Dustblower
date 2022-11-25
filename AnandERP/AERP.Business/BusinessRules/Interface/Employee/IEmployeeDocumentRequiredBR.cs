using AMS.Base.DTO;
using AMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Business.BusinessRules
{
    public interface IEmployeeDocumentRequiredBR
    {
        IValidateBusinessRuleResponse InsertEmployeeDocumentRequiredValidate(EmployeeDocumentRequired item);
        IValidateBusinessRuleResponse UpdateEmployeeDocumentRequiredValidate(EmployeeDocumentRequired item);
        IValidateBusinessRuleResponse DeleteEmployeeDocumentRequiredValidate(EmployeeDocumentRequired item);
    }
}
