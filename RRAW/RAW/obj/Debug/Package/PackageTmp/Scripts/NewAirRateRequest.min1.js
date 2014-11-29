/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />
/// <reference path="FormValidation.js" />
/// <reference path="../NewAirRateRequest.aspx" />


var errCode = -1;
var maxRateBasedOnIdValue = 9999;
var maxRateBasedOnId = maxRateBasedOnIdValue;
var ratesBasedOn;
var fileUploadComplete = false;
var callbackObject;
var maxFileUploadChecks = 60;
var fileUploadChecked = 0;
var fileUploadError = false;
var currentRateRequestId = 0;
var currentUserId = 0;
var currentUserType;
var currentRateRequestHolders;
var currentRateRequest;
var currentRateRequestGenerator;
var currentRateRequestApprover;
var uploadedFiles;
var oceanRateRequestAttachmentTypeID = 8;
var rateRequestAttachmentsServerPath = "DownloadFile.aspx?FileName=~/Attachments/RateRequestAttachment/";
var mastersLoadComplete = false;
var rateRequestJSON;
var existingRates;
var existingAddOnRates;
var collectedRates;
var collectedAddOnRates;
var isRateRequestUpdated;
var maxJSONCollectionCount = 2;
var rateRequestJSONCollectionCount = 0;
var previousCommentsJSONCollectionCount = 0;
var RateRequestHistoryJSONCollectionCount = 0;
var SimilarRateRequestsJSONCollectionCount = 0;
var mastersJSONCollectionCount = 0;
var SimilarTariffLanesJSONCollectionCount = 0;
var SimilarAdhocLanesJSONCollectionCount = 0;
var currentRateRequestLogs;
var NewAddOnRates;
var UserRegion;
//var CurrentAirTariffRates;
//var CurrentAirAddOnCharges;

//QuertString variables
var qStrOriginAirport;
var qStrOriginRegion;
var qStrDestAirport;
var qStrDestCity;
var qStrDestState;
var qStrDestCountry;
var qStrDestRegion;
var qStrDestZipcode;
var qStrCEVATransitMode;
var qStrShipMethod;
var qStrForwarderZipcode;
var qStrCustomClearanceMode;
var qStrServiceLevel;
var qStrForwarderService;
var qStrMinFreightRate;
var qStrFreightRate;
var qStrSecurityRate;
var qStrOtherCharges;
var qStrFreightForwarder;




$(document).ready(function () {

    $.each($("[id^='trDescription_']"), function (key, value) {
        //alert(value);
        $(this).fadeOut(100);
    });
    currentUserId = GetCookie("CurrentUserID");
    currentUserType = GetCookie("CurrentUserType");
    currentRateRequestId = GetQueryString("RateRequestID");

    if (currentRateRequestId > 0) {

        $("#lblTitle").html("New Air Rate Request 15<sup>th</sup><br /> July (ID: " + currentRateRequestId + ")");

        ShowNotification("Collecting rate request details...", "- Collecting rate request data...<br/>- Collecting authorization summary...<br/>- Collecting Attachments...", 5000);

        GetAllMasters();
        GetRateRequest();


        $("#trGuidelinesLineBreak").remove();
        $("#trGudelines").remove();
    }
    else {
        $("#lblTitle").html("New Air Rate Request");
        GetAllMasters();
        PrepareForNew();
        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
    }
    RRAW.AirRateRequests.CanUserViewBreakDownCharges(AuthenticationToken(), currentUserId, onSuccessOfCanUserViewBreakDownCharges, onFailureOfCanUserViewBreakDownCharges);

    //$("#tblBreakDownCharges input").validate({ rules: { field: { required: true, number: true}} });
    //    $("#tblBreakDownCharges input").validator.addMethod('Decimal', function (value, element) {
    //        return this.optional(element) || /^\d+(\.\d{0,2})?$/.test(value);
    //    }, "Please enter a correct number, format xxxx.xxx");

    AfterLoadAdjustments();
});

function onFailureOfCanUserViewBreakDownCharges(res) {
    window.location = "Errors.aspx?Operation=CanUserViewBreakDownCharges&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfCanUserViewBreakDownCharges(res) {
    if (res["CanUserViewBreakDownCharges"] == "No") {
        $("#trBradkDownChargesLineBreak").remove();
        $("#trBreakDownCharges").remove();
    }
}

function AfterLoadAdjustments() {
    try {
        top.document.getElementById('processing_image').style.display = 'none';
    }
    catch (e) {
    }

    isRateRequestUpdated = true;

    $('#hidCurrentDateTime').val(GetCurrentDate());
    $('#txtHAWBNumber').focus();

    // $("#txtRateValidDate").width($("#cmbRatesValidFor").width() - 1);

    var d = new Date();
    $("#lblRequestDate").text("Date: " + ((d.getMonth() + 1) < 10 ? "0" : "") + (d.getMonth() + 1) + "/" + (d.getDate() < 10 ? "0" : "") + d.getDate() + "/" + d.getFullYear());

    //$("#txtContainerNo").focus();
}


function GetAllMasters() {
    ShowNotification("Loading data...", "- Collecting master details...");

    RRAW.AirRateRequests.GetAllMasters(AuthenticationToken(), onSuccessOfGetAllMasters, onFailureOfGetAllMasters);
}

function onFailureOfGetAllMasters(res) {
    window.location = "Errors.aspx?Operation=GetAllMasters&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetAllMasters(res) {
    var masters = res;

    if (masters == undefined) {
        if (mastersJSONCollectionCount <= maxJSONCollectionCount) {
            mastersJSONCollectionCount++;
            GetAllMasters();
            return;
        }
    }

    onSuccessOfGetAllShipMethods(masters["GetAllShipMethods"]);
    onSuccessOfGetAllEuropianZone(masters["GetAllEuropianZone"]);

    LoadAirTariffRatesID(masters["AirTariffRates"]);

    mastersLoadComplete = true;

    if (currentRateRequestId == undefined) {
        ShowNotification("Data loaded successfully.", "- Continue creating new rate request.", 5000);

        $("#btnSave").fadeIn();
        $("#btnPostNewRateRequest").fadeIn();
        $("#btnBackToDashboard").fadeIn();
        //TestAssignments();
        AttachDatePickers();
    }
}

function AttachDatePickers() {
    $("#txtShipDate").datepicker({
        onSelect: function (dateText, inst) {
            $("#txtShipDate").focus();
        }
    });
}

function TestAssignments() {
    $("#txtShipDate").val("1/1/2011");
}

function onSuccessOfGetAllShipMethods(res) {
    if (Number(res) != NaN && (res == errCode || res == 0)) {
        DisplayError("Unknown error has occured. Please contact Administrator");
        return errCode;
    }

    var shipMethods = res;

    $("#cmbShipMethod").children().remove();

    $('#cmbShipMethod').append($("<option></option>").attr("value", "").text("Select"));
    $.each(shipMethods, function (key, val) {
        $('#cmbShipMethod').append($("<option></option>").attr("value", key).text(val));
    });
}

function onSuccessOfGetAllEuropianZone(res) {
    if (Number(res) != NaN && (res == errCode || res == 0)) {
        DisplayError("Unknown error has occured. Please contact Administrator");
        return errCode;
    }

    var EuropianZone = res;

    $("#cmbOriginEuropianZone").children().remove();
    $("#cmbDestEuropianZone").children().remove();

    $('#cmbOriginEuropianZone').append($("<option></option>").attr("value", "").text("Select"));
    $('#cmbDestEuropianZone').append($("<option></option>").attr("value", "").text("Select"));
    $.each(EuropianZone, function (key, val) {
        $('#cmbOriginEuropianZone').append($("<option></option>").attr("value", key).text(val));
        $('#cmbDestEuropianZone').append($("<option></option>").attr("value", key).text(val));
    });
}


function HideAllOperations() {
    $("#btnSave").fadeOut();
    $("#btnTransferToTariff").fadeOut();
    $("#btnPostComment").fadeOut();
    $("#btnApprove").fadeOut();
    $("#btnApproveAsAdhoc").fadeOut();
    $("#btnRevoke").fadeOut();
    $("#btnPostNewRateRequest").fadeOut();
    $("#btnBackToDashboard").fadeOut();
    $("#btnNeedToReviseRate").fadeOut();
    $("#btnSendBackToReviseRate").fadeOut();
    $("#btnReject").fadeOut();
    $("#btnDelete").fadeOut();
    $("#btnRemoveAllComments").fadeOut();
    $("#btnApprove_Permanent").fadeOut();
    $("#btnSendBackToRequestor").fadeOut();
}

