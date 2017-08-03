//Variables globales
var msjAbierto = 0;
var fechaInicial = new Date(Date.now());
var fechaSeleccionada;
var semanasContador = 0;
var numeroSemanaAnterior = 0;
var numeroSemanaNuevo = 0;
var esUsuarioLogistica = false;
var esJefeManejo = false;
var pedidoSemana = {};
var usuario;
var haySolicitudPendiente = false;
var esSolicitante = 0;
var pedidoGanadoID = 0;
var semanaInicial = 0;
var diasHabiles = {};
var pedidoID = 0;
var semana = {};
var primeraCarga = false;
var diaActual = false;
var esDiaHabil = true;
var hayOrganizacionesActivas = true;
var anioNuevo = false;
var anioActual = new Date().getFullYear();

//Carga inicial de la pantalla
$(document).ready(function () {
    Inicializar();
    AsignarEventosControles();
    LimpiarPantalla();
    DeshabilitarCeldas();
    ValidarOrganizacionesActivas();
    PreCondiciones();
    OrganizacionUsuario();
    ObtenerUsuario();
    ObtenerRol();
    if (esJefeManejo) {
        $("#txtNumOrganizacion").prop("disabled", true);
        $("#btnAyudaOrganizacion").unbind("click");
        $("#btnAyudaOrganizacion").removeProp("href");
        $("#btnAyudaOrganizacion").removeProp("id");
    }
    LlenarGridSemana(fechaInicial);
    $("#btnAutorizar").prop("disabled", true);
    $("#btnCancelarSolicitud").prop("disabled", true);
    semanaInicial = parseInt($("#txtFecha").val());
    $("#txtFecha").datepicker({
        firstDay: 1,
        showOn: 'button',
        buttonImage: '../assets/img/calander.png',
        onSelect: function() {
            fechaSeleccionada = $('#txtFecha').datepicker('getDate');
            $('#txtFecha').val(fechaSeleccionada.getWeek());
            $('#txtFecha').change();
            LlenarGridSemana(fechaSeleccionada);
        },
        dateFormat: 'dd-mm-yy'
    });
    $.datepicker.setDefaults($.datepicker.regional['es']);
    fechaInicial = $('#txtFecha').datepicker('getDate');
    $("#txtFecha").focusout(function() {
        var fechaSeleccionada = $('#txtFecha').datepicker('getDate').toString("yyyy-mm-dd");
        if (fechaSeleccionada == null || Date.parse(fechaSeleccionada) == Date.parse(fechaInicial)) {
            $('#txtFecha').datepicker('setDate', fechaInicial).trigger('change');
        }
    });
    

    $("#txtFecha").keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    $(".numerico").numericInput();

    $("#txtPromedioCabezas").on("change input", function(e) {
        if ($("#txtPromedioCabezas").val().replace(/^0+/, '').trim()) {
            HabilitarCeldas();
            HabilitarDiasHabiles(diasHabiles);
            if (anioActual == anioNuevo) {
                if (numeroSemanaNuevo > semanaInicial + 1) {
                    HabilitarCeldas();
                }
            }
            if (anioActual < anioNuevo) {
                    HabilitarCeldas();
            }
            
        } else {
            DeshabilitarCeldas();
            $("#txtPromedioCabezas").focus();
        }
        CalcularCabezas(this);

    });

    $("#txtFecha").on("change input", function(e) {
        numeroSemanaNuevo = $(this).val();
        semanasContador = 7 * (Math.round((fechaSeleccionada - fechaInicial) / 604800000));
    });

    $("#btnSemanaAnterior").click(function() {
        semanasContador -= 7;
        numeroSemanaNuevo = $("#txtFecha").val();
        var fecha = new Date();
        LlenarGridSemana(fecha.addDays(semanasContador));
    });
    $("#btnSemanaSiguiente").click(function() {
        semanasContador += 7;
        numeroSemanaNuevo = $("#txtFecha").val();
        var fecha = new Date();
        LlenarGridSemana(fecha.addDays(semanasContador));
    });

    $("#btnAutorizar").click(function () {
        BloquearPantalla();
        ActualizarComentarios(true);
    });
    $("#btnCancelarSolicitud").click(function () {
        BloquearPantalla();
        ActualizarComentarios(false);
    });

    $("#lblGuardarSemana").click(function() {
        GuardarSemana();
    });

    $("#lblActualizar").click(function() {
        semanasContador = 0;
        LlenarGridSemana(fechaInicial);
    });
    $("#btnLimpiar").click(function () {
        bootbox.dialog({
            message: window.msgCancelar,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    callback: function () {
                        LlenarGridSemana(fechaInicial.addDays(semanasContador));
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    callback: function () {
                        return true;
                    }
                }
            }
        });
        
    });
    diaActual = fechaInicial.getDay();
    $(".inputGrid").on("change input", CalcularCabezas(this));
});

