var rutaPantalla = location.pathname;
var fechaSeleccionada = "";
//Funciones que se ejecuntan cuando el documento se encuentra listo
$(document).ready(function () {

    $('#btnDescargarArchivo').attr("disabled", "disabled");

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#btnBuscar').click(function () {
        BuscarArchivo();
    });

    $('#btnDescargarArchivo').click(function () {
        CargarArchivo();
    });

    $(function () {

        $("#datepicker").datepicker({
            firstDay: 1,
            showOn: 'button',
            buttonImage: '../assets/img/calander.png',
            onSelect: function (date) {
                fechaSeleccionada = date;
                $('#btnDescargarArchivo').attr("disabled", "disabled");
            },
        });
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });

});

//mensaje que indica que el rol del usuario no es valido
EnviarMensajeRolUsuario = function () {
    bootbox.dialog({
        message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgErrorUsuario').val(),
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
//mensaje que indica que no se encuentran configurados los servicios
EnviarMensajeServicios = function () {
    bootbox.dialog({
        message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgNoExisteTurnos').val(),
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
//Mensaje indicando que la ruta de la configuracion no existe
EnviarMensajeRuta = function () {
    bootbox.dialog({
        message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgRuta').val(),
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
//Buscar el archivo con la informacion
BuscarArchivo = function () {
    BloquearPantalla();
    if ($("#cmbServicios").val() == 0) {
        bootbox.dialog({
            message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgSeleccionesServicio').val(),
            buttons: {
                success: {
                    label: $('#msgOK').val(),
                    callback: function () {
                        $("#cmbServicios").focus();
                    }
                },
            }
        });
        DesbloquearPantalla();
       
        return false;
    }
    if (fechaSeleccionada =="") {
        bootbox.dialog({
            message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgSeleccionesFecha').val(),
            buttons: {
                success: {
                    label: $('#msgOK').val(),
                    callback: function () {
                        $("#cmbServicios").focus();
                    }
                },
            }
        });
        DesbloquearPantalla();

        return false;
    }

    var fechaActual = new Date();
    var fechaDatapicker = new Date($('#datepicker').datepicker('getDate'));
   
    if (fechaActual < fechaDatapicker) {
        bootbox.dialog({
            message: "<img src='../Images/stop.png'/>&nbsp;" + $('#msgFechaMayor').val(),
            buttons: {
                success: {
                    label: $('#msgOK').val(),
                    callback: function () {
                        $("#cmbServicios").focus();
                    }
                },
            }
        });
        DesbloquearPantalla();

        return false;
    }

    var datos = "{ 'fecha':'" + fechaSeleccionada + "', 'tipoServicio':'" + $("#cmbServicios").val() + "' }";
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/ObtenerDatosArchivo',
        data: datos,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == null) {

                DesbloquearPantalla();
                MostrarErrorDatalink($('#msgErrorProceso').val());
            } else {
                if (data.d.EsValido) {
                    if (data.d.Datos.Resultado) {
                        $('#btnDescargarArchivo').removeAttr("disabled");
                       
                    } else {
                        MostrarErrorDatalink(data.d.Datos.Mensaje);
                    }
                    DesbloquearPantalla();
                } else {
                    DesbloquearPantalla();
                    MostrarErrorDatalink($('#RepartoErrorInesperado').val());
                }
            }
        },
        error: function () {
            DesbloquearPantalla();
            MostrarErrorDatalink($('#msgErrorProceso').val());
        }
    });
    return true;
};

//muestra mensaje con la imagen de error
MostrarErrorDatalink = function (error) {
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
//Realiza la carga del archivo a los reparto
CargarArchivo = function () {

    BloquearPantalla();
    
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/DescargarCargarArchivo',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                DesbloquearPantalla();
                MostrarErrorDatalink($('#msgErrorProceso').val());
            } else {
                if (data.d.EsValido) {
                    if (data.d.Datos.Resultado) {
                        $('#btnDescargarArchivo').attr("disabled", "disabled");
                        MostrarMensajeCargaCorrecta(data.d.Datos.DescripcionMensaje);
                    } else {
                        MostrarErrorDatalink(data.d.Datos.DescripcionMensaje);
                    }
                    DesbloquearPantalla();
                } else {
                    DesbloquearPantalla();
                    MostrarErrorDatalink("error");
                }

            }

        },
        error: function () {
            DesbloquearPantalla();
            MostrarErrorDatalink($('#msgErrorProceso').val());
        }
    });
    return true;
};
//Muestra un mensaje con la imagen de correcto
MostrarMensajeCargaCorrecta = function (mensaje) {
    bootbox.dialog({
        message: "<img src='../Images/Correct.png'/>&nbsp;" + mensaje,
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


