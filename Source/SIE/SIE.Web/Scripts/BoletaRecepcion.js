//Variables Globales 
var msjAbierto = 0;
var indicadores = 0;
var folio = 0;
var nombreproveedores = '';
var TIPOFLETE = '';
var estatusglobal;
var indicadoresCalidad;
var indicadoresOrigen;
var indicadoresDestino;
var entradaProductoActual;
var CalidadOrigenProporsionada;
var GradoPorcentualHumedad;
var RegistroVigilanciaID;

//Funcionamiento controles
$(document).ready(function () {
    CalidadOrigenProporsionada = false;
    indicadoresCalidad = {};
    entradaProductoActual = {};
    GradoPorcentualHumedad = 1;
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true, "autoUnmask": true });
    $("#txtTicket").inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true, "autoUnmask": true });
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true, "autoUnmask": true });
    $("#txtFolioCalidadOrigen").inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true, "autoUnmask": true });

    ObtenerGradoPorcentualHumedad();
    ObtenerFoliosPendientes();

    $("form").keypress(function (e) {
        if (e.which == 13) {
            return false;
        }
    });

    for (var i = 0; i < 14; i++) {
        $("#tblIndicadoresDestino tbody").append("<tr>" +
                                        "<td class='span5'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "</tr>");
    }

    //KeyPress
    $("#txtFolioBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {

            e.preventDefault();
        }
    });

    $("#txtFolioCalidadOrigen").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtFechaCalidadOrigen").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });
    $("#txtFolio").keydown(function (e) {
        var code = e.keyCode || e.which;
        $("#divBtnCalidad").hide();
        if (code == 13) {
            LimpiarIndicadores();
            LimpiarDatosCalidad();
            ObtenerFolio();
            e.preventDefault();
        } else if (code == 9) {
            LimpiarIndicadores();
            LimpiarDatosCalidad();
            ObtenerFolio();
        }
    });

    $("#txtTicket").keydown(function (e) {
        var code = e.keyCode || e.which;
        $("#divBtnCalidad").hide();
        if (code == 13) {
            $("#txtFolio").val("");
            ObtenerTicket();
            e.preventDefault();
        } else if (code == 9) {
            $("#txtFolio").val("");
            ObtenerTicket();
        }
    });

    //Change
    $("#cmbContrato").change(function () {
        ObtenerIndicadoresContrato($("#cmbContrato").val());
    });

    //Click
    $("#btnGuardar").click(function () {
        GuardarCambios();
    });

    $("#btnBuscar").click(function () {
        ObtenerFoliosPendientes();
        var registros = $("#tbBusqueda tbody tr");
        if (!(registros.length > 0)) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
                bootbox.alert(window.msgSinFolios, function () {
                    $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });

    $("#btnAgregarBuscar").click(function () {
        $("#divBtnCalidad").hide();
        LimpiarDatosCalidad();
        var renglones = $("input[class=indicadores]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtFolio").val($(this).attr("folio"));
            });
            ObtenerFolio();
            $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
            ObtenerFoliosPendientes();
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
                bootbox.alert(window.msgSeleccionarFolio, function () {
                    $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });

    $("#btnCancelarBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
    });

    $("#btnDialogoSi").click(function () {
        location.href = location.href;
    });

    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
        $("#txtFolioBuscar").val("");
        ObtenerFoliosPendientes();
    });

    $("#btnNoBuscar").click(function () {
        $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
    });

    $("#btnCalidadOrigen").click(function () {
        $("#dlgCalidadOrigen").modal("show");
    });

    $("#btnAgregarCalidadOrigen").click(function () {
        ValidarIndicadoresCalidad();
    });

    $("#btnCerrarCalidadOrigen").click(function () {
        CerrarDialogoCalidad();
    });
});

ValidarNumeros = function (e) {
    var code = e.keyCode || e.which;
    if (code != 49 && code != 50 && code != 51 && code != 52 && code != 53
        && code != 54 && code != 55 && code != 56 && code != 57 && code != 48
        && code != 13 && code != 9 && code != 8) {
        e.preventDefault();
    }
};

//Limpia los indicadores                                     
LimpiarIndicadores = function () {
    $("#tblIndicadores tbody").html("");
    $("#tblIndicadoresDestino tbody").html("");
    $("#tblIndicadores tbody").html("");
    $("#txtFolioCalidadOrigen").val("");
    $("#txtFechaCalidadOrigen").val("");
    for (var i = 0; i < 14; i++) {
        $("#tblIndicadoresDestino tbody").append("<tr>" +
                                        "<td class='span5'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "</tr>");

    }
    for (i = 0; i < 14; i++) {
        $("#tblIndicadores tbody").append("<tr>" +
                                        "<td class='span5'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "</tr>");

    }
    $("#imgCalidadOk").hide();

};

//Validaciones y consultas
//Obtiene los folios que se encuentran pendientes de autorizar
ObtenerFoliosPendientes = function () {
    var datos = {};
    if ($("#txtFolioBuscar").val() != "") {
        datos = { "folio": $("#txtFolioBuscar").val() };
    } else {
        datos = { "folio": 0 };
    }
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerEntradaProductosPendiente",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;

                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='span1'><input type='checkbox' class='indicadores' id='entrada" + resultado[i].EntradaProductoId + "' folio='" + resultado[i].Folio + "' onclick='SeleccionaUno(\"#entrada" + resultado[i].EntradaProductoId + "\");'/></td>" +
                        "<td class='span1' style='text-align:right;'>" + resultado[i].Folio + "</td>" +
                        "<td class='span2' style='text-align:right;'>" + resultado[i].Contrato.Folio + "</td>" +
                        "<td class='span3'>" + resultado[i].Producto.ProductoDescripcion + "</td>" +
                        "<td class='span3'>" + resultado[i].RegistroVigilancia.ProveedorMateriasPrimas.Descripcion + "</td>" +
                        "<td class='span1'>" + window.msgEstatus + "</td>" +
                        "</tr>");
                }
            }
        }
    });
};

//Selecciona solo un checkbox
SeleccionaUno = function (Id) {
    var listaCheckBox = $(".indicadores");
    var checkbox = $(Id);
    if (checkbox.is(":checked")) {
        listaCheckBox.each(function () {
            this.checked = false;
        });
        checkbox.attr("checked", true);
    }
};

