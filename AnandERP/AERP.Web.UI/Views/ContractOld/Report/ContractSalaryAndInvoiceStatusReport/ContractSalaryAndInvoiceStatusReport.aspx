<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.ContractSalaryAndInvoiceStatusReportController controller = new AERP.Web.UI.Controllers.ContractSalaryAndInvoiceStatusReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvContractSalaryAndInvoiceStatusReport);
            if (!IsPostBack)
            {
                rvContractSalaryAndInvoiceStatusReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.ContractSalaryAndInvoiceStatusReport> ContractSalaryAndInvoiceStatusReport = null;
                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;
                ContractSalaryAndInvoiceStatusReport = controller.GetContractSalaryAndInvoiceStatusReportList();

                if (ContractSalaryAndInvoiceStatusReport.Count == 0)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;

                    rvContractSalaryAndInvoiceStatusReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/ContractSalaryAndInvoiceStatusReport.rdlc");

                    rvContractSalaryAndInvoiceStatusReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "ContractSalaryAndInvoiceStatusReportDataSet";//Data Set Name
                    rdc.Value = ContractSalaryAndInvoiceStatusReport;                //DTO object
                    rvContractSalaryAndInvoiceStatusReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("CentreCode", ContractSalaryAndInvoiceStatusReport.Count > 0 ? ContractSalaryAndInvoiceStatusReport[0].CentreCode : string.Empty, false);
                    param[1] = new ReportParameter("SalaryMonth", ContractSalaryAndInvoiceStatusReport.Count > 0 ? ContractSalaryAndInvoiceStatusReport[0].SalaryMonth : string.Empty, false);
                    param[2] = new ReportParameter("SalaryYear", ContractSalaryAndInvoiceStatusReport.Count > 0 ? ContractSalaryAndInvoiceStatusReport[0].SalaryYear : string.Empty, false);
                    rvContractSalaryAndInvoiceStatusReport.LocalReport.SetParameters(param);

                    rvContractSalaryAndInvoiceStatusReport.LocalReport.Refresh();
                    rvContractSalaryAndInvoiceStatusReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvContractSalaryAndInvoiceStatusReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.ContractSalaryAndInvoiceStatusReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ContractSalaryAndInvoiceStatusReportDataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ContractSalaryAndInvoiceStatusReportListDataSetTableAdapters.InventoryContractSalaryAndInvoiceStatusReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


