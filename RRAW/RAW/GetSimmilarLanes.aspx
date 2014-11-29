<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GetSimilarLanes.aspx.vb"
    Inherits="RRAW.GetSimilarLanes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="GetRateRequestHistory"
            SelectCommandType="StoredProcedure" EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="RateRequestID" QueryStringField="RateRequestID"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
