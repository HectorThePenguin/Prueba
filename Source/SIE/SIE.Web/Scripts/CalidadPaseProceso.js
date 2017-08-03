/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

var tiposMovimiento = {
    PaseProceso: { value: 1, mensaje: "" },
    ProduccionFormula: { value: 2, mensaje: "" }
};

var mensajeProducto = false;
var mensajeFolio = false;
var mensajeCancelar = false;
var mensajeAgregar = false;
var mensajeIndicadores = false;
var presionoEnter = false;
var validarIndicador = false;

var tipoMovimiento = 0;
var cantidadIndicadores = 0;

var objetivoInvalido = false;

jQuery(document).ready(function () {
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
    
    asignaEventosCombo();
    asignarEventosFolioPasePRoceso();
    asignarEventosBotonGuardar();
    asignarEventosBotonCancelar();
    eventosBusquedaFolio();

    configurarControlesTemplate();
    cargarCombo();
    limpiar();
    
    $('body').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    habilitaDeshabilitaFolioPase(true);
});

cargarSeccionSeleccione = function(tipoMovimiento) {
    $('#divProductoFormula').html('');
    $('#divObservaciones').html('');

    if (tipoMovimiento > 0) {
        var recursosProductoFormula = { };
        if (tipoMovimiento == tiposMovimiento.ProduccionFormula.value) {
            recursosProductoFormula.Leyenda = window.leyendaFormula;
            recursosProductoFormula.Tipo = window.formula;
            habilitaDeshabilitaFolioPase(true);
        } else {
            recursosProductoFormula.Leyenda = window.leyendaProducto;
            recursosProductoFormula.Tipo = window.producto;
            habilitaDeshabilitaFolioPase(false);
        }
        recursosProductoFormula.SubFamilia = window.subFamilia;

        var recursos = { };
        recursos.Recursos = recursosProductoFormula;

        $('#divProductoFormula').setTemplateURL('../Templates/CalidadSeleccioneProductoFormula.htm');
        $('#divProductoFormula').processTemplate(recursos);

        configurarControlesTemplate();
        if (tipoMovimiento == tiposMovimiento.ProduccionFormula.value) {
            eventosBusquedaFormula();
        } else {
            eventosBusquedaProducto();
        }
        $("#txtID").focus();
    } else {
        habilitaDeshabilitaFolioPase(true);
    }
};

cargarCombo = function() {
    asignaValoresCombo();
};

asignaValoresCombo = function() {
    var valores = { };
    var recursos = { };
    recursos.Seleccione = window.seleccione;
    valores.Recursos = recursos;

    var listaValores = new Array();
    var valor = { };
    valor.Clave = 1;
    valor.Descripcion = window.paseProceso;
    listaValores.push(valor);

    valor = { };
    valor.Clave = 2;
    valor.Descripcion = window.produccionFormulas;
    listaValores.push(valor);

    valores.Valores = listaValores;

    $('#ddlTipoMovimiento').html('');
    $('#ddlTipoMovimiento').setTemplateURL('../Templates/ComboGenerico.htm');
    $('#ddlTipoMovimiento').processTemplate(valores);

    asignaEventosCombo();
};

buscarFolioPaseProceso = function(folioPedido) {
    var jFolioPedido = { "folioPedido": folioPedido };
    EjecutarWebMethod(window.location.pathname + '/ObtenerFolioPedido', jFolioPedido, validaFolioPaseProceso
                    , window.errorConsultarFolioProceso);
};

validaFolioPaseProceso = function(msg) {
    if (msg.d == null || msg.d.length == 0) {
        mensajeFolio = true;
        bootbox.dialog({
            message: window.folioInvalido,
            buttons: {
                Aceptar: {
                    label:'Ok',
                    callback: function () {
                        $("#txtFolioPaseProceos").val('');
                        $("#txtFolioPaseProceos").focus();
                    }
                }
            }
        });
        DesbloquearPantalla();
        return false;
    }
    var pedido = msg.d;
    $("#hdnPedidoID").val(pedido.PedidoID);
    tipoMovimiento = $("#ddlTipoMovimiento").val();
    if (tipoMovimiento > 0) {
        cargarSeccionSeleccione(tiposMovimiento.PaseProceso.value);
    }
    habilitaDeshabilitaFolioPase(true);
    DesbloquearPantalla();
};