PedidoEsIgual = function() {
    var pedidoSemanaNuevo = CrearObjetoPedidoGanado();
    if (pedidoSemanaNuevo.CabezasPromedio != pedidoSemana.CabezasPromedio) {
        return false;
    }
    if (pedidoSemanaNuevo.Lunes != pedidoSemana.Lunes) {
        return false;
    }
    if (pedidoSemanaNuevo.Martes != pedidoSemana.Martes) {
        return false;
    }
    if (pedidoSemanaNuevo.Miercoles != pedidoSemana.Miercoles) {
        return false;
    }
    if (pedidoSemanaNuevo.Jueves != pedidoSemana.Jueves) {
        return false;
    }
    if (pedidoSemanaNuevo.Viernes != pedidoSemana.Viernes) {
        return false;
    }
    if (pedidoSemanaNuevo.Sabado != pedidoSemana.Sabado) {
        return false;
    }
    if (pedidoSemanaNuevo.Domingo != pedidoSemana.Domingo) {
        return false;
    }
    return true;
};
GuardarSemana = function() {
    if (ValidarCamposVacios()) {
        if (pedidoGanadoID != 0) {
            if (PedidoEsIgual()) {
                var mensaje = window.msgGuardarExito;
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function() {
                        msjAbierto = 0;
                    });
                }
            } else {
                if (!esDiaHabil && numeroSemanaNuevo == semanaInicial + 1) {
                    $("#dlgMotivoCmabio").modal("show");
                } else {
                    BloquearPantalla();
                    var pedidoGanadoEspejoInfo = CrearObjetoPedidoGanado();
                    ActualizarPedidoGanado(pedidoGanadoEspejoInfo);
                }
            }
        } else {
            BloquearPantalla();
            var pedidoGanadoInfo = CrearObjetoPedidoGanado();
            GuardarPedidoGanado(pedidoGanadoInfo);
        }
    }

};
GuardarSemanaEspejo = function() {
    var mensaje = "";
    if ($("#txtMotivoCambio").val().trim()) {
        var pedidoGanadoInfo = CrearObjetoPedidoGanado();
        GuardarPedidoGanadoEspejo(pedidoGanadoInfo);
    } else {
        mensaje = window.msgJaulasMotivoCambioVacio;
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(mensaje, function() {
                msjAbierto = 0;
            });
            return false;
        }
    }
    return true;
};

function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}
CrearObjetoPedidoGanado = function () {
    var fecha = new Date();
    var fechaPrimerDiaSemana = sumarDias(fecha.addDays(semanasContador), -(fecha.addDays(semanasContador).getDay() - 1));
    var pedidoGanadoInfo = {};
    pedidoGanadoInfo.Organizacion = {};
    pedidoGanadoInfo.PedidoGanado = {};
    pedidoGanadoInfo.FechaInicio = fechaPrimerDiaSemana;
    pedidoGanadoInfo.Organizacion.OrganizacionID = parseInt($("#txtNumOrganizacion").val());
    pedidoGanadoInfo.CabezasPromedio = parseInt($("#txtPromedioCabezas").val());
    pedidoGanadoInfo.Lunes = parseInt($("#txtJaulasLunes").val());
    pedidoGanadoInfo.Martes = parseInt($("#txtJaulasMartes").val());
    pedidoGanadoInfo.Miercoles = parseInt($("#txtJaulasMiercoles").val());
    pedidoGanadoInfo.Jueves = parseInt($("#txtJaulasJueves").val());
    pedidoGanadoInfo.Viernes = parseInt($("#txtJaulasViernes").val());
    pedidoGanadoInfo.Sabado = parseInt($("#txtJaulasSabado").val());
    pedidoGanadoInfo.Domingo = parseInt($("#txtJaulasDomingo").val());
    pedidoGanadoInfo.Justificacion = $("#txtMotivoCambio").val();
    pedidoGanadoInfo.PedidoGanado.PedidoGanadoID = pedidoGanadoID;
    pedidoGanadoInfo.UsuarioCreacionID = 0;
    pedidoGanadoInfo.UsuarioSolicitanteID = 0;
    pedidoGanadoInfo.Estatus = null;
    return pedidoGanadoInfo;
};
    
