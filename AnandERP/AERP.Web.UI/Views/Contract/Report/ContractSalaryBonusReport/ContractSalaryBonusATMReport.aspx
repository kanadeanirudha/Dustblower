<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.ContractSalaryBonusReportController controller = new AERP.Web.UI.Controllers.ContractSalaryBonusReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvContractSalaryBonusATMReport);
            if (!IsPostBack)
            {
                rvContractSalaryBonusATMReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.ContractSalaryBonusReport> ContractSalaryBonusATMReport = null;
                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;
                ContractSalaryBonusATMReport = controller.GetContractSalaryBonusATMReportList();

                if (ContractSalaryBonusATMReport.Count == 0)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvContractSalaryBonusATMReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/ContractSalaryBonusATMReport.rdlc");

                    rvContractSalaryBonusATMReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "ContractSalaryBonusATMReportDataSet";//Data Set Name
                    rdc.Value = ContractSalaryBonusATMReport;                //DTO object
                    rvContractSalaryBonusATMReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("FromDate", ContractSalaryBonusATMReport.Count > 0 ? ContractSalaryBonusATMReport[0].FromDate : string.Empty, false);
                    param[1] = new ReportParameter("UptoDate", ContractSalaryBonusATMReport.Count > 0 ? ContractSalaryBonusATMReport[0].UptoDate : string.Empty, false);
                    param[2] = new ReportParameter("BranchName", ContractSalaryBonusATMReport.Count > 0 ? ContractSalaryBonusATMReport[0].CustomerBranchMasterName : string.Empty, false);
                    rvContractSalaryBonusATMReport.LocalReport.SetParameters(param);

                    rvContractSalaryBonusATMReport.LocalReport.Refresh();
                    rvContractSalaryBonusATMReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvContractSalaryBonusATMReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.ContractSalaryBonusATMReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ContractSalaryBonusATMReportDataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ContractSalaryBonusATMReportListDataSetTableAdapters.InventoryContractSalaryBonusATMReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


