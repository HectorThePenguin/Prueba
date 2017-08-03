/*
 * Documento de control javascript para Programacion de Sacrificio
 */

var totalRecibidos = 0;     //Contar los aretes recibidos para sacrificio
var Aretes;                    //Lista de aretes para sacrifico
var usuarioValido = true;       //Identifica si es un usuario valido
var OrdenesSacrificio;

$(document).ready(function() {
    var presionoEnter;

    Aretes = [];

    $("#gvBusqueda").dataTable();
    // Se crea un datatable jquery
    //var ordenes = $("#gvBusqueda tbody tr");
    //if (ordenes.length > 0) {
    //    var oTable = $("#gvBusqueda").dataTable({
    //        "bFilter": false,
    //        "bInfo": false,
    //        "bSort": false,
    //        "bLengthChange": false,
    //        "iDisplayLength": 10,
    //        //"sPaginationType": "bootstrap",
    //        "oLanguage": {
    //            "oPaginate": {
    //                "sPrevious": "Ant",
    //                "sNext": "Sig"
    //            }
    //        }
    //    });
    //}

    //Inicializadores
    $("#btnGuardar").html(btnGuardarText);
    $("#btnCancelar").html(btnCancelarText);
    $("#btnAgregar").html(btnAgregarText);
    $("#btnDlgSeleccionar").html(btnDlgSeleccionar);
    $("#btnDlgCerrar").html(btnDlgCerrar);
    //$('#btnGuardar').prop("disabled", true);
    
    $("#txtArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    
    //Eventos del txtArete
    /*
    $('#txtArete').bind("cut copy paste", function(e) {
        e.preventDefault();
    });
    */

    $('#txtArete').keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) { //Enter keycode
            presionoEnter = true;
            if ($('#txtArete').val() != '') {
                MarcarAreteRecibidoSacrificio();
            }
        } else {
            presionoEnter = false;
        }
    });

    //Prevenimos el evento enter para la pantalla completa
    $(window).keydown(function(event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
        return true;
    });


    $('#btnGuardar').click(function () {
        var nodes = $("#TablaAretes tbody tr");
        if ($("#txtCorral").val() != "") {
            if (nodes.length>0) {
                if (nodes.length == $("#cabezasASacrificar").val()) {
                    GuardarSalidaAnimal();
                } else {
                    bootbox.alert(msgCabezasMenorProgramadas, function () { $("#txtArete").focus(); });
                }
            } else {
                bootbox.alert(msgIngreseNoIndividual, function () { $("#txtArete").focus(); });
            }            
        } else {
            bootbox.alert(msgNoIngresoCorral);
        }

    });

    $('#btnCancelar').click(function () {
        VerCancelar();
    });

    $('#btnBuscar').click(function () {
        var ordenes = $("#gvBusqueda tbody tr");
        if (ordenes.length > 0) {
            var nodes = $("#TablaAretes tbody tr");
            if (nodes.length > 0) {
                VerBuscar();
            } else {
                $('#responsive').modal('show');
            }
        } else {
            bootbox.alert(msgNoExistenOrdenesSacrificio);
        }
    });
    
    $('#btnSeleccion').live("click", function () {
        ValidarSeleccion(oTable);
    });

    $('#gvBusqueda').click(function (e) {
        $(oTable.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(e.target.parentNode).addClass('row_selected');
    });

    $("#btnCerrar").click(function () {
        $(oTable.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
    });
    
    $('#btnAgregar').live("click", function () {
        if ($("#txtArete").val() != "") {
            AgregarArete();
            
        } else {
            bootbox.alert(msgIngreseNoIndividual, function () { $("#txtArete").focus(); });
        }
    });

    //Cargar grid sin datos
    InicializarTabla();
});

GuardarSalidaAnimal = function () {
    var datos = { "animalInfo": Aretes, loteID: $("#loteID").val(), corraletaID: $("#corraletaID").val(), ordenSacrificioDetalleID: $("#ordenSacrificioDetalleID").val() };
    $.ajax({
        type: "POST",
        url: "ProgramacionSacrificio.aspx/GuardarSalida",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function(request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            if (data.d) {
                if (data.d == 1) {
                    LimpiarControles();
                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgGuardadoExito, function () { Location.href = document.location.href = 'ProgramacionSacrificio.aspx'; });
                } else {
                    bootbox.alert(msgOcurrioErrorGrabar);
                }
            }
        }
    });
};

AgregarArete = function () {
    var consecutivo;
    var nodes = $("#TablaAretes tbody tr");
    var encontrado = 0;
    var datos = { 'arete': $("#txtArete").val(), 'loteID': $("#loteID").val() };
    $.ajax({
        type: "POST",
        url: "ProgramacionSacrificio.aspx/ObtenerExistenciaAnimal",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            if (data.d) {
                //Validar que no este agregado el no individual
                var nodes = $("#TablaAretes tbody tr");
                
                    $(nodes).each(function() {
                        if (this.cells[1].innerHTML == $("#txtArete").val()) {
                            encontrado = 1;
                        }
                    });
                    if (encontrado == 0) {

                        if (nodes.length == $("#cabezasASacrificar").val()) {
                            bootbox.alert(msgProgramadasNoCapacidad, function () { $("#txtArete").focus(); });
                        } else {
                        var cadena = "000" + (nodes.length + 1);
                        consecutivo = cadena.substring(cadena.length - 3, cadena.length);
                        var arete =
                        {
                            AnimalID: data.d.AnimalID,
                            Consecutivo: consecutivo,
                            Arete: $("#txtArete").val(),
                            Seleccionado: true,
                            CargaInicial: data.d.CargaInicial
                        };
                        Aretes.push(arete);
                        InicializarTabla();
                        $("#txtArete").val("");
                        PonerFocoArete();
                        }
                    } else {
                        bootbox.alert(msgAreteAgregado, function () { $("#txtArete").focus(); });
                    }
            } else {
                bootbox.alert(msgNoIndividualNoCorresponde, function () { $("#txtArete").focus(); });
            }
        }
    });
};

