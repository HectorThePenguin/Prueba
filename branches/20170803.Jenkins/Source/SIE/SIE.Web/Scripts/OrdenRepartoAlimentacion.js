


var tipoServicio = -1;
var rutaPantalla = location.pathname;
var value = 0;
var intervalID;
var idUsuario;
var turno = false;
var fechaSeleccionada;

$(document).ready(function () {

    $(function () {
        $("#txtFechaReparto").datepicker({
            firstDay: 1,
            showOn: 'button',
            buttonImage: '../assets/img/calander.png',
            onSelect: function (date) {

                $("#btnGuardarReparto").removeAttr("disabled");
                $('#btnCancelar').removeAttr("disabled");
                fechaSeleccionada = $("#txtFechaReparto").val();
                //if (ValidarFecha()) {

                //    ValidarCorral();
                //} else {
                //    //Mensaje campo corral vacio
                //    if (msjAbierto == 0) {
                //        msjAbierto = 1;
                //        bootbox.alert("La fecha seleccionada debe ser entre hoy y mañana", function () {
                //            $("#txtCorral").focus();
                //            msjAbierto = 0;
                //        });
                //        InicializarFecha();
                //    }
                //}

            },
        });
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });

    $('#btnGuardarReparto').attr("disabled", "disabled");
    $('#btnCancelar').attr("disabled", "disabled");

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#rdbMatutino').change(function () {
        if ($(this).is(':checked')) {
            //$("#btnGuardarReparto").removeAttr("disabled");
            //$('#btnCancelar').removeAttr("disabled");
            tipoServicio = 1;
            //var fechaActual = new Date();
            //$('#txtFechaReparto').val(FechaFormateada(fechaActual.addDays(1)));
        }
    });

    $('#rdbVespertino').change(function () {
        if ($(this).is(':checked')) {
            tipoServicio = 2;
            //$("#btnGuardarReparto").removeAttr("disabled");
            //$('#btnCancelar').removeAttr("disabled");
            //$('#txtFechaReparto').val(FechaFormateada(new Date()));
        }
    });

    $('#btnGuardarReparto').click(function () {
        ValidarGuardarReparto();
    });

    $('#btnCancelar').click(function () {
        CancelarProceso();
    });
});
//Verifica el rol del usuario
EnviarMensajeRolUsuario = function () {
    bootbox.dialog({
        message: $('#msgErrorUsuario').val(),
        buttons: {
            Aceptar: {
                label: $('#msgOK').val(),
                callback: function () {
                    history.go(-1);
                }
            }
        }
    });
};

//Verifica el rol del usuario
EnviarMensajeSinParametros = function () {

    $('#rdbVespertino').attr('disabled', true);
    $('#rdbMatutino').attr('disabled', true);
    bootbox.dialog({
        message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgErrorParametros').val(),
        buttons: {
            Aceptar: {
                label: $('#msgOK').val(),
                callback: function () {
                    history.go(-1);
                }
            }
        }
    });
};
//Verifica si se puede realizar la orden de reparto
ValidarGuardarReparto = function () {
    BloquearPantalla();
    var datos = { 'tipoServicio': tipoServicio };
    value = 0;
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ValidarGuardar',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                DesbloquearPantalla();
                MostrarError($('#msgErrorProceso').val());
            } else {
                if (data.d.EsValido) {

                    if (data.d.Datos.Resultado) {
                        DesbloquearPantalla();
                        $('#responsive').modal('show');
                        GuardarOrdenReparto();
                    } else {
                        DesbloquearPantalla();
                        MostrarError($('#' + data.d.Datos.Mensaje).val());
                    }
                } else {
                    DesbloquearPantalla();
                    MostrarError($('#msgErrorProceso').val());
                }

            }

        },
        error: function () {
            DesbloquearPantalla();
            MostrarError($('#msgErrorProceso').val());
        }
    });
    return true;
};
//Verifica el avance del proceso
function VerificarAvance() {
    var datos = "{ 'idUsuario':'" + $('#idUsuario').val() + "' }";
    $.ajax({
        type: "POST",
        url: "Reparto.asmx/ObtenerAvanceReparto",
        data: datos,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                clearInterval(intervalID);
                MostrarError($('#msgErrorProceso').val());
            } else {
                if (data.d.EsValido) {
                    if (data.d.Datos.EstatusError == 0) {
                        $('.bar').css("width", data.d.Datos.PorcentajeTotal + '%');
                        $("#lblEstatusProceso").text(data.d.Datos.Seccion);
                        $("#labelPorcentaje").text(data.d.Datos.TotalCorralesProcesados + " de " + data.d.Datos.TotalCorrales + "(" + data.d.Datos.PorcentajeTotal + "%)");
                    } else {
                        clearInterval(intervalID);
                        MostrarError($('#msgErrorProceso').val() + " con estatud error");
                    }

                } else {
                    clearInterval(intervalID);
                    MostrarError($('#msgErrorProceso').val() + " no fue valido");
                }

            }

        },
        error: function (error, textStatus, errorThrown) {
            console.log("error");
            console.log(error.responseText);
            console.log(textStatus);
            console.log(errorThrown);
            clearInterval(intervalID);
            MostrarError($('#msgErrorProceso').val() + " error critico");
        }
    });
}

