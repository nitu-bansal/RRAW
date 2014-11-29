/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />
/// <reference path="Tabs.min.js" />

var userDetails;
var userTypes;
var modules;
var userModuleAccessDetails;
var selectedUser;
var selectedUserId;

var posUserID = 0;
var posUserPassword = 1;
var posUserName = 2;
var posUserEmail = 3;
var posUserApproverID = 4;
var posUserTypeID = 5;
var posUserStation = 6;
var posUserCountry = 7;
var posUserRegion = 8;
var posUserEnabled = 9;

$(document).ready(function () {
    $("#lstUsers").focus();

    RefillAllData();
})

function RefillAllData() {
    RRAW.WebServices.Authentications.GetAllInitialDetails(getCookie("AuthenticationToken"), onSuccessOfAllInitialDetails);

    //RRAW.WebServices.Authentications.GetAllUserDetails(onSuccessOfGetUserDetails);
    //RRAW.WebServices.Authentications.GetAllUserModuleAccessDetails(onSuccessOfGetAllUserModuleAccessDetails);
    //RRAW.WebServices.Authentications.GetAllUserTypes(onSuccessOfGetAllUserTypes);
    //RRAW.WebServices.Authentications.GetAllModules(onSuccessOfGetAllModules);
}

function onSuccessOfAllInitialDetails(res) {
    if (res["AuthenticationError"] != undefined) {
        window.location = res["AuthenticationError"][0];
    }
    onSuccessOfGetUserDetails(res['GetAllUserDetails']);
    onSuccessOfGetAllUserTypes(res['GetAllUserTypes']);
    onSuccessOfGetAllModules(res['GetAllModules']);
    onSuccessOfGetAllUserModuleAccessDetails(res['GetAllUserModuleAccessDetails']);
}

function onSuccessOfGetUserDetails(res) {
    userDetails = res;
    FillUserDetails();

    if (selectedUserId != undefined) {
        $("#lstUsers").val(selectedUserId);
        selectedUser = userDetails[selectedUserId];
        DisplayCurrentUserDetails();
    }
}

function onSuccessOfGetAllUserTypes(res) {
    userTypes = res;
    FillUserTypes();
}

function onSuccessOfGetAllModules(res) {
    modules = res;
    FillModules();
}

function onSuccessOfGetAllUserModuleAccessDetails(res) {
    userModuleAccessDetails = res;

    if (selectedUserId != undefined) {
        FillUserModuleAccessDetails();
    }
}

function FillUserDetails() {
    $("#lstUsers").children().remove();
    $("#cmbNewUserApprovers").children().remove();
    $("#cmbUpdateUserApprovers").children().remove();

    $.each(userDetails, function (key, val) {
        $('#lstUsers').append($("<option></option>").attr("value", key).text(val[posUserName]));
        $('#cmbNewUserApprovers').append($("<option></option>").attr("value", key).text(val[posUserName]));
        $('#cmbUpdateUserApprovers').append($("<option></option>").attr("value", key).text(val[posUserName]));
    });

    $('#cmbNewUserApprovers').append($("<option></option>").attr("value", 999).text('Self'));
    $('#cmbUpdateUserApprovers').append($("<option></option>").attr("value", 999).text('Self'));

    SortItems($("#lstUsers"));
    SortItems($("#cmbNewUserApprovers"));
    SortItems($("#cmbUpdateUserApprovers"));
}

function FillUserTypes() {
    $("#cmbUpdateUserTypes").children().remove();
    $("#cmbNewUserTypes").children().remove();

    $.each(userTypes, function (key, val) {
        $('#cmbUpdateUserTypes').append($("<option></option>").attr("value", key).text(val));
        $('#cmbNewUserTypes').append($("<option></option>").attr("value", key).text(val));
    });
}

