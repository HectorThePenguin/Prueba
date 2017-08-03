// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="jscomun.js" />

var rutaPantalla = location.pathname;
var recursos = {};
var urlMetodos;
var datosMetodos;
var mensajeErrorMetodos;
var listaHorasMuertas = new Array();

var camionRepartoID;
var repartoAlimentoID = 0;
var repartoAlimento = {};
var repartoAlimentoDetalle = new Array();
var bloquearAyuda = false;

$(document).ready(function () {

    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    if ($('#hfErrorImprimir').val() != '') {
        if ($('#hfErrorImprimir').val() != '0') {
            MostrarMensaje($('#hfErrorImprimir').val(), function () {
                $('#hfErrorImprimir').val('');
            });
        }
    }

    if ($('#hfOperador').val() == '0') {
        MostrarMensaje(window.MensajeSinOperador, function () {
            location.href = "../Principal.aspx";
        });
        return;
    }
    $('#txtOperador').val($('#hfOperador').val());
    ObtenerFechaServidor();
    CargarGridRepartosDefault();
    CargarTiposServicio();
    AsignarPluginsControles();
    AsignarEventosControles();
    $('#ddlServicio').focus();
});

AsignarEventosControles = function () {

    $('input, select').not('.horas').keydown(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#btnCancelar').click(function () {
        MostrarMensajeCancelar();
    });

    $('#btnGuardar').click(function () {
        if (ValidarAntesGuardar()) {
            BloquearPantalla();
            Guardar();
        }
    });
    $('#divGridRepartos').on('keyup', '.numeroTolva', function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            if ($(this).val() != '') {
                $('#txtObservacion').focus();
            }
        }
    });
    $('#divGridRepartos').on('focusout', '.numeroTolva', function () {
        if ($(this).val() != '') {
            GenerarRepartos($(this));
        }
    });
    $('#btnImprimir').click(function () {
        if (!ValidarTipoServicioValor()) {
            return;
        }
        if (!ValidarFechaReparto()) {
            return;
        }
        if (!ValidarCamionReparto()) {
            return;
        }
        location.assign('ImpresionCheckListReparto.aspx?Fecha=' + $('#txtFecha').val() + '&OperadorID=' + $('#hfOperadorID').val() + '' + '&CamionRepartoID=' + camionRepartoID + '');
        setTimeout(function () {
            MostrarMensaje('Documento impreso correctamente.', function () {
                LimpiarPantallaCompleta();
            });
        }, 1000);
        //location.assign('ImpresionCheckListReparto.aspx?Fecha=' + $('#txtFecha').val() + '&OperadorID=' + $('#hfOperadorID').val() + '' + '&CamionRepartoID=' + camionRepartoID + '');
    });

    $('#btnImprimir').attr('disabled', true);

    EventosBusquedaCamionReparto();
    EventosModalHorasMuertas();
};

