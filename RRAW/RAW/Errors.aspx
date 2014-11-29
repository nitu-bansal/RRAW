<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Errors.aspx.vb" Inherits="RRAW.Errors" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Verdana;
            font-size: 12px;
        }
    </style>
</head>
<body onload="CallAjaxLoad('<%=Request.QueryString("ExceptionType")%>','<%=Request.QueryString("Msg")%>','<%=Request.QueryString("StackTrace")%>','<%=Request.QueryString("TimedOut")%>')">
    <form id="form1" runat="server">
    <div>
        <!--<asp:Label ID="lblErrorMessage" runat="server" Text="Error" Font-Bold="True"></asp:Label>-->
        <span id="lblErrorMessage" style="font-weight: 700">Error</span>
    </div>
    <br />
    <div id="divStatus">
        <!--<asp:Label ID="lblStatus" runat="server" Text="An unknow error has been occured in system. Apologizing for inconvenience.<br /><br />Sending exception details to admin..."></asp:Label>-->
        <%--<span id="lblStatus">An unknow error has been occured in system. Apologizing for inconvenience.<br />
            <br />
        --%>
        <span style="color: black;"><span style="color: darkred; text-decoration: underline">
            Error:</span><%=Request.QueryString("Msg").ToString%><%--</span>--%>
            <br />
            <br />
            
            Sending exception details to admin...</span>
          <br />
            <br />
        <a id="linkNavigateBack" href="javascript:history.go(-1);" style="">Navigate Back</a>
    </div>
    <div>
        <!--<asp:HiddenField ID="hidExceptionType" runat="server" />
    <asp:HiddenField ID="hidStackTrace" runat="server" />
    <asp:HiddenField ID="hidTimedOut" runat="server" />-->
    </div>
    </form>
</body>
<script type="text/javascript">
    //document.getElementById('lblErrorMessage').innerHTML = GetQueryString("Msg");

    function CallAjaxLoad(extType,Msg,StackTrace,TimeOut) {
        //AjaxLoad("SendMailAsync.aspx?ExceptionType=" + document.getElementById('lblErrorMessage').innerHTML + "&=" + document.getElementById('hidExceptionType').innerHTML + "&StackTrace=" + document.getElementById('hidStackTrace').innerHTML + "&TimedOut=" + document.getElementById('hidTimedOut').innerHTML, "lblStatus");

        //AjaxLoad("SendMailAsync.aspx?ExceptionType=" + GetQueryString("ExceptionType") + "&errMsg=" + GetQueryString("Msg") + "&StackTrace=" + GetQueryString("StackTrace") + "&TimedOut=" + GetQueryString("TimedOut"), "lblStatus");
        //alert("SendMailAsync.aspx?ExceptionType=" + extType + "&errMsg=" + Msg + "&StackTrace=" + StackTrace + "&TimedOut=" + TimeOut);
        AjaxLoad("SendMailAsync.aspx?ExceptionType=" + extType + "&errMsg=" + Msg + "&StackTrace=" + StackTrace + "&TimedOut=" + TimeOut, "lblStatus");
    }

    function GetQueryString(key) {
        var re = new RegExp('(?:\\?|&)' + key + '=(.*?)(?=&|$)', 'gi');
        var r = [], m;
        //alert(document.location.href);
        while ((m = re.exec(document.location)) != null) r[r.length] = m[1];
        if (r.length > 0) {
            return r;
        }
        else {
            return undefined;
        }
    }

    /*$(document).ready(function () {
    $("#lblStatus").load("SendMailAsync.aspx", { "errMsg": $("#lblErrorMessage").html() }, function () { });
    });*/
</script>
<script src="Scripts/AjaxLoad.min.js" type="text/javascript"></script>
</html>
