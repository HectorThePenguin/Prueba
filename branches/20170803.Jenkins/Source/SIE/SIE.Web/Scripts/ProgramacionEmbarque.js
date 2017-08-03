//Variables globales
var msjAbierto = 0;
var guardado = false;
var fechaInicial = new Date(Date.now());
var fechaCitaCarga = new Date(Date.now());
var embarqueInfo = { 'Ruteo': { RuteoID: 0 }, CitaCarga: 0 };
var esRuteo = false;
var ruteoDetalle = {};
var listadoProgramacionEmbarque = {};
var formatoHora = '';
var ultimaFecha = '';
var embarqueRuteoInfo = { EmbarqueID: 0, CitaCarga: 0 };
var tieneTarifa = false;
var contador = 0;
var ruteoIdSeleccionado = 0;
var idRuteos = [];
var transportistaSeleccionado = {};
var rutaSeleccionada = {};
var organizacionOrigen = 0;
var organizacionDestino = 0;
var transportistaPendiente = 0;
var idTransportista = 0;
var dobleTransportista = false;
var datosCapturados = false;
var fechaCitaCargaTransporte = new Date();
var fechaCitaDescargaTransporte = new Date();
var rolProgramacion = false;
var rolTransporteDatos = false;
var embarqueCorreoInfo = {};
var esEdicionDatos = false;
var ayudaDesplegada = 0;
var estatusValidado = false;
var embarqueID = 0;

$(document).ready(function () {
    Inicializar();
    AsignarEventosControles();
    InicicializarTransporte();
    InicializarDatos();
    AsignarEventosControlesTranasporte();
    AsignarEventosControlesDatos();
    LimpiarPantalla();
    llenarGridSemana(fechaInicial);
    DeshabilitarAyudas();
    ObtenerRol();
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    $("#txtFecha").datepicker({
        firstDay: 1,
        showOn: 'button',
        buttonImage: '../assets/img/calander.png',
        onSelect: function (date) {
            fechaInicial = $('#txtFecha').datepicker('getDate');
            $('#txtFecha').val(fechaInicial.getWeek());
            llenarGridSemana(fechaInicial);
        },

        dateFormat: 'dd-mm-yy',
        beforeShowDay: function(date) {
            var day = date.getDay();
            return [(day == 1 )];
        }

    });
    $.datepicker.setDefaults($.datepicker.regional['es']);
    fechaInicial = $('#txtFecha').datepicker('getDate');
    $("#txtFecha").focusout(function () {
        var fechaSeleccionada = $('#txtFecha').datepicker('getDate').toString("yyyy-mm-dd");
        if (fechaSeleccionada == null || Date.parse(fechaSeleccionada) == Date.parse(fechaInicial)) {
            $('#txtFecha').datepicker('setDate', fechaInicial).trigger('change');
        }
    });

    $("#txtFecha").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    $("#txtCitaCarga").datepicker({
        firstDay: 1,
        minDate: 0,
        maxDate: 7,
        showOn: 'button',
        disabled: true,
        buttonImage: '../assets/img/calander.png',
        onSelect: function (date) {
            fechaCitaCarga = $('#txtCitaCarga').datepicker('getDate');
            $('#txtCitaCarga').change();
        },
        dateFormat: 'dd-mm-yy'

    });
    $.datepicker.setDefaults($.datepicker.regional['es']);
    fechaCitaCarga = $('#txtCitaCarga').datepicker('getDate');
    $("#txtCitaCarga").focusout(function () {
        var fechaSeleccionada = $('#txtCitaCarga').datepicker('getDate').toString("yyyy-mm-dd");
        if (fechaSeleccionada == null || Date.parse(fechaSeleccionada) == Date.parse(fechaCitaCarga)) {
            $('#txtCitaCarga').datepicker('setDate', fechaCitaCarga).trigger('change');
        }
    });

    $("#txtCitaCarga").addClass("span4 margenIzquierdaCapturaDatos");

    $("#txtCitaCarga").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    $("#ddlTipoEmbarque").keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
            $("#txtNumOrigen").focus();
        }
    });

    $("#txtHoraCarga").keydown(function (event) {
        var code = event.keyCode || event.which;
        if (code == 13 || code == 9) {
            if (!esRuteo) {
                $("#txtHorasTransito").focus();
            } else {
                $("#txtObservacionCaptura").focus();
            }
        }
        return false;
    });

    $("#txtHoraCarga").on('change', function() {
        ObtenerFormatoHora();
        ConsultaDetallesRuteo();
    });

    $('#txtHoraCarga').timepicker();

});


Inicializar = function () {
    $('body').bind("cut copy paste", function (e) {
        e.preventDefault();
    });
    
    $("#txtNumOrganizacion").focus();
    $("#txtId").attr("disabled", true);
    $("#txtId").val(0);
    
    $("#txtNumOrganizacion").numericInput().attr("maxlength", "4");
    $("#txtOrganizacion").attr("disabled", true);

    $("#txtHoraCarga").val("8");
    
    $("#txtDescarga").attr("disabled", true);

    $("#txtNumOrigen").numericInput().attr("maxlength", "4");
    $("#divCapturaDatos *").attr("disabled", true);
    
    $("#txtNumDestino").numericInput().attr("maxlength", "4");
    $("#txtOrigen").attr("disabled", true);

    $("#txtHoraCarga").numericInput().attr("maxlength", "2");
    $("#txtDestino").attr("disabled", true);

    PreCondiciones();
    InicializarGridProgramacionEmbarque();
    InicializarGridTransporteEmbarque();
    InicializarGridDatosEmbarque();
    ObtenerTiposEmbarque();
}

/*Obtener rol del usuario*/
ObtenerRol = function () {

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerRol",
        contentType: "application/json; charset=utf-8",
        error: function () {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorPrecondiciones,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var resultado = data.d;
            if (resultado.Descripcion == window.rolEmbarqueProgramacion) {
                rolProgramacion = true;
            } else if (resultado.Descripcion == window.rolEmbarqueTransporteDatos) {
                rolTransporteDatos = true;
            }
        }
    });
};

//-----------------------------------------------------------------
// Funciones de la pestaña de Programación
//-----------------------------------------------------------------

/*Funcion para habilitar las ayudas de origen y destino*/
HabilitarAyudas = function () {
    if (rolProgramacion) {
        $("#divCapturaDatos *").attr("disabled", false);
        $("#txtCitaCarga").datepicker("option", "disabled", false);
        $("#txtCitaCarga").attr("disabled", true);
        $("#btnBusquedaOrigen").removeClass("btn-disabled");
        $("#btnBusquedaDestino").removeClass("btn-disabled");
    } else {
        $("#divCapturaDatos *").attr("disabled", true);
    }

    $("#txtId").attr("disabled", true);
    $("#txtOrigen").attr("disabled", true);
    
    $("#txtDestino").attr("disabled", true);
    
    $("#txtDescarga").attr("disabled", true);
    $("#txtObservaciones").attr("disabled", true);

    /* Habilitar ayuda */
    $("#btnBusquedaDestino").keypress(
    function (event) {
        if (event.which == '13') {
            if (rolProgramacion) {
                ObtenerOrganizacionesOrigen();
            }
        }
    });

    /* Habilitar ayuda */
    $("#btnBusquedaOrigen").keypress(
    function (event) {
        if (event.which == '13') {
            if (rolProgramacion) {
                ObtenerOrganizacionesOrigen();
            }
        }
    });

};

/*Funcion para deshabilitar las ayudas de origen y destino*/
DeshabilitarAyudas = function() {
    $("#divCapturaDatos *").attr("disabled", true);
    $("#txtCitaCarga").datepicker("option", "disabled", true);
    $("#btnBusquedaOrigen").addClass("btn-disabled");
    $("#btnBusquedaDestino").addClass("btn-disabled");

    /* Deshabilitar ayuda */
    $("#btnBusquedaDestino").keypress(
    function (event) {
        if (event.which == '13') {
            event.preventDefault();
        }
    });

    /* Deshabilitar ayuda */
    $("#btnBusquedaOrigen").keypress(
    function (event) {
        if (event.which == '13') {
            event.preventDefault();
        }
    });
};

AsignarEventosControles = function () {
    AsignarEventosModalBusqueda();

    // Deshabilitar opcion para arrastrar y soltar texto
    $('input').on('dragstart', function (e) {
        e.preventDefault();
    });

    $('input').on('drop', function (e) {
        e.preventDefault();
    });

    $('textarea').on('dragstart', function (e) {
        e.preventDefault();
    });

    $('textarea').on('drop', function (e) {
        e.preventDefault();
    });

    //// Al capturar la organizacion en la ventana principal
    $('#txtNumOrganizacion').bind("keydown", function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($("#txtNumOrganizacion").val() != "") {
                e.preventDefault();
                $('div.dataTables_filter input').focus();
            }
        }   
    });

    // Al capturar la organizacion origen al seleccionar tap
    $('#txtNumOrigen').bind("keydown", function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumOrigen').val() != "") {
                e.preventDefault();
                //ObtenerOrganizacionOrigen();
                $("#txtNumDestino").focus();
                //Sugerencias para los campos Responsable y Horas
                if ($('#txtNumOrigen').val().trim()) {
                    for (i = listadoProgramacionEmbarque.length - 1; i >= 0; i--) {
                        if (listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionOrigen.OrganizacionID == parseInt($("#txtNumOrigen").val()) &&
                            listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionDestino.OrganizacionID == parseInt($("#txtNumDestino").val())) {
                            $("#txtResponsableEmbarque").val(listadoProgramacionEmbarque[i].ResponsableEmbarque);
                            $("#txtHorasTransito").val(listadoProgramacionEmbarque[i].HorasTransito);

                        }
                    }
                }
                
            }
        }
    });

    // Al capturar la organizacion Destino al seleccionar tap
    $('#txtNumDestino').bind("keydown", function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumOrigen').val() != "") {
                e.preventDefault();
                //ObtenerOrganizacionDestino();
                $("#txtResponsableEmbarque").focus();
                //Sugerencias para los campos Responsable y Horas
                if ($('#txtNumDestino').val().trim()) {
                    for (i = listadoProgramacionEmbarque.length - 1; i >= 0; i--) {
                        if (listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionOrigen.OrganizacionID == parseInt($("#txtNumOrigen").val()) &&
                            listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionDestino.OrganizacionID == parseInt($("#txtNumDestino").val())) {
                            $("#txtResponsableEmbarque").val(listadoProgramacionEmbarque[i].ResponsableEmbarque);
                            $("#txtHorasTransito").val(listadoProgramacionEmbarque[i].HorasTransito);

                        }
                    }
                }
            }
        }
    });

    // Actualizar cita descarga al cambiar hora carga
    $("#txtHoraCarga").change(function (e) {
        e.preventDefault();
        ActualizarCitaDescarga();
    });

    // Actualizar cita descarga al fecha de cita carga
    $("#txtCitaCarga").change(function (e) {
        e.preventDefault();
        ActualizarCitaDescarga();
    });

    // Al perder el foco el textbox del indentificador de la organizacion
    $("#txtNumOrganizacion").focusout(function (e) {
        if ($("#txtNumOrganizacion").val() == "") {
            $("#txtOrganizacion").val("");
            DeshabilitarAyudas();
            InicializarGridProgramacionEmbarque();
            InicializarGridTransporteEmbarque();
            InicializarGridDatosEmbarque();
            $('#lblTotal').text("0 de 0");
            $('#lblTotalDatos').text("0 de 0");
        }
        else {
            ObtenerOrganizacion();
            HabilitarAyudas();
            if (rolProgramacion) {
                $("#ddlTipoEmbarque").focus();
            } 
        }
    });

    // Al perder el foco el textbox del indentificador de la organizacion Origen
    $("#txtNumOrigen").focusout(function () {
        if ($('#txtNumOrigen').val() == "") {
            $("#txtOrigen").val("");
        }
        else {
            ObtenerOrganizacionOrigen();
            if ($('#txtNumOrigen').val().trim()) {
                for (i = listadoProgramacionEmbarque.length - 1; i >= 0; i--) {
                    if (listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionOrigen.OrganizacionID == parseInt($("#txtNumOrigen").val()) &&
                        listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionDestino.OrganizacionID == parseInt($("#txtNumDestino").val())) {
                        $("#txtResponsableEmbarque").val(listadoProgramacionEmbarque[i].ResponsableEmbarque);
                        $("#txtHorasTransito").val(listadoProgramacionEmbarque[i].HorasTransito);

                    }
                }
            }
        }
    });

    // Al perder el foco el textbox del indentificador de la organizacion Destino
    $("#txtNumDestino").focusout(function () {
        if ($('#txtNumDestino').val() == "") {
            $("#txtDestino").val("");
        }
        else {
            ObtenerOrganizacionDestino();
            if ($('#txtNumDestino').val().trim()) {
                for (i = listadoProgramacionEmbarque.length - 1; i >= 0; i--) {
                    if (listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionOrigen.OrganizacionID == parseInt($("#txtNumOrigen").val()) &&
                        listadoProgramacionEmbarque[i].ListaEscala[0].OrganizacionDestino.OrganizacionID == parseInt($("#txtNumDestino").val())) {
                        $("#txtResponsableEmbarque").val(listadoProgramacionEmbarque[i].ResponsableEmbarque);
                        $("#txtHorasTransito").val(listadoProgramacionEmbarque[i].HorasTransito);

                    }
                }
            }
        }
    });

    // Boton que abre la ayuda de organizaciones
    $("#btnAyudaOrganizacion").click(function () {
        ObtenerOrganizacionesTipoGanadera();
    });

    // Boton que abre la ayuda para las organizaciones Origen
    $("#btnBusquedaOrigen").click(function () {
        ObtenerOrganizacionesOrigen();
    });

    // Boton que abre la ayuda para las organizaciones Origen
    $("#btnBusquedaDestino").click(function () {
        ObtenerOrganizacionesDestino();
    });

    // Boton limpiar
    $("#btnLimpiar").click(function () {
        LimpiarPantalla();
    });

    // Boton guardar para pantalla de programación
    $("#btnGuardar").click(function () {
        if (ValidarCapturaDatosProgramacion()) {
            ValidaFleteOrigenDestino();
            if (tieneTarifa) {
                if (ValidarProgramacionDatosCapturados(1)) {
                    var embarqueInfo = CrearObjetoProgramacionEmbarque();
                    ValidarEstatus(embarqueInfo, 1);
                    if (estatusValidado) {
                        return;
                    }
                    Guardar(embarqueInfo, 1);
                }
            }
        }
    });

    //Opciones de la ayuda al cancelar
    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    });

    //Opciones de la ayuda al cancelar
    $("#btnSiBuscar").bind("keydown", function (e) {
        e.preventDefault();
        var code = e.keyCode || e.which;        
        if (code == 13) {
            $("#dlgCancelarBuscar").modal("hide");
            $("#txtOrganizacionBuscar").val("");
            ayudaDesplegada = 0;
        } else if (code == 9) {
            // cambiar focus entre botones
            $("#btnNoBuscar").focus();
        }
    });

    $("#btnNoBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("show");
        $("#txtOrganizacionBuscar").focus();
    });

    //Opciones de la ayuda al cancelar
    $("#btnNoBuscar").bind("keydown", function (e) {
        e.preventDefault();
        var code = e.keyCode || e.which;
        if (code == 13) {
            $("#dlgCancelarBuscar").modal("hide");
            $("#dlgBusquedaOrganizacion").modal("show");
            $("#txtOrganizacionBuscar").val("");
        } else if (code == 9) {
            // cambiar focus entre botones
            $("#btnSiBuscar").focus();
        }
    });

    // Boton cancelar en la pantalla principal
    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: window.msgCancelar,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    className : 'SuKarne',
                    callback: function () {
                        $("#txtNumOrganizacion").prop('disabled', false);
                        $("#btnAyudaOrganizacion").removeClass('btn-disabled');
                        $("#ddlTipoEmbarque").prop('disabled', false);
                        $("#txtNumOrigen").prop("disabled", false);
                        $("#txtNumDestino").prop("disabled", false);
                        $("#btnBusquedaOrigen").removeClass('btn-disabled');
                        $("#btnBusquedaDestino").removeClass('btn-disabled');
                        LimpiarPantalla();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        return true;
                    }
                }
            }
        });
    });

    // Enter dentro del campo del responsable de embarque
    $("#txtResponsableEmbarque").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    // Validar caracteres dentro del campo observacion
    $("#txtObservacionCaptura").keypress(function (e) {
        ValidarCaracteresAlfanumericos(e);
        var code = e.which || e.keyCode;
        if (code == 13 || code == 9) {
            $("#btnGuardar").focus();
        }
    });

    // Validar caracteres dentro del campo observacion
    $("#txtObservacionesTransporteCaptura").keypress(function (e) {
        ValidarCaracteresAlfanumericos(e);
        var code = e.which || e.keyCode;
        if (code == 9 || code == 13)
            $("#btnAgregarTransporte").focus();
    });

    // Validar caracteres dentro del campo observacion
    $("#txtObservacionesDatosCaptura").keypress(function (e) {
        ValidarCaracteresAlfanumericos(e);
        var code = e.which || e.keyCode;
        if (code == 9 || code == 13)
            $("#btnGuardarDatos").focus();
    });

    $('#GridContenido').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#dlgCancelarBuscar').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#tbBusqueda').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $("#btnLimpiar").click(function () {
        LimpiarPantalla();
    });

    $("#btnSeleccionaRuta").click(function () {
        SeleccionaRuta();
    });

    $("#btnCancelaRuta").click(function () {
        $("#dvSeleccionaRuta").modal('hide');
        esRuteo = false;
        embarqueInfo.Ruteo.RuteoID = 0;
        LimpiaCamposRuteo();
    });

    $("#ddlTipoEmbarque").change(function () {
        if ($("#txtId").val() == 0) {
            LimpiarAyudasProgramacionEmbarque();
        }
        if ($("#ddlTipoEmbarque").val() == 3) {
            esRuteo = true;
            CrearTablaRuteo();
            ValidaRuteosActivos();
        } else {
            esRuteo = false;
            CrearDivNormal();
        }
    });

    $('#txtCitaCarga').change('input',function (e) {
        ConsultaDetallesRuteo();
    });

    $("#txtOrigen").on("change input", function (e) {
        if ($("#txtDestino").val().trim() != '' && $("#txtDestino").val() !== undefined) {
            ObtenerRuteosPorOrigenyDestino();
        }
    });

    $("#txtDestino").on("change input", function (e) {
        if ($("#txtOrigen").val().trim() != '' && $("#txtOrigen").val() !== undefined) {
            ObtenerRuteosPorOrigenyDestino();
        }
    });

    $("#txtCitaCarga").on("change input", function (e) {

        //transportistaSeleccionado

        
    });

    // Al capturar el responsable de embarque en la ventana principal
    $('#txtResponsableEmbarque').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($("#txtResponsableEmbarque").val() != "") {
                e.preventDefault();
                $("#txtHoraCarga")[0].focus();
            }
        }
    });

    //Opciones del dialogo de cancelar
    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
    });

    //Opciones del dialogo de cancelar
    $("#btnNoBuscar").click(function () {
        $("#dlgBusquedaOrganizacion").modal("show");
        $("#txtOrganizacionBuscar").focus();
    });
    
    //Dar click en la pestaña de programación embarque
    $('a[href="#TapGridProgramacion"]').on('shown.bs.tab', function (e) {
        $("#divCapturaDatosEmbarqueTransporte").hide();
        $("#divCapturaDatosEmbarqueDatos").hide();
        $("#divCapturaDatos").show();
    });

    // Boton guardar
    $("#btnAgregarTransporte").click(function () {
        if (ValidaCamposTransporte()) {
            if (ValidarProgramacionDatosCapturados(2)) {
                var embarqueInfo = CrearObjetoTransporteEmbarque();
                ValidarEstatus(embarqueInfo, 2);
                if (estatusValidado) {
                    return;
                }
                // Guardado para pestaña de transporte
                Guardar(embarqueInfo, 2);
            }   
        }
    });

    $("#btnPlacasTracto").click(function() {
        ObtenerTractoPorProveedorID(idTransportista);
    });

    $("#txtPlacasTracto").focusout(function() {
        if ($("#txtPlacasTracto").val() != '')
            ObtenerTractoPorDescripcion(idTransportista, $("#txtPlacasTracto").val(), false);
    });

    // Inhabilita la opcion de arrastrar texto dentro del campo
    $("#txtPlacasTracto").droppable({
        disabled: true
    });

    $("#btnPlacasJaula").click(function() {
        ObtenerJaulaPorProveedorID(idTransportista);
    });

    $("#txtPlacasJaula").focusout(function () {
        if ($("#txtPlacasJaula").val() != '')
            ObtenerJaulaPorDescripcion(idTransportista, $("#txtPlacasJaula").val(), false);
    });

    // Inhabilita la opcion de arrastrar texto dentro del campo
    $("#btnPlacasJaula").droppable({
        disabled: true
    });

    $("#btnCancelarTransporte").click(function () {
        bootbox.dialog({
            message: window.msgCancelarTransporte,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        $('#tab a[data-target="#TapGridProgramacion"]').click();
                        $("#txtNumOrganizacion").attr("disabled", false);
                        $("#btnAyudaOrganizacion").removeClass("btn-disabled");
                        $("#btnAgregarTransporte").val("Agregar");
                        LimpiarPantallaTransporte();
                        DeshabilitarControlesTransporte();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        return true;
                    }
                }
            }
        });
    });

    $("#btnGuardarDatos").click(function () {
        if (ValidaCamposDatos()) {
            var embarqueInfo = CrearObjetoProgramacionDatos();
            ValidarEstatus(embarqueInfo, 3);
            if (estatusValidado) {
                return;
            }
            Guardar(embarqueInfo, 3);
        }
    });

};

