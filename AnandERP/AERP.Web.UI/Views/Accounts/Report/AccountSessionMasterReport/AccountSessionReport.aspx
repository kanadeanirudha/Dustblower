﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>AccountSessionMasterReport</title>
    <script runat="server">
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Context.Handler = this.Page;
        }
        void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.rvAccountSessionList);
            if (!IsPostBack)
            {
                List<AERP.DTO.AccountSessionMasterReport> accountSessionMasterReport = null;
                List<AERP.DTO.OrganisationStudyCentreMaster> OrganisationStudyCentreMasterDetails = null;

                AERP.Web.UI.Controllers.AccountSessionMasterReportController controller = new AERP.Web.UI.Controllers.AccountSessionMasterReportController();
                OrganisationStudyCentreMasterDetails = controller.GetReportHeader();
                accountSessionMasterReport = controller.GetListAccountSessionMaster();
                if (accountSessionMasterReport.Count == 0 || OrganisationStudyCentreMasterDetails.Count == 0)
                {
                    MainDiv.Visible = false;
                    NoRecordDiv.Visible = true;
                }
                else
                {
                    MainDiv.Visible = true;    
                    NoRecordDiv.Visible = false;
                    rvAccountSessionList.LocalReport.ReportPath = Server.MapPath("~/Report/AccountReports/AccountSessionListReport.rdlc");
                    rvAccountSessionList.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource();
                    rdc.Name = "DataSetAccountSessionMasterReport";
                    rdc.Value = accountSessionMasterReport;
                    rvAccountSessionList.LocalReport.DataSources.Add(rdc);

                    ReportDataSource rdc2 = new ReportDataSource();
                    rdc2.Name = "StudyCentrePrintingFormat";
                    rdc2.Value = OrganisationStudyCentreMasterDetails;
                    rvAccountSessionList.LocalReport.DataSources.Add(rdc2);
                    
                    rvAccountSessionList.LocalReport.Refresh();
                    rvAccountSessionList.Visible = true;
                }
            }
        }
       

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <div id="MainDiv" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="rvAccountSessionList" runat="server" AsyncRendering="False" SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    <LocalReport ReportEmbeddedResource="AERP.Web.UI.Report.AccountSessionListReport.rdlc">

                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="AccountSessionList" />
                               <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="StudyCentrePrintingFormat" />

                        </DataSources>

                    </LocalReport>
                </rsweb:ReportViewer>

                  <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="AERPDataSetTableAdapters.OrgStudyCentrePrintingFormatTableAdapter"></asp:ObjectDataSource>
      
                <br />


            </div>
        </div>
        <div id="NoRecordDiv" runat="server" style="text-align:center;" >
                
           <b>No Record Found</b> 
        
        </div>

    </form>
</body>
</html>
