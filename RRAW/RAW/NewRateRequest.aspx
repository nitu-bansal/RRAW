<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewRateRequest.aspx.vb"
    Inherits="RRAW.NewRateRequest" ValidateRequest="true" %>

<!DOCTYPE html PUBLIC"-//W3C//DTD XHTML 1.0 Transitional//EN""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/NewRateRequest.min.css" rel="stylesheet" type="text/css" />

   <%-- <script language="javascript" type="text/javascript">

        function btnPostNewRateRequest_onClick() {
            if (('<%=Session("UserName") %>').toUpper() != "KATIE TRANG" && '<%= Now %>' <= '<%=CDate("12-Apr-2012 23:59:59") %>')
                if (Page_ClientValidate() == false) return false;

            DisplayProgress('Posting new rate request...');
            return true;
        }
    </script>--%>

</head>
<body style="margin-bottom: 0">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <div style="text-align: center;" class="pageWidth">
            <asp:HiddenField ID="hidCurrentDateTime" runat="server" EnableViewState="False" />
            <asp:Label ID="lblTitle" runat="server" Text="New Air Rate Request" Style="text-decoration: underline;
                font-size: small; font-weight: 700" EnableViewState="False"></asp:Label>
        </div>
        <table>
            <tr>
                <td class="pageWidth">
                    <!--<div id="PanelNewRateRequest"style="overflow-y: auto; height: 489px;"class="pageWidth">-->
                    <asp:Panel ID="PanelNewRateRequest" Style="overflow: auto;" CssClass="pageWidth"
                        runat="server" class="inputContainer">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="tbl">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblRequestDate" runat="server" EnableViewState="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="group">
                                                <div class="groupTitle">
                                                    &nbsp;Cargo Information</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 111px">
                                                            HAWB:
                                                        </td>
                                                        <td style="width: 218px">
                                                            <asp:TextBox ID="txtHAWBNumber" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 111px">
                                                            Mode of Transit:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbCEVATransitMode" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="AIR">AIR</asp:ListItem>
                                                                <asp:ListItem Value="AIR / GROUND">AIR / GROUND</asp:ListItem>
                                                                <asp:ListItem Value="GROUND / AIR">GROUND / AIR</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbCEVATransitMode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Shipment Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShipDate" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>(MM/DD/YYYY)
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtShipDate"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtShipDate"
                                                                Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" SetFocusOnError="True"
                                                                Type="Date" CultureInvariantValues="True"></asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            Service Level:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbServiceLevel" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">15</asp:ListItem>
                                                                <asp:ListItem Value="12">20</asp:ListItem>
                                                                <asp:ListItem Value="13">25</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbServiceLevel"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Chg. Weight (KG):
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWeight" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtWeight"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtWeight"
                                                                Display="Dynamic" ErrorMessage="Invalid Value" Operator="DataTypeCheck" SetFocusOnError="True"
                                                                Type="Double"></asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            Forwarder Svc.:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbForwarderService" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="ATA">ATA</asp:ListItem>
                                                                <asp:ListItem Value="ATD">ATD</asp:ListItem>
                                                                <asp:ListItem Value="DTA">DTA</asp:ListItem>
                                                                <asp:ListItem Value="DTD">DTD</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmbCEVATransitMode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                                        <td style="width: 111px">
                                                            Shipper Name:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtShipperName" runat="server" Width="200px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShipperName"
                                                                    Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            City:
                                                        </td>
                                                        <td style="width: 218px">
                                                            <asp:TextBox ID="txtOriginCity" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOriginCity"
                                                                    Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 111px">
                                                            Origin Port Code:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOriginAirport" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOriginAirport"
                                                                    Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOriginZipCode" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOriginZipCode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbOriginRegion" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="AP">AP</asp:ListItem>
                                                                <asp:ListItem Value="EU">EU</asp:ListItem>
                                                                <asp:ListItem Value="US">US</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="cmbOriginRegion"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                                        <td style="width: 111px">
                                                            Consignee Name:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtConsigneeName" runat="server" Width="200px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtConsigneeName"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dest. Port Code:
                                                        </td>
                                                        <td style="width: 218px">
                                                            <asp:TextBox ID="txtDestAirport" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDestAirport"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 111px">
                                                            City:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDestCity" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDestCity"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            State:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDestState" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtDestState"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Postal Zipcode:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDestZipCode" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDestZipCode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Region:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbDestRegion" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="AP">AP</asp:ListItem>
                                                                <asp:ListItem Value="EU">EU</asp:ListItem>
                                                                <asp:ListItem Value="US">US</asp:ListItem>
                                                                <asp:ListItem Value="LATAM">LATAM</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cmbDestRegion"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Country Code:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbDestCountry" runat="server" Width="104px">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="AP">AP</asp:ListItem>
                                                                <asp:ListItem Value="AU">AU</asp:ListItem>
                                                                <asp:ListItem Value="AT">AT</asp:ListItem>
                                                                <asp:ListItem Value="BE">BE</asp:ListItem>
                                                                <asp:ListItem Value="BG">BG</asp:ListItem>
                                                                <asp:ListItem Value="BH">BH</asp:ListItem>
                                                                <asp:ListItem Value="BR">BR</asp:ListItem>
                                                                <asp:ListItem Value="CA">CA</asp:ListItem>
                                                                <asp:ListItem Value="CH">CH</asp:ListItem>
                                                                <asp:ListItem Value="CN">CN</asp:ListItem>
                                                                <asp:ListItem Value="CS">CS</asp:ListItem>
                                                                <asp:ListItem Value="CZ">CZ</asp:ListItem>
                                                                <asp:ListItem Value="DE">DE</asp:ListItem>
                                                                <asp:ListItem Value="DJ">DJ</asp:ListItem>
                                                                <asp:ListItem Value="EE">EE</asp:ListItem>
                                                                <asp:ListItem Value="ES">ES</asp:ListItem>
                                                                <asp:ListItem Value="EU">EU</asp:ListItem>
                                                                <asp:ListItem Value="FI">FI</asp:ListItem>
                                                                <asp:ListItem Value="FR">FR</asp:ListItem>
                                                                <asp:ListItem Value="GB">GB</asp:ListItem>
                                                                <asp:ListItem Value="GR">GR</asp:ListItem>
                                                                <asp:ListItem Value="GU">GU</asp:ListItem>
                                                                <asp:ListItem Value="HK">HK</asp:ListItem>
                                                                <asp:ListItem Value="HR">HR</asp:ListItem>
                                                                <asp:ListItem Value="HU">HU</asp:ListItem>
                                                                <asp:ListItem Value="IE">IE</asp:ListItem>
                                                                <asp:ListItem Value="IL">IL</asp:ListItem>
                                                                <asp:ListItem Value="IN">IN</asp:ListItem>
                                                                <asp:ListItem Value="IT">IT</asp:ListItem>
                                                                <asp:ListItem Value="JP">JP</asp:ListItem>
                                                                <asp:ListItem Value="KE">KE</asp:ListItem>
                                                                <asp:ListItem Value="KR">KR</asp:ListItem>
                                                                <asp:ListItem Value="MX">MX</asp:ListItem>
                                                                <asp:ListItem Value="MY">MY</asp:ListItem>
                                                                <asp:ListItem Value="NL">NL</asp:ListItem>
                                                                <asp:ListItem Value="NP">NP</asp:ListItem>
                                                                <asp:ListItem Value="NZ">NZ</asp:ListItem>
                                                                <asp:ListItem Value="PH">PH</asp:ListItem>
                                                                <asp:ListItem Value="PL">PL</asp:ListItem>
                                                                <asp:ListItem Value="PO">PO</asp:ListItem>
                                                                <asp:ListItem Value="PT">PT</asp:ListItem>
                                                                <asp:ListItem Value="RO">RO</asp:ListItem>
                                                                <asp:ListItem Value="SA">SA</asp:ListItem>
                                                                <asp:ListItem Value="SE">SE</asp:ListItem>
                                                                <asp:ListItem Value="SG">SG</asp:ListItem>
                                                                <asp:ListItem Value="SI">SI</asp:ListItem>
                                                                <asp:ListItem Value="SK">SK</asp:ListItem>
                                                                <asp:ListItem Value="TH">TH</asp:ListItem>
                                                                <asp:ListItem Value="TN">TN</asp:ListItem>
                                                                <asp:ListItem Value="TR">TR</asp:ListItem>
                                                                <asp:ListItem Value="TU">TU</asp:ListItem>
                                                                <asp:ListItem Value="TW">TW</asp:ListItem>
                                                                <asp:ListItem Value="US">US</asp:ListItem>
                                                                <asp:ListItem Value="VN">VN</asp:ListItem>
                                                                <asp:ListItem Value="ZA">ZA</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="cmbDestCountry"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                                    &nbsp;Other Information</div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 112px">
                                                            Freight Forwarder:
                                                        </td>
                                                        <td style="width: 218px">
                                                            <asp:DropDownList ID="cmbFreightForwarder" runat="server" Width="104px">
                                                                <asp:ListItem Value="CEVA">CEVA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 111px">
                                                            Forwarder Zip:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtForwarderZipcode" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtForwarderZipcode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            XDoc AMS or DTD:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="cmbDocType" runat="server">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="ATA">ATA</asp:ListItem>
                                                                <asp:ListItem Value="ATD">ATD</asp:ListItem>
                                                                <asp:ListItem Value="DTA">DTA</asp:ListItem>
                                                                <asp:ListItem Value="DTD">DTD</asp:ListItem>
                                                                <asp:ListItem Value="WILL CALL AMS">WILL CALL AMS</asp:ListItem>
                                                                <asp:ListItem Value="X-DOC">X-DOC</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="cmbDocType"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Custom Clearance:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="cmbCustomClearanceMode" runat="server">
                                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                                <asp:ListItem Value="WCC">WITH CUSTOM CLEARANCE (WCC)</asp:ListItem>
                                                                <asp:ListItem Value="WOCC">WITHOUT CUSTOM CLEARANCE (WOCC)</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="cmbCustomClearanceMode"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            WD Ship Method:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="cmbShipMethod" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmbShipMethod"
                                                                Display="Dynamic" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                                    &nbsp;<asp:Label ID="lblRateGroupTitle" Font-Bold="true" runat="server" Text="Rates"></asp:Label></div>
                                                <table class="groupContainer">
                                                    <tr>
                                                        <td style="width: 111px">
                                                            Min.:
                                                        </td>
                                                        <td style="width: 218px">
                                                            <asp:TextBox ID="txtMinFreightRate" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                CssClass="ValueField number"></asp:TextBox>
                                                                <asp:Label ID="lblMinFreightRateUSD" runat="server" Text="" style="color:Blue;font-size:7pt" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 111px">
                                                            Per Kilo:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFreightRate" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                CssClass="ValueField number"></asp:TextBox>
                                                                <asp:Label ID="lblFreightRateUSD" runat="server" Text="" style="float:right;color:Blue;font-size:7pt" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Security:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSecurityRate" runat="server" Width="100px" AutoCompleteType="Disabled"
                                                                CssClass="ValueField number"></asp:TextBox>
                                                                <asp:Label ID="lblSecurityRateUSD" runat="server" Text="" style="color:Blue;font-size:7pt" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            Methodology used to determine rate<br />
                                                            <asp:TextBox ID="txtRateDeterMethodology" runat="server" TextMode="MultiLine" Width="550px"
                                                                Rows="2" AutoCompleteType="Disabled" onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            Other Charges<br />
                                                            <asp:TextBox ID="txtOtherCharges" runat="server" Rows="2" TextMode="MultiLine" Width="550px"
                                                                AutoCompleteType="Disabled" onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="550px" Rows="3"
                                                                AutoCompleteType="Disabled" onblur="this.value=this.value.toUpperCase()"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlAttachments">
                                                    <br />
                                                    <div class="group">
                                                        <div class="groupTitle">
                                                            &nbsp;Add New Attachments</div>
                                                        <table class="groupContainer">
                                                            <tr>
                                                                <td>
                                                                    <div id="divAttachments">
                                                                        <div id="divAttachmentControls">
                                                                            <asp:FileUpload ID="fUpload_1" runat="server" size="50" EnableViewState="false" onchange="attachFile(1);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_2" runat="server" size="50" EnableViewState="false" onchange="attachFile(2);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_3" runat="server" size="50" EnableViewState="false" onchange="attachFile(3);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_4" runat="server" size="50" EnableViewState="false" onchange="attachFile(4);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_5" runat="server" size="50" EnableViewState="false" onchange="attachFile(5);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_6" runat="server" size="50" EnableViewState="false" onchange="attachFile(6);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_7" runat="server" size="50" EnableViewState="false" onchange="attachFile(7);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_8" runat="server" size="50" EnableViewState="false" onchange="attachFile(8);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_9" runat="server" size="50" EnableViewState="false" onchange="attachFile(9);moveDivToBottom('PanelNewRateRequest')" />
                                                                            <asp:FileUpload ID="fUpload_10" runat="server" size="50" EnableViewState="false"
                                                                                onchange="attachFile(10);moveDivToBottom('PanelNewRateRequest')" /></div>
                                                                        <div style="display: none" id="divAttachment_1" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_1" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_1"
                                                                                    href="#" class="attachmentLink" onclick="lookupForFile(1);">Attach a file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_2" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_2" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_2"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(2);">Attach another file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_3" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_3" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_3"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(3);">Attach another file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_4" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_4" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_4"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(4);">Attach another file</a><br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_5" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_5" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_5"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(5);">Attach another file</a>
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_6" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_6" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_6"
                                                                                    href="#" class="attachmentLink" onclick="lookupForFile(6);">Attach another file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_7" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_7" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_7"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(7);">Attach another file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_8" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_8" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_8"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(8);">Attach another file</a>
                                                                            <br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_9" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_9" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_9"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(9);">Attach another file</a><br />
                                                                        </div>
                                                                        <div style="display: none" id="divAttachment_10" class="attachmentDiv">
                                                                            <img class="attachmentImage" src="Images/cleardot.gif" alt="" />&nbsp;<asp:CheckBox
                                                                                ID="chkAttachment_10" runat="server" Checked="true" />&nbsp;<a id="linkAttachFile_10"
                                                                                    class="attachmentLink" href="#" onclick="lookupForFile(10);">Attach another file</a>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <br />
                                                <asp:Button ID="btnSave" runat="server" OnClientClick="btnSave_onClick();" Text="Save"
                                                    EnableViewState="False" />
                                                <asp:Button ID="btnTransferToTariff" runat="server" EnableViewState="False" OnClientClick="btnTransferToTariff_onClick();"
                                                    Text="Transfer To Tariff" Visible="false" />
                                                <asp:Button ID="btnPostComment" runat="server" Text="Post Comment" Visible="False"
                                                    EnableViewState="False" OnClientClick="return btnPostComment_onClick();" />
                                                <asp:Button ID="btnApprove" runat="server" OnClientClick="return btnApprove_onClick();"
                                                    Text="Approve" Visible="false" EnableViewState="False" />
                                                <asp:Button ID="btnApproveAsAdhoc" runat="server" Text="Approve As Adhoc" 
                                                    Visible="false" EnableViewState="False" />
                                                <asp:Button ID="btnRevoke" runat="server" OnClientClick="btnRevoke_onClick();" Text="Revoke"
                                                    Visible="false" EnableViewState="False" />
                                                <asp:Button ID="btnPostNewRateRequest" runat="server" EnableViewState="False" Text="Post New Rate Request"/>
                                                <asp:Button ID="btnBackToDashboard" runat="server" Text="Back To Dashboard" OnClientClick="locate('dashboardlink');"
                                                    EnableViewState="False" UseSubmitBehavior="False" />
                                                <asp:Button ID="btnNeedToReviseRate" runat="server" Text="Need To Revise Rate" OnClientClick="btnNeedToReviseRate_onClick();"
                                                    EnableViewState="False" Visible="False" />
                                                <asp:Button ID="btnSendBackToReviseRate" runat="server" Text="Send Back To Revise Rate"
                                                    Visible="False" OnClientClick="return btnSendBackToReviseRate_onClick();" EnableViewState="False" />
                                                <asp:Button ID="btnReject" runat="server" OnClientClick="return btnReject_onClick();" Text="Reject"
                                                    Visible="False" EnableViewState="False" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClientClick="function f(){var v=btnDelete_onClick(); if(v==true){locate('dashboardlink'); return true;}else{return false;}}return f();"
                                                    EnableViewState="False" />
                                                <asp:Button ID="btnRemoveAllComments" runat="server" Text="Remove All Comments" Visible="False"
                                                    OnClientClick="btnRemoveAllComments_onClick();" EnableViewState="False" />
                                                <asp:Button ID="btnApprove_Permanent" runat="server" Text="Approved as Permanent"
                                                    Visible="False" OnClientClick="return btnApprove_Permanent_onClick();" EnableViewState="False" />
                                                <asp:Button ID="btnSendBackToRequestor" runat="server" Text="Send Back To Requestor" Visible="False"
                                                    OnClientClick="btnSendBackToRequestor_onClick();" EnableViewState="False" />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlPreviousComments" Visible="false" EnableViewState="false">
                                        <br />
                                        <div class="group">
                                            <div class="groupTitle">
                                                &nbsp;Previous Comments</div>
                                            <table class="groupContainer">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rptrPreviousComments" runat="server" DataSourceID="SqlDataSource8"
                                                            EnableViewState="False">
                                                            <HeaderTemplate>
                                                                <% 
                                                                    If rptrPreviousComments.Items.Count <= 0 Then
                                                                        lblNoData.Text = "No previous comments available."
                                                                    End If%></HeaderTemplate>
                                                            <ItemTemplate>
                                                                <b>Commented by
                                                                    <%# DataBinder.Eval(Container.DataItem,"CommentedBy")%>
                                                                    on
                                                                    <%# DataBinder.Eval(Container.DataItem,"CommentDate")%></b>
                                                                <br />
                                                                &nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"Comment")%><br />
                                                                -------------------------------------------------------------------------------------------------------------
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Label runat="server" ID="lblNoData" EnableViewState="False"></asp:Label>
                                                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" SelectCommand="GetRateRequestComments"
                                                            SelectCommandType="StoredProcedure" EnableViewState="False">
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter DefaultValue="0" Name="RateRequestID" QueryStringField="RateRequestID"
                                                                    DbType="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="text-align: left;" class="pageWidth">
                        <hr />
                    </div>
                    <asp:Label ID="lblStatus" runat="server" CssClass="statusLabel" ForeColor="#000"
                        Text="Add lane details and save to generate new rate request. All fields are compulsory except for rates and HAWB Number."
                        EnableViewState="False"></asp:Label>
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
                <td>
                    <asp:Panel runat="server" ID="pnlCapture" EnableViewState="false">
                        Paste data from FAMS & OFS here to capture<br />
                        <asp:TextBox ID="txtCapture" runat="server" Height="473px" TextMode="MultiLine" Width="530px"></asp:TextBox><hr />
                        <center>
                            <input id="btnCapture" type="button" value="Capture" onclick="getData('txtCapture');" /></center>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlAdditionalInformation" EnableViewState="false" Visible="false">
                        <div id="divAdditionalInformationContainer">
                            <div class="group">
                                <div class="groupTitle">
                                    &nbsp;Current Attachments</div>
                                <asp:GridView ID="gridAttachments" runat="server" EnableModelValidation="True" CssClass="Attachments groupContainer"
                                    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" ShowHeader="False">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="Uploaded File" DataNavigateUrlFormatString="DownloadFile.aspx?FileName=~/Attachments/RateRequestAttachment/{0}"
                                            DataTextField="Uploaded File" HeaderText="Uploaded File" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No attachments found.</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="RemoveAttachment"
                                DeleteCommandType="StoredProcedure" SelectCommand="GetAttachmentsByReferenceID"
                                SelectCommandType="StoredProcedure">
                                <DeleteParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:Parameter DbType="Int32" DefaultValue="7" Name="AttachmentTypeID" />
                                    <asp:QueryStringParameter DbType="Int32" DefaultValue="0" Name="ReferenceID" QueryStringField="RateRequestID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            <div id="divRateChangesHistory" class="group">
                                <div class="groupTitle">
                                    &nbsp;Rate Changes History</div>
                                <div class="groupContainer" style="padding: 3px; width: 98%;">
                                    <asp:Repeater ID="rptrRateRequestHistory" runat="server" DataSourceID="SqlDataSource9"
                                        EnableViewState="False">
                                        <HeaderTemplate>
                                            <% 
                                                If rptrRateRequestHistory.Items.Count <= 0 Then
                                                    lblNoData2.Text = "No previous changes available."
                                                End If%></HeaderTemplate>
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem,"UpdatorName")%>
                                                has updated rates on
                                                <%# DataBinder.Eval(Container.DataItem,"UpdateDate")%></b>
                                            <br />
                                            <table cellpadding="5" cellspacing="0" class="BorderedTable">
                                                <tr>
                                                    <th>
                                                    </th>
                                                    <th>
                                                        Min Freight Rate
                                                    </th>
                                                    <th>
                                                        Freight Rate
                                                    </th>
                                                    <th>
                                                        Security Rate
                                                    </th>
                                                    <th>
                                                        Other Charges
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th style="text-align: left">
                                                        Updated Rates
                                                    </th>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"MinFreightRate")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"FreightRate")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"SecurityRate")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"OtherCharges")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align: left">
                                                        Old Rates
                                                    </th>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"MinFreightRate_Old")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"FreightRate_Old")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"SecurityRate_Old")%>
                                                    </td>
                                                    <td align="right">
                                                        <%# DataBinder.Eval(Container.DataItem,"OtherCharges_Old")%>
                                                    </td>
                                                </tr>
                                            </table>
                                            --------------------------------------------------------------------------------------------------------
                                            <br />
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label runat="server" ID="lblNoData2"></asp:Label>
                                </div>
                                <asp:SqlDataSource ID="SqlDataSource9" runat="server" SelectCommand="GetAirRateRequestHistory"
                                    SelectCommandType="StoredProcedure" EnableViewState="False">
                                    <SelectParameters>
                                        <asp:QueryStringParameter DefaultValue="0" Name="RateRequestID" QueryStringField="RateRequestID"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <br />
                            <div id="SimilarLane">
                                <div id="divSimilarRateRequests" class="group">
                                    <div class="groupTitle">
                                        &nbsp;Similar rate requests</div>
                                    <asp:GridView ID="gridSimilarRateRequests" runat="server" EnableModelValidation="True"
                                        CssClass="SimilarLanes groupContainer">
                                        <EmptyDataTemplate>
                                            No Similar Rate Request Available</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <br />
                                <div id="divSimilarTariffLanes" class="group">
                                    <div class="groupTitle">
                                        &nbsp;Similar lanes in tariff</div>
                                    <asp:GridView ID="gridSimilarTariffLanes" runat="server" CssClass="SimilarLanes groupContainer">
                                        <EmptyDataTemplate>
                                            No Similar Tariff Lane Available</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <br />
                                <div id="divSimilarAdhocLanes" class="group">
                                    <div class="groupTitle">
                                        &nbsp;Similar lanes in approved as adhoc list</div>
                                    <asp:GridView ID="gridSimilarAdhocLanes" runat="server" CssClass="SimilarLanes groupContainer">
                                        <EmptyDataTemplate>
                                            No Similar Approved As Adhoc Lane Available</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div style="text-align: left;">
                            <hr />
                        </div>
                        <span>Above lanes were queried by 'Origin Airport', 'Destination Airport', 'Destination
                            City', 'Mode Of Transit' and 'WD Ship Method'</span>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/NewRateRequest.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
</html>
