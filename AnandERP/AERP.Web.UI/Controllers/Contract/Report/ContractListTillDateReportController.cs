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
    public class ContractListTillDateReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IContractListTillDateReportBA _ContractListTillDateReportBA = null;
        private readonly ILogger _logException;
        protected static string _SalaryMonth = string.Empty;
        protected static string _SalaryYear = string.Empty;
        protected static string _CentreCode = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public ContractListTillDateReportController()
        {
            _ContractListTillDateReportBA = new ContractListTillDateReportBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                _CentreCode = string.Empty;
                ContractListTillDateReportViewModel model = new ContractListTillDateReportViewModel();

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
                                
                return View("/Views/Contract/Report/ContractListTillDateReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(ContractListTillDateReportViewModel model)
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
                        
            if (model.IsPosted == true)
            {
                _CentreCode = model.CentreCode;
                model.IsPosted = false;
            }
            else
            {
                model.CentreCode = _CentreCode;
            }

            return View("/Views/Contract/Report/ContractListTillDateReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------
        
        public List<ContractListTillDateReport> GetContractListTillDateReportList()
        {
            try
            {
                List<ContractListTillDateReport> listContractListTillDateReport = new List<ContractListTillDateReport>();
                ContractListTillDateReportSearchRequest searchRequest = new ContractListTillDateReportSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);

                if ( _CentreCode != string.Empty)
                {
                    searchRequest.CentreCode = _CentreCode;
                    IBaseEntityCollectionResponse<ContractListTillDateReport> baseEntityCollectionResponse = _ContractListTillDateReportBA.GetContractListTillDateReportDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listContractListTillDateReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listContractListTillDateReport;
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
