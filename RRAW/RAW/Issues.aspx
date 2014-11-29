<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Issues.aspx.vb" Inherits="RRAW.Issues" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Issues.min.css" rel="stylesheet" type="text/css" />
</head>
<body id="interior" class="enhanced">
    <form runat="server" id="entriesForm">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptLocalization="False"
        ScriptMode="Release" EnableSecureHistoryState="False" LoadScriptsBeforeUI="False">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Issues.asmx" InlineScript="true" />
        </Services>
    </asp:ScriptManager>
    <div id="siteContain">
        <div id="header">
            <a id="siteLogo" title="Click here to navigate to RRAW Portal Main Page" href="Master.aspx">
                <div id="title">
                    RRAW</div>
            </a><a id="ownerLogo" href="#" title="Searce Home" target="_blank" style="">
                <img src="../Images/searce_logo.png" alt="Searce" /></a>
            <h1>
                Bug Tracker</h1>
            <div id="divPrimarySearchForm">
                <label for="primarySearch">
                    Search Tickets</label>
                <input type="text" value="" title="Search" name="q" id="primarySearch" />
                <button type="submit" name="go" id="jq-searchGoButton">
                    <span>Go</span></button>
                <input type="hidden" name="ticket" value="on" />
            </div>
        </div>
        <div id="content" class="clearfix" style="height: 504px;">
            <div id="navigation" style="overflow-y: no-display; height: 485px;">
                <h5>
                    Bug Tracker</h5>
                <div id="divBugTracker">
                    <a href="#">New Ticket</a> <a href="#">View Tickets</a> <a href="#">Roadmap</a>
                    <a href="#">Recent Changes</a> <a href="#">Summary</a> <a href="#">Profile</a>
                </div>
                <h5>
                    Tracker Account</h5>
                <div id="divTrackerAccount">
                    <a href="#">Login</a> <a href="#">Register</a> <a href="#">Preferences</a>
                </div>
            </div>
            <div id="primaryContent" style="height: 485px;">
                <div class="post">
                    <h2>
                        <span id="lblBlogTitle">Create Ticket
                            <br />
                        </span>
                    </h2>
                    <!--<small id="lblBlogPostedInfo">
                        <br />
                    </small>-->
                    <div class="entryText">
                        <hr />
                        <div id="loading_notifier">
                            <table class="loading_notifier_box">
                                <tbody>
                                    <tr>
                                        <td style="font-size: 11px; font-weight: 700;">
                                            <span id="lblProgressText">Loading...</span>
                                            <div class="processing_image">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="pageParking">
                            <fieldset id="properties">
                                <legend>Properties </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <th>
                                            <label for="selModules">
                                                Module:</label>
                                        </th>
                                        <td colspan="3">
                                            <select id="selModules" name="selModules">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <label for="txtSummary">
                                                Summary:</label>
                                        </th>
                                        <td colspan="3">
                                            <input type="text" id="txtSummary" name="txtSummary" style="width: 510px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <label for="txtDescription">
                                                Description:</label>
                                        </th>
                                        <td colspan="3">
                                            <fieldset class="iefix">
                                                <div class="trac-resizable">
                                                    <div>
                                                        <textarea id="txtDescription" name="txtDescription" class="trac-resizable" rows="10"
                                                            cols="68"></textarea>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div style="border-top: 1px dotted" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <label for="field-type">
                                                Type:</label>
                                        </th>
                                        <td>
                                            <select id="field-type" name="field_type">
                                                <option selected="selected" value="bug">bug</option>
                                                <option value="feature">feature</option>
                                                <option value="enhancement">enhancement</option>
                                            </select>
                                        </td>
                                        <th>
                                            <label for="fileAttachment_1">
                                                Attachment 1:</label>
                                        </th>
                                        <td>
                                            <input type="file" id="fileAttachment_1" name="fileAttachment_1" style="width: 250px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <label for="selPriority">
                                                Priority:</label>
                                        </th>
                                        <td>
                                            <select id="selPriority" name="selPriority">
                                                <option>Low</option>
                                                <option>Medium</option>
                                                <option>High</option>
                                            </select>
                                        </td>
                                        <th>
                                            <label for="fileAttachment_1">
                                                Attachment 2:</label>
                                        </th>
                                        <td>
                                            <input type="file" id="file1" name="fileAttachment_1" style="width: 250px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div style="border-top: 1px dotted" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                        </th>
                                        <td>
                                            <a id="btnSubmit" href="#" >Submit Ticket</a>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <hr />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
<script src="Scripts/GetAsync.min.js" type="text/javascript"></script>
<script src="Scripts/Issues.min.js" type="text/javascript"></script>
<script src="Scripts/Resizer.js" type="text/javascript"></script>
</html>
