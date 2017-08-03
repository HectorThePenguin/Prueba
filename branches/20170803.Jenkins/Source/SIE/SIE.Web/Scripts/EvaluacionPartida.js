/// <reference path="../Sanidad/EvaluacionPartida.aspx" />
/// <reference path="~/assets/plugins/jquery-1.7.1.js" />
/// <reference path="../Plugins/js/select2.min.js" />

//-------------------------------------------------------------------
// Funcion para leer los datos del grid y ponerlos en la pantalla principal
//-------------------------------------------------------------------
var rutaPantalla = location.pathname;

ValidarDatos = function () {
    var table, tbody, i, rowLen, row, cell;
    table = document.getElementById("gvBusqueda");
    tbody = table.tBodies[0];
    var kilosLlegada = 0;
    var kgsOrigen = 0;
    var hrs = 0;
    var seleccionados = 0;
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        row = tbody.rows[i];
        cell = row.cells[1];
        if (cell == undefined) {
            break;
        }
        var index = i - 1;
        var radioseleccionado = "gvBusqueda_radioCorral_".concat(index.toString());
        
        if (document.getElementById(radioseleccionado).checked) {
            seleccionados = seleccionados + 1;
            var corralId = "gvBusqueda_CorralID_".concat(index.toString());
            var pesoBruto = "gvBusqueda_PesoBruto_".concat(index.toString());
            var fechaSalida = "gvBusqueda_FechaSalida_".concat(index.toString());
            var horas = "gvBusqueda_Horas_".concat(index.toString());
            var horaLlegada = "gvBusqueda_HoraLlegada_".concat(index.toString());
            var loteID = "gvBusqueda_LoteID_".concat(index.toString());

            $('#hfLoteID').val(document.getElementById(loteID).value);
            $('#corralId').val(document.getElementById(corralId).value);
            $('#kgsOrigen').val(document.getElementById(pesoBruto).value);
            $('#fechaSalida').val(document.getElementById(fechaSalida).value);
            $('#Horas').val(document.getElementById(horas).value);
            $('#txtCorral').val(row.cells[2].innerHTML);
            $('#txtLote').val(row.cells[3].innerHTML);

            $('#txtFechaLlegada').val(row.cells[6].innerHTML);
            $('#txtInventarioInicial').val(row.cells[4].innerHTML);
            $('#txtInvFinal').val(row.cells[4].innerHTML);
            hrs = document.getElementById(horas).value;
            $('#txtCorral').attr("disabled", true);

            $('#txtHoraLlegada').val(document.getElementById(horaLlegada).value);
            
        }
    }
    if (seleccionados>0){
        ObtenerCorralesSinEvaluar(kgsOrigen, kilosLlegada, hrs);
        $('#responsive').modal('hide');
    }
    else
    {
        bootbox.alert(msgSeleccioneFolio);
    }
};

