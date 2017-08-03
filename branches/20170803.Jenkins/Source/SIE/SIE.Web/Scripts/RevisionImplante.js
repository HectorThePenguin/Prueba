/*
 * Documento de control javascript para Programacion de Sacrificio
 */

var totalRecibidos = 0;     //Contar los aretes recibidos para sacrificio
var Aretes;                    //Lista de aretes para sacrifico
var usuarioValido = true;       //Identifica si es un usuario valido
var OrdenesSacrificio;
var rutaPantalla = location.pathname;

$(document).ready(function () {
    var presionoEnter;

    Aretes = [];

    //Inicializadores
    $("#btnGuardar").html(btnGuardarText);
    $("#btnCancelar").html(btnCancelarText);
    $("#btnAgregar").html(btnAgregarText);
    $("#btnLimpiar").html(btnLimpiarText);

    $("#txtArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });

    $('#txtArete').keydown(function (e) {
        
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            if ($('#txtArete').val() != '') {
                ValidarArete($('#txtArete').val());
            }
        } else {
            presionoEnter = false;
        }
    });

    $('#ddlLugarValidacion').keydown(function (e) {

        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            $("#txtArete").focus();
        } else {
            presionoEnter = false;
        }
    });

    $('#ddlCausa').keydown(function (e) {

        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            $("#btnAgregar").focus();
        } else {
            presionoEnter = false;
        }
    });

    $("#txtCorral").inputmask({ "mask": "*", "repeat": 15, "greedy": false });

    $('#txtCorral').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            if ($('#txtCorral').val() != '') {
                ValidarCorral();
            }
        } else {
            presionoEnter = false;
        }
    });

    //Prevenimos el evento enter para la pantalla completa
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
        return true;
    });

    $('#btnAgregar').live("click", function () {
        ValidarDatos();
    });

    $('#btnLimpiar').live("click", function () {
        LimpiarDatos();
    });

    $('#btnCancelar').live("click", function () {
        CancelarRevision();
    });

    $('#btnGuardar').click(function () {
        GuardarRevison();
    });
    
    //Cargar datos
    CargarLugaresValidacion();
    CargarCausas();

    var revisiones = {};
    CargarGridRevision(revisiones);
    $('#lblTotal').text(0);
    $('#lblCorrectos').text(0);
    $('#lblIncorrectos').text(0);
    $('#txtEfectividad').val(0);

});

CargarLugaresValidacion = function () {
    var datos = {};
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ObtenerLugaresValidacion',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.Lista == null || msg.d.Lista.length == 0) {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.ErrorLugarValidacion,
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
            $(msg.d.Lista).each(function () {
                var semana = this;
                var html = '<option value=' + semana.AreaRevisionId + '>' + semana.Descripcion + ' </option>';
                $('#ddlLugarValidacion').append(html);
            });
            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorLugares,
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

CargarCausas = function () {
    var datos = {};
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ObtenerCausas',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null) {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.ErrorCausas,
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
            $(msg.d.Lista).each(function () {
                var semana = this;
                var html = '<option value=' + semana.CausaId + '>' + semana.Descripcion + ' </option>';
                $('#ddlCausa').append(html);
            });
            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorObtenerCausas,
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

DesbloquearPantalla = function () {
    $("#skm_LockPane").spin(false);
    var lock = document.getElementById('skm_LockPane');
    lock.className = 'LockOff';
};

maxLengthCheck = function (object) {
    if (object.value.length > object.maxLength) {
        object.value = object.value.slice(0, object.maxLength);
    }
};

ValidarCorral = function() {

    var datos = { codigoCorral: $("#txtCorral").val() };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ValidarCorral',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d == null || msg.d.Resultado == false) {
                DesbloquearPantalla();

                bootbox.dialog({
                    message: msg.d.Mensaje,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function() {
                                $("#txtCorral").val("");
                                codigoCorral: $("#txtCorral").focus();
                            }
                        }
                    }
                });


                return false;
            } else {
                $("#ddlLugarValidacion").focus();
            }
            
            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorValidarCorral,
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

EnviarMensajeUsuario = function () {
    bootbox.dialog({
        message: msgUsuarioNoPermitido,
        buttons: {
            success: {
                label: window.Ok,
                callback: function () {
                    GoPrincipal();
                }
            },
        }
    });
};

GoPrincipal = function () {
    document.location.href = '../Principal.aspx';
};

EnviarMensaje = function (mensaje, control) {
    bootbox.dialog({
        message: mensaje,
        buttons: {
            success: {
                label: window.Ok,
                callback: function () {
                    control.focus();
                }
            },
        }
    });
};

ValidarArete = function (arete) {

    var datos = { codigoArete: arete };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ValidarArete',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.Resultado == false) {
                DesbloquearPantalla();

                bootbox.dialog({
                    message: msg.d.Mensaje,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function() {
                                $("#txtArete").val("");
                                $("#txtArete").focus();
                            }
                        }
                    }
                });

                return false;
            } else {
                $("#ddlCausa").focus();
            }

            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorArete,
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

