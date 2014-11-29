<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadFilePage.aspx.vb"
    Inherits="RRAW.UploadFilePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .hide {
            display: none;
        }
    </style>
</head>
<body style="background-color: #EFF3FB; margin: 0; padding: 1px">
    <form id="form2" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" Width="500" /><br />
    <asp:FileUpload ID="FileUpload2" runat="server" Width="500" /><br />
    <asp:FileUpload ID="FileUpload3" runat="server" Width="500" /><br />
    <asp:FileUpload ID="FileUpload4" runat="server" Width="500" /><br />
    <asp:FileUpload ID="FileUpload5" runat="server" Width="500" /><br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="hide" />
    </form>
</body>
</html>
