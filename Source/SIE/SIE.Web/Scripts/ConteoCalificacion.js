/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="jscomun.js" />

var rutaPantalla = location.pathname;
var urlMetodos;
var datosMetodos;
var mensajeErrorMetodos;

var Mensajes = {
    EmbarqueNoRecibido: { value: 1, mensaje: window.EmbarqueNoRecibido },
    EntradaCosteada: { value: 2, mensaje: window.EntradaCosteada },
    EntradaConCalidad: { value: 3, mensaje: window.EntradaConCalidad },
    EntradaSinCondicion: { value: 4, mensaje: window.EntradaSinCondicion }
};

//Evento que se ejecuta cuando se carga la pantalla
$(document).ready(function () {
    $('#btnGuardar').attr('disabled', true);

    ObtenerCalidadesConteo();
    AsignarPluginsControles();
    AsignarEventosControles();
});

AsignarPluginsControles = function () {
    $('.soloNumeros').numericInput();
};

AsignarEventosControles = function () {
    $('#ConteoCalidad').on('click', '.cabezasMachosMenos', function () {
        var calidad = $(this);
        var textoBoxCalidad = $('#' + calidad.attr('data-textbox'));
        var cantidad = TryParseInt(textoBoxCalidad.val(), 0);
        if (cantidad > 0) {
            cantidad = cantidad - 1;
            textoBoxCalidad.val(cantidad);

            var textBoxMachos = $('#txtMachos');
            var cantidadMachos = TryParseInt(textBoxMachos.val(), 0);
            if (cantidadMachos > 0) {
                cantidadMachos = cantidadMachos - 1;
                textBoxMachos.val(cantidadMachos);
            }
            var cabezas = TryParseInt($('#txtCabezas').val(), 0);
            $('#txtCabezas').val(cabezas - 1);
        }

    });

    $('#ConteoCalidad').on('click', '.cabezasMachosMas', function () {
        var calidad = $(this);
        var textoBoxCalidad = $('#' + calidad.attr('data-textbox'));
        var cantidad = TryParseInt(textoBoxCalidad.val(), 0);
        cantidad = cantidad + 1;
        textoBoxCalidad.val(cantidad);

        var textBoxMachos = $('#txtMachos');
        var cantidadMachos = TryParseInt(textBoxMachos.val(), 0);
        cantidadMachos = cantidadMachos + 1;
        textBoxMachos.val(cantidadMachos);

        var cabezas = TryParseInt($('#txtCabezas').val(), 0);
        $('#txtCabezas').val(cabezas + 1);

    });

    $('#ConteoCalidad').on('click', '.cabezasHembrasMenos', function () {
        var calidad = $(this);
        var textoBoxCalidad = $('#' + calidad.attr('data-textbox'));
        var cantidad = TryParseInt(textoBoxCalidad.val(), 0);
        if (cantidad > 0) {
            cantidad = cantidad - 1;
            textoBoxCalidad.val(cantidad);

            var textBoxHembras = $('#txtHembras');
            var cantidadHembras = TryParseInt(textBoxHembras.val(), 0);
            if (cantidadHembras > 0) {
                cantidadHembras = cantidadHembras - 1;
                textBoxHembras.val(cantidadHembras);
            }
            var cabezas = TryParseInt($('#txtCabezas').val(), 0);
            $('#txtCabezas').val(cabezas - 1);
        }

    });

    $('#ConteoCalidad').on('click', '.cabezasHembrasMas', function () {
        var calidad = $(this);
        var textoBoxCalidad = $('#' + calidad.attr('data-textbox'));
        var cantidad = TryParseInt(textoBoxCalidad.val(), 0);
        cantidad = cantidad + 1;
        textoBoxCalidad.val(cantidad);

        var textBoxHembras = $('#txtHembras');
        var cantidadHembras = TryParseInt(textBoxHembras.val(), 0);
        cantidadHembras = cantidadHembras + 1;
        textBoxHembras.val(cantidadHembras);

        var cabezas = TryParseInt($('#txtCabezas').val(), 0);
        $('#txtCabezas').val(cabezas + 1);

    });

    $('#txtEntrada').keypress(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#txtEntrada').focusout(function () {
        $('#btnCancelar').attr('disabled', false);
        var entrada = TryParseInt($('#txtEntrada').val(), 0);
        if (entrada == 0) {
            return false;
        }
        CargarInformacionEntrada();
        return true;
    });

    $('#btnCancelar').click(function () {
        bootbox.dialog({
            message: window.Cancelar,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        LimpiarPantalla();
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
};

CargarInformacionEntrada = function () {
    BloquearPantalla();
    var filtroCalificacionGanado = {};
    filtroCalificacionGanado.FolioEntrada = parseInt($('#txtEntrada').val());
    var datos = { 'filtroCalificacionGanado': filtroCalificacionGanado };

    urlMetodos = rutaPantalla + '/TraerInformacionEntrada';
    datosMetodos = datos;
    mensajeErrorMetodos = window.ErrorEntrada;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarInformacionEntradaSucess, mensajeErrorMetodos);
};

CargarInformacionEntradaSucess = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        DesbloquearPantalla();
        MostrarMensaje(window.EntradaNoValida, function () {
            $('#txtEntrada').val('');
            $('#txtEntrada').focus();
        });
        return false;
    }
    var entradaGanado = msg.d;
    if (entradaGanado.MensajeRetornoCalificacion == 0) {
        $('#hfEntradaGanadoID').val(entradaGanado.EntradaGanadoID);
        $('#txtProveedor').val(entradaGanado.OrganizacionOrigen);
        $('#txtCorral').val(entradaGanado.CodigoCorral);
        $('#txtCabezas').val(entradaGanado.CabezasRecibidas);
        $('#txtMuertas').val(entradaGanado.CabezasMuertas);
        $('#txtFecha').val(FechaFormateada(new Date()));
        $('#btnGuardar').attr('disabled', false);
        $('#txtEntrada').attr('disabled', true);
        LimpiarControlesConteo();
        DesbloquearPantalla();
        return true;
    }
    if (entradaGanado.MensajeRetornoCalificacion != 0) {
        switch (entradaGanado.MensajeRetornoCalificacion) {
            case Mensajes.EmbarqueNoRecibido.value:
                MostrarMensaje(window.EmbarqueNoRecibido, function () {
                    $('#txtEntrada').val('');
                    $('#txtEntrada').focus();
                });
                break;
            case Mensajes.EntradaCosteada.value:
                MostrarMensaje(window.EntradaCosteada, function () {
                    $('#txtEntrada').val('');
                    $('#txtEntrada').focus();
                });
                break;
            case Mensajes.EntradaConCalidad.value:
                MostrarMensaje(window.EntradaConCalidad, function () {
                    $('#txtEntrada').val('');
                    $('#txtEntrada').focus();
                });
                break;
            case Mensajes.EntradaSinCondicion.value:
                MostrarMensaje(window.EntradaSinCondicion, function () {
                    $('#txtEntrada').val('');
                    $('#txtEntrada').focus();
                });
                break;
            default:
        }
    }
    DesbloquearPantalla();
    return true;
};

