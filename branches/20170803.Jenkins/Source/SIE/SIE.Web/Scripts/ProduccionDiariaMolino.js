/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="jscomun.js" />

var rutaPantalla = location.pathname;
var recursos = {};
var urlMetodos;
var datosMetodos;
var mensajeErrorMetodos;
var editandoRegistro = false;
var pesajeMateriaPrimaID;
var ticketHorasMuertas;
var listaHorasMuertas = new Array();
var listaRenglonesEliminar = new Array();
var usuarioAutorizo;
$(document).ready(function () {

    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    BloquearPantalla();
    AsignarPluginsControles();
    AsignarEventosControles();
    CargarGridDefault();
    CargarTurnos();
    CargarEspecificacionForraje();
});

//Función  que se utiliza para guardar la información
Guardar = function () {
    BloquearPantalla();
    var produccionDiariaMolino = {};
    var grid = $('#GridProduccionDiariaMolino tbody tr');
    if (grid.length == 0) {
        MostrarMensaje(window.MensajeDatosGuardar, null);
        DesbloquearPantalla();
        return;
    }
    var valido = true;
    grid.each(function () {
        if ($(this).find('.columnaHoras').text().trim() == '') {
            valido = false;
            return;
        }
    });
    if (!valido) {
        MostrarMensaje(window.MensajesRegistrosIncompletos, null);
        DesbloquearPantalla();
        return;
    }
    ObtenerGuardarProduccionDiaria(produccionDiariaMolino);
    ObtenerGuardarProduccionDiariaDetalle(produccionDiariaMolino);
    ObtenerGuardarTiempoMuerto(produccionDiariaMolino);

    datosMetodos = { 'produccionDiaria': produccionDiariaMolino };
    urlMetodos = rutaPantalla + '/Guardar';
    mensajeErrorMetodos = window.ErrorGuardar;
    EjecutarWebMethod(urlMetodos, datosMetodos, function () {
        MostrarMensaje(window.MensajeGuardoExito, null);
        LimpiarPantallaCompleta();
        DesbloquearPantalla();
    }, mensajeErrorMetodos);
};

//Función para obtener el cabecero de la tabla (ProduccionDiaria)
ObtenerGuardarProduccionDiaria = function (produccionDiariaMolino) {
    var grid = $('#GridProduccionDiariaMolino tbody tr');
    produccionDiariaMolino.Turno = $('#ddlTurno option:selected').val();
    var primerRenglon = grid.first();
    var ultimoRenglon = grid.last();

    produccionDiariaMolino.LitrosInicial = primerRenglon.attr('data-litrosInicial');
    produccionDiariaMolino.LitrosFinal = ultimoRenglon.attr('data-litrosFinal');

    produccionDiariaMolino.HorometroInicial = primerRenglon.find('.columnaHorometroInicial').text().trim();
    produccionDiariaMolino.HorometroFinal = ultimoRenglon.find('.columnaHorometroFinal').text().trim();

    produccionDiariaMolino.FechaProduccion = ToDate($('#txtFecha').val());

    if (usuarioAutorizo == undefined || usuarioAutorizo == '') {
        produccionDiariaMolino.UsuarioIDAutorizo = 0;
    } else {
        produccionDiariaMolino.UsuarioIDAutorizo = usuarioAutorizo;
    }
    produccionDiariaMolino.Observaciones = $('#txtObservacion').val().trim();
};

//Función para obtener el detalle de la tabla (ProduccionDiariaDetalle)
ObtenerGuardarProduccionDiariaDetalle = function (produccionDiariaMolino) {
    var listaProduccionDiariaDetalle = new Array();
    $('#GridProduccionDiariaMolino tbody  tr').each(function () {
        var produccionDiariaDetalle = {};
        produccionDiariaDetalle.ProductoID = $(this).attr('data-producto');
        produccionDiariaDetalle.PesajeMateriaPrimaID = $(this).attr('data-pesajeMateriaPrimaID');
        produccionDiariaDetalle.EspecificacionForraje = $(this).attr('data-forraje');
        produccionDiariaDetalle.HoraInicial = $(this).attr('data-horaInicial');
        produccionDiariaDetalle.HoraFinal = $(this).attr('data-horaFinal');
        produccionDiariaDetalle.KilosNeto = TryParseInt($(this).find('.columnaKilosNeto').text().trim().replace(/,/g, '').replace(/_/g, ''));
        listaProduccionDiariaDetalle.push(produccionDiariaDetalle);
    });
    produccionDiariaMolino.ListaProduccionDiariaDetalle = listaProduccionDiariaDetalle;
};

//Función para onbtener la lista de horas muertas tabla (TiempoMuerto)
ObtenerGuardarTiempoMuerto = function (produccionDiariaMolino) {
    if (listaHorasMuertas.length > 0) {
        produccionDiariaMolino.ListaTiempoMuerto = listaHorasMuertas;
    }
};

//Función para validar los campos antes de agregarlos al grid principal
ValidarAntesAgregarGridPrincipal = function () {
    var valido = true;
    $('#divContenedor input[type=text]').not('#txtFecha, .noRequerido').each(function () {
        if ($(this).val() == '') {
            $(this).addClass('claseRequerida');
            valido = false;
        } else {
            $(this).removeClass('claseRequerida');
        }
    });
    $('#divContenedor input[type=number]').not('.noRequerido').each(function () {
        if ($(this).val() == '') {
            $(this).addClass('claseRequerida');
            valido = false;
        }
        else {
            $(this).removeClass('claseRequerida');
        }
    });
    $('#divContenedor input[type=time]').not('.noRequerido').each(function () {
        if ($(this).val() == '') {
            $(this).addClass('claseRequerida');
            valido = false;
        }
        else {
            $(this).removeClass('claseRequerida');
        }
    });
    $('#divContenedor select').each(function () {
        if ($(this).val() == '0') {
            $(this).addClass('claseRequerida');
            valido = false;
        }
        else {
            $(this).removeClass('claseRequerida');
        }
    });

    if (!valido) {
        MostrarMensaje(window.MensajeCamposRequeridos, null);
        return valido;
    }

    if ($('#txtLitrosFinal').val() != '') {
        var litrosInicial = $('#txtLitrosInicial').val();
        var litrosFinal = $('#txtLitrosFinal').val();
        if (litrosFinal < litrosInicial) {
            MostrarMensaje(window.MensajeLitrosIniciales, null);
            return false;
        }
    }

    var horaInicial = $('#txtHoraTicketInicial').val();

    var inicialHoras = TryParseInt($('#txtHoraTicketInicial').val().split(':')[0], 0);
    var inicialMinutos = TryParseInt($('#txtHoraTicketInicial').val().split(':')[1], 0);

    if ((inicialHoras > 24 || inicialMinutos > 60) || (inicialHoras == 24 && inicialMinutos > 0)) {
        MostrarMensaje(window.MensajeFormatoHorasInicial, function () {
            $('#txtHoraTicketInicial').focus();
        });
        return false;
    }

    if ($('#txtHoraTicketFinal').val() != '') {
        var horaFinal = $('#txtHoraTicketFinal').val();

        var finalHoras = TryParseInt($('#txtHoraTicketFinal').val().split(':')[0], 0);
        var finalMinutos = TryParseInt($('#txtHoraTicketFinal').val().split(':')[1], 0);

        if ((finalHoras > 24 || finalMinutos > 60) || (finalHoras == 24 && finalMinutos > 0)) {
            MostrarMensaje(window.MensajeFormatoHorasFinal, function () {
                $('#txtHoraTicketFinal').focus();

            });
            return false;
        }

        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial > fechaFinal) {
            MostrarMensaje(window.MensajeHoraFinalMenor, function () {
                $('#txtHoraTicketFinal').focus();
            });
            return false;
        }
    }
    return valido;
};

