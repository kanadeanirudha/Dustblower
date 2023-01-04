using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AERP.Base.DTO;
using AERP.DTO;
using AERP.Business.BusinessAction;
using AERP.ExceptionManager;
using AERP.ViewModel;
using AERP.Common;
using AERP.DataProvider;
using System.Configuration;
using AERP.Web.UI;
using AERP.Web.UI.HtmlHelperExtensions;
using AERP.Web.UI.Models;

namespace AERP.Web.UI.Controllers
{

    public class TaskNotificationController : BaseController
    {
        ITaskNotificationBA _TaskNotificationBA = null;
        IDashboardBA _DashboardBA = null;
        IGeneralTaskReportingDetailsBA _generalTaskReportingDetailsBA = null;
        AdminRoleApplicableDetailsBaseViewModel _adminRoleApplicableDetailsBaseViewModel = null;
        private readonly ILogger _logException;
        ActionModeEnum actionModeEnum;
        string _actionMode = string.Empty;
        string _sortOrder = string.Empty;
        string _sortBy = string.Empty;
        string _searchBy = string.Empty;
        string _sortDirection = string.Empty;
        int _startRow;
        int _rowLength;
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

        public TaskNotificationController()
        {
            _TaskNotificationBA = new TaskNotificationBA();
            _generalTaskReportingDetailsBA = new GeneralTaskReportingDetailsBA();
            _adminRoleApplicableDetailsBaseViewModel = new AdminRoleApplicableDetailsBaseViewModel();
            _DashboardBA = new DashboardBA();
        }

        #region ------------------Controller Methods------------------

