//Variables globales
var msjAbierto = 0;
var ingredientes = 0;
var mostrarDiferencias = 0;
var objeto = document.activeElement;
var focoAlterado = 0;
var fecha = "";
var fechaInicial = "";
$(document).ready(function() {
    LimpiarCampos();
    $("#txtKilogramosProducidos").attr("disabled", true);
    $("#txtKilogramosProducidos").css("text-align", "right");
    $("#txtTotalKilogramosIngrediente").css("text-align", "right");
    $("#txtFecha").css("text-align", "right");
    $("#cmbFormula").attr("disabled", true);
    $("#cmbRotoMix").css("text-align", "left");
    $("#txtBatch").css("text-align", "right");
    
    $("#txtFecha").datepicker({
        firstDay: 1,
        showOn: 'button',
        buttonImage: '../assets/img/calander.png',
        onSelect: function (date) {
            fechaInicial = $('#txtFecha').datepicker('getDate');
        },
        dateFormat: 'dd-mm-yy'
    });

    $.datepicker.setDefaults($.datepicker.regional['es']);

    fechaInicial = $('#txtFecha').datepicker('getDate');

    $("#txtFecha").focusout(function () {
        var fechaSeleccionada = $('#txtFecha').datepicker('getDate');
        //var fechaSeleccionada = $('#txtFecha').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
        if (fechaSeleccionada == null || Date.parse(fechaSeleccionada) == Date.parse(fechaInicial)) {
            $('#txtFecha').datepicker('setDate', fechaInicial).trigger('change');
        }
    });

    $("#txtFecha").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    //Change  cmbRotoMix
    $("#cmbRotoMix").change(function() {
        if ($("#cmbRotoMix").val() > 0) {
            $("#cmbFormula").attr("disabled", false);
            determinaNumeroBatch();
        } else {
            document.getElementById("cmbFormula").value = [0];
            document.getElementById("txtBatch").value = "";
            document.getElementById("txtKilogramosProducidos").value = "";
            $("#cmbFormula").attr("disabled", true);
            LimpiarCampos();
        }
    });

    function determinaNumeroBatch() {
        var datos = {
            "rotoMix": $("#cmbRotoMix").val()};
        $.ajax({
            type: "POST",
            url: "ProduccionFormulas.aspx/DeterminaNumeroBatch",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    var dato = data.d;
                    document.getElementById("txtBatch").value = data.d;
                }
            },
            error: function(result) {
                alert('ERROR ' + result.status + ' ' + result.statusText);
            }
        });
    }

    $("#txtKilogramosProducidos").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    $("#txtKilogramosProducidos").focusout(function () {
        if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) > 0) {
            $("#txtKilogramosProducidos").val(accounting.formatNumber($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
        } else {
            $("#txtKilogramosProducidos").val("");
        }
    });

    //Change
    $("#cmbFormula").change(function () {
        if ($("#cmbFormula").val() > 0) {
            ObtenerIngredientesFormula();
        } else {
            LimpiarCampos();
        }
    });

    //Click
    $("#btnGuardar").click(function () {
        $(".mascara").trigger("change");
        if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) > 0) {

            if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) > parseFloat($("#txtTotalKilogramosIngrediente").val().replace(/,/g, '').replace(/_/g, '')))
            {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgKilogramosMenor, function () {
                        msjAbierto = 0;
                    });
                    return false;
                }
            }

            if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) < parseFloat($("#txtTotalKilogramosIngrediente").val().replace(/,/g, '').replace(/_/g, '')))
            {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgKilogramosMayor, function () {
                        msjAbierto = 0;
                    });
                    return false;
                }
            } else if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) > parseFloat($("#txtTotalKilogramosIngrediente").val().replace(/,/g, '').replace(/_/g, ''))) {
                var renglonesDiferencias = $("#tbIngredientesDiferencias tbody tr");
                var produccionFormulaDetalleLista = [];
                for (var i = 0; i < ingredientes; i++) {
                    var cantidad = renglonesDiferencias[i].cells[3].innerHTML;
                    cantidad = cantidad.replace(/,/g, '').replace(/_/g, '');
                    if (parseFloat(cantidad) > 0) {
                        var produccionFormulaDetalle = {
                            "Ingrediente": { "IngredienteId": renglonesDiferencias[i].cells[0].innerHTML },
                            "Producto": { "ProductoId": renglonesDiferencias[i].cells[1].innerHTML },
                            "CantidadProducto": cantidad
                        };
                        produccionFormulaDetalleLista.push(produccionFormulaDetalle);
                    } else {
                        produccionFormulaDetalleLista = [];
                        i = ingredientes + 1;
                    }
                }

                if ($('#txtFecha').datepicker('getDate') == null) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgFechaInvalida, function () {
                            msjAbierto = 0;
                        });
                        return false;
                    }
                }

                if (produccionFormulaDetalleLista.length > 0) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgKilogramosMenor, function () {
                            msjAbierto = 0;
                            $(".mascara").trigger("change");
                            if (mostrarDiferencias > 0) {
                                $("#dlgDiferencias").modal("show");
                            } else {
                                GuadarProduccion();
                            }
                        });
                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.lblMensajeCapturarKilogramos, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            } else {
                if ($('#txtFecha').datepicker('getDate') == null) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgFechaInvalida, function () {
                            msjAbierto = 0;
                        });
                        return false;
                    }
                }

                var renglonesDiferencias = $("#tbIngredientesDiferencias tbody tr");
                var produccionFormulaDetalleLista = [];
                for (var i = 0; i < ingredientes; i++) {
                    var cantidad = renglonesDiferencias[i].cells[3].innerHTML;
                    cantidad = cantidad.replace(/,/g, '').replace(/_/g, '');
                    if (parseFloat(cantidad) > 0) {
                        var produccionFormulaDetalle = {
                            "Ingrediente": { "IngredienteId": renglonesDiferencias[i].cells[0].innerHTML },
                            "Producto": { "ProductoId": renglonesDiferencias[i].cells[1].innerHTML },
                            "CantidadProducto": cantidad
                        };
                        produccionFormulaDetalleLista.push(produccionFormulaDetalle);
                    } else {
                        produccionFormulaDetalleLista = [];
                        i = ingredientes + 1;
                    }
                }


                if (produccionFormulaDetalleLista.length > 0) {
                    if (mostrarDiferencias > 0) {
                        $("#dlgDiferencias").modal("show"); //asimov
                    }   else {
                        GuadarProduccion();
                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.lblMensajeCapturarKilogramos, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgSinKilogramosProducidos, function () {
                    msjAbierto = 0;//asimov
                });
            }
        }
    });

    $("#btnAceptar").click(function () {
        GuadarProduccion();
    });

    $("#btnDialogoSi").click(function () {
        location.href = location.href;
    });
});