//-------------------------------------------------------------------
// Funcion para leer los datos del grid y ponerlos en la pantalla principal
//-------------------------------------------------------------------
ValidarDatosCorral = function (varCorral) {
 
    var table, tbody, i, rowLen, row, cell;
    table = document.getElementById("gvBusqueda");
    tbody = table.tBodies[0];
    var kilosLlegada = 0;
    var kgsOrigen = 0;
    var hrs = 0;
    var encontrado = 0;
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        row = tbody.rows[i];
        cell = row.cells[1];
        if (cell == undefined) {
            break;
        }
        var index = i - 1;

        if (row.cells[2].innerHTML.trim() == varCorral.trim()) {
            var pesoBruto = "gvBusqueda_PesoBruto_".concat(index.toString());
            var fechaSalida = "gvBusqueda_FechaSalida_".concat(index.toString());
            var horas = "gvBusqueda_Horas_".concat(index.toString());
            var corralId = "gvBusqueda_CorralID_".concat(index.toString());
            var loteID = "gvBusqueda_LoteID_".concat(index.toString());
            
            $('#hfLoteID').val(document.getElementById(loteID).value);
            $('#corralId').val(document.getElementById(corralId).value);
            $('#kgsOrigen').val(document.getElementById(pesoBruto).value);
            $('#fechaSalida').val(document.getElementById(fechaSalida).value);
            $('#Horas').val(document.getElementById(horas).value);
            $('#txtCorral').val(row.cells[2].innerHTML);
            $('#txtLote').val(row.cells[3].innerHTML);
            $('#txtFechaLlegada').val(row.cells[6].innerHTML);
            $('#txtInventarioInicial').val(row.cells[4].innerHTML);
            $('#txtInvFinal').val(row.cells[4].innerHTML);
            hrs = document.getElementById(horas).value;
            $('#txtCorral').attr("disabled", true);
            ObtenerCorralesSinEvaluar(kgsOrigen, kilosLlegada, hrs);

            $('#txtMermaVisible').val(merma.toFixed(2));
            i = tbody.rows.length + 1;
            encontrado = 1;
        }
    }
    
    if (encontrado == 0) {
        
        bootbox.alert(msgSinCorral);
    }
    
};
//-------------------------------------------------------------------
// Funcion para traer todos los corrales sin evaluar y tomar las partidas que
// le correspondan al id del corral seleccionada en la busqueda.
//-------------------------------------------------------------------
ObtenerCorralesSinEvaluar = function (kgsOrigen, kilosLlegada, hrs) {
    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/ObtenerCorralesSinEvaluar",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
           
            if (data.d.EsValido == false) {
                var msgError = data.d.Mensaje;
                bootbox.alert(msgError);
            } else {
                var rowLen;
                var x;
                for (x = 0, rowLen = data.d.Datos.length; x < rowLen; x++) {
                    if (parseInt($('#corralId').val()) == parseInt(data.d.Datos[x].CorralID)) {
                        if ($('#txtPartidas').val() == "") {
                            $('#txtPartidas').val(data.d.Datos[x].FolioEntrada);
                        } else {
                            var partidas = $('#txtPartidas').val().concat(",").concat(data.d.Datos[x].FolioEntrada);
                            $('#txtPartidas').val(partidas);
                        }
                        if ($('#txtCabezas').val() == "") {
                            $('#txtCabezas').val(data.d.Datos[x].CabezasRecibidas);
                            $('#txtInventarioInicial').val(data.d.Datos[x].CabezasRecibidas);
                            $('#txtInvFinal').val(data.d.Datos[x].CabezasRecibidas);
                        } else {
                            var cabezas = parseInt($('#txtCabezas').val()) + parseInt(data.d.Datos[x].CabezasRecibidas);
                            $('#txtCabezas').val(cabezas);
                            $('#txtInventarioInicial').val(cabezas);
                            $('#txtInvFinal').val(cabezas);
                        }
                        if ($('#txtProcedencia').val() == "") {
                            $('#txtProcedencia').val(data.d.Datos[x].OrganizacionOrigen);
                        } else {
                            var procedencia = $('#txtProcedencia').val().concat(",").concat(data.d.Datos[x].OrganizacionOrigen);
                            $('#txtProcedencia').val(procedencia);
                        }
                        kgsOrigen = kgsOrigen + data.d.Datos[x].PesoOrigen;
                        
                        kilosLlegada = kilosLlegada + data.d.Datos[x].PesoLlegada;
                    }
                    obtenerFecha();
                    obtenerHora();
                }
                
                $('#txtPesoLlegada').val(kilosLlegada.toFixed(2));
                
                var merma = ((kgsOrigen - kilosLlegada) / kgsOrigen) * 100;
                var pesoPromedio = kgsOrigen / $('#txtCabezas').val();
                $('#txtMerma').val(merma);
                $('#txtMermaVisible').val(merma.toFixed(2));
                $('#txtPesoPromedio').val(pesoPromedio);
                $('#txtHoras').val(hrs);
                validarPreguntas(merma, pesoPromedio, hrs);
            }
        }
    });
};
ObtenerRegistroGuardar = function (fila) {
    var Elemento = {};
    Elemento.PreguntaID = $(fila).attr("data-PreguntaID");
    Elemento.Activo = parseBool($(fila).attr("data-Activo"));
    Elemento.Valor = $(fila).attr("data-Valor");
    Elemento.Descripcion = $('.descripcion', $(fila)).text();
    return Elemento;
};
function parseBool(value) {
    return (typeof value === "undefined") ?
           false :
           // trim using jQuery.trim()'s source 
           value.replace(/^\s+|\s+$/g, "").toLowerCase() === "true";
}
//-------------------------------------------------------------------
// Funcion para refrescar el grid y poner checkiados las preguntas de acuerdo a su formula:
// merma > 9.5 , pesoPromedio < 250 , horas > 17
//-------------------------------------------------------------------
validarPreguntas = function (merma, pesoPromedio, horas) {
    var arregloElementos = new Array();
    var i = 0;
    var contador = 0;

    $('#TablaPreguntas tbody tr').each(function () {
        arregloElementos[i] = ObtenerRegistroGuardar(this);

        if (i == 2 && merma > parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (i == 3 && pesoPromedio < parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (i == 4 && horas > parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (arregloElementos[i].Activo == true) {
            contador++;
        }
        if (contador >= 4) {
            $('#rdbMetafilaxia')[0].checked = true;
            $('#rdbNormal')[0].checked = false;
            $('#txtJustificacion').val('');
            //$('#txtJustificacion').attr("disabled", true);
            $('#rdnMetafilaxiaAutorizada')[0].checked = false;
            $('#rdnMetafilaxiaAutorizada').attr("disabled", true);
        }
        else {
            $('#rdbNormal')[0].checked = true;
            $('#rdbMetafilaxia')[0].checked = false;
            $('#txtJustificacion').val('');
            //$('#txtJustificacion').attr("disabled", false);
            $('#rdnMetafilaxiaAutorizada')[0].checked = false;
            $('#rdnMetafilaxiaAutorizada').attr("disabled", false);
        }

        i++;
    });
    var respuesta = {};
    respuesta.Arreglo = arregloElementos;
    respuesta.Resources = RecursosBindPreguntas(contador);
    if (respuesta.Arreglo.length > 0) {
        $('#TablaPreguntas').setTemplateURL('../Templates/PreguntasEvaluacion.htm');
        $('#TablaPreguntas').processTemplate(respuesta);
    } else {
        $('#TablaPreguntas').html("");
    }
    //$('#txtInvFinal').focus();
    $('#btnDatosEnfermeria').focus();
};
RecursosBindPreguntas = function (contador) {
    Resources = {};
    Resources.Total = "Total";
    Resources.textoCantidadTotal = contador;
    return Resources;
};
//-------------------------------------------------------------------
// Funcion para refrescar el grid y poner checkiado la pregunta de la temperatura.
// PorcentajeCC3 > 25 o Parametrizado arregloElementos[i].Valor
//-------------------------------------------------------------------
validarPreguntasPorcentaje = function () {
    var arregloElementos = new Array();
    var i = 0;
    var contador = 0;
    $('#TablaPreguntas tbody tr').each(function () {
        arregloElementos[i] = ObtenerRegistroGuardar(this);
        
        arregloElementos[i].Activo = false;
        
        if (i == 0 && parseFloat($('#hdnCbzCRB').val()) >= parseInt(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
            contador++;
        }
        if (i == 1 && parseFloat($('#txtPorcentajeCC3').val()) >= parseInt(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (i == 2 && $('#txtMerma').val() > parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (i == 3 && $('#txtPesoPromedio').val() < parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (i == 4 && $('#txtHoras').val() > parseFloat(arregloElementos[i].Valor)) {
            arregloElementos[i].Activo = true;
        }
        if (arregloElementos[i].Activo == true) {
            contador++;
        }
        if (contador >= 4) {
            $('#rdbMetafilaxia')[0].checked = true;
            $('#rdbNormal')[0].checked = false;//
            $('#txtJustificacion').val('');
            $('#rdnMetafilaxiaAutorizada')[0].checked = false;//
            $('#rdnMetafilaxiaAutorizada').attr("disabled", true);
        } else {
            $('#rdbNormal')[0].checked = true;
            $('#rdbMetafilaxia')[0].checked = false;//
            $('#txtJustificacion').val('');
            $('#rdnMetafilaxiaAutorizada')[0].checked = false;//
            $('#rdnMetafilaxiaAutorizada').attr("disabled", false);
        }
        i++;
    });
    var respuesta = {};
    respuesta.Arreglo = arregloElementos;
    respuesta.Resources = RecursosBindPreguntas(contador);
    if (respuesta.Arreglo.length > 0) {
        $('#TablaPreguntas').setTemplateURL('../Templates/PreguntasEvaluacion.htm');
        $('#TablaPreguntas').processTemplate(respuesta);
    } else {
        $('#TablaPreguntas').html("");
    }
};
//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
};
//-------------------------------------------------------------------
// Funcion para validar el guardar los campos obligatorios
//-------------------------------------------------------------------
function ValidarCamposObligatorios() {
    if ($('#txtCorral').val() == "") {
        bootbox.alert(msgCorralNoValido, function() {
            $('#btnBuscar').focus();
        });
        return false;
    }

    if ($('#rdnMetafilaxiaAutorizada')[0].checked) {
        if ($('#txtJustificacion').val().trim() == "") {
            bootbox.alert(msgMetafiaxiaJustificacion, function() {
                $('#txtJustificacion').focus();
            });
            return false;
        }
    }
    return true;
}
//-------------------------------------------------------------------
// Funcion para validar solo numeros.
//-------------------------------------------------------------------
function SoloNumeros(evt) {
    evt = (evt) ? evt : ((window.event) ? event : null);
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
				((evt.which) ? evt.which : 0));
    if (charCode == 13) {
        var elem = (evt.target) ? evt.target :
						((evt.srcElement) ? evt.srcElement : null);
        //setControlsFocus(elem);
        return false;
    }
    if (charCode < 32 || (charCode > 47 && charCode < 58))
        return true;
    return false;
}

function solo_letras(event) {
   
    keychar = String.fromCharCode(event);

    var charCode = (event.which) ? event.which : 0;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || charCode == 44 || charCode == 46 || charCode == 32 || charCode == 8 || charCode == 0 || charCode == 241 || charCode == 209 || charCode == 193 || charCode == 201 || charCode == 205 || charCode == 211 || charCode == 218 || charCode == 225 || charCode == 233 || charCode == 237 || charCode == 243 || charCode == 250)
        return true;

    else {
    
            return false;
        }


}
//-------------------------------------------------------------------
// Funcion para guardar por medio de AJAX
//-------------------------------------------------------------------
GuardarEvaluacion = function () {
   
    var evaluacionCorralInfo = CargarInformacionGuardar();
    var datos = { 'evaluacionCorralInfo': evaluacionCorralInfo };
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/GuardarEvaluacion',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                var msgerror = data.d.Mensaje;
                if (msgerror == "ErrorGuardar") {
                    bootbox.alert(msgErrorGuardar);
                } else {
                    bootbox.alert(msgerror);
                }
            } else {
                $('#msgGuardadoModal').modal('show');
            }
        },
        error: function (request) {
            //alert(request.Message);
        }
    });
};


//Función para cargar la información de las evaluaciones que se van a Guardar.
CargarInformacionGuardar = function () {
    var evaluacionCorralInfo = {};

    var corralInfo = {};
    var loteInfo = {};
    
    corralInfo.CorralID = $('#corralId').val();
    loteInfo.LoteID = $('#hfLoteID').val();
    evaluacionCorralInfo.Corral = corralInfo;
    evaluacionCorralInfo.Lote = loteInfo;
    evaluacionCorralInfo.Cabezas = $('#txtCabezas').val();

    evaluacionCorralInfo.NivelGarrapata = $('#txtGarrapata').val();
    
    if ($('#rdbMetafilaxia')[0].checked) {
        evaluacionCorralInfo.EsMetafilaxia = true;
    } else {
        evaluacionCorralInfo.EsMetafilaxia = false;
    }
    if ($('#rdnMetafilaxiaAutorizada')[0].checked) {
        evaluacionCorralInfo.MetafilaxiaAutorizada = true;
    } else {
        evaluacionCorralInfo.MetafilaxiaAutorizada = false;
    }
    evaluacionCorralInfo.Justificacion = $('#txtJustificacion').val();
    
    evaluacionCorralInfo.PreguntasEvaluacionCorral = ObtenerPreguntasGuardar();

    return evaluacionCorralInfo;
};


//-------------------------------------------------------------------
// Funcion para consultar el corral por medio de AJAX
//-------------------------------------------------------------------
function ValidarCodigoCorral() {
 
    var parametros = {};
    if ($('#txtCorral').val() == "") {
        bootbox.alert(msgCorralNoValido);
        return false;
    }
    parametros.CodigoCorral = $('#txtCorral').val();
    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/ValidarCodigoCorral",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(parametros),
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                bootbox.alert(msgCorralNoExiste);
                return false;
            } else {
                GuardarEvaluacion();
                return true;
            }
        }
    });
    return true;
}

