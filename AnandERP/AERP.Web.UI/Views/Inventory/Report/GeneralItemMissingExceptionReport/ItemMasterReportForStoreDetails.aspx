﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Report For ContactPerson</title>
    <script runat="server">
   
        AERP.Web.UI.Controllers.GeneralItemMissingExceptionReportController controller = new AERP.Web.UI.Controllers.GeneralItemMissingExceptionReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
       
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvItemMasterReportForStoreDetails);
            if (!IsPostBack)
            {
                rvItemMasterReportForStoreDetails.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.InventoryReport> GeneralItemMissingExceptionReport = null;
                //List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;

                if (Request.RequestType == "POST")
                {

                    GeneralItemMissingExceptionReport = controller.GetMissingExceptionReportList("StoreDetails");
                }
                
                if (GeneralItemMissingExceptionReport == null)
                {
                    //MainDiv.Visible = false;
                    //NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvItemMasterReportForStoreDetails.LocalReport.ReportPath = Server.MapPath("~/Report/Inventory/ItemMasterReportForStoreDetails.rdlc");
                    rvItemMasterReportForStoreDetails.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "ItemMasterStoreDetailsDataSet1";//Data Set Name
                    rdc.Value = GeneralItemMissingExceptionReport;                //DTO object
                    rvItemMasterReportForStoreDetails.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[1];
                    param[0] = new ReportParameter("CentreName", GeneralItemMissingExceptionReport.Count > 0 ? GeneralItemMissingExceptionReport[0].CentreName : string.Empty, false);
                    rvItemMasterReportForStoreDetails.LocalReport.SetParameters(param);

                    rvItemMasterReportForStoreDetails.LocalReport.Refresh();
                    rvItemMasterReportForStoreDetails.Visible = true;
                }
            }
        
        }
        
    </script>
</head>
<body>
    <form runat="server">
        <div id="MainDiv" runat="server">
            <div id="categoryPrint">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <%--   Please Select Report :&nbsp&nbsp&nbsp;--%>
               
                 <rsweb:ReportViewer ID="rvItemMasterReportForStoreDetails" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Inventory.ItemMasterReportForStoreDetails.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ItemMasterStoreDetailsDataSet1" />

                          
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

              
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ItemMasterStoreDetailsDataSet1TableAdapters.ItemMasterStoreDetailsDataSet1TableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>
       
    </form>
</body>
</html>


