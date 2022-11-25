<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.ContractWisePFESICChecklistReportController controller = new AERP.Web.UI.Controllers.ContractWisePFESICChecklistReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvContractWisePFESICChecklistReport);
            if (!IsPostBack)
            {
                rvContractWisePFESICChecklistReport.ProcessingMode = ProcessingMode.Local;
                List<AERP.DTO.ContractWisePFESICChecklistReport> ContractWisePFESICChecklistReport = null;
                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;
                ContractWisePFESICChecklistReport = controller.GetContractWisePFESICChecklistReportList();

                if (ContractWisePFESICChecklistReport.Count == 0)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;

                    rvContractWisePFESICChecklistReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/ContractWisePFESICChecklistReport.rdlc");

                    rvContractWisePFESICChecklistReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "ContractWisePFESICChecklistReportDataSet";//Data Set Name
                    rdc.Value = ContractWisePFESICChecklistReport;                //DTO object
                    rvContractWisePFESICChecklistReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[12];
                    param[0] = new ReportParameter("CentreCode", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].CentreCode : string.Empty, false);
                    param[1] = new ReportParameter("SalaryMonth", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].SalaryMonth : string.Empty, false);
                    param[2] = new ReportParameter("SalaryYear", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].SalaryYear : string.Empty, false);
                    param[3] = new ReportParameter("TotalAcc01", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalAcc01.ToString() : string.Empty, false);
                    param[4] = new ReportParameter("TotalPFWorkerShare", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalPFWorkerShare.ToString() : string.Empty, false);
                    param[5] = new ReportParameter("TotalAcc02", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalAcc02.ToString() : string.Empty, false);
                    param[6] = new ReportParameter("TotalAcc21", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalAcc21.ToString() : string.Empty, false);
                    param[7] = new ReportParameter("TotalAcc10", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalAcc10.ToString() : string.Empty, false);
                    param[8] = new ReportParameter("TotalAcc22", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalAcc22.ToString() : string.Empty, false);
                    param[9] = new ReportParameter("TotalESICWorkersShare", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalESICWorkersShare.ToString() : string.Empty, false);
                    param[10] = new ReportParameter("TotalESICEmployerShare", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalESICEmployerShare.ToString() : string.Empty, false);
                    param[11] = new ReportParameter("TotalPF", ContractWisePFESICChecklistReport.Count > 0 ? ContractWisePFESICChecklistReport[0].TotalPF.ToString() : string.Empty, false);
                    
                    rvContractWisePFESICChecklistReport.LocalReport.SetParameters(param);

                    rvContractWisePFESICChecklistReport.LocalReport.Refresh();
                    rvContractWisePFESICChecklistReport.Visible = true;
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

                <rsweb:ReportViewer ID="rvContractWisePFESICChecklistReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.ContractWisePFESICChecklistReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ContractWisePFESICChecklistReportDataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ContractWisePFESICChecklistReportListDataSetTableAdapters.InventoryContractWisePFESICChecklistReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


