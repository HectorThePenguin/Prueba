var usuarioValido = true;
var Aretes;
//-------------------------------------------------------------------
// Inicio de eventos de la pagina
//-------------------------------------------------------------------
$(document).ready(function () {
    //Inicializadores
    var presionoEnter = false;
    $("#btnGuardar").html(btnGuardarText);
    $("#btnCancelar").html(btnCancelarText);
    $('#btnGuardar').prop("disabled", true);

    $('#lblFecha').val('Fecha: ' + FechaFormateada);

    PonerFocoArete();

    $('#txtArete').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtArete").inputmask({ "mask": "9", "repeat": 15, "greedy": false });

    $('#txtAreteTestigo').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtAreteTestigo").inputmask({ "mask": "9", "repeat": 15, "greedy": false });
    
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }

        return true;
    });

    $('#txtArete').keydown(function (e) {
        $('#txtAreteTestigo').val('');
    });


    $('#txtAreteTestigo').keydown(function (e) {
        $('#txtArete').val('');
    });

    $('#btnSeleccionarArete').click(function () {
        
        if ($('#txtArete').val() != '') {
            MarcarAreteRecolectado($('#txtArete').val(), 1);
        }
        
        if ($('#txtAreteTestigo').val() != '') {
            MarcarAreteTestigoRecolectado($('#txtAreteTestigo').val(), 2);
        }
    });
    
    $('#btnCancelar').click(function () {
        bootbox.dialog({
            message: msgDlgCancelar,
            //title: msgTitulo,
            //closeButton: true,
            buttons: {
                success: {
                    label: "Si",
                    className: "btn SuKarne",
                    callback: function () {
                        GoLocal();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function () {
                        PonerFocoArete();
                    }
                }
            }
        });
    });

    $('#btnGuardar').click(function () {
        GuardarRecoleccion();
    });

    if (usuarioValido)
        ObtenerGanadoMuertoDetectado();
});

/*
 * Poner foco del arete
 */
PonerFocoArete = function() {
    $('#txtArete').focus();
};

/*
 * Guarda la recoleccion del ganado seleccionado
 */
GuardarRecoleccion = function() {
    var elementos = new Array();

    for (var ele in Aretes) {
        //detectamos los seleccionados con el campo estatusid
        if (Aretes[ele].EstatusId == 1) {
           var arete =
           {
                OrganizacionId: Aretes[ele].OrganizacionId,
                CorralCodigo: Aretes[ele].CorralCodigo,
                LoteCodigo: Aretes[ele].LoteCodigo,
                CorralId: Aretes[ele].CorralId,
                MuerteId: Aretes[ele].MuerteId,
                Arete: Aretes[ele].Arete,
                AreteMetalico: Aretes[ele].AreteMetalico,
                Observaciones: Aretes[ele].Observaciones,
                LoteId: Aretes[ele].LoteId,
                OperadorDeteccionId: Aretes[ele].OperadorDeteccionId,
                OperadorDeteccionInfo:  null,
                FechaDeteccion: ToJavaScriptDate(Aretes[ele].FechaDeteccion),
                FotoDeteccion: Aretes[ele].FotoDeteccion,
                OperadorRecoleccionId: Aretes[ele].OperadorRecoleccionId,
                OperadorRecoleccionInfo: null,
                FechaRecoleccion: Aretes[ele].FechaRecoleccion,
                OperadorNecropsiaId: Aretes[ele].OperadorNecropsiaId,
                FechaNecropsia: ToJavaScriptDate(Aretes[ele].FechaNecropsia),
                FotoNecropsia: Aretes[ele].FotoNecropsia,
                OperadorCancelacion: Aretes[ele].OperadorCancelacion,
                OperadorCancelacionInfo: null,
                FechaCancelacion: ToJavaScriptDate(Aretes[ele].FechaCancelacion),
                MotivoCancelacion: Aretes[ele].MotivoCancelacion,
                EstatusId: Aretes[ele].EstatusId,
                ProblemaId: Aretes[ele].ProblemaId,
                Activo: Aretes[ele].Activo,
                FechaCreacion: ToJavaScriptDate(Aretes[ele].FechaCreacion),
                Peso: Aretes[ele].Peso,
                AnimalId: Aretes[ele].AnimalId,
                UsuarioCreacionID: Aretes[ele].UsuarioCreacionID
                

           };

            elementos.push(arete);
        }

    }

    $.ajax({
        type: "POST",
        url: "CheckListGanadoMuerto.aspx/GuardarRecoleccion",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(elementos),
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == true) {
                MostrarGuardadoExito(data);
            } else {
                MostrarGuardadoFallido(data.d.Mensaje);
            }
        },
        error: function (request) {
            MostrarGuardadoFallido(request);
        }
    });
};
/*
 * Funcion para mostrar el mensaje de usuario sin privilegios
 */
