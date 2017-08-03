//-------------------------------------------------------------------
// Inicio de eventos de la pagina
//-------------------------------------------------------------------

var muerteIDSeleccionada;
var rutaPantalla = location.pathname;
$(document).ready(function () {

    muerteIDSeleccionada = -1;

    $('#txtComentarios').val("");

    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#btnCancelar').click(function () {
        Cancelar();
    });

    LimpiarPantalla();

    ObtenerMovimientos();

    $('#btnGuardado').click(function () {
        LimpiarPantalla();
        $(location).attr('href', rutaPantalla);
    });

});

//Función para btnCancelar.
Cancelar = function () {
    $('#dlgCancelarMovimiento').modal('hide');
    bootbox.dialog({
        message: window.SalirSinGuardar,
        buttons: {
            Aceptar: {
                label: window.Si,
                callback: function () {
                    $('#dlgCancelarMovimiento').modal('hide');
                    return true;
                }
            },
            Cancelar: {
                label: window.No,
                callback: function () {
                    $('#dlgCancelarMovimiento').modal('show');
                    $('#txtComentarios').focus();
                    return true;
                }
            }
            
        }
    });

};

//Función para Validar las Cabezas capturadas antes de guardar
ValidarGuardar = function () {
    var comentarios = $('#txtComentarios').val();

    if ($.trim(comentarios) == "") {
        bootbox.dialog({
            message: ErrorDatosEnBlanco,
            buttons: {
                Aceptar: {
                    label: $('#msgOK ').val(),
                    callback: function () {
                        $('#dlgCancelarMovimiento').modal('show');
                        $('#txtComentarios').focus();
                    }
                }
            }
        });

        //$('#txtComentarios').focus();
        return false;
    }
    
    return true;
};

//Función para Guardar las Calidades de Ganado capturadas.
Guardar = function () {
    var valido = ValidarGuardar();
    if (!valido) {
        return false;
    }
    var ganadoMuertoInfo = CargarInformacionGuardar();
    var datos = { 'ganadoMuertoInfo': ganadoMuertoInfo };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/CancelarMovimientoMuerte',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            DesbloquearPantalla();
            $('#msgGuardadoModal').modal('show');
            /*bootbox.dialog({
                message: GuardoExito,
                buttons: {
                    Aceptar: {
                        label: $('#msgOK ').val(),
                        callback: function () {
                            LimpiarPantalla();
                            $(location).attr('href', rutaPantalla);
                        }
                    }
                }
            });*/
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorGuardar,
                buttons: {
                    Aceptar: {
                        label: $('#msgOK ').val(),
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
    return true;
};

//Función para cargar la información de las calidades que se van a Guardar.
CargarInformacionGuardar = function () {
    var ganadoMuertoInfo = {};

    ganadoMuertoInfo.MuerteID = muerteIDSeleccionada;
    ganadoMuertoInfo.MotivoCancelacion = $('#txtComentarios').val();

    return ganadoMuertoInfo;
};

LimpiarPantalla = function () {
    muerteIDSeleccionada = -1;
    $('#lblCodigoCorralSeleccionado').val('');
    $('#lblNoAreteSelecionado').val('');
    $('#lblDetectorSeleccionado').val('');
    $('#lblRecolectorSeleccionado').val('');
    $('#txtComentarios').val('');
};


ObtenerMovimientos = function () {
    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ObtenerInformacionCancelarMovimiento',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            //alert(request.Message);
        },
        success: function (data) {
            var respuesta = {};
            respuesta.Muertes = data.d;
            respuesta.Recursos = LlenarRecursos();
            if (data.d == null) {
                EnviarMensajeNoExistenMovimientos();
            } else {
                AgregaElementos(respuesta);
            }

        }
    });
};

EnviarMensajeRolUsuario = function() {
    bootbox.dialog({
        message:  $('#msgErrorRolJefeSanidad ').val(),
        buttons: {
            Aceptar: {
                label: $('#msgOK ').val(),
                callback: function() {
                    history.go(-1);
                }
            }
        }
    });
};

EnviarMensajeNoExistenMovimientos = function() {
    bootbox.dialog({
        message: $('#msgErrorNoHayMovimientos ').val(),
        buttons: {
            Aceptar: {
                label: $('#msgOK ').val(),
                callback: function() {
                    history.go(-1);
                }
            }
        }
    });
};

AgregaElementos = function (datos) {
    if (datos != null) {
        $('#TablaMovimientoCancelar').setTemplateURL('../Templates/GridCancelarMovimientos.htm');
        $('#TablaMovimientoCancelar').processTemplate(datos);
    } else {
        $('#TablaMovimientoCancelar').html("");
    }
};

//Cabecero de la tabla
LlenarRecursos = function () {
    Resources = {};
    Resources.Corral = msgCorral;
    Resources.NoArete = msgNoArete;
    Resources.AreteTestigo = msgAreteTestigo;
    Resources.FechaDeteccion = msgFechaDeteccion;
    Resources.HoraDeteccion = msgHoraDeteccion;
    Resources.Detector = msgDetector;
    Resources.Acciones = msgAccion;
    return Resources;
};

function SelecionaEditar(muerteID, arete, areteMetalico, operadorDeteccion, corralCodigo, operadorRecoleccion) {

    if (muerteID != 0) {
        muerteIDSeleccionada = muerteID;
        $('#lblCodigoCorralSeleccionado').text(corralCodigo);
        $('#lblNoAreteSelecionado').text(arete);
        $('#lblNoAreteMetalicoSeleccionado').text(areteMetalico);
        $('#lblDetectorSeleccionado').text(operadorDeteccion);
        $('#lblRecolectorSeleccionado').text(operadorRecoleccion);
        $('#txtComentarios').val('');

        $('#dlgCancelarMovimiento').modal('show');

        $('#txtComentarios').focus();
    }

}