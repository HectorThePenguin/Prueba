//-------------------------------------------------------------------
// Inicio de eventos de la pagina
//-------------------------------------------------------------------


var banActualizacion;

$(document).ready(function () {
    var esActualizacion = false;
    var oK = true;
    var presionoEnter;
    

    
    $('#txtCodigoCorral').keypress(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //Enter keycode
            presionoEnter = true;
            if ($('#txtCodigoCorral').val() != banActualizacion) {
                ValidarCodigoCorral();
            }
            else {
                $('#txtKgsProgramados').focus();
                $("#txtCodigoCorral").css("background-color", "white");
            }

            return false;
        }
        else {
            presionoEnter = false;
        }
    });


    $('#txtCodigoCorral').focusout(function () {
        if ($('#txtCodigoCorral').val() != "" && presionoEnter == false && $('#txtCodigoCorral').val() != banActualizacion) {
           ValidarCodigoCorral();
        }
        return false;
        
    });
    
    $('#txtComentarios').css("resize","none");
   /* $('#txtKgsProgramados').focusout(function () {
        $("#txtKgsProgramados").css("background-color", "white");
    });*/
    $('#btnImprimir').click(function () {
        var url = "PantallaImpresion.aspx";  
        var parameters = "width=760, height=500,scrollbars=0,toolbar=0,location=0,menubar=0,directories=0,status=0,menubar=0,resizable=0";
        window.open(url, 'Reporte', parameters);
    });
    $("#txtKgsProgramados").keypress(function (event) {
        var code = event.keyCode || event.which;
        if (code == 13) { //Enter keycode
            $("#ddlFormula").focus();
            $("#txtKgsProgramados").css("background-color", "white");
            return false;
        } else {
            return solo_num_ent(event);
        }

    });
    $("#ddlFormula").keypress(function (event) {
        var code = event.keyCode || event.which;
        if (code == 13) { //Enter keycode
            $("#ddlFormula").css("background-color", "white");
            $("#txtComentarios").focus();
            return false;
        }
    });

    $('#btnAgregar').click(function () {

        oK = true;
        $("#txtCodigoCorral").css("background-color", "white");
        $("#ddlFormula").css("background-color", "white");
        $("#txtKgsProgramados").css("background-color", "white");
        
        if ($('#btnAgregar')[0].value == msgActualizar) {
            if (ValidarLongitudPalabra()) {
                bootbox.alert(msgPalabrasInvalidas, function () {
                    $("#txtComentarios").css("background-color", "red");
                    $("#txtComentarios").focus();
                });
                oK = false;
                return false;
            }
            if ($("#txtCodigoCorral").val() == "") {
                bootbox.alert(msgConsultarCorral, function() {
                    $("#txtCodigoCorral").css("background-color", "red");
                    $("#txtCodigoCorral").focus();
                });
                
                oK = false;
                return false;
            }
            if ($("#txtKgsProgramados").val() == "" ) {
                bootbox.alert(msgAgregarKilos, function() {
                    $("#txtKgsProgramados").focus();
                    $("#txtKgsProgramados").css("background-color", "red");
                });
                oK = false;
                return false;
            }
            
            if ( $("#txtKgsProgramados").val() <= 0) {
                bootbox.alert(msgAgregarKilosMayorCero, function () {
                    $("#txtKgsProgramados").focus();
                    $("#txtKgsProgramados").css("background-color", "red");
                });
                oK = false;
                return false;
            }
            

            if ($("#ddlFormula").val() == 0) {
                bootbox.alert(msgSeleccionarFormula, function() {
                    $("#ddlFormula").css("background-color", "red");
                    $("#ddlFormula").focus();
                });
                oK = false;
                return false;
            }
           // if (ValidarExistente($('#txtCodigoCorral').val())) {
                if (oK) {
                    ValidarCodigoCorralParaModificarGrid($("#txtCodigoCorral").val());
                    $("#txtCodigoCorral").css("background-color", "white");
                    $("#ddlFormula").css("background-color", "white");
                    $("#txtKgsProgramados").css("background-color", "white");
                    $("#txtComentarios").css("background-color", "white");
                    $("#txtCodigoCorral").focus();
                    banActualizacion = "xx";
                }
                
           /* } else {
                bootbox.alert(msgCorralAsignado, function () {
                    $("#txtCodigoCorral").focus();
                });
            }*/
            //QUITE CODIGO AQUI
            return false;
        } else {
            if (ValidarLongitudPalabra()) {
                
                bootbox.alert(msgPalabrasInvalidas, function () {
                    $("#txtComentarios").css("background-color", "red");
                    $("#txtComentarios").focus();
                });
                oK = false;
                return false;
            }
            if ($("#txtCodigoCorral").val() == "") {
                bootbox.alert(msgConsultarCorral, function() {
                    $("#txtCodigoCorral").css("background-color", "red");
                    $("#txtCodigoCorral").focus();
                });

                oK = false;
                return false;
            }
            if ($("#txtKgsProgramados").val() == "" ) {
                bootbox.alert(msgAgregarKilos, function() {
                    $("#txtKgsProgramados").css("background-color", "red");
                    $("#txtKgsProgramados").focus();
                });

                oK = false;
                return false;
            }
            if ( $("#txtKgsProgramados").val() <= 0) {
                bootbox.alert(msgAgregarKilosMayorCero, function () {
                    $("#txtKgsProgramados").css("background-color", "red");
                    $("#txtKgsProgramados").focus();
                });

                oK = false;
                return false;
            }

            if ($("#ddlFormula").val() == 0) {
                bootbox.alert(msgSeleccionarFormula, function() {
                    $("#ddlFormula").css("background-color", "red");
                    $("#ddlFormula").focus();
                });

                oK = false;
                return false;
            }
            if (oK) {
           
                var respuesta = {};
                respuesta.Arreglo = $.merge($.merge([], ObtenerDatosAgregar()), ObtenerRegistros());
                respuesta.Resources = LlenarRecursos();
                $("#btnGuardar").attr("disabled", false);
                AgregaElementos(respuesta);
                $("#txtCodigoCorral").attr("disabled", false);
                $("#txtCodigoCorral").val('');
                $("#txtComentarios").val('');
                $("#txtKgsProgramados").val('');
                $("#ddlFormula").val('');
                $("#txtKgsProgramados").attr("disabled", true);
                $("#ddlFormula").attr("disabled", true);
                $("#txtCodigoCorral").focus();
                $("#txtCodigoCorral").css("background-color", "white");
                $("#ddlFormula").css("background-color", "white");
                $("#txtKgsProgramados").css("background-color", "white");
                $('#hdnCorralId').val("");
                $('#hdnCorralIdAnt').val("");
                $("#txtComentarios").css("background-color", "white");
                esActualizacion = false;

            }
            return false;
        }
    });
    $('#btnGuardar').click(function () {
        GuardarInformacionServicioAlimento();
        $("#txtCodigoCorral").css("background-color", "white");
        $("#ddlFormula").css("background-color", "white");
        $("#txtKgsProgramados").css("background-color", "white");
        LimpiarCantroles();
    });
    $("#btnGuardar").attr("disabled", true);
    $('#txtCodigoCorral').focus();
    $('#btnLimpiar').click(function () {
        LimpiarCantroles();
    });
    ObtenerInformacionDiariaAlimento();
});


