﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessAction
{
    public interface IContractSalaryBonusReportBA
    {
        IBaseEntityCollectionResponse<ContractSalaryBonusReport> GetContractSalaryBonusReportDataList(ContractSalaryBonusReportSearchRequest searchRequest);
        IBaseEntityResponse<ContractSalaryBonusReport> InsertContractSalaryBonusReport(ContractSalaryBonusReport item);
        IBaseEntityCollectionResponse<ContractSalaryBonusReport> GetContractSalaryBonusATMReportDataList(ContractSalaryBonusReportSearchRequest searchRequest);
    }
}
