<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluacionPartidaImpresion.aspx.cs" Inherits="SIE.Web.Sanidad.EvaluacionPartidaImpresion" %>
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
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link href="../assets/css/EvaluacionPartida.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    
    <script src="../Scripts/EvaluacionPartidaImpresion.js"></script>
    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body class="page-header-fixed">
    <div id="Principal">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="skm_LockPane" class="LockOff">
            </div>
            <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" />
                                <span>
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <ul class="breadcrumb">
                                <li>
                                    <i class="icon-home"></i>
                                    <a href="../Principal.aspx">Home</a>
                                    <i class="icon-angle-right"></i>
                                </li>
                                <li>
                                    <a href="EvaluacionPartidaImpresion.aspx">Evaluación Partida Impresión</a>
                                </li>
                            </ul>

                            <!-- Groupo box Busqueda -->
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblBusqueda" runat="server" meta:resourcekey="lblBusquedaResource1"></asp:Label></legend>
                                    
                                        <div class="span5">
                                            <span class="requerido">*</span>
                                            <asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFecha"></asp:Label>
                                            <input type="text" id="datepicker" readonly="true" style="margin-bottom: 0px;"/>
                                        </div>
                                        <div class="span2">
                                        <div class="pull-right">
                                            <div class="span8">
                                                <button type="button" id="btnBuscar" data-toggle="modal" class="btn letra SuKarne">
                                                   <asp:Label ID="Label2" runat="server" meta:resourcekey="btnBuscar"></asp:Label>
                                                </button>
                                             </div>
                                             <div class="span2">
                                                <button type="button" id="btnLimpiar" data-toggle="modal" class="btn letra SuKarne">
                                                    <asp:Label ID="Label6" runat="server" meta:resourcekey="btnLimpiar"></asp:Label>
                                                </button>
                                             </div>
                                         </div>
                                    </div>                      

                                </fieldset>
                            </div>

                            <!-- Grid Evaluaciones -->
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblEvaluaciones" runat="server" meta:resourcekey="lblEvaluacionesResource1"></asp:Label></legend>
                                    <div class="control-group">
                                        <div id="GridCorralesEvaluados"></div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <asp:HiddenField ID="hfOrganizacionID" runat="server" />
            <asp:HiddenField ID="hfErrorImprimir" runat="server" />
        </form>
    </div>
    <script type="text/javascript">
        //CABECEROS TABLA CORRALES
        var CabeceroFolio = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroFolio %>"/>';
        var CabeceroCorral = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroCorral %>"/>';
        var CabeceroLote = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroLote %>"/>';
        var CabeceroCabezas = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroCabezas %>"/>';
        var CabeceroKilosLlegada = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroKilosLlegada  %>"/>';
        var CabeceroFechaRecepcion = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroFechaRecepcion %>"/>';
        var CabeceroFechaEvaluacion = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroFechaEvaluacion %>"/>';
        var CabeceroOrigen = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroOrigen %>"/>';
        var CabeceroSalida = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroSalida %>"/>';
        var CabeceroOpcion = '<asp:Literal runat="server" Text="<%$ Resources:CabeceroOpcion %>"/>';
        
        //MENSAJES
        var SinCorrales = '<asp:Literal runat="server" Text="<%$ Resources:SinCorrales %>"/>';
        var ErrorAlConsultarCorrales = '<asp:Literal runat="server" Text="<%$ Resources:ErrorAlConsultarCorrales %>"/>';
        
        //DATATABLES
        var PrimeraPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.PrimeraPagina %>"/>';
        var UltimaPagina = '<asp:Literal runat="server" Text="<%$ Resources:datatable.UltimaPagina %>"/>';
        var Siguiente = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Siguiente %>"/>';
        var Anterior = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Anterior %>"/>';
        var SinDatos = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinDatos %>"/>';
        var Mostrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrando %>"/>';
        var SinInformacion = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinInformacion %>"/>';
        var Filtrando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Filtrando %>"/>';
        var Mostrar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Mostrar %>"/>';
        var Cargando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Cargando %>"/>';
        var Procesando = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Procesando %>"/>';
        var Buscar = '<asp:Literal runat="server" Text="<%$ Resources:datatable.Buscar %>"/>';
        var SinRegistros = '<asp:Literal runat="server" Text="<%$ Resources:datatable.SinRegistros %>"/>';
        
    </script>
</body>
    <!--
    <script src="../assets/plugins/jquery-ui-1.10.1.custom.min.js"></script>
    <script src="../assets/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
    <script src="../assets/scripts/jquery-jtemplates.js"></script>
    <script src="../assets/plugins/data-tables/DT_bootstrap.js"></script>
    <script src="../assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
    <script src="../assets/scripts/ui-modals.js"></script>
    <script src="../assets/scripts/app.js"></script>
    <script src="../assets/plugins/spin.js"></script>
    <script src="../assets/plugins/jquery.spin.js"></script>
    <script src="../assets/plugins/numericInput/jquery-numericInput.min.js"></script>
    <script src="../assets/plugins/jquery-linq/linq.js"></script>
    <script src="../assets/plugins/jquery-alphanumeric/jquery-alphanumeric.js"></script>
    -->
</html>