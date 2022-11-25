using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class DashboardViewModel : IDashboardViewModel
    {
        public DashboardViewModel()
        {
            DashboardDTO = new Dashboard();
            List<Dashboard> RequestApprovalFieldMasterList = new List<Dashboard>();
            List<Dashboard> GeneralRequestApprovalFieldMasterList = new List<Dashboard>();
            TaskCodeList = new List<Dashboard>();
        }
        public List<Dashboard> TaskCodeList { get; set; }
        public IEnumerable<SelectListItem> TaskCodeListItems { get { return new SelectList(TaskCodeList, "TaskCode", "TaskDescription"); } }
        public List<UserModuleMaster> ModuleList { get; set; }
        public List<Dashboard> DashboardContentList { get; set; }
        
        //public IEnumerable<SelectListItem> TaskCodeListItems { get { return new SelectList(ModuleList, "ModuleCode", "ModuleName"); } }

        public Dashboard DashboardDTO
        {
            get;
            set;
        }

        public List<Dashboard> RequestApprovalFieldMasterList
        {
            get;
            set;
        }

        public List<Dashboard> GeneralRequestApprovalFieldMasterList
        {
            get;
            set;
        }

        #region -------------- Deshboard Allocation ---------------

        public string AdminRoleCode
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.AdminRoleCode : string.Empty;
            }
            set
            {
                DashboardDTO.AdminRoleCode = value;
            }
        }
        public int AdminRoleMasterID
        {
            get
            {
                return (DashboardDTO != null && DashboardDTO.AdminRoleMasterID > 0) ? DashboardDTO.AdminRoleMasterID : new int();
            }
            set
            {
                DashboardDTO.AdminRoleMasterID = value;
            }
        }

        public int DashboardContentDetailsID
        {
            get
            {
                return (DashboardDTO != null && DashboardDTO.DashboardContentDetailsID > 0) ? DashboardDTO.DashboardContentDetailsID : new int();
            }
            set
            {
                DashboardDTO.DashboardContentDetailsID = value;
            }
        }
        public string ModuleCode
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.ModuleCode : string.Empty;
            }
            set
            {
                DashboardDTO.ModuleCode = value;
            }
        }
        public string ModuleName
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.ModuleName : string.Empty;
            }
            set
            {
                DashboardDTO.ModuleName = value;
            }
        }

        public string ContentTitle
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.ContentTitle : string.Empty;
            }
            set
            {
                DashboardDTO.ContentTitle = value;
            }
        }

        public bool ContentStatus
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.ContentStatus : false;
            }
            set
            {
                DashboardDTO.ContentStatus = value;
            }
        }

        public int DashboardAllocationID
        {
            get
            {
                return (DashboardDTO != null && DashboardDTO.DashboardAllocationID > 0) ? DashboardDTO.DashboardAllocationID : new int();
            }
            set
            {
                DashboardDTO.DashboardAllocationID = value;
            }
        }
        #endregion
        #region -------------- TaskNotificationMaster ---------------
        public int PersonID
        {
            get
            {
                return (DashboardDTO != null && DashboardDTO.PersonID > 0) ? DashboardDTO.PersonID : new int();
            }
            set
            {
                DashboardDTO.PersonID = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return (DashboardDTO != null) ? DashboardDTO.TaskCode : string.Empty;
            }
            set
            {
                DashboardDTO.TaskCode = value;
            }
        }
        #endregion

    }
}
