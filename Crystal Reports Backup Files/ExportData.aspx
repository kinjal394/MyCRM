<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportData.aspx.cs" Inherits="CRM.ExportData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function HideLoadingImg()
        {
            window.parent.document.getElementById('LoadingMainImg').style.display = 'none';
        }
        function onLoad()
        {
            alert("ok");
        }
    </script>
</head>
<body onsubmit="onLoad();">
    <form id="form1" runat="server">
        <div>
            
        </div>
    </form>
</body>
</html>
