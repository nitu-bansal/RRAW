<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GetBlogEntries.aspx.vb"
    Inherits="RRAW.GetBlogEntries" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enableviewstate="false">
    <asp:Label ID="lblTitle" runat="server" EnableViewState="False"></asp:Label>
    <asp:Label ID="lblFilePath" runat="server" EnableViewState="False"></asp:Label>
    <asp:Label ID="lblPostedOn" runat="server" EnableViewState="False"></asp:Label>
    <asp:Label ID="lblPostedBy" runat="server" EnableViewState="False"></asp:Label>
    <div id="divBlogEntries">
        <asp:PlaceHolder ID="container" runat="server" EnableViewState="False"></asp:PlaceHolder>
        <div id="divBlogEntriesComplete">
        </div>
    </div>
    </form>
</body>
</html>
