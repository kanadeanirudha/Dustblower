﻿using AERP.Base.DTO;
using AERP.DTO;
using AERP.Business.BusinessAction;
using AERP.ExceptionManager;
using AERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AERP.Common;

namespace AERP.Web.UI.Controllers
{
    public class SaleContractFixAttendanceController : BaseController
    {
        ISaleContractFixAttendanceBA _SaleContractFixAttendanceBA = null;
        ISaleContractMasterBA _SaleContractMasterBA = null;
        ISaleContractAttendanceBA _SaleContractAttendanceBA = null;

        string _centreCode = string.Empty;
        string _designationId = string.Empty;
        private readonly ILogger _logException;
        ActionModeEnum actionModeEnum;
        string _actionMode = string.Empty;
        string _sortBy = string.Empty;
        string _searchBy = string.Empty;
        string _sortDirection = string.Empty;
        int _startRow;
        int _rowLength;
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

        public SaleContractFixAttendanceController()
        {
            _SaleContractFixAttendanceBA = new SaleContractFixAttendanceBA();
            _SaleContractMasterBA = new SaleContractMasterBA();
            _SaleContractAttendanceBA = new SaleContractAttendanceBA();

        }

        #region Controller Methods

        /// <summary>
        /// First Load When controller call List Method
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (Convert.ToString(Session["UserType"]) == "A" || ((Convert.ToInt32(Session["Sales Manager"]) > 0 || Convert.ToInt32(Session["Sales Manager:Entity"]) > 0) && IsApplied == true))
            {
                SaleContractFixAttendanceViewModel _SaleContractFixAttendanceViewModel = new SaleContractFixAttendanceViewModel();

                int AdminRoleMasterID = 0;
                if (Session["RoleID"] == null)
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["DefaultRoleID"])) ? Convert.ToInt32(Session["DefaultRoleID"]) : 0;
                }

                else
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])) ? Convert.ToInt32(Session["RoleID"]) : 0;
                }

                List<AdminRoleApplicableDetails> listAdminRoleApplicableDetails = GetAdminRoleApplicableCentreBySalesManager(AdminRoleMasterID);
                AdminRoleApplicableDetails a = null;
                foreach (var item in listAdminRoleApplicableDetails)
                {
                    a = new AdminRoleApplicableDetails();
                    a.CentreCode = item.CentreCode;
                    a.CentreName = item.CentreName;
                    _SaleContractFixAttendanceViewModel.ListGetAdminRoleApplicableCentre.Add(a);
                }

                return View("/Views/Contract/SaleContractFixAttendance/Index.cshtml", _SaleContractFixAttendanceViewModel);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        public ActionResult List(string actionMode, string SaleContractMasterID, string SaleContractBillingSpanID)
        {
            try
            {
                SaleContractFixAttendanceViewModel model = new SaleContractFixAttendanceViewModel();
                if (!string.IsNullOrEmpty(actionMode))
                {
                    TempData["ActionMode"] = actionMode;
                }

                model.SaleContractFixAttendanceList = GetFixItemDataList(SaleContractMasterID, SaleContractBillingSpanID);

                return PartialView("/Views/Contract/SaleContractFixAttendance/List.cshtml", model);

            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult AddFixItemData(string SaleContractMasterID)
        {
            SaleContractFixAttendanceViewModel model = new SaleContractFixAttendanceViewModel();

            model.SaleContractFixAttendanceDTO.ConnectionString = _connectioString;
            model.SaleContractMasterID = Convert.ToInt64(SaleContractMasterID);
            model.SaleContractSpanList = GetSpanListBySaleContractMaster(SaleContractMasterID);

            return PartialView("/Views/Contract/SaleContractFixAttendance/AddFixItemData.cshtml", model);
        }

        [HttpGet]
        public ActionResult GetFixItemData(string SaleContractMasterID, string SaleContractBillingSpanID)
        {
            SaleContractFixAttendanceViewModel model = new SaleContractFixAttendanceViewModel();

            model.SaleContractFixAttendanceList = GetFixItemDataList(SaleContractMasterID, SaleContractBillingSpanID);

            return PartialView("/Views/Contract/SaleContractFixAttendance/GetFixItemData.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddFixItemData(SaleContractFixAttendanceViewModel model)
        {
            try
            {
                if (model != null && model.SaleContractFixAttendanceDTO != null)
                {
                    model.SaleContractFixAttendanceDTO.ConnectionString = _connectioString;
                    model.SaleContractFixAttendanceDTO.SaleContractBillingSpanID = model.SaleContractBillingSpanID;
                    model.SaleContractFixAttendanceDTO.SaleContractMasterID = model.SaleContractMasterID;
                    model.SaleContractFixAttendanceDTO.XMLstringForFixItemData = model.XMLstringForFixItemData;

                    model.SaleContractFixAttendanceDTO.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    model.SaleContractFixAttendanceDTO.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    IBaseEntityResponse<SaleContractFixAttendance> response = _SaleContractFixAttendanceBA.InsertSaleContractFixAttendance(model.SaleContractFixAttendanceDTO);

                    model.SaleContractFixAttendanceDTO.errorMessage = CheckError((response.Entity != null) ? response.Entity.ErrorCode : 20, ActionModeEnum.Insert);

                    return Json(model.SaleContractFixAttendanceDTO.errorMessage, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json("Please review your form");
                }

            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }

        }

        [HttpGet]
        public ActionResult ContractFixAttendanceApproval(int PersonID, string TNDID, string TNMID, string GTRDID1, string TaskCode, string StageSequenceNumber, string IsLast)
        {
            SaleContractFixAttendanceViewModel model = new SaleContractFixAttendanceViewModel();

            model.PersonID = Convert.ToInt32(PersonID);
            model.TaskNotificationDetailsID = Convert.ToInt32(TNDID);
            model.TaskNotificationMasterID = Convert.ToInt32(TNMID);
            model.GeneralTaskReportingDetailsID = Convert.ToInt32(GTRDID1);
            model.TaskCode = TaskCode;
            model.StageSequenceNumber = Convert.ToInt32(StageSequenceNumber);
            model.IsLastRecord = Convert.ToBoolean(IsLast);
            model.SaleContractFixAttendanceList = GetContractFixAttendanceRecord(PersonID, Convert.ToInt32(TNMID), model.GeneralTaskReportingDetailsID);
            model.ID = model.SaleContractFixAttendanceList[0].ID;
            model.ContractNumber = model.SaleContractFixAttendanceList[0].ContractNumber;
            model.SaleContractBillingSpanName = model.SaleContractFixAttendanceList[0].SaleContractBillingSpanName;

            return PartialView("/Views/Contract/SaleContractFixAttendance/ContractFixAttendanceApproval.cshtml", model);
        }

        [HttpPost]
        public ActionResult ContractFixAttendanceApproval(SaleContractFixAttendanceViewModel model)
        {
            try
            {
                if (model != null && model.SaleContractFixAttendanceDTO != null)
                {
                    model.SaleContractFixAttendanceDTO.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                    model.SaleContractFixAttendanceDTO.PersonID = model.PersonID;
                    model.SaleContractFixAttendanceDTO.ID = model.ID;
                    model.SaleContractFixAttendanceDTO.IsLastRecord = Convert.ToBoolean(model.IsLastRecord);
                    model.SaleContractFixAttendanceDTO.TaskNotificationMasterID = model.TaskNotificationMasterID;
                    model.SaleContractFixAttendanceDTO.TaskNotificationDetailsID = model.TaskNotificationDetailsID;
                    model.SaleContractFixAttendanceDTO.GeneralTaskReportingDetailsID = model.GeneralTaskReportingDetailsID;
                    model.SaleContractFixAttendanceDTO.StageSequenceNumber = model.StageSequenceNumber;
                    model.SaleContractFixAttendanceDTO.ApprovedStatus = model.ApprovedStatus;
                    model.SaleContractFixAttendanceDTO.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    IBaseEntityResponse<SaleContractFixAttendance> response = _SaleContractFixAttendanceBA.InsertContractFixAttendanceApproval(model.SaleContractFixAttendanceDTO);
                    model.SaleContractFixAttendanceDTO.errorMessage = CheckError((response.Entity != null) ? response.Entity.ErrorCode : 20, ActionModeEnum.Insert);
                }
                return Json(model.SaleContractFixAttendanceDTO.errorMessage, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        [NonAction]
        protected List<SaleContractFixAttendance> GetContractFixAttendanceRecord(int personId, int taskNotificationMasterID, int GeneralTaskReportingDetailsID)
        {
            SaleContractFixAttendanceSearchRequest searchRequest = new SaleContractFixAttendanceSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            searchRequest.PersonID = personId;
            searchRequest.TaskNotificationMasterID = taskNotificationMasterID;
            searchRequest.GeneralTaskReportingDetailsID = GeneralTaskReportingDetailsID;
            List<SaleContractFixAttendance> listSaleContractFixAttendanceDetails = new List<SaleContractFixAttendance>();
            IBaseEntityCollectionResponse<SaleContractFixAttendance> baseEntityCollectionResponse = _SaleContractFixAttendanceBA.GetContractFixAttendanceForApproval(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listSaleContractFixAttendanceDetails = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }
            return listSaleContractFixAttendanceDetails;
        }
        #endregion

        #region Methods
        protected List<SaleContractAttendance> GetSpanListBySaleContractMaster(string SaleContractMasterID)
        {

            SaleContractAttendanceSearchRequest searchRequest = new SaleContractAttendanceSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            searchRequest.SaleContractMasterID = Convert.ToInt64(SaleContractMasterID);

            List<SaleContractAttendance> listSaleContractAttendance = new List<SaleContractAttendance>();
            IBaseEntityCollectionResponse<SaleContractAttendance> baseEntityCollectionResponse = _SaleContractAttendanceBA.GetSpanListBySaleContractMaster(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listSaleContractAttendance = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }
            return listSaleContractAttendance;
        }

        //protected List<SaleContractFixAttendance> GetSaleContractFixAttendanceSpanWise(string SaleContractMasterID, string SaleContractBillingSpanID)
        //{

        //    SaleContractFixAttendanceSearchRequest searchRequest = new SaleContractFixAttendanceSearchRequest();
        //    searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        //    searchRequest.SaleContractMasterID = Convert.ToInt64(SaleContractMasterID);
        //    searchRequest.SaleContractBillingSpanID = Convert.ToInt64(SaleContractBillingSpanID);

        //    List<SaleContractFixAttendance> listSaleContractFixAttendance = new List<SaleContractFixAttendance>();
        //    IBaseEntityCollectionResponse<SaleContractFixAttendance> baseEntityCollectionResponse = _SaleContractFixAttendanceBA.GetSaleContractFixAttendanceSpanWise(searchRequest);
        //    if (baseEntityCollectionResponse != null)
        //    {
        //        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
        //        {
        //            listSaleContractFixAttendance = baseEntityCollectionResponse.CollectionResponse.ToList();
        //        }
        //    }
        //    return listSaleContractFixAttendance;
        //}

        protected List<SaleContractFixAttendance> GetFixItemDataList(string SaleContractMasterID, string SaleContractBillingSpanID)
        {

            SaleContractFixAttendanceSearchRequest searchRequest = new SaleContractFixAttendanceSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            searchRequest.SaleContractMasterID = Convert.ToInt64(SaleContractMasterID);
            searchRequest.SaleContractBillingSpanID = Convert.ToInt64(SaleContractBillingSpanID);

            List<SaleContractFixAttendance> listSaleContractFixAttendance = new List<SaleContractFixAttendance>();
            IBaseEntityCollectionResponse<SaleContractFixAttendance> baseEntityCollectionResponse = _SaleContractFixAttendanceBA.GetFixItemDataList(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listSaleContractFixAttendance = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }
            return listSaleContractFixAttendance;
        }
        #endregion

        #region AjaxHandler

        #endregion
    }
}


