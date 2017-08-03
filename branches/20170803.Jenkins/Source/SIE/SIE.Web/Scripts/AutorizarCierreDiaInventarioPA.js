/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="jscomun.js" />

var rutaPantalla = location.pathname;
var recursos = {};
var urlMetodos;
var datosMetodos;
var mensajeErrorMetodos;
var esCancelacion = false;
$(document).ready(function () {
    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    AsignarEventosControles();
    CargarGridDefault();
    CargarAlmacenes();
});

AsignarEventosControles = function () {
    $('#btnConsultar').click(function () {
        LimpiarPantalla();
        if ($('#ddlAlmacen option:selected').val() == '0') {
            MostrarMensaje(window.SeleccionarAlmacen, function () {
                $('#ddlAlmacen').focus();
            });
            return;
        }
        BuscarAlmacenMovimientosPendientes();
    });

    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#ddlAlmacen').change(function () {
        LimpiarPantalla();
    });

    $('#btnCancelar').click(function () {
        bootbox.dialog({
            message: window.MensajeCancelarPantalla,
            buttons: {

                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        LimpiarPantalla();
                        $('#ddlAlmacen').val('');
                        $('#ddlAlmacen').focus();
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
};

ValidarGuardar = function () {
    var esValido = true;

    return esValido;
};

Guardar = function () {
    var renglonesAGrid = $('#GridCierreDiaInventario tbody tr');
    if (renglonesAGrid.length == 0) {
        MostrarMensaje(window.SinInformacion, null);
        return;
    }
    var renglonesAutorizados = $('#GridCierreDiaInventario tbody tr .checkAutorizar:checked');
    if (renglonesAutorizados.length == 0) {
        bootbox.dialog({
            message: window.SinAutorizados,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        esCancelacion = true;
                        GuardarAutorizacion();
                    }
                },
                Cancelar: {
                    label: window.No,
                    callback: function () {
                        return;
                    }
                }
            }
        });
    } else {
        GuardarAutorizacion();
    }
};

GuardarAutorizacion = function () {
    BloquearPantalla();
    var cierreDiaInventarioPa = {};
    cierreDiaInventarioPa = CargarInformacionCabecero();
    cierreDiaInventarioPa.listaCierreDiaInventarioPaDetalle = CargarInformacionDetalle();
    datosMetodos = { 'cierreDiaInventarioPa': cierreDiaInventarioPa };
    urlMetodos = rutaPantalla + '/GuardarAutorizarCierreDia';
    mensajeErrorMetodos = window.ErrorGuardar;
    EjecutarWebMethod(urlMetodos, datosMetodos, GuardarSuccess, mensajeErrorMetodos);
}

GuardarSuccess = function () {
    DesbloquearPantalla();
    MostrarMensaje("<img src='../Images/Correct.png'/>"+window.GuardadoConExito, function () {
        LimpiarPantalla();
        $('#ddlAlmacen').val('0');
        $('#ddlAlmacen').focus();
    });
};

CargarInformacionCabecero = function () {
    var cierreDiaInventarioPa = {};
    cierreDiaInventarioPa.FolioMovimiento = TryParseInt($('#txtFolio').val(), 0);
    cierreDiaInventarioPa.AlmacenID = TryParseInt($('#ddlAlmacen option:selected').val(), 0);
    cierreDiaInventarioPa.Observaciones = $('#txtObservaciones').val();
    cierreDiaInventarioPa.EsCancelacion = esCancelacion;
    return cierreDiaInventarioPa;
};

CargarInformacionDetalle = function () {
    var listaCierreDiaInventarioPaDetalle = new Array();
    var renglonesAutorizados = $('#GridCierreDiaInventario tbody tr .checkAutorizar:checked');
    renglonesAutorizados.each(function () {
        var renglon = $(this).closest('tr');
        var cierreDiaInventarioPaDetalle = {};
        cierreDiaInventarioPaDetalle.ProductoID = TryParseInt(renglon.attr('data-ProductoID'), 0);
        cierreDiaInventarioPaDetalle.CostoUnitario = TryParseDecimal(renglon.attr('data-CostoUnitario'), 0);
        cierreDiaInventarioPaDetalle.AlmacenInventarioLoteID = TryParseInt(renglon.attr('data-AlmacenInventarioLoteID'), 0);

        cierreDiaInventarioPaDetalle.InventarioTeorico = TryParseInt(renglon.find('.columnaInventarioTeorico').text(), 0);
        cierreDiaInventarioPaDetalle.InventarioFisico = TryParseInt(renglon.find('.columnaInventarioFisico').text(), 0);
        var lote = TryParseInt(renglon.find('.columnaLote').text(), 0);
        if (lote > 0) {
            cierreDiaInventarioPaDetalle.ManejaLote = true;
        } else {
            cierreDiaInventarioPaDetalle.ManejaLote = false;
        }
        listaCierreDiaInventarioPaDetalle.push(cierreDiaInventarioPaDetalle);
    });
    return listaCierreDiaInventarioPaDetalle;
};

LimpiarPantalla = function () {
    $('#txtFolio').val('');
    $('#txtEstatus').val('');
    $('#txtFecha').val('');
    $('#txtObservaciones').val('');
    esCancelacion = false;
    CargarGridDefault();
};

BuscarAlmacenMovimientosPendientes = function () {
    BloquearPantalla();
    urlMetodos = rutaPantalla + '/ObtenerMovimientosPendientesAutorizar';
    var almacenId = TryParseInt($('#ddlAlmacen option:selected').val(), 0);
    var filtrosAutorizarCierreDia = {};
    filtrosAutorizarCierreDia.AlmacenID = almacenId;
    datosMetodos = { 'filtrosAutorizarCierreDia': filtrosAutorizarCierreDia };
    mensajeErrorMetodos = window.ErrorAlmacen;
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarAlmacenMovimientosPendientesSuccess, mensajeErrorMetodos);
};