buscarProducto = function(pedidoId, productoId) {
    var jProducto = { "pedidoID": pedidoId, "productoID": productoId };
    EjecutarWebMethod(window.location.pathname + '/ObtenerProducto', jProducto, productoValido, window.errorConsultarProducto);
};

buscarFormula = function(formulaId) {
    var jFormula = { "formulaID": formulaId };
    EjecutarWebMethod(window.location.pathname + '/ObtenerFormula', jFormula, formulaValido, window.errorConsultarFormula);
};

productoValido = function(msg) {
    if (msg.d == null || msg.d.length == 0) {
        if (!mensajeProducto) {
            mensajeProducto = true;
            bootbox.dialog({
                message: window.productoInvalido,
                buttons: {
                    Aceptar: {
                        label: 'Ok',
                        callback: function () {
                            mensajeProducto = false;
                            $("#txtID").val('');
                            $("#txtID").focus();
                        }
                    }
                }
            });
        }
        DesbloquearPantalla();
        return false;
    }
    var producto = msg.d;
    $("#txtDescripcion").val(producto.Descripcion);
    $("#hdnProductoID").val(producto.ProductoId);
    $('#txtSubFamilia').val(producto.SubFamilia.Descripcion);
    obtenerIndicadores();
    DesbloquearPantalla();
};

formulaValido = function(msg) {
    if (msg.d == null || msg.d.length == 0) {
        if (!mensajeProducto) {
            mensajeProducto = true;
            bootbox.dialog({
                message: window.formulaInvalida,
                buttons: {
                    Aceptar: {
                        label: 'Ok',
                        callback: function () {
                            mensajeProducto = false;
                            $("#txtID").val('');
                            $("#txtID").focus();
                        }
                    }
                }
            });
        }
        DesbloquearPantalla();
        return false;
    }
    var formula = msg.d;
    $("#txtDescripcion").val(formula.Descripcion);
    $("#hdnProductoID").val(formula.Producto.ProductoId);
    $('#txtSubFamilia').val(formula.Producto.SubFamilia.Descripcion);
    obtenerIndicadores();
    DesbloquearPantalla();
};

configurarControlesTemplate = function() {
    $('#txtFecha').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "es"
    });

    //$('.alfanumerico').numeric();
    $('.soloNumeros').numericInput();
    
    $("#txtID").attr("oninput","maxLengthCheck(this)");
    $("#txtID").attr("maxlength", "7");
    $("#txtID").addClass("enter");

    asignarEventosTextoClaveProductoFormula();

    $('#txtFecha').attr("disabled", 'disabled');
    $('#txtDescripcion').attr("disabled", 'disabled');
    $('#txtSubFamilia').attr("disabled", 'disabled');
};

limpiar = function() {
    $('#txtFecha').val($.datepicker.formatDate('dd/mm/yy', new Date()));
    $("#btnGuardar").attr("disabled", true);
    $("#txtFolioPaseProceos").val('');
};

habilitaDeshabilitaFolioPase = function(formula) {
    if (formula) {
        $("#txtFolioPaseProceos").attr("disabled", 'disabled');
        $("#btnAyudaFolio").attr("disabled", 'disabled');
        asignarEventoAyudaFolio(!formula);
    } else {
        $("#txtFolioPaseProceos").removeAttr("disabled");
        $("#btnAyudaFolio").removeAttr("disabled");
        asignarEventoAyudaFolio(!formula);
    }
};

limpiarTemplates = function() {
    $('#divProductoFormula').html('');
    $('#divObservaciones').html('');
    $("#divGridFormula").val('');
    $("#divGridProductos").val('');
    $("#divGridFolio").val('');
};

cambioEnCombo = function() {
    limpiar();
    var tipoMovimiento = $("#ddlTipoMovimiento").val();
    $("#txtFolioPaseProceos").val('');
    if (tipoMovimiento > 0) {
        if (tipoMovimiento == tiposMovimiento.PaseProceso.value) {
            limpiarTemplates();
            habilitaDeshabilitaFolioPase(false);
            $("#txtFolioPaseProceos").focus();
        } else {
            habilitaDeshabilitaFolioPase(true);
            cargarSeccionSeleccione(tipoMovimiento);
        }
    } else {
        limpiarTemplates();
        habilitaDeshabilitaFolioPase(true);
    }
};

