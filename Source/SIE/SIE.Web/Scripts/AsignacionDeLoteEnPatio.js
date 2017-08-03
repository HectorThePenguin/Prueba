$(document).ready(function () {
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtLoteAlmacen").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtLoteProceso").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtBodegaExterna").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 10, "greedy": false, numericInput: true });

    $("#txtFecha").attr("disabled", "disabled");

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    // Boton que agrega el lote
    $("#btnAyudaAgregar").click(function () {
        var renglonesLote = $("input[class=lotes]:checked");

        if (renglonesLote.length > 0) {
            renglonesLote.each(function () {
                switch ($('input[name="datosRecepcion"]:checked').val()) {
                    case "rbLoteAlmacen":
                        $("#txtLoteAlmacen").val($(this).attr("lote"));
                        $("#txtLoteAlmacenLoteId").val($(this).attr("almaceninventarioloteid"));
                        $("#txtLoteAlmacen").focus();
                        break;
                    case "rbLoteProceso":
                        $("#txtLoteProceso").val($(this).attr("lote"));
                        $("#txtLoteProcesoLoteId").val($(this).attr("almaceninventarioloteid"));
                        $("#txtLoteProceso").focus();
                        break;
                    case "rbBodegaExterna":
                        $("#txtBodegaExterna").val($(this).attr("lote"));
                        $("#txtBodegaExternaLoteId").val($(this).attr("almaceninventarioloteid"));
                        $("#txtBodegaExterna").focus();
                        break;

                    default:
                }
            });
            $("#dlgBusquedaLotes").modal("hide");
        } else {
            $("#dlgBusquedaLotes").modal("hide");
            bootbox.alert(window.mensajeSeleccionarLote, function () {
                $("#dlgBusquedaLotes").modal("show");
            });
        }
    });

    // Boton para crear el nuevo lote
    $("#btnLoteAlmacen").click(function () {
        ObtenerNuevoLoteMateriaPrima();
    });
    // Boton para crear el nuevo lote
    $("#btnLoteProceso").click(function () {
        ObtenerNuevoLoteMateriaPrima();
    });

    // Boton para crear el nuevo lote
    $("#btnBodegaExterna").click(function () {
        ObtenerNuevoLoteMateriaPrima();
    });

    // Boton para abrir la ayuda de lotes
    $("#btnBuscarLoteAlmacen").click(function () {
        AbrirAyudaLotes();
    });
    // Boton para abrir la ayuda de lotes
    $("#btnBuscarLoteProceso").click(function () {
        AbrirAyudaLotes();
    });
    // Boton para abrir la ayuda de lotes
    $("#btnBuscarBodegaExterna").click(function () {
        AbrirAyudaLotes();
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

    // Boton que cancela la seleccion de los lotes y cierra la ventana emergente
    $("#btnAyudaCancelar").click(function () {
        $("#dlgBusquedaLotes").modal("hide");
        bootbox.dialog({
            message: window.mensajeCancelarAyudaLote,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        switch ($('input[name="datosRecepcion"]:checked').val()) {
                            case "rbLoteAlmacen":
                                setTimeout(function () { $("#txtLoteAlmacen").focus(); }, 500);
                                break;
                            case "rbLoteProceso":
                                setTimeout(function () { $("#txtLoteProceso").focus(); }, 500);
                                break;
                            case "rbBodegaExterna":
                                setTimeout(function () { $("#txtBodegaExterna").focus(); }, 500);
                                break;
                        }
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgBusquedaLotes").modal("show");
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

        $("#txtLoteAlmacenLoteId").val("");
        $("#txtLoteProcesoLoteId").val("");
        $("#txtBodegaExternaLoteId").val("");

        $('#rbLoteAlmacen').attr("checked", "checked");
        $("#rbLoteAlmacen").removeAttr("disabled");

        $("#txtLoteAlmacen").removeAttr("disabled");
        $("#btnLoteAlmacen").removeAttr("disabled");
        $("#btnBuscarLoteAlmacen").removeAttr("disabled");

        $("#txtLoteProceso").attr("disabled", "disabled");
        $("#txtBodegaExterna").attr("disabled", "disabled");

        $("#rbLoteProceso").removeAttr("disabled");
        $("#rbBodegaExterna").removeAttr("disabled");

        $("#btnLoteProceso").attr("disabled", "disabled");
        $("#btnBuscarLoteProceso").attr("disabled", "disabled");
        $("#btnBodegaExterna").attr("disabled", "disabled");
        $("#btnBuscarBodegaExterna").attr("disabled", "disabled");

        $("#btnGuardar").removeAttr("disabled");
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

    $('#txtLoteAlmacen').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtLoteAlmacen').val() == "") {
                $('#btnLoteAlmacen').focus();
            } else {
                ObtenerDatosLote();
            }
        }
    });

    $('#txtLoteProceso').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtLoteProceso').val() == "") {
                $('#btnLoteProceso').focus();
            } else {
                ObtenerDatosLote();
            }
        }
    });

    $('#txtBodegaExterna').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtBodegaExterna').val() == "") {
                $('#btnBodegaExterna').focus();
            } else {
                ObtenerDatosLote();
            }
        }
    });

    // Al cambiar la seleccion de los tipo de lote, limpia el lote anterior y selecciona y habilita los botones que corresponden.
    $('input[name="datosRecepcion"]').change(function (e) {

        switch ($(this).val()) {
            case "rbLoteAlmacen":
                $("#btnLoteAlmacen").removeAttr("disabled");
                $("#btnBuscarLoteAlmacen").removeAttr("disabled");
                $("#txtLoteAlmacen").removeAttr("disabled");
                $("#txtLoteAlmacen").focus();

                $("#txtLoteProceso").attr("disabled", "disabled");
                $("#txtBodegaExterna").attr("disabled", "disabled");
                $("#txtLoteProceso").val("");
                $("#txtBodegaExterna").val("");

                $("#txtBodegaExternaLoteId").val("");
                $("#txtLoteProcesoLoteId").val("");

                $("#btnLoteProceso").attr("disabled", "disabled");
                $("#btnBuscarLoteProceso").attr("disabled", "disabled");
                $("#btnBodegaExterna").attr("disabled", "disabled");
                $("#btnBuscarBodegaExterna").attr("disabled", "disabled");
                break;
            case "rbLoteProceso":
                $("#btnLoteProceso").removeAttr("disabled");
                $("#btnBuscarLoteProceso").removeAttr("disabled");
                $("#txtLoteProceso").removeAttr("disabled");
                $("#txtLoteProceso").focus();

                $("#txtBodegaExterna").attr("disabled", "disabled");
                $("#txtLoteAlmacen").attr("disabled", "disabled");
                $("#txtBodegaExterna").val("");
                $("#txtLoteAlmacen").val("");

                $("#txtBodegaExternaLoteId").val("");
                $("#txtLoteAlmacenLoteId").val("");

                $("#btnLoteAlmacen").attr("disabled", "disabled");
                $("#btnBuscarLoteAlmacen").attr("disabled", "disabled");
                $("#btnBodegaExterna").attr("disabled", "disabled");
                $("#btnBuscarBodegaExterna").attr("disabled", "disabled");
                break;
            case "rbBodegaExterna":
                $("#btnBodegaExterna").removeAttr("disabled");
                $("#btnBuscarBodegaExterna").removeAttr("disabled");
                $("#txtBodegaExterna").removeAttr("disabled");
                $("#txtBodegaExterna").focus();

                $("#txtLoteAlmacen").attr("disabled", "disabled");
                $("#txtLoteProceso").attr("disabled", "disabled");
                $("#txtLoteAlmacen").val("");
                $("#txtLoteProceso").val("");

                $("#txtLoteAlmacenLoteId").val("");
                $("#txtLoteProcesoLoteId").val("");

                $("#btnLoteAlmacen").attr("disabled", "disabled");
                $("#btnBuscarLoteAlmacen").attr("disabled", "disabled");
                $("#btnLoteProceso").attr("disabled", "disabled");
                $("#btnBuscarLoteProceso").attr("disabled", "disabled");
                break;
        }
    });

    // Funcion para obtener los datos del folio de entrada producto.
    var ObtenerEntradaProducto = function () {
        App.bloquearContenedor($(".contenido-patio"));
        $.ajax({
            type: "POST",
            url: "AsignacionDeLoteEnPatio.aspx/ConsultarEntradaProducto",
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
                            }
                            else if (data.d.AlmacenInventarioLote.AlmacenInventarioLoteId == 0)
                            {

                                $("#txtFolio").attr("disabled", "disabled");
                                $("#btnBuscarFolio").attr("disabled", "disabled");
                                $("#txtEntradaProductoId").val(data.d.EntradaProductoId);
                                $("#txtProductoId").val(data.d.Producto.ProductoId);
                                $("#txtProducto").val(data.d.Producto.ProductoDescripcion);
                                $("#txtContrato").val(data.d.Contrato.Folio);
                                $("#txtProveedor").val(data.d.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion);
                                $("#txtPlacas").val(data.d.RegistroVigilancia.CamionCadena);
                                $("#txtChofer").val(data.d.RegistroVigilancia.Chofer);
                                $('#rbLoteAlmacen').focus();

                                if (data.d.Producto.EsPremezcla == true) {
                                    $('#rbBodegaExterna').attr('disabled', true);
                                    $('#rbLoteProceso').attr('disabled', true);
                                }
                            }
                            else { bootbox.alert(window.mensajeElFolioTieneLoteAsignado, Limpiar); }

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

    // Funcion para obtener el lote nuevo el cual se generara dependiendo del tipo de almacen
    var ObtenerNuevoLoteMateriaPrima = function () {

        if ($("#txtProductoId").val() > 0) {
            App.bloquearContenedor($(".contenido-patio"));
            $.ajax({
                type: "POST",
                url: "AsignacionDeLoteEnPatio.aspx/ObtenerNuevoLoteMateriaPrima",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ productoId: $("#txtProductoId").val(), tipoalmacen: $('input[name="datosRecepcion"]:checked').val() }),
                error: function (request) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    bootbox.alert(window.mensajeErrorAlGenerarElNuevoLote);
                },
                success: function (data) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    if (data.d) {
                        switch ($('input[name="datosRecepcion"]:checked').val()) {
                            case "rbLoteAlmacen":
                                $("#txtLoteAlmacen").val(data.d.Lote);
                                $("#txtLoteAlmacenLoteId").val(data.d.AlmacenInventarioLoteId);
                                $("#txtLoteAlmacen").focus();
                                break;
                            case "rbLoteProceso":
                                $("#txtLoteProceso").val(data.d.Lote);
                                $("#txtLoteProcesoLoteId").val(data.d.AlmacenInventarioLoteId);
                                $("#txtLoteProceso").focus();
                                break;
                            case "rbBodegaExterna":
                                $("#txtBodegaExterna").val(data.d.Lote);
                                $("#txtBodegaExternaLoteId").val(data.d.AlmacenInventarioLoteId);
                                $("#txtBodegaExterna").focus();
                                break;

                            default:
                        }
                    } else {
                        bootbox.alert(window.mensajeErrorAlGenerarElNuevoLote, function () {
                            switch ($('input[name="datosRecepcion"]:checked').val()) {
                                case "rbLoteAlmacen":
                                    setTimeout(function () {
                                        $("#txtLoteAlmacen").val(""); $("#txtLoteAlmacen").focus();
                                        $("#txtLoteAlmacenLoteId").val("");
                                    }, 500);
                                    break;
                                case "rbLoteProceso":
                                    setTimeout(function () {
                                        $("#txtLoteProceso").val(""); $("#txtLoteProceso").focus();
                                        $("#txtLoteProcesoLoteId").val("");
                                    }, 500);
                                    break;
                                case "rbBodegaExterna":
                                    setTimeout(function () {
                                        $("#txtBodegaExterna").val(""); $("#txtBodegaExterna").focus();
                                        $("#txtBodegaExternaLoteId").val("");
                                    }, 500);
                                    break;
                            }
                        });
                    }
                }
            });
        } else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido, function () {
                setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
            });
        }
    };

    // Metodo que servida para abrir las ayudas de los lotes, dependiedo del tipo de almacen seleccionado
    var AbrirAyudaLotes = function () {
        if ($("#txtProductoId").val() > 0) {
            App.bloquearContenedor($(".contenido-patio"));
            $.ajax({
                type: "POST",
                url: "AsignacionDeLoteEnPatio.aspx/ObtenerLotesPorTipoAlmacen",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ productoId: $("#txtProductoId").val(), tipoalmacen: $('input[name="datosRecepcion"]:checked').val() }),
                error: function (request) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    bootbox.alert(window.mensajeErrorAlConsultarLotes);
                },
                success: function (data) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    if (data.d) {
                        var resultado = data.d;
                        $("#tbBusquedaLotes tbody").html("");
                        for (var i = 0; i < resultado.length; i++) {
                            $("#tbBusquedaLotes tbody").append("<tr>" +
                                "<td class='alineacionCentro' style='width: 20px;'> <input type='checkbox' class='lotes' id='lote" + resultado[i].Lote + "' lote='" + resultado[i].Lote + "' almaceninventarioloteid='" + resultado[i].AlmacenInventarioLoteId + "' onclick='SeleccionaUnLote(\"#lote" + resultado[i].Lote + "\");'/></td>" +
                                "<td class='alineacionCentro' style='width: 50px;'>" + resultado[i].Lote + "</td>" +
                                "<td class='alineacionCentro' style='width: 50px;'>" + resultado[i].CodigoAlmacen + "</td>" +
                                "<td class='alineacionCentro' style='width: 50px;'>" + resultado[i].Cantidad + "</td>" +
                                "</tr>");
                        }
                        $("#dlgBusquedaLotes").modal("show");
                    } else {
                        bootbox.alert(window.mensajeNoExistenLotes);
                    }
                }
            });
        } else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido, function () {
                setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
            });
        }
    };

    // Metodo que funciona para consultar los folios que estan pendientes de descargar en patio.
    var ObtenerFoliosPendientes = function () {
        App.bloquearContenedor($(".contenido-patio"));
        $.ajax({
            type: "POST",
            url: "AsignacionDeLoteEnPatio.aspx/ConsultarListadoEntradaProducto",
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

    //Selecciona solo un checkbox de los lotes
    SeleccionaUnLote = function (Id) {
        var listaCheckBox = $(".lotes");
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
                url: "AsignacionDeLoteEnPatio.aspx/ConsultarEntradaProducto",
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
    // Método para actualizar el lote a la entrada producto
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
                    url: "AsignacionDeLoteEnPatio.aspx/ActualizarLoteEntradaProducto",
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
                bootbox.alert(window.mensajeEsNecesarioSeleccionarUnLote);
            }
        }
        else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido);
        }
    };

    // Método que consultará si el lote 
    var ObtenerDatosLote = function () {
        if ($("#txtProductoId").val() > 0) {
            var lote = 0;
            switch ($('input[name="datosRecepcion"]:checked').val()) {
                case "rbLoteAlmacen":
                    lote = $("#txtLoteAlmacen").val();
                    break;
                case "rbLoteProceso":
                    lote = $("#txtLoteProceso").val();
                    break;
                case "rbBodegaExterna":
                    lote = $("#txtBodegaExterna").val();
                    break;
            }

            var parametroLoteMateriaPrima = {};
            parametroLoteMateriaPrima.tipoalmacen = $('input[name="datosRecepcion"]:checked').val();
            parametroLoteMateriaPrima.ProductoId = $("#txtProductoId").val();
            parametroLoteMateriaPrima.Lote = lote;

            App.bloquearContenedor($(".contenido-patio"));
            $.ajax({
                type: "POST",
                url: "AsignacionDeLoteEnPatio.aspx/ObtenerLotePorTipoAlmacen",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ parametroLoteMateriaPrima: parametroLoteMateriaPrima }),
                error: function (request) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    bootbox.alert(window.mensajeErrorAlConsultarLotes);
                },
                success: function (data) {
                    App.desbloquearContenedor($(".contenido-patio"));
                    if (data.d) {

                        switch ($('input[name="datosRecepcion"]:checked').val()) {
                            case "rbLoteAlmacen":
                                $("#txtLoteAlmacen").val(data.d.Lote);
                                $("#txtLoteAlmacenLoteId").val(data.d.AlmacenInventarioLoteId);
                                break;
                            case "rbLoteProceso":
                                $("#txtLoteProceso").val(data.d.Lote);
                                $("#txtLoteProcesoLoteId").val(data.d.AlmacenInventarioLoteId);
                                break;
                            case "rbBodegaExterna":
                                $("#txtBodegaExterna").val(data.d.Lote);
                                $("#txtBodegaExternaLoteId").val(data.d.AlmacenInventarioLoteId);
                                break;
                        }
                    } else {
                        bootbox.alert(window.mensajeCapturarLoteValido);
                        switch ($('input[name="datosRecepcion"]:checked').val()) {
                            case "rbLoteAlmacen":
                                $("#txtLoteAlmacen").val("");
                                $("#txtLoteAlmacenLoteId").val("");
                                break;
                            case "rbLoteProceso":
                                $("#txtLoteProceso").val("");
                                $("#txtLoteProcesoLoteId").val("");
                                break;
                            case "rbBodegaExterna":
                                $("#txtBodegaExterna").val("");
                                $("#txtBodegaExternaLoteId").val("");
                                break;
                        }
                    }
                }
            });
        } else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido, function () {
                setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
            });
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