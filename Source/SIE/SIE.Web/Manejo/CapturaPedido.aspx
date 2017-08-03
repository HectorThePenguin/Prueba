<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturaPedido.aspx.cs"  Inherits="SIE.Web.Manejo.CapturaPedido" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/CapturaPedido.js"></script>
    <link rel="stylesheet" href="../assets/css/CapturaPedido.css" />
    <link rel="shortcut icon" href="../favicon.ico" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.css" rel="stylesheet"/>
    <script type="text/javascript">
        javascript: window.history.forward(1);
        var lblFolio = '<asp:Literal runat="server" Text="<%$ Resources:lblFolio.Text %>"/>';
        var lblProducto = '<asp:Literal runat="server" Text="<%$ Resources:lblProducto.Text %>"/>';
        var lblAlmacen = '<asp:Literal runat="server" Text="<%$ Resources:lblAlmacen.Text %>"/>';
        var lblLote = '<asp:Literal runat="server" Text="<%$ Resources:lblLote.Text %>"/>';
        var lblCostoUnitario = '<asp:Literal runat="server" Text="<%$ Resources:lblCostoUnitario.Text %>"/>';
        var lblPrecioVenta = '<asp:Literal runat="server" Text="<%$ Resources:lblPrecioVenta.Text %>"/>';
        var lblLoteUtilizar = '<asp:Literal runat="server" Text="<%$ Resources:lblLoteUtilizar.Text %>"/>';
        var lblCantidadAjuste = '<asp:Literal runat="server" Text="<%$ Resources:lblCantidadAjuste.Text %>"/>';
        var lblPorcentajeAjuste = '<asp:Literal runat="server" Text="<%$ Resources:lblPorcentajeAjuste.Text %>"/>';
        var lblJustificacion = '<asp:Literal runat="server" Text="<%$ Resources:lblJustificacion.Text %>"/>'
        var lblAutRech = '<asp:Literal runat="server" Text="<%$ Resources:lblAutRech.Text %>"/>';
        var lblObservaciones = '<asp:Literal runat="server" Text="<%$ Resources:lblObservaciones.Text %>"/>';
        var msgSeleccionarMovimiento = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarMovimiento.Text %>"/>';
        var msgSeleccionarOrganizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSeleccionarOrganizacion.Text %>"/>';
        var msgSinTipoAutorizacion = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTipoAutorizacion.Text %>"/>';
        var msgSinTipoGanadera = '<asp:Literal runat="server" Text="<%$ Resources:msgSinTipoGanadera.Text %>"/>';
        var msgSinMovimientosPendientes = '<asp:Literal runat="server" Text="<%$ Resources:msgSinMovimientosPendientes.Text %>"/>';
        var msgSinMovimientosPendientesSeleccionado = '<asp:Literal runat="server" Text="<%$ Resources:msgSinMovimientosPendientesSeleccionado.Text %>"/>';
        var msgSinOrganizacionValida = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizacionValida.Text %>"/>';
        var msgErrorMovimientos = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorMovimientos.Text %>"/>';
        var msgErrorPrecondiciones = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorPrecondiciones.Text %>"/>';
        var msgSinCambioEstatus = '<asp:Literal runat="server" Text="<%$ Resources:msgSinCambioEstatus.Text %>"/>';
        var msgSinObservacionesCapturadas = '<asp:Literal runat="server" Text="<%$ Resources:msgSinObservacionesCapturadas.Text %>"/>';
        var msgGuardadoConExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoConExito.Text %>"/>';
        var msgGuardadoSinExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardadoSinExito.Text %>"/>';
        var msgDialogoSi = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoSi.Text %>"/>';
        var msgDialogoNo = '<asp:Literal runat="server" Text="<%$ Resources:msgDialogoNo.Text %>"/>';
        var msgCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgCancelar.Text %>"/>';
        var msgSinOrganizaciones = '<asp:Literal runat="server" Text="<%$ Resources:msgSinOrganizaciones.Text %>"/>';

        var msgOrganizacionVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgOrganizacionVacio.Text %>"/>';
        var msgPromedioCabezasVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgPromedioCabezasVacio.Text %>"/>';
        var msgJaulasLunesVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasLunesVacio.Text %>"/>';
        var msgJaulasMartesVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasMartesVacio.Text %>"/>';
        var msgJaulasMiercolesVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasMiercolesVacio.Text %>"/>';
        var msgJaulasJuevesVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasJuevesVacio.Text %>"/>';
        var msgJaulasViernesVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasViernesVacio.Text %>"/>';
        var msgJaulasSabadoVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasSabadoVacio.Text %>"/>';
        var msgJaulasDomingoVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgJaulasDomingoVacio.Text %>"/>';
        var msgJaulasMotivoCambioVacio = '<asp:Literal runat="server" Text="<%$ Resources:msgMotivoCambioVacio.Text %>"/>';

        var msgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorGuardar.Text %>"/>';
        var msgErrorConsultar = '<asp:Literal runat="server" Text="<%$ Resources:msgErrorConsultar.Text %>"/>';
        var msgGuardarExito = '<asp:Literal runat="server" Text="<%$ Resources:msgGuardarExito.Text %>"/>';
        var msgHaySolicitudes = '<asp:Literal runat="server" Text="<%$ Resources:msgHaySolicitudes.Text %>"/>';

        var clvAutorizacion = '<asp:Literal runat="server" Text="<%$ Resources:clvAutorizacion.Text %>"/>';
        var clvRechazar = '<asp:Literal runat="server" Text="<%$ Resources:clvRechazar.Text %>"/>';
        var clvSolicitud = '<asp:Literal runat="server" Text="<%$ Resources:clvSolicitud.Text %>"/>';
        var clvCorreoJefeManejo = '<asp:Literal runat="server" Text="<%$ Resources:clvCorreoJefeManejo.Text %>"/>';
        var clvCorreoLogistica = '<asp:Literal runat="server" Text="<%$ Resources:clvCorreoLogistica.Text %>"/>';
        var clvRegistro = '<asp:Literal runat="server" Text="<%$ Resources:clvRegistro.Text %>"/>';
        var clvPedido = '<asp:Literal runat="server" Text="<%$ Resources:clvPedido.Text %>"/>';
        var clvCambio = '<asp:Literal runat="server" Text="<%$ Resources:clvCambio.Text %>"/>';

        
    </script>
    <style>
        .oculto {
            display: none !important;
        }
    </style>