//-------------------------------------------------------------------
// Funcion para obtener la fecha acual por medio de AJAX
//-------------------------------------------------------------------
function obtenerFecha() {
  

    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/ObtenerFecha",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            $('#txtFechaEvaluacion').val(data.d);
        }
    });
    return true;
}

//-------------------------------------------------------------------
// Funcion para obtener la fecha acual por medio de AJAX
//-------------------------------------------------------------------
function obtenerHora() {
    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/obtenerHora",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            $('#txtHrEvalacion').val(data.d);
        }
    });
    return true;
}
//-------------------------------------------------------------------
// Funcion para reclacular los datos de enfermeria, popup.
//-------------------------------------------------------------------
function RecalcularPreguntas() {
    var tasaMortalidad = 0;
    var tasaCC3 = 0;
    var nCbztmt = 0;
    var nCbzngp = 0;
    var nCbzDigestivos = 0;

    var nCbzCc3 = 0;
    var nCbzCc2 = 0;
    var nCbzCc1 = 0;
    var nCbzMayor3 = 0;
    var nEnferemosG1 = 0;
    var nEnferemosG2 = 0;
    var nEnferemosG3 = 0;
    var nEnferemosG4 = 0;
    var preguntaIdEnfermosG1 = 0;
    var table, tbody, i, rowLen, row, cell;
    cbzTotaLCapturadas = 0;
    var respuesta = {};
    var arregloElementos = new Array();
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    var contador = 0;
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        row = tbody.rows[i];
        cell = row.cells[1];
        var index = i - 1;
        var tmpConcepto;
        var preguntaGrado1 = "dgvDatosEnfermeria_txtResultado_5";
        var valorPregunta = "dgvDatosEnfermeria_txtResultado_".concat(index.toString());
        var valorActivo = "dgvDatosEnfermeria_Activo_".concat(index.toString());
        var preguntaId = "dgvDatosEnfermeria_PreguntaID_".concat(index.toString());

        if (isInteger(document.getElementById(valorPregunta).value) == true ||
            (document.getElementById(valorActivo).value == "True")) {

            if (row.cells[0].innerHTML.search("cbz CC3") != -1) {
                nCbzCc3 = document.getElementById(valorPregunta).value;
            } else if (row.cells[0].innerHTML.search("cbz CC2") != -1) {
                nCbzCc2 = document.getElementById(valorPregunta).value;
            } else if (row.cells[0].innerHTML.search("cbz CC1") != -1) {
                nCbzCc1 = document.getElementById(valorPregunta).value;
            } else if (row.cells[0].innerHTML.search("CC3 Mayor") != -1) {
                nCbzMayor3 = parseInt($('#txtInvFinal').val()) - parseInt(nCbzCc3) - parseInt(nCbzCc2) - parseInt(nCbzCc1);
                document.getElementById(valorPregunta).value = nCbzMayor3;
            } else if (row.cells[0].innerHTML.search("% CC3") != -1) {
                var sumaCalculo = 0;
                sumaCalculo = parseInt(nCbzCc3) + parseInt(nCbzCc2) + parseInt(nCbzCc1);
                tasaCC3 = (sumaCalculo / parseInt($('#txtInvFinal').val())) * 100;
                document.getElementById(valorPregunta).value = tasaCC3.toFixed(2);

                document.getElementById(valorPregunta).value.concat('%');
                $('#txtPorcentajeCC3').val(document.getElementById(valorPregunta).value);
                document.getElementById(valorPregunta).value = document.getElementById(valorPregunta).value.concat('%');
            } else if (row.cells[0].innerHTML.search("Grado 1") != -1) {
                preguntaIdEnfermosG1 = document.getElementById(preguntaId).value;
            } else if (row.cells[0].innerHTML.search("Grado 2") != -1) {
                nEnferemosG2 = document.getElementById(valorPregunta).value;
                var sumaCalculo = 0;
                sumaCalculo = parseInt(nEnferemosG2) + parseInt(nEnferemosG3);
                $('#hdnCbzCRB').val((sumaCalculo / parseInt($('#txtInvFinal').val())) * 100);
            } else if (row.cells[0].innerHTML.search("Grado 3") != -1) {
                nEnferemosG3 = document.getElementById(valorPregunta).value;
                $('#txtInvFinal').val(
                                        parseInt($('#txtInventarioInicial').val()) -
                                        parseInt(nEnferemosG3) -
                                        parseInt(nCbzDigestivos)
                                     );
                var sumaCalculo = 0;
                sumaCalculo = parseInt(nEnferemosG2) + parseInt(nEnferemosG3);
                $('#hdnCbzCRB').val((sumaCalculo / parseInt($('#txtInvFinal').val())) * 100);
            } else if (row.cells[0].innerHTML.search("Grado 4") != -1) {
                nEnferemosG4 = document.getElementById(valorPregunta).value;
                var preguntaEnfermosG1 = ObtenerDatosPorId(preguntaIdEnfermosG1, respuesta.Arreglo);
                nEnferemosG1 = parseInt($('#txtInvFinal').val()) - (parseInt(nEnferemosG2) + parseInt(nEnferemosG3) + parseInt(nEnferemosG4));
                if (preguntaEnfermosG1 >= 0) {
                    respuesta.Arreglo[preguntaEnfermosG1].value = nEnferemosG1;
                    document.getElementById(preguntaGrado1).value = nEnferemosG1;
                }
            } else if (row.cells[0].innerHTML.search("Morbilidad") != -1) {
                nEnferemosG1 = parseInt($('#txtInvFinal').val()) - nEnferemosG2 - nEnferemosG3 - nEnferemosG4;
                tasaMortalidad = ((
                                    parseInt(nEnferemosG2) +
                                    parseInt(nEnferemosG3) +
                                    parseInt(nEnferemosG4)
                                   ) / parseInt($('#txtInvFinal').val())
                                  ) * 100;
                document.getElementById(valorPregunta).value = tasaMortalidad.toFixed(2);
                document.getElementById(valorPregunta).value = document.getElementById(valorPregunta).value.concat('%');
            } else if (row.cells[0].innerHTML.search("Traumatismo") != -1) {
                nCbztmt = parseInt(document.getElementById(valorPregunta).value);
            } else if (row.cells[0].innerHTML.search("Postrados") != -1) {
                nCbzngp = parseInt(document.getElementById(valorPregunta).value);
            } else if (row.cells[0].innerHTML.search("Digestivos") != -1) {
                nCbzDigestivos = parseInt(document.getElementById(valorPregunta).value);
                $('#txtInvFinal').val(
                                        parseInt($('#txtInventarioInicial').val()) -
                                        parseInt(nEnferemosG3) -
                                        parseInt(nCbzDigestivos)
                                     );
            }
            
            $('#hdnCbzCcTotaL').val(parseInt(nCbzMayor3));

            $('#hdnCbzEnfermosGTotaL').val(parseInt(nEnferemosG1));
            
            $('#hdnCbzMorbilidad').val(parseInt(nCbzngp) + parseInt(nCbztmt));

            respuesta.Arreglo = ObtenerRegistros(document.getElementById(valorPregunta).value,
                document.getElementById(valorActivo).value, row.cells[0].innerHTML,
                document.getElementById(preguntaId).value, contador, arregloElementos);
            contador++;
        }
    }
}
ObtenerRegistros = function (txtValorcaja, activo, descripcion, preguntaid, index, arregloElementos) {

    arregloElementos[index] = ObtenerDatos(txtValorcaja, activo, descripcion, preguntaid, index);
    return arregloElementos;
};
ObtenerDatos = function (txtValorcaja, activo, descripcion, preguntaid, index) {
    var elemento = {};
    elemento.Valorcaja = txtValorcaja;
    elemento.Activo = activo;
    elemento.Descripcion = descripcion;
    elemento.Preguntaid = preguntaid;
    elemento.Posicion = index;
    return elemento;
};
//-------------------------------------------------------------------
// Funcion para validar que se haya capturado todos los datos de enfermeria
//-------------------------------------------------------------------
function ValidarPreguntasEnfermeria() {
    var table, tbody, i, rowLen, row;
    var respuesta = {};
    var arregloElementos = new Array();
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    var contador = 0;
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        var index = i - 1;

        var valorPregunta = "dgvDatosEnfermeria_txtResultado_".concat(index.toString());
        var valorActivo = "dgvDatosEnfermeria_Activo_".concat(index.toString());
        var preguntaId = "dgvDatosEnfermeria_PreguntaID_".concat(index.toString());
        respuesta.PreguntaID = document.getElementById(preguntaId).value;
        respuesta.Respuesta = document.getElementById(valorPregunta).value;
        respuesta.Activo = document.getElementById(valorActivo).value;
        if (respuesta.Respuesta == "") {
            bootbox.alert(msgCapturarDatosPreguntas, function() {
                
                $('#Enfermeria').modal('show');
            });
            $('#txtGuardar').val("0");
            return null;
        }
        respuesta.FechaModificacion = $('#txtFechaEvaluacion').val();
        respuesta.Arreglo = ObtenerRegistros(respuesta.Respuesta, respuesta.Activo, "",
            respuesta.PreguntaID, contador, arregloElementos);
        contador++;
        $('#txtGuardar').val("1");
    }
    return respuesta;
}
ObtenerDatosPorId = function (id, arreglo) {
    var index = 0;
    var resultado = 0;
    jQuery.each(arreglo, function () {
        if (arreglo[index].Preguntaid == id) {
            resultado = arreglo[index].Posicion;
        }
        index++;
    });
    return resultado;
};
//-------------------------------------------------------------------
// isBlank(value)
//   Returns true if value only contains spaces
//-------------------------------------------------------------------
function isBlank(val) {
    if (val == null) { return true; }
    for (var i = 0; i < val.length; i++) {
        if ((val.charAt(i) != ' ') && (val.charAt(i) != "\t") && (val.charAt(i) != "\n") && (val.charAt(i) != "\r")) {
            return false;
        }
    }
    return true;
}
//-------------------------------------------------------------------
// isDigit(value)
//   Returns true if value is a 1-character digit
//-------------------------------------------------------------------
function isDigit(num) {
    if (num.length > 1) { return false; }
    var string = "1234567890";
    if (string.indexOf(num) != -1) { return true; }
    return false;
}
//-------------------------------------------------------------------
// isInteger(value)
//   Returns true if value contains all digits
//-------------------------------------------------------------------
function isInteger(val) {
    if (isBlank(val)) { return false; }
    for (var i = 0; i < val.length; i++) {
        if (!isDigit(val.charAt(i))) { return false; }
    }
    return true;
}
//-------------------------------------------------------------------
// Funcion para obtener las preguntas checkiadas para guardar.
//-------------------------------------------------------------------
function ObtenerPreguntasCheckiadas(arregloElementos) {
    var i = 0;
    var respuesta = {};
    var arregloElementosPreguntas = new Array();
    $('#TablaPreguntas tbody tr').each(function () {
        arregloElementosPreguntas[i] = ObtenerRegistroGuardar(this);
        respuesta.PreguntaID = arregloElementosPreguntas[i].PreguntaID;
        if (arregloElementosPreguntas[i].Activo == true) {
            respuesta.Respuesta = "1";
        } else {
            respuesta.Respuesta = "0";
        }
        respuesta = ObtenerRegistrosPreguntas( respuesta.PreguntaID,
           respuesta.Respuesta, arregloElementos.length,arregloElementos);
        i++;
    });
    return respuesta;
}

