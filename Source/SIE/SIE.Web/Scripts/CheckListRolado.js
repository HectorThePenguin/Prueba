/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

var validarCheckList = {
    NoExiste: { value: 0, mensaje: "" },
    NoHaCumplidoUnaHora: { value: 1, mensaje: "Ya se generó checklist. No es posible ejecutarlo nuevamente." },
    PasoTolerancia: { value: 2, mensaje: "No es posible ejecutar el checklist, se han pasado los 30 minutos de tolerancia." },
    AgregarCheckList: { value: 3, mensaje: "" },
    CheckListGeneradoSupervisor: { value: 4, mensaje: "Ya se generó checklist. No es posible ejecutarlo nuevamente." }
};

var enumAgua = {
    inicial: { value: 1, descripcion: "Contador inicial de agua" },
    final: { value: 2, descripcion: "Contador final de agua" },
    total: { value: 3, descripcion: "Consumo de agua litro" }
};

var enumHumedad = {
    granoRoladoBodega: { value: 1, descripcion: "Humedad grano rolado en bodega" },
    granoEnteroBodega: { value: 2, descripcion: "Humedad grano entero en bodega" },
    adicionAguaSurfactante: { value: 3, descripcion: "Superávit adición de agua/surfactante" },
};

var enumSurfactante = {
    surfactante: { value: 1, descripcion: "Surfactante" }
};

var enumDiesel = {
    dieselCaldera: { value: 1, descripcion: "Consumo diesel calderas" },
    dieselRolado: { value: 2, descripcion: "Diesel/tonelada de grano rolado" }
};

var enumGrano = {
    granoEnteroPP: { value: 1, descripcion: "Total grano entero PP", clase: "deshabilitarGrano" },
    granoEnteroBodega: { value: 2, descripcion: "Total final grano ent. bodega", clase: "habilitarGrano" },
    granoProcesado: { value: 3, descripcion: "Total grano procesado", clase: "deshabilitarGrano" },
    granoSuperavitRolado: { value: 4, descripcion: "Superávit grano rolado", clase: "deshabilitarGrano" },
    granoRolado: { value: 5, descripcion: "Total grano rolado", clase: "deshabilitarGrano" }
};

var turnoSeleccionado = 0;
var preguntaID = 0;
var rangoID = 0;
var usuarioValido = false;
var checkListEnProceso = false;

var checkListRoladoraGeneral = {};
var checkListRoladora = {};
var checkListRoladoraHorometro = {};
var checkListRoladoraDetalle = new Array();
var listaCheckListRoladoraHorometro = new Array();
var listaCheckListRoladora = new Array();

var roladora = {};
var checkListDatos = {};
var fechaCheckList;
var controlaMensaje = false;
var checkListSupervisado = false;
var mensajeHorometros = false;
var mensajeCheck = false;

var OPERADOR_PLANTA_ALIMENTOS = 13;
var usuarioLogeadoSupervisor = false;

jQuery(document).ready(function () {
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    configurarControles();
    obtenerNotificaciones();
    setInterval(function() {
        obtenerNotificaciones();
    }, 300000);
    agregarTurnos();

    EventosModalSupervisor();

    deshabilitarControlesTabCheckList();

    $("#lblEstatus").text('Sin Supervisar');
    $("#txtObservaciones").attr("maxlength", "255");
    $('#ddlRoladora').attr("disabled", true);

    $('body').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#btnGuardar").click(function () {
        BloquearPantalla();
        guardarDivCheckListRolado();
    });
    $("#btnCancelar").click(function () {
        mensajeCancelar();
    });
    $("#btnGuardarParametros").click(function () {
        BloquearPantalla();
        guardarDivParametrosRolado();
    });
    $("#btnCancelarParametros").click(function () {
        cancelarParametros();
    });
    cargarTemplatesParametros();
    asignarClaseSupervisor();
    $('#TabParametrosRolado').attr('disabled', true);

    //obtenerUsuario();
});

obtenerUsuario = function (usuarioId) {
    var usuario = { "usuarioID": usuarioId };
    EjecutarWebMethod(window.location.pathname + '/ObtenerUsuarioLogeado', usuario, usuarioSupervisor
                    , "Ocurrió un error al cargar el usuario");
};

usuarioSupervisor = function(msg) {
    if (msg.d == null || msg.d.length == 0) {
        return;
    }
    var usuario = msg.d;
    if (usuario.Operador.Rol.RolID == OPERADOR_PLANTA_ALIMENTOS) {
        $("#txtUsuario").val(usuario.UsuarioActiveDirectory);
        $("#txtUsuario").attr('disabled', true);
        //$("#hdnUsuarioID").val(usuario.UsuarioID);
        $("#hdnUsuarioSupervisorID").val(usuario.UsuarioID);
        usuarioValido = true;
        $("#txtContrasenia").val('**********');
        $("#txtContrasenia").attr('disabled', true);
        //$("#btnSupervisor").attr('disabled', true);
        $("#btnOKSupervisor").attr('disabled', true);

        //usuarioLogeadoSupervisor = true;
    } else {
        usuarioLogeadoSupervisor = false;
        usuarioValido = false;
    }
};

limpiarUsuarioNoLogeado = function () {
    $("#btnSupervisor").attr('disabled', false);
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId == 0) {
        $('#txtUsuario').val('');
        $('#txtContrasenia').val('');
        
        $('#txtObservaciones').val('');
        $('#txtUsuario').attr('disabled', false);
        $('#txtContrasenia').attr('disabled', false);
        $('#btnOKSupervisor').attr('disabled', false);
        $("#hdnUsuarioID").val('');

        usuarioValido = false;
        usuarioLogeadoSupervisor = false;
        checkListSupervisado = false;
    } else {
        $("#btnSupervisor").attr('disabled', true);
    }
};

asignarClaseSupervisor = function () {
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId > 0) {
        if (checkListSupervisado) {
            $("#imgSupervisor").attr("src", "../Images/Correct.png");
            $("#btnSupervisor").attr('disabled', true);
        } else {
            $("#imgSupervisor").attr("src", "../Images/close.png");
            limpiarUsuarioNoLogeado();
        }
    } else {
        $("#imgSupervisor").attr("src", "../Images/close.png");
        limpiarUsuarioNoLogeado();
    }
};

deshabilitarControlesTabCheckList = function () {
    $("#btnGuardar").attr("disabled", true);
};

habilitarControlesTabCheckList = function () {
    $("#btnGuardar").attr("disabled", false);
};

configurarControles = function () {
    $("#tabs").tabs();
    $("#tabs").tabs("option", "disabled", [1]);

    $("#btnGuardar").attr("disabled", true);

    $('#txtFecha').datepicker({
        format: "dd/mm/yyyy",
        todayBtn: "linked",
        language: "es"
    });

    $('.soloNumeros').numericInput();
};

agregarTurnos = function () {
    var valores = {};
    var recursos = {};
    recursos.Seleccione = "Seleccione";
    valores.Recursos = recursos;

    var listaValores = new Array();
    var valor = {};
    valor.Clave = 1;
    valor.Descripcion = "Uno";
    listaValores.push(valor);

    valor = {};
    valor.Clave = 2;
    valor.Descripcion = "Dos";
    listaValores.push(valor);

    valor = {};
    valor.Clave = 3;
    valor.Descripcion = "Tres";
    listaValores.push(valor);

    valores.Valores = listaValores;

    $('#ddlTurno').html('');
    $('#ddlTurno').setTemplateURL('../Templates/ComboGenerico.htm');
    $('#ddlTurno').processTemplate(valores);

    $('#ddlTurno').change(function () {
        $("#hdnUsuarioSupervisorID").val(0);
        checkListEnProceso = false;
        $("#hdnUsuarioID").val(0);
        var supervisorId = 0; //TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
        if (supervisorId == 0) {
            limpiarCamposModalSupervisor();
            limpiarUsuarioNoLogeado();
        }
        $("#btnGuardar").attr('disabled', true);
        BloquearPantalla();
        limpiarTemplatesRolado();
        obtenerDatosGenerales();
        if ($('#ddlTurno option:selected').val() != '0') {
            $("#tabs").tabs("enable", 1);
        } else {
            {
                $("#tabs").tabs("option", "disabled", [1]);
            }
        }
    });

    $("#ddlTurno").keydown(function (event) {
        if (event.which == 13) {
        }
    });
};

obtenerDatosGenerales = function () {
    limpiarTemplatesParametros();
    if ($("#ddlTurno").val() > 0) {
        var jTurno = { "turno": $("#ddlTurno").val() };
        EjecutarWebMethod(window.location.pathname + '/ObtenerPorTurno', jTurno, asignarDatosGenerales
                        , "Ocurrió un error al consultar los datos generales");
        turnoSeleccionado = $("#ddlTurno").val();
    } else {
        DesbloquearPantalla();
        turnoSeleccionado = 0;
        limpiarCamposDatosGenerales();
    }
};

limpiarTemplatesParametros = function () {
    $('#divCocedor').html('');
    $('#divAmperaje').html('');
    $('#divCalidadRolado').html('');
};

limpiarTemplatesRolado = function () {
    $("#divHorometro").html('');
    $("#divHumedad").html('');
    $("#divSurfantante").html('');
    $("#divAguas").html('');
    $("#divGrano").html('');
    $("#divDiesel").html('');
};

limpiarCamposDatosGenerales = function () {
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId == 0) {
        $("#txtResponsable").val('');
        $('#txtFecha').val('');
        $('#txtHoraInicio').val('');

        $("#hdnUsuarioID").val('');
        $("#hdnUsuarioSupervisorID").val('');
        $("#btnSupervisor").attr('disabled', false);
        
        $('#ddlTurno').val(0);
        $('#ddlTurno').focus();
        
        $('#ddlRoladora').attr("disabled", true);
        $('#ddlRoladora').html('');

        $("#ddlRoladora").off("change");

        $("#lblEstatus").text('Sin Supervisar');
        $("#imgSupervisor").attr("src", "../Images/Close.png");
    }    
    limpiarUsuarioNoLogeado();
    $("#btnGuardar").attr('disabled', true);
    
    //usuarioLogeadoSupervisor = false;
    DesbloquearPantalla();
};

