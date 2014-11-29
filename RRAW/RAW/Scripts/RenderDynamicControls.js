/// <reference path="~/UserControls/DyanamicForm.ascx" />

// Author : Sushrut Sawarkar
// Desc: Generates HTML Control template and attaches validations on it dynamically.
// Date : 1-Feb-2013
// Last Modified : 20-Feb-2013
// Version : 1.0
// Dependency : Requires Validation Engine Plugin for validation purpose.
var MesData;
var ShipAddData;
var CmdtyData;
var GLB_UserType = "";
var GLB_RateRequestID; //= "10"; //1
var GLB_ClientID; //= "3";
var GLB_TransPortModeID; //= "1"; //1
var GLB_UserId; //= "0"; //1
var GLB_ContainerType = {};
var GLB_RequestType;
var GLB_ButtonOperation = {};
var GLB_Buttons = {};
var CanApprove = false;
var UserHasRevisedData = false;
var ButtonContainer;
var uploadedFiles;
var fileUploadComplete = false;
var maxFileUploadChecks = 60;
var fileUploadChecked = 0;
var fileUploadError = false;
var callbackObject;
//var UserIsRateRequestCreator;
var UserIsCurrentRequestHolder;
var RateRequestStatus;
var RateRequestLevel;

var SaveBtn = '<input type="submit" class="submitbutton save" optype="Save" opmode="save" value="Save" />';
var PostBtn = '<input type="submit" class="submitbutton post" optype="Post" opmode="post" value="Post Request" />';
var ApproveBtn = '<input type="submit" class="submitbutton approve" optype="Approve" opmode="approve" value="Approve" />';
var RejectBtn = '<input type="submit" class="submitbutton reject" optype="Reject" opmode="reject" value="Reject" />';
var SendBacktoRequestorBtn = '<input type="submit" class="submitbutton sendbacktorequestor" optype="SendBack" opmode="sendbacktoreq" value="Send Back To Requestor" />';
var SendBacktoReviseBtn = '<input type="submit" class="submitbutton sendbacktorevise" optype="Revise" opmode="sendbacktorevise" value="Send Back To Revise Rates" />  ';
var ReviseRatesBtn = '<input type="button" class="needtorevise" optype="Revise" opmode="revise" value="Add Rates" />';

GLB_Buttons["Save"] = "save";
GLB_Buttons["Post"] = "post";
GLB_Buttons["Approve"] = "approve";
GLB_Buttons["Reject"] = "reject";
GLB_Buttons["BacktoRequestor"] = "sendbacktorequestor";
GLB_Buttons["SendBacktoRevise"] = "sendbacktorevise";
GLB_Buttons["ReviseRate"] = "needtorevise";

GLB_ContainerType["Measurements"] = "2";
GLB_ContainerType["Addresses"] = "1";
GLB_ContainerType["Comodity"] = "3";
GLB_ContainerType["Services"] = "5";
GLB_ContainerType["Rates"] = "0";

GLB_ButtonOperation["post"] = 1;
GLB_ButtonOperation["save"] = 2;
GLB_ButtonOperation["approve"] = 3;
GLB_ButtonOperation["reject"] = 4;

GLB_ButtonOperation["sendbacktorevise"] = 5;
GLB_ButtonOperation["sendbacktoreq"] = 6;
GLB_ButtonOperation["postcomment"] = 7;
GLB_ButtonOperation["dashboard"] = 8;

//AppendButton(GLB_Buttons.Save, SaveBtn);
//AppendButton(GLB_Buttons.SendBacktoRevise, SendBacktoReviseBtn)
//AppendButton(GLB_Buttons.Post, PostBtn);
//AppendButton(GLB_Buttons.Approve, ApproveBtn)
//AppendButton(GLB_Buttons.Reject, RejectBtn);
//AppendButton(GLB_Buttons.BacktoRequestor, SendBacktoRequestorBtn)
//AppendButton(GLB_Buttons.ReviseRate, ReviseRatesBtn);



