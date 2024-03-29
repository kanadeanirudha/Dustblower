﻿using AERP.Base.DTO;
using AERP.DTO;
using AERP.ExceptionManager;
using AERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AERP.Common;
using AERP.DataProvider;
using AERP.Business.BusinessAction;
using System.Globalization;
namespace AERP.Web.UI.Controllers
{
    public class ContractSalaryBonusReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IContractSalaryBonusReportBA _ContractSalaryBonusReportBA = null;
        private readonly ILogger _logException;
        protected static string _FromDate = string.Empty;
        protected static string _UptoDate = string.Empty;
        protected static Int64 _CustomerMasterBranchID = 0;
        protected static string _CustomerBranchMasterName = string.Empty;
        protected static string _ReportType = string.Empty;
        protected static string _ReportTypeDisplay = string.Empty; 
        protected static int _AccountSessionID = 0;
        protected static bool _ATMReport = false;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public ContractSalaryBonusReportController()
        {
            _ContractSalaryBonusReportBA = new ContractSalaryBonusReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                _FromDate = string.Empty;
                _UptoDate = string.Empty;
                _AccountSessionID = 0;
                _CustomerBranchMasterName = string.Empty;
                _CustomerMasterBranchID = 0;
                _ReportType = string.Empty;
                _ReportTypeDisplay = string.Empty;
                _ATMReport = false;

                ContractSalaryBonusReportViewModel model = new ContractSalaryBonusReportViewModel();
                
                model.ListAccountSessionMaster = GetAllAccountSession();

                List<SelectListItem> li1 = new List<SelectListItem>();
                li1.Add(new SelectListItem { Text = "Leave With Wages", Value = "LWW" });
                li1.Add(new SelectListItem { Text = "Bonus", Value = "BONUS" });
                li1.Add(new SelectListItem { Text = "Gratuity", Value = "GRTY" });
                ViewData["ReportType"] = li1;

                return View("/Views/Contract/Report/ContractSalaryBonusReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(ContractSalaryBonusReportViewModel model)
        {

            model.ListAccountSessionMaster = GetAllAccountSession();

            List<SelectListItem> li1 = new List<SelectListItem>();
            li1.Add(new SelectListItem { Text = "Leave With Wages", Value = "LWW" });
            li1.Add(new SelectListItem { Text = "Bonus", Value = "BONUS" });
            li1.Add(new SelectListItem { Text = "Gratuity", Value = "GRTY" });

            if (model.IsPosted == true)
            {
                _FromDate = model.FromDate;
                _UptoDate = model.UptoDate;
                _AccountSessionID = model.AccountSessionID;
                _CustomerBranchMasterName = model.CustomerBranchMasterName;
                _CustomerMasterBranchID = model.CustomerBranchMasterID;
                _ReportType = model.ReportType;
                _ReportTypeDisplay = model.ReportTypeDisplay;
                _ATMReport = model.ATMReport;
                model.IsPosted = false;
            }
            else
            {
                model.FromDate = _FromDate;
                model.UptoDate = _UptoDate;
                model.AccountSessionID = _AccountSessionID;
                model.CustomerBranchMasterName = _CustomerBranchMasterName;
                model.CustomerBranchMasterID = Convert.ToInt32(_CustomerMasterBranchID);
                model.ReportType = _ReportType;
                model.ReportTypeDisplay = _ReportTypeDisplay;
                model.ATMReport = _ATMReport;
            }
            ViewData["ReportType"] = new SelectList(li1, "Value", "Text", model.ReportType);

            return View("/Views/Contract/Report/ContractSalaryBonusReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------
        
        public List<ContractSalaryBonusReport> GetContractSalaryBonusReportList()
        {
            try
            {
                List<ContractSalaryBonusReport> listContractSalaryBonusReport = new List<ContractSalaryBonusReport>();
                ContractSalaryBonusReportSearchRequest searchRequest = new ContractSalaryBonusReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_FromDate != string.Empty && _UptoDate != string.Empty)
                {
                    searchRequest.FromDate = _FromDate;
                    searchRequest.UptoDate = _UptoDate;
                    searchRequest.AccountSessionID = _AccountSessionID;
                    searchRequest.CustomerBranchMasterName = _CustomerBranchMasterName;
                    searchRequest.CustomerBranchMasterID = _CustomerMasterBranchID;
                    searchRequest.ReportType = _ReportType;
                    searchRequest.ReportTypeDisplay = _ReportTypeDisplay;
                    IBaseEntityCollectionResponse<ContractSalaryBonusReport> baseEntityCollectionResponse = _ContractSalaryBonusReportBA.GetContractSalaryBonusReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listContractSalaryBonusReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listContractSalaryBonusReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }
        public List<ContractSalaryBonusReport> GetContractSalaryBonusATMReportList()
        {
            try
            {
                List<ContractSalaryBonusReport> listContractSalaryBonusReport = new List<ContractSalaryBonusReport>();
                ContractSalaryBonusReportSearchRequest searchRequest = new ContractSalaryBonusReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_FromDate != string.Empty && _UptoDate != string.Empty)
                {
                    searchRequest.FromDate = _FromDate;
                    searchRequest.UptoDate = _UptoDate;
                    searchRequest.AccountSessionID = _AccountSessionID;
                    searchRequest.CustomerBranchMasterName = _CustomerBranchMasterName;
                    searchRequest.CustomerBranchMasterID = _CustomerMasterBranchID;
                    searchRequest.ReportType = _ReportType;
                    searchRequest.ReportTypeDisplay = _ReportTypeDisplay;
                    IBaseEntityCollectionResponse<ContractSalaryBonusReport> baseEntityCollectionResponse = _ContractSalaryBonusReportBA.GetContractSalaryBonusATMReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listContractSalaryBonusReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listContractSalaryBonusReport;
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