GuardarPedidoGanadoEspejo = function (pedidoGanadoEspejoInfo) {
    $("#dlgMotivoCmabio").modal("hide");
    BloquearPantalla();
    var datos = { 'pedidoGanadoEspejoInfo': pedidoGanadoEspejoInfo };
    $.ajax({
        data: JSON.stringify(datos),
        type: "POST",
        url: "CapturaPedido.aspx/GuardarPedidoGanadoEspejo",
        contentType: "application/json; charset=utf-8",
        async:true,
        error: function () {
            DesbloquearPantalla();
            var mensaje = window.msgErrorGuardar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function(data) {
            $("#txtMotivoCambio").val("");
            if (data.d.PedidoGanadoEspejoID > 0) {
                if (!esJefeManejo) {
                    EnviarCorreo(window.clvSolicitud, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), window.clvCorreoJefeManejo, 0);
                } else {
                    EnviarCorreo(window.clvSolicitud, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), window.clvCorreoLogistica, 0);
                }
                var mensaje = window.msgGuardarExito;
                if (msjAbierto == 0) {
                    bootbox.alert(mensaje, function() {
                        msjAbierto = 0;
                    });
                }
                var fecha = new Date(Date.now());
                LlenarGridSemana(fecha.addDays(semanasContador));
            } else {
                DesbloquearPantalla();
                var mensaje = window.msgHaySolicitudes;
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function () {
                        msjAbierto = 0;
                    });
                }
            }
           
        }
    });
};
GuardarPedidoGanado = function(pedidoGanadoInfo) {
    var datos = { 'pedidoGanadoInfo': pedidoGanadoInfo };
    $.ajax({
        data: JSON.stringify(datos),
        type: "POST",
        url: "CapturaPedido.aspx/GuardarPedidoGanado",
        contentType: "application/json; charset=utf-8",
        error: function () {
            DesbloquearPantalla();
            var mensaje = window.msgErrorGuardar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function () {
            EnviarCorreo(window.clvRegistro, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), window.clvCorreoLogistica, 0);
            var mensaje = window.msgGuardarExito;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function () {
                    msjAbierto = 0;
                });
            }
            var fecha = new Date(Date.now());
            LlenarGridSemana(fecha.addDays(semanasContador));
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

ValidarCamposVacios = function() {
    var campos = $(".validarVacio:enabled");
    var campo;
    var mensaje = "";
    var esNulo = 0;


    for (var i = 0; i < campos.length; i++) {
        if (campos[i].value == null || !campos[i].value.trim()) {
            campo = campos[i];
            if ("txtJaulasLunes" == campos[i].id) {
                mensaje = window.msgJaulasLunesVacio;
            }
            if ("txtJaulasMartes" == campos[i].id) {
                mensaje = window.msgJaulasMartesVacio;
            }
            if ("txtJaulasMiercoles" == campos[i].id) {
                mensaje = window.msgJaulasMiercolesVacio;
            }
            if ("txtJaulasJueves" == campos[i].id) {
                mensaje = window.msgJaulasJuevesVacio;
            }
            if ("txtJaulasViernes" == campos[i].id) {
                mensaje = window.msgJaulasViernesVacio;
            }
            if ("txtJaulasSabado" == campos[i].id) {
                mensaje = window.msgJaulasSabadoVacio;
            }
            if ("txtJaulasDomingo" == campos[i].id) {
                mensaje = window.msgJaulasDomingoVacio;
            }
            if ("txtMotivoCambio" == campos[i].id) {
                mensaje = window.msgJaulasMotivoCambioVacio;
            }

            esNulo = 1;
            break;
        }


    }

    if (!$("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
        mensaje = window.msgOrganizacionVacio;
        esNulo = 1;
        campo = $("#txtNumOrganizacion");
    }

    if (!$("#txtPromedioCabezas").val().replace(/^0+/, '').trim()) {
        mensaje = window.msgPromedioCabezasVacio;
        esNulo = 1;
        campo = $("#txtPromedioCabezas");
    }
    if (esNulo) {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(mensaje, function() {
                msjAbierto = 0;
                setTimeout(function() {
                    campo.focus();
                }, 10);
            });
            return false;
        }
        return false;
    }
    return true;
};

ObtenerPedidoGanadoSemanal = function(fechaInicio, organizacionID) {
    var pedidoGanadoInfo = {};
    pedidoGanadoInfo.Organizacion = {};
    pedidoGanadoInfo.FechaInicio = fechaInicio;
    pedidoGanadoInfo.Organizacion.OrganizacionID = organizacionID;
    if ($("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
        var datos = { 'pedidoGanadoInfo': pedidoGanadoInfo };
        $.ajax({
            data: JSON.stringify(datos),
            type: "POST",
            url: "CapturaPedido.aspx/ObtenerPedidoGanadoSemanal",
            contentType: "application/json; charset=utf-8",
            error: function() {
                var mensaje = window.msgErrorConsultar;
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function() {
                        msjAbierto = 0;
                        LimpiarPantalla();
                    });
                }
            },
            dataType: "json",
            async: false,
            success: function(data) {
                LlenarInputsGrid(data);
                LlenarComentarios(data.d.ListaSolicitudes);
                pedidoGanadoID = data.d.PedidoGanadoID;
                if (anioActual == anioNuevo) {
                    if (numeroSemanaNuevo <= semanaInicial) {
                        DeshabilitarCeldas();
                    } else {
                        ObtenerDiasHabiles();
                        HabilitarCeldas();;
                        $("#txtPromedioCabezas").change();
                    }
                }
                if (anioActual < anioNuevo) {
                    HabilitarCeldas();
                }

                if (anioActual > anioNuevo) {
                    DeshabilitarCeldas();
                }
                

            }
        });
    } else {
        $("#txtNumOrganizacion").focus();
    }
    
};
ObtenerPedidoGanadoSemanalCambio = function (fechaInicio, organizacionID) {
    var pedidoGanadoInfo = {};
    pedidoGanadoInfo.Organizacion = {};
    pedidoGanadoInfo.FechaInicio = fechaInicio;
    pedidoGanadoInfo.Organizacion.OrganizacionID = organizacionID;
    if ($("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
        var datos = { 'pedidoGanadoInfo': pedidoGanadoInfo };
        $.ajax({
            data: JSON.stringify(datos),
            type: "POST",
            url: "CapturaPedido.aspx/ObtenerPedidoGanadoSemanal",
            contentType: "application/json; charset=utf-8",
            error: function () {
                var mensaje = window.msgErrorConsultar;
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ListaSolicitudes.length > 0) {
                    haySolicitudPendiente = true;
                }

            }
        });
    } else {
        $("#txtNumOrganizacion").focus();
    }

};
LlenarComentarios = function(comentarios) {
    $("#tBody").html("");
    esSolicitante = false;
    haySolicitudPendiente = false;
    $("#txtComentario").val("");
    $("#btnAutorizar").prop("disabled", true);
    $("#btnCancelarSolicitud").prop("disabled", true);
    if (comentarios.length > 0) {
        haySolicitudPendiente = true;
        if (comentarios[0].UsuarioSolicitanteID == usuario) {
            esSolicitante = true;
        } else {
            $("#btnAutorizar").prop("disabled", false);
            $("#btnCancelarSolicitud").prop("disabled", false);
        }

        if (comentarios[0].Lunes != pedidoSemana.Lunes || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.LunesTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Lunes + "</td></tr>");
        }
        if (comentarios[0].Martes != pedidoSemana.Martes || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.MartesTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Martes + "</td></tr>");
        }
        if (comentarios[0].Miercoles != pedidoSemana.Miercoles || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.MiercolesTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Miercoles + "</td></tr>");
        }
        if (comentarios[0].Jueves != pedidoSemana.Jueves || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.JuevesTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Jueves + "</td></tr>");
        }
        if (comentarios[0].Viernes != pedidoSemana.Viernes || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.ViernesTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Viernes + "</td></tr>");
        }
        if (comentarios[0].Sabado != pedidoSemana.Sabado || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.SabadoTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Sabado + "</td></tr>");
        }
        if (comentarios[0].Domingo != pedidoSemana.Domingo || comentarios[0].CabezasPromedio != pedidoSemana.CabezasPromedio) {
            $("#tBody").append("<tr>" +
                "<td class='alineacionCentro borderTabla' >" + semana.DomingoTexto + "</td>" +
                "<td class='alineacionCentro' style='padding-left:10px;'>" + comentarios[0].Domingo + "</td></tr>");
        }
        $("#txtComentario").val(comentarios[0].Justificacion);
    }
    LlenarComentariosVacios(comentarios.length);


};
LlenarComentariosVacios = function(numeroComentarios) {
    var filasVacias = 0;
    if (numeroComentarios < 5) {
        filasVacias = 5 - numeroComentarios;
    }

    for (var j = 0; j < filasVacias; j++) {
        $("#tBody").append("<tr style='height:32px;'>" +
            "<td class='alineacionCentro borderTabla'> </td>" +
            "<td  class='alineacionCentro '> </td>" +
            "</tr>");
    }

};