asignarDatosGenerales = function (msg) {
    if (msg.d == null) {
        DesbloquearPantalla();
        return;
    }
    var datosGenerales = msg.d;
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (datosGenerales.CheckListRoladoraGeneral.UsuarioIDSupervisor != null
            && datosGenerales.CheckListRoladoraGeneral.UsuarioIDSupervisor != 0
            && $("#hdnUsuarioID").val() == $("#hdnUsuarioSupervisorID").val()) {
        if (!mensajeCheck) {
            mensajeCheck = true;
            bootbox.dialog({
                message: validarCheckList.CheckListGeneradoSupervisor.mensaje,
                buttons: {
                    Aceptar: {
                        label: "Ok",
                        callback: function () {
                            //usuarioValido = false;
                            //usuarioLogeadoSupervisor = false;
                            //limpiarCamposDatosGenerales();
                            //limpiarCamposModalSupervisor();
                            //$("#btnSupervisor").attr('disabled', false);
                            DesbloquearPantalla();
                            mensajeCheck = false;
                            return true;
                        }
                    },
                }
            });
        }
    } else {
        if (datosGenerales.CheckListRoladoraGeneral.UsuarioIDSupervisor != null
            && datosGenerales.CheckListRoladoraGeneral.UsuarioIDSupervisor != 0) {
            $("#imgSupervisor").attr("src", "../Images/Correct.png");
            $("#lblEstatus").text('Supervisado');
            $("#btnSupervisor").attr('disabled', true);            
            obtenerUsuario(datosGenerales.CheckListRoladoraGeneral.UsuarioIDSupervisor);
        } else {
            if (usuarioId == 0) {
                $("#imgSupervisor").attr("src", "../Images/Close.png");
                $("#lblEstatus").text('Sin Supervisar');
                limpiarUsuarioNoLogeado();
            }
        }

        $("#txtResponsable").val(datosGenerales.Usuario.Nombre);
        $("#hdnUsuarioID").val(datosGenerales.Usuario.UsuarioID);

        var fecha = new Date(parseInt(datosGenerales.CheckListRoladoraGeneral.FechaInicio.replace(/^\D+/g, '')));
        $('#txtFecha').val($.datepicker.formatDate('dd/mm/yy', fecha));
        $('#txtHoraInicio').val(datosGenerales.Hora);

        var roladoras = datosGenerales.Roladoras;
        asignarRoladoras(roladoras);
    }
};

asignarRoladoras = function (roladoras) {
    $('#ddlRoladora').html('');

    var valores = {};
    var recursos = {};
    recursos.Seleccione = "Seleccione";
    valores.Recursos = recursos;

    var listaValores = new Array();
    for (var i = 0; i < roladoras.length; i++) {
        var valor = {};
        valor.Clave = roladoras[i].RoladoraID;
        valor.Descripcion = roladoras[i].Descripcion;

        listaValores.push(valor);
    }

    valores.Valores = listaValores;

    $('#ddlRoladora').setTemplateURL('../Templates/ComboGenerico.htm');
    $('#ddlRoladora').processTemplate(valores);

    $('#ddlRoladora').attr("disabled", false);

    limpiarTemplatesRolado();
    agregarEventosComboRoladora();
    cargarTemplatesParametros();
    cargarTemplateHorometro();
    asignarEventosHorometro();
    obtenerValoresGranoEnteroDieselCalderas();

    DesbloquearPantalla();
};

agregarEventosComboRoladora = function () {
    $("#ddlRoladora").on("change", function () {
        if ($("#ddlRoladora").val() > 0 && $("#ddlTurno").val() > 0) {
            if ($("#hdnUsuarioID").val() != $("#hdnUsuarioSupervisorID").val()) {
                $("#hdnUsuarioSupervisorID").val(0);
            }
            validarCheckListCompleto();
        }
    });
};

validarCheckListCompleto = function () {
    BloquearPantalla();
    limpiarTemplatesParametros();
    consultarCheckListCompletado();
};

obtenerValoresGranoEnteroDieselCalderas = function () {
    var jGranoEnteroDieselCalderas = { "fechaInicio": $("#txtFecha").val(), "horaInicio": $("#txtHoraInicio").val() };
    EjecutarWebMethod(window.location.pathname + '/ObtenerGranoEnteroDieselCaldera', jGranoEnteroDieselCalderas
                    , asignarGranoEnteroDieselCaldera
                    , "Ocurrió un error al obtener los valores de Grano Entero y Diesel Calderas");
};

asignarGranoEnteroDieselCaldera = function (msg) {
    var granoEnteroDieselCaldera = msg.d;
    $("#txtGrano" + enumGrano.granoEnteroPP.value).val(granoEnteroDieselCaldera.TotalGranoEntreroPP);
    $("#txtDiesel" + enumDiesel.dieselCaldera.value).val(granoEnteroDieselCaldera.ConsumoDieselCalderas);
};

consultarCheckList = function () {
    var s = this;
    s.completo = function (fn) {
        return s;
    };
    s.siempre = function (fn) {
        if (fn) {
            fn();
        }
    };
    var completado = function (msg) {
        s.completo = function (fn) {
            if (fn) {
                fn();
            }
            return s;
        };
        if (!controlaMensaje) {
            asignarCheckList(msg);
            controlaMensaje = true;
        }
    };
    var jCheckList = { "turno": $("#ddlTurno").val(), "roladoraId": $("#ddlRoladora").val() };
    EjecutarWebMethod(window.location.pathname + '/ObtenerCheckList', jCheckList, completado
                    , "Ocurrió un error al obtener las notificaciones", { async: false });
    return s;
};

consultarCheckListCompletado = function () {
    var jCheckList = { "turno": $("#ddlTurno").val(), "roladoraId": $("#ddlRoladora").val() };
    EjecutarWebMethod(window.location.pathname + '/ObtenerCheckListCompleto', jCheckList, checkListCompleto
                    , "Ocurrió un error al obtener el Checklist");
};

checkListCompleto = function (msg) {
    controlaMensaje = false;
    checkListDatos = msg.d;

    if (checkListDatos.CheckListRoladora == undefined) {
        consultaParametros();
        asignarEventosHorometro();
        obtenerValoresGranoEnteroDieselCalderas();
    }
    consultarCheckList().completo(function () {
        //cargarTemplateHorometro();
        asignarEventosHorometro();
        obtenerValoresGranoEnteroDieselCalderas();
        asignarValoresPestañas(checkListDatos.CheckListRoladora, checkListDatos.CheckListRoladoraGeneral
                         , checkListDatos.CheckListRoladoraDetalle, checkListDatos.CheckListRoladoraHorometro);
    }).siempre(function () {
        DesbloquearPantalla();
    });
};

asignarValoresPestañas = function (roladora, roladoraGeneral, roladoraDetalle, roladoraHorometros) {
    setTimeout(function () {
        var roladoraID = $("#ddlRoladora").val();
        $(roladoraHorometros).each(function () {

            roladoraID = this.Roladora.RoladoraID;
            if (this.HorometroInicial != '' && this.HorometroInicial != '0') {
                $("#txtInicio" + roladoraID).val(this.HorometroInicial);
                $("#txtInicio" + roladoraID).attr('disabled', true);
            }
            if (this.HorometroFinal != null && this.HorometroFinal != '') {
                $("#txtFin" + roladoraID).val(this.HorometroFinal);
                $("#txtFin" + roladoraID).attr('disabled', true);

                var name = $("#txtFin" + roladoraID).attr("name");
                var clave = $("#txtFin" + roladoraID).attr("id");
                validaHorometros(name, clave);
            }
        });

        if (roladoraGeneral.UsuarioIDSupervisor > 0) {
            $("#hdnUsuarioSupervisorID").val(roladoraGeneral.UsuarioIDSupervisor);
            usuarioValido = true;
            //usuarioLogeadoSupervisor = true;
            checkListSupervisado = true;
        }
        $("#txtObservaciones").val(roladoraGeneral.Observaciones);

        if (roladoraGeneral.SurfactanteInicio != '' && roladoraGeneral.SurfactanteInicio != null) {
            $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).val(roladoraGeneral.SurfactanteInicio);
            $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).attr('disabled', true);
        }

        if (roladoraGeneral.SurfactanteFin != '' && roladoraGeneral.SurfactanteFin != null) {
            $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).val(roladoraGeneral.SurfactanteFin);
            $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).attr('disabled', true);
        }

        if (roladoraGeneral.ContadorAguaInicio != '' && roladoraGeneral.ContadorAguaInicio != null) {
            $("#txtAgua" + enumAgua.inicial.value).val(roladoraGeneral.ContadorAguaInicio);
            $("#txtAgua" + enumAgua.inicial.value).attr('disabled', true);
        }
        if (roladoraGeneral.ContadorAguaFin != '' && roladoraGeneral.ContadorAguaFin != null) {
            $("#txtAgua" + enumAgua.final.value).val(roladoraGeneral.ContadorAguaFin);
            $("#txtAgua" + enumAgua.final.value).attr('disabled', true);
        }

        if (roladoraGeneral.GranoEnteroFinal != '' && roladoraGeneral.GranoEnteroFinal != null) {
            $("#txtGrano" + enumGrano.granoEnteroBodega.value).val(roladoraGeneral.GranoEnteroFinal);
            $("#txtGrano" + enumGrano.granoEnteroBodega.value).attr('disabled', true);
        }
        validaLitrosAgua();
        validaGranos();

        asignarValoresDetalle(roladoraDetalle);
    }, 500);
};

