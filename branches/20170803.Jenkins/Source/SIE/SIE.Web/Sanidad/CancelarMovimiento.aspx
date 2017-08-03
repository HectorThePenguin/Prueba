<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelarMovimiento.aspx.cs" Inherits="SIE.Web.Sanidad.CancelarMovimiento" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Controles/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    
     <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/CancelarMovimiento.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div id="skm_LockPane" class="LockOff">
            </div>
            <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <uc1:Titulo runat="server" id="Titulo" meta:resourcekey="CancelarMovimiento_Title" />
                        <div class="portlet-body form">
                            <div class="row-fluid">
                                <div class="span12">
							        <ul class="breadcrumb">
				                        <li>
					                        <i class="icon-home"></i>
				                            <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="CancelarMovimiento_Home"/></a> 
					                        <i class="icon-angle-right"></i>
				                        </li>
                                        <li>
					                        <a href="CancelarMovimiento.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="CancelarMovimiento_Title"></asp:Label></a> 
				                        </li>
			                        </ul>
                                </div>   
                            </div>   
                            <div class="row-fluid">
                                <div class="span12" style="text-align: right">
                                    <div class="span10">
                                        <span>
                                            <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblFechaSistema" runat="server"></asp:Label>
                                        </span>
                                    </div>        
                                    <div class="span2">
                                        <span>
                                            <asp:Label ID="lblHora" runat="server" meta:resourcekey="lblHora"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblHoraSistema" runat="server"></asp:Label>
                                        </span>
                                    </div>        
                                </div>        
                            </div>    

                            <div class="row-fluid">
                                <div class="span12">
                                    <div class="portlet">
                                        <div class="portlet-body">
                                            <div id="scroll" class="span12">
                                                <div id="TablaMovimientoCancelar"></div>
                                            </div> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                
                        </div>
                    </div>
                
                </div>
                <br />
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
					<button id="btnGuardado" data-dismiss="modal" class="btn SuKarne">OK</button>
				</div>
			</div>
            <div id="dlgCancelarMovimiento" class="modal hide fade" tabindex="-1" role="dialog" 
                 style="margin-top: -150px; display: block; width: 700px" 
                 aria-labelledby="myModalLabel2" 
                 aria-hidden="true">
				<div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        
                        <div class="caption">
                            <asp:Label ID="lblTituloCancelar" runat="server" meta:resourcekey="CancelarMovimiento_Title"></asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <table>
                        <tr>
                            <td class="tabs-right top pull-left">
                                <div class="span7">
                                    <div class="span2">
                                        <span>
                                            <asp:Label ID="lblCodigoCorral" runat="server" meta:resourcekey="lblCodigoCorralDialogo"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblCodigoCorralSeleccionado" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                    <div class="span4">
                                        <span>
                                            <asp:Label ID="lblDetector" runat="server" meta:resourcekey="lblDetector"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblDetectorSeleccionado" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="span7">
                                    <div class="span2">
                                        <span>
                                            <asp:Label ID="lblNoArete" runat="server" meta:resourcekey="lblNoAreteDialogo"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblNoAreteSelecionado" runat="server" ></asp:Label>
                                        </span>
                                    </div>
                                    
                                    <div class="span4">
                                        <span>
                                            <asp:Label ID="lblRecolector" runat="server" meta:resourcekey="lblRecolector"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblRecolectorSeleccionado" runat="server" ></asp:Label>
                                        </span>
                                    </div>
                               </div>
                                <div class="span7">
                                    <div class="span4">
                                        <span>
                                            <asp:Label ID="lblNoAreteMetalico" runat="server" meta:resourcekey="lblNoAreteMetalicoDialogo"></asp:Label>
                                        </span>
                                        <span>
                                            <asp:Label ID="lblNoAreteMetalicoSeleccionado" runat="server" ></asp:Label>
                                        </span>
                                    </div>
                               </div>

                               <div class="span7">
                                   <div class="span3">
                                       <span class="requerido">*</span>
                                     <asp:Label ID="lblMotivoCancelacion" runat="server" meta:resourcekey="lblMotivoCancelacion"></asp:Label>
                                   </div>
                               </div>

                                <div class="span7" >
                                   <div class="span6">
                                        <textarea id="txtComentarios" 
                                            name="txtComentarios" 
                                            maxlength="255" 
                                            rows="3" 
                                            class="span6" 
                                            aria-setsize="none" 
                                            style="resize: none">
                                        </textarea>
                                   </div>
                               </div> 
                            </td>
                        </tr>
                    </table>
                    </div>

                    <div class="modal-footer"> 
                        <div class="span8" align="left">
                            <div class="span4">
                                <asp:Label ID="lblJefeSanidad" runat="server" meta:resourcekey="lblJefeSanidad"></asp:Label>
                                <asp:Label ID="lblJefeSanidadSeleccionado" runat="server" ></asp:Label>
                            </div>
                            <div class="span3">
                                <button id="btnGuardar" class="btn SuKarne" data-dismiss="modal">
                                    <asp:Label ID="lblCerrarEtiqueta" runat="server" meta:resourcekey="btnGuardarCancelacion"></asp:Label>
                                </button>
                                <button type="button" id="btnCancelar"  class="btn SuKarne">
                                    <asp:Label ID="lblSeleccionarEtiqueta" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                </button>
                             </div>
                            
                        </div>
                        
                    </div>
                </div>
			</div>
            
            <asp:HiddenField runat="server" ID="msgErrorRolJefeSanidad" />
            <asp:HiddenField runat="server" ID="msgErrorNoHayMovimientos" />
            <asp:HiddenField runat="server" ID="msgOK" />
        </form>
    </div>
    <script type="text/javascript">
        var msgCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Corral %>"/>';
        var msgNoArete = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoArete %>"/>';
        var msgAreteTestigo = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.AreteTestigo %>"/>';
        var msgFechaDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.FechaDeteccion %>"/>';
        var msgHoraDeteccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.HoraDeteccion %>"/>';
        var msgDetector = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Detector %>"/>';
        var msgAccion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Accion %>"/>';

        var SalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.SalirSinGuardar %>"/>';
        var Si = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Si %>"/>';
        //var msgOK = '<asp:Literal runat="server" Text="<%$ Resources:Ok %>"/>';
        var No = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.No %>"/>';
        var GuardoExito = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.GuardoExito %>"/>';
        var ErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.ErrorGuardar %>"/>';
        var ErrorDatosEnBlanco = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.ErrorDatosEnBlanco %>"/>';

    </script>

</body>

</html>