function CalcularCabezas(e, id) {
    var promedioCabezas = $("#txtPromedioCabezas").val();
    $('#' + id).val(promedioCabezas * $("#" + e.id).val());
    $("#txtJaulasTotal").val((-$("#txtJaulasLunes").val() - $("#txtJaulasMartes").val() - $("#txtJaulasMiercoles").val() - $("#txtJaulasJueves").val() - $("#txtJaulasViernes").val() - $("#txtJaulasSabado").val() - $("#txtJaulasDomingo").val()) * -1);
    $("#txtCabezasTotal").val((-$("#txtCabezasLunes").val() - $("#txtCabezasMartes").val() - $("#txtCabezasMiercoles").val() - $("#txtCabezasJueves").val() - $("#txtCabezasViernes").val() - $("#txtCabezasSabado").val() - $("#txtCabezasDomingo").val()) * -1);
}

function CalcularPromedioCabezas() {
    var promedioCabezas = $("#txtPromedioCabezas").val();
    $("#txtCabezasLunes").val(promedioCabezas * $("#txtJaulasLunes").val());
    $("#txtCabezasMartes").val(promedioCabezas * $("#txtJaulasMartes").val());
    $("#txtCabezasMiercoles").val(promedioCabezas * $("#txtJaulasMiercoles").val());
    $("#txtCabezasJueves").val(promedioCabezas * $("#txtJaulasJueves").val());
    $("#txtCabezasViernes").val(promedioCabezas * $("#txtJaulasViernes").val());
    $("#txtCabezasSabado").val(promedioCabezas * $("#txtJaulasSabado").val());
    $("#txtCabezasDomingo").val(promedioCabezas * $("#txtJaulasDomingo").val());
    $("#txtCabezasTotal").val(promedioCabezas * $("#txtJaulasTotal").val());
    $("#txtJaulasTotal").val((-$("#txtJaulasLunes").val() - $("#txtJaulasMartes").val() - $("#txtJaulasMiercoles").val() - $("#txtJaulasJueves").val() - $("#txtJaulasViernes").val() - $("#txtJaulasSabado").val() - $("#txtJaulasDomingo").val()) * -1);
    $("#txtCabezasTotal").val((-$("#txtCabezasLunes").val() - $("#txtCabezasMartes").val() - $("#txtCabezasMiercoles").val() - $("#txtCabezasJueves").val() - $("#txtCabezasViernes").val() - $("#txtCabezasSabado").val() - $("#txtCabezasDomingo").val()) * -1);
}

