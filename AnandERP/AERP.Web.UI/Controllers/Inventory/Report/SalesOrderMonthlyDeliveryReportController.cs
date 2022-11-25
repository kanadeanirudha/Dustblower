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
    public class SalesOrderMonthlyDeliveryReportController : BaseController
    {
        ISalesOrderMasterAndDetailsBA _SalesOrderMasterAndDetailsBA = null;
        IGeneralTaxGroupMasterBA _GeneralTaxGroupMasterBA = null;

        IInventoryReportBA _IInventoryReportBA = null;
        IGeneralUnitsBA _GeneralUnitsBA = null;
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
        // GET: SalesOrderMonthlyDeliveryReport

            public SalesOrderMonthlyDeliveryReportController()
            {
                _IInventoryReportBA = new InventoryReportBA();
             }
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (Convert.ToString(Session["UserType"]) == "A" || ((Convert.ToInt32(Session["Sales Manager"]) > 0 || Convert.ToInt32(Session["Sales Manager:Entity"]) > 0 || Convert.ToInt32(Session["Store Manager"]) > 0) && IsApplied == true))
            {

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
                return View("/Views/Inventory/SalesOrderMonthlyDeliveryReport/Index.cshtml");
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(SalesOrderMasterAndDetailsViewModel model)
        {

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
                model.IsPosted = false;
            }
            else
            {
                _SalaryMonth = model.SalaryMonth;
                _SalaryYear = model.SalaryYear;
               // model.SalaryMonth = _SalaryMonth;
               //model.SalaryYear = _SalaryYear;
                model.IsPosted = true;

            }

            return View("/Views/Inventory/SalesOrderMonthlyDeliveryReport/Index.cshtml", model);
        }


        public List<InventoryReport> GetSaleOrderDailyReportList()
        {
            try
            {
                List<InventoryReport> listInventoryReport = new List<InventoryReport>();
                InventoryReportSearchRequest searchRequest = new InventoryReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_SalaryMonth != string.Empty && _SalaryYear != string.Empty)
                {
                    searchRequest.SalaryMonth = _SalaryMonth;
                    searchRequest.SalaryYear = _SalaryYear;
                    

                    IBaseEntityCollectionResponse<InventoryReport> baseEntityCollectionResponse = _IInventoryReportBA.GetSaleOrderMonthlyDeliveryReportReportList(searchRequest);
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