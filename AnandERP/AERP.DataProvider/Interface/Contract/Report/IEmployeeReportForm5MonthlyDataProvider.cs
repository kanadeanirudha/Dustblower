﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AERP.DataProvider
{
    public interface IEmployeeReportForm5MonthlyDataProvider
    {
        IBaseEntityCollectionResponse<EmployeeReportForm5Monthly> GetEmployeeReportForm5MonthlyDataList(EmployeeReportForm5MonthlySearchRequest searchRequest);

    }
}
