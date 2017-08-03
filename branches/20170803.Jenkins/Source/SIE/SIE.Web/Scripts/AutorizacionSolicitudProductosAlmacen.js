/// <reference path="../Templates/GridAutorizacionSolicitudProductosAlmacen.html" />
/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.FixedHeaderTable.js" />
/// <reference path="~/Scripts/jscomun.js" />

var txtSolicitudProductoId;
var $txtFolio;
var $btnBuscar;
var $txtAutoriza;
var $txtFechaSolicitud;
var $tblDatos;
var $txtObservaciones;
var $btnGuardar;
var $btnCancelar;
var $hdAutorizaId;
var $divGridProductos;
var $btnAyudaFolio;

//[Botones para la ayudafolio]
var $btnAyudaFolioAgregar;
var $btnAyudaFolioSelecionar;
var $btnAyudaFolioCancelar;
var $btnAyudaFolioCerrar;
//[Inputs para la ayudafolio]
var $txtAyudaFolio;

var rutaPantalla = location.pathname;
var templateGridAyudaSolicitud = '../Templates/GridAyudaSolicitudProducto.htm';
var templateGridProductos = '../Templates/GridAutorizacionSolicitudProductosAlmacen.htm';

var recursos = {};
var urlMetodos;
var datosMetodos;
var mensajeErrorMetodos;
var editandoRegistro = false;
var pesajeMateriaPrimaID;
var usuarioAutorizo;
var puedeGuardar = true;

//[Recursos]
//---------------------------------------------------------------------------------------
var MsgNoExisteFolio = window.MsgNoExisteFolio;
var MsgErrorConsultarFolio = window.MsgFolioEntradaNoExiste;
var MsgGuardaExito = window.MsgGuardaExito;

//[Ready]
//---------------------------------------------------------------------------------------

$(document).ready(function () {
    Inicializar();
    AsignarEventosControles();
    validarRolUsuario();
});
//[funciones]
//---------------------------------------------------------------------------------------

validarRolUsuario = function() {
    var datosUsuario = { };
    urlMetodos = rutaPantalla + '/ValidaRolUsuario';
    mensajeErrorMetodos = "Ocurrio un error al consultar el rol del usuario";
    EjecutarWebMethod(urlMetodos, datosUsuario, validarRolSuccess, mensajeErrorMetodos);
};

validarRolSuccess = function(msg) {
    var rolAutorizador = msg.d;
    if (!rolAutorizador) {
        MostrarMensaje("El usuario no cuenta con el rol de autorizador", function() {
            window.location = '../Principal.aspx';
        });
    }
};

Inicializar = function() {
    txtSolicitudProductoId = 0;
    puedeGuardar = true;
    $txtFolio = $('#txtFolio');
    $btnBuscar = $('#btnBuscar');
    $txtAutoriza = $('#txtAutoriza');
    $txtFechaSolicitud = $('#txtFechaSolicitud');
    $tblDatos = $('#tblDatos');
    $txtObservaciones = $('#txtObservaciones');
    $btnGuardar = $('#btnGuardar');
    $btnCancelar = $('#btnCancelar');
    $hdAutorizaId = $('#hdAutorizaId');
    $divGridProductos = $('#divGridProductos');
    $btnAyudaFolio = $("#btnAyudaFolio");
    
    $txtFolio.numericInput().attr("maxlength", "8");
    $txtAutoriza.attr("disabled", true);
    $txtFechaSolicitud.attr("disabled", true);
    
    $btnAyudaFolioAgregar = $("#btnAyudaFolioAgregar");
    $btnAyudaFolioSelecionar = $("#btnAyudaFolioSelecionar");
    $btnAyudaFolioCancelar = $("#btnAyudaFolioCancelar");
    $btnAyudaFolioCerrar = $("#btnAyudaFolioCerrar");

    $txtObservaciones.attr("maxlength", "255");

    LimpiarPantalla();
};