function FillModules() {
    $("#divModules").children().remove();

    $("#divModules").children().remove();
    $("#divModules").append("<div><div style='font-weight:bold;margin-bottom: 15px;color:#800000'>New Users Access Format (effectives 15th Jul 2012):</div><table  id='tblNew' width='100%'  cellpadding='0' cellspacing='0'><thead><tr style='background-color: activeborder;font-size: 14px;font-weight: bold;'><td style='padding-left:22px'>Modules</td><td>Scope</td></thead></table></div>");
    $("#divModules").append("<div><div style='height:30px;font-weight:bold;margin-top: 15px;'>Old Tariff and Old rate request (Jan'10 - 14 Jul'12):</div><table  id='tblOld' width='100%' cellpadding='0' cellspacing='0'></table></div>");
    var i = 0;
    var dvTable = "", Color = "";
    $.each(modules, function (key, val) {
        //alert(key.toString().split('_')[1]);
        if (key.toString().split('_')[1] == 0) {
            dvTable = "tblNew";
            Color = "#800000"; //1001FD
        }
        else {
            dvTable = "tblOld";
            Color = "#000";
        }
        //alert(dvTable);
        if (i == 0) {
            $("#" + dvTable).append("<tr style='border:1px solid #DAE3F6;background-color:#EFF3FB;color:" + Color + ";height:25px;'><td><input type='checkbox' id='chk_" + val.toString().split(',')[0] + "' /><label id='lbl_" + val.toString().split(',')[0] + "' for='chk_" + val.toString().split(',')[0] + "'>" + val.toString().split(',')[1] + "</label></td><td>" + key.toString().split('_')[2] + "</td></tr>");
            i = 1;
        }
        else {
            $("#" + dvTable).append("<tr style='border:1px solid #DAE3F6;color:" + Color + ";height:25px;'><td><input type='checkbox' id='chk_" + val.toString().split(',')[0] + "' /><label id='lbl_" + val.toString().split(',')[0] + "' for='chk_" + val.toString().split(',')[0] + "'>" + val.toString().split(',')[1] + "</label></td><td>" + key.toString().split('_')[2] + "</td></tr>");
            i = 0;
        }

    });
}

function FillUserModuleAccessDetails() {
    $.each(modules, function (key, val) {
        $("#chk_" + val.toString().split(',')[0]).prop("checked", false);
    });

    if (userModuleAccessDetails[selectedUserId] != undefined) {
        $.each(userModuleAccessDetails[selectedUserId], function () {
            $("#chk_" + this).prop("checked", true);
        });
    }
}

function SortItems(listObject) {
    var listItems = listObject.children('option').get();
    listItems.sort(function (a, b) {
        var compA = $(a).text().toUpperCase();
        var compB = $(b).text().toUpperCase();
        return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
    })
    $.each(listItems, function (idx, itm) { listObject.append(itm); });
}

$("#lstUsers").bind("change", function () {
    //Select user
    selectedUserId = $("#lstUsers").val()[0];
    selectedUser = userDetails[selectedUserId];

    DisplayCurrentUserDetails();

    document.getElementById('lbl_newuser_status').innerHTML = '';
    document.getElementById('lbl_useraccess_status').innerHTML = '';
    document.getElementById('lbl_removeuser_status').innerHTML = '';
    document.getElementById('lbl_setpass_status').innerHTML = '';
    document.getElementById('lbl_userdetials_status').innerHTML = '';
});

function DisplayCurrentUserDetails() {
    //Set titles
    document.getElementById('lbl_pass_user').innerHTML = selectedUser[posUserName];
    document.getElementById('lbl_module_user').innerHTML = selectedUser[posUserName];
    document.getElementById('lbl_del_user').innerHTML = selectedUser[posUserName];
    document.getElementById('lblUserDetails').innerHTML = selectedUser[posUserName];

    //Set approver for current user
    $("#cmbNewUserApprovers").val(selectedUserId);

    //Display user details
    DisplayUserDetails();

    FillUserModuleAccessDetails();
}

//Display user details
function DisplayUserDetails() {
    DisableFieldsToUpdate(true);

    $("#txtUpdateUserName").val(selectedUser[posUserName]);
    $("#txtUpdateUserID").val(selectedUser[posUserID]);
    $("#txtUpdateUserEmail").val(selectedUser[posUserEmail]);
    $("#cmbUpdateUserApprovers").val(selectedUser[posUserApproverID]);
    $("#cmbUpdateUserTypes").val(selectedUser[posUserTypeID]);
    $("#txtUpdateUserStation").val(selectedUser[posUserStation]);
    $("#txtUpdateUserCountry").val(selectedUser[posUserCountry]);
    $("#txtUpdateUserRegion").val(selectedUser[posUserRegion]);
    $("#chkUpdateUserEnabled").prop("checked", (selectedUser[posUserEnabled] == 'True' ? true : false));
}

