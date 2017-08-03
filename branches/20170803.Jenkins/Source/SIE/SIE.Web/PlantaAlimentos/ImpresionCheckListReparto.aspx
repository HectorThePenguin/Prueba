<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpresionCheckListReparto.aspx.cs" Inherits="SIE.Web.PlantaAlimentos.ImpresionCheckListReparto" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="../assets/plugins/jquery-1.7.1.min.js"></script>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
</head>
     <script type="text/javascript">
         $(document).ready(function () {
             location.assign('CheckListReparto.aspx');
         });
    </script>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="crv" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
