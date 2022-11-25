<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.AllAccountDrillDownReportController controller = new AERP.Web.UI.Controllers.AllAccountDrillDownReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        List<AERP.DTO.AllAccountDrillDownReport> AllAccountDrillDownReport = null;
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvAllAccountDrillDownReport);
            if (!IsPostBack)
            {
                rvAllAccountDrillDownReport.ProcessingMode = ProcessingMode.Local;

                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;



                AllAccountDrillDownReport = controller.GetAllAccountDrillDownReportList();


                if (AllAccountDrillDownReport == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvAllAccountDrillDownReport.LocalReport.ReportPath = Server.MapPath("~/Report/AccountReports/AllAccountDrillDownReport.rdlc");
                    rvAllAccountDrillDownReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "AllAccountDrillDownReportDataSet";//Data Set Name
                    rdc.Value = AllAccountDrillDownReport;                //DTO object
                    rvAllAccountDrillDownReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[1];
                    param[0] = new ReportParameter("CentreName", AllAccountDrillDownReport.Count > 0 ? AllAccountDrillDownReport[0].CentreName : string.Empty, false);
                    //param[1] = new ReportParameter("AccountSessionName", AllAccountDrillDownReport.Count > 0 ? AllAccountDrillDownReport[0].AccountSessionName : string.Empty, false);
                    //param[2] = new ReportParameter("CentreName", AllAccountDrillDownReport.Count > 0 ? AllAccountDrillDownReport[0].CentreName : string.Empty, false);
                    //param[3] = new ReportParameter("EmployeeFathersFullName", AllAccountDrillDownReport.Count > 0 ? AllAccountDrillDownReport[0].EmployeeFathersFullName : string.Empty, false);
                    //param[4] = new ReportParameter("PFAccountNmber", AllAccountDrillDownReport.Count > 0 ? AllAccountDrillDownReport[0].PFAccountNmber : string.Empty, false);
                    //param[5] = new ReportParameter("RateOfContribution", AllAccountDrillDownReport.Count > 0 ? Convert.ToString(AllAccountDrillDownReport[0].RateOfContribution):  string.Empty, false);
                    //param[6] = new ReportParameter("FromDate", AllAccountDrillDownReport.Count > 0 ? Convert.ToString(AllAccountDrillDownReport[0].FromDate):  string.Empty, false);
                    //param[7] = new ReportParameter("UptoDate", AllAccountDrillDownReport.Count > 0 ? Convert.ToString(AllAccountDrillDownReport[0].UptoDate):  string.Empty, false);
                    rvAllAccountDrillDownReport.LocalReport.SetParameters(param);

                    rvAllAccountDrillDownReport.LocalReport.Refresh();
                    rvAllAccountDrillDownReport.Visible = true;
                }
            }
            rvAllAccountDrillDownReport.Drillthrough += new DrillthroughEventHandler(rvAllAccountDrillDownReport_Drillthrough);

        }
        public void rvAllAccountDrillDownReport_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            /*Collect report parameter from drillthrough report*/
            ReportParameterInfoCollection DrillThroughValues = e.Report.GetParameters();
            // Type parameterName = Type.Parse(DrillThroughValues[1].Values[0].ToString());
            if (DrillThroughValues[0].Name == "TransactionMainID")
            {
                AllAccountDrillDownReport = controller.GetAllAccountDrillDownReportList3(DrillThroughValues[0].Values[0].ToString(), DrillThroughValues[1].Values[0].ToString());

                /*Bind data source with report*/
                LocalReport localReport = (LocalReport)e.Report;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(new ReportDataSource("AllAccountDrillDownReport3DataSet", AllAccountDrillDownReport));
                localReport.EnableHyperlinks = true;

                /*Add parameter to the report if report have paramerter*/
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("TransactionMainID", DrillThroughValues[0].Values[0].ToString()));
                parameters.Add(new ReportParameter("VoucherNoWithTranType", DrillThroughValues[1].Values[0].ToString()));
                localReport.SetParameters(parameters);
                localReport.Refresh();
            }
            else if (DrillThroughValues[7].Name == "AccountName")
            {
                AllAccountDrillDownReport = controller.GetAllAccountDrillDownReportList2(DrillThroughValues[0].Values[0].ToString(), DrillThroughValues[1].Values[0].ToString(), DrillThroughValues[2].Values[0].ToString(), DrillThroughValues[3].Values[0].ToString(), DrillThroughValues[4].Values[0].ToString(), DrillThroughValues[5].Values[0].ToString(), DrillThroughValues[6].Values[0].ToString(), DrillThroughValues[8].Values[0].ToString());

                /*Bind data source with report*/
                LocalReport localReport = (LocalReport)e.Report;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(new ReportDataSource("AllAccountDrillDownReport2DataSet", AllAccountDrillDownReport));
                localReport.EnableHyperlinks = true;

                /*Add parameter to the report if report have paramerter*/
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("AccountName", DrillThroughValues[7].Values[0].ToString()));
                localReport.SetParameters(parameters);

                localReport.Refresh();
            }
        }

    </script>
</head>
<body>
    <form runat="server">
        <div id="MainDiv" runat="server">
            <div id="categoryPrint">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <rsweb:ReportViewer ID="rvAllAccountDrillDownReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.AllAccountDrillDownReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="AllAccountDrillDownReport1DataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="AllAccountDrillDownReportListDataSetTableAdapters.InventoryAllAccountDrillDownReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


