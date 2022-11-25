using AERP.Base.DTO;
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
    public class ContractSalaryAndInvoiceStatusReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IContractSalaryAndInvoiceStatusReportBA _ContractSalaryAndInvoiceStatusReportBA = null;
        private readonly ILogger _logException;
        protected static string _SalaryMonth = string.Empty;
        protected static string _SalaryYear = string.Empty;
        protected static string _CentreCode = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public ContractSalaryAndInvoiceStatusReportController()
        {
            _ContractSalaryAndInvoiceStatusReportBA = new ContractSalaryAndInvoiceStatusReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                _SalaryMonth = string.Empty;
                _SalaryYear = string.Empty;
                _CentreCode = string.Empty;
                ContractSalaryAndInvoiceStatusReportViewModel model = new ContractSalaryAndInvoiceStatusReportViewModel();

                int AdminRoleMasterID = 0;
                if (Session["RoleID"] == null)
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["DefaultRoleID"])) ? Convert.ToInt32(Session["DefaultRoleID"]) : 0;
                }

                else
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])) ? Convert.ToInt32(Session["RoleID"]) : 0;
                }
                model.ListGetAdminRoleApplicableCentre = GetAdminRoleApplicableCentreBySalesManager(AdminRoleMasterID);

                List<SelectListItem> MonthList = new List<SelectListItem>();
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                ViewBag.MonthList = new SelectList(MonthList, "Value", "Text");
                List<SelectListItem> li_MonthList = new List<SelectListItem>();
                li_MonthList.Add(new SelectListItem { Text = "--Select month -- ", Value = "0" });
                for (int i = 1; i < 13; i++)
                {
                    ViewBag.MonthList = new SelectList(info.GetMonthName(i), i.ToString());
                    li_MonthList.Add(new SelectListItem { Text = info.GetMonthName(i), Value = (i).ToString() });
                }
                ViewData["SalaryMonth"] = new SelectList(li_MonthList, "Value", "Text");
                //For Year
                int year = DateTime.Now.Year - 65;
                List<SelectListItem> li_YearList = new List<SelectListItem>();
                ViewBag.YearList = new SelectList(li_YearList, "Value", "Text");
                li_YearList.Add(new SelectListItem { Text = "-- Select Year --", Value = "0" });
                for (int i = DateTime.Now.Year; year <= i; i--)
                {
                    li_YearList.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) });
                }
                ViewData["SalaryYear"] = new SelectList(li_YearList, "Value", "Text");

                return View("/Views/Contract/Report/ContractSalaryAndInvoiceStatusReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(ContractSalaryAndInvoiceStatusReportViewModel model)
        {

            int AdminRoleMasterID = 0;
            if (Session["RoleID"] == null)
            {
                AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["DefaultRoleID"])) ? Convert.ToInt32(Session["DefaultRoleID"]) : 0;
            }

            else
            {
                AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])) ? Convert.ToInt32(Session["RoleID"]) : 0;
            }
            model.ListGetAdminRoleApplicableCentre = GetAdminRoleApplicableCentreBySalesManager(AdminRoleMasterID);

            List<SelectListItem> MonthList = new List<SelectListItem>();
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            ViewBag.MonthList = new SelectList(MonthList, "Value", "Text");
            List<SelectListItem> li_MonthList = new List<SelectListItem>();
            li_MonthList.Add(new SelectListItem { Text = "--Select month -- ", Value = "0" });
            for (int i = 1; i < 13; i++)
            {
                ViewBag.MonthList = new SelectList(info.GetMonthName(i), i.ToString());
                li_MonthList.Add(new SelectListItem { Text = info.GetMonthName(i), Value = (i).ToString() });
            }
            ViewData["SalaryMonth"] = new SelectList(li_MonthList, "Value", "Text");
            //For Year
            int year = DateTime.Now.Year - 65;
            List<SelectListItem> li_YearList = new List<SelectListItem>();
            ViewBag.YearList = new SelectList(li_YearList, "Value", "Text");
            li_YearList.Add(new SelectListItem { Text = "-- Select Year --", Value = "0" });
            for (int i = DateTime.Now.Year; year <= i; i--)
            {
                li_YearList.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) });
            }
            ViewData["SalaryYear"] = new SelectList(li_YearList, "Value", "Text");

            if (model.IsPosted == true)
            {
                _SalaryMonth = model.SalaryMonth;
                _SalaryYear = model.SalaryYear;
                _CentreCode = model.CentreCode;
                model.IsPosted = false;
            }
            else
            {
                model.SalaryMonth = _SalaryMonth;
                model.SalaryYear = _SalaryYear;
                model.CentreCode = _CentreCode;
            }

            return View("/Views/Contract/Report/ContractSalaryAndInvoiceStatusReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------
        
        public List<ContractSalaryAndInvoiceStatusReport> GetContractSalaryAndInvoiceStatusReportList()
        {
            try
            {
                List<ContractSalaryAndInvoiceStatusReport> listContractSalaryAndInvoiceStatusReport = new List<ContractSalaryAndInvoiceStatusReport>();
                ContractSalaryAndInvoiceStatusReportSearchRequest searchRequest = new ContractSalaryAndInvoiceStatusReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_SalaryMonth != string.Empty && _SalaryYear != string.Empty && _CentreCode != string.Empty)
                {
                    searchRequest.SalaryMonth = _SalaryMonth;
                    searchRequest.SalaryYear = _SalaryYear;
                    searchRequest.CentreCode = _CentreCode;
                    IBaseEntityCollectionResponse<ContractSalaryAndInvoiceStatusReport> baseEntityCollectionResponse = _ContractSalaryAndInvoiceStatusReportBA.GetContractSalaryAndInvoiceStatusReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listContractSalaryAndInvoiceStatusReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listContractSalaryAndInvoiceStatusReport;
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
