$(document).ready(function () {
    $("#cmbPrecio").attr("disabled", true);
    $("#txtTicket").inputmask({ "mask": "9", "repeat": 10, "greedy": false });
    $("#txtCorral").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    $("#txtCabezasVendidas").inputmask({ "mask": "9", "repeat": 3, "greedy": false });
    $("#txtNoIndividual").numericInput();
    $("#txtAreteRFID").numericInput();
    //$("#txtNoIndividual").inputmask({ "mask": "*", "repeat": 15, "greedy": false });

    $("#rdSalidaRecuperacion").click(function() {
        $("#hIr").val("SalidaIndividualRecuperacion.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#rdSalidaSacrificio").click(function() {
        $("#hIr").val("SalidaIndividualSacrificio.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#btnCancelar").click(function() {
        $("#hIr").val("SalidaIndividualVenta.aspx");
        $("#aCancelar").trigger("click");
    });

    $("#btnDialogoSi").click(function() {
        location.href = $("#hIr").val();
    });

    $("#btnDialogoNo").click(function() {
        $("#rdSalidaVenta").attr("checked", true);
    });

    $("#btnFoto").click(function () {
        if ($("#txtNoIndividual").val() != "" || $("#txtAreteRFID").val() != "") {
            $("#flFoto").trigger("click");
        } else {
            $("#txtNoIndividual").focus();
        }
    });

    $("#txtTicket").change(function() {
        ticketValido = 0;
        $("#txtCorral").val("");
        $("#txtCorral").trigger("change");
    });

    $("#txtCorral").change(function () {
        corralValido = 0;
        InicializarGrid();
    });

    $("#txtTicket").keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarTicket();
        }
    });

    $("#txtCabezasVendidas").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtNoIndividual").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtAreteRFID").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtCorral").focusin(function() {
        ValidarTicket();
    });

    $("#cmbCausa").focusin(function() {
        if (ValidarTicket() == true) {
            ValidarCorral();
        }
    });

    $("#txtCabezasVendidas").focusin(function() {
        if (ValidarTicket() == true) {
            ValidarCorral();
        }
    });

    $("#cmbCausa").change(function() {
        CargarPrecios();
    });

    $("#ddlTipoVenta").change(function () {
        $("#txtTicket").val("");
        $("#txtCorral").val("");
        $("#txtCabezasVendidas").val("");
        if ($("#ddlTipoVenta").val() == 2) {
            InicializarGrid();
            $("#aretes").hide();
            $("#lblNoIndividual").hide();
            $("#txtNoIndividual").hide();
            $("#btnFoto").hide();
            $("#btnAgregar").hide();
            $("#lblAreteRFID").hide();
            $("#txtAreteRFID").hide();
            $("#lblReqArete").hide();
            $("#lblReqAreteRFID").hide();
        }
        else {
            InicializarGrid();
            $("#aretes").show();
            $("#lblNoIndividual").show();
            $("#txtNoIndividual").show();
            $("#btnFoto").show();
            $("#btnAgregar").show();
            $("#lblAreteRFID").show();
            $("#txtAreteRFID").show();
            $("#lblReqArete").show();
            $("#lblReqAreteRFID").show();
        }
    });

    
    $("#txtNoIndividual").focusin(function () {
        if (ValidarTicket() == true) {
            if (ValidarCorral() == true) {
                if (!($("#txtCabezasVendidas").val() != "" && $("#txtCabezasVendidas").val() > 0)) {
                    bootbox.alert(msgCapturarCabezas, function () {
                        $("#txtCabezasVendidas").focus();
                    });
                } else if (cabezasLote < $("#txtCabezasVendidas").val() && $("#txtCabezasVendidas").val() != "") {
                    bootbox.alert(msgCabezasMayorLote, function () {
                        $("#txtCabezasVendidas").focus();
                    });
                }
            }
        }
    });

    $("#txtAreteRFID").focusin(function () {
        if (ValidarTicket() == true) {
            if (ValidarCorral() == true) {
                if (!($("#txtCabezasVendidas").val() != "" && $("#txtCabezasVendidas").val() > 0)) {
                    bootbox.alert(msgCapturarCabezas, function () {
                        $("#txtCabezasVendidas").focus();
                    });
                } else if (cabezasLote < $("#txtCabezasVendidas").val() && $("#txtCabezasVendidas").val() != "") {
                    bootbox.alert(msgCabezasMayorLote, function () {
                        $("#txtCabezasVendidas").focus();
                    });
                }
            }
        }
    });

    $("#txtCorral").keydown(function(e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            ValidarCorral();
        }
    });

    $("#btnAgregar").click(function() {
        AgregarArete();
    });

    $("#btnGuardar").click(function() {
        GuardarDetalle();
    });
});

