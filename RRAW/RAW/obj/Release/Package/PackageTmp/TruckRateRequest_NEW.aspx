<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="false" EnableSessionState="False" %>

<!--<%@ OutputCache Location="Any" Duration="604800" VaryByParam="None" %>-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Truck Rate Request</title>
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/TruckRateRequest.min.css" rel="stylesheet" type="text/css" />
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/TruckRateRequests.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <div>
        <div id="divNotification" class="notificationBox">
            <div id="divNotificationTitle" class="notificationTitle">
            </div>
            <div id="divNotificationDescription" class="notificationDescription">
            </div>
        </div>
        <div id="divPageTitle" class="pageWidth" style="margin-left: 4px; box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -ms-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);">
            <input type="hidden" id="hidCurrentDateTime" />
            <span id="lblTitle">&nbsp;</span><br />
            <br />
        </div>
        <table>
            <tr>
                <td class="pageWidth">
                    <div id="PanelNewRateRequest" class="pageWidth" style="box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                        -ms-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                        -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);">
                        <table>
                            <tr>
                                <td>
                                    <table class="tbl" style="width: 742px;">
                                        <tr>
                                            <td style="text-align: right">
                                                <span id="lblRequestDate">&nbsp;</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Cargo Information</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 110px">
                                                            Container No.:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <input type="text" id="txtContainerNo" class="validateRequired" />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Truck HBL:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtTruckHBL" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Ship Date:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtShipDate" class="validateRequired" />(MM/DD/YYYY)
                                                        </td>
                                                        <td>
                                                            Freight Term:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtFreightTerm" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            WD Ship Method:
                                                        </td>
                                                        <td colspan="3">
                                                            <select id="cmbShipMethod" style="width: auto" class="validateRequired">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Origin Information</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 110px">
                                                            Shipper Name:
                                                        </td>
                                                        <td colspan="3">
                                                            <input type="text" id="txtShipperName" style="width: 200px" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <input type="text" id="txtOriginCity" class="validateRequired" /><br />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Origin Port:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtOriginPort" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtOriginZipcode" class="validateRequired" />
                                                            <br />
                                                        </td>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <select id="cmbOriginRegion" class="validateRequired">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Destination Information</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 110px">
                                                            Consignee Name:
                                                        </td>
                                                        <td colspan="3">
                                                            <input type="text" id="txtConsigneeName" style="width: 200px" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <input type="text" id="txtDestCity" class="validateRequired" /><br />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Destination Port:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDestPort" class="validateRequired" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDestZipcode" class="validateRequired" /><br />
                                                        </td>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <select id="cmbDestRegion" class="validateRequired">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Rate Request</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="min-width: 110px; width: 110px;">
                                                            Rate Valid For:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <select id="cmbRatesValidFor" class="validateRequired" style="width: auto">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                        <td style="width: 110px">
                                                            Warehouse:
                                                        </td>
                                                        <td>
                                                            <select id="cmbWarehouseType" class="validateRequired">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Rate Valid Till:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtRatesValidTill" class="validateRequired" />(MM/DD/YYYY)
                                                        </td>
                                                        <td>
                                                            Custom Clearance:
                                                        </td>
                                                        <td>
                                                            <select id="cmbCustomClearanceMode" class="validateRequired" style="width: auto">
                                                                <option value="">Select</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table id="tblRates">
                                                                <tr>
                                                                    <th class="RatesTopBorder RatesBottomBorder" style="width: 259px;">
                                                                        Rates
                                                                    </th>
                                                                    <th class="RatesTopBorder RatesBottomBorder">
                                                                        Tonnage
                                                                    </th>
                                                                    <th class="RatesTopBorder RatesBottomBorder">
                                                                        LTL<br />
                                                                        (Per cbm)
                                                                    </th>
                                                                    <th class="RatesTopBorder RatesBottomBorder">
                                                                        LTL<br />
                                                                        (Min)
                                                                    </th>
                                                                    <th style="text-align: center" class="RatesTopBorder RatesBottomBorder">
                                                                        FTL
                                                                    </th>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <div id="divAddMoreCharges">
                                                                            <div class="addIcon" style="float: left; margin-top: -2px;">
                                                                            </div>
                                                                            &nbsp;Add More Charges
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
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Comment</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <textarea id="txtComment" style="width: 99%; border: none; border-radius: 5px; margin-top: 2px;
                                                                margin-bottom: 2px;" cols="1" rows="3"></textarea>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="pnlAttachments">
                                                    <br />
                                                    <div class="group">
                                                        <div class="groupTitle">
                                                            &nbsp;Add New Attachments</div>
                                                        <table class="groupContainer">
                                                            <tr>
                                                                <td>
                                                                    <iframe style="width: 99.4%; height: 91%; overflow-y: auto" id="UploadFileIframe"
                                                                        src=""></iframe>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; line-height: 38px;">
                                                &nbsp;
                                                <input type="button" id="btnSave" value="Save" style="display: none" />
                                                <input type="button" id="btnPostComment" style="display: none" value="Post Comment" />
                                                <input type="button" id="btnApprove" style="display: none" value="Approve" />
                                                <input type="button" id="btnSendBackToReviseRate" style="display: none" value="Send Back To Revise Rate" />
                                                <input type="button" id="btnRevoke" style="display: none" value="Revoke" />
                                                <input type="button" id="btnPostNewRateRequest" value="Post New Rate Request" style="display: none" />
                                                <input type="button" id="btnNeedToReviseRate" value="Need To Revise Rate" style="display: none" />
                                                <input type="button" id="btnBackToDashboard" value="Back To Dashboard" style="display: none" />
                                                <input type="button" id="btnReject" style="display: none" value="Reject" />
                                                <input type="button" id="btnArchive" style="display: none" value="Archive" />
                                                <input type="button" id="btnRemoveAllComments" value="Remove All Comments" style="display: none" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group" id="groupPreviousComments" style="display: none">
                                                <div class="groupTitle">
                                                    &nbsp;Previous Comments</div>
                                                <div id="divPreviousComments" class="groupContainer" style="padding: 2px; width: 99%">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="vertical-align: top; width: 100%">
                    <div id="divAdditionalInformation" style="display: none">
                        <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                            <tr>
                                <td>
                                    <table class="tbl" style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                        <tr>
                                            <td style="text-align: right">
                                                <span id="Span1">&nbsp;</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Current Attachments</div>
                                                <div id="divCurrentAttachments" class="groupContainer" style="padding: 2px; line-height: 20px;
                                                    width: 99%">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script type="text/javascript" src="Scripts/TruckRateRequest.min.js" async="async"></script>
<script type="text/javascript" src="Scripts/Navigation.min.js" defer="defer"></script>
<script type="text/javascript" src="Scripts/FormValidation.js" defer="defer"></script>
</html>
