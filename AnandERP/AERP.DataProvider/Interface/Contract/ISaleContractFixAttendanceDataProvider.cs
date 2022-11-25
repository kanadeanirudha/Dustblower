﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface ISaleContractFixAttendanceDataProvider
    {
        IBaseEntityCollectionResponse<SaleContractFixAttendance> GetFixItemDataList(SaleContractFixAttendanceSearchRequest searchRequest);
        IBaseEntityResponse<SaleContractFixAttendance> InsertSaleContractFixAttendance(SaleContractFixAttendance item);
        IBaseEntityResponse<SaleContractFixAttendance> InsertContractFixAttendanceApproval(SaleContractFixAttendance item);
        IBaseEntityCollectionResponse<SaleContractFixAttendance> GetContractFixAttendanceForApproval(SaleContractFixAttendanceSearchRequest searchRequest);
    }
}