//Función para Guardar la orden de raparto
GuardarOrdenReparto = function () {

    var seccion = TryParseInt($('#ddlSeccion').val(), 0);
    var datos = { 'tipoServicio': tipoServicio, 'seccion': seccion, 'fechaReparto': fechaSeleccionada };
    intervalID = setInterval(VerificarAvance, 250);
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/GenerarOrdenReparto',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (respuestaProceso) {
            clearInterval(intervalID);
            VerificarAvance();
            if (respuestaProceso.d == null) {
                MostrarError($('#msgErrorProceso').val());
            } else {
                if (respuestaProceso.d.Datos.Resultado) {
                    MostrarGuardadoCorrecto();
                } else {
                    if (respuestaProceso.d.Datos.CodigoMensaje == 1) {
                        EnviarMensajeEjecutandose();
                    } else {
                        MostrarError($('#' + respuestaProceso.d.Datos.Mensaje).val());
                    }
                }

            }

        },
        error: function () {
            clearInterval(intervalID);
            MostrarError($('#msgErrorProceso').val());
        }
    });
    return true;
};
//Muestra un mensage indicando que el proceso termino correctamente
MostrarGuardadoCorrecto = function () {
    bootbox.dialog({
        message: "<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardadosExito,
        buttons: {
            success: {
                label: $('#msgOK').val(),
                callback: function () {
                    $('#responsive').modal('hide');
                    LimpiarControles();
                }
            },
        }
    });
};
//Muestra mensaje de error
MostrarError = function (error) {
    bootbox.dialog({
        message: "<img src='../Images/stop.png'/>&nbsp;" + error,
        buttons: {
            success: {
                label: $('#msgOK').val(),
                callback: function () {
                    $('#responsive').modal('hide');
                }
            },
        }
    });
};
//Limpia los controles
LimpiarControles = function () {

    $('#txtFechaReparto').val('');
    $('#rdbVespertino').attr('checked', false);
    $('#rdbMatutino').attr('checked', false);
    $('#btnGuardarReparto').attr("disabled", "disabled");
    $('#btnCancelar').attr("disabled", "disabled");

};
//Muestra el dialogo de cancelacion
CancelarProceso = function () {

    bootbox.dialog({
        message: "<img src='../Images/questionmark.png'/>&nbsp;" + msgDatosPreguntaCancelar,
        buttons: {
            Aceptar: {
                label: "Si",
                callback: function () {
                    LimpiarControles();
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

/*
 * Funcion para mostrar el mensaje de que la orden ya se esta ejecutando
 */
EnviarMensajeEjecutandose = function () {

    bootbox.dialog({
        message: window.msgEjecutandoOrden,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    document.location.href = '../Principal.aspx';
                }
            }
        }
    });
};



