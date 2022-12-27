using AERP.Base.DTO;
using AERP.Business.BusinessAction;
using AERP.DTO;
using AERP.ExceptionManager;
using AERP.ViewModel;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
namespace AERP.Web.UI.Controllers
{
    public class EmployeePFSummaryReportController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        IEmployeePFSummeryBA _EmployeePFSummeryBA = null;
        private readonly ILogger _logException;
        protected static string _centreCode = string.Empty;
        protected static string _centreName = string.Empty;
        protected static string _FromDate = string.Empty;
        protected static string _UptoDate = string.Empty;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public EmployeePFSummaryReportController()
        {
            _EmployeePFSummeryBA = new EmployeePFSummeryBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                EmployeePFSummaryReportViewModel model = new EmployeePFSummaryReportViewModel();
                
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

                return View("/Views/Contract/Report/EmployeePFSummaryReport/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        [HttpPost]
        public ActionResult Index(EmployeePFSummaryReportViewModel model)
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
                _centreCode = model.CentreCode;
                _centreName = model.CentreName;
                model.IsPosted = false;

            }
            else
            {
                model.FromDate = _FromDate;
                model.UptoDate = _UptoDate;
                model.CentreCode = _centreCode;
                model.CentreName = _centreName;
            }
            return View("/Views/Contract/Report/EmployeePFSummaryReport/Index.cshtml", model);
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------

        public List<EmployeePFSummery> GetEmployeePFSummaryReportList()
        {
            try
            {
                List<EmployeePFSummery> listEmployeePFSummaryReport = new List<EmployeePFSummery>();
                EmployeePFSummerySearchRequest searchRequest = new EmployeePFSummerySearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
                if (_FromDate != string.Empty && _centreCode != string.Empty)
                {
                    searchRequest.FromDate = _FromDate;
                    searchRequest.UptoDate = Convert.ToString(_UptoDate);
                    searchRequest.CentreCode = _centreCode;
                    searchRequest.CentreName = _centreName;
                    IBaseEntityCollectionResponse<EmployeePFSummery> baseEntityCollectionResponse = _EmployeePFSummeryBA.GetEmployeePFSummeryDataList(searchRequest);
                    if (baseEntityCollectionResponse != null)
                    {
                        if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                        {
                            listEmployeePFSummaryReport = baseEntityCollectionResponse.CollectionResponse.ToList();
                        }
                    }
                }
                return listEmployeePFSummaryReport;
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