ValidarLongitudPalabra = function  () {


    var str = $("#txtComentarios").val();
    var res = str.split(" ");
    for (index = 0; index < res.length; ++index) {

        if (res[index].length >= 30) {

            return true;
        }
    }
    return false;
}



LimpiarCantroles = function() {
    $("#txtCodigoCorral").attr("disabled", false);
    $("#txtCodigoCorral").val('');
    $("#txtComentarios").val('');
    $("#txtKgsProgramados").val('');
    $("#ddlFormula").val('');
    $("#txtCodigoCorral").focus();
    esActualizacion = false;
    $('#btnAgregar')[0].value = msgAgregar;
    $("#txtCodigoCorral").attr("disabled", false);
    $("#txtComentarios").attr("disabled", false);
    $("#txtKgsProgramados").attr("disabled", true);
    $("#ddlFormula").attr("disabled", true);
    $("#txtKgsProgramados").css("background-color", "white");
    $("#txtCodigoCorral").css("background-color", "white");
    $("#txtComentarios").css("background-color", "white");
    banActualizacion="xx";
};
GuardarInformacionServicioAlimento = function () {
    var parametros = {};

    parametros.ListaServicioAlimentoInfos = ObtenerRegistrosGuardar();
    var paramSer = SerialiceJSON(parametros);
    $.ajax({
        type: "POST",
        url: "ServicioAlimentoCorrales.aspx/GuardarInformacionServicioAlimento",
        contentType: "application/json; charset=utf-8",
        data: paramSer,
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                bootbox.alert(msgErrorGuardar);
            } else {
                $('#msgGuardadoModal').modal('show');
                $("#btnGuardar").attr("disabled", true);
                ObtenerInformacionDiariaAlimento();
            }
        }
    });
};