ValidarDatos = function () {

    if ($("#txtCorral").val() == "") {
        EnviarMensaje(window.MensajeSinCorral, $("#txtCorral"));
        return false;
    }

    if ($("#ddlLugarValidacion").val() == 0) {
        EnviarMensaje(window.MensajeSinLugar, $("#ddlLugarValidacion"));
        return false;
    }

    if ($("#txtArete").val() == "") {
        EnviarMensaje(window.MensajeSinArete, $("#txtArete"));
        return false;
    }

    if ($("#ddlCausa").val() == 0) {
        EnviarMensaje(window.MensajeSinCausa, $("#ddlCausa"));
        return false;
    }

    var revision= {};
    var CorralInfo = {};
    var AnimalInfo = {};
    var CausaInfo = {}
    var lugarInfo ={};
    CorralInfo.Codigo = $("#txtCorral").val();
    AnimalInfo.Arete = $("#txtArete").val();
    CausaInfo.CausaId = $("#ddlCausa").val();
    lugarInfo.AreaRevisionId = $("#ddlLugarValidacion").val();
    revision.Corral = CorralInfo;
    revision.Animal = AnimalInfo;
    revision.Causa = CausaInfo;
    revision.LugarValidacion = lugarInfo;

    var datos = { revision: revision };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/AgregarArete',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d == null || msg.d.Resultado == false) {
                DesbloquearPantalla();

                bootbox.dialog({
                    message: msg.d.Mensaje,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function() {
                                $("#txtArete").val("");
                                $("#txtArete").focus();
                            }
                        }
                    }
                });

                return false;
            } else {
                CargarGridRevision(msg.d.Control);
                $("#txtArete").val("");
                $("#ddlLugarValidacion").prop('disabled', 'disabled');
                $("#ddlCausa > option[value='0']").attr('selected', 'selected');
                $("#txtArete").focus();
            }

            DesbloquearPantalla();
            return true;
        },
        error: function () {
            
            DesbloquearPantalla();
            bootbox.dialog({
                message: "error",
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

CargarGridRevision = function (datos) {

    var revisiones = {};
    var recursos = {};

    recursos.CabeceroCorral = window.CabeceroCorral;
    recursos.CabeceroArete = window.CabeceroArete;
    recursos.CabeceroLugarValidacion = window.CabeceroLugarValidacion;
    recursos.CabeceroCausa = window.CabeceroCausa;
    recursos.CabeceroCorrecto = window.CabeceroCorrecto;
    recursos.CabeceroEliminar = window.CabeceroEliminar;

    revisiones.Revision = datos;
    revisiones.Recursos = recursos;

    var contador = 0;
    var correctos = 0;
    var incorrectos = 0;

    if (datos != null) {
        contador = datos.length;
        for (var i = 0; i < contador; i++) {
            
            if (datos[i].Causa.Correcto == true) {
                correctos++;
            } else {
                incorrectos++;
            }
        }
    }

    var efectividad = 0;
    if (contador > 0) {
        efectividad = (correctos / contador) * 100;
    }
         
    $('#GridRevision').html('');
    $('#GridRevision').setTemplateURL('../Templates/GridRevisionImplante.htm');
    $('#GridRevision').processTemplate(revisiones);
    $('#lblTotal').text(contador);
    $('#lblCorrectos').text(correctos);
    $('#lblIncorrectos').text(incorrectos);
    $('#txtEfectividad').val(efectividad);
    return true;
};

LimpiarDatos = function () {

    $("#txtCorral").val('');
    $("#txtArete").val('');
    $("#ddlCausa > option[value='0']").attr('selected', 'selected');
};

EliminarRegistro = function (arete) {
    var datos = { codigoArete: arete }
    bootbox.dialog({
        message: window.MensajeLimpiarPantalla.replace("<ARETE>", arete),
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: rutaPantalla + '/EliminarRegistro',
                        data: JSON.stringify(datos),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d != null ) {
                                DesbloquearPantalla();
                                CargarGridRevision(msg.d.Control);
                            }

                            DesbloquearPantalla();
                            return true;
                        },
                        error: function () {
                            DesbloquearPantalla();
                            bootbox.dialog({
                                message: window.ErrorArete,
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
    
}
LimpiarTodo = function() {
    $("#txtCorral").val('');
    $("#txtArete").val('');
    $("#ddlCausa > option[value='0']").attr('selected', 'selected');
    $("#ddlLugarValidacion > option[value='0']").attr('selected', 'selected');
    $("#ddlLugarValidacion").removeAttr("disabled");
    $("#txtCorral").focus();
    $('#lblTotal').text(0);
    $('#lblCorrectos').text(0);
    $('#lblIncorrectos').text(0);
    var revisiones = {};
    CargarGridRevision(revisiones);
    LimpiarAmbiente();
}

CancelarRevision = function () {

    bootbox.dialog({
        message: window.MensajeCancelarPantalla,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function() {
                    LimpiarTodo();

                    LimpiarAmbiente();
                    return true;
                }
            },
            Cancelar: {
                label: window.No,
                callback: function() {
                    return true;
                }
            }
        }
    });

};

LimpiarAmbiente = function () {
    var datos = {};
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/Cancelar',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            return true;
        },
        error: function () {
            console.log("error al cancelar");
        }
    });
}

GuardarRevison = function() {

        var datos = {};
        BloquearPantalla();
        $.ajax({
            type: "POST",
            url: rutaPantalla + '/GuardarRevision',
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var mensaje = "";
                if (msg.d != null) {
                    mensaje = msg.d.Mensaje;
                    if (msg.d.Resultado == true) {
                        LimpiarTodo();
                    } 
                }
                else {
                    mensaje = "Ocurrio un problema al guardar la revisión";
                }
                bootbox.dialog({
                    message: mensaje,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {

                            }
                        }
                    }
                });
                
                DesbloquearPantalla();
                return true;
            },
            error: function () {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.ErrorLugares,
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