//Control
LimpiarCampos = function () {
    $("#tbIngredientes tbody").html("");
    $("#tbIngredientesDiferencias tbody").html("");
    for (var i = 0; i <= 10; i++) {
        $("#tbIngredientes tbody").append("<tr>" +
                                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                                "<td>&nbsp;</td>" +
                                                "<td>&nbsp;</td>" +
                                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                            "</tr>"
                                               );
        $("#tbIngredientesDiferencias tbody").append("<tr>" +
                                                        "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                                        "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                                        "<td>&nbsp;</td>" +
                                                        "<td style='text-align:right;'>&nbsp;</td>" +
                                                        "<td style='text-align:right;'>&nbsp;</td>" +
                                                        "<td style='text-align:right;'>&nbsp;</td>" +
                                                    "</tr>");
    }
};

AsignarFunciones = function () {
    $(".mascara").css("text-align", "right");
    //$(".mascara").inputmask('decimal', { radixPoint: ".", autoGroup: true, groupSeparator: ",", groupSize: 3, repeat: "9", digits: 2, numericInput: true, clearMaskOnLostFocus: true });

    $(".mascara").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) { //numeros
            e.preventDefault();
        }
    });

    $(".mascara").focusout(function () {
        var lista = $(".mascara");
        lista.each(function () {
            if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
                $(this).val(accounting.formatNumber($(this).val().replace(/,/g, '').replace(/_/g, ''), 0, ","));
            } else {
                $(this).val("");
            }
        });
    });

    $(".mascara").change(function () {
        if (parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')) > 0) {
            mostrarDiferencias = 0;
            var suma = 0.00;
            var lista = $(".mascara");
            lista.each(function () {
                if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
                    suma = parseFloat(suma) + parseFloat($(this).val().replace(/,/g, '').replace(/_/g, ''));
                }
                var porcentaje = (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) * 100).toFixed(2) / parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')).toFixed(2);
                var renglones = $("#tbIngredientes tbody tr");
                var renglonesDiferencia = $("#tbIngredientesDiferencias tbody tr");

                if (renglones.length > 0 && renglonesDiferencia.length > 0) {
                    renglones[$(this).attr("renglon")].cells[4].innerHTML = porcentaje.toFixed(2);

                    renglonesDiferencia[$(this).attr("renglon")].cells[3].innerHTML = accounting.toFixed($(this).val().replace(/,/g, '').replace(/_/g, ''), 2);
                    renglonesDiferencia[$(this).attr("renglon")].cells[4].innerHTML = porcentaje.toFixed(2) + "%";

                    if (parseFloat(renglones[$(this).attr("renglon")].cells[5].innerHTML).toFixed(2) != parseFloat(renglones[$(this).attr("renglon")].cells[4].innerHTML).toFixed(2)) {
                        $("#renglonDiferencia" + $(this).attr("renglon") + " td").css("background-color", "rgb(192, 80, 80)");
                        mostrarDiferencias++;
                    } else {
                        $("#renglonDiferencia" + $(this).attr("renglon") + " td").css("background-color", "");
                    }
                }
            });
            $("#txtTotalKilogramosIngrediente").val(accounting.toFixed(suma, 0));
        } else {
            $(".mascara").val("");

            $("#txtKilogramosProducidos").focus();
        }
    });
};

