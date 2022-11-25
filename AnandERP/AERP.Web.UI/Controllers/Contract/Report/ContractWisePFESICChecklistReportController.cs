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
    public class ContractWisePFESICChecklistReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IContractWisePFESICChecklistReportBA _ContractWisePFESICChecklistReportBA = null;
        IESICZoneMasterBA _ESICZoneMasterBA = null;
        private readonly ILogger _logException;
        protected static string _SalaryMonth = string.Empty;
        protected static string _SalaryYear = string.Empty;
        protected static string _CentreCode = string.Empty;
        protected static Int16 _CurrentESICZoneID = 0;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public ContractWisePFESICChecklistReportController()
        {
            _ContractWisePFESICChecklistReportBA = new ContractWisePFESICChecklistReportBA();
            _ESICZoneMasterBA = new ESICZoneMasterBA();
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
                _CurrentESICZoneID = 0;
                ContractWisePFESICChecklistReportViewModel model = new ContractWisePFESICChecklistReportViewModel();

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

                List<ESICZoneMaster> ESICZoneMasterList = GetListESICZoneMaster();
                List<SelectListItem> ESICZoneMaster = new List<SelectListItem>();
                ESICZoneMaster.Add(new SelectListItem { Text = "All", Value = "0" });
                foreach (ESICZoneMaster item in ESICZoneMasterList)
                {
                    ESICZoneMaster.Add(new SelectListItem { Text = item.ZoneName, Value = item.ID.ToString() });
                }
                ViewBag.ESICZoneMaster = new SelectList(ESICZoneMaster, "Value", "Text");

                return View("/Views/Contract/Report/ContractWisePFESICChecklistReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(ContractWisePFESICChecklistReportViewModel model)
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

            List<ESICZoneMaster> ESICZoneMasterList = GetListESICZoneMaster();
            List<SelectListItem> ESICZoneMaster = new List<SelectListItem>();
            ESICZoneMaster.Add(new SelectListItem { Text = "All", Value = "0" });
            foreach (ESICZoneMaster item in ESICZoneMasterList)
            {
                ESICZoneMaster.Add(new SelectListItem { Text = item.ZoneName, Value = item.ID.ToString() });
            }
            ViewBag.ESICZoneMaster = new SelectList(ESICZoneMaster, "Value", "Text");

            if (model.IsPosted == true)
            {
                _SalaryMonth = model.SalaryMonth;
                _SalaryYear = model.SalaryYear;
                _CentreCode = model.CentreCode;
                _CurrentESICZoneID = model.CurrentESICZoneID;
                model.IsPosted = false;
            }
            else
            {
                model.SalaryMonth = _SalaryMonth;
                model.SalaryYear = _SalaryYear;
                model.CentreCode = _CentreCode;
                model.CurrentESICZoneID = _CurrentESICZoneID;
            }

            return View("/Views/Contract/Report/ContractWisePFESICChecklistReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------

        protected List<ESICZoneMaster> GetListESICZoneMaster()
        {
            ESICZoneMasterSearchRequest searchRequest = new ESICZoneMasterSearchRequest();
            searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
            List<ESICZoneMaster> listESICZoneMaster = new List<ESICZoneMaster>();
            IBaseEntityCollectionResponse<ESICZoneMaster> baseEntityCollectionResponse = _ESICZoneMasterBA.GetDropDownListforESICZoneMaster(searchRequest);
            if (baseEntityCollectionResponse != null)
            {
                if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                {
                    listESICZoneMaster = baseEntityCollectionResponse.CollectionResponse.ToList();
                }
            }
            return listESICZoneMaster;
        }

        public List<ContractWisePFESICChecklistReport> GetContractWisePFESICChecklistReportList()
        {
            try
            {
                List<ContractWisePFESICChecklistReport> listContractWisePFESICChecklistReport = new List<ContractWisePFESICChecklistReport>();
                ContractWisePFESICChecklistReportSearchRequest searchRequest = new ContractWisePFESICChecklistReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_SalaryMonth != string.Empty && _SalaryYear != string.Empty && _CentreCode != string.Empty)
                {
                    searchRequest.SalaryMonth = _SalaryMonth;
                    searchRequest.SalaryYear = _SalaryYear;
                    searchRequest.CentreCode = _CentreCode;
                    searchRequest.CurrentESICZoneID = _CurrentESICZoneID;
                    IBaseEntityCollectionResponse<ContractWisePFESICChecklistReport> baseEntityCollectionResponse = _ContractWisePFESICChecklistReportBA.GetContractWisePFESICChecklistReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listContractWisePFESICChecklistReport = baseEntityCollectionResponse.CollectionResponse.ToList();

                            decimal TotalPFWorkerShare = 0, TotalESICWorkersShare = 0, TotalAcc01 = 0, TotalAcc10 = 0, TotalAcc02 = 0, TotalAcc21 = 0, TotalAcc22 = 0, TotalESICEmployerShare = 0;
                            foreach (var lst in listContractWisePFESICChecklistReport)
                            {
                                TotalPFWorkerShare = TotalPFWorkerShare + lst.PFWorkersShare;
                                TotalESICWorkersShare = TotalESICWorkersShare + lst.ESICWorkersShare;
                                TotalAcc01 = TotalAcc01 + lst.Acc01;
                                TotalAcc10 = TotalAcc10 + lst.Acc10;
                                TotalAcc02 = TotalAcc02 + lst.Acc02;
                                TotalAcc21 = TotalAcc21 + lst.Acc21;
                                TotalAcc22 = TotalAcc22 + lst.Acc22;
                                TotalESICEmployerShare = TotalESICEmployerShare + lst.ESIC;
                            }
                            foreach (var lst in listContractWisePFESICChecklistReport)
                            {
                                lst.TotalPFWorkerShare = TotalPFWorkerShare;
                                lst.TotalESICWorkersShare = TotalESICWorkersShare;
                                lst.TotalAcc01 = TotalAcc01;
                                lst.TotalAcc10 = TotalAcc10;
                                lst.TotalAcc02 = TotalAcc02;
                                lst.TotalAcc21 = TotalAcc21;
                                lst.TotalAcc22 = TotalAcc22;
                                lst.TotalESICEmployerShare = TotalESICEmployerShare;
                                lst.TotalPF = TotalPFWorkerShare + TotalAcc01 + TotalAcc10 + TotalAcc02 + TotalAcc21 + TotalAcc22;
                            }
                        }
                    }
                }
                return listContractWisePFESICChecklistReport;
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