ObtenerRegistrosPreguntas = function ( preguntaId, respuesta,
    index, arregloElementos) {

    arregloElementos[index] = ObtenerDatosPreguntas( preguntaId,
        respuesta);
    return arregloElementos;
};
ObtenerDatosPreguntas = function ( preguntaId, respuesta) {
    var elemento = {};
    elemento.PreguntaID = preguntaId;
    //elemento.UsuarioModificacion = usuarioModificacion;
    elemento.Respuesta = respuesta;
    //elemento.UsuarioCreacion = usuarioCreacion;
    return elemento;
};
//-------------------------------------------------------------------
// Funcion para obtener las preguntas y los resultados del popup de enfermeria
//-------------------------------------------------------------------
function ObtenerPreguntasGuardar() {
    var table, tbody, i, rowLen;
    var respuesta = {};
    var arregloElementos = new Array();
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    var contador = 0;
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        var index = i - 1;

        var valorPregunta = "dgvDatosEnfermeria_txtResultado_".concat(index.toString());
        var valorActivo = "dgvDatosEnfermeria_Activo_".concat(index.toString());
        var preguntaId = "dgvDatosEnfermeria_PreguntaID_".concat(index.toString());

        respuesta.PreguntaID = document.getElementById(preguntaId).value;
        respuesta.Respuesta = document.getElementById(valorPregunta).value;
        respuesta.Activo = document.getElementById(valorActivo).value;
        if (respuesta.Respuesta == "") {
            bootbox.alert(msgCapturarDatosPreguntas);
            $('#txtGuardar').val("0");
            return null;
        }

        respuesta.PreguntasEvaluacionCorral = ObtenerRegistrosPreguntas(
            respuesta.PreguntaID, respuesta.Respuesta, contador,
            arregloElementos);
        contador++;
        $('#txtGuardar').val("1");
    }
    var respuestaGuardar = ObtenerPreguntasCheckiadas(arregloElementos);

    return respuestaGuardar;
}



