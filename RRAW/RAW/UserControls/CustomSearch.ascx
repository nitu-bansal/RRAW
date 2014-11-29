<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CustomSearch.ascx.vb"
    Inherits="RRAW.CustomSearch" %>
<table style="width: 100%;">
    <tr>
        <td>
            <div id="PanelNewRateRequest" class="PanelsDiv Panel1Width" style="box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                -ms-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -moz-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);
                -webkit-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2); -o-box-shadow: 0 2px 2px -1px rgba(0, 0, 0, .2);">
                <table class="tbl MainTable">
                    <tr>
                        <td>
                            <input type="hidden" class="bigdrop select2-offscreen" id="cmbSelect"
                                tabindex="-1" /> &nbsp;&nbsp; <input type="button" id="btnSearch" value="Search" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                        <div id="divTariff" style="width: 1620px; max-height: 550px; overflow: auto;padding-top: 20px;"></div>
                        </td>
                    </tr>
                </table>
            </div>
            
        </td>
    </tr>
</table>