//Enable fields to update
function DisableFieldsToUpdate(isDisabled) {
    document.getElementById('btnUpdateUserDetails').disabled = isDisabled;
    document.getElementById('txtUpdateUserID').disabled = isDisabled;
    document.getElementById('txtUpdateUserName').disabled = isDisabled;
    document.getElementById('txtUpdateUserEmail').disabled = isDisabled;
    document.getElementById('cmbUpdateUserApprovers').disabled = isDisabled;
    document.getElementById('cmbUpdateUserTypes').disabled = isDisabled;
    document.getElementById('txtUpdateUserRegion').disabled = isDisabled;
    document.getElementById('txtUpdateUserStation').disabled = isDisabled;
    document.getElementById('txtUpdateUserCountry').disabled = isDisabled;
    document.getElementById('chkUpdateUserEnabled').disabled = isDisabled;

    if (isDisabled == true) {
        $("#btnUpdateUserDetails").css("display", "none");
        $("#btnEdit").css("display", "inline");
    }
    else {
        $("#btnUpdateUserDetails").css("display", "inline");
        $("#btnEdit").css("display", "none");
    }
}

$("#btnEdit").bind("click", function () {
    if (document.getElementById('lstUsers').selectedIndex >= 0) {
        DisableFieldsToUpdate(false)
    }
    else {
        document.getElementById('lbl_userdetials_status').style.color = '#f00';
        document.getElementById('lbl_userdetials_status').innerHTML = 'Please Select User First';
    }
});

function btnUpdateUserAccess_Click() {
}

$("#btnRemoveUser").bind("click", function () {
    if ((selectedUser == undefined) || (selectedUserId == undefined)) {
        alert("Please select user to remove.");
    }
    else {
        if (confirm('Are you sure you want to remove "' + selectedUser[posUserName] + '" from RRAW Portal?') == true) {
            $("#lbl_removeuser_status").css("color", "#000");
            $("#lbl_removeuser_status").html('Removing User...');

            RRAW.WebServices.Authentications.RemoveUser(selectedUserId, onSuccessOfRemoveUser);
        }
        else {
            return false;
        }
    }
});

function onSuccessOfRemoveUser(res) {
    if (res == 1) {
        //RefillAllData();
        $($("[value='" + selectedUserId + "']")[0]).remove();
        $($("[value='" + selectedUserId + "']")[0]).remove();
        $($("[value='" + selectedUserId + "']")[0]).remove();

        selectedUserId = undefined;

        $("#lbl_pass_user").html('(Selected User)');
        $("#lbl_module_user").html('(Selected User)');
        $("#lbl_del_user").html('(Selected User)');
        $("#lblUserDetails").html('(Selected User)');

        $("#lbl_useraccess_status").html("");
        $("#lbl_setpass_status").html("");
        $("#lbl_userdetials_status").html("");

        //Clean up user access details
        $.each(modules, function (key, val) {
            $("#chk_" + key).prop("checked", false);
        });

        $("#txtUpdateUserName").val("");
        $("#txtUpdateUserID").val("");
        $("#txtUpdateUserEmail").val("");
        $("#txtUpdateUserStation").val("");
        $("#txtUpdateUserCountry").val("");
        $("#txtUpdateUserRegion").val("");
        $("#chkUpdateUserEnabled").val("");

        $("#lbl_removeuser_status").css("color", "#00f");
        $("#lbl_removeuser_status").html('User "' + selectedUser[posUserName] + '" removed successfully from RRAW Portal.');
    }
    else {
        $("#lbl_removeuser_status").css("color", "#f00");
        $("#lbl_removeuser_status").html('An unexpected error has been occured while removing user "' + selectedUser[posUserName] + '". Please reload the page and try again.');
    }
}

