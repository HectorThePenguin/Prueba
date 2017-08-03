
$(document).ready(function () {
    $("#txtCorral").inputmask({ "mask": "*", "repeat": 10, "greedy": false });

    var corralValido = false;
    
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#txtCorral').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $('input[name = checkEstatus]').focus();
        }
    });

    $('#txtCorral').change(function(e) {
        Limpiar();
        ValidarCorral();
    });
    
    $('input[name = checkEstatus]').focus(function (e) {
        ValidarCorral();
    });

    $("#btnGuardar").click(function () {
        if (corralValido) {

            var estatusDistribucion = $('input[name=checkEstatus]:checked').val();

            var datos = { 'codigoCorral': $("#txtCorral").val(), 'estatusDistribucion': estatusDistribucion };

            $.ajax({
                type: "POST",
                url: "LectorDistribucionAlimentos.aspx/GuardarEstatusDistribucion",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(request) {
                    bootbox.alert(request.Message);
                },
                success: function (data) {
                    if (data.d) {
                        bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + window.mensajeGuardadoOk, function() {
                            Limpiar();
                            $("#txtCorral").val("");
                            ObtenerSiguienteReparto();
                        });
                    } else {
                        bootbox.alert(window.mensajeErrorAlGuardar, function () {
                            Limpiar();
                            $("#txtCorral").val("");
                            ObtenerSiguienteReparto();
                        });
                    }
                }
            });
        }
    });
    
    // Obtiene el siguiente corral en la lista segun el orden en que se registro.
    var ObtenerSiguienteReparto = function () {
        
        corralValido = false;
        
        $.ajax({
            type: "POST",
            url: "LectorDistribucionAlimentos.aspx/ObtenerSiguienteCorral",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d) {
                    $("#txtTotal").val(data.d.TotalRepartos);
                    $("#txtLecturas").val(data.d.TotalRepartosLeidos);
                    $("#txtFaltantes").val(data.d.TotalRepartos - data.d.TotalRepartosLeidos);
                    
                    if (data.d.Corral) {
                        $("#txtSeccion").val(data.d.Corral.Seccion);
                        $("#txtCorral").val(data.d.Corral.Codigo);
                        $("#txtOrden").val(data.d.Corral.Orden);
                        corralValido = true;
                    }
                } else {
                    bootbox.alert(window.mensajeNoHayMasCorrales);
                    Deshabilitar();
                }
            }
        });
    };
    
    // Obtiene el reparto del corral tecleado
    var ObtenerRepartoPorCorral = function () {

        corralValido = false;
        
        var info = {};
        info.Codigo = $("#txtCorral").val();

        var datos = { 'corral': info };
        
        $.ajax({
            type: "POST",
            url: "LectorDistribucionAlimentos.aspx/ObtenerRepartoPorCorral",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d) {
                    $("#txtTotal").val(data.d.TotalRepartos);
                    $("#txtLecturas").val(data.d.TotalRepartosLeidos);
                    $("#txtFaltantes").val(data.d.TotalRepartos - data.d.TotalRepartosLeidos);

                    if (data.d.Corral) {
                        $("#txtSeccion").val(data.d.Corral.Seccion);
                        $("#txtCorral").val(data.d.Corral.Codigo);
                        $("#txtOrden").val(data.d.Corral.Orden);

                        corralValido = true;
                    }
                } else {
                    bootbox.alert(window.mensajeCorralVerificado, ObtenerSiguienteReparto);
                }
            }
        });
    };
    
    //Obtiene el listado de estatus para la distribucion de alimentos
    var ObtenerEstatusDistribucion = function () {
        $.ajax({
            type: "POST",
            url: "LectorDistribucionAlimentos.aspx/ObtenerEstatusDistribucion",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                if (data.d) {
                    var contador = 0;
                    var colEncabezado = "", colRadios = "";
                    $(data.d).each(function () {
                        var encabezado = "";
                        if (this.DescripcionCorta == 'B') {
                            encabezado = this.Descripcion;
                        } else {
                            encabezado = this.DescripcionCorta;
                        }
                        colEncabezado += '<td style="background-color:' + ObtenerFondo(contador) + '; text-align:center; font-weight: bold; font-size:16px;">' + encabezado + '</td>';
                        colRadios += '<td style="text-align:center;"><input type="radio" name="checkEstatus" value="' + this.EstatusId + '"></td>';
                        contador++;
                    });
                    $("#tablaEstatus tbody").append('<tr>' + colEncabezado + '</tr>');
                    $("#tablaEstatus tbody").append('<tr>' + colRadios + '</tr>');

                    $('input[name=checkEstatus]')[0].checked = true;
                    ObtenerSiguienteReparto();
                } else {
                    bootbox.alert(window.mensajeNoHayEstadosDistribucion);
                    Deshabilitar();
                }
            }
        });
    };
    // Obtiene el color para los encabezados
    // Son 7 colores predefinidos, del octavo en adelante se genrara un color random.
    // Si se necesita otro color predeterminado para la columna siguiente solo se tiene que agregar al array.
    var ObtenerFondo = function (conta) {
        var colores = ["#FFFFFF", "#0080FF", "#9A2EFE", "#FFFFFF", "#FE9A2E", "#4B8A08", "#FFFFFF"];
        var color = "";
        
        if (conta > colores.length - 1) {
            color = RandomColor();
        } else {
            color = colores[conta];
        }
        
        return color;
    };

    // Funcion que genera un color hexadecimal.
    var RandomColor = function() {
        var letters = '0123456789ABCDEF'.split('');
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.round(Math.random() * 15)];
        }
        return color;
    };

    var Deshabilitar = function() {
        $("#txtCorral").attr("disabled", "disabled");
        $("#btnGuardar").attr("disabled", "disabled");
        $('input[name=checkEstatus]').attr("disabled", true);
    };

    var Limpiar = function () {
        $("#txtTotal").val("");
        $("#txtLecturas").val("");
        $("#txtFaltantes").val("");
        $("#txtSeccion").val("");
        $("#txtOrden").val("");
        corralValido = false;
        $('input[name=checkEstatus]')[0].checked = true;
    };
    
    // Valida el corral tecleado por el usuario.
    var ValidarCorral = function () {
        
        if ($.trim($('#txtCorral').val()) == "") {
            bootbox.alert(window.mensajeCapturarCorral, function () {
                $("#txtCorral").focus();
                ObtenerSiguienteReparto();
            });
        } else {
        
            var info = {};
            info.Codigo = $.trim($('#txtCorral').val());

            var datos = { 'corralInfo': info };

            $.ajax({
                type: "POST",
                url: "LectorDistribucionAlimentos.aspx/ValidarCorral",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(request) {
                    bootbox.alert(request.Message);
                },
                success: function(data) {
                    if (data.d) {
                        if (data.d.Operador) {
                            ObtenerRepartoPorCorral();
                        } else {
                            bootbox.alert(window.mensajeCorralNoEsDelLector, function() {
                                $("#txtCorral").focus();
                                ObtenerSiguienteReparto();
                            });
                        }
                    } else {
                        bootbox.alert(window.mensajeCorralNoValido, function() {
                            $("#txtCorral").focus();
                            ObtenerSiguienteReparto();
                        });
                    }
                }
            });
        }
    };

    function EnviarMensajeUsuarioErrorSession(mensaje) {
        bootbox.alert(mensaje, function () {
            history.go(-1);
            return false;
        });
    }

    function EnviarMensajeUsuario(mensaje) {
        bootbox.alert(mensaje, function () {
            return false;
        });
    }
    
    ObtenerEstatusDistribucion();
});