</head>
<body class="page-header-fixed">
    <div id="skm_LockPane" class="LockOff">
        Enviando Correo, por favor espere...
            </div>
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
        <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloCapturaPedido"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="portlet-body form">
						    <ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
				                    <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="AutorizacionMovimientos_Home"/></a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="CapturaPedido.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="CapturaPedido_Title"></asp:Label></a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <div class="span12">
                                    <div id="contenedoInputs" class="row-fluid span6">
                                        <div class="span12">
                                            <!-- Organizacion -->
                                            <div  class="span11">
                                                <div style="margin-left: 10px;">
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblOrganizacion" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label> 
                                                </div>
                                                <div class="span1 " >
                                                    <asp:TextBox ID="txtNumOrganizacion" CssClass="span12 validarVacio" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="span10">
                                                    <asp:TextBox ID="txtOrganizacion" CssClass="span11" runat="server"></asp:TextBox>
                                                    <a id="btnAyudaOrganizacion" href="#dlgBusquedaOrganizacion" data-toggle="modal"><img src="../Images/find.png" width="26" height="26"/></a>
                                                </div>
                                            </div>
                                            <div class="span7" >
                                                <div class="space10"></div>
                                                <div class="separador">
                                                    <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                                </div> 
                                                <asp:TextBox ID="txtFecha" runat="server" Enabled="false" MaxLength="10"/>
                                            </div>
                                            <div class="span7" >
                                                <div class="space10"></div>
                                                <div>
                                                    <asp:Label ID="lblPromedioCabezas" runat="server" meta:resourcekey="lblPromedioCabezas"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtPromedioCabezas" mensajeVacio="msgPromedioCabezasVacio" CssClass="validarVacio numerico" runat="server" oninput="CalcularPromedioCabezas()" MaxLength="3"/>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="space20"></div>
                                <!-- GRID DE SEMANAS -->
                                <div id="Div1" class="span11">
                                    <div class="span1">
                                        <img id="btnSemanaAnterior" class="EstiloFlecha cursorPointer" src="../Images/flecha_izquierda.png"/>
                                        <asp:Label Text="text" CssClass="spanLblGrid" runat="server" >Jaula</asp:Label>
                                        <asp:Label Text="text" CssClass="spanLblGrid spanLblGridCabezas" runat="server" >Cabezas</asp:Label>
                                    </div>
                                        <table id="GridSemanal"class="table table-hover table-striped table-bordered span10 tablacapturaEstilo">
                                                <thead>
                                                            <tr>
                                                                <th id="anioEtiqueta" scope="col" class="alineacionCentro" colspan="8"></th>   
                                                            </tr>
                                                            <tr>
                                                                <th id="lunesEtiqueta" scope="col" class="alineacionCentro span12">-</th>
                                                                <th id="martesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="miercolesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="juevesEtiqueta" scope="col" class="alineacionCentro">-</th>        
                                                                <th id="viernesEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="sabadoEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th id="domingoEtiqueta" class="alineacionCentro" scope="col">-</th>        
                                                                <th class="alineacionCentro" scope="col"></th>        
                                                            </tr>
                                                            <tr>
                                                                <th scope="col" class="alineacionCentro lunesEtiqueta" style="background-color:#B94A48; color:white;">LUNES</th>        
                                                                <th class="alineacionCentro martesEtiqueta" scope="col" style="background-color:#B94A48; color:white;">MARTES</th>        
                                                                <th class="alineacionCentro miercolesEtiqueta" scope="col" style="background-color:#B94A48; color:white;">MIÉRCOLES</th>        
                                                                <th scope="col" class="alineacionCentro juevesEtiqueta" style="background-color:#B94A48; color:white;">JUEVES</th>        
                                                                <th class="alineacionCentro viernesEtiqueta" scope="col" style="background-color:#B94A48; color:white;">VIERNES</th>        
                                                                <th class="alineacionCentro sabadoEtiqueta" scope="col" style="background-color:#B94A48; color:white;">SÁBADO</th>        
                                                                <th class="alineacionCentro domingoEtiqueta" scope="col" style="background-color:#B94A48; color:white;">DOMINGO</th>        
                                                                <th class="alineacionCentro" scope="col" >TOTAL</th>        
                                                            </tr>
                                                </thead>    
                                            <tbody style ="overflow-y:scroll">
                                                    <tr>
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasLunes" oninput="CalcularCabezas(this, 'txtCabezasLunes')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasMartes" oninput="CalcularCabezas(this, 'txtCabezasMartes')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasMiercoles" oninput="CalcularCabezas(this, 'txtCabezasMiercoles')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasJueves" oninput="CalcularCabezas(this, 'txtCabezasJueves')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasViernes" oninput="CalcularCabezas(this, 'txtCabezasViernes')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasSabado" oninput="CalcularCabezas(this, 'txtCabezasSabado')" maxlength="3"/></td> 
                                                        <td class="span9 habilitarInput"><input class="span12 inputGrid validarVacio numerico" type="text" id="txtJaulasDomingo" oninput="CalcularCabezas(this, 'txtCabezasDomingo')" maxlength="3"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtJaulasTotal"/></td>            
                                                    </tr>
                                                    <tr>
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasLunes" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasMartes" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasMiercoles" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasJueves" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasViernes" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasSabado" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasDomingo" disabled="disabled"/></td> 
                                                        <td class="span9 celdaDeshabilitada"><input class="span12 inputGrid" type="text" id="txtCabezasTotal" disabled="disabled"/></td>            
                                                    </tr>
                                            
                                            </tbody>
                                        </table>
                                    <img id="btnSemanaSiguiente" class="EstiloFlecha cursorPointer" src="../Images/flecha_derecha.svg" style="width: 50px;height: 50px;"/>
                                    </div>
                                <!--GRID DE SEMANAS -->
                                <div class="space20"></div>
                                <!-- BOTONES -->
                                <div class="row-fluid span12">
                                        <div class="textoDerecha span11">
                                            <button id="lblActualizar" type="button" class="btn SuKarne">
                                                <asp:Label runat="server"  meta:resourcekey="lblActualizar"></asp:Label>
                                            </button>
                                            <button id="lblGuardarSemana" type="button" class="btn SuKarne">
                                                <asp:Label runat="server" meta:resourcekey="lblGuardar"></asp:Label>
                                            </button>

                                            <button id="btnLimpiar" type="button" class="btn SuKarne">
                                                <asp:Label runat="server"  meta:resourcekey="lblCancelar"></asp:Label>
                                            </button>
                                        </div>
                                </div>
                                <!-- BOTONES -->
                                <div class="space20"></div>

                                <div id="contenedorComentarios" class="span6">
                                <fieldset id="dlgAutorizacionMovimientos"  class="scheduler-border span10" >
                                    <legend class="scheduler-border">
                                        Comentarios
                                    </legend>

                                    <!--GRID DE COMENTARIOS -->
                                    <div id="GridComentarios" class="span12">
                                        <div id="tHeadContainer">
                                        <table id="tbSolicitudes" class=" table-hover table-striped table-bordered" >
                                            <tr>
                                                <td  class="alineacionCentro borderTabla cabecerosTabla" >FECHA CAMBIO</td>        
                                                <td class="alineacionCentro cabecerosTabla" >JAULAS CAMBIO</td>        
                                            </tr> 
                                            
                                        </table>
                                            </div>
                                        <div id="tBodyContainer">    
                                            <table id="tBody" class=" table-hover table-striped table-bordered" >
                                            </table>
                                            </div>
                                        </div>
                                    <div class="span12">
                                        <label class="span11">Comentarios:</label>
                                    </div>
                                    <div class="span12">
                                        <textarea id="txtComentario"class="span11" disabled="true"></textarea>
                                    </div>
                                    <div style="text-align: center">
                                        <button id="btnAutorizar" type="button" style="margin-right: 10px;margin-top: 5px;" class="btn SuKarne">
                                            <asp:Label ID="Label7" runat="server" meta:resourcekey="lblAutorizar"></asp:Label>
                                        </button>
                                        <button id="btnCancelarSolicitud" type="button" style="margin-top: 5px;" class="btn SuKarne">
                                            <asp:Label ID="Label6" runat="server"  meta:resourcekey="lblRechazar"></asp:Label>
                                        </button>
                                    </div>
                                    <!-- GRID DE COMENTARIOS -->
                                </fieldset>
                                        </div>
                            </div>
                            <!-- Grid de Movimientos con solicitudes pendientes -->
                            <div id="GridSolicitudes" class="portlet-body form" style="display: none; height: 300px; margin-right: 0px;">
                                <div class="alineacionCentro">
                                    <table id="GridEncabezado" class="table table-striped table-advance table-hover">
                                        <thead></thead>
                                    </table>
                                    <div id="dvContenido" style="height: 300px; overflow: auto;">
                                        <table id="GridContenido" class="table table-striped table-advance table-hover no-left-margin">
                                            <tbody></tbody>
                                        </table>
                                    </div>
					            </div>
                            </div>
                            <div id="BotonesPie" class="row-fluid" style="display: none;">
                                <div class="textoDerecha">
                                    <button id="btnGuardar" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblGuardar" runat="server" meta:resourcekey="lblGuardar"></asp:Label>
                                    </button>

                                    <button id="btnCancelar" type="button" class="btn SuKarne">
                                        <asp:Label ID="lblCancelar" runat="server"  meta:resourcekey="lblCancelar"></asp:Label>
                                    </button>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Dialogo de Cancelacion Buscar -->
                        <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
			                <div class="modal-body">
				                <asp:Label ID="Label3" runat="server" meta:resourcekey="msgCancelarBuscar"></asp:Label>
			                </div>
			                <div class="modal-footer">
				                <asp:Button runat="server" ID="btnSiBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnNoBuscar" CssClass="btn SuKarne" meta:resourcekey="msgDialogoNo" data-dismiss="modal"/>
			                </div>
		                </div>
                    </div>
                </div>  
            </div>
            
            
              <!-- MOTIVO CAMBIO -->
        <div id="dlgMotivoCmabio" class="modal hide fade" style=" margin-top: -150px;height: 200px; width:390px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
            <div class="span4">
                <div class="span3 alineacionCentro"><asp:Label ID="LblMotivoCambio" runat="server" meta:resourcekey="LblMotivoCambio"></asp:Label></div>
                <div class="span3 alineacionCentro"><textarea type="text" name="txtMotivoCambio" id="txtMotivoCambio"value="" style="height:100px" maxlength="255"></textarea></div>
                <div class="span3 alineacionCentro" style="margin-top:20px">
                    <button id="lblAceptarMotivo" type="button" class="btn SuKarne alineacionCentro">
                        <asp:Label ID="Label8" runat="server"  meta:resourcekey="lblAceptar"></asp:Label>
                    </button>  
                    <button id="lblCancelarMotivo" type="button" class="btn SuKarne alineacionCentro">
                        <asp:Label runat="server"  meta:resourcekey="lblCancelar"></asp:Label>
                    </button>    
                </div>
            </div>
		</div>
            <!-- Dialogo de Cancelacion Buscar -->
        <div id="dlgCancelarMotivo" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="modal-body">
				<asp:Label ID="Label1" runat="server" meta:resourcekey="msgCancelarMotivo"></asp:Label>
			</div>
			<div class="modal-footer">
				<asp:Button runat="server" ID="btnSi" CssClass="btn SuKarne" meta:resourcekey="msgDialogoSi" data-dismiss="modal"/>
                <asp:Button runat="server" ID="btnNo" CssClass="btn SuKarne" meta:resourcekey="msgDialogoNo" data-dismiss="modal"/>
			</div>
		</div>
        <!-- MOTIVO CAMBIO -->

        </form>
  

        <!-- Dialogo de Busqueda Organización -->
        <div id="dlgBusquedaOrganizacion" class="modal hide fade" style="margin-top: -150px;height: 400px; width:650px;" tabindex="-1" data-backdrop="static" data-keyboard="false">
			<div class="portlet box SuKarne2">
				<div class="portlet-title">
                    <div class="caption">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span class="letra">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="BusquedaGanadera_Title"></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form" style="height: 400px; margin-right: 0px;">
                    <div class="modal-body">
						<fieldset class="scheduler-border">
                            <legend class="scheduler-border"><asp:Label ID="lblFiltro" runat="server" meta:resourcekey="lblFiltro"></asp:Label></legend>
                            <div class=" no-left-margin" style="width:575px">
                                <asp:Label ID="lblOrganizacionBusqueda" runat="server" meta:resourcekey="lblOrganizacion"></asp:Label>
                                <input type="text" id="txtOrganizacionBuscar" style="width: 230px;"/>
                                <a id="btnAyudaBuscarOrganizacion" class="btn SuKarne" style="margin-left: 10px;" meta:resourcekey="btnBuscar">Buscar</a>
                                <a id="btnAyudaAgregarBuscar" class="btn SuKarne" meta:resourcekey="btnAgregar">Agregar</a>
                                <a id="btnAyudaCancelarBuscar" class="btn SuKarne" meta:resourcekey="btnCancelar">Cancelar</a>
                            </div>
                        </fieldset>
                        <div class="alineacionCentro">
                            <table id="tbBusquedaEncabezado" class="table table-striped table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 20px;" class=" alineacionCentro" scope="col"></th>
                                        <th style="width: 100px;" class=" alineacionCentro" scope="col"><asp:Label ID="Label4" runat="server" meta:resourcekey="lblAyudaGridIdentificador"></asp:Label></th>
                                        <th style="width: auto;" class=" alineacionCentro" scope="col"><asp:Label ID="Label5" runat="server" meta:resourcekey="lblAyudaGridGanadera"></asp:Label></th>
                                    </tr>
                                </thead>
                            </table>
                            <div class="space5"></div>
                            <div id="dvBusqueda" style="height: 200px; overflow: auto;">
                                <table id="tbBusqueda" class="table table-striped table-advance table-hover no-left-margin">
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
					</div>
                </div>
            </div>
		</div>
    </div>
</body>
</html>