function PrepareForNew() {
    qStrOriginAirport = GetQueryString("OriginAirport");
    qStrOriginRegion = GetQueryString("OriginRegion");
    qStrDestAirport = GetQueryString("DestAirport");
    qStrDestCity = GetQueryString("DestCity");
    qStrOriginEuropianZone = GetQueryString("OriginEuropianZone");
    qStrDestEuropianZone = GetQueryString("DestEuropianZone");
    //    qStrDestState = GetQueryString("DestState");
    //    qStrDestCountry = GetQueryString("DestCountry");
    qStrDestRegion = GetQueryString("DestRegion");
    qStrDestZipcode = GetQueryString("DestZipcode");
    //    qStrCEVATransitMode = GetQueryString("CEVATransitMode");
    qStrShipMethod = GetQueryString("ShipMethod");
    //    qStrForwarderZipcode = GetQueryString("ForwarderZipcode");
    //    qStrCustomClearanceMode = GetQueryString("CustomClearanceMode");
    qStrServiceLevel = GetQueryString("ServiceLevel");
    //    if (qStrServiceLevel != undefined) {
    //        qStrServiceLevel = qStrServiceLevel.substr(0, qStrServiceLevel.indexOf(" "));
    //    }

    //    qStrForwarderService = GetQueryString("ForwarderServiceLevel");
    //    if (qStrForwarderService != undefined) {
    //        qStrForwarderService = qStrForwarderService.substr(qStrForwarderService.indexOf(" ") + 1, qStrForwarderService.length - qStrForwarderService.indexOf(" ") + 1);
    //    }
    qStrMinFreightRate = GetQueryString("MinFreightRate");
    qStrFreightRate = GetQueryString("FreightRate");
    qStrCurrency = GetQueryString("Currency");
    qStrOtherCharges = GetQueryString("OtherCharges");
    //    qStrFreightForwarder = GetQueryString("FreightForwarder");

    var d = new Date();
    $("#lblRequestDate").text("Date: " + ((d.getMonth() + 1) < 10 ? "0" : "") + (d.getMonth() + 1) + "/" + (d.getDate() < 10 ? "0" : "") + d.getDate() + "/" + d.getFullYear());
    $("#txtOriginAirport").val(qStrOriginAirport);
    $("#txtDestAirport").val(qStrDestAirport);
    $("#txtDestCity").val(qStrDestCity);
    $("#txtDestZipCode").val(qStrDestZipcode);
    $("#txtMinFreightRate").val(qStrMinFreightRate);
    $("#txtFreightRate").val(qStrFreightRate);
    $("#txtOtherCharges").val(qStrOtherCharges);
    $("#cmbOriginRegion option:contains(" + qStrOriginRegion + ")").attr('selected', 'selected');
    $("#cmbDestRegion option:contains(" + qStrDestRegion + ")").attr('selected', 'selected');
    $("#cmbShipMethod option:contains(" + qStrShipMethod + ")").attr('selected', 'selected');
    $("#cmbServiceLevel option:contains(" + qStrServiceLevel + ")").attr('selected', 'selected');
    $("#cmbOriginEuropianZone option:contains(" + qStrOriginEuropianZone + ")").attr('selected', 'selected');
    $("#cmbDestEuropianZone option:contains(" + qStrDestEuropianZone + ")").attr('selected', 'selected');
    $("#cmbCurrency option:contains(" + qStrCurrency + ")").attr('selected', 'selected');
}

function GetRateRequest() {
    RRAW.AirRateRequests.GetRateRequest(AuthenticationToken(), String(currentRateRequestId), String(currentUserId), onSuccessOfGetRateRequest, onFailureOfGetRateRequest);
}

function onFailureOfGetRateRequest(res) {
    window.location = "Errors.aspx?Operation=GetRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}


function onSuccessOfGetRateRequest(res) {
    if (res != undefined) {
        rateRequestJSON = res;
    }
    if (rateRequestJSON == undefined) {
        if (rateRequestJSONCollectionCount <= maxJSONCollectionCount) {
            rateRequestJSONCollectionCount++;
            GetRateRequest();
            return;
        }
    }

    if (rateRequestJSON == undefined || rateRequestJSON["Error"] != undefined || rateRequestJSON["RateRequest"] == "Error" || rateRequestJSON["CurrentRateRequestGenerator"] == "Error") {
        ShowNotification("Collecting rate request failed", "- Please cofirm rate request id and reload the page or contact administrator.", 10000);
        return;
    }
    else {


        isRateRequestUpdated = false;
        currentRateRequest = rateRequestJSON["RateRequest"];

        if (mastersLoadComplete == false) {
            var t = setTimeout("onSuccessOfGetRateRequest()", 1000);
        }

        LoadRateRequest(currentRateRequest);
        existingRates = rateRequestJSON["AirTariffRates"];
        LoadAirTariffRates(existingRates)
        existingAddOnRates = rateRequestJSON["AirAddOnCharges"];

        if (existingAddOnRates != "Error") {
            $.each($("[id^='trAddOn_']"), function (key, value) {
                $(this).remove();
            });
        }

        LoadAirAddOnCharges(existingAddOnRates);

        if ($("#cmbOriginRegion option:selected").attr('value') == "EMEA") {
            $("#cmbOriginEuropianZone").attr("disabled", false);
        } else {
            $("#cmbOriginEuropianZone").attr("disabled", "disabled");
        }

        if ($("#cmbDestRegion option:selected").attr('value') == "EMEA") {
            $("#cmbDestEuropianZone").attr("disabled", false);
        } else {
            $("#cmbDestEuropianZone").attr("disabled", "disabled");
        }

        if (currentUserType != "Admin") {
            DisableFields();
        }

        currentRateRequestHolders = rateRequestJSON["CurrentRateRequestHolders"];

        currentRateRequestGenerator = rateRequestJSON["CurrentRateRequestGenerator"];

        currentRateRequestApprover = rateRequestJSON["CurrentRateRequestApprover"];

        currentRateRequestLogs = rateRequestJSON["RateRequestLogs"];

        PrepareAttachments(rateRequestJSON["Attachments"]);

        var dbServerDate = new Date(rateRequestJSON["DBServerDate"]);
        var effectiveDate = new Date(currentRateRequest.EffectiveDate);
        $("#pnlAttachments").hide();

        $("#btnApprove").hide();
        $("#btnApproveAsAdhoc").hide();
        $("#btnBackToDashboard").hide();
        $("#btnNeedToReviseRate").hide();
        $("#btnPostNewRateRequest").hide();
        $("#btnReject").hide();
        $("#btnRevoke").hide();
        $("#btnRemoveAllComments").hide();
        $("#btnSendBackToReviseRate").hide();
        $("#btnTransferToTariff").hide();
        $("#btnDelete").hide();
        $("#btnSave").hide();
        $("#btnPostComment").fadeIn();
        if (currentRateRequest.Approver == "") {

            $("#btnArchive").attr('value', 'Delete')

            if (isInArray(currentRateRequestHolders, currentUserId) == true) {
                $("#btnApprove").show();

                if (currentUserType == "Client") {
                    //$("#btnPostComment").fadeIn();
                    $("#btnApproveAsAdhoc").show();
                    $("#btnNeedToReviseRate").hide();
                    $("#btnReject").show();
                    $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
                    $("#pnlAttachments").show();
                }
                else {
                    if (currentUserId == 3) {
                        $("#btnBackToDashboard").hide();
                        $("#btnNeedToReviseRate").show();
                        $("#btnApprove_Permanent").show();
                        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
                        $("#pnlAttachments").show();
                    }
                    else {
                        $("#btnBackToDashboard").hide();
                        $("#btnNeedToReviseRate").show();
                        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
                        $("#pnlAttachments").show();
                    }
                }
            }
            if (currentUserType == "Admin") {
                $("#btnDelete").show();
                $("#btnBackToDashboard").hide();
                $("#btnRemoveAllComments").show();

                for (var i = 0; i < currentRateRequestHolders.length; i++) {

                    if (currentRateRequestHolders[i] == 3 || currentRateRequestHolders[i] == 11) {
                        $("#btnSendBackToRequestor").show();
                        break;
                    }
                    $("#btnNeedToReviseRate").show();

                }
                $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
                $("#pnlAttachments").show();
            }

            $("#txtComment").removeAttr("disabled");
            $("#btnPostComment").removeAttr("disabled");
            $("#lblStatus").html("Details for all fields are loaded successfully. You can add your comment(s) to this rate request.");
            $("#lblStatus").css("color", "#00f");

        }
        else {

            if (isInArray(currentRateRequestHolders, currentUserId) == true) {
                $("#btnApprove").fadeIn();

                if (currentUserType == "Admin") {
                    //$("#btnPostComment").fadeIn();
                    $("#btnDelete").show();
                    $("#btnBackToDashboard").hide();
                    $("#btnRemoveAllComments").show();

                    for (var i = 0; i < currentRateRequestHolders.length; i++) {

                        if (currentRateRequestHolders[i] == 3 || currentRateRequestHolders[i] == 11) {
                            $("#btnSendBackToRequestor").show();
                            break;
                        }
                        $("#btnNeedToReviseRate").show();

                    }
                }
                else {
                    //alert(currentUserId);
                    if (currentRateRequest.RequestorID != currentUserId) {
                        $("#btnSendBackToReviseRate").fadeIn();
                    }
                    $("#btnNeedToReviseRate").fadeIn();
                    if (currentUserId == 3) {
                        $("#btnApprove_Permanent").fadeIn();
                        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
                        $("#pnlAttachments").show();
                    }
                }
            }
            $("#txtComment").attr("disabled", "disabled");
            $("#btnPostComment").attr("disabled", "disabled");
            $("#lblStatus").html("This lane was approved by '" + currentRateRequest.Approver + "' on " + currentRateRequest.Approval_Date + " as effective from " + currentRateRequest.Effective_Date + " to " + currentRateRequest.Expiry_Date + ".");
            $("#lblStatus").css("color", "#00f");

        }
        $("#pnlPreviousComments").show();
        $("#pnlAdditionalInformation").show();
        $("#groupPreviousComments").show();
        GetPreviousComments();
        GetRateRequestHistory();
        GetSimilarRateRequests();
        GetSimilarTariffLanes();
        GetSimilarAdhocLanes();

        if (isInArray(currentRateRequestLogs, currentUserId) == true) {
            if ((currentUserType == "Client") && ((effectiveDate - dbServerDate) > 0)) {
                $("#btnRevoke").fadeIn();

            }
        }

    }
}

function isInArray(arr, expValue) {
    var res = false;

    $.each(arr, function (key, val) {
        if (val == Number(expValue)) {
            res = true;
        }
    });

    return res;
}

function isEmpty(obj) {
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop))
            return false;
    }

    return true;
}

function GetPreviousComments() {
    RRAW.AirRateRequests.GetPreviousComments(AuthenticationToken(), String(currentRateRequestId).toString(), onSuccessOfGetPreviousComments, onFailureOfGetPreviousComments);
}

function onFailureOfGetPreviousComments(res) {
    window.location = "Errors.aspx?Operation=GetPreviousComments&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetPreviousComments(res) {
    var previousComments = res["PreviousComments"];

    if (previousComments == undefined) {
        if (previousCommentsJSONCollectionCount <= maxJSONCollectionCount) {
            previousCommentsJSONCollectionCount++;
            GetPreviousComments();

            return;
        }
    }


    $("#divPreviousComments").html("");

    if (previousComments == "") {
        $("#divPreviousComments").html("No previous comments available.");
    }
    else {
        $.each(previousComments, function (key, val) {
            $("#divPreviousComments").append("<strong>Commented by " + val.CommentedBy + " on " + val.CommentDate + "</strong><br/>&nbsp;&nbsp;" + val.Comment + "<br/>---------------------<br/>");
        });
    }

    $("#divPreviousComments").fadeIn();
    ShowNotification("Previous comments Loaded Successfully", "- Data loaded.", 5000);

}



