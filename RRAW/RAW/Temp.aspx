<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Temp.aspx.vb" Inherits="RRAW.Temp"
    EnableViewState="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <link href="FileUploader/fileuploader.css" rel="stylesheet" type="text/css" />
    <script src="FileUploader/fileuploader.js" type="text/javascript"></script>
    <script type="text/javascript">
        //File Upload supporters
        var filesInProgress = 0;
        var buttonPressed = false;
        var ieCheckDone = false;
        var processing = false;
        var callbackObject;

        $(document).ready(function () {
            //File Upload Handler
            var uploader = new qq.FileUploader({
                // pass the dom node (ex. $(selector)[0] for jQuery users)
                element: $('#f')[0],
                // path to server-side upload script
                action: 'FileUploader/UploadFile.aspx',
                debug: true,

                //For File Upload in FF & Chrome
                onSubmit: function (id, fileName) {
                    filesInProgress++;
                },
                onComplete: function (id, fileName, responseJSON) {
                    filesInProgress--;
                    if (filesInProgress <= 0 && buttonPressed == true) {
                        $(callbackObject).click();
                    }
                }
            });

            $("#Button1").click(function () {
                if ($.browser.msie && ieCheckDone == false) {
                    callbackObject = this;
                    CheckFileUploadProgress();
                }
                if (filesInProgress > 0) {
                    buttonPressed = true;
                    callbackObject = this;
                }
                else {
                    if (processing == false) {
                        processing = true;
                        alert("Calling Web Service to store data...");
                    }
                }
            });
        });

        //For File Upload in IE
        function CheckFileUploadProgress() {
            filesInProgress = 0;
            $.each($("[id*='qq-upload-handler-iframe']"), function (key, val) {
                filesInProgress++;
                if (($(val).contents().children().length > 0)) {
                    if (($(val).contents().children(0).html().indexOf("{success:true}")) >= 0) {
                        filesInProgress--;
                    }
                }
            });

            if (filesInProgress <= 0) {
                ieCheckDone = true;
                $(callbackObject).click();
            }
            else {
                var t = setTimeout("CheckFileUploadProgress()", 2000);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="f">
    </div>
    <input id="Button1" type="button" value="button" />
    </form>
</body>
</html>
