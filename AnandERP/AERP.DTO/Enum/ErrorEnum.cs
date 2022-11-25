using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.DTO
{
    public enum ErrorEnum
    {
        AllOk,
        DuplicateEntry = 11,
        LimitExceeds = 9,
        DependantEntry = 547,
        WorkFlowNotDefined = 10,

    }
}
