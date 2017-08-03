//-------------------------------------------------------------------
// Inicio de eventos de la pagina
//-------------------------------------------------------------------
var bEncontroDetectores = true;
var bEncontroCuestionario;
var usuarioValido = true;
var Preguntas = null;
var total = 0;
var criterios = null;
var evaluacionesAnteriores = null;
var criterioAplicadoId;
var incidencia = false;
var configuracionPeriodo = null;

$(document).ready(function () {

    //inicializadores
    $("#btnGuardar").html(btnGuardarText);
    $("#btnCancelar").html(btnCancelarText);
    $('#btnGuardar').prop("disabled", true);

    if ($('#cmbDetectores option').length == 0 && usuarioValido == true) {
        MostrarNoHayDetectores();
    }

    $("#cmbDetectores").focus();
    //quitamos el evento enter a la ventana
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }

        return true;
    });

    //evitamos el copy past de las observaciones
    $('#txtObservaciones').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    //evento keydown de las observaciones
    $('#txtObservaciones').keydown(function (e) {
        if (!solo_letras(e)) {
            e.preventDefault();
        } else {
            var charCode = (e.which) ? e.which : 0;
            if (!((charCode >= 37 && charCode <= 40) || charCode == 8)) {
                if ($('#txtObservaciones').val().length >= 254) {
                    var texto = $('#txtObservaciones').val();
                    $('#txtObservaciones').val(texto.substring(0, 255));
                }
            }
        }
    });
    //evento keyup de las observaciones
    $('#txtObservaciones').keyup(function(e) {
       if ($('#txtObservaciones').val().length >= 254) {
            var texto = $('#txtObservaciones').val();
            $('#txtObservaciones').val(texto.substring(0, 255));
        }
    
    });

    //cuando cambia la lista de detectores el valor seleccionado
    $("#cmbDetectores").change(function () {

        if ($('#cmbDetectores').val() != 0) {
            $('#dvEvaluacionAnterior').addClass('hidden');
            ObtenerSupervisionesAnteriores($('#cmbDetectores').val());
        } else {
            $("#btnGuardar").prop('disabled', true);
            $('#dvEvaluacionAnterior').addClass('hidden'); //ocultamos las evaluaciones anteriores
        }
    });

    //ligar el evento click del guardar
    $('#btnGuardar').click(function () {
        GuardarEvaluacion();
    });

    //click del cancelar
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
                        GoEvaluacion();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function () {
                        ;
                    }
                }
            }
        });
    });

    //si es usuario valido obtenemos la informacion necesaria para la funcionalidad
    if (usuarioValido) {
        ObtenerConfiguracionPeriodo();
        ObtenerCuestionarioPreguntas();
        ObtenerCriteriosEvaluacion();
        
    }


});

Date.prototype.addDays = function(days) {
    this.setDate(this.getDate() + days);
};

/*
 * Valida si el operador seleccionado tiene una evaluacion en el mismo periodo
 */
ValidarSupervision = function() {
    var fechaactual = new Date();
    var retValue = true;
    var numEvaluaciones = 0;
    var fechaInicioPeriodo = new Date(); //fecha de inicio del periodo de evaluacion
    var fechaFinPeriodo = new Date(); //fecha e fin del periodo de evaluacion fecha actual
    //restamos los dias a la fecha actual con la configuracion de los dias del periodo 
    fechaInicioPeriodo.setDate(fechaInicioPeriodo.getDate() - parseInt(configuracionPeriodo.Valor) + 1);

    //inicilizacion de las horas
    fechaInicioPeriodo.setHours(0);
    fechaInicioPeriodo.setSeconds(0);
    fechaInicioPeriodo.setMinutes(0);
    fechaInicioPeriodo.setMilliseconds(0);
    fechaFinPeriodo.setHours(23);
    fechaFinPeriodo.setSeconds(59);
    fechaFinPeriodo.setMinutes(59);
    fechaFinPeriodo.setMilliseconds(0);

    $('#dvEvaluacionAnterior').addClass('hidden'); //ocultamos las evaluaciones anteriores

    //recorremos las evaluaciones anteriores para determinar los criterios obtenidos
    for (var ele in evaluacionesAnteriores) {
        var fechajs = ToJavaScriptDate(evaluacionesAnteriores[ele].FechaSupervision);

        //si la evaluacion esta dentro del periodo
        if (fechajs > fechaInicioPeriodo && fechajs <= fechaFinPeriodo) {

            numEvaluaciones++; //incrementamos el numero de evaluaciones en el periodo
            //si ya tiene una evaluacion apta en el mismo periodo se envia el mensaje
            if (evaluacionesAnteriores[ele].CriterioSupervisionId == 3) {
                bootbox.dialog({
                    message: msgDetectorNoApto,
                    //title: msgTitulo,
                    //closeButton: true,
                    buttons: {
                        success: {
                            label: "Ok",
                            //className: "btn SuKarne",
                            callback: function() {
                                $('#cmbDetectores').val(0);
                                $('#dvEvaluacionAnterior').addClass('hidden');

                            }
                        },
                    }
                });

                retValue = false;
                break;

            } 
        }

    }

    //revisamos si el detector seleccionado tiene ya dos evaluaciones en el periodo
    if (numEvaluaciones >= 2) {
        bootbox.dialog({
            message: msgDetectorMasdeDosEvaluaciones,
            //title: msgTitulo,
            //closeButton: true,
            buttons: {
                success: {
                    label: "Ok",
                    //className: "btn SuKarne",
                    callback: function() {
                        $('#cmbDetectores').val(0);
                        $('#dvEvaluacionAnterior').addClass('hidden');
                        $('#btnGuardar').prop("disabled", true);
                    }
                },
            }
        });

        retValue = false;

    } else if (retValue == true) {
       
        MostrarUltimaEvaluacion();
    }

    return retValue;

};

