<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.EmployeeESICForm6ReportController controller = new AERP.Web.UI.Controllers.EmployeeESICForm6ReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvEmployeeESICForm6Report);
            if (!IsPostBack)
            {
                rvEmployeeESICForm6Report.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.EmployeeESICForm6Report> EmployeeESICForm6Report = null;

                if (Request.RequestType == "POST")
                {

                    EmployeeESICForm6Report = controller.GetEmployeeESICForm6ReportList();
                }

                if (EmployeeESICForm6Report == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvEmployeeESICForm6Report.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/EmployeeESICForm6Report.rdlc");
                    rvEmployeeESICForm6Report.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "EmployeeESICForm6ReportDataSet";//Data Set Name
                    rdc.Value = EmployeeESICForm6Report;                //DTO object
                    rvEmployeeESICForm6Report.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[5];
                    param[0] = new ReportParameter("CentreName", EmployeeESICForm6Report.Count > 0 ? EmployeeESICForm6Report[0].CentreName : string.Empty, false);
                    param[1] = new ReportParameter("FromDate", EmployeeESICForm6Report.Count > 0 ? EmployeeESICForm6Report[0].FromDate : string.Empty, false);
                    param[2] = new ReportParameter("UptoDate", EmployeeESICForm6Report.Count > 0 ? EmployeeESICForm6Report[0].UptoDate : string.Empty, false);
                    param[3] = new ReportParameter("ESICZone", EmployeeESICForm6Report.Count > 0 ? EmployeeESICForm6Report[0].ESICZone : string.Empty, false);
                    param[4] = new ReportParameter("ZoneCode", EmployeeESICForm6Report.Count > 0 ? EmployeeESICForm6Report[0].ZoneCode : string.Empty, false);
                    rvEmployeeESICForm6Report.LocalReport.SetParameters(param);
                    rvEmployeeESICForm6Report.LocalReport.Refresh();
                    rvEmployeeESICForm6Report.Visible = true;
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

                <rsweb:ReportViewer ID="rvEmployeeESICForm6Report" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.EmployeeESICForm6Report.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="EmployeeESICForm6ReportDataSet" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

              
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="EmployeeESICForm6ReportListDataSetTableAdapters.InventoryEmployeeESICForm6ReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>
       
    </form>
</body>
</html>