//Obtiene el folio tecleado
ObtenerFolio = function () {
    if ($("#txtFolio").val() != "" && (folio != $("#txtFolio").val() || folio == 0)) {
        App.bloquearContenedor($(".container-fluid"));
        var datos = { "folio": $("#txtFolio").val() };
        $.ajax({
            type: "POST",
            url: "BoletaRecepcion.aspx/ObtenerFolio",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request, ajaxOptions, thrownError) {
                var respuesta = $.parseJSON(request.responseText);
                App.desbloquearContenedor($(".container-fluid"));
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(respuesta.Message, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                App.desbloquearContenedor($(".container-fluid"));
                if (data.d) {

                    entradaProductoActual = data.d;

                    if (entradaProductoActual.Estatus.EstatusId != $("#txtAprobado").val() && entradaProductoActual.Estatus.EstatusId != $("#txtRechazado").val() && entradaProductoActual.Estatus.EstatusId != $("#txtAutorizado").val()) {
                        $("#txtTicket").val(entradaProductoActual.RegistroVigilancia.FolioTurno);
                        $("#cmbDestino").val(entradaProductoActual.TipoContrato.TipoContratoId);
                        $("#cmbProducto").append("<option value='" + entradaProductoActual.RegistroVigilancia.Producto.ProductoId + "' selected>" + entradaProductoActual.RegistroVigilancia.Producto.ProductoDescripcion + "</option>");
                        ObtenerContratos(entradaProductoActual.Contrato.ContratoId, entradaProductoActual.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID, entradaProductoActual.RegistroVigilancia.Producto.ProductoId, entradaProductoActual.RegistroVigilancia.ProveedorMateriasPrimas.RegistroVigilanciaId, 0);

                        if (entradaProductoActual.RegistroVigilancia.Contrato.TipoFlete.TipoFleteId != '1' && entradaProductoActual.RegistroVigilancia.Contrato.TipoFlete.TipoFleteId != 1) {
                            ObtenerListaChofer(entradaProductoActual.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProductoActual.RegistroVigilancia.ProveedorChofer.Chofer.ChoferID, entradaProductoActual.Contrato.TipoFlete.TipoFleteId, entradaProductoActual.RegistroVigilancia.Chofer);
                            ObtenerListaPlacas(entradaProductoActual.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProductoActual.RegistroVigilancia.Camion.CamionID, entradaProductoActual.Contrato.TipoFlete.TipoFleteId, entradaProductoActual.RegistroVigilancia.CamionCadena);
                        } else {
                            $("#cmbChofer").append("<option value='" + 0 + "' selected>" + entradaProductoActual.RegistroVigilancia.Chofer + "</option>");
                            $("#cmbPlacas").append("<option value='" + 0 + "' selected>" + entradaProductoActual.RegistroVigilancia.CamionCadena + "</option>");
                        }

                        nombreproveedores = entradaProductoActual.RegistroVigilancia.Transportista;
                        TIPOFLETE = entradaProductoActual.Contrato.TipoFlete.TipoFleteId;
                        estatusglobal = entradaProductoActual.Estatus.EstatusId;
                        $("#txtJustificacion").val(entradaProductoActual.Justificacion);
                        $("#txtAutorizadoPor").text("Autorizado por: " + entradaProductoActual.OperadorAutoriza.NombreCompleto);

                        if (entradaProductoActual.ProductoDetalle.length > 0) {
                            var detalle = entradaProductoActual.ProductoDetalle;
                            var renglones = $("#tblIndicadores tbody tr");
                            indicadores = 0;
                            for (var i = 0; i < detalle.length; i++) {
                                /*if (indicadores >= 13) {
                                    $("#tblIndicadores tbody").append("<tr>" +
                                                                    "<td class='span5'>&nbsp;</td>" +
                                                                    "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                                    "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                                    "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                                    "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                                    "</tr>");
                                }*/
                                /* renglones[i].id = detalle[i].Indicador.IndicadorId;
                                 renglones[i].cells[0].innerHTML = detalle[i].Indicador.Descripcion;
                                 renglones[i].cells[3].innerHTML = detalle[i].ProductoMuestras[detalle[i].ProductoMuestras.length - 1].Porcentaje;
                                 */
                                var indicador = [];
                                for (var j = 0; j < entradaProductoActual.Contrato.ListaContratoDetalleInfo.length; j++) {
                                    if (detalle[i].Indicador.IndicadorId == entradaProductoActual.Contrato.ListaContratoDetalleInfo[j].Indicador.IndicadorId) {
                                        indicador = entradaProductoActual.Contrato.ListaContratoDetalleInfo[j];
                                        j = entradaProductoActual.Contrato.ListaContratoDetalleInfo.length;
                                    }
                                }


                                /*if (!(parseFloat(detalle[i].ProductoMuestras[detalle[i].ProductoMuestras.length - 1].Porcentaje) > parseFloat(indicador.PorcentajePermitido))) {
                                    renglones[i].cells[4].innerHTML = detalle[i].ProductoMuestras[detalle[i].ProductoMuestras.length - 1].Descuento;
                                } else {
                                    renglones[i].cells[4].innerHTML = "<input maxlength='5' type='tel' class='descuentos mascara span12' entradaProductoDetalleId='" + detalle[i].EntradaProductoDetalleId + "' id='indicadorDescuento" + detalle[i].Indicador.IndicadorId + "'>";
                                    $('#tblIndicadores tbody tr:eq(' + i + ') td').css("background-color", "rgb(248, 141, 141)");
                                }*/
                                indicadores++;
                            }
                        }
                        $("#cmbContrato").prop("disabled", true);
                        $("#cmbDestino").prop("disabled", true);
                        $("#dvJustificacion").css("display", "block");
                        ObtenerIndicadoresCalidad(entradaProductoActual.Producto.ProductoId);
                        AsignarMascaraFunciones();
                        if (entradaProductoActual.Estatus.EstatusId == $("#txtPendienteAutorizar").val()) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioPendientePorAprobar, function () {
                                    msjAbierto = 0;
                                    $("#btnGuardar").hide();
                                });
                            }
                        } else {
                            $("#btnGuardar").show();

                            if (entradaProductoActual.Contrato.CalidadOrigen == 1) {
                                $("#divBtnCalidad").show();
                            }
                        }

                    } else {
                        if (entradaProductoActual.Estatus.EstatusId == $("#txtAprobado").val()) {
                            $("#cmbContrato").html("");
                            $("#cmbProducto").html("");
                            $("#cmbChofer").html("");
                            $("#cmbPlacas").html("");
                            $("#cmbProveedor").html("");
                            LimpiarIndicadores();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioAprobado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (entradaProductoActual.Estatus.EstatusId == $("#txtRechazado").val()) {
                            $("#cmbContrato").html("");
                            $("#cmbProducto").html("");
                            $("#cmbChofer").html("");
                            $("#cmbPlacas").html("");
                            $("#cmbProveedor").html("");
                            LimpiarIndicadores();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioRechazado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (entradaProductoActual.Estatus.EstatusId == $("#txtAutorizado").val()) {
                            $("#txtJustificacion").val(entradaProductoActual.Justificacion);
                            $("#txtAutorizadoPor").text("Autorizado por: " + entradaProductoActual.OperadorAutoriza.NombreCompleto);
                            $("#cmbContrato").html("");
                            $("#cmbProducto").html("");
                            $("#cmbChofer").html("");
                            $("#cmbPlacas").html("");
                            $("#cmbProveedor").html("");
                            LimpiarIndicadores();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioAutorizado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                    }
                } else {
                    $("#cmbContrato").html("");
                    $("#cmbProducto").html("");
                    $("#cmbChofer").html("");
                    $("#cmbProveedor").html("");
                    $("#cmbPlacas").html("");
                    LimpiarIndicadores();
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgFolioInvalido, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        });
    }
};



//Obteniene el ticket
ObtenerTicket = function () {
    LimpiarIndicadores();
    $("#btnGuardar").show();
    if ($("#txtTicket").val() != "") {
        App.bloquearContenedor($(".container-fluid"));
        var datos = { "ticket": $("#txtTicket").val() };
        $.ajax({
            type: "POST",
            url: "BoletaRecepcion.aspx/ObtenerTicket",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                App.desbloquearContenedor($(".container-fluid"));
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                $("#dvJustificacion").css("display", "none");
                App.desbloquearContenedor($(".container-fluid"));
                if (data.d) {
                    var datos = data.d;
                    var fechaSalida = FormtatoFecha(datos.FechaSalida);
                    
                    if (datos.FechaSalida == null || fechaSalida == "1/1/1900") {
                        HabilitarDeshabilitarCampos(false);
                        if (datos.RegistroVigilanciaId > 0) {
                            RegistroVigilanciaID = datos.RegistroVigilanciaId;
                            $("#cmbProducto").append("<option value='" + datos.Producto.ProductoId + "' selected>" + datos.Producto.ProductoDescripcion + "</option>");
                            ObtenerContratos(datos.Contrato.ContratoId, datos.ProveedorMateriasPrimas.ProveedorID, datos.Producto.ProductoId, datos.RegistroVigilanciaId, 1);

                            if (datos.Contrato.TipoFlete.TipoFleteId != '1' && datos.Contrato.TipoFlete.TipoFleteId != 1) {
                                ObtenerListaChofer(datos.ProveedorChofer.Proveedor.ProveedorID, datos.ProveedorChofer.Chofer.ChoferID, datos.Contrato.TipoFlete.TipoFleteId, datos.Chofer);
                                ObtenerListaPlacas(datos.ProveedorChofer.Proveedor.ProveedorID, datos.Camion.CamionID, datos.Contrato.TipoFlete.TipoFleteId, datos.CamionCadena);
                            } else {
                                $("#cmbChofer").append("<option value='" + 0 + "' selected>" + datos.Chofer + "</option>");
                                $("#cmbPlacas").append("<option value='" + 0 + "' selected>" + datos.CamionCadena + "</option>");
                            }
                            nombreproveedores = datos.Transportista;
                            TIPOFLETE = datos.Contrato.TipoFlete.TipoFleteId;
                            entradaProductoActual.Contrato = datos.Contrato;
                            if (entradaProductoActual.Contrato.CalidadOrigen == 1) {
                                $("#divBtnCalidad").show();
                            }

                            $("#dvJustificacion").css("display", "none");
                            $("#cmbContrato").prop("disabled", false);
                            $("#cmbDestino").prop("disabled", false);
                        } else {
                            $("#cmbContrato").html("");
                            $("#cmbProducto").html("");
                            $("#cmbChofer").html("");
                            $("#cmbProveedor").html("");
                            $("#cmbPlacas").html("");
                            LimpiarIndicadores();
                            $("#divBtnCalidad").hide();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgTicketNoEncuentra, function () {
                                    msjAbierto = 0;
                                    $("#txtTicket").focus();
                                });
                            }
                        }
                    }
                    else {
                        HabilitarDeshabilitarCampos(false);
                        $("#cmbContrato").html("");
                        $("#cmbContrato").text("");
                        $("#cmbProducto").html("");
                        $("#cmbChofer").html("");
                        $("#cmbProveedor").html("");
                        $("#cmbPlacas").html("");
                        $("#divBtnCalidad").hide();
                        $("#cmbProducto").text("");
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(window.msjValidacionFechaSalida, function () {
                                msjAbierto = 0;
                                $("#txtTicket").focus();
                            });
                        }
                    }
                } else {
                    $("#cmbContrato").html("");
                    $("#cmbProducto").html("");
                    $("#cmbChofer").html("");
                    $("#cmbProveedor").html("");
                    $("#cmbPlacas").html("");
                    $("#divBtnCalidad").hide();
                    LimpiarIndicadores();
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgTicketNoEncuentra, function () {
                            msjAbierto = 0;
                            $("#txtTicket").focus();
                        });
                    }
                }
            }
        });
    }
};