//Evento para consultar las calidades de ganado para armar los datos del conteo
ObtenerCalidadesConteo = function () {
    urlMetodos = rutaPantalla + '/ObtenerCalidadesConteo';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorConsultaCalidad;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerCalidadesConteoSuccess, mensajeErrorMetodos);
};

//Evento Success para consultar las calidades de ganado para armar los datos del conteo
ObtenerCalidadesConteoSuccess = function (msg) {
    if (msg.d != null) {
        GenerarControlesConteo(msg.d);
    }
};

GenerarControlesConteo = function (calidades) {
    var divContenedor = $('#ConteoCalidad');
    for (var i = 0; i < calidades.CalidadMachos.length; i++) {
        if (calidades.CalidadMachos[i] != undefined) {
            var html = '<div class="row-fluid espacioCortoArriba">';
            html += '<span class = "span3">';
            html += calidades.CalidadMachos[i].Descripcion + ': </span>';

            html += '<span class="span3">';

            html += '<img id="imgMenosSemana" class="cabezasMachosMenos imagenesGrid espacioCortoDerecha" data-textbox="' + calidades.CalidadMachos[i].CalidadGanadoID + '" src="../Images/round_minus48.png" />';
            html += '<input class="span4 textoDerecha espacioCortoDerecha capturaConteo textBoxGrande" disabled="disabled" id="' + calidades.CalidadMachos[i].CalidadGanadoID + '" type="text" />';
            html += '<img id="imgMasSemana" class="cabezasMachosMas imagenesGrid" data-textbox="' + calidades.CalidadMachos[i].CalidadGanadoID + '" src="../Images/round_plus48.png" />';
            html += '</span>';


            html += '<span class = "span3">';
            html += calidades.CalidadHembras[i].Descripcion + ': </span>';

            html += '<span class = "span3">';
            html += '<img id="imgMenosSemana" class="cabezasHembrasMenos imagenesGrid espacioCortoDerecha" data-textbox="' + calidades.CalidadHembras[i].CalidadGanadoID + '" src="../Images/round_minus48.png" />';
            html += '<input class="span4 textoDerecha espacioCortoDerecha capturaConteo textBoxGrande" disabled="disabled" id="' + calidades.CalidadHembras[i].CalidadGanadoID + '" type="text" />';
            html += '<img id="imgMasSemana" class="cabezasHembrasMas imagenesGrid" data-textbox="' + calidades.CalidadHembras[i].CalidadGanadoID + '" src="../Images/round_plus48.png" />';
            html += '</span>';
            html += '</div>';
            divContenedor.append(html);
        }
    }
};