function GetRateRequestHistory() {
    RRAW.AirRateRequests.GetRateRequestHistory(AuthenticationToken(), String(currentRateRequestId).toString(), onSuccessOfGetRateRequestHistory, onFailureOfGetRateRequestHistory);
}

function onFailureOfGetRateRequestHistory(res) {
    window.location = "Errors.aspx?Operation=GetRateRequestHistory&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetRateRequestHistory(res) {
    var RateRequestHistory = res["RateRequestHistory"];

    if (RateRequestHistory == undefined) {
        if (RateRequestHistoryJSONCollectionCount <= maxJSONCollectionCount) {
            RateRequestHistoryJSONCollectionCount++;
            GetRateRequestHistory();
            return;
        }
    }


    $("#divRateRequestHistory").html("");

    if (RateRequestHistory == "" || RateRequestHistory == "Error") {
        $("#divRateRequestHistory").html("No previous changes available.");
    }
    else {
        $.each(RateRequestHistory, function (key, val) {
            var str = "<table cellpadding='5' cellspacing='0' class='BorderedTable'><tr><th></th><th> Min Freight Rate</th> <th> Freight Rate</th><th> Other Charges</th></tr><tr>" +
           "<th style='text-align: left'>Updated Rates </th><td align='right'>" + val.MinFreightRate + "</td><td align='right'>" + val.FreightRate +
           "</td><td align='right'>" + val.OtherCharges + "</td></tr>" +
           "<th style='text-align: left'>Old Rates </th><td align='right'>" + val.MinFreightRate_Old + "</td><td align='right'>" + val.FreightRate_Old +
           "</td><td align='right'>" + val.OtherCharges_Old + "</td></tr></table>-----------------------------------------------------------------------------------------------------<br /><br />";

            $("#divRateRequestHistory").append("<strong> " + val.UpdatorName + " has updated rates on " + val.UpdateDate + "</strong><br/>&nbsp;&nbsp;" + val.Comment + "<br/>---------------------<br/>" + str);

        });
    }
    $("#pnlCapture").hide();
    $("#divRateRequestHistory").fadeIn();
    //$("#divAdditionalInformation").fadeIn();

    ShowNotification("Rate Request History Loaded Successfully", "- Data loaded.", 5000);
}


function GetSimilarRateRequests() {
    RRAW.AirRateRequests.GetSimilarRateRequests(AuthenticationToken(), String(currentRateRequestId).toString(), onSuccessOfGetSimilarRateRequests, onFailureOfGetSimilarRateRequests);
}

function onFailureOfGetSimilarRateRequests(res) {
    window.location = "Errors.aspx?Operation=GetSimilarRateRequests&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetSimilarRateRequests(res) {
    var SimilarRateRequests = res["SimilarRateRequests"];

    if (SimilarRateRequests == undefined) {
        if (SimilarRateRequestsJSONCollectionCount <= maxJSONCollectionCount) {
            SimilarRateRequestsJSONCollectionCount++;
            GetSimilarRateRequests();
            return;
        }
    }


    $("#divSimilarRateRequests").html("");


    if (SimilarRateRequests == "" || SimilarRateRequests == "Error") {
        $("#divSimilarRateRequests").html("No Similar Rate Request Available");
    }
    else {
        var strHeader = "";
        strHeader = "<table cellpadding='5' cellspacing='0' class='SimilarLanes groupContainer' border='1' style='border-collapse:collapse;'><tr><th> RateRequestID </th><th> Consignee Name</th> <th> Chargeable Weight</th> <th> Minimum Freight Rate</th> <th> Freight Rate (Per K.g.)</th><th> Current Approver</th></tr>";
        $.each(SimilarRateRequests, function (key, val) {
            var strData = "<tr>" +
           "<td style='text-align: left'><a href=AirRateRequest.aspx?RateRequestID=" + val.RateRequestID.toString() + ">" + val.RateRequestID.toString() +
           "</a></td><td style='text-align: left'>" + val["ConsigneeName"] + "</td><td align='right'>" + val["ChargeableWeight"] + "</td><td align='right'>" + val["MinimumFreightRate"] +
           "</td><td align='right'>" + val["FreightRate(PerK.g.)"] +
           "</td><td>" + val["CurrentApprover"] + "</td></tr>";
            strHeader += strData;
        });
        strHeader += "</table";
        $("#divSimilarRateRequests").append(strHeader);

    }
    $("#pnlCapture").hide();
    $("#divAdditionalInformation").fadeIn();
    $("#divSimilarRateRequests").fadeIn();

    ShowNotification("Similar Rate Request Loaded Successfully", "- Data loaded.", 5000);
}


function GetSimilarTariffLanes() {
    RRAW.AirRateRequests.GetSimilarTariffLanes(AuthenticationToken(), String(currentRateRequestId).toString(), onSuccessOfGetSimilarTariffLanes, onFailureOfGetSimilarTariffLanes);
}

function onFailureOfGetSimilarTariffLanes(res) {
    window.location = "Errors.aspx?Operation=GetSimilarTariffLanes&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetSimilarTariffLanes(res) {
    var SimilarTariffLanes = res["SimilarTariffLanes"];
    if (SimilarTariffLanes == undefined) {
        if (SimilarTariffLanesJSONCollectionCount <= maxJSONCollectionCount) {
            SimilarTariffLanesJSONCollectionCount++;
            GetSimilarTariffLanes();
            return;
        }
    }
    $("#divSimilarTariffLanes").html("");


    if (SimilarTariffLanes == "" || SimilarTariffLanes == "Error") {
        $("#divSimilarTariffLanes").html("No Similar Tariff Lane Available");
    }
    else {
        var strHeader = "";
        strHeader = "<table cellpadding='5' cellspacing='0' class='SimilarLanes groupContainer' border='1' style='border-collapse:collapse;'><tr><th>ID </th><th> Consignee Name</th><th> Chargeable Weight</th><th> Minimum Freight Rate</th><th> Freight Rate (Per K.g.)</th><th> Current Approver</th></tr>";
        $.each(SimilarTariffLanes, function (key, val) {
            var strData = "<tr>" +
           "<td style='text-align: left'><a href=\"Tariff_New.aspx?OriginAirportCode=" + val.OriginAirportCode + "&DestinationAirportCode=" + val.Destination
           + "&MinFreightRate=" + val["MinimumFreightRate"] + "&FreightRatePerKG=" + val["FreightRate(PerK.g.)"] + "\">" + val.RateRequestID +
             "</a></td><td align='right'>" + val["ChargeableWeight"] +
             "</td><td align='right'>" + val["MinimumFreightRate"] + "</td><td align='right'>" + val["FreightRate(PerK.g.)"] +
          "</td><td>" + val["CurrentApprover"] + "</td></tr>";
            strHeader += strData;
        });
        strHeader += "</table";
        $("#divSimilarTariffLanes").append(strHeader);

    }
    $("#pnlCapture").hide();
    $("#divSimilarTariffLanes").fadeIn();
    $("#divAdditionalInformation").fadeIn();
    ShowNotification("Similar Tariff Lanes Loaded Successfully", "- Data loaded.", 5000);
}



function GetSimilarAdhocLanes() {
    RRAW.AirRateRequests.GetSimilarAdhocLanes(AuthenticationToken(), String(currentRateRequestId).toString(), onSuccessOfGetSimilarAdhocLanes, onFailureOfGetSimilarAdhocLanes);
}

function onFailureOfGetSimilarAdhocLanes(res) {
    window.location = "Errors.aspx?Operation=GetSimilarAdhocLanes&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetSimilarAdhocLanes(res) {
    var SimilarAdhocLanes = res["SimilarAdhocLanes"];

    if (SimilarAdhocLanes == undefined) {
        if (SimilarAdhocLanesJSONCollectionCount <= maxJSONCollectionCount) {
            SimilarAdhocLanesJSONCollectionCount++;
            GetSimilarAdhocLanes();
            return;
        }
    }


    $("#divSimilarAdhocLanes").html("");


    if (SimilarAdhocLanes == "" || SimilarAdhocLanes == "Error") {
        $("#divSimilarAdhocLanes").html("No Similar Approved As Adhoc Lane Available");
    }
    else {
        var strHeader = "";
        strHeader = "<table cellpadding='5' cellspacing='0' class='SimilarLanes groupContainer' border='1' style='border-collapse:collapse;'><tr><th>ID </th><th> Consignee Name</th><th> Chargeable Weight</th><th> Minimum Freight Rate</th><th> Freight Rate (Per K.g.)</th><th> Current Approver</th></tr>";
        $("#divSimilarAdhocLanes").append(strHeader);
        $.each(SimilarAdhocLanes, function (key, val) {
            var strData = "<tr>" +
           "<td style='text-align: left'><a href=\"ApprovedAsAdhoc.aspx?Customer=" + val["ConsigneeName"] + "&OriginAirport=" + val.OriginAirport + "&DestAirport=" + val.DestAirport
           + "&DestCity=" + val.DestCity + "&ShipMethod=" + val.ShipMethod +
            "&MinFreightRate=" + val["MinimumFreightRate"] + "&FreightRate=" + val["FreightRate(PerK.g.)"] + "\">" + val.RateRequestID +
             "</a></td><td style='text-align: left'>" + val["ConsigneeName"] + "</td><td align='right'>" + val["ChargeableWeight"] +
             "</td><td align='right'>" + val["MinimumFreightRate"] + "</td><td align='right'>" + val["FreightRate(PerK.g.)"] +
           "</td><td>" + val["CurrentApprover"] + "</td></tr>";
            strHeader += strData;
        });
        strHeader += "</table";
        $("#divSimilarAdhocLanes").append(strHeader);
    }
    $("#pnlCapture").hide();
    $("#divSimilarAdhocLanes").fadeIn();
    $("#divAdditionalInformation").fadeIn();

    ShowNotification("Similar Adhoc Lanes Loaded Successfully", "- Data loaded.", 5000);
}