EnviarMensajeUsuario = function () {

    usuarioValido = false;

    bootbox.dialog({
        message: msgNoTienePermiso,
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


/*
 * Funcion que envia a la pantalla de salida por muerte
 */
GoLocal = function () {
    document.location.href = 'CheckListGanadoMuerto.aspx';
};

/*
 * Funcion para obtener la informacion de los aretes detectados para recoleccion
 */
ObtenerGanadoMuertoDetectado = function () {
    $.ajax({
        type: "POST",
        url: "CheckListGanadoMuerto.aspx/ObtenerAretesMuertosRecoleccion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            MostrarFalloCargarDatos();
        },
        success: function (data) {
            if (data.d == null) {
                MostrarNoHayDatos();
                return;
            }
            var respuesta = {};
            respuesta.Aretes = data.d;
            Aretes = respuesta.Aretes;
        
            respuesta.Recursos = LlenarRecursos();
            var listaAretes = [];
            var listaAretesMetalico = [];
            for (var ele in respuesta.Aretes) {

                Aretes[ele].EstatusId = 0; //elemento para determinar la seleccion

                respuesta.Aretes[ele].MotivoCancelacion = 'chkRecibido_' + respuesta.Aretes[ele].MuerteId; //identinficador del check

                if (Aretes[ele].Arete != '')
                    listaAretes.push(Aretes[ele].Arete);

                if (Aretes[ele].AreteMetalico != '')
                    listaAretesMetalico.push(Aretes[ele].AreteMetalico);

                respuesta.Aretes[ele].HoraRecoleccionString = '';

            }

            $('#txtArete').typeahead({
                source: listaAretes
            });

            $('#txtAreteTestigo').typeahead({
                source: listaAretesMetalico
            });

            if (data.d.EsValido == false) {
                AgregaElementosTabla(respuesta);

            } else {
                AgregaElementosTabla(respuesta);

            }
        }
    });
};

/*
 * Marcar el arete testigo
 */
MarcarAreteTestigoRecolectado = function (areteMarcar, tipo) {
    var control = null;
    var encontrado = false;

    for (var x in Aretes) { //buscamos el id el arete

        if ( Aretes[x].AreteMetalico == areteMarcar) {

            if(Aretes[x].EstatusId == 1)
            {
                //El arete ya fue marcado previamente.
                MostrarMensajeAreteMarcadoPrevio(tipo);
                return;
            }

            control = $('#chkRecibido_' + Aretes[x].MuerteId);

            if (control.length == 1) {

                control.attr('Checked', 'True');
                var fecha = new Date();
                $('#chkRecibido_' + Aretes[x].MuerteId + '_hora').text(fecha.toLocaleTimeString());

                Aretes[x].EstatusId = 1; //determinamos como seleccionado el elemento
                Aretes[x].FechaRecoleccion = fecha;
                HabilitarGuardado();


                $('#txtArete').val('');
                $('#txtAreteTestigo').val('');
                PonerFocoArete();
                encontrado = true;
                break;

            }
            else {
                break;
            }

        }

    }

    if (!encontrado)
        MostrarDialogoNoDetectado(tipo);


};

/*
 * Marca el arte recolectado
 */ 
MarcarAreteRecolectado = function(areteMarcar, tipo) {
    var control = null;

    var encontrado = false;

    for (var x in Aretes) { //buscamos el id el arete

        if (Aretes[x].Arete == areteMarcar) {

            if (Aretes[x].EstatusId == 1) {
                //El arete ya fue marcado previamente.
                MostrarMensajeAreteMarcadoPrevio(tipo);
                return;
            }

            control = $('#chkRecibido_' + Aretes[x].MuerteId);

            if (control.length == 1) {
                
                    control.attr('Checked', 'True');
                    var fecha = new Date();
                    $('#chkRecibido_' + Aretes[x].MuerteId + '_hora').text(fecha.toLocaleTimeString());

                    Aretes[x].EstatusId = 1; //determinamos como seleccionado el elemento
                    Aretes[x].FechaRecoleccion = fecha;
                    HabilitarGuardado();


                    $('#txtArete').val('');
                    $('#txtAreteTestigo').val('');
                    PonerFocoArete();
                    encontrado = true;
                    break;

            }
            else
            {
                break;
            }

        }

    }

    if(! encontrado)
            MostrarDialogoNoDetectado(tipo);
       

};

/*
 * Funcion que muestra el dialogo de mensaje de arete no detectado
 */
MostrarDialogoNoDetectado = function (tipo) {
    var mensaje = msgAreteNoDetectado;

    if (tipo == 2)
        mensaje = msgAreteNoDetectadoMetalico;

    bootbox.dialog({
        message: mensaje,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function() {
                    PonerFocoArete();
                    $('#txtArete').val('');
                    $('#txtAreteTestigo').val('');
                }
            },
        }
    });
};

