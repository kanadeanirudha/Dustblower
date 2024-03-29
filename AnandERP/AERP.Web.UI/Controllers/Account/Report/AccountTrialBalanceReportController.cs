﻿using AERP.Base.DTO;
using AERP.DTO;
using AERP.Business.BusinessAction;
using AERP.ViewModel;
using System;
using AERP.ExceptionManager;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AERP.Common;
using System.IO;

namespace AERP.Web.UI.Controllers
{
    public class AccountTrialBalanceReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IAccountTrialBalanceReportBA _accountTrialBalanceBABA = null;

        private readonly ILogger _logException;
        protected static int _balanesheetMstID;
        protected static string _balancesheetName = string.Empty;
        protected static int _accSessionId;
        protected static string _sessionFromDate = string.Empty;
        protected static string _sessionUptoDate = string.Empty;
        protected static string _GroupBy = string.Empty;
        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public AccountTrialBalanceReportController()
        {
            _accountTrialBalanceBABA = new AccountTrialBalanceReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                if (Convert.ToInt32(Session["Account Manager"]) > 0 || Convert.ToInt32(Session["Admin Manager"]) > 0)
                {
                    AccountTrialBalanceReportViewModel model = new AccountTrialBalanceReportViewModel();
                    model.ListAccountSessionMasterReport = GetAllAccountSession();
                    List<SelectListItem> li_GroupBy = new List<SelectListItem>();
                    li_GroupBy.Add(new SelectListItem { Text = "Account Wise", Value = "A" });
                    li_GroupBy.Add(new SelectListItem { Text = "Group Wise", Value = "G" });
                    li_GroupBy.Add(new SelectListItem { Text = "Category Wise", Value = "C" });
                    ViewData["GroupBy"] = new SelectList(li_GroupBy, "Value", "Text", model.AccountTrialBalanceReportDTO.GroupBy);
                    return View("/Views/Accounts/Report/AccountTrialBalanceReport/Index.cshtml", model);
                }
                else
                {
                    return RedirectToAction("UnauthorizedAccess", "Home");
                }
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(AccountTrialBalanceReportViewModel model)
        {
            try
            {
                model.ListAccountSessionMasterReport = GetAllAccountSession();
                List<SelectListItem> li_GroupBy = new List<SelectListItem>();
                li_GroupBy.Add(new SelectListItem { Text = "Account Wise", Value = "A" });
                li_GroupBy.Add(new SelectListItem { Text = "Group Wise", Value = "G" });
                li_GroupBy.Add(new SelectListItem { Text = "Category Wise", Value = "C" });
                ViewData["GroupBy"] = new SelectList(li_GroupBy, "Value", "Text", model.AccountTrialBalanceReportDTO.GroupBy);

                if (model.IsPosted == true)
                {
                    _balanesheetMstID = model.BalanceSheetMstID;
                    _balancesheetName = model.AccBalsheetName;
                    _accSessionId = model.AccountSessionID;
                    _sessionFromDate = model.SessionFromDate;
                    _sessionUptoDate = model.SessionUptoDate;
                    _GroupBy = model.GroupBy;
                    model.IsPosted = false;
                }
                else
                {
                    model.BalanceSheetMstID = _balanesheetMstID;
                    model.AccBalsheetName = _balancesheetName;
                    model.AccountSessionID = _accSessionId;
                    model.SessionFromDate = _sessionFromDate;
                    model.SessionUptoDate = _sessionUptoDate;
                    model.GroupBy = _GroupBy;
                }
                return View("/Views/Accounts/Report/AccountTrialBalanceReport/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }
        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------
        public List<OrganisationStudyCentreMaster> GetReportHeader()
        {
            List<OrganisationStudyCentreMaster> listOrganisationStudyCentreMaster = new List<OrganisationStudyCentreMaster>();
            listOrganisationStudyCentreMaster = GetStudyCentreDetailsForReports(string.Empty, _balanesheetMstID);
            return listOrganisationStudyCentreMaster;
        }

        public List<AccountTrialBalanceReport> GetAccountDetailsForReport()
        {
            try
            {
                List<AccountTrialBalanceReport> listaccountDayBookReport = new List<AccountTrialBalanceReport>();
                AccountTrialBalanceReportSearchRequest searchRequest = new AccountTrialBalanceReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_balanesheetMstID > 0 && _sessionFromDate != string.Empty && _sessionUptoDate != string.Empty && _GroupBy != string.Empty && _accSessionId != 0)
                {
                    searchRequest.AccBalsheetMstId = _balanesheetMstID;
                    searchRequest.AccBalsheetName = _balancesheetName;
                    searchRequest.SessionFromDate = _sessionFromDate;
                    searchRequest.SessionUptoDate = _sessionUptoDate;
                    searchRequest.GroupBy = _GroupBy;
                    searchRequest.AccountSessionID = _accSessionId;
                    IBaseEntityCollectionResponse<AccountTrialBalanceReport> baseEntityCollectionResponse = _accountTrialBalanceBABA.GetBySearch(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listaccountDayBookReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listaccountDayBookReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }
        #endregion
    }
}