//Obtiene los proveedores
ObtenerProveedores = function (ProveedorID) {
    App.bloquearContenedor($(".container-fluid"));
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerProveedores",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var datos = data.d;
                $("#cmbProveedor").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ProveedorID == datos[i].ProveedorID) {
                        if (TIPOFLETE == '1') {
                            $("#cmbProveedor").append("<option value='" + 0 + "'selected>" + nombreproveedores + "</option>");
                        } else {
                            $("#cmbProveedor").append("<option value='" + datos[i].ProveedorID + "'selected>" + datos[i].Descripcion + "</option>");
                        }
                    } else {
                        if (TIPOFLETE == '1') {
                            $("#cmbProveedor").append("<option value='" + 0 + "'selected>" + nombreproveedores + "</option>");
                        } else {
                            $("#cmbProveedor").append("<option value='" + datos[i].ProveedorID + "'>" + datos[i].Descripcion + "</option>");
                        }
                    }
                }
            } else {
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinProveedores, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};


//Obtiene los productos del proveedor
ObtenerProductosProveedor = function (ProveedorID, ProductoID) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerProductosProveedor",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var datos = data.d;
                $("#cmbProducto").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ProductoID == datos[i].ProductoId) {
                        $("#cmbProducto").append("<option value='" + datos[i].ProductoId + "' selected>" + datos[i].ProductoDescripcion + "</option>");
                    } else {
                        $("#cmbProducto").append("<option value='" + datos[i].ProductoId + "'>" + datos[i].ProductoDescripcion + "</option>");
                    }
                }
            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinProducto, function () {
                        msjAbierto = 0;
                    });
                }
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
            }
        }
    });
};

