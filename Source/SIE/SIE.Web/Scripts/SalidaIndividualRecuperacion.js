$(document).ready(function () {
    $("#txtArete").focus();

    $("#txtArete").numericInput();
    //$("#txtArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    //$("#txtCorraleta").numericInput();
    //$("#txtCorraleta").inputmask({ "mask": "*", "repeat": 10, "greedy": false });

    $("#rdSalidaSacrificio").click(function () {
        $("#hIr").val("SalidaIndividualSacrificio.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#rdSalidaVenta").click(function () {
        $("#hIr").val("SalidaIndividualVenta.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#btnCancelar").click(function () {
        $("#hIr").val("SalidaIndividualRecuperacion.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#txtArete").change(function () {
        if (validarArete > 0) {
            areteValido = 0;
            $("#ddlCorralDestino").val("");
            $("#txtCorral").val("");
        } else {
            //validarArete = 1;
        }
    });

    $("#ddlCorralDestino").change(function () {
        corraletaValida = 0;
    });

    $("#txtArete").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarArete();
        } else if (code == 9) {
            $("#ddlCorralDestino").focus();
        }
    });

    $("#txtCorral").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtFecha").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#ddlCorralDestino").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            e.preventDefault();
            //ValidarCorraleta();
        }
    });

    $("#ddlCorralDestino").focusin(function () {
        ValidarArete();
    });

    $("#btnDialogoSi").click(function () {
        location.href = $("#hIr").val();
    });

    $("#btnDialogoNo").click(function () {
        $("#rdSalidaRecuperacion").attr("checked", true);
    });

    $("#btnGuardar").click(function (e) {
        if (!$("#btnGuardar").is(':disabled')) {
            if (ValidarArete() == true) {
                GuardarSalida(e);

            }
        }
    });
});
var validarArete = 0;
var areteValido = 0;
var corraletaValida = 0;
ValidarArete = function () {
    if (areteValido != $("#txtArete").val()) {
        $("#txtCorral").val("");
        //$("#txtCorraleta").val("");
        if ($("#txtArete").val() != "") {
            var datos = { "arete": $("#txtArete").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualRecuperacion.aspx/ObtenerExisteArete",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    bootbox.alert(request.Message);
                    areteValido = 0;
                    return false;
                },
                dataType: "json",
                success: function (data) {
                    if (data.d == null) {
                        bootbox.alert(window.msgNoExisteArete, function () {
                            $("#txtArete").focus();
                        });
                        areteValido = 0;
                        return false;
                    } else {
                        if (data.d.AnimalID == -1) {
                            bootbox.alert(window.msgAreteDetectadoMuerto, function () {
                                $("#txtArete").focus();
                            });
                            areteValido = 0;
                            return false;
                        }
                        if (ValidarAreteEnfermeria() != false) {
                            areteValido = $("#txtArete").val();
                            $("#ddlCorralDestino").focus();
                            return true;
                        } else {
                            areteValido = 0;
                            return false;
                        }
                    }
                }
            });
        } else {
            bootbox.alert(window.msgAreteVacio, function () {
                $("#txtArete").focus();
            });
            return false;
        }
    } else {
        return true;
    }
    return true;
};

ValidarAreteEnfermeria = function () {
    var datos = { "arete": $("#txtArete").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualRecuperacion.aspx/ObtenerUltimoMovimiento",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            bootbox.alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                areteValido = 0;
                bootbox.alert(window.msgNoExisteArete, function () {
                    $("#txtArete").focus();
                });
                return false;
            } else {
                if (data.d == 1) {
                    areteValido = $("#txtArete").val();
                    return ObtenerCorral();
                } else if (data.d == 2) {
                    bootbox.alert(window.msgCronicoVentaMuerte, function () {
                        $("#txtArete").focus();
                    });
                    areteValido = 0;
                    return false;
                } else if (data.d == 3) {
                    bootbox.alert(window.msgAreteSalida, function () {
                        $("#txtArete").focus();
                    });
                    areteValido = 0;
                    return false;
                } else if (data.d == 0) {
                    bootbox.alert(window.msgNoEnfermeria, function () {
                        $("#txtArete").focus();
                    });
                    areteValido = 0;
                    return false;
                } else if (data.d == -1) {
                    bootbox.alert(window.msgNoInventario, function () {
                        $("#txtArete").focus();
                    });
                    areteValido = 0;
                    return false;
                }

            }
            return true;
        }
    });
};