LlenarInputsGrid = function(data) {
    pedidoSemana = data.d;
    $("#txtPromedioCabezas").val(pedidoSemana.CabezasPromedio);
    var promedioCabezas = $("#txtPromedioCabezas").val();

    $("#txtJaulasLunes").val(pedidoSemana.Lunes);
    $("#txtJaulasMartes").val(pedidoSemana.Martes);
    $("#txtJaulasMiercoles").val(pedidoSemana.Miercoles);
    $("#txtJaulasJueves").val(pedidoSemana.Jueves);
    $("#txtJaulasViernes").val(pedidoSemana.Viernes);
    $("#txtJaulasSabado").val(pedidoSemana.Sabado);
    $("#txtJaulasDomingo").val(pedidoSemana.Domingo);
    $("#txtJaulasTotal").val(pedidoSemana.Lunes + pedidoSemana.Miercoles + pedidoSemana.Martes + pedidoSemana.Jueves + pedidoSemana.Viernes + pedidoSemana.Sabado + pedidoSemana.Domingo);

    $("#txtCabezasLunes").val(promedioCabezas * $("#txtJaulasLunes").val());
    $("#txtCabezasMartes").val(promedioCabezas * $("#txtJaulasMartes").val());
    $("#txtCabezasMiercoles").val(promedioCabezas * $("#txtJaulasMiercoles").val());
    $("#txtCabezasJueves").val(promedioCabezas * $("#txtJaulasJueves").val());
    $("#txtCabezasViernes").val(promedioCabezas * $("#txtJaulasViernes").val());
    $("#txtCabezasSabado").val(promedioCabezas * $("#txtJaulasSabado").val());
    $("#txtCabezasDomingo").val(promedioCabezas * $("#txtJaulasDomingo").val());
    $("#txtCabezasTotal").val(promedioCabezas * $("#txtJaulasTotal").val());
};
DeshabilitarCeldas = function() {
    $("#GridSemanal td").addClass("celdaDeshabilitada");
    $("#GridSemanal input").prop("disabled", true);
    $(".ico-btn").prop("onclick", null).addClass("img-gray");
};
HabilitarCeldas = function() {
    $(".habilitarInput").removeClass("celdaDeshabilitada");
    $(".habilitarInput input").prop("disabled", false);
};
LlenarGridSemana = function(fechaBase) {
    var meses = ["ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];
    var diaDeLaSeamana = fechaBase.getDay();

    var anio = fechaBase.getFullYear();
    anioNuevo = fechaBase.getFullYear();
    $("#anioEtiqueta").text(anio);
    var fechaPrimerDiaSemana = sumarDias(fechaBase, -(diaDeLaSeamana - 1));
    var mes = (fechaPrimerDiaSemana.getMonth() + 1);
    var mesString = "0" + mes;
    if (mes > 9) {
        mesString = "" + mes;
    }

    var fechaCalcular = fechaPrimerDiaSemana.getFullYear() + "-" + mesString + "-" + fechaPrimerDiaSemana.getDate();
    $("#lunesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    semana.LunesTexto = "Lunes " + $("#lunesEtiqueta").text();
    fechaPrimerDiaSemana = fechaBase.addDays(1);
    $("#martesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    semana.MartesTexto = "Martes " + $("#martesEtiqueta").text();
    fechaPrimerDiaSemana = fechaBase.addDays(2);
    $("#miercolesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    semana.MiercolesTexto = "Miércoles " + $("#miercolesEtiqueta").text();
    fechaPrimerDiaSemana = fechaBase.addDays(3);
    $("#juevesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(4);
    semana.JuevesTexto = "Jueves " + $("#juevesEtiqueta").text();
    $("#viernesEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    fechaPrimerDiaSemana = fechaBase.addDays(5);
    semana.ViernesTexto = "Viernes " + $("#viernesEtiqueta").text();
    $("#sabadoEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    semana.SabadoTexto = "Sábado " + $("#sabadoEtiqueta").text();
    fechaPrimerDiaSemana = fechaBase.addDays(6);
    $("#domingoEtiqueta").text(fechaPrimerDiaSemana.getDate() + "-" + meses[fechaPrimerDiaSemana.getMonth()]);
    semana.DomingoTexto = "Domingo " + $("#domingoEtiqueta").text();


    $('#txtFecha').val(fechaBase.getWeek());;
    numeroSemanaNuevo = $("#txtFecha").val();
    if (!primeraCarga) {
        semanaInicial = $("#txtFecha").val();
        primeraCarga = true;
    }
    ObtenerPedidoGanadoSemanal(fechaCalcular, $("#txtNumOrganizacion").val());
    if (anioActual == anioNuevo) {
        if (numeroSemanaNuevo <= semanaInicial) {
            $("#tBody").html("");
            LlenarComentariosVacios(0);
            $("#txtPromedioCabezas").prop("disabled", true);
        }
        if (numeroSemanaNuevo > semanaInicial + 1) {
            HabilitarCeldas();
        }
        if (numeroSemanaNuevo == semanaInicial) {
            DeshabilitarCeldas();
        }
        if (numeroSemanaNuevo > semanaInicial) {
            $("#txtPromedioCabezas").prop("disabled", false);
            $("#txtPromedioCabezas").change();
        }
    }
    if (anioActual < anioNuevo) {
        HabilitarCeldas();
        $("#txtPromedioCabezas").prop("disabled", false);
        $("#txtPromedioCabezas").change();
    }

    if (anioActual > anioNuevo) {
        $("#tBody").html("");
        LlenarComentariosVacios(0);
        $("#txtPromedioCabezas").prop("disabled", true);
        DeshabilitarCeldas();
    }
   
    
    if (esSolicitante || haySolicitudPendiente) {
        DeshabilitarCeldas();
        $("#txtPromedioCabezas").prop("disabled", true);
    }
    if (!esSolicitante) {
        $(".ico-btn").prop("onclick", null).removeClass("img-gray");
    }
};


/* Función que suma o resta días a una fecha, si el parámetro días es negativo restará los días*/
sumarDias = function(fecha, dias) {
    fecha.setDate(fecha.getDate() + dias);
    return fecha;
};

Date.prototype.getWeek = function() {


    var weeknum = 15;
    var mes = (this.getMonth() + 1);
    var mesString = "0" + mes;
    if (mes > 9) {
        mesString = "" + mes;
    }
    var fechaCalcular = this.getFullYear() + "-" + mesString + "-" + this.getDate();
    var datos = { "fechaCalcular": fechaCalcular };
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerNumeroSemana",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify(datos),
        error: function() {
            var mensaje = window.msgErrorConsultar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function(data) {
            weeknum = parseInt(data.d);

        }
    });

    return weeknum;
};

Inicializar = function() {
    $('body').bind("cut copy paste", function(e) {
        e.preventDefault();
    });

    $("#txtNumOrganizacion").numericInput().attr("maxlength", "4");
    $("#txtOrganizacion").attr("disabled", true);

    
};
AsignarEventosControles = function() {
    // Al capturar la organizacion en la ventana principal
    $('#txtNumOrganizacion').keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $("#txtNumOrganizacion").focusout();
        }
    });
    // Al perder el foco el textbox del indentificador de la organizacion
    $("#txtNumOrganizacion").focusout(function() {
        if (!$("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
            $("#txtOrganizacion").val("");
            $("#txtNumOrganizacion").val("");
        } else {
            ObtenerOrganizacion();
            var fecha = new Date();
            if ($("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
                LlenarGridSemana(fecha.addDays(semanasContador));
            }
        }
    });
    // Boton que abre la ayuda de organizaciones
    $("#btnAyudaOrganizacion").click(function() {
        ObtenerOrganizacionesTipoGanadera();
    });
    // Boton buscar de la ventana ayuda
    $("#btnAyudaBuscarOrganizacion").click(function() {
        ObtenerOrganizacionesTipoGanadera();
    });
    // Boton agregar de la ventana ayuda
    $("#btnAyudaAgregarBuscar").click(function() {
        var renglones = $("input[class=organizaciones]:checked");

        if (renglones.length > 0) {
            renglones.each(function() {
                $("#txtNumOrganizacion").val($(this).attr("organizacion"));
                $("#txtOrganizacion").val($(this).attr("descripcion"));
            });
            $("#dlgBusquedaOrganizacion").modal("hide");
            $("#txtOrganizacionBuscar").val("");
            var fecha = new Date();
            LlenarGridSemana(fecha.addDays(semanasContador));
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaOrganizacion").modal("hide");
                bootbox.alert(window.msgSeleccionarOrganizacion, function() {
                    $("#dlgBusquedaOrganizacion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });

    // Boton cancelar de la ventana ayuda
    $("#lblAceptarMotivo").click(function() {
        GuardarSemanaEspejo();
    });

    // Boton cancelar de la ventana ayuda
    $("#lblCancelarMotivo").click(function() {
        $("#dlgCancelarMotivo").modal("show");
    });
    // Boton cancelar de la ventana ayuda
    $("#btnSi").click(function() {
        $("#dlgCancelarMotivo").modal("hide");
        $("#dlgMotivoCmabio").modal("hide");
        $("#txtMotivoCambio").val("");
    });
    // Boton cancelar de la ventana ayuda
    $("#btnNo").click(function() {
        $("#dlgCancelarMotivo").modal("hide");
    });

    // Boton cancelar de la ventana ayuda
    $("#btnAyudaCancelarBuscar").click(function() {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaOrganizacion").modal("hide");
    });

    $("#btnSiBuscar").click(function() {
        $("#dlgBusquedaOrganizacion").modal("hide");
        $("#txtOrganizacionBuscar").val("");
    });

    $("#btnNoBuscar").click(function() {
        $("#dlgBusquedaOrganizacion").modal("show");
        $("#txtOrganizacionBuscar").focus();
    });

    // Boton guardar
    $("#btnGuardar").click(function() {
        Guardar();
    });

    // Boton cancelar en la pantalla principal
    $("#btnCancelar").click(function() {
        bootbox.dialog({
            message: window.msgCancelar,
            buttons: {
                Aceptar: {
                    label: window.msgDialogoSi,
                    callback: function() {
                        LimpiarPantalla();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.msgDialogoNo,
                    callback: function() {
                        return true;
                    }
                }
            }
        });
    });
    // Enter dentro de la ayuda de organizacion
    $("#txtOrganizacionBuscar").keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#GridContenido').keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#dlgCancelarBuscar').keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });

    $('#tbBusqueda').keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //enter
            e.preventDefault();
        }
    });
};

//Validaciones y consultas
// Se hacen las validaciones para cumplir con las precondiciones necesarias
PreCondiciones = function() {
    var continuar = false;
    var mensaje = "";
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ValidarPreCondiciones",
        contentType: "application/json; charset=utf-8",
        error: function () {
            DeshabilitarPantalla();
            var mensajeError = window.msgErrorConsultar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensajeError, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            var resultado = data.d;
            if (resultado < 1) {
                switch (resultado) {
                    case -1:
                    mensaje = window.msgSinTipoGanadera;
                    break;
                }

                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    DeshabilitarPantalla();
                    bootbox.alert(mensaje, function() {
                        msjAbierto = 0;
                    });
                }
            } else {
                continuar = true;
            }
        }
    });
    return continuar;
};
//Obtiene la organizacion capturada
ObtenerOrganizacion = function() {

    if ($("#txtNumOrganizacion").val().replace(/^0+/, '').trim()) {
        var datos = { "organizacion": $("#txtNumOrganizacion").val() };
        $.ajax({
            type: "POST",
            url: "CapturaPedido.aspx/ObtenerOrganizacion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            async: false,
            error: function() {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinOrganizacionValida, function() {
                        msjAbierto = 0;
                    });
                    $("#txtNumOrganizacion").val("");
                }
            },
            dataType: "json",
            success: function(data) {
                if (data.d.length > 0) {
                    $("#txtOrganizacion").val(data.d[0].Descripcion);
                } else {
                    $("#txtNumOrganizacion").val("");
                    $("#txtOrganizacion").val("");
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgSinOrganizacionValida, function () {
                            msjAbierto = 0;
                            setTimeout(function () { $("#txtNumOrganizacion").focus(); }, 500);
                        });
                    }
                }
            }
        });
    }
};
//Obtiene las organizaciones de tipo ganadera para la ayuda
ObtenerOrganizacionesTipoGanadera = function() {
    var datos;
    if ($("#txtOrganizacionBuscar").val() != "") {
        datos = { "organizacion": $("#txtOrganizacionBuscar").val() };
    } else {
        datos = { "organizacion": "" };
    }
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerOrganizacionesTipoGanadera",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async:false,
        error: function () {
            DeshabilitarPantalla();
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgSinTipoGanadera, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                var resultado = data.d;
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='alineacionCentro' style='width: 20px;'><input type='checkbox' class='organizaciones' id='organizacion" + resultado[i].OrganizacionID + "' organizacion='" + resultado[i].OrganizacionID + "' descripcion='" + resultado[i].Descripcion + "' onclick='SeleccionaUno(\"#organizacion" + resultado[i].OrganizacionID + "\");'/></td>" +
                        "<td class='alineacionCentro' style='width: 100px;'>" + resultado[i].OrganizacionID + "</td>" +
                        "<td class='alineacionCentro' style='width: auto;'>" + resultado[i].Descripcion + "</td>" +
                        "</tr>");
                }
                setTimeout(function() {
                    $("#txtOrganizacionBuscar").val("");
                    $("#txtOrganizacionBuscar").focus();
                }, 500);
            } else {
                DeshabilitarPantalla();
                bootbox.alert(window.msgSinTipoGanadera, function () {
                    setTimeout(function() {
                        $("#txtOrganizacion").val("");
                    }, 500);
                });
            }
        }
    });
};