function LoadAirTariffRates(AirTariffRates) {
    var MinValue = "";
    var PerKg = "";
    var JsonValue;
    $.each($("[id$='_Name']"), function (key, val) {

        MinValue = this.id.split("_")[0] + "_Min";
        PerKg = this.id.split("_")[0] + "_PerKG";

        JsonValue = AirTariffRates[$("#" + this.id + " span").html()];
        //$(this).attr("id", this.id.split("_")[0] + "_" + JsonValue.ID + "_" + this.id.split("_")[2]);
        if (JsonValue != undefined) {

            $("#" + MinValue).val(JsonValue.MinValue);
            $("#" + PerKg).val(JsonValue.PerKG);
        }
    });
}

function LoadAirTariffRatesID(AirTariffRates) {

    $.each($("[id$='_Name']"), function (key, val) {
        $(this).attr("id", this.id.split("_")[0] + "_" + AirTariffRates[$("#" + this.id + " span").html()].ID + "_" + this.id.split("_")[1]);
    });
}

function LoadAirAddOnCharges(AirAddOnCharges) {
    var trRowID = "";
    var PartID = "";

    if (AirAddOnCharges != "Error") {
        $.each(AirAddOnCharges, function (key, value) {
            trRowID = AppendBlankAirAddOnCharges(key);
            PartID = trRowID.split("_")[1] + "_" + trRowID.split("_")[2];
            $("#txtDetails_" + PartID).val(value.Description);
            $("#cmbRateBasedOn_" + PartID + " option:contains(" + value.UOM + ")").attr('selected', 'selected');
            $("#txtAddOnFee_" + PartID).val(value.Value);
            //$("#" + trRowID).attr("id", "trAddOn_" + key);
        });
    }
}





/* Get Rate Request Over */

function CollectRates() {
    var Rates = new Array();
    var PreID;
    var NewRateCount = 0;
    var dbID;
    $.each($("[id$='_Name']"), function (key, item) {
        PreID = this.id.split("_")[0];
        dbID = this.id.split("_")[1];
        if (($("#" + PreID + "_Min").val().length > 0 && $("#" + PreID + "_PerKG").val().length > 0) || ($("#" + PreID + "_Min").val().length == 0 && $("#" + PreID + "_PerKG").val().length == 0)) {
            Rates[NewRateCount] = new Array();
            Rates[NewRateCount][0] = dbID;
            Rates[NewRateCount][1] = $("#" + PreID + "_Min").val();
            Rates[NewRateCount][2] = $("#" + PreID + "_PerKG").val();
            NewRateCount++;
        }
        else {
            alert($("#" + this.id + " span").html() + " have no value should be blank.");
        }
    });
    return Rates;
}

function ValidateRates() {
    var Rates = new Array();
    var PreID;
    var NewRateCount = 0;
    var dbID;
    $.each($("[id$='_Name']"), function (key, item) {
        PreID = this.id.split("_")[0];
        dbID = this.id.split("_")[1];
        if (($("#" + PreID + "_Min").val().length > 0 && $("#" + PreID + "_PerKG").val().length == 0) || ($("#" + PreID + "_Min").val().length == 0 && $("#" + PreID + "_PerKG").val().length > 0)) {
            NewRateCount = 1;
            alert($("#" + this.id + " span").html() + " have no value should be blank.");
        }
    });

    if (NewRateCount == 0) {
        return true;
    }
    else {
        return false;
    }
}

function CollectAddOnRates() {
    var rates = new Array();
    //var id = 0;
    var NewRateCount = 0;
    NewAddOnRates = new Array();
    var PartID;
    var ID;
    var PostFix;
    $.each($("[id^='trAddOn_']"), function (key, item) {
        PartID = this.id.split("_")[1] + "_" + this.id.split("_")[2];
        ID = this.id.split("_")[1];
        PostFix = this.id.split("_")[2];

        if (($("#txtDetails_" + PartID).val().length > 0) || ($("#cmbRateBasedOn_" + PartID).val().length > 0) || ($("#txtAddOnFee_" + PartID).val().length > 0)) {

            if (PostFix == "NewAddOnCharges") {
                NewAddOnRates[NewRateCount] = new Array();
                NewAddOnRates[NewRateCount][0] = $("#txtDetails_" + PartID).val()
                NewAddOnRates[NewRateCount][1] = $("#cmbRateBasedOn_" + PartID).val()
                NewAddOnRates[NewRateCount][2] = $("#txtAddOnFee_" + PartID).val()
                NewRateCount++;
            }
            else {
                rates[ID] = new Array();
                rates[ID][0] = $("#txtDetails_" + PartID).val()
                rates[ID][1] = $("#cmbRateBasedOn_" + PartID).val()
                rates[ID][2] = $("#txtAddOnFee_" + PartID).val()

            }
        }
    });

    return rates;
}

//function NewAddOnRates() {
//    if (existingAddOnRates == undefined) {
//        return CollectAddOnRates();
//    }
//    else {
//        var newAddOnRates = new Array();
//        var i = 0;

//        if (collectedAddOnRates == undefined) {
//            collectedAddOnRates = CollectAddOnRates();
//        }

//        $.each(collectedAddOnRates, function (key, val) {
//            if (val != undefined) {
//                if (existingAddOnRates[key] == undefined) {
//                    newAddOnRates[i] = val;
//                    i++;
//                }
//            }
//        });
//    }

//    return newAddOnRates;
//}

function UpdatedAddOnRates() {
    var updatedAddOnRates = new Array();
    var i = 0;

    if (collectedAddOnRates == undefined) {
        collectedAddOnRates = CollectAddOnRates();
    }

    $.each(collectedAddOnRates, function (key, val) {
        if (val != undefined) {
            var currentExistingAddOnRate = existingAddOnRates[key];
            if (currentExistingAddOnRate != undefined) {
                if ((val[0] != currentExistingAddOnRate["Description"]) || (val[1] != currentExistingAddOnRate["UOM"]) || (val[2] != currentExistingAddOnRate["Value"])) {
                    updatedAddOnRates[i] = new Array();

                    updatedAddOnRates[i][0] = key;
                    updatedAddOnRates[i][1] = val[0];
                    updatedAddOnRates[i][2] = val[1];
                    updatedAddOnRates[i][3] = val[2];
                    //                    updatedRates[i][4] = val[3];
                    //                    updatedRates[i][5] = val[4];
                    //                    updatedRates[i][6] = val[5];

                    i++;
                }
            }
        }
    });

    return updatedAddOnRates;
}

function RemovedAddOnRates() {
    var removedAddOnRates = new Array();

    if (collectedAddOnRates == undefined) {
        collectedAddOnRates = CollectAddOnRates();
    }

    if (existingAddOnRates != undefined) {
        $.each(existingAddOnRates, function (key, val) {
            if (val != undefined) {
                if (collectedAddOnRates[key] == undefined) {
                    removedAddOnRates.push(key);
                }
            }
        });
    }
    return removedAddOnRates;
}

function LoadRateRequest(rateRequest) {
    $("#lblRequestDate").val(rateRequest["Request Date"]);
    $("#txtHAWBNumber").val(rateRequest["HAWB Number"]);
    $("#cmbShipMethod option:contains(" + rateRequest["Ship Method"] + ")").attr('selected', 'selected');
    var ShipDate = GetDate(rateRequest["Ship Date"]) == "01/01/1900" ? "" : GetDate(rateRequest["Ship Date"]);
    $("#txtShipDate").val(ShipDate);
    $("#cmbServiceLevel option").filter(function () { return $(this).text() == rateRequest["Requested Service Level"]; }).attr('selected', 'selected');
    $("#txtWeight").val(rateRequest["Weight"]);
    $("#txtShipperName").val(rateRequest["Shipper Name"]);
    $("#txtOriginAirport").val(rateRequest["Origin Airport"]);
    $("#txtOriginCity").val(rateRequest["Origin City"]);
    $("#cmbOriginRegion option:contains(" + rateRequest["Origin Region"] + ")").attr('selected', 'selected');
    $("#txtOriginZipCode").val(rateRequest["Origin Zipcode"]);
    $("#txtConsigneeName").val(rateRequest["Consignee Name"]);
    $("#txtDestAirport").val(rateRequest["Destination Airport"]);
    $("#txtDestCity").val(rateRequest["Destination City"]);
    $("#cmbDestRegion option:contains(" + rateRequest["Destination Region"] + ")").attr('selected', 'selected');
    $("#txtDestZipCode").val(rateRequest["Destination Zipcode"]);
    $("#txtRateDeterMethodology").val(rateRequest["Rate Determination Method"]);
    $("#txtMinFreightRate,#txtFinalMin").val(rateRequest["Min Freight Rate"]);
    $("#txtFreightRate,#txtFinalPerKG").val(rateRequest["Freight Rate"]);
    $("#txtOtherCharges").val(rateRequest["Other Charges"]);
    $("#txtTotalAddOnFees").val(rateRequest["GrossAddOnCharges"]);
    $("#cmbOriginEuropianZone option:contains(" + rateRequest["Origin European Zones"] + ")").attr('selected', 'selected');
    $("#cmbDestEuropianZone option:contains(" + rateRequest["Destinition European Zones"] + ")").attr('selected', 'selected');
    $("#cmbCurrency option:contains(" + rateRequest["Currency"] + ")").attr('selected', 'selected');
    $("#cmbTransportMode option:[text='" + rateRequest["TransportMode"] + "']").attr('selected', 'selected');


}