ObtenerInformacionDiariaAlimento = function () {
    $.ajax({
        type: "POST",
        url: "ServicioAlimentoCorrales.aspx/ObtenerInformacionDiariaAlimento",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            //alert(request.Message);
        },
        success: function (data) {
            var respuesta = {};
            respuesta.Arreglo = data.d.Datos;
            respuesta.Resources = LlenarRecursos();
            if (data.d.EsValido == false) {
                AgregaElementos(respuesta);
                $("#btnImprimir").attr("disabled", true);
            } else {
                AgregaElementos(respuesta);
                $("#btnImprimir").attr("disabled", false);
            }
            esActualizacion = false;
        }
    });
};
AgregaElementos = function (datos) {
    if (datos != null) {
        $('#Grid').setTemplateURL('../Templates/ServicioAlimento.htm');
        $('#Grid').processTemplate(datos);
    } else {
        $('#Grid').html("");
    }
};
//Cabecero de la tabla
LlenarRecursos = function () {
    Resources = {};
    Resources.Corral = msgCorral;
    Resources.KilosProgramados = msgKilosProgramados;
    Resources.DescripcionFormula = msgFormula;
    Resources.Comentarios = msgComentarios;
    Resources.FechaRegistro = msgFechaRegistro;
    Resources.Opcion = msgOpcion;
    return Resources;
};
//-------------------------------------------------------------------
// Funcion para consultar el corral por medio de AJAX
//-------------------------------------------------------------------
function ValidarCodigoCorral() {
   
    var parametros = {};
    if ($('#txtCodigoCorral').val() == "") {
              bootbox.alert(msgConsultarCorral, function () {
            $("#txtCodigoCorral").css("background-color", "white");
            $("#txtCodigoCorral").focus();
        });
        return false;
    } 
    $("#txtCodigoCorral").css("background-color", "white");
    parametros.CodigoCorral = $('#txtCodigoCorral').val().trim();
    $.ajax({
        type: "POST",
        url: "ServicioAlimentoCorrales.aspx/ValidarCodigoCorral",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(parametros),
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                return false;
            } else {
                switch (data.d.Datos.CorralID) {
                    case -3:
                        $("#txtCodigoCorral").val('');
                        bootbox.alert(msgTipoCorralDiferente, function () {
                            $("#txtCodigoCorral").css("background-color", "white");
                            $("#txtCodigoCorral").focus();
                        });
                        break;
                    case -2:
                        $("#txtCodigoCorral").val('');
                        bootbox.alert(msgCorralNoExiste, function () {
                            $("#txtCodigoCorral").css("background-color", "white");
                            $("#txtCodigoCorral").focus();
                        });
                        break;
                    case -1:
                        $("#txtCodigoCorral").val('');
                        if ($('#btnAgregar')[0].value == msgActualizar) {
                            bootbox.alert(msgLoteAsignadoActualizado, function () {
                                $("#txtCodigoCorral").css("background-color", "white");
                                $("#txtCodigoCorral").focus();
                            });
                        } else {
                            bootbox.alert(msgLoteAsignado, function () {
                                $("#txtCodigoCorral").css("background-color", "white");
                                $("#txtCodigoCorral").focus();
                            });
                        }
                        break;
                    case 0:
                       
                        $("#txtCodigoCorral").val('');
                        bootbox.alert(msgCorralAsignado, function () {
                            $("#txtCodigoCorral").css("background-color", "white");
                            $("#txtCodigoCorral").focus();
                        });
                        break;
                    default:

                        if (!ValidarExistente(data.d.Datos.CorralID)) {

                            if (data.d.Datos.CorralID > 0) {
                                if ( !esActualizacion) {
                                    $('#hdnCorralId').val(data.d.Datos.CorralID);
                                    $("#txtKgsProgramados").val('');
                                    $("#txtComentarios").val('');
                                    var combo = $("#ddlFormula");
                                    combo[0].selectedIndex = 0;
                                    $("#txtCodigoCorral").attr("disabled", true);
                                    $("#txtComentarios").attr("disabled", false);
                                    $("#txtKgsProgramados").attr("disabled", false);
                                    $("#ddlFormula").attr("disabled", false);
                                    $('#btnAgregar')[0].value = msgAgregar;
                                } else {
                             
                                    $('#btnAgregar')[0].value = msgActualizar;
                                }
                                //
                            
                                $("#txtCodigoCorral").css("background-color", "white");
                                $("#txtKgsProgramados").focus();
                                
                            }
                        }
                        else {
                            bootbox.alert(msgCorralAsignado, function () {
                                $("#txtCodigoCorral").css("background-color", "white");
                                $("#txtCodigoCorral").focus();

                                $("#txtCodigoCorral").val('');
                            });
                        }
                        
                        break;
                }
                
                return true;
            }
        }
    });
    return true;
}
//-------------------------------------------------------------------
// Validar si ya existe el corral agregado
//-------------------------------------------------------------------
function ValidarExistente(corralId) {
    var existe;
    $('#Grid table tbody tr').each(function () {
        if (corralId == $(this).attr("data-id")) {
            existe = 1;
        }
    });

   /* $('#Grid table tbody tr').each(function () {
        if (corralId == $(this).attr("data-codigocorral")) {
            existe = 1;
        }
    });*/
    if (existe == 1) {
        return true;
    } 

}
ObtenerDatosAgregar = function () {
    var ArregloElementos = new Array();
    var Elementos = {};
    var combo = $("#ddlFormula");
    var valor = combo[0][combo[0].selectedIndex].text;
    Elementos.CorralID = $('#hdnCorralId').val();
    Elementos.FormulaID = $("#ddlFormula").val();
    Elementos.CodigoCorral = $("#txtCodigoCorral").val();
    Elementos.KilosProgramados = $("#txtKgsProgramados").val();
    Elementos.DescripcionFormula = valor;
    Elementos.Comentarios = $("#txtComentarios").val();
    Elementos.FechaRegistro = ObtenFechaHoy();
    Elementos.ServicioID = 0;
    Elementos.Editado = 1;
    ArregloElementos[0] = Elementos;
    return ArregloElementos;
};
ObtenerRegistros = function () {
    var ArregloElementos = new Array();
    var index = 0;

    $('#Grid table tbody tr').each(function () {
            ArregloElementos[index] = ObtenerDatos(this);
        index++;
    });
    return ArregloElementos;
};
ObtenerDatos = function (fila) {
   
    var Elemento = {};
    Elemento.CorralID = $(fila).attr("data-id");
    Elemento.FormulaID = $(fila).attr("data-formulaid");
    Elemento.CodigoCorral = $(fila).attr("data-codigocorral");
    Elemento.KilosProgramados = $(fila).attr("data-kilos");
    Elemento.DescripcionFormula = $(fila).attr("data-descripcion");
    Elemento.Comentarios = $(fila).attr("data-comentarios");
    Elemento.FechaRegistro = $(fila).attr("data-FechaRegistro");
    Elemento.ServicioID = $(fila).attr("data-idServicio");
    Elemento.Editado = $(fila).attr("data-editado");
    return Elemento;
};
ObtenerRegistrosGuardar = function () {
    var ArregloElementos = new Array();
    var index = 0;
    $('#Grid table tbody tr').each(function () {
        if ($(this).attr("data-editado") == 1) {
            ArregloElementos[index] = ObtenerDatosGuardar(this);
            index++;
        }
    });
    return ArregloElementos;
};