asignaEventosCombo = function() {
    $("#ddlTipoMovimiento").change(function() {
        cambioEnCombo();
    });

    $("#ddlTipoMovimiento").focusout(function () {
        if (!presionoEnter) {
            cambioEnCombo();
        }
        presionoEnter = false;
    });

    $("#ddlTipoMovimiento").keydown(function(event) {
        if (event.which == 13) {
            if (!$("#txtFolioPaseProceos").disabled) {
                presionoEnter = true;
                $("#txtFolioPaseProceos").focus();
            }
        }
    });
};

asignarEventosFolioPasePRoceso = function() {
    $("#txtFolioPaseProceos").keypress(function(event) {
        if (event.which == 13) {
            presionoEnter = true;
            validarFolioProceso();
            mensajeFolio = false;
        }
    });

    $("#txtFolioPaseProceos").focusout(function () {
        if (!presionoEnter) {
            validarFolioProceso();
            mensajeFolio = false;
        }
        presionoEnter = false;
    });
};

validarFolioProceso = function() {
    if (!mensajeFolio) {
        var folioPedido = TryParseInt($("#txtFolioPaseProceos").val(), 0);
        if (folioPedido > 0) {
            BloquearPantalla();
            buscarFolioPaseProceso(folioPedido);
        }
    }
};

asignarEventosTextoClaveProductoFormula = function() {
    $("#txtID").keydown(function(event) {
        if (event.which == 13) {
            presionoEnter = true;
            disparadorEventoTxtClave();
        } else {
            if (event.which == 8 || event.which == 46) {
                limpiarControlesSeccionProductoFormula();
                presionoEnter = false;
            } else {
                limpiarControlesSeccionProductoFormula();
                $("#hdnProductoID").val('');
                $("#btnGuardar").attr("disabled", true);
                presionoEnter = false;
            }
        }
    });

    $("#txtID").focusout(function () {
        if (!presionoEnter) {
            disparadorEventoTxtClave();
        }
        presionoEnter = false;
    });

    $("#txtID").change(function () {
        $("#hdnProductoID").val('');
        limpiarControlesSeccionProductoFormula();
        $("#btnGuardar").attr("disabled", true);
        presionoEnter = false;
    });
};

disparadorEventoTxtClave = function() {
    var subFamilia = $("#txtSubFamilia").val().length;
    if (subFamilia === 0
            || $("#hdnProductoID").val().length == 0) {
        tipoMovimiento = $("#ddlTipoMovimiento").val();
        if (tipoMovimiento == tiposMovimiento.PaseProceso.value) {
            validarClaveProducto();
        } else {
            validarClaveFormula();
        }
    }
    //mensajeProducto = false;
    //mensajeIndicadores = false;
};

validarClaveProducto = function() {
    if (!mensajeProducto) {
        var productoId = TryParseInt($("#txtID").val(), 0);
        if (productoId > 0) {
            BloquearPantalla();
            var pedidoId = $("#hdnPedidoID").val();
            buscarProducto(pedidoId, productoId);
        }
    }
};

validarClaveFormula = function() {
    if (!mensajeProducto) {
        var formulaId = TryParseInt($("#txtID").val(), 0);
        if (formulaId > 0) {
            BloquearPantalla();
            buscarFormula(formulaId);
        }
    }
};

limpiarControlesSeccionProductoFormula = function() {
    $('#txtDescripcion').val('');
    $('#txtSubFamilia').val('');
    $('#divObservaciones').html('');
};

obtenerIndicadores = function() {
    var jIndicadores = { "pedidoID": $("#hdnPedidoID").val(), "productoID": $("#hdnProductoID").val() };
    EjecutarWebMethod(window.location.pathname + '/ObtenerIndicadores', jIndicadores, asignarIndicadores, window.errorConsultarFolioProceso);
};

