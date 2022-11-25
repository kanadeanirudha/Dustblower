<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.SalesInvoiceMasterCancelledReportController controller = new AERP.Web.UI.Controllers.SalesInvoiceMasterCancelledReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvSalesInvoiceMasterCancelledReport);
            if (!IsPostBack)
            {
                rvSalesInvoiceMasterCancelledReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.SalesInvoiceMasterCancelledReport> SalesInvoiceMasterCancelledReport = null;

                if (Request.RequestType == "POST")
                {

                    SalesInvoiceMasterCancelledReport = controller.GetSalesInvoiceMasterCancelledReportList();
                }

                if (SalesInvoiceMasterCancelledReport == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvSalesInvoiceMasterCancelledReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/SalesInvoiceMasterCancelledReport.rdlc");
                    rvSalesInvoiceMasterCancelledReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "SalesInvoiceMasterCancelledReportDataSet";//Data Set Name
                    rdc.Value = SalesInvoiceMasterCancelledReport;                //DTO object
                    rvSalesInvoiceMasterCancelledReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[3];
                     param[0] = new ReportParameter("CentreName", SalesInvoiceMasterCancelledReport.Count > 0 ? SalesInvoiceMasterCancelledReport[0].CentreName : string.Empty, false);
                   // param[1] = new ReportParameter("CentreEstCode", EmployeePFReportForm9.Count > 0 ? EmployeePFReportForm9[0].CentreName : string.Empty, false);
                    param[1] = new ReportParameter("MonthName", SalesInvoiceMasterCancelledReport.Count > 0 ? SalesInvoiceMasterCancelledReport[0].MonthFullName : string.Empty, false);
                    param[2] = new ReportParameter("Year", SalesInvoiceMasterCancelledReport.Count > 0 ? SalesInvoiceMasterCancelledReport[0].MonthYear : string.Empty, false);
                    rvSalesInvoiceMasterCancelledReport.LocalReport.SetParameters(param);
                    rvSalesInvoiceMasterCancelledReport.LocalReport.Refresh();
                    rvSalesInvoiceMasterCancelledReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvSalesInvoiceMasterCancelledReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.SalesInvoiceMasterCancelledReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="SalesInvoiceMasterCancelledReportDataSet" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

              
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="SalesInvoiceMasterCancelledReportListDataSet1TableAdapters.InventorySalesInvoiceMasterCancelledReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>
       
    </form>
</body>
</html>