var ticketValido = 0;
var corralValido = 0;
var contador = 0;
var cabezasLote = 0;
var listaAretes = {};

InicializarGrid = function() {
    $("#aretes tbody").html('<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>' +
        '<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>' +
        '<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>' +
        '<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>' +
        '<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>');
    contador = 0;
};

ValidarTicket = function() {
    if (ticketValido == 0) {
        if ($("#txtTicket").val() != "") {
            var datos = {
                "ticket": $("#txtTicket").val(),
                "tipoVenta": $("#ddlTipoVenta").val()
            };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualVenta.aspx/ValidarTicket",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    bootbox.alert(request.Message);
                    ticketValido = 0;
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == null) {
                        bootbox.alert(msgTicketInvalido, function() {
                            $("#txtTicket").focus();
                        });
                        ticketValido = 0;
                        return false;
                    } else if (data.d.FolioTicket > 0) {
                        ticketValido = 1;
                        return true;
                    } else {
                        bootbox.alert(msgTicketInvalido, function() {
                            $("#txtTicket").focus();
                        });
                        ticketValido = 0;
                        return false;
                    }
                }
            });
        } else {
            bootbox.alert(msgTicketInvalido, function() {
                $("#txtTicket").focus();
            });
            return false;
        }
    } else {
        return true;
    }
};

ValidarCorral = function() {
    if (corralValido == 0) {
        if ($("#txtCorral").val() != "") {
            var datos = { "corralCodigo": $("#txtCorral").val(), "TipoVenta": $("#ddlTipoVenta").val() };
            $.ajax({
                type: "POST",
                url: "SalidaIndividualVenta.aspx/ObtenerCorral",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function(request) {
                    bootbox.alert(request.Message);
                    return false;
                },
                dataType: "json",
                success: function(data) {
                    if (data.d == 0) {
                        bootbox.alert(msgCorralNoExiste, function() {
                            $("#txtCorral").focus();
                        });
                        corralValido = 0;
                        return false;
                    } else if (data.d == 2) {
                        if ($("#ddlTipoVenta").val() == 1) {
                            bootbox.alert(msgNoVentaCronico, function() {
                                $("#txtCorral").focus();
                            });
                        } else {
                            bootbox.alert(msgNoMaquilaIntensivo, function () {
                                $("#txtCorral").focus();
                            });
                        }
                        corralValido = 0;
                        return false;
                    } else if (data.d == 1) {
                        corralValido = 1;
                        ObtenerLote();
                        ObtenerAnimalesPorCorral();
                        return true;
                    }
                }
            });
        } else {
            bootbox.alert(msgCorralVacio, function() {
                $("#txtCorral").focus();
            });
            return false;
        }
    } else {
        return true;
    }
};

ObtenerLote = function() {
    datos = { "corralCodigo": $("#txtCorral").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualVenta.aspx/ObtenerLote",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            bootbox.alert(request.Message);
            return false;
        },
        dataType: "json",
        success: function(data) {
            if (data.d == null) {
                bootbox.alert(msgCorralNoExiste, function() {
                    $("#txtCorral").focus();
                });
                corralValido = 0;
                return false;
            } else if (data.d) {
                cabezasLote = data.d.Cabezas;
                return true;
            }
        }
    });
};

