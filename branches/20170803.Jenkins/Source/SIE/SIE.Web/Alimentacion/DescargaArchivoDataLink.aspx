<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DescargaArchivoDataLink.aspx.cs" Inherits="SIE.Web.Alimentacion.DescargaArchivoDataLink" %>
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
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
  
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
    
    <script src="../Scripts/DescargaArchivoDataLink.js"></script>
    <script type="text/javascript">
        var msgDatosGuardadosExito = '<asp:Literal runat="server" Text="<%$ Resources:msgDatosGuardadosExito.Text %>"/>';
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
                                <a href="../Alimentacion/DescargaArchivoDataLink.aspx">Descargar archivo para DataLink</a>
                            </li>
                        </ul>
                    </div>

                    <div class="row-fluid">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <asp:Label ID="lblCorrales" runat="server"  meta:resourcekey="lblTituloGrupo"></asp:Label>
                            </legend>
                            
                            <div class="row-fluid">
                                <div class="span4"><label class=""></label></div>
                            </div>
                             
                            <div class="span12">
                                
                                <div class="span3">
                                    <span class="requerido">*</span>
                                    <asp:Label ID="lblTurno" runat="server" meta:resourcekey="lblEtiquetaTurno"></asp:Label>
                                    <asp:DropDownList ID="cmbServicios" CssClass="span6" runat="server" DataValueField="TipoServicioId" DataTextField="DescripcionCombo" />
                                </div>
                              
                                <div class="span5">
                                    <span class="requerido">*</span>
                                   <asp:Label ID="Label1" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                   <input type="text" id="datepicker" readonly="true"/>
                                </div>
                                <div class="span2">
                                    <div class="pull-right">
                                        <div class="span8">
                                            <button type="button" id="btnBuscar" data-toggle="modal" class="btn letra SuKarne">
                                               <asp:Label ID="Label2" runat="server" meta:resourcekey="btnBuscar"></asp:Label>
                                            </button>
                                         </div>
                                         <div class="span2">
                                            <button type="button" id="btnDescargarArchivo" data-toggle="modal" class="btn letra SuKarne">
                                                <asp:Label ID="Label6" runat="server" meta:resourcekey="btnDescargarArchivo"></asp:Label>
                                            </button>
                                         </div>
                                     </div>
                                </div>                      
                            </div>
                        </fieldset>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
       <asp:HiddenField runat="server" ID="msgErrorUsuario" />
       <asp:HiddenField runat="server" ID="msgNoExisteTurnos" />
       <asp:HiddenField runat="server" ID="msgOK" />
       <asp:HiddenField runat="server" ID="msgRuta" />
       <asp:HiddenField runat="server" ID="msgSeleccionesServicio" />
       <asp:HiddenField runat="server" ID="msgSeleccionesFecha" />
       <asp:HiddenField runat="server" ID="RepartoFechaNoValida" />
       <asp:HiddenField runat="server" ID="RepartoArchivoSinDatos" />
       <asp:HiddenField runat="server" ID="RepartoErrorInesperado" />
       <asp:HiddenField runat="server" ID="msgFechaMayor" />
    </form>
    </div>
</body>
</html>
