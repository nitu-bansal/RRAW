<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminPanel.aspx.vb" Inherits="RRAW.AdminPanel"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Tabs.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/AdminPanel.min.css" rel="stylesheet" type="text/css" />
    <title>Admin Panel</title>
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Authentications.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="hidCurrentUserID" runat="server" />
    <div style="margin-left: 0; width: 1135px; height: 548px; background: none repeat scroll 0 0 #fff;
        box-shadow: 0 0 3px #B6B7BB; border: 1px solid #D6D6D6;">
        <div style="width: 200px; height: 507px; background: none repeat scroll 0 0 #F4F5F8;
            box-shadow: 0 0 3px #B6B7BB; display: inline-block; float: left; padding: 20px;">
            <span class="page_title">Select User</span>
            <br />
            <br />
            <br />
            <div style="background: none repeat scroll 0 0 #F4F5F8; box-shadow: 0 0 3px #B6B7BB;
                height: 475px">
                <select id="lstUsers" multiple="multiple" style="height: 100%; width: 100%" class="list">
                </select>
            </div>
        </div>
        <div id='container' style="width: 889px; height: 546px;" class="container">
            <div id="stripUserAdmin">
                <a id="tabNewUser" class="tab tab_selected" onclick="selectMe('stripUserAdmin', this);document.getElementById('txtNewUserName').focus()">
                    Create New User</a> <a id="tabUserAccess" class="tab" onclick="selectMe('stripUserAdmin', this);document.getElementById('divModules').focus()">
                        User Access</a> <a id="tabRemoveUser" class="tab" onclick="selectMe('stripUserAdmin', this);document.getElementById('btnRemoveUser').focus()">
                            Remove User</a> <a id="tabSetPassword" class="tab" onclick="selectMe('stripUserAdmin', this);document.getElementById('txtNewPassword').focus()">
                                Reset Password</a> <a id="tabUserDetails" class="tab" onclick="selectMe('stripUserAdmin', this);document.getElementById('btnEdit').focus()">
                                    User Details</a>
            </div>
            <div id="contentsMaster" style="width: auto; padding: 20px">
                <div id="contentNewUser" style="display: block;">
                    <input type="hidden" id="hidCurrentDateTime" />
                    <div class="page_title">
                        Create New User</div>
                    <br />
                    <hr />
                    Fill up required information and create new user<hr />
                    <br />
                    <table>
                        <tr>
                            <td>
                                User Name:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserName" maxlength="50" onblur="txtUserName_blur();"
                                    style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User ID:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserID" maxlength="50" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Password:
                            </td>
                            <td>
                                <input type="password" id="txtNewUserPassword" maxlength="50" style="width: 150px" />
                                <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic"
                                    ControlToValidate="txtNewUserPassword" ErrorMessage="Minimum 5 characters required"
                                    ValidationExpression="[\w\s]{5,50}"></asp:RegularExpressionValidator>-->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserEmail" maxlength="255" style="width: 225px" />
                                <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtNewUserEmail"
                                    Display="Dynamic" ErrorMessage="Invalid Email" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>-->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Approving Manager:
                            </td>
                            <td>
                                <select id="cmbNewUserApprovers">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Type:
                            </td>
                            <td>
                                <select id="cmbNewUserTypes">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Station:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserStation" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserCountry" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Region:
                            </td>
                            <td>
                                <input type="text" id="txtNewUserRegion" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Enabled:
                            </td>
                            <td>
                                <input type="checkbox" id="chkNewUserEnabled" checked="checked" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input type="button" id="btnCreateUser" value="Create User" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span id="lbl_newuser_status" style="color: #f00"></span>
                </div>
                <div id="contentUserAccess" style="display: none;">
                    <div class="page_title">
                        User Module Access</div>
                    <br />
                    <hr />
                    <span id="lblmodule">Accessible modules of</span>&nbsp;<span id="lbl_module_user"
                        style="font-weight: 700">(Selected User)</span>
                    <hr />
                    <br />
                    <div id="divModules" style="overflow-y: scroll; height: 320px">
                    </div>
                    <br />
                    <hr />
                    <br />
                    <input type="button" id="btnUpdateUserAccess" value="Update User Access" />
                    <br />
                    <span id="lbl_useraccess_status" style="color: #f00"></span>
                    <br />
                </div>
                <div id="contentRemoveUser" style="display: none;">
                    <div class="page_title">
                        Remove User</div>
                    <br />
                    <hr />
                    <span id="lbldelete">Are You sure you want to remove</span>&nbsp;<span id="lbl_del_user"
                        style="font-weight: 700">(Selected User)</span>
                    <hr />
                    <br />
                    <input type="button" id="btnRemoveUser" value="Remove User" />
                    <br />
                    <br />
                    <span id="lbl_removeuser_status" style="color: #f00"></span>
                </div>
                <div id="contentSetPassword" style="display: none;">
                    <div class="page_title">
                        Reset Password</div>
                    <br />
                    <hr />
                    <span id="lblpassword">Enter new password for</span>&nbsp;<span id="lbl_pass_user"
                        style="font-weight: 700">(Selected User)</span>
                    <hr />
                    <br />
                    <table id="tblResetPassword" style="">
                        <tr>
                            <td>
                                New Password:
                            </td>
                            <td>
                                <input type="password" id="txtNewPassword" maxlength="50" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm New Password:
                            </td>
                            <td>
                                <input type="password" id="txtConfirmNewPassword" maxlength="50" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input type="button" id="btnResetPassword" value="Reset Password" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span id="lbl_setpass_status" style="color: #f00"></span>
                </div>
                <div id="contentUserDetails" style="display: none;">
                    <div class="page_title">
                        User Details</div>
                    <br />
                    <hr />
                    Details of&nbsp;<span id="lblUserDetails" style="font-weight: 700">(Selected User)</span>
                    Click on 'Edit Details' to update information.
                    <hr />
                    <br />
                    <table>
                        <tr>
                            <td>
                                User Name:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserName" maxlength="50" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User ID:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserID" maxlength="50" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserEmail" maxlength="255" style="width: 225px" />
                                <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtUpdateUserEmail"
                                    Display="Dynamic" Enabled="True" ErrorMessage="Invalid Email" SetFocusOnError="True"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>-->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Approving Manager:
                            </td>
                            <td>
                                <select id="cmbUpdateUserApprovers">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Type:
                            </td>
                            <td>
                                <select id="cmbUpdateUserTypes">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Station:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserStation" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserCountry" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Region:
                            </td>
                            <td>
                                <input type="text" id="txtUpdateUserRegion" maxlength="25" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Enabled:
                            </td>
                            <td>
                                <input type="checkbox" id="chkUpdateUserEnabled" checked="checked" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input type="button" id="btnUpdateUserDetails" style="display: none" value="Update" />
                                <input type="button" id="btnEdit" value="Edit Details" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span id="lbl_userdetials_status" style="color: #f00"></span>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script src="Scripts/Tabs.min.js" type="text/javascript"></script>
    <script src="Scripts/AdminPanel.min.js" type="text/jscript"></script>
    <script type="text/javascript">
    </script>
</body>
</html>