ObtenerDatosGuardar = function (fila) {
    var Elemento = {};
    Elemento.CorralID = $(fila).attr("data-id");
    Elemento.FormulaID = $(fila).attr("data-formulaid");
    Elemento.CodigoCorral = $(fila).attr("data-codigocorral");
    Elemento.KilosProgramados = quitarformatoKilogramos($(fila).attr("data-kilos"));
    Elemento.DescripcionFormula = $(fila).attr("data-descripcion");
    Elemento.Comentarios = $(fila).attr("data-comentarios");
    Elemento.FechaRegistro = $(fila).attr("data-FechaRegistro");
    Elemento.Editado = $(fila).attr("data-editado");
    Elemento.ServicioID = $(fila).attr("data-idServicio");
    return Elemento;
};



quitarformatoKilogramos = function (variable) {

    var kilos = "";
    var res = variable.split(",");
     if (res.length > 0) {
         for (index = 0; index < res.length; ++index) {
             kilos = kilos.concat(res[index]);

         }
     } else {
         kilos = variable;
     }
     return kilos;
}

agregarformatoKilogramos = function (variable) {
   
    var kilos = "";
    var aux = quitarformatoKilogramos(variable.toString());
    var res = aux.split("");
    var tres = res.length - 3;
    var seis = res.length - 6;
    for (index = 0; index < res.length; ++index) {
        if (res.length > 3) {
            if (index == tres) {
                kilos = kilos.concat(',');

            }
        }
        if (res.length > 6) {
            if (index == seis) {
                kilos = kilos.concat(',');
            }
        }
        kilos = kilos.concat(res[index]);

    }

    return kilos;

}



