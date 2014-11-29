Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Web.Script.Serialization

Namespace WebServices
    <WebService([Namespace]:="http://tempuri.org/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <System.ComponentModel.ToolboxItem(False)> _
    <System.Web.Script.Services.ScriptService()> _
    Public Class Select2
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Sub GetJson(Data As String)
            'List<string> lst = new List<string>();
            Dim str As String = "[{""id"":""1"",""Status"":""Working"",""Ok"":""NotOk""}," & "{""id"":""2"",""Status"":""NotWorking"",""Ok"":""DoOk""}," & "{""id"":""3"",""Status"":""OkWorking"",""Ok"":""WhyOk""}]"
            'string str = "{\"Status\"=\"Working\"}";
            Context.Response.Clear()
            Context.Response.ContentType = "application/json"
            Context.Response.Flush()
            Context.Response.Write(str)
            'return "{\"Status\":\"Working\"}";
            'return lst;
        End Sub
        

        Public Function Serialize(data As [Object]) As [String]
            Dim serializer As New JavaScriptSerializer()
            Dim str As String = serializer.Serialize(data)
            Return str
        End Function

        <WebMethod()> _
       <Script.Services.ScriptMethod(responseFormat:=Script.Services.ResponseFormat.Json)> _
        Public Sub GetTempjson(Data As String)
            Dim res As List(Of Dictionary(Of String, String)) '= GetCachedDictionaryItems(cacheItemName)

            res = DataTableToListDictionary(DB.Tariffs_New.GetSearchstringColumns(Data))
            Dim str As String = Serialize(res)
            Context.Response.Clear()
            Context.Response.ContentType = "application/json"

            'added
            Context.Response.ContentEncoding = Encoding.UTF8
            Context.Response.Write(str)
            Context.Response.Flush()

            'added
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            'return "{\"Status\":\"Working\"}";
            'return lst;


        End Sub


    End Class
End Namespace
