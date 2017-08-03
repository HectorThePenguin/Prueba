var validarArete = 0;
var areteValido = 0;
var corraletaValida = 0;
var msjAbierto = 0;
var mensaje = "";
$(document).ready(function () {
    //ObtenerCorraletaSacrificio();
    
    $("#txtArete").numericInput();
    //$("#txtArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtCorraleta").inputmask({ "mask": "*", "repeat": 10, "greedy": false });

    $("#txtArete").focus();

    $("#rdSalidaRecuperacion").click(function () {
        if (mensaje != "") {
            mensaje.modal('hide');
        }
        $("#hIr").val("SalidaIndividualRecuperacion.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#rdSalidaVenta").click(function () {
        $("#hIr").val("SalidaIndividualVenta.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#btnCancelar").click(function () {
        $("#hIr").val("SalidaIndividualSacrificio.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#txtArete").change(function () {
        if (validarArete > 0) {
            areteValido = 0;
            $("#txtCorral").val("");
            $("#txtPesoProyectado").val("");
            $("#txtPeso").val(0);
        } else {
            validarArete = 1;
        }
    });
    /*
    $("#txtCorraleta").change(function () {
        corraletaValida = 0;
    });
    */
    $("#cmbCorraletas").change(function() {
        ValidarCapacidadCorraleta();
    });
    $("#txtArete").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarArete(1);
            $("#btnGuardar").focus();
        } else if (code == 9) {
            e.preventDefault();
            ValidarArete(1);
            $("#btnGuardar").focus();
        }
    });

    $("#txtCorral").keydown(function(e) {
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

    $("#txtPesoProyectado").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });
    /*
    $("#txtCorraleta").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarCorraleta();
        }
    });
    
    $("#txtCorraleta").focusin(function () {
        ValidarArete(1);
    });
    */
    $("#btnDialogoSi").click(function () {
        location.href = $("#hIr").val();
    });

    $("#btnDialogoNo").click(function() {
        $("#rdSalidaSacrificio").attr("checked", true);
    });

    $("#btnGuardar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            $("#btnGuardar").trigger('click');
        }
    })
    $("#btnGuardar").click(function (e) {
        if (ValidarArete(2)) {
            e.preventDefault();
        }
        
    });
});

ValidarArete = function (tipoValidar) {
    if (areteValido == 0 || $("#txtArete").val() != areteValido) {
        if ($("#txtArete").val() != "") {
            var datos = { "arete": $("#txtArete").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualSacrificio.aspx/ObtenerExisteArete",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(request.Message, function () {
                            msjAbierto = 0;
                        });
                    }
                    areteValido = 0;
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == null) {
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            mensaje = bootbox.alert(msgNoExisteArete, function () {
                                $("#txtArete").focus();
                                msjAbierto = 0;
                            });
                        }
                        areteValido = 0;
                        return false;
                    } else {
                        $("#txtPesoProyectado").val(data.d.PesoAlCorte + " Kgs");
                        $("#txtPeso").val(data.d.PesoAlCorte);
                        if (ValidarAreteEnfermeria(tipoValidar) != false) {
                            areteValido = $("#txtArete").val();
                            return true;
                        } else {
                            areteValido = 0;
                            return false;
                        }
                    }
                }
            });
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgAreteVacio, function () {
                    $("#txtArete").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        }
    } else {
        if (tipoValidar == 2) {
            GuardarSalida();
        }
        return true;
    }
}

ExisteCorraletaDeSacrificioConfigurada = function() {
    if ($("#txtArete").val() != "") {
        var datos = { "arete": $("#txtArete").val() };
        $.ajax({
            type: "POST",
            url: "SalidaIndividualSacrificio.aspx/ExisteCorraletaDeSacrificioConfigurada",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function(request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function() {
                        msjAbierto = 0;
                    });
                }
                areteValido = 0;
                return false;
            },
            dataType: "json",
            success: function(data) {
                if (data.d == null) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        mensaje = bootbox.alert('No existe corraleta de sacrificio Configurada. Favor de validar.'/*msgNoExisteArete*/, function() {
                            $("#txtArete").focus();
                            msjAbierto = 0;
                        });
                    }
                    areteValido = 0;
                    return false;
                } else {
                    var datos = data.d.Lista;
                    $("#cmbCorraletas").html("");
                    for (var i = 0; i < datos.length; i++) {
                        $("#cmbCorraletas").append("<option value='" + datos[i].CorralID + "'>" + datos[i].Codigo + "</option>");
                    }
                }
            }
        });
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(msgAreteVacio, function() {
                $("#txtArete").focus();
                msjAbierto = 0;
            });
        }
        return false;
    }
    
    return false;
};

