<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlSacrificio.aspx.cs" Inherits="SIE.Web.Manejo.ControlSacrificio" %>
<!DOCTYPE html>
<%@ Import Namespace="System" %>
<%@ Import Namespace="SIE.Services.Info.Info" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%= this.GetLocalResourceObject("ControlSacrificio_Title").ToString() %></title>
    <link href="<%= Ruta("~/Content/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= Ruta("~/Content/bootstrap-theme.min.css") %>" rel="stylesheet" />
    <link href="<%= Ruta("~/Content/font-awesome.min.css") %>" rel="stylesheet" />
    <link href="<%= Ruta("~/Content/toastr.min.css") %>" rel="stylesheet" />
    <link href="<%= Ruta("~/assets/css/controlSacrificio.css") %>" rel="stylesheet" type="text/css" />

    <style>
        .Cabecero
        {
            background-color: #B94A48 !important;
            border: 1px solid #B94A48;
        }
            .Cabecero h3 {
                display:inline-block;
            }
    </style>

    
</head>

<body class="content">
    <input type="hidden" data-bind="value: '<%= DateTime.Today.ToString("yyyy/MM/dd") %>'" />

	<div class="row-fluid Cabecero">
		<img id="imgLogo" src="../Images/skLogo.png" />
		<h3><%= GetLocalResourceObject("ControlSacrificio_Title").ToString() %></h3>
	</div>
    
    <div class="row well">
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <%= GetLocalResourceObject("lblFecha").ToString() %>
            </div>

            <div class="col-md-2 col-xs-6">
                <div class="input-group">
                  <input type="datetime" class="form-control" data-bind="datepicker: fechaSacrificio, datepickerOptions: { dataType: 'format', format: 'YYYY/MM/DD' }" />
                  <span class="input-group-btn">
                    <button class="btn btn-primary" data-bind="click: ConsultaSacrificio">
                        <i class="fa fa-search"></i>
                    </button>
                  </span>
                </div><!-- /input-group -->
            </div>

            <div class="col-md-2 col-xs-6">
                <%= GetLocalResourceObject("lblTotal").ToString() %>
            </div>

            <div class="col-md-2 col-xs-6">
                <input type="text" class="form-control" data-bind="value: Total" disabled="disabled" />
            </div>

            <div class="col-md-2 col-xs-6">
                <%= GetLocalResourceObject("lblEstatus").ToString() %>
            </div>
            <div class="col-md-2 col-xs-6">
                <label data-bind="text: Estatus(), css: EstatusClass()"></label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <%= GetLocalResourceObject("lblOrganizacion").ToString() %>
            </div>
            <div class="col-md-2 col-xs-6">
                <input type="text" class="form-control" data-bind="value: Organizacion" disabled="disabled"/>
            </div>
            <div class="col-md-2 <%--col-md-1 col-md-offset-1--%> col-xs-6">
                <%= GetLocalResourceObject("lblProcesadas").ToString() %>
            </div>
            <div class="col-md-2 col-xs-6">
                <input type="text" class="form-control" data-bind="value: Procesadas" disabled="disabled" />
            </div>
            <div class="col-md-2 col-xs-0">
            </div>
            <div class="col-md-2 col-xs-12">
                <button type="button" data-toggle="button" aria-pressed="false" data-bind="text: FiltradoTexto(), click: Filtrar, css: FiltradoClass()">
                </button>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <table id="mytable" class="table table-condensed table-striped table-fixed-header">
                <colgroup>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                    <col class="col-md-1"/>
                </colgroup>
                    <tr class="header">
                        <th class="col-md-1"><%= GetLocalResourceObject("lblPO").ToString() %>              </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblCorral").ToString() %>           </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblCorralSalida").ToString() %>           </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblTipoGanado").ToString() %>           </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblArete").ToString() %>           </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblConsecutivo").ToString() %>          </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblNoqueo").ToString() %>          </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblPielEnSangre").ToString() %>      </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblPielDescarnada").ToString() %>      </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblInspeccion").ToString() %>      </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblCanalCompleta").ToString() %>      </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblCanalCaliente").ToString() %>      </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblVisceras").ToString() %>        </th>
                        <th class="col-md-1"><%= GetLocalResourceObject("lblAcciones").ToString() %>        </th>
                    </tr>
                <tbody data-bind="foreach: Sacrificio">
                    <tr data-bind="visible: enlaceFiltrado">
                        <td id="po" data-bind="text: Po"></td>
                        <td id="corral" data-bind="text: NumeroCorral"></td>
                        <td id="tipo" data-bind="text: CodigoCorral"></td>
                        <td id="tipoGanado" data-bind="text: TipoGanado"></td>
                        <td id="arete"data-bind="text: Arete"></td>
                        <td id="consecutivo" data-bind="text: Consecutivo"></td>
                        <td id="noqueo">
                            <i data-bind="css: iNoqueo()"></i></td>
                        <td id="pSangre">
                            <i data-bind="css: iPielEnSangre()"></i></td>
                        <td id="pDescarnada">
                            <i data-bind="css: iPielDescarnada()"></i></td>
                        <td id="inspeccion">
                            <i data-bind="css: iInspeccion()"></i></td>
                        <td id="cCompleta">
                            <i data-bind="css: iCanalCompleta()"></i></td>
                        <td id="cCaliente">
                            <i data-bind="css: iCanalCaliente()"></i></td>
                        <td id="visceras">
                            <i data-bind="css: iVisceras()"></i>
                        </td>
                        <td id="acciones">
                            <button class="btn btn-danger" data-bind="click: Corregir, visible: ConConflicto()">
                                <i class="fa fa-wrench"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
                <%--<tfoot>
                    <tr>
                        <td colspan="5"></td>
                        <td> Totales</td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().noqueo:0"></td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().pSangre:0"></td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().pDescarnada:0"></td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().inspeccion:0"></td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().cCompleta:0"></td>
                        <td data-bind="text: Sacrificio().lenght >0? totalesIndicadores().cCaliente:0"></td>
                        <td data-bind="text: Sacrificio().lenght > 0 ? totalesIndicadores().visceras : 0"></td>
                    </tr>
                </tfoot>--%>
            </table>
        </div>
    </div>

        <div class="modal fade" id="planchado" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-bind="with: ePlanchado">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel" data-bind="text: Titulo"></h4>
                </div>
                <div class="modal-body" style="max-height: 600px; overflow-y: auto;">
                    <div class="row" style="position: relative; margin-top: .5em;">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="panel panel-default"> 
                                <div class="panel-heading"> 
                                    <h3 class="panel-title"> Aretes con Correspondencia </h3>
                                </div> 
                                    <table class="table table-condensed table-striped">
                                        <thead>
                                            <tr>
                                                <th>Sacrificado </th>
                                                <th>
                                                    <button type="button" class="btn btn-success" data-bind="click: PlanchadoAretesCorral">
                                                        <i class="fa fa-fast-forward"></i>
                                                    </button>
                                                </th>
                                                <th>No Sacrificado</th>
                                                <th>Corral</th>
                                            </tr>
                                        </thead>
                                        <tbody data-bind="foreach:RelacionAretes">
                                            <tr>
                                                <td data-bind="text:animalid" style="display:none;"></td>
                                                <td data-bind="text:arete"></td>
                                                <td>
                                                    <button type="button" class="btn btn-primary" data-bind="click: $root.PlanchadoAreteIndividual">
                                                        <i class="fa fa-play"></i>
                                                    </button>
                                                </td>
                                                <td data-bind="text:areteViejo"></td>
                                                <td data-bind="text:corral"></td>
                                            </tr>
                                        </tbody>  
                                    </table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="panel panel-warning"> 
                                <div class="panel-heading"> 
                                    <h3 class="panel-title"> Aretes Sin Correspondencia</h3>
                                </div> 
                                    <table class="table table-condensed table-striped">
                                        <thead>
                                            <tr>
                                                <th>Arete</th>
                                                <th>Reparar</th>
                                                <th></th>
                                                <th>Corral</th>
                                            </tr>
                                        </thead>
                                        <tbody data-bind="foreach:AretesSinRelacion">
                                            <tr>
                                                <td data-bind="text:Arete"></td>
                                                <td>
                                                    <button type="button" class="btn btn-primary" data-bind="click: $root.PlanchadoAreteActivoLote, enable: $root.RelacionAretes().length>0?false:true" >
                                                        <i class="fa fa-play"></i>
                                                    </button>
                                                </td>
                                                <td data-bind="text:Arete" style="visibility:hidden"></td>
                                                <td data-bind="text:Corral"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="listaSimple" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-bind="with: eListadoSimple">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="planchadoLabel" data-bind="text: pl_titulo"></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-10 col-lg-offset-1">
                            <div>
                                <table class="table table-condensed table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th><span>Arete:</span></th>
                                            <th><span>Corral:</span></th>
                                        </tr>
                                    </thead>
                                    <tbody data-bind="foreach: pl_arete">
                                        <tr>
                                            <td><input  type="radio" name="arete" data-bind="checkedValue: AnimalID,
                                                                                                checked: $parent.animalID_ls"/>
                                                <span data-bind="text: Arete"></span>
                                            </td>
                                            <td><span data-bind="text: Corral"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" data-bind="click: ListadoSimple_Save">Save changes</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="espera" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-position="1060">
        <div class="modal-dialog" role="document">
            <div class="modal-content" >
                <div class="modal-header">
                    <h4 class="modal-title">Un momento por favor...</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-10 col-lg-offset-1">
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center">
                        Recuperando información
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