asignarIndicadores = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        if (!mensajeIndicadores) {
            mensajeIndicadores = true;
            bootbox.dialog({
                message: 'No se encuentran registrados indicadores para los valores ingresados. Favor de verificar.',
                buttons: {
                    Aceptar: {
                        label: 'Ok',
                        callback: function () {
                            mensajeIndicadores = false;
                            $("#txtDescripcion").val('');
                            $("#txtID").val('');
                            $("#txtSubFamilia").val('');
                            $("#txtID").focus();
                        }
                    }
                }
            });
        }
        DesbloquearPantalla();
        return false;
    }
    var indicadores = msg.d;
    var items = [];

    $("#hdnIndicadores").val(indicadores);
    var indicador = "";
    cantidadIndicadores = 0;
    $(indicadores).find('semaforo').each(function() {
        var item = { };
        if (indicador === "" || indicador != $(this).find("Indicador").text()) {
            item.Descripcion = $(this).find("Indicador").text();
            item.RangoObjetivo = $(this).find("Rango").text();
            item.Medicion = $(this).find("Medicion").text();
            item.Resultado = $(this).find("Resultado").text();

            items.push(item);
            
            cantidadIndicadores++;
        }
        indicador = $(this).find("Indicador").text();
    });

    var recursosObservaciones = { };
    recursosObservaciones.IndicadoresCalidad = window.indicadoresCalidad;
    recursosObservaciones.Resultado = window.resultado;
    recursosObservaciones.RangoObjetivo = window.rangoObjetivo;
    recursosObservaciones.Observaciones = window.observaciones;

    var observaciones = { };
    observaciones.Recursos = recursosObservaciones;
    observaciones.Indicadores = items;

    $('#divObservaciones').html("");
    $('#divObservaciones').setTemplateURL('../Templates/CalidadIndicadoresObservaciones.htm');
    $('#divObservaciones').processTemplate(observaciones);
    
    $("#txtProteina").focus();

    $("#txtObservaciones").attr("maxlength", "255");

    $('.soloNumeros').numericInput();
    
    //$(".alfanumerico").inputmask({ "mask": "9", "repeat": 9, "greedy": false, "numericInput": true, "autoUnmask": true });
    //$('.alfanumerico').numericInput();
    $(".alfanumerico").addClass("textoDerecha");
    $(".alfanumerico").attr("oninput", "maxLengthCheck(this)");
    $(".alfanumerico").attr("maxlength", "5");

    var inputs = $("#dvIndicadores .alfanumerico");
    inputs.eq(inputs.index(this) + 1).focus();
    inputs.each(function () {
        var divSemaforo = $(this).closest('#dvIndicadores').find('#dvSemaforo');
        if (!divSemaforo.hasClass("deshabilitado")) {
            divSemaforo.addClass("deshabilitado");
        }
        $(this).on("keydown", function(event) {
            var nombre;
            if (event.which == 13) {
                validarIndicador = true;
                nombre = $(this).closest('#dvIndicadores').find(':input[name=*]');
                if ((inputs.index(this) + 1) === cantidadIndicadores) {
                    $("#txtObservaciones").focus();
                } else {
                    inputs.eq(inputs.index(this) + 1).focus();
                }
                consultaSemaforoIndicador($(this), nombre.attr("name"));
            }
            if (event.which == 9) {
                validarIndicador = true;
                nombre = $(this).closest('#dvIndicadores').find(':input[name=*]');
                consultaSemaforoIndicador($(this), nombre.attr("name"));
            }
        });
        $(this).on("focusout", function () {
            if (!validarIndicador) {
                var nombre = $(this).closest('#dvIndicadores').find(':input[name=*]');
                consultaSemaforoIndicador($(this), nombre.attr("name"));
            }
            validarIndicador = false;
        });
    });
    $("#btnGuardar").attr("disabled", false);
};

consultaSemaforoIndicador = function(valor, indicador) {
    var valorIndicador = TryParseDecimal(valor.val(), -1);
    if (valorIndicador >= 0) {
        BloquearPantalla();
        var jIndicadores = {
            "valorIndicador": valorIndicador,
            "descripcionIndicador": indicador,
            "indicadores": $("#hdnIndicadores").val()
        };
        EjecutarWebMethod(window.location.pathname + '/ValidarIndicadores', jIndicadores, asignarValoresSemaforo
                        , window.errorConsultarSemaforo);
    }
};

