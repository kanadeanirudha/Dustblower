﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessAction
{
    public interface IAccountHeadMasterBA
    {
        /// <summary>
        /// business action interface of select all record of account head master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityResponse<AccountHeadMaster> InsertAccountHeadMaster(AccountHeadMaster item);

        /// <summary>
        /// business action interface of select one record of account head master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityResponse<AccountHeadMaster> UpdateAccountHeadMaster(AccountHeadMaster item);

        /// <summary>
        /// business action interface of insert new record of account head master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityResponse<AccountHeadMaster> DeleteAccountHeadMaster(AccountHeadMaster item);

        /// <summary>
        /// business action interface of update record of account head master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityCollectionResponse<AccountHeadMaster> GetAccountHeadMasterBySearch(AccountHeadMasterSearchRequest searchRequest);

        /// <summary>
        /// business action interface of select head name list from account head master table.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityCollectionResponse<AccountHeadMaster> GetAccountHeadNameList(AccountHeadMasterSearchRequest searchRequest);

        /// <summary>
        /// business action interface of dalete record of account head master.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IBaseEntityResponse<AccountHeadMaster> GetAccountHeadMasterByID(AccountHeadMaster item);
    }
}