asignarValoresDetalle = function (detalle) {
    //if (detalle.length == 0) {
    //    return;
    //}
    //var i = 0;
    //$(".rango").each(function () {
    //    //$(this).off("change");
    //    $(this).val(detalle[i].CheckListRoladoraRango.CheckListRoladoraRangoID);
    //    //$(this).attr('disabled', true);
    //    i++;
    //});

    //i = 0;
    //$(".accion").each(function () {
    //    $(this).val(detalle[i].CheckListRoladoraAccion.CheckListRoladoraAccionID);
    //    //$(this).attr('disabled', true);
    //    i++;
    //});
};

asignarCheckList = function (msg) {
    var checkListValido = msg.d.TiempoTranscurrido;
    var supervisado = msg.d.Supervisado;
    if (checkListValido == -1) {
        consultaParametros();
        return true;
    }
    validaGeneracionCheckList(checkListValido, supervisado);
};

validaGeneracionCheckList = function (checkListValido, supervisado) {
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (supervisado > 0 && usuarioId > 0 && $("#hdnUsuarioID").val() == $("#hdnUsuarioSupervisorID").val()) {
        if (!mensajeCheck) {
            mensajeCheck = true;
            bootbox.dialog({
                message: validarCheckList.CheckListGeneradoSupervisor.mensaje,
                buttons: {
                    Aceptar: {
                        label: "Ok",
                        callback: function () {
                            limpiarCamposDatosGenerales();
                            usuarioValido = false;
                            usuarioLogeadoSupervisor = false;
                            mensajeCheck = false;
                            return true;
                        }
                    },
                }
            });
        }
    } else {
        if (usuarioId == 0) {
            if (checkListValido < 60) {
                if (!mensajeCheck) {
                    mensajeCheck = true;
                    bootbox.dialog({
                        message: validarCheckList.NoHaCumplidoUnaHora.mensaje,
                        buttons: {
                            Aceptar: {
                                label: "Ok",
                                callback: function () {
                                    $("#hdnUsuarioSupervisorID").val(0);
                                    limpiarCamposDatosGenerales();
                                    limpiarTemplatesParametros();
                                    usuarioValido = false;
                                    usuarioLogeadoSupervisor = false;
                                    mensajeCheck = false;
                                    return true;
                                }
                            },
                        }
                    });
                }
                return true;
            }
            if (checkListValido > 90 && checkListValido < 120
                || checkListValido > 150 && checkListValido < 180
                || checkListValido > 210 && checkListValido < 240
                || checkListValido > 270 && checkListValido < 300
                || checkListValido > 330 && checkListValido < 360
                || checkListValido > 390 && checkListValido < 420
                || checkListValido > 450 && checkListValido < 480) {
                if (!mensajeCheck) {
                    mensajeCheck = true;
                    bootbox.dialog({
                        message: validarCheckList.PasoTolerancia.mensaje,
                        buttons: {
                            Aceptar: {
                                label: "Ok",
                                callback: function () {
                                    $("#hdnUsuarioSupervisorID").val(0);
                                    limpiarCamposDatosGenerales();
                                    usuarioValido = false;
                                    usuarioLogeadoSupervisor = false;
                                    mensajeCheck = false;
                                    return true;
                                }
                            },
                        }
                    });
                }
            } else {
                if (checkListValido > 40 && checkListValido < 60) {
                    if (!mensajeCheck) {
                        mensajeCheck = true;
                        bootbox.dialog({
                            message: validarCheckList.NoHaCumplidoUnaHora.mensaje,
                            buttons: {
                                Aceptar: {
                                    label: "Ok",
                                    callback: function () {
                                        $("#hdnUsuarioSupervisorID").val(0);
                                        limpiarCamposDatosGenerales();
                                        usuarioValido = false;
                                        usuarioLogeadoSupervisor = false;
                                        mensajeCheck = false;
                                        return true;
                                    }
                                },
                            }
                        });
                    }
                } else {
                    consultaParametros();
                }
            }
        } else {
            consultaParametros();
        }
    }
};

consultaParametros = function () {
    EjecutarWebMethod(window.location.pathname + '/ObtenerParametros', {}, asignarParametros
                    , "Ocurrió un error al obtener los parametros");
};

obtenerNotificaciones = function () {
    EjecutarWebMethod(window.location.pathname + '/ObtenerNotificaciones', {}, asignarNotificaciones
                    , "Ocurrió un error al obtener las notificaciones");
};

asignarNotificaciones = function (msg) {
    $("#lnkNotificaciones").removeClass("existeNotificacion");
    $("#lnkNotificaciones").removeClass("noExisteNotificacion");
    if (msg.d == null || msg.d.length == 0) {
        $("#lblNotificacionesTotales").text("Notificaciones (0)");
        $("#lnkNotificaciones").addClass("existeNotificacion");
        return;
    }
    var notificaciones = msg.d;
    var recursos = {};

    recursos.Turno = 'Turno';
    recursos.Roladora = 'Roladora';
    recursos.Descripcion = 'Descripción';
    recursos.Hora = 'Hora';

    var itemsNotificaciones = new Array();
    for (var i = 0; i < notificaciones.length; i++) {
        var item = {};
        switch (notificaciones[i].CheckListRoladoraGeneral.Turno) {
            case 1:
                item.Turno = "Uno";
                break;
            case 2:
                item.Turno = "Dos";
                break;
            case 3:
                item.Turno = "Tres";
                break;
            default:
                break;
        }
        item.Descripcion = 'Aplicar checklist';
        item.Roladora = notificaciones[i].Roladora.Descripcion;
        item.Hora = notificaciones[i].Hora;

        itemsNotificaciones.push(item);
    }

    $("#lblNotificacionesTotales").text("Notificaciones (" + notificaciones.length + ")");
    $("#lnkNotificaciones").addClass("noExisteNotificacion");

    MostrarMensaje('Tiene ' + notificaciones.length + ' notificación(es) pendiente(s) por atender', function () {
        mostrarVentanaNotificacion();
    });
        var datos = { };
        datos.Recursos = recursos;
        datos.Notifiaciones = itemsNotificaciones;
        var divContenedorGrid = $('#divGridNotificaciones');
        divContenedorGrid.setTemplateURL('../Templates/GridNotificaciones.htm');
        divContenedorGrid.processTemplate(datos);
        
    
    $("#lnkNotificaciones").click(function () {
        mostrarVentanaNotificacion();
    });
    $("#btnCerrarNotificacion").click(function () {
        cerrarVentanaNotificacion();
    });
    $(".cerrarNotificacion").on("click", function () {
        cerrarVentanaNotificacion();
    });

};

mostrarVentanaNotificacion = function () {
    $("#modalNotificaciones").modal('show');
};

cerrarVentanaNotificacion = function () {
    $('#modalNotificaciones').modal('hide');
};

asignarParametros = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        deshabilitarControlesTabCheckList();
        return;
    }
    checkListEnProceso = true;

    var parametros = msg.d;
    var cocedor = parametros[0];
    var amperaje = parametros[1];
    var calidadRolado = parametros[2];

    $("#hdnXmlParametros").val(cocedor.Xml);

    habilitarControlesTabCheckList();

    generarDivCocedor(cocedor);
    generarDivAmperaje(amperaje);
    generarDivCalidadRolado(calidadRolado);

    $(".rango").first().focus();
};

generarDivCocedor = function (cocedor) {
    var valores = {};
    var recursos = {};

    recursos.Titulo = cocedor.Titulo;
    recursos.SeleccioneRango = "Seleccione Rango";
    recursos.SeleccioneAccion = "Seleccione acción ó corrección";
    valores.Recursos = recursos;

    var preguntas = cocedor.Parametros;
    var listaPreguntas = new Array();
    var pregId = 0;
    for (var i = 0; i < preguntas.length; i++) {
        if (pregId != preguntas[i].PreguntaID) {
            var valorPregunta = {};
            valorPregunta.Descripcion = preguntas[i].Descripcion;
            valorPregunta.RoladoraRango = preguntas[i].PreguntaID;
            valorPregunta.Clase = "";

            var rangos = preguntas[i].Rangos;
            var listaRangos = new Array();
            var descripcionRango = "";
            for (var j = 0; j < rangos.length; j++) {
                if (descripcionRango != rangos[j].Descripcion) {
                    var valorRango = {};
                    valorRango.Descripcion = rangos[j].Descripcion;
                    valorRango.Clave = rangos[j].CheckListRoladoraRangoID;

                    listaRangos.push(valorRango);
                }
                descripcionRango = rangos[j].Descripcion;
            }
            valorPregunta.Rangos = listaRangos;

            var listaAcciones = new Array();
            var acciones = preguntas[i].Acciones;
            var descripcionAcciones = "";
            for (var k = 0; k < acciones.length; k++) {
                if (acciones[k].CheckListRoladoraAccionID > 0) {
                    if (descripcionAcciones != acciones[k].Descripcion) {
                        var valorAccion = {};
                        valorAccion.Descripcion = acciones[k].Descripcion;
                        valorAccion.Clave = acciones[k].CheckListRoladoraAccionID;
                        listaAcciones.push(valorAccion);
                    }
                    descripcionAcciones = acciones[k].Descripcion;
                }
            }
            valorPregunta.Acciones = listaAcciones;

            listaPreguntas.push(valorPregunta);
        }
        pregId = preguntas[i].PreguntaID;
    }
    valores.Preguntas = listaPreguntas;

    $('#divCocedor').html('');
    $('#divCocedor').setTemplateURL('../Templates/ParametrosCheckListRolado.htm');
    $('#divCocedor').processTemplate(valores);

    cargaSemaforoDeshabilitado();
    asignarEventosControlesTemplate("divCocedor");
};

