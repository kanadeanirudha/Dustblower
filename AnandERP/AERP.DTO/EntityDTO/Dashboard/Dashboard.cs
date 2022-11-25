using AERP.Base.DTO;
using System;
namespace AERP.DTO
{
    public class Dashboard : BaseDTO
    {
        #region -------------- Dashboard ---------------

        public string ContentCode
        {
            get;
            set;
        }
        public string ContentTitle
        {
            get;
            set;
        }
        public int AdminRoleMasterID
        {
            get;set;
        }
        public int ID
        {
            get;
            set;
        }
        #endregion

        #region ------------------------ Dashboard Allocation---------------------------

        public string AdminRoleCode
        {
            get; set;
        }
        public int DashboardContentDetailsID
        {
            get; set;
        }
        public string ModuleCode
        {
            get; set;
        }
        public string ModuleName
        {
            get; set;
        }
        public int DashboardAllocationID
        {
            get; set;
        }
        public bool ContentStatus
        {
            get; set;
        }
        #endregion
        #region -------------- TaskNotificationMaster ---------------
        public int PersonID
        {
            get;
            set;
        }
        public string TaskCode
        {
            get;
            set;
        }
        public string TaskDescription
        {
            get;
            set;
        }
        #endregion
        public string errorMessage
        {
            get;
            set;
        }
        public int CreatedBy { get; set; }
        public int DeletedBy { get; set; }
    }
}
