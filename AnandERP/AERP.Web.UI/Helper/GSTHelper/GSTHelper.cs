using AERP.Business.BusinessAction;
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
                GSTAuthTokenErrorResponse gstErrorResponse = JsonConvert.DeserializeObject<GSTAuthTokenErrorResponse>(response.Content);
                gstAuthTokenResponse.AuthTokenStatus = false;
                gstAuthTokenResponse.ErrorMessage = gstErrorResponse?.ErrorDetails[0]?.ErrorMessage;
            }
            return gstAuthTokenResponse;
        }

        #endregion

        #region E-Invoince
        public static GSTInvoiceResponse GenerateEInvoice(GSTInvoiceRequestModel gstInvoiceRequestModel, OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTInvoiceResponse gstInvoiceResponse = new GSTInvoiceResponse();
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

                gstInvoiceResponse = JsonConvert.DeserializeObject<GSTInvoiceResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstInvoiceResponse.Status == "1")
                {
                    string data = gstInvoiceResponse.Data.ToString();
                    gstInvoiceResponse.DataResponse = JsonConvert.DeserializeObject<DataResponse>(data);

                    //code to save QR image
                    byte[] qrImg = Convert.FromBase64String(gstInvoiceResponse.DataResponse.QrCodeImage.ToString());
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(qrImg);
                    bitmap1.Save(string.Format(System.Configuration.ConfigurationManager.AppSettings["GSTQRCodePath"], gstInvoiceRequestModel.DocDtls.No));
                }
                else
                {
                    if (!string.IsNullOrEmpty(gstInvoiceResponse?.error?.message))
                    {
                        gstInvoiceResponse.ErrorMessage = gstInvoiceResponse?.error?.error_cd + ":" + gstInvoiceResponse?.error?.message;
                    }
                    else
                    {
                        if (gstInvoiceResponse?.ErrorDetails?.Count > 0)
                        {
                            foreach (var error in gstInvoiceResponse.ErrorDetails)
                            {
                                if (string.IsNullOrEmpty(gstInvoiceResponse.ErrorMessage))
                                {
                                    gstInvoiceResponse.ErrorMessage = $"Error Message:({error.ErrorCode}){error.ErrorMessage}";
                                }
                                else
                                {
                                    gstInvoiceResponse.ErrorMessage = $"{gstInvoiceResponse.ErrorMessage} and ({error.ErrorCode}){error.ErrorMessage}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return gstInvoiceResponse;
        }
        #endregion

    }
}