SeleccionaUno = function(id) {
    $(".organizaciones").prop("checked", false);
    $(id).prop("checked", true);
}
// Boton que abre la ayuda de organizaciones
ActualizarComentarios = (function(estatus) {
    var datos;
    var pedidoGanadoEspejoInfo = {};
    var lista = pedidoSemana.ListaSolicitudes;
    pedidoGanadoEspejoInfo.Lunes = lista[0].Lunes;
    pedidoGanadoEspejoInfo.Martes = lista[0].Martes;
    pedidoGanadoEspejoInfo.Miercoles = lista[0].Miercoles;
    pedidoGanadoEspejoInfo.Jueves = lista[0].Jueves;
    pedidoGanadoEspejoInfo.Viernes = lista[0].Viernes;
    pedidoGanadoEspejoInfo.Sabado = lista[0].Sabado;
    pedidoGanadoEspejoInfo.Domingo = lista[0].Domingo;
    pedidoGanadoEspejoInfo.CabezasPromedio = lista[0].CabezasPromedio;
    pedidoGanadoEspejoInfo.PedidoGanado = {};
    pedidoGanadoEspejoInfo.PedidoGanado.PedidoGanadoID = pedidoGanadoID;

    pedidoGanadoEspejoInfo.Estatus = estatus;
    pedidoGanadoEspejoInfo.PedidoGanadoEspejoID = lista[0].PedidoGanadoEspejoID;
    pedidoGanadoEspejoInfo.UsuarioModificacionID = 0;
    datos = { "pedidoGanadoEspejoInfo": pedidoGanadoEspejoInfo };
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ActualizarPedidoGanadoEspejoEstatus",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function () {
            DesbloquearPantalla();
            var mensaje = window.msgErrorGuardar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                var fecha = new Date();
                LlenarGridSemana(fecha.addDays(semanasContador));
                if (estatus) {
                    EnviarCorreo(window.clvAutorizacion, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), "", lista[0].UsuarioSolicitanteID);
                } else {
                    EnviarCorreo(window.clvRechazar, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), "", lista[0].UsuarioSolicitanteID);
                }
            }
        }
    });
});