        //public ActionResult GetNotificationCount()
        //{
        //    MessagesRepository _messageRepository = new MessagesRepository();
        //    return Json(_messageRepository.GetAllMessages(), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Index()
        {
            TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
            if (Session["UserType"] != null)
            {
                if (Session["UserType"].ToString() == "E")
                {
                    _adminRoleApplicableDetailsBaseViewModel.RoleList = GetRoleListByUserID();
                    if (_adminRoleApplicableDetailsBaseViewModel.RoleList.Count > 0)
                    {

                        //Create selectlistitem list 
                        List<SelectListItem> items = new List<SelectListItem>();
                        SelectListItem s = null;

                        //add the empty selection
                        s = new SelectListItem();
                        //s.Value = "";
                        //s.Text = "";
                        //items.Add(s);
                        foreach (var t in _adminRoleApplicableDetailsBaseViewModel.RoleList)
                        {
                            s = new SelectListItem();
                            s.Text = t.AdminRoleMasterID.ToString();
                            s.Value = t.AdminRoleCode.ToString();
                            items.Add(s);

                            if (t.RoleType == "Regular")
                            {
                                //DefaultRoleCode = t.AdminRoleCode;
                                Session["DefaultRoleID"] = t.AdminRoleMasterID;
                            }

                        }

                        ViewData["UserType"] = "E";

                    }

                    _TaskNotificationViewModel.TaskNotificationContentList = GetDashboardContentListByAdminRoleID(Convert.ToInt32(Session["DefaultRoleID"]));
                    if (_TaskNotificationViewModel.TaskNotificationContentList.Count > 0)
                    {
                        return View("/Views/TaskNotification/TaskNotificationIndex.cshtml", _TaskNotificationViewModel);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }


            }
            else
            {

                //return View("Login","Account");
                return RedirectToAction("Login", "Account");
                // return PartialView("Login");
            }
        }


        public ActionResult NotificationList(string actionMode, string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.PersonID = Convert.ToInt32(Session["PersonID"]);
                _TaskNotificationViewModel.TaskCodeList = GetTaskCodeList(_TaskNotificationViewModel.PersonID);
                if (TaskCode == null)
                {
                    _TaskNotificationViewModel.TaskCode = "LA";

                }
                else
                {
                    _TaskNotificationViewModel.TaskCode = TaskCode;
                }
                if (!string.IsNullOrEmpty(actionMode))
                {
                    TempData["ActionMode"] = actionMode;
                }
                var AdminRoleMasterID = 0;
                if ((Session["UserType"] != null))
                {
                    AdminRoleMasterID = Convert.ToInt32(Session["RoleID"]);
                    if (Session["UserType"].ToString() == "E")
                    {
                        _TaskNotificationViewModel.ModuleList = GetModuleListByUserID(AdminRoleMasterID);
                    }
                    else if (Session["UserType"].ToString() == "A")
                    {
                        _TaskNotificationViewModel.ModuleList = GetModuleListForAdmin();
                    }
                    // ViewData["ModuleRowCount"] = _TaskNotificationViewModel.ModuleList.Count()/4;
                }

                return PartialView("NotificationList", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult NotificationListCount()
        {
            GeneralTaskReportingDetailsViewModel _generalTaskReportingDetailsViewModel = new GeneralTaskReportingDetailsViewModel();
            _generalTaskReportingDetailsViewModel.EmployeeID = Convert.ToInt32(Session["PersonID"]);
            _generalTaskReportingDetailsViewModel.GeneralTaskReportingDetailsDTO.ConnectionString = _connectioString;
            IBaseEntityResponse<GeneralTaskReportingDetails> response = _generalTaskReportingDetailsBA.GetTotalPendingCountTaskEmployeewise(_generalTaskReportingDetailsViewModel.GeneralTaskReportingDetailsDTO);
            int result = 0;
            if (response != null && response.Entity != null)
            {
                result = Convert.ToInt32(response.Entity.TotalPendingRequest);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult List(string actionMode, string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.PersonID = Convert.ToInt32(Session["PersonID"]);
                _TaskNotificationViewModel.TaskCodeList = GetTaskCodeList(_TaskNotificationViewModel.PersonID);
                if (TaskCode == null)
                {
                    _TaskNotificationViewModel.TaskCode = "LA";

                }
                else
                {
                    _TaskNotificationViewModel.TaskCode = TaskCode;
                }
                if (!string.IsNullOrEmpty(actionMode))
                {
                    TempData["ActionMode"] = actionMode;
                }
                var AdminRoleMasterID = 0;
                if ((Session["UserType"] != null))
                {
                    AdminRoleMasterID = Convert.ToInt32(Session["RoleID"]);
                    if (Session["UserType"].ToString() == "E")
                    {
                        _TaskNotificationViewModel.ModuleList = GetModuleListByUserID(AdminRoleMasterID);
                    }
                    else if (Session["UserType"].ToString() == "A")
                    {
                        _TaskNotificationViewModel.ModuleList = GetModuleListForAdmin();
                    }
                    // ViewData["ModuleRowCount"] = _TaskNotificationViewModel.ModuleList.Count()/4;
                }

                //return PartialView("List", _TaskNotificationViewModel);
                return PartialView("/Views/Home/ListV2.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }
        public ActionResult PendingRequestList(string actionMode, string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.PersonID = Convert.ToInt32(Session["PersonID"]);
                _TaskNotificationViewModel.TaskCodeList = GetTaskCodeList(_TaskNotificationViewModel.PersonID);
                if (TaskCode == null)
                {
                    _TaskNotificationViewModel.TaskCode = "LA";

                }
                else
                {
                    _TaskNotificationViewModel.TaskCode = TaskCode;
                }
                if (!string.IsNullOrEmpty(actionMode))
                {
                    TempData["ActionMode"] = actionMode;
                }
                return PartialView("RequestOnTaskNotification", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }



        public ActionResult PendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                //_TaskNotificationViewModel.PersonID = Convert.ToInt32(Session["PersonID"]);
                //_TaskNotificationViewModel.TaskCodeList = GetTaskCodeList(_TaskNotificationViewModel.PersonID);               
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/Home/PendingRequests.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult PurchaseRequirementPendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/PurchaseRequirementPendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        //public ActionResult PurchaseRequsitionPendingRequest(string TaskCode)
        //{
        //    try
        //    {
        //        TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
        //        _TaskNotificationViewModel.TaskCode = TaskCode;
        //        return PartialView("/Views/TaskNotification/PurchaseRequisitionPendingRequest.cshtml", _TaskNotificationViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logException.Error(ex.Message);
        //        throw;
        //    }
        //}

        public ActionResult PurchaseOrderPendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/PurchaseOrderPendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult SalesOrderPendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/SalesOrderPendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult ManPowerItemPendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/ManPowerItemPendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult SaleContractPendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/SaleContractPendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult ContractAttendancePendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/ContractAttendancePendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult ContractFixAttendancePendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/ContractFixAttendancePendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult SalesInvoicePendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/SalesInvoicePendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult CancelSalesInvoicePendingRequest(string TaskCode)
        {
            try
            {
                TaskNotificationViewModel _TaskNotificationViewModel = new TaskNotificationViewModel();
                _TaskNotificationViewModel.TaskCode = TaskCode;
                return PartialView("/Views/TaskNotification/CancelSalesInvoicePendingRequest.cshtml", _TaskNotificationViewModel);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public ActionResult CustomerSaleRatePendingRequest(string TaskCode)
        {
            try
            {
                return (ActionResult)PartialView("/Views/TaskNotification/CustomerSaleRatePendingRequest.cshtml", (object)new TaskNotificationViewModel()
                {
                    TaskCode = TaskCode
                });
            }
            catch (Exception ex)
            {
                _logException.Error((object)ex.Message);
                throw;
            }
        }

        public ActionResult CustomerSaleRateDeletePendingRequest(string TaskCode)
        {
            try
            {
                return (ActionResult)PartialView("/Views/TaskNotification/CustomerSaleRateDeletePendingRequest.cshtml", (object)new TaskNotificationViewModel()
                {
                    TaskCode = TaskCode
                });
            }
            catch (Exception ex)
            {
                _logException.Error((object)ex.Message);
                throw;
            }
        }
        #endregion

        #region ---------------------Methods-ddd----------------------
        protected List<TaskNotification> GetDashboardContentListByAdminRoleID(int AdminRoleMasterID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            searchRequest.AdminRoleMasterID = AdminRoleMasterID;
            List<TaskNotification> ModuleList = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetDashboardContentListByAdminRoleID(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    ModuleList = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }

            return ModuleList;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingPORequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "PurchaseRequisitionNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "PurchaseRequisitionNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listPORequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingPORequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingPORequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingPORequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listPORequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listPORequestViewModel;
        }


        public IEnumerable<TaskNotificationViewModel> GetPendingSORequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "SalesOrderNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "SalesOrderNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listPORequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingPORequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingPORequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingPORequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listPORequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listPORequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingMPRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "CompanyName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "CompanyName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listMPRequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingMPRequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingMPRequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingMPRequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listMPRequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listMPRequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingSCRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "CompanyName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "CompanyName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listSCRequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingSCRequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingSCRequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingSCRequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listSCRequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listSCRequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingCARequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "ContractNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "ContractNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listCARequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingCARequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingCARequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingCARequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listCARequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listCARequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingCFRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "ContractNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "ContractNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listCFRequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingCFRequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingCFRequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingCFRequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listCFRequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listCFRequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingSIRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "CustomerInvoiceNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "CustomerInvoiceNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listSIRequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingSIRequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingSIRequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingSIRequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listSIRequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listSIRequestViewModel;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingCIRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse(_actionMode, out actionModeEnum))     // checks actionMode i.e. Insert or Update // 
            {
                if (actionModeEnum == ActionModeEnum.Insert)        // parameters for SelectAll procedures under Insert or Update mode condition
                {
                    searchRequest.SortBy = "CustomerInvoiceNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "CustomerInvoiceNumber";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;                        // parameters for SelectAll procedures under normal condition
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            List<TaskNotificationViewModel> listCIRequestViewModel = new List<TaskNotificationViewModel>();
            List<TaskNotification> listPendingCIRequest = new List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> baseEntityCollectionResponse = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listPendingCIRequest = baseEntityCollectionResponse.CollectionResponse.ToList();
                    foreach (TaskNotification item in listPendingCIRequest)
                    {
                        TaskNotificationViewModel TaskNotificationViewModel = new TaskNotificationViewModel();
                        TaskNotificationViewModel.TaskNotificationDTO = item;
                        listCIRequestViewModel.Add(TaskNotificationViewModel);
                    }
                }
            }

            TotalRecords = baseEntityCollectionResponse.TotalRecords;
            return listCIRequestViewModel;
        }

        protected List<Dashboard> GetTaskCodeList(int PersonID)
        {
            DashboardSearchRequest searchRequest = new DashboardSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            searchRequest.PersonID = PersonID;
            List<Dashboard> listTaskCode = new List<Dashboard>();
            IBaseEntityCollectionResponse<Dashboard> baseEntityCollectionResponse = _DashboardBA.GetGeneralTaskModelListByPersonID(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listTaskCode = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }
            return listTaskCode;
        }

        public IEnumerable<TaskNotificationViewModel> GetPendingCSRRequest(out int TotalRecords, string TaskCode, int PersonID)
        {
            TaskNotificationSearchRequest searchRequest = new TaskNotificationSearchRequest();
            searchRequest.ConnectionString = Convert.ToString((object)ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            _actionMode = Convert.ToString(TempData["ActionMode"]);
            if (Enum.TryParse<ActionModeEnum>(_actionMode, out actionModeEnum))
            {
                if (actionModeEnum == ActionModeEnum.Insert)
                {
                    searchRequest.SortBy = "CustomerName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.SearchBy = string.Empty;
                    searchRequest.SortDirection = "Desc";
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
                if (actionModeEnum == ActionModeEnum.Update)
                {
                    searchRequest.SortBy = "CustomerName";
                    searchRequest.StartRow = 0;
                    searchRequest.EndRow = 10;
                    searchRequest.PersonID = PersonID;
                    searchRequest.TaskCode = TaskCode;
                }
            }
            else
            {
                searchRequest.SortBy = _sortBy;
                searchRequest.StartRow = _startRow;
                searchRequest.EndRow = _startRow + _rowLength;
                searchRequest.SearchBy = _searchBy;
                searchRequest.SortDirection = _sortDirection;
                searchRequest.PersonID = PersonID;
                searchRequest.TaskCode = TaskCode;
            }
            System.Collections.Generic.List<TaskNotificationViewModel> pendingCsrRequest = new System.Collections.Generic.List<TaskNotificationViewModel>();
            System.Collections.Generic.List<TaskNotification> taskNotificationList = new System.Collections.Generic.List<TaskNotification>();
            IBaseEntityCollectionResponse<TaskNotification> searchForTaskApproval = _TaskNotificationBA.GetBySearchForTaskApproval(searchRequest);
            if (searchForTaskApproval != null && searchForTaskApproval.CollectionResponse != null && searchForTaskApproval.CollectionResponse.Count > 0)
            {
                foreach (TaskNotification taskNotification in searchForTaskApproval.CollectionResponse.ToList<TaskNotification>())
                    pendingCsrRequest.Add(new TaskNotificationViewModel()
                    {
                        TaskNotificationDTO = taskNotification
                    });
            }
            TotalRecords = searchForTaskApproval.TotalRecords;
            return (IEnumerable<TaskNotificationViewModel>)pendingCsrRequest;
        }
        #endregion


        #region ----------------------AjaxHandler---------------------

        public ActionResult AjaxHandlerMyDataTablePurchaseOrderRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingPORequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "F.PurchaseRequisitionNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "F.PurchaseRequisitionNumber Like '%" + param.sSearch + "%' or F.TransDate Like '%" + param.sSearch + "%' or H.Vender Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "F.TransDate";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "F.PurchaseRequisitionNumber Like '%" + param.sSearch + "%' or F.TransDate Like '%" + param.sSearch + "%' or H.Vender Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "H.Vender";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "F.PurchaseRequisitionNumber Like '%" + param.sSearch + "%' or F.TransDate Like '%" + param.sSearch + "%' or H.Vender Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingPORequest = GetPendingPORequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingPORequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingPORequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.StudentName), Convert.ToString(c.SessionName), Convert.ToString(c.SectionDescription), Convert.ToString(c.TotalFeeAmount), Convert.ToString(c.FeeStructureMasterID), Convert.ToString(c.FeeStructureApplicableHistoryID), Convert.ToString(c.StudentID), Convert.ToString(c.PurchaseRequisitionNumber), Convert.ToString(c.TransDate), Convert.ToString(c.Vendor) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableSalesOrderRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingPORequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "SalesOrderNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "SalesOrderNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "CompanyName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "SalesOrderNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "TransDate";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "SalesOrderNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingPORequest = GetPendingSORequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingPORequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingPORequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.SalesOrderNumber), Convert.ToString(c.CompanyName), Convert.ToString(c.TransDate) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableManPowerRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingMPRequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "CompanyName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CompanyName Like '%" + param.sSearch + "%' or Designation Like '%" + param.sSearch + "%' or H.TotalAmount Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "Designation";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CompanyName Like '%" + param.sSearch + "%' or Designation Like '%" + param.sSearch + "%' or H.TotalAmount Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "TotalAmount";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CompanyName Like '%" + param.sSearch + "%' or Designation Like '%" + param.sSearch + "%' or H.TotalAmount Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingMPRequest = GetPendingMPRequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingMPRequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingMPRequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.CompanyName), Convert.ToString(c.Designation), Convert.ToString(c.TotalAmount) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableSaleContractRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingSCRequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "ContractNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or Customer Like '%" + param.sSearch + "%' or BillingType Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "Customer";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or Customer Like '%" + param.sSearch + "%' or BillingType Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "BillingType";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or Customer Like '%" + param.sSearch + "%' or BillingType Like '%" + param.sSearch + "%''";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingSCRequest = GetPendingSCRequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingSCRequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingSCRequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.ContractNumber), Convert.ToString(c.CompanyName), Convert.ToString(c.BillingType) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableContractAttendanceRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingCARequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "ContractNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or SaleContractBillingSpan Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "SaleContractBillingSpan";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or SaleContractBillingSpan Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;

            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingCARequest = GetPendingCARequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingCARequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingCARequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.ContractNumber), Convert.ToString(c.SaleContractBillingSpan) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableContractFixAttendanceRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingCFRequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "ContractNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or SaleContractBillingSpan Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "SaleContractBillingSpan";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "ContractNumber Like '%" + param.sSearch + "%' or SaleContractBillingSpan Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingCFRequest = GetPendingCFRequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingCFRequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingCFRequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.ContractNumber), Convert.ToString(c.SaleContractBillingSpan) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableSalesInvoiceRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingSIRequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "CustomerInvoiceNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "CompanyName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "TransDate";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingSIRequest = GetPendingSIRequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingSIRequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingSIRequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.CustomerInvoiceNumber), Convert.ToString(c.CompanyName), Convert.ToString(c.TransDate) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableCancelSalesInvoiceRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int TotalRecords;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc

            IEnumerable<TaskNotificationViewModel> filteredPendingCIRequest;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "CustomerInvoiceNumber";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 1:
                    _sortBy = "CompanyName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
                case 2:
                    _sortBy = "TransDate";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                    }
                    else
                    {
                        _searchBy = "CustomerInvoiceNumber Like '%" + param.sSearch + "%' or CompanyName Like '%" + param.sSearch + "%' or TransDate Like '%" + param.sSearch + "%'";        //this "if" block is added for search functionality
                    }
                    break;
            }
            _sortDirection = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;

            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                //  int AdminRoleMasterID;
                int PersonID = Convert.ToInt32(Session["PersonID"]);

                filteredPendingCIRequest = GetPendingCIRequest(out TotalRecords, TaskCode, PersonID);
            }
            else
            {
                filteredPendingCIRequest = new List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }

            var records = filteredPendingCIRequest.Skip(0).Take(param.iDisplayLength);
            var result = from c in records select new[] { Convert.ToString(c.TaskDescription), Convert.ToString(c.ApprovalStatus), Convert.ToString(c.MenuCodeLink), Convert.ToString(c.TaskNotificationDetailsID), Convert.ToString(c.TaskNotificationMasterID), Convert.ToString(c.GeneralTaskReportingDetailsID), Convert.ToString(c.StageSequenceNumber), Convert.ToString(c.IsLastRecordFlag), Convert.ToString(c.ApplicationDate), Convert.ToString(c.IsEngaged), (Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false)), Convert.ToString(c.CustomerInvoiceNumber), Convert.ToString(c.CompanyName), Convert.ToString(c.TransDate) };
            return Json(new { sEcho = param.sEcho, iTotalRecords = TotalRecords, iTotalDisplayRecords = TotalRecords, aaData = result }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AjaxHandlerMyDataTableCustomerSaleRateRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int int32_1 = Convert.ToInt32(Request["iSortCol_0"]);
            string str = Convert.ToString(Request["sSortDir_0"]);
            switch (Convert.ToInt32(int32_1))
            {
                case 0:
                    _sortBy = "CustomerName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%'";
                    break;
                case 1:
                    _sortBy = "ItemName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
                case 2:
                    _sortBy = "Location";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
                case 3:
                    _sortBy = "SalePrice";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
            }
            _sortDirection = str;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;
            int TotalRecords;
            IEnumerable<TaskNotificationViewModel> source;
            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                int int32_2 = Convert.ToInt32(Session["PersonID"]);
                source = GetPendingCSRRequest(out TotalRecords, TaskCode, int32_2);
            }
            else
            {
                source = (IEnumerable<TaskNotificationViewModel>)new System.Collections.Generic.List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }
            IEnumerable<string[]> strArrays = source.Skip<TaskNotificationViewModel>(0).Take<TaskNotificationViewModel>(param.iDisplayLength).Select<TaskNotificationViewModel, string[]>((Func<TaskNotificationViewModel, string[]>)(c => new string[15]
           {
        Convert.ToString(c.TaskDescription),
        Convert.ToString(c.ApprovalStatus),
        Convert.ToString(c.MenuCodeLink),
        Convert.ToString(c.TaskNotificationDetailsID),
        Convert.ToString(c.TaskNotificationMasterID),
        Convert.ToString(c.GeneralTaskReportingDetailsID),
        Convert.ToString(c.StageSequenceNumber),
        Convert.ToString(c.IsLastRecordFlag),
        Convert.ToString(c.ApplicationDate),
        Convert.ToString(c.IsEngaged),
        Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false),
        Convert.ToString(c.CustomerName),
        Convert.ToString(c.ItemName),
        Convert.ToString(c.Location),
        Convert.ToString(c.SalePrice)
           }));
            return (ActionResult)Json((object)new
            {
                sEcho = param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = strArrays
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxHandlerMyDataTableCustomerSaleRateDeleteRequest(JQueryDataTableParamModel param, string TaskCode)
        {
            int int32_1 = Convert.ToInt32(Request["iSortCol_0"]);
            string str = Convert.ToString(Request["sSortDir_0"]);
            switch (Convert.ToInt32(int32_1))
            {
                case 0:
                    _sortBy = "CustomerName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%'";
                    break;
                case 1:
                    _sortBy = "ItemName";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
                case 2:
                    _sortBy = "Location";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
                case 3:
                    _sortBy = "SalePrice";
                    if (string.IsNullOrEmpty(param.sSearch))
                    {
                        _searchBy = string.Empty;
                        break;
                    }
                    _searchBy = "CustomerName Like '%" + param.sSearch + "%' or ItemName Like '%" + param.sSearch + "%' or Location Like '%" + param.sSearch + "%' or SalePrice Like '%" + param.sSearch + "%";
                    break;
            }
            _sortDirection = str;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;
            int TotalRecords;
            IEnumerable<TaskNotificationViewModel> source;
            if (!string.IsNullOrEmpty(Convert.ToString(TaskCode)))
            {
                int int32_2 = Convert.ToInt32(Session["PersonID"]);
                source = GetPendingCSRRequest(out TotalRecords, TaskCode, int32_2);
            }
            else
            {
                source = (IEnumerable<TaskNotificationViewModel>)new System.Collections.Generic.List<TaskNotificationViewModel>();
                TotalRecords = 0;
            }
            IEnumerable<string[]> strArrays = source.Skip<TaskNotificationViewModel>(0).Take<TaskNotificationViewModel>(param.iDisplayLength).Select<TaskNotificationViewModel, string[]>((Func<TaskNotificationViewModel, string[]>)(c => new string[15]
           {
        Convert.ToString(c.TaskDescription),
        Convert.ToString(c.ApprovalStatus),
        Convert.ToString(c.MenuCodeLink),
        Convert.ToString(c.TaskNotificationDetailsID),
        Convert.ToString(c.TaskNotificationMasterID),
        Convert.ToString(c.GeneralTaskReportingDetailsID),
        Convert.ToString(c.StageSequenceNumber),
        Convert.ToString(c.IsLastRecordFlag),
        Convert.ToString(c.ApplicationDate),
        Convert.ToString(c.IsEngaged),
        Convert.ToString(c.EngagedByUserID) == Convert.ToString(Session["UserID"]) ? Convert.ToString(true) : Convert.ToString(false),
        Convert.ToString(c.CustomerName),
        Convert.ToString(c.ItemName),
        Convert.ToString(c.Location),
        Convert.ToString(c.SalePrice)
           }));
            return (ActionResult)Json((object)new
            {
                sEcho = param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = strArrays
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }


}