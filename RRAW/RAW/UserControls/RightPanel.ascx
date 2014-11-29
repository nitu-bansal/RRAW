<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RightPanel.ascx.vb"
    Inherits="RRAW.RightPanel" %>
<form id="EntryForm1">

    <table style="width: 100%; border-collapse: collapse; border-spacing: 0px">
        <tr>
            <td>
                <table class="tbl MainTable" style="width: 100%;">
                  
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

</form>