asignarValoresSemaforo = function(msg) {
    var indicadores = msg.d;

    $("#hdnIndicadores").val(indicadores);

    var inputs = $("#dvIndicadores .alfanumerico");
    objetivoInvalido = false;
    $(inputs).each(function() {
        var txt = $(this);
        var nombre = $(this).closest('#dvIndicadores').find(':input[name=*]');
        var divSemaforo = $(this).closest('#dvIndicadores').find('#dvSemaforo');
        $(indicadores).find('semaforo').each(function() {
            if (nombre.attr("name") === $(this).find("Indicador").text()) {
                var resultado = TryParseDecimal($(this).find("Resultado").text(), 0);
                if (resultado > 0) {
                    txt.val($(this).find("Resultado").text());
                } else {
                    txt.val('');
                }
                var clase = divSemaforo.attr("class");
                divSemaforo.removeClass(clase);
                if ($(this).find("ColorSemaforo").text().length > 0) {
                    divSemaforo.addClass($(this).find("ColorSemaforo").text());
                } else {
                    divSemaforo.addClass("deshabilitado");
                }
                var valido = TryParseDecimal($(this).find("Valido").text(), 0);

                if (resultado > 0) {
                    if (valido <= 0) {
                        objetivoInvalido = true;
                    }
                }
            }
        });
    });
    if (objetivoInvalido) {
        if (!$("#observacionRequerida").hasClass()) {
            $("#observacionRequerida").text('*');
            $("#observacionRequerida").addClass("requerido");
        }
    } else {
        if ($("#observacionRequerida").hasClass("requerido")) {
            $("#observacionRequerida").text('');
            $("#observacionRequerida").removeClass("requerido");
        }
    }
    DesbloquearPantalla();
};

asignarEventosBotonGuardar = function() {
    $("#btnGuardar").click(function() {
        BloquearPantalla();
        var guardar = validaGuardar();
        if (guardar) {
            guardarIndicadores();
        } else {
            DesbloquearPantalla();
        }
    });
};

asignarEventosBotonCancelar = function() {
    $('#btnCancelar').click(function() {
        bootbox.dialog({
            message: window.Cancelar,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function() {
                        limpiarPantalla();
                    }
                },
                Cancelar: {
                    label: window.No,
                    callback: function() {

                    }
                }
            }
        });
    });
};

guardarIndicadores = function() {
    var jIndicadores = {
        "indicadoresMateriaPrima": $("#hdnIndicadores").val(),
        "observaciones": $("#txtObservaciones").val(),
        "movimiento": $("#ddlTipoMovimiento").val()
    };
    EjecutarWebMethod(window.location.pathname + '/Guardar', jIndicadores, guardadoExitoso, "");
};

guardadoExitoso = function(msg) {
    DesbloquearPantalla();
    bootbox.dialog({
        message: window.MsgGuardadoExitoso,
        buttons: {
            Aceptar: {
                label: 'Ok',
                callback: function() {
                    $("#ddlTipoMovimiento").focus();
                }
            }
        }
    });
    limpiarPantalla();
};

limpiarPantalla = function() {
    limpiar();
    limpiarTemplates();
    $("#ddlTipoMovimiento").focus();
    $("#ddlTipoMovimiento").val(0);

    $("#hdnPedidoID").val(0);
    $("#hdnIndicadores").val('');
    $("#hdnProductoID").val(0);

    mensajeProducto = false;
    mensajeIndicadores = false;
    mensajeFolio = false;

    tipoMovimiento = 0;
    cantidadIndicadores = 0;

    objetivoInvalido = false;
    
    habilitaDeshabilitaFolioPase(true);
};