$("#btnResetPassword").bind("click", function () {
    if (selectedUserId == undefined) {
        alert("Please select user to reset password.");
    }
    else {
        if ($("#txtNewPassword").val() == "") {
            $("#lbl_setpass_status").css("color", "#f00");
            $("#lbl_setpass_status").html('Please fill in New Password.');
            $("#txtNewPassword").focus();
        }
        else if ($("#txtConfirmNewPassword").val() == "") {
            $("#lbl_setpass_status").css("color", "#f00");
            $("#lbl_setpass_status").html('Please fill in Confirm New Password.');
            $("#txtConfirmNewPassword").focus();
        }
        else if ($("#txtNewPassword").val() != $("#txtConfirmNewPassword").val()) {
            $("#lbl_setpass_status").css("color", "#f00");
            $("#lbl_setpass_status").html('New Password and Confirm New Password must be same.');
            $("#txtNewPassword").val("");
            $("#txtConfirmNewPassword").val("");
            $("#txtNewPassword").focus();
        }
        else {
            $("#btnResetPassword").prop("disabled", "disabled");
            $("#lbl_setpass_status").css("color", "#000");
            $("#lbl_setpass_status").html('Resetting password...');

            RRAW.WebServices.Authentications.ResetPassword(selectedUserId, $("#txtNewPassword").val(), onSuccessOfResetPassword);
        }
    }
});

function onSuccessOfResetPassword(res) {
    if (res == 1) {
        $("#btnResetPassword").prop("disabled", "");
        $("#txtNewPassword").val("");
        $("#txtConfirmNewPassword").val("");

        $("#lbl_setpass_status").css("color", "#00f");
        $("#lbl_setpass_status").html('New password has been successfully applied. <a href="Login.aspx" target="_parent">Test by Login</a>');

        $("#txtNewPassword").focus();
    }
    else {
        $("#lbl_setpass_status").css("color", "#f00");
        $("#lbl_setpass_status").html('An unexpected error has been occured while applying new password to user "' + selectedUser[posUserName] + '". Please reload the page and try again.');
    }
}

$("#btnCreateUser").bind("click", function () {
    if ($("#txtNewUserName").val() == "") {
        $("#lbl_newuser_status").css("color", "#f00");
        $("#lbl_newuser_status").html('Please fill in User Name.');
        $("#txtNewUserName").focus();
    }
    else if ($("#txtNewUserID").val() == "") {
        $("#lbl_newuser_status").css("color", "#f00");
        $("#lbl_newuser_status").html('Please fill in User ID.');
        $("#txtNewUserID").focus();
    }
    else if ($("#txtNewUserPassword").val() == "") {
        $("#lbl_newuser_status").css("color", "#f00");
        $("#lbl_newuser_status").html('Please fill in Password.');
        $("#txtNewUserPassword").focus();
    }
    else if ($("#txtNewUserEmail").val() == "") {
        $("#lbl_newuser_status").css("color", "#f00");
        $("#lbl_newuser_status").html('Please fill in Email Address.');
        $("#txtNewUserEmail").focus();
    }
    else {
        $("#btnCreateUser").prop("disabled", "disabled");
        $("#lbl_newuser_status").css("color", "#000");
        $("#lbl_newuser_status").html('Creating new user...');

        RRAW.WebServices.Authentications.CreateUser($("#txtNewUserID").val(), $("#txtNewUserName").val(), $("#txtNewUserPassword").val(), $("#txtNewUserEmail").val(), $("#cmbNewUserApprovers").val(), $("#cmbNewUserTypes").val(), $("#txtNewUserStation").val(), $("#txtNewUserCountry").val(), $("#txtNewUserRegion").val(), $("#chkNewUserEnabled").prop("checked"), onSuccessOfCreateUser);
    }
});

