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
    public class PurchaseRegisterDrillDownReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IPurchaseRegisterDrillDownReportBA _PurchaseRegisterDrillDownReportBA = null;
        private readonly ILogger _logException;
        protected static string _CentreName = string.Empty;
        protected static string _CentreCode = string.Empty;
        protected static int _AccountSessionID = 0;
        protected static string _AccountSessionName = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public PurchaseRegisterDrillDownReportController()
        {
            _PurchaseRegisterDrillDownReportBA = new PurchaseRegisterDrillDownReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (Convert.ToInt32(Session["Account Manager"]) > 0 || Convert.ToInt32(Session["Admin Manager"]) > 0 && IsApplied == true)
            {
                PurchaseRegisterDrillDownReportViewModel model = new PurchaseRegisterDrillDownReportViewModel();
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

                List<AdminRoleApplicableDetails> listAdminRoleApplicableDetails = GetAdminRoleApplicableCentreByFinanceManager(AdminRoleMasterID);
                AdminRoleApplicableDetails a = null;
                foreach (var item in listAdminRoleApplicableDetails)
                {
                    a = new AdminRoleApplicableDetails();
                    a.CentreCode = item.CentreCode;
                    a.CentreName = item.CentreName;
                    model.ListGetAdminRoleApplicableCentre.Add(a);
                }

                return View("/Views/Purchase/Report/PurchaseRegisterDrillDownReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(PurchaseRegisterDrillDownReportViewModel model)
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

            List<AdminRoleApplicableDetails> listAdminRoleApplicableDetails = GetAdminRoleApplicableCentreByFinanceManager(AdminRoleMasterID);
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
                _CentreCode = model.CentreCode;
                _CentreName = model.CentreName;
                _AccountSessionID = model.AccountSessionID;
                _AccountSessionName = model.AccountSessionName;
                model.IsPosted = false;
            }
            else
            {
                model.CentreCode = _CentreCode;
                model.CentreName = _CentreName;
                model.AccountSessionID = _AccountSessionID;
                model.AccountSessionName = _AccountSessionName;
            }

            return View("/Views/Purchase/Report/PurchaseRegisterDrillDownReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------



        public List<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList()
        {
            try
            {
                List<PurchaseRegisterDrillDownReport> listPurchaseRegisterDrillDownReport = new List<PurchaseRegisterDrillDownReport>();
                PurchaseRegisterDrillDownReportSearchRequest searchRequest = new PurchaseRegisterDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_CentreCode != string.Empty)
                {
                    searchRequest.CentreCode = _CentreCode;
                    searchRequest.CentreName = _CentreName;
                    searchRequest.AccountSessionID = _AccountSessionID;
                    searchRequest.AccountSessionName = _AccountSessionName;
                    IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollectionResponse = _PurchaseRegisterDrillDownReportBA.GetPurchaseRegisterDrillDownReportList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listPurchaseRegisterDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listPurchaseRegisterDrillDownReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public List<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList2(string CentreCode, string TransMonth, string TransYear,string TransMonthName,string CentreName)
        {
            try
            {
                List<PurchaseRegisterDrillDownReport> listPurchaseRegisterDrillDownReport = new List<PurchaseRegisterDrillDownReport>();
                PurchaseRegisterDrillDownReportSearchRequest searchRequest = new PurchaseRegisterDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_CentreCode != string.Empty)
                {
                    searchRequest.CentreCode = CentreCode;
                    searchRequest.TransMonth = TransMonth;
                    searchRequest.TransYear = TransYear;
                    searchRequest.TransMonthName = TransMonthName;
                    searchRequest.CentreName = CentreName;
                    IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollectionResponse = _PurchaseRegisterDrillDownReportBA.GetPurchaseRegisterDrillDownReportList2(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listPurchaseRegisterDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listPurchaseRegisterDrillDownReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public List<PurchaseRegisterDrillDownReport> GetPurchaseRegisterDrillDownReportList3(string VendorInvoiceNumber, string PurchaseInvoiceMasterID)
        {
            try
            {
                List<PurchaseRegisterDrillDownReport> listPurchaseRegisterDrillDownReport = new List<PurchaseRegisterDrillDownReport>();
                PurchaseRegisterDrillDownReportSearchRequest searchRequest = new PurchaseRegisterDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (VendorInvoiceNumber != string.Empty)
                {
                    searchRequest.PurchaseInvoiceMasterID = Convert.ToInt64(PurchaseInvoiceMasterID);
                    searchRequest.VendorInvoiceNumber = VendorInvoiceNumber;
                    IBaseEntityCollectionResponse<PurchaseRegisterDrillDownReport> baseEntityCollectionResponse = _PurchaseRegisterDrillDownReportBA.GetPurchaseRegisterDrillDownReportList3(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listPurchaseRegisterDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listPurchaseRegisterDrillDownReport;
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