//Mustra la ultima evaluacion del detector
MostrarUltimaEvaluacion = function() {

    var ultimaEvaluacion = evaluacionesAnteriores[evaluacionesAnteriores.length - 1];
    var criterio;
    
    var total = 0;

    for (var res in ultimaEvaluacion.Respuestas) {
        total += ultimaEvaluacion.Respuestas[res].Respuesta;
    }

    for (var ele in criterios) {
        if (criterios[ele].CriterioSupervisionId == ultimaEvaluacion.CriterioSupervisionId) {
            criterio = criterios[ele];
        }
    }

    if (criterio != null && ultimaEvaluacion.CriterioSupervisionId != 3) {
       

        $('#evalSemaforo').css('background-color', criterio.CodigoColor);
        $('#evalFecha').text('Fecha Evaluación: ' + ultimaEvaluacion.FechaSupervisionString);
        $('#evalResultado').text('Resultado Anterior: ' + total);

        $('#dvEvaluacionAnterior').removeClass('hidden');
    }
    


};

/*
 * Hailita el boton guardar
 */
HabilitarGuardado = function () {
    $('#btnGuardar').prop("disabled", false);
};


/*
 * Guarda el resultado de la evaluacion
 */
GuardarEvaluacion = function() {

    var respuesta = {};

    var elementos = new Array();
    var preguntasEnviar = new Array(); //arreglo auxiliar a enviar por el formato de datos
    for (var ele in Preguntas) {
        //detectamos los seleccionados con el campo estatusid
        if (Preguntas[ele].Activo == true) {
            elementos.push(Preguntas[ele].PreguntaID);
        }

        var pr = {};
        pr.PreguntaID = Preguntas[ele].PreguntaID;
        pr.Valor = Preguntas[ele].Valor;
        pr.Activo = Preguntas[ele].Activo;

        preguntasEnviar.push(pr);
    }

    //Validamos que haya seleccionado al menos una pregunta
    if (elementos.length == 0) {
        MostrarNoSeleccionoPregunta();
        return false;
    }

    respuesta.OperadorID = $('#cmbDetectores').val();
    respuesta.FechaSupervision = new Date;
    respuesta.CriterioSupervisionId = criterioAplicadoId;
    respuesta.Observaciones = $('#txtObservaciones').val();
    respuesta.Activo = 1;

    respuesta.Preguntas = preguntasEnviar; //se envian todas las preguntas de la supervision marcadas como Activo las seleccionadas
   
    $.ajax({
        type: "POST",
        url: "SupervisionTecnicaDetectores.aspx/GuardarSupervision",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(respuesta),
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == true) {
                MostrarGuardadoExito(data);
            } else {
                MostrarGuardadoFallido(data.d.message);
                
            }
        },
        error: function (request) {
            MostrarGuardadoFallido(request);
           
        }
    });

};

/*
 * Obtiene el listado de preguntas de la evaluacion
 */