function onSuccessOfCreateUser(res) {
    //if (res == 1) {

    selectedUserId = res;

    var userDetailNode = new Array();
    userDetailNode[0] = $("#txtNewUserID").val();
    userDetailNode[1] = "";
    userDetailNode[2] = $("#txtNewUserName").val();
    userDetailNode[3] = $("#txtNewUserEmail").val();
    userDetailNode[4] = $("#cmbNewUserApprovers").val();
    userDetailNode[5] = $("#cmbNewUserTypes").val();
    userDetailNode[6] = $("#txtNewUserStation").val();
    userDetailNode[7] = $("#txtNewUserCountry").val();
    userDetailNode[8] = $("#txtNewUserRegion").val();
    userDetailNode[9] = $("#chkNewUserEnabled").prop("checked") == true ? "True" : "False";

    userDetails[selectedUserId] = userDetailNode;

    $("#btnCreateUser").removeAttr("disabled");
    $("#txtNewUserName").val("");
    $("#txtNewUserID").val("");
    $("#txtNewUserPassword").val("");
    $("#txtNewUserEmail").val("");
    $("#txtNewUserStation").val("");
    $("#txtNewUserCountry").val("");
    $("#txtNewUserRegion").val("");
    $("#chkNewUserEnabled").val("");

    selectMe("stripUserAdmin", document.getElementById("tabUserDetails"));

    $('#lstUsers').append($("<option></option>").attr("value", res).text(userDetails[selectedUserId][posUserName]));
    $('#cmbNewUserApprovers').append($("<option></option>").attr("value", res).text(userDetails[selectedUserId][posUserName]));
    $('#cmbUpdateUserApprovers').append($("<option></option>").attr("value", res).text(userDetails[selectedUserId][posUserName]));
    SortItems($("#lstUsers"));
    SortItems($("#cmbNewUserApprovers"));
    SortItems($("#cmbUpdateUserApprovers"));

    $("#lstUsers").val(selectedUserId);
    selectedUser = userDetails[selectedUserId];

    DisplayCurrentUserDetails();

    $("#lbl_newuser_status").html("");
    $("#lbl_userdetials_status").css("color", "#00f");
    $("#lbl_userdetials_status").html("New user has been created successfully. Please verify above details and <a href='#' onclick=\"selectMe('stripUserAdmin', document.getElementById('tabUserAccess'))\">visit User Access page to apply module access</a>.");

    /*}
    else {
    $("#lbl_newuser_status").css("color", "#f00");
    $("#lbl_newuser_status").html('An unexpected error has been occured while creating new user. Please reload the page and try again.');
    }*/
}

$("#btnUpdateUserDetails").bind("click", function () {
    if ($("#txtUpdateUserName").val() == "") {
        $("#lbl_userdetials_status").css("color", "#f00");
        $("#lbl_userdetials_status").html('Please fill in User Name.');
        $("#txtUpdateUserName").focus();
    }
    else if ($("#txtUpdateUserID").val() == "") {
        $("#lbl_userdetials_status").css("color", "#f00");
        $("#lbl_userdetials_status").html('Please fill in User ID.');
        $("#txtUpdateUserID").focus();
    }
    else if ($("#txtUpdateUserPassword").val() == "") {
        $("#lbl_userdetials_status").css("color", "#f00");
        $("#lbl_userdetials_status").html('Please fill in Password.');
        $("#txtUpdateUserPassword").focus();
    }
    else if ($("#txtUpdateUserEmail").val() == "") {
        $("#lbl_userdetials_status").css("color", "#f00");
        $("#lbl_userdetials_status").html('Please fill in Email Address.');
        $("#txtUpdateUserEmail").focus();
    }
    else {
        $("#btnCreateUser").prop("disabled", "disabled");
        $("#lbl_userdetials_status").css("color", "#000");
        $("#lbl_userdetials_status").html('Updating user details...');

        RRAW.WebServices.Authentications.UpdateUserDetails(selectedUserId, $("#txtUpdateUserID").val(), $("#txtUpdateUserName").val(), $("#txtUpdateUserEmail").val(), $("#cmbUpdateUserApprovers").val(), $("#cmbUpdateUserTypes").val(), $("#txtUpdateUserStation").val(), $("#txtUpdateUserCountry").val(), $("#txtUpdateUserRegion").val(), $("#chkUpdateUserEnabled").prop("checked"), onSuccessOfUpdateUserDetails);
    }
});