LlenaGridAutorizaciones = function (data) {
    var grid = {};
    var recursos = {};

    recursos.Codigo = window.ColumnaGridCodigo;
    recursos.Articulo = window.ColumnaGridArtiulo;
    recursos.Cantidad = window.ColumnaGridCantidad;
    recursos.UnidadMedicion = window.ColumnaGridUnidadMedicion;
    recursos.Desctipcion = window.ColumnaGridDescripcion;
    recursos.ClaseCosto = window.ColumnaGridClaseCosto;
    recursos.CentroCosto = window.ColumnaGridCentroCosto;
    recursos.Activo = window.ColumnaGridAutorizar;

    grid.Recursos = recursos;
    grid.Productos = data;
    
    $divGridProductos.html('');
    $divGridProductos.setTemplateURL(templateGridProductos);
    $divGridProductos.processTemplate(grid);

};

LimpiarPantalla = function () {
    var fecha = new Date();

    $txtFolio.val('');
    $btnBuscar.attr("disabled", false);
    $txtAutoriza.val('');
    //$txtFechaSolicitud.val(FechaFormateada(fecha));
    $txtFechaSolicitud.val("");
    $tblDatos.html('');
    $txtObservaciones.val('');
    $btnGuardar.attr("disabled", true);
    $btnCancelar.attr("disabled", false);
    $hdAutorizaId.val('0');
    LlenaGridAutorizaciones(null);
    puedeGuardar = true;
    ObtenerUsuario();
};

ValidaGuardar = function () {
    var resultado = true;
    var mensaje = "";
    if($txtObservaciones.val() == "") {
        mensaje = window.MensajeCapturarObservaciones;
    }
    
    if (mensaje != "") {
        bootbox.dialog({
            //title: window.MensajeInformacion,
            message: mensaje,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    //className: "btn SuKarne",
                    callback: function () {
                        $txtObservaciones.focus();
                    }
                }
            }
        });
        return false;
    }
    return resultado;
};

Guardar = function () {
    BloquearDiv("#pantallaCompleta");
    if (puedeGuardar) {
        var info = ValidaGuardar();
        if (!info) {
            DesbloquearPantalla();
            return false;
        }
        puedeGuardar = false;
        var solicitud = {};
        solicitud.SolicitudProductoID = txtSolicitudProductoId;
        solicitud.FolioEntrada = $txtFolio.val();
        solicitud.ObservacionUsuarioAutoriza = $txtObservaciones.val().trim();
        //var renglones = $('#GridProductos tbody tr .columnaCheck:checked');
        var renglones = $('#GridProductos tbody tr');

        var productos = [];

        renglones.each(function () {
            //apuntamos al renglon para recuperar los valores
            var renglon = $(this);
            var producto = {};
            producto.SolicitudProductoDetalleID = renglon.attr("data-SolicitudProductoDetalleID");
            producto.ProductoId = renglon.find(".Producto").text().trim();
            producto.Cantidad = renglon.find(".Cantidad").text().trim();
            producto.Activo = renglon.find(".columnaCheck").is(':checked') ? 1 : 0;
            productos.push(producto);
        });

        solicitud.Detalle = productos;
        
        datosMetodos = { 'solicitud': solicitud };
        urlMetodos = rutaPantalla + '/Guardar';
        mensajeErrorMetodos = window.MensajeErrorGuardar;
        EjecutarWebMethod(urlMetodos, datosMetodos, GuardarSuccess, mensajeErrorMetodos);
    }
};

//Funnción que se ejecuta despues de llamar el guardar de la pantalla.
GuardarSuccess = function () {
    MostrarMensaje(window.MensajeGuardoExito, null);
    LimpiarPantallaCompleta();
    DesbloquearPantalla();
};

//Función para asignarle los eventos a los controles de la pantalla
AsignarEventosControles = function () {
    $(':input').not('.textoAyuda').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var tabIndex = $(this).attr('tabindex');
            tabIndex = tabIndex + 1;
            var totalControles = $('#divContenedor :input').length;
            var buscaHabilitado = true;
            while (buscaHabilitado) {
                var control = $('[tabindex=' + tabIndex + ']');
                if (control.is(':enabled')) {
                    control.focus();
                    buscaHabilitado = false;
                }
                tabIndex = tabIndex + 1;
                if (tabIndex > totalControles) {
                    buscaHabilitado = false;
                }
            }

        }
    });
    
    $txtFolio.keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            if ($('#txtFolio').val() != '') {
                $('#txtTicket').focus();
            }
        }
    });

    $txtFolio.focusout(function (e) {
        var valor = $(this).val();
        LlenaGridAutorizaciones(null);
        if (valor == '0') {

            e.preventDefault();
            return false;
        }
        if (valor != '') {
            if (!ObtenerPorFolioSolicitud()) {
                e.preventDefault();
            }
        }
        return false;
    });

    $txtFolio.keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#btnLimpiar').click(function () {
        LimpiarPantalla();
    });
    
    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#btnCancelar').click(function () {
        bootbox.dialog({
            message: window.MensajeCancelarPantalla,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        LimpiarPantallaCompleta();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.No,
                    callback: function () {
                        return true;
                    }
                }
            }
        });
    });

    $('body').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    EventosBusquedaFolio();
};

