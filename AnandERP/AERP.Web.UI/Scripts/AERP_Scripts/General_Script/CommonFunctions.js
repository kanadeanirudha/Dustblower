var commonFunctions =
{
    getNotificationCount: function (event) {
        $('#NotiCountHeaderDiv').removeAttr('class');
        $.ajax({
            url: '/Home/GetNotificationCount',
            contentType: 'application/html; charset:utf-8',
            type: 'GET',
            dataType: 'JSON'
        }).success(function (result) {
            //if (result[0] && result[0].NotificationCount != 0) {
            if (result && result != 0) {
                //$("#NotiCount").html(result[0].NotificationCount);
                $("#NotiCount").html(result);
                $("#NotiCountHeader").show();
                //$("#NotiCountHeader").html(result[0].NotificationCount);
                $("#NotiCountHeader").html(result);
                $("#NotiCountHeaderDiv").addClass('animated tada');
            } else {
                $('#NotiCount').html(0);
                $('#NotiCountHeader').hide();
            }
        }).error(function () {

        })
    }
};