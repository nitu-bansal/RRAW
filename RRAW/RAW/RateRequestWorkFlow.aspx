<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateRequestWorkFlow.aspx.vb"
    Inherits="RRAW.RateRequestWorkFlow" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <link href="CSS/AdminPanel.min.css" rel="stylesheet" type="text/css" />
    <link href="jQuery-Validation-Engine-master/css/validationEngine.jquery.css" rel="stylesheet"
        type="text/css" />
    <title>Workflow</title>
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Authentications.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="hidCurrentUserID" runat="server" />
    <div id='container' style="width: 100%; height: 546px;" class="container">
        <div id="contentsMaster" style="width: auto; padding: 10px;">
            <div id="contentWorkflow" style="display: block;">
                <div class="page_title">
                    Workflow Details</div>
                <br />
                <hr />
                Click on 'Update/Insert' to update workflow information.
                <hr />
                <table style="width: 30%;" border="0">
                    <tr>
                        <td colspan="4">
                            <span id="lbl_UserWorkflowDetails_Status" style="color: #f00"></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%;">
                            Client:
                        </td>
                        <td style="width: 10%;">
                            <select id="cmbClient_workflow">
                            </select>
                        </td>
                        <td style="width: 2%;">
                            Mode:
                        </td>
                        <td style="width: 10%;">
                            <select id="cmbMode_workflow">
                                <option value="0">Select</option>
                                <option value="1">Air</option>
                                <option value="2">Ocean</option>
                                <option value="3">Ground</option>
                            </select>
                        </td>
                    </tr>
                </table>
                <div id="divWorkflow" style="overflow-y: scroll; height: 320px">
                    <table id="tblFormValidation_Workflow" width="100%">
                        <thead>
                        </thead>
                    </table>
                </div>
                <div id="divAddMoreCharges" style="display: none;">
                    <div id="divAddIcon" style="float: left" class="addIcon">
                    </div>
                    &nbsp;Add More Rules
                </div>
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
<script src="jQuery-Validation-Engine-master/js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script src="Scripts/RateRequestWorkflow.min.js" type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/languages/jquery.validationEngine-en.js"
    type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/jquery.validationEngine.js" type="text/javascript"></script>
<script type="text/javascript"></script>
</html>
