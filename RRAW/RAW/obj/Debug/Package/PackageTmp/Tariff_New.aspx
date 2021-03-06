﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Tariff_New.aspx.vb" Inherits="RRAW.Tariff_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <link href="media/css/TableTools.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Tariff.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="CSS/TableTools.css" rel="stylesheet" type="text/css" />--%>
    
    <link href="CSS/demo_table.css" rel="stylesheet" type="text/css" />
    <link href="CSS/demo_table_jui.css" rel="stylesheet" type="text/css" />
</head>
<body onload="try{locateonload(this.offsetTop)}catch(e){};try{top.document.getElementById('processing_image').style.display='none'}catch(e){};">
    <form id="form1" runat="server">
    <!--<div class="Freezing" style="text-align: center; width: 100%; background-color: #fff">-->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="False"
        EnableViewState="False" ScriptMode="Release" EnableSecureHistoryState="False"
        LoadScriptsBeforeUI="False">
    </asp:ToolkitScriptManager>
    <div style="position: fixed; text-align: center; text-decoration: underline; width: 100%;">
        <br />
        <span class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active"
            id="lblTitle" style="text-decoration: underline; font-size: small; font-weight: 700;">
            Air Tariff Effectives</span>
    </div>
    <br />
    <br />
    <br />
    <%--<div id="div1" style="width: 100%;">
    <table  style="text-align: center; position: relative;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
            -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);"
            cellpadding="5" cellspacing="0">            
            <tr>
                <td id="tdOperations" colspan="5" align="left" style="background-color: White; height: 30px;">
                    <div id="divOperations" >                        
                        <asp:Button ID="btnExportToCSV" runat="server" Text="Export to CSV" Visible="true"
                            EnableViewState="false" />
                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Visible="true"
                            EnableViewState="false" />                        
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>--%>
    <div id="divTariff" style="height: 715px; width: 100%; overflow: auto;">
        <%--<div id="divOperationAdjustments" style="height: 100px;">
        </div>--%>
        <!--<div style="position: absolute; left: 50%; top: 50%; width: 32px;">-->
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="False" DisplayAfter="500">
            <ProgressTemplate>
                <div id="DivUpdateProgress2">
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
            </ProgressTemplate>
        </asp:UpdateProgress>
        <!--</div>-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <asp:HiddenField ID="hidSelections" runat="server" />
                <div class="dataTables_wrapper">
                    <asp:GridView ID="gridTariff" runat="server" CellPadding="5" ForeColor="#333" CaptionAlign="Right">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                    </asp:GridView>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                <asp:Label ID="lblLaneNotFoundMessage" runat="server" Text="Above lane is not available in RAW Portal, "
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="linkPostNewRateRequest" runat="server" OnClick="linkPostNewRateRequest_Click"
                    Visible="False" OnClientClick="locate('newraterequestlink');">Click here to Post a New Rate Request</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>

<script src="Scripts/Tariff_New.min.js" type="text/javascript"></script>
<script src="Scripts/Navigation.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.dataTables.js" type="text/javascript"></script>
<script src="Scripts/jquery.dataTables.columnFilter.js" type="text/javascript"></script>
<script src="Scripts/HighlightCell.js" type="text/javascript"></script>
<script src="Scripts/ZeroClipboard.js" type="text/javascript"></script>
<script src="Scripts/TableTools.js" type="text/javascript"></script>
<script src="Scripts/TableTools.min.js" type="text/javascript"></script>
<script src="Scripts/HighlightCell.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance(); prm.add_pageLoaded(pageLoaded);

    function pageLoaded() { addReflections(); }

    function addReflections() {
        var trhead ="<tr>"+ $('#<%=gridTariff.ClientID %> tr:first').html()+"</tr>";
        $('#<%=gridTariff.ClientID %>').prepend(
        $('<thead></thead>').append($('#<%=gridTariff.ClientID %> tr:first').remove()));
        $('#<%=gridTariff.ClientID %> thead').prepend(trhead);
        $('#<%=gridTariff.ClientID %>').dataTable({
            "bProcessing": true,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "sScrollX": "auto",
            "sScrollY": "auto",
            "sDom": 'T<"clear">lfrtip',
            "oTableTools": {
                "sRowSelect": "multi",
                "sSwfPath": "/media/swf/copy_csv_xls_pdf.swf",
                "aButtons": ["copy", {
                    "sExtends": "csv",
                    "sFileName": "Tariff_Air.csv",
                    "bFooter": false
                },
                                    {
                                        "sExtends": "xls",
                                        "sFileName": "Tariff_Air.xls",
                                        "bFooter": false
                                    }]
            }
        }).columnFilter({ "sPlaceHolder": "head:after" });

                            $('.DataTables_sort_wrapper').first().click();
                            $('.DataTables_sort_wrapper').first().click();                           
//        var cells = [];
//        var rows = $("#gridTariff").dataTable().fnGetNodes();
//        for (var i = 0; i < rows.length; i++) {
//            cells.push($(rows[i]).find("td:eq(0)").children(0).html());
//        }

    }

//    $('#btnExportToExcel').click(function () {
//        var cells = [];
//        var rows = $("#gridTariff").dataTable().fnGetNodes();
//        for (var i = 0; i < rows.length; i++) {
//            cells.push($(rows[i]).find("td:eq(0)").children(0).html());
//        }
//        alert(cells);
//        return false;
//    });


    
   

</script>
</html>