AsignarPluginsControles = function () {
    $('#txtFecha').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "es",
        autoclose: true
    });

    $('.soloNumeros').numericInput();
    $('.horas').inputmask('99:99');

    $('#txtCamionReparto, #txtCamionRepartoBusqueda').alpha({
        allow: '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ',
        disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`._¿¡¨¨~``^^°´´áéíóúýÁÉÍÓÚÝñÑ'
    });
};

//Función para asignarle los eventos a los controles de la ayuda de CamionReparto
EventosBusquedaCamionReparto = function () {
    //Evento para quitarle el Estilo del modal cuando se cierra la pantalla
    $('#modalBusquedaCamionReparto').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    //Evento que se ejecuta cuando se abre el Modal
    $('#modalBusquedaCamionReparto').bind('show.bs.modal', function () {
        CargarGridBusquedaCamionRepartoDefault();
        BuscarCamionReparto();
        $("html").css("margin-right", "-15px");
        $('#txtCamionRepartoBusqueda').val('');
        $('#txtCamionRepartoBusqueda').focus();
    });

    //Evento que se dispara para abrir el modal de ayuda de CamionRepartos
    $('#btnAyudaCamionReparto').click(function () {
        if (!bloquearAyuda) {
            if (ValidarValoresAntesCamion()) {
                $('#modalBusquedaCamionReparto').modal('show');
            } else {
                $('#txtCamionReparto').val('');
            }
        }
    });

    //Evento para buscar los CamionRepartos en base al criterio de búsqueda
    $('#btnBuscarCamionReparto').click(function () {
        BuscarCamionReparto();
    });

    //Evento para agregar el CamionReparto seleccionado en la búsqueda
    $('#btnAgregarCamionReparto').click(function () {
        var renglon = $("#GridCamionReparto tr.highlight");
        if (renglon.length == 0) {
            MostrarMensaje(window.MensajeSeleccionarCamionReparto, null);
            return false;
        }
        var numeroEconomico = renglon.find('.columnaDescripcion').text().trim();
        camionRepartoID = renglon.find('.columnaID').text().trim();
        //var descripcion = renglon.find('.columnaDescripcion').text().trim();
        $('#txtCamionReparto').val(numeroEconomico);
        $('#modalBusquedaCamionReparto').modal('hide');
        ConsultarRepartos();
        //ObtenerValoresProduccionCamionReparto();
        return true;
    });

    //Evento que se dispara al darle al botón Cancelar
    $('#btnCancelarCamionReparto').click(function () {
        MensajeCerrarModalCamionReparto();
    });

    $('.cerrarBusquedaCamionReparto').click(function () {
        MensajeCerrarModalCamionReparto();
    });

    //Evento para cuando se da DobleClic en el elemento seleccionado del Grid
    $('#divGridCamionReparto').on("dblclick", "#GridCamionReparto tbody tr", function () {
        var numeroEconomico = $(this).find('.columnaDescripcion').text().trim();
        camionRepartoID = $(this).find('.columnaID').text().trim();
        $('#txtCamionReparto').val(numeroEconomico);
        //$('#txtDescripcionCamionReparto').val(descripcion);
        $('#modalBusquedaCamionReparto').modal('hide');
        ConsultarRepartos();
        //ObtenerValoresProduccionCamionReparto();
    });

    $('#txtCamionReparto').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            if ($('#txtCamionReparto').val() != '') {
                $('#txtHorometroInicial').focus();
            }
        }
    });

    $('#txtCamionReparto').focusout(function (e) {
        if ($('#txtCamionReparto').val() != '') {
            e.preventDefault();
            BuscarCamionRepartoPorNumeroEconomico();
            $('#txtHorometroInicial').focus();
        }
    });

    $('#txtCamionRepartoBusqueda').keyup(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            BuscarCamionReparto();
        }
    });
};

//Función para cargar el grid de la ayuda de CamionRepartos
CargarGridBusquedaCamionRepartoDefault = function () {
    var datos = {};
    CargarRecursosGridBusquedaCamionReparto();
    datos.Recursos = recursos;
    datos.CamionesReparto = {};
    var divContenedor = $('#divGridCamionReparto');


    divContenedor.setTemplateURL('../Templates/GridBusquedaCamionReparto.htm');
    divContenedor.processTemplate(datos);
};

//Función para cargar los recursos  del grid busqueda de CamionRepartos
CargarRecursosGridBusquedaCamionReparto = function () {
    recursos = {};
    recursos.Id = window.ColumnaId;
    recursos.NumeroEconomico = window.ColumnaNumeroEconomico;
};

//Función para cargar los CamionRepartos cuando no se usa la ayuda
BuscarCamionRepartoPorNumeroEconomico = function () {
    if (ValidarValoresAntesCamion()) {
        BloquearPantalla();
        $('#txtDescripcionCamionReparto').val('');
        var numeroEconomico = $('#txtCamionReparto').val();
        datosMetodos = { 'numeroEconomico': numeroEconomico };
        urlMetodos = rutaPantalla + '/ObtenerPorNumeroEconomico';
        mensajeErrorMetodos = window.ErrorConsultarCamionReparto;
        EjecutarWebMethod(urlMetodos, datosMetodos, BuscarCamionRepartoPorNumeroEconomicoSuccess, mensajeErrorMetodos);
    }
    else {
        $('#txtCamionReparto').val('');
    }
};

//Función para mandar el mensaje al querer cerrar el modal de la ayuda de CamionReparto
MensajeCerrarModalCamionReparto = function () {
    bootbox.dialog({
        message: window.MensajeSalirSinSeleccionarCamionReparto,
        buttons: {
            Aceptar: {
                label: 'Si',
                callback: function () {
                    $('#modalBusquedaCamionReparto').modal('hide');
                    return true;
                }
            },
            Cancelar: {
                label: 'No',
                callback: function () {
                    return true;
                }
            },
        }
    });
};

ValidarValoresAntesCamion = function () {
    if (!ValidarTipoServicioValor()) {
        return false;
    }
    if (!ValidarFechaReparto()) {
        return false;
    }
    return true;
};

//Función para buscar los CamionRepartos mediante la ayuda
BuscarCamionReparto = function () {
    if (ValidarValoresAntesCamion()) {
        var numeroEconomico = $('#txtCamionRepartoBusqueda').val();
        datosMetodos = { 'numeroEconomico': numeroEconomico };
        urlMetodos = rutaPantalla + '/ObtenerPorNumeroEconomicoBusqueda';
        mensajeErrorMetodos = window.ErrorConsultarCamionReparto;
        EjecutarWebMethod(urlMetodos, datosMetodos, BuscarCamionRepartoSuccess, mensajeErrorMetodos);
    }
};

//Función Success para buscar los CamionRepartos mediante la ayuda
BuscarCamionRepartoSuccess = function (msg) {
    var datos = {};
    CargarRecursosGridBusquedaCamionReparto();
    datos.Recursos = recursos;
    datos.CamionesReparto = msg.d;
    var divContenedorGrid = $('#divGridCamionReparto');
    divContenedorGrid.setTemplateURL('../Templates/GridBusquedaCamionReparto.htm');
    divContenedorGrid.processTemplate(datos);
    $("#GridCamionReparto tr").click(function () {
        var selected = $(this).hasClass("highlight");
        $("#GridCamionReparto tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
};

//Función Succcess para cargar los CamionRepartos cuando no se usa la ayuda
BuscarCamionRepartoPorNumeroEconomicoSuccess = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        MostrarMensaje(window.MensajeCamionRepartoNoExiste, function () {
            $('#txtCamionReparto').val('');
            $('#txtCamionReparto').focus();
            DesbloquearPantalla();
        });
        return false;
    }
    camionRepartoID = msg.d.CamionRepartoID;
    $('#txtCamionReparto').val(msg.d.NumeroEconomico);
    ConsultarRepartos();
    DesbloquearPantalla();
    return true;
};

ConsultarRepartos = function () {
    CargarGridRepartosDefault();
    LimpiarConsultarRepartos();
    BloquearPantalla();
    urlMetodos = rutaPantalla + '/ConsultarRepartos';
    var filtroCheckListReparto = {};
    filtroCheckListReparto.CamionRepartoID = camionRepartoID;
    filtroCheckListReparto.TipoServicioID = $('#ddlServicio option:selected').val();
    filtroCheckListReparto.Fecha = ToDate($('#txtFecha').val());
    datosMetodos = { 'filtro': filtroCheckListReparto };
    mensajeErrorMetodos = window.ErrorCausasTiempoMuerto;
    EjecutarWebMethod(urlMetodos, datosMetodos, ConsultarRepartosSuccess, mensajeErrorMetodos);
};

ConsultarRepartosSuccess = function (msg) {
    if (msg.d == null) {
        DesbloquearPantalla();
        return;
    }
    var reparto = msg.d;
    repartoAlimentoID = msg.d.RepartoAlimentoID;
    $('#txtHorometroInicial').val(reparto.HorometroInicial);
    $('#txtOdometroInicial').val(reparto.OdometroInicial);
    if (reparto.LitrosDiesel == 0) {
        $('#txtLitrosDiesel').val('');
    } else {
        $('#txtLitrosDiesel').val(reparto.LitrosDiesel);
        $('#txtLitrosDiesel').attr('disabled', true);
    }
    if (reparto.HorometroFinal == 0) {
        $('#txtHorometroFinal').val('');
    } else {
        $('#txtHorometroFinal').val(reparto.HorometroFinal);
        $('#txtHorometroFinal').attr('disabled', true);
    }
    if (reparto.OdometroFinal == 0) {
        $('#txtOdometroFinal').val('');
    } else {
        $('#txtOdometroFinal').val(reparto.OdometroFinal);
        $('#txtOdometroFinal').attr('disabled', true);
    }

    var completo = false;
    if (reparto.LitrosDiesel != 0 && reparto.HorometroFinal != 0 && reparto.OdometroFinal != 0) {
        completo = true;

    }
    if (completo) {
        $('#btnGuardar').attr('disabled', true);
        $('#txtObservacion').val(reparto.Observaciones);
        $('#txtObservacion').attr('disabled', true);
        $('#btnImprimir').attr('disabled', false);
    }
    var datos = {};
    CargarRecursosGridRepartos();
    datos.Recursos = recursos;
    datos.Repartos = msg.d.ListaGridRepartos;
    var divContenedor = $('#divGridRepartos');

    divContenedor.setTemplateURL('../Templates/GridCheckListReparto.htm');
    divContenedor.processTemplate(datos);
    $('#GridCheckListReparto tbody tr').first().remove();
    if (completo) {
        $('#btnGuardar').attr('disabled', true);
    } else {
        AgregarRenglonTolva();
    }

    DesbloquearPantalla();
    AsignarValoresPiePagina(msg.d);
    BloqueaDesbloqueaControlesIniciales(true);

    if (msg.d.ListaTiempoMuerto != null && msg.d.ListaTiempoMuerto.length > 0) {
        listaHorasMuertas = msg.d.ListaTiempoMuerto;
    }
    else {
        listaHorasMuertas = new Array();
    }
    bloquearAyuda = true;
};

AgregarRenglonTolva = function () {
    var renglon = '<tr>' +
        '<td>' +
        '<input class="numeroTolva textBoxTablas span12" oninput="maxLengthCheck(this)" maxlength="10" type="text" />' +
        '</td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td></td> ' +
        '</tr>';
    $('#GridCheckListReparto tbody').append(renglon);

    $('.numeroTolva').alpha({
        allow: '0123456789-',
        disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`¿¡¨¨~``^^°´´áéíóúýÁÉÍÓÚÝñÑabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    });
};

