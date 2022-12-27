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
    public class EmployeeESICForm6ReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        IEmployeeESICForm6ReportBA _EmployeeESICForm6ReportBA = null;
        IESICZoneMasterBA _ESICZoneMasterBA = null;
        private readonly ILogger _logException;
        protected static string _centreCode = string.Empty;
        protected static string _ESICZone = string.Empty;
        protected static string _FromDate = string.Empty;
        protected static string _UptoDate = string.Empty;
        //protected static string _MonthFullName = string.Empty;
        //protected static int _SaleContractEmployeeMasterID = 0;
        //protected static int _AccountSessionID = 0;
        protected static int _ESICZoneID = 0;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public EmployeeESICForm6ReportController()
        {
            _EmployeeESICForm6ReportBA = new EmployeeESICForm6ReportBA();
            _ESICZoneMasterBA = new ESICZoneMasterBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                EmployeeESICForm6ReportViewModel model = new EmployeeESICForm6ReportViewModel();
                
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

                return View("/Views/Contract/Report/EmployeeESICForm6Report/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(EmployeeESICForm6ReportViewModel model)
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
                model.IsPosted = false;

            }
            else
            {
                model.FromDate = _FromDate;
                model.UptoDate = _UptoDate;
                model.ESICZoneID = _ESICZoneID;
                model.CentreCode = _centreCode;
                model.ESICZone = _ESICZone;
            }
            return View("/Views/Contract/Report/EmployeeESICForm6Report/Index.cshtml", model);
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

        public List<EmployeeESICForm6Report> GetEmployeeESICForm6ReportList()
        {
            try
            {
                List<EmployeeESICForm6Report> listEmployeeESICForm6Report = new List<EmployeeESICForm6Report>();
                EmployeeESICForm6ReportSearchRequest searchRequest = new EmployeeESICForm6ReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
                searchRequest.CentreCode = _centreCode;
                if (_FromDate != string.Empty && _centreCode != string.Empty)
                {
                    searchRequest.FromDate = _FromDate;
                    searchRequest.UptoDate = Convert.ToString(_UptoDate);
                    searchRequest.ESICZoneID = _ESICZoneID;
                    searchRequest.CentreCode = _centreCode;
                    searchRequest.ESICZone = _ESICZone;
                    IBaseEntityCollectionResponse<EmployeeESICForm6Report> baseEntityCollectionResponse = _EmployeeESICForm6ReportBA.GetEmployeeESICForm6ReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listEmployeeESICForm6Report = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listEmployeeESICForm6Report;
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
