<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="RRAW.WebForm1" %>

<!--<%@ OutputCache Location="Any" Duration="604800" VaryByParam="None" %>-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Dashboard_NEW.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Test.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <!--<input type="text" id="txtSearch" value="0" /><br />-->
    <div id="tblRecentActivity">
    </div><br />
    <div id="tblRecentActivity2">
    </div>
    </form>
</body>
<script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
<!--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>-->
<script src="Scripts/Dashboard_NEW.min.js" type="text/javascript"></script>
</html>