ValidarSeleccion = function (oTableLocal) {
    var row = oTableLocal.fnGetNodes();
    var contadorSeleccionados = 0;
    
    for (var i = 0 ; i < row.length ; i++) {
        if ($(row[i]).hasClass('row_selected')) {
            contadorSeleccionados++;
            
            $('#txtCorral').val(row[i].cells[0].innerHTML);
            $('#txtCorral').attr("disabled", true);
            $('#cabezasASacrificar').val(row[i].cells[3].innerHTML);
            $('#txtCabezasASacrificar').val(row[i].cells[3].innerHTML);
            $('#loteID').val(row[i].cells[6].innerHTML);
            $('#corraletaID').val(row[i].cells[7].innerHTML);
            $('#ordenSacrificioDetalleID').val(row[i].cells[8].innerHTML);
            
            $('#responsive').modal('hide');
            $('#txtArete').removeAttr("disabled");
            PonerFocoArete();
            
            $(oTableLocal.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
        }
    }
    
    if (contadorSeleccionados == 0) {
        $('#responsive').modal('hide');
        bootbox.alert(msgSeleccioneOrden, function () { $('#responsive').modal('show'); });
    } else {
        ObtenerAnimalesPorCorral();
    }
};

function ObtenerAnimalesPorCorral() {
    var datos = { 'loteID': $("#loteID").val() };
    $.ajax({
        type: "POST",
        url: "ProgramacionSacrificio.aspx/ObtenerAnimalesPorLoteID",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            if (data.d) {
                listaAretes = [];
                $(data.d).each(function () {
                    listaAretes.push(this.Arete);
                });

                $('#txtArete').typeahead({
                    source: listaAretes
                });
            } else {
                bootbox.alert(msgCorralSinAretes);
            }
        }
    });
};

EnviarMensajeUsuario = function() {
    bootbox.dialog({
        message: msgUsuarioNoPermitido,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    GoPrincipal();
                }
            },
        }
    });
};

MostrarMensajeErrorObtenerLista = function() {
    bootbox.dialog({
        message: msgErrorObtenerListaCorrales,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    PonerFocoCorral();
                }
            },
        }
    });
};

MostrarMensajeErrorNoHayCorrales = function(){
    bootbox.dialog({
        message: msgErrorObtenerNoHayCorrales,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    PonerFocoBuscar();
                }
            },
        }
    });
};

/*
 * Inicializar el mostrado de la tabla
 */
InicializarTabla = function () {
    var respuesta = {}; //arreglo de datos para la tabla de recursos
    //LoadDataDemo();
    respuesta.Aretes = Aretes;
    respuesta.Recursos = LlenarRecursos();

    //Inicializamos de nuevo la tabla
    AgregaElementos(respuesta);
};

MarcarAreteRecibidoSacrificio = function() {

    //TODO: Marcar el arete como recibido

};

VerBuscar = function () {
    bootbox.dialog({
        message: msgDlgBuscar,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Si",
                className: "btn SuKarne",
                callback: function () {
                    /*
                    LimpiarControles();
                    $('#responsive').modal('show');
                    */
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

};

VerCancelar = function() {
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
                    PonerFocoBuscar();
                }
            }
        }
    });

};
/*
 *Colocar el foco en el txtArete
 */
PonerFocoArete = function () {
    $("#txtArete").focus();
};

PonerFocoBuscar = function() {
    $("#btnBuscar").focus();
};

LimpiarControles = function() {
    $('#txtCorral').val('');
    $('#txtArete').val('');
    Aretes.length=0;
    InicializarTabla();
};
/*
 * Funcion que envia a la pantalla de salida por muerte
 */
GoLocal = function () {
    document.location.href = 'ProgramacionSacrificio.aspx';
};

/*
 * ir a pantalla principoal
 */
GoPrincipal = function () {
    document.location.href = '../Principal.aspx';
};

/*
 * Agrega los elementos a la tabla plantilla de aretes para sacrificio
 */
AgregaElementos = function (datos) {
    if (datos != null) {
        $('#TablaAretes').setTemplateURL('../Templates/GridProgramacionSacrificio.htm');
        $('#TablaAretes').processTemplate(datos);
    } else {
        $('#TablaAretes').html("");
    }
};

//Cabecero de la tabla
LlenarRecursos = function () {
    Resources = {};
    Resources.HeaderConsecutivo = hConsecutivo;
    Resources.HeaderArete = hArete;
    Resources.HeaderSeleccion = hSeleccion;
    return Resources;
};