BuscarAlmacenMovimientosPendientesSuccess = function (msg) {
    DesbloquearPantalla();
    if (msg.d == null || msg.d.length == 0) {
        MostrarMensaje(window.SinMovimientos, function () {
            $('#ddlAlmacen').val('');
            $('#ddlAlmacen').focus();
        });
    }
    var datos = {};
    var elemento = msg.d[0];

    $('#txtFolio').val(elemento.FolioMovimiento);
    var fechaMovimiento = new Date(parseInt(elemento.FechaMovimiento.replace(/^\D+/g, '')));
    $('#txtFecha').val(FechaFormateada(fechaMovimiento));
    $('#txtObservaciones').val(elemento.Observaciones);
    $('#txtEstatus').val(window.PorAutorizar);

    CargarRecursosGridGeneral();
    datos.Recursos = recursos;
    datos.MovimientosAutorizar = msg.d;
    var divContenedor = $('#divGridCierreDiaInventario');

    divContenedor.setTemplateURL('../Templates/GridCierreDiaInventario.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar el combo ddlAlmacen
CargarAlmacenes = function () {
    BloquearPantalla();
    urlMetodos = rutaPantalla + '/ObtenerAlmacenes';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorAlmacen;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarAlmacenesSucess, mensajeErrorMetodos);
};

//Función Success para cargar el combo ddlTurno
CargarAlmacenesSucess = function (msg) {
    DesbloquearPantalla();
    var valores = {};
    var recursos = {};
    recursos.Seleccione = window.Seleccione;
    valores.Recursos = recursos;
    var listaValores = new Array();
    $(msg.d).each(function () {
        var valor = {};
        valor.Clave = this.AlmacenID;
        valor.Descripcion = this.Almacen;
        listaValores.push(valor);
    });
    valores.Valores = listaValores;
    var comboAlmacen = $('#ddlAlmacen');
    comboAlmacen.setTemplateURL('../Templates/ComboGenerico.htm');
    comboAlmacen.processTemplate(valores);
};

//Función para cargar el grid principal
CargarGridDefault = function () {
    var datos = {};
    CargarRecursosGridGeneral();
    datos.Recursos = recursos;
    datos.MovimientosAutorizar = {};
    var divContenedor = $('#divGridCierreDiaInventario');

    divContenedor.setTemplateURL('../Templates/GridCierreDiaInventario.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar los recursos  del grid principal
CargarRecursosGridGeneral = function () {
    recursos = {};
    recursos.Producto = window.ColumnaProducto;
    recursos.Lote = window.ColumnaLote;
    recursos.TamanioLote = window.ColumnaTamanioLote;
    recursos.InventarioTeorico = window.ColumnaInventarioTeorico;
    recursos.InventarioFisico = window.ColumnaInventarioFisico;
    recursos.MermaSuperavit = window.ColumnaMermaSuperavit;
    recursos.PorcentajeLote = window.ColumnaPorcentajeLote;
    recursos.PorcentajePermitido = window.ColumnaPorcentajePermitido;
    recursos.Autorizar = window.ColumnaAutorizar;
};