ObtenerRegistrosActualizar = function (corralId) {
    var ArregloElementos = new Array();
    var index = 0;
    $('#Grid table tbody tr').each(function () {
        ArregloElementos[index] = ObtenerDatosActualizar(this, corralId);
        index++;
    });
    return ArregloElementos;
};
ObtenerExistencias = function (fila, corralId) {
    if (corralId == $(fila).attr("data-id")) {
        return true;
    } else {
        return false;
    }
}
ObtenerDatosActualizar = function (fila, corralId) {
    
    var Elementos = {};
    if (corralId == $(fila).attr("data-id")) {
        var combo = $("#ddlFormula");
        var valor = combo[0][combo[0].selectedIndex].text;
        //Elementos.CorralID = $(fila).attr("data-id");
        if ($("#hdnCorralId").val() == 0) {
            $("#hdnCorralId").val($("#hdnCorralIdAnt").val());
        }
        Elementos.CorralID = $("#hdnCorralId").val();
        Elementos.CorralAnterioID = $("#hdnCorralIdAnt").val();
        Elementos.FormulaID = $("#ddlFormula").val();
        Elementos.CodigoCorral = $("#txtCodigoCorral").val();
        Elementos.KilosProgramados = $("#txtKgsProgramados").val();
        Elementos.DescripcionFormula = valor;
        Elementos.Comentarios = $("#txtComentarios").val();
        Elementos.ServicioID = $(fila).attr("data-idServicio");
        Elementos.Editado = 1;
        Elementos.FechaRegistro = ObtenFechaHoy();
    } else {
        Elementos.CorralID = $(fila).attr("data-id");
        Elementos.FormulaID = $(fila).attr("data-formulaid");
        Elementos.CodigoCorral = $(fila).attr("data-codigocorral");
        Elementos.KilosProgramados = $(fila).attr("data-kilos");
        Elementos.DescripcionFormula = $(fila).attr("data-descripcion");
        Elementos.Comentarios = $(fila).attr("data-comentarios");
        Elementos.FechaRegistro = $(fila).attr("data-FechaRegistro");
        Elementos.ServicioID = $(fila).attr("data-idServicio");
        Elementos.Editado = $(fila).attr("data-editado");
    }
    return Elementos;
};
//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
};

