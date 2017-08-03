$(document).ready(function() {

    $("#txtArete").inputmask({ "mask": "9", "repeat": 15, "greedy": false });
    $("#txtAreteTestigo").inputmask({ "mask": "9", "repeat": 15, "greedy": false });
    
    // Evento que evita que se pegue en los input 
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });
    
    var Limpiar = function () {
        $("#txtArete").val("");
        $("#txtAreteTestigo").val("");
        $("#contenedorMensaje").html("");
        $("#txtArete").focus();
    };

    // Evento Click del boton limpiar
    $("#btnLimpiar").click(function () {
        Limpiar();
    });

    $("#txtArete").keydown(function () {
        $("#contenedorMensaje").html('');
    });
    
    $("#txtAreteTestigo").keydown(function () {
        $("#contenedorMensaje").html('');
    });
    
    // Evento Click del boton Buscar
    $("#btnBuscar").click(function () {

        if ($.trim($("#txtArete").val()) != "" || $.trim($("#txtAreteTestigo").val()) != "") {
            var cadenaUrl = "", mensajeEnviarError = "", mensajeEnviar = "";
            var datosAretes;
            var controlAlQueSeIraElFoco = $("#txtAreteTestigo");
            
            if ($.trim($("#txtArete").val()) != "" && $.trim($("#txtAreteTestigo").val()) != "")
            {
                cadenaUrl = "GanadoVago.aspx/ObtenerCorralAmbosArete";
                datosAretes = { arete: $("#txtArete").val(), areteTestigo: $("#txtAreteTestigo").val() };
                mensajeEnviarError = window.mensajeAmbosAretesNoEncontrado;
                mensajeEnviar = window.mensajePrimerSeccionArete + ' <span class="numeroGrande">' + $("#txtArete").val() + '</span>';
            }
            else if ($.trim($("#txtArete").val()) != "")
            {
                cadenaUrl = "GanadoVago.aspx/ObtenerCorralArete";
                datosAretes = { arete: $("#txtArete").val() };
                mensajeEnviarError = window.mensajeAreteNoEncontrado;
                mensajeEnviar = window.mensajePrimerSeccionArete + ' <span class="numeroGrande">' + $("#txtArete").val() + '</span>';
                controlAlQueSeIraElFoco = $("#txtArete");
            }
            else
            {
                cadenaUrl = "GanadoVago.aspx/ObtenerCorralAreteTestigo";
                datosAretes = { areteTestigo: $("#txtAreteTestigo").val() };
                mensajeEnviarError = window.mensajeAreteTestigoNoEncontrado;
                mensajeEnviar = window.mensajePrimerSeccionAreteTestigo + ' <span class="numeroGrande">' + $("#txtAreteTestigo").val() + '</span>';
            }
            
            $.ajax({
                type: "POST",
                url: cadenaUrl,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datosAretes),
                error: function (request) {
                    bootbox.alert(window.mensajeErrorAlConsultarAretes, function () {
                        setTimeout(function () { $("#txtArete").focus(); }, 500);
                    });
                },
                success: function (data) {
                    if (data.d) {
                        $("#contenedorMensaje").html('<div class="alert alert-error"><strong>' + mensajeEnviar + ' ' + window.mensajeSegundaSeccion + ' <span class="numeroGrande">' + data.d.Codigo + '</span></strong></div>');
                    }
                    else {
                        bootbox.alert(mensajeEnviarError, function () {
                            setTimeout(function () { controlAlQueSeIraElFoco.focus(); }, 500);
                        });
                    }
                }
            });
        } else {
            bootbox.alert(window.mensajeDebeIngresarArete, function () {
                setTimeout(function () { $("#txtArete").focus(); }, 500);
            });
        }
    });

    Limpiar();
});