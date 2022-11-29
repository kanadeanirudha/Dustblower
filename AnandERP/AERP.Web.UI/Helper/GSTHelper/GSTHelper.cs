using AERP.DTO;

using Newtonsoft.Json;

using RestSharp;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;

namespace AERP.Web.UI.Helper
{
    public static class GSTHelper
    {
        #region Auth Token
        public static GSTAuthTokenRequest GenerateGSTAuthToken()
        {
            GSTAuthTokenRequest gstAuthTokenRequest = GetGSTValidateToken();
            if (GetGSTValidateToken().AuthTokenStatus)
                return gstAuthTokenRequest;

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth", Method.Get);
            request.Timeout = -1;
            request.AddHeader("Gstin", "34AACCC1596Q002");
            request.AddHeader("user_name", "TaxProEnvPON");
            request.AddHeader("aspid", "1712482722");
            request.AddHeader("password", "Gst@1007");
            request.AddHeader("eInvPwd", "abc34*");
            request.RequestFormat = DataFormat.Json;
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                gstAuthTokenRequest = JsonConvert.DeserializeObject<GSTAuthTokenRequest>(response.Content);
                if (gstAuthTokenRequest.Status == 1)
                {
                    gstAuthTokenRequest.AuthTokenStatus = SaveGSTAuthToken(gstAuthTokenRequest);
                }
            }
            else
            {
                gstAuthTokenRequest.AuthTokenStatus = false;
            }
            return gstAuthTokenRequest;
        }

        public static GSTAuthTokenRequest GetGSTValidateToken()
        {
            //check is token is valid or not
            return new GSTAuthTokenRequest();
        }

        public static bool SaveGSTAuthToken(GSTAuthTokenRequest gstAuthTokenRequest)
        {
            return true;
        }
        #endregion

        #region E-Invoince
        public static bool GenerateEInvoice(GSTInvoiceRequestModel gstInvoiceRequestModel, string centreCode)
        {
            bool status = false;
            try
            {

                string requestBody = JsonConvert.SerializeObject(gstInvoiceRequestModel);
                GSTAuthTokenRequest gstAuthTokenRequest = GenerateGSTAuthToken();
                if (gstAuthTokenRequest.AuthTokenStatus)
                {
                    RestClient client = new RestClient();
                    RestRequest request = new RestRequest("https://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?QrCodeSize=250", Method.Post);
                    request.AddHeader("Gstin", "34AACCC1596Q002");
                    request.AddHeader("user_name", "TaxProEnvPON");
                    request.AddHeader("AuthToken", gstAuthTokenRequest.Data.AuthToken);
                    request.AddHeader("aspid", "1712482722");
                    request.AddHeader("password", "Gst@1007");
                    request.AddHeader("Content-Type", "application/json; charset=utf-8");
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(requestBody);     //Request Payload in object format
                    RestResponse response = client.Execute(request);

                    GSTAuthTokenResponse respPlGenIRN = JsonConvert.DeserializeObject<GSTAuthTokenResponse>(response.Content);
                    if (response.IsSuccessful && respPlGenIRN.Status == "1")
                    {
                        string data = respPlGenIRN.Data.ToString();
                        //DataResponse dataResponse = JsonConvert.DeserializeObject<DataResponse>(respPlGenIRN.Data);
                        ////code to save QR image
                        //byte[] qrImg = Convert.FromBase64String(dataResponse.QrCodeImage.ToString());
                        //TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                        //Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(qrImg);

                        //bitmap1.Save(@"D:\Anirudha\qr.png");
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }
        #endregion

    }
}