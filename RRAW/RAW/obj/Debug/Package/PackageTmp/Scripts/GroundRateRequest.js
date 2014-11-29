var currentUserId;
var currentUserType;
var currentRateRequestId;
var currentClientId;
var currentTransPortModeID;

$(document).ready(function () {

    $.each($("[id^='trDescription_']"), function (key, value) {
        //alert(value);
        $(this).fadeOut(100);
    });

    currentClientId = GetCookie("CurrentClientID");
    currentUserId = GetCookie("CurrentUserID");
    currentUserType = GetCookie("CurrentUserType");

    if (GetQueryString("RateRequestID") !== undefined) {
        currentRateRequestId = GetQueryString("RateRequestID")[0];
    }
    else {
        currentRateRequestId = 0;
    }

    //1(AIR), 2(Ocean), 3(Ground), 4(Other)
    currentTransPortModeID = GetQueryString("TransPortModeId")[0];
    SetDetails(currentClientId, currentRateRequestId, currentTransPortModeID, currentUserId, currentUserType);
    RightPanel_SetRateRequestID(currentRateRequestId);
    if (currentRateRequestId > 0) {

        $("#lblTitle").html("New Ground Rate Request (ID: " + currentRateRequestId + ")");

       // ShowNotification("Collecting rate request details...", "- Collecting rate request data...<br/>- Collecting authorization summary...<br/>- Collecting Attachments...", 5000);
        top.document.getElementById('processing_image').style.display = 'none';
    }
    else {
        $("#lblTitle").html("New Ground Rate Request");

        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
     //   ShowNotification("Data loaded successfully.", "- Continue creating new rate request.", 5000);
        top.document.getElementById('processing_image').style.display = 'none';
    }

});
function GetQueryString(key) {
    var re = new RegExp('(?:\\?|&)' + key + '=(.*?)(?=&|$)', 'gi');
    var r = [], m;
    while ((m = re.exec(document.location.search)) != null) r[r.length] = m[1];
    if (r.length > 0) {
        return r;
    }
    else {
        return undefined;
    }
}
function GetCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function ShowNotification(title, description, fadeOutAfter) {
    $("#divNotificationTitle").html(title);
    $("#divNotificationDescription").html(description);
    $("#divNotification").css("top", "154px");
    if ($("#divNotification").css("display") == "none") {
        $("#divNotification").fadeIn();
    }
    if (fadeOutAfter != undefined) {
        var t = setTimeout("$('#divNotification').fadeOut()", fadeOutAfter);
    }
}