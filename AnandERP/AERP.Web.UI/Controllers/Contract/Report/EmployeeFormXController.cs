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
using System.IO;
using System.Text;
using AERP.Business.BusinessAction;
using System.Globalization;
namespace AERP.Web.UI.Controllers
{
    public class EmployeeFormXController : BaseController
    {
        #region ------------CONTROLLER CLASS VARIABLE------------
        private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        IEmployeeFormXBA _EmployeeFormXBA = null;
        private readonly ILogger _logException;
        protected static string _centreCode = string.Empty;
        protected static string _centreName = string.Empty;
        protected static string _Month = string.Empty;
        protected static string _Year = string.Empty;
        protected static int _SaleContractEmployeeMasterID = 0;
        protected static int _AccountSessionID = 0;

        #endregion

        #region ------------CONTROLLER CLASS CONSTRUCTOR------------
        public EmployeeFormXController()
        {
            _EmployeeFormXBA = new EmployeeFormXBA();
        }
        #endregion

        #region ------------CONTROLLER ACTION METHODS------------
        public ActionResult Index()
        {
            bool IsApplied = CheckMenuApplicableOrNot(ControllerContext.RouteData.Values["Controller"].ToString());
            if (IsApplied == true)
            {
                EmployeeFormXViewModel model = new EmployeeFormXViewModel();
                return View("/Views/Contract/Report/EmployeeFormX/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("UnauthorizedAccess", "Home");
            }
        }

        public ActionResult List(string SaleContractMasterID, string SaleContractBillingSpanID)
        {
            try
            {
                EmployeeFormXViewModel model = new EmployeeFormXViewModel();
                model.EmployeeFormXDetailListForparticulars = GetEmployeeFormXListForParticularsMonthWise(SaleContractMasterID, SaleContractBillingSpanID);
                return PartialView("/Views/Contract/Report/EmployeeFormX/List.cshtml", model);
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }

        }
  

        [HttpPost]
        [ValidateInput(false)]
        public FileResult DownloadExcelFile(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
        }

        #endregion

        #region ------------CONTROLLER NON ACTION METHODS------------


        public List<EmployeeFormX> GetEmployeeFormXList(string SaleContractMasterID, string SaleContractBillingSpanID)
        {
            try
            {
                List<EmployeeFormX> listEmployeeFormX = new List<EmployeeFormX>();
                EmployeeFormXSearchRequest searchRequest = new EmployeeFormXSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
                searchRequest.ContractNumber = Convert.ToInt64(SaleContractMasterID);
                searchRequest.SaleContractBillingSpanID =Convert.ToInt64(SaleContractBillingSpanID);
                IBaseEntityCollectionResponse<EmployeeFormX> baseEntityCollectionResponse = _EmployeeFormXBA.GetEmployeeFormXDataList(searchRequest);
                if (baseEntityCollectionResponse != null)
                {
                    if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                    {
                        listEmployeeFormX = baseEntityCollectionResponse.CollectionResponse.ToList();
                    }
                }
                return listEmployeeFormX;
            }
            catch (Exception ex)
            {
                _logException.Error(ex.Message);
                throw;
            }
        }
        public List<EmployeeFormX> GetEmployeeFormXListForParticularsMonthWise(string SaleContractMasterID, string SaleContractBillingSpanID)
        {
            try
            {
                List<EmployeeFormX> listEmployeeFormX = new List<EmployeeFormX>();
                EmployeeFormXSearchRequest searchRequest = new EmployeeFormXSearchRequest();
                searchRequest.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
                searchRequest.ContractNumber = Convert.ToInt64(SaleContractMasterID);
                searchRequest.SaleContractBillingSpanID =Convert.ToInt64(SaleContractBillingSpanID);
                IBaseEntityCollectionResponse<EmployeeFormX> baseEntityCollectionResponse = _EmployeeFormXBA.GetEmployeeFormXDataList(searchRequest);
                if (baseEntityCollectionResponse != null)
                {
                    if (baseEntityCollectionResponse.CollectionResponse != null && baseEntityCollectionResponse.CollectionResponse.Count > 0)
                    {
                        listEmployeeFormX = baseEntityCollectionResponse.CollectionResponse.ToList();
                    }
                }
                return listEmployeeFormX;
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
