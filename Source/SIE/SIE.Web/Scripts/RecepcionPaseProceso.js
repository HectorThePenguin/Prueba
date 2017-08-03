
var Surtidos;
var Pedido;
var limpiarTodo;
$(document).ready(function () {
    Surtidos = [];
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 10, "greedy": false });

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    // Boton que abre la ayuda de los folios.
    $("#btnBuscarFolio").click(function () {
        $("#txtFolioBuscar").val('');
        InicializarTablaRecepcion();
        ObtenerFoliosParciales();
        $("#txtFolioBuscar").focus();
        $("#dlgBusquedaFolio").modal("show");
    });

    // Boton que busca el folio capturado
    $("#btnBuscarAyudaFolio").click(function () {
        ObtenerPedidoFolioTecleado();
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
            ObtenerPedidoPorFolio(true);
        } else {
            $("#dlgBusquedaFolio").modal("hide");
            bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSeleccionarFolio, function () {
                $("#dlgBusquedaFolio").modal("show");
            });
        }
    });

    // Boton que cancela la busqueda de folios
    $("#btnCancelarAyudaFolio").click(function () {
        $("#dlgBusquedaFolio").modal("hide");
        ObtenerFoliosParciales();
        $("#txtFolioBuscar").val("");
    });

    $("#btnGuardar").click(function () {
        App.bloquearContenedor($(".contenedorPaseProceso"));
        GuardarRecepcionMateriaPrima();
    });

    $('#txtFolio').keydown(function (e) {
        var code = e.keyCode || e.which;

        if (code == 13 || code == 9) {
            if ($('#txtFolio').val() == "") {
                $('#btnBuscarFolio').focus();
            } else {
                ObtenerPedidoPorFolio(false);
            }
            e.preventDefault();
        }
        if (code == 8 || code == 46) {
            limpiarTodo = false;
            LimpiarRecepcionMateria();
            limpiarTodo = true;
        }
    });

    // Boton que cancela la captura y limpia la pantalla
    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: "<img src='../Images/questionmark.png'/>&nbsp;" + window.mensajeCancelacion,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        LimpiarRecepcionMateria();
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

    $("#btnFinalizarPedido").click(function () {
        if (Pedido != null) {
            bootbox.dialog({
                message: "<img src='../Images/questionmark.png'/>&nbsp;" + window.mensajeSeguroDeCancelar,
                title: "",
                buttons: {
                    success: {
                        label: "SI",
                        className: "btn-default",
                        callback: function () {
                            App.bloquearContenedor($(".contenedorPaseProceso"));
                            $.ajax({
                                type: "POST",
                                url: "RecepcionPaseProceso.aspx/FinalizarPedido",
                                data: JSON.stringify({ pedido: Pedido.PedidoID }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                error: function (request) {
                                    App.desbloquearContenedor($(".contenedorPaseProceso"));
                                    bootbox.alert(window.mensajeErrorAlActualizarEstatus);
                                },
                                success: function (data) {
                                    App.desbloquearContenedor($(".contenedorPaseProceso"));
                                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeEstatusFinalizado, function() {
                                        ObtenerFoliosParciales();
                                        LimpiarRecepcionMateria();
                                    });
                                }
                            });
                        }
                    },
                    danger: {
                        label: "NO",
                        className: "btn-default",
                        callback: function() {
                        }
                    }
                }
            });
        } else {
            bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSeleccioneFolio);
        }
    });
    
    // Boton que cancela la seleccion de los lotes y cierra la ventana emergente
    $("#btnCancelarAyudaFolio").click(function () {
        
        bootbox.dialog({
            message: "<img src='../Images/questionmark.png'/>&nbsp;"+ window.mensajeCancelacionAyuda,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#dlgBusquedaFolio").modal("hide");
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

    $('#txtObservaciones').keyup(function () {
        if ($('#txtObservaciones').val().length >= 254) {
            var texto = $('#txtObservaciones').val();
            $('#txtObservaciones').val(texto.substring(0, 255));
        }
    });

    $('#txtObservaciones').keydown(function (event) {

        if(event.shiftKey)
        {
            event.preventDefault();
        }

        if (event.key == "ñ" || event.key == "Ñ" || event.key == "." || event.key == "," || event.key == ":" || event.key == "-" || event.key == "(" || event.key == ")" || event.key == "?" || event.key == "¿" || event.key == "!" || event.key == "¡" || event.key == "+") {
            return;
        }
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 32) {
        }
        else {
            if (event.keyCode < 95) {
                if (event.keyCode < 48 || event.keyCode > 90) {
                    event.preventDefault();
                }
            } 
            else {
                if (event.keyCode < 96 || event.keyCode > 105) {
                    event.preventDefault();
                }
            }
        }
    });

    ValidarAlmacenOrganizacion();
    InicializarTablaRecepcion();
    limpiarTodo = true;
});


/*
 * Inicializar el mostrado de la tabla
 */
InicializarTablaRecepcion = function () {
    var respuesta = {}; //arreglo de datos para la tabla de recursos
    respuesta.Surtidos = Surtidos;
    respuesta.Recursos = LlenarRecursos();

    //Inicializamos de nuevo la tabla
    AgregaElementosGridRecepcionMateriaPrima(respuesta);
};

//Cabecero de la tabla
LlenarRecursos = function () {
    Resources = {};
    Resources.headerFechaSurtida = headerFechaSurtida;
    Resources.headerTicket = headerTicket;
    Resources.headerProductos = headerProductos;
    Resources.headerChofer = headerChofer;
    Resources.headerProveedor = headerProveedor;
    Resources.headerLoteDestino = headerLoteDestino;
    Resources.headerCantidadSurtida = headerCantidadSurtida;
    Resources.headerCantidadSolicitada = headerCantidadSolicitada;
    Resources.headerCantidadRecibida = headerCantidadRecibida;
    Resources.headerCantidadPendiente = headerCantidadPendiente;
    Resources.headerCantidadProgramada = headerCantidadProgramada;
    Resources.headerCantidadEntregada = headerCantidadEntregada;
    return Resources;
};

/*
 * Agrega los elementos a la tabla plantilla de aretes para sacrificio
 */
AgregaElementosGridRecepcionMateriaPrima = function (datos) {
    $('#tablaFolios').html("");
    if (datos != null) {
        $('#tablaFolios').setTemplateURL('../Templates/GridRecepcionMateriaPrima.htm');
        $('#tablaFolios').processTemplate(datos);
    } else {
        $('#tablaFolios').html("");
    }
};

ValidarAlmacenOrganizacion = function () {
    $.ajax({
        type: "POST",
        url: "RecepcionPaseProceso.aspx/ValidarAlmacenOrganizacion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            
        },
        success: function (data) {

            if (data.d.EsValido == false) {
 
                bootbox.dialog({
                    message: "<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSinAlmacen + " " + data.d.Datos.Descripcion,
                    buttons: {
                        Aceptar: {
                            label: window.labelOk,
                            callback: function () {
                                history.go(-1);
                            }
                        }
                    }
                });

                return false;
            } else {
                return true;
            }
        }
    });
};

