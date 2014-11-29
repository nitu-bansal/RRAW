<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserModulesMapping.aspx.vb"
    Inherits="RRAW.UserModulesMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/UserModulesMapping.min.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){}; javascript:document.getElementById('lstUsers').focus();">
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
            <span style="text-decoration: underline; font-size: small; font-weight: bold">User Modules
                Mapping</span><br /><br />
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table id="Table1" style="text-align: left">
                        <tr>
                            <td>
                                <b>Users</b><br />
                                <asp:ListBox ID="lstUsers" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                                    DataTextField="Name" DataValueField="ID" Height="474px" Width="200px"></asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                    SelectCommand="GetAllUsers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            </td>
                            <td style="width: 350px;">
                                <b>Accessible modules of
                                    <asp:Label ID="lblSelectedUser" runat="server" Text="Selected User"></asp:Label></b><br />
                                <div style="height: 450px; overflow-y: auto; width:100%">
                                    <asp:CheckBoxList ID="chlModules" runat="server" DataSourceID="SqlDataSource2" DataTextField="Description"
                                        DataValueField="ID" Width="100%">
                                    </asp:CheckBoxList>
                                </div>
                                <asp:Button ID="btnUpdate" runat="server" Height="25px" Width="75px" Text="Update" />
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                    SelectCommand="GetAllModules" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblStatus" runat="server" Text="Status" Visible="false"></asp:Label>
                                <div class='UpdateProgressDiv'>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="1">
                                        <ProgressTemplate>
                                            <img alt="Updating..." src="Images/procImage.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
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