ObtenerCriteriosEvaluacion = function () {

    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: "SupervisionTecnicaDetectores.aspx/ObtenerCriteriosEvaluacion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            MostrarFalloCargarDatosCriterios();
        },
        success: function (data) {
            if (data.d == null) {
                MostrarNoHayDatosCriterios();
                return;
            }

            criterios = data.d;
           
        }
    });


};

/*
 * Obtiene la informacion del periodo de evaluacion de la configuracion del sistema
 */
ObtenerConfiguracionPeriodo = function () {

    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: "SupervisionTecnicaDetectores.aspx/ObtenerConfiguracionPeriodo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            MostrarFalloCargarDatosCriterios();
        },
        success: function (data) {
            if (data.d == null) {
                MostrarNoHayConfiguracionPerido();
                return;
            }

            configuracionPeriodo = data.d;

        }
    });


};

/*
 * Obtiene el listado de preguntas de la evaluacion
 */
ObtenerSupervisionesAnteriores = function (operadorId) {

    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: "SupervisionTecnicaDetectores.aspx/ObtenerEvaluacionesAnteriores",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: SerialiceJSON(operadorId),
        error: function (request) {
            MostrarFalloCargarDatosSupervisionesAnteriores();
        },
        success: function (data) {

            evaluacionesAnteriores = data.d;

            if (evaluacionesAnteriores != null) {
                if (ValidarSupervision() == true) {
                    HabilitarGuardado();

                }
            } else {
                HabilitarGuardado(); //habilitamos el guardado por que no tiene datos
            }

        }
    });


};

/*
 * Obtiene el listado de preguntas de la evaluacion
 */
ObtenerCuestionarioPreguntas = function () {

    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: "SupervisionTecnicaDetectores.aspx/ObtenerPreguntas",
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
            respuesta.Preguntas = data.d;
            respuesta.Recursos = LlenarRecursos();
            Preguntas = respuesta.Preguntas;
            var orden = 0;
            for (var ele in Preguntas) {
                orden++;
                Preguntas[ele].TipoDato = orden; //determinamos como seleccionado el elemento
            }



            if (data.d.EsValido == false) {
                AgregaElementosTabla(respuesta);

            } else {
                AgregaElementosTabla(respuesta);

            }
        }
    });

    
};

/*
 * Marca el valor seleccionado
 */
MarcarValor = function (id) {
    var control = $('#chk_' + id);
    incidencia = false;

    if (control.length == 1) {
        var valor = parseInt($('#chk_' + id).attr('data-valor'));
        if ($('#chk_' + id).attr('Checked') == 'checked') {

            $('#chk_' + id).attr('Checked', 'True');
            total += valor;

            for (var ele in Preguntas) {
                if (Preguntas[ele].PreguntaID == id) {
                    Preguntas[ele].Activo = true; //determinamos como seleccionado el elemento
                    break;
                }
            }
        } else {

            for (var ele in Preguntas) {
                if (Preguntas[ele].PreguntaID == id) {

                    Preguntas[ele].Activo = false; //determinamos como seleccionado el elemento
                    break;
                }
            }
            total -= valor;
        }

        $('#txtTotal').val(total);

        ActualizarCriterio(total);
    }
};

/*
 * Actualiza los criterios en la interfaz
 */
ActualizarCriterio = function(valor) {
    var ultimaEvaluacion = null;
    
    //Buscamos incidencias de la ultima evaluacion
    if (evaluacionesAnteriores != null)
        ultimaEvaluacion = evaluacionesAnteriores[evaluacionesAnteriores.length - 1];

    if (ultimaEvaluacion != null) {
        //buscamos en las respuestas de la evaluacion la coincidencia de incidencias
        //Recorremos las preguntas
        for (var pr in Preguntas) {
            if (Preguntas[pr].Activo == false) { //Buscamos incidencias repetidas
                for (var val in ultimaEvaluacion.Respuestas) {
                    if (Preguntas[pr].PreguntaID == ultimaEvaluacion.Respuestas[val].PreguntaId && ultimaEvaluacion.Respuestas[val].Respuesta == 0) {
                        //Es la misma pregunta que no esta seleccionada en la tabla, sumado a que la respuesta es 0 que no fue seleccionada en el anterior
                        incidencia = true;
                        break;
                    }
                }
            }
            if (incidencia) break;

        }
    }
    //se establece el criterio no apto cuando tiene repetida una misma incidencia
    if (incidencia == true) {

        for (var eles in criterios) {
            if (criterios[eles].CriterioSupervisionId == 1) {
                //actualizamos la informacion de criterio
                criterioAplicadoId = criterios[eles].CriterioSupervisionId;
                $('#txtDescripcionCriterio').text(criterios[eles].Descripcion);
                $('#txtSemaforo').css('background-color', criterios[eles].CodigoColor);
                break;
            }
        }
    } else {
        //se aplica la evaluacion de criterios normal

        for (var ele in criterios) {
            if (valor >= criterios[ele].ValorInicial && valor <= criterios[ele].ValorFinal) {
                //actualizamos la informacion de criterio
                criterioAplicadoId = criterios[ele].CriterioSupervisionId;
                $('#txtDescripcionCriterio').text(criterios[ele].Descripcion);
                $('#txtSemaforo').css('background-color', criterios[ele].CodigoColor);

                break;
            }
        }
    }
};
/*
 * Recargamos la pagina para limpiar los valores
 */