// Funcion para obtener los datos del pedido por el folio en la ayuda
var ObtenerPedidoFolioTecleado = function () {
    if ($("#txtFolioBuscar").val() > 0) {
        $.ajax({
            type: "POST",
            url: "RecepcionPaseProceso.aspx/ObtenerPedidoParcialesPorFolio",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folio: $("#txtFolioBuscar").val() }),
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d) {
                    var resultado = data.d;

                    $("#gridFoliosMateriaPrima tbody").html("");
                    for (var i = 0; i < resultado.length; i++) {
                        $("#gridFoliosMateriaPrima tbody").append("<tr>" +
                            "<td style='width: 20px;'><input type='checkbox' class='folios' id='folio" + resultado[i].FolioPedido + "' folio='" + resultado[i].FolioPedido + "' onclick='SeleccionaUno(\"#folio" + resultado[i].FolioPedido + "\");'/></td>" +
                            "<td style='text-align: right'>" + resultado[i].PedidoID + "</td>" +
                            "<td style='text-align: right'>" + resultado[i].FolioPedido + "</td>" +
                            "<td style='text-align: left'>" + resultado[i].Organizacion.Descripcion + "</td>" +
                            "<td style='text-align: left'>" + resultado[i].EstatusPedido.Descripcion + "</td>" +
                            "</tr>");
                    }

                } else {
                    $("#dlgBusquedaFolio").modal("hide");
                    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;"+window.mensajeFolioNoValido, function () {
                        $("#dlgBusquedaFolio").modal("show");
                    });
                }
            }
        });
    } else {
        ObtenerFoliosParciales();
    }
};

