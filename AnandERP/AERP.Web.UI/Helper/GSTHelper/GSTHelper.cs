using AERP.Business.BusinessAction;
using AERP.DTO;

using Newtonsoft.Json;

using RestSharp;

using System;
using System.Net;

namespace AERP.Web.UI.Helper
{
    public static class GSTHelper
    {
        #region Auth Token
        public static GSTAuthTokenResponse GenerateGSTAuthToken(OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTAuthTokenResponse gstAuthTokenResponse = new GSTAuthTokenResponse();
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{GSTCredential.Urls}/eivital/dec/v1.04/auth", Method.Get);
            request.Timeout = -1;
            request.AddHeader("Gstin", GSTCredential.GSTIN);
            request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
            request.AddHeader("eInvPwd", GSTCredential.EInvoicePassword); //
            request.AddHeader("aspid", GSTCredential.AspId);
            request.AddHeader("password", GSTCredential.AspUserPassword);
            request.RequestFormat = DataFormat.Json;
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                gstAuthTokenResponse = JsonConvert.DeserializeObject<GSTAuthTokenResponse>(response.Content);
                if (gstAuthTokenResponse.Status == 1)
                {
                    gstAuthTokenResponse.AuthTokenStatus = true;
                }
            }
            else
            {
                GSTErrorResponse gstErrorResponse = JsonConvert.DeserializeObject<GSTErrorResponse>(response.Content);
                gstAuthTokenResponse.AuthTokenStatus = false;
                gstAuthTokenResponse.ErrorMessage = gstErrorResponse?.ErrorDetails[0]?.ErrorMessage;
            }
            return gstAuthTokenResponse;
        }

        #endregion

        #region E-Invoince
        public static bool GenerateEInvoice(GSTInvoiceRequestModel gstInvoiceRequestModel, OrganisationCentrewiseGSTCredential GSTCredential)
        {
            bool status = false;
            try
            {
                gstInvoiceRequestModel.Version = GSTCredential.Version;
                string requestBody = JsonConvert.SerializeObject(gstInvoiceRequestModel);
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/eicore/dec/v1.03/Invoice?QrCodeSize={GSTCredential.QrCodeSize}", Method.Post);
                request.AddHeader("Gstin", GSTCredential.GSTIN);
                request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
                request.AddHeader("AuthToken", GSTCredential.AuthToken);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(requestBody);     //Request Payload in object format
                RestResponse response = client.Execute(request);

                GSTInvoiceResponse gstInvoiceResponse = JsonConvert.DeserializeObject<GSTInvoiceResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstInvoiceResponse.Status == "1")
                {
                    string data = gstInvoiceResponse.Data.ToString();
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
            catch (Exception ex)
            {

            }
            return status;
        }
        #endregion

    }
}