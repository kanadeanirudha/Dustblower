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
    public class AllAccountDrillDownReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IAllAccountDrillDownReportBA _AllAccountDrillDownReportBA = null;
        private readonly ILogger _logException;
        protected static string _CentreName = string.Empty;
        protected static string _CentreCode = string.Empty;
        protected static int _AccountSessionID = 0;
        protected static string _AccountSessionName = string.Empty;
        protected static string _SessionFromDate = string.Empty;
        protected static string _SessionUptoDate = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public AllAccountDrillDownReportController()
        {
            _AllAccountDrillDownReportBA = new AllAccountDrillDownReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (Convert.ToInt32(Session["Account Manager"]) > 0 || Convert.ToInt32(Session["Admin Manager"]) > 0 && IsApplied == true)
            {
                AllAccountDrillDownReportViewModel model = new AllAccountDrillDownReportViewModel();
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

                return View("/Views/Accounts/Report/AllAccountDrillDownReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(AllAccountDrillDownReportViewModel model)
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
                _SessionFromDate = model.SessionFromDate;
                _SessionUptoDate = model.SessionUptoDate;
                model.IsPosted = false;
            }
            else
            {
                model.CentreCode = _CentreCode;
                model.CentreName = _CentreName;
                model.AccountSessionID = _AccountSessionID;
                model.AccountSessionName = _AccountSessionName;
                model.SessionFromDate = _SessionFromDate;
                model.SessionUptoDate = _SessionUptoDate;
            }

            return View("/Views/Accounts/Report/AllAccountDrillDownReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------



        public List<AllAccountDrillDownReport> GetAllAccountDrillDownReportList()
        {
            try
            {
                List<AllAccountDrillDownReport> listAllAccountDrillDownReport = new List<AllAccountDrillDownReport>();
                AllAccountDrillDownReportSearchRequest searchRequest = new AllAccountDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_CentreCode != string.Empty && _SessionFromDate != string.Empty && _SessionUptoDate != string.Empty)
                {
                    searchRequest.CentreCode = _CentreCode;
                    searchRequest.CentreName = _CentreName;
                    searchRequest.AccountSessionID = _AccountSessionID;
                    searchRequest.AccountSessionName = _AccountSessionName;
                    searchRequest.SessionFromDate = _SessionFromDate;
                    searchRequest.SessionUptoDate = _SessionUptoDate;
                    IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollectionResponse = _AllAccountDrillDownReportBA.GetAllAccountDrillDownReportList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listAllAccountDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listAllAccountDrillDownReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public List<AllAccountDrillDownReport> GetAllAccountDrillDownReportList2(string SessionFromDate, string SessionUptoDate, string AccountMasterID, string AccountSessionID, string ActBalsheetMstID, string CentreCode, string PersonID, string PersonType)
        {
            try
            {
                List<AllAccountDrillDownReport> listAllAccountDrillDownReport = new List<AllAccountDrillDownReport>();
                AllAccountDrillDownReportSearchRequest searchRequest = new AllAccountDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (_CentreCode != string.Empty)
                {
                    searchRequest.SessionFromDate = SessionFromDate;
                    searchRequest.SessionUptoDate = SessionUptoDate;
                    searchRequest.AccountMasterID = Convert.ToInt32(AccountMasterID);
                    searchRequest.AccountSessionID = Convert.ToInt32(AccountSessionID);
                    searchRequest.ActBalsheetMstID = Convert.ToInt32(ActBalsheetMstID);
                    searchRequest.CentreCode = CentreCode;
                    searchRequest.PersonID = Convert.ToInt32(PersonID);
                    searchRequest.PersonType = PersonType;
                    IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollectionResponse = _AllAccountDrillDownReportBA.GetAllAccountDrillDownReportList2(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listAllAccountDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listAllAccountDrillDownReport;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }

        public List<AllAccountDrillDownReport> GetAllAccountDrillDownReportList3(string TransactionMainID, string VoucherNoWithTranType)
        {
            try
            {
                List<AllAccountDrillDownReport> listAllAccountDrillDownReport = new List<AllAccountDrillDownReport>();
                AllAccountDrillDownReportSearchRequest searchRequest = new AllAccountDrillDownReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if (TransactionMainID != string.Empty)
                {
                    searchRequest.TransactionMainID = Convert.ToInt32(TransactionMainID);
                    searchRequest.VoucherNoWithTranType = VoucherNoWithTranType;
                    IBaseEntityCollectionResponse<AllAccountDrillDownReport> baseEntityCollectionResponse = _AllAccountDrillDownReportBA.GetAllAccountDrillDownReportList3(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listAllAccountDrillDownReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listAllAccountDrillDownReport;
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