//Obtiene los choferes del proveedor
ObtenerContratos = function (ContratoID, ProveedorID, ProductoID, RegistroVigilanciaID, Indicadores) {
    App.bloquearContenedor($(".container-fluid"));
    //console.log(ContratoID);
    var datos = { "proveedorId": ProveedorID, "productoId": ProductoID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerProveedorContratos",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));

            if (data.d) {
                var datos = data.d;

                $("#cmbContrato").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ContratoID == datos[i].ContratoId) {
                        $("#cmbContrato").append("<option value='" + datos[i].ContratoId + "' selected>" + datos[i].Folio + "</option>");
                    }
                }

                if (datos.length > 0) {
                    ObtenerProveedores(ProveedorID);
                    if (Indicadores) {
                        ObtenerIndicadoresContrato(ContratoID);
                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgProveedorSinContrato, function () {
                            msjAbierto = 0;
                        });
                    }
                    $("#cmbContrato").html("");
                    $("#cmbProducto").html("");
                    $("#cmbChofer").html("");
                    $("#cmbPlacas").html("");
                    $("#cmbProveedor").html("");
                }
            } else {
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinContrato, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

//Obtiene los indicadores de un contrato
ObtenerIndicadoresContrato = function (ContratoID) {

    LimpiarIndicadores();
    var fechainiciohumedad;
    var fechaactual;
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "contratoId": ContratoID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerIndicadoresContrato",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));

            if (data.d) {
                var datos = data.d;
                if (datos.ListaContratoDetalleInfo != null) {
                    var renglones = $("#tblIndicadoresDestino tbody tr");
                    entradaProductoActual.Contrato = datos;
                    indicadores = datos.ListaContratoDetalleInfo.length;
                    ObtenerIndicadoresCalidad(datos.Producto.ProductoId);
                    /* for (var i = 0; i < datos.ListaContratoDetalleInfo.length; i++) {
                         if (indicadores >= 13) {
                             $("#tblIndicadoresDestino tbody").append("<tr>" +
                                                             "<td class='span5'>&nbsp;</td>" +
                                                             "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                             "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                             "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                             "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                             "</tr>");
                         }
                         renglones[i].id = indicadores;
                         renglones[i].cells[0].innerHTML = datos.ListaContratoDetalleInfo[i].Indicador.Descripcion;
 
                         //validar fecha de inicio contra la fecha actual
                         if (datos.ListaContratoDetalleInfo[i].Indicador.IndicadorId == $("#hdIditificadorHumedad").val()) {
                             fechaactual = $("#txtFecha").val();
                             fechainiciohumedad = datos.ListaContratoDetalleInfo[i].FechaInicio;
                             if (process(fechaactual) >= process(fechainiciohumedad)) {
                                 renglones[i].cells[1].innerHTML = datos.ListaContratoDetalleInfo[i].PorcentajeHumedad;
                             } else {
                                 renglones[i].cells[1].innerHTML = datos.ListaContratoDetalleInfo[i].PorcentajePermitido;
                             }
                         } else {
                             renglones[i].cells[1].innerHTML = datos.ListaContratoDetalleInfo[i].PorcentajePermitido;
                         }
 
                         if (datos.ListaContratoDetalleInfo[i].Indicador.IndicadorId == $("#hdIditificadorHumedad").val()) {
                             renglones[i].cells[2].innerHTML = datos.ListaContratoDetalleInfo[i].PorcentajeHumedad;
                         } else {
                             renglones[i].cells[2].innerHTML = 0;
                         }
                         renglones[i].cells[3].innerHTML = "<input maxlength='5' type='tel' class='mascara span12' renglon='"+indicadores+"' indicador='" + datos.ListaContratoDetalleInfo[i].Indicador.IndicadorId + "' maximo='" + parseFloat(datos.ListaContratoDetalleInfo[i].PorcentajePermitido) + "' id='indicador" + indicadores + "'>";
                         renglones[i].cells[4].innerHTML = "<input maxlength='5' type='tel' class='mascara span12' id='indicadorDescuento" + indicadores + "' disabled>";
                         
                         indicadores++;
                     }*/
                    AsignarMascaraFunciones();
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgContratoSinDetalle, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            } else {
                $("#cmbContrato").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgContratoSinDetalle, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

function process(date) {
    var parts = date.split("/");
    var date = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
    return date.getTime();
}

//Asigna las mascaras y funciones
AsignarMascaraFunciones = function () {
    $(".mascara").css("text-align", "right");

    $(".mascara").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $(".mascara").focusout(function () {
        var lista = $(".mascara");
        lista.each(function () {
            if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
                $(this).val(accounting.formatNumber($(this).val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
            } else {
                $(this).val("");
            }
        });
    });

    $(".mascara").change(function () {
        var lista = $(".mascara");
        lista.each(function () {
            if ($(this).val() > 100) {
                $(this).val("");
                $(this).focus();
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgMayorPorcentaje, function () {
                        msjAbierto = 0;
                    });
                }
            } else {
                if ($(this).attr("indicador")) {
                    if (parseFloat($(this).attr("maximo")) < parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) && $(this).val() != "") {
                        $("#indicadorDescuento" + $(this).attr("renglon")).prop("disabled", false);
                    } else {
                        $("#indicadorDescuento" + $(this).attr("renglon")).prop("disabled", true);
                        $("#indicadorDescuento" + $(this).attr("renglon")).val("");
                    }
                }
            }
        });
    });


};


//Obtiene los choferes del proveedor
ObtenerListaChofer = function (ProveedorID, ChoferID, tipoflete, nombrechofer) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerProveedorChofer",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var datos = data.d;
                $("#cmbChofer").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ChoferID == datos[i].Chofer.ChoferID) {
                        if (tipoflete == '1') {
                            $("#cmbChofer").append("<option value='" + 0 + "' selected>" + nombrechofer + "</option>");
                        } else {
                            $("#cmbChofer").append("<option value='" + datos[i].Chofer.ChoferID + "' selected>" + datos[i].Chofer.NombreCompleto + "</option>");
                        }
                    } else {
                        if (tipoflete == '1') {
                            $("#cmbChofer").append("<option value='" + 0 + "' selected>" + nombrechofer + "</option>");
                        } else {
                            $("#cmbChofer").append("<option value='" + datos[i].Chofer.ChoferID + "'>" + datos[i].Chofer.NombreCompleto + "</option>");
                        }
                    }
                }
            } else {
                $("#cmbChofer").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinChofer, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

//Obtiene las placas del proveedor
ObtenerListaPlacas = function (ProveedorID, PlacaCamion, TipoFlete, placas) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerProveedorPlacas",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var datos = data.d;
                $("#cmbPlacas").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (PlacaCamion == datos[i].CamionID) {
                        if (TipoFlete == '1') {
                            $("#cmbPlacas").append("<option value='" + 0 + "' selected>" + placas + "</option>");
                        } else {
                            $("#cmbPlacas").append("<option value='" + datos[i].CamionID + "' selected>" + datos[i].PlacaCamion + "</option>");
                        }
                    } else {
                        if (TipoFlete == '1') {
                            $("#cmbPlacas").append("<option value='" + 0 + "' selected>" + placas + "</option>");
                        } else {
                            $("#cmbPlacas").append("<option value='" + datos[i].CamionID + "'>" + datos[i].PlacaCamion + "</option>");
                        }
                    }
                }
            } else {
                $("#cmbPlacas").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinPlacas, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};


