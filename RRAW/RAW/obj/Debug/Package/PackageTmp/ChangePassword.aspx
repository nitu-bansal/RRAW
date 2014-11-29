<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb"
    Inherits="RRAW.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <style type="text/css">
        .style1 {
            width: 133px;
        }
        .style2 {
            width: 156px;
        }
    </style>
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};document.getElementById('txtCurrentPassword').focus();">
    <form id="form1" runat="server" accept="btnChangePassword" defaultbutton="btnChangePassword">
    <div>
        <center>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
                    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span style="text-decoration: underline; font-size: small; font-weight: 700">Change Password</span>
    </div>
<br /><br />
            <br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table id="Table1">
                        <tr>
                            <td class="style2" align="left">
                                User:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="lblUserID" runat="server" Text="User ID" Enabled="false" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" align="left">
                                Current Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" Width="150px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCurrentPassword"
                                    Display="dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" align="left">
                                New Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="150px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewPassword"
                                    Display="dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" align="left">
                                Confirm New Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Width="150px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtConfirmNewPassword"
                                    Display="dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                    ControlToValidate="txtConfirmNewPassword" Display="dynamic" ErrorMessage="Confirm New Password"
                                    SetFocusOnError="True"></asp:CompareValidator>
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
                            <td align="left">
                                                   <asp:Button runat="server" ID="btnChangePassword" OnClientClick="document.getElementById('Table1').disabled=true" Text="Change Password" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" align="left" valign="top" colspan="2">
                                <br />
                                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        Changing Password...</ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
