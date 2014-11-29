<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewAirRateRequest.aspx.vb"
    Inherits="RRAW.NewAirRateRequest1" %>

<%@ Register Src="UserControls/DyanamicForm.ascx" TagName="DyanamicForm" TagPrefix="uc1" %>
<%@ Register Src="UserControls/RightPanel.ascx" TagName="OtherForm" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0  Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ground Rate Request</title>
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/DynamicControl.css" rel="stylesheet" type="text/css" />
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="jQuery-Validation-Engine-master/css/validationEngine.jquery.css" rel="stylesheet"
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class='ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top'
        style="height: 23px">
        <div id="divPageTitle" class="Panel1Width ">
            <span id="lblRequestDate">&nbsp;</span>
            <input type="hidden" id="hidCurrentDateTime" />
            <span id="lblTitle">&nbsp;</span><br />
        </div>
    </div>
    <div class="ui-helper-reset ui-widget-content ui-accordion-content-active">
        <form id="form2">
        </form>
        <uc1:DyanamicForm ID="DyanamicForm1" runat="server" />
    </div>
    <div class="accordionpane">
        <h3 class="AccOtherInfo">
            More Infromation ...
        </h3>
        <div style="height: 110px">
            <form id="form3">
            </form>
            <uc1:OtherForm ID="DyanamicForm2" runat="server" />
        </div>
    </div>
    </form>
</body>
<script src="jQuery-Validation-Engine-master/js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script src="jquery-ui-1.9.2.overcast/js/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/languages/jquery.validationEngine-en.js"
    type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/jquery.validationEngine.js" type="text/javascript"></script>
<script src="Scripts/XMLHttpSyncExecutor.js" type="text/javascript"></script>
<script src="Scripts/RenderDynamicControls.js" type="text/javascript"></script>
<script src="Scripts/RightPanel.js" type="text/javascript"></script>
<script src="Scripts/AirRateRequest.js" type="text/javascript"></script>
</html>