function DisableFields() {
    $("#txtHAWBNumber").attr("disabled", "disabled");
    $("#txtShipDate").attr("disabled", "disabled");
    $("#txtWeight").attr("disabled", "disabled");
    $("#txtShipperName").attr("disabled", "disabled");
    $("#txtOriginCity").attr("disabled", "disabled");
    $("#txtOriginAirport").attr("disabled", "disabled");
    $("#txtOriginZipCode").attr("disabled", "disabled");
    $("#cmbOriginRegion").attr("disabled", "disabled");
    $("#txtConsigneeName").attr("disabled", "disabled");
    $("#txtDestCity").attr("disabled", "disabled");
    $("#txtDestAirport").attr("disabled", "disabled");
    $("#txtDestZipCode").attr("disabled", "disabled");
    $("#cmbDestRegion").attr("disabled", "disabled");
    $("#txtMinFreightRate").attr("disabled", "disabled");
    $("#txtFreightRate").attr("disabled", "disabled");
    $("#txtRateDeterMethodology").attr("disabled", "disabled");
    $("#txtOtherCharges").attr("disabled", "disabled");
    $("#cmbServiceLevel").attr("disabled", "disabled");
    $("#cmbShipMethod").attr("disabled", "disabled");
    $("#pnlCapture").attr("display", "none"); //.hide();
    $("#pnlAttachments").hide();

    $("#cmbOriginEuropianZone").attr("disabled", "disabled");
    $("#cmbDestEuropianZone").attr("disabled", "disabled");
    $("#cmbCurrency").attr("disabled", "disabled");
    $.each($("#tblBreakDownCharges input"), function (key, val) { $(this).attr("disabled", "disabled") });
    $.each($("#tblAddOnDetailCharges input,select,td"), function (key, val) { $(this).attr("disabled", "disabled"); $(this).removeClass("deleteIcon"); });
    $("#txtTotalAddOnFees").attr("disabled", "disabled");
    $("#divAddMoreCharges").fadeOut();

}


function EnableFields() {
    $("#txtHAWBNumber").removeAttr("disabled");
    $("#txtShipDate").removeAttr("disabled");
    $("#txtWeight").removeAttr("disabled");
    $("#txtShipperName").removeAttr("disabled");
    $("#txtOriginCity").removeAttr("disabled");
    $("#txtOriginAirport").removeAttr("disabled");
    $("#txtOriginZipCode").removeAttr("disabled");
    $("#cmbOriginRegion").removeAttr("disabled");
    $("#txtConsigneeName").removeAttr("disabled");
    $("#txtDestCity").removeAttr("disabled");
    $("#txtDestAirport").removeAttr("disabled");
    $("#txtDestZipCode").removeAttr("disabled");
    $("#cmbDestRegion").removeAttr("disabled");
    $("#txtMinFreightRate").removeAttr("disabled");
    $("#txtFreightRate").removeAttr("disabled");
    $("#txtRateDeterMethodology").removeAttr("disabled");
    $("#txtOtherCharges").removeAttr("disabled");
    $("#cmbServiceLevel").removeAttr("disabled");
    $("#cmbShipMethod").removeAttr("disabled");
    $("#pnlAttachments").show();

//    $("#cmbOriginEuropianZone").removeAttr("disabled");
//    $("#cmbDestEuropianZone").removeAttr("disabled");
    $("#cmbCurrency").removeAttr("disabled");

    $.each($("[id^='chkAttachment_']"), function (key, item) {
        item.attr('checked', true);

        $($("#UploadFileIframe").contents().find("input[type='file']")).removeAttr("disabled");
    })

    $.each($("#tblBreakDownCharges input"), function (key, val) { $(this).removeAttr("disabled"); });
    $.each($("#tblAddOnDetailCharges input,select,td"), function (key, val) {
        $(this).removeAttr("disabled");
        if (this.id.indexOf("AddOnCharges") > 0 && this.id.indexOf("td_") == 0) {
            $(this).addClass("deleteIcon");
        }
    });
    $("#txtTotalAddOnFees").removeAttr("disabled");
    $("#divAddMoreCharges").fadeIn();

    if ($("#cmbOriginRegion option:selected").attr('value') == "EMEA") {
        $("#cmbOriginEuropianZone").attr("disabled", false);
    } else {
        $("#cmbOriginEuropianZone").attr("disabled", "disabled");
    }

    if ($("#cmbDestRegion option:selected").attr('value') == "EMEA") {
        $("#cmbDestEuropianZone").attr("disabled", false);
    } else {
        $("#cmbDestEuropianZone").attr("disabled", "disabled");
    }
}

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

/* File Upload */
function CheckFileUploadProgress() {
    fileUploadChecked++;

    if (fileUploadChecked > maxFileUploadChecks) {
        fileUploadComplete = true;
        fileUploadError = true;
        $(callbackObject).click();

        return;
    }

    var res;

    try {
        //Tested with IE6, IE7, IE8, Chrome12 & FF5
        //res = $("#UploadFileIframe").contents().find("PRE").html();
        //res = res.replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&quot;/g, "\"").replace(/&gt/g, ">").replace("<pre>", "").replace("</pre>", "");
        //res = $.parseJSON(res);

        res = $("#UploadFileIframe").contents().find("PRE").html().replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&quot;/g, "\"").replace(/&gt/g, ">").replace(/\\/g, "\\\\")
        if (res.indexOf("<pre>") >= 0) {
            res = res.substr(res.indexOf("<pre>") + 5, res.indexOf("</pre>") - 5);
        }
        res = eval(res);
        if (res == null) {
            var t = setTimeout("CheckFileUploadProgress()", 2000);

            return;
        }
        else {
            uploadedFiles = res;
            //$("#UploadFileIframe").contents().children().remove();
            $("#UploadFileIframe").hide();
        }
    }
    catch (e) {
        //alert("Error callback");
        var t = setTimeout("CheckFileUploadProgress()", 2000);

        return;
    }
    fileUploadComplete = true;
    $(callbackObject).click();
}


function UploadFiles() {
    if (fileUploadComplete == false) {
        $("#UploadFileIframe").contents().find("#btnUpload").click();

        var t = setTimeout("CheckFileUploadProgress()", 1000);

        return false;
    }

    if (fileUploadError == true) {
        alert("Error Occured. Please try again.");

        return false;
    }
}
/* File Upload over*/

function AddValidationClass(ids) {
    if ($("#" + ids).val() == "") {
        //$("#cmbServiceLevel option:selected").attr('value') == "Select"
        $("#" + ids).addClass("validateRequired");
    }
    else {
        $("#" + ids).removeClass("validateRequired");
    }
}

function AddValidations() {
    AddValidationClass('cmbServiceLevel');
    AddValidationClass('txtShipperName');
    AddValidationClass('txtOriginAirport');
    AddValidationClass('txtOriginCity');
    AddValidationClass('cmbOriginRegion');
    AddValidationClass('txtOriginZipCode');
    AddValidationClass('txtConsigneeName');
    AddValidationClass('txtDestAirport');
    AddValidationClass('txtDestCity');
    AddValidationClass('cmbDestRegion');
    AddValidationClass('txtDestZipCode');
    AddValidationClass('txtMinFreightRate');
    AddValidationClass('txtFreightRate');
    AddValidationClass('cmbCurrency');
}

function PrepareAttachments(attachments) {
    $("#divCurrentAttachments").children().remove();

    if (isEmpty(attachments)) {
        $("#divCurrentAttachments").html("No attachment available.");
    }
    else {
        $.each(attachments, function (key, val) {
            $("#divCurrentAttachments").append("<a href='" + rateRequestAttachmentsServerPath + val[1] + "'>" + val[1] + "</a><br/>");
        });
    }
}

function AppendBlankAirAddOnCharges(rateId) {
    if (rateId == 0) {
        rateId = ($("tr[id^='trAddOn_']").length + 1) + "_NewAddOnCharges";
    }
    else {
        rateId = rateId + "_OldAddOnCharges";
    }
    var NewTr = "<tr id='trAddOn_" + rateId + "'><td><input type='text' style='color: #B22222;background-color: #92D050'   id='txtDetails_" + rateId + "'/></td>" +
          "<td><select style='width: 90px; height: 20px;color: #B22222;background-color: #92D050' id='cmbRateBasedOn_" + rateId + "'><option value=''>Select</option><option value='Per Shipment'>Per Shipment</option><option value='Per Kilo'>Per Kilo</option></select></td>" +
          "<td><input type='text'  style='color: #B22222;background-color: #92D050' class='ValueField number' id='txtAddOnFee_" + rateId + "'/></td><td class='deleteIcon' id='td_" + rateId + "'></td></tr>";
    $("#tblAddOnDetailCharges").append(NewTr);
    return "trAddOn_" + rateId;
}

function validateDecimal(controlToValidate, e) {
    //alert(String.fromCharCode(e.keyCode)+"="+ e.keyCode);
    //alert($(controlToValidate).val().indexOf("."));
    if ((e.keyCode < 37 || e.keyCode > 40) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode != 8 && e.keyCode != 9 && e.keyCode != 46 && e.keyCode != 116 && (e.keyCode < 48 || e.keyCode > 57)) {
        if (e.keyCode == 190 || e.keyCode==110) {
            if ($(controlToValidate).val().indexOf(".") > -1) {
                e.preventDefault();
            }
        }
        else {
            e.preventDefault();
        }
    }
    //alert(". Index=" + $(controlToValidate).val().indexOf(".") +"<br /> Caret Pos=" + controlToValidate.selectionStart);
    if (($(controlToValidate).val() == "" || ($(controlToValidate).val().indexOf(".") == -1 && controlToValidate.selectionStart == 0)) && (e.keyCode == 190 || e.keyCode == 110)) {

        $(controlToValidate).val("0." + $(controlToValidate).val());
        controlToValidate.selectionStart = 2;
        controlToValidate.selectionEnd = 2;
        //controlToValidate.select(controlToValidate.Text.length, 0);
        e.preventDefault();
    }
}


function validateDecimal1(controlToValidate, e) {
    //alert(String.fromCharCode(e.keyCode) + "=" + e.keyCode);
    if ((e.keyCode < 37 || e.keyCode > 40) && e.keyCode != 8 && e.keyCode != 9 && e.keyCode != 46 && (e.keyCode < 48 || e.keyCode > 57)) {
        if (e.keyCode == 190) {
            if ($(controlToValidate).val().indexOf(".") > -1) {
                e.preventDefault();
            }
        }
        else {
            e.preventDefault();
        }
    }
    if ($(controlToValidate).val() == "" && e.keyCode == 190) {
        $(controlToValidate).val("0.");
        e.preventDefault();
    }
}
////*************************************************Button events**************************************************
$('#tblBreakDownCharges input[type=text]').blur(function () {
    var MinSum = 0.0;
    //alert("Working");
    $.each($("[id$='_Min']"), function (key, value) {
        if (this.id != "txtFSC_Min") {
            MinSum += parseFloat(this.value == "" ? 0 : this.value);
        }
    });
    //alert(MinSum);
    $("#txtFinalMin,#txtMinFreightRate").val(MinSum.toFixed(2));


    MinSum = 0.0;
    $.each($("[id$='_PerKG']"), function (key, value) {
        if (this.id != "txtFSC_PerKG") {
            MinSum += parseFloat(this.value == "" ? 0 : this.value);
        }
    });
    $("#txtFinalPerKG,#txtFreightRate").val(MinSum.toFixed(2));

});