/*Metodo para limpiar las ayudas de Origen y Destino*/
LimpiarAyudasProgramacionEmbarque = function() {
    $("#txtNumOrigen").val("");
    $("#txtOrigen").val("");
    $("#txtNumDestino").val("");
    $("#txtDestino").val("");
    $("#txtHorasTransito").val("");
    $("#txtResponsableEmbarque").val("");
};

//Cargar Eventos al modal de Busqueda
AsignarEventosModalBusqueda = function() {
    // Enter dentro de la ayuda de organizacion
    $("#txtOrganizacionBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
            switch (ayudaDesplegada) {
                case 1:
                    ObtenerOrganizacionesTipoGanadera();
                    break;
                case 2:
                    ObtenerOrganizacionesOrigen();
                    break;
                case 3:
                    ObtenerOrganizacionesDestino();
                    break;
                case 4:
                    ObtenerTransportistas();
                    break;
                case 5:
                    ObtenerRutas();
                    break;
                case 6:
                    ObtenerOperadores1();
                    break;
                case 7:
                    ObtenerOperadores2();
                    break;
                case 8:
                    ObtenerTractoPorDescripcion();
                    break;
                case 9:
                    ObtenerJaulaPorDescripcion(idTransportista, $("#txtPlacasJaula").val(), false);
                    break;
            }
        }
    });
};

//Validaciones y consultas
// Se hacen las validaciones para cumplir con las precondiciones necesarias
PreCondiciones = function () {
    var continuar = false;
    var mensaje = "";
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ValidarPreCondiciones",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorPrecondiciones,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var resultado = data.d;
            if (resultado < 1) {
                switch (resultado) {
                    case -1:
                        mensaje = window.msgSinTipoGanadera;
                        break;
                    default:
                        mensaje = window.msgErrorPrecondiciones;
                }

                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: mensaje, 
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
            else {
                continuar = true;
                $("#dlgFiltrosBusqueda").attr("disabled", false);
                ValidaRuteosActivos();
            }
        }
    });
    return continuar;
};

//Obtiene las organizaciones de tipo ganadera, Cadis y Descanso para la ayuda
ObtenerOrganizacionesTipoGanadera = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = { "organizacion": $("#txtOrganizacionBuscar").val() };
    } else {
        datos = { "organizacion": "" };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerOrganizacionesTipoGanaderaCadisDescanso",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //opciones de la ayuda de busqueda origen
                ayudaDesplegada = 1;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblOrganizacion + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacion' onClick='ObtenerOrganizacionesTipoGanadera();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].OrganizacionID + "' organizacion='" + resultado[i].OrganizacionID + "' descripcion='" + resultado[i].Descripcion + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].OrganizacionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].OrganizacionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

//Obtiene las organizaciones Origen
ObtenerOrganizacionesOrigen = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = { "organizacion": $("#txtOrganizacionBuscar").val() };
    } else {
        datos = { "organizacion": "" };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerOrganizacionOrigenPorDescripcion",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //opciones de la ayuda de busqueda origen
                ayudaDesplegada = 2;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblOrganizacion + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerOrganizacionesOrigen();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarBuscarOrigen();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Contenido de la busqueda
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].OrganizacionID + "' organizacion='" + resultado[i].OrganizacionID + "' descripcion='" + resultado[i].Descripcion + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].OrganizacionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].OrganizacionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

//Obtiene las organizaciones Destino
ObtenerOrganizacionesDestino = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = { "organizacion": $("#txtOrganizacionBuscar").val() };
    } else {
        datos = { "organizacion": "" };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerOrganizacionesTipoGanaderaCadisDescanso",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //opciones de la ayuda de busqueda destino
                ayudaDesplegada = 3;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblOrganizacion + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionDestino' onClick='ObtenerOrganizacionesDestino();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscarDestino' onClick='AyudaAgregarBuscarDestino();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Contenido de la busqueda
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].OrganizacionID + "' organizacion='" + resultado[i].OrganizacionID + "' descripcion='" + resultado[i].Descripcion + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].OrganizacionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].OrganizacionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgSinTipoGanadera,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

// Boton cancelar de la ventana ayuda
AyudaCancelarBuscar = (function () {
    $("#dlgCancelarBuscar").modal("show");
    $('#dlgCancelarBuscar').on('shown', function() {
        $("#btnSiBuscar").focus();
    });
    $("#dlgBusquedaOrganizacion").modal("hide");
});

//Opcion de la ventna de ayuda
OpcionSiBuscar = (function () {
    $("#dlgBusquedaOrganizacion").modal("hide");
    $("#txtOrganizacionBuscar").val("");
});

//Opcion de la ventna de ayuda
OpcionNoBuscar = (function () {
    $("#dlgBusquedaOrganizacion").modal("show");
    $("#txtOrganizacionBuscar").focus();
});

// Boton agregar de la ventana ayuda de organizacion principal
AyudaAgregarBuscar = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtNumOrganizacion").val($(this).attr("organizacion"));
            $("#txtOrganizacion").val($(this).attr("descripcion"));
        });
        HabilitarAyudas();
        llenarGridSemana(fechaInicial);
        ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
        ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val(), 0, false);
        ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val(), 0, false);
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionarOrganizacion,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

// Boton agregar de la ventana ayuda de organizacion origen
AyudaAgregarBuscarOrigen = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtNumOrigen").val($(this).attr("organizacion"));
            $("#txtOrigen").val($(this).attr("descripcion"));
            $("#txtNumDestino").focus();
        });
        ValidarOrigenyDestino();
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionarOrganizacion,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

// Boton agregar de la ventana ayuda de organizacion origen
AyudaAgregarBuscarDestino = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtNumDestino").val($(this).attr("organizacion"));
            $("#txtDestino").val($(this).attr("descripcion"));
            $("#txtResponsableEmbarque").focus();
        });
        ValidarOrigenyDestino();
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionarOrganizacion,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

//Funcion para limpiar los campos obligatorios
LimpiarPantalla = function () {
    $("#txtId").val(0);
    $("#txtOrigen").val("");
    $("#txtNumOrigen").val("");
    $("#txtDestino").val("");
    $("#txtNumDestino").val("");
    $("#ddlTipoEmbarque").val(0);
    $("#txtResponsableEmbarque").val("");
    $("#txtHoraCarga").val("8:00am");
    $("#txtCitaCarga").val("");
    $("#txtDescarga").val("");
    $("#txtHorasTransito").val("");
    $("#txtObservaciones").val("");
    $("#txtObservacionCaptura").val("");
    $("#btnGuardar").val("Agregar");
    CrearDivNormal();
}

//Selecciona sólo un checkbox
SeleccionaUno = function (Id) {
    var listaCheckBox = $(".organizaciones");
    var checkbox = $(Id);
    if (checkbox.is(":checked")) {
        listaCheckBox.each(function () {
            this.checked = false;
        });
        checkbox.attr("checked", true);
    }
};

//Obtiene el numero de semana seleccionada en el datapiker
Date.prototype.getWeek = function () {

    var weeknum = 15;
    var mes = (this.getMonth() + 1);
    var mesString = "0" + mes;
    if (mes > 9) {
        mesString = "" + mes;
    }
    var FechaCalcular = this.getFullYear() + "-" + mesString + "-" + this.getDate();
    var datos = { "fechaCalcular": FechaCalcular };
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerNumeroSemana",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify(datos),
        error: function (request) {
            var mensaje = msgErrorConsultar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: mensaje,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            weeknum = parseInt(data.d);

        }
    });

    return weeknum;
};

//Nombre de Titulos para el grid de Programación Embarque
RecursosBindTitulos = function () {
    var recursos = {};
    recursos.cabeceroId = window.cabeceroId;
    recursos.cabeceroFolioEmbarque = window.cabeceroFolioEmbarque;
    recursos.cabeceroOrigen = window.cabeceroOrigen;
    recursos.cabeceroDestino = window.cabeceroDestino;
    recursos.cabeceroResponsableEmbarque = window.cabeceroResponsableEmbarque;
    recursos.cabeceroTipo = window.cabeceroTipo;
    recursos.cabeceroCitaCarga = window.cabeceroCitaCarga;
    recursos.cabeceroObservaciones = window.cabeceroObservaciones;
    recursos.cabeceroOpcion = window.cabeceroOpcion;
    return recursos;
};

//Obtenemos el listado de progrmación Embarque y lo dibujamos en el grid
ObtenerProgramacionEmbarque = function (organizacionId) {

    var datos = { organizacionId: organizacionId }

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerProgramacionPorOrganizacionId',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        async:false,
        dataType: "json",
        success: function (msg) {

            listadoProgramacionEmbarque = msg.d;

            if (listadoProgramacionEmbarque != null) {
                    var recursos = RecursosBindTitulos();
                    var listadoProgramacionEmbarqueFinal = {};
                    listadoProgramacionEmbarqueFinal.ListadoProgramacionEmbarque = listadoProgramacionEmbarque;
                    listadoProgramacionEmbarqueFinal.Recursos = recursos;
                    listadoProgramacionEmbarqueFinal.rolProgramacion = rolProgramacion;

                    $('#GridProgramacionEmbarque').html('');
                    $('#GridProgramacionEmbarque').setTemplateURL('../Templates/GridProgramacionEmbarque.htm');
                    $('#GridProgramacionEmbarque').processTemplate(listadoProgramacionEmbarqueFinal);

                    $('#ProgramacionEmbarque').dataTable({
                        "oLanguage": {
                            "oPaginate": {
                                "sFirst": window.primeraPagina,
                                "sLast": window.ultimaPagina,
                                "sNext": window.siguiente,
                                "sPrevious": window.anterior
                            },
                            "sEmptyTable": window.sinDatos,
                            "sInfo": window.mostrando,
                            "sInfoEmpty": window.sinInformacion,
                            "sInfoFiltered": window.filtrando,
                            "sLengthMenu": window.mostrar,
                            "sLoadingRecords": window.cargando,
                            "sProcessing": window.procesando,
                            "sSearch": window.buscar,
                            "sZeroRecords": window.sinRegistros
                        }
                    });
                }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorProgramaciones,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

//Cargar los campos seleccionados del grid de programacion
CargarCamposActualizarProgramacionEmbarque = function (embarqueId) {
    $("#txtObservacionCaptura").val("");
    $("#btnGuardar").val("Actualizar");
    $("#ddlTipoEmbarque").prop('disabled', true);
    $("#txtNumOrganizacion").prop('disabled', true);
    $("#btnAyudaOrganizacion").addClass('btn-disabled');
    $("#txtNumOrigen").prop('disabled', true);
    $("#txtNumDestino").prop('disabled', true);
    $("#btnBusquedaOrigen").addClass('btn-disabled');
    $("#btnBusquedaDestino").addClass('btn-disabled');
    listadoProgramacionEmbarque.forEach(function (registro) {
        if (registro.EmbarqueID == embarqueId) {
            $("#txtCitaCarga").val(registro.FechaCitaCargaString.substr(0, 10).replace(/\//g, "-"));
            ObtenerFormatoHoraCarga(registro.CitaCarga);
            if (registro.TipoEmbarque.Descripcion === window.tipoRuteo) {
                esRuteo = true;
                CrearTablaRuteo();
                ObtenerDetallesEmbarqueRuteo(registro.EmbarqueID);
            }
            else {
                CrearDivNormal();
            }
            $("#txtId").val(registro.EmbarqueID);
            /*se cargar el embarque a variable global*/
            embarqueID = registro.EmbarqueID;
            $("#ddlTipoEmbarque").val(registro.TipoEmbarque.TipoEmbarqueID);
            $("#txtNumOrigen").val(registro.ListaEscala[0].OrganizacionOrigen.OrganizacionID);
            $("#txtOrigen").val(registro.ListaEscala[0].OrganizacionOrigen.Descripcion);
            $("#txtNumDestino").val(registro.ListaEscala[0].OrganizacionDestino.OrganizacionID);
            $("#txtDestino").val(registro.ListaEscala[0].OrganizacionDestino.Descripcion);
            $("#txtResponsableEmbarque").val(registro.ResponsableEmbarque);
            $("#txtObservaciones").val(registro.Observacion.Descripcion);
            $("#txtCitaCarga").val(registro.FechaCitaCargaString.substr(0, 10).replace(/\//g, "-"));
            $("#txtHorasTransito").val(registro.HorasTransito);
            $("#txtDescarga").val(registro.FechaCitaDescargaString);
            datosCapturados = registro.DatosCapturados;

            if (registro.TipoEmbarque.Descripcion == window.tipoRuteo) {
                CuentaConFolioEmbarque(registro);
            }

            return;
        }
    });
};

//Validacion si cuenta con un folio embarque
CuentaConFolioEmbarque = function (registro) {

    var datos = { embarqueId: registro.EmbarqueID}

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/CuentaConFolioEmbarque',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var dato = msg.d;
            if (dato != null) {
                if (dato.FolioEmbarque == 0 || dato.DescripcionEstatus != window.descripcionEstatusRecibido || dato.DescripcionEstatus != window.descripcionEstatusCancelado) {
                    $("#txtNumOrigen").attr("disabled", true);
                    $("#txtNumDestino").attr("disabled", true);
                    $("#btnBusquedaOrigen").addClass("btn-disabled");
                    $("#btnBusquedaDestino").addClass("btn-disabled");   
                }
            }

        },
        error: function () {
            
        }
    });
};

// Inactivar un registro de Programacion Embarque
Eliminar = function (embarqueId,seccion) {
    var datos = { 'embarqueInfo': { EmbarqueID: embarqueId, UsuarioModificacionID: 0 }  };

    bootbox.dialog({
        message: window.msgPeticionEliminar,
        buttons: {
            Aceptar: {
                label: window.msgDialogoSi,
                className : 'SuKarne',
                callback: function () {
                    ValidarEstatus(datos, 2);
                    if (!estatusValidado) {
                        $.ajax({
                        type: "POST",
                        url: 'ProgramacionEmbarque.aspx/Eliminar',
                        data: JSON.stringify(datos),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (seccion == 1) {
                                ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
                                LimpiarPantalla();
                            }
                            else if (seccion == 2) {
                                ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val());
                                ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val());
                            }
                            else if (seccion == 3) {
                                ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val());
                                ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val());
                            }                        
                            llenarGridSemana(fechaInicial);
                        },
                        error: function (error) {
                            bootbox.alert({
                                    message: window.msgErrorEliminar,
                                    closeButton: false,
                                    buttons: {
                                    ok: {
                                        label: window.Aceptar,
                                        className: 'SuKarne btn-primary'
                                        }
                                    }
                                });
                            }
                        });
                    }                 
                    return true;
                }
            },
            Cancelar: {
                label: window.msgDialogoNo,
                className: 'SuKarne btn-primary',
                callback: function () {
                    return true;
                }
            }
        }
    });

};

//Inicializar el listado de progrmación Embarque
InicializarGridProgramacionEmbarque = function() {
    var recursos = RecursosBindTitulos();
    var listadoProgramacionEmbarqueFinal = {};
    listadoProgramacionEmbarqueFinal.Recursos = recursos;

    $('#GridProgramacionEmbarque').html('');
    $('#GridProgramacionEmbarque').setTemplateURL('../Templates/GridProgramacionEmbarque.htm');
    $('#GridProgramacionEmbarque').processTemplate(listadoProgramacionEmbarqueFinal);

    $('#ProgramacionEmbarque').dataTable({
        "oLanguage": {
            "oPaginate": {
                "sFirst": window.primeraPagina,
                "sLast": window.ultimaPagina,
                "sNext": window.siguiente,
                "sPrevious": window.anterior
            },
            "sEmptyTable": window.sinDatos,
            "sInfo": window.mostrando,
            "sInfoEmpty": window.sinInformacion,
            "sInfoFiltered": window.filtrando,
            "sLengthMenu": window.mostrar,
            "sLoadingRecords": window.cargando,
            "sProcessing": window.procesando,
            "sSearch": window.buscar,
            "sZeroRecords": window.sinRegistros
        }
    });

};

