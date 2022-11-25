<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Inventory Item Price Report</title>
    <script runat="server">

        AERP.Web.UI.Controllers.SalesRegisterDrillDownReportController controller = new AERP.Web.UI.Controllers.SalesRegisterDrillDownReportController();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        List<AERP.DTO.SalesRegisterDrillDownReport> SalesRegisterDrillDownReport = null;
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvSalesRegisterDrillDownReport);
            if (!IsPostBack)
            {
                rvSalesRegisterDrillDownReport.ProcessingMode = ProcessingMode.Local;

                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;



                SalesRegisterDrillDownReport = controller.GetSalesRegisterDrillDownReportList();


                if (SalesRegisterDrillDownReport == null)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;
                    NoRecordDiv.Visible = false;
                    rvSalesRegisterDrillDownReport.LocalReport.ReportPath = Server.MapPath("~/Report/Contract/SalesRegisterDrillDownReport.rdlc");
                    rvSalesRegisterDrillDownReport.LocalReport.DataSources.Clear();//collection Of Reports

                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "SalesRegisterDrillDownReport1DataSet";//Data Set Name
                    rdc.Value = SalesRegisterDrillDownReport;                //DTO object
                    rvSalesRegisterDrillDownReport.LocalReport.DataSources.Add(rdc);

                    ReportParameter[] param = new ReportParameter[2];
                    param[0] = new ReportParameter("CentreName", SalesRegisterDrillDownReport.Count > 0 ? SalesRegisterDrillDownReport[0].CentreName : string.Empty, false);
                    param[1] = new ReportParameter("AccountSessionName", SalesRegisterDrillDownReport.Count > 0 ? SalesRegisterDrillDownReport[0].AccountSessionName : string.Empty, false);
                    //param[2] = new ReportParameter("CentreName", SalesRegisterDrillDownReport.Count > 0 ? SalesRegisterDrillDownReport[0].CentreName : string.Empty, false);
                    //param[3] = new ReportParameter("EmployeeFathersFullName", SalesRegisterDrillDownReport.Count > 0 ? SalesRegisterDrillDownReport[0].EmployeeFathersFullName : string.Empty, false);
                    //param[4] = new ReportParameter("PFAccountNmber", SalesRegisterDrillDownReport.Count > 0 ? SalesRegisterDrillDownReport[0].PFAccountNmber : string.Empty, false);
                    //param[5] = new ReportParameter("RateOfContribution", SalesRegisterDrillDownReport.Count > 0 ? Convert.ToString(SalesRegisterDrillDownReport[0].RateOfContribution):  string.Empty, false);
                    //param[6] = new ReportParameter("FromDate", SalesRegisterDrillDownReport.Count > 0 ? Convert.ToString(SalesRegisterDrillDownReport[0].FromDate):  string.Empty, false);
                    //param[7] = new ReportParameter("UptoDate", SalesRegisterDrillDownReport.Count > 0 ? Convert.ToString(SalesRegisterDrillDownReport[0].UptoDate):  string.Empty, false);
                    rvSalesRegisterDrillDownReport.LocalReport.SetParameters(param);

                    rvSalesRegisterDrillDownReport.LocalReport.Refresh();
                    rvSalesRegisterDrillDownReport.Visible = true;
                }
            }
            rvSalesRegisterDrillDownReport.Drillthrough += new DrillthroughEventHandler(rvSalesRegisterDrillDownReport_Drillthrough);

        }
        public void rvSalesRegisterDrillDownReport_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            /*Collect report parameter from drillthrough report*/
            ReportParameterInfoCollection DrillThroughValues = e.Report.GetParameters();
            // Type parameterName = Type.Parse(DrillThroughValues[1].Values[0].ToString());
            if (DrillThroughValues[0].Name == "CentreName")
            {
                SalesRegisterDrillDownReport = controller.GetSalesRegisterDrillDownReportList2(DrillThroughValues[2].Values[0].ToString(), DrillThroughValues[3].Values[0].ToString(), DrillThroughValues[4].Values[0].ToString(), DrillThroughValues[5].Values[0].ToString(), DrillThroughValues[0].Values[0].ToString());

                /*Bind data source with report*/
                LocalReport localReport = (LocalReport)e.Report;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(new ReportDataSource("SalesRegisterDrillDownReport2DataSet", SalesRegisterDrillDownReport));
                localReport.EnableHyperlinks = true;

                /*Add parameter to the report if report have paramerter*/
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("CentreCode", DrillThroughValues[2].Values[0].ToString()));
                parameters.Add(new ReportParameter("TransMonth", DrillThroughValues[3].Values[0].ToString()));
                parameters.Add(new ReportParameter("TransYear", DrillThroughValues[4].Values[0].ToString()));
                parameters.Add(new ReportParameter("TransMonthName", DrillThroughValues[5].Values[0].ToString()));
                parameters.Add(new ReportParameter("CentreName", DrillThroughValues[0].Values[0].ToString()));
                localReport.SetParameters(parameters);

                localReport.Refresh();
            }
            else if (DrillThroughValues[0].Name == "CustomerInvoiceNumber")
            {
                SalesRegisterDrillDownReport = controller.GetSalesRegisterDrillDownReportList3(DrillThroughValues[0].Values[0].ToString(), DrillThroughValues[1].Values[0].ToString());

                /*Bind data source with report*/
                LocalReport localReport = (LocalReport)e.Report;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(new ReportDataSource("SalesRegisterDrillDownReport3DataSet", SalesRegisterDrillDownReport));
                localReport.EnableHyperlinks = true;

                /*Add parameter to the report if report have paramerter*/
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("CustomerInvoiceNumber", DrillThroughValues[0].Values[0].ToString()));
                parameters.Add(new ReportParameter("SaleInvoiceMasterID", DrillThroughValues[1].Values[0].ToString()));
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

                <rsweb:ReportViewer ID="rvSalesRegisterDrillDownReport" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="" Width="" SizeToReportContent="True">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.Contract.SalesRegisterDrillDownReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="SalesRegisterDrillDownReport1DataSet" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>


                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="SalesRegisterDrillDownReportListDataSetTableAdapters.InventorySalesRegisterDrillDownReportListTableAdapter"></asp:ObjectDataSource>

            </div>

        </div>
        <div id="NoRecordDiv" runat="server" style="text-align: center;">

            <b>No Record Found</b>

        </div>

    </form>
</body>
</html>