$('#tblBreakDownCharges input[type=text]').live("keydown", function (e) {
    validateDecimal(this, e);
});

$("[id^='txtAddOnFee_']").live("keydown", function (e) {
    validateDecimal(this, e);
});

$("input[type='text']").live("blur", function (e) {
    this.value = this.value.toUpperCase();
});

$('#txtWeight').live("keydown", function (e) {
    validateDecimal(this, e);
});

$("#divAddMoreCharges").click(function () {
    //alert($("tr[id^='trAddOn_']").length);
    AppendBlankAirAddOnCharges(0);
    ChangeAddOnChargesBackColor();
});

function ChangeAddOnChargesBackColor()
{
if($("tr[id^='trAddOn_']").length==0)
    {
    $("#tdAddOnHeader1,#tdAddOnHeader2").attr("style","background-color: Transperant");
    }
    else
    {
    $("#tdAddOnHeader1,#tdAddOnHeader2").attr("style","background-color: #FFA500");
    }
}

$("td.deleteIcon").live("click", function () {
    //alert(this.id.replace("td", "trAddOn"));
    $("#" + this.id.replace("td", "trAddOn")).remove();
    
    ChangeAddOnChargesBackColor();
    
    CalculateAddOnTotal();
});

$("[id^='txtAddOnFee_']").live("blur", function () {
    if ($(this).val() != "") {
        this.value = parseFloat(this.value).toFixed(2);
    }
    CalculateAddOnTotal();
});
$("[id^='txtAddOnFee_']").live("focus", function () {
    //CalculateAddOnTotal();
    if ($(this).val() == "NaN")
        $(this).val("");
});

$("[id^='dvExpand_']").click(function () {
    //$.fadeToggle();
    var Ids = this.id.replace("dvExpand_", "trDescription_");
    //var imgDv = this.id.replace("trRules_", "dvExpand_");
    //$(this).is(":visible")
    if ($("#" + Ids).is(":visible")) {
        $("#" + Ids).hide();
        $(this).addClass("ShowDescription");
        $(this).removeClass("HideDescription");
        $(this).prev().removeClass("SelectedRules");
        $(this).attr("title", "Click to view description");
    }
    else {
        $("#" + Ids).show();
        $(this).addClass("HideDescription");
        $(this).removeClass("ShowDescription");
        $(this).prev().addClass("SelectedRules");
        $(this).attr("title", "Click to hide description");
    }
});

function CalculateAddOnTotal() {
    var AddOnFees = 0.0
    //alert(AddOnFees);
    $.each($("[id^='txtAddOnFee_']"), function (key, value) {
        AddOnFees += parseFloat(this.value == "" ? 0 : this.value);
    });
    $("#txtTotalAddOnFees").val(AddOnFees.toFixed(2));
}

$('#txtOriginAirport').blur(function () {
    //alert('Handler for .blur() called.');
    //    If (OriginCode = "BKK" Or OriginCode = "BKC") Then
    //            cmbCurrency.SelectedValue = "THB"
    //        ElseIf (OriginCode = "KUL" Or OriginCode = "PEN" Or OriginCode = "JHB") Then
    //            cmbCurrency.SelectedValue = "MYR"
    //        Else
    //            cmbCurrency.SelectedValue = "USD"
    //        End If

    if ($('#txtOriginAirport').val().toUpperCase() == "BKK" || $('#txtOriginAirport').val().toUpperCase() == "BKC") {
        $("#cmbCurrency option:contains('THB')").attr('selected', 'selected');
    }
    else if ($('#txtOriginAirport').val().toUpperCase() == "KUL" || $('#txtOriginAirport').val().toUpperCase() == "PEN" || $('#txtOriginAirport').val().toUpperCase() == "JHB") {
        $("#cmbCurrency option:contains('MYR')").attr('selected', 'selected');
    }
    else {
        $("#cmbCurrency option:contains('USD')").attr('selected', 'selected');
    }
});

$("#cmbOriginRegion").change(function () {
    //alert($("#cmbOriginRegion option:selected").attr('value'));
    if ($("#cmbOriginRegion option:selected").attr('value') == "EMEA") {
        $("#cmbOriginEuropianZone").attr("disabled", false);
    } else {
        $("#cmbOriginEuropianZone").attr("disabled", "disabled");
        $("#cmbOriginEuropianZone option:contains('Select')").attr('selected', 'selected');
    }
});
$("#cmbDestRegion").change(function () {
    if ($("#cmbDestRegion option:selected").attr('value') == "EMEA") {
        $("#cmbDestEuropianZone").attr("disabled", false);
    } else {
        $("#cmbDestEuropianZone").attr("disabled", "disabled");
        $("#cmbDestEuropianZone option:contains(Select)").attr('selected', 'selected');
    }
});

$("#btnPostNewRateRequest").click(function () {
    var RulesAccepted = true;
    var ErrorMessage = "";
    AddValidations();
    if (isFormValid(true) == false) return;
    if (ValidateRates(true) == false) return;

    $.each($("[id^='trRules_']"), function (key, val) {
        if (!$("#" + this.id + " input")[0].checked) {
            ErrorMessage += "<tr><td>" + $("#" + this.id + " label").html() + "</td><tr>";
            $(this).attr("style", "color:red");
            RulesAccepted = false
        }
        else {
            $(this).attr("style", "color:black");
        }
    });
    if (!RulesAccepted) {
        ErrorMessage = "<table style=\"color:red;\">" + ErrorMessage + "<table>";
        ShowNotification("Read & Accept \"Guidelines\"", ErrorMessage, 10000);
        return;
    }

    ShowNotification("Posting New Rate Request", "- Saving data...<br/>- Sending mail to your approver...");

    //return;



    callbackObject = this;
    if (UploadFiles() == false) return;

    DisableFields();
    HideAllOperations();

    collectedRates = CollectRates();
    var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();

    var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));

    var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
    var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
    var RatesValidTill = $("#txtRatesValidTill").val() == "12:00:00 AM" ? "" : $("#txtRatesValidTill").val();
    var collectedAddOnRates = CollectAddOnRates();
    var varGrossAddOnFees = 0.0;
    if ($("#txtTotalAddOnFees").val().length > 0) {
        varGrossAddOnFees=parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
    }
    RRAW.AirRateRequests.PostNewRateRequest(AuthenticationToken(), currentUserId, $('#hidCurrentDateTime').val(), $("#txtHAWBNumber").val(), $("#cmbShipMethod option:selected").val()
    , ShipDate, $("#cmbServiceLevel option:selected").text(), varWeight, $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val()
    , $("#cmbOriginRegion option:selected").text(), $("#txtOriginZipCode").val(), $("#txtConsigneeName").val(),
    $("#txtDestAirport").val(), $("#txtDestCity").val(),
    $("#cmbDestRegion option:selected").text(), $("#txtDestZipCode").val(), $("#txtRateDeterMethodology").val(),
    varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(), $("#txtComment").val(),
    $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
    uploadedFiles, collectedRates, NewAddOnRates, UpdatedAddOnRates(), RemovedAddOnRates(), $("#cmbTransportMode option:selected").text(), varGrossAddOnFees
    , onSuccessOfPostNewRateRequest, onFailureOfPostNewRateRequest);

});

function onFailureOfPostNewRateRequest(res) {
    window.location = "Errors.aspx?Operation=PostNewRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfPostNewRateRequest(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    currentRateRequestId = res;

    ShowNotification("Rate Request Posted Successfully", "- Email has been sent out to your approver.<br />- You will be notified by mail once your request is being reviewed by client.", 5000);

    GetRateRequest(currentRateRequestId);
}


$("#btnSave").click(function () {
    AddValidations();
    if (ValidateRates(true) == false) return;
    var RulesAccepted = true;
    var ErrorMessage = "";
    if (isFormValid(true) == false) return;

    $.each($("[id^='trRules_']"), function (key, val) {
        if (!$("#" + this.id + " input")[0].checked) {
            ErrorMessage += "<tr><td>" + $("#" + this.id + " label").html() + "</td><tr>";
            $(this).attr("style", "color:red");
            RulesAccepted = false
        }
        else {
            $(this).attr("style", "color:black");
        }
    });
    if (!RulesAccepted) {
        ErrorMessage = "<table style=\"color:red;\">" + ErrorMessage + "<table>";
        ShowNotification("Read & Accept \"Guidelines\"", ErrorMessage, 10000);
        return;
    }

    ShowNotification("Saving Rate Request", "- Saving data...");
    var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();


    callbackObject = this;
    if (UploadFiles() == false) return;

    DisableFields();
    HideAllOperations();

    collectedRates = CollectRates();
    var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
    var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
    var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
    var varGrossAddOnFees = 0.0;
    if ($("#txtTotalAddOnFees").val().length > 0) {
        varGrossAddOnFees = parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
    }
    if (currentRateRequestId == undefined) {
        collectedAddOnRates = CollectAddOnRates();
        RRAW.AirRateRequests.SaveNewRateRequest(AuthenticationToken(), currentUserId, $('#hidCurrentDateTime').val(), $("#txtHAWBNumber").val(),
        $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(),
        varWeight, $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(), $("#cmbOriginRegion option:selected").text(),
        $("#txtOriginZipCode").val(), $("#txtConsigneeName").val(), $("#txtDestAirport").val(), $("#txtDestCity").val(), $("#cmbDestRegion option:selected").text(),
        $("#txtDestZipCode").val(),
        $("#txtRateDeterMethodology").val(), varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(), $("#txtComment").val()
        , $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text()
         , uploadedFiles, collectedRates, NewAddOnRates, $("#cmbTransportMode option:selected").text(), varGrossAddOnFees, onSuccessOfSaveRateRequest, onFailureOfSaveRateRequest);
    }
    else {
        collectedAddOnRates = CollectAddOnRates();
        var updatedAddOnRates = UpdatedAddOnRates();
        var removedAddOnRates = RemovedAddOnRates();
        RRAW.AirRateRequests.SaveExistingRateRequest(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId,
        $("#txtHAWBNumber").val(), $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(), varWeight
        , $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(), $("#cmbOriginRegion option:selected").text(), $("#txtOriginZipCode").val()
        , $("#txtConsigneeName").val(), $("#txtDestAirport").val(), $("#txtDestCity").val(),
        $("#cmbDestRegion option:selected").text(), $("#txtDestZipCode").val(), $("#txtRateDeterMethodology").val(),
        varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(), $("#txtComment").val(),
        $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
         uploadedFiles, collectedRates, NewAddOnRates, updatedAddOnRates, removedAddOnRates, $("#cmbTransportMode option:selected").text(), varGrossAddOnFees, onSuccessOfSaveRateRequest, onFailureOfSaveRateRequest);
    }
});

