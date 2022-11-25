using AMS.Base.DTO;
using AMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.DataProvider
{
    public interface IGeneralHolidaysDataProvider
    {
        IBaseEntityResponse<GeneralHolidays> InsertGeneralHolidays(GeneralHolidays item);
        IBaseEntityResponse<GeneralHolidays> UpdateGeneralHolidays(GeneralHolidays item);
        IBaseEntityResponse<GeneralHolidays> DeleteGeneralHolidays(GeneralHolidays item);
        IBaseEntityCollectionResponse<GeneralHolidays> GetGeneralHolidaysBySearch(GeneralHolidaysSearchRequest searchRequest);
        IBaseEntityResponse<GeneralHolidays> GetGeneralHolidaysByID(GeneralHolidays item);
        IBaseEntityCollectionResponse<GeneralHolidays> GetHolidayAndWeeklyOffDayByEmployeeID(GeneralHolidaysSearchRequest searchRequest);
        
    }
}
