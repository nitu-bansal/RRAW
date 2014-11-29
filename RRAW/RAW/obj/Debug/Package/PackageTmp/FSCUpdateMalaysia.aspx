<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FSCUpdateMalaysia.aspx.vb"
    Inherits="RRAW.FSCUpdateMalaysia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Updates.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Tariff.min.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();">
    <form id="form1" runat="server" accept="" defaultbutton="">
    <div style="height: 575px; overflow: auto;">
        <center>
            <input id="hidCurrentUserAdded" type="hidden" />
            <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
            <span style="text-decoration: underline; font-size: small; font-weight: 700">WD Monthly FSC</span>
            <table style="width: 100%">
                <tr>
                    <td colspan="2" style="width:80%;">
                    <table>
                        <tr>
                            <td style="text-align:center;padding-bottom:5px;">
                                Effective Date : &nbsp;<asp:DropDownList ID="lstEfftDt" runat="server"></asp:DropDownList>
                                &nbsp;<asp:Button ID="btnShowTariff" runat="server" Text="Show Tariff" Width="92px" CausesValidation="false" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="text-align:center;">
                                <asp:Label runat="server" ID="lblFSCRateHeader" Text="" style="font-weight:bold;font-size:14px;"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:GridView ID="gridTariff" runat="server" AllowPaging="True" AllowSorting="false"
                                AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" ForeColor="#333"
                                GridLines="None" CaptionAlign="Right"  PageSize="12" Width="800"
                                EnableViewState="true" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" ShowHeader="True" CssClass="UpdateMalaysiaFooter" >
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Origin"   HeaderText="Origin" ItemStyle-CssClass="UpdateMalaysiaTD" />
                                    <asp:BoundField DataField="Destination" HeaderText="Destination"  ItemStyle-CssClass="UpdateMalaysiaTD" />
                                    <asp:BoundField DataField="THB" HeaderText="FSC THB"  />
                                    <asp:BoundField DataField="MYR" HeaderText="FSC MYR" />
                                    <asp:BoundField DataField="USD" HeaderText="FSC USD" />
                                    <%--<asp:TemplateField><FooterTemplate><asp:Label ID="lblfooter" runat="server"></asp:Label></FooterTemplate></asp:TemplateField>--%>
                                </Columns>
               
                                <EditRowStyle BackColor="#2461BF" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                                    NextPageText="Next" PreviousPageText="Previous" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" CssClass="FixedPosition"
                                    Font-Bold="True" VerticalAlign="Top" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                            </asp:GridView>
                            </td>
                        </tr>
                    </table>
                        
                    </td>
                </tr>
              <%--  <tr style="height:25px;">
                    <td colspan="2">&nbsp;</td>
                </tr>--%>
                <tr>
                    <td style="width: 50%;padding-top:10px;">
                        <table style="width: 100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="font-weight: 700; text-decoration: underline;">
                                    Previous Updates<hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gridUpdates" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="5" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                                        ForeColor="#000333" GridLines="None" CaptionAlign="Right" Width="100%" 
                                        DataKeyNames="ID">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField DeleteText="Remove" HeaderText="Remove File" ShowDeleteButton="True"
                                                Visible="False">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Update Date" HeaderText="Update Date" SortExpression="Update Date"
                                                ReadOnly="True">
                                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="Uploaded File" DataNavigateUrlFormatString="~/Attachments/{0}"
                                                DataTextField="Uploaded File" HeaderText="Uploaded File" 
                                                SortExpression="Uploaded File">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            No FSC Update files available.<br />
                                            <br />
                                            You can upload from the &quot;New Update&quot; panel available in right.
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="FixedPosition"
                                            Font-Bold="True" VerticalAlign="Top" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="GetAttachments"
                                        SelectCommandType="StoredProcedure" 
                                        DeleteCommand="RemoveAttachment" DeleteCommandType="StoredProcedure">
                                        <DeleteParameters>
                                            <asp:Parameter Name="ID" Type="Int32" />
                                        </DeleteParameters>
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="FSCUpdateMalaysia" Name="AttachmentType" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%;padding-top:10px;">
                        <table runat="server" id="tblFSCUpload" visible="false" style="width: 100%" cellspacing="0"
                            cellpadding="0">
                            <tr>
                                <td>
                                    <span style="font-weight: 700; text-decoration: underline;">New Update</span><hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                Title:
                                            </td>
                                            <td width="450">
                                                <asp:TextBox ID="txtTitle" runat="server" Width="99%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                    Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Browser to Upload:
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="uploadFSCUpdate" runat="server" Height="25px" Width="100%" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uploadFSCUpdate"
                                                    Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnUploadFSC" runat="server" Text="Upload" 
                                                    OnClientClick="btnUploadFSC_Click();" CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                                Status:&nbsp;<asp:Label ID="lblStatus" runat="server" Text="Browse for latest FSC file and upload"></asp:Label>
                                                <div id="DivUpdateProgress">
                                                    <table>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <div id="overlayDiv">
                                                                </div>
                                                                <div id="loading_notifier">
                                                                    <table id="loading_notifier_box">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td valign="middle" style="font-size: 10px; font-weight: 700;">
                                                                                    <span id="lblProgressText"></span>
                                                                                    <div id="processing_image">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
<script src="Scripts/FSCUpdate.min.js" type="text/javascript"></script>
</html>