function solo_num_ent(event) {
    
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode == 37 || charCode == 39  || charCode == 44)
        return true;
    
    if (charCode > 31 && (charCode < 48 || charCode > 57) )
        return false;

    return true;
}
function alphanumeric(val) {
    var regex = /^[0-9A-Za-z]+$/;
    if (regex.test(val)) {
        return true;
    }
    else {
        return false;
    }
}
function ObtenFechaHoy() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function SelecionaEditar(corralId, servicio, codigoCorral) {
    esActualizacion = true;
    $('#hdnCorralId').val(corralId);
    $('#hdnCorralIdAnt').val(corralId);
    banActualizacion = corralId;
    
    if (servicio != 0) {
        if (ValidarCodigoCorralParaModificar(codigoCorral.trim(), this, corralId)) {
           
           
        } else {
            $('#btnAgregar')[0].value = msgAgregar;
            $("#txtCodigoCorral").attr("disabled", false);
            $("#txtCodigoCorral").val('');
            $("#txtKgsProgramados").val('');
            $("#txtComentarios").val('');
            var combo = $("#ddlFormula");
            combo[0].selectedIndex = 0;
           // $("#txtCodigoCorral").focus();
        }
        return false;
    }
    $('#Grid table tbody tr').each(function () {
        if (corralId == $(this).attr("data-id")) {
            $('#hdnCorralId').val($(this).attr("data-id"));
            $("#ddlFormula").val($(this).attr("data-formulaid"));
            $("#txtCodigoCorral").val($(this).attr("data-codigocorral"));
            $("#txtKgsProgramados").val(agregarformatoKilogramos($(this).attr("data-kilos")));
            $("#txtComentarios").val($(this).attr("data-Comentarios"));
            $("#txtKgsProgramados").attr("disabled", false);
            $("#ddlFormula").attr("disabled", false);
            $("#txtCodigoCorral").css("background-color", "white");
            $("#ddlFormula").css("background-color", "white");
            $('#btnAgregar')[0].value = msgActualizar;
            
           
        }
    });
    
}
//-------------------------------------------------------------------
// Funcion para consultar el corral por medio de AJAX
// para validacion de editar registros guardados o no
//-------------------------------------------------------------------
function ValidarCodigoCorralParaModificar(codigoCorral, fila, corralId) {
    var parametros = {};
    parametros.CodigoCorral = codigoCorral;
    var esValido = 0;
    $.ajax({
        type: "POST",
        url: "ServicioAlimentoCorrales.aspx/ValidarCodigoCorral",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(parametros),
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if ( !data.d.EsValido ) {
                return false;
            } else {
                switch (data.d.Datos.CorralID) {
                    case -3:
                        bootbox.alert(msgTipoCorralDiferente);
                        esValido = 0;
                        break;
                    case -2:
                        bootbox.alert(msgCorralNoExiste);
                        esValido = 0;
                        break;
                    case -1:
                        bootbox.alert(msgLoteAsignadoActualizado);
                        LimpiarCantroles();
                        esValido = 0;
                        break;
                    case 0:
                        esValido = 1;
                        break;
                    default:
                            if (data.d.Datos.CorralID > 0) {
                                esValido = 1;
                            }
                            else {
                                bootbox.alert(msgLoteAsignado);
                        }
                        break;
                }
                if (esValido == 1) {
                    $('#Grid table tbody tr').each(function () {
                        if (corralId == $(this).attr("data-id")) {
                            $('#hdnCorralId').val($(this).attr("data-id").trim());
                            $("#ddlFormula").val($(this).attr("data-formulaid").trim());
                            $("#txtCodigoCorral").val($(this).attr("data-codigocorral").trim());
                            $("#txtKgsProgramados").val(agregarformatoKilogramos($(this).attr("data-kilos")));
                            $("#txtComentarios").val($(this).attr("data-Comentarios").trim());
                            $("#txtKgsProgramados").attr("disabled", false);
                            $("#ddlFormula").attr("disabled", false);
                            $("#txtCodigoCorral").css("background-color", "white");
                            $("#ddlFormula").css("background-color", "white");
                            $('#btnAgregar')[0].value = msgActualizar;
                        }
                    });
                    return true;
                }
            }
        }
    });
    return false;
}
//-------------------------------------------------------------------
// Funcion para consultar el corral por medio de AJAX
// para validacion de editar registros anexados al grid
//-------------------------------------------------------------------
function ValidarCodigoCorralParaModificarGrid(codigoCorral) {
    var parametros = {};
    parametros.CodigoCorral = codigoCorral;
    $.ajax({
        type: "POST",
        url: "ServicioAlimentoCorrales.aspx/ValidarCodigoCorral",
        contentType: "application/json; charset=utf-8",
        data: SerialiceJSON(parametros),
        error: function (request) {
            //alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                return false;
            } else {
                switch (data.d.Datos.CorralID) {
                    case -3:
                        bootbox.alert(msgTipoCorralDiferente);
                        break;
                    case -2:
                        bootbox.alert(msgCorralNoExiste);
                        break;
                    case -1:
                        bootbox.alert(msgLoteAsignado);
                        break;
                    default:
                            var respuesta = {};
                            $('#hdnCorralId').val(data.d.Datos.CorralID);
                            respuesta.Arreglo = ObtenerRegistrosActualizar($('#hdnCorralIdAnt').val());
                            respuesta.Resources = LlenarRecursos();
                            AgregaElementos(respuesta);
                            $('#btnAgregar')[0].value = msgAgregar;
                            $("#txtCodigoCorral").val('');
                            $("#txtKgsProgramados").val('');
                            $("#txtComentarios").val('');
                            var combo = $("#ddlFormula");
                            combo[0].selectedIndex = 0;
                            $("#btnGuardar").attr("disabled", false);
                            $("#txtCodigoCorral").focus();
                            $('#hdnCorralId').val("");
                            $('#hdnCorralIdAnt').val("");
                            esActualizacion = false;
                        break;
                }
            }
        }
    });

    return false;
}