$(document).ready(function () {


    var ErrorCode = "SS01VS00NB00";
    try {

        ButtonContainer = $('.ButtonOperationDiv');
        var txt = $('#comments'),
    hiddenDiv = $(document.createElement('div')),
    content = null;

        txt.addClass('txtstuff');
        hiddenDiv.addClass('hiddendiv common');

        $('.UserComments').append(hiddenDiv);

        txt.on('keyup', function () {
            content = $(this).val();
            content = content.replace(/\n/g, '<br>');
            hiddenDiv.html(content + '<br class="lbr">');
            $(this).css('height', hiddenDiv.height());

        });
        $("#UploadFileIframe").attr("src", "UploadFilePage.aspx");

        $(".accordionpane").accordion({

            collapsible: true,
            heightStyle: "content",
            active: false

        });
        //        $(".AccOtherInfo")


    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
});

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

function AppendButton(name, btn) {

    if ($("." + name).length > 0) {
    }
    else {

        ButtonContainer.append(btn);
    }

}

function SetDetails(ClientID, RateRequestID, TransPortModeID, UserID, UserType) {
    var ErrorCode = "SS02VS00NB00";
    try {

        var messagetouser = "";
        ShowNotification("Loading...", "- Please wait for a while.", 2000);

        $('.save').remove();
        $('.post').remove();
        $('.approve').remove();
        $('.reject').remove();
        $('.sendbacktorequestor').remove();
        $('.sendbacktorevise').remove();
        $('.needtorevise').remove();


        GLB_RateRequestID = RateRequestID;
        GLB_TransPortModeID = TransPortModeID;
        GLB_ClientID = ClientID;
        GLB_UserId = UserID;
        GLB_UserType = UserType;


        if (GLB_TransPortModeID == '1') {
            GLB_RequestType = 'A';
        }
        else if (GLB_TransPortModeID == '3') {
            GLB_RequestType = 'G';
        }
        else if (GLB_TransPortModeID == '2') {
            GLB_RequestType = 'O';
        }
        UserIsRateRequestCreator = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'IsUserRateRequestCreator', { UserID: GLB_UserId, RateRequestID: GLB_RateRequestID });
        UserIsCurrentRequestHolder = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'IsUserRateRequestHolder', { UserID: GLB_UserId, RateRequestID: GLB_RateRequestID });
        RateRequestStatus = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'RateRequestStatus', { RateRequestID: GLB_RateRequestID });
        RateRequestLevel = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'GetCurrentRateRequestLevel', { RateRequestID: GLB_RateRequestID , UserID: GLB_UserId });

        if (RateRequestLevel > 1) {
            ReviseRatesBtn = '<input type="button" class="needtorevise" optype="Revise" opmode="revise" value="Revise Rates" />';
        }

        if (GLB_RateRequestID == 0) {

            AppendButton(GLB_Buttons.Post, PostBtn);
            AppendButton(GLB_Buttons.Save, SaveBtn);
            $(".postComment").hide();
        }
        else if (UserIsRateRequestCreator && UserIsCurrentRequestHolder && RateRequestStatus == 2) {
            //user is creator then disply buttons accordingly
            messagetouser += " - You have not posted this request yet <br/>";
            AppendButton(GLB_Buttons.Post, PostBtn);
            AppendButton(GLB_Buttons.Save, SaveBtn);

        }

        else if (UserIsCurrentRequestHolder) {

            //user is currentRateRequest Holder then disply buttons accordingly
            AppendButton(GLB_Buttons.Approve, ApproveBtn)
            AppendButton(GLB_Buttons.ReviseRate, ReviseRatesBtn);
            CanApprove = true;
            if (GLB_UserType == 'GlobalAccountManager') {

                AppendButton(GLB_Buttons.ReviseRate, ReviseRatesBtn);
                AppendButton(GLB_Buttons.BacktoRequestor, SendBacktoRequestorBtn)
            }

            if (GLB_UserType == "Client") {

                AppendButton(GLB_Buttons.Reject, RejectBtn);
                AppendButton(GLB_Buttons.Approve, ApproveBtn)
                // AppendButton(GLB_Buttons.SendBacktoRevise, SendBacktoReviseBtn) 
            }
            messagetouser += " - You have some action items pending <br/>";
        }
        else {
            // some other user then hide every thing
            $(".postComment").hide();
            messagetouser += " - You can only view this request <br/>";

        }

        //sepcial case for admin user
        if (GLB_UserType == "Admin") {
            messagetouser += " - you have access to some more buttons <br/>";

            $(".postComment").hide();
            if (GLB_RateRequestID !== 0) {
                AppendButton("needtorevise", ReviseRatesBtn);
                ButtonContainer.append(SendBacktoRequestorBtn);
                $(".postComment").show();
            }
            if (UserIsCurrentRequestHolder) {
                $(".approve").show();
            }
            else {

                $(".approve").hide();
            }
        }
        //GLB_UserType != "Admin"
    
        if (UserIsRateRequestCreator) {
            if (UserIsCurrentRequestHolder || RateRequestStatus == 1) {
                messagetouser += " - Rates are visible <br/>";
            }
            else {
                if (GLB_UserType != "Admin") {
                    $('.spaceDynamicRates').remove();
                    $('.trDynamicRates').remove();
                }
            }
        }
        else {
            if (UserIsCurrentRequestHolder || RateRequestStatus == 1) {
                messagetouser += " - Rates are visible <br/>";
            }
            else {
                if (GLB_UserType != "Admin") {
                    $('.spaceDynamicRates').remove();
                    $('.trDynamicRates').remove();
                }
            }                
        }
        
 


        if (GLB_ClientID !== undefined) {
            RRAW.DynamicFieldsControl.GenerateContainerFields(GLB_ClientID, GLB_RateRequestID, GLB_ContainerType.Addresses, GLB_TransPortModeID, GLB_UserId, ShipperAddressDataReceived, DataReceivedFailed);
            RRAW.DynamicFieldsControl.GenerateContainerFields(GLB_ClientID, GLB_RateRequestID, GLB_ContainerType.Comodity, GLB_TransPortModeID, GLB_UserId, ComodityDataReceived, DataReceivedFailed);
            RRAW.DynamicFieldsControl.GenerateContainerFields(GLB_ClientID, GLB_RateRequestID, GLB_ContainerType.Measurements, GLB_TransPortModeID, GLB_UserId, ShipmentMeasureMentDataReceived, DataReceivedFailed);
            RRAW.DynamicFieldsControl.GenerateContainerFields(GLB_ClientID, GLB_RateRequestID, GLB_ContainerType.Services, GLB_TransPortModeID, GLB_UserId, ShipmentServicesDataReceived, DataReceivedFailed);
            RRAW.DynamicFieldsControl.GetAllPreviousComments(GLB_RateRequestID, PreviousCommentsReceived, DataReceivedFailed);

            if (GLB_RateRequestID != 0) {
                
               RRAW.DynamicFieldsControl.GenerateRatesContainerFields(GLB_ClientID, GLB_RateRequestID, GLB_TransPortModeID, RatesDataReceived, DataReceivedFailed);              

            }
            else {
                $('.spaceDynamicRates').remove();
                $('.trDynamicRates').remove();
            }
            messagetouser += " - Continue with your request.. <br/>";

        }
        //    if (DupRRQID > 0) {
        //        ShowNotification("Duplicate Rate Request [ID : " + DupRRQID + "]", "You are not allowed to generate duplicate rate request", 5000);
        //    }

        var FrmtoValidate = $('#EntryForm1');
        $(FrmtoValidate).validationEngine({
            autoPositionUpdate: true,
            autoHidePrompt: false,
            autoHideDelay: 10000,
            focusFirstField: true,
            promptPosition: "bottomRight",
            scroll: true
        });

        $('div').scroll(function () {
            $(FrmtoValidate).validationEngine("updatePromptsPosition")
        });


        $('.needtorevise').click(function () {

            $("#EntryForm1 [name='RenderedControl']").removeAttr('disabled', 'false');

            $("#EntryForm1 [name='RenderedButton']").removeClass("hideControl");


            $.each($("[Class^=ApplyCurrencyConversion_]"), function (key, val) {

                var Selectedval = $(val).find(":selected").val();
                if (Selectedval == 1) {
                    $('.tdinputConvertToCurrency_' + this.className.split('_')[1]).removeAttr('disabled');
                }
                else {
                    $('.tdinputConvertToCurrency_' + this.className.split('_')[1]).attr('disabled', 'disabled');

                }


            });


            AppendButton(GLB_Buttons.Save, SaveBtn);
            AppendButton(GLB_Buttons.SendBacktoRevise, SendBacktoReviseBtn)
            UserHasRevisedData = true;

        });

        $(".postComment").click(function () {
            if ($("#txtComment").val() != "") {
                //ShowNotification("Posting Your Comment", "- Saving data...<br/>- Sending mail to your approver...");
                RRAW.DynamicFieldsControl.AddNewComment(GLB_RateRequestID, GLB_UserId, $("#txtComment").val(), OnAddingNewComment, OnAddingNewComment);
                $("#txtComment").val("");
            }
            else {
                $("#txtComment").focus();
                ShowNotification("Validation Failed", "- Please enter some comment in the box to post comment.", 5000);
            }
        });

        //At last
        $('.submitbutton').live("click", function () {

            var buttonpressed = GLB_ButtonOperation[$(this).attr('opmode')];
            callbackObject = this;
            AttachValidationEnginetoForm(FrmtoValidate, buttonpressed);
        });

        ShowNotification("Loaded successfully.", messagetouser, 2000);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }

}