//Función para asignarle los eventos a los controles del Modal de Horas Muertas
EventosModalHorasMuertas = function () {
    $('#btnHoraMuertas').click(function () {
        $('#GridCausasRegistradas tbody').html('');
        CargarGridHorasMuertas();
        $('#modalHorasMuertas').modal('show');
        //$("html").css("margin-right", "-15px");
    });

    $('#btnAgregarMuertas').click(function () {
        AgregarHorasMuertas();
    });

    $('.cerrarHorasMuertas').click(function () {
        $('#modalHorasMuertas').modal('hide');
    });

    $('#btnCerrarHorasMuertas').click(function () {
        $('#modalHorasMuertas').modal('hide');
    });

    $('#modalHorasMuertas').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });

    $('#modalHorasMuertas').on('shown.bs.modal', function () {
        LimpiarHorasMuertas();
        CargarCausasTiempoMuerto();
        $('#txtHoraInicial').focus();
        $("html").css("margin-right", "-15px");
    });

    $('#btnLimpiarMuertas').click(function () {
        LimpiarHorasMuertas();
    });

    $('#txtHoraInicial').keydown(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            if ($('#txtHoraInicial').val() != '') {
                $('#txtHoraFinal').focus();
            }
        }
    });

    $('#txtHoraFinal').keydown(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            $('#ddlCausa').focus();
        }
    });

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


    if ((inicialHoras > 24 || inicialMinutos >= 60) || (inicialHoras == 24 && inicialMinutos > 0) ||
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

        if ((finalHoras > 24 || finalMinutos >= 60) || (finalHoras == 24 && finalMinutos > 0) ||
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
        if (fechaInicial >= fechaFinal) {
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
        if (fechaInicial >= fechaFinal) {
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
        var causaTiempoMuerto = {};
        horasMuertas.Horas = diferenciaFinal;
        horasMuertas.HoraInicio = horaInicial;
        horasMuertas.HoraFin = horaFinal;

        causaTiempoMuerto.CausaTiempoMuertoID = $('#ddlCausa option:selected').val();
        causaTiempoMuerto.Descripcion = $('#ddlCausa option:selected').text();
        horasMuertas.CausaTiempoMuerto = causaTiempoMuerto;
        listaHorasMuertas.push(horasMuertas);

        var causa = $('#ddlCausa option:selected').text();
        AgregarRenglonGridHorasMuertas(diferenciaFinal, causa);
        LimpiarHorasMuertas();
    }
};

CargarGridHorasMuertas = function () {
    $(listaHorasMuertas).each(function () {
        var horaInicial = this.HoraInicio;
        var horaFinal = this.HoraFin;
        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);

        var diferenciaHoras = DiferenciaHorasFechas(fechaInicial, fechaFinal);
        var horas = Math.floor(diferenciaHoras);
        var minutos = diferenciaHoras % 1;
        var minutosFormateados = Math.round(minutos * 60);
        if (minutosFormateados < 10) {
            minutosFormateados = '0' + minutosFormateados;
        }
        var diferenciaFinal = horas + ':' + minutosFormateados;

        AgregarRenglonGridHorasMuertas(diferenciaFinal, this.CausaTiempoMuerto.Descripcion);
    });

};

//Función para limpiar los controles del modal Horas Muertas
LimpiarHorasMuertas = function () {
    $('#txtHoraInicial').val('');
    $('#txtHoraFinal').val('');
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
    var comboCausa = $('#ddlCausa');
    comboCausa.setTemplateURL('../Templates/ComboGenerico.htm');
    comboCausa.processTemplate(valores);
    $('#txtHoraInicio').focus();
};
//Función para agregar el Renglon al Grid de Horas Muertas
AgregarRenglonGridHorasMuertas = function (horas, causa) {
    var renglon = '<tr>';
    renglon += '<td class="horasMuertas">' + horas + ' </td>';
    renglon += '<td>' + causa + ' </td>';
    renglon += '</tr>';
    $('#GridCausasRegistradas tbody').append(renglon);
};

CargarGridRepartosDefault = function () {
    var datos = {};
    CargarRecursosGridRepartos();
    datos.Recursos = recursos;
    datos.Repartos = {};
    var divContenedor = $('#divGridRepartos');
    $('#GridCheckListReparto').html('');
    divContenedor.setTemplateURL('../Templates/GridCheckListReparto.htm');
    divContenedor.processTemplate(datos);

    $('.numeroTolva').alpha({
        allow: '0123456789-',
        disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`¿¡¨¨~``^^°´´áéíóúýÁÉÍÓÚÝñÑabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    });
};

//Función para cargar los recursos  del grid busqueda de CamionRepartos
CargarRecursosGridRepartos = function () {
    recursos = {};
    recursos.NumeroTolva = window.ColumnaNumeroTolva;
    recursos.Servicio = window.ColumnaServicio;
    recursos.Reparto = window.ColumnaReparto;
    recursos.Racion = window.ColumnaRacion;
    recursos.KilosEmbarcados = window.ColumnaKilosEmbarcados;
    recursos.KilosRepartidos = window.ColumnaKilosRepartidos;
    recursos.Sobrante = window.ColumnaSobrante;
    recursos.CorralInicio = window.ColumnaCorralInicio;
    recursos.CorralFinal = window.ColumnaCorralFin;
    recursos.HoraInicioReparto = window.ColumnaHoraInicio;
    recursos.HoraFinalReparto = window.ColumnaHoraFin;
    recursos.TiempoTotalViaje = window.ColumnaTotalViaje;

    recursos.PiePaginaTotal = window.PiePaginaTotal;
    recursos.PiePaginaMerma = window.PiePaginaMerma;
};

//Función para cargar el combo ddlCausa
CargarTiposServicio = function () {
    BloquearPantalla();
    urlMetodos = rutaPantalla + '/ObtenerTiposServicio';
    datosMetodos = {};
    mensajeErrorMetodos = window.ErrorTipoServicio;
    EjecutarWebMethod(urlMetodos, datosMetodos, CargarTiposServicioSuccess, mensajeErrorMetodos);
};

//Función Success para cargar el combo ddlCausa
CargarTiposServicioSuccess = function (msg) {
    var valores = {};
    var recursos = {};
    recursos.Seleccione = window.Seleccione;
    valores.Recursos = recursos;
    var listaValores = new Array();
    $(msg.d).each(function () {
        var valor = {};
        valor.Clave = this.TipoServicioId;
        valor.Descripcion = this.Descripcion;
        listaValores.push(valor);
    });
    valores.Valores = listaValores;
    var comboTurno = $('#ddlServicio');
    comboTurno.setTemplateURL('../Templates/ComboGenerico.htm');
    comboTurno.processTemplate(valores);
    DesbloquearPantalla();
};

ValidarAntesNumeroTolva = function () {
    if (!ValidarTipoServicioValor()) {
        return false;
    }
    if (!ValidarFechaReparto()) {
        return false;
    }
    if (!ValidarCamionReparto()) {
        return false;
    }
    if (!ValidarHorometroInicial()) {
        return false;
    }
    if (!ValidarOdometroInicial()) {
        return false;
    }
    return true;
};

//Función para cargar el combo ddlCausa
GenerarRepartos = function (textBoxTolva) {
    if (ValidarAntesNumeroTolva()) {
        BloquearPantalla();
        urlMetodos = rutaPantalla + '/GenerarRepartos';
        var filtroCheckListReparto = {};
        filtroCheckListReparto.NumeroEconomico = $('#txtCamionReparto').val();
        filtroCheckListReparto.TipoServicioID = $('#ddlServicio option:selected').val();
        filtroCheckListReparto.Fecha = ToDate($('#txtFecha').val());
        filtroCheckListReparto.NumeroTolva = textBoxTolva.val();
        filtroCheckListReparto.CamionRepartoID = camionRepartoID;
        datosMetodos = { 'filtro': filtroCheckListReparto };
        mensajeErrorMetodos = window.ErrorGenerarRepartos;
        EjecutarWebMethod(urlMetodos, datosMetodos, GenerarRepartosSuccess, mensajeErrorMetodos);
    } else {
        textBoxTolva.val('');
    }
};

//Función Success para cargar el combo ddlCausa
GenerarRepartosSuccess = function (msg) {
    if (msg.d.Mensaje != null && msg.d.Mensaje.length > 0) {
        MostrarMensaje(msg.d.Mensaje, function () {
            $('.numeroTolva').val('');
            $('.numeroTolva').focus();
        });
        DesbloquearPantalla();
        return;
    }
    var datos = {};
    CargarRecursosGridRepartos();
    datos.Recursos = recursos;
    datos.Repartos = msg.d.DatosDataLink;
    var divContenedor = $('#divGridRepartos');

    divContenedor.setTemplateURL('../Templates/GridCheckListReparto.htm');
    divContenedor.processTemplate(datos);
    $('#GridCheckListReparto tbody tr').first().remove();
    AsignarValoresPiePagina(msg.d);
    $('.numeroTolvaGuardar').alpha({
        allow: '0123456789-',
        disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`¿¡¨¨~``^^°´´áéíóúýÁÉÍÓÚÝñÑabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    });
    DesbloquearPantalla();
};

AsignarValoresPiePagina = function (filtroGenerarArchivoDataLink) {
    var renglonPie = $('#GridCheckListReparto tfoot tr').first();
    renglonPie.find('.pieTotalKilosEmbarcados').text(filtroGenerarArchivoDataLink.TotalKilosEmbarcados);
    renglonPie.find('.pieTotalKilosRepartidos').text(filtroGenerarArchivoDataLink.TotalKilosRepartidos);
    renglonPie.find('.pieTotalSobrante').text(filtroGenerarArchivoDataLink.TotalSobrante);
    renglonPie.find('.pieMerma').text(filtroGenerarArchivoDataLink.MermaReparto);
    renglonPie.find('.pieTotalTiempo').text(filtroGenerarArchivoDataLink.TotalTiempoViaje);
};

//Función para Guardar el Reparto
Guardar = function () {
    ObtenerDatosReparto();
    ObtenerDatosRepartoDetalle();
    var sinTolva = false;
    $(repartoAlimentoDetalle).each(function () {
        if (this.NumeroTolva == '' && this.Servicio != 0) {
            MostrarMensaje('Debe capturar todos los números de tolva. Favor de verificar.', null);
            sinTolva = true;
            return false;
        }
        return true;
    });
    if (sinTolva) {
        DesbloquearPantalla();
        return;
    }

    urlMetodos = rutaPantalla + '/GuardarReparto';
    datosMetodos = { 'reparto': repartoAlimento, 'repartoDetalle': repartoAlimentoDetalle };
    mensajeErrorMetodos = 'Ocurrio un error al guardar la información';
    EjecutarWebMethod(urlMetodos, datosMetodos, GuardarSuccess, mensajeErrorMetodos);
};

ObtenerDatosReparto = function () {
    repartoAlimento = {};
    repartoAlimento.RepartoAlimentoID = repartoAlimentoID;
    repartoAlimento.TipoServicioID = TryParseInt($('#ddlServicio option:selected').val(), 0);
    repartoAlimento.FechaReparto = ToDate($('#txtFecha').val());
    repartoAlimento.CamionRepartoID = camionRepartoID;
    repartoAlimento.HorometroInicial = TryParseInt($('#txtHorometroInicial').val(), 0);
    repartoAlimento.HorometroFinal = TryParseInt($('#txtHorometroFinal').val(), 0);
    repartoAlimento.OdometroInicial = TryParseInt($('#txtOdometroInicial').val(), 0);
    repartoAlimento.OdometroFinal = TryParseInt($('#txtOdometroFinal').val(), 0);
    repartoAlimento.LitrosDiesel = TryParseInt($('#txtLitrosDiesel').val(), 0);
    ObtenerHorasMuertas();
};

ObtenerHorasMuertas = function () {
    var listaTiempoMuertoGuardar = new Array();
    $(listaHorasMuertas).each(function () {
        var tiempoMuerto = {};
        tiempoMuerto.TiempoMuertoID = this.TiempoMuertoID;
        tiempoMuerto.HoraInicio = this.HoraInicio;
        tiempoMuerto.HoraFin = this.HoraFin;
        tiempoMuerto.CausaTiempoMuertoID = this.CausaTiempoMuerto.CausaTiempoMuertoID;

        listaTiempoMuertoGuardar.push(tiempoMuerto);
    });
    repartoAlimento.ListaTiempoMuerto = listaTiempoMuertoGuardar;
};

ObtenerDatosRepartoDetalle = function () {
    repartoAlimentoDetalle = new Array();
    $('#GridCheckListReparto tbody tr').each(function () {
        var detalle = {};
        var renglon = $(this);
        detalle.NumeroTolva = renglon.find('.numeroTolvaGuardar').val();
        detalle.Servicio = TryParseInt(renglon.find('.columnaServicio').text(), 0);
        detalle.Reparto = TryParseInt(renglon.find('.columnaReparto').text(), 0);
        detalle.RacionFormula = TryParseInt(renglon.find('.columnaRacionFormula').text(), 0);
        detalle.KilosEmbarcados = TryParseInt(renglon.find('.columnaKilosEmbarcados').text(), 0);
        detalle.KilosRepartidos = TryParseInt(renglon.find('.columnaKilosRepartidos').text(), 0);
        detalle.Sobrante = TryParseInt(renglon.find('.columnaSobrante').text(), 0);
        detalle.PesoFinal = TryParseInt(renglon.attr('data-PesoFinal'), 0);
        detalle.CorralInicio = renglon.find('.columnaCorralInicio').text();
        detalle.CorralFinal = renglon.find('.columnaCorralFinal').text();
        detalle.HoraInicioReparto = renglon.find('.columnaHoraInicio').text();
        detalle.HoraFinalReparto = renglon.find('.columnaHoraFinal').text();
        detalle.Observaciones = $('#txtObservacion').val();

        if ((detalle.NumeroTolva != undefined || detalle.NumeroTolva != '') && detalle.Servicio != 0) {
            repartoAlimentoDetalle.push(detalle);
        }


    });
};


//Función Success del Guardar
GuardarSuccess = function () {
    DesbloquearPantalla();
    MostrarMensaje(window.MensajeGuardadoExitoso, function () {
        LimpiarPantallaCompleta();
    });
};

ValidarTipoServicioValor = function () {
    var valido = true;
    if ($('#ddlServicio option:selected').val() == '0') {
        MostrarMensaje(window.MensajeSeleccionarServicio, function () {
            $('#ddlServicio').focus();
        });
        valido = false;

    }
    return valido;
};

ValidarFechaReparto = function () {
    var valido = true;
    if ($('#txtFecha').val() == '') {
        MostrarMensaje(window.MensajeSeleccionarFecha, function () {
            $('#txtFecha').focus();
        });
        valido = false;

    }
    return valido;
};

ValidarHorometroInicial = function () {
    var valido = true;
    if ($('#txtHorometroInicial').val() == '' || TryParseInt($('#txtHorometroInicial').val(), 0) == 0) {
        MostrarMensaje(window.MensajeSeleccionarHorometro, function () {
            $('#txtHorometroInicial').val('');
            $('#txtHorometroInicial').focus();
        });
        valido = false;
    }
    return valido;
};

ValidarOdometroInicial = function () {
    var valido = true;
    if ($('#txtOdometroInicial').val() == '' || TryParseInt($('#txtOdometroInicial').val(), 0) == 0) {
        MostrarMensaje(window.MensajeSeleccionarOdometro, function () {
            $('#txtOdometroInicial').val('');
            $('#txtOdometroInicial').focus();
        });
        valido = false;
    }
    return valido;
};

ObtenerFechaServidor = function () {
    urlMetodos = rutaPantalla + '/ObtenerFechaServidor';
    datosMetodos = {};
    mensajeErrorMetodos = 'Ocurrio un error al obtener la fecha del servidor';
    EjecutarWebMethod(urlMetodos, datosMetodos, ObtenerFechaServidorSuccess, mensajeErrorMetodos);
};

ObtenerFechaServidorSuccess = function (msg) {
    var fecha = new Date(parseInt(msg.d.replace(/^\D+/g, '')));
    $('#txtFecha').val(FechaFormateada(fecha));
};

ValidarAntesGuardar = function () {
    var valido = true;
    if (!ValidarAntesNumeroTolva()) {
        return false;
    }
    if ($('#txtHorometroFinal').val() != '') {
        var horometroInicial = TryParseInt($('#txtHorometroInicial').val(), 0);
        var horometroFinal = TryParseInt($('#txtHorometroFinal').val(), 0);
        if (horometroFinal <= horometroInicial) {
            MostrarMensaje(window.MensajeHorometroFinalMenor, function () {
                $('#txtHorometroFinal').val('');
                $('#txtHorometroFinal').focus();
            });
            return false;
        }

    }
    if ($('#txtOdometroFinal').val() != '') {
        var odometroInicial = TryParseInt($('#txtOdometroInicial').val(), 0);
        var odometroFinal = TryParseInt($('#txtOdometroFinal').val(), 0);
        if (odometroFinal <= odometroInicial) {
            MostrarMensaje(window.MensajeOdometroFinalMenor, function () {
                $('#txtOdometroFinal').val('');
                $('#txtOdometroFinal').focus();
            });
            return false;
        }

    }
    if ($('#GridCheckListReparto tbody tr').length <= 1) {
        var primerRenglon = $('#GridCheckListReparto tbody tr').first();
        if (primerRenglon.find('.numeroTolva').length == 1) {
            MostrarMensaje(window.MensajeSinGuardar, null);
            return false;
        }
    }

    return valido;
};

ValidarCamionReparto = function () {
    var valido = true;
    if ($('#txtCamionReparto').val() == '') {
        MostrarMensaje(window.MensajeSeleccionarCamion, function () {
            $('#txtCamionReparto').focus();
        });
        valido = false;
    }
    return valido;
};

MostrarMensajeCancelar = function () {
    bootbox.dialog({
        message: window.MensajeCancelar,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    LimpiarPantallaCompleta();
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

LimpiarPantallaCompleta = function () {
    $('input[type=text]').not('#txtOperador, #txtFecha').each(function () {
        $(this).val('');
        $(this).attr('disabled', false);
    });
    $('input[type=number]').each(function () {
        $(this).val('');
        $(this).attr('disabled', false);
    });
    //$('input[type=date]').each(function () {
    //    $(this).val('');
    //    $(this).attr('disabled', false);
    //});
    $('select').each(function () {
        $(this).val('0');
        $(this).attr('disabled', false);
    });
    CargarGridRepartosDefault();
    $('#txtOperador').val($('#hfOperador').val());
    $('#txtObservacion').val('');
    $('#txtObservacion').attr('disabled', false);
    ObtenerFechaServidor();
    $('#ddlServicio').focus();
    listaHorasMuertas = new Array();
    $('#btnGuardar').attr('disabled', false);
    bloquearAyuda = false;
    camionRepartoID = 0;
    repartoAlimentoID = 0;
};

LimpiarConsultarRepartos = function () {
    $('#txtHorometroInicial').val('');
    $('#txtHorometroFinal').val('');
    $('#txtLitrosDiesel').val('');
    $('#txtOdometroInicial').val('');
    $('#txtOdometroFinal').val('');
};

BloqueaDesbloqueaControlesIniciales = function (deshabilitado) {
    $('#txtHorometroInicial').attr('disabled', deshabilitado);

    $('#txtOdometroInicial').attr('disabled', deshabilitado);

    $('#ddlServicio').attr('disabled', deshabilitado);
    $('#txtFecha').attr('disabled', deshabilitado);
    $('#txtCamionReparto').attr('disabled', deshabilitado);
    $('#btnAyudaCamionReparto').attr('disabled', deshabilitado);
};