generarDivAmperaje = function (amperaje) {
    var valores = {};
    var recursos = {};

    recursos.Titulo = amperaje.Titulo;
    recursos.SeleccioneRango = "Seleccione Rango";
    recursos.SeleccioneAccion = "Seleccione acción ó corrección";
    valores.Recursos = recursos;

    var preguntas = amperaje.Parametros;
    var listaPreguntas = new Array();
    var pregId = 0;
    for (var i = 0; i < preguntas.length; i++) {
        if (pregId != preguntas[i].PreguntaID) {
            var valorPregunta = {};
            valorPregunta.Descripcion = preguntas[i].Descripcion;
            valorPregunta.RoladoraRango = preguntas[i].PreguntaID;
            valorPregunta.Clase = "";

            var rangos = preguntas[i].Rangos;
            var listaRangos = new Array();
            var descripcionRango = "";
            for (var j = 0; j < rangos.length; j++) {
                if (descripcionRango != rangos[j].Descripcion) {
                    var valorRango = {};
                    valorRango.Descripcion = rangos[j].Descripcion;
                    valorRango.Clave = rangos[j].CheckListRoladoraRangoID;

                    listaRangos.push(valorRango);
                }
                descripcionRango = rangos[j].Descripcion;
            }
            valorPregunta.Rangos = listaRangos;

            var listaAcciones = new Array();
            var acciones = preguntas[i].Acciones;
            var descripcionAciones = "";
            for (var k = 0; k < acciones.length; k++) {
                if (acciones[k].CheckListRoladoraAccionID > 0) {
                    if (descripcionAciones != acciones[k].Descripcion) {
                        var valorAccion = {};
                        valorAccion.Descripcion = acciones[k].Descripcion;
                        valorAccion.Clave = acciones[k].CheckListRoladoraAccionID;
                        listaAcciones.push(valorAccion);
                    }
                    descripcionAciones = acciones[k].Descripcion;
                }
            }
            valorPregunta.Acciones = listaAcciones;

            listaPreguntas.push(valorPregunta);
        }
        pregId = preguntas[i].PreguntaID;
    }
    valores.Preguntas = listaPreguntas;

    $('#divAmperaje').html('');
    $('#divAmperaje').setTemplateURL('../Templates/ParametrosCheckListRolado.htm');
    $('#divAmperaje').processTemplate(valores);

    cargaSemaforoDeshabilitado();
    asignarEventosControlesTemplate("divAmperaje");
};

generarDivCalidadRolado = function (calidadRolado) {
    var valores = {};
    var recursos = {};

    recursos.Titulo = calidadRolado.Titulo;
    recursos.SeleccioneRango = "Seleccione Rango";
    recursos.SeleccioneAccion = "Seleccione acción ó corrección";
    valores.Recursos = recursos;

    var preguntas = calidadRolado.Parametros;
    var listaPreguntas = new Array();
    var pregId = 0;
    for (var i = 0; i < preguntas.length; i++) {
        if (pregId != preguntas[i].PreguntaID) {
            var valorPregunta = {};
            valorPregunta.Descripcion = preguntas[i].Descripcion;
            valorPregunta.RoladoraRango = preguntas[i].PreguntaID;
            valorPregunta.Clase = "";

            var rangos = preguntas[i].Rangos;
            var listaRangos = new Array();
            var descripcionRangos = "";
            for (var j = 0; j < rangos.length; j++) {
                if (descripcionRangos != rangos[j].Descripcion) {
                    var valorRango = {};
                    valorRango.Descripcion = rangos[j].Descripcion;
                    valorRango.Clave = rangos[j].CheckListRoladoraRangoID;

                    listaRangos.push(valorRango);
                }
                descripcionRangos = rangos[j].Descripcion;
            }
            valorPregunta.Rangos = listaRangos;

            var listaAcciones = new Array();
            var acciones = preguntas[i].Acciones;
            var descripcionAcciones = "";
            for (var k = 0; k < acciones.length; k++) {
                if (acciones[k].CheckListRoladoraAccionID > 0) {
                    if (descripcionAcciones != acciones[k].Descripcion) {
                        var valorAccion = {};
                        valorAccion.Descripcion = acciones[k].Descripcion;
                        valorAccion.Clave = acciones[k].CheckListRoladoraAccionID;
                        listaAcciones.push(valorAccion);
                    }
                    descripcionAcciones = acciones[k].Descripcion;
                }
            }
            valorPregunta.Acciones = listaAcciones;

            listaPreguntas.push(valorPregunta);
        }
        pregId = preguntas[i].PreguntaID;
    }
    valores.Preguntas = listaPreguntas;

    $('#divCalidadRolado').html('');
    $('#divCalidadRolado').setTemplateURL('../Templates/ParametrosCheckListRolado.htm');
    $('#divCalidadRolado').processTemplate(valores);

    cargaSemaforoDeshabilitado();
    asignarEventosControlesTemplate("divCalidadRolado");
};

asignarEventosControlesTemplate = function (divTemplate) {
    var div = $("#" + divTemplate).find('#dvItems .rango');
    div.each(function () {
        $(this).on("change", function () {
            preguntaID = $(this).attr("name");
            rangoID = $(this).val();
            validarRango();
        });
    });
};

validarRango = function () {
    var jRango = { "preguntaId": preguntaID, "rangoId": rangoID, "parametros": $("#hdnXmlParametros").val() };
    EjecutarWebMethod(window.location.pathname + '/ValidarRango', jRango, asignarSemaforo
                    , "Ocurrió un error al consultar los rangos");
};

asignarSemaforo = function (msg) {
    if (msg.d == null) {
        return;
    }
    var clase = msg.d.Clase;
    var habilitar = msg.d.Habilitar;
    var div = $("#divSemaforo" + preguntaID);
    var css = div.attr("class");
    div.removeClass(css);

    if (clase.length == 0) {
        clase = "deshabilitado";
    }
    div.addClass(clase);
    
    var ddlAccion = $("#dllAccion" + preguntaID);
    var ddlRango = $("#dllRango" + preguntaID);
    if (habilitar == 0) {
        ddlAccion.val(0);
        ddlAccion.attr("disabled", true);
    } else {
        ddlAccion.attr("disabled", false);
    }
    
    $("select[id$=" + ddlAccion.attr("id") + "] > option").remove();
    
    var indicadores = $("#hdnXmlParametros").val();

    var option = "<option value='0'>Seleccione acción ó corrección</option>";
    ddlAccion.append(option);
    $(indicadores).find('semaforo').each(function () {
        if ($(this).find("CheckListRoladoraRangoID").text() == ddlRango.val()) {
            var option = "<option value='" + $(this).find("CheckListRoladoraRangoID").text() + "'>"
                            + $(this).find("DescripcionAccion").text() + "</option>";
            ddlAccion.append(option);
        }
    });
};

cargaSemaforoDeshabilitado = function () {
    var indicadores = $("#hdnXmlParametros").val();
    $(indicadores).find('semaforo').each(function () {
        var divSemaforo = $('#divSemaforo' + $(this).find("PreguntaID").text());
        divSemaforo.removeClass("deshabilitado");
        divSemaforo.addClass("deshabilitado");
    });
};

guardarDivCheckListRolado = function () {
    var guardar = validaGuardarDivCheckListRolado();
    if (guardar) {
        generarCheckListoRoladoraGeneral();
        generarCheckListoRoladora();
        generarCheckListRoladoraDetalle();

        var jGuardar = {
            "checkListRoladora": checkListRoladora
          , "checkListRoladoraGeneral": checkListRoladoraGeneral
          , "checkListRoladoraDetalle": checkListRoladoraDetalle
        };
        EjecutarWebMethod(window.location.pathname + '/GuardarCheckList', jGuardar, guardadoExitoso
                        , "Ocurrió un error al guardar");
    } else {
        DesbloquearPantalla();
    }
};

guardadoExitoso = function (msg) {
    if (msg.d == null || msg.d.length == 0) {
        MostrarMensaje("Error al Guardar", null);
        DesbloquearPantalla();
        return;
    }
    MostrarMensaje("Datos guardados con éxito.", function () {
        $("#txtContrasenia").attr('disabled', false);
        $("#txtUsuario").attr('disabled', false);
        $("#txtObservaciones").attr('disabled', true);
        $("#btnOKSupervisor").attr('disabled', false);
        $("#btnSupervisor").attr('disabled', false);
        usuarioValido = false;
        usuarioLogeadoSupervisor = false;
        $("#txtContrasenia").val('');
        $("#txtUsuario").val('');
        $("#txtObservaciones").val('');
        $("#hdnUsuarioSupervisorID").val(0);
        
        limpiarCamposDatosGenerales();
        limpiarCamposModalSupervisor();
        limpiarControlesCancelar();
        limpiarTemplatesParametros();
        limpiarTemplatesRolado();
        $("#ddlTurno").focus();
        checkListEnProceso = false;

    });
    DesbloquearPantalla();
};

limpiarControlesCancelar = function () {
    $("#hdnUsuarioID").val('');
    $("#hdnUsuarioSupervisorID").val('');
    $("#txtContrasenia").val('');
    $("#txtUsuario").val('');

    $("#txtContrasenia").attr('disabled', false);
    $("#txtUsuario").attr('disabled', false);
    $("#tabs").tabs("option", "disabled", [1]);
};

mensajeCancelar = function () {
    bootbox.dialog({
        message: "No se ha guardado la información, ¿Está seguro de cancelar sin guardar la información capturada?",
        buttons: {
            Aceptar: {
                label: "Si",
                callback: function () {
                    usuarioValido = false;                    
                    checkListEnProceso = false;
                    limpiarControlesCancelar();
                    limpiarTemplatesParametros();
                    limpiarCamposDatosGenerales();
                    cargarTemplateHorometro();
                    asignarEventosHorometro();
                    usuarioLogeadoSupervisor = false;
                    checkListSupervisado = false;
                    $("#btnSupervisor").attr('disabled', false);
                    $("#btnOKSupervisor").attr('disabled', false);
                    asignarClaseSupervisor();
                    /*
                    setTimeout(function() {
                        obtenerUsuario();
                    }, 1000);
                    */
                }
            },
            Cancelar: {
                label: "No",
                callback: function () {

                }
            }
        }
    });
};

