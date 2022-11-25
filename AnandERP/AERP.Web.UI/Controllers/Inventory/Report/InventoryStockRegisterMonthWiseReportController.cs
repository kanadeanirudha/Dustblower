using AERP.Base.DTO;
using AERP.Business.BusinessAction;
using AERP.Business.BusinessActions;
using AERP.DTO;
using AERP.ExceptionManager;
using AERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AERP.Web.UI.Controllers
{
    public class InventoryStockRegisterMonthWiseReportController : BaseController
    {
        IInventoryReportBA _IInventoryReportBA = null;

        private readonly ILogger _logException;
        ActionModeEnum actionModeEnum;
        string _actionMode = string.Empty;
        string _sortBy = string.Empty;
        string _searchBy = string.Empty;
        string _sortDirection = string.Empty;
        int _startRow;
        int _rowLength;
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        protected static string _SalaryMonth = string.Empty;
        protected static string _SalaryYear = string.Empty;
        protected static string _CentreCode = string.Empty;
        protected static string _CentreName = string.Empty;
        protected static string _MonthName = string.Empty;
        // GET: InventoryStockRegisterMonthWiseReport

        public InventoryStockRegisterMonthWiseReportController()
        {
            _IInventoryReportBA = new InventoryReportBA();
        }
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (Convert.ToString(Session["UserType"]) == "A" || ((Convert.ToInt32(Session["Sales Manager"]) > 0 || Convert.ToInt32(Session["Sales Manager:Entity"]) > 0 || Convert.ToInt32(Session["Store Manager"]) > 0) && IsApplied == true))
            {
                InventoryReportViewModel model = new InventoryReportViewModel();
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
                ViewData["MonthReport"] = new SelectList(li_MonthList, "Value", "Text");
                //For Year
                int year = DateTime.Now.Year - 65;
                List<SelectListItem> li_YearList = new List<SelectListItem>();
                ViewBag.YearList = new SelectList(li_YearList, "Value", "Text");
                li_YearList.Add(new SelectListItem { Text = "-- Select Year --", Value = "0" });
                for (int i = DateTime.Now.Year; year <= i; i--)
                {
                    li_YearList.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) });
                }
                ViewData["YearReport"] = new SelectList(li_YearList, "Value", "Text");

                return View("/Views/Inventory/Report/InventoryStockRegisterMonthWiseReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(InventoryReportViewModel model)
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
            ViewData["MonthReport"] = new SelectList(li_MonthList, "Value", "Text");
            //For Year
            int year = DateTime.Now.Year - 65;
            List<SelectListItem> li_YearList = new List<SelectListItem>();
            ViewBag.YearList = new SelectList(li_YearList, "Value", "Text");
            li_YearList.Add(new SelectListItem { Text = "-- Select Year --", Value = "0" });
            for (int i = DateTime.Now.Year; year <= i; i--)
            {
                li_YearList.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString(i) });
            }
            ViewData["YearReport"] = new SelectList(li_YearList, "Value", "Text");

            if (model.IsPosted == true)
            {
                _SalaryMonth = model.MonthReport;
                _SalaryYear = model.YearReport;
                _CentreCode = model.CentreCode;
                _CentreName = model.CentreName;
                _MonthName = model.MonthName;
                model.IsPosted = false;
            }
            else
            {
                _SalaryMonth = model.MonthReport;
                _SalaryYear = model.YearReport;
                model.CentreCode = _CentreCode;
                model.CentreName = _CentreName;
                model.MonthName = _MonthName;
                model.IsPosted = true;

            }

            return View("/Views/Inventory/Report/InventoryStockRegisterMonthWiseReport/Index.cshtml", model);
        }


        public List<InventoryReport> GetInventoryStockRegisterMonthWiseReportList()
        {
            try
            {
                List<InventoryReport> listInventoryReport = new List<InventoryReport>();
                InventoryReportSearchRequest searchRequest = new InventoryReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_SalaryMonth != string.Empty && _SalaryYear != string.Empty && _CentreCode != string.Empty)
                {
                    searchRequest.SalaryMonth = _SalaryMonth;
                    searchRequest.SalaryYear = _SalaryYear;
                    searchRequest.CentreCode = _CentreCode;
                    searchRequest.CentreName = _CentreName;
                    searchRequest.MonthName = _MonthName;

                    IBaseEntityCollectionResponse<InventoryReport> baseEntityCollectionResponse = _IInventoryReportBA.GetInventoryStockRegisterMonthWiseReportList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listInventoryReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listInventoryReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

    }
}