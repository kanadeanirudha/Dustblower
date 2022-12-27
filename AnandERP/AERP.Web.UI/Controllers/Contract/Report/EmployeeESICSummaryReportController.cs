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
    public class EmployeeESICSummaryReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        IEmployeeESICSummaryReportBA _EmployeeESICSummaryReportBA = null;
        IESICZoneMasterBA _ESICZoneMasterBA = null;
        private readonly ILogger _logException;
        protected static string _centreCode = string.Empty;
        protected static string _centreName = string.Empty;
        protected static string _ESICZone = string.Empty;
        protected static string _FromDate = string.Empty;
        protected static string _UptoDate = string.Empty;
        protected static int _ESICZoneID = 0;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public EmployeeESICSummaryReportController()
        {
            _EmployeeESICSummaryReportBA = new EmployeeESICSummaryReportBA();
            _ESICZoneMasterBA = new ESICZoneMasterBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                EmployeeESICSummaryReportViewModel model = new EmployeeESICSummaryReportViewModel();
                
                List<ESICZoneMaster> ESICZoneMasterList = GetListESICZoneMaster();
                List<SelectListItem> ESICZoneMaster = new List<SelectListItem>();
                foreach (ESICZoneMaster item in ESICZoneMasterList)
                {
                    ESICZoneMaster.Add(new SelectListItem { Text = item.ZoneName, Value = item.ID.ToString() });
                }
                ViewBag.ESICZoneMaster = new SelectList(ESICZoneMaster, "Value", "Text");

                int AdminRoleMasterID = 0;
                if (Session["RoleID"] == null)
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["DefaultRoleID"])) ? Convert.ToInt32(Session["DefaultRoleID"]) : 0;
                }

                else
                {
                    AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])) ? Convert.ToInt32(Session["RoleID"]) : 0;
                }

                List<AdminRoleApplicableDetails> listAdminRoleApplicableDetails = GetAdminRoleApplicableCentreByHRManager(AdminRoleMasterID);
                AdminRoleApplicableDetails a = null;
                foreach (var item in listAdminRoleApplicableDetails)
                {
                    a = new AdminRoleApplicableDetails();
                    a.CentreCode = item.CentreCode;
                    a.CentreName = item.CentreName;
                    model.ListGetAdminRoleApplicableCentre.Add(a);
                }

                return View("/Views/Contract/Report/EmployeeESICSummaryReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(EmployeeESICSummaryReportViewModel model)
        {

            List<ESICZoneMaster> ESICZoneMasterList = GetListESICZoneMaster();
            List<SelectListItem> ESICZoneMaster = new List<SelectListItem>();
            foreach (ESICZoneMaster item in ESICZoneMasterList)
            {
                ESICZoneMaster.Add(new SelectListItem { Text = item.ZoneName, Value = item.ID.ToString() });
            }
            ViewBag.ESICZoneMaster = new SelectList(ESICZoneMaster, "Value", "Text");

            int AdminRoleMasterID = 0;
            if (Session["RoleID"] == null)
            {
                AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["DefaultRoleID"])) ? Convert.ToInt32(Session["DefaultRoleID"]) : 0;
            }

            else
            {
                AdminRoleMasterID = !string.IsNullOrEmpty(Convert.ToString(Session["RoleID"])) ? Convert.ToInt32(Session["RoleID"]) : 0;
            }

            List<AdminRoleApplicableDetails> listAdminRoleApplicableDetails = GetAdminRoleApplicableCentreByHRManager(AdminRoleMasterID);
            AdminRoleApplicableDetails a = null;
            foreach (var item in listAdminRoleApplicableDetails)
            {
                a = new AdminRoleApplicableDetails();
                a.CentreCode = item.CentreCode;
                a.CentreName = item.CentreName;
                model.ListGetAdminRoleApplicableCentre.Add(a);
            }

            if (model.IsPosted == true)
            {
                _FromDate = model.FromDate;
                _UptoDate = model.UptoDate;
                _ESICZoneID = model.ESICZoneID;
                _centreCode = model.CentreCode;
                _ESICZone = model.ESICZone;
                _centreName = model.CentreName;
                model.IsPosted = false;

            }
            else
            {
                model.FromDate = _FromDate;
                model.UptoDate = _UptoDate;
                model.ESICZoneID = _ESICZoneID;
                model.CentreCode = _centreCode;
                model.CentreName = _centreName;
                model.ESICZone = _ESICZone;
            }
            return View("/Views/Contract/Report/EmployeeESICSummaryReport/Index.cshtml", model);
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

        public List<EmployeeESICSummaryReport> GetEmployeeESICSummaryReportList()
        {
            try
            {
                List<EmployeeESICSummaryReport> listEmployeeESICSummaryReport = new List<EmployeeESICSummaryReport>();
                EmployeeESICSummaryReportSearchRequest searchRequest = new EmployeeESICSummaryReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
                searchRequest.CentreCode = _centreCode;
                if (_FromDate != string.Empty && _centreCode != string.Empty)
                {
                    searchRequest.FromDate = _FromDate;
                    searchRequest.UptoDate = Convert.ToString(_UptoDate);
                    searchRequest.ESICZoneID = _ESICZoneID;
                    searchRequest.CentreCode = _centreCode;
                    searchRequest.CentreName = _centreName;
                    searchRequest.ESICZone = _ESICZone;
                    IBaseEntityCollectionResponse<EmployeeESICSummaryReport> baseEntityCollectionResponse = _EmployeeESICSummaryReportBA.GetEmployeeESICSummaryReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listEmployeeESICSummaryReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listEmployeeESICSummaryReport;
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