//Función para asignarle los eventos a los controles de la ayuda de Producto
EventosBusquedaFolio = function () {
    //Evento para quitarle el Estilo del modal cuando se cierra la pantalla
    $('#modalBusquedaFolio').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaFolio').bind('show.bs.modal', function () {
        CargarGridBusquedaFolioDefault();
        $("html").css("margin-right", "-15px");
        $('#txtFolioBusqueda').focus();
    });

    //Evento que se dispara para abrir el modal de ayuda de Productos
    $('#btnAyudaFolio').click(function () {
        $('#modalBusquedaFolio').modal('show');
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarFolio').click(function () {
        ObtenerPorPagina();
    });

    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarFolio').click(function () {
        var renglon = $("#GridAyudaSolicitudProducto tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje(window.MensajeSeleccionarFolio, null);
            return false;
        }
        var folioId = renglon.find('.columnaID').text().trim();
        txtSolicitudProductoId = renglon.attr('data-FolioID');

        //var descripcion = renglon.find('.columnaDescripcion').text().trim();
        $('#txtFolio').val(folioId);
        $('#modalBusquedaFolio').modal('hide');
        ObtenerPorFolioSolicitud();

        return true;
    });

    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarFolio').click(function () {
        MensajeCerrarModalFolio();
    });

    $('.cerrarBusquedaFolio').click(function () {
        MensajeCerrarModalFolio();
    });

    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridAyudaSolicitudProducto').on("dblclick", "#GridAyudaSolicitudProducto tbody tr", function () {
        //var renglon = $("#GridAyudaSolicitudProducto tr.highlight");
        var folioId = $(this).find('.columnaID').text().trim();
        txtSolicitudProductoId = $(this).attr('data-FolioID');
        //var descripcion = $(this).find('.columnaDescripcion').text().trim();
        $txtFolio.val(folioId);
        $txtFolio.attr("disabled", true);
        //$('#txtDescripcionProducto').val(descripcion);
        $('#modalBusquedaFolio').modal('hide');
        //ObtenerValoresProduccionProducto();
        ObtenerPorFolioSolicitud();
    });

    $('#txtFolioBusqueda').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            ObtenerPorPagina();
        }
    });
};

//Función para cargar el grid de la ayuda de productos
CargarGridBusquedaFolioDefault = function () {
    var datos = {};
    CargarRecursosGridBusquedaFolios();
    datos.Recursos = recursos;
    datos.Productos = {};
    var divContenedor = $('#divGridAyudaSolicitudProducto');
    divContenedor.setTemplateURL(templateGridAyudaSolicitud);
    divContenedor.processTemplate(datos);
    setTimeout(function () {
        ObtenerPorPagina();
    }, 500);
};

//Función para cargar los recursos  del grid busqueda de productos
CargarRecursosGridBusquedaFolios = function () {
    recursos = {};
    recursos.Id = window.ColumnaGridAyudaClave;
    recursos.Descripcion = window.ColumnaGridayudaDescripcion;
    recursos.FolioDetalleId = 'FolioId';
};

//[Funcionalidad para la búsqueda por Id]
//Función para buscar las solicitudes por su Id
ObtenerUsuario = function () {
    BloquearDiv("#pantallaCompleta");
    //var folioSolicitudInfo = {};
    //folioSolicitud = $txtFolio.val();
    datosMetodos = {};
    urlMetodos = rutaPantalla + '/ObtenerUsuario';
    mensajeErrorMetodos = window.MensajeErrorObtenerUsuario;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerUsuarioSuccess, mensajeErrorMetodos);
};

