/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

var rutaPantalla = location.pathname;
var Mensajes = {
    EmbarqueNoRecibido: { value: 1, mensaje: window.EmbarqueNoRecibido },
    EntradaCosteada: { value: 2, mensaje: window.EntradaCosteada },
    EntradaConCalidad: { value: 3, mensaje: window.EntradaConCalidad },
    EntradaSinCondicion: { value: 4, mensaje: window.EntradaSinCondicion }
};

//Evento que se Ejecuta cuando la Pantalla esta lista para su uso
$(document).ready(function () {


    $('.soloNumeros').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('.soloNumeros').numericInput();

    $('#txtMuertas').keyup(function () {
        if ($('#hfEntradaGanadoID').val() == '') {
            $(this).val('');
            return;
        }
    });

    $('#btnGuardar').attr('disabled', true);

    $('#btnCancelar').click(function () {
        if ($('#hfEntradaGanadoID').val() == '') {
            LimpiarPantalla(true);
            return;
        }
        bootbox.dialog({
            message: window.Cancelar,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        LimpiarPantalla(true);
                    }
                },
                Cancelar: {
                    label: window.No,
                    callback: function () {

                    }
                }
            }
        });
    });

    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#hfEntradaGanadoID').val('');
    $('#txtEntrada').val('');
    $('#txtMuertas').val('');
    $('#txtEntrada').focus();

    CargarCalidades();

    $('#GridCalidad').on("keydown", ".soloNumeros", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#GridCalidad').on("focusout", ".soloNumeros", function (e) {
        if ($('#hfEntradaGanadoID').val() == '') {
            $(this).val('');
            return;
        }
    });
    
    $('#txtEntrada').focusin(function () {
        $('#btnCancelar').attr('disabled', true);
    });

    $('#txtEntrada').focusout(function () {
        $('#btnCancelar').attr('disabled', false);
        var entrada = TryParseInt($('#txtEntrada').val(), 0);
        if (entrada == 0) {
            return false;
        }
        CargarInformacionEntrada();
    });

    $('#txtEntrada').keypress(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#GridCalidad').on("keyup", ".EnLinea", function () {
        if ($('#hfEntradaGanadoID').val() == '') {
            $(this).val('');
            return;
        }
        CalcularTotalCabezas('.EnLinea', '#lblLinea');
        var total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
        var totalValido = ValidarTotalCabezas(total);
        if (totalValido) {
            $('#lblTotales').text(total);
        } else {
            $(this).val('0');
            CalcularTotalCabezas('.EnLinea', '#lblLinea');
            total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
            $('#lblTotales').text(total);
        }
    });

    $('#GridCalidad').on("keyup", ".Produccion", function () {
        if ($('#hfEntradaGanadoID').val() == '') {
            $(this).val('');
            return;
        }
        CalcularTotalCabezas('.Produccion', '#lblProduccion');
        var total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
        var totalValido = ValidarTotalCabezas(total);
        if (totalValido) {
            $('#lblTotales').text(total);
        } else {
            $(this).val('0');
            CalcularTotalCabezas('.Produccion', '#lblProduccion');
            total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
            $('#lblTotales').text(total);
        }
    });

    $('#GridCalidad').on("keyup", ".Venta", function () {
        if ($('#hfEntradaGanadoID').val() == '') {
            $(this).val('');
            return;
        }
        CalcularTotalCabezas('.Venta', '#lblVenta');
        var total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
        var totalValido = ValidarTotalCabezas(total);
        if (totalValido) {
            $('#lblTotales').text(total);
        } else {
            $(this).val('0');
            CalcularTotalCabezas('.Venta', '#lblVenta');
            total = TryParseInt($('#lblLinea').text(), 0) + TryParseInt($('#lblProduccion').text(), 0) + TryParseInt($('#lblVenta').text(), 0);
            $('#lblTotales').text(total);
        }
    });

});

//maxLengthCheck = function (object) {
//    if (object.value.length > object.maxLength) {
//        object.value = object.value.slice(0, object.maxLength);
//    }
//};

//Funcion para Calcular el total de Cabezas capturadas por Calificacion de Ganado 'En Linea', 'Produccion', 'Venta'
CalcularTotalCabezas = function (clase, label) {
    var total = 0;
    $(clase).each(function () {
        total = total + TryParseInt($(this).val(), 0);
    });
    $(label).text(total);
};

//Funcion para validar que el total de cabezas no supere las cabezas del corral.
ValidarTotalCabezas = function (total) {
    var cabezasCorral = TryParseInt($('#txtCabezas').val(), 0);

    if (total > cabezasCorral) {
        bootbox.dialog({
            message: window.CabezasMayor,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    callback: function () {

                    }
                }
            }
        });
        return false;
    }
    return true;
};

