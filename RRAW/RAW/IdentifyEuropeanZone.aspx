<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IdentifyEuropeanZone.aspx.vb"
    Inherits="RRAW.IdentifyEuropeanZone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/demo_table_jui.css" rel="stylesheet" type="text/css" />
    <link href="CSS/IdentifyEuropeanZon.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.ui.accordion.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <%-- <link href="CSS/jquery.dataTables.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.accordion.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dataTables.columnFilter.js" type="text/javascript"></script>
    <script src="Scripts/HighlightCell.js" type="text/javascript"></script>
    <script type="text/javascript">
//        var prm = Sys.WebForms.PageRequestManager.getInstance(); prm.add_pageLoaded(pageLoaded);

//        function pageLoaded() { addReflections(); }

        function addReflections() {
            $('#<%=gvEuropeanZone.ClientID %>').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10,
                "sScrollX": "100%",
                "sScrollY": "193px",
                "bFilter": false,
                "aoColumns": [{ "sWidth": "200px", "bSortable": false }, { "sWidth": "125px", "bSortable": false },
                                    { "sWidth": "125px", "bSortable": false },
                                     { "sWidth": "165px", "bSortable": false }, { "sWidth": "150px", "bSortable": false}]
            });
        }
        $(document).ready(function () {
            //alert("Called");
            $(function () {
                $("#accordion").accordion({
                    autoHeight: false,
                    navigation: true,
                    collapsible: true,
                    clearStyle: true,
                    active: 1
                });
            });
            //.columnFilter({ "sPlaceHolder": "head:before",
            //"aoColumns": [{ type: "text" }, null, { type: "text" }, null, null]
            //});

        });
        //        $("#H1").live("click", function (e) {

        //            if ($("#H1").attr('class').toString().indexOf("ui-state-active") > 0) {
        //                //alert("H1 Clicked.");
        //                $("#H1").removeClass("ui-state-active");
        //                //$("#H2").addClass("ui-state-default");
        //                $("#H2").addClass("ui-state-active");
        //                //$("#H1").removeClass("ui-state-default");
        //                $("#D2").addClass("ui-accordion-content-active");
        //                $("#D1").removeClass("ui-accordion-content-active");
        //                $("#H1 span").removeClass("ui-icon-triangle-1-s");
        //                $("#H1 span").addClass("ui-icon-triangle-1-e");
        //                $("#H2 span").addClass("ui-icon-triangle-1-s");
        //                $("#H2 span").removeClass("ui-icon-triangle-1-e");
        //                $("#D2").css({ display: "block" });
        //                $("#D1").css({ display: "none" });
        //            }
        //        });
        //        $("#H2").live("click", function (e) {
        //            //alert("H2 Clicked");
        //            if ($("#H2").attr('class').toString().indexOf("ui-state-active") > 0) {
        //                $("#H2").removeClass("ui-state-active");
        //                //$("#H2").addClass("ui-state-default");
        //                $("#H1").addClass("ui-state-active");
        //                //$("#H1").removeClass("ui-state-default");
        //                $("#D1").addClass("ui-accordion-content-active");
        //                $("#D2").removeClass("ui-accordion-content-active");
        //                $("#H2 span").removeClass("ui-icon-triangle-1-s");
        //                $("#H2 span").addClass("ui-icon-triangle-1-e");
        //                $("#H1 span").addClass("ui-icon-triangle-1-s");
        //                $("#H1 span").removeClass("ui-icon-triangle-1-e");
        //                $("#D1").css({ display: "block" });
        //                $("#D2").css({ display: "none" });

        //            }
        //        });
    </script>