// Funcion para obtener los datos del pedido sin la ayuda
var ObtenerPedidoPorFolio = function (mostrarAyuda) {
    App.bloquearContenedor($(".contenedorPaseProceso"));
    if ($("#txtFolio").val() > 0) {
        $.ajax({
            type: "POST",
            url: "RecepcionPaseProceso.aspx/ObtenerPedidoParcialesPorFolioUnico",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folio: $("#txtFolio").val() }),
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d.EsValido) {
                    Pedido = data.d.Datos;
                    ObtenerSurtidoPorFolio(mostrarAyuda);
                } else {
                    if (data.d.Mensaje.trim() == "SINRESULTADOS") {
                        bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + window.mensajeFolioNoValido, function () {
                            LimpiarRecepcionMateria();
                        });
                    } else {
                        bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + window.mensajeEstatusIncorrecto, function () {
                            LimpiarRecepcionMateria();
                        });
                    }
                    
                }
                App.desbloquearContenedor($('.contenedorPaseProceso'));
            }
        });
    } else {
        ObtenerFoliosParciales();
        App.desbloquearContenedor($('.contenedorPaseProceso'));
    }
};

// Funcion para obtener los datos del folio de entrada producto de la ayuda
var ObtenerSurtidoPorFolio = function (mostrarDialogo) {
    App.bloquearContenedor($(".contenedorPaseProceso"));
    if ($("#txtFolio").val() > 0) {
        $.ajax({
            type: "POST",
            url: "RecepcionPaseProceso.aspx/ObtenerSurtidoPorFolio",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ folio: $("#txtFolio").val() }),
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d) {

                    var respuesta = {};
                    Surtidos = data.d;
                    respuesta.Surtidos = data.d;
                    respuesta.Recursos = LlenarRecursos();

                    AgregaElementosGridRecepcionMateriaPrima(respuesta);

                    ChecarLineasPendientesCero();
                    App.desbloquearContenedor($('.contenedorPaseProceso'));

                } else {
                    $("#dlgBusquedaFolio").modal("hide");
                    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSinSurtido, function () {
                        if (mostrarDialogo) {
                            $("#dlgBusquedaFolio").modal("show");
                        }
                        
                    });
                }
            }
        });
    } else {
        ObtenerFoliosParciales();
    }
};