validaGuardarDivCheckListRolado = function () {
    var rangos = $(".rango");
    var valido = true;
    if ($('#ddlRoladora option:selected').val() == '0') {
        MostrarMensaje('Debe seleccionar una roladora para continuar.', function () {
            $('#ddlRoladora').focus();
        });
        return false;
    }
    rangos.each(function () {
        if (valido) {
            if ($(this).val() == 0) {
                valido = false;
                var cmb = $(this);
                var lbl = $("#lbl" + $(this).attr("name"));
                bootbox.dialog({
                    message: "Debe seleccionar el rango del del parámetro:" + lbl.text() + ", para continuar.",
                    buttons: {
                        Aceptar: {
                            label: "Ok",
                            callback: function () {
                                cmb.focus();
                                return;
                            }
                        }
                    }
                });
            }
        }
        if (!valido) {
            return false;
        }
    });
    if (valido) {
        var acciones = $(".accion");
        acciones.each(function() {
            if (valido) {
                if ($(this).val() == 0) {
                    if ($(this).attr("disabled") == null || $(this).attr("disabled") == 'undefined') {
                        valido = false;
                        var cmb = $(this);
                        var lbl = $("#lbl" + $(this).attr("name"));
                        bootbox.dialog({
                            message: "Debe seleccionar una acción o corrección para el parámetro:" + lbl.text() + ", para continuar.",
                            buttons: {
                                Aceptar: {
                                    label: "Ok",
                                    callback: function() {
                                        cmb.focus();
                                        return;
                                    }
                                }
                            }
                        });
                    }
                }
            }
            if (!valido) {
                return false;
            }
        });
    }
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (valido && usuarioId > 0) {
        if (!$.trim($("#txtObservaciones").val())) {
            valido = false;
            DesbloquearModal();
            MostrarMensaje("Debe ingresar las observaciones, para continuar", function () {
                mostrarVentanaSupervisor();
            });
        }
    }
    return valido;
};

validarGuardarDivParametros = function () {
    var guardar = true;
    if (guardar) {
        var humedadGranoEntero = $("#txtHumedad" + enumHumedad.granoEnteroBodega.value).val();
        var humedadGranoRolado = $("#txtHumedad" + enumHumedad.granoRoladoBodega.value).val();

        if ((!$.trim(humedadGranoEntero) && humedadGranoRolado != '')
                || (!$.trim(humedadGranoRolado) && humedadGranoEntero != '')) {
            guardar = false;
            MostrarMensaje("Campos de humedad tiene valores invalidos. Favor de verificar", function () {
                $("#txtHumedad" + enumHumedad.granoRoladoBodega.value).focus();
                DesbloquearPantalla();
            });
        }
    }
    return guardar;
};

validaCheckListProceso = function () {
    if (checkListEnProceso) {
        MostrarMensaje("Se encuentra un checklist en proceso, se debe finalizar para poder Supervisar.", null);
        return checkListEnProceso;
    }
    return checkListEnProceso;
};
//Modales
//Función para asignarle los eventos a los controles del Modal de inicio de sesión
mostrarVentanaSupervisor = function () {
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId > 0) {
        $('#modalSupervisor').modal('show');
    } else {
        if (!validaCheckListProceso()) {
            $("#txtObservaciones").attr("disabled", true);
            $('#modalSupervisor').modal('show');
        }
    }
};

EventosModalSupervisor = function () {
    $('.cerrarSupervisor').click(function () {
        BloquearModal();
        cancelarModalSupervisor();
    });

    $('#btnSupervisor').click(function () {
        DesbloquearModal();
        mostrarVentanaSupervisor();
    });

    $('#modalSupervisor').on('shown.bs.modal', function () {
        var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
        if (usuarioId == 0) {
            limpiarCamposModalSupervisor();
            $('#txtUsuario').focus();
        } else {
            $('#txtUsuario').attr('disabled', true);
            $('#txtContrasenia').attr('disabled', true);
            $('#txtObservaciones').attr('disabled', false);
        }
        $("html").css("margin-right", "-15px");
    });

    $('#btnCancelarSupervisor').click(function () {
        BloquearModal();
        cancelarModalSupervisor();
    });

    $('#btnGuardarSupervisor').click(function () {
        BloquearPantalla();
        if (guardarSupervisor()) {
            $('#modalSupervisor').modal('hide');
            guardarDivCheckListRolado();
        }
    });

    $("#btnOKSupervisor").click(function () {
        BloquearModal();
        var credencialesValidas = validarCamposSupervisor();
        if (credencialesValidas) {
            BloquearModal();
            var usuario = $("#txtUsuario").val();
            var contraseña = $("#txtContrasenia").val();
            validarCredencialesUsuario(usuario, contraseña);
        } else {
            MostrarMensaje("Usuario y/o contraseña son requeridos", asignarFocoUsuario);
        }        
    });

    $("#txtUsuario").keydown(function (event) {
        if (event.which == 13) {
            $("#txtContrasenia").focus();
        }
    });

    $("#txtContrasenia").keydown(function (event) {
        if (event.which == 13) {
            $("#btnOKSupervisor").focus();
        }
    });
};

guardarSupervisor = function () {
    var guardar = true;
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);    
    if (usuarioId > 0) {
        if (!$.trim($("#txtObservaciones").val())) {
            guardar = false;
            BloquearModal();
            MostrarMensaje("Debe ingresar las observaciones, para continuar", function () {
                DesbloquearModal();
            });
            DesbloquearPantalla();            
        }
    }
    return guardar;
};

validarCamposSupervisor = function () {
    var valido = true;
    var inputs = $(".supervisor");
    inputs.each(function () {
        if (!$.trim($(this).val())) {
            valido = false;
        }
    });
    return valido;
};

validarCredencialesUsuario = function (usuario, contraseña) {
    var jUsuario = { "usuario": usuario, "contraseña": contraseña };
    EjecutarWebMethod(window.location.pathname + '/ValidarCredencialesUsuario', jUsuario, usuarioValidio
                    , "Ocurrió un error al consultar los datos generales");
};

usuarioValidio = function(msg) {
    var usuario = msg.d;
    var mensaje = usuario.Mensaje;
    if (mensaje != 'OK') {
        $("#hdnUsuarioSupervisorID").val('');
        MostrarMensaje(mensaje, asignarFocoUsuario);
        DesbloquearModal();
        return;
    }
    DesbloquearModal();
    limpiarTemplatesParametros();
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId == 0) {
        $("#hdnUsuarioSupervisorID").val(usuario.Usuario.UsuarioID);
        $("#hdnUsuarioID").val(usuario.Usuario.UsuarioID);
        $("#txtResponsable").val(usuario.Usuario.Nombre);
        $("#lblEstatus").text('Supervisado');
        usuarioValido = true;
        usuarioLogeadoSupervisor = true;
        checkListSupervisado = true;
        $("#btnOKSupervisor").attr('disabled', true);
        $("#btnSupervisor").attr('disabled', true);
        asignarClaseSupervisor();
        $('#modalSupervisor').modal('hide');
    }
    limpiarCamposDatosGenerales();
};

asignarFocoUsuario = function () {
    $("#txtUsuario").focus();
    DesbloquearModal();
};

//Función para mostrar los mensajes
MostrarMensaje = function (mensaje, funcionCallback) {
    bootbox.dialog({
        message: mensaje,
        buttons: {
            Aceptar: {
                label: "Ok",
                callback: funcionCallback
            }
        }
    });
};

cancelarModalSupervisor = function () {
    bootbox.dialog({
        message: "No se ha guardado la información, ¿Está seguro de cancelar sin guardar la información capturada?",
        buttons: {
            Aceptar: {
                label: "Si",
                callback: function () {
                    limpiarCamposModalSupervisor();
                    $('#modalSupervisor').modal('hide');
                }
            },
            Cancelar: {
                label: "No",
                callback: function () {
                    DesbloquearModal();
                }
            }
        }
    });
};

limpiarCamposModalSupervisor = function () {
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId == 0) {
        $('#txtUsuario').val('');
        $('#txtContrasenia').val('');
        $('#txtObservaciones').val('');
        $('#txtUsuario').attr('disabled', false);
        $('#txtContrasenia').attr('disabled', false);
        $('#btnOKSupervisor').attr('disabled', false);

        if ($("#hdnUsuarioSupervisorID").val().length == 0) {
            usuarioLogeadoSupervisor = false;
        }        
    }
};

cargarTemplatesParametros = function () {
    cargarTemplateHumedad();
    cargarTemplateSurfactante();
    cargarTemplateAgua();
    cargarTemplateGrano();
    cargarTemplateDiesel();

    asignarEventosSurfactante();
    asignarEventosAgua();
    asignarEventosGrano();
    asignarEventosHumerdad();
    BloquearControlesFinales();

};

asignarEventosHumerdad = function () {
    $(".humedad").attr("oninput", "maxLengthCheck(this)");
    $(".humedad").attr("maxlength", "7");
    $(".humedad").attr("disabled", true);
    $(".humedad").numericInput();

    $('#Humedad').on("keydown", ".humedad", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#Humedad').on("focusout", ".humedad", function () {
        var name = $(this).attr("name");
        var clave = $(this).attr("id");
        validarHumedad(name, clave);
    });
};

validarHumedad = function (name, clave) {
    var roladoBodega = $("#txtHumedad" + enumHumedad.granoRoladoBodega.value).val();
    var entBodega = $("#txtHumedad" + enumHumedad.granoEnteroBodega.value).val();
    if (roladoBodega > 0 && entBodega > 0) {
        $("#txtHumedad" + enumHumedad.adicionAguaSurfactante.value).val(roladoBodega - entBodega);
    }
};

asignarEventosHorometro = function () {
    $('.horometro').inputmask('99:99');
    $(".horometro").attr("oninput", "maxLengthCheck(this)");
    $(".horometro").attr("maxlength", "7");

    $('#Horometro').on("keydown", ".horometro", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13 || key == 9) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
            var name = $(this).attr("name");
            var clave = $(this).attr("id");
            validaHorometros(name, clave);
        }
    });
    $('.horometro').on("focusout", function (e) {
        e.preventDefault();
        var inputs = $(this).closest('form').find(':input:enabled');
        inputs.eq(inputs.index(this) + 1).focus();
        var name = $(this).attr("name");
        var clave = $(this).attr("id");
        return validaHorometros(name, clave);
    });
};

