<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentStorage.aspx.vb" Inherits="RRAW.DocumentStorage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Dashboard.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Tariff.min.css" rel="stylesheet" type="text/css" />
</head>


<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};function f(){var t=new Date();return((t.getMonth()+1)+'/'+t.getDate()+'/'+t.getFullYear()+' '+t.getHours()+':'+t.getMinutes()+':'+t.getSeconds())};document.getElementById('hidCurrentDateTime').value=f();">
    <form id="form1" runat="server" accept="" defaultbutton="">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="height: 575px; overflow: auto;">
        <center>
            <input id="hidCurrentUserAdded" type="hidden" />
            <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
            <span style="text-decoration: underline;  font-weight: 700">Document list</span>
            <table style="width: 100%">                
                <tr>
                    <td style="padding-top:10px;">
                            <table runat="server" id="tblDocumentUpload" visible="false" style="width: 100%" cellspacing="0" border="0"
                                cellpadding="0">
                                <tr>
                                    <td>
                                        <span style="font-weight: 700; text-decoration: underline;">New Update</span><hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <table style="width: 50%;" border="0">
                                            <tr>
                                                <td style="width:20%">
                                                    Title:
                                                </td>
                                                <td style="width:80%">
                                                    <asp:TextBox ID="txtTitle" runat="server" Width="79%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                        Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                Category:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlCategory" DataTextField="Category" DataValueField="ID"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" InitialValue="0"
                                                        Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Effective Date:
                                                </td>
                                                <td align="left">                
                                                   <asp:TextBox ID="txtEffectiveDate" runat="server" Width="80" AutoCompleteType="Disabled"
                                                        CssClass="align_center"></asp:TextBox>
                                                      <asp:CalendarExtender ID="txtEffectiveDate_CalendarExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtEffectiveDate" Animated="False" Format="MM/dd/yyyy">
                                                    </asp:CalendarExtender>  
                                                    <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                                                        Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                   
                                                </td>                 
                                            </tr>
                                            <tr>
                                            <td>
                                                    Expiry Date:
                                                </td>
                                            <td>                                            
                                                 <asp:TextBox ID="txtExpiryDate" runat="server" Width="80" AutoCompleteType="Disabled"
                                                        CssClass="align_center"></asp:TextBox>                                                
                                                    <asp:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtExpiryDate" Animated="False" Format="MM/dd/yyyy">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                                        Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                     
                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Browser to Upload:
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="uploadDocument" runat="server" Height="25px" Width="100%" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uploadDocument"
                                                        Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUploadDocument" runat="server" Text="Upload" 
                                                        OnClientClick="btnUploadDocument_Click();" CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                    Status:&nbsp;<asp:Label ID="lblStatus" runat="server" Text="Browse for latest file and upload"></asp:Label>
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
                <tr>
                    <td style="padding-top:10px;">
                        <table  runat="server" id="tblDocumentDownload" visible="false" style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td style="font-weight: 700; text-decoration: underline;">
                                    Previous Updates<hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gridUpdates" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="5" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                                        ForeColor="#000333" GridLines="None"  Width="100%" 
                                        DataKeyNames="ID">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>                                            
                                            <asp:TemplateField ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center">
                                            <HeaderTemplate >Remove File</HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" style="color:Blue;"
                                            OnClientClick="return confirm('Are you sure you want to delete this document?');" Text="Remove" CausesValidation="false" ></asp:LinkButton>
                                            </ItemTemplate></asp:TemplateField>
                                            <asp:BoundField  DataField="ID"  HeaderText="ID" SortExpression="ID" Visible="false"  ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Upload Date" HeaderText="Upload Date"  SortExpression="Upload Date" ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center"
                                                ReadOnly="True">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DocumentTitle" HeaderText="Title" SortExpression="DocumentTitle" ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center">
                                                
                                            </asp:BoundField>                                            
                                            <asp:BoundField DataField="EffectiveDate" HeaderText="EffectiveDate" SortExpression="EffectiveDate" ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center">
                                                
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" SortExpression="ExpiryDate" ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center">
                                                
                                            </asp:BoundField>   
                                            <asp:HyperLinkField DataNavigateUrlFields="DocumentName"  DataNavigateUrlFormatString="~/Documents/{0}"
                                                DataTextField="DocumentName" HeaderText="Uploaded File" ItemStyle-CssClass="align_center" HeaderStyle-CssClass="align_center"
                                                SortExpression="DocumentName" >
                                                <ItemStyle HorizontalAlign="Left"  />
                                            </asp:HyperLinkField>

                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            No Documents available.<br />
                                            <br />
                                            You can upload from the &quot;New Update&quot; panel available in above.
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#336699" HorizontalAlign="Left" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                        CssClass="with-border" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="FixedPosition"
                                            Font-Bold="True" VerticalAlign="Top" />
                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                        
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="GetDocuments"
                                        SelectCommandType="StoredProcedure" 
                                        DeleteCommand="RemoveDocument" DeleteCommandType="StoredProcedure" >
                                        <DeleteParameters>
                                            <asp:Parameter Name="ID" Type="Int32" />
                                            <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                                        </DeleteParameters>
                                        <SelectParameters>                                            
                                            <asp:Parameter Name="ClientId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
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
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
</html>