////Función para intentar Parsear un valor a entero si no es valido regresa un default Value
//TryParseInt = function (str, defaultValue) {
//    var retValue = defaultValue;
//    if (str != null) {
//        if (str.length > 0) {
//            if (!isNaN(str)) {
//                retValue = parseInt(str);
//            }
//        }
//    }
//    return retValue;
//};

//Función para cargar la información de las Calidades de Ganado para su captura
CargarCalidades = function () {
    var datos = {};
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerCalidades',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.SinDatos,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {

                            }
                        }
                    }
                });
                return false;
            }

            var contenedor = {};
            var recursos = {};
            recursos.Calidad = window.ColumnaCalidad;
            recursos.Hembras = window.ColumnaHembras;
            recursos.Machos = window.ColumnaMachos;

            contenedor.Calidades = msg.d;
            contenedor.Recursos = recursos;

            $('#GridCalidad').html('');
            $('#GridCalidad').setTemplateURL('../Templates/GridCalidadClasificacionGanado.htm');
            $('#GridCalidad').processTemplate(contenedor);
            $('.soloNumeros').bind("cut copy paste", function (e) {
                e.preventDefault();
            });

            $('.soloNumeros').numericInput();

            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorConsultarCalidad,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
};

//Función para cargar la información de la Entrada de Ganado.
CargarInformacionEntrada = function () {
    var filtroCalificacionGanado = {};
    filtroCalificacionGanado.FolioEntrada = parseInt($('#txtEntrada').val());
    var datos = { 'filtroCalificacionGanado': filtroCalificacionGanado };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerInformacionEntrada',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                window.DesbloquearPantalla();
                bootbox.dialog({
                    message: window.SinEntrada,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                $('#txtEntrada').val('');
                                $('#txtEntrada').focus();
                            }
                        }
                    }
                });
                return false;
            }

            var entradaGanado = msg.d;
            //if (entradaGanado.MensajeRetornoCalificacion == 0) {
                $('#hfEntradaGanadoID').val(entradaGanado.EntradaGanadoID);
                $('#txtProveedor').val(entradaGanado.OrganizacionOrigen);
                $('#txtCorral').val(entradaGanado.CodigoCorral);
                $('#txtCabezas').val(entradaGanado.CabezasRecibidas);
                $('#txtCabezasMuertas').val(entradaGanado.CabezasMuertasCondicion);
                $('#txtMuertas').val(entradaGanado.CabezasMuertas);
                $('#txtFecha').val(FechaFormateada(new Date()));
                $('#btnGuardar').attr('disabled', false);
                $('#txtEntrada').attr('disabled', true);
                CargarDatosCalidad();
                DesbloquearPantalla();
                return true;
            //}
            //if (entradaGanado.MensajeRetornoCalificacion != 0) {
            //    switch (entradaGanado.MensajeRetornoCalificacion) {
            //        case Mensajes.EmbarqueNoRecibido.value:
            //            bootbox.dialog({
            //                message: window.EmbarqueNoRecibido,
            //                buttons: {
            //                    Aceptar: {
            //                        label: window.Aceptar,
            //                        callback: function () {
            //                            $('#txtEntrada').val('');
            //                            $('#txtEntrada').focus();
            //                        }
            //                    }
            //                }
            //            });
            //            break;
            //        case Mensajes.EntradaCosteada.value:
            //            bootbox.dialog({
            //                message: window.EntradaCosteada,
            //                buttons: {
            //                    Aceptar: {
            //                        label: window.Aceptar,
            //                        callback: function () {
            //                            $('#txtEntrada').val('');
            //                            $('#txtEntrada').focus();
            //                        }
            //                    }
            //                }
            //            });
            //            break;
            //        case Mensajes.EntradaConCalidad.value:
            //            bootbox.dialog({
            //                message: window.EntradaConCalidad,
            //                buttons: {
            //                    Aceptar: {
            //                        label: window.Aceptar,
            //                        callback: function () {
            //                            $('#txtEntrada').val('');
            //                            $('#txtEntrada').focus();
            //                        }
            //                    }
            //                }
            //            });
            //            break;
            //        case Mensajes.EntradaSinCondicion.value:
            //            bootbox.dialog({
            //                message: window.EntradaSinCondicion,
            //                buttons: {
            //                    Aceptar: {
            //                        label: window.Aceptar,
            //                        callback: function () {
            //                            $('#txtEntrada').val('');
            //                            $('#txtEntrada').focus();
            //                        }
            //                    }
            //                }
            //            });
            //            break;
            //        default:
            //    }
            //}
            window.DesbloquearPantalla();
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorConsultarEntrada,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
};

