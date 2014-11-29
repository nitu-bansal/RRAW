<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OceanRateRequest.aspx.vb"
    Inherits="RRAW.OceanRateRequest" EnableViewState="false" %>

<%@ OutputCache Location="Any" Duration="604800" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ocean Rate Request</title>
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/OceanRateRequest.min.css" rel="stylesheet" type="text/css" />
    <![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
</head>
<body>
    <form id="form0" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/OceanRateRequests.asmx" InlineScript="true" />
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
                                    <table class="tbl">
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
                                                            <input type="text" id="txtContainerNo" />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Ocean HBL:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtOceanHBL" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Ship Date:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtShipDate" readonly="true" />(MM/DD/YYYY)
                                                        </td>
                                                        <td>
                                                            Freight Term:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtFreightTerm" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            WD Ship Method:
                                                        </td>
                                                        <td>
                                                            <select id="cmbWDShipMethod" style="width: auto">
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
                                                            <input type="text" id="txtShipperName" style="width: 200px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <input type="text" id="txtOriginCity" /><br />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Origin Port:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtOriginPort" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtOriginZipcode" />
                                                            <br />
                                                        </td>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <select id="cmbOriginRegion">
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
                                                            <input type="text" id="txtConsigneeName" style="width: 200px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td style="width: 230px">
                                                            <input type="text" id="txtDestCity" /><br />
                                                        </td>
                                                        <td style="width: 110px">
                                                            Destination Port:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDestPort" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDestZipcode" /><br />
                                                        </td>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <select id="cmbDestRegion">
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
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="min-width: 110px; width: 110px;">
                                                                        Rate Valid For:
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <select id="cmbRatesValidFor" style="width: auto">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Rate Valid Till:
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtRatesValidTill" readonly="true" />(MM/DD/YYYY)
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="tblRates">
                                                                <tr>
                                                                    <th class="RatesTopBorder RatesBottomBorder" style="width: 262px;" rowspan="2">
                                                                        Rates
                                                                    </th>
                                                                    <th class="RatesTopBorder RatesBottomBorder" rowspan="2">
                                                                        Based On
                                                                    </th>
                                                                    <th class="RatesTopBorder RatesBottomBorder" rowspan="2">
                                                                        LCL<br />
                                                                        (Per cbm)
                                                                    </th>
                                                                    <th colspan="3" style="text-align: center" class="RatesTopBorder RatesBottomBorder">
                                                                        FCL
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="RatesBottomBorder">
                                                                        per 20&#39;GP
                                                                    </th>
                                                                    <th class="RatesBottomBorder">
                                                                        per 40&#39;GP
                                                                    </th>
                                                                    <th class="RatesBottomBorder">
                                                                        per 40&#39;HC
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_0">Freight:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_0">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_0" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_0" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_0" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_1">Origin FOB:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_1">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_1" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_1" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_1" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_1" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_2">Origin Security Escort:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_2">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_2" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_2" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_2" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_2" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_3">Origin Stuffing:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_3">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_3" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_3" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_3" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_3" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_4">BAF:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_4">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_4" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_4" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_4" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_4" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_5">CAF:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_5">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_5" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_5" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_5" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_5" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_6">GRI/RR:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_6">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_6" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_6" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_6" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_6" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_7">PSS:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_7">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_7" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_7" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_7" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_7" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_8">Destination Security Escort:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_8">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_8" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_8" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_8" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_8" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_9">Destination Drayage:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_9">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_9" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_9" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_9" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_9" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_10">Destination Terminal Charge:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_10">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_10" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_10" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_10" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_10" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span id="txtRateTitle_11">Destination Pier Pass:</span>
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_11">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_11" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_11" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_11" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_11" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="height: 19px; vertical-align: bottom">
                                                                        Other Charges...
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>
                                                                        <input type="text" id="txtRateTitle_9999" style="width: 97%; text-align: left" />
                                                                    </td>
                                                                    <td>
                                                                        <select id="cmbRateBasedOn_9999">
                                                                            <option value="">Select</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPerCBM_9999" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer20GP_9999" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40GP_9999" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" id="txtPer40HC_9999" />
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <table id="tblOtherCharges">
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <span style="float: left; height: 19px;">&nbsp;</span>
                                                                        <div id="divAddMoreCharges" style="display: none">
                                                                            <div id="divAddIcon" style="float: left">
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
                                                                    <iframe style="width: 99.4%; overflow-y: auto; height: 132px" id="UploadFileIframe"
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
<!--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.min.js"></script>-->
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script type="text/javascript" src="Scripts/OceanRateRequest.min.js"></script>
<script type="text/javascript" src="Scripts/FormValidation.js" defer="defer"></script>
<!--<script type="text/javascript" src="Scripts/Navigation.min.js" defer="defer"></script>-->
</html>
