//Document Ready Function, la pagina esta completamente cargados
var totalRecibidos = 0;
var Aretes;
var usuarioValido = true;
/*
 * Inicialización del documento con jquery
 */
$(document).ready(function () {
    var presionoEnter;
    //Inicializadores
    $("#btnGuardar").html(btnGuardarText);
    $("#btnCancelar").html(btnCancelarText);
    $('#btnGuardar').prop("disabled", true);

    PonerFocoArete();

    $("#txtArete").inputmask({ "mask": "9", "repeat": 15, "greedy": false });
    $("#txtAreteTestigo").inputmask({ "mask": "9", "repeat": 15, "greedy": false });

    //Eventos del txtArete
    $('#txtArete').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#txtArete').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            if ($('#txtArete').val() != '') {
                MarcarAreteRecibido();
            }
        }
        else {
            presionoEnter = false;
        }
    });

   //Eventos del txtAreteTestigo

    $('#txtAreteTestigo').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#txtAreteTestigo').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            if ($('#txtAreteTestigo').val() != '') {
                MarcarAreteTestigoRecibido();
            }
        } else {
            presionoEnter = false;
        }
    });

    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }

        return true;
    });

    $('#reload').click(function() {

        if ($('#txtArete').val() != '' &&  $('#txtAreteTestigo').val() == '')
        {
            MarcarAreteRecibido();
        } else if ($('#txtArete').val() == '' && $('#txtAreteTestigo').val() != '')
        {
            MarcarAreteTestigoRecibido();
        }
    });
   
    $('#btnGuardar').click(function () {
        GuardarRecepcion();
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

    if (usuarioValido)
        ObtenerAretesMuertosRecepcion();
});

//Poner foco el el texto arete
PonerFocoArete = function() {

    $('#txtArete').focus();
};

//Poner foco en el texto arete metalico
PonerFocoAreteMetalico = function() {
    $('#txtAreteTestigo').focus();;
};
/*
 * Funcion que envia a la pantalla de salida por muerte
 */
GoLocal = function () {
    document.location.href = 'RecepcionGanadoMuerto.aspx';
};

/*
 * Funcion que muestra el dialogo de mensaje de arete no detectado
 */
MostrarDialogoNoDetectado = function (arete) {


    bootbox.dialog({
        message: msgAreteNoDetectado,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    //PonerFocoArete();
                }
            },
        }
    });

    

};


/*
 * Funcion que muestra el dialogo de mensaje de arete no detectado
 */
MostrarDialogoNoDetectadoTestigo = function (arete) {
    bootbox.dialog({
        message: msgAreteNoDetectadoTestigo,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    //PonerFocoArete();
                }
            },
        }
    });

};
/*
 * Hailita el boton guardar
 */
HabilitarGuardado = function() {
    $('#btnGuardar').prop("disabled", false);
};

/*
 * Marca un arete recibido de la lista
 */
MarcarAreteRecibido = function() {
   
    var arete = $('#txtArete').val();
    var control = $('#chkRecibido_' + arete);
    if (control.length == 1) {
        var dato = $('#chkRecibido_' + arete).attr('data-arete');


        if (dato == arete) {
            $('#chkRecibido_' + arete).attr('Checked', 'True');

            for (var ele in Aretes) {
                if (Aretes[ele].Arete == arete && Aretes[ele].EstatusId == 0) {
                    Aretes[ele].EstatusId = 1; //determinamos como seleccionado el elemento
                    totalRecibidos++;
                    HabilitarGuardado();
                    break;
                }

            }

            $('#txtTotalRecibidos').val(totalRecibidos);
            $('#txtArete').val('');
            $('#txtArete').focus();
        } else {
            MostrarDialogoNoDetectado();
            PonerFocoArete();
        }

    } else {
        MostrarDialogoNoDetectado();
        PonerFocoArete();
    }

};

/*
 * Marcar arete de la lista con el arete testigo
 */
MarcarAreteTestigoRecibido = function() {
    var arete = $('#txtAreteTestigo').val();
    var areteMetalico = null;
    var areteLista = null;


    for (var ele in Aretes) {
        if (Aretes[ele].AreteMetalico == arete) {
            areteLista = Aretes[ele].Arete;
            areteMetalico = Aretes[ele].AreteMetalico;
            break;
        }

    }

    var control = $('#chkRecibido_' + areteLista);
    arete = areteLista;
    if (areteLista == null) {
        control = $('#chkRecibido_' + areteMetalico);
        arete = areteMetalico;
    }

    if (control.length == 1) {
        var dato = $('#chkRecibido_' + arete).attr('data-aretet');

        if (dato == areteMetalico ) {
            $('#chkRecibido_' + arete).attr('Checked', 'True');

            for (var ele in Aretes) {
                if ( (Aretes[ele].AreteMetalico == arete || Aretes[ele].Arete == arete ) && Aretes[ele].EstatusId == 0) {
                    Aretes[ele].EstatusId = 1; //determinamos como seleccionado el elemento
                    totalRecibidos++;
                    HabilitarGuardado();
                    break;
                }

            }


            $('#txtTotalRecibidos').val(totalRecibidos);
            $('#txtAreteTestigo').val('');
            $('#txtArete').focus();
        } else {
            MostrarDialogoNoDetectadoTestigo();
            PonerFocoAreteMetalico();
        }

    } else {
        MostrarDialogoNoDetectadoTestigo();
        PonerFocoAreteMetalico();
    }

};