CargarDatosCalidad = function () {
    var filtroCalificacionGanado = {};
    filtroCalificacionGanado.EntradaGanadoID = TryParseInt($('#hfEntradaGanadoID').val(), 0);
    var datos = { 'filtroCalificacionGanado': filtroCalificacionGanado };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerCalidadesEntradaGanado',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            if (msg.d == null || msg.d.length == 0) {
                window.DesbloquearPantalla();
                bootbox.dialog({
                    message: window.SinEntrada,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function() {
                                $('#txtEntrada').val('');
                                $('#txtEntrada').focus();
                            }
                        }
                    }
                });
                return false;
            }
            $(msg.d).each(function() {
                $('#' + this.CalidadGanado.CalidadGanadoID).val(this.Valor);
            });
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorConsultarEntrada,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
   
};

//Función para Guardar las Calidades de Ganado capturadas.
Guardar = function () {
    var valido = ValidarGuardar();
    if (!valido) {
        return false;
    }
    var filtroCalificacionGanadoInfo = CargarInformacionGuardar();
    var datos = { 'filtroCalificacionGanadoInfo': filtroCalificacionGanadoInfo };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/Guardar',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.GuardoExito,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {
                            LimpiarPantalla(true);
                        }
                    }
                }
            });
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorGuardar,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
    return true;
};

LimpiarPantalla = function (todo) {
    if (todo) {
        $('#txtEntrada').val('');
        $('#txtEntrada').attr('disabled', false);
        $('#txtEntrada').focus();
    }
    $('#txtProveedor').val('');
    $('#txtCorral').val('');
    $('#txtCabezas').val('');
    $('#txtCabezasMuertas').val('');
    $('#txtFecha').val('');
    $('#lblLinea').text('0');
    $('#lblProduccion').text('0');
    $('#lblVenta').text('0');
    $('#txtMuertas').val('');
    $('#lblTotales').text('0');
    $('#btnGuardar').attr('disabled', true);
    $('#hfEntradaGanadoID').val('');
    CargarCalidades();
};

//Función para Validar las Cabezas capturadas antes de guardar
ValidarGuardar = function () {
    var cabezasCorral = TryParseInt($('#txtCabezas').val(), 0);

    var total = TryParseInt($('#lblTotales').text(), 0);
    var muertas = TryParseInt($('#txtMuertas').val(), 0);

    var cabezasCapturadas = (total + muertas);

    if (cabezasCorral > cabezasCapturadas) {
        bootbox.dialog({
            message: window.FaltaDatos + (cabezasCorral - cabezasCapturadas),
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    callback: function () {

                    }
                }
            }
        });
        return false;
    }
    if (cabezasCorral < cabezasCapturadas) {
        bootbox.dialog({
            message: window.CabezasMayor,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    callback: function () {

                    }
                }
            }
        });
        return false;
    }
    return true;
};

//Función para cargar la información de las calidades que se van a Guardar.
CargarInformacionGuardar = function () {
    var filtroCalificacionGanadoInfo = {};
    var listaCalidades = new Array();
    filtroCalificacionGanadoInfo.EntradaGanadoID = TryParseInt($('#hfEntradaGanadoID').val(), 0);
    filtroCalificacionGanadoInfo.CabezasMuertas = TryParseInt($('#txtMuertas').val(), 0);
    var indice = 0;
    $('#GridCalidad table tbody tr').each(function () {
        var calidad = {};
        calidad.Calidad = $('.columnaCalidad', this).text();
        calidad.CabezasMacho = TryParseInt($('.Machos', this).val(), 0);
        calidad.CabezasHembra = TryParseInt($('.Hembras', this).val(), 0);
        listaCalidades[indice] = calidad;
        indice = indice + 1;
    });
    filtroCalificacionGanadoInfo.ListaCalidades = listaCalidades;
    return filtroCalificacionGanadoInfo;
};

////Función para bloquear la pantalla mientras se ejecuta una operación
//BloquearPantalla = function () {
//    var lock = document.getElementById('skm_LockPane');
//    if (lock) {
//        lock.className = 'LockOn';
//        $('#skm_LockPane').spin(
//            {
//                top: '30',
//                color: '#6E6E6E'
//            });
//    }
//};

////Función para desbloquear la pantalla mientras se ejecuta una operación
//DesbloquearPantalla = function () {
//    $("#skm_LockPane").spin(false);
//    var lock = document.getElementById('skm_LockPane');
//    lock.className = 'LockOff';
//};

////Función para regresar una fecha en formato dd/mm/yyyy
//FechaFormateada = function (date) {
//    var d = new Date(date),
//       month = '' + (d.getMonth() + 1),
//       day = '' + d.getDate(),
//       year = d.getFullYear();

//    if (month.length < 2) month = '0' + month;
//    if (day.length < 2) day = '0' + day;

//    if (year > 2050) {
//        day = '01';
//        month = '01';
//        year = '0001';
//    }
//    return [day, month, year].join('/');
//};