//Consultas
ObtenerIngredientesFormula = function () {
    var datos = { "formulaId": $("#cmbFormula").val() };
    $.ajax({
        type: "POST",
        url: "ProduccionFormulas.aspx/ObtenerIngredientesFormula",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var datos = data.d;
                ingredientes = 0;
                LimpiarCampos();
                $("#txtTotalKilogramosIngrediente").val("");
                $("#txtKilogramosProducidos").val("");
                if (datos.length > 0) {

                    if (datos.length > 10) {
                        for (var i = 10; i < datos.length; i++) {
                            $("#tbIngredientes tbody").append("<tr>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "<td>&nbsp;</td>" +
                                "<td>&nbsp;</td>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "</tr>");
                            $("#tbIngredientesDiferencias tbody").append("<tr>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "<td style='text-align:right;display: none;'>&nbsp;</td>" +
                                "<td>&nbsp;</td>" +
                                "<td style='text-align:right;'>&nbsp;</td>" +
                                "<td style='text-align:right;'>&nbsp;</td>" +
                                "<td style='text-align:right;'>&nbsp;</td>" +
                                "</tr>");
                        }
                    }
                    var renglones = $("#tbIngredientes tbody tr");
                    var renglonesDiferencias = $("#tbIngredientesDiferencias tbody tr");
                    for (var i = 0; i < datos.length; i++) {
                        renglones[i].id = "renglon" + i;
                        renglones[i].cells[0].innerHTML = datos[i].IngredienteId;
                        renglones[i].cells[1].innerHTML = datos[i].Producto.ProductoId;
                        renglones[i].cells[2].innerHTML = datos[i].Producto.ProductoDescripcion;
                        renglones[i].cells[3].innerHTML = "<input type='tel' class='mascara span12' renglon='" + i + "' id='ingrediente" + datos[i].IngredienteId + "'>";
                        renglones[i].cells[5].innerHTML = parseFloat(datos[i].PorcentajeProgramado).toFixed(2);

                        renglonesDiferencias[i].id = "renglonDiferencia" + i;
                        renglonesDiferencias[i].cells[0].innerHTML = datos[i].IngredienteId;
                        renglonesDiferencias[i].cells[1].innerHTML = datos[i].Producto.ProductoId;
                        renglonesDiferencias[i].cells[2].innerHTML = datos[i].Producto.ProductoDescripcion;
                        renglonesDiferencias[i].cells[5].innerHTML = parseFloat(datos[i].PorcentajeProgramado).toFixed(2) + "%";
                        ingredientes++;
                    }
                    $("#txtKilogramosProducidos").attr("disabled", false);
                    AsignarFunciones();
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgSinIngredientes, function () {
                            msjAbierto = 0;
                        });
                        $("#txtKilogramosProducidos").attr("disabled", true);
                    }
                }
            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinIngredientes, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};


//valida si la cantidad en el inventario es menor a la cantidad repartida, 
//si es menor se mostrará una ventana denominada ‘Resumen de producción de fórmulas’
function cargarTablaResumen() {
    var datos = { "formulaId": $("#cmbFormula").val() };
        $.ajax
            ({
                type: "POST",
                url: "ProduccionFormulas.aspx/ConsultarFormulaId",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data)
                    {
                    if (data.d)
                        {

                            $("#tbResumenFormulas tbody").append("<tr>" +
                                   "<td style='text-align:center;'>&nbsp;</td>" +
                                   "<td style='text-align:center;'>&nbsp;</td>" +
                                   "<td style='text-align:center;'>&nbsp;</td>" +
                                   "</tr>");

                            var dato = data.d;
                                if (dato.CantidadProducida < dato.CantidadReparto)
                                {
                                    var renglon = $("#tbResumenFormulas tbody tr");
                                    renglon[0].id = "renglon" + 0;
                                    renglon[0].cells[0].innerHTML = dato.DescripcionFormula;
                                    renglon[0].cells[1].innerHTML = dato.CantidadProducida;
                                    renglon[0].cells[2].innerHTML = dato.CantidadReparto;
                                    $("#dlgResumenProduccionFormulas").modal("show");
                                    $("#btnAceptarResumen").click(function() {
                                        location.href = location.href;
                                    });
                                }
                                else
                                {
                                    location.href = location.href;
                                }
                        }
                        else
                        {
                            location.href = location.href; 
                        }
                    },
                error: function (result)
                    {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                    }
            });
    }