function onSuccessOfUpdateUserDetails(res) {
    //if (res == 1) {

    $("#lbl_userdetials_status").css("color", "#00f");
    $("#lbl_userdetials_status").html('User details has been updated successfully.');

    //RefillAllData();
    var userDetailNode = new Array();
    userDetailNode[0] = $("#txtUpdateUserID").val();
    userDetailNode[1] = userDetails[selectedUserId][posUserPassword];
    userDetailNode[2] = $("#txtUpdateUserName").val();
    userDetailNode[3] = $("#txtUpdateUserEmail").val();
    userDetailNode[4] = $("#cmbUpdateUserApprovers").val();
    userDetailNode[5] = $("#cmbUpdateUserTypes").val();
    userDetailNode[6] = $("#txtUpdateUserStation").val();
    userDetailNode[7] = $("#txtUpdateUserCountry").val();
    userDetailNode[8] = $("#txtUpdateUserRegion").val();
    userDetailNode[9] = $("#chkUpdateUserEnabled").prop("checked") == true ? "True" : "False";

    userDetails[selectedUserId] = userDetailNode;

    $($("[value='" + selectedUserId + "']")[0]).remove();
    $($("[value='" + selectedUserId + "']")[0]).remove();
    $($("[value='" + selectedUserId + "']")[0]).remove();

    $('#lstUsers').append($("<option></option>").attr("value", selectedUserId).text(userDetails[selectedUserId][posUserName]));
    $('#cmbNewUserApprovers').append($("<option></option>").attr("value", selectedUserId).text(userDetails[selectedUserId][posUserName]));
    $('#cmbUpdateUserApprovers').append($("<option></option>").attr("value", selectedUserId).text(userDetails[selectedUserId][posUserName]));
    SortItems($("#lstUsers"));
    SortItems($("#cmbNewUserApprovers"));
    SortItems($("#cmbUpdateUserApprovers"));

    $("#lstUsers").val(selectedUserId);

    /*}
    else {
    $("#lbl_userdetials_status").css("color", "#f00");
    $("#lbl_userdetials_status").html('An unexpected error has been occured while creating new user. Please reload the page and try again.');
    }*/
}

$("#btnUpdateUserAccess").bind("click", function () {
    if (selectedUserId == undefined) {
        alert("Please select user to update access details.");
    }
    else {
        $("#btnUpdateUserAccess").prop("disabled", "disabled");
        $("#lbl_useraccess_status").css("color", "#000");
        $("#lbl_useraccess_status").html('Updating user access details...');

        var moduleIDs = new Array();
        var j = 0;
        for (i = 0; i < 100; i++) {
            if ($("#chk_" + i).prop("checked") == true) {
                moduleIDs[j] = i;
                j++;
            }
        }
        RRAW.WebServices.Authentications.UpdateUserAccess($("#hidCurrentUserID").val(), selectedUserId, moduleIDs, onSuccessOfUpdateUserAccess);
    }
});

function onSuccessOfUpdateUserAccess(res) {
    //if (res == 1) {

    $("#btnUpdateUserAccess").removeAttr("disabled");

    $("#lbl_useraccess_status").css("color", "#00f");
    $("#lbl_useraccess_status").html('User access details has been updated successfully. <a href="Login.aspx" target="_parent">Test by Login</a>');

    //RefillAllData();
    var userModeAccessDetailsNode = new Array();
    for (i = 0; i < 100; i++) {
        if ($("#chk_" + i).prop("checked") == true) {
            userModeAccessDetailsNode.push(i);
        }
    }

    userModuleAccessDetails[selectedUserId] = userModeAccessDetailsNode;

    /*}
    else {
    $("#lbl_useraccess_status").css("color", "#f00");
    $("#lbl_useraccess_status").html('An unexpected error has been occured while creating new user. Please reload the page and try again.');
    }*/
}

function txtUserName_blur() {
    document.getElementById("txtNewUserID").value = document.getElementById("txtNewUserName").value.trim().toLowerCase().replace(" ", ".");
    document.getElementById("txtNewUserEmail").value = document.getElementById("txtNewUserID").value.trim() + "@cevalogistics.com";
    document.getElementById("txtNewUserID").select();
}

function getCookie(c_name) {
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