//Guarda los cambios
GuardarCambios = function () {

    if (indicadoresCalidad.length > 0 || $("#txtFolio").val() != "") {
        if ($("#txtTicket").val() != "" && $("#cmbProducto").val() > 0 && $("#cmbDestino").val() > 0 && $("#cmbContrato").val() > 0) {

            if (ValidarCalidadDestino() == false) {
                bootbox.alert(window.msgFaltanaIndicadorCalidadDestino, function () {
                    msjAbierto = 0;
                });
                return false;
            }

            if (entradaProductoActual.Contrato.CalidadOrigen == 1) {
                if (CalidadOrigenProporsionada == false) {
                    bootbox.alert(window.msgFaltanaIndicadorCalidadOrigen, function () {
                        msjAbierto = 0;
                    });
                    return false;
                }
            }
            var renglones = $("#tblIndicadoresDestino tbody tr");
            var listaIndicadores = [];
            var listaMuestrasOrigen = [];
            var listaMuestrasDestino = [];
            var indicador = {};
            var muestras = {};
            var PorcentajePermitido = 0;
            var PorcentajeHumedad = 0;
            var contador = 0;
            var contador2 = 0;
            var bandera = 1;//1.- Aprobada:2.- Pendiente por Autorizar;3.- Autorizado:
            var contadordescuentos = 0;
            var tmpIdIndicadorActual = 0;
            if ($("#txtFolio").val() != "" && indicadores > 0) {
                GuardarCambiosIndicadores();
                return false;
            }

            for (var i = 0; i < indicadoresCalidad.length; i++) {
                listaMuestrasOrigen = [];
                listaMuestrasDestino = [];
                indicador = {};
                muestras = {};
                tmpIdIndicadorActual = indicadoresCalidad[i].IndicadorInfo.IndicadorId;

                var descuento = 0;
                if ($("#txtFolio").val() == "") {
                    if ($("#descuentoCalidad_" + tmpIdIndicadorActual).val() != "") {
                        descuento = parseFloat($("#descuentoCalidad_" + tmpIdIndicadorActual).val());
                    }
                }

                var calidad = 0;
                if ($("#porcentajeCalidad_" + tmpIdIndicadorActual).val() != "") {
                    calidad = parseFloat($("#porcentajeCalidad_" + tmpIdIndicadorActual).val());
                }

                if (entradaProductoActual.Contrato.CalidadOrigen == 1) {
                    var indicadorOrigen = "#indicadorProducto_" + tmpIdIndicadorActual;
                    muestras = {
                        "Porcentaje": parseFloat($(indicadorOrigen).val()),
                        "Descuento": 0,
                        "EsOrigen": 1
                    };

                    listaMuestrasOrigen.push(muestras);
                    indicador = { "Indicador": indicadoresCalidad[i].IndicadorInfo, "ProductoMuestras": listaMuestrasOrigen };
                    listaIndicadores.push(indicador);
                }

                muestras = { "Porcentaje": calidad, "Descuento": descuento, "EsOrigen": 0 };
                listaMuestrasDestino.push(muestras);
                indicador = { "Indicador": indicadoresCalidad[i].IndicadorInfo, "ProductoMuestras": listaMuestrasDestino };
                listaIndicadores.push(indicador);

                console.log(listaIndicadores);
            }

            if (contadordescuentos > 0) {
                bootbox.alert(window.msjFaltanDescuentosCapturarI + contadordescuentos + window.msjFaltanDescuentosCapturarII, function () {
                    msjAbierto = 0;
                });
                return false;
            }

            if (indicadoresCalidad.length > 0 || $("#txtFolio").val() > 0) {
                var entradaProductoEnviar = {};

                if ($("#txtFolio").val() > 0) {
                    if ($("#txtJustificacion").val() == "") {
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(window.msgDatosBlanco, function () {
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    }
                    entradaProductoEnviar = {
                        "Folio": $("#txtFolio").val(),
                        "Contrato": { "ContratoId": $("#cmbContrato").val() },
                        "TipoContrato": { "TipoContratoId": $("#cmbDestino").val() },
                        "Producto": { "ProductoId": $("#cmbProducto").val() },
                        "ProductoDetalle": listaIndicadores,
                        "RegistroVigilancia": { "FolioTurno": $("#txtTicket").val() },
                        "Justificacion": $("#txtJustificacion").val(),
                    };
                } else {
                    entradaProductoEnviar = {
                        "Contrato": { "ContratoId": $("#cmbContrato").val() },
                        "TipoContrato": { "TipoContratoId": $("#cmbDestino").val() },
                        "Producto": { "ProductoId": $("#cmbProducto").val() },
                        "ProductoDetalle": listaIndicadores,
                        "RegistroVigilancia": { "FolioTurno": $("#txtTicket").val() },
                        "EntradaProductoId": "0",
                    };
                }

                var fechaTemp = new Date();
                var folioOrigen = "0";
                var parts = [];
                if ($("#txtFechaCalidadOrigen").val() != "") {
                    parts = $("#txtFechaCalidadOrigen").val().split('/');
                    folioOrigen = $("#txtFolioCalidadOrigen").val();

                } else {
                    //console.log("aqui---------------------");
                    var tmpFecha = "01/01/1900";
                    parts = tmpFecha.split('/');
                }

                fechaTemp = new Date(parts[2], parts[1] - 1, parts[0]);
                entradaProductoEnviar.FechaEmbarque = fechaTemp;
                entradaProductoEnviar.FolioOrigen = folioOrigen;
                entradaProductoEnviar.EsOrigen = entradaProductoActual.Contrato.CalidadOrigen;


                App.bloquearContenedor($(".container-fluid"));
                var datos = { "entradaProducto": entradaProductoEnviar, "Bandera": bandera };
                $.ajax({
                    type: "POST",
                    url: "BoletaRecepcion.aspx/GuardarEntradaProducto",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(datos),
                    error: function (request) {
                        App.desbloquearContenedor($(".container-fluid"));
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(request.responseText, function () {
                                msjAbierto = 0;
                            });
                        }
                    },
                    dataType: "json",
                    success: function (data) {
                        App.desbloquearContenedor($(".container-fluid"));
                        if (data.d != null) {
                            var datos = data.d;
                            if (datos.Resultado == false) {
                                bootbox.alert(datos.Mensaje, function () {
                                    msjAbierto = 0;
                                });
                            }
                            else {
                                if (datos.Control.Estatus.EstatusId == 27) { //Pendiente de autorizar
                                    if (msjAbierto == 0) {
                                        msjAbierto = 1;
                                        bootbox.alert('<table>' +
                                            '<tr>' +
                                            '<td>' +
                                            '<img class="span1 no-left-margin" src="../Images/Correct.png"></img>' +
                                            '</td>' +
                                            '<td>' +
                                            '<label class="span4" style="word-wrap:true;">Folio: ' + datos.Control.Folio + ". " + window.msgSolicitaAutorizacion + '</label>' +
                                            '</td>' +
                                            '</tr>' +
                                            '</table>',
                                            function () {
                                                location.href = location.href;
                                                msjAbierto = 0;
                                            });

                                    }
                                } else if (datos.Control.Estatus.EstatusId == 24 || datos.Control.Estatus.EstatusId == 26) {
                                    if (msjAbierto == 0) {
                                        msjAbierto = 1;
                                        bootbox.alert("<img src='../Images/Correct.png'/>Folio: " + datos.Control.Folio + ". " + window.msgDatosGuardados, function () {
                                            location.href = location.href;
                                            msjAbierto = 0;
                                        });
                                    }
                                } else if (datos.Control.Estatus.EstatusId == 25) {
                                    if (msjAbierto == 0) {
                                        msjAbierto = 1;
                                        bootbox.alert("<img src='../Images/Correct.png'/>Folio: " + datos.Control.Folio + ". " + window.msgDatosGuardadosRechazado, function () {
                                            location.href = location.href;
                                            msjAbierto = 0;
                                        });
                                    }
                                } else {
                                    if (msjAbierto == 0) {
                                        msjAbierto = 1;
                                        bootbox.alert(window.msgErrorGuardar, function () {
                                            msjAbierto = 0;
                                        });
                                    }
                                }
                            }
                        } else {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgErrorGuardar, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                    }
                });


            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgDatosBlanco, function () {
                        msjAbierto = 0;
                    });
                }
            }
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgDatosBlanco, function () {
                    msjAbierto = 0;
                });
            }
        }
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgDatosBlanco, function () {
                msjAbierto = 0;
            });
        }
    }
};


