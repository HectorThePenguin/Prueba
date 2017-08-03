<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PantallaImpresion.aspx.cs" Inherits="SIE.Web.Alimentacion.PantallaImpresion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../Scripts/json2.js"></script>
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/style-metro.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery-1.7.1.min.js"></script>
    <script src="../Scripts/ServicioAlimentoCorrales.js"></script>
    <link href="../assets/plugins/bootstrap/css/bootstrap-modal.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap-bootbox/js/bootbox.min.js"></script>
    <link href="../assets/css/media-queries.css" rel="stylesheet" />
    <link href="../assets/plugins/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.print();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" class="btn SuKarne pull-right" meta:resourcekey="btnImprimirResource1" OnClientClick="javascript:window.print();" Visible="False" />

            <asp:DataGrid ID="dgImpresion" runat="server" CellSpacing="0" Width="600px" AutoGenerateColumns="False">
                <AlternatingItemStyle CssClass="arowStyle"></AlternatingItemStyle>
                <ItemStyle CssClass="rowStyle"></ItemStyle>
                <HeaderStyle CssClass="headerStyle" />
                <Columns>
                    <asp:BoundColumn HeaderText="# De Corral" DataField="CodigoCorral" HeaderStyle-Width="50px"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Kg Prog" DataField="KilosProgramados" HeaderStyle-Width="50px"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Form" DataField="DescripcionFormula" HeaderStyle-Width="100px"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Comentarios" DataField="Comentarios" HeaderStyle-Width="200px"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            
        </div>
    </form>
</body>
</html>