/*Funcion para dibujar el grid semanal para las Jaulas*/
llenarGridSemana = function (fechaBase) {
    var Meses = ["ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];
    var DiaDeLaSeamana = fechaBase.getDay();

    var anio = fechaBase.getFullYear();
    $("#anioEtiqueta").text(anio);
    var fechaPrimerDiaSemana = sumarDias(fechaBase, -(DiaDeLaSeamana - 1));
    var mes = (fechaPrimerDiaSemana.getMonth() + 1);
    var mesString = "0" + mes;
    if (mes > 9) {
        mesString = "" + mes;
    }
    var FechaCalcular = fechaPrimerDiaSemana.getFullYear() + "-" + mesString + "-" + fechaPrimerDiaSemana.getDate();
    $("#lunesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(1);
    $("#martesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(2);
    $("#miercolesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(3);
    $("#juevesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(4);
    $("#viernesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(5);
    $("#sabadoEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(6);
    $("#domingoEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + Meses[fechaPrimerDiaSemana.getMonth()]);

    $('#txtFecha').val(fechaBase.getWeek());
    $("#txtFecha").change();

    if ($('#txtNumOrganizacion').val() != "") {
        ObtenerJaulasSolicitadas($("#txtNumOrganizacion").val(),FechaCalcular);
        ObtenerJaulasProgramadas($("#txtNumOrganizacion").val(), FechaCalcular);
    }
};

// Obtener los tipos de embarque y mostrarlos en el combo box
// de tipo embarque
ObtenerTiposEmbarque = function () {
    var datos = { Activo: 1 }
    var listadoTiposEmbarque = "";

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerTiposEmbarque',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            listadoTiposEmbarque = msg.d;
            var recursos = RecursosBindTitulos();
            var listadoTiposEmbarqueFinal = {};
            listadoTiposEmbarqueFinal.ListadoTiposEmbarque = listadoTiposEmbarque;
            listadoTiposEmbarqueFinal.Recursos = recursos;

            $('#ddlTipoEmbarque').html('');

            var valores = {};
            var recursos = {};
            recursos.Seleccione = "Seleccione";
            valores.Recursos = recursos;

            var listaValores = new Array();
            if (msg.d != null || msg.d.length > 0) {
                var condiciones = msg.d;
                for (var i = 0; i < condiciones.length; i++) {
                    var valor = {};
                    valor.Clave = condiciones[i].TipoEmbarqueID;
                    valor.Descripcion = condiciones[i].Descripcion;

                    listaValores.push(valor);
                }
            }
            valores.Valores = listaValores;

            $('#ddlTipoEmbarque').setTemplateURL('../Templates/ComboGenerico.htm');
            $('#ddlTipoEmbarque').processTemplate(valores);
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorTiposEmbarque,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
};

/* Método que obtiene los ruteos configurados para una organización origen y destino */

ObtenerRuteosPorOrigenyDestino = function () {
    var datos = {
                    'ruteoInfo':    {
                        'OrganizacionOrigen': { OrganizacionID: $("#txtNumOrigen").val() },
                        'OrganizacionDestino': { OrganizacionID: $("#txtNumDestino").val() }
                                    }   
                }

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerRuteosPorOrigenyDestino',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: "json",
        success: function (data) {
            if (data.d.length != 0) {
                $("#tbRutas tbody").html('');
                for (var i = 0; i < data.d.length; i++) {
                    $("#tbRutas tbody").append("<tr style='height: 50px;'>" +
                        "<td class='alineacionCentro' style='width: 150px;'> " +
                        "<input type='radio' class='' id='" + data.d[i].RuteoID + "' name='ruteos'/>" +
                        "</td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + data.d[i].NombreRuteo + "</td>" +
                        "</tr>");
                    $('#' + data.d[i].RuteoID).keydown(function (e) {
                        var code = e.which || e.keyCode;
                        if (code != 9) {
                            return false;
                        }
                    });
                }
                $("#dvSeleccionaRuta").modal('show');
            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinRuteoConfigurado,
                        callback: function () {
                            $("#txtNumDestino").val("");
                            $("#txtDestino").val("");
                            $("#txtNumOrigen").val("");
                            $("#txtOrigen").val("");
                            contador = 0;
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorBusquedaRuteo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
};

/* Método que obtiene los detalles del ruteo seleccionado */
ObtenerRuteoDetallesPorRuteoID = function () {
    var arregloFecha = $("#txtCitaCarga").val().split("-");
    var fechaSeleccionada = new Date(arregloFecha[2], arregloFecha[1] -1, arregloFecha[0]);
    fechaSeleccionada.setSeconds(formatoHora*60*60);
    embarqueInfo.CitaCarga = fechaSeleccionada;
    var datos = {'embarqueInfo': embarqueInfo}

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerRuteoDetallesPorRuteoID',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.length != 0) {
                $("#tbDetallesRuteo tbody").html('');
                var fecha = $("#txtCitaCarga").val().split('-');
                fecha = new Date(fecha[2], fecha[1], fecha[0]);
                fecha.setHours(formatoHora);
                var hora = new Date(0, 0);
                hora.setSeconds(formatoHora * 60 * 60);
                var mes = '';
                if (fecha.getMonth()<10){mes = "0"+fecha.getMonth()}else{mes = fecha.getMonth()}
                $("#tbDetallesRuteo tbody").append("<tr style='height: 50px;'>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'> " + $("#txtOrigen").val() + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + fecha.getDate() + "/" + mes + "/" + fecha.getFullYear() + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + hora.toTimeString().slice(0, 5) + "</td>" +
                        "</tr>");
                for (var i = 0; i < data.d.length; i++) {
                    var fechaFormato = ObtenerFechaFormato(data.d[i].Fecha);
                    var horas = new Date(0, 0);
                    horas.setSeconds(+data.d[i].Horas * 60 * 60);
                    $("#tbDetallesRuteo tbody").append("<tr style='height: 50px;'>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'> " + data.d[i].OrganizacionOrigen.Descripcion + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + fechaFormato + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + horas.toTimeString().slice(0, 5) + "</td>" +
                        "</tr>");
                    ultimaFecha = fechaFormato + " " + horas.toTimeString().slice(0, 5);
                }
                $("#txtDescarga").val(ultimaFecha);
                ruteoDetalle = data.d;
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorBusquedaDetallesRuteo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });

};

/*Obtener dias habiles*/
ObtenerDiasHabiles = function () {
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerDiasHabiles",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            //PONER MENSAJES DE ERRORES
            bootbox.alert({
                message: "ERROR: " + request,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#txtJaulasLunes").prop("disabled", !data.d.Lunes);
            $("#txtJaulasMartes").prop("disabled", !data.d.Martes);;
            $("#txtJaulasMiercoles").prop("disabled", !data.d.Miercoles);
            $("#txtJaulasJueves").prop("disabled", !data.d.Jueves);
            $("#txtJaulasViernes").prop("disabled", !data.d.Viernes);
            $("#txtJaulasSabado").prop("disabled", !data.d.Sabado);
            $("#txtJaulasDomingo").prop("disabled", !data.d.Domingo);
        }
    });
};

/* Función que suma o resta días a una fecha, si el parámetro días es negativo restará los días*/
sumarDias = function (fecha, dias) {
    fecha.setDate(fecha.getDate() + dias);
    return fecha;
};

/* Función que obtiene las jaulas solicitadas*/
ObtenerJaulasSolicitadas = function (OrganizacionID, FechaInicio) {
    var PedidoGanadoInfo = {};
    PedidoGanadoInfo.Organizacion = {};
    PedidoGanadoInfo.Organizacion.OrganizacionID = OrganizacionID;
    PedidoGanadoInfo.FechaInicio = FechaInicio;

    var datos = { 'pedidoGanadoInfo': PedidoGanadoInfo };

    $.ajax({
        data: JSON.stringify(datos),
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerJaulasSolicitadas",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            //PONER MENSAJES DE ERRORES
            bootbox.alert({
                message: "ERROR: " + request,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        },
        dataType: "json",
        async: false,
        success: function (data) {
            LlenarGridJaulasSolicitadas(data);
        }
    });
};

/* Función que dibuja las jaulas solicitadas*/
LlenarGridJaulasSolicitadas = function(data) {
    $("#txtJaulasLunes").text(data.d.Lunes);
    $("#txtJaulasMartes").text(data.d.Martes);
    $("#txtJaulasMiercoles").text(data.d.Miercoles);
    $("#txtJaulasJueves").text(data.d.Jueves);
    $("#txtJaulasViernes").text(data.d.Viernes);
    $("#txtJaulasSabado").text(data.d.Sabado);
    $("#txtJaulasDomingo").text(data.d.Domingo);
};

/*Método que se dispara al seleccionar un ruteo*/
SeleccionaRuta = function () {
    var id = $('#tbRutasBody input:radio:checked').attr('id');
    if (id <= 0 || id === undefined) {
        bootbox.dialog(
                {
                    message: window.msgValidaRuteoSeleccionado,
                    buttons: {
                        Aceptar: {
                            className: 'SuKarne btn-primary',
                            label: window.Aceptar
                        }
                    }
                });
        return;
    }
    embarqueInfo.Ruteo.RuteoID = $('#tbRutasBody input:radio:checked').attr('id');
    ruteoIdSeleccionado = embarqueInfo.Ruteo.RuteoID;
    $("#dvSeleccionaRuta").modal('hide');
    if ($("#txtOrigen").val().trim() != '' && $("#txtDestino").val().trim() != '' && esRuteo) {
        CrearTablaRuteo();
        ConsultaDetallesRuteo();
        $("#txtResponsableEmbarque").focus();
    }
}

/* Método que obtiene el formato correcto para las fechas */
ObtenerFechaFormato = function (fecha) {
    var dia = 0, mes = 0;
    var fechaInt = fecha.replace('/Date(', '');
    fechaInt = fechaInt.replace(')/', '');
    var fechaEntera = new Date(Number(fechaInt));
    if (fechaEntera.getDate() < 10) {
        dia = "0" + fechaEntera.getDate();
    } else {
        dia = fechaEntera.getDate();
    }
    if (fechaEntera.getMonth() + 1 < 10) {
        mes = "0" + (fechaEntera.getMonth() + 1);
    } else {
        mes = fechaEntera.getMonth() + 1 ;
    }

    var fechaFormato = dia + "/" + mes + "/" + fechaEntera.getFullYear();
    return fechaFormato;
};

/* Método que obtiene el formato correcto para las fechas */
ObtenerFechaFormatoAnoMesDia = function (fecha) {
    var dia = 0, mes = 0;
    var fechaInt = fecha.replace('/Date(', '');
    fechaInt = fechaInt.replace(')/', '');
    var fechaEntera = new Date(Number(fechaInt));
    if (fechaEntera.getDate() < 10) {
        dia = "0" + fechaEntera.getDate();
    } else {
        dia = fechaEntera.getDate();
    }
    if (fechaEntera.getMonth() + 1 < 10) {
        mes = "0" + (fechaEntera.getMonth() + 1);
    } else {
        mes = (fechaEntera.getMonth() + 1);
    }

    var fechaFormato = fechaEntera.getFullYear() + "/" + mes + "/" + dia;
    return fechaFormato;
};

CrearObjetoProgramacionEmbarque = function () {
    var embarqueInfo = {};
    var citaCarga = $("#txtCitaCarga").val().trim();
    var diaCarga = citaCarga.substring(0, 2);
    var mesCarga = citaCarga.substring(3, 5);
    var anoCarga = citaCarga.substring(6);

    citaCarga = anoCarga + "-" + mesCarga + "-" + diaCarga + " " + $('#txtHoraCarga').val();

    var citaDescarga = $("#txtDescarga").val().trim();
    var diaDescarga = citaDescarga.substring(0, 2);
    var mesDescarga = citaDescarga.substring(3, 5);
    var anoDescarga = citaDescarga.substring(6, 10);
    var horaDescarga = citaDescarga.substring(10);
    citaDescarga = anoDescarga + "-" + mesDescarga + "-" + diaDescarga + " " + horaDescarga;

    var horasTransito = $("#txtHorasTransito").val();
    if ($('#ddlTipoEmbarque option:selected').text() !== window.tipoRuteo) {
        if (horasTransito.length > 9) {
            horasTransito = horasTransito.toString() + ":00:00.0000000";
        } else {
            horasTransito = "0" + horasTransito.toString() + ":00:00.0000000";
        }
    }

    var observacion;

    var ruteo = {};
    var embarqueRuteoDetalle = [];
    var embarqueRuteoIDs = [];
    ruteo.RuteoDetalle = [];

    if ($("#txtObservacionCaptura").val() != "") {
        observacion = $("#txtObservacionCaptura").val();
    }

    $("#tbDetallesRuteo tbody tr").each(function(index) {
        embarqueRuteoIDs.push($(this).attr('id'));
    });

    var embarqueRuteoIDOrigen = 0;
    if (idRuteos[0] !== undefined) {
        embarqueRuteoIDOrigen = idRuteos[0].EmbarqueRuteoID;
    } 

    var embarqueRuteoOrigen = {
            HorasString: $("#txtHoraCarga").val().substring(0, $("#txtHoraCarga").val().length - 2),
            Fecha: citaCarga,
            OrganizacionOrigen: {
                OrganizacionID: $("#txtNumOrigen").val()
            },
            Ruteo: {
                RuteoID : ruteoIdSeleccionado   
            },
            EmbarqueRuteoID: embarqueRuteoIDOrigen
            
        }
    ruteo.RuteoDetalle.push(embarqueRuteoOrigen);

    if ($("#txtId").val() > 0) {
        if (esRuteo) {
            for (var i = 0; i < ruteoDetalle.length; i++) {

                var horasGuardar = new Date(0, 0);
                horasGuardar.setSeconds(+ruteoDetalle[i].Horas * 60 * 60);

                var horas = 0;
                var minutos = 0;
                var fechaFormato = ObtenerFechaFormatoAnoMesDia(ruteoDetalle[i].Fecha);
                var embarqueRuteo = {};
                if (horasGuardar.getHours() < 10) {
                    horas = "0" + horasGuardar.getHours();
                } else {
                    horas = horasGuardar.getHours();
                }

                if (horasGuardar.getMinutes() < 10) {
                    minutos = "0" + horasGuardar.getMinutes();
                } else {
                    minutos = horasGuardar.getMinutes();
                }

                var horasEmbarqueRuteo = horas + ":" + minutos;

                embarqueRuteo.HorasString = horasEmbarqueRuteo;
                embarqueRuteo.Kilometros = ruteoDetalle[i].Kilometros;
                embarqueRuteo.Fecha = fechaFormato;
                embarqueRuteo.OrganizacionOrigen = {};
                embarqueRuteo.OrganizacionOrigen.OrganizacionID = ruteoDetalle[i].OrganizacionOrigen.OrganizacionID;
                embarqueRuteo.EmbarqueRuteoID = idRuteos[i+1].EmbarqueRuteoID;

                ruteo.RuteoDetalle.push(embarqueRuteo);

            }
        }

        embarqueInfo = {
            'embarqueInfo': {
                'CitaCarga': citaCarga,
                'CitaDescarga': $("#txtDescarga").val().trim() === "" ? "1900-01-01" : citaDescarga,
                'EmbarqueID': $("#txtId").val() != 0 ? $("#txtId").val() : 0,
                'HorasTransito': $("#txtHorasTransito").val(),
                'Observaciones': observacion,
                'Organizacion': {
                    'OrganizacionID': $("#txtNumOrganizacion").val(),
                },
                'ResponsableEmbarque': $("#txtResponsableEmbarque").val(),
                'EmbarqueRuteoDetalle': ruteo.RuteoDetalle,
                'Ruteo': {
                    'OrganizacionOrigen': {
                        OrganizacionID: $("#txtNumOrigen").val(),
                    },
                    'OrganizacionDestino': {
                        OrganizacionID: $("#txtNumDestino").val(),
                    }
                },
                'TipoEmbarque': {
                    'TipoEmbarqueID': parseInt($('#ddlTipoEmbarque').val()),
                    'Descripcion': $('#ddlTipoEmbarque option:selected').text()
                },
                'UsuarioCreacionID': 0,
                'UsuarioModificacionID': 0
            }
        }
    }
    else {

            for (var i = 0; i < ruteoDetalle.length; i++) {

                var horasGuardar = new Date(0, 0);
                horasGuardar.setSeconds(+ruteoDetalle[i].Horas * 60 * 60);

                var embarqueRuteo = {
                    OrganizacionOrigen: {
                        OrganizacionID: ruteoDetalle[i].OrganizacionOrigen.OrganizacionID
                    },
                    HorasString: horasGuardar.toTimeString().slice(0, 5),
                    Kilometros: ruteoDetalle[i].Kilometros,
                    Fecha: ObtenerFechaFormatoAnoMesDia(ruteoDetalle[i].Fecha),
                    Ruteo : {
                        RuteoID : ruteoIdSeleccionado
                    }
                };

                ruteo.RuteoDetalle.push(embarqueRuteo);
            }

        embarqueInfo = {
            'embarqueInfo': {
                'CitaCarga': citaCarga,
                'CitaDescarga': $("#txtDescarga").val().trim() === "" ? "1900-01-01" : citaDescarga,
                'EmbarqueID': $("#txtId").val() != 0 ? $("#txtId").val() : 0,
                'HorasTransito': $("#txtHorasTransito").val(),
                'Observaciones': observacion,
                'Organizacion': {
                    'OrganizacionID': $("#txtNumOrganizacion").val(),
                },
                'ResponsableEmbarque': $("#txtResponsableEmbarque").val(),
                'Ruteo': {
                    'RuteoDetalle': ruteo.RuteoDetalle,
                    'OrganizacionOrigen': {
                        OrganizacionID: $("#txtNumOrigen").val(),
                    },
                    'OrganizacionDestino': {
                        OrganizacionID: $("#txtNumDestino").val(),
                    }
                },
                'TipoEmbarque': {
                    'TipoEmbarqueID': parseInt($('#ddlTipoEmbarque').val()),
                    'Descripcion': $('#ddlTipoEmbarque option:selected').text()
                },
                'UsuarioCreacionID': 0,
                'UsuarioModificacionID': 0
            }
        }
        
    }      
   
    return embarqueInfo;
}

/* Metodo de prueba para guardar */
Guardar = function (datos, seccion) {  
    $.ajax({
        data: JSON.stringify({ embarqueInfo: datos.embarqueInfo, seccion: seccion }),
        type: "POST",
        url: "ProgramacionEmbarque.aspx/Guardar",
        contentType: "application/json; charset=utf-8",      
        dataType: "json",
        async: false,
        success: function (data) {
            // Guardado para seccion de programacion
            if (seccion == 1) {
                MensajeInformativo(window.msgExitoGuardar, seccion);
            }
            // Guardado para seccion de transporte
            else if (seccion == 2) {
                // Enviar correo
                if (transportistaSeleccionado.correo != "" || transportistaSeleccionado.correo != null) {
                    BloquearPantalla();
                    EnviarCorreoTransportista(transportistaSeleccionado);
                }
               
            }
            // Guardado para seccion de datos
            else if (seccion == 3) {
                // Mensaje de exito con folio generado              
                var msgExito = window.msgExitoFolioEmbarque;

                // Modificar mensaje de exito con folio de embarque registrado
                msgExito = msgExito.substr(0, msgExito.indexOf("#") + 1) + data.d.FolioEmbarque + msgExito.substr(msgExito.indexOf("#") + 1, msgExito.length);

                MensajeInformativo(msgExito, seccion);
            }
        },
        error: function (request, status, error) {
            esRuteo = false;
            bootbox.alert({
                message: window.msgErrorGuardar,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

/* Mensaje Informativo */
MensajeInformativo = function (mensaje, seccion) {
    bootbox.dialog(
                {
                    message: mensaje,
                    buttons: {
                        Aceptar: {
                            className: 'SuKarne btn-primary',
                            label: window.Aceptar,
                            callback: function () {
                                if (seccion == 1) {
                                    setTimeout(function () {
                                        LimpiarPantalla();
                                        $("#ddlTipoEmbarque").focus();
                                    }, 10);
                                    llenarGridSemana(fechaInicial);
                                    LimpiarPantalla();
                                    $("#txtNumOrganizacion").attr("disabled", false);
                                    $("#btnAyudaOrganizacion").removeClass("btn-disabled");
                                    ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
                                    HabilitarAyudas();
                                    ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val());
                                    ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val());
                                } else if (seccion == 2) {
                                    LimpiarPantalla();
                                    $("#btnGuardar").val("Agregar");
                                    $("#txtNumOrganizacion").attr("disabled", false);
                                    $("#btnAyudaOrganizacion").removeClass("btn-disabled");
                                    ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
                                    ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val());
                                    ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val());
                                    LimpiarPantallaTransporte();
                                    DeshabilitarControlesTransporte();
                                    LimpiarCamposDatos();
                                    DeshabilitarCapturaDeDatos();
                                    setTimeout(function () {
                                        $("#ddlTipoEmbarque").focus();
                                    }, 10);
                                } else if (seccion == 3) {
                                    $("#txtNumOrganizacion").attr("disabled", false);
                                    $("#btnAyudaOrganizacion").removeClass("btn-disabled");
                                    ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
                                    ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val());
                                    ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val());
                                    LimpiarCamposDatos();
                                    DeshabilitarCapturaDeDatos();
                                    LimpiarPantallaTransporte();
                                    DeshabilitarControlesTransporte();
                                    $("#btnGuardarDatos").val("Agregar");
                                }
                            }
                        }
                    }
                });
};

/* Función que obtiene las jaulas programadas*/
ObtenerJaulasProgramadas = function (OrganizacionID, FechaInicio) {
    var EmbarqueInfo = {};
    EmbarqueInfo.Organizacion = {};
    EmbarqueInfo.Organizacion.OrganizacionID = OrganizacionID;
    EmbarqueInfo.FechaInicio = FechaInicio;

    var datos = { 'embarqueInfo': EmbarqueInfo };
    $.ajax({
        data: JSON.stringify(datos),
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerJaulasProgramadas",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            //PONER MENSAJES DE ERRORES
            bootbox.alert({
                message: window.msgErrorObtenerJaulasProgramadas,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        },
        dataType: "json",
        async: false,
        success: function (data) {
            LlenarGridJaulasProgramadas(data);
        }
    });
};

/* Función que dibuja las jaulas solicitadas*/
LlenarGridJaulasProgramadas = function (data) {
    
    $("#txtJaulaProgramadasDomingo").text("-");
    $("#txtJaulaProgramadasLunes").text("-");
    $("#txtJaulaProgramadasMartes").text("-");
    $("#txtJaulaProgramadasMiercoles").text("-");
    $("#txtJaulaProgramadasJueves").text("-");
    $("#txtJaulaProgramadasViernes").text("-");
    $("#txtJaulaProgramadasSabado").text("-");

    data.d.forEach(function (registro) {
        var Dia = new Date(registro.FechaCitaCargaStringJaula).getDay();

        switch (Dia) {
            case 0:
                $("#txtJaulaProgramadasDomingo").text(registro.JaulasProgramadas);
                break;
            case 1:
                $("#txtJaulaProgramadasLunes").text(registro.JaulasProgramadas);
                break;
            case 2:
                $("#txtJaulaProgramadasMartes").text(registro.JaulasProgramadas);
                break;
            case 3:
                $("#txtJaulaProgramadasMiercoles").text(registro.JaulasProgramadas);
                break;
            case 4:
                $("#txtJaulaProgramadasJueves").text(registro.JaulasProgramadas);
                break;
            case 5:
                $("#txtJaulaProgramadasViernes").text(registro.JaulasProgramadas);
                break;
            case 6:
                $("#txtJaulaProgramadasSabado").text(registro.JaulasProgramadas);
        }
    });  
};

//Obtiene la organizacion capturada
ObtenerOrganizacion = function () {

    if ($("#txtNumOrganizacion").val() != "") {
        datos = { "organizacion": $("#txtNumOrganizacion").val() };
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerOrganizacion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function(request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinOrganizacionValida,
                        callback: function() {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumOrganizacion").val("");
                }
            },
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        $("#txtOrganizacion").val(data.d[0].Descripcion);
                        llenarGridSemana(fechaInicial);
                        ObtenerProgramacionEmbarque($("#txtNumOrganizacion").val());
                        ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val(),0,false);
                        ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val(), 0, false);
                    } else {
                        $("#txtNumOrganizacion").val("");
                        $("#txtOrganizacion").val("");
                        bootbox.alert({
                            message: window.msgSinOrganizacionValida,
                            callback: function () {
                                setTimeout(function () { $("#txtNumOrganizacion").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                } else {
                    $("#txtNumOrganizacion").val("");
                    $("#txtOrganizacion").val("");
                    bootbox.alert({
                        message: window.msgSinOrganizacionValida,
                        callback: function () {
                            setTimeout(function () { $("#txtNumOrganizacion").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
    
};

//Obtiene la organizacion Origen capturada
ObtenerOrganizacionOrigen = function () {

    if ($("#txtNumOrigen").val() != "") {
        datos = { "organizacion": $("#txtNumOrigen").val() };
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerOrganizacionOrigen",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinOrganizacionOrigenValida,
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumOrigen").val("");
                }
            },
            dataType: "json",
            async:false,
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        $("#txtOrigen").val(data.d[0].Descripcion);
                        ValidarOrigenyDestino();
                        $('#txtNumDestino').focus();
                    } else {
                        $("#txtNumOrigen").val("");
                        $("#txtOrigen").val("");
                        bootbox.alert({
                            message: window.msgSinOrganizacionOrigenValida,
                            callback: function() {
                                setTimeout(function() { $("#txtNumOrigen").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                } else {
                    $("#txtNumOrigen").val("");
                    $("#txtOrigen").val("");
                    bootbox.alert({
                        message: window.msgSinOrganizacionOrigenValida,
                        callback: function () {
                            setTimeout(function () { $("#txtNumOrigen").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Obtiene la organizacion Destino capturada
ObtenerOrganizacionDestino = function () {

    if ($("#txtNumDestino").val() != "") {
        datos = { "organizacion": $("#txtNumDestino").val() };
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerOrganizacion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            async: false,
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinOrganizacionDestinoValida,
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumDestino").val("");
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        $("#txtDestino").val(data.d[0].Descripcion);
                        $('#txtResponsableEmbarque').focus();
                        ValidarOrigenyDestino();
                    }
                    else {
                        $("#txtNumDestino").val("");
                        $("#txtDestino").val("");
                        bootbox.alert({
                            message: window.msgSinOrganizacionDestinoValida,
                            callback: function () {
                                setTimeout(function () { $("#txtNumDestino").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                }
                else {
                    $("#txtNumDestino").val("");
                    $("#txtDestino").val("");
                    bootbox.alert({
                        message: window.msgSinOrganizacionDestinoValida,
                        callback: function () {
                            setTimeout(function () { $("#txtNumDestino").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

/*Método que valida los campos hora carga y cita carga para llamar al método para obtener los detalles del ruteo.*/
ConsultaDetallesRuteo = function () {
    if (esRuteo) {
        if ($("#txtHoraCarga").val().trim() != '' && $("#txtHoraCarga").val() !== undefined) {
            ObtenerFormatoHora();
            if ($("#txtCitaCarga").val().trim() != '' && $("#txtCitaCarga").val() !== undefined) {
                if (embarqueInfo.Ruteo.RuteoID != 0) {
                    ObtenerRuteoDetallesPorRuteoID();
                }
            }
        }
    }  
};

/*Valida que existan las organizaciones origen y destino.*/
ValidarOrigenyDestino = function () {
    if ($("#txtOrigen").val().trim() != '' && $("#txtOrigen").val() !== undefined) {
        if ($("#txtDestino").val().trim() != '' && $("#txtDestino").val() !== undefined) {
            ValidaFleteOrigenDestino();
            if (!tieneTarifa) {
                if (contador == 0) {
                    contador = contador + 1;
                    bootbox.alert({
                        message: window.msgNoTieneTarifa,
                        callback: function () {
                            $("#txtNumDestino").val("");
                            $("#txtDestino").val("");
                            contador = 0;
                            if ($("#ddlTipoEmbarque").val() != 3) {
                                embarqueInfo.Ruteo.RuteoID = 0;
                                CrearDivNormal();
                            } else {
                                CrearTablaRuteo();
                            }
                            $("#txtNumDestino").focus();
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            } else {
                if (esRuteo && contador == 0) {
                    ObtenerRuteosPorOrigenyDestino();
                }
            }
        }
    }
};

FuncionCargarEventosHorasTransito = function () {

    $("#txtHorasTransito").numericInput().attr("maxlength", "4");
    if ($("#txtNumOrganizacion").val() == "") {
        $("#txtHorasTransito").attr("disabled", true);
    }
    
    $("#txtHorasTransito").on("change input", function (e) {
        if ($('#txtHorasTransito').val() != "") {
            if ($('#txtCitaCarga').val() != "") {
                ActualizarCitaDescarga();
            }
        } else {
            $('#txtDescarga').val("");
        }
    });

    // Al capturar la hora transito al seleccionar tap
    $('#txtHorasTransito').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtHorasTransito').val() != "") {
                e.preventDefault();
                if ($('#txtCitaCarga').val() != "") {
                    var horas = parseInt($('#txtHoraCarga').val()) + parseInt($('#txtHorasTransito').val());
                    $('#txtDescarga').val(ObtenerFormatoFechaCitaDescarga(horas));
                }
            }
        }
    });

    // Al dar enter en las horas transito en la ventana principal
    $('#txtHorasTransito').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($("#txtHorasTransito").val() != "") {
                e.preventDefault();
                $("#txtObservacionCaptura").focus();
            }
        }
    });
};

/*Método que construye el div normal para los tipos de embarque directo y descanso.*/
CrearDivNormal = function () {
    var horasTransito = $("#txtHorasTransito").val();
    $("#divCambiante").html('');
    $("#divCambiante").append("<span class='textoDerecha span3'>" +
        "<span class='requerido'>*</span>" +
        "<asp:Label ID='lblCabezasOrigen' runat='server' > " + lblHorasTransito + "  </asp:Label>" +
        "</span>" +
        "<input id='txtHorasTransito' tabindex='10' class='span2 textBoxTablas margenIzquierdaCapturaDatos' type='text' />  ");
    $("#txtHorasTransito").val(horasTransito);
    FuncionCargarEventosHorasTransito();
};

/*Crea la tabla para los detalles del ruteo en el tipo de embarque ruteo*/
CrearTablaRuteo = function() {
    $("#divCambiante").html('');
    $("#divCambiante").append("<div id='divDetallesRuteo' style='height: 250px; overflow: auto; width: 97%'>" +
        "<table id='tbDetallesRuteo' style='width: 100% !important; height: 243px; overflow: auto;' class='table table-striped table-advance table-hover no-left-margin'>" +
        "<thead>" +
        "<th class='columnaGridTitulo alineacionCentro' style='height:30px !important'>Ruta</th>" +
        "<th class='columnaGridTitulo alineacionCentro' style='height:30px !important'>Fecha</th>" +
        "<th class='columnaGridTitulo alineacionCentro' style='height:30px !important'>Hora</th>" +
        "</thead>" +
        "<tbody>" +
        "<tr>" +
        "<td class='alineacionCentro' style='vertical-align:middle'>Ruta</td>" +
        "<td class='alineacionCentro' style='vertical-align:middle'>Fecha</td>" +
        "<td class='alineacionCentro' style='vertical-align:middle'>Hora</td>" +
        "</tr>" +
        "</tbody>" +
        "</table>" +
        "</div>");
};

/*Limpia los campos que se utilizan cuando el tipo de embarque es ruteo.*/
LimpiaCamposRuteo = function() {
    $("#txtOrigen").val("");
    $("#txtNumOrigen").val("");
    $("#txtDestino").val("");
    $("#txtNumDestino").val("");
    $("#ddlTipoEmbarque").val(0);
    $("#txtHoraCarga").val("8:00am");
    $("#txtResponsableEmbarque").val("");
    CrearDivNormal();
};

/*Validar campos vacios*/
ValidarCapturaDatosProgramacion = function () {
    if ($("#ddlTipoEmbarque").val() == 0) {
        bootbox.dialog({
            message: window.msgErrorValidarTiposEmbarque,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#ddlTipoEmbarque").focus();
                        }, 10);
                    }
                }
            }
        });      
        return false;
    }

    if ($("#txtNumOrigen").val() == "" && $("#txtOrigen").val() == "") {
        bootbox.dialog({
            message: window.msgErrorValidarOrigen,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#txtNumOrigen").focus();
                        }, 10);                        
                    }
                }
            }
        });
        return false;
    }

    if ($("#txtNumDestino").val() == "" && $("#txtDestino").val() == "") {
        bootbox.dialog({
            message: window.msgErrorValidarDestino,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#txtNumDestino").focus();
                        }, 10);
                    }
                }
            }
        });      
        return false;
    }

    if ($("#txtResponsableEmbarque").val() == "") {
        bootbox.dialog({
            message: window.msgErrorValidarResponsableCarga,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#txtResponsableEmbarque").focus();
                        }, 10);
                    }
                }
            }
        });
        return false;
    }

    if ($("#txtCitaCarga").val() == "") {
        bootbox.dialog({
            message: window.msgErrorValidarCitaCarga,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#txtCitaCarga").datepicker("show");
                        }, 10);
                    }
                }
            }
        });
        return false;
    }

    if ($("#txtHorasTransito").val() == "" || $("#txtHorasTransito").val() <= 0) {
        bootbox.dialog({
            message: window.msgErrorValidarHorasTransito,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        setTimeout(function () {
                            $("#txtHorasTransito").focus();
                        }, 10);
                    }
                }
            }
        });
        return false;
    }

    return true;
};

//Obtiene el formato para la cita descarga
ObtenerFormatoFechaCitaDescarga = function (horas) {
    var dia = 0, mes = 0, minutos = 0;
    var fechaActual = new Date(fechaCitaCarga);
    fechaActual.setSeconds(horas * 60 * 60);

    if (isNaN(fechaActual.getDate())) {
        return null;
    }

    if (fechaActual.getDate() < 10) {
        dia = "0" + fechaActual.getDate();
    } else {
        dia = fechaActual.getDate();
    }
    if (fechaActual.getMonth() + 1 < 10) {
        mes = "0" + (fechaActual.getMonth() +1);
    } else {
        mes = (fechaActual.getMonth() + 1);
    }
    if (fechaActual.getMinutes() < 10) {
        minutos = "0" + fechaActual.getMinutes();
    } else {
        minutos = fechaActual.getMinutes();
    }

    var fecha = dia + "/" + mes + "/" + fechaActual.getFullYear()  + " " + fechaActual.getHours() + ":" + minutos;
    return fecha;
}

ValidaRuteosActivos = function() {
    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerRuteosActivos',
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            if (data.d.length == 0) {
                bootbox.alert({
                    message: window.msgNoRuteosActivos,
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
                return;
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorBusquedaRuteo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
};

ObtenerFormatoHora = function() {
    var hora = $("#txtHoraCarga").val().replace("m", "");
    hora = hora.replace("a", '');
    hora = hora.replace("p", '');
    hora = hora.split(':');
    if ($("#txtHoraCarga").val().substr(-2) == "pm") {
        hora[0] = Number(hora[0]) + 12;
    }
    if ($("#txtHoraCarga").val().substr(0, 2) == 12 && $("#txtHoraCarga").val().substr(-2) == "am") {
        hora[0] = Number(hora[0]) - 12;
    }
    formatoHora = hora[0] + "." + (hora[1] / 6);
};

/* Método que obtiene el formato correcto para las horas */
ObtenerFechaFormatoHora = function (fecha) {
    var dia = 0, mes = 0;
    var fechaInt = fecha.replace('/Date(', '');
    fechaInt = fechaInt.replace(')/', '');
    var fechaEntera = new Date(Number(fechaInt));
    if (fechaEntera.getDate() < 10) {
        dia = "0" + fechaEntera.getDate();
    } else {
        dia = fechaEntera.getDate();
    }
    if (fechaEntera.getMonth() < 10) {
        mes = "0" + fechaEntera.getMonth();
    } else {
        mes = fechaEntera.getMonth();
    }

    var fechaFormato = dia + "/" + mes + "/" + fechaEntera.getFullYear();
    return fechaFormato;};

ObtenerFormatoHoraCarga = function (fecha) {
    var horas = 0, minutos = 0, tiempo = '';
    var fechaInt = fecha.replace('/Date(', '');
    fechaInt = fechaInt.replace(')/', '');
    var fechaEntera = new Date(Number(fechaInt));
    if (fechaEntera.getHours() > 11) {
        horas = fechaEntera.getHours() - 12;
        tiempo = "pm";
    } else {
        horas = fechaEntera.getHours();
        tiempo = "am";
    }
    if (fechaEntera.getMinutes() < 10) {
        minutos = "0" + fechaEntera.getMinutes();
    } else {
        minutos = fechaEntera.getMinutes();
    }
    var formatoHoraCarga = horas + ":" + minutos + tiempo;
    $("#txtHoraCarga").val(formatoHoraCarga);   
}

ObtenerDetallesEmbarqueRuteo = function (embarqueId) {
    var arregloFecha = $("#txtCitaCarga").val().split("-");
    var fechaSeleccionada = new Date(arregloFecha[2], arregloFecha[1], arregloFecha[0]);
    ObtenerFormatoHora();
    fechaSeleccionada.setHours(formatoHora);
    embarqueRuteoInfo.CitaCarga = fechaSeleccionada;
    embarqueRuteoInfo.EmbarqueID = embarqueId;
    var datos = { 'embarqueInfo': embarqueRuteoInfo }

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerDetallesEmbarqueRuteo',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.length != 0) {
                $("#tbDetallesRuteo tbody").html('');
                for (var i = 0; i < data.d.length; i++) {
                    var horas = 0;
                    var minutos = 0;
                    var fechaFormato = ObtenerFechaFormato(data.d[i].Fecha);
                    if (data.d[i].Horas.Hours < 10) { horas = "0" + data.d[i].Horas.Hours } else { horas = data.d[i].Horas.Hours }
                    if (data.d[i].Horas.Minutes < 10) { minutos = "0" + data.d[i].Horas.Minutes } else { minutos = data.d[i].Horas.Minutes }
                    $("#tbDetallesRuteo tbody").append("<tr style='height: 50px;' id='" + data.d[i].EmbarqueRuteoID + "'>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'> " + data.d[i].OrganizacionOrigen.Descripcion + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + fechaFormato + "</td>" +
                        "<td class='alineacionCentro' style='vertical-align:middle'>" + horas + ":" + minutos + "</td>" +
                        "</tr>");
                    ultimaFecha = fechaFormato;
                }
                embarqueInfo.Ruteo.RuteoID = data.d[0].Ruteo.RuteoID;
                $("#txtDescarga").val(ultimaFecha);
                ruteoDetalle = data.d;
                idRuteos = ruteoDetalle;
                ObtenerRuteoDetallesPorRuteoID(embarqueInfo.Ruteo.RuteoID);
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorBusquedaDetallesRuteo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

ValidaFleteOrigenDestino = function () {
    var datos = {
        'ruteoInfo': {
            'OrganizacionOrigen': { OrganizacionID: $("#txtNumOrigen").val() },
            'OrganizacionDestino': { OrganizacionID: $("#txtNumDestino").val() }
        }
    }

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerFleteOrigenDestino',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async:false,
        success: function (data) {
            tieneTarifa = data.d;
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorBusquedaRuteo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

ActualizarCitaDescarga = function () {
    var txtHoraCarga = $('#txtHoraCarga').val();
    var horasCarga = parseInt(txtHoraCarga);
    if (txtHoraCarga.length == 7) {
        var minutosHoraCarga = $('#txtHoraCarga').val().substring(3, 5);
    } else {
        var minutosHoraCarga = $('#txtHoraCarga').val().substring(2, 4);
    }
    //var minutosHoraCarga = $('#txtHoraCarga').val().substring(2, 4);
    var minutosCarga = minutosHoraCarga / 6;
    var horasTransito = parseInt($('#txtHorasTransito').val());
    horasTransito = isNaN(horasTransito) ? 0 : horasTransito;
    var arrFechaCitaCarga = $('#txtCitaCarga').val().split("-");
    if (txtHoraCarga.includes("am")) {
        if (horasCarga == 12) {
            horasCarga = 0;
        }
    }
    else if (horasCarga < 12) {
        horasCarga += 12;
    }

    var horas = horasCarga + horasTransito +"." + minutosCarga;
    fechaCitaCarga = new Date(arrFechaCitaCarga[2], arrFechaCitaCarga[1] - 1, arrFechaCitaCarga[0]);
    $('#txtDescarga').val(ObtenerFormatoFechaCitaDescarga(horas));
}

//-----------------------------------------------------------------
// Funciones de la pestaña de Transporte
//-----------------------------------------------------------------

InicicializarTransporte = function () {
    
    $("#txtIdTransporte").val(0);

    $("#txtNumTransportista").numericInput().attr("maxlength", "11");

    $("#txtNumRuta").numericInput().attr("maxlength", "11");

    $("#txtFlete").numericInput().attr("maxlength", "4");

    $("#txtGastoFijo").numericInput().attr("maxlength", "4");

    $("#txtGastoVariable").numericInput().attr("maxlength", "8");

    $("#txtDemora").numericInput().attr("maxlength", "8");

    $("#txtObservacionesTransporteCaptura").attr("maxlength", "255");

};

AsignarEventosControlesTranasporte = function() {

    //Dar click en la pestaña de transporte embarque
    $('a[href="#TapGridTransporte"]').on('shown.bs.tab', function (e) {
        $("#divCapturaDatos").hide();
        $("#divCapturaDatosEmbarqueDatos").hide();
        $("#divCapturaDatosEmbarqueTransporte").show();
    });

    // Al capturar el codigo del Transportista
    $('#txtNumTransportista').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumTransportista').val() != "") {
                e.preventDefault();
                $('#btnTransportista').focus();
            }
        }
    });

    // Al capturar el codigo de la ruta
    $('#txtNumRuta').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumRuta').val() != "") {
                e.preventDefault();
                $('#btnRuta').focus();
            }
        }
    });

    // Al perder el foco el textbox del indentificador de la Ruta
    $("#txtNumRuta").focusout(function () {
        if ($('#txtNumRuta').val() == "") {
            $("#txtRuta").val("");
            $("#txtNumTransportista").val("");
            $("#txtTransportista").val("");
            $("#txtKms").val("");
            $("#txtCitaDescargaTransporte").val("");
            $("#txtFlete").val("");
            $("#txtGastoFijo").val("");
        }
        else {
            ObtenerRuta();
        }
    });

    // Al perder el foco el textbox del indentificador del Transportista
    $("#txtNumTransportista").focusout(function () {
        if ($('#txtNumTransportista').val() == "") {
            $("#txtTransportista").val("");
            $("#txtFlete").val("");
            $("#txtGastoFijo").val("");
        }
        else {
            ObtenerTransportista();
        }
    });

    // Boton que abre la ayuda para Transportistat
    $("#btnTransportista").click(function () {
        ObtenerTransportistas();
    });

    // Boton que abre la ayuda para la Ruta
    $("#btnRuta").click(function () {
        ObtenerRutas();
    });

};

//Obtiene el Transportista capturado por codigo
ObtenerTransportista = function () {

    if ($("#txtNumTransportista").val() != "") {

        var datos = {
            "codigoSAP": $("#txtNumTransportista").val(),
            "ConfiguracionEmbarqueDetalleID": $("#txtNumRuta").val()
        };
        
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerProveedorDescripcion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgTransportistaNoValido,
                        callback: function () {
                            msjAbierto = 0;
                            $("#txtNumTransportista").focus();
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        transportistaSeleccionado.proveedorId = data.d[0].ProveedorID;
                        transportistaSeleccionado.codigo = data.d[0].CodigoSAP;
                        transportistaSeleccionado.descripcion = data.d[0].Descripcion;
                        ValidarCorreoTransportista();
                    } else {
                        $("#txtNumTransportista").val("");
                        $("#txtTransportista").val("");
                        $("#txtFlete").val("");
                        $("#txtGastoFijo").val("");
                        bootbox.alert({
                            message: window.msgSinRutaProveedor,
                            callback: function () {
                                setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                } else {
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                    $("#txtFlete").val("");
                    $("#txtGastoFijo").val("");
                    bootbox.alert({
                        message: window.msgTransportistaNoValido,
                        callback: function () {
                            setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Valida que el proveedor pertenesca a la ruta seleccionada
ValidaProveedorTieneRutaSeleccionada = function() {
    if ($("#txtNumTransportista").val() != "") {
        var datos = {
            "codigoSAP": $("#txtNumTransportista").val(),
            "ConfiguracionEmbarqueDetalleID": $("#txtNumRuta").val()
        };
        
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerProveedorDescripcion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
            },
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        if ($("#txtNumTransportista").val() != "") {
                            CargarCostoFlete();
                        }
                        return;
                    } else {
                        $("#txtNumTransportista").val("");
                        $("#txtTransportista").val("");
                        $("#txtFlete").val("");
                        $("#txtGastoFijo").val("");
                        bootbox.alert({
                            message: window.msgSinRutaProveedor,
                            callback: function () {
                                setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                } else {
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                    $("#txtFlete").val("");
                    $("#txtGastoFijo").val("");
                    bootbox.alert({
                        message: window.msgSinRutaProveedor,
                        callback: function () {
                            setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Obtiene la ruta capturada por codigo
ObtenerRuta = function () {

    if ($("#txtNumRuta").val() != "") {

        var datos = {
            'embarque': {
                'ConfiguracionEmbarqueDetalle': {
                    'ConfiguracionEmbarqueDetalleID': $("#txtNumRuta").val()
                },
                'OrganizacionOrigen': {
                    'OrganizacionID': organizacionOrigen,
                },
                'OrganizacionDestino': {
                    'OrganizacionID': organizacionDestino,
                }
            }
        };

        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerRutasPorId",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinRuta,
                        callback: function () {
                            msjAbierto = 0;
                            $("#txtNumRuta").focus();
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumRuta").val("");
                    $("#txtRuta").val("");
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                    $("#txtKms").val("");
                    $("#txtCitaDescargaTransporte").val("");
                    $("#txtFlete").val("");
                    $("#txtGastoFijo").val("");
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d != null) {
                    if (data.d.length > 0) {
                        $("#txtRuta").val(data.d[0].Descripcion);
                        $("#txtNumRuta").val(rutaSeleccionada.rutaId = data.d[0].ConfiguracionEmbarqueDetalleID);
                        $("#txtRuta").val(rutaSeleccionada.descripcion = data.d[0].Descripcion);
                        $("#txtKms").val(rutaSeleccionada.kilometros = data.d[0].Kilometros);
                        rutaSeleccionada.Horas = data.d[0].Horas.Hours;
                        rutaSeleccionada.Minutos = data.d[0].Horas.Minutes;
                        ActualizarCitaDescargaTransporte();
                        ValidaProveedorTieneRutaSeleccionada();
                        $("#txtNumTransportista").focus();
                    } else {
                        $("#txtNumRuta").val("");
                        $("#txtRuta").val("");
                        $("#txtNumTransportista").val("");
                        $("#txtTransportista").val("");
                        $("#txtKms").val("");
                        $("#txtCitaDescargaTransporte").val("");
                        $("#txtFlete").val("");
                        $("#txtGastoFijo").val("");
                        bootbox.alert({
                            message: window.msgSinRuta,
                            callback: function () {
                                setTimeout(function () { $("#txtNumRuta").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                } else {
                    $("#txtNumRuta").val("");
                    $("#txtRuta").val("");
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                    $("#txtKms").val("");
                    $("#txtCitaDescargaTransporte").val("");
                    $("#txtFlete").val("");
                    $("#txtGastoFijo").val("");
                    bootbox.alert({
                        message: window.msgSinRuta,
                        callback: function () {
                            setTimeout(function () { $("#txtNumRuta").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Abre modal y obtiene los datos de Transportista
ObtenerTransportistas = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = {
            "codigoSAP": $("#txtOrganizacionBuscar").val(), 
            "ConfiguracionEmbarqueDetalleID": $("#txtNumRuta").val()
        };
    } else {
        datos = {
            "codigoSAP": "",
            "ConfiguracionEmbarqueDetalleID": $("#txtNumRuta").val()
        };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerProveedorDescripcion",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgProveedoresSinRuta,
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary',
                        }
                    },
                    callback:function() {
                         msjAbierto = 0;
                    }
                });
            }
       },
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d) {
                    var resultado = data.d;
                    CargarEncabezadoAyudaTransportista();
                    //Contenido de la busqueda
                    $("#tbBusqueda tbody").html("");
                    for (var i = 0; i < resultado.length; i++) {
                        $("#tbBusqueda tbody").append("<tr>" +
                            "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].CodigoSAP + "' proveedorID='" + resultado[i].ProveedorID + "' organizacion='" + resultado[i].CodigoSAP + "' descripcion='" + resultado[i].Descripcion + "' correo='" + resultado[i].Correo + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].CodigoSAP + "\");'/></td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].ProveedorID + "</td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].CodigoSAP + "</td>" +
                            "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                            "</tr>");
                    }
                    AsignarEventosModalBusqueda();
                    setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                    $("#dlgBusquedaOrganizacion").modal("show");
                }
            } else {
                CargarEncabezadoAyudaTransportista();
                $("#txtNumTransportista").val("");
                $("#txtTransportista").val("");
                bootbox.alert({
                    message: window.msgProveedoresSinRuta,
                    callback:function () {
                        setTimeout(function() {
                            $("#txtNumTransportista").focus();
                            $("#dlgBusquedaOrganizacion").modal("hide");
                            $("#txtOrganizacionBuscar").val("");
                        }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

CargarEncabezadoAyudaTransportista = function() {
    //Titulo de la ayuda
    $("#lblTituloDialogo").html("");
    $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaTransportista_Titulo + "</asp:Label>");
    //opciones de la ayuda de Transportista
    ayudaDesplegada = 4;
    $("#OpcionesAyuda").html("");
    $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblCodigoSAP + "  </asp:Label>" +
                    "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                    "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerTransportistas();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                    "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarTransportita();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                    "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
    //Encabezado de busqueda
    $("#tbBusquedaEncabezado thead").html("");
    $("#tbBusquedaEncabezado thead").append("<tr>" +
        "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
        "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
        "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4' runat='server'>" + lblCodigoSAP + "</asp:Label></th>" +
        "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblAyudaGrid + "</asp:Label></th>" +
        "</tr>");
    //Cuerpo de la busqueda
    $("#tbBusqueda tbody").html("");
};

//Abre modal y obtiene los datos de Transportista
ObtenerRutas = function () {
    var datos = {};
    var configuracionEmbarque = {
        'OrganizacionOrigen': {
            'OrganizacionID': organizacionOrigen,
        },
        'OrganizacionDestino': {
            'OrganizacionID': organizacionDestino,
        }
    }

    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = {
            "descripcion": $("#txtOrganizacionBuscar").val(),
            "embarque": configuracionEmbarque
        };
    } else {
        datos = {
            "descripcion": "",
            "embarque": configuracionEmbarque
        };
    }


    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerRutasPorDescripcion",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message:window.msgSinTipoGanadera,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d) {
                    var resultado = data.d;
                    CargarEncabezadoAyudaRutas();
                    //Contenido de la busqueda
                    $("#tbBusqueda tbody").html("");
                    for (var i = 0; i < resultado.length; i++) {
                        $("#tbBusqueda tbody").append("<tr>" +
                            "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].ConfiguracionEmbarqueDetalleID + "' ConfiguracionEmbarqueDetalleID='" +
                            resultado[i].ConfiguracionEmbarqueDetalleID + "' descripcion='" + resultado[i].Descripcion + "' Kilometros='" + resultado[i].Kilometros +
                            "'horas='" + resultado[i].Horas.Hours +
                            "'minutos='" + resultado[i].Horas.Minutes + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].ConfiguracionEmbarqueDetalleID + "\");'/></td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].ConfiguracionEmbarqueDetalleID + "</td>" +
                            "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                            "</tr>");
                    }
                    AsignarEventosModalBusqueda();
                    setTimeout(function() {
                        $("#txtOrganizacionBuscar").val("");
                        $("#txtOrganizacionBuscar").focus();
                    }, 500);
                    $("#dlgBusquedaOrganizacion").modal("show");
                }
            } else {
                CargarEncabezadoAyudaRutas();
            }
        }
    });
};

CargarEncabezadoAyudaRutas = function() {
    //Titulo de la ayuda
    $("#lblTituloDialogo").html("");
    $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaRuta_Titulo + "</asp:Label>");
    //opciones de la ayuda de Ruta
    ayudaDesplegada = 5;
    $("#OpcionesAyuda").html("");
    $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblRuta + "  </asp:Label>" +
                    "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                    "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerRutas();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                    "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarRuta();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                    "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
    //Encabezado de busqueda
    $("#tbBusquedaEncabezado thead").html("");
    $("#tbBusquedaEncabezado thead").append("<tr>" +
        "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
        "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
        "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblAyudaGrid + "</asp:Label></th>" +
        "</tr>");
    //Cuerpo de la busqueda
    $("#tbBusqueda tbody").html("");
};

// Boton agregar de la ventana ayuda Transportista
AyudaAgregarTransportita = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            //Cargamos el objeto para el transportista Seleccionado
            transportistaSeleccionado.proveedorId = $(this).attr("proveedorID");
            transportistaSeleccionado.codigo = $(this).attr("organizacion");
            transportistaSeleccionado.descripcion = $(this).attr("descripcion");
            transportistaSeleccionado.correo = $(this).attr("Correo");
            ValidarCorreoTransportista();
        });
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionaTransportista,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

// Boton agregar de la ventana ayuda Transportista
AyudaAgregarRuta = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtNumRuta").val(rutaSeleccionada.rutaId = $(this).attr("ConfiguracionEmbarqueDetalleID"));
            $("#txtRuta").val(rutaSeleccionada.descripcion = $(this).attr("descripcion"));
            $("#txtKms").val(rutaSeleccionada.kilometros = $(this).attr("Kilometros"));
            rutaSeleccionada.Horas = $(this).attr("horas");
            rutaSeleccionada.Minutos = $(this).attr("minutos");
            ActualizarCitaDescargaTransporte();
            ValidaProveedorTieneRutaSeleccionada();
        });
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        $("#txtNumTransportista").focus();
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionaRuta,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

//Inicializar el listado de progrmación Embarque
InicializarGridTransporteEmbarque = function () {
    var recursos = RecursosBindTitulosTransporte();
    var listadoTransporteEmbarque = {};
    listadoTransporteEmbarque.Recursos = recursos;

    $('#GridTransporteEmbarque').html('');
    $('#GridTransporteEmbarque').setTemplateURL('../Templates/GridTransporteProgramacionEmbarque.htm');
    $('#GridTransporteEmbarque').processTemplate(listadoTransporteEmbarque);

    $('#TransporteEmbarque').dataTable({
        "oLanguage": {
            "oPaginate": {
                "sFirst": window.primeraPagina,
                "sLast": window.ultimaPagina,
                "sNext": window.siguiente,
                "sPrevious": window.anterior
            },
            "sEmptyTable": window.sinDatos,
            "sInfo": window.mostrando,
            "sInfoEmpty": window.sinInformacion,
            "sInfoFiltered": window.filtrando,
            "sLengthMenu": window.mostrar,
            "sLoadingRecords": window.cargando,
            "sProcessing": window.procesando,
            "sSearch": window.buscar,
            "sZeroRecords": window.sinRegistros,
            "sDom": '<"toolbar">frtip'
        }
    });
};

//Nombre de Titulos para el grid de Transporte Embarque
RecursosBindTitulosTransporte = function () {
    var recursos = {};
    recursos.cabeceroId = window.cabeceroId;
    recursos.cabeceroFolioEmbarque = window.cabeceroFolioEmbarque;
    recursos.cabeceroTransportista = window.cabeceroTransportista;
    recursos.cabeceroRuta = window.cabeceroRuta;
    recursos.cabeceroKilometros = window.cabeceroKilometros;
    recursos.cabeceroCitaDescarga = window.cabeceroCitaDescarga;
    recursos.cabeceroFlete = window.cabeceroFlete;
    recursos.cabeceroGastoFijo = window.cabeceroGastoFijo;
    recursos.cabeceroGastoVariable = window.cabeceroGastoVariable;
    recursos.cabeceroDemora = window.cabeceroDemora;
    recursos.cabeceroObservaciones = window.cabeceroObservaciones;
    recursos.cabeceroOpcion = window.cabeceroOpcion;
    return recursos;
};

//Valida si el proveedor seleccionado tiene correo
ValidarCorreoTransportista = function () {
    datos = { "proveedorId": transportistaSeleccionado.proveedorId };
    
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerPorIDConCorreo",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorConsultar,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d.Correo != '' && data.d.Correo != null) {
                    ValidarChoferTransportista();
                }
                else {
                    bootbox.alert({
                        message: window.msgValidaCorreoProveedor,
                        callback: function () {
                            setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                    $("#txtFlete").val("");
                    $("#txtGastoFijo").val("");
                }
            }
        }
    });
};

//Valida si el proveedor seleccionado tiene Chofer registrado
ValidarChoferTransportista = function () {
    datos = { "proveedorId": transportistaSeleccionado.proveedorId };

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerProveedorChoferPorProveedorId",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorConsultar,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            
            if (data.d != null) {
                
                if (data.d[0].ProveedorChoferID != null) {
                    ValidarJaulaTransportista();
                }

            } else {
                bootbox.alert({
                    message: window.msgValidaChoferProveedor,
                    callback: function () {
                       setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
                $("#txtNumTransportista").val("");
                $("#txtTransportista").val("");
                $("#txtFlete").val("");
                $("#txtGastoFijo").val("");
            }
        }
    });
};

//Valida si el proveedor seleccionado tiene Jaula registrada
ValidarJaulaTransportista = function () {
    datos = { "proveedorId": transportistaSeleccionado.proveedorId };

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerJaulaPorProveedorID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorConsultar,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            
            if (data.d != null) {

                if (data.d[0].JaulaID != null) {
                    ValidarTractoTransportista();
                }

            } else {
                bootbox.alert({
                    message: window.msgValidaJaulaTractoProveedor,
                    callback: function () {
                        setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
                $("#txtNumTransportista").val("");
                $("#txtTransportista").val("");
                $("#txtFlete").val("");
                $("#txtGastoFijo").val("");
            }
        }
    });
};

//Valida si el proveedor seleccionado tiene Tracto registrado
ValidarTractoTransportista = function () {
    datos = { "proveedorId": transportistaSeleccionado.proveedorId };

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerTractoPorProveedorID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorConsultar,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {

            if (data.d != null) {

                if (data.d[0].CamionID != null) {
                    ValidarConfiguracionOrigenDestinoTransportista();
                }

            } else {
                bootbox.alert({
                    message: window.msgValidaJaulaTractoProveedor,
                    callback: function () {
                        setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });

                $("#txtNumTransportista").val("");
                $("#txtTransportista").val("");
                $("#txtFlete").val("");
                $("#txtGastoFijo").val("");
            }
        }
    });
};

//Valida si el proveedor seleccionado tiene Origen-Destino Configurado
ValidarConfiguracionOrigenDestinoTransportista = function () {
    datos = {
        'embarque': {
            'Proveedor': {
                'ProveedorID': transportistaSeleccionado.proveedorId,
            },
            'OrganizacionOrigen': {
                'OrganizacionID': organizacionOrigen,
            },
            'OrganizacionDestino': {
                'OrganizacionID': organizacionDestino,
            } 
        }
    }

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerProveedorConfiguradoOrigenDestino",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorConsultar,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {

            if (data.d != null) {
                $("#txtNumTransportista").val(transportistaSeleccionado.codigo);
                $("#txtTransportista").val(transportistaSeleccionado.descripcion);
                $("#txtGastoVariable").focus();
                CargarCostoFlete();
            } else {
                bootbox.alert({
                    message: window.msgValidaConfiguracionOrigenDestinoProveedor,
                    callback: function () {
                        setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
                $("#txtNumTransportista").val("");
                $("#txtTransportista").val("");
                $("#txtFlete").val("");
                $("#txtGastoFijo").val("");
            }
        }
    });
};

//Actualizacion del campo CitaDescarga del embarque Selecionado
ActualizarCitaDescargaTransporte = function() {
    
    var fechaInt = fechaCitaCargaTransporte.replace('/Date(', '');
    fechaInt = fechaInt.replace(')/', '');
    
    var fecha = new Date(parseInt(fechaInt));
    fechaCitaDescargaTransporte = fecha;

    fecha.setHours(fecha.getHours() + parseInt(rutaSeleccionada.Horas));
    fecha.setMinutes(fecha.getMinutes() + parseInt(rutaSeleccionada.Minutos));
    var horas = fecha.getHours();
    if (fecha.getMinutes() < 10) {
        var minutos = "0" + fecha.getMinutes();
    } else {
        minutos = fecha.getMinutes();
    }

    var fechaFinal = ("0" + fecha.getDate()).slice(-2) + "/" + ("0" + (fecha.getMonth() + 1)).slice(-2) + "/" +
        fecha.getFullYear() + " " + horas + ":" + minutos;

    $("#txtCitaDescargaTransporte").val(fechaFinal);

};

CargarCostoFlete = function () {
        var datos = {
            "embarqueTarifa": {
                'ConfiguracionEmbarqueDetalle': {
                    'ConfiguracionEmbarqueDetalleID': rutaSeleccionada.rutaId
                },
                'Proveedor': {
                    'ProveedorID': transportistaSeleccionado.proveedorId
                }
            }
        };

        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerCostoFlete",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            async: true,
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgErrorConsultar,
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                var embarqueTarifa = data.d;
                if (embarqueTarifa != null) {
                    CargarGastosFijos();
                    $("#txtFlete").val(embarqueTarifa.Importe);
                }
            }
        });
};

ObtenerProgramacionEmbarqueTransporte = function(organizacionId, embarqueID, esEdicion) {
    var datos = { 'embarqueInfo': { 'Organizacion': { OrganizacionID: organizacionId }, EmbarqueId: embarqueID } }

    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerProgramacionTransporte',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {

            var resultado = msg.d;
            var contadorPendientes = 0;
            if (resultado != null) {
                if (resultado.length == 1 && esEdicion) {
                    HabilitarAyudasTransporte();
                    CargarCamposActualizarProgramacionEmbarqueTransporte(resultado[0]);
                } else {

                    //Calculo de los embarques pendientes
                    for (var i = 0; i < resultado.length; i++) {
                        if (resultado[i].PendienteTransporte) {
                            contadorPendientes = contadorPendientes + 1;
                        }
                    }

                    $('#lblTotal').text(contadorPendientes + " de " + resultado.length);

                    var recursos = RecursosBindTitulosTransporte();
                    var listadoProgramacionTransporteFinal = {};
                    listadoProgramacionTransporteFinal.ListadoProgramacionTransporte = resultado;
                    listadoProgramacionTransporteFinal.Recursos = recursos;
                    listadoProgramacionTransporteFinal.rolTransporteDatos = rolTransporteDatos;

                    $('#GridTransporteEmbarque').html('');

                    $('#GridTransporteEmbarque').setTemplateURL('../Templates/GridTransporteProgramacionEmbarque.htm');
                    $('#GridTransporteEmbarque').processTemplate(listadoProgramacionTransporteFinal);

                    $('#TransporteEmbarque').dataTable({

                        "oLanguage": {
                            "oPaginate": {
                                "sFirst": window.primeraPagina,
                                "sLast": window.ultimaPagina,
                                "sNext": window.siguiente,
                                "sPrevious": window.anterior
                            },
                            "sEmptyTable": window.sinDatos,
                            "sInfo": window.mostrando,
                            "sInfoEmpty": window.sinInformacion,
                            "sInfoFiltered": window.filtrando,
                            "sLengthMenu": window.mostrar,
                            "sLoadingRecords": window.cargando,
                            "sProcessing": window.procesando,
                            "sSearch": window.buscar,
                            "sZeroRecords": window.sinRegistros
                        }
                    });

                    if (rolTransporteDatos) {
                        $('div.dataTables_filter input').focus();
                    }
                }
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorCargarGridTransporte,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
};

BtnEditarTransportes_OnClick = function (embarqueID) {
    $("#btnAgregarTransporte").val("Actualizar");
    ObtenerProgramacionEmbarqueTransporte($("#txtNumOrganizacion").val(), embarqueID, true);
}

CargarCamposActualizarProgramacionEmbarqueTransporte = function (embarqueInfo) {
    $("#txtObservacionesTransporteCaptura").val("");
    //Se llena información para enviar el correo.
    embarqueCorreoInfo.EmbarqueID = embarqueInfo.EmbarqueID;

    // Validar si es guardado o edicion de informacion de transporte
    transportistaPendiente = embarqueInfo.PendienteTransporte ? true : false;
    transportistaSeleccionado.proveedorId = embarqueInfo.Proveedor.ProveedorID;
    transportistaSeleccionado.correo = embarqueInfo.Proveedor.Correo;
    transportistaSeleccionado.codigo = embarqueInfo.Proveedor.CodigoSAP;
    transportistaSeleccionado.descripcion = embarqueInfo.Proveedor.Descripcion;

    //Asiganamos los valores de origen-destino a variables globales
    organizacionOrigen = embarqueInfo.ConfiguracionEmbarque.OrganizacionOrigen.OrganizacionID;
    organizacionDestino = embarqueInfo.ConfiguracionEmbarque.OrganizacionDestino.OrganizacionID;
    transportistaSeleccionado.proveedorId = embarqueInfo.Proveedor.ProveedorID;
    datosCapturados = embarqueInfo.DatosCapturados;
    rutaSeleccionada.rutaId = embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueID;


    //Asignamos el campo fecha cita carga Global del embarque seleccionado
    fechaCitaCargaTransporte = embarqueInfo.CitaCarga;

    $("#txtIdTransporte").val(embarqueInfo.EmbarqueID);
    /*se cargar el embarque a variable global*/
    embarqueID = embarqueInfo.EmbarqueID;
    $("#txtFlete").val(embarqueInfo.Costos[0].Importe);
    $("#txtGastoVariable").val(embarqueInfo.Costos[1].Importe);
    $("#txtDemora").val(embarqueInfo.Costos[2].Importe);
    $("#txtKms").val(embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.Kilometros);
    $("#txtCitaDescargaTransporte").val(embarqueInfo.FechaCitaDescargaString);
    $("#txtGastoFijo").val(embarqueInfo.GastosFijos[0].Importe);
    $("#txtObservacionesTransporte").val(embarqueInfo.Observaciones);
    $("#cbxDobleTransportista").prop('checked', embarqueInfo.DobleTransportista);
    if (embarqueInfo.Proveedor.CodigoSAP != 0) {
        $("#txtNumTransportista").val(embarqueInfo.Proveedor.CodigoSAP);
        $("#txtTransportista").val(embarqueInfo.Proveedor.Descripcion);
    } else {
        $("#txtNumTransportista").val("");
        $("#txtTransportista").val("");
    }
    if (embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueID != 0) {
        $("#txtNumRuta").val(embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueID);
        $("#txtRuta").val(embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.Descripcion);
    } else {
        $("#txtNumRuta").val("");
        $("#txtRuta").val("");
    }

    $("#txtNumOrganizacion").attr("disabled", true);
    $("#btnAyudaOrganizacion").addClass("btn-disabled");
    $("#cbxDobleTransportista").focus();
}

/*Funcion para habilitar las ayudas de origen y destino*/
HabilitarAyudasTransporte = function () {
    $("#txtNumTransportista").prop("disabled", false);
    $("#btnTransportista").removeClass("btn-disabled");
    $("#txtNumRuta").prop("disabled", false);
    $("#btnRuta").removeClass("btn-disabled");
    HabilitarControlesTransporte();
};

/* Crear objeto para el guardado de la informacion de transporte del embarque */
CrearObjetoTransporteEmbarque = function () {

    var citaDescarga = $("#txtCitaDescargaTransporte").val().trim();
    var diaDescarga = citaDescarga.substring(0, 2);
    var mesDescarga = citaDescarga.substring(3, 5);
    var anoDescarga = citaDescarga.substring(6);
    citaDescarga = anoDescarga + "-" + mesDescarga + "-" + diaDescarga;

    if (fechaCitaDescargaTransporte != new Date()) {
        citaDescarga = fechaCitaDescargaTransporte;
    }

    // Objeto guardado
    if (transportistaPendiente) {
        embarqueInfo = {      
            'embarqueInfo': {
                'CitaDescarga': citaDescarga,
                'EmbarqueID': $("#txtIdTransporte").val(),
                'DobleTransportista': $("#cbxDobleTransportista").is(":checked"),
                'Observaciones': $("#txtObservacionesTransporteCaptura").val() === "" ? null : $("#txtObservacionesTransporteCaptura").val(),
                'Costos': [
                    {
                        // Flete
                        'Importe': $("#txtFlete").val()
                    },
                    {
                        // Gasto Variable 
                        'Importe': $("#txtGastoVariable").val()
                    },
                    {
                        // Demora
                        'Importe': $("#txtDemora").val()
                    }
                ],
                'ConfiguracionEmbarque': {
                    'ConfiguracionEmbarqueDetalle': {
                        'ConfiguracionEmbarqueDetalleID': $("#txtNumRuta").val(),
                        'Kilometros': $("#txtKms").val()
                    }
                },
                'Proveedor': {
                    'ProveedorID': transportistaSeleccionado.proveedorId
                },
                'UsuarioCreacionID': 0,
                'Ruteo': {
                    'OrganizacionOrigen': {
                        'OrganizacionID': organizacionOrigen
                    },
                    'OrganizacionDestino': {
                        'OrganizacionID': organizacionDestino
                    }
                }
            }
        };
    } else {
        // Objeto edicion
        embarqueInfo = {
            'embarqueInfo': {
                'CitaDescarga': citaDescarga,
                'EmbarqueID': $("#txtIdTransporte").val(),
                'DobleTransportista': $("#cbxDobleTransportista").is(":checked"),
                'Observaciones': $("#txtObservacionesTransporteCaptura").val() === "" ? null : $("#txtObservacionesTransporteCaptura").val(),
                'Costos': [
                    {
                        // Flete
                        'Importe': $("#txtFlete").val()
                    },
                    {
                        // Gasto Variable 
                        'Importe': $("#txtGastoVariable").val()
                    },
                    {
                        // Demora
                        'Importe': $("#txtDemora").val()
                    }
                ],
                'ConfiguracionEmbarque': {
                    'ConfiguracionEmbarqueDetalle': {
                        'ConfiguracionEmbarqueDetalleID': $("#txtNumRuta").val(),
                        'Kilometros': $("#txtKms").val()
                    }
                },
                'Proveedor': {
                    'ProveedorID': transportistaSeleccionado.proveedorId
                },
                'UsuarioCreacionID': -1,
                'UsuarioModificacionID': 0,
                'Ruteo': {
                    'OrganizacionOrigen': {
                        'OrganizacionID': organizacionOrigen
                    },
                    'OrganizacionDestino': {
                        'OrganizacionID': organizacionDestino
                    }
                }
            }
        };
    }
    return embarqueInfo;
}

EnviarCorreoTransportista = function (transportista, descripcion) {
    transportista.CodigoSAP = transportista.codigo;
    embarqueCorreoInfo.Proveedor = transportista;
    var datos = { 'embarqueInfo': embarqueCorreoInfo, 'organizacionID': $("#txtNumOrganizacion").val() };
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/EnviarCorreo",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            bootbox.alert({
                message: window.msgErrorEnviarCorreo,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
            DesbloquearPantalla();
            $("#btnAgregarTransporte").focus();
        },
        dataType: "json",
        success: function (data) {
            DesbloquearPantalla();
            MensajeInformativo(window.msgExitoGuardar, 2);
        }
    });
}

EliminarInformacionSeccionDatos = function (seccion) {
    var datos = { embarqueInfo: { EmbarqueID: embarqueID } };
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/EliminarInformacionDatos",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        dataType: "json",
        success: function (data) {
            Guardar(embarqueInfo, seccion);
        },
        error: function (request, status, error) {
            bootbox.alert({
                message: window.msgErrorEliminar,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

LimpiarPantallaTransporte = function() {
    $("#txtIdTransporte").val("");
    $("#txtFlete").val("");
    $("#txtGastoVariable").val("");
    $("#txtDemora").val("");
    $("#txtKms").val("");
    $("#txtCitaDescargaTransporte").val("");
    $("#txtGastoFijo").val("");
    $("#txtObservacionesTransporte").val("");
    $("#txtObservacionesTransporteCaptura").val("");
    $("#cbxDobleTransportista").prop('checked', false);
    $("#txtNumTransportista").val("");
    $("#txtTransportista").val("");
    $("#txtNumRuta").val("");
    $("#txtRuta").val("");
};

DeshabilitarControlesTransporte = function() {
    $("#txtIdTransporte").attr("disabled",true);
    $("#txtFlete").attr("disabled", true);
    $("#txtGastoVariable").attr("disabled", true);
    $("#txtDemora").attr("disabled", true);
    $("#txtKms").attr("disabled", true);
    $("#txtCitaDescargaTransporte").attr("disabled", true);
    $("#txtGastoFijo").attr("disabled", true);
    $("#txtObservacionesTransporteCaptura").attr("disabled", true);
    $("#cbxDobleTransportista").attr("disabled", true);
    $("#txtNumTransportista").attr("disabled", true);
    $("#txtTransportista").attr("disabled", true);
    $("#txtNumRuta").attr("disabled", true);
    $("#txtRuta").attr("disabled", true);
    $("#btnTransportista").addClass("btn-disabled");
    $("#btnRuta").addClass("btn-disabled");
    $("#btnAgregarTransporte").attr("disabled", true);
    $("#btnCancelarTransporte").attr("disabled", true);
}

HabilitarControlesTransporte = function () {
    $("#txtObservacionesTransporteCaptura").prop("disabled", false);
    $("#txtGastoVariable").prop("disabled", false);
    $("#txtDemora").prop("disabled", false);
    $("#cbxDobleTransportista").prop("disabled", false);
    $("#btnAgregarTransporte").attr("disabled", false);
    $("#btnCancelarTransporte").attr("disabled", false);
}

ValidaCamposTransporte = function() {
    if ($("#txtRuta").val() == '') {
        bootbox.alert({
            message: window.msgSeleccionaRuta,
            callback: function () {
                $("#txtNumRuta").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    } else if ($("#txtTransportista").val() == '') {
        bootbox.alert({
            message: window.msgSeleccionaTransportista,
            callback: function () {
                $("#txtNumTransportista").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    } else if ($("#txtKms").val() == '0') {
        bootbox.alert({
            message: window.msgSeleccionaKms,
            callback: function () {
                $("#txtKms").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    } else if ($("#txtCitaDescargaTransporte").val() == '') {
        bootbox.alert({
            message:window.msgSeleccionaCitaDescarga,
            callback: function () {
                $("#txtCitaDescargaTransporte").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    }
    else if ($("#txtFlete").val() == '0') {
        bootbox.alert({
            message: window.msgValidaFlete,
            callback: function () {
                $("#txtFlete").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    }
    else if ($("#txtGastoFijo").val() == '0') {
        bootbox.alert({
            message: window.msgValidaGastoFijo,
            callback: function () {
                $("#txtGastoFijo").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    }
    return true;
};

/* Metodo que valida si el embarque tiene informacion capturada 
 * en la seccion se datos
 */
ValidarProgramacionDatosCapturados = function (seccion) {
    if (datosCapturados) {
        bootbox.dialog({
            message: window.msgValidaProgramacionDatosCapturados,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        if (seccion == 1) {
                            embarqueInfo = CrearObjetoProgramacionEmbarque();
                            ValidarEstatus(embarqueInfo, seccion);
                        } else if (seccion == 2) {
                            ValidarEstatus(CrearObjetoTransporteEmbarque(), seccion);
                        }
                        
                        if (!estatusValidado) {
                            EliminarInformacionSeccionDatos(seccion);
                        }
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    className: 'SuKarne btn-primary',
                    callback: function() {
                        return true;
                    }
                }
            }
        });
    } else {
        return true;
    }
}

CargarGastosFijos = function () {
    var datos = {
        "embarqueInfo": {
            'ConfiguracionEmbarque': {
                'ConfiguracionEmbarqueDetalle': {
                    'ConfiguracionEmbarqueDetalleID': rutaSeleccionada.rutaId
                }
            },
            'Proveedor': {
                'ProveedorID': transportistaSeleccionado.proveedorId
            }
        }
    };

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerGastosFijos",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgErrorGastosFijos,
                    callback: function() {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
            });
            }
        },
        dataType: "json",
        success: function (data) {
            var gastoFijo = data.d;
            if (gastoFijo != null) {
                $("#txtGastoFijo").val(gastoFijo.Importe);
            }
        }
    });
};

//Función para bloquear la pantalla mientras se ejecuta una operación
BloquearPantalla = function () {
    var lock = document.getElementById('skm_LockPane');
    if (lock) {
        lock.className = 'LockOn';
        $('#skm_LockPane').spin(
            {
                top: '30',
                color: '#6E6E6E'
            });
    }
};

//Función para desbloquear la pantalla mientras se ejecuta una operación
DesbloquearPantalla = function () {
    $("#skm_LockPane").spin(false);
    var lock = document.getElementById('skm_LockPane');
    lock.className = 'LockOff';
};

//-----------------------------------------------------------------
// Funciones de la pestaña de Datos
//-----------------------------------------------------------------

InicializarDatos = function() {
    $("#txtNumOperador1").numericInput().attr("maxlength", "4");
    $("#txtNumOperador2").numericInput().attr("maxlength", "4");
    $("#cbxDobleTransportista").keydown(function(event) {
        var code = event.keyCode || event.which;
        if (code == 13) {
            event.preventDefault();
        }
    });
}

AsignarEventosControlesDatos = function () {

    //Dar click en la pestaña de datos embarque
    $('a[href="#TapGridDatos"]').on('shown.bs.tab', function (e) {
        $("#divCapturaDatos").hide();
        $("#divCapturaDatosEmbarqueTransporte").hide();
        $("#divCapturaDatosEmbarqueDatos").show();
    });

    // Al capturar el codigo del Transportista
    $('#txtNumOperador1').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumOperador1').val() != "") {
                e.preventDefault();
                $('#btnOperador1').focus();
            }
        }
    });

    // Al capturar el codigo del Transportista
    $('#txtNumOperador2').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#txtNumOperador2').val() != "") {
                e.preventDefault();
                ObtenerOperador2();
            }
        }
    });

    // Al perder el foco el textbox del indentificador del Operador 1
    $("#txtNumOperador1").focusout(function () {
        if ($('#txtNumOperador1').val() == "") {
            $("#txtOperador1").val("");
        }
        else {
            ObtenerOperador1();
        }
    });

    // Al perder el foco el textbox del indentificador del Operador 2
    $("#txtNumOperador2").focusout(function () {
        if ($('#txtNumOperador2').val() == "") {
            $("#txtOperador2").val("");
        }
        else {
            ObtenerOperador2();
        }
    });

    // Boton que abre la ayuda para Transportista
    $("#btnOperador1").click(function () {
        ObtenerOperadores1();
    });

    // Boton que abre la ayuda para Transportista
    $("#btnOperador2").click(function () {
        ObtenerOperadores2();
    });

    //Boton para cancelar en la captura de datos
    $("#btnCancelarDatos").click(function () {
        bootbox.dialog({
            message: window.msgCancelar,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        $("#requeridoOperador2").show();
                        $("#txtNumOrganizacion").attr("disabled", false);
                        $("#btnAyudaOrganizacion").removeClass("btn-disabled");
                        LimpiarCamposDatos();
                        DeshabilitarCapturaDeDatos();
                        $("#btnGuardarDatos").val("Agregar");
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    className: 'SuKarne btn-primary',
                    callback: function () {
                        return true;
                    }
                }
            }
        });
    });

    $("#txtPlacasJaula").keypress(function(event) {
        var code = event.which || event.keyCode;
        if (code == 13) {
            event.preventDefault();
            $("#btnPlacasJaula").focus();
        }
    });

    $("#txtPlacasTracto").keypress(function (event) {
        var code = event.which || event.keyCode;
        if (code == 13) {
            event.preventDefault();
            $("#btnPlacasTracto").focus();
        }
    });

};

//Inicializar el listado de progrmación Embarque
InicializarGridDatosEmbarque = function () {
    var recursos = RecursosBindTitulosDatos();
    var listadoDatosEmbarque = {};
    listadoDatosEmbarque.Recursos = recursos;

    $('#GridDatosEmbarque').html('');
    $('#GridDatosEmbarque').setTemplateURL('../Templates/GridDatosProgramacionEmbarque.htm');
    $('#GridDatosEmbarque').processTemplate(listadoDatosEmbarque);

    $('#DatosEmbarque').dataTable({
        "oLanguage": {
            "oPaginate": {
                "sFirst": window.primeraPagina,
                "sLast": window.ultimaPagina,
                "sNext": window.siguiente,
                "sPrevious": window.anterior
            },
            "sEmptyTable": window.sinDatos,
            "sInfo": window.mostrando,
            "sInfoEmpty": window.sinInformacion,
            "sInfoFiltered": window.filtrando,
            "sLengthMenu": window.mostrar,
            "sLoadingRecords": window.cargando,
            "sProcessing": window.procesando,
            "sSearch": window.buscar,
            "sZeroRecords": window.sinRegistros
        }
    });

};

//Obtiene el operador 1 capturado por codigo
ObtenerOperador1 = function () {

    if ($("#txtNumOperador1").val() != "") {
        datos = {
            "proveedorId": idTransportista,
            "choferId": $("#txtNumOperador1").val()
        };
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerChofer",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message:window.msgSinOperadorValido,
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumOperador1").val("");
                }
            },
            dataType: "json",
            async: false,
            success: function (data) {
                
                var operador1 = data.d;
                if (operador1 != null) {
                    if (operador1.length > 0) {
                        if (ValidaAyudaOperador1(operador1[0].Chofer.ChoferID)) {
                            if (!$("#txtNumOperador2").prop("disabled")) {
                                $("#txtNumOperador2").focus();
                            } else {
                                $("#txtPlacasTracto").focus();
                            }
                           
                            $("#txtOperador1").val(operador1[0].Chofer.NombreCompleto);                        
                        }
                    } else {
                        $("#txtNumOperador1").val("");
                        $("#txtOperador1").val("");
                        bootbox.alert({
                            message: window.msgSinOperadorValido,
                            callback: function () {
                                setTimeout(function () { $("#txtNumOperador1").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }
                   
                } else {
                    $("#txtNumOperador1").val("");
                    $("#txtOperador1").val("");
                    bootbox.alert({
                        message: window.msgSinOperadorValido,
                        callback: function () {
                            setTimeout(function () { $("#txtNumOperador1").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Obtiene el operador 2 capturado por codigo
ObtenerOperador2 = function () {

    if ($("#txtNumOperador12").val() != "") {
        datos = {
            "proveedorId": idTransportista,
            "choferId": $("#txtNumOperador2").val()
        };
        $.ajax({
            type: "POST",
            url: "ProgramacionEmbarque.aspx/ObtenerChofer",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            async: false,
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert({
                        message: window.msgSinOperadorValido,
                        callback: function () {
                            msjAbierto = 0;
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumOperador2").val("");
                }
            },
            dataType: "json",
            success: function (data) {

                var operador2 = data.d;
                if (operador2 != null) {
                    if (operador2.length > 0) {
                        if (ValidaAyudaOperador2(operador2[0].Chofer.ChoferID)) {
                            $("#txtOperador2").val(operador2[0].Chofer.NombreCompleto);
                            $("#txtPlacasTracto").focus();
                        }
                    } else {
                        $("#txtNumOperador2").val("");
                        $("#txtOperador2").val("");
                        bootbox.alert({
                            message: window.msgSinOperadorValido,
                            callback: function () {
                                setTimeout(function () { $("#txtNumOperador2").focus(); }, 500);
                            },
                            closeButton: false,
                            buttons: {
                                ok: {
                                    label: window.Aceptar,
                                    className: 'SuKarne btn-primary'
                                }
                            }
                        });
                    }

                } else {
                    $("#txtNumOperador2").val("");
                    $("#txtOperador2").val("");
                    bootbox.alert({
                        message: window.msgSinOperadorValido,
                        callback: function () {
                            setTimeout(function () { $("#txtNumOperador2").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
            }
        });
    }
};

//Abre modal y obtiene los datos del Operador 1
ObtenerOperadores1 = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = {
            "nombre": $("#txtOrganizacionBuscar").val(),
            "proveedorId": idTransportista
        };
    } else {
        datos = {
            "nombre": "",
            "proveedorId": idTransportista
        };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerProveedorOperador",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgValidaChoferProveedor,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //Titulo de la ayuda
                $("#lblTituloDialogo").html("");
                $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaOperador1 + "</asp:Label>");
                //opciones de la ayuda de Transportista
                ayudaDesplegada = 6;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblNombreOperador + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerOperadores1();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarOperador1();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Encabezado de busqueda
                $("#tbBusquedaEncabezado thead").html("");
                $("#tbBusquedaEncabezado thead").append("<tr>" +
                    "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblNombre + "</asp:Label></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4' runat='server'>" + lblApellidoPaterno + "</asp:Label></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblApellidoMaterno + "</asp:Label></th>" +
                    "</tr>");
                //Contenido de la busqueda
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='choferID" + resultado[i].Chofer.ChoferID + "' " +
                        "choferID='" + resultado[i].Chofer.ChoferID + "' nombre='" + resultado[i].Chofer.NombreCompleto + "' onclick='SeleccionaUno(\"#choferID" + resultado[i].Chofer.ChoferID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.Nombre + "</td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.ApellidoPaterno + "</td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.ApellidoMaterno + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgValidaChoferProveedor,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

//Abre modal y obtiene los datos del Operador 1
ObtenerOperadores2 = function () {
    var datos = {};
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = {
            "nombre": $("#txtOrganizacionBuscar").val(),
            "proveedorId": idTransportista
        };
    } else {
        datos = {
            "nombre": "",
            "proveedorId": idTransportista
        };
    }
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerProveedorOperador",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgValidaChoferProveedor,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //Titulo de la ayuda
                $("#lblTituloDialogo").html("");
                $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaOperador2 + "</asp:Label>");
                //opciones de la ayuda de Transportista
                ayudaDesplegada = 7;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblNombreOperador + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerOperadores2();' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarOperador2();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Encabezado de busqueda
                $("#tbBusquedaEncabezado thead").html("");
                $("#tbBusquedaEncabezado thead").append("<tr>" +
                    "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblNombre + "</asp:Label></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4' runat='server'>" + lblApellidoPaterno + "</asp:Label></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblApellidoMaterno + "</asp:Label></th>" +
                    "</tr>");
                //Contenido de la busquedar
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='choferID" + resultado[i].Chofer.ChoferID + "' choferID='" + resultado[i].Chofer.ChoferID + "' nombre='" + resultado[i].Chofer.NombreCompleto + "' onclick='SeleccionaUno(\"#choferID" + resultado[i].Chofer.ChoferID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.Nombre + "</td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.ApellidoPaterno + "</td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].Chofer.ApellidoMaterno + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgValidaChoferProveedor,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

// Boton agregar de la ventana ayuda Transportista
AyudaAgregarOperador1 = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            if (ValidaAyudaOperador1($(this).attr("choferID"))) {
                $("#txtNumOperador1").val($(this).attr("choferID"));
                $("#txtOperador1").val($(this).attr("nombre"));
                $("#dlgBusquedaOrganizacion").modal("hide");
                $("#txtOrganizacionBuscar").val("");
                // Cambiar focus a campo tracto, si el operador 2
                // está deshabilitado
                if ($("#txtNumOperador2").prop("disabled")) {
                    $("#txtPlacasTracto").focus();
                } else {
                    $("#txtNumOperador2").focus();
                }            
            }
        });        
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionaOperador1,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

// Boton agregar de la ventana ayuda Transportista
AyudaAgregarOperador2 = (function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            if (ValidaAyudaOperador2($(this).attr("choferID"))) {
                $("#txtNumOperador2").val($(this).attr("choferID"));
                $("#txtOperador2").val($(this).attr("nombre"));
                $("#dlgBusquedaOrganizacion").modal("hide");
                $("#txtOrganizacionBuscar").val("");
                $("#txtPlacasTracto").focus();
            }
        });
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionaOperador2,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
});

//Valida si el operador ya esta seleccionado
ValidaAyudaOperador1 = function(id) {
    if (id != $("#txtNumOperador2").val()) {
        return true;
    }
    else {
        bootbox.alert({
            message: window.msgValidaOperador1Igual,
            callback: function () {
                setTimeout(function () { $("#txtNumOperador1").focus(); }, 500);
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        $("#txtNumOperador1").val("");
        $("#txtOperador1").val("");
        return false;
    }
};

//Valida si el operador ya esta seleccionado
ValidaAyudaOperador2 = function (id) {
    if (id != $("#txtNumOperador1").val()) {
        return true;
    }
    else {
        bootbox.alert({
            message: window.msgValidaOperador2Igual,
            callback: function () {
                setTimeout(function () { $("#txtNumOperador2").focus(); }, 500);
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        $("#txtNumOperador2").val("");
        $("#txtOperador2").val("");
        return false;
    }
};

//Valida si el chofer seleccionado es valido
ValidarChofer = function () {
    datos = { "proveedorId": transportistaSeleccionado.proveedorId };
    
    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerPorIDConCorreo",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: true,
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert({
                    message: window.msgSinOrganizacionOrigenValida,
                    callback: function () {
                        msjAbierto = 0;
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d != null) {
                if (data.d.Correo > 0) {
                    ValidarChoferTransportista();
                }
                else {
                    bootbox.alert({
                        message: window.msgValidaCorreoProveedor,
                        callback: function () {
                            setTimeout(function () { $("#txtNumTransportista").focus(); }, 500);
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                    $("#txtNumTransportista").val("");
                    $("#txtTransportista").val("");
                }
            }
        }
    });
};

//Nombre de Titulos para el grid de Transporte Embarque
RecursosBindTitulosDatos = function () {
    var recursos = {};
    recursos.cabeceroId = window.cabeceroId;
    recursos.cabeceroFolioEmbarque = window.cabeceroFolioEmbarque;
    recursos.cabeceroOperador = window.cabeceroOperador;
    recursos.cabeceroPlacaTracto = window.cabeceroPlacaTracto;
    recursos.cabeceroPlacaJaula = window.cabeceroPlacaJaula;
    recursos.cabeceroEcoTracto = window.cabeceroEcoTracto;
    recursos.cabeceroEcoJaula = window.cabeceroEcoJaula;
    recursos.cabeceroObservaciones = window.cabeceroObservaciones;
    recursos.cabeceroOpcion = window.cabeceroOpcion;
    return recursos;
};

ObtenerProgramacionEmbarqueDatos = function(organizacionId, embarqueId, esEdicion) {
    var datos = { 'embarqueInfo': { 'Organizacion': { OrganizacionID: organizacionId }, EmbarqueId: embarqueId } }
    $.ajax({
        type: "POST",
        url: 'ProgramacionEmbarque.aspx/ObtenerProgramaciondatos',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var resultado = msg.d;
            var contadorPendientes = 0;
            if (resultado != null && resultado.length > 0) {
                if (resultado.length == 1 && esEdicion) {
                    CargarCamposActualizarProgramacionEmbarqueDatos(resultado[0]);
                } else {
                    for (var i = 0; i < resultado.length; i++) {
                        if (resultado[i].DobleTransportista && resultado[i].Operador2.ChoferID && resultado[i].Operador2.Nombre != '') {
                            resultado[i].Operador1.Nombre = resultado[i].Operador1.Nombre + ' / ' + resultado[i].Operador2.Nombre;
                        }
                    }

                    //Calculo de los embarques pendientes
                    for (var i = 0; i < resultado.length; i++) {
                        if (resultado[i].DatosCapturados) {
                            contadorPendientes = contadorPendientes + 1;
                        }
                    }

                    $('#lblTotalDatos').text(contadorPendientes + " de " + resultado.length);

                    var recursos = RecursosBindTitulosDatos();
                    var listadoProgramacionDatosFinal = {};
                    listadoProgramacionDatosFinal.ListadoProgramacionDatos = resultado;
                    listadoProgramacionDatosFinal.Recursos = recursos;
                    listadoProgramacionDatosFinal.rolTransporteDatos = rolTransporteDatos;

                    $('#GridDatosEmbarque').html('');

                    $('#GridDatosEmbarque').setTemplateURL('../Templates/GridDatosProgramacionEmbarque.htm');
                    $('#GridDatosEmbarque').processTemplate(listadoProgramacionDatosFinal);

                    $('#DatosEmbarque').dataTable({
                        "oLanguage": {
                            "oPaginate": {
                                "sFirst": window.primeraPagina,
                                "sLast": window.ultimaPagina,
                                "sNext": window.siguiente,
                                "sPrevious": window.anterior
                            },
                            "sEmptyTable": window.sinDatos,
                            "sInfo": window.mostrando,
                            "sInfoEmpty": window.sinInformacion,
                            "sInfoFiltered": window.filtrando,
                            "sLengthMenu": window.mostrar,
                            "sLoadingRecords": window.cargando,
                            "sProcessing": window.procesando,
                            "sSearch": window.buscar,
                            "sZeroRecords": window.sinRegistros
                        }
                    });

                    if (rolTransporteDatos) {
                        $('div.dataTables_filter input').focus();
                    }
                }
            } else {
                InicializarGridDatosEmbarque();
            }
        },
        error: function () {
            bootbox.alert({
                message: window.msgErrorCargarGridDatos,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
}

BtnEditarDatos_OnClick = function (embarqueID) {
    ObtenerProgramacionEmbarqueDatos($("#txtNumOrganizacion").val(), embarqueID, true);
}

CargarCamposActualizarProgramacionEmbarqueDatos = function (embarque) {
    idTransportista = embarque.Proveedor.ProveedorID;
    dobleTransportista = embarque.DobleTransportista;
    $("#txtObservacionesDatosCaptura").val("");
    if (idTransportista > 0) {
        $("#txtIdDatos").val(embarque.EmbarqueID);
        if (embarque.Operador1.ChoferID != 0 && embarque.Operador1.Nombre != "") {
            $("#txtNumOperador1").val(embarque.Operador1.ChoferID);
            $("#txtOperador1").val(embarque.Operador1.Nombre);
            // Variable que indica si es guardado e edicion de la informacion
            // ingresada en la pestaña de datos
            esEdicionDatos = true;
        } else {
            $("#txtNumOperador1").val("");
            $("#txtOperador1").val("");
            esEdicionDatos = false;
        }
        if (embarque.DobleTransportista && embarque.Operador2.ChoferID != 0 && embarque.Operador2.Nombre != "") {
            $("#txtNumOperador2").val(embarque.Operador2.ChoferID);
            $("#txtOperador2").val(embarque.Operador2.Nombre);
        } else {
            $("#txtNumOperador2").val("");
            $("#txtOperador2").val("");
        }
        $("#txtPlacasTracto").val(embarque.Tracto.PlacaCamion);
        $("#txtPlacasJaula").val(embarque.Jaula.PlacaJaula);
        $("#txtEcoTracto").val(embarque.Tracto.Economico);
        $("#txtEcoJaula").val(embarque.Jaula.NumEconomico);
        $("#txtObservacionesDatos").val(embarque.Observaciones);
        HabilitarCapturaDeDatos();
        $("#txtNumOrganizacion").attr("disabled", true);
        $("#btnAyudaOrganizacion").addClass("btn-disabled");
        $("#txtNumOperador1").focus();
        $("#btnGuardarDatos").val("Actualizar");
    } else {
        bootbox.alert({
            message: window.msgNoTieneTransportista,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
    }
};

HabilitarCapturaDeDatos = function() {
    $("#txtNumOperador1").attr("disabled", false);
    $("#btnOperador1").removeClass("btn-disabled");
    if (dobleTransportista) {
        $("#requeridoOperador2").show();
        $("#txtNumOperador2").attr("disabled", false);
        $("#btnOperador2").removeClass("btn-disabled");
    } else {
        $("#requeridoOperador2").hide();
        $("#txtNumOperador2").attr("disabled", true);
        $("#btnOperador2").addClass("btn-disabled");
    }
    $("#txtPlacasTracto").attr("disabled", false);
    $("#btnPlacasTracto").removeClass("btn-disabled");
    $("#txtPlacasJaula").attr("disabled", false);
    $("#btnPlacasJaula").removeClass("btn-disabled");
    $("#txtObservacionesDatosCaptura").attr("disabled", false);
    $("#btnGuardarDatos").attr("disabled", false);
    $("#btnCancelarDatos").attr("disabled", false);
};

DeshabilitarCapturaDeDatos = function() {
    $("#txtNumOperador1").attr("disabled", true);
    $("#btnOperador1").addClass("btn-disabled");
    $("#txtNumOperador2").attr("disabled", true);
    $("#btnOperador2").addClass("btn-disabled");
    $("#txtPlacasTracto").attr("disabled", true);
    $("#btnPlacasTracto").addClass("btn-disabled");
    $("#txtPlacasJaula").attr("disabled", true);
    $("#btnPlacasJaula").addClass("btn-disabled");
    $("#txtObservacionesDatosCaptura").attr("disabled", true);
    $("#btnGuardarDatos").attr("disabled", true);
    $("#btnCancelarDatos").attr("disabled", true);
};

LimpiarCamposDatos = function () {
    $("#txtIdDatos").val("0");
    $("#txtNumOperador1").val("");
    $("#txtOperador1").val("");
    $("#txtNumOperador2").val("");
    $("#txtOperador2").val("");
    $("#txtPlacasTracto").val("");
    $("#txtPlacasJaula").val("");
    $("#txtEcoTracto").val("");
    $("#txtEcoJaula").val("");
    $("#txtObservacionesDatos").val("");
    $("#txtObservacionesDatosCaptura").val("");
}

ObtenerTractoPorProveedorID = function (transportistaId) {
    var datos = { 'camionInfo': { 'Proveedor': { 'ProveedorID': transportistaId } } }

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerTractosPorProveedorID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //Titulo de la ayuda
                $("#lblTituloDialogo").html("");
                $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaTracto_Titulo + "</asp:Label>");
                //Opciones de la ayuda de tractos
                ayudaDesplegada = 8;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblCamionID + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerTractoPorDescripcion(" + resultado[0].Proveedor.ProveedorID + "," +
                                $("#txtOrganizacionBuscar").val() +","+true+ ");' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarTracto();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Encabezado de busqueda
                $("#tbBusquedaEncabezado thead").html("");
                $("#tbBusquedaEncabezado thead").append("<tr>" +
                    "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
                    "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblPlacaTracto + "</asp:Label></th>" +
                    "</tr>");
                //Contenido de la busqueda
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='camion" 
                        + resultado[i].CamionID + "' proveedorID='" + resultado[i].Proveedor.ProveedorID + "' numEconomico='"+ resultado[i].Economico +
                        "' placaTracto='" + resultado[i].PlacaCamion + "' onclick='SeleccionaUno(\"#camion" +
                        resultado[i].CamionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].CamionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].PlacaCamion + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgSinTractos,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

AyudaAgregarTracto = function() {
    var renglones = $("input[class=organizaciones]:checked"); 

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtPlacasTracto").val($(this).attr("placaTracto"));
            $("#txtEcoTracto").val($(this).attr("numEconomico"));
        });
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionarCamion,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
}

ObtenerTractoPorDescripcion = function (transportistaId, placaTracto, esAyuda) {
    if (placaTracto === undefined && $("#txtOrganizacionBuscar").val().trim() == '') {
        ObtenerTractoPorProveedorID(transportistaId);
        return;
    } else  if ($("#txtOrganizacionBuscar").val() != ''){
        placaTracto = $("#txtOrganizacionBuscar").val();
    }
    var datos = { 'camionInfo': { 'PlacaCamion': placaTracto, 'Proveedor': { 'ProveedorID': transportistaId } } }

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerTractosPorDescripcion",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: false,
        error: function (request) {

        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                if (resultado.CamionID == 0) {
                    bootbox.alert({
                        message: window.msgTractoInvalido,
                        callback: function () {
                            $("#txtPlacasTracto").val("");
                            $("#txtPlacasTracto").focus();
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                }
                if (esAyuda) {
                    //Titulo de la ayuda
                    $("#lblTituloDialogo").html("");
                    $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaTracto_Titulo + "</asp:Label>");
                    //Opciones de la ayuda de tractos
                    ayudaDesplegada = 8;
                    $("#OpcionesAyuda").html("");
                    $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblCamionID + "  </asp:Label>" +
                        "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                        "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerTractoPorDescripcion(" + resultado.Proveedor.ProveedorID + "," +
                        $("#txtOrganizacionBuscar").val() +","+true+ ");' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                        "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarTracto();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                        "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                    //Encabezado de busqueda
                    $("#txtOrganizacionBuscar").val(placaTracto);
                    $("#tbBusquedaEncabezado thead").html("");
                    $("#tbBusquedaEncabezado thead").append("<tr>" +
                        "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                        "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
                        "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblPlacaTracto + "</asp:Label></th>" +
                        "</tr>");
                    //Contenido de la busqueda
                    $("#tbBusqueda tbody").html("");
                        $("#tbBusqueda tbody").append("<tr>" +
                            "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='camion"
                            + resultado.CamionID + "' proveedorID='" + resultado.Proveedor.ProveedorID + "' numEconomico='" + resultado.Economico +
                            "' placaTracto='" + resultado.PlacaCamion + "' onclick='SeleccionaUno(\"#organizacion" +
                            resultado.CodigoSAP + "\");'/></td>" +
                            "<td class='alineacionCentro' style='width: 100px;'>" + resultado.CamionID + "</td>" +
                            "<td class='alineacionCentro' style='width: auto;'>" + resultado.PlacaCamion + "</td>" +
                            "</tr>");
                    setTimeout(function() {
                        $("#txtOrganizacionBuscar").focus();
                    }, 500);
                    $("#dlgBusquedaOrganizacion").modal("show");
                } else {
                    $("#txtPlacasTracto").val(resultado.PlacaCamion);
                    $("#txtEcoTracto").val(resultado.Economico);
                }
            }
            else {
                bootbox.alert({
                    message: window.msgTractoInvalido,
                    callback: function () {
                        $("#txtPlacasTracto").val("");
                        $("#txtEcoTracto").val("");
                        $("#txtPlacasTracto").focus();
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
}

ObtenerJaulaPorProveedorID = function (transportistaId) {
    var datos = { 'jaulaInfo': { 'Proveedor': { 'ProveedorID': transportistaId } } }

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerJaulasPorProveedorID",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {

        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                //Titulo de la ayuda
                $("#lblTituloDialogo").html("");
                $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaJaula_Titulo + "</asp:Label>");
                //Opciones de la ayuda de tractos
                ayudaDesplegada = 9;
                $("#OpcionesAyuda").html("");
                $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblPlacaJaula + "  </asp:Label>" +
                                "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                                "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerJaulaPorDescripcion(" + resultado[0].Proveedor.ProveedorID + "," +
                                $("#txtOrganizacionBuscar").val() + "," + true + ");' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                                "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarJaula();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                                "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                //Encabezado de busqueda
                $("#tbBusquedaEncabezado thead").html("");
                $("#tbBusquedaEncabezado thead").append("<tr>" +
                    "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                    "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
                    "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblPlacaTracto + "</asp:Label></th>" +
                    "</tr>");
                //Contenido de la busqueda
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='jaula"
                        + resultado[i].JaulaID + "' proveedorID='" + resultado[i].Proveedor.ProveedorID + "' numEconomico='" + resultado[i].NumEconomico +
                        "' placaJaula='" + resultado[i].PlacaJaula + "' onclick='SeleccionaUno(\"#jaula" +
                        resultado[i].JaulaID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].JaulaID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].PlacaJaula + "</td>" +
                        "</tr>");
                }
                AsignarEventosModalBusqueda();
                setTimeout(function () { $("#txtOrganizacionBuscar").val(""); $("#txtOrganizacionBuscar").focus(); }, 500);
                $("#dlgBusquedaOrganizacion").modal("show");
            }
            else {
                bootbox.alert({
                    message: window.msgSinJaulas,
                    callback: function () {
                        setTimeout(function () { $("#txtOrganizacion").val(""); $("#txtOrganizacion").focus(); }, 500);
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
};

AyudaAgregarJaula = function () {
    var renglones = $("input[class=organizaciones]:checked");

    if (renglones.length > 0) {
        renglones.each(function () {
            $("#txtPlacasJaula").val($(this).attr("placaJaula"));
            $("#txtEcoJaula").val($(this).attr("numEconomico"));
        });
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
        $("#txtObservacionesDatosCaptura").focus();
        ayudaDesplegada = 0;
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            $("#dlgBusquedaOrganizacion").modal("hide");
            bootbox.alert({
                message: window.msgSeleccionaJaula,
                callback: function () {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    }
}

ObtenerJaulaPorDescripcion = function(transportistaId, placaJaula, esAyuda) {
    if (placaJaula === undefined && $("#txtOrganizacionBuscar").val().trim() == '') {
        ObtenerJaulaPorProveedorID(transportistaId);
        return;
    } else if ($("#txtOrganizacionBuscar").val() != '') {
        placaJaula = $("#txtOrganizacionBuscar").val();
    }
    var datos = { 'jaulaInfo': { 'PlacaJaula': placaJaula, 'Proveedor': { 'ProveedorID': idTransportista } } }

    $.ajax({
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ObtenerJaulasPorDescripcion",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: false,
        error: function(request) {

        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                var resultado = data.d;
                if (resultado.JaulaID == 0) {
                    bootbox.alert({
                        message: window.msgJaulaInvalido,
                        callback: function() {
                            $("#txtPlacasJaula").val("");
                            $("#txtPlacasJaula").focus();
                        },
                        closeButton: false,
                        buttons: {
                            ok: {
                                label: window.Aceptar,
                                className: 'SuKarne btn-primary'
                            }
                        }
                    });
                } else {
                if (esAyuda) {
                    //Titulo de la ayuda
                    $("#lblTituloDialogo").html("");
                    $("#lblTituloDialogo").append("<asp:Label ID='lblTituloDialogo' runat='server'> " + lblBusquedaJaula_Titulo + "</asp:Label>");
                    //Opciones de la ayuda de tractos
                    ayudaDesplegada = 9;
                    $("#OpcionesAyuda").html("");
                    $("#OpcionesAyuda").append("<asp:Label ID='lblOrganizacionBusqueda' runat='server' > " + lblPlacaJaula + "  </asp:Label>" +
                        "<input type'text' id='txtOrganizacionBuscar' style='width: 230px;'/>" +
                        "<a id='btnAyudaBuscarOrganizacionOrigen' onClick='ObtenerJaulaPorDescripcion(" + resultado.Proveedor.ProveedorID + "," +
                        $("#txtOrganizacionBuscar").val() + "," + true + ");' class='btn SuKarne' style='margin-left: 10px;'>Buscar</a>" +
                        "<a id='btnAyudaAgregarBuscar' onClick='AyudaAgregarJaula();'  class='btn SuKarne' style='margin-left: 10px;'>Agregar</a>" +
                        "<a id='btnAyudaCancelarBuscar' onClick='AyudaCancelarBuscar();' class='btn SuKarne' style='margin-left: 10px;'>Cancelar</a>");
                    //Encabezado de busqueda
                    $("#txtOrganizacionBuscar").val(placaJaula);
                    $("#tbBusquedaEncabezado thead").html("");
                    $("#tbBusquedaEncabezado thead").append("<tr>" +
                        "<th style='width: 20px;' class='alineacionCentro' scope='col'></th>" +
                        "<th style='width: 100px;' class='alineacionCentro' scope='col'><asp:Label ID='Label4 runat='server'>" + lblAyudaGridIdentificador + "</asp:Label></th>" +
                        "<th style='width: auto;' class='alineacionCentro' scope='col'><asp:Label ID='Label5' runat='server'>" + lblPlacaTracto + "</asp:Label></th>" +
                        "</tr>");
                    //Contenido de la busqueda
                    $("#tbBusqueda tbody").html("");
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='jaula"
                        + resultado.JaulaID + "' proveedorID='" + resultado.Proveedor.ProveedorID + "' numEconomico='" + resultado.NumEconomico +
                        "' placaTracto='" + resultado.PlacaJaula + "' onclick='SeleccionaUno(\"#organizacion" +
                        resultado.JaulaID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado.JaulaID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado.PlacaJaula + "</td>" +
                        "</tr>");
                    setTimeout(function() {
                        $("#txtOrganizacionBuscar").focus();
                    }, 500);
                    $("#dlgBusquedaOrganizacion").modal("show");
                } else {
                    $("#txtPlacasJaula").val(resultado.PlacaJaula);
                    $("#txtEcoJaula").val(resultado.NumEconomico);
                }
            }
            } else {
                bootbox.alert({
                    message: window.msgJaulaInvalido,
                    callback: function () {
                        $("#txtPlacasJaula").val("");
                        $("#txtEcoJaula").val("");
                        $("#txtPlacasJaula").focus();
                    },
                    closeButton: false,
                    buttons: {
                        ok: {
                            label: window.Aceptar,
                            className: 'SuKarne btn-primary'
                        }
                    }
                });
            }
        }
    });
}
    
ValidaCamposDatos = function () {
    if ($("#txtOperador1").val() == "") {
        // Es necesario aplicar focus de esta manera para que funcione
        bootbox.alert({
            message: window.msgSeleccionaOperador1,
            callback: function() {
                $("#txtNumOperador1").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
    });
        return false;
    }
    if (dobleTransportista){
        if ($("#txtOperador2").val() == "") {
            bootbox.alert({
                message: window.msgSeleccionaOperador2,
                callback: function () {
                    $("#txtNumOperador2").focus();
                },
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
            return false;
        }
    }
    if ($("#txtPlacasTracto").val() == "") {
        bootbox.alert({
            message: window.msgSeleccionaTracto,
            callback: function () {
                $("#txtPlacasTracto").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    }
    if ($("#txtPlacasJaula").val() == "") {
        bootbox.alert({
            message: window.msgSeleccionaPlacaJaula,
            callback: function () {
                $("#txtPlacasJaula").focus();
            },
            closeButton: false,
            buttons: {
                ok: {
                    label: window.Aceptar,
                    className: 'SuKarne btn-primary'
                }
            }
        });
        return false;
    }

    return true;
}

CrearObjetoProgramacionDatos = function () {
       
    if (esEdicionDatos) {
        // Objeto para edición
        // Indica si es edicion o creacion ya que no
        // no se puede validar con el embarque id
        embarqueInfo = {
            'embarqueInfo': {
                'EmbarqueID': $("#txtIdDatos").val(),
                'Operador1': {
                    'ChoferID': $("#txtNumOperador1").val()
                },
                'Operador2': {
                    'ChoferID': $("#txtNumOperador2").val() == "" ? 0 : $("#txtNumOperador2").val()
                },
                'Tracto': {
                    'PlacaCamion': $("#txtPlacasTracto").val()
                },
                'Jaula': {
                    'PlacaJaula': $("#txtPlacasJaula").val()
                },
                'Observaciones': $("#txtObservacionesDatosCaptura").val() === "" ? null : $("#txtObservacionesDatosCaptura").val(),
                'UsuarioCreacionID': -1
            }
        }

    } else {
        // Objeto para guardado
        embarqueInfo = {
            'embarqueInfo': {
                'EmbarqueID': $("#txtIdDatos").val(),
                'Operador1': {
                    'ChoferID': $("#txtNumOperador1").val()
                },
                'Operador2': {
                    'ChoferID': $("#txtNumOperador2").val() == "" ? 0 : $("#txtNumOperador2").val()
                },
                'Tracto': {
                    'PlacaCamion': $("#txtPlacasTracto").val()
                },
                'Jaula': {
                    'PlacaJaula': $("#txtPlacasJaula").val()
                },
                'Observaciones': $("#txtObservacionesDatosCaptura").val() === "" ? null : $("#txtObservacionesDatosCaptura").val(),
                'UsuarioCreacionID': 0
            }
        }
    }

    return embarqueInfo;
}

// Valida que nosolo se ingresen caracteres alfanumericos, espacios, comas y puntos
ValidarCaracteresAlfanumericos = function(e) {
    var code = e.keyCode || e.which;
    var regex = new RegExp("^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ., ]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (code == 13) { //enter
        e.preventDefault();
    }
    if (regex.test(str) || code == 8 || code == 39 || code == 46 || code == 37 && str !== '%') {
        return true;
    }

    e.preventDefault();
    return false;
};

/* Validar Estatus del Embarque*/
ValidarEstatus = function (datos, seccion) {
    $.ajax({
        data: JSON.stringify({ embarqueInfo: datos.embarqueInfo}),
        type: "POST",
        url: "ProgramacionEmbarque.aspx/ValidarEstatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var estatus = data.d;
            //Validación del campo Estatus
            if (estatus.DescripcionEstatus == window.EmbarqueRecibido || estatus.DescripcionEstatus == window.EmbarqueCancelado) {
                MensajeInformativo(window.msgEstatusActualizar, seccion);
                estatusValidado = true;
            } else {
                estatusValidado = false;
            }
        },
        error: function (err) {
            esRuteo = false;
            bootbox.alert({
                message: window.msgErrorGuardar,
                closeButton: false,
                buttons: {
                    ok: {
                        label: window.Aceptar,
                        className: 'SuKarne btn-primary'
                    }
                }
            });
        }
    });
};