validaGuardar = function() {
    var guardar = true;
    var inputs = $("#dvIndicadores .alfanumerico");
    inputs.each(function () {
        var txt = $(this);
        if (!$.trim(txt.val())) {
            if (guardar) {
                bootbox.dialog({
                    message: window.camposFaltantes + " " + $(this).attr("name") + ".",
                    buttons: {
                        Aceptar: {
                            label: 'Ok',
                            callback: function () {
                                txt.focus();
                            }
                        }
                    }
                });
                guardar = false;
            }
        }
    });
    if (guardar) {
        if (objetivoInvalido) {
            if (!$.trim($("#txtObservaciones").val())) {
                bootbox.dialog({
                    message: window.observacionesFaltantes,
                    buttons: {
                        Aceptar: {
                            label: 'Ok',
                            callback: function () {
                                $("#txtObservaciones").focus();
                            }
                        }
                    }
                });
                guardar = false;
            }
        }
    }
    return guardar;
};
//Ayudas
asignarEventoAyudaFolio = function(habilitar) {
    //Evento que se dispara para abrir el modal de ayuda de Folio
    if (habilitar) {
        $('#btnAyudaFolio').on("click", function() {
            $("#txtFolioBusqueda").val('');
            $('#modalBusquedaFolio').modal('show');
        });
    } else {
        $('#btnAyudaFolio').off("click");
    }
};

eventosBusquedaFolio = function() {
    $('#modalBusquedaFolio').bind('hidden.bs.modal', function() {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaFolio').bind('show.bs.modal', function() {
        $('#txtProductoBusqueda').focus();
        CargarGridBusquedaFolioDefault();
        buscarFolioAyuda();
        $("html").css("margin-right", "-15px");
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarFolio').on("click", function() {
        buscarFolioAyuda();
    });

    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarFolio').on("click", function() {
        var renglon = $("#GridProductos tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje('Debe seleccionar un folio de pase a proceso.', null);
            return false;
        }
        var folioId = renglon.find('.columnaID').text().trim();
        $('#txtFolioPaseProceos').val(folioId);
        $('#modalBusquedaFolio').modal('hide');
        validarFolioProceso();
        return true;
    });

    $('#btnCancelarFolio').on("click", function() {
        MensajeCerrarModalFolio();
    });

    $('.cerrarBusquedaFolio').on("click", function() {
        MensajeCerrarModalFolio();
    });

    $('#divGridFolio').on("dblclick", "#GridProductos tbody tr", function() {
        var folioId = $(this).find('.columnaID').text().trim();
        $('#txtFolioPaseProceos').val(folioId);
        $('#modalBusquedaFolio').modal('hide');
        validarFolioProceso();
    });
    
    $("#txtFolioBusqueda").on("keyup", function (event) {
        if (event.which == 13) {
            buscarFolioAyuda();
        }
    });
};

eventosBusquedaProducto = function () {
    //Evento para quitarle el Estilo del modal cuando se cierra la pantalla
    $('#modalBusquedaProducto').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });
    
    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaProducto').bind('show.bs.modal', function () {
        mensajeCancelar = false;
        $('#txtProductoBusqueda').focus();
        CargarGridBusquedaProductoDefault();
        BuscarProductos();
        $("html").css("margin-right", "-15px");
    });
    
    //Evento que se dispara para abrir el modal de ayuda de Productos
    $('#btnAyudaProductos').on("click", function () {
        mensajeCancelar = false;
        $("#txtProductoBusqueda").val('');
        $('#modalBusquedaProducto').modal('show');
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarProducto').on("click", function () {
        mensajeCancelar = false;
        BuscarProductos();
    });
    
    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarProducto').on("click", function() {
        mensajeCancelar = false;
        var renglon = $("#GridProductos tr.highlight");
        if (renglon.length == 0) {
            if (!mensajeAgregar) {
                mensajeAgregar = true;
                MostrarMensaje('Debe seleccionar un producto.', function () {
                    mensajeAgregar = false;
                });
            }            
            return false;
        }
        var productoId = renglon.find('.columnaID').text().trim();
        var descripcion = renglon.find('.columnaDescripcion').text().trim();
        $('#txtID').val(productoId);
        $('#txtDescripcion').val(descripcion);
        $('#modalBusquedaProducto').modal('hide');
        disparadorEventoTxtClave();
        return true;
    });
    
    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarProducto').on("click", function () {
        MensajeCerrarModalProducto();
    });
    
    $('.cerrarBusquedaProductos').on("click", function () {
        MensajeCerrarModalProducto();
    });
    
    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridProductos').on("dblclick", "#GridProductos tbody tr", function() {
        var productoId = $(this).find('.columnaID').text().trim();
        var descripcion = $(this).find('.columnaDescripcion').text().trim();
        $('#txtID').val(productoId);
        $('#txtDescripcion').val(descripcion);
        $('#modalBusquedaProducto').modal('hide');
        disparadorEventoTxtClave();
    });

    $("#txtProductoBusqueda").on("keyup", function (event) {
        if (event.which == 13) {
            BuscarProductos();
        }
    });
};