GoEvaluacion = function() {
    document.location.href = 'SupervisionTecnicaDetectores.aspx';
};

/*
 * Muestra el mensaje cuando no existe informacion de la configuracion del periodo
 */
MostrarNoHayConfiguracionPerido = function () {
    bootbox.dialog({
        message: msgMostrarNoHayConfiguracionPerido,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            },
        }
    });
};

/*
 * Muestra el mensaje de fallo al cargar datos de supervisiones anteriores
 */
MostrarFalloCargarDatosSupervisionesAnteriores = function () {
    bootbox.dialog({
        message: msgFalloCargarDatosSupervisionesAnteriores,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            },
        }
    });
};

/*
 * Funcion muestra el dialogo de mensaje de guardar fallido
 */
MostrarGuardadoFallido = function (mensaje) {
    bootbox.dialog({
        message: msgFalloAlGuardar + ' ' + mensaje,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            },
        }
    });
};

/*
 * Mostrar guardado de exito
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
                callback: function () {
                    GoEvaluacion();
                }
            }
        }
    });
};

/*
 * Mostrar mensaje que no hay detectores
 */
MostrarNoHayDetectores = function (){
    bootbox.dialog({
        message: msgNoHayDetectores,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });
};

/*
 * Funcion que muestra elde que no existen datos de criterios configurados
 */
MostrarNoHayDatosCriterios = function () {
    bootbox.dialog({
        message: msgNoExistenCriterios,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });
};

/*
 * Mostrar mensaje que no selecciono preguntas
 */
MostrarNoSeleccionoPregunta = function() {
    bootbox.dialog({
        message: msgNoSeleccionoPregunta,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });

};

/*
 * Funcion que muestra el mensaje fallo al cargar los criterios
 */
MostrarFalloCargarDatosCriterios = function () {

    bootbox.dialog({
        message: msgFalloCargarCriterios,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });
};

/*
 * Funcion que muestra el mensaje de que no existen datos de preguntas
 */
MostrarNoHayDatos = function () {
    bootbox.dialog({
        message: msgNoCargaronPreguntas,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });
};

/*
 * Funcion que muestra el mensaje de privilegios insuficientes
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
 * Agrega los elementos a la tabla de datos principal
 */
AgregaElementosTabla = function (datos) {
    if (datos != null) {
        $('#TablaPreguntas').setTemplateURL('../Templates/GridSupervicionTecnica.html');
        $('#TablaPreguntas').processTemplate(datos);
    } else {
        $('#TablaPreguntas').html("");
    }
};

/*
 * Almancena los recursos para ser usados en el template
 */
LlenarRecursos = function () {
    Resources = {};
    return Resources;
};

/*
 * Funcion para validar letras y numeros
 */
function solo_letras(event) {

    var keychar = String.fromCharCode(event);

    var charCode = (event.which) ? event.which : 0;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || charCode == 44 || charCode == 46 || charCode == 32 || charCode == 8 || charCode == 0 || charCode == 241 || charCode == 209 || charCode == 193 || charCode == 201 || charCode == 205 || charCode == 211 || charCode == 218 || charCode == 225 || charCode == 233 || charCode == 237 || charCode == 243 || charCode == 250)
        return true;

    else {

        return false;
    }


}
//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
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

/*
 * Funcion para obtener el numero de semana
 */
function obtenerSemanaAnio(d) {
    
    d = new Date(+d);
    d.setHours(0, 0, 0);
    d.setDate(d.getDate() + 4 - (d.getDay() || 7));
    var anioInicio = new Date(d.getFullYear(), 0, 1);
    var semana = Math.ceil((((d - anioInicio) / 86400000) + 1) / 7);
    return [d.getFullYear(), semana];
}