//Función para validar el login del supervisor
InicioSesionSupervisor = function () {
    if ($('#txtUsuario').val() == '' || $('#txtContrasenia').val() == '') {
        MostrarMensaje(window.MensajeSesionRequerido, function () {
            $('#txtUsuario').focus();
        });
        return;
    }
    BloquearModal();
    urlMetodos = rutaPantalla + '/VerificarUsuario';
    datosMetodos = { 'usuario': $('#txtUsuario').val(), 'contrasenia': $('#txtContrasenia').val() };
    mensajeErrorMetodos = window.ErrorValidarUsuario;
    EjecutarWebMethod(urlMetodos, datosMetodos, InicioSesionSupervisorSuccess, mensajeErrorMetodos);
};

//Función Success del login del supervisor
InicioSesionSupervisorSuccess = function (msg) {
    if (msg.d.Mensaje != 'OK') {
        MostrarMensaje(msg.d.Mensaje, null);
        DesbloquearModal();
        return;
    }

    $('#txtUsuario').val('');
    $('#txtContrasenia').val('');
    $('#modalSupervisor').modal('hide');
    $('#txtUsuarioSupervisor').val(msg.d.Usuario.Nombre);
    AgregarRenglonesGridSupervisor();
    $('#txtObservacionesSupervisor').val('');
    $('#modalSupervisorCompleto').modal('show');
    usuarioAutorizo = msg.d.Usuario.UsuarioID;
    DesbloquearModal();
};

//Función para agregar los renglones al Grid del Supervisor
AgregarRenglonesGridSupervisor = function () {
    var datos = {};
    CargarRecursosGridSupervisor();
    datos.Recursos = recursos;
    datos.Produccion = {};
    var divContenedor = $('#divGridProduccionSupervisor');

    divContenedor.setTemplateURL('../Templates/GridProduccionSupervisor.htm');
    divContenedor.processTemplate(datos);

    var gridSupervisor = $('#GridProduccionSupervisor tbody');

    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        var renglon = '<tr>';
        renglon += '<td class="textoDerecha columnaTicket"> ' + $(this).find('.columnaTicket').text() + ' </td>';
        renglon += '<td class="textoIzquierda"> ' + $(this).find('.columnaHoras').text() + ' </td>';
        renglon += '<td class="textoIzquierda"> ' + $(this).find('.columnaProducto').text() + ' </td>';
        renglon += '<td class="textoDerecha"> ' + $(this).find('.columnaHorometroInicial').text() + ' </td>';
        renglon += '<td class="textoDerecha"> ' + $(this).find('.columnaHorometroFinal').text() + ' </td>';
        renglon += '<td class="textoIzquierda"> ' + $(this).find('.columnaForraje').text() + ' </td>';
        renglon += '<td class="textoDerecha"> ' + $(this).find('.columnaLote').text() + ' </td>';
        renglon += '<td class="textoDerecha columnaKilosNeto"> ' + FormatearCantidad($(this).find('.columnaKilosNeto').text()) + ' </td>';
        renglon += '<td class="textoDerecha columnaConsumo"> ' + $(this).find('.columnaConsumo').text() + ' </td>';
        renglon += '<td class="textoDerecha"> ' + $(this).find('.columnaHumedad').text() + ' </td>';
        renglon += '<td class="textoIzquierda"> ' + $(this).find('.columnaHorasMuertas').text() + ' </td>';
        renglon += '<td class="textoDerecha columnaConteoPacas"> ' + $(this).find('.columnaConteoPacas').text() + ' </td>';
        renglon += '<td class="alineacionCentro"> <img src="../Images/close.png" class="imagen cancelarTicket" /> </td>';
        gridSupervisor.append(renglon);
    });

    if ($('#GridProduccionDiariaMolino tbody tr').length > 1) {
        CalcularCantidadPiePaginaSupervisor();
    }
};

//Función para cargar el combo ddlForraje
CargarEspecificacionForraje = function () {
    urlMetodos = rutaPantalla + '/ObtenerEspecificacionForraje';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorObtenerForraje;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarEspecificacionForrajeSuccess, mensajeErrorMetodos);
};

//Función Success para cargar el combo ddlForraje
CargarEspecificacionForrajeSuccess = function (msg) {
    var valores = {};
    var recursos = {};
    recursos.Seleccione = window.Seleccione;
    valores.Recursos = recursos;
    var listaValores = new Array();
    $(msg.d).each(function () {
        var valor = {};
        valor.Clave = this.EspecificacionForrajeID;
        valor.Descripcion = this.Descripcion;
        listaValores.push(valor);
    });
    valores.Valores = listaValores;
    var comboForraje = $('#ddlForraje');
    comboForraje.setTemplateURL('../Templates/ComboGenerico.htm');
    comboForraje.processTemplate(valores);
    DesbloquearPantalla();
};

//Función para limpiar los controles del modal Horas Muertas
LimpiarHorasMuertas = function () {
    $('#txtHoraInicial').val('');
    $('#txtHoraFinal').val('');
    $('#txtHoraFinal').attr('disabled', true);
    $('#ddlCausa').val('0');
    $('#txtHoraInicial').focus();

};

//Función para cargar el combo ddlCausa
CargarCausasTiempoMuerto = function () {
    urlMetodos = rutaPantalla + '/ObtenerCausasTiempoMuerto';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorCausasTiempoMuerto;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarCausasTiempoMuertoSuccess, mensajeErrorMetodos);
};

//Función Success para cargar el combo ddlCausa
CargarCausasTiempoMuertoSuccess = function (msg) {
    var valores = {};
    var recursos = {};
    recursos.Seleccione = window.Seleccione;
    valores.Recursos = recursos;
    var listaValores = new Array();
    $(msg.d).each(function () {
        var valor = {};
        valor.Clave = this.CausaTiempoMuertoID;
        valor.Descripcion = this.Descripcion;
        listaValores.push(valor);
    });
    valores.Valores = listaValores;
    var comboTurno = $('#ddlCausa');
    comboTurno.setTemplateURL('../Templates/ComboGenerico.htm');
    comboTurno.processTemplate(valores);
};

//Función para cargar el combo ddlTurno
CargarTurnos = function () {
    urlMetodos = rutaPantalla + '/ObtenerTurnos';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorTurnos;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarTurnosSucess, mensajeErrorMetodos);
};

