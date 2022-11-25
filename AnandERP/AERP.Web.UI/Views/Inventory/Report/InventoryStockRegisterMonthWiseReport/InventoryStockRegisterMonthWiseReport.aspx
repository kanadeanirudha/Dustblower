﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.InventoryStockRegisterMonthWiseReportController controller = new AERP.Web.UI.Controllers.InventoryStockRegisterMonthWiseReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvInventoryStockRegisterMonthWiseReport);
            if (!IsPostBack)
            {
                rvInventoryStockRegisterMonthWiseReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.InventoryReport> InventoryReport = null;
                //List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;



                InventoryReport = controller.GetInventoryStockRegisterMonthWiseReportList();


                if (InventoryReport == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvInventoryStockRegisterMonthWiseReport.LocalReport.ReportPath = Server.MapPath("~/Report/Inventory/InventoryStockRegisterMonthWiseReport.rdlc");
                    rvInventoryStockRegisterMonthWiseReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "InventoryStockRegisterMonthWiseReportDataSet";//Data Set Name
                    rdc.Value = InventoryReport;                //DTO object
                    rvInventoryStockRegisterMonthWiseReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("CentreName", InventoryReport.Count > 0 ? InventoryReport[0].CentreName : string.Empty, false);
                    param[1] = new ReportParameter("Month", InventoryReport.Count > 0 ? InventoryReport[0].MonthName : string.Empty, false);
                    param[2] = new ReportParameter("Year", InventoryReport.Count > 0 ? InventoryReport[0].YearReport : string.Empty, false);
                    rvInventoryStockRegisterMonthWiseReport.LocalReport.SetParameters(param);

                    rvInventoryStockRegisterMonthWiseReport.LocalReport.Refresh();
                    rvInventoryStockRegisterMonthWiseReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvInventoryStockRegisterMonthWiseReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Inventory.InventoryStockRegisterMonthWiseReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="InventoryStockRegisterMonthWiseReportDataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="InventoryStockRegisterMonthWiseReportDataSetTableAdapters.InventoryStockRegisterMonthWiseReportTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