ValidarAreteEnfermeria = function (tipoValidar) {
    var datos = { "arete": $("#txtArete").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualSacrificio.aspx/ObtenerUltimoMovimiento",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
            areteValido = 0;
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                areteValido = 0;
                $("#txtPesoProyectado").val("");
                $("#txtPeso").val(0);
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(msgNoExisteArete, function () {
                        $("#txtArete").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            } else {
                if (data.d == 1) {
                    ObtenerCorral();
                    if ($("#txtPeso").val() < 300) {
                        areteValido = 0;
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgPesoNoValido, function () {
                                $("#txtArete").focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    } else {
                        areteValido = $("#txtArete").val();
                        if (tipoValidar == 2) {
                            GuardarSalida();
                        }else {
                            if (ExisteCorraletaDeSacrificioConfigurada() == false) {
                                areteValido = 0;
                                $("#txtPeso").val(0);
                                return false;
                            }
                        }
                        return true;
                    }
                } else if (data.d == 2) {
                    areteValido = 0;
                    $("#txtPesoProyectado").val("");
                    $("#txtPeso").val(0);
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgCronicoVentaMuerte, function () {
                            $("#txtArete").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                } else if (data.d == 3) {
                    $("#txtPesoProyectado").val("");
                    $("#txtPeso").val(0);
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgAreteSalida, function () {
                            $("#txtArete").focus();
                            msjAbierto = 0;
                        });
                    }
                    areteValido = 0;
                    return false;
                } else if (data.d == 4) {
                    $("#txtPesoProyectado").val("");
                    $("#txtPeso").val(0);
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msg30Dias, function () {
                            $("#txtArete").focus();
                            msjAbierto = 0;
                        });
                    }
                    areteValido = 0;
                    return false; 
                } else if (data.d == 0) {
                    areteValido = 0;
                    $("#txtPesoProyectado").val("");
                    $("#txtPeso").val(0);
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgNoEnfermeria, function () {
                            $("#txtArete").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                } else {
                    return false;
                }
            }
        }
    });
}

ObtenerCorral = function () {
    var datos = { "arete": $("#txtArete").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualSacrificio.aspx/ObtenerCorral",
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
                $("#txtPesoProyectado").val("");
                $("#txtPeso").val(0);
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(msgNoEncontroCorral, function () {
                        $("#txtArete").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            } else {
                $("#txtCorral").val(data.d.Codigo);
                return true;
            }
        }
    });
}

ValidarCapacidadCorraleta = function () {
    if (corraletaValida == 0) {
        if ($("#cmbCorraletas").val() != "" && $("#cmbCorraletas").val()>0) {
            var datos = { "corraletaID": $("#cmbCorraletas").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualSacrificio.aspx/ValidarCapacidadCorraleta",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    bootbox.alert(request.Message);
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == 0) {
                        bootbox.alert(msgCorralNoExiste, function () {
                            $("#cmbCorraletas").val(0);
                            $("#cmbCorraletas").focus();
                        });
                        return false;
                    } else if (data.d == 2) {
                        bootbox.alert('Corral no tiene capacidad. Favor de validar.', function () {
                            $("#cmbCorraletas").val(0);
                            $("#cmbCorraletas").focus();
                        });
                        return false;
                    } else if (data.d == 3) {
                        bootbox.alert('Corral no tiene lote activo. Favor de validar.', function () {
                            $("#cmbCorraletas").val(0);
                            $("#cmbCorraletas").focus();
                        });
                        return false;
                    } else if (data.d == 1) {
                        corraletaValida = 1;
                        return true;
                    }
                }
            });
        } 
        else {
            corraletaValida = 0;
            /*bootbox.alert(msgCorraletaVacio, function() {
                $("#cmbCorraletas").focus();
            });*/
            return false;
        }
    } else {
        return true;
    }
}

ValidarCorraleta = function () {
    if (corraletaValida == 0) {
        if ($("#txtCorraleta").val() != "") {
            var datos = { "corraleta": $("#txtCorraleta").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualSacrificio.aspx/ValidarCorraleta",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    bootbox.alert(request.Message);
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == 0) {
                        bootbox.alert(msgCorralNoExiste, function() {
                            $("#txtCorraleta").focus();
                        });
                        return false;
                    } else if (data.d == 2) {
                        bootbox.alert(msgNoCorraletaSacrificio, function() {
                            $("#txtCorraleta").focus();
                        });
                        return false;
                    } else if (data.d == 1) {
                        corraletaValida = 1;
                        return true;
                    }
                }
            });
        } else {
            bootbox.alert(msgCorraletaVacio, function() {
                $("#txtCorraleta").focus();
            });
            return false;
        }
    } else {
        return true;
    }
}

ObtenerCorraletaSacrificio = function() {
    $.ajax({
        type: "POST",
        url: "SalidaIndividualSacrificio.aspx/ObtenerCorraleta",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            if (data.d != null) {
                var lista = data.d.Lista;
                $("#txtCorraleta").val(lista[0].Codigo);
            }
        }
    });
    $("#txtCorraleta").addClass("span12");
}

GuardarSalida = function () {
    if ($("#txtArete").val() != "" && $("#txtCorral").val() != "" && $("#cmbCorraletas").val() > 0) {
        var datos = { "arete": $("#txtArete").val(), "codigoCorral": $("#txtCorral").val(), "corraletaID": $("#cmbCorraletas").val(), "tipoMovimiento": parseInt($("#cmbSalida").val()) };

        $.ajax({
            type: "POST",
            url: "SalidaIndividualSacrificio.aspx/Guardar",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d == 0) {
                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardados, function () {
                        location.href = location.href;
                    });
                } else {
                    bootbox.alert(msgOcurrioErrorGrabar);
                }
            }
        });
    } else if ($("#cmbCorraletas").val() != "") {
        bootbox.alert(msgDatosBlanco);
    }
}

//Validar que el usuario tenga permisos suficientes
function EnviarMensajeUsuario() {
    bootbox.alert(msgNoTienePermiso, function () {
        location.href = "../Principal.aspx";
        return false;
    });
}