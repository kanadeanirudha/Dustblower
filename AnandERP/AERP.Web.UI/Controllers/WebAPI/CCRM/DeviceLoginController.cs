using AERP.Base.DTO;
using AERP.Business.BusinessAction;
using AERP.DTO;
using AERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AERP.Web.UI
{
    public class DeviceLoginController : BaseAPIController
    {
        private static string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        static ILoginBA _ILoginBA = null;
        public DeviceLoginController()
        {
            _ILoginBA = new LoginBA();
        }

        static DeviceLoginController()
        {
            _ILoginBA = new LoginBA();
        }

        [HttpPost]
        [AllowAnonymous]
        public object Login(UserMasterViewModel model)
        {
            UserMasterViewModel userMasterViewModel = new UserMasterViewModel();
            if (ModelState.IsValid && model != null && !string.IsNullOrEmpty(model.EmailID) && !string.IsNullOrEmpty(model.Password))
            {
                userMasterViewModel.UserMasterDTO = new UserMaster();
                userMasterViewModel.UserMasterDTO.EmailID = model.EmailID;
                userMasterViewModel.UserMasterDTO.MobileNumber = model.MobileNumber;
                userMasterViewModel.UserMasterDTO.Password = model.Password;
                userMasterViewModel.UserMasterDTO.IP = model.IP;
                userMasterViewModel.UserMasterDTO.VersionNumber = model.VersionNumber;
                userMasterViewModel.UserMasterDTO.MachinName = model.MachinName;
                userMasterViewModel.UserMasterDTO.DeviceToken = model.DeviceToken;
                userMasterViewModel.UserMasterDTO.ConnectionString = _connectioString;
                IBaseEntityResponse<UserMaster> response = _ILoginBA.UserLoginApi(userMasterViewModel.UserMasterDTO);
                Dictionary<String, object> Data = new Dictionary<string, object>();
                if (response != null)
                {
                    if(response.Entity.exists.ToUpper() == "EXIST")
                    {
                        Data.Add("EmailID", response.Entity.EmailID);
                        Data.Add("UserName", response.Entity.UserName);
                        Data.Add("MobileNumber", response.Entity.MobileNumber);
                        Data.Add("EmployeeDesignation", response.Entity.EmployeeDesignation);
                        Data.Add("EmployeeDesignationID", response.Entity.EmployeeDesignationID);
                        Data.Add("ServiceEnginner", response.Entity.IsServiceEnginner);
                        Data.Add("ServiceManager", response.Entity.IsServiceManager);
                        Data.Add("CollectionExecutive", response.Entity.IsCollectionExecutive);
                        Data.Add("EmployeeMasterID", response.Entity.EmployeeMasterID);
                        Data.Add("UserID", response.Entity.UserID);
                    }

                    Dictionary<string, object> _dict = new Dictionary<string, object>
                    {
                        {"StatusCode",response.Entity.ErrorCode },
                        {"Message", CheckError(response.Entity.ErrorCode)},
                        {"Data", Data }
                    };
                    return _dict;
                }
           }
            return new Dictionary<string, object>
                    {
                        {"StatusCode", 417},//417 Expectation Failed
                        {"Message", CheckError(417)}
                    };
        }

        public static Boolean IsValidate(UserMasterViewModel model)
        {
            UserMasterViewModel userMasterViewModel = new UserMasterViewModel();
            if (model != null && !string.IsNullOrEmpty(model.EmailID) && !string.IsNullOrEmpty(model.Password))
            {
                userMasterViewModel.UserMasterDTO = new UserMaster();
                userMasterViewModel.UserMasterDTO.EmailID = model.EmailID;
                userMasterViewModel.UserMasterDTO.Password = model.Password;
              
                userMasterViewModel.UserMasterDTO.ConnectionString = _connectioString;
                IBaseEntityResponse<UserMaster> response = _ILoginBA.IsValidate(userMasterViewModel.UserMasterDTO);
                Dictionary<String, object> Data = new Dictionary<string, object>();
                if (response != null)
                {
                    if (response.Entity.exists.ToUpper() == "EXIST")
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}