validaHorometros = function(name, clave) {
    var valido = true;
    var inicioHoras = TryParseInt($("#txtInicio" + name).val().split(':')[0], 0);
    var inicioMinutos = TryParseInt($("#txtInicio" + name).val().split(':')[1], 0);

    if ($("#txtInicio" + name).val().trim().length > 0) {
        if (clave === ("txtInicio" + name)) {

            if ($("#txtInicio" + name).val().indexOf("_") > -1) {
                if (!mensajeHorometros) {
                    mensajeHorometros = true;
                    MostrarMensaje("El horómetro inicial no tiene el formato correcto", function() {
                        $("#txtInicio" + name).focus();
                        mensajeHorometros = false;
                    });
                }
                return false;
            }

            if ((inicioHoras > 24 || inicioMinutos >= 60) || (inicioHoras == 24 && inicioMinutos > 0) ||
                ($("#txtInicio" + name).val().split(':')[1] == '__' || $("#txtInicio" + name).val().split(':')[1] == '') ||
                ($("#txtInicio" + name).val().split(':')[0] == '__' || $("#txtInicio" + name).val().split(':')[0] == '')) {
                if (!mensajeHorometros) {
                    mensajeHorometros = true;
                    MostrarMensaje("El horómetro inicial no tiene el formato correcto", function() {
                        $("#txtInicio" + name).val('');
                        $("#txtInicio" + name).focus();
                        mensajeHorometros = false;
                    });
                }
                return false;
            }
        }
    }
    //if (clave === ("txtFin" + name)) {
    if ($("#txtFin" + name).val().trim().length > 0) {

        var horaFinal = $("#txtFin" + name).val();
        var horaInicial = $("#txtInicio" + name).val();

        inicioHoras = TryParseInt($("#txtInicio" + name).val().split(':')[0], 0);
        inicioMinutos = TryParseInt($("#txtInicio" + name).val().split(':')[1], 0);

        if ($("#txtInicio" + name).val().indexOf("_") > -1) {
            if (!mensajeHorometros) {
                mensajeHorometros = true;
                MostrarMensaje("El horómetro inicial no tiene el formato correcto", function() {
                    $("#txtInicio" + name).focus();
                    mensajeHorometros = false;
                });
            }
            return false;
        }

        if ((inicioHoras > 24 || inicioMinutos >= 60) || (inicioHoras == 24 && inicioMinutos > 0) ||
            ($("#txtInicio" + name).val().split(':')[1] == '__' || $("#txtInicio" + name).val().split(':')[1] == '') ||
            ($("#txtInicio" + name).val().split(':')[0] == '__' || $("#txtInicio" + name).val().split(':')[0] == '')) {
            if (!mensajeHorometros) {
                mensajeHorometros = true;
                MostrarMensaje("El horómetro inicial no tiene el formato correcto", function() {
                    mensajeHorometros = false;
                    $("#txtInicio" + name).val('');
                    $("#txtInicio" + name).focus();
                });
            }
            return false;
        }

        var finalHoras = TryParseInt($("#txtFin" + name).val().split(':')[0], 0);
        var finalMinutos = TryParseInt($("#txtFin" + name).val().split(':')[1], 0);

        if ($("#txtFin" + name).val().indexOf("_") > -1) {
            if (!mensajeHorometros) {
                mensajeHorometros = true;
                MostrarMensaje("El horómetro final no tiene el formato correcto", function() {
                    mensajeHorometros = false;
                    $("#txtFin" + name).focus();
                });
            }
            return false;
        }

        if ((finalHoras > 24 || finalMinutos >= 60) || (finalHoras == 24 && finalMinutos > 0) ||
            ($("#txtFin" + name).val().split(':')[1] == '__' || $("#txtFin" + name).val().split(':')[1] == '') ||
            ($("#txtFin" + name).val().split(':')[0] == '__' || $("#txtFin" + name).val().split(':')[0] == '')) {
            if (!mensajeHorometros) {
                mensajeHorometros = true;
                MostrarMensaje("El horómetro final no tiene el formato correcto", function() {
                    mensajeHorometros = false;
                    $("#txtFin" + name).val('');
                    $("#txtFin" + name).focus();
                });
            }
            return false;
        }

        var fechaInicial = new Date(1970, 1, 1, horaInicial.split(':')[0], horaInicial.split(':')[1], 0, 0);
        var fechaFinal = new Date(1970, 1, 1, horaFinal.split(':')[0], horaFinal.split(':')[1], 0, 0);
        if (fechaInicial >= fechaFinal) {
            if (!mensajeHorometros) {
                mensajeHorometros = true;
                MostrarMensaje("El horómetro final debe de ser mayor al horómetro inicial", function() {
                    mensajeHorometros = false;
                    $("#txtFin" + name).val('');
                    $("#txtFin" + name).focus();
                });
            }
            return false;
        }

        var diferenciaHoras = DiferenciaHorasFechas(fechaInicial, fechaFinal);
        var horas = Math.floor(diferenciaHoras);
        var minutos = diferenciaHoras % 1;
        var minutosFormateados = Math.round(minutos * 60);
        if (minutosFormateados < 10) {
            minutosFormateados = '0' + minutosFormateados;
        }
        var diferenciaFinal = horas + ':' + minutosFormateados;
        $("#txtResultadoHorometro" + name).val(diferenciaFinal);
    } else {
        $("#txtResultadoHorometro" + name).val('');
    }
    //}
    return valido;
};

asignarEventosSurfactante = function () {
    $('.surfactante').numericInput();
    $('.surfactante').attr("oninput", "maxLengthCheck(this)");
    $('.surfactante').attr("maxlength", "6");

    $('#Surfactante').on("keydown", ".surfactante", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#txtSurfactanteFinal' + enumSurfactante.surfactante.value).on("focusout", function () {
        validaSurfactante();
    });
};

validaSurfactante = function () {
    var surfactanteInicio = TryParseDecimal($("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).val(), 0);
    var surfactanteFinal = TryParseDecimal($("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).val(), 0);

    var control;
    var mensaje = "";
    if (surfactanteFinal > 0 && surfactanteInicio > 0) {
        if (surfactanteFinal < surfactanteInicio) {
            mensaje = "El surfactante final debe ser mayor al surfactante inicial.";
            $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).focus();
            control = $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value);
        } else {
            if (surfactanteInicio > surfactanteFinal) {
                mensaje = "El surfactante inicial no debe ser mayor que el surfactante final";
                $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).focus();
                control = $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value);
            }
        }
    }
    if (mensaje.length > 0) {
        MostrarMensaje(mensaje, function () {
            control.val('');
            control.focus();
        });
    }
};

asignarEventosAgua = function () {
    $('.agua').attr("oninput", "maxLengthCheck(this)");
    $('.agua').attr("maxlength", "8");
    $('.agua').numeric({ negative: false, decimal: '.', maxDecimalPlaces: 2 });

    $("#txtAgua" + enumAgua.total.value).attr('disabled', true);

    $('#Aguas').on("keydown", ".agua", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#txtAgua' + enumAgua.inicial.value).on("focusout", function (e) {
        var control;
        var mensaje = '';
        validaLitrosAgua();
        var litrosIniciales = TryParseDecimal($("#txtAgua" + enumAgua.inicial.value).val(), 0);
        var litrosFinales = TryParseDecimal($("#txtAgua" + enumAgua.final.value).val(), 0);
        if (litrosIniciales > 0 && litrosFinales > 0) {
            if (litrosIniciales > litrosFinales) {
                mensaje = "El contador inicial de agua no debe ser mayor que el contador final";
                control = $("#txtAgua" + enumAgua.inicial.value);
                control.focus();
                e.preventDefault();
            }
            if (mensaje.length == 0) {
                $("#txtAgua" + enumAgua.total.value).val(litrosFinales - litrosIniciales);
            } else {
                MostrarMensaje(mensaje, function () {
                    control.val('');
                });
            }
        }

        FormatearCantidad($("#txtAgua" + enumAgua.inicial.value).val());
    });

    $('#txtAgua' + enumAgua.final.value).on("focusout", function () {
        validaLitrosAgua();
        FormatearCantidad($(this).val());
    });
};

validaLitrosAgua = function () {
    var litrosIniciales = TryParseDecimal($("#txtAgua" + enumAgua.inicial.value).val(), 0);
    var litrosFinales = TryParseDecimal($("#txtAgua" + enumAgua.final.value).val(), 0);

    var mensaje = "";
    var control;
    if (litrosIniciales > 0 && litrosFinales > 0) {
        if (litrosFinales < litrosIniciales) {
            mensaje = "El contador final de agua debe ser mayor al contador inicial de agua";
            control = $("#txtAgua" + enumAgua.final.value);
        } else {
            if (litrosIniciales > litrosFinales) {
                mensaje = "El contador inicial de agua no debe ser mayor que el contador final";
                control = $("#txtAgua" + enumAgua.inicial.value);
            }
        }
        if (mensaje.length == 0) {
            $("#txtAgua" + enumAgua.total.value).val(litrosFinales - litrosIniciales);
        } else {
            MostrarMensaje(mensaje, function () {
                control.val('');
                control.focus();
            });
        }
    }
};

