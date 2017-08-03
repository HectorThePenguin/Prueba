$(document).ready(function () {
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtLoteAlmacen").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtLoteProceso").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtBodegaExterna").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtPiezas").numericInput();
    $("#txtPiezas").addClass('textoDerecha');
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });

    $("#txtFecha").attr("disabled", "disabled");

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtPiezas").focusout(function () {
        var cantidadFormateada = FormatearCantidad($("#txtPiezas").val());
        $("#txtPiezas").val(cantidadFormateada);
    });

    // Boton para crear el nuevo lote
    $("#btnFechasDescarga").click(function () {
        ActualizarFechaDescarga();
    });

    // Boton que abre la ayuda de los folios.
    $("#btnBuscarFolio").click(function () {
        ObtenerFoliosPendientes();
    });
    // Boton que agrega el folio
    $("#btnAgregarAyudaFolio").click(function () {
        var renglones = $("input[class=folios]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtFolio").val($(this).attr("folio"));
                $("#txtFolio").focus();
            });
            Limpiar();
            ObtenerEntradaProducto();
            $("#dlgBusquedaFolio").modal("hide");
        } else {
            $("#dlgBusquedaFolio").modal("hide");
            bootbox.alert(window.mensajeSeleccionarFolio, function () {
                $("#dlgBusquedaFolio").modal("show");
            });
        }
    });
    // Boton que cancela la busqueda de folios
    $("#btnCancelarAyudaFolio").click(function () {
        $("#dlgBusquedaFolio").modal("hide");
        bootbox.dialog({
            message: window.mensajeCancelarAyudaFolio,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#txtFolioBuscar").val("");
                        setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgBusquedaFolio").modal("show");
                    }
                }
            }
        });
    });

    $("#btnGuardar").click(function () {
        ActualizarEntradaEnPatio();
    });

    // Boton que cancela la captura y limpia la pantalla
    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: window.mensajeCancelacion,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        Limpiar();
                        $("#txtFolio").val("");
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                    }
                }
            }
        });
    });

    // Boton que busca el folio capturado
    $("#btnBuscarAyudaFolio").click(function () {
        ObtenerEntradaProductoFolioTecleado();
    });

    var DesactivarLotes = function () {
        switch (parseInt($("#txtAlmacenUsuario").val())) {

            case 6: //Materias Primas
                $("#rbLoteAlmacen").removeAttr("disabled");
                $("#rbLoteAlmacen").attr("checked", "checked");
                $("#btnLoteAlmacen").removeAttr("disabled");
                $("#btnBuscarLoteAlmacen").removeAttr("disabled");

                $("#rbLoteProceso").attr("disabled", "disabled");
                $("#txtLoteProceso").attr("disabled", "disabled");
                $("#txtBodegaExterna").attr("disabled", "disabled");
                $("#txtLoteProceso").val("");
                $("#txtBodegaExterna").val("");

                $("#txtBodegaExternaLoteId").val("");
                $("#txtLoteProcesoLoteId").val("");

                $("#rbBodegaExterna").attr("disabled", "disabled");
                break;
            case 8: //Planta Alimentos
                $("#rbLoteProceso").removeAttr("disabled");
                $("#rbLoteProceso").attr("checked", "checked");
                $("#btnLoteProceso").removeAttr("disabled");
                $("#btnBuscarLoteProceso").removeAttr("disabled");

                $("#rbBodegaExterna").attr("disabled", "disabled");
                $("#txtBodegaExterna").attr("disabled", "disabled");
                $("#txtLoteAlmacen").attr("disabled", "disabled");
                $("#txtBodegaExterna").val("");
                $("#txtLoteAlmacen").val("");

                $("#txtBodegaExternaLoteId").val("");
                $("#txtLoteAlmacenLoteId").val("");

                $("#rbLoteAlmacen").attr("disabled", "disabled");
                break;
            case 9: //Bodega Externa
                $("#rbBodegaExterna").removeAttr("disabled");
                $("#btnBodegaExterna").removeAttr("disabled");
                $("#btnBuscarBodegaExterna").removeAttr("disabled");

                $("#rbLoteAlmacen").attr("disabled", "disabled");
                $("#txtLoteAlmacen").attr("disabled", "disabled");
                $("#txtLoteProceso").attr("disabled", "disabled");
                $("#txtLoteAlmacen").val("");
                $("#txtLoteProceso").val("");

                $("#txtLoteAlmacenLoteId").val("");
                $("#txtLoteProcesoLoteId").val("");

                $("#rbLoteProceso").attr("disabled", "disabled");
                break;
            default:
                $("#rbLoteProceso").attr("disabled", "disabled");
                $("#rbLoteAlmacen").attr("disabled", "disabled");
                $("#rbBodegaExterna").attr("disabled", "disabled");
                break;
        }
    };

    // Funcion para limpiar controles.
    var Limpiar = function () {
        $('#txtFecha').val($.datepicker.formatDate('dd/mm/yy', new Date()));
        $("#txtFolio").removeAttr("disabled");
        $("#txtFolio").focus();
        $("#btnBuscarFolio").removeAttr("disabled");
        $('#txtLoteAlmacen').val("");
        $('#txtLoteProceso').val("");
        $('#txtBodegaExterna').val("");
        $('#txtProducto').val("");
        $('#txtContrato').val("");
        $('#txtProveedor').val("");
        $('#txtPlacas').val("");
        $('#txtChofer').val("");
        $("#txtProductoId").val("");
        $("#txtEntradaProductoId").val("");
        $('#txtPiezas').val("");

        $("#txtLoteAlmacenLoteId").val("");
        $("#txtLoteProcesoLoteId").val("");
        $("#txtBodegaExternaLoteId").val("");

        $("#txtLoteAlmacen").attr("disabled", "disabled");
        $("#txtLoteProceso").attr("disabled", "disabled");
        $("#txtBodegaExterna").attr("disabled", "disabled");
        $("#txtPiezas").attr("disabled", "disabled");

        $("#rbLoteProceso").attr("disabled", "disabled");
        $("#rbLoteAlmacen").attr("disabled", "disabled");
        $("#rbBodegaExterna").attr("disabled", "disabled");

        $("#btnFechasDescarga").hide();
        $("#btnGuardar").removeAttr("disabled");
        $("#btnFechasDescarga").removeAttr("disabled");
        $("#lblFechaDescarga").html(btnHoraInicioDescarga);
        DesactivarLotes();
    };

    var DesactivarCaptura = function () {

        $("#rbLoteAlmacen").attr("disabled", "disabled");
        $("#txtLoteAlmacen").attr("disabled", "disabled");

        $("#rbLoteProceso").attr("disabled", "disabled");
        $("#txtLoteProceso").attr("disabled", "disabled");

        $("#rbBodegaExterna").attr("disabled", "disabled");
        $("#txtBodegaExterna").attr("disabled", "disabled");
    };

    $('#txtFolioBuscar').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtFolioBuscar').val() != "") {
                ObtenerEntradaProductoFolioTecleado();
            }
            e.preventDefault();
        }
    });

    $('#txtFolio').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtFolio').val() == "") {
                $('#btnBuscarFolio').focus();
            } else {
                Limpiar();
                ObtenerEntradaProducto();
            }
            e.preventDefault();
        }
    });

    // Funcion para obtener los datos del folio de entrada producto.
    var ObtenerEntradaProducto = function () {
        App.bloquearContenedor($(".contenido-patio"));
        $.ajax({
            type: "POST",
            url: "RecepcionMateriaPrimaEnPatio.aspx/ConsultarEntradaProducto",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folio: $("#txtFolio").val() }),
            error: function (request) {
                App.desbloquearContenedor($(".contenido-patio"));
                bootbox.alert(window.mensajeErrorAlConsultarFolio);
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenido-patio"));
                if (data.d) {
                    if (data.d.Activo == 1) {
                        if (data.d.PesoBruto > 0) {
                            var fechaFinDescarga = new Date(parseInt(data.d.FechaFinDescarga.substr(6)));
                            if (fechaFinDescarga.getFullYear() > 1) {
                                bootbox.alert(window.mensajeElFolioTieneAsignadoLote, Limpiar);
                            } else {

                                $("#txtFolio").attr("disabled", "disabled");
                                $("#btnBuscarFolio").attr("disabled", "disabled");
                                $("#txtEntradaProductoId").val(data.d.EntradaProductoId);
                                $("#txtProductoId").val(data.d.Producto.ProductoId);
                                $("#txtProducto").val(data.d.Producto.ProductoDescripcion);
                                $("#txtContrato").val(data.d.Contrato.Folio);
                                $("#txtProveedor").val(data.d.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion);
                                $("#txtPlacas").val(data.d.RegistroVigilancia.CamionCadena);
                                $("#txtChofer").val(data.d.RegistroVigilancia.Chofer);
                                $("#txtPiezas").val(FormatearCantidad(data.d.Piezas));

                                var fechaInicioDescarga = new Date(parseInt(data.d.FechaInicioDescarga.substr(6)));

                                if (fechaInicioDescarga.getFullYear() > 1) {
                                    $("#lblFechaDescarga").html(btnHoraFinDescarga);
                                    $("#btnGuardar").attr("disabled", "disabled");
                                    $("#btnFechasDescarga").show();
                                }

                                if (data.d.AlmacenInventarioLote.AlmacenInventarioLoteId != 0) {
                                    DesactivarCaptura();

                                    // Si ya se tiene la fecha de inicio se valida si se desactivan las piezas.
                                    if (fechaInicioDescarga.getFullYear() > 1) {
                                        // Se activan las piezas si el producto es forraje o si la subfamilia es MicroIngredientes
                                        if (data.d.Producto.Forraje || data.d.Producto.EsPremezcla) {
                                            $("#txtPiezas").removeAttr("disabled");
                                            $("#txtPiezas").focus();
                                        }
                                    }
                                    switch (data.d.AlmacenInventarioLote.TipoAlmacenId) {
                                        case 6: //Materias Primas
                                            $("#rbLoteAlmacen").attr("checked", "checked");
                                            $("#txtLoteAlmacen").val(data.d.AlmacenInventarioLote.Lote);
                                            $("#txtLoteAlmacenLoteId").val(data.d.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                            break;
                                        case 8: //Planta Alimentos
                                            $("#rbLoteProceso").attr("checked", "checked");
                                            $("#txtLoteProceso").val(data.d.AlmacenInventarioLote.Lote);
                                            $("#txtLoteProcesoLoteId").val(data.d.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                            break;
                                        case 9: //Bodega Externa
                                            $("#rbBodegaExterna").attr("checked", "checked");
                                            $("#txtBodegaExterna").val(data.d.AlmacenInventarioLote.Lote);
                                            $("#txtBodegaExternaLoteId").val(data.d.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                            break;
                                    }
                                }
                                else {
                                    bootbox.alert(window.mensajeNoSeLeHaAsignadoLote, Limpiar);
                                }
                            }

                        } else {
                            bootbox.alert(window.mensajeBoletaNoTienePesaje, Limpiar);
                        }
                    }
                    else {
                        bootbox.alert(window.mensajeFolioActivo, Limpiar);
                    }
                } else {
                    bootbox.alert(window.mensajeFolioNoValido, Limpiar);
                }
            }
        });
    };

    // Metodo que funciona para consultar los folios que estan pendientes de descargar en patio.
    var ObtenerFoliosPendientes = function () {
        App.bloquearContenedor($(".contenido-patio"));
        $.ajax({
            type: "POST",
            url: "RecepcionMateriaPrimaEnPatio.aspx/ConsultarListadoEntradaProducto",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folio: 0 }),
            error: function (request) {
                App.desbloquearContenedor($(".contenido-patio"));
                bootbox.alert(window.mensajeErrorAlConsultarFolios);
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenido-patio"));
                if (data.d) {
                    var resultado = data.d;
                    $("#gridFoliosProductos tbody").html("");
                    for (var i = 0; i < resultado.length; i++) {
                        $("#gridFoliosProductos tbody").append("<tr>" +
                            "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='folios' id='folio" + resultado[i].Folio + "' folio='" + resultado[i].Folio + "' onclick='SeleccionaUno(\"#folio" + resultado[i].Folio + "\");'/></td>" +
                            "<td class='alineacionCentro' style='width: 50px;'>" + resultado[i].Folio + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Contrato.Folio + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Producto.ProductoDescripcion + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].RegistroVigilancia.ProveedorMateriasPrimas.Descripcion + "</td>" +
                            "</tr>");
                    }
                    setTimeout(function () { $("#txtFolioBuscar").val(""); $("#txtFolioBuscar").focus(); }, 500);
                    $("#dlgBusquedaFolio").modal("show");
                } else {
                    bootbox.alert(window.mensajeNoSeEncontraronFoliosPendientes, function () {
                        setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
                    });
                }
            }
        });
    };

    //Selecciona solo un checkbox
    SeleccionaUno = function (Id) {
        var listaCheckBox = $(".folios");
        var checkbox = $(Id);
        if (checkbox.is(":checked")) {
            listaCheckBox.each(function () {
                this.checked = false;
            });
            checkbox.attr("checked", true);
        }
    };

    // Funcion para obtener los datos del folio de entrada producto de la ayuda
    var ObtenerEntradaProductoFolioTecleado = function () {
        if ($("#txtFolioBuscar").val() > 0) {
            App.bloquearContenedor($(".contenido-patio"));
            $.ajax({
                type: "POST",
                url: "RecepcionMateriaPrimaEnPatio.aspx/ConsultarEntradaProducto",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ folio: $("#txtFolioBuscar").val() }),
                error: function (request) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    bootbox.alert(request.Message);
                },
                success: function (data) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    if (data.d) {
                        if (data.d.Activo == 1) {
                            if (data.d.PesoBruto > 0) {
                                var fechaFinDescarga = new Date(parseInt(data.d.FechaFinDescarga.substr(6)));
                                if (data.d.AlmacenInventarioLote.AlmacenInventarioLoteId == 0 && fechaFinDescarga.getFullYear() == 1) {
                                    $("#gridFoliosProductos tbody").html("");
                                    $("#gridFoliosProductos tbody").append("<tr>" +
                                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='folios' id='folio" + data.d.Folio + "' folio='" + data.d.Folio + "' onclick='SeleccionaUno(\"#folio" + data.d.Folio + "\");'/></td>" +
                                        "<td class='alineacionCentro' style='width: 50px;'>" + data.d.Folio + "</td>" +
                                        "<td class='alineacionCentro' style='width: 100px;'>" + data.d.Contrato.Folio + "</td>" +
                                        "<td class='alineacionCentro' style='width: 100px;'>" + data.d.Producto.ProductoDescripcion + "</td>" +
                                        "<td class='alineacionCentro' style='width: 100px;'>" + data.d.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion + "</td>" +
                                        "</tr>");
                                }
                                else {
                                    $("#dlgBusquedaFolio").modal("hide");
                                    bootbox.alert(window.mensajeElFolioTieneAsignadoLote, function () {
                                        $("#dlgBusquedaFolio").modal("show");
                                    });
                                }
                            } else {
                                $("#dlgBusquedaFolio").modal("hide");
                                bootbox.alert(window.mensajeBoletaNoTienePesaje, function () {
                                    $("#dlgBusquedaFolio").modal("show");
                                });
                            }
                        } else {
                            $("#dlgBusquedaFolio").modal("hide");
                            bootbox.alert(window.mensajeFolioActivo, function () {
                                $("#dlgBusquedaFolio").modal("show");
                            });
                        }
                    } else {
                        $("#dlgBusquedaFolio").modal("hide");
                        bootbox.alert(window.mensajeFolioNoValido, function () {
                            $("#dlgBusquedaFolio").modal("show");
                        });
                    }
                }
            });
        } else {
            $("#dlgBusquedaFolio").modal("hide");
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido, function () {
                $("#dlgBusquedaFolio").modal("show");
            });
        }
    };
    //// Método para actualizar el lote a la entrada producto
    var ActualizarEntradaEnPatio = function () {

        if ($("#txtProductoId").val() > 0) {
            if ($("#txtLoteAlmacenLoteId").val() > 0 || $("#txtLoteProcesoLoteId").val() > 0 || $("#txtBodegaExternaLoteId").val() > 0) {
                var datosLote = {};
                var almacenInventarioLote = {};

                switch ($('input[name="datosRecepcion"]:checked').val()) {
                    case "rbLoteAlmacen":
                        almacenInventarioLote.AlmacenInventarioLoteId = $("#txtLoteAlmacenLoteId").val();
                        break;
                    case "rbLoteProceso":
                        almacenInventarioLote.AlmacenInventarioLoteId = $("#txtLoteProcesoLoteId").val();
                        break;
                    case "rbBodegaExterna":
                        almacenInventarioLote.AlmacenInventarioLoteId = $("#txtBodegaExternaLoteId").val();
                        break;
                }

                datosLote.EntradaProductoId = $("#txtEntradaProductoId").val();
                datosLote.AlmacenInventarioLote = almacenInventarioLote;

                App.bloquearContenedor($(".contenido-patio"));
                $.ajax({
                    type: "POST",
                    url: "RecepcionMateriaPrimaEnPatio.aspx/ActualizaOperadorFechaDescargaEnPatio",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ entrada: datosLote }),
                    error: function (request) {
                        App.desbloquearContenedor($(".contenido-patio"));
                        bootbox.alert(window.mensajeErrorAlGuardar);
                    },
                    success: function (data) {
                        App.desbloquearContenedor($(".contenido-patio"));
                        if (data.d) {
                            bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeGuardadoOK, function () {
                                Limpiar();
                                $("#txtFolio").val("");
                            });
                        } else {
                            bootbox.alert(window.mensajeErrorAlGuardar);
                        }
                    }
                });
            }
            else {
                bootbox.alert(window.mensajeCapturarLoteValido);
            }
        }
        else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido);
        }
    };

    var ActualizarFechaDescarga = function () {
        if ($("#txtEntradaProductoId").val() > 0) {

            var datosEntrada = {};
            var producto = {};
            producto.ProductoId = $("#txtProductoId").val();

            datosEntrada.Producto = producto;
            datosEntrada.EntradaProductoId = $("#txtEntradaProductoId").val();
            datosEntrada.Piezas = 0;
            var piezas = TryParseInt($("#txtPiezas").val().replace(/,/g, '').replace(/_/g, ''), 0);
            if (piezas > 0 || $("#txtPiezas").is(":disabled")) {
                if (piezas > 0) {
                    datosEntrada.Piezas = piezas;
                }

                App.bloquearContenedor($(".contenido-patio"));

                $.ajax({
                    type: "POST",
                    url: "RecepcionMateriaPrimaEnPatio.aspx/ActualizaFechaDescargaEnPatio",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ entrada: datosEntrada }),
                    error: function (request) {
                        App.desbloquearContenedor($(".contenido-patio"));
                        bootbox.alert(window.mensajeErrorAlActualizarFecha);
                    },
                    success: function (data) {
                        App.desbloquearContenedor($(".contenido-patio"));
                        if (data.d) {
                            if (data.d == "ErrorForraje") {
                                bootbox.alert(window.mensajeBoletaNoHaSidoFinalizada);
                            } else {
                                bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeFechaActualizada, function () {
                                    Limpiar();
                                    $("#txtFolio").val("");
                                });
                            }
                        } else {
                            bootbox.alert(window.mensajeErrorAlActualizarFecha);
                        }
                    }
                });
            }
            else {
                bootbox.alert(window.mensajePiezasMayorACero, function () {
                    setTimeout(function () { $("#txtPiezas").val(""); $("#txtPiezas").focus(); }, 500);
                });
            }
        }
        else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido);
        }
    };

    Limpiar();
});

function EnviarMensajeUsuario(mensaje) {
    bootbox.alert(mensaje, function () {
        history.go(-1);
        return false;
    });
}