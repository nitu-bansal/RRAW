<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AirRateRequest.aspx.vb"
    Inherits="RRAW.AirRateRequest" %>

<%@ OutputCache Location="Any" Duration="604800" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/NewRateRequest.min.css" rel="stylesheet" type="text/css" />
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
            <asp:ServiceReference Path="~/WebServices/AirRateRequests.asmx" InlineScript="true" />
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
            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            text-align: center; font-weight: bold; font-size: 12px; text-decoration: underline;
            height: 35px;">
            <input type="hidden" id="hidCurrentDateTime" />
            <span id="lblTitle">&nbsp;</span><br />
            <br />
        </div>
        <div>
            <table>
                <tr>
                    <td class="pageWidth">
                        <div id="PanelNewRateRequest" class="pageWidth" style="box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                            -ms-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                            height: 490px; overflow: auto">
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <span id="lblRequestDate">&nbsp;</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="group">
                                        <div class="groupTitle">
                                            &nbsp;Cargo Information
                                        </div>
                                        <table class="groupContainer">
                                            <tr>
                                                <td style="width: 110px">
                                                    HAWB:
                                                </td>
                                                <td style="width: 218px">
                                                    <input type="text" id="txtHAWBNumber" style="width: 100px" />
                                                </td>
                                                <td style="width: 110px">
                                                    Chg. Weight (KG):
                                                </td>
                                                <td>
                                                    <input type="text" id="txtWeight" style="width: 100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 110px">
                                                    Shipment Date:
                                                </td>
                                                <td>
                                                    <input type="text" id="txtShipDate" readonly="readonly" style="width: 100PX" />
                                                </td>
                                                <td style="width: 110px">
                                                    Service Level:
                                                </td>
                                                <td>
                                                    <select id="cmbServiceLevel" style="width: 105px; height: 21px">
                                                        <option value="">Select</option>
                                                        <option value="DEF">DEF</option>
                                                        <option value="EXP">EXP</option>
                                                        <option value="STD">STD</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    WD Ship Method:
                                                </td>
                                                <td>
                                                    <select id="cmbShipMethod" style="width: 150px; height: 21px">
                                                    </select>
                                                </td>
                                                <td style="width: 110px">
                                                   Transport Mode:
                                                </td>
                                                <td>
                                                    <select id="cmbTransportMode" style="width: 105px; height: 21px">
                                                        <option value="">Select</option>
                                                        <option value="Air">Air</option>
                                                        <option value="Local Trucking">Local Trucking</option>
                                                        <option value="Air & Local Trucking">Air & Local Trucking</option>
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
                                            &nbsp;Origin Information
                                        </div>
                                        <table class="groupContainer">
                                            <tr>
                                                <td style="width: 110px">
                                                    Shipper Name:
                                                </td>
                                                <td style="width: 218px">
                                                    <input type="text" id="txtShipperName" style="width: 200px;" />
                                                </td>
                                                <td>
                                                    Origin Port Code:
                                                </td>
                                                <td>
                                                    <input type="text" id="txtOriginAirport" style="width: 100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    City:
                                                </td>
                                                <td style="width: 218px">
                                                    <input type="text" id="txtOriginCity" style="width: 100px" />
                                                </td>
                                                <td style="width: 111px">
                                                    Region:
                                                </td>
                                                <td>
                                                    <select id="cmbOriginRegion" style="width: 106px; height: 20px;">
                                                        <option value="">Select</option>
                                                        <option value="AM">AM</option>
                                                        <option value="EMEA">EMEA</option>
                                                        <option value="FE">FE</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Postal Zipcode:
                                                </td>
                                                <td>
                                                    <input type="text" id="txtOriginZipCode" style="width: 100px;" />
                                                </td>
                                                <td>
                                                    European zone:
                                                </td>
                                                <td colspan="3">
                                                    <select id="cmbOriginEuropianZone" style="width: 106px; height: 20px" disabled="disabled">
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
                                            &nbsp;Destination Information
                                        </div>
                                        <table class="groupContainer">
                                            <tr>
                                                <td style="width: 110px">
                                                    Consignee Name:
                                                </td>
                                                <td style="width: 218px">
                                                    <input type="text" id="txtConsigneeName" style="width: 200px;" />
                                                </td>
                                                <td>
                                                    Dest. Port Code:
                                                </td>
                                                <td>
                                                    <input type="text" id="txtDestAirport" style="width: 100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    City:
                                                </td>
                                                <td style="width: 218px">
                                                    <input type="text" id="txtDestCity" style="width: 100px" />
                                                </td>
                                                <td style="width: 110px">
                                                    Region:
                                                </td>
                                                <td>
                                                    <select id="cmbDestRegion" style="width: 106px; height: 20px;">
                                                        <option value="">Select</option>
                                                        <option value="AM">AM</option>
                                                        <option value="EMEA">EMEA</option>
                                                        <option value="FE">FE</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Postal Zipcode:
                                                </td>
                                                <td>
                                                    <input type="text" id="txtDestZipCode" style="width: 100px" />
                                                </td>
                                                <td>
                                                    European zone:
                                                </td>
                                                <td>
                                                    <select id="cmbDestEuropianZone" style="width: 106px; height: 20px" disabled="disabled">
                                                    </select>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trBradkDownChargesLineBreak">
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trBreakDownCharges">
                                    <td class="group">
                                        <div class="groupTitle">
                                            Cost</div>
                                            <table class="groupContainer" cellpadding="0" cellspacing="0"><tr><td>
                                            <div style="width:559px;overflow:auto;">
                                        <table id="tblBreakDownCharges" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <th colspan="2" id="txtOrigin_Name">
                                                   <span>Origin Charges</span>
                                                </th>
                                                <th colspan="2" id="txtATA_Name">
                                                    <span>ATA Rates</span>
                                                </th>
                                                <th colspan="2" id="txtDest_Name">
                                                    <span>Destination Charges</span>
                                                </th>
                                                <th colspan="2" id="txtISS_Name">
                                                   <span>ISS Rate</span>
                                                </th>
                                                <th colspan="2" id="txtFSCProt_Name">
                                                    <span>FSC (Protection)</span>
                                                </th>
                                                <th colspan="2" id="txtMargin_Name">
                                                    <span>Margin</span>
                                                </th>
                                                <th colspan="2" style="color: white; background-color: #B22222;" id="txtFinalName">
                                                    <span>Final Submission</span>
                                                </th>
                                                 <th colspan="2" id="txtFSC_Name">
                                                    <span>Actual FSC Cost</span>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                                <td style="color: white; background-color: #B22222;">
                                                    MIN
                                                </td>
                                                <td style="color: white; background-color: #B22222;">
                                                    Per Kilo
                                                </td>
                                                <td>
                                                    MIN
                                                </td>
                                                <td>
                                                    Per Kilo
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" id="txtOrigin_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtOrigin_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtATA_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtATA_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtDest_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtDest_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtISS_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtISS_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtFSCProt_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtFSCProt_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtMargin_Min" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtMargin_PerKG" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtFinalMin" style="color: #B22222;background-color: #92D050"  readonly="readonly" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtFinalPerKG" style="color: #B22222;background-color: #92D050" readonly="readonly" />
                                                </td>
                                                 <td>
                                                    <input type="text" id="txtFSC_Min" style="background-color: #DAF0E0" />
                                                </td>
                                                <td>
                                                    <input type="text" id="txtFSC_PerKG" style="background-color: #DAF0E0"/>
                                                </td>
                                            </tr>
                                        </table></div>
                                        </td></tr></table>
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
                                            &nbsp;<span id="lblRateGroupTitle">Rates</span></div>
                                        <table class="groupContainer">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="3">
                                                                <span>Currency:</span>
                                                                <select id="cmbCurrency" style="width: 102px; height: 20px">
                                                                    <option value="">Select</option>
                                                                    <option value="THB">THB</option>
                                                                    <option value="MYR">MYR</option>
                                                                    <option value="USD">USD</option>
                                                                </select>
                                                            </td>

                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr id="trFinalRates">
                                                            <td style="width: auto; vertical-align: top;">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td colspan="2" style="color: white; background-color: #B22222;">
                                                                            Final Submission
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="color: white; background-color: #B22222;">
                                                                        <td>
                                                                            MIN
                                                                        </td>
                                                                        <td>
                                                                            Per Kilo
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="text" id="txtMinFreightRate" style="color: #B22222;background-color: #92D050" class="ValueField number" readonly="readonly" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" id="txtFreightRate" style="color: #B22222;background-color: #92D050" class="ValueField number" readonly="readonly" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="center" style="font-size: 20px; width: auto; font-weight: bold; vertical-align: top;">
                                                                +
                                                            </td>
                                                            <td style="width: 130px; vertical-align: top;">
                                                                <table cellpadding="0" cellspacing="0" width="125px">
                                                                    <tr style="background-color: #FFA500;">
                                                                        <td>
                                                                            Add On Charges
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="background-color: #FFA500;">
                                                                        <td>
                                                                            Total Fees
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="text" id="txtTotalAddOnFees" class="ValueField number" onblur="this.value=parseFloat(this.value).toFixed(2)"
                                                                                style="width: 95% !important;color: #B22222;background-color: #92D050" readonly="readonly" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" id="tblAddOnDetailCharges">
                                                                    <tr style="background-color: #FFA500;" id="tdAddOnHeader1">
                                                                        <td colspan="4">
                                                                            Add On
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="background-color: #FFA500;" id="tdAddOnHeader2">
                                                                        <td>
                                                                            Details
                                                                        </td>
                                                                        <td>
                                                                            UOM
                                                                        </td>
                                                                        <td>
                                                                            Fees
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trAddOn_1_NewAddOnCharges">
                                                                        <td>
                                                                            <input type="text" id="txtDetails_1_NewAddOnCharges" style="color: #B22222;background-color: #92D050"/>
                                                                        </td>
                                                                        <td>
                                                                            <select style="width: 90px; height: 20px;color: #B22222;background-color: #92D050" id="cmbRateBasedOn_1_NewAddOnCharges">
                                                                                <option value="">Select</option>
                                                                                <option value="Per Shipment">Per Shipment</option>
                                                                                <option value="Per Kilo">Per Kilo</option>
                                                                            </select>
                                                                        </td>
                                                                        <td>
                                                                            <input type="text" class="ValueField number"
                                                                                id="txtAddOnFee_1_NewAddOnCharges" style="color: #B22222;background-color: #92D050"/>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <div id="divAddMoreCharges">
                                                                    <div id="divAddIcon" style="float: left" class="addIcon">
                                                                    </div>
                                                                    &nbsp;Add More Charges
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                Methodology used to determine rate<br />
                                                                <textarea id="txtRateDeterMethodology" cols="1" rows="2" style="width: 99%; border: none;
                                                                    border-radius: 5px; margin: 2px;"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                Additional Charges (Require WD Approval)<br />
                                                                <textarea id="txtOtherCharges" style="width: 99%; border: none; border-radius: 5px;
                                                                    margin: 2px;" cols="20" rows="2"></textarea>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trGuidelinesLineBreak">
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trGudelines">
                                    <td class="group">
                                        <div class="groupTitle">
                                            RRAW Costing Guidelines</div>
                                        <table class="groupContainer">
                                            <tr>
                                                <td>
                                                <span style="color:Maroon;font-size:10px;">* Please read and accept below guidelines to submit request.</span>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr id="trRules_1">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_1" type="checkbox" /><label for="chkRules_1">1) Origin Charges</label></div>
                                                                <div id="dvExpand_1" class="ShowDescription" title="Click to view rule description">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_1">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>Includes FOB Charges, Pick
                                                                    up and Export Declaration;
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_2">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_2" type="checkbox" /><label for="chkRules_2">2) Port to Port Charges</label></div>
                                                                <div id="dvExpand_2" class="ShowDescription" title="Click to view rule description">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_2">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>Cost of Airfreight which CEVA
                                                                    pays the Airlines/Liners
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_3">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_3" type="checkbox" /><label for="chkRules_3">3) Destination Charges</label></div>
                                                                <div id="dvExpand_3" class="ShowDescription" title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_3">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>Includes all delivery, onforwarding
                                                                    (if applicable), terminal handling, gateway fees, console fees, customs clearance
                                                                    & CIQ
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_4">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_4" type="checkbox" /><label for="chkRules_4">4) ISS/SSC</label></div>
                                                                <div id="dvExpand_4" class="ShowDescription"  title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_4">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>Security Charges where applicable
                                                                    by Carrier
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_5">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_5" type="checkbox" /><label for="chkRules_5">5) FSC Arbitration
                                                                        Costs</label></div>
                                                                <div id="dvExpand_5" class="ShowDescription"  title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_5">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>WD pays a monthly FSC based
                                                                    on WD's FSC Index which varies from month to month. CEVA requires to arbitrate the
                                                                    cost difference between what CEVA pays to Airlines and the WD's FSC Index. CEVA
                                                                    will need to put an arbitrary costs to cover the FSC variance as well as the Risks
                                                                    of the volatile Fuel market.
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_6">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_6" type="checkbox" /><label for="chkRules_6">6) Special Charges
                                                                        (Based on Special Approvals)
                                                                    </label>
                                                                </div>
                                                                <div id="dvExpand_6" class="ShowDescription"  title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_6">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>For example: Refurbished Drives
                                                                    examination fee for India
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_7">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_7" type="checkbox" /><label for="chkRules_7">7) Rate Validity
                                                                    </label>
                                                                </div>
                                                                <div id="dvExpand_7" class="ShowDescription"  title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_7">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span>All Rates must be good till
                                                                    June 30, 2013 where WD may consider Rate review
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRules_8">
                                                            <td>
                                                                <div class="RulesDiv">
                                                                    <input id="chkRules_8" type="checkbox" /><label for="chkRules_8">8) Special Notes
                                                                    </label>
                                                                </div>
                                                                <div id="dvExpand_8" class="ShowDescription"  title="Click to view rule description"/>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDescription_8">
                                                            <td>
                                                                <div class="RuleDescription">
                                                                    <span style="font-weight: bold">Description : </span><br />1) Western Digital will only
                                                                    Accept billings to show DTD/DTA/ATD/ATA Rates in Min or Flat/kg and WD's Monthly
                                                                    FSC. WD Will not accept add-on or accessorial charges unless special approvals has
                                                                    been made.<br />
                                                                    <br />
                                                                    2) At point of quotation, "All Known costs" to be incurred requires to be included
                                                                    and quoted in Min or Flat/kg.<br />
                                                                    <br />
                                                                    3) For pickup and delivery in China, rates must include both bonded and non-bonded
                                                                    trucks.<br />
                                                                    <br />
                                                                    4) DTA shipments to Hong Kong must move on a direct master and will be consigned
                                                                    to our consignee's agent.<br />
                                                                    <br />
                                                                    5) Lanes must be quoted in the currency shown on each line. WDT pays its Malaysia
                                                                    bills in MYR without conversion. WDT pays its Thailand bills in THB without conversion.
                                                                    All other lanes represented in this bid are to be billed in USD and will be paid
                                                                    in USD. USD denominated lanes represent less than 5% of the overall tonnage. As
                                                                    a result there will be no adjustment for CAF during the life of the contract for
                                                                    the USD lanes.<br />
                                                                    <br />
                                                                    6) Rates to Singapore are to include GST Application Fee.<br />
                                                                    <br />
                                                                    7) In the event that a survey of a shipment is needed for damage, WDT's freight
                                                                    forwarder will need to absorb this cost.<br />
                                                                    <br />
                                                                    8) Customs clearance cost for DTD shipments should be included in the base rate
                                                                    per kg even if our customer's agent performs customs clearance. Since customs clearance
                                                                    is a standard cost, it should be easily predicted.<br />
                                                                    <br />
                                                                    9) For terms not DTD, similar conditions apply; no additional fees, surcharges or
                                                                    accessorial may be assessed between points named to/from door/port.<br />
                                                                    <br />
                                                                    10) Shipments include 7 days of free storage at destination airports in the rare
                                                                    event destination storage charges apply.<br />
                                                                    <br />
                                                                    11) Airport to Door ATD will be the same as DTD with origin cartage/drayage subtracted.<br />
                                                                    <br />
                                                                    12) Pricing for Ocean must be "all in" like Airfreight. Only BAF, duties and taxes
                                                                    may be separately assessed at cost.<br />
                                                                    <br />
                                                                    13) Door to Airport DTA will be the same as DTD with destination cartage/drayage
                                                                    subtracted. Dest Hong Kong DTA shipments should also have terminal charges subtracted.<br />
                                                                    <br />
                                                                    14) Airport to Airport will be the same as DTD with both origin and destination
                                                                    cartage/drayage subtracted.<br />
                                                                    <br />
                                                                    15) If WDT ships at an expedited service-level and the forwarder does not achieve
                                                                    an expedited delivery, the forwarder will credit the delta between expedited and
                                                                    standard service. Likewise if WDT ships at an standard service-level and the forwarder
                                                                    does not achieve a standard delivery, the forwarder will credit the delta between
                                                                    standard and defered service.<br />
                                                                    <br />
                                                                    16) Pick up and delivery areas are expected to cover the major metropolitan areas
                                                                    of the city code shown, a 150 mile radius from that origin/destination city, notwhithstanding
                                                                    that the actual origin shipping point or destination receiving point may be in a
                                                                    location beyond the national border of the origin or destination port city shown.
                                                                    For example, a shipment from Shenzhen to Eindhoven may use a HKG to NL01 DTD rate.<br />
                                                                    <br />
                                                                    17) Only charges for Fuel and taxes such as VAT/Duty may be charged separately on
                                                                    shipments.
                                                                    <br />
                                                                    <br />
                                                                    18) Transit days are to be counted with pick up day as "Day 0" (Compass Approval)
                                                                    (Ignore the Intl Dateline). Each weekend day count as one day. A Friday pick up
                                                                    in Kuala Lumpur delivered the following Tuesday in Amsterdam is a 4 day transit.<br />
                                                                    <br />
                                                                    19) Volume for lanes not showing shipments/weights will vary. WDT wants competitive
                                                                    rates as these lanes are likely to become active, especially DEF lanes.<br />
                                                                    <br />
                                                                    20) Transit days are shown as per the WDT specification and your quotation must
                                                                    support consistent performance to this standard.<br />
                                                                    <br />
                                                                    21) Rates quoted in this RFP must apply to all commodities.<br />
                                                                    <br />
                                                                    22) DTD / Will-Call shipments must include the administrative costs associated with
                                                                    managing the shipments at destination stations (contacting the customer's agent
                                                                    for pick-up, managing pick-up appointments, updating Compass, temporarily storage,
                                                                    etc..)<br />
                                                                    <br />
                                                                    23) Shipments to GDL require (2) two separate security escorts (front and back)
                                                                    from the airport to the final destination and should be included in the per kg rate<br />
                                                                    <br />
                                                                    24) Customs duties and taxes will be paid by WDT under contractual payment terms
                                                                    of Net 30
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
                                                <td>
                                                    <textarea id="txtComment" style="width: 99%; border: none; border-radius: 5px; margin: 2px;"
                                                        cols="1" rows="3"></textarea>
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
                                                    &nbsp;Add New Attachments
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
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; line-height: 38px;">
                                        &nbsp;
                                        <input type="button" id="btnSave" value="Save" style="display: none" />
                                        <input type="button" id="btnTransferToTariff" value="Transfer To Tariff" style="display: none" />
                                        <input type="button" id="btnPostComment" value="Post Comment" style="display: none" />
                                        <input type="button" id="btnApprove" value="Approve" style="display: none" />
                                        <input type="button" id="btnApproveAsAdhoc" value="Approve As Adhoc" style="display: none" />
                                        <input type="button" id="btnRevoke" value="Revoke" style="display: none" />
                                        <input type="button" id="btnPostNewRateRequest" value="Post New Rate Request" style="display: none" />
                                        <input type="button" id="btnBackToDashboard" value="Back To Dashboard" style="display: none" />
                                        <input type="button" id="btnNeedToReviseRate" value="Need To Revise Rate" style="display: none" />
                                        <input type="button" id="btnSendBackToReviseRate" value="Send Back To Revise Rate"
                                            style="display: none" />
                                        <input type="button" id="btnReject" value="Reject" style="display: none" />
                                        <input type="button" id="btnDelete" value="Delete" style="display: none" />
                                        <input type="button" id="btnRemoveAllComments" value="Remove All Comments" style="display: none" />
                                        <input type="button" id="btnApprove_Permanent" value="Approved as Permanent" style="display: none" />
                                        <input type="button" id="btnSendBackToRequestor" value="Send Back To Requestor" style="display: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="groupTitle">
                                            &nbsp;Previous Comments
                                        </div>
                                        <div id="divPreviousComments" class="groupContainer" style="padding: 2px; width: 99%">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: left;" class="pageWidth">
                            <hr />
                        </div>
                        <span id="lblStatus" class="statusLabel" style="color: #000">Add lane details and save
                            to generate new rate request. All fields are compulsory except for rates and HAWB
                            Number. </span>
                        <div id="divUpdateProgress">
                            <table>
                                <tr>
                                    <td style="text-align: left; vertical-align: top">
                                        <div id="overlayDiv">
                                        </div>
                                        <div class="loading_notifier">
                                            <table class="loading_notifier_box">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: middle; font-size: 10px; font-weight: 700;">
                                                            <span id="lblProgressText">Loading Data...</span>
                                                            <div id="processing_image">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="bottom" style="padding-bottom: 6px;">
                        <div id="pnlCapture" style="display:none">
                            Paste data from FAMS & OFS here to capture<br />
                            <textarea id="txtCapture" style="height: 473px; width: 530px" cols="1" rows="50">
                        </textarea><br />
                            <center>
                                <input id="btnCapture" type="button" value="Capture" onclick="getData('txtCapture');" />
                            </center>
                        </div>
                        <div id="divAdditionalInformation" style="display: none; height: 490px; overflow: auto">
                            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                <tr>
                                    <td>
                                        <table class="tbl" style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                            <tr>
                                                <td style="text-align: right">
                                                    <span id="Span2">&nbsp;</span>
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
                            <div id="divRateChangesHistory">
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
                                                            &nbsp;Rate Changes History</div>
                                                        <div id="divRateRequestHistory" style="display: none" class="groupContainer" style="padding: 2px;
                                                            line-height: 20px; width: 99%">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="SimilarLane">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                    <tr>
                                        <td>
                                            <table class="tbl" style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                                <tr>
                                                    <td style="text-align: right">
                                                        <span id="Span3">&nbsp;</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="group">
                                                        <div class="groupTitle">
                                                            &nbsp;Similar rate requests</div>
                                                        <div id="divSimilarRateRequests" style="display: none" class="groupContainer" style="padding: 2px;
                                                            line-height: 20px; width: 99%">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divSimilarTariffLanesMain">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                    <tr>
                                        <td>
                                            <table class="tbl" style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                                <tr>
                                                    <td style="text-align: right">
                                                        <span id="Span4">&nbsp;</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="group">
                                                        <div class="groupTitle">
                                                            &nbsp;Similar lanes in tariff</div>
                                                        <div id="divSimilarTariffLanes" style="display: none" class="groupContainer" style="padding: 2px;
                                                            line-height: 20px; width: 99%">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divSimilarAdhocLanesMain">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                    <tr>
                                        <td>
                                            <table class="tbl" style="width: 100%; border-collapse: collapse; border-spacing: 0px">
                                                <tr>
                                                    <td style="text-align: right">
                                                        <span id="Span5">&nbsp;</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="group">
                                                        <div class="groupTitle">
                                                            &nbsp;Similar lanes in approved as adhoc list</div>
                                                        <div id="divSimilarAdhocLanes" class="groupContainer" style="padding: 2px; line-height: 20px;
                                                            width: 99%" style="display: none">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="text-align: left;">
                            </div>
                        </div>
                        <div>
                            <hr />
                            <span>Above lanes were queried by 'Origin Airport', 'Destination Airport', 'Destination
                                City', 'Mode Of Transit' and 'WD Ship Method' </span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.8.14.custom.min.js" defer="defer"></script>
<script type="text/javascript" src="Scripts/NewAirRateRequest.min1.js"></script>
<script type="text/javascript" src="Scripts/FormValidation.js" defer="defer"></script>
<%--<script type="text/javascript" src="Scripts/Test.js"></script>--%>
</html>