//Función para buscar el suaurio logueado
ObtenerUsuarioSuccess = function (msg) {
    if (msg.d != null) {
        $txtAutoriza.val(msg.d.Nombre);
    }
};

//[Funcionalidad para la búsqueda por medio de la ayuda]
//Función para buscar las solicitudes mediante la ayuda
ObtenerPorPagina = function () {
    BloquearDiv("#pantallaCompleta");
    var folioSolicitudInfo = {};
    folioSolicitudInfo.Autoriza = $('#txtAutoriza').val();
    datosMetodos = { 'folioSolicitudInfo': folioSolicitudInfo };
    urlMetodos = rutaPantalla + '/ObtenerPorPagina';
    mensajeErrorMetodos = window.MensajeConsultarSolicitudesPorPagina;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerPorPaginaSuccess, mensajeErrorMetodos);
};

//Función Success para buscar las solicitudes mediante la ayuda
ObtenerPorPaginaSuccess = function (msg) {
    var datos = {};
    CargarRecursosGridBusquedaFolios();
    datos.Recursos = recursos;
    datos.Solicitudes = msg.d;
    var divContenedorGrid = $('#divGridAyudaSolicitudProducto');
    divContenedorGrid.setTemplateURL(templateGridAyudaSolicitud);
    divContenedorGrid.processTemplate(datos);
    $("#GridAyudaSolicitudProducto tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#GridAyudaSolicitudProducto tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};

//[Funcionalidad para la búsqueda por Id]
//Función para buscar las solicitudes por su Id
ObtenerPorFolioSolicitud = function () {
    BloquearDiv("#pantallaCompleta");
    BloquearPantalla();
    //var folioSolicitudInfo = {};
    folioSolicitud = $txtFolio.val();
    datosMetodos = { 'folioSolicitud': folioSolicitud };
    urlMetodos = rutaPantalla + '/ObtenerPorFolioSolicitud';
    mensajeErrorMetodos = window.MensajeConsultarSolicitudesPorFolio;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerPorFolioSolicitudSuccess, mensajeErrorMetodos);
};

//Función Success para obtener el detalle de la solicitud
ObtenerPorFolioSolicitudSuccess = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        MostrarMensaje(window.MensajeConsultarSolicitudesPorFolio, function () {
            DesbloquearPantalla();
        });
        return;
    }
    setTimeout(function() {
        var fechaSolicitud = new Date(parseInt(msg.d[0].FechaSolicitud.replace(/^\D+/g, '')));
        txtSolicitudProductoId = msg.d[0].SolicitudProductoId;
        $txtFechaSolicitud.val(FechaFormateada(fechaSolicitud));
        $txtObservaciones.val(msg.d[0].ObservacionUsuarioAutoriza);
    }, 500);
    setTimeout(function() {
        LlenaGridAutorizaciones(msg.d);
    }, 500);
    DesbloquearPantalla();
    $txtFolio.attr("disabled", true);
    $btnGuardar.attr("disabled", msg.d[0].IsAutorizado);
};

//Función para limpiar totalmente la pantalla
LimpiarPantallaCompleta = function () {
    Inicializar();
    LlenaGridAutorizaciones(null);
    $txtFolio.val('');
    $txtFolio.attr("disabled", false);
    $txtFolio.focus();
    $('#txtObservacion').val('');
    editandoRegistro = false;
    usuarioAutorizo = 0;
};

//Función para mandar el mensaje al querer cerrar el modal de la ayuda de Producto
MensajeCerrarModalFolio = function() {
    bootbox.dialog({
        message: window.MensajeSalirSinSeleccionarFolio,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function() {
                    $('#modalBusquedaFolio').modal('hide');
                    return true;
                }
            },
            Cancelar: {
                label: window.No,
                callback: function() {
                    return true;
                }
            }
        }
    });
};

//Función para mostrar los mensajes
MostrarMensaje = function (mensaje, funcionCallback) {
    bootbox.dialog({
        message: mensaje,
        buttons: {
            Aceptar: {
                label: window.OK,
                callback: funcionCallback
            }
        }
    });
};