eventosBusquedaFormula = function () {
    //Evento para quitarle el Estilo del modal cuando se cierra la pantalla
    $('#modalBusquedaFormula').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaFormula').bind('show.bs.modal', function () {
        $('#txtFormulaBusqueda').focus();
        CargarGridBusquedaFormulaDefault();
        BuscarFormula();
        $("html").css("margin-right", "-15px");
    });
    
    //Evento que se dispara para abrir el modal de ayuda de Productos
    $('#btnAyudaProductos').on("click", function () {
        mensajeCancelar = false;
        $("#txtFormulaBusqueda").val('');
        $('#modalBusquedaFormula').modal('show');
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarFormula').on("click", function () {
        mensajeCancelar = false;
        BuscarFormula();
    });

    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarFormula').on("click", function() {
        mensajeCancelar = false;
        var renglon = $("#GridProductos tr.highlight");
        if (renglon.length == 0) {
            if (!mensajeAgregar) {
                mensajeAgregar = true;
                MostrarMensaje('Debe seleccionar una fórmula.', function () {
                    mensajeAgregar = false;
                });
            }
            return false;
        }
        var formulaId = renglon.find('.columnaID').text().trim();
        var descripcion = renglon.find('.columnaDescripcion').text().trim();
        $('#txtID').val(formulaId);
        $('#txtDescripcion').val(descripcion);
        $('#modalBusquedaFormula').modal('hide');
        disparadorEventoTxtClave();
        return true;
    });

    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarFormula').on("click", function () {
        MensajeCerrarModalFormula();
    });

    $('.cerrarBusquedaFormula').on("click", function () {
        MensajeCerrarModalFormula();
    });

    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridFormula').on("dblclick", "#GridProductos tbody tr", function () {
        mensajeCancelar = false;
        var formulaId = $(this).find('.columnaID').text().trim();
        var descripcion = $(this).find('.columnaDescripcion').text().trim();
        $('#txtID').val(formulaId);
        $('#txtDescripcion').val(descripcion);
        $('#modalBusquedaFormula').modal('hide');
        disparadorEventoTxtClave();
    });

    $("#txtFormulaBusqueda").on("keyup", function (event) {
        if (event.which == 13) {
            mensajeCancelar = false;
            BuscarFormula();
        }
    });
};

CargarGridBusquedaProductoDefault = function () {
    var datos = {};
    
    var recursos = {};
    recursos.Id = 'Id';
    recursos.Descripcion = 'Descripción';

    datos.Recursos = recursos;
    datos.Productos = {};
    var divContenedor = $('#divGridProductos');

    divContenedor.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedor.processTemplate(datos);
};

CargarGridBusquedaFormulaDefault = function () {
    var datos = {};

    var recursos = {};
    recursos.Id = 'Clave';
    recursos.Descripcion = 'Descripción';

    datos.Recursos = recursos;
    datos.Productos = {};
    var divContenedor = $('#divGridFormula');

    divContenedor.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedor.processTemplate(datos);
};

CargarGridBusquedaFolioDefault = function () {
    var datos = {};

    var recursos = {};
    recursos.Id = 'Folio';
    recursos.Descripcion = 'Almacén';

    datos.Recursos = recursos;
    datos.Productos = {};
    var divContenedor = $('#divGridFolio');

    divContenedor.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedor.processTemplate(datos);
};

MensajeCerrarModalProducto = function () {
    if (!mensajeCancelar) {
        mensajeCancelar = true;
        bootbox.dialog({
            message: '¿Está seguro que desea salir sin agregar el producto?.',
            buttons: {
                Aceptar: {
                    label: 'Si',
                    callback: function() {
                        $('#modalBusquedaProducto').modal('hide');
                        $("#txtID").focus();
                    }
                },
                Cancelar: {
                    label: 'No',
                    callback: function() {
                        mensajeCancelar = false;
                    }
                }
            }
        });
    }
};

