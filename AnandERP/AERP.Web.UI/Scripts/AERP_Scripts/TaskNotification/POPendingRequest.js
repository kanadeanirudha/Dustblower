//this class contain methods related to nationality functionality
var POPendingRequest = {
    //Member variables
    ActionName: null,
    //Class intialisation method
    XMLstring: null,
    Initialize: function () {
        //organisationStudyCentre.loadData();

        POPendingRequest.constructor();
        //POPendingRequest.initializeValidation();
    },
    //Attach all event of page
    constructor: function () {
        // Create new record

        $('#CreateApprovedPurchaseOrderMaster').on("click", function () {
            POPendingRequest.ActionName = "Approved";
            POPendingRequest.AjaxCallPOPendingRequest();
        });
    },
    ////Load method is used to load *-Load-* page
    //LoadList: function () {
    //    debugger;
    //    $.ajax(
    //     {
    //         cache: false,
    //         type: "POST",

    //         dataType: "html",
    //         url: '/Home/List',
    //         success: function (data) {
    //             //Rebind Grid Data
    //             $('#ListViewModel').html(data);
    //         }
    //     });
    //},
    //ReloadList method is used to load List page
    ReloadList: function (message, colorCode, actionMode) {
        var TaskCode = 'PO';
        $.magnificPopup.close();
        notify(message, colorCode);
        $('#' + TaskCode).click();
        commonFunctions.getNotificationCount('');
    },


    //Fire ajax call to insert update and delete record
    AjaxCallPOPendingRequest: function () {
        var POPendingRequestData = null;
        if (POPendingRequest.ActionName == "Approved") {
            POPendingRequestData = null;
            POPendingRequestData = POPendingRequest.GetPOPendingRequest();
            ajaxRequest.makeRequest("/PurchaseOrderMasterAndDetails/PurchaseOrderApproval", "POST", POPendingRequestData, POPendingRequest.Success);
        }
    },
    //Get properties data from the Create, Update and Delete page
    GetPOPendingRequest: function () {
        var Data = {
        };
        
        if (POPendingRequest.ActionName == "Approved" || POPendingRequest.ActionName == "Rejected") {
            Data.ID = $('#ID').val();
            Data.TaskCode = $('#TaskCode').val();
            Data.IsLastRecord = $('#IsLastRecord').val();
            Data.TaskNotificationMasterID = $('#TaskNotificationMasterID').val();
            Data.TaskNotificationDetailsID = $("#TaskNotificationDetailsID").val();
            Data.GeneralTaskReportingDetailsID = $("#GeneralTaskReportingDetailsID").val();
            Data.StageSequenceNumber = $("#StageSequenceNumber").val();
            Data.PersonID = $("#PersonID").val();
            Data.ApprovedStatus = $("#SelectedStatus").val();
        }
        return Data;
    },

    //this is used to for showing successfully record creation message and reload the list view
    Success: function (data) {
        var splitData = data.split(',');
        
        POPendingRequest.ReloadList(splitData[0], splitData[1], splitData[2]);
        
    },

    //CheckedAll: function () {
    //    $("#MyDataTablePendingRequest-PR thead tr th input[class='checkall-user']").on('click', function () {
    //        var $this = $(this);
    //        if ($this.is(":checked")) {
    //            $("#MyDataTablePendingRequest-PR tbody tr td p input[class='check-user']").prop("checked", true);
    //            $("#btnApproved").prop("disabled", false);
    //            $("#btnReject").prop("disabled", false);
    //            //$("#MyDataTablePendingLeaveRequest tbody tr").prop("color", '#fff');
    //        }
    //        else {
    //            $("#MyDataTablePendingRequest-PR tbody tr td p input[class='check-user']").prop("checked", false);
    //            //$("#MyDataTablePendingLeaveRequest tbody tr").prop("color", '#fff');
    //            $("#btnApproved").prop("disabled", true);
    //            $("#btnReject").prop("disabled", true);
    //        }
    //    });
    //},

    //CheckedSingle: function () {
    //    $("#MyDataTablePendingRequest-PR tbody tr td p input[class='check-user']").on('click', function () {
    //        var CheckedArray = [];
    //        var table = $('#MyDataTablePendingRequest-PR').DataTable();
    //        var TotalCheckedRecord;
    //        var data = table.$("input[class='check-user']").each(function () {
    //            CheckedArray.push($(this).val());
    //            var $this = $(this);
    //            if ($this.is(":checked")) {
    //                CheckedArray.push($(this).val());
    //                TotalCheckedRecord = CheckedArray.length;
    //            }
    //        });
    //        if (TotalCheckedRecord > 0) {
    //            $("#btnApproved").prop("disabled", false);
    //            $("#btnReject").prop("disabled", false);
    //        }
    //        else {
    //            $("#btnApproved").prop("disabled", true);
    //            $("#btnReject").prop("disabled", true);
    //        }
    //    });
    //},

    //GetXmlData: function () {
    //    var DataArray = [];
    //    ParameterXml = "<rows>";
    //    var table = $('#MyDataTablePendingRequest-PR').DataTable();
    //    var data = table.$("input[class='check-user']").each(function () {
    //        var $this = $(this);
    //        if ($this.is(":checked")) {
    //            var checkboxVal = $this.val();
    //            var splitedVal = checkboxVal.split('~');
    //            var EmployeeID = (splitedVal[0].split('='))[1];
    //            //var TotalDay = parseFloat(splitedVal[12]) + parseFloat(splitedVal[13] / 2);

    //            if (POPendingRequest.ActionName == "Approved") {
    //                //ParameterXml = ParameterXml + "<row><EmployeeID>" + EmployeeID + "</EmployeeID><LeaveApplicationID>" + splitedVal[9] + "</LeaveApplicationID><IsLast>" + splitedVal[4] + "</IsLast><TaskNotificationMasterID>" + splitedVal[1] + "</TaskNotificationMasterID><TaskNotificationDetailsID>" + splitedVal[0] + "</TaskNotificationDetailsID><GeneralTaskReportingDetailsID>" + splitedVal[2] + "</GeneralTaskReportingDetailsID><ApprovalStatus>1</ApprovalStatus><CentreCode>" + splitedVal[8] + "</CentreCode><LeaveMasterID>" + splitedVal[10] + "</LeaveMasterID><StageSequenceNumber>" + splitedVal[3] + "</StageSequenceNumber><TotalDay>" + TotalDay + "</TotalDay><TotalApprovedFullDay>" + splitedVal[12] + "</TotalApprovedFullDay><TotalApprovedHalfDay>" + splitedVal[13] + "</TotalApprovedHalfDay></row>";
    //                ParameterXml = ParameterXml + "<row><ApplicationID>" + splitedVal[9] + "</ApplicationID><EmployeeID> " + EmployeeID + "</EmployeeID><TaskCode >" + splitedVal[8] + "</TaskCode ><IsLast>" + splitedVal[1] + "</IsLast><TaskNotificationMasterID>" + splitedVal[2] + "</TaskNotificationMasterID><TaskNotificationDetailsID>" + splitedVal[3] + "</TaskNotificationDetailsID><GeneralTaskReportingDetailsID>" + splitedVal[4] + "</GeneralTaskReportingDetailsID><ApprovalStatus>1</ApprovalStatus><CentreCode>" + splitedVal[6] + "</CentreCode><StageSequenceNumber>" + splitedVal[7] + "</StageSequenceNumber><LeaveSessionID>" + 2 + "</LeaveSessionID></row>";

    //            }
    //            else if (POPendingRequest.ActionName == "Rejected") {
    //                ParameterXml = ParameterXml + "<row><ApplicationID> " + splitedVal[9] + "</ApplicationID><EmployeeID> " + EmployeeID + "</EmployeeID><TaskCode >" + splitedVal[8] + "</TaskCode ><IsLast>" + splitedVal[1] + "</IsLast><TaskNotificationMasterID>" + splitedVal[2] + "</TaskNotificationMasterID><TaskNotificationDetailsID>" + splitedVal[3] + "</TaskNotificationDetailsID><GeneralTaskReportingDetailsID>" + splitedVal[4] + "</GeneralTaskReportingDetailsID><ApprovalStatus>0</ApprovalStatus><CentreCode>" + splitedVal[6] + "</CentreCode><StageSequenceNumber>" + splitedVal[7] + "</StageSequenceNumber><LeaveSessionID>" + 2 + "</LeaveSessionID></row>";
    //            }
    //        }
    //    });

    //    POPendingRequest.XMLstring = ParameterXml + "</rows>";

    //},

};