</body>
    <script type="text/javascript" >
        var Etiquetas = {
            Completo: "<%= GetLocalResourceObject("lblCompleto").ToString() %>"
            , Incompleto: "<%= GetLocalResourceObject("lblIncompleto").ToString() %>"
            , Todos: "<%= GetLocalResourceObject("lblTodos").ToString() %>"
            , Conflictos: "<%= GetLocalResourceObject("lblConflictos").ToString() %>"
        };
    </script>
    <script src="<%= Ruta("~/Manejo/Scripts/json3.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/linq.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/jquery-2.1.4.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/knockout-3.3.0.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/bootstrap.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/toastr.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/moment.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/moment-with-locales.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/moment-datepicker.min.js") %>"></script>
    <script src="<%= Ruta("~/Manejo/Scripts/moment-datepicker-ko.js") %>"></script>
    <script src="<%= Ruta("~/Scripts/Base.js") %>"></script>
    <% #if !DEBUG %>
    <script src="<%= Ruta("~/Scripts/jquery.mockjson.js") %>"></script>   
    <script src="<%= Ruta("~/Scripts/ControlSacrificio.Mocks.js") %>"></script>
    <% #endif %>
    <script src="<%= Ruta("~/Scripts/ControlSacrificio.js") %>"></script>

</html>