var listaAretes = [];
//Funcion que obtiene los aretes del corral
function ObtenerAnimalesPorCorral() {
    var datos = { 'corralCodigo': $("#txtCorral").val() };
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerAnimalesPorCodigoCorral",
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

                $('#txtNoIndividual').typeahead({
                    source: listaAretes
                });

                listaAretesMetalicos = [];
                $(data.d).each(function () {
                    listaAretesMetalicos.push(this.AreteMetalico);
                });

                $('#txtAreteRFID').typeahead({
                    source: listaAretesMetalicos
                });

            }
        }
    });
};

Bloquear = function () {
    $.blockUI(
    {
        message: '<img src="../assets/img/ajax-loading.gif" align="">',
        css: {
            top: '10%',
            border: 'none',
            padding: '2px',
            backgroundColor: 'none'
        },
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.05,
            cursor: 'wait'
        }
    });
};

Desbloquear = function () {
    $.unblockUI();
};

AgregarArete = function() {
    var encontrado = 0;
    var archivoSeleccionado = document.getElementById('flFoto').files[0];
    if ($("#txtCabezasVendidas").val() != "") {
        if ($("#txtCabezasVendidas").val() > contador && $("#txtCabezasVendidas").val() > 0) {
            if (($("#txtNoIndividual").val() != "" || $("#txtAreteRFID").val() != "") && archivoSeleccionado != null) {
                var nodes = $("#aretes tbody tr");

                if (nodes != null) {

                    if ($("#txtNoIndividual").val() != "") {
                        $(nodes).each(function () {
                            if (this.cells[1].innerHTML == $("#txtNoIndividual").val()) {
                                encontrado = 1;
                            }
                        });
                    }
                    else
                    {
                        $(nodes).each(function () {
                            if (this.cells[1].innerHTML == $("#txtAreteRFID").val()) {
                                encontrado = 1;
                            }
                        });
                    }
                    

                    if (encontrado == 0) {
                        Bloquear();
                        var datos = { "corralCodigo": $('#txtCorral').val(), "arete": $("#txtNoIndividual").val(), "areteRFID": $("#txtAreteRFID").val() };
                        $.ajax({
                            type: "POST",
                            url: "SalidaIndividualVenta.aspx/ObtenerExisteArete",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(datos),
                            error: function(request) {
                                Desbloquear();
                                bootbox.alert(request.Message);
                            },
                            dataType: "json",
                            success: function(data) {
                                Desbloquear();
                                if (data.d.TipoGanadoID == 1 || data.d.TipoGanadoID == 3) {
                                    contador++;
                                    if (contador >= 5) {
                                        $("#aretes tbody").append('<tr id="tr' + contador + '"><td></td> <td></td><td></td> <td></td> <td></td> <td  style="visibility: hidden;"></td><td  style="visibility: hidden;"></td></tr>');
                                    }

                                    //var id = "fl" + $("#txtNoIndividual").val();
                                    var id = "fl" + data.d.Arete;

                                    var reader = new FileReader();
                                    reader.onload = (function(theFile) {
                                        return function(e) {
                                            // Render thumbnail.
                                            var span = document.createElement('a');
                                            span.innerHTML = ['<div class="span12"><img class="thumb" src="', e.target.result,
                                                '" title="', escape(theFile.name), '" width="60" height="60"/></div>'].join('');


                                            span.setAttribute('class', 'imagen-seleccion alineacionCentro fancybox');
                                            span.setAttribute('id', id);
                                            span.setAttribute('href', e.target.result);

                                            nodes[contador - 1].cells[3].innerHTML = '';
                                            nodes[contador - 1].cells[3].insertBefore(span, null);

                                            $(id).fancybox();

                                            $(".fancybox").fancybox();
                                        };
                                    })(archivoSeleccionado);

                                    reader.readAsDataURL(archivoSeleccionado);

                                    var cadena = "000" + contador;
                                    nodes[contador - 1].id = "tr" + contador;
                                    nodes[contador - 1].cells[0].innerHTML = cadena.substring(cadena.length - 3, cadena.length);
                                    //nodes[contador - 1].cells[1].innerHTML = $("#txtNoIndividual").val();
                                    nodes[contador - 1].cells[1].innerHTML = data.d.Arete;
                                    nodes[contador - 1].cells[2].innerHTML = data.d.AreteMetalico;
                                    nodes[contador - 1].cells[4].innerHTML = "<div class='span12 alineacionCentro'><a onclick='borrarRenglon(" + contador + ")'><img src='../Images/cross-icon.png' width='20' height='20' style='padding-top:10px;'/></a></div>";
                                    nodes[contador - 1].cells[5].innerHTML = false;
                                    nodes[contador - 1].cells[6].innerHTML = $("#cmbPrecio").val();

                                    if (data.d.TipoGanadoID == 3) {
                                        nodes[contador - 1].cells[5].innerHTML = true;
                                    }


                                    $("#txtNoIndividual").val("");
                                    $("#txtAreteRFID").val("");
                                    document.getElementById('flFoto').value = "";
                                } else if (data.d.TipoGanadoID == 2) {
                                    bootbox.alert(msgAreteSalida, function() {
                                        $("#txtNoIndividual").focus();
                                    });
                                } else {
                                    bootbox.alert(msgAreteNoDelCorral, function() {
                                        $("#txtNoIndividual").focus();
                                    });
                                }
                            }
                        });
                    } else {
                        bootbox.alert(msgAreteCapturado);
                    }
                }
            } else {
                if ($("#txtNoIndividual").val() == "" && $("#txtAreteRFID").val() == "") {
                    bootbox.alert(msgSinArete);
                }
                else if (archivoSeleccionado == null) {
                    bootbox.alert(msgSinFoto);
                }
            }
        } else {
            bootbox.alert(msgAretesMaximo);
            $("#txtNoIndividual").val("");
            $("#txtAreteRFID").val("");
        }
    } else {
        bootbox.alert(msgCapturarCabezas);
    }
};

