<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServicioAlimentoCorrales.aspx.cs" Inherits="SIE.Web.Alimentacion.ServicioAlimetoCorrales" meta:resourcekey="PageResource1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
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

    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <style type="text/css">
        </style>
   <link rel="shortcut icon" href="../favicon.ico" />
</head>
<body class="page-header-fixed">
    <div class="header navbar navbar-inverse navbar-fixed-top">
        <form id="frmServicio" runat="server">
            <div class="container-fluid" />
            <div class="row-fluid">
                <div class="portlet box SuKarne">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="span12">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
					                <a href="../Principal.aspx">Home</a> 
					                <i class="icon-angle-right"></i>    
				                </li>
                                <li>
					                <a href="ServicioAlimentoCorrales.aspx">Servicio Alimento</a> 
				                </li>
			                </ul>
                            <div class="span12">
                                <div class="span2">
                                    <span>
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblCodigoCorral" runat="server" meta:resourcekey="lblCodigoCorralResource1"></asp:Label>
                                    </span>
                                    <span>
                                        <asp:TextBox ID="txtCodigoCorral" runat="server" style="width: 98%;" meta:resourcekey="txtCodigoCorralResource1" maxlength="10"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="span2">
                                    <span>
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblKgsProgramados" runat="server" meta:resourcekey="lblKgsProgramadosResource1"></asp:Label>
                                    </span>
                                    <span>
                                        <input id="txtKgsProgramados" type="tel" class="text-right" onkeypress="mascara(this,cpf)" style="width: 98%;" disabled="disabled" oninput="maxLengthCheck(this)" maxlength="10" />
                                        <%--<asp:TextBox ID="txtKgsProgramados"  onkeypress="mascara(this,cpf)"  runat="server" style="width: 98%;" Enabled="False" meta:resourcekey="txtKgsProgramadosResource1" MaxLength="10"></asp:TextBox>--%>
                                        
                                           
                                    </span>
                                </div>
                                <div class="span2">
                                    <span>
                                        <span class="requerido">*</span>
                                        <asp:Label ID="lblFormula" runat="server" meta:resourcekey="lblFormulaResource1"></asp:Label>
                                    </span>
                                    <asp:DropDownList ID="ddlFormula" runat="server" Enabled="False" style="width:100%;" DataTextField="Descripcion" DataValueField="FormulaID" AppendDataBoundItems="True" meta:resourcekey="ddlFormulaResource1">
                                        <asp:ListItem Text="Seleccione" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                                    </asp:DropDownList>
                                </div>
                                <div class="span2">
                                    <span>
                                        <asp:Label ID="lblComentarios" runat="server" meta:resourcekey="lblComentariosResource1"></asp:Label>
                                    </span>
                                    <textarea id="txtComentarios" name="txtComentarios" maxlength="255" rows="3" class="span12" aria-setsize="none"></textarea>

                                </div>
                                <div class="span1">
                                    <asp:Button runat="server" ID="btnAgregar" class="btn SuKarne" meta:resourcekey="btnAgregarResource1" Width="84px"/>
                                    <asp:Button runat="server" ID="btnLimpiar" class="btn SuKarne" Text="Limpiar"  meta:resourcekey="btnLimpiarResource1" OnClientClick="return false" Width="84px"/>
                                </div>
                            </div>



                        </div>
                        <div class="row-fluid">
                            <div class="span12">
                                <div class="portlet">
                                    <div class="portlet-body">
                                        <div id="scroll" class="span12">
                                            <div id="Grid" class="span12"></div>
                                        </div>
                                        <div class="span12 pull-right">
                                            <table class="pull-right">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn SuKarne pull-right" meta:resourcekey="btnGuardarResource1" OnClientClick="return false"/>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" class="btn SuKarne pull-right" meta:resourcekey="btnImprimirResource1" OnClientClick="return false"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                         <div id="msgGuardadoModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
							<div class="modal-header">
								<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
								<h3 id="myModalLabel2">Éxito</h3>
							</div>
							<div class="modal-body">
								<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Correct.png" meta:resourcekey="imgLogoResource1" />
								<asp:Label ID="lblmsgGuardado" runat="server" meta:resourcekey="lblmsgGuardarBotonResource1"></asp:Label>
							</div>
							<div class="modal-footer">
								<button id="btnGuardado" data-dismiss="modal" class="btn SuKarne">Aceptar</button>
							</div>
						</div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnCorralId" runat="server" />
            <asp:HiddenField ID="hdnCorralIdAnt" runat="server" />
        </form>
    </div>
    <script type="text/javascript">
        var msgActualizar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Actualizar %>"/>';
        var msgAgregar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Agregar %>"/>';
        var msgConsultarCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.ConsultarCorral %>"/>';
        var msgAgregarKilos = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.AgregarKilos %>"/>';
        var msgAgregarKilosMayorCero = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.AgregarKilosMayorCero %>"/>';
        var msgSeleccionarFormula = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.SeleccionarFormula %>"/>';
        var msgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.ErrorGuardar %>"/>';
        var msgGuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Guardado %>"/>';
        var msgCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Corral %>"/>';
        var msgKilosProgramados = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.KilosProgramados %>"/>';
        var msgFormula = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Formula %>"/>';
        var msgComentarios = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Comentarios %>"/>';
        var msgFechaRegistro = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.FechaRegistro %>"/>';
        var msgOpcion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Opcion %>"/>';
        var msgCapturarCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CapturarCorral %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CorralNoExiste %>"/>';
        var msgLoteAsignado = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.LoteAsignado %>"/>';
        var msgLoteAsignadoActualizado = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.LoteAsignadoActualizado %>"/>';
        var msgCorralAsignado = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CorralAsignado %>"/>';
        var msgNoEditar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoEditar %>"/>';
        var msgTipoCorralDiferente = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgTipoCorralDiferente %>"/>';
        var msgPalabrasInvalidas = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgPalabrasInvalidas %>"/>';
    </script>
    
    <script language="javascript">
        function mascara(o, f) {
            v_obj = o;
            v_fun = f;
            setTimeout("execmascara()", 1);
        }
        function execmascara() {
            v_obj.value = v_fun(v_obj.value);
        }
        function cpf(v) {
            v = v.replace(/([^0-9\.]+)/g, '');
            v = v.replace(/^[\.]/, '');
            v = v.replace(/[\.][\.]/g, '');
            v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
            v = v.replace(/\.(\d{1,2})\./g, '.$1');
            v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
            v = v.split('').reverse().join('').replace(/^[\,]/, '');
            return v;
        }
        </script>

	  
</body>
<script src="../assets/plugins/jquery-ui-1.10.1.custom.min.js"></script>
<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
<script src="../assets/scripts/jquery-jtemplates.js"></script>
<script src="../assets/plugins/data-tables/jquery.dataTables.js"></script>
<!-- <script src="../Plugins/data-tables/DT_bootstrap.js"></script> -->
<script src="../assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
<script src="../assets/scripts/ui-modals.js"></script>
<script src="../assets/scripts/app.js"></script>
</html>