function onFailureOfSaveRateRequest(res) {
    window.location = "Errors.aspx?Operation=SaveExistingRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfSaveRateRequest(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    currentRateRequestId = res;

    ShowNotification("Rate Request Saved Successfully", "- You can review your rate request under Open To Me in Dashboard.", 5000);

    GetRateRequest(currentRateRequestId);
}



$("#btnApprove").click(function () {


    var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
    var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
    var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
    var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();
    var varGrossAddOnFees = 0.0;
    if ($("#txtTotalAddOnFees").val().length > 0) {
        varGrossAddOnFees = parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
    }
    if (currentUserType == "Client") {
        ShowNotification("Uploading Documents", "- Uploading attachments...");

        callbackObject = this;
        if (UploadFiles() == false) return;





        ShowNotification("Approving Rate Request", "- Saving data...<br/>- Sending mail to related users...");

        RRAW.AirRateRequests.ApproveRateRequestByClient(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId, $("#txtComment").val(), uploadedFiles,
        $('#hidCurrentDateTime').val(), false, onSuccessOfApproveRateRequest);

        HideAllOperations();
    }
    else {
        if (isRateRequestUpdated == false) {
            ShowNotification("Approving Rate Request", "- Saving data...<br/>- Sending mail to your approver...");

            RRAW.AirRateRequests.ApproveRateRequestWithoutChange(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId,
            $("#txtComment").val(), onSuccessOfApproveRateRequest, onFailureOfApproveRateRequest);

            HideAllOperations();
        }
        else {

            if (isFormValid(true) == false) return;
            if (ValidateRates(true) == false) return;
            HideAllOperations();

            ShowNotification("Uploading Documents", "- Uploading attachments...");

            callbackObject = this;
            AddValidations();
            if (UploadFiles() == false) return;

            ShowNotification("Approving Rate Request", "- Saving data...<br/>- Sending mail to your approver...");
            collectedAddOnRates = CollectAddOnRates();
            var updatedAddOnRates = UpdatedAddOnRates();
            var removedAddOnRates = RemovedAddOnRates();
            RRAW.AirRateRequests.ApproveRateRequest(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId,
            $('#hidCurrentDateTime').val(), $("#txtHAWBNumber").val(), $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(), varWeight,
            $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(), $("#cmbOriginRegion option:selected").text(), $("#txtOriginZipCode").val(), $("#txtConsigneeName").val(),
            $("#txtDestAirport").val(), $("#txtDestCity").val(), $("#cmbDestRegion option:selected").text(),
            $("#txtDestZipCode").val(),
            $("#txtRateDeterMethodology").val(), varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(), $("#txtComment").val(),
            $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
             uploadedFiles, CollectRates(), NewAddOnRates, updatedAddOnRates, removedAddOnRates, $("#cmbTransportMode option:selected").text(), varGrossAddOnFees, onSuccessOfApproveRateRequest, onFailureOfApproveRateRequest);
        }
    }
});

$("#btnApproveAsAdhoc").click(function () {

    if (ValidateRates(true) == false) return;
    ShowNotification("Uploading Documents", "- Uploading attachments...");

    callbackObject = this;
    if (UploadFiles() == false) return;
    ShowNotification("Approving Rate Request", "- Saving data...<br/>- Sending mail to related users...");

    RRAW.AirRateRequests.ApproveRateRequestByClient(AuthenticationToken(), String(currentRateRequestId), currentUserId, $("#txtComment").val(), uploadedFiles,
        $('#hidCurrentDateTime').val(), true, onSuccessOfApproveRateRequest);

    HideAllOperations();
});

function onFailureOfApproveRateRequest(res) {
    window.location = "Errors.aspx?Operation=ApproveRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfApproveRateRequest(res) {

    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request Approved Successfully", "- You can review your rate request under Open For Others in Dashboard.", 5000);

    window.location = "Dashboard.aspx";
}


$("#btnApprove_Permanent").click(function () {
    if (ValidateRates(true) == false) return;
    if (currentUserId == 3) {
        ShowNotification("Uploading Documents", "- Uploading attachments...");

        callbackObject = this;
        if (UploadFiles() == false) return;
        var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
        var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
        var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
        var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();
        var varGrossAddOnFees = 0.0;
        if ($("#txtTotalAddOnFees").val().length > 0) {
            varGrossAddOnFees = parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
        }

        ShowNotification("Approving Rate Request", "- Saving data...<br/>- Sending mail to related users...");

        collectedRates = CollectRates();
        collectedAddOnRates = CollectAddOnRates();
        var updatedAddOnRates = UpdatedAddOnRates();
        var removedAddOnRates = RemovedAddOnRates();

        RRAW.AirRateRequests.ApproveRateRequestPermanent(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId, $('#hidCurrentDateTime').val(),
        $("#txtHAWBNumber").val(), $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(),
        varWeight, $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(), $("#cmbOriginRegion option:selected").text(), $("#txtOriginZipCode").val(),
        $("#txtConsigneeName").val(), $("#txtDestAirport").val(), $("#txtDestCity").val(),
        $("#cmbDestRegion option:selected").text(), $("#txtDestZipCode").text(),
        $("#txtRateDeterMethodology").val(),
        varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(), $("#txtComment").val(),
        $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
         uploadedFiles, collectedRates, NewAddOnRates, updatedAddOnRates, removedAddOnRates, $("#cmbTransportMode option:selected").text(), varGrossAddOnFees
         , onSuccessOfApproveRateRequest_Parmanent, onFailureOfApproveRateRequest_Parmanent);

        HideAllOperations();
    }
});

function onFailureOfApproveRateRequest_Parmanent(res) {
    window.location = "Errors.aspx?Operation=ApproveRateRequestParmanently&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}
function onSuccessOfApproveRateRequest_Parmanent(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request Approved Successfully", "- You can review your rate request under Approved in Dashboard.", 5000);

    window.location = "Dashboard.aspx";
}

$("#btnSendBackToRequestor").click(function () {
    ShowNotification("Reverting Rate Request", "- Saving data...<br/>- Sending mail to related users...");

    RRAW.AirRateRequests.SendRateRequestBackToOriginator(AuthenticationToken(), String(currentRateRequestId), currentUserId, 'A', $("#txtComment").val(), $('#hidCurrentDateTime').val()
    , onSuccessOfSendBackToRequestor);

    HideAllOperations();
});

function onSuccessOfSendBackToRequestor(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request reverted back Successfully", "- You can review your rate request under Open For Others in Dashboard.", 5000);

    window.location = "Dashboard.aspx";
}


$("#btnPostComment").click(function () {
    if ($("#txtComment").val() != "") {
        ShowNotification("Posting Your Comment", "- Saving data...<br/>- Sending mail to your approver...");

        RRAW.AirRateRequests.PostNewComment(AuthenticationToken(), String(currentRateRequestId), currentUserId, $("#txtComment").val(), onSuccessOfPostNewComment, onFailureOfPostNewComment);

        HideAllOperations();
    }
    else {
        $("#txtComment").focus();
        ShowNotification("Validation Failed", "- Please fill up comment box to post comment.", 5000);
    }
});

function onFailureOfPostNewComment(res) {
    window.location = "Errors.aspx?Operation=PostNewComment&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfPostNewComment(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Comment Posted Successfully", "- .", 5000);

    //replace this by having an independent call GetPreviousComments()
    GetRateRequest(currentRateRequestId);
}

$("#btnSendBackToReviseRate").click(function () {
    if (isRateRequestUpdated == false) {
        ShowNotification("Sending Rate Request back to revise", "- Saving data...<br/>- Sending mail to your approver...");

        RRAW.AirRateRequests.SendRateRequestBackToReviseWithoutChange(AuthenticationToken(), String(currentRateRequestId), currentUserId,
         $("#txtComment").val(), $('#hidCurrentDateTime').val()
        , onSuccessOfSendRateRequestBackToRevise, onFailureOfSendRateRequestBackToRevise);

        HideAllOperations();
    }
    else {
        var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
        var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
        var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
        var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();
        var varGrossAddOnFees = 0.0;
        if ($("#txtTotalAddOnFees").val().length > 0) {
            varGrossAddOnFees = parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
        }

        AddValidations();
        if (isFormValid(true) == false) return;
        if (ValidateRates(true) == false) return;
        ShowNotification("Sending Rate Request back to revise", "- Saving data...<br/>- Sending mail to your approver...");

        

        callbackObject = this;
        if (UploadFiles() == false) return;
        collectedAddOnRates = CollectAddOnRates();
        RRAW.AirRateRequests.SendRateRequestBackToRevise(AuthenticationToken(), parseInt(currentRateRequestId), currentUserId, $('#hidCurrentDateTime').val(), $("#txtHAWBNumber").val(),
         $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(),
        varWeight, $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(),
        $("#cmbOriginRegion option:selected").text(), $("#txtOriginZipCode").val(), $("#txtConsigneeName").val(), $("#txtDestAirport").val(), $("#txtDestCity").val(),
        $("#cmbDestRegion option:selected").text(), $("#txtDestZipCode").val(),
        $("#txtRateDeterMethodology").val(), varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(),
        $("#txtComment").val(),
        $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
        uploadedFiles, CollectRates(), NewAddOnRates, UpdatedAddOnRates(), RemovedAddOnRates(), $("#cmbTransportMode option:selected").text(), varGrossAddOnFees, onSuccessOfSendRateRequestBackToRevise, onFailureOfSendRateRequestBackToRevise);
        HideAllOperations();
    }
});