ConsultarPreguntas = function () {
    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/ConsultarPreguntas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            //alert(request.Message);
        },
        success: function (data) {
            if (data.d.EsValido == false) {
                bootbox.alert(msgNoPreguntas);
            } else {
                var respuesta = {};
                respuesta.Arreglo = data.d.Datos;
                respuesta.Resources = LlenarRecursos();
                AgregaElementos(respuesta);
            }
        }
    });
};


ValidarRolUsuario = function () {
    $.ajax({
        type: "POST",
        url: "EvaluacionPartida.aspx/ValidarRolUsuario",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
        },
        success: function (data) {
            if (data.d.EsValido == false) {
                $('#txtEvaluador').focus();
                bootbox.dialog({
                    message: $('#msgErrorRolEvaluador').val(),
                    buttons: {
                        Aceptar: {
                            label: "OK",
                            callback: function() {
                                history.go(-1);
                            }
                        }
                    }
                });
                return false;
            } else {
                return true;
            }
        }
    });
};
RecargarDatos = function () {

    if ($('#txttotalAyuda').val() == "0") {
        bootbox.alert(msgNoHayRegistros);
        return false;
    }
    $('#responsive').modal('show');
 
};
AgregaElementos = function (datos) {
    if (datos.Arreglo.length > 0) {
        $('#TablaPreguntas').setTemplateURL('../Templates/PreguntasEvaluacion.htm');
        $('#TablaPreguntas').processTemplate(datos);
    } else {
        $('#TablaPreguntas').html("");
    }
};
LlenarRecursos = function () {
    Resources = {};
    Resources.Total = "Total";
    Resources.textoCantidadTotal = 0;
    return Resources;
};

