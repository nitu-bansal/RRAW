<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Blog.aspx.vb" Inherits="RRAW.Blog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><![if !IE]>
    <link href="CSS/Global.min.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <!--[if IE]>
    <link href="CSS/Global_IE.min.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <link href="CSS/Blog.min.css" rel="stylesheet" type="text/css" />
</head>
<body id="interior" class="enhanced" onload="LoadContents();">
    <form runat="server" id="entriesForm">
    <div id="siteContain">
        <div id="header">
            <a id="siteLogo" title="Click here to navigate to RRAW Portal Main Page" href="Master.aspx">
                <div id="title">
                    Rate Request and Approval Workflow - RRAW</div>
            </a>
            <h1>
                Release Blog</h1>
        </div>
        <div id="content" class="clearfix">
            <div id="navigation" style="overflow-y:no-display">
                <h5>
                    Blog Entries</h5>
                <div id="blogEntries">
                </div>
            </div>
            <div id="primaryContent">
                <div class="post">
                    <h2>
                        <span id="lblBlogTitle">
                            <br />
                        </span>
                    </h2>
                    <small id="lblBlogPostedInfo">
                        <br />
                    </small>
                    <div class="entryText">
                        <hr />
                        <div id="loading_notifier">
                            <table class="loading_notifier_box">
                                <tbody>
                                    <tr>
                                        <td style="font-size: 11px; font-weight: 700;">
                                            <span id="lblProgressText">Loading Blog...</span>
                                            <div class="processing_image">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="blogParking">
                        </div>
                        <hr />
                    </div>
                </div>
                <div id="footer" runat="server" visible="false">
                    <h3>
                        <span id="lblBlogResponseCount"></span>&nbsp;Responses to&nbsp;"<span id="lblBlogTitle2"></span>"</h3>
                    <asp:Repeater ID="rptrCommentList" runat="server">
                    </asp:Repeater>
                    <h3>
                        Leave a Reply</h3>
                    <div id="replyContent">
                        <p>
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:LinkButton ID="btnSubmitComment" runat="server" CssClass="button">Submit Comment</asp:LinkButton>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
<script src="Scripts/GetAsync.min.js" type="text/javascript"></script>
<script src="Scripts/Blog.min.js" type="text/javascript"></script>
</html>