// Funcion para obtener los datos del pedido que se encuentren en estatus parciales
var ObtenerFoliosParciales = function () {
    $("#gridFoliosMateriaPrima tbody").html("");
    $.ajax({
        type: "POST",
        url: "RecepcionPaseProceso.aspx/ObtenerPedidosParciales",
        contentType: "application/json; charset=utf-8",
        error: function (request) {

            bootbox.alert(window.mensajeErrorCosultarPedidos);
        },
        success: function (data) {

            if (data.d) {
                var resultado = data.d;
                
                for (var i = 0; i < resultado.length; i++) {
                    $("#gridFoliosMateriaPrima tbody").append("<tr>" +
                        "<td style='width: 20px;'><input type='checkbox' class='folios' id='folio" + resultado[i].FolioPedido + "' folio='" + resultado[i].FolioPedido + "' onclick='SeleccionaUno(\"#folio" + resultado[i].FolioPedido + "\");'/></td>" +
                        "<td style='text-align: right'>" + resultado[i].PedidoID + "</td>" +
                        "<td style='text-align: right'>" + resultado[i].FolioPedido + "</td>" +
                        "<td style='text-align: left'>" + resultado[i].Organizacion.Descripcion + "</td>" +
                        "<td style='text-align: left'>" + resultado[i].EstatusPedido.Descripcion + "</td>" +
                        "</tr>");
                }
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

ChecarLineasPendientesCero = function() {

    var listaCheckBox = $(".Surtido");

    listaCheckBox.each(function() {
        ValidarCantidadPendiente(this);
    });
};

ValidarCantidadPendiente = function (check) {
    
    var idCheckbox = check.id;
    var id = idCheckbox.substring(4, idCheckbox.length);
    var index;
    var cantidadEntregada = 0;
    var cantidadPendiente = 0;
    for (index = 0; index < Surtidos.length; ++index) {
        var tmpSurtido = Surtidos[index];
        
        if (tmpSurtido.PesajeMateriaPrima.Ticket == id) {

            cantidadEntregada = tmpSurtido.PesajeMateriaPrima.PesoBruto - tmpSurtido.PesajeMateriaPrima.PesoTara;
            cantidadPendiente = tmpSurtido.ProgramacionMateriaPrima.CantidadProgramada - tmpSurtido.ProgramacionMateriaPrima.CantidadEntregada;

            //Por si la cantidad entregada es mayor a la solicitada
            if (cantidadPendiente < 0) {
                cantidadPendiente = 0;
            }

            if (cantidadPendiente == 0 || check.checked) {
                check.checked = true;
                tmpSurtido.Seleccionado = true; 
            }
            
            if (!check.checked) {
                tmpSurtido.Seleccionado = false;
            }

            if (tmpSurtido.PesajeMateriaPrima.Activo == 0 || tmpSurtido.PesajeMateriaPrima.AlmacenMovimientoOrigenId != 0) {
                check.checked = true;
                check.disabled = true;
                tmpSurtido.Seleccionado = false;
            }
            
            Surtidos[index] = tmpSurtido;

            $("#colCantidadRecibida_" + id).html(accounting.formatNumber(cantidadEntregada));
            $("#colCantidadPendiente_" + id).html(accounting.formatNumber(cantidadPendiente));
        }
    }
};

ValidaCheck = function (check) {

    var idCheckbox = check.id;
    var id = idCheckbox.substring(4, idCheckbox.length);
    var index;
    for (index = 0; index < Surtidos.length; ++index) {
        var tmpSurtido = Surtidos[index];

        if (tmpSurtido.PesajeMateriaPrima.Ticket == id) {

            if (check.checked) {
                tmpSurtido.Seleccionado = true;
            }
            else{
                tmpSurtido.Seleccionado = false;
            }

            Surtidos[index] = tmpSurtido;
        }
    }
};

//Selecciona o deselecciona todos los checks del grid
SeleccionarTodosLosChecks = function () {

    var listaCheckBox = $(".Surtido");
    var checkbox = $("#CheckboxAll");

    if (checkbox.is(":checked")) {
        listaCheckBox.each(function () {
            if (!this.disabled) {
                this.checked = true;
                ValidaCheck(this);
            }
        });

    } else {
        listaCheckBox.each(function() {
            if (!this.disabled) {
                this.checked = false;
                ValidaCheck(this);
            }
        });
    }
};

// Funcion para obtener los datos del pedido que se encuentren en estatus parciales
var GuardarRecepcionMateriaPrima = function () {

    //App.bloquearContenedor($(".contenedorPaseProceso"));
    if (!ValidarGuardado()) {
        App.desbloquearContenedor($('.contenedorPaseProceso'));
        return;
    }
    
    Pedido.Observaciones = $('#txtObservaciones').val();
    var datos = { 'listaSurtido': Surtidos, 'pedido': Pedido };
    $.ajax({
        type: "POST",
        url: "RecepcionPaseProceso.aspx/ActualizarRecepcionMateriaPrima",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            App.desbloquearContenedor($('.contenedorPaseProceso'));
            bootbox.alert(window.mensajeErrorCosultarPedidos);
        },
        success: function (data) {
            App.desbloquearContenedor($('.contenedorPaseProceso'));
            if (data.d) {
                var resultado = data.d;
                if (resultado) {

                    bootbox.dialog({
                        message: "<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeGuardadoExito,
                        buttons: {
                            success: {
                                label: window.labelOk,
                                callback: function() {
                                    ObtenerFoliosParciales();
                                    LimpiarRecepcionMateria();
                                }
                            },
                        }
                    });
                }
            }
            else {

                bootbox.dialog({
                    message: "<img src='../Images/stop.png'/>&nbsp;" + window.mensajeErrorGuardado,
                    buttons: {
                        Aceptar: {
                            label: window.labelOk
                        }
                    }
                });
            }
        }
    });
};

var ValidarGuardado = function () {

    var seleccionados = 0;
    var index;

    if ($('#txtFolio').val() == '') {
        bootbox.dialog({
            message: "<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSeleccioneFolio,
            buttons: {
                Aceptar: {
                    label: window.labelOk
                }
            }
        });
        return false;
    }

    for (index = 0; index < Surtidos.length; ++index) {
        Surtidos[index].AlmacenInventarioLote.FechaInicio = new Date();
        Surtidos[index].AlmacenInventarioLote.FechaFin = new Date();
        Surtidos[index].AlmacenInventarioLote.FechaProduccionFormula = new Date();
        Surtidos[index].AlmacenInventarioLote.AlmacenInventario.FechaCreacion = new Date();
        Surtidos[index].PesajeMateriaPrima.FechaSurtido = new Date();
        Surtidos[index].PesajeMateriaPrima.FechaRecibe = new Date();
        Surtidos[index].PesajeMateriaPrima.FechaCreacion = new Date();
        Surtidos[index].Producto.FechaCreacion = new Date();
        Surtidos[index].PedidoDetalle.InventarioLoteDestino.FechaInicio = new Date();
        Surtidos[index].PedidoDetalle.InventarioLoteDestino.FechaFin = new Date();
        Surtidos[index].PedidoDetalle.InventarioLoteDestino.FechaProduccionFormula = new Date();
        Surtidos[index].PedidoDetalle.FechaCreacion = new Date();
        Surtidos[index].PedidoDetalle.FechaModificacion = new Date();
        Surtidos[index].ProgramacionMateriaPrima.InventarioLoteOrigen.FechaInicio = new Date();
        Surtidos[index].ProgramacionMateriaPrima.InventarioLoteOrigen.FechaFin = new Date();
        Surtidos[index].ProgramacionMateriaPrima.InventarioLoteOrigen.FechaProduccionFormula = new Date();
        Surtidos[index].ProgramacionMateriaPrima.FechaProgramacion = new Date();
        Surtidos[index].ProgramacionMateriaPrima.FechaCreacion = new Date();
        Surtidos[index].ProgramacionMateriaPrima.FechaModificacion = new Date();
        Surtidos[index].Pedido.FechaCreacion = new Date();
        Surtidos[index].Pedido.FechaModificacion = new Date();
        Surtidos[index].Pedido.FechaPedido = new Date();
        
        Pedido.FechaCreacion = new Date();
        Pedido.FechaModificacion = new Date();
        Pedido.EstatusPedido.FechaCreacion = new Date();
        if (Surtidos[index].Seleccionado) {
            seleccionados++;
        }

    }

    Pedido.FechaPedido = new Date();

    if (seleccionados == 0) {

        bootbox.dialog({
            message: "<img src='../Images/stop.png'/>&nbsp;" + window.mensajeSeleccioneRegistro,
            buttons: {
                Aceptar: {
                    label: window.labelOk
                }
            }
        });

        return false;
    }
    return true;
};
//Limpia la pantalla
var LimpiarRecepcionMateria = function() {
    Surtidos = [];
    Pedido = null;
    InicializarTablaRecepcion();
    if (limpiarTodo) {
        $("#txtFolio").val('');
        $('#txtObservaciones').val('');
    }
    
};