/*
 * Mostrar cuando un arete ya fue marcado
 */
MostrarMensajeAreteMarcadoPrevio = function(tipo) {

    var mensaje = msgMostrarMensajeAreteMarcadoPrevio;

    if (tipo == 2)
        mensaje = msgMostrarMensajeAreteTestigoMarcadoPrevio;

    bootbox.dialog({
        message: mensaje,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    $('#txtArete').val('');
                    $('#txtAreteTestigo').val('');
                    PonerFocoArete();

                }
            },
        }
    });
};

/*
 * Funcion muestra el dialogo de mensaje de guardar fallido
 */
MostrarGuardadoFallido = function () {
    bootbox.dialog({
        message: msgFalloAlGuardar,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    PonerFocoArete();
                }
            },
        }
    });
};

/*
 * Funcion que muestra el dialogo de mensaje de guardado exitoso
 */
MostrarGuardadoExito = function () {
    bootbox.dialog({
        message: "<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardadosExito,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    GoLocal();
                }
            },
        }
    });
};

/*
 * Funcion que muestra el dialogo de no existen datos
 */
MostrarNoHayDatos = function () {
    bootbox.dialog({
        message: msgNoHayMuertesRecoleccion,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    PonerFocoArete();
                }
            }
        }
    });
};

/*
 * Funcion que muestra el mensaje de fallo de cargar datos
 */
MostrarFalloCargarDatos = function () {
    bootbox.dialog({
        message: msgFalloCargarDatos,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    PonerFocoArete();
                }
            }
        }
    });
};


/*
 * Hailita el boton guardar
 */
HabilitarGuardado = function () {
    $('#btnGuardar').prop("disabled", false);
};

AgregaElementosTabla = function (datos) {
    if (datos != null) {
        $('#TablaAretes').setTemplateURL('../Templates/GridCheckListGanadoMuerto.htm');
        $('#TablaAretes').processTemplate(datos);
    } else {
        $('#TablaAretes').html("");
    }
};

//Cabecero de la tabla
LlenarRecursos = function () {
    Resources = {};
    Resources.HeaderArete = hArete;
    Resources.HeaderAreteTestigo = hAreteTestigo;
    Resources.HeaderObservaciones = hObservaciones;
    Resources.HeaderCorral = hCorral;
    Resources.HeaderFechaDeteccion = hFechaDeteccion;
    Resources.HeaderHoraDeteccion = hHoraDeteccion;
    Resources.HeaderDetector = hDetector;
    Resources.HeaderHoraRecoleccion = hHoraRecoleccion;
    Resources.HeaderSeleccione = hSeleccione;
    Resources.GridTitle = msgTitulo;
    return Resources;
};

//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
};

/* fecha formateada */
FechaFormateada = function (date) {
    var d = new Date(date),
       month = '' + (d.getMonth() + 1),
       day = '' + d.getDate(),
       year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    if (year > 2050) {
        day = '01';
        month = '01';
        year = '0001';
    }
    return [day, month, year].join('/');
};

/*
 * convertir fecha en formato net
 */
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    //return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    return dt;
}