asignarEventosGrano = function () {
    $(".deshabilitarGrano").attr('disabled', true);
    $(".habilitarGrano").numericInput();
    $(".habilitarGrano").attr("oninput", "maxLengthCheck(this)");
    $(".habilitarGrano").attr("maxlength", "7");

    $('#Granos').on("keydown", ".grano", function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    $('#Granos').on("focusout", ".grano", function () {
        validaGranos();
    });
};

validaGranos = function () {
    var granoEnteroPP = TryParseDecimal($("#txtGrano" + enumGrano.granoEnteroPP.value).val(), 0);
    var granEnteroBodega = TryParseDecimal($("#txtGrano" + enumGrano.granoEnteroBodega.value).val(), 0);

    var habilitado = $("#txtGrano" + enumGrano.granoEnteroBodega.value).attr('disabled');
    if (!habilitado) {
        if ($("#txtGrano" + enumGrano.granoEnteroBodega.value).val().length > 0) {
            if (granEnteroBodega < granoEnteroPP) {
                MostrarMensaje("El total final grano ent. Bodega debe ser mayor al total grano pp.", function () {
                    $("#txtGrano" + enumGrano.granoEnteroBodega.value).val('');
                    $("#txtGrano" + enumGrano.granoEnteroBodega.value).focus();
                });
                return false;
            }
        } else {
            return false;
        }
    }
    var totalGranoProceso = granEnteroBodega - granoEnteroPP;
    $("#txtGrano" + enumGrano.granoProcesado.value).val(totalGranoProceso);

    var superavitGranoRolado = $("#txtHumedad" + enumHumedad.adicionAguaSurfactante.value).val() * totalGranoProceso;
    $("#txtGrano" + enumGrano.granoSuperavitRolado.value).val(superavitGranoRolado);
    $("#txtGrano" + enumGrano.granoRolado.value).val(totalGranoProceso + superavitGranoRolado);

    var totalGranoRolado = $("#txtGrano" + enumGrano.granoRolado.value).val();
    asignarDieselToneladaGranoRolado(totalGranoRolado);
};

asignarDieselToneladaGranoRolado = function (totalGranoRolado) {
    var diesel = $("#txtDiesel" + enumDiesel.dieselCaldera.value).val();
    $("#txtDiesel" + enumDiesel.dieselRolado.value).val(diesel / totalGranoRolado);
};

cargarTemplateHorometro = function () {
    var valores = {};
    var listaValores = new Array();

    $("#ddlRoladora > option").each(function () {
        if ($(this).val() > 0) {
            var valor = {};
            valor.RoladoraID = $(this).val();
            valor.Roladora = $(this).text();

            listaValores.push(valor);
        }
    });
    valores.Horometros = listaValores;

    $('#divHorometro').html('');
    $('#divHorometro').setTemplateURL('../Templates/ParametroHorometro.htm');
    $('#divHorometro').processTemplate(valores);
    deshabilitarControlesHorometroFinal();
    obtenerHorometros();
};

deshabilitarControlesHorometroFinal = function () {
    $(".horometroFin").each(function () {
        $(this).attr("disabled", true);
    });
};

cargarTemplateHumedad = function () {
    var valores = {};
    var listaValores = new Array();

    var valor = {};
    valor.HumedadID = enumHumedad.granoRoladoBodega.value;
    valor.Humedad = enumHumedad.granoRoladoBodega.descripcion;
    listaValores.push(valor);

    valor = {};
    valor.HumedadID = enumHumedad.granoEnteroBodega.value;
    valor.Humedad = enumHumedad.granoEnteroBodega.descripcion;
    listaValores.push(valor);

    valor = {};
    valor.HumedadID = enumHumedad.adicionAguaSurfactante.value;
    valor.Humedad = enumHumedad.adicionAguaSurfactante.descripcion;
    listaValores.push(valor);

    valores.Humedades = listaValores;

    $('#divHumedad').html('');
    $('#divHumedad').setTemplateURL('../Templates/ParametroHumedad.htm');
    $('#divHumedad').processTemplate(valores);
};

cargarTemplateSurfactante = function () {
    var valores = {};
    var listaValores = new Array();

    var valor = {};
    valor.SurfactanteID = enumSurfactante.surfactante.value;
    valor.Surfactante = enumSurfactante.surfactante.descripcion;
    listaValores.push(valor);

    valores.Surfactantes = listaValores;

    $('#divSurfantante').html('');
    $('#divSurfantante').setTemplateURL('../Templates/ParametroSurfactante.htm');
    $('#divSurfantante').processTemplate(valores);
};

cargarTemplateAgua = function () {
    var valores = {};
    var listaValores = new Array();

    var valor = {};
    valor.AguaID = enumAgua.inicial.value;
    valor.Agua = enumAgua.inicial.descripcion;
    listaValores.push(valor);

    valor = {};
    valor.AguaID = enumAgua.final.value;
    valor.Agua = enumAgua.final.descripcion;
    listaValores.push(valor);

    valor = {};
    valor.AguaID = enumAgua.total.value;
    valor.Agua = enumAgua.total.descripcion;
    listaValores.push(valor);

    valores.Aguas = listaValores;

    $('#divAguas').html('');
    $('#divAguas').setTemplateURL('../Templates/ParametroAgua.htm');
    $('#divAguas').processTemplate(valores);
};

cargarTemplateGrano = function () {
    var valores = {};
    var listaValores = new Array();

    var valor = {};
    valor.GranoID = enumGrano.granoEnteroPP.value;
    valor.Grano = enumGrano.granoEnteroPP.descripcion;
    valor.Clase = enumGrano.granoEnteroPP.clase;
    listaValores.push(valor);

    valor = {};
    valor.GranoID = enumGrano.granoEnteroBodega.value;
    valor.Grano = enumGrano.granoEnteroBodega.descripcion;
    valor.Clase = enumGrano.granoEnteroBodega.clase;
    listaValores.push(valor);

    valor = {};
    valor.GranoID = enumGrano.granoProcesado.value;
    valor.Grano = enumGrano.granoProcesado.descripcion;
    valor.Clase = enumGrano.granoProcesado.clase;
    listaValores.push(valor);

    valor = {};
    valor.GranoID = enumGrano.granoSuperavitRolado.value;
    valor.Grano = enumGrano.granoSuperavitRolado.descripcion;
    valor.Clase = enumGrano.granoSuperavitRolado.clase;
    listaValores.push(valor);

    valor = {};
    valor.GranoID = enumGrano.granoRolado.value;
    valor.Grano = enumGrano.granoRolado.descripcion;
    valor.Clase = enumGrano.granoRolado.clase;
    listaValores.push(valor);

    valores.Granos = listaValores;

    $('#divGrano').html('');
    $('#divGrano').setTemplateURL('../Templates/ParametroGrano.htm');
    $('#divGrano').processTemplate(valores);
};

cargarTemplateDiesel = function () {
    var valores = {};
    var listaValores = new Array();

    var valor = {};
    valor.DieselID = enumDiesel.dieselCaldera.value;
    valor.Descripcion = enumDiesel.dieselCaldera.descripcion;
    listaValores.push(valor);

    valor = {};
    valor.DieselID = enumDiesel.dieselRolado.value;
    valor.Descripcion = enumDiesel.dieselRolado.descripcion;
    listaValores.push(valor);

    valores.Diesel = listaValores;

    $('#divDiesel').html('');
    $('#divDiesel').setTemplateURL('../Templates/ParametroDiesel.htm');
    $('#divDiesel').processTemplate(valores);
};

guardarDivParametrosRolado = function () {
    if ($("#ddlTurno").val() <= 0) {
        MostrarMensaje("Debe de seleccionar el turno, en la pestaña checklist de rolado, para continuar.", null);
        DesbloquearPantalla();
    } else {
        var guardar = validarGuardarDivParametros();
        if (guardar) {
            guardarParametrosRolado();
        }
    }
};

guardarParametrosRolado = function () {
    generarCheckListoRoladoraGeneral();
    generarListaCheckListRoladora();
    generarListaCheckListRoladoraHorometro();
    //generarCheckListoRoladora();

    var jParametros = { "listaCheckListRoladora": listaCheckListRoladora, "checkListRoladoraGeneral": checkListRoladoraGeneral, 'listaCheckListRoladoraHorometro': listaCheckListRoladoraHorometro };
    EjecutarWebMethod(window.location.pathname + '/GuardarParametrosCheckList', jParametros, guardadoExitoso
                    , "Ocurrió un error guardar los parametros");
};

generarListaCheckListRoladora = function () {
    listaCheckListRoladora = new Array();
    $('#ddlRoladora > option').each(function () {
        if ($(this).val() != '0') {
            checkListRoladora = {};
            roladora = {};

            checkListRoladora.RoladoraID = $(this).val();
            roladora.RoladoraID = $(this).val();
            checkListRoladora.CheckListRoladoraGeneral = checkListRoladoraGeneral;
            checkListRoladora.Roladora = roladora;
            var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
            if (usuarioId > 0) {
                checkListRoladora.UsuarioIDResponsable = $("#hdnUsuarioSupervisorID").val();
                checkListRoladoraGeneral.Observaciones = $("#txtObservaciones").val();
            } else {
                checkListRoladora.UsuarioIDResponsable = $("#hdnUsuarioID").val();
            }
            checkListRoladora.UsuarioCreacionID = $("#hdnUsuarioID").val();
            checkListRoladora.UsuarioModificacionID = $("#hdnUsuarioID").val();
            checkListRoladora.FechaCheckList = fechaCheckList;
            checkListRoladora.Hora = $("#txtHoraInicio").val();
            listaCheckListRoladora.push(checkListRoladora);
        }
    });
};

generarListaCheckListRoladoraHorometro = function () {
    listaCheckListRoladoraHorometro = new Array();
    $('#ddlRoladora > option').each(function () {
        if ($(this).val() != '0') {
            checkListRoladoraHorometro = {};
            checkListRoladoraHorometro.CheckListRoladoraGeneral = checkListRoladoraGeneral;
            roladora = {};
            roladora.RoladoraID = $(this).val();
            checkListRoladoraHorometro.Roladora = roladora;
            var horaInicial = $("#txtInicio" + $(this).val()).val();
            var horaFinal = $("#txtFin" + $(this).val()).val();
            if (horaInicial != '' && horaInicial != null) {
                checkListRoladoraHorometro.HorometroInicial = horaInicial;
            }
            if (horaFinal != '' && horaFinal != null) {
                checkListRoladoraHorometro.HorometroFinal = horaFinal;
            }
            checkListRoladoraHorometro.UsuarioCreacionID = $("#hdnUsuarioID").val();
            checkListRoladoraHorometro.UsuarioModificacionID = $("#hdnUsuarioID").val();
            listaCheckListRoladoraHorometro.push(checkListRoladoraHorometro);
        }
    });
};

cancelarParametros = function () {
    bootbox.dialog({
        message: "No se ha guardado la información, ¿Está seguro de cancelar sin guardar la información capturada?",
        buttons: {
            Aceptar: {
                label: "Si",
                callback: function () {
                    cargarTemplatesParametros();
                    cargarTemplateHorometro();
                    asignarEventosHorometro();
                }
            },
            Cancelar: {
                label: "No",
                callback: function () {
                }
            }
        }
    });
};


generarCheckListoRoladoraGeneral = function () {
    checkListRoladoraGeneral = {};

    checkListRoladoraGeneral.Turno = $("#ddlTurno").val();
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId > 0) {
        checkListRoladoraGeneral.UsuarioIDSupervisor = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    }
    fechaCheckList = ToDate($("#txtFecha").val());
    checkListRoladoraGeneral.FechaInicio = fechaCheckList;
    checkListRoladoraGeneral.UsuarioCreacionID = $("#hdnUsuarioID").val();
    checkListRoladoraGeneral.UsuarioModificacionID = $("#hdnUsuarioID").val();

    checkListRoladoraGeneral.SurfactanteInicio = $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).val();
    checkListRoladoraGeneral.SurfactanteFin = $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).val();

    checkListRoladoraGeneral.ContadorAguaInicio = $("#txtAgua" + enumAgua.inicial.value).val();
    checkListRoladoraGeneral.ContadorAguaFin = $("#txtAgua" + enumAgua.final.value).val();

    checkListRoladoraGeneral.GranoEnteroFinal = $("#txtGrano" + enumGrano.granoEnteroBodega.value).val();

};

