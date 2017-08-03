<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpresionCheckList.aspx.cs" Inherits="SIE.Web.Manejo.ImpresionCheckList" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <script src="../assets/plugins/jquery-1.7.1.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            location.assign('CierreCorral.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    </form>
</body>
</html>