GuardarCambiosIndicadores = function () {
    var descuentos = $(".descuentos");
    var listaMuestras = [];
    var indicadoresCambios = {};
    var muestrasOrigen = {};
    var muestrasDestino = {};

    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var productoDetalleId = "#productoDetalleId_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;
        var controlDescuento = "#descuentoCalidad_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;
        var indicadorOrigen = "#indicadorProducto_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;
        var descuento = 0;

        if ($(controlDescuento).val() != "") {
            descuento = $(controlDescuento).val();
        }

        if (entradaProductoActual.Contrato.CalidadOrigen == 1) {

            muestras = {
                "EntradaProductoDetalleId": $(indicadorOrigen).val(),
                "Descuento": descuento,
                "EsOrigen": 1
            };

            listaMuestras.push(muestras);
        }

        muestras = {
            "EntradaProductoDetalleId": $(productoDetalleId).val(),
            "Descuento": descuento,
            "EsOrigen": 0
        };

        listaMuestras.push(muestras);

    }
    /*descuentos.each(function () {

        if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
            muestras = {
                "EntradaProductoDetalleId": $(this).attr("entradaproductodetalleid"),
                "Descuento": parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')),
                "EsOrigen" : entradaProductoActual.Contrato.CalidadOrigen
            };
            listaMuestras.push(muestras);
            console.log($(this));
        } else {
            listaMuestras = [];
        }
    });*/

    if (listaMuestras.length > 0) {
        indicadoresCambios = { "ProductoMuestras": listaMuestras };

        var datos = { "indicadores": indicadoresCambios };
        App.bloquearContenedor($(".container-fluid"));
        $.ajax({
            type: "POST",
            url: "BoletaRecepcion.aspx/GuardarActualizacionProductos",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                App.desbloquearContenedor($(".container-fluid"));
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                App.desbloquearContenedor($(".container-fluid"));
                if (data.d) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert("<img src='../Images/Correct.png'/>" + window.msgDatosGuardados, function () {
                            location.href = location.href;
                            msjAbierto = 0;
                        });
                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgErrorGuardar, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        });
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgDatosBlanco, function () {
                msjAbierto = 0;
            });
        }
    }
};