function AttachValidationEnginetoForm(Form, OperationType) {
    var ErrorCode = "SS03VS00NB00";
    try {

        $(Form).validationEngine('detach');
        $(Form).validationEngine('attach', {
            onValidationComplete: function (form, status) {
                if (status) {
                    // var c = 0;
                    var Container = $('.ShipAddr');
                    var Container2 = $('.ComodityInfo');
                    var Container3 = $('.MeasurementInfo');
                    var Container4 = $('.tblBreakDownCharges');
                    var Container5 = $('.ServicesInfor');
                    //@@@@@@@------------@@@@@@
                    if (GLB_RateRequestID == 0) {
                        GLB_RateRequestID = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'NewRateRequest', { ClientID: GLB_ClientID, TransPortMode: GLB_TransPortModeID, RequestType: GLB_RequestType, UserID: GLB_UserId });
                        ShowNotification("New Rate Request [ID : " + GLB_RateRequestID + "]", "Current Request is being processed", 800);
                        //need to preserve this RateRequestID and setting it globally for further use.             
                    }
                    //@@@@@@@------------@@@@@@
                    var AddressesData = GetDataFromContainerSpecialCase(Container);
                    var ComodityData = GetDataFromContainer(Container2);
                    var MeasuremetDataData = GetDataFromContainerGridCase(Container3);
                    var ServicesData = GetDataFromContainer(Container5);
                    var RatesData = GetDataFromRatesContainer(Container4);

                    var FormData = {}
                    var Message_WebService = "";
                    var Excp = 0;

                    FormData[GLB_ContainerType.Addresses] = AddressesData;
                    FormData[GLB_ContainerType.Comodity] = ComodityData;
                    FormData[GLB_ContainerType.Services] = ServicesData;
                    FormData[GLB_ContainerType.Measurements] = MeasuremetDataData;
                    FormData[GLB_ContainerType.Rates] = RatesData;
                    UploadFiles();
                    if (fileUploadComplete) {
                        RRAW.DynamicFieldsControl.SaveAllData(FormData, OperationType, Number(GLB_RateRequestID), GLB_ClientID, GLB_TransPortModeID, GLB_UserId, GLB_UserType, UserHasRevisedData, DataSaved, DataSaved);
                        ShowNotification("Saving...", "Sending Data to Server..", 4000);
                    }


                    //############# OLD CODE ############
                    //RRAW.DynamicFieldsControl.SaveShipmentData(GLB_ContainerType.Addresses, GetDataFromContainerSpecialCase(Container), DataSaved, DataSaved);
                    //RRAW.DynamicFieldsControl.SaveShipmentData(GLB_ContainerType.Measurements, GetDataFromContainerGridCase(Container3), DataSaved, DataSaved);
                    //RRAW.DynamicFieldsControl.SaveData(GLB_ContainerType.Comodity, GetDataFromContainer(Container2), DataSaved, DataSaved);
                    //RRAW.DynamicFieldsControl.SaveShipmentData(GLB_ContainerType.Rates, GetDataFromRatesContainer(Container4), DataSaved, DataSaved);
                    //############# OLD CODE ############

                }

                else {
                    alert("The form status is: " + status + ", it will never submit");
                }


            }
        });
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function AttachmentSaved(res) {
    var ErrorCode = "SS04VS00NB00";
    try {
        if (res) {
            //  ShowNotification("Attachments added successfully", "- Saved", 5000);
        } else {
            // ShowNotification("Attachments were not added", "- Try Again", 5000);
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function DataSaved(res) {
    var ErrorCode = "SS05VS00NB00";
    try {

        var Title = "Rate Request [ID : " + GLB_RateRequestID + "] Processed ...";
        var msg = "";

        if (res.Save) {
            msg = "- Data save successfully (Refreshing...) <br/>";



        }
        else {
            msg = "- Data was not saved, please contact administrator <br/>";
        }

        if (res.Exp) {

            msg += '- ' + res.Message + "<br/>";
            msg += '- Try again later';
        }

        $(".postComment").click();
        ShowNotification(Title, msg, 2000);

        SetDetails(GLB_ClientID, GLB_RateRequestID, GLB_TransPortModeID, GLB_UserId, GLB_UserType);

    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 8400);
    }
}

function validateit(field, rules, i, options) {
    var ErrorCode = "SS06VS00NB00";
    try {
        var pattern = new RegExp(field.attr('reg'));
        var ValidationMsg = field.attr('ValMsg');
        var CurrentVal = field.val();
        if (!pattern.test(CurrentVal)) {
            return ValidationMsg;
        }
        else {
            return true;
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
        return false;
    }
}

function AddRow(e) {
    var ErrorCode = "SS07VS00NB00";
    try {

        RRAW.DynamicFieldsControl.GenerateContainerFields(GLB_ClientID, '0', GLB_ContainerType.Measurements, GLB_TransPortModeID, GLB_UserId, AddNewRow, DataReceivedFailed);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
        return false;
    }
}

function RemoveRow(e) {
    var ErrorCode = "SS08VS00NB00";
    try {
        var MasterID = $($(e).parent().parent()).find("[name='RenderedControl']")[1].id.split('_')[3];
        if (MasterID.indexOf('#') >= 0) {
            MasterID = "";
            $(e).parent().parent().remove();
        }
        else {
            ConfirmDialog("Please Confrim ...", "Delete Current Record ?", function (result) {
                if (result) {
                    RRAW.DynamicFieldsControl.DeleteShipmentMeasurementRow(MasterID, DeleteRowResult, DeleteRowResult);
                    $(e).parent().parent().remove();
                }
            });
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function ConfirmDialog(Title, Message, Result) {
    var ErrorCode = "SS09VS00NB00";
    try {

        $('<div></div>').appendTo('body').html("<div><p></p><strong><span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 20px 0;'></span>" + Message + "</strong><p>Note : This operation cannot be undone.</p></div>").dialog({
            modal: true, zIndex: 10000, autoOpen: true,
            title: Title,
            height: 180, resizable: false,
            show: {
                effect: "shake",
                duration: 500
            },
            hide: {
                effect: "slide",
                duration: 500
            },
            buttons: {
                Yes: function () {
                    Result(true);
                    $(this).dialog("close");
                },
                No: function () {
                    Result(false);
                    $(this).dialog("close");
                }
            }
        });
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function DeleteRowResult(res) {
    if (res) {

    }
}

function AddNewRow(res) {
    var Container = $('#tblMeasurements');
    AddNewRowinGrid(res, Container, Container[0].rows.length)
}

function ShowRangeTariffRow(Control) {
    var ErrorCode = "SS10VS00NB00";
    try {
        var Container = $('.DivRangeInputs');
        var SelectedText_ContainerClass = $(Control).find(":selected").text();
        var Title = $(Control).attr('chargetype');
        var Class = Title.replace(/\s/g, '');
        var ID = $(Control).attr('id') + "_" + Class;

        var SelectedText = $(Control).find(":selected").text();
        if (SelectedText == "Select") return;

        RangeSel = Control;
        var RateID = $(Control).attr('rateid').trim();
        var RateRequestID = $(Control).attr('RateRequestID').trim();
        var StoredScaleCode = $(Control).attr('StoredScaleCode').trim();
        var result;
        if (StoredScaleCode !== SelectedText) {

            result = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'GenerateRangeInputFileds', { ScaleCode: SelectedText, RateRequestID: 0, RateID: 0 });
            //var y = RRAW.DynamicFieldsControl.GenerateRangeInputFileds(SelectedText, "0", "0");
        }
        else {

            result = ExecuteSynchronously('WebServices/DynamicFieldsControl.asmx', 'GenerateRangeInputFileds', { ScaleCode: StoredScaleCode, RateRequestID: RateRequestID, RateID: RateID });
            //  var x = RRAW.DynamicFieldsControl.GenerateRangeInputFileds(SelectedText, RateRequestID, RateID);

        }
        if (document.getElementById(ID)) {
            $('.' + Class).remove();
        }
        var con = AddContainer(Container, Class, ID, Title, SelectedText_ContainerClass);

        JSON2HTMLControls(result, con, 0, true);

    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
        return false;
    }
}

function ExecuteSynchronously(url, method, args) {
    var ErrorCode = "SS11VS00NB00";
    try {

        var executor = new Sys.Net.XMLHttpSyncExecutor();
        var request = new Sys.Net.WebRequest(); // Instantiate a WebRequest.    
        request.set_url(url + '/' + method); // Set the request URL.   
        request.set_httpVerb('POST'); // Set the request verb.    
        request.get_headers()['Content-Type'] = 'application/json; charset=utf-8'; // Set request header.   
        request.set_executor(executor); // Set the executor.  
        request.set_body(Sys.Serialization.JavaScriptSerializer.serialize(args));  // Serialize argumente into a JSON string.
        request.invoke();    // Execute the request.
        if (executor.get_responseAvailable()) {
            return (executor.get_object().d);
        }
        return (false);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
        return false;
    }
}

function OnAddingNewComment(res) {
    var ErrorCode = "SS12VS00NB00";
    try {

        if (res) {
            //   ShowNotification("Comment Posted Successfully", "- .", 5000);
            RRAW.DynamicFieldsControl.GetAllPreviousComments(GLB_RateRequestID, PreviousCommentsReceived, DataReceivedFailed);
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function DisableForm() {
    if (!(UserIsRateRequestCreator && UserIsCurrentRequestHolder)) {

        $("#EntryForm1 [name='RenderedControl']").attr('disabled', 'disabled');
        $("#EntryForm1 [name='RenderedButton']").addClass("hideControl");
    }
}

function ComodityDataReceived(res) {
    var ErrorCode = "SS13VS00NB00";
    try {

        var Container = $('.ComodityInfo');
        if (res.Data0.length > 0) {
            JSON2HTMLControls(res, Container, 8, true);
        }
        else {
            $('.spaceComodityInfo').remove();
            $('.trComodityInfo').remove();
        }

        if (GLB_RateRequestID > 0) {

        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function ShipperAddressDataReceived(res) {
    var ErrorCode = "SS14VS00NB00";
    try {

        var Container = $('.ShipAddr');
        if (res.Data0.length > 0) {
            JSON2HTMLControls(res, Container, 8, true);
        }
        else {
            $('.spaceComodityInfo').remove();
            $('.trShipAddr').remove();
        }
        if (GLB_RateRequestID > 0) {
            DisableForm();
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function ShipmentMeasureMentDataReceived(res) {
    var ErrorCode = "SS15VS00NB00";
    try {

        var Container = $('.MeasurementInfo');
        if (res.Data0.length > 0) {

            JSON2HTMLGridControls(res, Container);
        }
        else {
            $('.spaceMeasurementInfo').remove();
            $('.trMeasurementInfo').remove();
        }
        if (GLB_RateRequestID > 0) {
            DisableForm();
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function ShipmentServicesDataReceived(res) {
    var ErrorCode = "SS16VS00NB00";
    try {

        var Container = $('.ServicesInfor');
        if (res.Data0.length > 0) {
            JSON2HTMLControls(res, Container, 8, true);
        }
        else {
            $('.spaceServicesInfor').remove();
            $('.trServicesInfor').remove();
        }
        if (GLB_RateRequestID > 0) {
            DisableForm()
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function RatesDataReceived(res) {
    var ErrorCode = "SS17VS00NB00";
    try {

        ResData = res;
        var RateContainer = $('.DynamicRates');
        if (res.Data0.length > 0) {
            JSON2HTMLratesControls(ResData, RateContainer);
        }
        else {
            $('.spaceDynamicRates').remove();
            $('.trDynamicRates').remove();
        }

        // RRAW.DynamicFieldsControl.GenerateRangeInputFileds(SelectedText, RateRequestID, RateID, RangeInputsDataReceived, RangeInputsDataFailed);


        $.each($("[id^='sel_']"), function (key, val) {
            var myObject2 = {};
            $.each($(this).find("[class='RangeSelector']"), function (key1, val1) {
                ShowRangeTariffRow(val1);
            });
        });

        $('.RangeSelector').change(function () {
            ShowRangeTariffRow(this)
        });

        $("[Class^=ApplyCurrencyConversion_]").change(function () {

            var Selectedval = $(this).find(":selected").val();
            if (Selectedval == 1) {
                $('.tdinputConvertToCurrency_' + this.className.split('_')[1]).removeAttr('disabled');
            }
            else {
                $('.tdinputConvertToCurrency_' + this.className.split('_')[1]).attr('disabled', 'disabled');

            }


        });



        if (GLB_RateRequestID > 0) {
            DisableForm();
        }
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function PreviousCommentsReceived(res) {
    var ErrorCode = "SS18VS00NB00";
    try {

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
                $("#divPreviousComments").append("<p class='triangle-right right'><strong>" + val.CommentedBy + " </strong> <span style='float:right'> " + val.CommentDate + "</span><br/>&nbsp;&nbsp;" + val.Comment + "</p>");
            });
        }

        $("#divPreviousComments").fadeIn();

    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }

}

function DataReceivedFailed(res) {
    alert("FAIL");
}

function AddContainer(MasterContainer, DivClass, DivID, Title, SelectedOption) {
    var ErrorCode = "SS19VS00NB00";
    try {

        var Div1StartTag = "<div id='" + DivID + "' class='RatesRangeHolder " + DivClass + "'>";
        var TableStarttag = "<table id='" + SelectedOption + "_tblRange' class='tblRangeInputHolder " + DivClass + "_" + SelectedOption + "'>";
        var TableEndtag = '</table>';
        var Div1EndtTag = '</div>';
        $(MasterContainer).append(Div1StartTag + "</br><fieldset class='RangeFieldSet' style='width: 100%;' ><legend> " + Title + " [Range Tariff : " + SelectedOption + "] </legend>" + TableStarttag + TableEndtag + "</fieldset>" + Div1EndtTag);
        return $('.' + DivClass + "_" + SelectedOption);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}
////#############################################////
function JSON2HTMLratesControls(JsonRateData, RateBucket) {
    var ErrorCode = "SS20VS00NB00";
    try {

        var tb = '<table id="tblBreakDownCharges" class="tblBreakDownCharges" cellpadding="0">';

        var colspan = 0;
        var xx = "";
        var row0 = "";
        var row1 = "";
        var row2 = "";
        var str = "";
        var Customfuncall;
        var pattern = ""
        var required = ""
        var addValdationClass = "";
        var CurrnetFieldMasteID = "";

        $.each(JsonRateData.Data0, function (i, v) {
            colspan = 0;

            pattern = v.Pattern;
            required = v.ValReq;
            Customfuncall = "";
            addValdationClass = "";
            flag = false;
            if (v.MasterID != "") {
                CurrnetFieldMasteID = v.MasterID;
            }
            else {
                CurrnetFieldMasteID = "#" + i;
            }
            if (pattern != "") {
                Customfuncall = "funcCall[validateit]";
                flag = true
            }
            if (required != "") {
                flag = true
            }
            if (pattern != "" && required != "") {
                seperator = ",";
                flag = true;
            }

            if (flag) {
                addValdationClass = " validate[" + required + seperator + Customfuncall + "] ";
            }


            row1 += "<td> Min</td>";
            row2 += "<td><input name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "'  rateid='" + v.RateID + "' RateRequestID='" + v.RateRequestID + "' ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='Minimum_" + CurrnetFieldMasteID + "' class='inputbox " + addValdationClass + "' value='" + v.Minimum + "'/></td>";
            colspan++;



            colspan++;
            row1 += "<td> Max </td>";
            row2 += "<td><input name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' rateid='" + v.RateID + "'ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='Maximum_" + CurrnetFieldMasteID + "' class='inputbox " + addValdationClass + "'  value='" + v.Maximum + "'/></td>";



            colspan++;
            if (v.UnitHeader === "") {
                row1 += "<td> *NA* </td>";
            }
            else {
                row1 += "<td> " + v.UnitHeader + " </td>";
            }
            row2 += "<td><input type='hidden' name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' rateid='" + v.RateID + "' RateRequestID='" + v.RateRequestID + "' id='UOMID_" + CurrnetFieldMasteID + "' class='inputbox' value='" + v.UOMID + "'/> <input  name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' rateid='" + v.RateID + "' RateRequestID='" + v.RateRequestID + "' ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='Rate1_" + CurrnetFieldMasteID + "' class='inputbox' value='" + v.Rate1 + "'/></td>";

            //---------------------

            if (v.IsCurrency == "1") {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.CurrencyDataSource1.split(',')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.Currency == Dat[k].split('-')[0]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";

                    }

                }
                row1 += "<td> Actual Currency </td>";
                row2 += "<td id='sel_" + i + "'><select name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredCurrencyID='" + v.Currency + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='CurrencyID_" + v.RateID + "' class='" + "CurrencySelector" + "'>" + str + " </select></td>";
            }

            if (v.PartyID > 0) {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.PartyDataSource.split(',')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.Party == Dat[k].split('-')[0]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";

                    }

                }
                row1 += "<td>Party</td>";
                row2 += "<td id='sel_" + i + "'><select name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredPartyID='" + v.Currency + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='PartyID_" + v.RateID + "' class='" + "PartySelector" + "'>" + str + " </select></td>";
            }





            if (v.ChargeTypeID > 0) {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.ChargeTypeDataSource.split(',')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.ChargeType == Dat[k].split('-')[0]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";

                    }

                }
                row1 += "<td>ChargeType</td>";
                row2 += "<td id='sel_" + i + "'><select name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredChargeTypeID='" + v.Currency + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='ChargeTypeID_" + v.RateID + "' class='" + "ChargeTypeSelector" + "'>" + str + " </select></td>";
            }

            if (v.IsCurrencyConversionApply == "1") {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.IsConvDataSource.split(',')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.IsCurrConversion == Dat[k].split('-')[0]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";

                    }

                }
                row1 += "<td> Convert Curr? </td>";
                row2 += "<td id='sel_" + i + "'><select name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredIsConversion='" + v.IsCurrConversion + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='IsCurrencyConversionApply_" + v.RateID + "' class='ApplyCurrencyConversion_" + v.RateID + "'>" + str + " </select></td>";
            }


            if (v.IsConverttoCurrency == "1") {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.CurrencyDataSource2.split(',')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.ConvCurrency == Dat[k].split('-')[0]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('-')[0] + "' title='" + Dat[k].split('-')[1] + "' value='" + Dat[k].split('-')[0] + "'>" + Dat[k].split('-')[1] + "</option>";

                    }

                }
                row1 += "<td> Converted Currency </td>";
                row2 += "<td id='sel_" + i + "'><select disabled name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredConvtoCurrID='" + v.ConvCurrency + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='ConvertToCurrencyID_" + v.RateID + "' class='tdinputConvertToCurrency_" + v.RateID + "'>" + str + " </select></td>";
            }
            ///////------------------------


            if (v.IsRangeTariff == "1") {
                colspan++;
                var str = "<option value=''>Select</option>";
                var Dat = v.DataSource.split('~')
                for (var k = 0; k < Dat.length; k++) {

                    if (v.ScaleCode == Dat[k].split('^')[1]) {

                        str += " <option selected = 'selected' id='range_" + Dat[k].split('^')[0] + "' title='" + Dat[k].split('^')[2] + "' value='" + Dat[k].split('^')[0] + "'>" + Dat[k].split('^')[1] + "</option>";
                    }
                    else {
                        str += " <option id='range_" + Dat[k].split('^')[0] + "' title='" + Dat[k].split('^')[2] + "' value='" + Dat[k].split('^')[0] + "'>" + Dat[k].split('^')[1] + "</option>";

                    }

                }
                row1 += "<td> Range </td>";
                row2 += "<td id='sel_" + i + "'><select name='RenderedControl' recordmasterid='" + CurrnetFieldMasteID + "' StoredScaleCode='" + v.ScaleCode + "' RateRequestID='" + v.RateRequestID + "' chargetype='" + v.RateHeader + "' rateid='" + v.RateID + "'  id='ScaleCode_" + v.RateID + "' class='" + "RangeSelector" + "'>" + str + " </select></td>";
            }





            row0 += "<th colspan='" + colspan + "'> <span>" + v.RateHeader + " </span></th>";

            xx = "<tr>" + row0 + "</tr>";
            xx += "<tr>" + row1 + "</tr>";
            xx += "<tr>" + row2 + "</tr>";


        });
        $(RateBucket).html("");
        $(RateBucket).prepend(tb + xx + "</table>");
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}
// send ContrlsPerRow as zero for unlimited
function JSON2HTMLControls(JsonData, Bucket, ContrlsPerRow, LabelAboveInput) {
    var ErrorCode = "SS21VS00NB00";
    try {

        var tb = '<tr>';
        var xx = "";
        var Customfuncall;
        var pattern = ""
        var required = ""
        var addValdationClass = "";
        var seperator = "";
        var flag = false;
        var label = "";
        var CurrnetFieldValue = "";
        var CurrnetFieldMasteID = "";
        var CurrentControlID = "";
        var IntervalText = "";
        var IntervalStart = "";
        var IntervalEnd = "";
        var RateRequestID = "";
        var RateID = "";
        var FieldIndex = "";
        var DataPresent = false;

        if ((JsonData.Data1 != undefined)) {
            if (JsonData.Data1.length > 0) {
                DataPresent = true;
            }
        }
        //for (var i = 0; i < v.Length - 1; i++) {
        $.each(JsonData.Data0, function (i, v) {
            pattern = v.Pattern;
            required = v.ValReq;
            Customfuncall = "";
            addValdationClass = "";
            seperator = "";
            flag = false;


            if (DataPresent) {

                CurrnetFieldValue = JsonData.Data1[0][v.ID]; //assign value to the filed
                CurrnetFieldMasteID = JsonData.Data1[0]["ID"];
                CurrentControlID = v.ID + "_" + JsonData.Data1[0]["ID"];

            }
            else {
                CurrnetFieldValue = v.value;
                if (v.MasterID != "") {
                    CurrnetFieldMasteID = v.MasterID;
                    CurrentControlID = v.ID + "_" + v.MasterID;

                    //only for Rates
                    IntervalText = "IntervalText='" + v.Label + "'";
                    IntervalStart = " IntervalStart='" + v.Start + "'";
                    IntervalEnd = " IntervalEnd='" + v.End + "'";
                    RateRequestID = " RateRequestID='" + v.RateRequestID + "'";
                    RateID = " RateID='" + v.RateID + "' ";
                    FieldIndex = " FieldIndex='" + v.InputID + "' ";
                }
                else {
                    CurrnetFieldMasteID = v.ID + "_#" + i;
                    CurrentControlID = v.ID + "_" + "#" + i;
                    IntervalText = "IntervalText='" + v.Label + "'";
                    IntervalStart = " IntervalStart='" + v.Start + "'";
                    IntervalEnd = " IntervalEnd='" + v.End + "'";
                    RateRequestID = " RateRequestID='" + v.RateRequestID + "'";
                    RateID = " RateID='" + v.RateID + "' ";
                    FieldIndex = " FieldIndex='" + v.InputID + "' ";
                }


            }

            if (pattern != "") {
                Customfuncall = "funcCall[validateit]";
                flag = true
            }
            if (required != "") {
                flag = true
            }
            if (pattern != "" && required != "") {
                seperator = ",";
                flag = true;
            }

            if (flag) {
                addValdationClass = " validate[" + required + seperator + Customfuncall + "] ";
            }
            if (LabelAboveInput) {

                if (i < JsonData.Data0.length - 1 && JsonData.Data0.length < ContrlsPerRow - 1) {
                    label = "<td><span class='labelaboveinput'>" + v.Label + " </span> </br> ";

                }
                else

                    label = "<td><span class='labelaboveinput'>" + v.Label + " </span> </br>";
            }
            else {
                //                if (i < JsonData.Data0.length && JsonData.Data0.length < ContrlsPerRow) {
                //                    label = "<td style='max-width: 25px;'>" + v.Label + "</td> <td>";
                //                    if (i < JsonData.Data0.length-1)
                //                        label += "<td style='max-width: 25px;'>"
                //                        else
                //                         label += "<td>"
                //                }
                //                else
                label = "<td>" + v.Label + "</td><td>";
            }


            if (v.Type === "select" || v.Type === "multiple") {

                var MultiClass = "";
                var arsr = v.DataSource.split(',');
                var str = "<option value=''>Select</option>";
                var Mult = "";
                var setselected = " selected = 'selected' ";
                if (CurrnetFieldValue == true) {
                    CurrnetFieldValue = '1';
                }
                if (CurrnetFieldValue == false) {
                    CurrnetFieldValue = '0';
                }
                if (v.Type === "multiple") {
                    Mult = " multiple='multiple' ";
                    MultiClass = " multiselect ";
                    str = "";

                }
                CurrnetFieldValue = CurrnetFieldValue.toString().split(',');


                for (var z = 0; z < arsr.length; z++) {

                    if ($.inArray(arsr[z].split('-')[0].toString().trim(), CurrnetFieldValue) >= 0) {
                        setselected = " selected = 'selected' ";
                    }
                    else {
                        setselected = " ";
                    }
                    str += " <option " + setselected + " value='" + arsr[z].split('-')[0] + "'>" + arsr[z].split('-')[1] + "</option>";

                }

                xx += label + "<select " + Mult + FieldIndex + IntervalText + IntervalStart + IntervalEnd + RateRequestID + RateID + " name='RenderedControl' id='" + CurrentControlID + "' title='" + v.ToolTip + "' FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "' class='" + "sds MultiClass" + "'>" + str + " </select></td>";

            }
            else {
                xx += label + "<input " + FieldIndex + IntervalText + IntervalStart + IntervalEnd + RateRequestID + RateID + " name='RenderedControl'  title='" + v.ToolTip + "' FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "'   ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='" + CurrentControlID + "' class='inputbox  " + addValdationClass + "' value='" + CurrnetFieldValue + "'/></td>";
            }
            if (ContrlsPerRow !== 0) {
                if ((i + 1) % ContrlsPerRow === 0) {
                    xx += "</tr><tr>";
                }
            }
            //}
        });
        $(Bucket).html("");
        $(Bucket).append(tb + xx + "</tr>");
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }

}

function JSON2HTMLGridControls(JsonData, Bucket) {
    var ErrorCode = "SS22VS00NB00";
    try {

        var tb = '<table class="tblMeasurements" id="tblMeasurements" cellpadding="0">';
        // var tb = '<tr>';
        var xx = "";
        var rows = "";
        var Customfuncall;
        var pattern = ""
        var required = ""
        var addValdationClass = "";
        var seperator = "";
        var flag = false;
        var label = "";
        var row0 = "";
        var row1 = ""
        var Header = "";
        var InnerControls = "";
        var TotalInnerControls = 1;
        var CurrnetFieldValue = "";
        var CurrnetFieldMasteID = "";
        var CurrentControlID = "";
        var removerow = "";
        var DataPresent = false;
        //for (var i = 0; i < v.Length - 1; i++) {

        //    if (JsonData.Data1 != undefined) {
        //        $.each(JsonData.Data1, function (key, val) {
        //            $.each(val, function (key1, val1) {
        //                if (key1 != "ID") {
        //                    row0 += "<th colspan='" + JsonData.Data1.length + "'> <span>" + key1 + " </span></th>";
        //                }
        //                // $('#' + key1).val(val1);
        //            });
        //        });
        //    }
        //else



        if (JsonData.Data1 != undefined) {
            TotalInnerControls = JsonData.Data1.length
            if (TotalInnerControls == 0) {
                TotalInnerControls = 1;
            } else {
                DataPresent = true;
            }
        }

        for (var k = 0; k < TotalInnerControls; k++) {
            row1 = "";
            row0 = "";
            removerow = "";
            CurrnetFieldValue = "";
            CurrnetFieldMasteID = "";
            $.each(JsonData.Data0, function (i, v) {
                pattern = v.Pattern;
                required = v.ValReq;
                Customfuncall = "";
                addValdationClass = "";
                seperator = "";

                flag = false;

                if (DataPresent) {
                    CurrnetFieldValue = JsonData.Data1[k][v.ID]; //assign value to the filed
                    CurrnetFieldMasteID = JsonData.Data1[k]["ID"];
                    CurrentControlID = v.ID + "_" + JsonData.Data1[k]["ID"];

                }
                else {
                    CurrnetFieldValue = v.value; //assign value to the filed
                    CurrnetFieldMasteID = "#" + k;
                    CurrentControlID = v.ID + "_" + "#" + k;
                }

                if (pattern != "") {
                    Customfuncall = "funcCall[validateit]";
                    flag = true
                }
                if (required != "") {
                    flag = true
                }
                if (pattern != "" && required != "") {
                    seperator = ",";
                    flag = true;
                }

                if (flag) {
                    addValdationClass = " validate[" + required + seperator + Customfuncall + "] ";
                }


                if (v.Type == "select") {
                    var arsr = v.DataSource.split(',');
                    var str = "<option value=''>Select</option>";
                    if (CurrnetFieldValue == true) {
                        CurrnetFieldValue = 1;
                    }
                    if (CurrnetFieldValue == false) {
                        CurrnetFieldValue = 0;
                    }
                    for (var z = 0; z < arsr.length; z++) {

                        if (CurrnetFieldValue == Number(arsr[z].split('-')[0])) {
                            str += " <option  selected = 'selected' value='" + arsr[z].split('-')[0] + "'>" + arsr[z].split('-')[1] + "</option>";
                        }
                        else {
                            str += " <option  value='" + arsr[z].split('-')[0] + "'>" + arsr[z].split('-')[1] + "</option>";
                        }
                    }
                    row1 += "<td><select name='RenderedControl'  title='" + v.ToolTip + "'  FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "'  id='" + CurrentControlID + "' class='" + "sders" + "'>" + str + " </select></td>";
                }
                else {

                    row1 += "<td><input name='RenderedControl' title='" + v.ToolTip + "' FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "'   ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='" + CurrentControlID + "' class='inputbox " + addValdationClass + "' value='" + CurrnetFieldValue + "'/></td>";
                }

                row0 += "<th> <span class='ColumnHeader'>" + v.Label + " </span></th>";
                Header = "<tr>" + "<th></th>" + row0 + "</tr>";

            });
            removerow = "<td><a name='RenderedButton' id='RemRow_" + k + "' class='RemRow' onclick='RemoveRow(this); return false;'><img width='16' height='16' border='0' title='Delete Row' src='images/cross.png'></a></td>";
            InnerControls += "<tr id='row_" + k + "'>" + removerow + row1 + "</tr>";

        }
        $(Bucket).html("");
        $(Bucket).prepend(tb + Header + InnerControls + "</table>");


    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }

}

function AddNewRowinGrid(JsonData, Bucket, Count) {
    var ErrorCode = "SS23VS00NB00";
    try {


        var xx = "";
        var rows = "";
        var Customfuncall;
        var pattern = ""
        var required = ""
        var addValdationClass = "";
        var seperator = "";
        var flag = false;
        var label = "";
        var row0 = "";
        var row1 = ""
        var Header = "";
        var InnerControls = "";
        var TotalInnerControls = 1;
        var CurrnetFieldValue = "";
        var CurrnetFieldMasteID = "";
        var CurrentControlID = "";
        var removerow = "";



        CurrnetFieldValue = "";
        CurrnetFieldMasteID = "";
        $.each(JsonData.Data0, function (i, v) {
            pattern = v.Pattern;
            required = v.ValReq;
            Customfuncall = "";
            addValdationClass = "";
            seperator = "";

            flag = false;


            CurrnetFieldValue = v.value; //assign value to the filed
            CurrnetFieldMasteID = "#" + Count;
            CurrentControlID = v.ID + "_" + "#" + Count;
            Count++;

            if (pattern != "") {
                Customfuncall = "funcCall[validateit]";
                flag = true
            }
            if (required != "") {
                flag = true
            }
            if (pattern != "" && required != "") {
                seperator = ",";
                flag = true;
            }

            if (flag) {
                addValdationClass = " validate[" + required + seperator + Customfuncall + "] ";
            }

            if (v.Type == "select") {
                var arsr = v.DataSource.split(',');
                var str = "<option value=''>Select</option>";
                for (var z = 0; z < arsr.length; z++) {
                    str += " <option value='" + arsr[z].split('-')[0] + "'>" + arsr[z].split('-')[1] + "</option>";
                }
                row1 += "<td><select name='RenderedControl' title='" + v.ToolTip + "'  FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "'  id='" + CurrentControlID + "' class='" + "sders" + "'>" + str + " </select></td>";
            }
            else {

                row1 += "<td><input name='RenderedControl' title='" + v.ToolTip + "' FieldMasterID='" + v.FieldMasterID + "' RecordMasterID='" + CurrnetFieldMasteID + "'   ValMsg='" + v.ValidationMessage + "' reg='" + v.Pattern + "' type='" + v.Type + "' id='" + CurrentControlID + "' class='inputbox " + addValdationClass + "' value='" + CurrnetFieldValue + "'/></td>";
            }

            row0 += "<th> </th><th> <span class='ColumnHeader'>" + v.Label + " </span></th>";
            Header = "<tr>" + row0 + "</tr>";

        });
        removerow = "<td><a name='RenderedButton' id='RemRow_" + Count + "' class='RemRow' onclick='RemoveRow(this); return false;'><img width='16' height='16' border='0' title='Delete Row' src='images/cross.png'></a></td>";

        InnerControls += "<tr id='row_" + Count + "'>" + removerow + row1 + "</tr>";


        $(Bucket).append(InnerControls);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }


}

function getMultiSelectValues(select) {
    var ErrorCode = "SS24VS00NB00";
    try {

        var result = "";
        var options = select && select.options;
        var opt;

        for (var i = 0, iLen = options.length; i < iLen; i++) {
            opt = options[i];

            if (opt.selected) {
                result += opt.value.trim() + ",";
            }
        }
        return result.slice(0, -1);
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function GetDataFromContainer(container) {
    var ErrorCode = "SS25VS00NB00";
    try {

        var obj = {};
        var Comments = "";

        for (var i = 0; i < container.find("[name='RenderedControl']").length; i++) {
            var myObject2 = {};
            var RecordMasterID = $(container.find("[name='RenderedControl']")[i]).attr('recordmasterid');
            var value = "";
            if (i == 0) {


                if (RecordMasterID.indexOf('#') >= 0) {
                    RecordMasterID = "";
                }
                myObject2.ClientID = GLB_ClientID;
                myObject2.RateRequestID = GLB_RateRequestID;
                myObject2.TransportModeID = GLB_TransPortModeID;
                myObject2.MasterID = RecordMasterID;
            }
            if (container.find("[name='RenderedControl']")[i].type == 'select-multiple') {
                value = getMultiSelectValues(container.find("[name='RenderedControl']")[i]);
            }
            else {
                value = $(container.find("[name='RenderedControl']")[i]).attr('value').trim();
            }
            var FieldMasterID = $(container.find("[name='RenderedControl']")[i]).attr('FieldMasterID').trim();
            if (value === "undefined") {
                value = "";
            }
            if (value != "") {
                myObject2[$(container.find("[name='RenderedControl']")[i]).attr('id').split('_')[2]] = value;
                obj[i] = myObject2;
            }

        }


        return obj;
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }

}
//For ShipmentAddresses
function GetDataFromContainerSpecialCase(container) {
    var ErrorCode = "SS26VS00NB00";
    try {


        var obj = {};
        for (var i = 0; i < container.find("[name='RenderedControl']").length; i++) {
            var myObject2 = {};
            var ValidRecord = true;
            var RecordMasterID = $(container.find("[name='RenderedControl']")[i]).attr('recordmasterid');
            var value = $(container.find("[name='RenderedControl']")[i]).attr('value').trim();
            var FieldMasterID = $(container.find("[name='RenderedControl']")[i]).attr('FieldMasterID').trim();
            if (value === "undefined") {
                value = "";
            }

            if (RecordMasterID.indexOf('#') >= 0) {
                RecordMasterID = "";
            }
            //        if (RecordMasterID.trim() === "" && value.trim() === "") {
            //            ValidRecord = false;
            //        }
            if (ValidRecord) {

                myObject2.ClientID = GLB_ClientID;
                myObject2.RateRequestID = GLB_RateRequestID;
                myObject2.TransportModeID = GLB_TransPortModeID;

                myObject2.MasterID = RecordMasterID;
                myObject2.FieldMasterID = FieldMasterID;
                myObject2.Data = value;
                obj[i] = myObject2
            }
        }

        return obj;
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function GetDataFromContainerGridCase(container) {
    var ErrorCode = "SS27VS00NB00";
    try {

        var obj = {};
        $.each($("[id^='row_']"), function (key, val) {
            var myObject2 = {};
            $.each($(this).find("[name='RenderedControl']"), function (key1, val) {



                var ValidRecord = true;
                var RecordMasterID = $(val).attr('recordmasterid');
                var value = $(val).attr('value').trim();
                var FieldMasterID = $(val).attr('FieldMasterID').trim();
                if (value === "undefined") {
                    value = "";
                }

                if (RecordMasterID.indexOf('#') >= 0) {
                    RecordMasterID = "";
                }
                if (RecordMasterID.trim() === "" && value.trim() === "") {
                    ValidRecord = false;
                }
                if (ValidRecord) {
                    if (key1 == 0) {
                        myObject2.ClientID = GLB_ClientID;
                        myObject2.RateRequestID = GLB_RateRequestID;
                        myObject2.TransportModeID = GLB_TransPortModeID;

                        myObject2.MasterID = RecordMasterID;
                    }
                    myObject2[$(val).attr('id').split('_')[2]] = value;

                }


            });
            obj[key] = myObject2

        });
        return obj;
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
    }
}

function GetDataFromRatesContainer(container) {

    var ErrorCode = "SS28VS00NB00";
    try {

        var obj = {};
        var NextRateID = "0";
        var myObject2 = {};
        var value = "";
        var checkRange = false;
        for (var i = 0; i < container.find("[name='RenderedControl']").length; i++) {
            checkRange = false;
            var RecordMasterID = $(container.find("[name='RenderedControl']")[i]).attr('recordmasterid');
            var RateID = $(container.find("[name='RenderedControl']")[i]).attr('rateid');
            NextRateID = $(container.find("[name='RenderedControl']")[i + 1]).attr('rateid');

            if (RecordMasterID.indexOf('#') >= 0) {
                RecordMasterID = "";
            }

            if (container.find("[name='RenderedControl']")[i].type == "select-one") {

                if (container.find("[name='RenderedControl']")[i].className == 'RangeSelector') {
                    value = $($(container.find("[name='RenderedControl']")[i])).find(":selected").text();
                    if (value !== 'Select') {
                        checkRange = true;
                    }
                    else {
                        value = "";
                    }

                }
                else {

                    value = $($(container.find("[name='RenderedControl']")[i])).find(":selected").val();
                }


            }
            else {

                value = $(container.find("[name='RenderedControl']")[i]).val();
            }

            if (value === "undefined") {
                value = "";
            }
            if (value != "") {
                myObject2[$(container.find("[name='RenderedControl']")[i]).attr('id').split('_')[0]] = value;
            }

            if (checkRange) {

                $.each($("[id^='" + value + "_']"), function (key, ob) {

                    var val = "";

                    $.each($(ob).find("[name='RenderedControl']"), function (key1, val1) {
                        var intervalend = $(val1).attr('intervalend');
                        var intervalstart = $(val1).attr('intervalstart');
                        var intervaltext = $(val1).attr('intervaltext');
                        var fieldindex = $(val1).attr('fieldindex');
                        val = $(val1).val();

                        if (val === "undefined") {
                            val = "";
                        }
                        if (val != "") {
                            myObject2[$(val1).attr('id').split('_')[0]] = val;
                            myObject2["IntervalEnd" + fieldindex] = intervalend;
                            myObject2["IntervalStart" + fieldindex] = intervalstart;
                            myObject2["IntervalText" + fieldindex] = intervaltext;
                        }
                    });
                });
            }


            if (NextRateID !== RateID) {

                myObject2.RateRequestID = GLB_RateRequestID;
                myObject2.MasterID = RecordMasterID;
                myObject2.RateID = RateID
                obj[RateID] = myObject2;
                myObject2 = {};
                LastRateID = RateID;

            }
        }

        return obj;
    }
    catch (err) {
        ShowNotification("Please try again later...", "Operation was not succssfull<br/> Code: " + ErrorCode, 4000);
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
    RRAW.DynamicFieldsControl.AddAttachments(Number(GLB_RateRequestID), uploadedFiles, AttachmentSaved, AttachmentSaved);
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


