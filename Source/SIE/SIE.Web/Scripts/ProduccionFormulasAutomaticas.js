// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="jscomun.js" />
var fecha = "";
var fechaInicial = "";
$(document).ready(function () {
    $("#txtFecha").css({ "text-align": "right", "margin-top": "-5px", "pointer-events": "none", "background-color": "#F4F4F4 !important" });
    $("#txtFecha").datepicker({
        firstDay: 1,
        showOn: 'button',
        buttonImage: '../assets/img/calander.png',
        onSelect: function (date) {
            fechaInicial = $('#txtFecha').datepicker('getDate');
            $('#txtFecha').trigger('change');
        }
    });

    $.datepicker.setDefaults($.datepicker.regional['es']);

    fechaInicial = $('#txtFecha').datepicker('getDate');

    $("#txtFecha").focusout(function () {
        //var fechaSeleccionada = $('#txtFecha').datepicker('getDate');
        var fechaSeleccionada = $('#txtFecha').val();
        console.log(Date.parse(fechaSeleccionada));
        if (fechaSeleccionada == null ) {
            $('#txtFecha').datepicker('setDate', fechaInicial).trigger('change');
        }
    });

    $("#txtFecha").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };

    $('#btnImportar2').click(function () {
        $('#FileUploadControl').trigger('click');
    });

    $('#Cerrarventana').click(function () {
        $('#modalBachadas').modal('hide');
    });

    $('#btnGuardar').click(function () {
        if ($("#txtArchivoProduccion").val() == '') {
            bootbox.alert(window.msjSeleccionArchivo);
        } else {
            BloquearPantalla();
            GuardarDatos();
        }
    });

    $('#btnCancelar').click(function () {
        $('#dlgCancelar').modal('show');
    });

    $('#btnDialogoSi').click(function () {
        LimpiarPantalla();
        //location.href = location.href;
    });



});

GuardarDatos = function () {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "ProduccionFormulasAutomaticas.aspx/Guardar",
        success: function (data) {
            if (data.d.CodigoMensajeRetorno == 0) {
                bootbox.alert(window.msjGuardadoExitoso, function () {
                    DesbloquearPantalla();
                    MostrarResumenDeProduccion();
                });
            } else {
                if (data.d.CodigoMensajeRetorno == 1) {
                    DesbloquearPantalla();
                    bootbox.alert('El almacén no cuenta con existencia, para el producto ' + data.d.Mensaje + '.', function () {
                        //location.href = location.href;
                    });
                }
                if (data.d.CodigoMensajeRetorno == 2) {
                    DesbloquearPantalla();
                    bootbox.alert('No existe almacén inventario para el producto' + data.d.Mensaje + '.', function () {
                        //location.href = location.href;
                    });
                }
                if (data.d.CodigoMensajeRetorno == 3) {
                    DesbloquearPantalla();
                    bootbox.alert('No hay cantidad suficiente para ' + data.d.Mensaje + '.', function () {
                        //location.href = location.href;
                    });
                }
                if (data.d.CodigoMensajeRetorno == 4) {
                    DesbloquearPantalla();
                    bootbox.alert('No se encontro el almacén, de planta de alimento configurado.', function () {
                        //location.href = location.href;
                    });
                }
                if (data.d.CodigoMensajeRetorno == 5) {
                    DesbloquearPantalla();
                    bootbox.alert('Ocurrio un error al intentar guardar la producción de formulas.', function () {
                        //location.href = location.href;
                    });
                }
                if (data.d.CodigoMensajeRetorno == 6) {//Mensaje de la poliza
                    DesbloquearPantalla();
                    bootbox.alert(data.d.Mensaje, function () {
                        //location.href = location.href;
                    });
                }
            }
        },
        error: function (e) {
            DesbloquearPantalla();
            bootbox.alert('Ocurrio un error al intentar guardar la producción de formulas.', function () {
                //LimpiarPantalla();
            });
        }
    });
};

MostrarResumenDeProduccion = function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "ProduccionFormulasAutomaticas.aspx/ResumenDeProduccion",
        success: function (data) {
            if (data.d != null) {
                DesbloquearPantalla();
                var datos = data.d;
                for (var row = 0; row < datos.length; row++) {
                    if (datos[row].CantidadProducida < datos[row].CantidadReparto) {
                        for (var i = 0; i < data.d.length; i++) {
                            $("#tbResumenFormulas tbody").append("<tr>" +
                                               "<td style='text-align:center;'>&nbsp;</td>" +
                                               "<td style='text-align:center;'>&nbsp;</td>" +
                                               "<td style='text-align:center;'>&nbsp;</td>" +
                                               "</tr>");
                        }
                        var renglon = $("#tbResumenFormulas tbody tr");
                        for (i = 0; i < data.d.length; i++) {
                            renglon[i].cells[0].innerHTML = data.d[i].Formula.Descripcion;
                            renglon[i].cells[1].innerHTML = data.d[i].CantidadProducida;
                            renglon[i].cells[2].innerHTML = data.d[i].CantidadReparto;
                        }
                        $("#dlgResumenProduccionFormulas").modal("show");
                        $("#btnAceptarResumen").click(function () {
                            location.href = location.href;
                        });
                        return;
                    }
                }
            }
            LimpiarPantalla();
            //location.href = location.href;
        }
    });
};

