
$(document).ready(function() {
    $("#txtKilogramos").inputmask({ "mask": "9", "repeat": 5, "greedy": false });
    $("#txtCorralProduccion").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    
    $("#txtFecha").attr("disabled", "disabled");
    $("#groupCorral").hide();

    var intervalID;
    
    $("#cmbTipoCorral").change(function() {
        if ($(this).val() != -1) {
            if ($(this).val() == 2) { // Si es de Produccion se hace captura manual
                $('#txtCorralProduccion').val("");
                $("#listaSeleccion").empty();
                $("#groupCorral").show();
                $("#txtCorralProduccion").focus();
            } else {
                $("#groupCorral").hide();
                ObtenerCorralesPorGrupo();
            }
        } else {
            $("#listaSeleccion").empty();
            $("#groupCorral").hide();
        }
    });
    
    $('#txtCorralProduccion').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $('#btnAgregarCorral').focus();
            e.preventDefault();
        }
    });

    $('#txtKilogramos').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $('#cmbFormulas').focus();
            e.preventDefault();
        }
    });
    
    $('#cmbFormulas').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            if ($('#cmbFormulas option:selected').text() == 'F1')
                $('#cmbTipoCorral').focus();
            else
                $('#cmbLotes').focus();
            e.preventDefault();
        }
    });
    
    $('#cmbLotes').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $('#cmbTipoCorral').focus();
            e.preventDefault();
        }
    });
    
    $("#cmbFormulas").change(function () {
        LimpiarLotes();
        if ($(this).val() != -1) {
            if ($('#cmbFormulas option:selected').text() == 'F1') {
                $('#cmbLotes').prop('disabled', true);
            }
            else {
                $('#cmbLotes').prop('disabled', false);
                ObtenerLotesProductos();
            }
         }
    });
    
    $('#cmbTipoCorral').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $("#btnSoloChecadosADerecha").focus();
            e.preventDefault();
        }
    });
    
    $('#btnAgregarCorral').click(function (e) {
        ValidarCorral();
    });

    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: window.mensajeCancelar,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        Limpiar();
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                    }
                }
            }
        });
    });
    
    $('#btnGuardar').click(function () {
        if ($("#listaSeleccionados li").length == 0) {
            bootbox.alert(window.sinCorralSeleccionado);
        } else {
            ValidarExistenciaInventarioFormulaGuardar($('#cmbFormulas').val());
        }
    });
    
    $("#btnSoloChecadosADerecha").click(function () {
        $("#listaSeleccion li").each(function () {
            if ($("#" + this.id + " input[type=checkbox]").attr("checked")) {
                $("#" + this.id + " input[type=checkbox]").removeAttr("checked");
                $("#listaSeleccionados").append(this.outerHTML);
                $(this).remove();
            }
        });
    });
    
    $("#btnTodosADerecha").click(function () {
        $("#listaSeleccion li").each(function () {
            $("#" + this.id + " input[type=checkbox]").removeAttr("checked");
            $("#listaSeleccionados").append(this.outerHTML);
            $(this).remove();
        });
    });
    
    $("#btnSoloChecadosAIzquierda").click(function () {
        $("#listaSeleccionados li").each(function () {
            if ($("#" + this.id + " input[type=checkbox]").attr("checked")) {
                var renglon = this;
                $(renglon.attributes).each(function () {
                    var atributo = this;
                    if (atributo.name == "idgrupocorral") {
                        if (atributo.value == $('#cmbTipoCorral').val()) {
                            
                            $("#" + renglon.id + " input[type=checkbox]").removeAttr("checked");
                            
                            if (atributo.value != 2) { // Si es de Produccion solo lo elimina de la lista
                                $("#listaSeleccion").append(renglon.outerHTML);
                            }
                            $(renglon).remove();
                        }
                    }
                });
            }
        });
    });
    
    $("#btnTodosAIzquierda").click(function () {
        $("#listaSeleccionados li").each(function() {
            var renglon = this;
            $(renglon.attributes).each(function() {
                var atributo = this;
                if (atributo.name == "idgrupocorral") {
                    if (atributo.value == $('#cmbTipoCorral').val()) {
                        $("#" + renglon.id + " input[type=checkbox]").removeAttr("checked");
                        if (atributo.value != 2) { // Si es de Produccion solo lo elimina de la lista
                            $("#listaSeleccion").append(renglon.outerHTML);
                        }
                        $(renglon).remove();
                    }
                }
            });
        });
    });
    
    // Guarda el Registro del consumo manual 
    var GuardarConsumoManual = function () {
        var datosConsumo = {};
        var listaDetectados = [];

        //if ($('#cmbFormulas').val() != -1 && $('#cmbLotes').val() != -1 && $.trim($('#txtKilogramos').val()) > 0 && $('#cmbTipoCorral').val() != -1) {
        if(ValidarDatos())
        {
            $("#listaSeleccionados li").each(function () {
                var info = {};
                info.CorralID = this.id;
                listaDetectados.push(info);
            });

            if (listaDetectados.length > 0)
            {
                $('.bar').css("width", "0%");
                $("#labelPorcentaje").html("");
                $('#responsive').modal('show');
                
                intervalID = setInterval(VerificarAvance, 250);

                var fecha = new Date(),
                hora = Right('00' + fecha.getHours(), 2),
                minutos = Right('00' + fecha.getMinutes(), 2);

                var horaMaquina = [hora, minutos].join(':');

                datosConsumo.FormulaId = $('#cmbFormulas').val();
                datosConsumo.KilogramosServidos = $('#txtKilogramos').val();
                datosConsumo.TipoCorralId = $('#cmbTipoCorral').val();
                datosConsumo.HoraSistema = horaMaquina;
                datosConsumo.LoteId = $('#cmbLotes').val();
                
                var datos = { 'corrales': listaDetectados, 'parametroConsumo': datosConsumo };
                $.ajax({
                    type: "POST",
                    url: "CapturaManualDeConsumo.aspx/GenerarOrdenRepartoManual",
                    data: JSON.stringify(datos),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    error: function (request) {
                        clearInterval(intervalID);
                        bootbox.alert(request.Message);
                    },
                    success: function (data) {
                        clearInterval(intervalID);
                        VerificarAvance();
                        if (data.d) {
                            if (data.d.Resultado) {
                                $('#responsive').modal('hide');

                                bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeExito);
                                Limpiar();
                            } else {
                                bootbox.alert(window.mensajeErrorGenerarConsumo);
                                $('.bar').css("width", "0%");
                                $("#labelPorcentaje").text("");
                                $('#responsive').modal('hide');
                            }
                        }
                    }
                });
            } else {
                bootbox.alert(window.mensajeNoHaCapturadoCorrales);
            }
        }
    };

    var ValidarDatos = function () {
        var valido = true;
        if ($.trim($('#txtKilogramos').val()) == 0 || $.trim($('#txtKilogramos').val()) == "") {
            bootbox.alert(window.mensajeIngresarKilogramosServidos, function () {
                $("#txtKilogramos").focus();
            });
            valido = false;
        } else if ($('#cmbFormulas').val() == -1) {
            bootbox.alert(window.mensajeSeleccionarFormula, function () {
                $("#cmbFormulas").focus();
            });
            valido = false;
        } else if ($('#cmbFormulas option:selected').text() != "F1" && $("#cmbLotes").val() == -1) {
            bootbox.alert(window.mensajeSeleccionarLote, function () {
                $("#cmbLotes").focus();
            });
            valido = false;
        } else if ($('#cmbTipoCorral').val() == -1) {
            bootbox.alert(window.mensajeSeleccionarTipoCorral, function () {
                $("#cmbTipoCorral").focus();
            });
            valido = false;
        }
        return valido;
    };

    // Valida el corral tecleado por el usuario.
    var ValidarCorral = function () {

        if ($.trim($('#txtCorralProduccion').val()) == "") {
            bootbox.alert(window.mensajeCapturarCorral, function () {
                $("#txtCorralProduccion").focus();
            });
        } else {

            var info = {};
            info.Codigo = $.trim($('#txtCorralProduccion').val());

            var datos = { 'corralInfo': info };
            
            App.bloquearContenedor($(".contenedor-capturamanual"));
            $.ajax({
                type: "POST",
                url: "CapturaManualDeConsumo.aspx/ValidarCorral",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (request) {
                    App.desbloquearContenedor($(".contenedor-capturamanual"));
                    bootbox.alert(request.Message);
                },
                success: function (data) {
                    App.desbloquearContenedor($(".contenedor-capturamanual"));
                    if (data.d) {
                        if (data.d.CorralID == -2) {
                            $("#btnAgregarCorral").blur();
                            bootbox.alert(window.mensajeErrorSession, function () {
                                $("#txtCorralProduccion").focus();
                            });
                        }
                        else if (data.d.CorralID == -1) {
                            $("#btnAgregarCorral").blur();
                            bootbox.alert(window.mensajeCorralNoCorrespondeAProduccion, function() {
                                $("#txtCorralProduccion").focus();
                            });
                        }
                        else if (data.d.CorralID == -3) {
                            $("#btnAgregarCorral").blur();
                            bootbox.alert(window.mensajeCorralNoTieneLoteActivo, function () {
                                $("#txtCorralProduccion").focus();
                            });
                        } else {

                            if (!SeEncuentraSeleccionado(data.d.CorralID)) {
                                $("#listaSeleccionados").append('<li id="' + data.d.CorralID + '" idgrupocorral="' + data.d.GrupoCorral + '" class=""><input type="checkbox" /> ' + data.d.Codigo + '</li>');
                                $("#txtCorralProduccion").val("");
                                $("#txtCorralProduccion").focus();
                            } else {
                                $("#btnAgregarCorral").blur();
                                bootbox.alert(window.mensajeCorralYaEstaEnLaLista, function () {
                                    $("#txtCorralProduccion").focus();
                                });
                            }
                        }
                    } else {
                        $("#btnAgregarCorral").blur();
                        bootbox.alert(window.mensajeCorralNoExite, function () {
                            $("#txtCorralProduccion").focus();
                        });
                    }
                }
            });
        }
    };
    
    // Funcion para validar la existencia en Inventario
    var ValidarExistenciaInventarioFormulaGuardar = function () {

        if (ValidarDatos()) {
            var url = "CapturaManualDeConsumo.aspx/ObtenerExistenciaInventario";
            var datos = { 'almacenInventarioLoteId': $("#cmbLotes").val() };

            if ($('#cmbFormulas option:selected').text() == "F1") {
                url = "CapturaManualDeConsumo.aspx/ObtenerExistenciaInventarioFormula";
                datos = { 'formulaId' : $('#cmbFormulas').val() };
            }
            console.log(datos);
            App.bloquearContenedor($(".contenedor-capturamanual"));
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(request) {
                    App.desbloquearContenedor($(".contenedor-capturamanual"));
                    bootbox.alert(request.Message);
                },
                success: function(data) {
                    App.desbloquearContenedor($(".contenedor-capturamanual"));
                    if (data.d) {
                        var kilogramos = $.trim($('#txtKilogramos').val());
                        if (kilogramos > data.d.Cantidad) {
                            bootbox.alert(window.mensajeInsuficienteInventario, function() {
                                $('#txtKilogramos').val(data.d.Cantidad);
                                $("#txtKilogramos").focus();
                            });
                        } else {
                           GuardarConsumoManual();
                        }
                    } else {
                        bootbox.alert(window.mensajeErrorAlConsultarInventario);
                    }
                }
            });
        } 
    };

    // Funcion para obtener el listado de formulas.
    var ObtenerFormulas = function() {
        App.bloquearContenedor($(".contenedor-capturamanual"));
        $.ajax({
            type: "POST",
            url: "CapturaManualDeConsumo.aspx/ConsultarFormulas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                bootbox.alert(request.Message);
                Deshabilitar();
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                if (data.d) {
                    if (data.d.length > 0) {
                        $("#cmbFormulas").append('<option value="-1">Selecciona...</option>');
                        $(data.d).each(function() {
                            $("#cmbFormulas").append('<option value="' + this.FormulaId + '">' + this.Descripcion + '</option>');
                        });

                        ObtenerTipoCorral();
                    } else {
                        bootbox.alert(window.mensajeNoHayFormulas);
                        Deshabilitar();
                    }
                } else {
                    bootbox.alert(window.mensajeNoHayFormulas);
                    Deshabilitar();
                }
            }
        });
    };
    
    // Funcion para obtener el listado de lotes del la formula seleccionada.
    var ObtenerLotesProductos = function () {
        App.bloquearContenedor($(".contenedor-capturamanual"));
        $.ajax({
            type: "POST",
            url: "CapturaManualDeConsumo.aspx/ObtenerLotesDelProducto",
            data: JSON.stringify({ 'formulaId': $('#cmbFormulas').val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                bootbox.alert(window.mensajeErrorAlConsultarLotes);
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                if (data.d) {
                    if (data.d.length > 0) {
                        $(data.d).each(function() {
                            $("#cmbLotes").append('<option value="' + this.AlmacenInventarioLoteId + '">' + this.Lote + '</option>');
                        });

                    } else {
                        bootbox.alert(window.mensajeNoHayLotes);
                    }
                } else {
                    bootbox.alert(window.mensajeNoHayLotes);
                }
            }
        });
    };

    // Funcion para obtener el listado de tipos de corral.
    var ObtenerTipoCorral = function () {
        App.bloquearContenedor($(".contenedor-capturamanual"));
        $.ajax({
            type: "POST",
            url: "CapturaManualDeConsumo.aspx/ConsultarTipoDeCorral",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                bootbox.alert(request.Message);
                Deshabilitar();
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                if (data.d) {
                    if (data.d.length > 0) {
                        $("#cmbTipoCorral").append('<option value="-1">Selecciona...</option>');
                        $(data.d).each(function () {
                            $("#cmbTipoCorral").append('<option value="' + this.GrupoCorralID + '">' + this.Descripcion + '</option>');
                        });
                        Limpiar();
                    }
                    else {
                        bootbox.alert(window.mensajeNoHayTipoCorral);
                        Deshabilitar();
                    }
                } else {
                    bootbox.alert(window.mensajeNoHayTipoCorral);
                    Deshabilitar();
                }
            }
        });
    };
    
    // Funcion para obtener el listado de corrales por tipo de corral.
    var ObtenerCorralesPorGrupo = function () {
        App.bloquearContenedor($(".contenedor-capturamanual"));
        $.ajax({
            type: "POST",
            url: "CapturaManualDeConsumo.aspx/ConsultarCorralesPorGrupoCorral",
            data: JSON.stringify({ 'grupoCorralId': $('#cmbTipoCorral').val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                bootbox.alert(request.Message);
                Deshabilitar();
            },
            success: function (data) {
                App.desbloquearContenedor($(".contenedor-capturamanual"));
                if (data.d) {
                    if (data.d.length > 0) {
                        $("#listaSeleccion").empty();
                        $(data.d).each(function () {
                            if (!SeEncuentraSeleccionado(this.CorralID)) {
                                if ($('#cmbTipoCorral').val() == 1) {
                                    $("#listaSeleccion").append('<li id="' + this.CorralID + '" idgrupocorral="' + this.GrupoCorral + '" class=""><input type="checkbox" checked /> ' + this.Codigo + '</li>');
                                } else {
                                    $("#listaSeleccion").append('<li id="' + this.CorralID + '" idgrupocorral="' + this.GrupoCorral + '" class=""><input type="checkbox" /> ' + this.Codigo + '</li>');
                                }
                            }
                        });
                    }
                    else {
                        if ($('#cmbTipoCorral').val() == 3)//Enfermeria
                        {
                            bootbox.alert(window.mensajeNoExistenCorralesDeEnfermeria, function () {
                                $('#cmbTipoCorral').focus();
                            });
                        } else {
                            bootbox.alert(window.mensajeNoExistenCorrales, function() {
                                $('#cmbTipoCorral').focus();
                            });
                        }
                    }
                } else {
                    if ($('#cmbTipoCorral').val() == 3)//Enfermeria
                    {
                        bootbox.alert(window.mensajeNoExistenCorralesDeEnfermeria, function () {
                            $('#cmbTipoCorral').focus();
                        });
                    } else {
                        bootbox.alert(window.mensajeNoExistenCorrales, function () {
                            $('#cmbTipoCorral').focus();
                        });
                    }
                }
            }
        });
    };

    var SeEncuentraSeleccionado = function (corralId) {
        var respuesta = false;
        
        $("#listaSeleccionados li").each(function () {
            if (corralId == this.id) {
                respuesta = true;
            }
        });

        return respuesta;
    };
    
    // Funcion para deshabilitar controles.
    var Deshabilitar = function() {
        $("#txtKilogramos").attr("disabled", "disabled");
        $("#cmbFormulas").attr("disabled", "disabled");
        $("#cmbTipoCorral").attr("disabled", "disabled");
        $("#cmbLotes").attr("disabled", "disabled");
        
        $("#btnGuardar").attr("disabled", "disabled");
        $('input[name=checkEstatus]').attr("disabled", true);
    };
    
    // Funcion para limpiar controles.
    var Limpiar = function() {
        $('#txtFecha').val($.datepicker.formatDate('dd/mm/yy', new Date()));
        $("#txtKilogramos").val("");
        $("#cmbFormulas option[value=-1]").attr('selected', 'selected');
        $("#cmbTipoCorral option[value=-1]").attr('selected', 'selected');
        LimpiarLotes();
        $("#txtCorralProduccion").val("");
        $('.bar').css("width", "10%");
        $("#labelPorcentaje").text("");
        $("#listaSeleccion").empty();
        $("#listaSeleccionados").empty();
        $("#txtKilogramos").focus();
        $("#groupCorral").hide();
    };
    // Funcion que limpia los campos del lote
    var LimpiarLotes = function () {
        $("#cmbLotes").html('');
        $("#cmbLotes").append('<option value="-1">Selecciona...</option>');
        $("#cmbLotes option[value=-1]").attr('selected', 'selected');
    };
    
    //Verifica el avance del proceso
    var VerificarAvance = function () {
        var datos = "{ 'idUsuario':'" + $('#idUsuario').val() + "' }";
        $.ajax({
            type: "POST",
            url: "Reparto.asmx/ObtenerAvanceReparto",
            data: datos,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.d != null) {
                    if (data.d.EsValido) {
                        if (data.d.Datos.EstatusError == 0) {
                            $('.bar').css("width", data.d.Datos.PorcentajeTotal + '%');
                            $("#labelPorcentaje").html(data.d.Datos.TotalCorralesProcesados + " de " + data.d.Datos.TotalCorrales + " corrales (" + data.d.Datos.PorcentajeTotal + "%)");
                        } else {
                            $('#responsive').modal('hide');
                            clearInterval(intervalID);
                        }
                    } else {
                        $('#responsive').modal('hide');
                        clearInterval(intervalID);
                    }
                }
            },
            error: function (error, textStatus, errorThrown) {
                $('#responsive').modal('hide');
                clearInterval(intervalID);
            }
        });
    };
    
    ObtenerFormulas();
});