/*
 * Funcion que valida la previa a guardar la recepcion de ganado
 */
GuardarRecepcion = function() {

    var totalRecolectados = $('#txtTotalRecolectados').val();
    var totalDetectados = $('#txtTotalDetectados').val();
    //RN mosrar el mensaje de diferencia entre los recibidos y los detectados

    if (totalRecibidos != totalRecolectados || totalRecibidos != totalDetectados) {
        bootbox.dialog({
            message: msgNoCuardraRecibidos,
            //title: msgTitulo,
            //closeButton: true,
            buttons: {
                success: {
                    label: "Si",
                    className: "btn SuKarne",
                    callback: function() {
                        GuardarConfirmado();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function() {
                        PonerFocoArete();
                    }
                }
            }
        });
    } else {
        GuardarConfirmado();
    }
 
};

/*
 * Funcion que muestra el dialogo de mensaje de arete no detectado
 */
GuardarConfirmado = function() {

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
               OperadorDeteccionInfo: null,
               FechaDeteccion: ToJavaScriptDate(Aretes[ele].FechaDeteccion),
               FotoDeteccion: Aretes[ele].FotoDeteccion,
               OperadorRecoleccionId: Aretes[ele].OperadorRecoleccionId,
               OperadorRecoleccionInfo: null,
               FechaRecoleccion: ToJavaScriptDate(Aretes[ele].FechaRecoleccion),
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
        url: "RecepcionGanadoMuerto.aspx/GuardarRecepcion",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(elementos),
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == true) {
                MostrarGuardadoExito(data);
            } else {
                MostrarGuardadoFallido(data.d.Mensaje);
                PonerFocoArete();
            }
        },
        error: function (request) {
            MostrarGuardadoFallido(request);
            PonerFocoArete();
        }
    });

};

/*
 * Funcion muestra el dialogo de mensaje de guardar fallido
 */
MostrarGuardadoFallido = function() {
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
MostrarGuardadoExito = function() {
    bootbox.dialog({
        message: "<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardadosExito,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function() {
                    GoLocal();
                }
            },
        }
    });
};

/*
 * Funcion para obtener los artes muertos de la recepción
 */
ObtenerAretesMuertosRecepcion = function () {
    //modificar para obtener los movimientos

    $.ajax({
        type: "POST",
        url: "RecepcionGanadoMuerto.aspx/ObtenerAretesMuertosRecepcion",
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

            var totalDetectados = 0;
            var totalRecolectados = 0;
            var areteValidar;

            var listaAretes = [];
            var listaAretesMetalico = [];

            for (var ele in respuesta.Aretes) {

                Aretes[ele].EstatusId = 0; //elemento para determinar la seleccion

                areteValidar = respuesta.Aretes[ele].Arete;

                if (respuesta.Aretes[ele].Arete == '')
                    areteValidar = respuesta.Aretes[ele].AreteMetalico;

                respuesta.Aretes[ele].MotivoCancelacion = 'chkRecibido_' + areteValidar;

                if (respuesta.Aretes[ele].FechaDeteccionString != '') {
                    totalDetectados ++;
                }

                if (respuesta.Aretes[ele].FechaRecoleccionString != '') {
                    totalRecolectados++;
                }

                if (Aretes[ele].Arete != '')
                    listaAretes.push(Aretes[ele].Arete);

                if (Aretes[ele].AreteMetalico != '')
                    listaAretesMetalico.push(Aretes[ele].AreteMetalico);
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

            //Establecemos los valores de los text
            $('#txtTotalDetectados').val(totalDetectados);
            $('#txtTotalRecolectados').val(totalRecolectados);
            $('#txtTotalRecibidos').val(totalRecibidos);
        }
    });


};

/*
 * Funcion que muestra el dialogo de no existen datos
 */
MostrarNoHayDatos = function () {
    bootbox.dialog({
        message: msgNoHayMuertesRecepcion,
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
 * Function que agrega los elementos al template del grid
 */
AgregaElementosTabla = function (datos) {
    if (datos != null) {
        $('#TablaAretesMuertos').setTemplateURL('../Templates/GridRecepcionGanadoMuerto.htm');
        $('#TablaAretesMuertos').processTemplate(datos);
    } else {
        $('#TablaAretesMuertos').html("");
    }
};

/*
 * Funcion para llenar los recursos de la tabla
 */
LlenarRecursos = function () {
    Resources = {};
    Resources.HeaderArete = hArete;
    Resources.HeaderAreteTestigo = hAreteTestigo;
    Resources.HeaderFechaDeteccion = hFechaDeteccion;
    Resources.HeaderHoraDeteccion = hHoraDeteccion;
    Resources.HeaderFechaRecoleccion = hFechaRecoleccion;
    Resources.HeaderHoraRecoleccion = hHoraRecoleccion;
    Resources.HeaderRecibido = hRecibido;
    Resources.GridTitle = msgTitulo;
    return Resources;
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

//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
};

//convertir a formato javascript
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    //return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    return dt;
}