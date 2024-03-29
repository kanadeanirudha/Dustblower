﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.Business.BusinessAction
{
    public interface ICustomerMasterBA
    {
        IBaseEntityResponse<CustomerMaster> InsertCustomerMaster(CustomerMaster item);
        IBaseEntityResponse<CustomerMaster> InsertCustomerMasterContactDetails(CustomerMaster item);
        IBaseEntityResponse<CustomerMaster> InsertCustomerMasterBranchDetails(CustomerMaster item);
        IBaseEntityResponse<CustomerMaster> UpdateCustomerMaster(CustomerMaster item);
        IBaseEntityResponse<CustomerMaster> DeleteCustomerMaster(CustomerMaster item);
        IBaseEntityCollectionResponse<CustomerMaster> GetBySearch(CustomerMasterSearchRequest searchRequest);
        IBaseEntityCollectionResponse<CustomerMaster> GetCustomerMasterSearchList(CustomerMasterSearchRequest searchRequest);
        IBaseEntityCollectionResponse<CustomerMaster> GetCustomerBranchMasterSearchList(CustomerMasterSearchRequest searchRequest);
        IBaseEntityCollectionResponse<CustomerMaster> GetCustomerContactDetailsSearchList(CustomerMasterSearchRequest searchRequest);
        IBaseEntityCollectionResponse<CustomerMaster> GetContactDetailsByCustomerMasterID(CustomerMasterSearchRequest searchRequest);
        IBaseEntityResponse<CustomerMaster> SelectByID(CustomerMaster item);
        IBaseEntityResponse<CustomerMaster> SelectByCustomerMasterID(CustomerMaster item);

        IBaseEntityResponse<CustomerMaster> UpdateCustomerMasterByCustomerMasterID(CustomerMaster item);
        IBaseEntityCollectionResponse<CustomerMaster> GetCustomerBranchSearchListAll(CustomerMasterSearchRequest searchRequest);
    }
}
