<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.EmployeeESICSummaryReportController controller = new AERP.Web.UI.Controllers.EmployeeESICSummaryReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvEmployeeESICSummaryReport);
            if (!IsPostBack)
            {
                rvEmployeeESICSummaryReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.EmployeeESICSummaryReport> EmployeeESICSummaryReport = null;

                if (Request.RequestType == "POST")
                {

                    EmployeeESICSummaryReport = controller.GetEmployeeESICSummaryReportList();
                }

                if (EmployeeESICSummaryReport == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvEmployeeESICSummaryReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/EmployeeESICSummaryReport.rdlc");
                    rvEmployeeESICSummaryReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "EmployeeESICSummaryReportDataSet";//Data Set Name
                    rdc.Value = EmployeeESICSummaryReport;                //DTO object
                    rvEmployeeESICSummaryReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[4];
                    param[0] = new ReportParameter("CentreName", EmployeeESICSummaryReport.Count > 0 ? EmployeeESICSummaryReport[0].CentreName : string.Empty, false);
                    param[1] = new ReportParameter("FromDate", EmployeeESICSummaryReport.Count > 0 ? EmployeeESICSummaryReport[0].FromDate : string.Empty, false);
                    param[2] = new ReportParameter("UptoDate", EmployeeESICSummaryReport.Count > 0 ? EmployeeESICSummaryReport[0].UptoDate : string.Empty, false);
                    param[3] = new ReportParameter("ESICZone", EmployeeESICSummaryReport.Count > 0 ? EmployeeESICSummaryReport[0].ESICZone : string.Empty, false);
                    rvEmployeeESICSummaryReport.LocalReport.SetParameters(param);
                    rvEmployeeESICSummaryReport.LocalReport.Refresh();
                    rvEmployeeESICSummaryReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvEmployeeESICSummaryReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.EmployeeESICSummaryReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="EmployeeESICSummaryReportDataSet" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

              
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="EmployeeESICSummaryReportListDataSetTableAdapters.InventoryEmployeeESICSummaryReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>
       
    </form>
</body>
</html>


