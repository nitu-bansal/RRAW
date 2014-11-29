/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />
/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js" />
/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />
/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js" />
/// <reference path="~/UserControls/RightPanel.ascx" />
var AttachmentsJSON;
var RateRequestID;
var rateRequestAttachmentsServerPath = "DownloadFile.aspx?FileName=~/Attachments/RateRequestAttachment/";

$(document).ready(function () {
   
});

function RightPanel_SetRateRequestID(R) {
     RateRequestID=R;
    GetAttachments();
    GetSimilarRateRequests();
    GetSimilarTariffLanes();
}
function GetAttachments() {
    RRAW.WebServices.Authentications.GetAttachments(RateRequestID, onSuccessOfGetAttachments, onFailureOfGetAttachments);    
}

function onFailureOfGetAttachments(res) {
    window.location = "Errors.aspx?Operation=GetRateRequest&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}
function onSuccessOfGetAttachments(res) {
    $("#divAdditionalInformation").fadeIn();
    if (res != undefined) {
        AttachmentsJSON = res;
    }

    PrepareAttachments(AttachmentsJSON["Attachments"]);

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

function isEmpty(obj) {
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop))
            return false;
    }

    return true;
}



function GetSimilarRateRequests() {
    RRAW.WebServices.Authentications.GetSimilarRateRequestsRightPanel(RateRequestID, 0, onSuccessOfGetSimilarRateRequests, onFailureOfGetSimilarRateRequests);
}

function onFailureOfGetSimilarRateRequests(res) {
    window.location = "Errors.aspx?Operation=GetSimilarRateRequests&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetSimilarRateRequests(res) {
    var strHTML = res;   
    $("#divSimilarRateRequests").html("");


    if (strHTML == "" || strHTML == "Error") {
        $("#divSimilarRateRequests").html("No Similar Rate Request Available");
    }
    else {       
        $("#divSimilarRateRequests").append(strHTML);

    }    
    $("#divAdditionalInformation").fadeIn();
    $("#divSimilarRateRequests").fadeIn();

}


function GetSimilarTariffLanes() {
    RRAW.WebServices.Authentications.GetSimilarRateRequestsRightPanel(RateRequestID, 1, onSuccessOfGetSimilarTariffLanes, onFailureOfGetSimilarTariffLanes);
}

function onFailureOfGetSimilarTariffLanes(res) {
    window.location = "Errors.aspx?Operation=GetSimilarTariffs&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfGetSimilarTariffLanes(res) {
    var strHTML = res;
    $("#divSimilarTariffLanes").html("");


    if (strHTML == "" || strHTML == "Error") {
        $("#divSimilarTariffLanes").html("No Similar Tariff Lane Available");
    }
    else {
        $("#divSimilarTariffLanes").append(strHTML);

    }
    $("#divAdditionalInformation").fadeIn();
    $("#divSimilarTariffLanes").fadeIn();

}