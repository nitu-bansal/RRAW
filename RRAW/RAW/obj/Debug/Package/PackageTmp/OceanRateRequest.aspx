<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OceanRateRequest.aspx.vb"
    Inherits="RRAW.OceanRateRequest" EnableViewState="false" %>

<%@ Register Src="~/UserControls/RightPanel.ascx" TagName="RightPanel" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/NewRateRequest.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Tabs.min.css" rel="stylesheet" type="text/css" />    
    <link href="jQuery-Validation-Engine-master/css/validationEngine.jquery.css" rel="stylesheet"
        type="text/css" />
    <title>Attachments</title>
</head>

<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
<form id="form1" class="form" runat="server">
    <form>
    </form>
        <div>
    <uc:RightPanel ID="ucRightPanel" runat="server" /> </div>
    </form>     
 </body>


<script src="jQuery-Validation-Engine-master/js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script src="jQuery-Validation-Engine-master/js/languages/jquery.validationEngine-en.js"
    type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/jquery.validationEngine.js" type="text/javascript"></script>
<script src="Scripts/XMLHttpSyncExecutor.js" type="text/javascript"></script>
<script src="Scripts/RightPanel.js" type="text/javascript"></script>


</html>
