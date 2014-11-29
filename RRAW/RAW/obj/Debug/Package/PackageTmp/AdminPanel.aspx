<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminPanel.aspx.vb" Inherits="RRAW.AdminPanel"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Tabs.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/AdminPanel.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <title>Admin Panel</title>
</head>
<body onload="try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <form id="form1" class="form" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Authentications.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="hidCurrentUserID" runat="server" />
    <div style="margin-left: 0; width: 1335px; height: 548px; background: none repeat scroll 0 0 #fff;
        box-shadow: 0 0 3px #B6B7BB; border: 1px solid #D6D6D6;">
        <div id='container' style="width: 100%; height: 546px;" class="container">
            <div id="stripUserAdmin">
                <a id="tabQuoteAccess" class="tab tab_selected" onclick="selectMe('stripUserAdmin', this);document.getElementById('cmbClient').focus()">
                    Quote Access</a><a id="tabRatesAccess" class="tab" onclick="selectMe('stripUserAdmin', this);document.getElementById('cmbClientRates').focus()">
                        Rates Access</a>
            </div>            
            <span id="lbl_UserQuoteAccessDetails_Status" style="color: #f00"></span>
            
            <div id="contentsMaster" style="width: auto; padding: 10px;">
                <div id="contentQuoteAccess" style="display: block;">
                    <div class="page_title">
                        Quote access Details</div>
                    <br />
                    <hr />
                    Click on 'Update/Insert' to update quote access information.
                    <hr />
                    <br />
                    <table>
                        <tr>
                            <td>
                                Client:
                            </td>
                            <td>
                                <select id="cmbClient">
                                </select>
                            </td>                            
                        </tr>
                    </table>
                    <div id="divQuotes" style="overflow-y: scroll; height: 320px">
                        <table id="tblFormValidation" width="100%">
                            <thead>
                            </thead>
                        </table>
                    </div>
                </div>
                <div id="contentRatesAccess" style="display: none;">
                    <div class="page_title">
                        Rates access Details</div>
                    <br />
                    <hr />
                    Click on 'Update/Insert' to update rates access information.
                    <hr />
                    <br />
                    <table style="width: 30%;" border="0">
                        <tr>
                            <td style="width: 2%;">
                                Client:
                            </td>
                            <td style="width: 10%;">
                                <select id="cmbClientRates">
                                </select>
                            </td>
                            <td style="width: 2%;">
                                Mode:
                            </td>
                            <td style="width: 10%;">
                                <select id="cmbMode">
                                    <option value="0">Select</option>
                                    <option value="1">Air</option>
                                    <option value="2">Ocean</option>
                                    <option value="3">Ground</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                    <div id="divRates" style="overflow-y: scroll; height: 320px">
                        <table id="tblFormValidation_Rates" width="100%">
                            <thead>
                            </thead>
                        </table>
                    </div>
                    <br />
                    <span id="lbl_UserRatesAccessDetails_Status" style="color: #f00"></span>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script>
        $("#accordion").accordion();
    </script>
</body>
<script src="jQuery-Validation-Engine-master/js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script src="Scripts/AdminPanel.min.js" type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/languages/jquery.validationEngine-en.js"
    type="text/javascript"></script>
<script src="jQuery-Validation-Engine-master/js/jquery.validationEngine.js" type="text/javascript"></script>
<script type="text/javascript"></script>
<script type="text/ecmascript" src="Scripts/Tabs.min.js"></script>
</html>
