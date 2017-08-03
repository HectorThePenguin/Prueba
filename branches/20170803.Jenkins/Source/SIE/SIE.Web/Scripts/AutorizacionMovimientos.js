//Variables globales
var msjAbierto = 0;
var guardado = false;

$(document).ready(function () {
    Inicializar();
    AsignarEventosControles();
    LimpiarPantalla();
});

Inicializar = function () {
    $('body').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtNumOrganizacion").numericInput().attr("maxlength", "4");
    $("#txtOrganizacion").attr("disabled", true);

    PreCondiciones();
}

AsignarEventosControles = function () {
    // Al capturar la organizacion en la ventana principal
    $('#txtNumOrganizacion').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumOrganizacion').val() != "") {
                e.preventDefault();
                ObtenerOrganizacion();
            }
        }
    });
    // Al perder el foco el textbox del indentificador de la organizacion
    $("#txtNumOrganizacion").focusout(function () {
        if ($('#txtNumOrganizacion').val() == "") {
            $("#txtOrganizacion").val("");
        }
        else {
            ObtenerOrganizacion();
        }
    });
    // Boton que abre la ayuda de organizaciones
    $("#btnAyudaOrganizacion").click(function () {
        ObtenerOrganizacionesTipoGanadera();
    });
    // Boton buscar de la ventana ayuda
    $("#btnAyudaBuscarOrganizacion").click(function () {
        ObtenerOrganizacionesTipoGanadera();
    });
    // Boton agregar de la ventana ayuda
    $("#btnAyudaAgregarBuscar").click(function () {
        var renglones = $("input[class=organizaciones]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtNumOrganizacion").val($(this).attr("organizacion"));
                $("#txtOrganizacion").val($(this).attr("descripcion"));
            });
            $("#dlgBusquedaOrganizacion").modal("hide");
            $("#txtOrganizacionBuscar").val("");
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaOrganizacion").modal("hide");
                bootbox.alert(window.msgSeleccionarOrganizacion, function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });
    // Boton cancelar de la ventana ayuda
    $("#btnAyudaCancelarBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaOrganizacion").modal("hide");
    });

    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
    });

    $("#btnNoBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("show");
        $("#txtOrganizacionBuscar").focus();
    });
    // Boton buscar solicitudes pendientes para el tipo de movimientos seleccionado
    $("#btnBuscar").click(function () {
        BuscarSolicitudesPendientes();
    });
    // Boton limpiar
    $("#btnLimpiar").click(function () {
        LimpiarPantalla();
    });
    // Boton guardar
    $("#btnGuardar").click(function () {
        Guardar();
    });
    // Boton cancelar en la pantalla principal
    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: window.msgCancelar,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    callback: function () {
                        LimpiarPantalla();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    callback: function () {
                        return true;
                    }
                }
            }
        });
    });
    // Enter dentro de la ayuda de organizacion
    $("#txtOrganizacionBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#GridContenido').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#dlgCancelarBuscar').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#tbBusqueda').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });
};
//Validaciones y consultas
// Se hacen las validaciones para cumplir con las precondiciones necesarias
PreCondiciones = function () {
    var continuar = false;
    var mensaje = "";
    $.ajax({
        type: "POST",
        url: "AutorizacionMovimientos.aspx/ValidarPreCondiciones",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorPrecondiciones, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var resultado = data.d;
            if (resultado < 1) {
                switch (resultado) {
                    case -1:
                        mensaje = window.msgSinTipoGanadera;
                        break;
                    case -2:
                        mensaje = window.msgSinTipoAutorizacion;
                        break;
                    case -3:
                        mensaje = window.msgSinMovimientosPendientes;
                        break;
                    default:
                        mensaje = window.msgErrorPrecondiciones;
                }

                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function () {
                        msjAbierto = 0;
                    });
                }
            }
            else {
                continuar = true;
                $("#dlgAutorizacionMovimientos").attr("disabled", false);
            }
        }
    });
    return continuar;
};
//Obtiene la organizacion capturada
ObtenerOrganizacion = function () {
    var datos = {};
    if ($("#txtNumOrganizacion").val() != "") {
        LimpiarGrid();

        datos = { "organizacion": $("#txtNumOrganizacion").val() };
        $.ajax({
            type: "POST",
            url: "AutorizacionMovimientos.aspx/ObtenerOrganizacion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinOrganizacionValida, function () {
                        msjAbierto = 0;
                    });
                    $("#txtNumOrganizacion").val("");
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d.length > 0) {
                    $("#txtOrganizacion").val(data.d[0].Descripcion);
                    $("#cmbMovimiento").focus();
                }
                else {
                    $("#txtNumOrganizacion").val("");
                    $("#txtOrganizacion").val("");
                    bootbox.alert(window.msgSinOrganizacionValida, function () {
                        setTimeout(function () { $("#txtNumOrganizacion").focus(); }, 500);
                    });
                }
            }
        });
    }
};
//Obtiene las organizaciones de tipo ganadera para la ayuda
ObtenerOrganizacionesTipoGanadera = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = { "organizacion": $("#txtOrganizacionBuscar").val() };
    } else {
        datos = { "organizacion": "" };
    }
    $.ajax({
        type: "POST",
        url: "AutorizacionMovimientos.aspx/ObtenerOrganizacionesTipoGanadera",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgSinTipoGanadera, function () {
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
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].OrganizacionID + "' organizacion='" + resultado[i].OrganizacionID + "' descripcion='" + resultado[i].Descripcion + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].OrganizacionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].OrganizacionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                        "</tr>");
                }
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else 
            {
                bootbox.alert(window.msgSinTipoGanadera, function () {
                    setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                });
            }
        }
    });
};
//Valida el tipo de solicitud a consultar
BuscarSolicitudesPendientes = function () {
    LimpiarGrid();
    if ($("#txtOrganizacion").val() != '') {
        switch ($("#cmbMovimiento option:selected").val()) {
            case $('#txtPrecioVenta').val():
                ObtenerSolicitudesPendientesPrecioVenta();
                break;
            case $("#txtUsoLote").val():
                ObtenerSolicitudesPendientesUsoLote();
                break;
            case $("#txtAjusteInventario").val():
                ObtenerSolicitudesPendientesAjusteInventario();
                break;
            default:
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSeleccionarMovimiento, function () {
                        msjAbierto = 0;
                    });
                    $("#cmbMovimiento").focus();
                }
        }
    }
    else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgSeleccionarOrganizacion, function () {
                msjAbierto = 0;
            });
            $("#txtNumOrganizacion").focus();
        }
    }
}
//Obtiene las solicitudes de precio venta pendiente
ObtenerSolicitudesPendientesPrecioVenta = function () {
    App.bloquearContenedor($(".container-fluid"));
    $.ajax({
        type: "POST",
        url: "AutorizacionMovimientos.aspx/ObtenerSolicitudesPendientesPrecioVenta",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ organizacionID: $("#txtNumOrganizacion").val(), tipoAutorizacionID: $("#cmbMovimiento option:selected").val() }),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorMovimientos, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var resultado = data.d;
                if (resultado.length > 0) {
                    document.getElementById("GridSolicitudes").style.display = 'block';

                    $("#GridEncabezado thead").append("<tr>" +
                    "<th style='width: 40px;' class='alineacionCentro' scope='col'>" + window.lblFolio + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblProducto + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblAlmacen + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblLote + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblCostoUnitario + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblPrecioVenta + "</th>" +
                    "<th style='width: 125px;' class='alineacionCentro' scope='col'>" + window.lblJustificacion + "</th>" +
                    "<th style='width: 100px;' scope='col'>" + window.lblAutRech + "</th>" +
                    "<th style='width: 140px;' scope='col'>" + window.lblObservaciones + "</th>" +
                    "</tr>");

                    for (var i = 0; i < resultado.length; i++) {
                        $("#GridContenido tbody").append("<tr>" +
                            "<td class='alineacionCentro' id='autorizacion" + i + "' value='" + resultado[i].AutorizacionID + "' style='width: 50px;'>" + resultado[i].Folio + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Producto + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Almacen + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + resultado[i].Lote + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.toFixed(resultado[i].Costo, 2) + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.toFixed(resultado[i].Precio, 2) + "</td>" +
                            "<td class='alineacionCentro' style='width: 150px;'>" + resultado[i].Justificacion + "</td>" + 
                            "<td class='alineacionCentro' style='width: 100px;'>" +
                            "<input type='radio' id='solicitudAut" + i + "' name='solicitud" + i + "' value='" + $('#txtAutorizado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Aut " +
                            "<input type='radio' id='solicitudRech" + i + "' name='solicitud" + i + "' value='" + $('#txtRechazado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Rech</td>" +
                            "<td class='alineacionCentro' style='width: 220px;'><input type='tel' disabled='disabled' id='observaciones" + i + "' name='observacion" + i + "' MaxLength='250'/></td>" +
                            "</tr>");
                    }
                    document.getElementById("BotonesPie").style.display = 'block';
                }
                else {
                    if (!guardado) {
                        bootbox.alert(window.msgSinMovimientosPendientes, function () {
                            setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                        });
                    }
                    else {
                        guardado = false;
                    }
                }
            }
            else {
                if (!guardado) {
                    bootbox.alert(window.msgSinMovimientosPendientes, function () {
                        setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                    });
                }
                else {
                    guardado = false;
                }
            }
        }
    });
};
//Obtiene las solicitudes de uso de lote pendiente
ObtenerSolicitudesPendientesUsoLote = function () {
    App.bloquearContenedor($(".container-fluid"));
    $.ajax({
        type: "POST",
        url: "AutorizacionMovimientos.aspx/ObtenerSolicitudesPendientesUsoLote",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ organizacionID: $("#txtNumOrganizacion").val(), tipoAutorizacionID: $("#cmbMovimiento option:selected").val() }),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorMovimientos, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var resultado = data.d;
                if (resultado.length > 0) {
                    document.getElementById("GridSolicitudes").style.display = 'block';

                    $("#GridEncabezado thead").append("<tr>" +
                    "<th style='width: 40px;' class='alineacionCentro' scope='col'>" + window.lblFolio + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblProducto + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblAlmacen + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblLote + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblCostoUnitario + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblLoteUtilizar + "</th>" +
                    "<th style='width: 125px;' class='alineacionCentro' scope='col'>" + window.lblJustificacion + "</th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'>" + window.lblAutRech + "</th>" +
                    "<th style='width: 140px;' class='alineacionCentro' scope='col'>" + window.lblObservaciones + "</th>" +
                    "</tr>");

                    for (var i = 0; i < resultado.length; i++) {
                        $("#GridContenido tbody").append("<tr>" +
                            "<td class='alineacionCentro' id='autorizacion" + i + "' value='" + resultado[i].AutorizacionID + "' style='width: 50px;'>" + resultado[i].Folio + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Producto + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Almacen + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + resultado[i].Lote + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.toFixed(resultado[i].Precio, 2) + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + resultado[i].LoteNuevo + "</td>" +
                            "<td class='alineacionCentro' style='width: 150px;'>" + resultado[i].Justificacion + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" +
                            "<input type='radio' id='solicitudAut" + i + "' name='solicitud" + i + "' value='" + $('#txtAutorizado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Aut " +
                            "<input type='radio' id='solicitudRech" + i + "' name='solicitud" + i + "' value='" + $('#txtRechazado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Rech</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'><input type='tel' disabled='disabled' id='observaciones" + i + "' name='observacion" + i + "' MaxLength='250'/></td>" +
                            "</tr>");
                    }
                    document.getElementById("BotonesPie").style.display = 'block';
                }
                else {
                    if(!guardado){
                        bootbox.alert(window.msgSinMovimientosPendientes, function () {
                            setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                        });
                    }
                    else {
                        guardado = false;
                    }
                }
            }
            else {
                if (!guardado) {
                    bootbox.alert(window.msgSinMovimientosPendientes, function () {
                        setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                    });
                }
                else {
                    guardado = false;
                }
            }
        }
    });
};
//Obtiene las solicitudes de ajuste de inventario pendiente
ObtenerSolicitudesPendientesAjusteInventario = function () {
    App.bloquearContenedor($(".container-fluid"));
    $.ajax({
        type: "POST",
        url: "AutorizacionMovimientos.aspx/ObtenerSolicitudesPendientesAjusteInventario",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ organizacionID: $("#txtNumOrganizacion").val(), tipoAutorizacionID: $("#cmbMovimiento option:selected").val() }),
        error: function (request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorMovimientos, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var resultado = data.d;
                if (resultado.length > 0) {
                    document.getElementById("GridSolicitudes").style.display = 'block';

                    $("#GridEncabezado thead").append("<tr>" +
                    "<th style='width: 40px;' class='alineacionCentro' scope='col'>" + window.lblFolio + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblProducto + "</th>" +
                    "<th style='width: 82px;' class='alineacionCentro' scope='col'>" + window.lblAlmacen + "</th>" +
                    "<th style='width: 50px;' class='alineacionCentro' scope='col'>" + window.lblLote + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblCostoUnitario + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblCantidadAjuste + "</th>" +
                    "<th style='width: 65px;' class='alineacionCentro' scope='col'>" + window.lblPorcentajeAjuste + "</th>" +
                    "<th style='width: 110px;' class='alineacionCentro' scope='col'>" + window.lblJustificacion + "</th>" +
                    "<th style='width: 80px;' scope='col'>" + window.lblAutRech + "</th>" +
                    "<th style='width: 140px;' scope='col'>" + window.lblObservaciones + "</th>" +
                    "</tr>");

                    for (var i = 0; i < resultado.length; i++) {
                        $("#GridContenido tbody").append("<tr>" +
                            "<td class='alineacionCentro' id='autorizacion" + i + "' value='" + resultado[i].AutorizacionID + "' style='width: 50px;'>" + resultado[i].Folio + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Producto + "</td>" +
                            "<td class='alineacionCentro' id='Almacen" + i + "' value='" + resultado[i].AlmacenMovimientoID + "' style='width: 100px;'>" + resultado[i].Almacen + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + resultado[i].Lote + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.toFixed(resultado[i].Precio, 2) + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.formatNumber(resultado[i].CantidadAjuste) + "</td>" +
                            "<td class='alineacionCentro' style='width: 80px;'>" + accounting.toFixed(resultado[i].PorcentajeAjuste, 2) + "</td>" +
                            "<td class='alineacionCentro' style='width: 150px;'>" + resultado[i].Justificacion + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" +
                            "<input type='radio' id='solicitudAut" + i + "' name='solicitud" + i + "' value='" + $('#txtAutorizado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Aut " +
                            "<input type='radio' id='solicitudRech" + i + "' name='solicitud" + i + "' value='" + $('#txtRechazado').val() + "' onclick='SeleccionaAutRech(" + i + ");'/>Rech</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'><input type='tel' disabled='disabled' id='observaciones" + i + "' name='observacion" + i + "' MaxLength='250'/></td>" +
                            "</tr>");
                    }
                    document.getElementById("BotonesPie").style.display = 'block';
                }
                else {
                    if(!guardado){
                        bootbox.alert(window.msgSinMovimientosPendientes, function () {
                            setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                        });
                    }
                    else {
                        guardado = false;
                    }
                }
            }
            else {
                if (!guardado) {
                    bootbox.alert(window.msgSinMovimientosPendientes, function () {
                        setTimeout(function () { $("#txtNumOrganizacion").val(""); $("#txtOrganizacion").val(""); document.getElementById("cmbMovimiento").value = [0]; $("#txtNumOrganizacion").focus(); }, 500);
                    });
                }
                else {
                    guardado = false;
                }
            }
        }
    });
};
// Se realiza el proceso de guardar las Autorizaciones/Rechazos a las solicitudes
Guardar = function () {
    var renglonesSolicitudes = $("#GridContenido tbody tr");
    var respuestasLista = [];
    var marcadas = 0;
    var almacenMovID = 0;
    var inventario = false;

    if ($('#cmbMovimiento option:selected').val() == $('#txtAjusteInventario').val()) {
        inventario = true;
    }

    for (var i = 0; i < renglonesSolicitudes.length; i++) {
        //var folio = renglonesSolicitudes[i].cells[0].innerHTML;
        //var lote = renglonesSolicitudes[i].cells[3].innerHTML;
        var AutorizacionID = $("#autorizacion" + i).attr("value");
        var estatus = 0;
        var observaciones = "";

        if ($("input[name=solicitud" + i + "]").is(":checked")) {
            marcadas++;

            if ($("input[id=solicitudAut" + i + "]").is(":checked")) {
                estatus = $("input[id=solicitudAut" + i + "]").val();
            }
            else if ($("input[id=solicitudRech" + i + "]").is(":checked") && $("#observaciones" + i).val().trim() == "") { // Se valida que la solicitud rechazada 
                bootbox.alert(window.msgSinObservacionesCapturadas, function () {                                                  // tenga capturada las observaciones
                    setTimeout(function () { $("#observaciones" + i).focus(); }, 500);
                });
                respuestasLista = [];
                break;
            }
            else {
                estatus = $("input[id=solicitudRech" + i + "]").val();
                observaciones = $("#observaciones" + i).val();
            }

            if (inventario) {
                almacenMovID = $("#Almacen" + i).attr("value");
            }

            var respuesta = {
                "AutorizacionID": AutorizacionID,
                "EstatusID": estatus,
                "AlmacenMovimientoID": almacenMovID,
                "Observaciones": observaciones
            };
            respuestasLista.push(respuesta);
        }
    }
    if (marcadas == 0) {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgSinCambioEstatus, function () {
                msjAbierto = 0;
            });
        }
    }
    else {
        if (respuestasLista.length > 0) {
            App.bloquearContenedor($(".container-fluid"));
            var datos = { "respuestaSolicitudes": respuestasLista, "organizacionID": $('#txtNumOrganizacion').val(), "tipoAutorizacionID": $('#cmbMovimiento option:selected').val() };

            $.ajax({
                type: "POST",
                url: "AutorizacionMovimientos.aspx/GuardarRespuestasSolicitudes",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (result) {
                    App.desbloquearContenedor($(".container-fluid"));
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                },
                dataType: "json",
                success: function (data) {
                    App.desbloquearContenedor($(".container-fluid"));
                    
                    if (data.d.Resultado == false) {
                        if (data.d.CodigoMensaje = 1) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(data.d.Mensaje, function() {
                                    guardado = true;
                                    BuscarSolicitudesPendientes();
                                    msjAbierto = 0;
                                });
                            }
                        } else {
                            if (data.d.CodigoMensaje = 2) {
                                if (msjAbierto == 0) {
                                    msjAbierto = 1;
                                    bootbox.alert(data.d.Mensaje, function() {
                                        msjAbierto = 0;
                                    });
                                }
                            }
                        }
                    }

                    if (data.d) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgGuardadoConExito, function () {
                                    guardado = true;
                                    BuscarSolicitudesPendientes();
                                    msjAbierto = 0;
                                });
                            }
                        }
                        else {
                            App.desbloquearContenedor($(".container-fluid"));
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgGuardadoSinExito, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                    }
                });
        }
    }
}

LimpiarPantalla = function () {
    document.getElementById("cmbMovimiento").value = [0];
    $("#txtOrganizacion").val("");
    $("#txtNumOrganizacion").val("");
    $("#txtNumOrganizacion").focus();
    LimpiarGrid();
};
//Se limpia y oculta el grid y los botones Guardar y Cancenlar
LimpiarGrid = function () {
    $("#GridEncabezado thead").html("");
    $("#GridContenido tbody").html("");
    document.getElementById("GridSolicitudes").style.display = 'none';
    document.getElementById("BotonesPie").style.display = 'none';
}
//Selecciona sólo un checkbox
SeleccionaUno = function (Id) {
    var listaCheckBox = $(".organizaciones");
    var checkbox = $(Id);
    if (checkbox.is(":checked")) {
        listaCheckBox.each(function () {
            this.checked = false;
        });
        checkbox.attr("checked", true);
    }
};
//Selecciona sólo un radio Aut/Rech por renglon
SeleccionaAutRech = function (ID) {
    if ($("#solicitudAut" + ID).is(":checked"))
    {
        $("#observaciones" + ID).attr("disabled", true);
    }
    else{
        $("#observaciones" + ID).attr("disabled", false);
    }
};