maxLengthCheck = function (object) {
    if (object.value.length > object.maxLength) {
        object.value = object.value.slice(0, object.maxLength);
    }
};

limpiarCajas = function () {
    $('#corralId').val('');
    $('#kgsOrigen').val('');
    $('#fechaSalida').val('');
    $('#Horas').val('');
    $('#txtCorral').val('');
    $('#txtLote').val('');
    $('#txtFechaLlegada').val('');
    $('#txtInventarioInicial').val('');
    $('#txtProcedencia').val('');
    $('#txtPartidas').val('');
    $('#txtCabezas').val('');
    $('#txtInvFinal').val('');
    $('#txtJustificacion').attr("readonly", true);
    document.getElementById("idform").reset();
    limpiarEnfermeria();
    ConsultarPreguntas();

};

limpiarEnfermeria = function() {
    /* Limpiar Datos de Enfermeria */
    var table, tbody, i, rowLen;
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        var index = i - 1;
        var valorPregunta = "#dgvDatosEnfermeria_txtResultado_".concat(index.toString());
        /* Se limpian valores del grid de enfermeria */
        $(valorPregunta).val('');
    }
};


function PreguntasCapturadas() {
    var table, tbody, i, rowLen;
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        var index = i - 1;

        var valorPregunta = "dgvDatosEnfermeria_txtResultado_".concat(index.toString());
        var valorActivo = "dgvDatosEnfermeria_Activo_".concat(index.toString());
        
        if (isInteger(document.getElementById(valorPregunta).value) == true ||
           (document.getElementById(valorActivo).value == "True")) {
          if (document.getElementById(valorPregunta).value  != ""){
                return true;    
          }
          
        }
    }
    return false;
}
//-------------------------------------------------------------------
// Inicio de eventos de la pagina
//-------------------------------------------------------------------
$(document).ready(function () {
    var cbzTotaLCapturadas = 0;
    $('#txtCorral').keypress(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //Enter keycode
            ValidarDatosCorral($('#txtCorral').val());
            
            return false;
        }
    });
    
    $('#txtProcedencia').css("resize", "none");
    $('#btnSeleccion').live("click", function () {
        limpiarCajas();
        ValidarDatos();
    });

    $('#rdnMetafilaxiaAutorizada').live("click", function () {
        
        $('#txtJustificacion').attr("readonly", ! $('#rdnMetafilaxiaAutorizada')[0].checked );
        $('#txtJustificacion').focus();
    });
    $('#btnGuardar').live("click", function () {
        if (ValidarCamposObligatorios()) {

            if ($('#txtGuardar').val() == "1") {
               
                    ValidarCodigoCorral();
               
            } else {
                bootbox.alert(msgNoHayDatosEnfermeria, function() {
                    $('#btnDatosEnfermeria').focus();
                });
            }
        }
    });
    

    /* Agregar eventos a las preguntas de enfermeria*/
    var table, tbody, i, rowLen;
    table = document.getElementById("dgvDatosEnfermeria");
    tbody = table.tBodies[0];
    for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
        var index = i - 1;
        var valorPregunta = "#dgvDatosEnfermeria_txtResultado_".concat(index.toString());

        /* Evento para recalcular los valores */
        $(valorPregunta).live("blur", function () {
            RecalcularPreguntas();
        });
        
        /* Evento para permitir solo numeros en la entrada de datos */
        $(valorPregunta).live("keypress", function (event) {
            if (!SoloNumeros(event)) {
                return false;
            }
        });
    }
    
    $('#txtJustificacion').live("keypress", function (event) {
        if (!solo_letras(event)) {
            return false;
        }
    });
    

    $('#btnDatosEnfermeria').live("click", function () {
        if ($('#txtCorral').val() == "") {
            bootbox.alert(msgCorralNoValido, function () {
                $('#btnBuscar').focus();
            });
            return false;
        }
        
        if ($('#txttotalPreguntas').val() == "0") {
            bootbox.alert(msgNoHayPreguntas);
            return false;
        }
        

        $('#Enfermeria').modal('show');
        return true;
    });


    $('#btnGuardarEnfermeria').live("click", function() {
        RecalcularPreguntas(); 
        if ($('#hdnCbzCcTotaL').val() < 0 || $('#hdnCbzEnfermosGTotaL').val() < 0 || parseInt($('#hdnCbzMorbilidad').val()) > parseInt($('#txtInvFinal').val())) {
            bootbox.alert(msgCabezasEnfermeriasMayorInvFinal, function() {
                $('#Enfermeria').modal('show');
            });
            return false;
        }
        
        if ($('#txttotalPreguntas').val() == "0") {
            bootbox.alert(msgNoHayPreguntasGuardar);
            return false;
        }
        if ($('#rdbLeve')[0].checked) {
            $('#txtGarrapata').val('1');
        }
        if ($('#rdbModerado')[0].checked) {
            $('#txtGarrapata').val('2');
        }
        if ($('#rbdGrave')[0].checked) {
            $('#txtGarrapata').val('3');
        }
        var resultado = ValidarPreguntasEnfermeria();
        if (resultado != null) {
            validarPreguntasPorcentaje();
        }

         if ($('#txtGuardar').val() == "1"){         
            bootbox.alert(msgGuardadoExito);
        } 
        return true;
    });

    $('#btnSeleccion').live("click", function () {
        if ($('#txttotalAyuda').val() == "0") {
            bootbox.alert(msgNoSeleccionar);
            return false;
        }
        return true;
    });
    $('#btnBuscar').live("click", function () {
        
        RecargarDatos();
        
        return true;
    });
    if ($('#paginado').val() == "1") {
        $('#paginado').val('0');
        $('#responsive').modal('show');
    }
	$('#btnCancelar').click(function () {
        document.getElementById("idform").reset();
    });
    $('#gvBusqueda').click(function (e) {
        var table, tbody, i, rowLen;
        table = document.getElementById("gvBusqueda");
        tbody = table.tBodies[0];

        for (i = 1, rowLen = tbody.rows.length; i < rowLen; i++) {
            var index = i - 1;
            var radioseleccionado = "gvBusqueda_radioCorral_".concat(index.toString());
            if (document.getElementById(radioseleccionado) == null) {
                break;
            }
            if (document.getElementById(radioseleccionado).checked) {
                document.getElementById(radioseleccionado).checked = false;
            }
        }

        var radioseleccionado = e.target.id;
        if (radioseleccionado != "") {
            document.getElementById(radioseleccionado).checked = true;
        }
    });
    $('#btnCerrar').live("click", function () {
		$('#btnCancelar').click();
    });

    ConsultarPreguntas();
    
    ValidarRolUsuario();

    $('#btnGuardado').click(function () {
        var url = "../Sanidad/EvaluacionPartida.aspx";
        $(location).attr('href', url);
    });
    
    $('#btnCancelarEvaluacion').click(function () {

        bootbox.dialog({
            message: msgSalirSinGuardar,
            title: "Evaluación Partida",
            buttons: {
                success: {
                    label: "Si",
                    className: "btn SuKarne",
                    callback: function () {
                        document.getElementById("idform").reset();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function () {
                        //$('#Enfermeria').modal('show');
                    }
                }
            }
        });
    });
    
    $('#btnCancelarEnfermeria').click(function () {

        if (PreguntasCapturadas()) {
            bootbox.dialog({
                message: msgSalirSinGuardar,
                title: msgDatosEnfermeria,
                buttons: {
                    success: {
                        label: "Si",
                        className: "btn SuKarne",
                        callback: function() {
                            limpiarEnfermeria();
                        }
                    },
                    danger: {
                        label: "No",
                        className: "btn SuKarne",
                        callback: function() {
                            $('#Enfermeria').modal('show');
                        }
                    }
                }
            });
        } else {
            limpiarEnfermeria();
        }        
        
        
    });
    $('#btnBuscar').focus();
});