</head>
<body onload="top.document.getElementById('processing_image').style.display = 'none';">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
        <%-- <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance(); prm.add_pageLoaded(pageLoaded);

        function pageLoaded() { addReflections(); }

       
        </script>--%>
    </asp:ToolkitScriptManager>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance(); prm.add_pageLoaded(pageLoaded);

        function pageLoaded() { addReflections(); }</script>
    <div>
        <span style="font-weight: bold; text-decoration: underline; margin-left: 475px;">European
            Zones Code Library</span>
        <%-- <span style="font-weight: bold; text-decoration: underline;">Steps to select Correct European Zone Code</span><br />--%>
        <fieldset class="FieldSet">
            <legend class="Legend">Steps to select correct European zones code</legend>
            <table>
                <tr>
                    <td class="RuleStyle">
                        Step 1:
                    </td>
                    <td>
                        Go to "How to read EU zones code from WD Commercial invoice". Samples are provided for your reference. 
                    </td>
                </tr>
                <tr>
                    <td class="RuleStyle">
                       Step 2:
                    </td>
                    <td>
                        Go to European zones code library to filter the correct EU zones code. 
                    </td>
                </tr>
                <tr>
                    <td class="RuleStyle">
                       Step 3:
                    </td>
                    <td>
                        Go to "Quick Search", enter the Country Name and "Postcode" the first 2 digits of the postcode from the address. 
                    </td>
                </tr><tr>
                    <td class="RuleStyle">
                       Step 4:
                    </td>
                    <td>
                        Click Filter, refer to the Europe Zone column, the correct EU zones code is highlighted in Yellow.  
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="height: 445px; overflow: auto;">
            <div id="accordion">
                <h3 id="H1">
                    <a href="#">How to read EU zones code from WD Commercial invoice?</a></h3>
                <div id="D1" class="AccordionHight">
                    <img src="CSS/images/FindEuropeanZone.png" alt="Sample image to get European Zone" />
                </div>
                <h3 id="H2">
                    <a href="#">European Zones Code Library</a></h3>
                <div id="D2"  class="AccordionHight">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr style="vertical-align: top">
                            <td width="225px">
                                <fieldset class="FieldSet" style="height: 300px">
                                    <legend class="Legend">Quick Search</legend>
                                    <div id="divFilter" style="height: 275px; width: 220px; overflow: auto; box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                                        -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                                        -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); background-color: #EAEBFF">
                                        <table style="width: 217px;
                                            background-color: transparent;" cellpadding="0" cellspacing="0">
                                            <tr class="ui-widget-header">
                                                <td width="70%">
                                                    Country Name
                                                </td>
                                                <td width="29%">
                                                    PostCode
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" width="70%" bgcolor="#EFF3FB" style="text-align: left;">
                                                    <asp:TextBox ID="txtCountryName" runat="server" AutoCompleteType="Disabled" style="background-color:#DEB887;width:80%;" placeholder="Country Name"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtCountryName_AutoCompleteExtender1" runat="server"
                                                        DelimiterCharacters="" Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx"
                                                        ServiceMethod="GetEuropeanZoneAutoComplete" TargetControlID="txtCountryName"
                                                        UseContextKey="True" CompletionInterval="1" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ContextKey="CountryName">
                                                    </asp:AutoCompleteExtender>
                                                </td>
                                                <td valign="top" width="29%" bgcolor="#EFF3FB" >
                                                    <asp:TextBox ID="txtPC2" runat="server" Width="97%" AutoCompleteType="Disabled" style="background-color:#FFDAB9;" placeholder="Post Code"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtPC2_AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServicePath="~/WebServices/AutoComplete.asmx" ServiceMethod="GetEuropeanZoneAutoComplete"
                                                        TargetControlID="txtPC2" UseContextKey="True" CompletionInterval="1" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ContextKey="postalcode">
                                                    </asp:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="tdOperations" colspan="2" align="left" style="background-color: transparent;
                                                    height: 30px;">
                                                    <div id="divOperations" style="position: relative; margin-top: 20px;">
                                                        <asp:Button ID="btnFilter" runat="server" Text="Apply Filter" EnableViewState="false"
                                                            OnClientClick="document.getElementById('DivUpdateProgress').style.display = 'inline';" />
                                                        <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" OnClientClick="$('#txtPC2').val('');$('#txtCountryName').val('');document.getElementById('DivUpdateProgress').style.display = 'inline';"
                                                            EnableViewState="false" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                                            </Triggers>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnRemoveFilter" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div id="DivUpdateProgress">
                                                    <table>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <div class="loading_notifier">
                                                                    <table class="loading_notifier_box">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td valign="middle" style="font-size: 10px; font-weight: 700;">
                                                                                    Processing... please wait
                                                                                    <div class="processing_image">
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset class="FieldSet">
                                    <legend class="Legend">European Zones Code</legend>
                                    <div class="dataTables_wrapper">
                                        <asp:UpdatePanel ID="updGrid" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                                            </Triggers>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnRemoveFilter" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:GridView ID="gvEuropeanZone" runat="server">
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- #leftPane -->
                <!-- #rightTopPane-->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