/*Actualizar pedido ganado*/
ActualizarPedidoGanado = (function (pedidoGanadoInfo) {
    var datos;
    var pedidoGanadoEspejoInfo = {};
    var lista = pedidoSemana.ListaSolicitudes;
    pedidoGanadoEspejoInfo.Lunes = pedidoGanadoInfo.Lunes;
    pedidoGanadoEspejoInfo.Martes = pedidoGanadoInfo.Martes;
    pedidoGanadoEspejoInfo.Miercoles = pedidoGanadoInfo.Miercoles;
    pedidoGanadoEspejoInfo.Jueves = pedidoGanadoInfo.Jueves;
    pedidoGanadoEspejoInfo.Viernes = pedidoGanadoInfo.Viernes;
    pedidoGanadoEspejoInfo.Sabado = pedidoGanadoInfo.Sabado;
    pedidoGanadoEspejoInfo.Domingo = pedidoGanadoInfo.Domingo;
    pedidoGanadoEspejoInfo.CabezasPromedio = pedidoGanadoInfo.CabezasPromedio;
    pedidoGanadoEspejoInfo.PedidoGanado = {};
    pedidoGanadoEspejoInfo.PedidoGanado.PedidoGanadoID = pedidoGanadoID;
    pedidoGanadoEspejoInfo.Estatus = true;
    pedidoGanadoEspejoInfo.PedidoGanadoEspejoID = 0;
    pedidoGanadoEspejoInfo.UsuarioModificacionID = 0;
    datos = { "pedidoGanadoEspejoInfo": pedidoGanadoEspejoInfo };
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ActualizarPedidoGanado",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function () {
            DesbloquearPantalla();
            var mensaje = window.msgErrorGuardar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                if (!esJefeManejo) {
                    EnviarCorreo(window.clvCambio, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), window.clvCorreoJefeManejo, 0);
                } else {
                    EnviarCorreo(window.clvCambio, $("#txtFecha").val(), $("#txtNumOrganizacion").val(), window.clvCorreoLogistica, 0);
                }
                var mensaje = window.msgGuardarExito;
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(mensaje, function () {
                        msjAbierto = 0;
                    });
                }
                var fecha = new Date();
                LlenarGridSemana(fecha.addDays(semanasContador));
            }
        }
    });
});

