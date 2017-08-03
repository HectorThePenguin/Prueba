
$(document).ready(function () {
    var contenedorAbasto = $(".contenedor-abasto");
    
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 9, "greedy": false, numericInput: true });
    $("#txtTicket").inputmask({ "mask": "9", "repeat": 9, "greedy": false, numericInput: true });
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 9, "greedy": false, numericInput: true });
    $("#txtProveedorCodigoSap").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    $("#txtChoferId").inputmask({ "mask": "9", "repeat": 9, "greedy": false, numericInput: true });
    $("#txtCamionId").inputmask({ "mask": "9", "repeat": 9, "greedy": false, numericInput: true });
    $(".cantidadEntregada").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $(".piezas").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    
    $(".scroller").slimScroll();
    
    // Funciones

    var ValidarAscii = function(code)
    {
        if (code == 13 || code == 9 || code == 42 || code == 43 || code == 44 || code == 45 || code == 46 || code == 47 || code == 229) {
            return true;
        }
        return false;
    };
    
    var Limpiar = function () {
        $("#txtFolio").val("");
        $("#txtTicket").val("");
        $("#tbDetallesProductos tbody").html("");
        $("#txtFolio").focus();
    };
    
    // Acciones
    
    // Evento que evita que se pegue en los input 
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });
    
    // Evento de tecla enter.
    $('#txtFolio').keydown(function (e) {
        var code = e.keyCode || e.which;
        
        if ((code > 47 && code < 58) || code == 8) {
            $("#txtTicket").val("");
            $("#tbDetallesProductos tbody").html("");
        }
        
        if (ValidarAscii(code)) {
            e.preventDefault();
        }
    });
    
    $('#txtTicket').keydown(function (e) {
        var code = e.keyCode || e.which;
        
        if ((code > 47 && code < 58) || code == 8) {
            //$("#txtFolio").val("");
            $("#tbDetallesProductos tbody").html("");
        }
        
        if (ValidarAscii(code)) {
            e.preventDefault();
        }
    });
    
    // Evento Click del boton limpiar
    $("#btnLimpiar").click(function () {
        Limpiar();
    });
    
    // Boton que abre la ayuda de los folios.
    $("#btnBuscarFolio").click(function () {
        ObtenerFolios();
    });

    $("#btnBuscarFolioTicket").click(function () {
        if ($.trim($("#txtFolio").val()) != "") {
            if ($.trim($("#txtTicket").val()) != "") {
                ObtenerDatosTicket();
            } else {
                // Buscar por folio
                ObtenerDatosFolio();
            }
        } else {
            bootbox.alert(window.mensajeTienesQueCapturarUnFolioValido, function () {
                setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
            });
        }
    });
    
    // Funcion para consultar los productos por folio tecleado con estatus programados y parcial
    var ObtenerDatosFolio = function () {
        if ($("#txtFolio").val() > 0) {
            App.bloquearContenedor(contenedorAbasto);
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerPedidoFolio",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ folioPedido: $("#txtFolio").val() }),
                error: function (request) {
                    App.desbloquearContenedor(contenedorAbasto);
                    bootbox.alert(window.mensajeErrorAlConsultarFolio, function() {
                        setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
                    });
                },
                success: function (data) {
                    App.desbloquearContenedor(contenedorAbasto);
                    if (data.d) {
                        CrearGridProductos(data.d);
                    }
                    else {
                        bootbox.alert(window.mensajeFolioInvalido, function () {
                            setTimeout(function () { $("#txtFolio").val(""); $("#txtFolio").focus(); }, 500);
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
    
    // Funcion para consultar los productos por ticket tecleado con estatus programados y parcial
    var ObtenerDatosTicket = function () {
        if ($("#txtTicket").val() > 0) {
            App.bloquearContenedor(contenedorAbasto);
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerPedidoPorTicket",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ticket: $("#txtTicket").val(), folio: $("#txtFolio").val() }),
                error: function (request) {
                    App.desbloquearContenedor(contenedorAbasto);
                    bootbox.alert(window.mensajeErrorAlConsultarTicket, function () {
                        setTimeout(function () { $("#txtTicket").val(""); $("#txtTicket").focus(); }, 500);
                    });
                },
                success: function (data) {
                    App.desbloquearContenedor(contenedorAbasto);
                    if (data.d) {
                        CrearGridProductos(data.d);
                    }
                    else {
                        bootbox.alert(window.mensajeTicketInvalido, function () {
                            setTimeout(function () { $("#txtTicket").val(""); $("#txtTicket").focus(); }, 500);
                        });
                    }
                }
            });
        } else {
            bootbox.alert(window.mensajeTienesQueCapturarUnTicketValido, function () {
                setTimeout(function () { $("#txtTicket").val(""); $("#txtTicket").focus(); }, 500);
            });
        }
    };
    
    // Funcion que genera el grid para el detalle de productos
    CrearGridProductos = function (datosProductos) {
        $("#tbDetallesProductos tbody").html("");
        for (var i = 0; i < datosProductos.length; i++) {
            var contenido = '<tr>' +
                '<td class="colProducto" >' + datosProductos[i].Producto.ProductoDescripcion + '</td>' +
                '<td class="colCantidades textoDerecha">' + accounting.formatNumber(datosProductos[i].CantidadSolicitada) + '</td>' +
                '<td class="colCantidades textoDerecha">' + accounting.formatNumber(datosProductos[i].CantidadEntregada) + '</td>' +
                '<td class="colCantidades textoDerecha">' + accounting.formatNumber(datosProductos[i].CantidadPendiente) + '</td>' +
                '<td class="colLoteProceso textoDerecha">' + datosProductos[i].LoteProceso.Lote + '</td>';

            if (datosProductos[i].CantidadPendiente > 0) {
                contenido +=
                    '<td class="colOpcionEditar alineacionCentro">' +
                        '<button type="button" class="btn btnSolicitudMateriaPrima SuKarne" onclick="SolicitudMateriaPrima(' + datosProductos[i].PedidoDetalleId + ')"><i class="icon-edit"></i></button>' +
                        '</td>';
            } else {
                contenido +=
                    '<td class="colOpcionEditar alineacionCentro">' +
                        '<button type="button" class="btn btnSolicitudMateriaPrima SuKarne" disabled="disabled"><i class="icon-edit"></i></button>' +
                        '</td>';
            }
            contenido += '</tr>';

            $("#tbDetallesProductos tbody").append(contenido);
        }
    };
    
    // ******************  Pantalla Solicitud de materia prima  ********************* //
    
    // Limpiar pantalla de solicitud.
    var LimpiarSolicitud = function () {
        $("#txtPesajeMateriaPrimaId").val("0");
        $("#txtSubFamilia").val("");
        $("#txtProducto").val("");
        $("#txtProductoId").val("");
        $("#txtCantidadSolicitada").val("");
        $("#txtProveedor").val("");
        $("#txtProveedorId").val("0");
        $("#txtProveedorCodigoSap").val("");
        $("#txtChoferValido").val("0");
        $("#txtChofer").val("");
        $("#txtChoferId").val("");
        $("#txtPlacaValida").val("0");
        $("#txtCamionId").val("");
        $("#txtPlaca").val("");
        $("#tablaProgramacion tbody").html("");
        $("#tabProducto").click();
        
        $("#txtProveedorCodigoSap").removeAttr("disabled");
        $("#txtChoferId").removeAttr("disabled");
        $("#txtCamionId").removeAttr("disabled");
        $("#btnAyudaProveedores").removeAttr("disabled");
        $("#btnAyudaChoferes").removeAttr("disabled");
        $("#btnAyudaPlacas").removeAttr("disabled");
        $("#txtPedidoDetalleId").val("");
    };
    
    //Boton Ayuda Proveedor
    $("#btnAyudaProveedores").click(function () {
        $("#dlgSolicitudMateriaPrima").modal("hide");
        AbrirAyudaProveedores();
    });

    //Boton Ayuda Chofer
    $("#btnAyudaChoferes").click(function () {
        $("#dlgSolicitudMateriaPrima").modal("hide");
        AbrirAyudaChoferes();
    });
    
    //Boton Ayuda Placas
    $("#btnAyudaPlacas").click(function () {
        $("#dlgSolicitudMateriaPrima").modal("hide");
        AbrirAyudaPlacas();
    });
    
    // Limpiamos el Proveedor.
    LimpiarEdicionProveerdor = function () {
        $('#txtProveedorId').val("");
        $('#txtProveedor').val("");
        $('#txtProveedorCodigoSap').val("");
    };
    
    // Limpiamos el Chofer
    LimpiarEdicionChofer = function () {
        $('#txtChoferValido').val("0");
        $('#txtChofer').val("");
        $('#txtChoferId').val("");
    };

    // Limpiamos la Placa
    LimpiarEdicionPlaca = function () {
        $('#txtPlacaValida').val("0");
        $('#txtCamionId').val("");
        $('#txtPlaca').val("");
    };
    
    // Enter en el Proveedor
    $('#txtProveedorCodigoSap').keydown(function (e) {
        var code = e.keyCode || e.which;
        
        if ((code > 47 && code < 58) || code == 8 || (code > 64 && code < 91)) {
            $('#txtProveedorId').val("0");
            $('#txtProveedor').val("");
            LimpiarEdicionChofer();
            LimpiarEdicionPlaca();
        }
        
        if (code == 13 || code == 9) {
            if ($('#txtProveedorCodigoSap').val() == "") {
                $('#btnAyudaProveedores').focus();
            } else {
                ObtenerPorProveedorId();
            }
            e.preventDefault();
        }
    });

    // Enter en el Chofer
    $('#txtChoferId').keydown(function (e) {
        var code = e.keyCode || e.which;
        
        if ((code > 47 && code < 58) || code == 8) {
            $('#txtChoferValido').val("0");
            $('#txtChofer').val("");
        }
        
        if (ValidarAscii(code)) {
            e.preventDefault();
        }
        
        if (code == 13 || code == 9) {
            if ($('#txtChoferId').val() == "") {
                $('#btnAyudaChoferes').focus();
            } else {
                ObtenerPorChoferId();
            }
            e.preventDefault();
        }
    });
    
    // Enter en la Placa
    $('#txtCamionId').keydown(function (e) {
        var code = e.keyCode || e.which;
        
        if ((code > 47 && code < 58) || code == 8) {
            $('#txtPlacaValida').val("0");
            $('#txtPlaca').val("");
        }
        
        if (ValidarAscii(code)) {
            e.preventDefault();
        }
        
        if (code == 13 || code == 9) {
            if ($('#txtCamionId').val() == "") {
                $('#btnAyudaPlacas').focus();
            } else {
                ObtenerPorPlacaId();
            }
            e.preventDefault();
        }
    });
    
    // Boton que cancela la pantalla de Solicitud de Materia Prima
    $("#btnCancelarProgramacion").click(function () {
        $("#dlgSolicitudMateriaPrima").modal("hide");
        bootbox.dialog({
            message: window.MensajeCancelar,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        LimpiarSolicitud();
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                    }
                }
            }
        });
    });

    // Evento Click del boton limpiar
    $("#btnGuardarProgramacion").click(function () {
        $("#btnGuardarProgramacion").attr("disabled", "disabled");
        $("#btnCancelarProgramacion").attr("disabled", "disabled");
        
        var pesajeMateriaPrima = {};
        var programacionMateriaPrima = {};
        var proveedorInfo = {};
        var choferInfo = {};
        var proveedorChoferInfo = {};
        var inventarioLoteOrigen = {};
        var almacenInfo = {};
        
        var cantidadEntregada = 0, piezas = 0, programacionMateriaPrimaId = 0, almacenId = 0, inventarioLoteId = 0;
        var cantidadProgramada = 0;
        var cantidadYaEntregada = 0, piezasLote = 0;
        var justificacion = "";
        var continuarGuardar = false, validarCantidad = false;
        
        // Verificar el renglon que esta seleccionado.
        $(".lotesprogramacion:checked").each(function() {
            
            var id = $(this).attr("programacion");
            programacionMateriaPrimaId = $(this).attr("programacion");
            almacenId = $(this).attr("almacenid");
            inventarioLoteId = $(this).attr("inventarioloteid");
            cantidadProgramada = $(this).attr("cantidadProgramada");
            cantidadYaEntregada = $(this).attr("cantidadyaentregada");
            piezasLote = $(this).attr("piezas");
            
            // Si no esta activo ninguno de los dos campos Cantidad Entregada y Piezas se permite que se guarde.
            if ($("#cantidadentregada" + id).length == 0 && $("#piezas" + id).length == 0) {
                continuarGuardar = true;
            }
            
            // Verifico que existan los controles.
            if ($("#cantidadentregada" + id).length > 0) {
                cantidadEntregada = $("#cantidadentregada" + id).val().replace(/,/g, '').replace(/_/g, '');
                if (cantidadEntregada != "" && cantidadEntregada != 0) {
                    continuarGuardar = true;
                    validarCantidad = true;
                }
            } else {
                continuarGuardar = true;
            }
            
            if ($("#justificacion" + id).length > 0) {
                justificacion = $("#justificacion" + id).val();
            }
            
            if (continuarGuardar) {
                
                if ($("#piezas" + id).length > 0) {
                    continuarGuardar = false;
                    piezas = $("#piezas" + id).val().replace(/,/g, '').replace(/_/g, '');
                    if (piezas != "" && piezas != 0)
                    {
                        continuarGuardar = true;
                    }
                }
            }
        });

        if (continuarGuardar)
        {
            if (parseInt(piezas) > parseInt(piezasLote)) {
                $("#dlgSolicitudMateriaPrima").modal("hide");
                bootbox.alert(window.mensajePiezasMayorALote, function () {
                    $("#dlgSolicitudMateriaPrima").modal("show");
                    setTimeout(function () {
                        $("#piezas" + programacionMateriaPrimaId).val("");
                        $("#piezas" + programacionMateriaPrimaId).focus();
                        
                        $("#btnGuardarProgramacion").attr("disabled", false);
                        $("#btnCancelarProgramacion").attr("disabled", false);
                    }, 500);
                });
            }
            else if (parseInt(cantidadEntregada) == 0 && validarCantidad) {
                $("#dlgSolicitudMateriaPrima").modal("hide");
                bootbox.alert(window.mensajeCantidadEntregadaCero, function () {
                    $("#dlgSolicitudMateriaPrima").modal("show");
                    setTimeout(function () {
                        $("#cantidadentregada" + programacionMateriaPrimaId).val("");
                        $("#cantidadentregada" + programacionMateriaPrimaId).focus();
                        
                        $("#btnGuardarProgramacion").attr("disabled", false);
                        $("#btnCancelarProgramacion").attr("disabled", false);
                    }, 500);
                });
            }
            else if ((parseInt(cantidadEntregada) + parseInt(cantidadYaEntregada)) > parseInt(cantidadProgramada)) {
                $("#dlgSolicitudMateriaPrima").modal("hide");
                bootbox.alert(window.mensajeCantidadEntregadaMayorAProgramada, function() {
                    $("#dlgSolicitudMateriaPrima").modal("show");
                    setTimeout(function() {
                        $("#cantidadentregada" + programacionMateriaPrimaId).val("");
                        $("#cantidadentregada" + programacionMateriaPrimaId).focus();
                        
                        $("#btnGuardarProgramacion").attr("disabled", false);
                        $("#btnCancelarProgramacion").attr("disabled", false);
                    }, 500);
                });
            } else {
                // Se obtienen los datos de los proveedores y del chofer
                proveedorInfo.ProveedorID = $("#txtProveedorId").val();
                if (proveedorInfo.ProveedorID == "") {
                    proveedorInfo.ProveedorID = 0;
                }
                choferInfo.choferID = $("#txtChoferId").val();
                if (choferInfo.choferID == "") {
                    choferInfo.choferID = 0;
                }

                proveedorChoferInfo.Proveedor = proveedorInfo;
                proveedorChoferInfo.Chofer = choferInfo;

                pesajeMateriaPrima.PesajeMateriaPrimaID = $("#txtPesajeMateriaPrimaId").val();
                if ($("#txtTicket").val() > 0) {
                    pesajeMateriaPrima.Ticket = $("#txtTicket").val();
                }
                pesajeMateriaPrima.ProgramacionMateriaPrimaID = programacionMateriaPrimaId;
                pesajeMateriaPrima.ProveedorChofer = proveedorChoferInfo;
                pesajeMateriaPrima.CamionID = $("#txtCamionId").val();
                if (pesajeMateriaPrima.CamionID == "") {
                    pesajeMateriaPrima.CamionID = 0;
                }
                pesajeMateriaPrima.Piezas = piezas;

                programacionMateriaPrima.PedidoDetalleID = $("#txtPedidoDetalleId").val();
                programacionMateriaPrima.ProgramacionMateriaPrimaID = programacionMateriaPrimaId;
                programacionMateriaPrima.CantidadEntregada = cantidadEntregada;
                // SE ASIGNARA AL PESO BRUTO LA CANTIDAD ENTREGADA
                pesajeMateriaPrima.PesoBruto = cantidadEntregada;
                programacionMateriaPrima.Justificacion = justificacion;

                almacenInfo.AlmacenID = almacenId;
                programacionMateriaPrima.Almacen = almacenInfo;

                inventarioLoteOrigen.AlmacenInventarioLoteId = inventarioLoteId;
                programacionMateriaPrima.InventarioLoteOrigen = inventarioLoteOrigen;

                var urlProgramacion;
                if ($("#txtFolio").val() != "" && $("#txtFolio").val() > 0 && $("#txtTicket").val() == "") {
                    urlProgramacion = "AbastoDeMateriaPrima.aspx/CrearPesajeMateriaPrima";
                } else {
                    urlProgramacion = "AbastoDeMateriaPrima.aspx/ActualizarPesajeMateriaPrimaTicket";
                }

                $.ajax({
                    type: "POST",
                    url: urlProgramacion,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ pesajeMateriaPrima: pesajeMateriaPrima, programacionMateriaPrima: programacionMateriaPrima, pedido: $("#txtFolio").val() }),
                    error: function (request) {

                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensjaeErrorAlGuardar, function() {
                            $("#dlgSolicitudMateriaPrima").modal("show");

                            $("#btnGuardarProgramacion").attr("disabled", false);
                            $("#btnCancelarProgramacion").attr("disabled", false);
                        });
                    },
                    success: function (data) {
                        $("#btnGuardarProgramacion").attr("disabled", false);
                        $("#btnCancelarProgramacion").attr("disabled", false);
                        
                        $("#dlgSolicitudMateriaPrima").modal("hide");

                        if ($("#txtTicket").val() > 0) {
                            bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.MensajeDatosGuardadosConExito, Limpiar);
                        } else {
                            bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.MensajeDatosGuardadosConExito, ObtenerDatosFolio);
                        }
                    }
                });
            }
        } else {
            $("#dlgSolicitudMateriaPrima").modal("hide");
            bootbox.alert(window.mensajeExistenDatosEnBlanco, function() {
                $("#dlgSolicitudMateriaPrima").modal("show");
                setTimeout(function () {
                    if ($("#txtCamionId").val() == 0) {
                        $("#tabProducto").click();
                        $("#txtCamionId").focus();
                    }
                    if ($("#txtChoferId").val() == 0) {
                        $("#tabProducto").click();
                        $("#txtChoferId").focus();
                    }
                    if ($("#txtProveedorId").val() == 0) {
                        $("#tabProducto").click();
                        $("#txtProveedorCodigoSap").focus();
                    }
                    if ($("#cantidadentregada" + programacionMateriaPrimaId).length > 0 && cantidadEntregada == 0) {
                        $("#tabSurtir").click();
                        $("#cantidadentregada" + programacionMateriaPrimaId).val();
                        $("#cantidadentregada" + programacionMateriaPrimaId).focus();
                    }
                    if ($("#piezas" + programacionMateriaPrimaId).length > 0 && piezas == 0) {
                        $("#tabSurtir").click();
                        $("#piezas" + programacionMateriaPrimaId).val();
                        $("#piezas" + programacionMateriaPrimaId).focus();
                    }
                    
                    $("#btnGuardarProgramacion").attr("disabled", false);
                    $("#btnCancelarProgramacion").attr("disabled", false);
                    
                }, 500);
            });
        }
    });
    
    //Abre la pantalla de Solicitud de materia prima
    SolicitudMateriaPrima = function (idDetallePedido) {
        var urlProgramacion = "", jsonDatos;
        if ($.trim($("#txtFolio").val()) != "" && $("#txtFolio").val() > 0 && $.trim($("#txtTicket").val()) == "") {
            urlProgramacion = "AbastoDeMateriaPrima.aspx/ObtenerProgramacionPedidoDetalle";
            jsonDatos = { folioDetallePedido: idDetallePedido, folioPedido: $("#txtFolio").val() };
        } else if($.trim($("#txtFolio").val()) != "" && $("#txtFolio").val() > 0 && $.trim($("#txtTicket").val()) != "" && $.trim($("#txtTicket").val()) > 0) {
            urlProgramacion = "AbastoDeMateriaPrima.aspx/ObtenerProgramacionPedidoDetalleTicket";
            jsonDatos = { folioDetallePedido: idDetallePedido, ticket: $("#txtTicket").val() };
        }

        if (urlProgramacion != "") {
            $.ajax({
                type: "POST",
                url: urlProgramacion,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonDatos),
                error: function(request) {
                    bootbox.alert(window.mensajeErrorAlConsultarProgramacion);
                    $("#txtPedidoDetalleId").val("");
                },
                success: function(data) {
                    if (data.d) {
                        if (data.d.ProgramacionMateriaPrima) {

                            LimpiarSolicitud();

                            $("#txtPedidoDetalleId").val(idDetallePedido);
                            $("#txtSubFamilia").val(data.d.Producto.SubFamilia.Descripcion);
                            $("#txtProductoId").val(data.d.Producto.ProductoId);
                            $("#txtProducto").val(data.d.Producto.ProductoDescripcion);
                            $("#txtCantidadSolicitada").val(accounting.formatNumber(data.d.CantidadSolicitada));

                            var resultado = data.d.ProgramacionMateriaPrima;

                            $("#tablaProgramacion tbody").html("");
                            for (var i = 0; i < resultado.length; i++) {
                                var piezasPesaje = 0;

                                var renglones = "<tr><td class='alineacionCentro' style='width: 5%;'><input type='checkbox' class='lotesprogramacion' id='lote" + resultado[i].ProgramacionMateriaPrimaId + "' cantidadyaentregada ='" + resultado[i].CantidadEntregada + "' cantidadProgramada ='" + resultado[i].CantidadProgramada + "' almacenId='" + resultado[i].Almacen.AlmacenID + "' programacion='" + resultado[i].ProgramacionMateriaPrimaId + "' inventarioloteid='" + resultado[i].InventarioLoteOrigen.AlmacenInventarioLoteId + "' piezas='"+resultado[i].InventarioLoteOrigen.Piezas+"' onclick='SeleccionaUnLote(\"" + resultado[i].ProgramacionMateriaPrimaId + "\");'/></td>" +
                                    '<td class="textoDerecha" style="width: 20%;"><div style="width:90%;">' + accounting.formatNumber(resultado[i].CantidadProgramada) + '</div></td>' +
                                    '<td class="textoDerecha" style="width: 20%;"><div style="width:90%;">' + resultado[i].InventarioLoteOrigen.Lote + '</div></td>';

                                if ($("#txtFolio").val() != "" && $("#txtFolio").val() > 0 && $.trim($("#txtTicket").val()) == "") {
                                    renglones +=
                                        '<td class="alineacionCentro" style="width: 20%;"><input type="tel" class="cantidadEntregada" id="cantidadentregada' + resultado[i].ProgramacionMateriaPrimaId + '" value="" style="width:90%;" disabled /></td>';
                                } else {
                                    renglones +=
                                        '<td class="textoDerecha" style="width: 20%;">' + accounting.formatNumber(resultado[i].CantidadEntregada) + '</td>';

                                    var pesajes = resultado[i].PesajeMateriaPrima;
                                    
                                    for (var j = 0; j < pesajes.length; j++) {
                                        $("#txtPesajeMateriaPrimaId").val(pesajes[j].PesajeMateriaPrimaID);
                                        $("#txtProveedorId").val(pesajes[j].ProveedorChofer.Proveedor.ProveedorID);
                                        $("#txtProveedorCodigoSap").val(pesajes[j].ProveedorChofer.Proveedor.CodigoSAP);
                                        $("#txtProveedor").val(pesajes[j].ProveedorChofer.Proveedor.Descripcion);

                                        $("#txtChoferId").val(pesajes[j].ProveedorChofer.Chofer.ChoferID);
                                        $("#txtChofer").val(pesajes[j].ProveedorChofer.Chofer.NombreCompleto);

                                        $("#txtCamionId").val(pesajes[j].Camion.CamionID);
                                        $("#txtPlaca").val(pesajes[j].Camion.PlacaCamion);
                                        piezasPesaje += pesajes[j].Piezas;
                                    }
                                    $("#txtProveedorCodigoSap").attr("disabled", "disabled");
                                    $("#txtChoferId").attr("disabled", "disabled");
                                    $("#txtCamionId").attr("disabled", "disabled");
                                    $("#btnAyudaProveedores").attr("disabled", "disabled");
                                    $("#btnAyudaChoferes").attr("disabled", "disabled");
                                    $("#btnAyudaPlacas").attr("disabled", "disabled");
                                }

                                renglones +=
                                    '<td class="alineacionCentro" style="width: 20%;"><textarea class="justificacion" id="justificacion' + resultado[i].ProgramacionMateriaPrimaId + '" value="" style="width:90%; height:100%; resize:none;" disabled></textarea></td>';

                                // Se activan las piezas si el producto es forraje o si la subfamilia es MicroIngredientes
                                if (data.d.Producto.Forraje || data.d.Producto.EsPremezcla) {
                                    renglones += '<td class="alineacionCentro" style="width: 15%;"><input type="tel" class="piezas" id="piezas' + resultado[i].ProgramacionMateriaPrimaId + '" value="" style="width:80%;" disabled /></td>';
                                } else {
                                    renglones += '<td class="textoDerecha" style="width: 15%;">' + accounting.formatNumber(piezasPesaje) + '</td>';
                                }

                                renglones += '</tr>';

                                $("#tablaProgramacion tbody").append(renglones);
                            }

                            $(".cantidadEntregada").inputmask('integer', { repeat: "9", groupSeparator: ",", groupSize: 3, autoGroup: true, numericInput: true });
                            $(".piezas").inputmask('integer', { repeat: "9", groupSeparator: ",", groupSize: 3, autoGroup: true, numericInput: true });

                            $(".justificacion").alphanum({ allow: "., " });
                            $(".justificacion").keydown(function(e) {
                                if ($(this).val().length > 254) {
                                    $(this).val($(this).val().substring(0, 254));
                                    return false;
                                }
                                return true;
                            });

                            $(".cantidadEntregada").keydown(function(e) {
                                var code = e.keyCode || e.which;
                                if (code == 37 || code == 38 || code == 39 || code == 40) {
                                    e.preventDefault();
                                }
                                if (ValidarAscii()) {
                                    e.preventDefault();
                                }
                            });

                            $(".piezas").keydown(function(e) {
                                var code = e.keyCode || e.which;
                                if (code == 37 || code == 38 || code == 39 || code == 40) {
                                    e.preventDefault();
                                }
                                if (ValidarAscii()) {
                                    e.preventDefault();
                                }
                            });

                            $("#dlgSolicitudMateriaPrima").modal("show");

                            if ($("#txtFolio").val() != "" && $("#txtFolio").val() > 0 && $.trim($("#txtTicket").val()) == "") {
                                $("#tabProducto").hide();
                                setTimeout(function () { $("#tabSurtir").trigger("click"); }, 500);
                            } else {
                                $("#tabProducto").show();
                            }

                        } else {
                            bootbox.alert(window.mensajeNoTieneProgramacion, function() {
                                setTimeout(function() {
                                    $("#txtFolio").focus();
                                    $("#txtPedidoDetalleId").val("");
                                }, 500);
                            });
                        }
                    } else {
                        bootbox.alert(window.mensajeNoTieneProgramacion, function() {
                            setTimeout(function() {
                                $("#txtFolio").focus();
                                $("#txtPedidoDetalleId").val("");
                            }, 500);
                        });
                    }
                }
            });
        }
    };

    //Selecciona solo un checkbox
    SeleccionaUnLote = function (Id) {
        
        var listaCheckBox = $(".lotesprogramacion");
        var listaCantidadEntregada = $(".cantidadEntregada");
        var listaPiezas = $(".piezas");
        var listaJustificacion = $(".justificacion");
        
        var checkbox = $("#lote" + Id);
        var cantidadEntregada = $("#cantidadentregada" + Id);
        var piezas = $("#piezas" + Id);
        var justificacion = $("#justificacion" + Id);
        
        if (checkbox.is(":checked")) {
            listaCheckBox.each(function() {
                this.checked = false;
            });
            listaCantidadEntregada.each(function() {
                $(this).attr("disabled", "disabled");
                $(this).val("");
            });
            listaJustificacion.each(function() {
                $(this).attr("disabled", "disabled");
                $(this).val("");
            });
            listaPiezas.each(function() {
                $(this).attr("disabled", "disabled");
                $(this).val("");
            });
            checkbox.attr("checked", true);
            cantidadEntregada.removeAttr("disabled");
            piezas.removeAttr("disabled");
            justificacion.removeAttr("disabled");
        } else {
            checkbox.attr("checked", false);
            cantidadEntregada.attr("disabled", "disabled");
            piezas.attr("disabled", "disabled");
            justificacion.attr("disabled", "disabled");
        }
    };
    
    // ******************  ********************  ********************** //
    
    // ******************  Pantalla Ayuda Proveedores  ********************* //
    
    // Boton que cancela la busqueda de Proveedores
    $("#btnCancelarAyudaProveedor").click(function () {
        $("#dlgAyudaProveedores").modal("hide");
        bootbox.dialog({
            message: window.mensajeCancelarAyudaTransportista,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        $("#txtDescripcionProveedorAyuda").val("");
                        setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgAyudaProveedores").modal("show");
                        setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); }, 500);
                    }
                }
            }
        });
    });
    
    // Boton que agrega el Proveedor
    $("#btnAgregarAyudaProveedor").click(function () {
        var renglones = $("input[class=proveedor]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtProveedorId").val($(this).attr("proveedor"));
                $("#txtProveedor").val($(this).attr("nombre"));
                $("#txtProveedorCodigoSap").val($(this).attr("codigosap"));
                $("#btnAyudaChoferes").focus();
                LimpiarEdicionPlaca();
                LimpiarEdicionChofer();
            });
            $("#dlgAyudaProveedores").modal("hide");
            $("#dlgSolicitudMateriaPrima").modal("show");
            $("#txtDescripcionProveedorAyuda").val("");
        } else {
            $("#dlgAyudaProveedores").modal("hide");
            bootbox.alert(window.mensajeSeleccionarTransportista, function () {
                $("#dlgAyudaProveedores").modal("show");
                setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); }, 500);
            });
        }
    });
    
    $("#btnBuscarAyudaProveedor").click(function () {
        ObtenerPorProveedor();
    });
    
    $('#txtDescripcionProveedorAyuda').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            ObtenerPorProveedor();
            e.preventDefault();
        }
    });
    
    AbrirAyudaProveedores = function () {
        
        var proveedorInfo = {};
        proveedorInfo.CodigoSAP = "";
        proveedorInfo.Descripcion = "";
        
        $.ajax({
            type: "POST",
            url: "AbastoDeMateriaPrima.aspx/ObtenerProveedoresFleteros",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ proveedor: proveedorInfo, productoId: $("#txtProductoId").val() }),
            error: function (request) {
                $("#dlgSolicitudMateriaPrima").modal("hide");
                bootbox.alert(window.mensajeErrorAlConsultarTransportistas, function () {
                    $("#dlgSolicitudMateriaPrima").modal("show");
                    setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                });
            },
            success: function (data) {
                if (data.d)
                {
                    if (data.d.length > 0) {
                        CrearGridProveedores(data.d);
                        $("#dlgAyudaProveedores").modal("show");
                        setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); $("#txtDescripcionProveedorAyuda").val(""); }, 500);
                    }
                    else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoHayTransportistas, function () {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                            setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                        });
                    }
                }
                else {
                    $("#dlgSolicitudMateriaPrima").modal("hide");
                    bootbox.alert(window.mensajeNoHayTransportistas, function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                    });
                }
            }
        });
    };
    
    ObtenerPorProveedor = function () {
        
        var proveedorInfo = {};
        proveedorInfo.Descripcion = $("#txtDescripcionProveedorAyuda").val();
        proveedorInfo.CodigoSAP = "";
        
        $.ajax({
            type: "POST",
            url: "AbastoDeMateriaPrima.aspx/ObtenerProveedoresFleteros",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ proveedor: proveedorInfo, productoId: $("#txtProductoId").val() }),
            error: function (request) {
                $("#dlgAyudaProveedores").modal("hide");
                bootbox.alert(window.mensajeErrorAlConsultarTransportistas, function () {
                    $("#dlgAyudaProveedores").modal("show");
                    setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); $("#txtDescripcionProveedorAyuda").val(""); }, 500);
                });
            },
            success: function (data) {
                if(data.d)
                {
                    if (data.d.length > 0) {
                        CrearGridProveedores(data.d);
                    } else {
                        $("#dlgAyudaProveedores").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroElTransportista, function () {
                            $("#dlgAyudaProveedores").modal("show");
                            setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); $("#txtDescripcionProveedorAyuda").val(""); }, 500);
                        });
                    }
                }
                else {
                    $("#dlgAyudaProveedores").modal("hide");
                    bootbox.alert(window.mensajeNoSeEncontroElTransportista, function () {
                        $("#dlgAyudaProveedores").modal("show");
                        setTimeout(function () { $("#txtDescripcionProveedorAyuda").focus(); $("#txtDescripcionProveedorAyuda").val(""); }, 500);
                    });
                }
            }
        });
        
    };

    ObtenerPorProveedorId = function () {
        if ($("#txtProveedorCodigoSap").val() != "") {

            var proveedorInfo = {};
            proveedorInfo.CodigoSAP = $("#txtProveedorCodigoSap").val();
            proveedorInfo.Descripcion = "";
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerProveedoresFleteros",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ proveedor: proveedorInfo, productoId: $("#txtProductoId").val() }),
                error: function (request) {
                    $("#dlgAyudaProveedores").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarTransportistas, function () {
                        $("#dlgAyudaProveedores").modal("show");
                        setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                    });
                },
                success: function (data) {
                    if(data.d){
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                $("#txtProveedor").val(data.d[i].Descripcion);
                                $("#txtProveedorId").val(data.d[i].ProveedorID);
                                $("#txtChoferId").focus();
                            }
                        } else {
                            $("#dlgSolicitudMateriaPrima").modal("hide");
                            bootbox.alert(window.mensajeNoSeEncontroElTransportista, function () {
                                $("#dlgSolicitudMateriaPrima").modal("show");
                                setTimeout(function() { $("#txtProveedorCodigoSap").focus(); }, 500);
                                $('#txtProveedorId').val("");
                                $('#txtProveedor').val("");
                                LimpiarEdicionChofer();
                                LimpiarEdicionPlaca();
                            });
                        }
                    } else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroElTransportista, function () {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                            setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                            $('#txtProveedorId').val("");
                            $('#txtProveedor').val("");
                            LimpiarEdicionChofer();
                            LimpiarEdicionPlaca();
                        });
                    }
                }
            });
        } else {
            $("#dlgAyudaProveedores").modal("hide");
            bootbox.alert(window.mensajeFavorCapturaTransportista, function () {
                $("#dlgAyudaProveedores").modal("show");
                setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
            });
        }
    };
    
    // Funcion que genera el grid en la ayuda de proveedores
    CrearGridProveedores = function(datosProveedores) {
        $("#tablaProveedores tbody").html("");
        for (var i = 0; i < datosProveedores.length; i++) {
            $("#tablaProveedores tbody").append("<tr>" +
                "<td class='colCheckBox alineacionCentro'><input type='checkbox' class='proveedor' id='proveedor" + datosProveedores[i].ProveedorID + "' proveedor='" + datosProveedores[i].ProveedorID + "' nombre='" + datosProveedores[i].Descripcion + "' codigosap='" + datosProveedores[i].CodigoSAP + "' onclick='SeleccionaUnProveedor(\"#proveedor" + datosProveedores[i].ProveedorID + "\");'/></td>" +
                '<td class="colClave textoDerecha">' + datosProveedores[i].CodigoSAP + '</td>' +
                '<td class="colDescripcion">' + datosProveedores[i].Descripcion + '</td>' +
                '</tr>');
        }
    };
    
    //Selecciona solo un checkbox
    SeleccionaUnProveedor = function (Id) {
        var listaCheckBox = $(".proveedor");
        var checkbox = $(Id);
        if (checkbox.is(":checked")) {
            listaCheckBox.each(function () {
                this.checked = false;
            });
            checkbox.attr("checked", true);
        }
    };
    
    // ******************  ********************  ********************** //

    // ******************  Pantalla Ayuda Folios  ********************* //
    
    $('#txtFolioBuscar').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            ObtenerFolioTecleado();
            e.preventDefault();
        }
        if (ValidarAscii()) {
            e.preventDefault();
        }
    });
    
    // Boton que cancela la busqueda de folios
    $("#btnCancelarAyudaFolio").click(function () {
        $("#dlgBusquedaFolio").modal("hide");
        bootbox.dialog({
            message: window.MensajeCancelarAyudaFolio,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#txtFolioBuscar").val("");
                        setTimeout(function () { $("#txtFolio").focus(); }, 500);
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgBusquedaFolio").modal("show");
                        setTimeout(function () { $("#txtFolioBuscar").focus(); }, 500);
                    }
                }
            }
        });
    });

    // Boton que agrega el folio
    $("#btnAgregarAyudaFolio").click(function () {
        var renglones = $("input[class=folios]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtFolio").val($(this).attr("folio"));
                $("#txtFolio").focus();
            });
            $("#dlgBusquedaFolio").modal("hide");
            ObtenerDatosFolio();
            $("#txtTicket").val("");
        } else {
            $("#dlgBusquedaFolio").modal("hide");
            bootbox.alert(window.mensajeSeleccionarFolio, function () {
                $("#dlgBusquedaFolio").modal("show");
                setTimeout(function () { $("#txtFolioBuscar").focus(); }, 500);
            });
        }
    });

    $("#btnBuscarAyudaFolio").click(function () {
        ObtenerFolioTecleado();
    });
    
    // Funcion para consultar los folios programados y parcial
    var ObtenerFolios = function () {
        App.bloquearContenedor(contenedorAbasto);
        $.ajax({
            type: "POST",
            url: "AbastoDeMateriaPrima.aspx/ObtenerFolios",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folioPedido: 0 }),
            error: function (request) {
                App.desbloquearContenedor(contenedorAbasto);
                bootbox.alert(window.mensajeErrorAlConsultarLosFolios);
            },
            success: function (data) {
                App.desbloquearContenedor(contenedorAbasto);
                if (data.d)
                {
                    CrearGridFolios(data.d);
                    $("#dlgBusquedaFolio").modal("show");
                    setTimeout(function() {
                        $("#txtFolioBuscar").focus();
                        $("#txtFolioBuscar").val("");
                    }, 500);
                }
                else {
                    bootbox.alert(window.mensajeNoSeEncontraronFolios, function() {
                        setTimeout(function () { $("#txtFolio").focus(); $("#txtFolio").val(""); }, 500);
                    });
                }
            }
        });
    };
    
    // Funcion para consultar los folios tecleado con estatus programados y parcial
    var ObtenerFolioTecleado = function () {

        var folioBuscar = 0;
        
        if ($("#txtFolioBuscar").val() > 0) {
            folioBuscar = $("#txtFolioBuscar").val();
        }
        
        $.ajax({
            type: "POST",
            url: "AbastoDeMateriaPrima.aspx/ObtenerFolios",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folioPedido: folioBuscar }),
            error: function (request) {
                $("#dlgBusquedaFolio").modal("hide");
                bootbox.alert(window.mensajeErrorAlConsultarFolio, function () {
                    $("#dlgBusquedaFolio").modal("show");
                    setTimeout(function () { $("#txtFolioBuscar").focus(); $("#txtFolioBuscar").val(""); }, 500);
                });
            },
            success: function (data) {
                if (data.d)
                {
                    CrearGridFolios(data.d);
                }
                else {
                    $("#dlgBusquedaFolio").modal("hide");
                    bootbox.alert(window.mensajeFolioInvalido, function () {
                        $("#dlgBusquedaFolio").modal("show");
                        setTimeout(function () { $("#txtFolioBuscar").focus(); $("#txtFolioBuscar").val(""); }, 500);
                    });
                }
            }
        });
    };

    // Funcion que crea los folios 
    CrearGridFolios = function (folios) {
        $("#gridFoliosProductos tbody").html("");
        for (var i = 0; i < folios.length; i++) {
            $("#gridFoliosProductos tbody").append("<tr>" +
                "<td class='colCheckBox alineacionCentro'><input type='checkbox' class='folios' id='folio" + folios[i].FolioPedido + "' folio='" + folios[i].FolioPedido + "' onclick='SeleccionaUno(\"#folio" + folios[i].FolioPedido + "\");'/></td>" +
                "<td class='colClave textoDerecha'>" + folios[i].FolioPedido + "</td>" +
                "<td class='colDescripcion'>" + folios[i].Organizacion.Descripcion + "</td>" +
                "</tr>");
        }
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
    
    // ****************** ********************  ********************* //

    // ******************  Pantalla Ayuda Choferes  ********************* //

    // Boton que cancela la busqueda de Choferes
    $("#btnCancelarAyudaChofer").click(function () {
        $("#dlgAyudaChoferes").modal("hide");
        bootbox.dialog({
            message: window.mensajeCancelarAyudaChofer,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        $("#txtDescripcionChoferAyuda").val("");
                        setTimeout(function () { $("#txtChoferId").focus(); }, 500);
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgAyudaChoferes").modal("show");
                        setTimeout(function () { $("#txtDescripcionChoferAyuda").focus(); }, 500);
                    }
                }
            }
        });
    });
    
    // Boton que agrega el Chofer
    $("#btnAgregarAyudaChofer").click(function () {
        var renglones = $("input[class=chofer]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtChoferId").val($(this).attr("chofer"));
                $("#txtChofer").val($(this).attr("nombre"));
                $('#txtChoferValido').val("1");
            });
            $("#dlgAyudaChoferes").modal("hide");
            $("#dlgSolicitudMateriaPrima").modal("show");
            $("#txtDescripcionChoferAyuda").val("");
            setTimeout(function () { $("#txtCamionId").focus(); }, 500);
        } else {
            $("#dlgAyudaChoferes").modal("hide");
            bootbox.alert(window.mensajeSeleccionarChofer, function () {
                $("#dlgAyudaChoferes").modal("show");
                setTimeout(function () { $("#txtDescripcionChoferAyuda").focus(); }, 500);
            });
        }
    });

    $("#btnBuscarAyudaChofer").click(function () {
        ObtenerPorChofer();
    });

    $('#txtDescripcionChoferAyuda').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            ObtenerPorChofer();
            e.preventDefault();
        }
    });

    // Abre el dialogo de los choferes
    AbrirAyudaChoferes = function () {

        if ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0) {
            var proveedorChofer = {};
            var proveedorInfo = {};
            var choferInfo = {};

            proveedorInfo.proveedorID = $("#txtProveedorId").val();

            proveedorChofer.Proveedor = proveedorInfo;
            proveedorChofer.Chofer = choferInfo;

            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerChoferes",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ choferDescripcion: "", proveedorChofer: proveedorChofer }),
                error: function(request) {
                    $("#dlgSolicitudMateriaPrima").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarChoferes, function() {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                    });
                },
                success: function (data) {
                    
                    if (data.d) {
                        if (data.d.length > 0) {
                            CrearGridChoferes(data.d);
                            $("#dlgAyudaChoferes").modal("show");
                            setTimeout(function () { $("#txtDescripcionChoferAyuda").focus(); $("#txtDescripcionChoferAyuda").val(""); }, 500);
                        } else {
                            $("#dlgSolicitudMateriaPrima").modal("hide");
                            bootbox.alert(window.mensajeNoHayChoferesAsignados, function() {
                                $("#dlgSolicitudMateriaPrima").modal("show");
                            });
                        }
                    } else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoHayChoferesAsignados, function() {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                        });
                    }
                }
            });
        } else {
            $("#dlgSolicitudMateriaPrima").modal("hide");
            bootbox.alert(window.mensajeFavorCapturaTransportista, function () {
                $("#dlgSolicitudMateriaPrima").modal("show");
                LimpiarEdicionChofer();
                setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
            });
        }
    };
    
    // Obtiene el chofer por su nombre
    ObtenerPorChofer = function () {
        if ( ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0)) {
            
            var proveedorChofer = {};
            var proveedorInfo = {};
            var choferInfo = {};

            proveedorInfo.proveedorID = $("#txtProveedorId").val();

            proveedorChofer.Proveedor = proveedorInfo;
            proveedorChofer.Chofer = choferInfo;
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerChoferes",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ choferDescripcion: $("#txtDescripcionChoferAyuda").val(), proveedorChofer: proveedorChofer }),
                error: function (request) {
                    $("#dlgAyudaChoferes").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarChoferes, function () {
                        $("#dlgAyudaChoferes").modal("show");
                        setTimeout(function () { $("#txtDescripcionChoferAyuda").focus(); $("#txtDescripcionChoferAyuda").val(""); }, 500);
                    });
                },
                success: function (data) {
                    if (data.d.length > 0) {
                        CrearGridChoferes(data.d);
                    } else {
                        $("#dlgAyudaChoferes").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroElChoferAyuda, function () {
                            $("#dlgAyudaChoferes").modal("show");
                            setTimeout(function () { $("#txtDescripcionChoferAyuda").focus(); $("#txtDescripcionChoferAyuda").val(""); }, 500);
                        });
                    }
                }
            });
        }
    };

    // Obtiene el Chofer por identificador.
    ObtenerPorChoferId = function () {
        if ($("#txtChoferId").val() != "" && ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0)) {
            
            var proveedorChofer = {};
            var proveedorInfo = {};
            var choferInfo = {};
            
            proveedorInfo.proveedorID = $("#txtProveedorId").val();
            choferInfo.choferID = $("#txtChoferId").val();

            proveedorChofer.Proveedor = proveedorInfo;
            proveedorChofer.Chofer = choferInfo;
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerChoferes",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ choferDescripcion: "", proveedorChofer: proveedorChofer }),
                error: function (request) {
                    $("#dlgSolicitudMateriaPrima").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarChoferes, function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        LimpiarEdicionChofer();
                        setTimeout(function () { $("#txtChoferId").focus(); }, 500);
                    });
                },
                success: function (data) {
                    
                    if(data.d){
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                $("#txtChoferId").val(data.d[i].ChoferID);
                                $("#txtChofer").val(data.d[i].NombreCompleto);
                                $("#txtCamionId").focus();
                                $('#txtChoferValido').val("1");
                            }
                        } else {
                            $("#dlgSolicitudMateriaPrima").modal("hide");
                            bootbox.alert(window.mensajeNoSeEncontroElChofer, function () {
                                $("#dlgSolicitudMateriaPrima").modal("show");
                                LimpiarEdicionChofer();
                                setTimeout(function () { $("#txtChoferId").focus(); }, 500);
                            });
                        }
                    } else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroElChofer, function () {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                            LimpiarEdicionChofer();
                            setTimeout(function () { $("#txtChoferId").focus(); }, 500);
                        });
                    }
                }
            });
        } else {
            $("#dlgSolicitudMateriaPrima").modal("hide");
            bootbox.alert(window.mensajeFavorCapturaChofer, function () {
                $("#dlgSolicitudMateriaPrima").modal("show");
                LimpiarEdicionChofer();
                setTimeout(function () { $("#txtChoferId").focus(); }, 500);
            });
        }
    };

    // Funcion que genera el grid en la ayuda de choferes
    CrearGridChoferes = function (datosChofer) {
        $("#tablaChoferes tbody").html("");
        for (var i = 0; i < datosChofer.length; i++) {
            $("#tablaChoferes tbody").append("<tr>" +
                "<td class='colCheckBox alineacionCentro'><input type='checkbox' class='chofer' id='chofer" + datosChofer[i].ChoferID + "' chofer='" + datosChofer[i].ChoferID + "' nombre='" + datosChofer[i].NombreCompleto + "' onclick='SeleccionaUnChofer(\"#chofer" + datosChofer[i].ChoferID + "\");'/></td>" +
                '<td class="colClave textoDerecha">' + datosChofer[i].ChoferID + '</td>' +
                '<td class="colDescripcion">' + datosChofer[i].NombreCompleto + '</td>' +
                '</tr>');
        }
    };

    //Selecciona solo un checkbox
    SeleccionaUnChofer = function (Id) {
        var listaCheckBox = $(".chofer");
        var checkbox = $(Id);
        if (checkbox.is(":checked")) {
            listaCheckBox.each(function () {
                this.checked = false;
            });
            checkbox.attr("checked", true);
        }
    };

    // ******************  ********************  ********************** //
    

    // ******************  Pantalla Ayuda Placas  ********************* //

    // Boton que cancela la busqueda de Placas
    $("#btnCancelarAyudaPlaca").click(function () {
        $("#dlgAyudaPlacas").modal("hide");
        bootbox.dialog({
            message: window.mensajeCancelarAyudaPlacas,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        $("#txtDescripcionPlacaAyuda").val("");
                        setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgAyudaPlacas").modal("show");
                        setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); }, 500);
                    }
                }
            }
        });
    });

    // Boton que agrega la Placa
    $("#btnAgregarAyudaPlaca").click(function () {
        var renglones = $("input[class=placa]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtCamionId").val($(this).attr("camion"));
                $("#txtPlaca").val($(this).attr("placa"));
                $('#txtPlacaValida').val("1");
            });
            $("#dlgAyudaPlacas").modal("hide");
            $("#dlgSolicitudMateriaPrima").modal("show");
            $("#txtDescripcionPlacaAyuda").val("");
            setTimeout(function () { $("#txtCamionId").focus(); }, 500);
        } else {
            $("#dlgAyudaPlacas").modal("hide");
            bootbox.alert(window.mensajeSeleccionarPlaca, function () {
                $("#dlgAyudaPlacas").modal("show");
                setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); }, 500);
            });
        }
    });

    $("#btnBuscarAyudaPlaca").click(function () {
        ObtenerPorPlaca();
    });

    $('#txtDescripcionPlacaAyuda').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            ObtenerPorPlaca();
            e.preventDefault();
        }
    });

    // Abre la ayuda de Placas
    AbrirAyudaPlacas = function () {

        if ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0) {

            var camionInfo = {};
            var proveedorInfo = {};
            
            proveedorInfo.ProveedorID = $("#txtProveedorId").val();
            camionInfo.Proveedor = proveedorInfo;
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerPlacas",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ placaDescripcion: "", camionInfo: camionInfo }),
                error: function (request) {
                    $("#dlgSolicitudMateriaPrima").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarPlacas, function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        LimpiarEdicionPlaca();
                        setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                    });
                },
                success: function (data) {
                    
                    if (data.d) {
                        if (data.d.length > 0) {
                            CrearGridPlacas(data.d);
                            $("#dlgAyudaPlacas").modal("show");
                            setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); $("#txtDescripcionPlacaAyuda").val(""); }, 500);
                        } else {
                            $("#dlgSolicitudMateriaPrima").modal("hide");
                            bootbox.alert(window.mensajeNoHayPlacasAsignadas, function () {
                                $("#dlgSolicitudMateriaPrima").modal("show");
                                LimpiarEdicionPlaca();
                                setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                            });
                        }
                    } else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoHayPlacasAsignadas, function () {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                            LimpiarEdicionPlaca();
                            setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
                        });
                    }
                }
            });
        } else {
            $("#dlgSolicitudMateriaPrima").modal("hide");
            bootbox.alert(window.mensajeFavorCapturaTransportista, function () {
                $("#dlgSolicitudMateriaPrima").modal("show");
                LimpiarEdicionPlaca();
                setTimeout(function () { $("#txtProveedorCodigoSap").focus(); }, 500);
            });
        }
    };

    ObtenerPorPlaca = function () {
        if ( ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0)) {

            var camionInfo = {};
            var proveedorInfo = {};
            
            proveedorInfo.ProveedorID = $("#txtProveedorId").val();
            camionInfo.Proveedor = proveedorInfo;
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerPlacas",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ placaDescripcion: $("#txtDescripcionPlacaAyuda").val(), camionInfo: camionInfo }),
                error: function (request) {
                    $("#dlgAyudaPlacas").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarPlacas, function () {
                        $("#dlgAyudaPlacas").modal("show");
                        LimpiarEdicionPlaca();
                        setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); $("#txtDescripcionPlacaAyuda").val(""); }, 500);
                    });
                },
                success: function (data) {
                    if (data.d.length > 0) {
                        CrearGridPlacas(data.d);
                        setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); }, 300);
                    } else {
                        $("#dlgAyudaPlacas").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroLaPlaca, function () {
                            $("#dlgAyudaPlacas").modal("show");
                            //LimpiarEdicionPlaca();
                            setTimeout(function () { $("#txtDescripcionPlacaAyuda").focus(); $("#txtDescripcionPlacaAyuda").val(""); }, 500);
                        });
                    }
                }
            });
        }
    };

    ObtenerPorPlacaId = function () {
        if ($("#txtCamionId").val() != "" && ($("#txtProveedorId").val() != "" && $("#txtProveedorId").val() > 0)) {

            var camionInfo = {};
            var proveedorInfo = {};
            
            camionInfo.CamionID = $("#txtCamionId").val();
            proveedorInfo.ProveedorID = $("#txtProveedorId").val();
            camionInfo.Proveedor = proveedorInfo;
            
            $.ajax({
                type: "POST",
                url: "AbastoDeMateriaPrima.aspx/ObtenerPlacas",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ placaDescripcion: "", camionInfo: camionInfo }),
                error: function (request) {
                    $("#dlgSolicitudMateriaPrima").modal("hide");
                    bootbox.alert(window.mensajeErrorAlConsultarPlacas, function () {
                        $("#dlgSolicitudMateriaPrima").modal("show");
                        LimpiarEdicionPlaca();
                        setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                    });
                },
                success: function (data) {
                    
                    if (data.d) {
                        if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                $("#txtPlaca").val(data.d[i].PlacaCamion);
                                $('#txtPlacaValida').val("1");
                            }
                        } else {
                            $("#dlgSolicitudMateriaPrima").modal("hide");
                            bootbox.alert(window.mensajeNoSeEncontroLaPlaca, function () {
                                $("#dlgSolicitudMateriaPrima").modal("show");
                                LimpiarEdicionPlaca();
                                setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                            });
                        }
                    } else {
                        $("#dlgSolicitudMateriaPrima").modal("hide");
                        bootbox.alert(window.mensajeNoSeEncontroLaPlaca, function () {
                            $("#dlgSolicitudMateriaPrima").modal("show");
                            LimpiarEdicionPlaca();
                            setTimeout(function () { $("#txtCamionId").focus(); }, 500);
                        });
                    }
                }
            });
        } else {
            $("#dlgSolicitudMateriaPrima").modal("hide");
            bootbox.alert(window.mensajeFavorCapturaPlaca, function () {
                $("#dlgSolicitudMateriaPrima").modal("show");
                LimpiarEdicionPlaca();
                setTimeout(function () { $("#txtCamionId").focus(); }, 500);
            });
        }
    };

    // Funcion que genera el grid en la ayuda de proveedores
    CrearGridPlacas = function (datosPlaca) {
        $("#tablaPlacas tbody").html("");
        for (var i = 0; i < datosPlaca.length; i++) {
            $("#tablaPlacas tbody").append("<tr>" +
                "<td class='colCheckBox alineacionCentro'><input type='checkbox' class='placa' id='placa" + datosPlaca[i].CamionID + "' camion='" + datosPlaca[i].CamionID + "' placa='" + datosPlaca[i].PlacaCamion + "' onclick='SeleccionaUnaPlaca(\"#placa" + datosPlaca[i].CamionID + "\");'/></td>" +
                '<td class="colClave textoDerecha">' + datosPlaca[i].CamionID + '</td>' +
                '<td class="colDescripcion">' + datosPlaca[i].PlacaCamion + '</td>' +
                '</tr>');
        }
    };

    //Selecciona solo un checkbox
    SeleccionaUnaPlaca = function (Id) {
        var listaCheckBox = $(".placa");
        var checkbox = $(Id);
        if (checkbox.is(":checked")) {
            listaCheckBox.each(function () {
                this.checked = false;
            });
            checkbox.attr("checked", true);
        }
    };

    // ******************  ********************  ********************** //


    Limpiar();
});