ObtenerIndicadoresCalidad = function (productoID) {

    App.bloquearContenedor($(".container-fluid"));

    datos = { "IdProducto": productoID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerIndicadorProducto",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        data: JSON.stringify(datos),
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var datos = data.d;
                if (datos.Resultado) {
                    var resultado = datos.Control;
                    indicadoresCalidad = resultado;

                    $("#tblIndicadores tbody").html("");
                    $("#tblIndicadoresDestino tbody").html("");
                    for (var i = 0; i < resultado.length; i++) {

                        $("#tblIndicadores tbody").append("<tr>" +
                            "<td class='span4' style='text-align:left;'>" + resultado[i].IndicadorInfo.Descripcion + "</td>" +
                            "<td style='display: none;'><input class='descuentos mascara span12' id='productoDetalleIdOrigen_" + resultado[i].IndicadorInfo.IndicadorId + "' value='" + entradaProductoDetallaId + "'></input></td>" +
                            "<td class='span4' style='text-align:right;'><input maxlength='5' style='-moz-appearance: textfield' type='number' min='0' max='100' class='descuentos mascara span12' indicadorProductoId='" + resultado[i].IndicadorInfo.IndicadorId + "' id='indicadorProducto_" + resultado[i].IndicadorInfo.IndicadorId + "' oninput='ValidarPorcentaje(this)'></td>" +
                            "<td style='display: none;'><input class='mascara span12' entradaProductoDetalleId='" + entradaProductoDetallaId + "' descuentoCalidadDestino='" + resultado[i].IndicadorInfo.IndicadorId + "' id='descuentoCalidadOrigen_" + resultado[i].IndicadorInfo.IndicadorId + "' disabled='disabled'></td>" +
                            "<td style='display: none;'><input type='tel' id='indicadorOrigenMin_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].Minimo + "'></td>" +
                            "<td style='display: none;'><input type='tel' id='indicadorOrigenMax_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].Maximo + "'></td>" +
                            "<td style='display: none;'><input type='tel' id='indicadorOrigen_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].IndicadorInfo.IndicadorId + "'></td>" +

                            "</tr>");

                        var detalle = entradaProductoActual.ProductoDetalle;

                        var entradaProductoDetallaId = 0;
                        if (detalle != null) {
                            for (var index = 0; index < detalle.length; index++) {
                                if (detalle[index].Indicador.IndicadorId == resultado[i].IndicadorInfo.IndicadorId) {
                                    entradaProductoDetallaId = detalle[index].EntradaProductoDetalleId;
                                }
                            }
                        }

                        $("#tblIndicadoresDestino tbody").append("<tr>" +
                          "<td class='span4' style='text-align:left;'>" + resultado[i].IndicadorInfo.Descripcion + "</td>" +
                          "<td style='display: none;'><input class='descuentos mascara span12' id='productoDetalleId_" + resultado[i].IndicadorInfo.IndicadorId + "' value='" + entradaProductoDetallaId + "'></input></td>" +
                          "<td class='span4' style='text-align:right;'><input maxlength='5' style='-moz-appearance: textfield' type='number' step='any' min='0' max='100' class='mascara soloNumeros span12' indicador='" + resultado[i].IndicadorInfo.IndicadorId + "' id='porcentajeCalidad_" + resultado[i].IndicadorInfo.IndicadorId + "' oninput='CalcularPorcentaje(this)' ></td>" +
                          "<td class='span4' style='text-align:right;'><input class='mascara span12' entradaProductoDetalleId='" + entradaProductoDetallaId + "' descuentoCalidadDestino='" + resultado[i].IndicadorInfo.IndicadorId + "' id='descuentoCalidad_" + resultado[i].IndicadorInfo.IndicadorId + "' disabled='disabled'></td>" +
                          "<td style='display: none;'><input type='tel' id='indicadorMin_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].Minimo + "'></td>" +
                          "<td style='display: none;'><input type='tel' id='indicadorMax_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].Maximo + "'></td>" +
                          "<td style='display: none;'><input type='tel' id='indicador_" + resultado[i].IndicadorInfo.IndicadorId + "'  value='" + resultado[i].IndicadorInfo.IndicadorId + "'></td>" +
                          "</tr>");

                    }

                    if ($('#hfAplicaHumedad').val() == '1') {
                        ObtenerCapturaHumedad();
                    }

                    /*var renglones = $("#tblIndicadoresDestino tbody tr");
                    for ( i = 0; i < renglones.length; i++) {
                        var control = renglones[i].cells[2].getElementsByTagName("input")[0].id;
                        $("#" + control).inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true });
                    }*/
                }
            } else {

                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinProveedores, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

ObtenerCapturaHumedad = function () {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "registroVigilanciaID": RegistroVigilanciaID };
    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerHumedad",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            var indicador = $('#porcentajeCalidad_' + $('#hdIditificadorHumedad').val() + '');
            if (data.d.Resultado == false) {
                
                if (msjAbierto == 0) {
                    msjAbierto = 1;

                    switch (data.d.CodigoMensaje) {
                        case 1:
                            $(indicador).attr('disabled', true);
                            bootbox.alert(window.msgConfiguracionProductos, function () {
                                msjAbierto = 0;
                            });
                        case 2:
                            $(indicador).attr('disabled', true);
                            bootbox.alert(window.msgSinHumedadCapturada, function () {
                                msjAbierto = 0;
                            });
                        default:
                            return;
                    }

                   
                }
            }
            else {
                var registroVigilanciaHumedad = data.d.Control;
                
                $(indicador).val(registroVigilanciaHumedad.Humedad);
                $(indicador).attr('disabled', true);
                CalcularDescuentos();
            }
        }
    });
};

ValidarIndicadoresCalidad = function () {
    CalidadOrigenProporsionada = false;
    if ($("#txtFolioCalidadOrigen").val() == "") {
        EnviarMensajeConFoco(window.msgFaltaFolioOrigen, $("#txtFolioCalidadOrigen"), true, $("#dlgCalidadOrigen"));
        return false;
    }

    if ($("#txtFechaCalidadOrigen").val() == "") {
        EnviarMensajeConFoco(window.msgFaltaFechaOrigen, $("#txtFechaCalidadOrigen"), true, $("#dlgCalidadOrigen"));
        return false;
    }

    if (ValidarFormatoFecha($("#txtFechaCalidadOrigen").val()) == false) {
        EnviarMensajeConFoco(window.msgFechaIncorrecta, $("#txtFechaCalidadOrigen"), true, $("#dlgCalidadOrigen"));
        return false;
    }

    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var control = "#indicadorProducto_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;
        if ($(control).val() == "") {
            EnviarMensajeConFoco(window.msgFaltaPorcentajeCalidad + " " + indicadoresCalidad[i].IndicadorInfo.Descripcion, $(control), true, $("#dlgCalidadOrigen"));
            return false;
        }
    }

    $("#imgCalidadOk").show();
    CalidadOrigenProporsionada = true;
    CalcularDescuentos();
};