LimpiarPantalla = function () {
    $('#btnGuardar').attr('disabled', true);
    $('input[type=text]').val('');
    $('#txtEntrada').val('');
    $('#txtEntrada').attr('disabled', false);
    $('#txtMachos').val('0');
    $('#txtHembras').val('0');
    $('#txtGanadoMuerto').val('');
    $('#txtEntrada').focus();
};

LimpiarControlesConteo = function () {
    $('.capturaConteo').val('');
    $('#txtMachos').val('0');
    $('#txtHembras').val('0');
    $('#txtGanadoMuerto').val('');
};


//Función para Validar las Cabezas capturadas antes de guardar
ValidarGuardar = function () {
    var cabezasCorral = TryParseInt($('#txtCabezas').val(), 0);

    var totalMachos = TryParseInt($('#txtMachos').val(), 0);
    var totalHembras = TryParseInt($('#txtHembras').val(), 0);
    //var totalMuertos = TryParseInt($('#txtGanadoMuerto').val(), 0);

    var cabezasCapturadas = totalMachos + totalHembras; //+ totalMuertos;

    if (cabezasCorral > cabezasCapturadas) {
        MostrarMensaje(window.FaltaDatos + (cabezasCorral - cabezasCapturadas), function () {

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
    filtroCalificacionGanadoInfo.CabezasMuertas = TryParseInt($('#txtGanadoMuerto').val(), 0);
    var indice = 0;
    $('.capturaConteo').each(function () {
        var valorCabezas = TryParseInt($(this).val(), 0);
        if (valorCabezas > 0) {
            var calidad = {};
            calidad.CalidadID = TryParseInt($(this).attr('id'), 0);
            calidad.ValorCabezas = valorCabezas;

            listaCalidades[indice] = calidad;
            indice = indice + 1;
        }
    });
    filtroCalificacionGanadoInfo.ListaCalidades = listaCalidades;
    return filtroCalificacionGanadoInfo;
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
    urlMetodos = rutaPantalla + '/Guardar';
    datosMetodos = datos;
    mensajeErrorMetodos = window.ErrorGuardar;
    EjecutarWebMethod(urlMetodos, datosMetodos, GuardarSuccess, mensajeErrorMetodos);
    return true;
};

GuardarSuccess = function () {
    DesbloquearPantalla();
    MostrarMensaje('<img src="../Images/Correct.png"/>' + window.GuardoExito, function () {
        LimpiarPantalla();
    });
};
