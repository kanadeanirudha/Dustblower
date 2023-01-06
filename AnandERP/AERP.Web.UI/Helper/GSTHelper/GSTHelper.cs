using AERP.DTO;

using Newtonsoft.Json;

using RestSharp;

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;

namespace AERP.Web.UI.Helper
{
    public static class GSTHelper
    {
        #region Auth Token
        public static GSTAuthTokenResponse GenerateGSTAuthToken(OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTAuthTokenResponse gstAuthTokenResponse = new GSTAuthTokenResponse();
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{GSTCredential.Urls}/eivital/dec/v1.04/auth", Method.GET);
            request.Timeout = -1;
            request.AddHeader("Gstin", GSTCredential.GSTIN);
            request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
            request.AddHeader("eInvPwd", GSTCredential.EInvoicePassword); //
            request.AddHeader("aspid", GSTCredential.AspId);
            request.AddHeader("password", GSTCredential.AspUserPassword);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
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
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/eicore/dec/v1.03/Invoice?QrCodeSize={GSTCredential.QrCodeSize}", Method.POST);
                request.AddHeader("Gstin", GSTCredential.GSTIN);
                request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
                request.AddHeader("AuthToken", GSTCredential.AuthToken);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(requestBody);     //Request Payload in object format
                IRestResponse response = client.Execute(request);

                gstInvoiceResponse = JsonConvert.DeserializeObject<GSTInvoiceResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstInvoiceResponse.Status == "1")
                {
                    string data = gstInvoiceResponse.Data.ToString();
                    gstInvoiceResponse.DataResponse = JsonConvert.DeserializeObject<DataResponse>(data);

                    //code to save QR image
                    byte[] qrImg = Convert.FromBase64String(gstInvoiceResponse.DataResponse.QrCodeImage.ToString());
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap bitmap1 = (Bitmap)tc.ConvertFrom(qrImg);
                    bitmap1.Save(HttpContext.Current.Server.MapPath(string.Format(System.Configuration.ConfigurationManager.AppSettings["GSTQRCodePath"], gstInvoiceRequestModel.DocDtls.No)));
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

        public static GSTInvoiceCancelledResponse CancelledEInvoice(GSTInvoiceCancelledRequestModel gstInvoiceCancelledRequestModel, OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTInvoiceCancelledResponse gstInvoiceCancelledResponse = new GSTInvoiceCancelledResponse();
            try
            {
                string requestBody = JsonConvert.SerializeObject(gstInvoiceCancelledRequestModel);
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/eicore/dec/v1.03/Invoice/Cancel", Method.POST);
                request.AddHeader("Gstin", GSTCredential.GSTIN);
                request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
                request.AddHeader("AuthToken", GSTCredential.AuthToken);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(requestBody);     //Request Payload in object format
                IRestResponse response = client.Execute(request);

                gstInvoiceCancelledResponse = JsonConvert.DeserializeObject<GSTInvoiceCancelledResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstInvoiceCancelledResponse.Status == "1")
                {
                    string data = gstInvoiceCancelledResponse.Data.ToString();
                    gstInvoiceCancelledResponse.CancelledDataResponse = JsonConvert.DeserializeObject<CancelledDataResponse>(data);
                    return gstInvoiceCancelledResponse;
                }
                else
                {
                    if (!string.IsNullOrEmpty(gstInvoiceCancelledResponse?.error?.message))
                    {
                        gstInvoiceCancelledResponse.ErrorMessage = gstInvoiceCancelledResponse?.error?.error_cd + ":" + gstInvoiceCancelledResponse?.error?.message;
                    }
                    else
                    {
                        if (gstInvoiceCancelledResponse?.ErrorDetails?.Count > 0)
                        {
                            foreach (var error in gstInvoiceCancelledResponse.ErrorDetails)
                            {
                                if (string.IsNullOrEmpty(gstInvoiceCancelledResponse.ErrorMessage))
                                {
                                    gstInvoiceCancelledResponse.ErrorMessage = $"Error Message:({error.ErrorCode}){error.ErrorMessage}";
                                }
                                else
                                {
                                    gstInvoiceCancelledResponse.ErrorMessage = $"{gstInvoiceCancelledResponse.ErrorMessage} and ({error.ErrorCode}){error.ErrorMessage}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return gstInvoiceCancelledResponse;
        }

        #endregion

        #region E-Way Bill
        public static GSTEWayBillResponse GenerateEWayBill(GSTEWayBillRequestModel gstEWayBillRequestModel, OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTEWayBillResponse gstEWayBillResponse = new GSTEWayBillResponse();
            try
            {
                string requestBody = JsonConvert.SerializeObject(gstEWayBillRequestModel);
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/eiewb/dec/v1.03/ewaybill", Method.POST);
                request.AddHeader("Gstin", GSTCredential.GSTIN);
                request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
                request.AddHeader("AuthToken", GSTCredential.AuthToken);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(requestBody);     //Request Payload in object format
                IRestResponse response = client.Execute(request);

                gstEWayBillResponse = JsonConvert.DeserializeObject<GSTEWayBillResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstEWayBillResponse.Status == "1")
                {
                    string data = gstEWayBillResponse.Data.ToString();
                    gstEWayBillResponse.DataResponse = JsonConvert.DeserializeObject<EWayBillDataResponse>(data);
                }
                else
                {
                    if (!string.IsNullOrEmpty(gstEWayBillResponse?.error?.message))
                    {
                        gstEWayBillResponse.ErrorMessage = gstEWayBillResponse?.error?.error_cd + ":" + gstEWayBillResponse?.error?.message;
                    }
                    else
                    {
                        if (gstEWayBillResponse?.ErrorDetails?.Count > 0)
                        {
                            foreach (var error in gstEWayBillResponse.ErrorDetails)
                            {
                                if (string.IsNullOrEmpty(gstEWayBillResponse.ErrorMessage))
                                {
                                    gstEWayBillResponse.ErrorMessage = $"Error Message:({error.ErrorCode}){error.ErrorMessage}";
                                }
                                else
                                {
                                    gstEWayBillResponse.ErrorMessage = $"{gstEWayBillResponse.ErrorMessage} and ({error.ErrorCode}){error.ErrorMessage}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return gstEWayBillResponse;
        }

        public static void PrintEWayBill(OrganisationCentrewiseGSTCredential GSTCredential, string ewbNo)
        {
            GSTEWayBillResponse gstEWayBillResponse = new GSTEWayBillResponse();
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/ewaybillapi/dec/v1.03/ewayapi?action=GetEwayBill&ewbNo={ewbNo}", Method.GET);
                request.Timeout = -1;
                request.AddHeader("Gstin", GSTCredential.GSTIN);
                request.AddHeader("user_name", GSTCredential.EInvoiceUserName);
                request.AddHeader("AuthToken", GSTCredential.AuthToken);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                IRestResponse response = client.Execute(request);
                gstEWayBillResponse = JsonConvert.DeserializeObject<GSTEWayBillResponse>(response.Content);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && gstEWayBillResponse.Status == "1")
                {
                    RestClient clientPost = new RestClient();
                    RestRequest requestPost = new RestRequest($"{GSTCredential.Urls}/aspapi/v1.0/printewb", Method.POST);
                    requestPost.AddHeader("aspid", GSTCredential.AspId);
                    requestPost.AddHeader("password", GSTCredential.AspUserPassword);
                    requestPost.AddHeader("Gstin", GSTCredential.GSTIN);
                    requestPost.AddHeader("Content-Type", "text/plain");
                    var body = response.Content;
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    byte[] responsePost = client.DownloadData(request);
                    try
                    {
                        //This methods checks and creates directory if does not exists
                        string pdfFolderPath = @"MyeWaybillDoc\"; //Providing Path
                        string pdfPath = pdfFolderPath + ewbNo + ".pdf";
                        //Writing the responce in Byte
                        //File.WriteAllBytes(pdfPath, response);
                        //if (displayPdf == true)
                        //    Process.Start(pdfPath); //Code to display .pdf file
                    }
                    catch (Exception ex)
                    { 
                    
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(gstEWayBillResponse?.error?.message))
                    {
                        gstEWayBillResponse.ErrorMessage = gstEWayBillResponse?.error?.error_cd + ":" + gstEWayBillResponse?.error?.message;
                    }
                    else
                    {
                        if (gstEWayBillResponse?.ErrorDetails?.Count > 0)
                        {
                            foreach (var error in gstEWayBillResponse.ErrorDetails)
                            {
                                if (string.IsNullOrEmpty(gstEWayBillResponse.ErrorMessage))
                                {
                                    gstEWayBillResponse.ErrorMessage = $"Error Message:({error.ErrorCode}){error.ErrorMessage}";
                                }
                                else
                                {
                                    gstEWayBillResponse.ErrorMessage = $"{gstEWayBillResponse.ErrorMessage} and ({error.ErrorCode}){error.ErrorMessage}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static GSTEWayBillCancelledResponse CancelledEWayBill(GSTEWayBillCancelledRequestModel gstEWayBillCancelledRequestModel, OrganisationCentrewiseGSTCredential GSTCredential)
        {
            GSTEWayBillCancelledResponse gstEWayBillCancelledResponse = new GSTEWayBillCancelledResponse();
            try
            {
                string requestBody = JsonConvert.SerializeObject(gstEWayBillCancelledRequestModel);
                RestClient client = new RestClient();
                RestRequest request = new RestRequest($"{GSTCredential.Urls}/ewaybillapi/dec/v1.03/ewayapi?action=CANEWB&gstin={GSTCredential.GSTIN}&username={GSTCredential.EInvoiceUserName}&authtoken={GSTCredential.AuthToken}", Method.POST);
                request.AddHeader("aspid", GSTCredential.AspId);
                request.AddHeader("password", GSTCredential.AspUserPassword);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(requestBody);     //Request Payload in object format
                IRestResponse response = client.Execute(request);
                
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
                {
                    gstEWayBillCancelledResponse.Data = response.Content;
                    return gstEWayBillCancelledResponse;
                }
                else
                {
                    if (!string.IsNullOrEmpty(gstEWayBillCancelledResponse?.error?.message))
                    {
                        gstEWayBillCancelledResponse.ErrorMessage = gstEWayBillCancelledResponse?.error?.error_cd + ":" + gstEWayBillCancelledResponse?.error?.message;
                    }
                    else
                    {
                        if (gstEWayBillCancelledResponse?.ErrorDetails?.Count > 0)
                        {
                            foreach (var error in gstEWayBillCancelledResponse.ErrorDetails)
                            {
                                if (string.IsNullOrEmpty(gstEWayBillCancelledResponse.ErrorMessage))
                                {
                                    gstEWayBillCancelledResponse.ErrorMessage = $"Error Message:({error.ErrorCode}){error.ErrorMessage}";
                                }
                                else
                                {
                                    gstEWayBillCancelledResponse.ErrorMessage = $"{gstEWayBillCancelledResponse.ErrorMessage} and ({error.ErrorCode}){error.ErrorMessage}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gstEWayBillCancelledResponse = null;
            }
            return gstEWayBillCancelledResponse;
        }

        #endregion
    }
}