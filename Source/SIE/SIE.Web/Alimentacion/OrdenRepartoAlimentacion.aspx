<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdenRepartoAlimentacion.aspx.cs" Inherits="SIE.Web.Alimentacion.OrdenRepartoAlimentacion" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>

    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
        
    </asp:PlaceHolder>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    
    <style>
        .caja {
            border: 2px solid #c0c0c0;
            padding: 10px;
        }

        .seccionbotones {
            padding: 10px;
        }

        textarea {
	        resize: none;
        }
    </style>
    
    <script src="../Scripts/OrdenRepartoAlimentacion.js"></script>
    
    <script type="text/javascript">
        var msgDatosGuardadosExito = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardadosExito.Text %>"/>';
        var msgDatosPreguntaCancelar = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosPreguntaCancelar.Text %>"/>';
        var msgEjecutandoOrden = '<asp:Literal runat="server" Text="<%$ Resources:msgEjecutandoOrden %>"/>';
    </script>
</head>
<body class="page-header-fixed">
    <div id="pagewrap">
   <form id="idform" runat="server" class="form-horizontal">
       <div id="skm_LockPane" class="LockOff"></div>
    <div class="row-fluid">
        <div class="span12">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="row-fluid caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span>
                            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo"></asp:Label>
                        </span>
                    </div>
                </div>

                <div class="portlet-body form">
                    <div class="row-fluid">
                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home"></i>
                                <a href="../Principal.aspx">Home</a>
                                <i class="icon-angle-right"></i>
                            </li>
                            <li>
                                <a href="../Alimentacion/OrdenRepartoAlimentacion.aspx">Orden de reparto</a>
                            </li>
                        </ul>
                    </div>

                    <div class="row-fluid">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblCorrales" runat="server"  meta:resourcekey="lblTituloGrupo"></asp:Label>
                            </legend>
                            
                            <div class="row-fluid">
                                <div class="span4"><label class=""><asp:Label ID="LblTipoServicio" runat="server" meta:resourcekey="lblTipoServicio"></asp:Label></label></div>
                            </div>
                            
                              <div class="span12">
                                <div class="span4">
                                    <div class="space10"></div>
                                    <div class="span12">
                                        <asp:Label ID="lblSeccion" meta:resourcekey="lblSeccion" Text="Seccion" runat="server" />
                                        <asp:DropDownList runat="server" ID="ddlSeccion" DataTextField="Descripcion" DataValueField="Seccion" AutoPostBack="False"/>
                                        
                                    </div>
                                </div>
                            </div>
                             
                            <div class="span12">
                                <div class="span4">
                                    <div class="space10"></div>
                                    <div class="span12">
                                        <asp:RadioButton ID="rdbMatutino" runat="server" GroupName="radios" TextAlign="Right"/>
                                        <asp:Label ID="lblrdbMatutino" runat="server" />
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="space10"></div>
                                    <div class="span12">
                                        <asp:RadioButton ID="rdbVespertino" runat="server" GroupName="radios" TextAlign="Right"/>
                                        <asp:Label ID="lblrdbVespertino" runat="server" />
                                    </div>
                                </div>
                                
                            </div>
                             <div class="span12">
                                 <hr width=50% align="center"></hr>
                             </div>
                            <div class="span12">
                                <div class="span4">
                                    <div class="space10"></div>
                                    <div class="span12">
                                        <asp:Label ID="lblFechaReparto" runat="server" meta:resourcekey="lblFechaReparto" />
                                        <asp:TextBox ID="txtFechaReparto" class="span5" ReadOnly="True" runat="server" meta:resourcekey="txtFechaReparto" ViewStateMode="Enabled"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            
                            <div class="row-fluid">
                                <div class="seccionbotones span12">
                                    <div class="pull-right">
                                        <button type="button" id="btnGuardarReparto" data-toggle="modal" class="btn letra SuKarne">
                                            <asp:Label ID="Label6" runat="server" meta:resourcekey="btnGuardar"></asp:Label>
                                        </button>
                                        <button type="button" id="btnCancelar" data-toggle="modal" class="btn letra SuKarne">
                                            <asp:Label ID="Label7" runat="server" meta:resourcekey="btnCancelar"></asp:Label>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
   
        <div id="msgCancelar" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
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

       <div id="responsive" class="modal container hide fade" tabindex="-1" data-width="750" data-backdrop="static">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label ID="lblBusquedaFolio" runat="server" meta:resourcekey="lblTituloProgress"></asp:Label>
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="lblProceso"></asp:Label>
                    <asp:Label ID="lblEstatusProceso" runat="server"></asp:Label>
                </div>
                <div class="portlet-body">
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="lblAvance"></asp:Label>
                    
                    <div class="progress progress-striped active">
                      <div class="bar" style="width: 0%;"></div>
                    </div>
                    <div id="labelPorcentaje" align="center"></div>
                </div>
            </div>
        </div>
       
       

        <asp:HiddenField runat="server" ID="msgErrorUsuario" />
        <asp:HiddenField runat="server" ID="msgSinCorrales" />
        <asp:HiddenField runat="server" ID="msgOK" />
        <asp:HiddenField runat="server" ID="msgSinConsumoTotal" />
        <asp:HiddenField runat="server" ID="msgCorralesIncompletos" />
        <asp:HiddenField runat="server" ID="msgErrorProceso" />
        <asp:HiddenField runat="server" ID="msgFaltaPorcentaje" />
        <asp:HiddenField runat="server" ID="AlimentoNoServidoMatutino" />
        <asp:HiddenField runat="server" ID="AlimentoNoServidoVespertino" />
        <asp:HiddenField runat="server" ID="idUsuario" />
        <asp:HiddenField runat="server" ID="msgErrorParametros" />
        <asp:HiddenField runat="server" ID="btnSi" />
        <asp:HiddenField runat="server" ID="btnNo" />
       
    </form>
    </div>
</body>
</html>