borrarRenglon = function(id) {
    bootbox.dialog({
        message: "¿Desea eliminar el arete?",
        buttons: {
            success: {
                label: "Si",
                className: "btn SuKarne",
                callback: function() {
                    $("#tr" + id).remove();
                    contador--;

                    if (contador < 5) {
                        $("#aretes tbody").append('<tr id="tr' + contador + '"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="visibility: hidden;">&nbsp;</td><td style="visibility: hidden;"></td></tr>');
                    }
                    var nodes = $("#aretes tbody tr");
                    var recontar = 1;
                    $(nodes).each(function() {
                        if (recontar <= contador) {
                            var cadena = "000" + recontar;
                            this.cells[0].innerHTML = cadena.substring(cadena.length - 3, cadena.length);
                            recontar++;
                        }
                    });
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

CargarPrecios = function() {
    $("#cmbPrecio").html("");
    var datos = { "causa": $("#cmbCausa").val() };
    $.ajax({
        type: "POST",
        url: "SalidaIndividualVenta.aspx/LlenarcomboPrecio",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            bootbox.alert(request.Message);
            ticketValido = 0;
            return false;
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                var datos = data.d;

                for (var i = 0; i < datos.length; i++) {
                    var registro = datos[i];
                    $("#cmbPrecio").append("<option value='" + registro.CausaPrecioID + "'>$ " + registro.Precio + "</option>");
                }

                if (datos.length > 1) {
                    $("#cmbPrecio").attr("disabled", false);
                } else {
                    $("#cmbPrecio").attr("disabled", true);
                }
            }
        }
    });
};

SubirFoto = function(nombreArchivo, id) {
    var fd = new FormData();
    fd.append("fileupload", id.attr("href"));
    fd.append("filename", nombreArchivo);
    fd.append("tipo", 2);
    fd.append("carpetaFoto", 5);
    var xhr = new XMLHttpRequest();
    xhr.upload.addEventListener('progress', UploadProgress, false);
    xhr.onreadystatechange = StateChange;
    xhr.open('POST', '../Fakes/fileupload.aspx');
    xhr.send(fd);
}

//Funcion que muestra el progreso de la carga
function UploadProgress(event) {
    var percent = parseInt(event.loaded / event.total * 100);
    $('#attach_link').text('Uploading: ' + percent + '%');

}

//Funcion que muestra el cambio de estado del evento de carga de archivo
function StateChange(event) {

    if (event.target.readyState == 4) {
        if (event.target.status == 200 || event.target.status == 304) {
            //$('#attach_link').text('Agregar archivo »');
        }
        else {
        }
    }
}

GuardarDetalle = function() {
    if (ticketValido > 0) {
        if (corralValido > 0) {
            if ($("#cmbCausa").val() > 0) {
                if ($("#cmbPrecio").val() > 0) {
                    if (($("#ddlTipoVenta").val() == 1 && contador > 0 && contador == $("#txtCabezasVendidas").val()) || ($("#ddlTipoVenta").val() == 2 && contador == 0)) {
                        var nodes = $("#aretes tbody tr");

                        var ventaGanado = { };
                        ventaGanado.FolioTicket = $("#txtTicket").val();

                        var ventaGanadoDetalle = [];

                        for (var i = 0; i < contador; i++) {
                            var animal = { };
                            var arete = { };

                            arete.Arete = nodes[i].cells[1].innerHTML;

                            animal.CargaInicial = nodes[i].cells[5].innerHTML;
                            animal.Arete = arete.Arete;

                            arete.CausaPrecioID = nodes[i].cells[6].innerHTML;

                            arete.Animal = animal;

                            var uuid = GeneraUUID();
                            arete.FotoVenta = uuid + "-sal.JPG";
                            console.log($("#fl" + nodes[i].cells[1].innerHTML));
                            SubirFoto(arete.FotoVenta, $("#fl" + nodes[i].cells[1].innerHTML));

                            ventaGanadoDetalle.push(arete);
                        }

                        Bloquear();
                        var datos = {
                            "codigoCorral": $("#txtCorral").val(),
                            "causaPrecioId": $("#cmbPrecio").val(),
                            "ventaGanado": ventaGanado,
                            "ventaGanadoDetalle": ventaGanadoDetalle,
                            "tipoVenta": $("#ddlTipoVenta").val(),
                            "totalCabezas": $("#txtCabezasVendidas").val()
                        };
                        $.ajax({
                            type: "POST",
                            url: "SalidaIndividualVenta.aspx/GuardarVentaDetalle",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(datos),
                            error: function(request) {
                                Desbloquear();
                                bootbox.alert(request.Message);
                                ticketValido = 0;
                                return false;
                            },
                            dataType: "json",
                            success: function(data) {
                                Desbloquear();
                                if (data.d == 0) {
                                    bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardados, function() {
                                        location.href = location.href;
                                    });
                                } else {
                                    bootbox.alert(msgOcurrioErrorGrabar);
                                }
                            }
                        });
                    } else {
                        if (contador > 0) {
                            //Mensaje el numero de cabezas diferente
                            bootbox.alert(msgAretesDifCabezas);
                        } else {
                            //Capturar cabezas
                            bootbox.alert(msgDatosBlanco);
                        }
                    }
                } else {
                    //Mensaje sin precio seleccionado
                    bootbox.alert(msgDatosBlanco);
                }
            } else {
                //Mensaje sin causa
                bootbox.alert(msgDatosBlanco);
            }
        } else {
            //Corral no valido
            bootbox.alert(msgDatosBlanco);
        }
    } else {
        //Ticket no valido
        bootbox.alert(msgDatosBlanco);
    }
};

GeneraUUID = function() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};

//Validar que el usuario tenga permisos suficientes
EnviarMensajeUsuario = function() {
    bootbox.alert(msgNoTienePermiso, function() {
        location.href = "../Principal.aspx";
        return false;
    });
};