function onFailureOfSendRateRequestBackToRevise(res) {
    window.location = "Errors.aspx?Operation=SendRateRequestBackToRevise&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfSendRateRequestBackToRevise(res) {
    if (CheckPostCallStatus(res) == false) return errCode;
    ShowNotification("Rate Request Sent Back To Revise Successfully", "- You can review your rate request under Open For Others in Dashboard.", 5000);
    GetRateRequest(currentRateRequestId);
}


$("#btnNeedToReviseRate").click(function () {
    $("#PanelNewRateRequest").scrollTop(0);

    EnableFields();

    isRateRequestUpdated = true;

    $("#lblRateGroupTitle").html("Changing rates will keep copy of older rates");

    $("#ContainerNo").focus();
    $("#btnNeedToReviseRate").hide();
    $("#btnBackToDashboard").hide();
    $("#btnPostNewRateRequest").hide();
    $("#btnPostComment").hide();
    $("#btnApprove").show();
    $("#btnSave").show();
    $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");
    $("#UploadFileIframe").show();
    $("#pnlAttachments").show();
    $("#divAddIcon").addClass("addIcon");

    if (currentRateRequest.RequestorID != currentUserId) {
        $("#btnSendBackToReviseRate").show();
    }

    if (currentUserType == "Admin") {
        $("#btnSendBackToReviseRate").hide();
    }

    for (var i = 0; i < currentRateRequestHolders.length; i++) {

        if (currentRateRequestHolders[i] == currentUserId) {
            $("#btnApprove").show();
            if (currentRateRequestHolders[i] != currentRateRequest.RequestorID) {
                $("#btnSendBackToReviseRate").show();
            }
            $("#btnTransferToTariff").show();
            break;
        }

        if (currentRateRequest.RequestorID == currentUserId) {
            $("#btnApprove").attr('text', 'Post New Rate Request');
            $("btnApprove").width($("#btnPostNewRateRequest").width);
        }
        $("#btnApprove").hide();
        $("#btnSendBackToReviseRate").hide();
        $("#btnTransferToTariff").hide();
    }
    AttachDatePickers();
});

$("#btnRevoke").click(function () {
    ShowNotification("Revoking Rate Request", "- Setting attributes...<br/>- Sending mail to related persons...");

    HideAllOperations();

    RRAW.AirRateRequests.RevokeRateRequest(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId, $("#txtComment").val(), onSuccessOfRevokeRateRequest, onFailureOfRevokeRateRequest);
});

function onFailureOfRevokeRateRequest(res) {
    window.location = "Errors.aspx?Operation=RevokeRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfRevokeRateRequest(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request Revoked Successfully", "- You can now Approve or Reject rate request again.", 5000);

    GetRateRequest(currentRateRequestId);
}


$("#btnReject").click(function () {
    ShowNotification("Rejecting Rate Request", "- Uploading attachments...");

    callbackObject = this;
    if (UploadFiles() == false) return;
    var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
    var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
    var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
    var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();

    ShowNotification("Rejecting Rate Request", "- Setting attributes...<br/>- Sending mail to related persons...");

    HideAllOperations();

    RRAW.AirRateRequests.RejectRateRequest(AuthenticationToken(), String(currentRateRequestId), currentUserId,
    $("#hidCurrentDateTime").val(), varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(),
     $("#txtComment").val(), uploadedFiles, onSuccessOfRejectRateRequest, onFailureOfRejectRateRequest);
});

function onFailureOfRejectRateRequest(res) {
    window.location = "Errors.aspx?Operation=RejectRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfRejectRateRequest(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request Rejected Successfully", "- You will be notified by mail once your subordinate operates.", 5000);

    GetRateRequest(currentRateRequestId);
}


$("#btnRemoveAllComments").click(function () {
    if (confirm("Are you sure you want to remove all comments?") == true) {
        ShowNotification("Removing Comments", "- Sending mail to related persons...");

        HideAllOperations();

        RRAW.AirRateRequests.RemoveAllComments(AuthenticationToken(), String(currentRateRequestId), currentUserId, $("#txtComment").val(), $("#hidCurrentDateTime").val(), onSuccessOfRemoveAllComments, onFailureOfRemoveAllComments);
    }
    else {
        return false;
    }
});

function onFailureOfRemoveAllComments(res) {
    window.location = "Errors.aspx?Operation=RemoveAllComments&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfRemoveAllComments(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Comments Removed Successfully", "- Mail has been sent out to all related persons.", 5000);

    GetRateRequest(currentRateRequestId);
}

$("#btnBackToDashboard").click(function () {
    window.location = 'Dashboard.aspx';
});

function AuthenticationToken() {
    return GetCookie("AuthenticationToken");
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

function DisplayError(errorMessage, control) {
    if (control != undefined) {
        $(control).focus();
    }

    alert(errorMessage);

    return 1;
}

function GetCurrentDate() {
    var t = new Date();
    return ((t.getMonth() + 1) + '/' + t.getDate() + '/' + t.getFullYear() + ' ' + t.getHours() + ':' + t.getMinutes() + ':' + t.getSeconds());
}

function GetDate(dt) {
    dt = new Date(dt);
    var m = dt.getMonth() + 1;
    var d = dt.getDate();
    var y = dt.getFullYear();

    return (((m < 10) ? ("0" + m) : m) + '/' + ((d < 10) ? ("0" + d) : d) + '/' + y);
}

function CheckPostCallStatus(res) {
    if (Number(res) != NaN && (res == errCode || res == 0)) {
        DisplayError("Unknown error has occured. Please contact Administrator:" + res);
        return false;
    }
}




$("#btnTransferToTariff").click(function () {

    ShowNotification("Transfering Rate Request to Tariff", "- Transfering Rate Request to Tariff...");
    var varWeight = parseFloat($("#txtWeight").val().replace(",", ""));
    var varMinFreighRate = parseFloat($("#txtMinFreightRate").val().replace(",", ""));
    var varFreighRate = parseFloat($("#txtFreightRate").val().replace(",", ""));
    var ShipDate = $("#txtShipDate").val() == "12:00:00 AM" ? "" : $("#txtShipDate").val();
    var varGrossAddOnFees = 0.0;
    if ($("#txtTotalAddOnFees").val().length > 0) {
        varGrossAddOnFees = parseFloat($("#txtTotalAddOnFees").val().replace(",", ""));
    }

    ShowNotification("Uploading Documents", "- Uploading attachments...");
    callbackObject = this;
    if (UploadFiles() == false) return;

    ShowNotification("Transfering Rate Request to Tariff", "- Saving data...<br/>- Sending mail to related users...");
    RRAW.AirRateRequests.RateRequestTransferToTariff(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId,
        $("#hidCurrentDateTime").val(), $("#txtHAWBNumber").val(), $("#cmbShipMethod option:selected").val(), ShipDate, $("#cmbServiceLevel option:selected").text(),
        varWeight,
        $("#txtShipperName").val(), $("#txtOriginAirport").val(), $("#txtOriginCity").val(), $("#cmbOriginRegion option:selected").text(),
        $("#txtOriginZipCode").val(), $("#txtConsigneeName").val(), $("#txtDestAirport").val(), $("#txtDestCity").val(),
        $("#cmbDestRegion option:selected").text(), $("#txtDestZipCode").val(),
        $("#txtRateDeterMethodology").val(), varMinFreighRate, varFreighRate, $("#txtOtherCharges").val(),
        $("#txtComment").val(),
        $("#cmbOriginEuropianZone option:selected").text(), $("#cmbDestEuropianZone option:selected").text(), $("#cmbCurrency option:selected").text(),
        uploadedFiles, $("#hidCurrentDateTime").val(), $("#cmbTransportMode option:selected").text(), varGrossAddOnFees,
         onSuccessOfRateRequestTransferToTariff, onFailureOfRateRequestTransferToTariff);

    HideAllOperations();
});

function onSuccessOfRateRequestTransferToTariff(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request transfer to Tariff Successfully", "- You can review your rate request under Tariff.", 5000);

    window.location = "Dashboard.aspx";
}

function onFailureOfRateRequestTransferToTariff(res) {
    window.location = "Errors.aspx?Operation=RateRequestTransferToTariff&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}


$("#btnDelete").click(function () {
    if (trim(document.getElementById("txtComment").value) == "") {
        alert("Please add reason in Comment box to delete this Rate Request.");
        document.getElementById("txtComment").focus();
        return false;
    }
    if (confirm("Are you sure you want to delete this rate request?") == true) {
        ShowNotification("Deleting Rate Request", "- Setting attributes...<br/>- Sending mail to related persons...");

        HideAllOperations();

        RRAW.AirRateRequests.DeleteRateRequest(AuthenticationToken(), String(currentRateRequestId).toString(), currentUserId, $("#txtComment").val(), onSuccessOfDeleteRateRequest, onFailureOfDeleteRateRequest);
    }
    else {
        return false;
    }
});

function onFailureOfDeleteRateRequest(res) {
    window.location = "Errors.aspx?Operation=ArchiveRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfDeleteRateRequest(res) {
    if (CheckPostCallStatus(res) == false) return errCode;

    ShowNotification("Rate Request deleted Successfully", "- No one will be able to work on this rate request in system from now onwards.", 5000);

    window.location = 'Dashboard.aspx';
    //GetRateRequest(currentRateRequestId);
}

function trim(data) {
    if (data.length > 0) {
        return data.replace(/^\s*|\s*$/g, '');
    } else {
        return "";
    }
}