ObtenerCorral = function () {
    var datos = { "arete": $("#txtArete").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualRecuperacion.aspx/ObtenerCorral",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            bootbox.alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                bootbox.alert(window.msgNoEncontroCorral, function () {
                    $("#txtArete").focus();
                });
                areteValido = 0;
                return false;
            } else {
                $("#txtCorral").val(data.d.Codigo);
                ObtenerCorralesDestino();
                return true;
            }
        }
    });
};

ObtenerCorralesDestino = function () {
    var datos = { "arete": $("#txtArete").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualRecuperacion.aspx/ObtenerCorralDestinoAnimal",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            bootbox.alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d == null || data.d.length == 0) {
                bootbox.alert(window.msgNoEncontroCorral, function () {
                    $("#txtArete").focus();
                });
                areteValido = 0;
                return false;
            } else {
                var opcionSeleccion = '<option value="0">Seleccione</option>';
                var combo = $('#ddlCorralDestino');
                combo.append(opcionSeleccion);
                $(data.d).each(function () {
                    var corral = this;
                    var opcion = '<option value="' + corral.Codigo + '">' + corral.Codigo + '</option>';
                    combo.append(opcion);
                });
                return true;
            }
        }
    });
};
/* 
ValidarCorraleta = function () {
    if (corraletaValida != $("#ddlCorralDestino").val()) {
        if ($("#ddlCorralDestino").val() != "") {
            var datos = { "arete": $("#txtArete").val(), "corraleta": $("#ddlCorralDestino").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualRecuperacion.aspx/ValidarCorraleta",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    bootbox.alert(request.Message);
                    return false;
                },
                dataType: "json",
                success: function (data) {
                    if (data.d == 0) {
                        bootbox.alert(window.msgCorralNoExiste, function () {
                            $("#ddlCorralDestino").focus();
                        });
                        return false;
                    } else if (data.d == 2) {
                        bootbox.alert(window.msgAreteMayorDias, function () {
                            $("#ddlCorralDestino").focus();
                        });
                        return false;
                    } else if (data.d == 3) {
                        bootbox.alert(window.msgNoCorraletaManejo, function () {
                            $("#ddlCorralDestino").focus();
                        });
                        return false;
                    } else if (data.d == 1) {
                        corraletaValida = $("#ddlCorralDestino").val();
                        return true;
                    }
                    return true;
                }
            });
        } else {
          
        }
    } else {
        return true;
    }
    return true;
};
*/

GuardarSalida = function (e) {
    e.preventDefault();
    BloquearPantalla();
    if ($("#ddlCorralDestino").val() == "" || $("#ddlCorralDestino").val() == "0") {
        bootbox.alert(window.msgCorraletaVacio, function () {
            $("#ddlCorralDestino").focus();
        });
        DesbloquearPantalla();
        return false;
    }
    if ($("#txtArete").val() != "" && $("#txtCorral").val() != "" && $("#ddlCorralDestino").val()) {
        var datos = { "arete": $("#txtArete").val(), "codigoCorral": $("#txtCorral").val(), "codigoCorraleta": $("#ddlCorralDestino").val(), "tipoMovimiento": parseInt($("#cmbSalida").val()) };
        $("#btnGuardar").attr('disabled', true);
        $.ajax({
            type: "POST",
            url: "SalidaIndividualRecuperacion.aspx/Guardar",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
                $("#btnGuardar").attr('disabled', false);
                DesbloquearPantalla();
            },
            success: function (data) {
                if (data.d == 0) {
                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.msgDatosGuardados, function () {
                        $("#btnGuardar").attr('disabled', false);
                        DesbloquearPantalla();
                        location.href = location.href;
                    });
                } else {
                    bootbox.alert(window.msgOcurrioErrorGrabar);
                    $("#btnGuardar").attr('disabled', false);
                    DesbloquearPantalla();
                }
            }
        });
    } else {
        bootbox.alert(window.msgDatosBlanco);
        DesbloquearPantalla();
    }
};

//Validar que el usuario tenga permisos suficientes
function EnviarMensajeUsuario() {
    bootbox.alert(window.msgNoTienePermiso, function () {
        location.href = "../Principal.aspx";
        return false;
    });
}