LimpiarPantalla = function() {
    $("#txtPromedioCabezas").val(0);
    var promedioCabezas = $("#txtPromedioCabezas").val();

    $("#txtJaulasLunes").val(0);
    $("#txtJaulasMartes").val(0);
    $("#txtJaulasMiercoles").val(0);
    $("#txtJaulasJueves").val(0);
    $("#txtJaulasViernes").val(0);
    $("#txtJaulasSabado").val(0);
    $("#txtJaulasDomingo").val(0);
    $("#txtJaulasTotal").val(0);

    $("#txtCabezasLunes").val(promedioCabezas * $("#txtJaulasLunes").val());
    $("#txtCabezasMartes").val(promedioCabezas * $("#txtJaulasMartes").val());
    $("#txtCabezasMiercoles").val(promedioCabezas * $("#txtJaulasMiercoles").val());
    $("#txtCabezasJueves").val(promedioCabezas * $("#txtJaulasJueves").val());
    $("#txtCabezasViernes").val(promedioCabezas * $("#txtJaulasViernes").val());
    $("#txtCabezasSabado").val(promedioCabezas * $("#txtJaulasSabado").val());
    $("#txtCabezasDomingo").val(promedioCabezas * $("#txtJaulasDomingo").val());
    $("#txtCabezasTotal").val(promedioCabezas * $("#txtJaulasTotal").val());
    $("#tBody").html("");
    esSolicitante = false;
    haySolicitudPendiente = false;
    $("#txtComentario").val("");
    $("#btnAutorizar").prop("disabled", true);
    $("#btnCancelarSolicitud").prop("disabled", true);

}; //Envia un correo a los usuarios cuyo rol esté configurado
EnviarCorreo = (function(tipoSolicitud, semanaCambio, organizacionID, clave, usuarioID) {
    var datos = { "tipoSolicitud": tipoSolicitud, "semanaCambio": semanaCambio, "organizacionID": organizacionID, "clave": clave, "usuarioID": usuarioID };
    
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/EnviarCorreo",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        async: false,
        error: function() {
            //Error al enviar el correo electronico
            DesbloquearPantalla();
        },
        dataType: "json",
        success: function() {
            //se envio correctamente el correo
            DesbloquearPantalla();
        }
    });
});

//Obtener dias habiles
ObtenerDiasHabiles = function() {
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerDiasHabiles",
        contentType: "application/json; charset=utf-8",
        error: function() {
            var mensaje = window.msgErrorConsultar;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(mensaje, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            diasHabiles = data.d;
            HabilitarDiasHabiles(diasHabiles);
        }
    });
};

HabilitarDiasHabiles = function (dias) {
    if (diaActual == 0 && !dias.Domingo) {
        esDiaHabil = false;
    }
    if (diaActual == 1 && !dias.Lunes) {
        esDiaHabil = false;
    }
    if (diaActual == 2 && !dias.Martes) {
        esDiaHabil = false;
    }
    if (diaActual == 3 && !dias.Miercoles) {
        esDiaHabil = false;
    }
    if (diaActual == 4 && !dias.Jueves) {
        esDiaHabil = false;
    }
    if (diaActual == 5 && !dias.Viernes) {
        esDiaHabil = false;
    }
    if (diaActual == 6 && !dias.Sabado) {
        esDiaHabil = false;
    }
};

/*Obtener Organizacion usuario*/
OrganizacionUsuario = function() {
    var continuar = false;
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerOrganizacionUsuario",
        contentType: "application/json; charset=utf-8",
        error: function() {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorPrecondiciones, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            var resultado = data.d;
            $("#txtNumOrganizacion").val(resultado);
            ObtenerOrganizacion();
            continuar = true;
        }
    });
    return continuar;
};

/*Obtener usuario*/
ObtenerUsuario = function() {
    var continuar = false;
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerUsuarioID",
        contentType: "application/json; charset=utf-8",
        error: function() {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorPrecondiciones, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            var resultado = data.d;
            usuario = resultado;
            ObtenerOrganizacion();
            continuar = true;
        }
    });
    return continuar;
};

/*Obtener rol*/
ObtenerRol = function() {
    var continuar = false;
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ObtenerRolManejo",
        contentType: "application/json; charset=utf-8",
        error: function() {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgErrorPrecondiciones, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            var resultado = data.d;
            if (resultado == 1) {
                esJefeManejo = true;
            } else {
                esJefeManejo = false;
            }
            continuar = true;
        }
    });
    return continuar;
};

ValidarOrganizacionesActivas = function() {
    $.ajax({
        type: "POST",
        url: "CapturaPedido.aspx/ValidarOrganizaciones",
        contentType: "application/json; charset=utf-8",
        error: function () {
            hayOrganizacionesActivas = false;
            DeshabilitarPantalla();
            msjAbierto = 0;
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgSinOrganizaciones, function() {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        async: false,
        success: function(data) {
            var resultado = data.d;
            if (!resultado) {
                DeshabilitarPantalla();
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinOrganizaciones, function() {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

DeshabilitarPantalla = function() {
    LimpiarPantalla();
    DeshabilitarCeldas();
    $("#txtNumOrganizacion").prop("disabled", true);
    $("#btnAyudaOrganizacion").unbind("click");
    $("#btnAyudaOrganizacion").removeProp("href");
    $("#btnAyudaOrganizacion").removeProp("id");
    $(".ui-datepicker-trigger").prop("disabled", true);
    $("#txtFecha").datepicker({
        showOn: "off"
    });
    $(".ui-datepicker-trigger").css("background-color", "lightgrey");
    $("#txtPromedioCabezas").prop("disabled", true);
    $("#lblActualizar").prop("disabled", true);
    $("#lblGuardarSemana").prop("disabled", true);
    $("#btnLimpiar").prop("disabled", true);
    $("#btnSemanaAnterior").removeProp("id");
    $("#btnSemanaSiguiente").removeProp("id");
};