MensajeCerrarModalFormula = function () {
    if (!mensajeCancelar) {
        mensajeCancelar = true;
        bootbox.dialog({
            message: '¿Está seguro que desea salir sin agregar la fórmula?',
            buttons: {
                Aceptar: {
                    label: 'Si',
                    callback: function() {
                        $('#modalBusquedaFormula').modal('hide');
                        $("#txtID").focus();
                    }
                },
                Cancelar: {
                    label: 'No',
                    callback: function() {
                        mensajeCancelar = false;
                    }
                }
            }
        });
    }
};

MensajeCerrarModalFolio = function() {
    bootbox.dialog({
        message: '¿Está seguro que desea salir sin agregar el folio?',
        buttons: {
            Aceptar: {
                label: 'Si',
                callback: function() {
                    $('#modalBusquedaFolio').modal('hide');
                    return true;
                }
            },
            Cancelar: {
                label: 'No',
                callback: function() {
                    return true;
                }
            }
        }
    });
};

BuscarProductos = function () {
    var datosMetodos = { 'productoDescripcion': $('#txtProductoBusqueda').val(), "pedidoID": $("#hdnPedidoID").val() };
    var urlMetodos = window.location.pathname + '/ObtenerProductos';
    var mensajeErrorMetodos = 'Ocurrio un error al consultar los productos';
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarProductosSuccess, mensajeErrorMetodos);
};

BuscarFormula = function () {
    var datosMetodos = { 'descripcionFormula': $('#txtFormulaBusqueda').val() };
    var urlMetodos = window.location.pathname + '/ObtenerFormulaPagina';
    var mensajeErrorMetodos = 'Ocurrio un error al consultar las fórmulas';
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarFormulaSuccess, mensajeErrorMetodos);
};

buscarFolioAyuda = function() {
    var datosMetodos = { 'almacen': $('#txtFolioBusqueda').val() };
    var urlMetodos = window.location.pathname + '/ObtenerFolios';
    var mensajeErrorMetodos = 'Ocurrio un error al consultar los folios';
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarFolioSuccess, mensajeErrorMetodos);
};

BuscarProductosSuccess = function (msg) {
    var datos = {};
    
    var recursos = {};
    recursos.Id = 'Id';
    recursos.Descripcion = 'Descripción';

    datos.Recursos = recursos;
    datos.Productos = msg.d;
    var divContenedorGrid = $('#divGridProductos');
    divContenedorGrid.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedorGrid.processTemplate(datos);
    $("#GridProductos tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#GridProductos tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};

BuscarFormulaSuccess = function (msg) {    
    var datos = {};

    var recursos = {};
    recursos.Id = 'Clave';
    recursos.Descripcion = 'Descripción';

    var formulas = msg.d;
    var resultadoFormula = new Array();
    for (var i = 0; i < formulas.length; i++) {
        var item = {};
        item.ProductoId = formulas[i].FormulaId;
        item.Descripcion = formulas[i].Descripcion;

        resultadoFormula.push(item);
    }

    datos.Recursos = recursos;
    datos.Productos = resultadoFormula;
    var divContenedorGrid = $('#divGridFormula');
    divContenedorGrid.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedorGrid.processTemplate(datos);
    $("#GridProductos tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#GridProductos tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};

BuscarFolioSuccess = function (msg) {
    var datos = {};

    var recursos = {};
    recursos.Id = 'Folio';
    recursos.Descripcion = 'Almacén';

    var folios = msg.d;
    var resultadoFolio = new Array();
    for (var i = 0; i < folios.length; i++) {
        var item = { };
        item.ProductoId = folios[i].FolioPedido;
        item.Descripcion = folios[i].Almacen.Descripcion;

        resultadoFolio.push(item);
    }

    datos.Recursos = recursos;
    datos.Productos = resultadoFolio;
    var divContenedorGrid = $('#divGridFolio');
    divContenedorGrid.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedorGrid.processTemplate(datos);
    $("#GridProductos tr").click(function() {
        var selected = $(this).hasClass("highlight");
        $("#GridProductos tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};
