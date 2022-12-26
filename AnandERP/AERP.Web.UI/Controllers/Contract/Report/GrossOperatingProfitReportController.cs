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
    public class GrossOperatingProfitReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        ISaleContractAttendanceBA _SaleContractAttendanceBA = null;
        IGrossOperatingProfitReportBA _GrossOperatingProfitReportBA = null;
        private readonly ILogger _logException;
        protected static string _centreCode = string.Empty;
        protected static string _centreName = string.Empty;
        protected static string _CustomerMasterName = string.Empty;
        protected static string _CustomerBranchMasterName = string.Empty;
        protected static Int32 _CustomerMasterID = 0;
        protected static Int32 _CustomerBranchMasterID = 0;
        protected static int _AccountSessionID = 0;
        protected static string _AccountSessionName = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public GrossOperatingProfitReportController()
        {
            _GrossOperatingProfitReportBA = new GrossOperatingProfitReportBA();
            _SaleContractAttendanceBA = new SaleContractAttendanceBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                GrossOperatingProfitReportViewModel model = new GrossOperatingProfitReportViewModel();
                model.ListAccountSessionMaster = GetAllAccountSession();
                _CustomerMasterID = model.CustomerMasterID;
                _CustomerMasterName = model.CustomerMasterName;
                _CustomerBranchMasterID = model.CustomerBranchMasterID;
                _CustomerBranchMasterName = model.CustomerBranchMasterName;

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

                return View("/Views/Contract/Report/GrossOperatingProfitReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(GrossOperatingProfitReportViewModel model)
        {
            model.ListAccountSessionMaster = GetAllAccountSession();
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
                _CustomerMasterID = model.CustomerMasterID;
                _CustomerMasterName = model.CustomerMasterName;
                _CustomerBranchMasterID = model.CustomerBranchMasterID;
                _CustomerBranchMasterName = model.CustomerBranchMasterName;
                _centreCode = model.CentreCode;
                _centreName = model.CentreName;
                _AccountSessionID = model.AccountSessionID;
                _AccountSessionName = model.AccountSessionName;
                model.IsPosted = false;
            }
            else
            {
                model.CustomerMasterID = _CustomerMasterID;
                model.CustomerMasterName = _CustomerMasterName;
                model.CustomerBranchMasterID = _CustomerBranchMasterID;
                model.CustomerBranchMasterName = _CustomerBranchMasterName;
                model.CentreCode = _centreCode;
                model.CentreName = _centreName;
                model.AccountSessionID = _AccountSessionID;
                model.AccountSessionName = _AccountSessionName;
            }
            
            return View("/Views/Contract/Report/GrossOperatingProfitReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------

        public List<GrossOperatingProfitReport> GetGrossOperatingProfitReportList()
        {
            try
            {
                List<GrossOperatingProfitReport> listGrossOperatingProfitReport = new List<GrossOperatingProfitReport>();
                GrossOperatingProfitReportSearchRequest searchRequest = new GrossOperatingProfitReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_centreCode != string.Empty)
                {
                    searchRequest.CustomerMasterID = _CustomerMasterID;
                    searchRequest.CustomerMasterName = _CustomerMasterName;
                    searchRequest.CustomerBranchMasterID = _CustomerBranchMasterID;
                    searchRequest.CustomerBranchMasterName = _CustomerBranchMasterName;
                    searchRequest.CentreCode = _centreCode;
                    searchRequest.CentreName = _centreName;
                    searchRequest.AccountSessionID = _AccountSessionID;
                    searchRequest.AccountSessionName = _AccountSessionName;
                    IBaseEntityCollectionResponse<GrossOperatingProfitReport> baseEntityCollectionResponse = _GrossOperatingProfitReportBA.GetGrossOperatingProfitReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listGrossOperatingProfitReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listGrossOperatingProfitReport;
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