//Función Success para cargar el combo ddlTurno
CargarTurnosSucess = function (msg) {
    var valores = {};
    var recursos = {};
    recursos.Seleccione = window.Seleccione;
    valores.Recursos = recursos;
    var listaValores = new Array();
    $(msg.d).each(function () {
        var valor = {};
        valor.Clave = this.TurnoID;
        valor.Descripcion = this.Descripcion;
        listaValores.push(valor);
    });
    valores.Valores = listaValores;
    var comboTurno = $('#ddlTurno');
    comboTurno.setTemplateURL('../Templates/ComboGenerico.htm');
    comboTurno.processTemplate(valores);
};

//Función para asignar los plugins a los controles
AsignarPluginsControles = function () {
    $('#txtFecha').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "es"
    });
    $('.txtAyudaTicket').alpha({
        allow: '0123456789-',
        disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`._¿¡¨¨~``^^°´´abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZáéíóúýÁÉÍÓÚÝñÑ'
    });
    $('.horas').inputmask('99:99');
    $('.soloNumeros').numericInput();
};

//Función para validar antes de agregar los registros a las Horas Muertas
ValidarAntesAgregarHorasMuertas = function () {

    if ($('#txtHoraInicial').val() == '') {
        MostrarMensaje(window.MensajeHoraInicialRequerida, function () {
            $('#txtHoraInicial').focus();
        });
        return false;
    }
    if ($('#txtHoraFinal').val() == '') {

        MostrarMensaje(window.MensajeHoraFinalRequerida, function () {
            $('#txtHoraFinal').focus();
        });
        return false;
    }

    var horaInicial = $('#txtHoraInicial').val();

    var inicialHoras = TryParseInt($('#txtHoraInicial').val().split(':')[0], 0);
    var inicialMinutos = TryParseInt($('#txtHoraInicial').val().split(':')[1], 0);


    if ($('#txtHoraInicial').val().indexOf("_") > -1) {
        MostrarMensaje(window.MensajeFormatoHorasInicial, function () {
            $('#txtHoraInicial').focus();
        });
        return false;
    }


    if ((inicialHoras > 24 || inicialMinutos > 60) || (inicialHoras == 24 && inicialMinutos > 0) ||
        ($('#txtHoraInicial').val().split(':')[1] == '__' || $('#txtHoraInicial').val().split(':')[1] == '') ||
        ($('#txtHoraInicial').val().split(':')[0] == '__' || $('#txtHoraInicial').val().split(':')[0] == '')) {
        $('#txtHoraInicial').focus();
        MostrarMensaje(window.MensajeFormatoHorasInicial, function () {
            $('#txtHoraInicial').focus();
        });
        return false;
    }

    if ($('#txtHoraFinal').val() != '') {
        var horaFinal = $('#txtHoraFinal').val();

        var finalHoras = TryParseInt($('#txtHoraFinal').val().split(':')[0], 0);
        var finalMinutos = TryParseInt($('#txtHoraFinal').val().split(':')[1], 0);
        
        if ($('#txtHoraFinal').val().indexOf("_") > -1) {
            MostrarMensaje(window.MensajeFormatoHorasFinal, function () {
                $('#txtHoraFinal').focus();
            });
            return false;
        }

        if ((finalHoras > 24 || finalMinutos > 60) || (finalHoras == 24 && finalMinutos > 0) ||
            ($('#txtHoraFinal').val().split(':')[1] == '__' || $('#txtHoraFinal').val().split(':')[1] == '') ||
            ($('#txtHoraFinal').val().split(':')[0] == '__' || $('#txtHoraFinal').val().split(':')[0] == '')) {
            $('#txtHoraFinal').focus();
            MostrarMensaje(window.MensajeFormatoHorasFinal, function () {
                $('#txtHoraFinal').focus();
            });
            return false;
        }

        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial > fechaFinal) {
            MostrarMensaje(window.MensajeHoraFinalMenor, function () {
                $('#txtHoraFinal').focus();
            });
            return false;
        }
    }

    if ($('#ddlCausa option:selected').val() == '0') {
        MostrarMensaje(window.MensajeCausaRequerida, function () {
            $('#ddlCausa').focus();
        });
        return false;
    }
    return true;
};

//Función para agregar un registro al Grid de Horas Muertas
AgregarHorasMuertas = function () {
    if (ValidarAntesAgregarHorasMuertas()) {
        var horaInicial = $('#txtHoraInicial').val();
        var horaFinal = $('#txtHoraFinal').val();
        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial > fechaFinal) {
            MostrarMensaje(window.MensajeHoraFinalMenor, function () {
                $('#txtHoraFinal').focus();
            });
            return;
        }
        var diferenciaHoras = DiferenciaHorasFechas(fechaInicial, fechaFinal);
        var horas = Math.floor(diferenciaHoras);
        var minutos = diferenciaHoras % 1;
        var minutosFormateados = Math.round(minutos * 60);
        if (minutosFormateados < 10) {
            minutosFormateados = '0' + minutosFormateados;
        }
        var diferenciaFinal = horas + ':' + minutosFormateados;


        var horasMuertas = {};
        horasMuertas.Horas = diferenciaFinal;
        horasMuertas.HoraInicio = horaInicial;
        horasMuertas.HoraFin = horaFinal;
        horasMuertas.CausaTiempoMuertoID = $('#ddlCausa option:selected').val();
        horasMuertas.Ticket = ticketHorasMuertas;
        listaHorasMuertas.push(horasMuertas);

        var causa = $('#ddlCausa option:selected').text();
        AgregarRenglonGridHorasMuertas(diferenciaFinal, causa);
        LimpiarHorasMuertas();
    }
};

//Función para agregar el Renglon al Grid de Horas Muertas
AgregarRenglonGridHorasMuertas = function (horas, causa) {
    var renglon = '<tr>';
    renglon += '<td class="horasMuertas textoIzquierda">' + horas + ' </td>';
    renglon += '<td>' + causa + ' </td>';
    renglon += '</tr>';
    $('#GridCausasRegistradas tbody').append(renglon);
};

//Función para modificar el registro del Grid principal
AgregarRenglonGridPrincipalModificacion = function () {

    if ($('#txtLitrosFinal').val() == '') {
        MostrarMensaje(window.MensajeCapturarLitrosFinal, function () {
            $('#txtLitrosFinal').focus();
        });
        return;
    }
    if ($('#txtHorometroFinal').val() == '') {
        MostrarMensaje(window.MensajeCapturarHorometroFinal, function () {
            $('#txtHorometroFinal').focus();
        });
        return;
    }
    if ($('#txtHoraTicketFinal').val() == '') {
        MostrarMensaje(window.MensajeCapturarHoraTicketFinal, function () {
            $('#txtHoraTicketFinal').focus();
        });
        return;
    }

    var consumoAguaGrasa = '';
    if ($('#txtLitrosFinal').val() != '') {
        var litrosInicio = TryParseInt($('#txtLitrosInicial').val(), 0);
        var litrosFinal = TryParseInt($('#txtLitrosFinal').val(), 0);
        if (litrosFinal < litrosInicio) {
            MostrarMensaje(window.MensajeLitrosFinalMenor, function () {
                $('#txtLitrosFinal').focus();
            });
            return;
        }
        consumoAguaGrasa = litrosFinal - litrosInicio;
    }

    var horometroInicial = TryParseInt($('#txtHorometroInicial').val(), 0);
    var horometroFinal = TryParseInt($('#txtHorometroFinal').val(), 0);
    if (horometroFinal < horometroInicial) {
        MostrarMensaje(window.MensajeHorometroFinal, function () {
            $('#txtHorometroFinal').focus();
        });
        return;
    }

    var diferenciaFinal = '';
    if ($('#txtHoraTicketFinal').val() != '') {

        var finalHoras = TryParseInt($('#txtHoraTicketFinal').val().split(':')[0], 0);
        var finalMinutos = TryParseInt($('#txtHoraTicketFinal').val().split(':')[1], 0);
        
        if ($('#txtHoraTicketFinal').val().indexOf("_") > -1) {
            MostrarMensaje(window.MensajeFormatoHorasFinal, function () {
                $('#txtHoraTicketFinal').focus();
            });
            return;
        }

        if ((finalHoras > 24 || finalMinutos > 60) || (finalHoras == 24 && finalMinutos > 0) ||
            ($('#txtHoraTicketFinal').val().split(':')[1] == '' || $('#txtHoraTicketFinal').val().split(':')[1] == '__') ||
            ($('#txtHoraTicketFinal').val().split(':')[0] == '' || $('#txtHoraTicketFinal').val().split(':')[0] == '__')) {
            MostrarMensaje(window.MensajeFormatoHorasFinal, function () {
                $('#txtHoraTicketFinal').focus();
            });
            return;
        }

        var horaInicial = $('#txtHoraTicketInicial').val();
        var horaFinal = $('#txtHoraTicketFinal').val();
        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial > fechaFinal) {
            MostrarMensaje(window.MensajeHoraFinalMenor, function () {
                $('#txtHoraTicketFinal').focus();
            });
            return;
        }
        var diferenciaHoras = DiferenciaHorasFechas(fechaInicial, fechaFinal);
        var horas = Math.floor(diferenciaHoras);
        var minutos = diferenciaHoras % 1;
        var minutosFormateados = Math.round(minutos * 60);
        if (minutosFormateados < 10) {
            minutosFormateados = '0' + minutosFormateados;
        }
        diferenciaFinal = horas + ':' + minutosFormateados;

    }


    var renglon;

    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        if ($(this).find('.columnaTicket').text().trim() == $('#txtTicket').val()) {
            renglon = $(this);
        }
    });



    renglon.find('.columnaHoras').text(diferenciaFinal);
    renglon.find('.columnaConsumo').text(consumoAguaGrasa);
    renglon.find('.columnaHorometroFinal').text($('#txtHorometroFinal').val().trim());
    renglon.attr('data-litrosFinal', $('#txtLitrosFinal').val());
    renglon.attr('data-horaFinal', $('#txtHoraTicketFinal').val());
    editandoRegistro = false;
    LimpiarControles();
    $('#txtLitrosFinal').attr('disabled', true);
    $('#txtHorometroFinal').attr('disabled', true);
    $('#txtHoraTicketFinal').attr('disabled', true);

    if ($('#GridProduccionDiariaMolino tbody tr').length > 1) {
        CalcularCantidadPiePagina();
    }
};

//Función para agregar los valores a los controles del renglon a modificar
ModificarRegistroGrid = function (renglon) {
    if (renglon.find('.columnaHoras').text().trim() != '') {
        MostrarMensaje(window.MensajeRegistroCompleto, null);
        return;
    }
    $('#ddlTurno').val(renglon.attr('data-turno').trim());
    $('#txtProducto').val(renglon.attr('data-producto').trim());
    $('#ddlForraje').val(renglon.attr('data-forraje').trim());
    $('#txtLitrosInicial').val(renglon.attr('data-litrosInicial').trim());
    $('#txtHoraTicketInicial').val(renglon.attr('data-horaInicial').trim());

    $('#txtTicket').val(renglon.find('.columnaTicket').text().trim());
    $('#txtLote').val(renglon.find('.columnaLote').text().trim());
    $('#txtHumedad').val(renglon.find('.columnaHumedad').text().trim());
    $('#txtKilosNeto').val(FormatearCantidad(renglon.find('.columnaKilosNeto').text().trim()));
    $('#txtConteoPacas').val(renglon.find('.columnaConteoPacas').text().trim());
    $('#txtHorometroInicial').val(renglon.find('.columnaHorometroInicial').text().trim());
    $('#txtDescripcionProducto').val(renglon.find('.columnaProducto').text().trim());

    editandoRegistro = true;
    $('#txtHoraTicketFinal').attr('disabled', false);
    $('#txtHorometroFinal').attr('disabled', false);
    $('#txtLitrosFinal').attr('disabled', false);


    $('#ddlTurno').attr('disabled', true);
    $('#txtTicket').attr('disabled', true);
    $('#txtProducto').attr('disabled', true);
    $('#ddlForraje').attr('disabled', true);
    $('#txtLitrosInicial').attr('disabled', true);
    $('#txtHorometroInicial').attr('disabled', true);
    $('#txtHoraTicketInicial').attr('disabled', true);


    $('#txtLitrosFinal').focus();
};

//Función para agregar el Renglon al Grid principal
AgregarRenglonGridPrincipal = function () {
    var renglon = '<tr data-turno=' + $('#ddlTurno option:selected').val() + ' data-producto = ' + $('#txtProducto').val() +
                     ' data-forraje=' + $('#ddlForraje').val() + ' data-litrosInicial=' + $('#txtLitrosInicial').val() + ' data-horaInicial =' + $('#txtHoraTicketInicial').val() + ' data-litrosFinal = "0"' +
        '               data-pesajeMateriaPrimaID = ' + pesajeMateriaPrimaID + ' data-horaFinal = "">';
    renglon += '<td class="textoDerecha columnaTicket"> ' + $('#txtTicket').val() + ' </td>';

    var diferenciaFinal = '';
    if ($('#txtHoraTicketFinal').val() != '') {
        var horaInicial = $('#txtHoraTicketInicial').val();
        var horaFinal = $('#txtHoraTicketFinal').val();
        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial > fechaFinal) {
            MostrarMensaje(window.MensajeHoraFinalMenor, null);
            return;
        }
        var diferenciaHoras = DiferenciaHorasFechas(fechaInicial, fechaFinal);
        var horas = Math.floor(diferenciaHoras);
        var minutos = diferenciaHoras % 1;
        var minutosFormateados = Math.round(minutos * 60);
        if (minutosFormateados < 10) {
            minutosFormateados = '0' + minutosFormateados;
        }
        diferenciaFinal = horas + ':' + minutosFormateados;

    }

    var horometroFinal = '';
    if ($('#txtHorometroFinal').val() != '') {
        horometroFinal = $('#txtHorometroFinal').val();
    }

    renglon += '<td class="textoIzquierda columnaHoras"> ' + diferenciaFinal + ' </td>';
    renglon += '<td class="textoIzquierda columnaProducto "> ' + $('#txtDescripcionProducto').val() + ' </td>';
    renglon += '<td class="textoDerecha columnaHorometroInicial "> ' + $('#txtHorometroInicial').val() + ' </td>';
    renglon += '<td class="textoDerecha columnaHorometroFinal "> ' + horometroFinal + ' </td>';
    renglon += '<td class="textoIzquierda columnaForraje "> ' + $('#ddlForraje option:selected').text() + ' </td>';
    renglon += '<td class="textoDerecha columnaLote "> ' + $('#txtLote').val() + ' </td>';
    renglon += '<td class="textoDerecha columnaKilosNeto "> ' + FormatearCantidad($('#txtKilosNeto').val()) + ' </td>';

    var consumoAguaGrasa = '';
    if ($('#txtLitrosFinal').val() != '') {
        var horainicio = TryParseInt($('#txtLitrosInicial').val(), 0);
        var horafinal = TryParseInt($('#txtLitrosFinal').val(), 0);
        consumoAguaGrasa = horafinal - horainicio;
    }
    renglon += '<td class="textoDerecha columnaConsumo "> ' + consumoAguaGrasa + ' </td>';
    renglon += '<td class="textoDerecha columnaHumedad "> ' + $('#txtHumedad').val() + ' </td>';
    renglon += '<td class="textoDerecha columnaHorasMuertas "> ' + 0 + ' </td>'; //Horas Muertas
    renglon += '<td class="textoDerecha columnaConteoPacas "> ' + $('#txtConteoPacas').val() + ' </td>';
    renglon += '</tr>';
    var grid = $('#GridProduccionDiariaMolino tbody');
    grid.append(renglon);
    if ($('#GridProduccionDiariaMolino tbody tr').length > 1) {
        CalcularCantidadPiePagina();
    }
    LimpiarControles();
    $('#ddlTurno').attr('disabled', true);

};

//Función para calcular el Pie de Pagina del Grid principal
CalcularCantidadPiePagina = function () {
    var totalKilosNeto = 0;
    var totalConsumo = 0;
    var totalConteoPacas = 0;
    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        totalKilosNeto += TryParseInt($(this).find('.columnaKilosNeto').text().trim().replace(/,/g, '').replace(/_/g, ''), 0);
        totalConsumo += TryParseInt($(this).find('.columnaConsumo').text().trim(), 0);
        totalConteoPacas += TryParseInt($(this).find('.columnaConteoPacas').text().trim(), 0);
    });
    var piePaginaGrid = $('#GridProduccionDiariaMolino tfoot tr').first();
    piePaginaGrid.find('.pieKilosNetos').text(FormatearCantidad(totalKilosNeto));
    piePaginaGrid.find('.pieConsumo').text(totalConsumo);
    piePaginaGrid.find('.pieConteoPacas').text(totalConteoPacas);
};

//Función para calcular el Pie de Pagina del Grid supervisor
CalcularCantidadPiePaginaSupervisor = function () {
    var totalKilosNeto = 0;
    var totalConsumo = 0;
    var totalConteoPacas = 0;
    $('#GridProduccionSupervisor tbody tr').each(function () {
        totalKilosNeto += TryParseInt($(this).find('.columnaKilosNeto').text().trim().replace(/,/g, '').replace(/_/g, ''), 0);
        totalConsumo += TryParseInt($(this).find('.columnaConsumo').text().trim(), 0);
        totalConteoPacas += TryParseInt($(this).find('.columnaConteoPacas').text().trim(), 0);
    });
    var piePaginaGrid = $('#GridProduccionSupervisor tfoot tr').first();
    piePaginaGrid.find('.pieKilosNetos').text(FormatearCantidad(totalKilosNeto));
    piePaginaGrid.find('.pieConsumo').text(totalConsumo);
    piePaginaGrid.find('.pieConteoPacas').text(totalConteoPacas);
};

//Función para limpiar totalmente la pantalla
LimpiarPantallaCompleta = function () {
    LimpiarControles();
    CargarGridDefault();
    $('#ddlTurno').attr('disabled', false).val('0');
    $('#txtObservacion').val('');
    $('#lblEstatus').text(window.NoSupervisado);
    editandoRegistro = false;
    pesajeMateriaPrimaID = 0;
    ticketHorasMuertas = 0;
    listaHorasMuertas.length = 0;
    listaRenglonesEliminar.length = 0;
    usuarioAutorizo = 0;
};

//Función para limpiar los controles de captura de la pantalla
LimpiarControles = function () {
    $('input[type=text]').not('#txtFecha').each(function () {
        if ($(this).hasClass("claseRequerida")) {
            $(this).removeClass('claseRequerida');
        }
        $(this).val('');
    });
    $('input[type=number]').each(function () {
        if ($(this).hasClass("claseRequerida")) {
            $(this).removeClass('claseRequerida');
        }
        $(this).val('');
    });
    $('input[type=time]').each(function () {
        if ($(this).hasClass("claseRequerida")) {
            $(this).removeClass('claseRequerida');
        }
        $(this).val('');
    });
    $('select').not('#ddlTurno').each(function () {
        if ($(this).hasClass("claseRequerida")) {
            $(this).removeClass('claseRequerida');
        }
        $(this).val('0');
    });

    if ($('#GridProduccionDiariaMolino tbody tr').length == 0) {
        $('#ddlTurno').val('0');
    }

    $('#ddlTurno').removeClass('claseRequerida');
    editandoRegistro = false;

    $('#txtProducto').attr('disabled', false);
    $('#txtTicket').attr('disabled', false);
    $('#ddlForraje').attr('disabled', false);
    $('#txtLitrosInicial').attr('disabled', false);
    $('#txtHorometroInicial').attr('disabled', false);
    $('#txtLitrosFinal').attr('disabled', true);
    $('#txtHorometroFinal').attr('disabled', true);
    $('#txtHoraTicketFinal').attr('disabled', true);

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

    $('#btnLimpiar').click(function () {
        LimpiarControles();
    });

    $('#btnAgregar').click(function () {
        if (editandoRegistro) {
            AgregarRenglonGridPrincipalModificacion();
        } else {
            if (ValidarAntesAgregarGridPrincipal()) {
                AgregarRenglonGridPrincipal();
            }
        }
    });

    $('#divGridMolino').on("dblclick", "#GridProduccionDiariaMolino tbody tr", function () {
        ModificarRegistroGrid($(this));
    });

    $('#divGridMolino').on('click', "#GridProduccionDiariaMolino tbody tr", function () {
        var selected = $(this).hasClass("highlight");
        $("#GridProduccionDiariaMolino tbody tr").removeClass("highlight");
        if (!selected) {
            $(this).addClass("highlight");
            ticketHorasMuertas = $(this).find('.columnaTicket').text().trim();
        }
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

    EventosModalSupervisor();
    EventosModalHorasMuertas();
    EventosModalGridSupervisor();
    EventosBusquedaProducto();
    EventosBusquedaTicket();
};

//Función para asignarle los eventos a los controles del Modal Grid Supervisor
EventosModalGridSupervisor = function () {
    $('#btnCancelarSupervision').click(function () {
        MensajeCerrarModalSupervisor();
    });

    $('.cerrarSupervisorCompleto').click(function () {
        MensajeCerrarModalSupervisor();
    });

    $('#btnGuardarSupervision').click(function () {
        GuardarSupervisor();
    });

    $('#divGridProduccionSupervisor').on('click', '.cancelarTicket', function () {
        var renglon = $(this).closest('tr');
        bootbox.dialog({
            message: window.MensajeCancelarTicket,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        EliminarRenglonGridSupervisor(renglon);
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

//Función para guardar la supervisión
GuardarSupervisor = function () {
    if ($('#txtObservacionesSupervisor').val() == '') {
        MostrarMensaje(window.MensajeCapturarObservaciones, null);
        return;
    }
    EliminarRenglonesGridPrincipal();
    MostrarMensaje(window.MensajeGuardoExito, function () {
        $('#modalSupervisorCompleto').modal('hide');
        $('#lblEstatus').text(window.Supervisado);

    });
};

//Función para eliminar los renglones que se haya cancelado en el Grid del supervisor
EliminarRenglonesGridPrincipal = function () {
    $(listaRenglonesEliminar).each(function () {
        $(this).remove();
    });
    CalcularCantidadPiePagina();
};

//Función para borrar el renglon del Grid de supervisor
EliminarRenglonGridSupervisor = function (renglon) {
    var ticket = renglon.find('.columnaTicket').text().trim();
    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        if ($(this).find('.columnaTicket').text().trim() == ticket) {
            listaRenglonesEliminar.push($(this));
        }
    });
    renglon.remove();
    CalcularCantidadPiePaginaSupervisor();
};

//Función para mandar el mensaje al querer cerrar el modal del supervisor
MensajeCerrarModalSupervisor = function () {
    bootbox.dialog({
        message: window.MensajeSalirSinGuardar,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    listaRenglonesEliminar.length = 0;
                    usuarioAutorizo = 0;
                    $('#modalSupervisorCompleto').modal('hide');
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
};

//Función para asignarle los eventos a los controles del Modal de inicio de sesión
EventosModalSupervisor = function () {
    $('.cerrarSupervisor').click(function () {
        $('#modalSupervisor').modal('hide');
    });

    $('#btnSupervisor').click(function () {
        if ($('#GridProduccionDiariaMolino tbody tr').length == 0) {
            MostrarMensaje(window.MensajeSinDatosSupervisar, null);
            return;
        }
        $('#modalSupervisor').modal('show');
    });

    $('#modalSupervisor').on('shown.bs.modal', function () {
        $('#txtUsuario').val('');
        $('#txtContrasenia').val('');
        $('#txtUsuario').focus();
        $("html").css("margin-right", "-15px");
    });

    $('#btnCancelarSupervisor').click(function () {
        $('#modalSupervisor').modal('hide');
    });

    $('#btnAceptarSupervisor').click(function () {
        InicioSesionSupervisor();
    });
};

//Función para agregar las horas muertas al Grid principal
CargarHorasMuertasGrid = function () {
    if ($('#GridCausasRegistradas tbody tr').length == 0) {
        return;
    }
    var minutosTotales = 0;
    $('#GridCausasRegistradas tbody tr').each(function () {
        var horas = TryParseInt($(this).find('.horasMuertas').text().trim().split(':')[0], 0);
        var minutos = TryParseInt($(this).find('.horasMuertas').text().trim().split(':')[1], 0);

        minutosTotales += ((horas * 60) + minutos);
    });

    var horasdivididas = minutosTotales / 60;

    var horasFinal = Math.floor(horasdivididas);
    var minutosFinal = horasdivididas % 1;
    var minutosFormateados = Math.round(minutosFinal * 60);
    if (minutosFormateados < 10) {
        minutosFormateados = '0' + minutosFormateados;
    }
    var diferenciaFinal = horasFinal + ':' + minutosFormateados;


    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        if ($(this).find('.columnaTicket').text().trim() == ticketHorasMuertas) {
            $(this).find('.columnaHorasMuertas').text(diferenciaFinal);
        }
    });
};

//Función para asignarle los eventos a los controles del Modal de Horas Muertas
EventosModalHorasMuertas = function () {
    $('#btnHoraMuertas').click(function () {
        var renglon = $("#GridProduccionDiariaMolino tbody tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje(window.MensajeSeleccionarRegistroHorasMuertas, null);
            return false;
        }
        if (renglon.find('.columnaHorasMuertas').text().trim() != '0') {
            MostrarMensaje(window.MensajeModificarHorasMuertas, null);
            return false;
        }
        $('#GridCausasRegistradas tbody').html('');
        $('#modalHorasMuertas').modal('show');
        $("html").css("margin-right", "-15px");
        return true;
    });

    $('#btnAgregarMuertas').click(function () {
        AgregarHorasMuertas();
    });

    $('.cerrarHorasMuertas').click(function () {
        CargarHorasMuertasGrid();
        $('#modalHorasMuertas').modal('hide');
    });

    $('#btnCerrarHorasMuertas').click(function () {
        CargarHorasMuertasGrid();
        $('#modalHorasMuertas').modal('hide');
    });

    $('#modalHorasMuertas').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    $('#modalHorasMuertas').bind('show.bs.modal', function () {
        LimpiarHorasMuertas();
        CargarCausasTiempoMuerto();
        $("html").css("margin-right", "-15px");
    });

    $('#btnLimpiarMuertas').click(function () {
        LimpiarHorasMuertas();
    });

    $('#txtHoraInicial').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            $('#txtHoraFinal').focus();
        }
    });

    $('#txtHoraInicial').focusout(function () {
        if ($('#txtHoraInicial').val() != '') {
            $('#txtHoraFinal').attr('disabled', false);
        }
    });

    $('#txtHoraFinal').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            $('#ddlCausa').focus();
        }
    });

};

//Función para asignarle los eventos a los controles de la ayuda de Producto
EventosBusquedaProducto = function () {
    //Evento para quitarle el Estilo del modal cuando se cierra la pantalla
    $('#modalBusquedaProducto').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaProducto').bind('show.bs.modal', function () {
        CargarGridBusquedaProductoDefault();
        $("html").css("margin-right", "-15px");
        $('#txtProductoBusqueda').focus();
    });

    //Evento que se dispara para abrir el modal de ayuda de Productos
    $('#btnAyudaProductos').click(function () {
        $('#modalBusquedaProducto').modal('show');
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarProducto').click(function () {
        BuscarProductos();
    });

    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarProducto').click(function () {
        var renglon = $("#GridProductos tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje(window.MensajeSeleccionarProducto, null);
            return false;
        }
        var productoId = renglon.find('.columnaID').text().trim();
        var descripcion = renglon.find('.columnaDescripcion').text().trim();
        $('#txtProducto').val(productoId);
        $('#txtDescripcionProducto').val(descripcion);
        $('#modalBusquedaProducto').modal('hide');
        ObtenerValoresProduccionProducto();
        return true;
    });

    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarProducto').click(function () {
        MensajeCerrarModalProducto();
    });

    $('.cerrarBusquedaProductos').click(function () {
        MensajeCerrarModalProducto();
    });

    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridProductos').on("dblclick", "#GridProductos tbody tr", function () {
        var productoId = $(this).find('.columnaID').text().trim();
        var descripcion = $(this).find('.columnaDescripcion').text().trim();
        $('#txtProducto').val(productoId);
        $('#txtDescripcionProducto').val(descripcion);
        $('#modalBusquedaProducto').modal('hide');
        ObtenerValoresProduccionProducto();
    });

    $('#txtProducto').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            if ($('#txtProducto').val() != '') {
                $('#txtTicket').focus();
            }
        }
    });

    $('#txtProducto').focusout(function (e) {
        if ($('#txtProducto').val() != '') {
            e.preventDefault();
            BuscarProductosPorID();
            $('#txtTicket').focus();
        }
    });

    $('#txtProductoBusqueda').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            BuscarProductos();
        }
    });
};

//Función para mandar el mensaje al querer cerrar el modal de la ayuda de Ticket
MensajeCerrarModalTicket = function () {
    bootbox.dialog({
        message: window.MensajeSalirSinSeleccionarTicket,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    $('#modalBusquedaTicket').modal('hide');
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
};

//Función para mandar el mensaje al querer cerrar el modal de la ayuda de Producto
MensajeCerrarModalProducto = function () {
    bootbox.dialog({
        message: window.MensajeSalirSinSeleccionarProducto,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    $('#modalBusquedaProducto').modal('hide');
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
};

//Función para asignarle los eventos a los controles de la ayuda de Ticket
EventosBusquedaTicket = function () {

    //Evento para quitarle el Estilo cuando se cierra el modal
    $('#modalBusquedaTicket').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaTicket').bind('show.bs.modal', function () {
        CargarGridBusquedaTicketDefault();
        $("html").css("margin-right", "-15px");
        $('#txtTicketBusqueda').focus();
        BuscarTickets();
    });

    //Evento que se dispara para abrir el modal de ayuda de Ticket
    $('#btnAyudaTickets').click(function () {
        //if (TryParseInt($('#txtProducto').val(), 0) == 0) {
        //    MostrarMensaje(window.MensajeProductoRequerido, function () {
        //        $('#txtProducto').focus();
        //    });
        //    return;
        //}
        $('#modalBusquedaTicket').modal('show');
    });

    //Evento para buscar los productos en base al criterio de búsqueda
    $('#btnBuscarTicket').click(function () {
        BuscarTickets();
    });

    //Evento para agregar el Producto seleccionado en la búsqueda
    $('#btnAgregarTicket').click(function () {
        BloquearPantalla();
        var renglon = $("#GridTickets tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje(window.MensajeSeleccionarTicket, null);
            return false;
        }
        var ticket = renglon.find('.columnaID').text().trim();
        $('#txtTicket').val(ticket);
        $('#modalBusquedaTicket').modal('hide');
        var existe = false;
        var renglonExiste;
        $('#GridProduccionDiariaMolino tbody tr').each(function () {
            if ($(this).find('.columnaTicket').text().trim() == $('#txtTicket').val().trim()) {
                existe = true;
                renglonExiste = $(this);
            }
        });
        if (existe) {
            ModificarRegistroGrid(renglonExiste);
            DesbloquearPantalla();
        } else {
            ObtenerValoresProduccionTicket();
        }
        //ObtenerValoresProduccionTicket();
        return true;
    });

    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarTicket').click(function () {
        MensajeCerrarModalTicket();
    });

    $('.cerrarBusquedaTicket').click(function () {
        MensajeCerrarModalTicket();
    });

    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridTickets').on("dblclick", "#GridTickets tbody tr", function () {
        var ticket = $(this).find('.columnaID').text().trim();
        $('#txtTicket').val(ticket);
        $('#modalBusquedaTicket').modal('hide');
        var existe = false;
        var renglon;
        $('#GridProduccionDiariaMolino tbody tr').each(function () {
            if ($(this).find('.columnaTicket').text().trim() == $('#txtTicket').val().trim()) {
                existe = true;
                renglon = $(this);
            }
        });
        if (existe) {
            ModificarRegistroGrid(renglon);
            DesbloquearPantalla();
        } else {
            ObtenerValoresProduccionTicket();
        }
        //ObtenerValoresProduccionTicket();
    });

    $('#txtTicket').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            if ($('#txtTicket').val() != '') {
                $('#ddlForraje').focus();
            }
        }
    });

    $('#txtTicket').focusout(function (e) {
        if ($('#txtTicket').val() != '') {
            e.preventDefault();
            BuscarTicketsPorID();
        }
    });

    $('#txtTicketBusqueda').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            BuscarTickets();
        }
    });
};

//Función para cargar los Tickets
BuscarTickets = function () {
    var filtroTicket = {};
    filtroTicket.Ticket = $('#txtTicketBusqueda').val();
    filtroTicket.ProductoID = TryParseInt($('#txtProducto').val(), 0);
    datosMetodos = { 'filtroTicket': filtroTicket };
    urlMetodos = rutaPantalla + '/ObtenerTickets';
    mensajeErrorMetodos = window.ErrorConsultarTickets;
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarTicketsSuccess, mensajeErrorMetodos);
};

//Función Success para cargar los Tickets
BuscarTicketsSuccess = function (msg) {
    var datos = {};
    CargarRecursosGridBusquedaTicket();
    datos.Recursos = recursos;
    datos.Tickets = msg.d;
    var divContenedorGrid = $('#divGridTickets');
    divContenedorGrid.setTemplateURL('../Templates/GridBusquedaTicket.htm');
    divContenedorGrid.processTemplate(datos);
    $("#GridTickets tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#GridTickets tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};

//Función para cargar los Ticket cuando no se usa la ayuda
BuscarTicketsPorID = function () {
    BloquearPantalla();
    var filtroTicket = {};
    filtroTicket.Ticket = $('#txtTicket').val();
    filtroTicket.ProductoID = TryParseInt($('#txtProducto').val(), 0);
    //if (filtroTicket.ProductoID == 0) {
    //    MostrarMensaje(window.MensajeProductoRequerido, function () {
    //        $('#txtTicket').val('');
    //        $('#txtProducto').focus();
    //        DesbloquearPantalla();
    //    });
    //    return;
    //}
    datosMetodos = { 'filtroTicket': filtroTicket };
    urlMetodos = rutaPantalla + '/ObtenerTickets';
    mensajeErrorMetodos = window.ErrorConsultarTickets;
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarTicketsPorIDSuccess, mensajeErrorMetodos);
};

//Función Success para cargar los Ticket cuando no se usa la ayuda
BuscarTicketsPorIDSuccess = function (msg) {
    if (msg.d.length == 0) {
        MostrarMensaje(window.MensajeTicketNoExiste, function () {
            $('#txtTicket').val('');
            $('#txtTicket').focus();
            DesbloquearPantalla();
        });
        return false;
    }
    var existe = false;
    var renglon;
    $('#GridProduccionDiariaMolino tbody tr').each(function () {
        if ($(this).find('.columnaTicket').text().trim() == $('#txtTicket').val().trim()) {
            existe = true;
            renglon = $(this);
        }
    });
    if (existe) {
        ModificarRegistroGrid(renglon);
        DesbloquearPantalla();
    } else {
        ObtenerValoresProduccionTicket();
    }

    return true;
};

//Función para cargar los Productos cuando no se usa la ayuda
BuscarProductosPorID = function () {
    BloquearPantalla();
    $('#txtDescripcionProducto').val('');
    var productoInfo = {};
    productoInfo.ProductoId = $('#txtProducto').val();
    datosMetodos = { 'productoInfo': productoInfo };
    urlMetodos = rutaPantalla + '/ObtenerProductos';
    mensajeErrorMetodos = window.ErrorConsultarProductos;
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarProductosPorIDSuccess, mensajeErrorMetodos);
};

//Función Succcess para cargar los Productos cuando no se usa la ayuda
BuscarProductosPorIDSuccess = function (msg) {
    if (msg.d.length == 0) {
        MostrarMensaje(window.MensajeProductoNoExiste, function () {
            $('#txtProducto').val('');
            $('#txtProducto').focus();
            DesbloquearPantalla();
        });
        return false;
    }
    $('#txtDescripcionProducto').val(msg.d[0].Descripcion);
    ObtenerValoresProduccionProducto();

    return true;
};

//Función para buscar los productos mediante la ayuda
BuscarProductos = function () {
    var productoInfo = {};
    productoInfo.Descripcion = $('#txtProductoBusqueda').val();
    datosMetodos = { 'productoInfo': productoInfo };
    urlMetodos = rutaPantalla + '/ObtenerProductos';
    mensajeErrorMetodos = window.MensajeConsultarProductosTodos;
    EjecutarWebMethod(urlMetodos, datosMetodos, BuscarProductosSuccess, mensajeErrorMetodos);
};

//Función Success para buscar los productos mediante la ayuda
BuscarProductosSuccess = function (msg) {
    var datos = {};
    CargarRecursosGridBusquedaProductos();
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

//Función para obtener los datos adicionales del Producto
ObtenerValoresProduccionProducto = function () {
    var productoInfo = {};
    productoInfo.ProductoId = $('#txtProducto').val();
    datosMetodos = { 'productoInfo': productoInfo };
    urlMetodos = rutaPantalla + '/ObtenerValoresProduccionMolino';
    mensajeErrorMetodos = window.ErrorInformacionAdicional;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerValoresProduccionProductoSuccess, mensajeErrorMetodos);
};

//Función Success para obtener los datos adicionales del Producto
ObtenerValoresProduccionProductoSuccess = function (msg) {
    if (msg.d.length == 0) {
        MostrarMensaje(window.MensajeSinPesajeMateriaPrima, function () {
            DesbloquearPantalla();
        });
        return;
    }
    var valores = msg.d[0];
    $('#txtLote').val(valores.Lote);
    $('#txtHumedad').val(valores.HumedadForraje);
    DesbloquearPantalla();
};

//Función para obtener los datos adicionales del Ticket
ObtenerValoresProduccionTicket = function () {
    var filtroTicket = {};
    filtroTicket.Ticket = $('#txtTicket').val();
    datosMetodos = { 'filtroTicket': filtroTicket };
    urlMetodos = rutaPantalla + '/ObtenerValoresTicketProduccion';
    mensajeErrorMetodos = window.ErrorInformacionAdicionalTicket;
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerValoresProduccionTicketSuccess, mensajeErrorMetodos);
};

//Función Success para obtener los datos adicionales del Ticket
ObtenerValoresProduccionTicketSuccess = function (msg) {
    if (msg.d.length == 0) {
        MostrarMensaje(window.MensajeSinPesajeMateriaPrima, null);
        DesbloquearPantalla();
        return;
    }
    var valores = msg.d[0];
    pesajeMateriaPrimaID = valores.PesajeMateriaPrimaID;
    $('#txtProducto').val(valores.ProductoID);
    $('#txtDescripcionProducto').val(valores.Descripcion);
    $('#txtLote').val(valores.Lote);
    $('#txtHumedad').val(valores.HumedadForraje);
    $('#txtKilosNeto').val(FormatearCantidad(valores.KilosNetos));
    $('#txtConteoPacas').val(valores.ConteoPacas);
    $('#txtHoraTicketInicial').val(valores.HoraTicketInicial);
    DesbloquearPantalla();
};

//Función para cargar el grid de la ayuda de productos
CargarGridBusquedaProductoDefault = function () {
    var datos = {};
    CargarRecursosGridBusquedaProductos();
    datos.Recursos = recursos;
    datos.Productos = {};
    var divContenedor = $('#divGridProductos');

    divContenedor.setTemplateURL('../Templates/GridBusquedaProductos.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar el grid de la ayuda de tickets
CargarGridBusquedaTicketDefault = function () {
    var datos = {};
    CargarRecursosGridBusquedaTicket();
    datos.Recursos = recursos;
    datos.Tickets = {};
    var divContenedor = $('#divGridTickets');

    divContenedor.setTemplateURL('../Templates/GridBusquedaTicket.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar el grid principal
CargarGridDefault = function () {
    var datos = {};
    CargarRecursosGridGeneral();
    datos.Recursos = recursos;
    datos.Produccion = {};
    var divContenedor = $('#divGridMolino');

    divContenedor.setTemplateURL('../Templates/GridProduccionDiariaMolino.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar los recursos  del grid principal
CargarRecursosGridGeneral = function () {
    recursos = {};
    recursos.Ticket = window.ColumnaTicket;
    recursos.HoraTicket = window.ColumnaHoraTicket;
    recursos.Producto = window.ColumnaProducto;
    recursos.HorometroInicial = window.ColumnaHorometroInicial;
    recursos.HorometroFinal = window.ColumnaHorometroFinal;
    recursos.EspecificacionForraje = window.ColumnaEspecificacionForraje;
    recursos.Lote = window.ColumnaLote;
    recursos.KilosNeto = window.ColumnaKilosNetos;
    recursos.ConsumoAgua = window.ColumnaConsumo;
    recursos.HumedadForraje = window.ColumnaHumedad;
    recursos.HorasMuertas = window.ColumnaHorasMuertas;
    recursos.ConteoPacas = window.ColumnaConteoPacas;
};

//Función para cargar los recursos  del grid supervisor
CargarRecursosGridSupervisor = function () {
    recursos = {};
    recursos.Ticket = window.ColumnaTicket;
    recursos.HoraTicket = window.ColumnaHoraTicket;
    recursos.Producto = window.ColumnaProducto;
    recursos.HorometroInicial = window.ColumnaHorometroInicial;
    recursos.HorometroFinal = window.ColumnaHorometroFinal;
    recursos.EspecificacionForraje = window.ColumnaEspecificacionForraje;
    recursos.Lote = window.ColumnaLote;
    recursos.KilosNeto = window.ColumnaKilosNetos;
    recursos.ConsumoAgua = window.ColumnaConsumo;
    recursos.HumedadForraje = window.ColumnaHumedad;
    recursos.HorasMuertas = window.ColumnaHorasMuertas;
    recursos.ConteoPacas = window.ColumnaConteoPacas;
    recursos.Cancelar = window.ColumnaCancelar;
};

//Función para cargar los recursos  del grid busqueda de productos
CargarRecursosGridBusquedaProductos = function () {
    recursos = {};
    recursos.Id = window.ColumnaId;
    recursos.Descripcion = window.ColumnaDescripcion;
};

//Función para cargar los recursos  del grid busqueda de tickets
CargarRecursosGridBusquedaTicket = function () {
    recursos = {};
    recursos.Id = window.ColumnaTicket;
    recursos.Descripcion = window.ColumnaProducto;
};