//Guarda la produccion
GuadarProduccion = function () {
    var renglonesDiferencias = $("#tbIngredientesDiferencias tbody tr");
    var produccionFormulaDetalleLista = [];
    for (var i = 0; i < ingredientes; i++) {
        var cantidad = renglonesDiferencias[i].cells[3].innerHTML;
        cantidad = cantidad.replace(/,/g, '').replace(/_/g, '');
        if (parseFloat(cantidad) > 0) {
            var produccionFormulaDetalle = {
                "Ingrediente": { "IngredienteId": renglonesDiferencias[i].cells[0].innerHTML },
                "Producto": { "ProductoId": renglonesDiferencias[i].cells[1].innerHTML },
                "CantidadProducto": cantidad
            };
            produccionFormulaDetalleLista.push(produccionFormulaDetalle);
        } else {
            produccionFormulaDetalleLista = [];
            break;
        }
    }

    if (produccionFormulaDetalleLista.length > 0) {
        App.bloquearContenedor($(".container-fluid"));
        fecha = $('#txtFecha').val();
        //fecha = $('#txtFecha').datepicker({ dateFormat: 'dd-mm-yy' }).val();
        //var produccionFormula = { "FechaProduccion": fecha, "Formula": { "FormulaId": $("#cmbFormula").val() }, "CantidadProducida": parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')).toFixed(2), "ProduccionFormulaDetalle": produccionFormulaDetalleLista, "RotoMixID": document.getElementById("cmbRotoMix").value, Batch: document.getElementById("txtBatch").value };
        var produccionFormula = { "fecha": fecha, "FormulaId": $("#cmbFormula").val(), "CantidadProducida": parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')).toFixed(2), "ProduccionFormulaDetalle": produccionFormulaDetalleLista, "RotoMixID": document.getElementById("cmbRotoMix").value, Batch: document.getElementById("txtBatch").value };
        //var produccionFormula = { "Formula": { "FormulaId": $("#cmbFormula").val() }, "CantidadProducida": parseFloat($("#txtKilogramosProducidos").val().replace(/,/g, '').replace(/_/g, '')).toFixed(2), "ProduccionFormulaDetalle": produccionFormulaDetalleLista, "RotoMixID": document.getElementById("cmbRotoMix").value, Batch: document.getElementById("txtBatch").value };
        //var datos = { "produccionFormula": produccionFormula };
        $.ajax({
            type: "POST",
            url: "ProduccionFormulas.aspx/GuardarProduccionFormula",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(produccionFormula),
            error: function (request) {
                App.desbloquearContenedor($(".container-fluid"));
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                App.desbloquearContenedor($(".container-fluid"));
                if (data.d) {
                    var datos = data.d;
                    if (datos.ProduccionFormulaId > 0) {
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert("<img src='../Images/Correct.png'/>Folio: " + datos.FolioFormula + ". " + window.msgDatosGuardados, function () {
                                cargarTablaResumen();
                                //location.href = location.href;
                            });
                        }
                    } else {
                        if (datos.ProduccionFormulaId == -1) {
                            var cadenaProductos = "";
                            var contador = 0;
                            for (var i = 0; i < datos.ProduccionFormulaDetalle.length; i++) {
                                if (datos.ProduccionFormulaDetalle[i].CantidadProducto < 0) {
                                    if (contador > 0) {
                                        cadenaProductos += ",";
                                    }
                                    cadenaProductos += " " + datos.ProduccionFormulaDetalle[i].Producto.Descripcion;
                                    contador++;
                                }
                            }
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgSinInventarioProductos + cadenaProductos + window.msgSinInventarioProductosVerificar, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (datos.ProduccionFormulaId == -2) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgSinAlmacen, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (datos.ProduccionFormulaId == -3) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgSinAlmacen, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (datos.ProduccionFormulaId == -4) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgErrorGrabar, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                            //ERROR AL GENERAR LA POLIZA
                        else if (datos.ProduccionFormulaId == -5) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(datos.MensajePolizas, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }

                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgErrorGrabar, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        });
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.lblMensajeCapturarKilogramos, function () {
                msjAbierto = 0;
            });
        }
    }
};