<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluacionPartida.aspx.cs" Inherits="SIE.Web.Sanidad.EvaluacionPartida" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headEvaluacion" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>

    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/css/style-metro.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style-responsive.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap-modal.css" rel="stylesheet" />
    <script src="../assets/plugins/jquery-1.7.1.min.js"></script>
    <script src="../assets/plugins/bootstrap-bootbox/js/bootbox.min.js"></script>
    <link href="../assets/css/media-queries.css" rel="stylesheet" />
    <link href="../assets/plugins/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <script src="../assets/plugins/data-tables/jquery.dataTables.js"></script>
    <script src="../Scripts/EvaluacionPartida.js"></script>
    <script src="../Scripts/json2.js"></script>

    <link rel="shortcut icon" href="../favicon.ico" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body class="page-header-fixed">
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
                                    <a href="EvaluacionPartida.aspx">Evaluación Partida</a>
                                </li>
                            </ul>
                            <div class="row-fluid">
                                <div class="span1">
                                </div>
                                <div class="span1">
                                    <span class="requerido">*</span>
                                    <asp:Label ID="lblEtiquetaCorral" runat="server" meta:resourcekey="lblEtiquetaCorralResource1"></asp:Label>
                                </div>

                                <div class="span1">
                                    <asp:TextBox ID="txtCorral" runat="server" ReadOnly="true" class="span12" meta:resourcekey="txtCorralResource1"></asp:TextBox>
                                </div>
                                <div class="span2">
                                    <button type="button" id="btnBuscar" data-toggle="modal" class="btn letra SuKarne">
                                        <asp:Label ID="lblBuscar" runat="server" meta:resourcekey="lblBuscarResource1"></asp:Label></button>

                                </div>
                                <div class="span1">
                                    <asp:Label ID="lblLotess" runat="server" Text="Lote:" meta:resourcekey="lblEtiquetaLoteResource1"></asp:Label>
                                </div>
                                <div class="span1">
                                    <span>
                                        <asp:TextBox ID="txtLote" class="span12" ReadOnly="True" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="span4">
                                    <span>
                                        <asp:Label ID="lblEtiquetaTitulo" runat="server" meta:resourcekey="lblEtiquetaTituloResource1"></asp:Label>
                                    </span>
                                </div>
                                <div class="span12">
                                    <div class="span4">
                                        <div class="span6">
                                            <span>
                                                <asp:Label ID="lblEtiquetaCabezas" runat="server" Text="Cabezas:" meta:resourcekey="lblEtiquetaCabezasResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtCabezas" runat="server" class="span12" ReadOnly="True" meta:resourcekey="txtCabezasResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <asp:Label ID="lblEtiquetaFechaEvaluacion" runat="server" Text="Fecha Evaluación" meta:resourcekey="lblEtiquetaFechaEvaluacionResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtFechaEvaluacion" class="span12" ReadOnly="True" runat="server" meta:resourcekey="txtFechaEvaluacionResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <asp:Label ID="lblPesoLlegada" runat="server" Text="Peso llegada:" meta:resourcekey="lblPesoLlegadaResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtPesoLlegada" runat="server" class="span12" ReadOnly="True" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <span>
                                                <asp:Label ID="lblNumeroPartida" runat="server" Text="Número de Partida:" meta:resourcekey="lblNumeroPartidaResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtPartidas" runat="server" class="span12" ReadOnly="True" meta:resourcekey="txtPartidasResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <asp:Label ID="lblEtiquetaHr" runat="server" Text="Hora Evaluación" meta:resourcekey="lblEtiquetaHrResource1"></asp:Label>
                                                </span>
                                                <span>
                                                    <asp:TextBox ID="txtHrEvalacion" runat="server" class="span12" ReadOnly="True" meta:resourcekey="txtHrEvalacionResource1" ViewStateMode="Enabled"></asp:TextBox>
                                                </span>
                                            </div>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <asp:Label ID="lblHoraLlegada" runat="server" Text="Hora llegada:" meta:resourcekey="lblHoraLlegadaResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtHoraLlegada" runat="server" class="span12" ReadOnly="True" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span8">
                                        <div class="span4">
                                            <span>
                                                <asp:Label ID="lblEtiquetaProcedencia" runat="server" meta:resourcekey="lblEtiquetaProcedenciaResource1"></asp:Label>
                                            </span>
                                            <textarea id="txtProcedencia" name="txtProcedencia" readonly="readonly" rows="1" class="span12"></textarea>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <span class="requerido"></span>
                                                    <asp:Label ID="lblEtiquetaEvaluador" runat="server" Text="Evaluador" meta:resourcekey="lblEtiquetaEvaluadorResource1"></asp:Label>
                                                </span>
                                                <span>
                                                    <asp:TextBox ID="txtEvaluador" runat="server" class="span12" ReadOnly="True" ViewStateMode="Enabled"></asp:TextBox>
                                                </span>
                                                <!--<asp:DropDownList ID="cmbEvaluador" class="span12" require="true" runat="server" meta:resourcekey="cmbEvaluadorResource1" ViewStateMode="Enabled" />-->
                                            </div>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <asp:Label ID="lblMerma" runat="server" Text="Merma:" meta:resourcekey="lblMermaResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtMermaVisible" runat="server" class="span12" ReadOnly="True" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <span>
                                                <asp:Label ID="lblEtiquetaInvInicial" runat="server" Text="Inv Inicial" meta:resourcekey="lblEtiquetaInvInicialResource1"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtInventarioInicial" class="span12" runat="server" ReadOnly="True" meta:resourcekey="txtInventarioInicialResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            <div style="margin-top: 20px">
                                                <span>
                                                    <span class="requerido">*</span>
                                                    <asp:Label ID="lblInvFinal" runat="server" Text="Inv Final" meta:resourcekey="lblInvFinalResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtInvFinal" class="span12" runat="server" ReadOnly="True" MaxLength="10" type="number"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="span7">
                                                <span>
                                                    <asp:Label ID="lblEtiquetaFechaLlegada" runat="server" meta:resourcekey="lblEtiquetaFechaLlegadaResource1"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtFechaLlegada" runat="server" ReadOnly="True" class="span12" ViewStateMode="Enabled"></asp:TextBox>

                                                <br />
                                                <br />
                                                <br />

                                                <button type="button" id="btnDatosEnfermeria" data-toggle="modal" class="btn letra SuKarne">
                                                    <asp:Label ID="lblEnfermeria" runat="server" meta:resourcekey="lblEnfermeriaResource1"></asp:Label></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <br />
                            <br />
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label ID="lblDatosMetafilaxia" runat="server" meta:resourcekey="lblDatosMetafilaxiaResource1"></asp:Label>
                                </div>
                            </div>
                            <table class="portlet-body form">
                                <div id="TablaPreguntas"></div>
                                <table class="table">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAcciones" runat="server" meta:resourcekey="lblAccionesResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <div>
                                                <label class="radio">
                                                    <asp:RadioButton ID="rdbMetafilaxia" class="radio" runat="server" Enabled="False" meta:resourcekey="rdbMetafilaxiaResource1" />
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <label class="radio">
                                                <asp:RadioButton ID="rdbNormal" runat="server" class="radio" Enabled="False" meta:resourcekey="rdbNormalResource1" />
                                            </label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNota" runat="server" meta:resourcekey="lblNotaResource1"></asp:Label>
                                            <asp:Label ID="lblNotaDescripcion" runat="server" meta:resourcekey="lblNotaDescripcionResource1"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="radio">
                                                <asp:RadioButton ID="rdnMetafilaxiaAutorizada" runat="server" meta:resourcekey="rdnMetafilaxiaAutorizadaResource1" />
                                            </label>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblJustificacion" runat="server" meta:resourcekey="lblJustificacionResource1"></asp:Label>
                                            <textarea id="txtJustificacion" name="txtJustificacion" maxlength="255" rows="4" class="span12" readonly="readonly" style="resize: none"></textarea>
                                        </td>
                                    </tr>
                                </table>
                                <div id="idConfirmacion" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="idConfirmacion" aria-hidden="true">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                        <h3 id="myModalLabel3">
                                            <br />
                                            Confirmación
                                        </h3>
                                    </div>
                                    <div class="modal-body">
                                        <p>
                                            <asp:Label ID="lblGuardarConfirm" runat="server" meta:resourcekey="lblGuardarConfirmResource1"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="modal-footer">
                                        <button data-dismiss="modal" id="btnGuardar" class="btn SuKarne">
                                            <asp:Label ID="lblGuardarEtiqueta" runat="server" meta:resourcekey="lblGuardarEtiquetaResource1"></asp:Label>
                                        </button>
                                        <button class="btn SuKarne  " data-dismiss="modal" aria-hidden="true">
                                            <asp:Label ID="lblCancelarEtiqueta" runat="server" meta:resourcekey="lblCancelarEtiquetaResource1"></asp:Label>
                                        </button>

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
                                <div class="form-actions">
                                    <div>
                                        <table class="pull-right">
                                            <tr>
                                                <td>
                                                    <a href="#idConfirmacion" role="button" class="btn SuKarne pull-right" data-toggle="modal">
                                                        <asp:Label ID="lblGuardarEtiquetaBoton" runat="server" meta:resourcekey="lblGuardarEtiquetaBotonResource1"></asp:Label></a>
                                                </td>

                                                <td>
                                                    <button id="btnCancelarEvaluacion" type="button" class="btn SuKarne" data-dismiss="modal">
                                                        <asp:Label ID="lblCancel" runat="server" meta:resourcekey="lblCancelarEtiquetaResource1"></asp:Label></button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div id="responsive" class="modal container hide fade" tabindex="-1" data-width="750" data-backdrop="static">
                <div class="portlet box SuKarne2">
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Label ID="lblBusquedaFolio" runat="server" meta:resourcekey="lblBusquedaFolioResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <table class="table table-striped table-bordered table-hover">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBusqueda" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True"
                                        BorderColor="#CC9966" BorderStyle="None"
                                        BorderWidth="1px" CellPadding="4" OnRowDataBound="gvBusqueda_RowDataBound" meta:resourcekey="gvBusquedaResource1" OnPageIndexChanging="gvBusqueda_PageIndexChanging" PageSize="15">
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <Columns>
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="radioCorral" runat="server" GroupName="seleccionar" meta:resourcekey="radioCorralResource1" />
                                                    <asp:HiddenField ID="CorralID" runat="server" Value='<%# Eval("CorralID") %>' />
                                                    <asp:HiddenField ID="PesoBruto" runat="server" Value='<%# Eval("PesoBruto") %>' />
                                                    <asp:HiddenField ID="FechaSalida" runat="server" Value='<%# Eval("FechaSalida") %>' />
                                                    <asp:HiddenField ID="Horas" runat="server" Value='<%# Eval("Horas") %>' />
                                                    <asp:HiddenField ID="HoraLlegada" runat="server" Value='<%# Eval("HoraLlegada") %>' />
                                                    <asp:HiddenField ID="LoteID" runat="server" Value='<%# Eval("LoteID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FolioEntrada" HeaderText="Folio" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                            <asp:BoundField DataField="CodigoCorral" HeaderText="Corral" HtmlEncode="false" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                            <asp:BoundField DataField="Lote" HeaderText="Lote" HtmlEncode="false" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                            <asp:BoundField DataField="CabezasRecibidas" HeaderText="Cabezas" HtmlEncode="false" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                                            <asp:BoundField DataField="PesoLlegada" HeaderText="Kgs Llegada" HtmlEncode="false" meta:resourcekey="BoundFieldResource6"></asp:BoundField>
                                            <asp:BoundField DataField="FechaEntrada" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" meta:resourcekey="BoundFieldResource7"></asp:BoundField>
                                            <asp:BoundField DataField="OrganizacionOrigen" HeaderText="Origen" HtmlEncode="false" meta:resourcekey="BoundFieldResource8"></asp:BoundField>
                                            <asp:BoundField DataField="FolioOrigen" HeaderText="Salida" HtmlEncode="false" meta:resourcekey="BoundFieldResource9"></asp:BoundField>
                                        </Columns>
                                        <PagerSettings FirstPageText="Primero" LastPageText="Ultimo" PageButtonCount="4" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSeleccion" class="btn SuKarne">
                            <asp:Label ID="lblSeleccionarEtiqueta" runat="server" meta:resourcekey="lblSeleccionarEtiquetaResource1"></asp:Label>
                        </button>
                        <button id="btnCerrar" type="submit" class="btn SuKarne" data-dismiss="modal">
                            <asp:Label ID="lblCerrarEtiqueta" runat="server" meta:resourcekey="lblCerrarEtiquetaResource1"></asp:Label>
                        </button>
                    </div>
                </div>
                <div id="Enfermeria" class="modal container hide fade" tabindex="1" data-width="750" data-backdrop="static">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Label ID="lblDatosEnfermeria" runat="server" meta:resourcekey="lblDatosEnfermeriaResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <table>
                                <tr>
                                    <td class="tabs-right top pull-left">
                                        <asp:GridView ID="dgvDatosEnfermeria" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CC9966" BorderStyle="None"
                                            CellPadding="0" meta:resourcekey="dgvDatosEnfermeriaResource1">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                                <asp:BoundField DataField="Descripcion" HeaderText="Concepto" meta:resourcekey="BoundFieldResource10"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Resultados" meta:resourcekey="TemplateFieldResource3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtResultado" type='<%# Eval("Valor") %>' oninput="maxLengthCheck(this)" MaxLength="10" runat="server" ReadOnly='<%# Eval("Activo") %>' meta:resourcekey="txtResultadoResource1"></asp:TextBox>
                                                        <asp:HiddenField ID="PreguntaID" runat="server" Value='<%# Eval("PreguntaID") %>' />
                                                        <asp:HiddenField ID="Activo" runat="server" Value='<%# Eval("Activo") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td class="tabs-right top pull-left"></td>
                                    <td class="tabs-right top pull-left">
                                        <aside id="seccionGarrapata">
                                            <section class="widget">
                                                <h4>
                                                    <asp:Label ID="lblPartidaGarrapatas" runat="server" meta:resourcekey="lblPartidaGarrapatasResource1"></asp:Label><br />
                                                </h4>
                                                <ul class="radio right">
                                                    <asp:RadioButton ID="rdbLeve" TextAlign="right" runat="server" GroupName="garrapata" meta:resourcekey="rdbLeveResource1"></asp:RadioButton><br />
                                                    <asp:RadioButton ID="rdbModerado" runat="server" GroupName="garrapata" meta:resourcekey="rdbModeradoResource1"></asp:RadioButton><br />
                                                    <asp:RadioButton ID="rbdGrave" runat="server" GroupName="garrapata" meta:resourcekey="rbdGraveResource1"></asp:RadioButton><br />
                                                </ul>
                                            </section>
                                        </aside>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="modal-footer" style="width: 402px;">
                                            <button type="button" id="btnGuardarEnfermeria" data-dismiss="modal" class="btn SuKarne">
                                                <asp:Label ID="lblGuardarEtiqueta2" runat="server" meta:resourcekey="lblGuardarEtiquetaBotonResource1"></asp:Label>
                                            </button>
                                            <button type="button" id="btnCancelarEnfermeria" data-dismiss="modal" class="btn SuKarne">
                                                <asp:Label ID="lblCancelarEtiqueta1" runat="server" meta:resourcekey="lblCancelarEtiquetaResource1"></asp:Label>
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="corralId" />
                <asp:HiddenField runat="server" ID="hfLoteID" />
                <asp:HiddenField runat="server" ID="kgsOrigen" />
                <asp:HiddenField runat="server" ID="fechaSalida" />
                <asp:HiddenField runat="server" ID="Horas" />
                <asp:HiddenField runat="server" ID="txttotalPreguntas" />
                <asp:HiddenField runat="server" ID="txttotalAyuda" />
                <asp:HiddenField runat="server" ID="txtGarrapata" Value="0" />
                <asp:HiddenField runat="server" ID="txtPorcentajeCC3" Value="0" />
                <asp:HiddenField runat="server" ID="txtGuardar" Value="0" />
                <asp:HiddenField runat="server" ID="paginado" Value="0" />
                <asp:HiddenField runat="server" ID="evaluadores" Value="0" />
                <asp:HiddenField runat="server" ID="txtMerma" Value="0" />
                <asp:HiddenField runat="server" ID="txtPesoPromedio" Value="0" />
                <asp:HiddenField runat="server" ID="txtHoras" Value="0" />
                <asp:HiddenField runat="server" ID="hdnCbzCcTotaL" Value="0" />
                <asp:HiddenField runat="server" ID="hdnCbzEnfermosGTotaL" Value="0" />
                <asp:HiddenField runat="server" ID="hdnCbzMorbilidad" Value="0" />
                <asp:HiddenField runat="server" ID="msgErrorRolEvaluador" />
                <asp:HiddenField runat="server" ID="hdnCbzCRB" />
            </div>
        </form>
    </div>
    <script type="text/javascript">
        var msgGuardado = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.Guardado %>"/>';
        var msgErrorGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.ErrorGuardar %>"/>';
        var msgNoPreguntas = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoPreguntas %>"/>';
        var msgDatosObligatorios = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.DatosObligatorios %>"/>';
        var msgMetafiaxiaJustificacion = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.MetafiaxiaJustificacion %>"/>';
        var msgCapturarDatosPreguntas = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CapturarDatosPreguntas %>"/>';
        var msgInventarioNoMayorInicial = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.InventarioNoMayorInicial %>"/>';
        var msgSeleccionarEvaluador = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.SeleccionarEvaluador %>"/>';
        var msgCapturarInvFinal = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CapturarInvFinal %>"/>';
        var msgNoHayPreguntas = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoHayPreguntas %>"/>';
        var msgNoHayPreguntasGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoHayPreguntasGuardar %>"/>';
        var msgGuardadoExito = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.GuardadoExito %>"/>';
        var msgNoSeleccionar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoSeleccionar %>"/>';
        var msgNoHayRegistros = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoHayRegistros %>"/>';
        var msgNoHayEvaluadores = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NoHayEvaluadores %>"/>';
        var msgCorralNoValido = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CorralNoValido %>"/>';
        var msgCorralNoExiste = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.CorralNoExiste %>"/>';
        var msgNecesarioRadio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.NecesarioRadio %>"/>';
        var msgSeleccioneFolio = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.SeleccioneFolio %>"/>';
        var msgSinCorral = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.SinCorral %>"/>';
        var msgNoHayDatosEnfermeria = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.FaltanDatosEnfermeria %>"/>';
        var msgInvintarioMayorCero = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.InventarioMayorCero %>"/>';
        var msgCabezasEnfermeriasMayorInvFinal = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgCabezasEnfermeriasMayorInvFinal %>"/>';
        var msgSalirSinGuardar = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgSalirSinGuardar %>"/>';
        var msgDatosEnfermeria = '<asp:Literal runat="server" Text="<%$ Resources:Codebehind.msgDatosEnfermeria %>"/>';

    </script>
    <%--</body>--%>
    <!--<script src="../assets/plugins/bootstrap-bootbox/js/bootbox.min.js"></script>   -->
    <script src="../assets/plugins/jquery-ui-1.10.1.custom.min.js"></script>
    <script src="../assets/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
    <script src="../assets/scripts/jquery-jtemplates.js"></script>
    <script src="../assets/plugins/data-tables/DT_bootstrap.js"></script>
    <script src="../assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
    <script src="../assets/scripts/ui-modals.js"></script>
    <script src="../assets/scripts/app.js"></script>
</html>