function checkfile(sender) {
    var validExts = new Array(".txt");
    var fileExt = sender.value;
    fileExt = fileExt.substring(fileExt.lastIndexOf('.'));

    if (validExts.indexOf(fileExt) < 0) {
        bootbox.alert(window.msjArchivoTXT);
        return false;
    } else {
        if (sender.value == null || sender.value == '') {
            bootbox.alert(window.msjSeleccioneArchivo);
            return false;
        }
        $("#btnCargarImagen").click();
        BloquearPantalla();
        return true;
    }
};

ValidacionErrores = function (paramSemana) {
    var jsonText = JSON.stringify({ error: paramSemana });
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "ProduccionFormulasAutomaticas.aspx/MostrarErrores",
        data: jsonText,
        success: function (data) {
            sacarerror(data.d);
        },
        error: function (e) {
            DesbloquearPantalla();
            bootbox.alert('Ocurrio un error al mostrar los errores', function () {
                location.href = location.href;
            });
        }
    });
};

sacarerror = function (error) {
    $("#txtArchivoProduccion").val('');
    if (error == window.vacio) {
        bootbox.alert(window.msjArchivoVacio);
        return;
    }
    if (error == window.columnas) {
        bootbox.alert(window.msjInformacionRequerida);
        return;
    }
    if (error == window.ColumnaCodigo) {
        bootbox.alert(window.msnInicioFin);
        return;
    }
    if (error == window.sumatorias) {
        bootbox.alert(window.msjSumaTotal);
        return;
    }
    if (error == window.producto) {
        bootbox.alert(window.msjProductos);
        return;
    }
    if (error == window.rotomix) {
        bootbox.alert(window.msjRotomix);
        return;
    }
    if (error == window.batch) {
        bootbox.alert(window.msjBatch);
        return;
    }
    if (error == window.formulas) {
        bootbox.alert(window.msjFormulas);
        return;
    }
    if (error == window.fecha) {
        bootbox.alert(window.msjFechaYHora);
        return;
    }
    if (error == window.ingrediente) {
        bootbox.alert(window.msjIngrediente);
        return;
    }
    bootbox.alert(error);
};

AbrirDetalle = function (dato) {

    var datos2 = { 'formula': dato };
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(datos2),
        contentType: "application/json",
        url: "ProduccionFormulasAutomaticas.aspx/RegresarListasDetalleBatchs",
        success: function (data) {

            var recursos = {};
            recursos.NumeroBachada = window.NumeroBachada;
            recursos.KilosProducidos = window.KilosProducidos;
            recursos.rotomix2 = window.rotomix2;

            var datos = {};
            datos.Recursos = recursos;
            datos.Valores = data.d;
            var divContenedorDetalladas = $('#divFormulasDetalladas');
            divContenedorDetalladas.setTemplateURL('../Templates/GridDetalleBatchs.htm');
            divContenedorDetalladas.processTemplate(datos);
            $('#modalBachadas').modal('show');
        },
        error: function (e) {
            DesbloquearPantalla();
            bootbox.alert('Ocurrio un error al intentar consultar las bachadas', function () {
                location.href = location.href;
            });
        }
    });
};

CargarGrid = function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "ProduccionFormulasAutomaticas.aspx/RegresarListas",
        success: function (data) {

            var recursos = {};
            recursos.Formula = window.Formula;
            recursos.CantidadProgramada = window.CantidadProgramada;
            recursos.CantidadReal = window.CantidadReal;
            recursos.VerDetalle = window.VerDetalle;
            var datos = {};

            datos.Recursos = recursos;
            datos.Valores = data.d;
            var divContenedor = $('#divGridFormulasAgrupadas');

            divContenedor.setTemplateURL('../Templates/GridFormulasAcumuladas.htm');
            divContenedor.processTemplate(datos);
        },
        error: function (e) {
            DesbloquearPantalla();
            bootbox.alert('Ocurrio un error al mostrar los datos de las formulas.', function () {
                location.href = location.href;
            });
        }
    });
};

LimpiarPantalla = function () {
    $('#divGridFormulasAgrupadas').html('');
    $("#txtArchivoProduccion").val('');
    $('#GridFormulaAcumulada').html('');
};





