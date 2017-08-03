
var fechaSeleccionada = '';
$(document).ready(function () {
    
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#btnGenerarArchivo').click(function () {
        GenerarArchivo();
    });

    $('#btnCancelar').click(function () {
        VerCancelar();
    });
    
    $(function () {

        $("#datepicker").datepicker({
            firstDay: 1,
            showOn: 'button',
            buttonImage: '../assets/img/calander.png',
            onSelect: function (date) {
                fechaSeleccionada = date;
            },
        });
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });
    
});

GenerarArchivo = function () {
    
    if ($("#cboServicios").val() == 0) {
        bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgSeleccioneServicio, function () { $("#cboServicios").focus(); });
        return false;
    }

    if (fechaSeleccionada == "") {
        bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgSeleccioneFecha, function () { $("#cboServicios").focus(); });
        return false;
    }
    var datos = { fechaReparto: fechaSeleccionada, tipoServicioID: $("#cboServicios").val() };
    $.ajax({
        type: "POST",
        url: "GenerarArchivoDataLink.aspx/GenerarArchivo",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == null) {
                bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgOcurrioErrorProceso);
            } else {
                if (data.d.Resultado) {
                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgGeneradoExito);
                } else {
                    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgOcurrioErrorProceso);
                }
            }
        },
        error: function () {
            bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgOcurrioErrorProceso);
        }
    });
    return true;
};

VerCancelar = function () {
    bootbox.dialog({
        message: msgDlgCancelar,
        buttons: {
            success: {
                label: "Si",
                className: "btn SuKarne",
                callback: function () {
                    GoPrincipal();
                }
            },
            danger: {
                label: "No",
                className: "btn SuKarne",
                callback: function () {
                    PonerFocoFecha();
                }
            }
        }
    });

};

GoPrincipal = function () {
    document.location.href = '../Principal.aspx';
};

PonerFocoFecha = function () {
    $("#txtFechaReparto").focus();
};

//mensaje que indica que el rol del usuario no es valido
EnviarMensajeRolUsuario = function () {    
    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgErrorRolUsuario, function () { history.go(-1); });
};

//mensaje que indica que no se encuentran configurados los servicios
EnviarMensajeServicios = function () {
    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgNoExisteTurnos, function () { history.go(-1); });
};
//Mensaje indicando que la ruta de la configuracion no existe
EnviarMensajeRuta = function () {
    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgRutaNoExiste, function () { history.go(-1); });
};

//Mensaje indicando que la ruta no esta configurada
EnviarMensajeRutaNoConfigurada = function () {
    bootbox.alert("<img src='../Images/stop.png'/>&nbsp;" + msgRutaNoConfigurada, function () { history.go(-1); });
};