CerrarDialogoCalidad = function () {

    if ($("#txtFolioCalidadOrigen").val() != "") {
        PreguntaCancelar();
        return false;
    }

    if ($("#txtFechaCalidadOrigen").val() != "") {
        PreguntaCancelar();
        return false;
    }
    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var control = "#indicadorProducto_" + indicadoresCalidad[i].IndicadorInfo.indicadorId;

        if ($(control).val() != "") {
            PreguntaCancelar();
            return false;
        }
    }
};

EnviarMensajeConFoco = function (mensaje, control, regresar, dialogo) {

    bootbox.alert(mensaje, function () {
        msjAbierto = 0;
        if (regresar) {
            dialogo.modal("show");
            control.focus();
        }
    });

};

PreguntaCancelar = function () {
    bootbox.dialog({
        message: window.msgCancelar,
        buttons: {
            Aceptar: {
                label: window.msgDialogoSi,
                callback: function () {
                    LimpiarDatosCalidad();
                    return true;
                }
            },
            Cancelar: {
                label: window.msgDialogoNo,
                callback: function () {
                    $("#dlgCalidadOrigen").modal("show");
                    return true;
                }
            }
        }
    });
};

LimpiarDatosCalidad = function () {
    $("#txtFolioCalidadOrigen").val("");

    $("#txtFechaCalidadOrigen").val("");

    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var control = "#indicadorProducto_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;

        $(control).val("");
        CambiarColores("tblIndicadores", 100, indicadoresCalidad[i].IndicadorInfo.IndicadorId);

    }
    $("#imgCalidadOk").hide();
};

CalcularDescuentos = function () {

    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var control = "#indicadorProducto_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;
        CalcularPorcentaje($(control));

    }
};

CalcularPorcentaje = function (control) {

    var idControl = control.id;
    if (idControl == null) {
        idControl = control.attr("id");
    }
    var arregloId = idControl.split("_");
    var identificador = arregloId[1];
    var porcentajeBoleta = 0;
    var porcentajePermitido = 0;
    var contrato = entradaProductoActual.Contrato;
    var descuento = 0;
    var indicadorActualLocal = {};

    for (var i = 0; i < contrato.ListaContratoDetalleInfo.length; i++) {

        if (contrato.ListaContratoDetalleInfo[i].Indicador.IndicadorId.toString() == identificador.toString()) {

            indicadorActualLocal = contrato.ListaContratoDetalleInfo[i].Indicador;
            porcentajePermitido = contrato.ListaContratoDetalleInfo[i].PorcentajePermitido;
        }
    }

    if (entradaProductoActual.Contrato.CalidadOrigen == 1) {
        if (CalidadOrigenProporsionada) {
            porcentajeBoleta = $("#indicadorProducto_" + identificador).val();
        }
    } else {
        porcentajeBoleta = $("#porcentajeCalidad_" + identificador).val();
    }
    var porcentajeDestino = 0;

    if ($("#porcentajeCalidad_" + identificador).val() != "") {
        porcentajeDestino = parseFloat($("#porcentajeCalidad_" + identificador).val());
    }
    descuento = porcentajeBoleta - porcentajePermitido;
    if (indicadorActualLocal.Descripcion == "Humedad") {
        descuento = descuento * GradoPorcentualHumedad;
    }

    console.log("porcentajeBoleta " + porcentajeBoleta);
    console.log("porcentajePermitido " + porcentajePermitido);
    if (porcentajeBoleta <= porcentajePermitido) {
        $("#descuentoCalidad_" + identificador).val('');
    } else {
        $("#descuentoCalidad_" + identificador).val(descuento.toFixed(2));
    }
    CambiarColores("tblIndicadoresDestino", porcentajePermitido, identificador);
};

CambiarColores = function (tabla, porcentajePermitido, indicador) {

    var renglones = $("#" + tabla + " tbody tr");
    if (renglones != null) {
        for (var i = 0; i < renglones.length; i++) {
            if (renglones[i].cells[6] != null) {
                if (indicador == renglones[i].cells[6].getElementsByTagName("input")[0].value) {
                    if (porcentajePermitido < renglones[i].cells[2].getElementsByTagName("input")[0].value) {
                        renglones[i].style.backgroundColor = "#ff0000";

                    } else {
                        renglones[i].style.backgroundColor = "";
                    }
                }
            }
        }
    }

};

ValidarPorcentaje = function (control) {

    var porcentajePermitido = 0;
    var contrato = entradaProductoActual.Contrato;
    var idControl = control.id;
    if (idControl == null) {
        idControl = control.attr("id");
    }
    var arregloId = idControl.split("_");
    var identificador = arregloId[1];

    for (var i = 0; i < contrato.ListaContratoDetalleInfo.length; i++) {

        if (contrato.ListaContratoDetalleInfo[i].Indicador.IndicadorId.toString() == identificador.toString()) {
            porcentajePermitido = contrato.ListaContratoDetalleInfo[i].PorcentajePermitido;
        }
    }

    CambiarColores("tblIndicadores", porcentajePermitido, identificador);
};

function ValidarFormatoFecha(campo) {
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((campo.match(RegExPattern)) && (campo != '')) {
        return true;
    } else {
        return false;
    }
}


ObtenerGradoPorcentualHumedad = function () {

    $.ajax({
        type: "POST",
        url: "BoletaRecepcion.aspx/ObtenerGradoPorcentual",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                if (resultado.Resultado) {
                    GradoPorcentualHumedad = resultado.Control.Valor;
                }
            }
        }
    });
};

ValidarCalidadDestino = function () {

    for (var i = 0; i < indicadoresCalidad.length; i++) {
        var control = "#porcentajeCalidad_" + indicadoresCalidad[i].IndicadorInfo.IndicadorId;

        if ($(control).val() == "") {
            return false;
        }
    }
    return true;
}

FormtatoFecha = function (fecha) {
    var milli = fecha.replace(/\/Date\((-?\d+)\)\//, '$1');
    var d = new Date(parseInt(milli));
    var date = new Date(d);
    return (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
}

HabilitarDeshabilitarCampos = function (valor) {
    if (valor) {
        $("#cmbDestino").prop('disabled', 'true');
        $("#cmbContrato").prop('disabled', 'true');
        $("#btnGuardar").prop('disabled', 'true');
        return;
    }
    $("#cmbDestino").prop('disabled', 'false');
    $("#cmbContrato").prop('disabled', 'false');
    $("#btnGuardar").prop('disabled', 'false');
}
