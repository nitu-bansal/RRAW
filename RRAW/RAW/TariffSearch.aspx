<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TariffSearch.aspx.vb"
    Inherits="RRAW.TariffSearch" %>

<%@ Register Src="~/UserControls/CustomSearch.ascx" TagName="Search" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>        
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/select2.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Dashboard.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/select2.js" type="text/javascript"></script>
    <script src="Scripts/TariffSearch.min.js" type="text/javascript"></script>     
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/AirRateRequests.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <div class="ui-helper-reset ui-widget-content ui-accordion-content-active" ><uc1:Search runat="server" ID="ucSearch"  />        
    </div>
    </form>    
</body>
</html>
