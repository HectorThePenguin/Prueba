var corralAnterior = 0;
var msjAbierto = 0;
var contador = 0;
var proceso = 0;//Se usa en el boton agregar 0 = Agregar, 1 = Actualizar
var estadoComedero = 0;
var formulaTardeAnterior = 0;
var guardarManiana = 0;
var estadoComederoAnterior = 0;
var reparto = 0;
var fechaSeleccionada = "";
$(document).ready(function () {
    $("#txtCorral").focus();
    $("#txtLote").attr("disabled", true);
    $("#txtCabezas").attr("disabled", true);
    $("#txtTipoProceso").attr("disabled", true);

    $("#txtCorral").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    $("#txtKilogramos").inputmask({ "mask": "9", "repeat": 10, "greedy": false });

    for (var i = 0; i < 6; i++) {
        $('#tbRegistros tbody').append('<tr>' +
                                        ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                        ' <td style="display:none;">&nbsp;</td>' +
                                     '</tr>');
    }

    $("#txtCorral").keydown(function (e) {
        // Limpiamos todos los controles al teclear algo nuevo
        $("#txtLote").val("");
        $("#txtCabezas").val("");
        $("#txtTipoProceso").val("");
        $("#txtKilogramosOculto").val(0);
        $("#txtRepartoID").val(0);
        $("#txtKilogramos").val("");
        $("#txtObservaciones").val("");
        $("#cmbFormulaManiana").val(0);
        $("#txtRepartoDetalleIdManiana").val(0);
        $("#txtCantidadServidaManiana").val(0);
        $("#cmbFormulaTarde").val(0);
        $("#txtRepartoDetalleIdTarde").val(0);
        $("#txtServidoManiana").val(0);
        $("#txtServidoTarde").val(0);
        $("#cmbEstadoComedero").val(0);
        $("#txtKilogramosOculto").val(0);
        $("#txtRepartoID").val(0);
        $("#cmbFormulaManiana").attr("disabled", false);
        $("#cmbFormulaTarde").attr("disabled", false);
        $("#cmbEstadoComedero").attr("disabled", false);
        corralAnterior = 0;
        proceso = 0;
        
        if ($("#checkSoloFormula:checked").length > 0) {
            $("#cmbEstadoComedero").attr("disabled", "disabled");
            $("#txtKilogramos").attr("disabled", "disabled");
            $("#txtObservaciones").attr("disabled", "disabled");
        }
        
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarCorral();
        } else if (code == 9) {
            if (ValidarCorral() != true) {
                e.preventDefault();
            }
        }
    });

    $('#txtObservaciones').keydown(function (e) {
        if (!SoloNumeroLetras(e)) {
            e.preventDefault();
        }
    });

    $("#cmbFormulaManiana").focusin(function () {
        ValidarCorral();
    });

    $("#cmbFormulaTarde").focusin(function () {
        ValidarCorral();
    });

    $("#cmbEstadoComedero").focusin(function () {
        ValidarCorral();
    });

    $("#txtKilogramos").focusin(function () {
        ValidarCorral();
    });

    $("#txtObservaciones").focusin(function () {
        ValidarCorral();
    });

    $(function () {
        $("#datepicker").datepicker({
            firstDay: 1,
            showOn: 'button',
            buttonImage: '../assets/img/calander.png',
            onSelect: function (date) {

                if (ValidarFecha()) {
                    fechaSeleccionada = date;
                    $('#txtCorral').val('');
                    //ValidarCorral();
                } else {
                    //Mensaje campo corral vacio
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert("La fecha seleccionada debe ser entre hoy y mañana", function () {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                        InicializarFecha();
                    }
                }

            },
        });
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });
    
    $("#txtCorral").change(function () {
        if (corralAnterior != 0 && $("#txtCorral").val() != corralAnterior) {
            $("#txtLote").val("");
            $("#txtCabezas").val("");
            $("#txtTipoProceso").val("");
            $("#cmbFormulaManiana").val(0);
            $("#txtRepartoDetalleIdManiana").val(0);
            $("#txtCantidadServidaManiana").val(0);
            $("#cmbFormulaTarde").val(0);
            $("#txtRepartoDetalleIdTarde").val(0);
            $("#txtServidoManiana").val(0);
            $("#txtServidoTarde").val(0);
            $("#cmbEstadoComedero").val(0);
            $("#txtKilogramosOculto").val(0);
            $("#txtRepartoID").val(0);
            $("#txtKilogramos").val("");
            $("#txtObservaciones").val("");
        }
    });

    $("#txtKilogramos").change(function () {
        ObtenerEstadoComederoPorKilogramos();
    });

    $("#txtKilogramos").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $('#txtObservaciones').keyup(function () {
        if ($('#txtObservaciones').val().length >= 254) {
            var texto = $('#txtObservaciones').val();
            $('#txtObservaciones').val(texto.substring(0, 255));
        }
    });

    $("#cmbEstadoComedero").change(function () {
        ObtenerKilogramosCalculados();
    });

    $("#btnLimpiar").click(function () {
        LimpiarCampos();
        InicializarFecha();
    });

    $("#btnAgregar").click(function () {
        if($(this).attr('disabled'))
        {
            return;
        }
        if (VerificarCampos()) {
            if (proceso == 0) {
                AgregarRegistro();
            } else {
                ActualizarRegistro();
            }
        }
    });

    $("#btnGuardar").click(function () {
        if (contador > 0) {
            GuardarCambios();
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgDatosBlanco, function () {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
        }
    });

    $("#btnDialogoSi").click(function () {
        location.href = location.href;
    });

    $("#checkSoloFormula").click(function () {
        //if (this.checked) {
        //    $("#cmbEstadoComedero").attr("disabled", "disabled");
        //    $("#txtKilogramos").attr("disabled", "disabled");
        //    $("#txtObservaciones").attr("disabled", "disabled");
        //    $("#checkSoloFormula").attr("disabled", "disabled");
        //}
        if (this.checked) {
            $("#checkSoloFormula").attr("disabled", "disabled");
        }
        LimpiarCampos();
        InicializarFecha();
    });
    
    InicializarFecha();
});

ValidarFecha = function () {
    var fechaManiana = mostrarFecha(1);
    var fechaAyer = mostrarFecha(-1);
    var fechaDatapicker = new Date($('#datepicker').datepicker('getDate'));
    //alert("Ayer: " + fechaAyer + "**** fechaManiana: " + fechaManiana + "**** fechaDatapicker: " + fechaDatapicker);
    if (fechaDatapicker > fechaAyer  && fechaDatapicker <= fechaManiana) {
        return true;
    }
    return false;
};

function mostrarFecha(days) {
    fecha = new Date();
    day = fecha.getDate();
    month = fecha.getMonth() + 1;
    year = fecha.getFullYear();

    //document.write("Fecha actual: " + day + "/" + month + "/" + year);

    tiempo = fecha.getTime();
    milisegundos = parseInt(days * 24 * 60 * 60 * 1000);
    total = fecha.setTime(tiempo + milisegundos);
    day = fecha.getDate();
    month = fecha.getMonth() + 1;
    year = fecha.getFullYear();
    return fecha;
    //document.write("Fecha modificada: " + day + "/" + month + "/" + year);
}

InicializarFecha = function () {
    var fecha = new Date();
    fechaSeleccionada = FechaFormateada(fecha);

    $("#datepicker").val(fechaSeleccionada);
};

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

LimpiarCampos = function() {
    $("#txtCorral").attr("disabled", false);
    $("#txtCorral").val("");
    $("#txtLote").val("");
    $("#txtCabezas").val("");
    $("#txtTipoProceso").val("");
    $("#cmbFormulaManiana").val(0);
    $("#txtRepartoDetalleIdManiana").val(0);
    $("#txtCantidadServidaManiana").val(0);
    $("#cmbFormulaTarde").val(0);
    $("#txtRepartoDetalleIdTarde").val(0);
    $("#txtServidoManiana").val(0);
    $("#txtServidoTarde").val(0);
    $("#cmbEstadoComedero").val(0);
    $("#txtKilogramos").val("");
    $("#txtKilogramosOculto").val(0);
    $("#txtRepartoID").val(0);
    $("#txtObservaciones").val("");
    $("#cmbFormulaManiana").attr("disabled", false);
    $("#cmbFormulaTarde").attr("disabled", false);
    $("#cmbEstadoComedero").attr("disabled", false);
    $("#txtKilogramos").attr("disabled", false);
    $("#txtObservaciones").attr("disabled", false);
    $("#txtCorral").focus();
    $("#btnAgregar").html("Agregar");
    $("#btnAgregar").attr("disabled", false);
    corralAnterior = 0;
    proceso = 0;
    
    if ($("#checkSoloFormula:checked").length > 0) {
        $("#cmbEstadoComedero").attr("disabled", "disabled");
        $("#txtKilogramos").attr("disabled", "disabled");
        $("#txtObservaciones").attr("disabled", "disabled");
    }
};

VerificarCampos = function () {
    


    if ($("#txtCorral").val() != "") {
        if ($("#txtCabezas").val() != "") {
            if ($("#txtKilogramos").val() != "") {
                //Validar que los kilos de ajuste no sean menores a los programados de la maniana
                var kilosAjuste = TryParseInt($("#txtKilogramos").val(), 0);
                if (kilosAjuste > 0) {
                    var kilosProgramadosMatutino = TryParseInt($("#txtProgramadoManiana").val(), 0);
                    if (kilosAjuste < kilosProgramadosMatutino) {
                        bootbox.alert(window.msgKilosAjusteMenorMatutino + kilosProgramadosMatutino, function () {
                            setTimeout(function () { $("#txtKilogramos").focus(); }, 500);
                        });
                        return false;
                    }
                }

                if ($("#cmbFormulaManiana").val() != "") {
                    if ($("#cmbFormulaTarde").val() != "") {
                        if ($("#cmbEstadoComedero").val() != "") {
                            if (parseInt($("#txtCantidadServidaManiana").val()) > 0 && parseInt($("#txtKilogramos").val()) > 0) {
                                if (parseInt($("#txtCantidadServidaManiana").val()) > parseInt($('#txtKilogramos').val())) {
                                    bootbox.alert(window.MensajeCantidadMenorAProgramada, function() {
                                        setTimeout(function() { $("#txtKilogramos").focus(); }, 500);
                                    });
                                } else {
                                    return true;
                                }
                            } else {
                                return true;
                            }
                        } else {
                            //Mensaje campo estado de comedero
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgIngresarComedero, function() {
                                    $("#cmbEstadoComedero").focus();
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                        }
                    } else {
                        //Mensaje campo formula tarde
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgIngresarFormulaTarde, function() {
                                $("#cmbFormulaTarde").focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    }
                } else {
                    //Mensaje campo formula mañana
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgIngresarFormulaManiana, function() {
                            $("#cmbFormulaManiana").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                }
            } else {
                //Mensaje campo kilogramos vacio
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(msgIngresarKilogramos, function() {
                        $("#txtKilogramos").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            }
        } else {
            //Mensaje campo cabezas vacio
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgIngresarCabezas, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        }
    } else {
        //Mensaje campo corral vacio
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(msgIngresarCorral, function() {
                $("#txtCorral").focus();
                msjAbierto = 0;
            });
        }
        return false;
    }
};

AgregarRegistro = function () {

    if ($("#checkSoloFormula:checked").length == 0) {
        $("#checkSoloFormula").attr("disabled", "disabled");
    }
    
    var encontrado = 0;
    if (contador >= 6) {
        $('#tbRegistros tbody').append('<tr>' +
            ' <td class="alineacionCentro span1" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td class="alineacionCentro span1" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td class="alineacionCentro span1" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td class="alineacionCentro span1" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td class="alineacionCentro span1" style="word-wrap: break-word;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            ' <td style="display:none;">&nbsp;</td>' +
            '</tr>');
    }
    var renglones = $("#tbRegistros tbody tr");
    if (renglones != null) {
        $(renglones).each(function() {
            if ($(this)[0].cells[0].innerHTML == $("#txtCorral").val().toUpperCase()) {
                encontrado = 1;
            }
        });

        if (encontrado == 0) {
            renglones[contador].id = $("#txtCorral").val();
            renglones[contador].cells[0].innerHTML = $("#txtCorral").val().toUpperCase();
            renglones[contador].cells[1].innerHTML = $("#txtLote").val().toUpperCase();
            renglones[contador].cells[2].innerHTML = $("#txtCabezas").val();
            renglones[contador].cells[3].innerHTML = $("#txtTipoProceso").val().toUpperCase();
            renglones[contador].cells[4].innerHTML = $("#cmbFormulaManiana").val();
            renglones[contador].cells[5].innerHTML = $("#cmbFormulaManiana option:selected").text().toUpperCase();
            renglones[contador].cells[6].innerHTML = $("#cmbFormulaTarde").val();
            renglones[contador].cells[7].innerHTML = $("#cmbFormulaTarde option:selected").text().toUpperCase();
            renglones[contador].cells[8].innerHTML = $("#cmbEstadoComedero").val();
            renglones[contador].cells[9].innerHTML = $("#cmbEstadoComedero option:selected").text().toUpperCase();
            renglones[contador].cells[10].innerHTML = $("#txtKilogramosOculto").val();
            renglones[contador].cells[11].innerHTML = $("#txtKilogramos").val();
            renglones[contador].cells[12].innerHTML = $("#txtObservaciones").val().toUpperCase();
            renglones[contador].cells[13].innerHTML = "<label class='span12 alineacionCentro'><img src='../Images/lapiz_editar.png'/ width='20' onclick='CargarRegistro(\"" + $("#txtCorral").val() + "\");'><img src='../Images/cross-icon.png'/ width='20' onclick='EliminarRegistro(\"" + $("#txtCorral").val() + "\");'></label>";

            if (reparto == 1) {
                renglones[contador].cells[14].innerHTML = "COMPLETO";
            } else {
                if (guardarManiana == 1) {
                    renglones[contador].cells[14].innerHTML = "MANIANA";
                } else {
                    renglones[contador].cells[14].innerHTML = "TARDE";
                }
            }

            renglones[contador].cells[15].innerHTML = $("#txtRepartoID").val();
            renglones[contador].cells[16].innerHTML = $("#txtRepartoDetalleIdManiana").val();
            renglones[contador].cells[17].innerHTML = $("#txtRepartoDetalleIdTarde").val();
            renglones[contador].cells[18].innerHTML = $("#txtServidoManiana").val();
            renglones[contador].cells[19].innerHTML = $("#txtServidoTarde").val();
            renglones[contador].cells[20].innerHTML = $("#txtCantidadServidaManiana").val();
            LimpiarCampos();
            contador++;
        } else {
            //Mensaje el corral ya se encuentra capturado
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgCorralCapturado, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
        }
    }
};

ActualizarRegistro = function() {
    var renglones = $("#tbRegistros tbody tr");
    if (renglones != null) {
        $(renglones).each(function() {
            if (this.cells[0].innerHTML == $("#txtCorral").val()) {
                this.cells[1].innerHTML = $("#txtLote").val().toUpperCase();
                this.cells[2].innerHTML = $("#txtCabezas").val();
                this.cells[3].innerHTML = $("#txtTipoProceso").val().toUpperCase();
                this.cells[4].innerHTML = $("#cmbFormulaManiana").val();
                this.cells[5].innerHTML = $("#cmbFormulaManiana option:selected").text().toUpperCase();
                this.cells[6].innerHTML = $("#cmbFormulaTarde").val();
                this.cells[7].innerHTML = $("#cmbFormulaTarde option:selected").text().toUpperCase();
                this.cells[8].innerHTML = $("#cmbEstadoComedero").val();
                this.cells[9].innerHTML = $("#cmbEstadoComedero option:selected").text().toUpperCase();
                this.cells[10].innerHTML = $("#txtKilogramosOculto").val();
                this.cells[11].innerHTML = $("#txtKilogramos").val();
                this.cells[12].innerHTML = $("#txtObservaciones").val().toUpperCase();
                this.cells[15].innerHTML = $("#txtRepartoID").val();
                this.cells[16].innerHTML = $("#txtRepartoDetalleIdManiana").val();
                this.cells[17].innerHTML = $("#txtRepartoDetalleIdTarde").val();
                this.cells[18].innerHTML = $("#txtServidoManiana").val();
                this.cells[19].innerHTML = $("#txtServidoTarde").val();
                this.cells[20].innerHTML = $("#txtCantidadServidaManiana").val();
                LimpiarCampos();
            }
        });
    }
};

EliminarRegistro = function(corral) {
    var registro = $('#' + corral);
    bootbox.dialog({
        message: msgEliminarRegistro,
        buttons: {
            success: {
                label: "Si",
                className: "btn SuKarne",
                callback: function() {
                    LimpiarCampos();
                    registro.remove();
                    contador--;
                    if (contador < 7) {
                        $('#tbRegistros tbody').append('<tr>' +
                            ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td class="alineacionCentro span2" style="word-wrap: break-word;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td class="alineacionCentro span2" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td class="alineacionCentro span1" style="word-wrap: break-word;" scope="col">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            ' <td style="display:none;">&nbsp;</td>' +
                            '</tr>');
                    }
                    
                    var contadorRenglones = 0;
                    $("#tbRegistros tbody tr").each(function() {
                        if ($(this)[0].cells[0].innerHTML != "" && $(this)[0].cells[0].innerHTML != "&nbsp;") {
                            contadorRenglones++;
                        }
                    });
                    
                    if (contadorRenglones == 0) {
                        LimpiarCampos();
                    }
                }
            },
            danger: {
                label: "No",
                className: "btn SuKarne",
                callback: function() {
                    ;
                }
            }
        }
    });
};

CargarRegistro = function(corral) {
    var registro = $('#' + corral);
    if (registro) {
        proceso = 1;
        $("#txtCorral").attr("disabled", true);
        $("#txtCorral").val(registro[0].cells[0].innerHTML);
        corralAnterior = registro[0].cells[0].innerHTML;
        $("#txtLote").val(registro[0].cells[1].innerHTML);
        $("#txtCabezas").val(registro[0].cells[2].innerHTML);
        $("#txtTipoProceso").val(registro[0].cells[3].innerHTML);
        $("#cmbFormulaManiana").val(registro[0].cells[4].innerHTML);
        $("#cmbFormulaTarde").val(registro[0].cells[6].innerHTML);
        $("#cmbEstadoComedero").val(registro[0].cells[8].innerHTML);
        $("#txtKilogramosOculto").val(registro[0].cells[10].innerHTML);
        $("#txtKilogramos").val(registro[0].cells[11].innerHTML);
        $("#txtObservaciones").val(registro[0].cells[12].innerHTML);
        $("#txtRepartoID").val(registro[0].cells[15].innerHTML);
        $("#txtRepartoDetalleIdManiana").val(registro[0].cells[16].innerHTML);
        $("#txtRepartoDetalleIdTarde").val(registro[0].cells[17].innerHTML);
        $("#txtServidoManiana").val(registro[0].cells[18].innerHTML);
        $("#txtServidoTarde").val(registro[0].cells[19].innerHTML);
        $("#txtCantidadServidaManiana").val(registro[0].cells[20].innerHTML);

        $("#btnAgregar").html("Actualizar");
    }
};

//Validaciones para el corral
ValidarCorral = function() {
    if (corralAnterior != $("#txtCorral").val() || corralAnterior == 0) {
        if ($("#txtCorral").val() == "") {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgIngresarCorral, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        } else {
            var datos = { "corralCodigo": $('#txtCorral').val() };
            App.bloquearContenedor($(".container-fluid"));
            $.ajax({
                type: "POST",
                url: "ConfiguracionAjustes.aspx/ObtenerCorral",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    App.desbloquearContenedor($(".container-fluid"));
                    $("#txtLote").val("");
                    $("#txtCabezas").val("");
                    $("#txtTipoProceso").val("");
                    $("#txtKilogramosOculto").val(0);
                    $("#txtRepartoID").val(0);
                    $("#txtKilogramos").val("");
                    $("#txtObservaciones").val("");
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(request.Message, function() {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == null) {
                        App.desbloquearContenedor($(".container-fluid"));
                        $("#txtLote").val("");
                        $("#txtCabezas").val("");
                        $("#txtTipoProceso").val("");
                        $("#txtKilogramosOculto").val(0);
                        $("#txtRepartoID").val(0);
                        $("#txtKilogramos").val("");
                        $("#txtObservaciones").val("");
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgCorralNoExiste, function() {
                                $("#txtCorral").focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    } else {
                        var datos = { "corralID": data.d.CorralID };
                        ObtenerLoteCorral(datos);
                        return true;
                    }
                }
            });
        }
    } else {
        return true;
    }
};

//Funcion que obtiene el lote del corral
ObtenerLoteCorral = function(datos) {
    try {
        $.ajax({
            type: "POST",
            url: "ConfiguracionAjustes.aspx/ObtenerLotesCorral",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function(request) {
                App.desbloquearContenedor($(".container-fluid"));
                $("#txtLote").val("");
                $("#txtCabezas").val("");
                $("#txtTipoProceso").val("");
                $("#txtKilogramosOculto").val(0);
                $("#txtRepartoID").val(0);
                $("#txtKilogramos").val("");
                $("#txtObservaciones").val("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function() {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            },
            dataType: "json",
            success: function(data) {
                if (data.d == null) {
                    App.desbloquearContenedor($(".container-fluid"));
                    $("#txtLote").val("");
                    $("#txtCabezas").val("");
                    $("#txtTipoProceso").val("");
                    $("#txtKilogramosOculto").val(0);
                    $("#txtRepartoID").val(0);
                    $("#txtKilogramos").val("");
                    $("#txtObservaciones").val("");
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgNoTieneLoteActivo, function() {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                } else {
                    corralAnterior = $("#txtCorral").val();
                    $("#txtLote").val(data.d.Lote);
                    $("#txtCabezas").val(data.d.Cabezas);
                    if (data.d.TipoProcesoID > 0) {
                        ObtenerTipoProceso(data.d.TipoProcesoID);
                    }
                    ObtenerOrdenReparto(data.d.LoteID, data.d.CorralID);
                    return true;
                }
            }
        });
    } catch(err) {
        return false;
    }
};

//Funcion que obtiene el tipo proceso
ObtenerTipoProceso = function(tipoProcesoID) {
    var datos = { "tipoProcesoID": tipoProcesoID };
    try {
        $.ajax({
            type: "POST",
            url: "ConfiguracionAjustes.aspx/ObtenerTipoProceso",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function(request) {
                $("#txtLote").val("");
                $("#txtCabezas").val("");
                $("#txtTipoProceso").val("");
                $("#txtKilogramosOculto").val(0);
                $("#txtRepartoID").val(0);
                $("#txtKilogramos").val("");
                $("#txtObservaciones").val("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function() {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            },
            dataType: "json",
            success: function(data) {
                if (data.d) {
                    $("#txtTipoProceso").val(data.d.Descripcion);
                }
            }
        });
    } catch(err) {
        return false;
    }
};

//Funcion que obtiene el lote del corral
ObtenerOrdenReparto = function(loteID, corralID) {
    datos = { "loteID": loteID, "corralID": corralID, "fechaReparto": fechaSeleccionada };

    try {
        
        $.ajax({
            type: "POST",
            url: "ConfiguracionAjustes.aspx/ObtenerOrdenReparto",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                App.desbloquearContenedor($(".container-fluid"));
                $("#txtLote").val("");
                $("#txtRepartoID").val(0);
                $("#txtCabezas").val("");
                $("#txtKilogramosOculto").val(0);
                $("#txtRepartoID").val(0);
                $("#txtKilogramos").val("");
                $("#txtObservaciones").val("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function() {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            },
            dataType: "json",
            success: function(data) {
                if (data.d == null) {
                    App.desbloquearContenedor($(".container-fluid"));
                    reparto = 1;
                    $("#txtRepartoID").val(0);
                    $("#cmbFormulaManiana").attr("disabled", false);
                    $("#cmbFormulaTarde").attr("disabled", false);
                    $("#cmbEstadoComedero").attr("disabled", false);
                    return true;
                } else {
                    App.desbloquearContenedor($(".container-fluid"));
                    $("#txtRepartoID").val(data.d.RepartoID);
                    $('#txtProgramadoManiana').val(data.d.CantidadProgramadaManiana);

                    if (data.d.TotalRepartos == 0) { // No se a servido ningun servicio
                        $("#cmbFormulaManiana").attr("disabled", false);
                        $("#cmbFormulaTarde").attr("disabled", false);
                        $("#btnAgregar").attr("disabled", false);
                        reparto = 1;
                        
                        if ($("#checkSoloFormula:checked").length > 0) {
                            bootbox.alert(window.MensajeCorralNoTieneProgramacion, function() {
                                LimpiarCampos();
                            });
                        }
                        return true;
                        //estadoComedero = 1;
                        //guardarManiana = 0;
                    } else {
                        if (data.d.TotalRepartos == 1) { // Ya se sirvio el servicio de la mañana
                            $("#cmbFormulaManiana").attr("disabled", true);
                            $("#cmbFormulaTarde").attr("disabled", false);
                            $("#txtServidoManiana").val(1);
                            $("#txtServidoTarde").val(0);
                            $("#btnAgregar").attr("disabled", false);
                            $("#cmbEstadoComedero").attr("disabled", true);
                            guardarManiana = 0; //Solo se actualiza el de la tarde
                            //estadoComedero = 1;
                        } else if (data.d.TotalRepartos == 2) { // Ya se sirvio el servicio de la tarde
                            $("#cmbFormulaManiana").attr("disabled", true);
                            $("#cmbFormulaTarde").attr("disabled", true);
                            $("#txtServidoManiana").val(1);
                            $("#txtServidoTarde").val(1);
                            $("#btnAgregar").attr("disabled", true);
                            $("#txtKilogramos").attr("disabled", false);
                            $("#cmbEstadoComedero").attr("disabled", true);
                            guardarManiana = 1; //Actualizar Ambos

                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert("Se encuentran servidos los servicios de este corral. Para el día seleccionado. ", function() {
                                    $("#txtCorral").focus();
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                            //estadoComedero = 0;
                        } else if (data.d.TotalRepartos == 3) { // Programados pero no servidos
                            $("#cmbFormulaManiana").attr("disabled", false);
                            $("#cmbFormulaTarde").attr("disabled", false);
                            $("#txtServidoManiana").val(0);
                            $("#txtServidoTarde").val(0);
                            $("#btnAgregar").attr("disabled", false);
                            reparto = 1;
                            //guardarManiana = 1;//Actualizar Ambos
                            //estadoComedero = 0;
                        }
                        estadoComedero = 1;
                        
                        if (data.d.DetalleReparto) {
                            $(data.d.DetalleReparto).each(function() {
                                if ($(this)[0].TipoServicioID == 1) {
                                    $("#txtRepartoDetalleIdManiana").val($(this)[0].RepartoDetalleID);
                                    $("#txtCantidadServidaManiana").val($(this)[0].CantidadServida);
                                    $("#cmbFormulaManiana").val($(this)[0].FormulaIDProgramada);
                                    if (estadoComedero > 0) {
                                        $("#cmbEstadoComedero").val($(this)[0].EstadoComederoID);
                                        estadoComederoAnterior = $(this)[0].EstadoComederoID;
                                    }
                                }
                                if ($(this)[0].TipoServicioID == 2) {
                                    $("#txtRepartoDetalleIdTarde").val($(this)[0].RepartoDetalleID);
                                    $("#cmbFormulaTarde").val($(this)[0].FormulaIDProgramada);
                                    formulaTardeAnterior = $(this)[0].FormulaIDProgramada;
                                }
                            });
                        }

                        if (loteID > 0) {
                            ObtenerKilogramos(loteID);
                        } else {
                            $("#txtKilogramos").val(data.d.CantidadPedido);
                            $("#txtKilogramosOculto").val(data.d.CantidadPedido);
                        }
                        
                        if ($("#checkSoloFormula:checked").length > 0) {
                            if ($.trim($("#txtKilogramos").val()) == "") {
                                $("#txtKilogramos").val("0");
                            }
                            $("#cmbEstadoComedero").attr("disabled", "disabled");
                            $("#txtKilogramos").attr("disabled", "disabled");
                            $("#txtObservaciones").attr("disabled", "disabled");
                        }
                    }
                    return true;
                }
            }
        });
    } catch(err) {
        return false;
    }
};

//Obtiene los kilogramos programados
ObtenerKilogramos = function(LoteID) {

    var datos = { "loteID": LoteID };
    $.ajax({
        type: "POST",
        url: "ConfiguracionAjustes.aspx/ObtenerKilogramosProgramados",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            $("#txtLote").val("");
            $("#txtCabezas").val("");
            $("#txtKilogramosOculto").val(0);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                $("#txtKilogramos").val(data.d.CantidadPedido);
                $("#txtKilogramosOculto").val(data.d.CantidadPedido);
                if (data.d.CambioFormula) {
                    $("#cmbFormulaTarde").attr("disabled", true);
                    $("#cmbEstadoComedero").attr("disabled", true);
                    $("#cmbEstadoComedero").val(estadoComederoAnterior);
                } else {
                    if (!guardarManiana) {
                        $("#cmbFormulaTarde").attr("disabled", false);
                    }
                    $("#cmbEstadoComedero").attr("disabled", false);
                    ConsultarConfiguracionFormula(LoteID);
                    ObtenerHabilitarEstadoComedero(LoteID);
                }
            }
        }
    });
};

//Obtiene la formula para el corral dependiendo de la configuracion
ConsultarConfiguracionFormula = function(LoteID) {
    var datos = { "LoteID": LoteID };
    $.ajax({
        type: "POST",
        url: "ConfiguracionAjustes.aspx/ConsultarConfiguracionFormula",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            $("#txtLote").val("");
            $("#txtCabezas").val("");
            $("#txtKilogramosOculto").val(0);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                datos = data.d;
                var encontro = 0;
                for (var i = 0; i < datos.length; i++) {
                    if (datos[i].Formula.FormulaID == $("#cmbFormulaTarde").val() && encontro == 0) {
                        encontro = 1;
                    }
                }
                if (encontro == 0) {
                    $("#cmbFormulaTarde").val(formulaTardeAnterior);
                }
            } else {
                $("#cmbFormulaTarde").val(formulaTardeAnterior);
            }
        }
    });
};

//Obtiene si el corral tiene orden sacrificio, va a zilmax o a reimplante, para deshabilitar el estado de comedero
ObtenerHabilitarEstadoComedero = function(LoteID) {
    var datos = { "loteID": LoteID };
    $.ajax({
        type: "POST",
        url: "ConfiguracionAjustes.aspx/ObtenerHabilitarEstadoComedero",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            $("#txtLote").val("");
            $("#txtCabezas").val("");
            $("#txtKilogramosOculto").val(0);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function() {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        },
        dataType: "json",
        success: function(data) {
            if (data.d == 1) {
                $("#cmbEstadoComedero").attr("disabled", false);
            } else {
                $("#cmbEstadoComedero").attr("disabled", true);
                $("#cmbEstadoComedero").val(estadoComederoAnterior);
            }
        }
    });
};

//Obtiene los kilogramos calculados de acuerdo al estado de comedero
ObtenerKilogramosCalculados = function() {
    if (reparto == 0) {
        if ($("#txtKilogramos").val() != "") {
            var datos = { "estadoComederoID": $("#cmbEstadoComedero").val(), "kilogramosProgramados": $("#txtKilogramosOculto").val() };
            $.ajax({
                type: "POST",
                url: "ConfiguracionAjustes.aspx/ObtenerKilogramosCalculados",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    $("#txtLote").val("");
                    $("#txtCabezas").val("");
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(request.Message, function() {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d != -1) {
                        $("#txtKilogramos").val(data.d);
                    }
                }
            });
        }
    }
};

ObtenerEstadoComederoPorKilogramos = function() {
    var cantidadValida = true;
    
    if (parseInt($("#txtCantidadServidaManiana").val()) > 0 && parseInt($("#txtKilogramos").val()) > 0) {
        if (parseInt($("#txtCantidadServidaManiana").val()) > parseInt($('#txtKilogramos').val())) {
            cantidadValida = false;
            bootbox.alert(window.MensajeCantidadMenorAProgramada, function() {
                setTimeout(function() { $("#txtKilogramos").focus(); }, 500);
            });
        }
    }
    if (cantidadValida) {
        if (reparto == 0) {
            var datos = { 'kilogramos': $('#txtKilogramos').val(), 'kilogramosProgramados': $('#txtKilogramosOculto').val() };
            $.ajax({
                type: "POST",
                url: "ConfiguracionAjustes.aspx/ObtenerEstadoPorKilogramos",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(request.Message, function() {
                            $("#txtKilogramos").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d != null) {
                        $("#cmbEstadoComedero").val(data.d.EstadoComederoID);
                    }
                }
            });
        }
    }
};

GuardarCambios = function() {
    var listaCambios = [];
    App.bloquearContenedor($(".container-fluid"));
    $("#tbRegistros tbody tr").each(function() {
        if ($(this)[0].cells[0].innerHTML != "" && $(this)[0].cells[0].innerHTML != "&nbsp;") {

            var codigoCorral = '';
            codigoCorral = $(this)[0].cells[0].innerHTML;

            var lote = 0;
            if ($(this)[0].cells[1].innerHTML != '') {
                lote = $(this)[0].cells[1].innerHTML;
            }

            var repartoID = 0;
            if ($(this)[0].cells[15].innerHTML != '') {
                repartoID = $(this)[0].cells[15].innerHTML;
            }
            var repartoDetalleIdManiana = 0;
            if ($(this)[0].cells[16].innerHTML != '') {
                repartoDetalleIdManiana = $(this)[0].cells[16].innerHTML;
            }

            var cambiosRepartoInfo = {};

            var corralInfo = {};
            corralInfo.Codigo = codigoCorral;
            cambiosRepartoInfo.CorralInfo = corralInfo;
            cambiosRepartoInfo.Lote = lote;
            cambiosRepartoInfo.TipoServicioID = 1; //Formula mañana
            cambiosRepartoInfo.FormulaIDProgramada = $(this)[0].cells[4].innerHTML;
            cambiosRepartoInfo.EstadoComederoID = $(this)[0].cells[8].innerHTML;
            cambiosRepartoInfo.CantidadProgramada = $(this)[0].cells[11].innerHTML;
            cambiosRepartoInfo.Observaciones = $(this)[0].cells[12].innerHTML;
            cambiosRepartoInfo.RepartoDetalleIdManiana = repartoDetalleIdManiana;
            cambiosRepartoInfo.Servido = $(this)[0].cells[18].innerHTML;
            cambiosRepartoInfo.RepartoID = repartoID;
            cambiosRepartoInfo.CantidadServida = $(this)[0].cells[20].innerHTML;
            cambiosRepartoInfo.CantidadProgramadaOriginal = $(this)[0].cells[10].innerHTML;
            

            listaCambios.push(cambiosRepartoInfo);

            var repartoDetalleIdTarde = 0;
            if ($(this)[0].cells[17].innerHTML != '') {
                repartoDetalleIdTarde = $(this)[0].cells[17].innerHTML;
            }

            cambiosRepartoInfo = {};
            cambiosRepartoInfo.CorralInfo = corralInfo;
            cambiosRepartoInfo.Lote = lote;
            cambiosRepartoInfo.TipoServicioID = 2; //Formula tarde
            cambiosRepartoInfo.FormulaIDProgramada = $(this)[0].cells[6].innerHTML;
            cambiosRepartoInfo.EstadoComederoID = $(this)[0].cells[8].innerHTML;
            cambiosRepartoInfo.CantidadProgramada = $(this)[0].cells[11].innerHTML;
            cambiosRepartoInfo.Observaciones = $(this)[0].cells[12].innerHTML;
            cambiosRepartoInfo.RepartoDetalleIdTarde = repartoDetalleIdTarde;
            cambiosRepartoInfo.Servido = $(this)[0].cells[19].innerHTML;
            cambiosRepartoInfo.RepartoID = repartoID;
            cambiosRepartoInfo.CantidadProgramadaOriginal = $(this)[0].cells[10].innerHTML;

            listaCambios.push(cambiosRepartoInfo);
        }
    });

    var datos = { "cambiosDetalle": listaCambios, "fechaReparto": fechaSeleccionada };
    var urlAjuste = "ConfiguracionAjustes.aspx/GuardarReparto";
    
    if ($("#checkSoloFormula:checked").length > 0) {
        urlAjuste = "ConfiguracionAjustes.aspx/GuardarSoloFormulas";
    }
    
    $.ajax({
        type: "POST",
        url: urlAjuste,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            App.desbloquearContenedor($(".container-fluid"));
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(request.Message, function() {
                    msjAbierto = 0;
                });
            }
            return false;
        },
        dataType: "json",
        success: function(data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d == 0) {
                bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.msgDatosGuardados, function() {
                    location.href = location.href;
                });
            } else {
                bootbox.alert(window.msgOcurrioErrorGrabar);
            }
        }
    });
};

//Funcion para validar letras y numeros
SoloNumeroLetras = function (event) {
    var charCode = (event.which) ? event.which : 0;
    if (charCode < 32 ||
        (charCode > 47 && charCode < 58) ||
        (charCode >= 65 && charCode <= 90) ||
        (charCode >= 97 && charCode <= 122) ||
        (charCode >= 48 && charCode <= 57) ||
        charCode == 44 || charCode == 46 || charCode == 32 ||
        charCode == 8 || charCode == 0 || charCode == 241 ||
        charCode == 209 || charCode == 193 || charCode == 201 ||
        charCode == 205 || charCode == 211 || charCode == 218 ||
        charCode == 225 || charCode == 233 || charCode == 237 ||
        charCode == 243 || charCode == 250 || charCode == 188 || charCode == 190 || (charCode >= 37 && charCode <= 40))
        return true;
    else {
        return false;
    }
};