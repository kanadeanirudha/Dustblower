<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Report For ContactPerson</title>
    <script runat="server">
   
        AERP.Web.UI.Controllers.SalesOrderMonthlyDeliveryReportController controller = new AERP.Web.UI.Controllers.SalesOrderMonthlyDeliveryReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }

        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvSaleOrderMonthlyDeliveryReport);
            if (!IsPostBack)
            {
                rvSaleOrderMonthlyDeliveryReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.InventoryReport> InventoryReport = null;
                //List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;

                {
                    InventoryReport = controller.GetSaleOrderDailyReportList();
                }

                if (InventoryReport == null)
                {
                    //MainDiv.Visible = false;
                    //NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvSaleOrderMonthlyDeliveryReport.LocalReport.ReportPath = Server.MapPath("~/Report/Inventory/SaleOrderMonthlyDeliveryReport.rdlc");
                    rvSaleOrderMonthlyDeliveryReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "SaleOrderMonthlyDeliveryReportDataset";//Data Set Name
                    rdc.Value = InventoryReport;                //DTO object
                    rvSaleOrderMonthlyDeliveryReport.LocalReport.DataSources.Add(rdc);

                    //ReportParameter[] param = new ReportParameter[1];
                    //param[0] = new ReportParameter("CentreName", GeneralItemMissingExceptionReport.Count > 0 ? GeneralItemMissingExceptionReport[0].CentreName : string.Empty, false);
                    //rvItemMasterReportForCategoryDetails.LocalReport.SetParameters(param);

                    rvSaleOrderMonthlyDeliveryReport.LocalReport.Refresh();
                    rvSaleOrderMonthlyDeliveryReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvSaleOrderMonthlyDeliveryReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Inventory.SaleOrderMonthlyDeliveryReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="SaleOrderMonthlyDeliveryReportDataset" />


                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="SaleOrderMonthlyDeliveryReportDataset2TableAdapters.SaleOrderMonthlyDeliveryReportDataset2TableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