generarCheckListoRoladora = function () {
    checkListRoladora = {};
    roladora = {};

    checkListRoladora.RoladoraID = $("#ddlRoladora").val();
    roladora.RoladoraID = $("#ddlRoladora").val();
    checkListRoladora.CheckListRoladoraGeneral = checkListRoladoraGeneral;
    checkListRoladora.Roladora = roladora;
    var usuarioId = TryParseInt($("#hdnUsuarioSupervisorID").val(), 0);
    if (usuarioId) {
        checkListRoladora.UsuarioIDResponsable = $("#hdnUsuarioSupervisorID").val();
        checkListRoladoraGeneral.Observaciones = $("#txtObservaciones").val();
    } else {
        checkListRoladora.UsuarioIDResponsable = $("#hdnUsuarioID").val();
    }
    checkListRoladora.UsuarioCreacionID = $("#hdnUsuarioID").val();
    checkListRoladora.UsuarioModificacionID = $("#hdnUsuarioID").val();
    checkListRoladora.FechaCheckList = fechaCheckList;
    checkListRoladora.HorometroInicial = $("#txtInicio" + $("#ddlRoladora").attr("name")).val();
    checkListRoladora.HorometroFinal = $("#txtFin" + $("#ddlRoladora").attr("name")).val();
    checkListRoladora.Hora = $("#txtHoraInicio").val();

    checkListRoladora.HorometroInicial = '';
    checkListRoladora.HorometroFinal = null;

    var horaInicial = $("#txtInicio" + $("#ddlRoladora").val()).val();
    var horaFinal = $("#txtFin" + $("#ddlRoladora").val()).val();
    if (horaInicial != '' && horaInicial != null) {
        checkListRoladora.HorometroInicial = horaInicial;
    }
    if (horaFinal != '' && horaFinal != null) {
        checkListRoladora.HorometroFinal = horaFinal;
    }
};

generarCheckListRoladoraDetalle = function () {
    checkListRoladoraDetalle = new Array();

    $(".rango").each(function () {
        var item = {};
        item.CheckListRoladoraRangoID = $(this).val();
        var name = $(this).attr("name");
        item.CheckListRoladoraAccionID = $("#dllAccion" + name).val();
        item.UsuarioCreacionID = $("#hdnUsuarioID").val();

        checkListRoladoraDetalle.push(item);
    });
};

generarCheckListRoladoraHorometro = function () {
    checkListRoladoraHorometro = {};

    $(".rango").each(function () {
        var item = {};
        item.CheckListRoladoraRangoID = $(this).val();
        var name = $(this).attr("name");
        item.CheckListRoladoraAccionID = $("#dllAccion" + name).val();
        item.UsuarioCreacionID = $("#hdnUsuarioID").val();

        checkListRoladoraDetalle.push(item);
    });
};

obtenerHorometros = function () {
    var turno = $('#ddlTurno').val();
    var jParametros = { "turno": turno };
    EjecutarWebMethod(window.location.pathname + '/ObtenerHorometros', jParametros, obtenerHorometrosSuccess
                    , "Ocurrió un error al consultar los horometros");
};

obtenerHorometrosSuccess = function (msg) {
    if (msg.d.length > 0) {
        var horometro = msg.d[0];
        asignarValoresGeneral(horometro.CheckListRoladoraGeneral);
    }
    deshabilitarControlesHorometroFinal();
    var horometros = Enumerable.From(msg.d).OrderBy(function (x) { return x.CheckListRoladoraHorometroID; }).ToArray();

    var primerHorometro = Enumerable.From(horometros).FirstOrDefault();

    if (primerHorometro == undefined || primerHorometro == null) {
        $("#ddlRoladora > option").each(function () {

            var roladoraID = $(this).val();
            $("#txtFin" + roladoraID).attr('disabled', true);
        });
    }

    var horometroInicial = primerHorometro.HorometroInicial;
    if (horometroInicial == null) {
        $("#ddlRoladora > option").each(function () {

            var roladoraID = $(this).val();
            $("#txtFin" + roladoraID).attr('disabled', true);
        });
        return;
    }

    var fechaInicio = new Date(parseInt(primerHorometro.CheckListRoladoraGeneral.FechaInicio.replace(/^\D+/g, '')));

    var fechaPrimerHorometro = new Date(fechaInicio.getFullYear(), fechaInicio.getMonth(), fechaInicio.getDate(), horometroInicial.split(':')[0], horometroInicial.split(':')[1])
    var fechaServidor = new Date(parseInt(primerHorometro.FechaServidor.replace(/^\D+/g, '')));

    var diferenciaHoras = DiferenciaHorasFechas(fechaPrimerHorometro, fechaServidor);
    var aplicanControlesFinales = true;
    var horas = Math.floor(diferenciaHoras);
    if (horas < 7) {
        BloquearControlesFinales();
        aplicanControlesFinales = false;
    } else {
        DesBloquearControlesFinales();
    }

    $(msg.d).each(function () {
        var roladoraID = this.Roladora.RoladoraID;
        if (this.HorometroInicial != '' && this.HorometroInicial != '0') {
            $("#txtInicio" + roladoraID).val(this.HorometroInicial);
            $("#txtInicio" + roladoraID).attr('disabled', true);
        }
        if (!aplicanControlesFinales) {
            $("#txtFin" + roladoraID).attr('disabled', true);
        }
        else {
            $("#txtFin" + roladoraID).attr('disabled', false);
        }
        if (this.HorometroFinal != null && this.HorometroFinal != '') {
            $("#txtFin" + roladoraID).val(this.HorometroFinal);
            $("#txtFin" + roladoraID).attr('disabled', true);

            var name = $("#txtFin" + roladoraID).attr("name");
            var clave = $("#txtFin" + roladoraID).attr("id");
            validaHorometros(name, clave);
        }
    });

};

BloquearControlesFinales = function () {
    $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).attr('disabled', true);
    $("#txtAgua" + enumAgua.final.value).attr('disabled', true);
    $("#txtGrano" + enumGrano.granoEnteroBodega.value).attr('disabled', true);
};

DesBloquearControlesFinales = function () {
    $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).attr('disabled', false);
    $("#txtAgua" + enumAgua.final.value).attr('disabled', false);
    $("#txtGrano" + enumGrano.granoEnteroBodega.value).attr('disabled', false);
};

asignarValoresGeneral = function (roladoraGeneral) {
    setTimeout(function () {
        if (roladoraGeneral.UsuarioIDSupervisor > 0) {
            $("#hdnUsuarioSupervisorID").val(roladoraGeneral.UsuarioIDSupervisor);
            usuarioValido = true;
        }
        $("#txtObservaciones").val(roladoraGeneral.Observaciones);

        if (roladoraGeneral.SurfactanteInicio != '' && roladoraGeneral.SurfactanteInicio != null) {
            $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).val(roladoraGeneral.SurfactanteInicio);
            $("#txtSurfactanteInicio" + enumSurfactante.surfactante.value).attr('disabled', true);
        }

        if (roladoraGeneral.SurfactanteFin != '' && roladoraGeneral.SurfactanteFin != null) {
            $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).val(roladoraGeneral.SurfactanteFin);
            $("#txtSurfactanteFinal" + enumSurfactante.surfactante.value).attr('disabled', true);
        }

        if (roladoraGeneral.ContadorAguaInicio != '' && roladoraGeneral.ContadorAguaInicio != null) {
            $("#txtAgua" + enumAgua.inicial.value).val(roladoraGeneral.ContadorAguaInicio);
            $("#txtAgua" + enumAgua.inicial.value).attr('disabled', true);
        }
        if (roladoraGeneral.ContadorAguaFin != '' && roladoraGeneral.ContadorAguaFin != null) {
            $("#txtAgua" + enumAgua.final.value).val(roladoraGeneral.ContadorAguaFin);
            $("#txtAgua" + enumAgua.final.value).attr('disabled', true);
        }

        if (roladoraGeneral.GranoEnteroFinal != '' && roladoraGeneral.GranoEnteroFinal != null) {
            $("#txtGrano" + enumGrano.granoEnteroBodega.value).val(roladoraGeneral.GranoEnteroFinal);
            $("#txtGrano" + enumGrano.granoEnteroBodega.value).attr('disabled', true);
        }
    }, 500);
};