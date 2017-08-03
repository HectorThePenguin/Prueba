//Variables
var msjAbierto = 0;
var folioValido = false;

$(document).ready(function () {
    //$("#txtFolio").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtFolio").numericInput();
    $("#txtPiezas").numericInput();
    $("#txtFolioBuscar").numericInput();
    //$("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    
    //KeyPress
    $("#txtFolio").keydown(function(e) {
        var code = e.keyCode || e.which;
        var buscarFolio = true;
        if (code == 13) {
            if (folioValido) {
                bootbox.confirm(msgValidaOtroFolio, function (result) {
                    if (result) {
                        folioValido = false;
                        ValidarFolio();
                    }
                });
            } else {
                ValidarFolio();
            }
            
            e.preventDefault();
        } else if (code == 9) {
            if (folioValido) {
                bootbox.confirm(msgValidaOtroFolio, function (result) {
                    if (result) {
                        folioValido = false;
                        ValidarFolio();
                    }
                });
            } else {
                ValidarFolio();
            }
        }
    });

    $("#txtPiezas").keypress(function (e) {
        var code = e.keyCode || e.which;
        if (code >= 48 && code <= 57) { //numeros
            return true;
        } else if (code == 8 || code == 13 || code == 9 || code == 127 || code == 32) {// del,enter,tab,espacio
            return true;
        }
        return false;
    });


    $("#txtPiezas").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $("#txtPiezas").val(accounting.formatNumber($("#txtPiezas").val()));
            ValidarCantidadPiezas();
        }
    });

    $("#btnGuardar").click(function() {
        GuardarInformacion();
    });

    $("#btnCancelar").click(function() {
        $("#dlgCancelar").modal("show");
    });

    $("#btnDialogoSi").click(function () {
        LimpiarFormulario();
    });

    $("#btnDialogoNo").click(function () {
        $("#dlgCancelar").modal("hide");
    });

    $("#lblBuscar").click(function () {
        if (BuscarFolios()) {
            $("#dlgBusquedaFolio").modal("show");
            $("#txtFolioBuscar").focus();
        };
        
    });

    $("#btnBuscarFolio").click(function () {
        BuscarFolios();
    });

    $("#btnAgregarBuscar").click(function () {
        var renglones = $("input[class=folios-resultado]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                MostrarDatos($(this));
            });
            $("#txtFolioBuscar").val("");
            $("#tbBusqueda tbody").empty();
            $("#dlgBusquedaFolio").modal("hide");

        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolio").modal("hide");
                MensajeSeleccionarFolio();
            }
        }
    });

    $("#txtFolioBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            BuscarFolios();
        }
    });
    

    $("#btnCancelarBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaFolio").modal("hide");
    });

    $("#btnDialogoCancelarSi").click(function () {
        $("#dlgCancelarBuscar").modal("hide");
        $("#txtFolioBuscar").val("");
        $("#tbBusqueda tbody").empty();
    });

    $("#btnDialogoCancelarNo").click(function () {
        $("#dlgCancelarBuscar").modal("hide");
        $("#dlgBusquedaFolio").modal("show");
    });
    
    $("#cmbAlmacen").change(function () {
        var almacen = $(this).val();
        $("#cmbProducto").empty();
        $("#cmbProducto").attr("disabled", "disabled");
        $("#cmbLote").empty();
        $("#cmbLote").attr("disabled", "disabled");
        $("#txtKilogramos").val("");
        $("#txtKilogramos").attr("disabled", "disabled");
        $("#txtPiezas").val("");
        $("#txtPiezas").attr("disabled", "disabled");
        $("#txtObservaciones").val("");
        if (almacen != "" && almacen != 0) {
            ObtenerProductosPorAlmacen(almacen);
        }
    });

    $("#cmbLote").change(function() {
        var cantidad = $(this).find(':selected').data('cantidad');
        if ($("#cmbLote option:selected").text().toUpperCase() != "SELECCIONE") {
            $("#txtKilogramos").val(accounting.formatNumber(cantidad));
        } else {
            $("#txtKilogramos").val("");
        }
        $("#txtObservaciones").val("");
        $("#txtPiezas").val("");

        if (subfamilia == $("#txtSubFamiliaForraje").val()) {
            $("#txtPiezas").focus();
        }
    });

    $("#cmbProducto").change(function () {
        var producto = $(this).val();
        var almacen = $("#cmbAlmacen").val();
        if (almacen != "" && almacen != 0) {
            ObtenerLotes(producto, almacen);

            var objProducto = $(this).find(':selected').data('subfamilia');
            ValidaSubFamilia(objProducto);
        }
        
    });

    // Evento que evita que se pegue en los input 
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtObservaciones").alphanum({ allow: "., " });

    $('#txtObservaciones').live('keyup change', function () {
        var str = $(this).val();
        var mx = 255;
        if (str.length > mx) {
            $(this).val(str.substr(0, mx));
            return false;
        }
    });

    ConsultaAlmacenes();
});

//Consultas 
MostrarDatos = function (folio) {
    ReiniciarFormulario();
    var movimiento = folio.data("movimiento");
    var fecha = folio.data("fecha");
    var folioSalida = folio.val();
    var claveSalida = folio.data("clave");

    $("#txtClaveSalida").val(claveSalida);
    $("#txtFolio").val(folioSalida);
    $("#txtSalida").val(movimiento);
    var fecha = new Date(parseInt(fecha.substr(6)));
    $("#txtFecha").val(ObtenerFecha(fecha));

    $("#cmbAlmacen").removeAttr("disabled");
    $("#DatosSalida").show();
    folioValido = true;
    $("#cmbAlmacen").focus();
}

ConsultaAlmacenes = function() {
    $.ajax({
        type: "POST",
        url: "SalidaVentaTraspaso.aspx/ObtenerAlmacenes",
        contentType: "application/json; charset=utf-8",
        error: function (request) {
            ValidaMensajeError(request);
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var salida = data.d;
                if (salida != undefined && salida.length > 0) {
                    var almacenPlantaAlimento = false;
                    var almacenMateriaPrima = false;
                    var almacenBodegaTercero = false;
                    var almacenBodegaExterno = false;

                    $("#cmbAlmacen").append("<option value='0'>Seleccione</option>");
                    for (var i = 0 ; i < salida.length ; i++) {
                        var almacen = salida[i];
                        var tipoAlmacen = almacen.TipoAlmacen;
                        $("#cmbAlmacen").append("<option value='" + almacen.AlmacenID + "' data-almacen='" + tipoAlmacen.TipoAlmacenID + "'>" + almacen.Descripcion + "</option>");

                        switch (tipoAlmacen.TipoAlmacenID) {
                            case parseInt($("#txtAlmacenMateriaPrima").val()):
                                almacenMateriaPrima = true;
                                break;
                            case parseInt($("#txtAlmacenPlantaAlimento").val()):
                                almacenPlantaAlimento = true;
                                break;
                            case parseInt($("#txtAlmacenBodegaTercero").val()):
                                almacenBodegaTercero = true;
                                break;
                            case parseInt($("#txtAlmacenBodegaExterno").val()):
                                almacenBodegaExterno = true;
                                break;

                        default:
                        }

                    }

                    if (!almacenMateriaPrima) {
                        MensajeSinAlmaceMateriaPrima();
                    }

                    if (!almacenPlantaAlimento) {
                        MensajeSinAlmacePlantaAlimentos();
                    }

                    if (!almacenBodegaTercero) {
                        MensajeSinAlmaceBodegaTercero();
                    }

                    if (!almacenBodegaExterno) {
                        MensajeSinAlmacenBodegaExterna();
                    }
                    
                }
            }
        }
    });
}

ObtenerProductosPorAlmacen = function (almacen) {
    var datos = { "idAlmacen": almacen};
    $.ajax({
        type: "POST",
        url: "SalidaVentaTraspaso.aspx/ObtenerProductosPorAlmacen",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            ValidaMensajeError(request);
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var productos = data.d;
                if (productos != undefined && productos.length > 0) {
                    $("#cmbProducto").removeAttr("disabled");
                    $("#cmbProducto").empty();
                    $("#cmbProducto").append("<option value='0'>Seleccione</option>");
                    for (var i = 0; i < productos.length; i++) {
                        var producto = productos[i].Producto;
                        var subfamilia = producto.SubFamilia;
                        var subfamiliaId = 0;
                        if (subfamilia != null && subfamilia != undefined) {
                            subfamiliaId = subfamilia.SubFamiliaID;
                        }

                        $("#cmbProducto").append("<option value='" + producto.ProductoId + "' data-subfamilia='" + subfamiliaId + "'>" + producto.ProductoDescripcion + "</option>");
                    }

                    $("#cmbLote").empty();
                    $("#cmbLote").attr("disabled", "disabled");
                    $("#txtKilogramos").val("");
                    $("#txtKilogramos").attr("disabled", "disabled");
                    $("#txtPiezas").val("");
                    $("#txtPiezas").attr("disabled", "disabled");
                    $("#txtObservaciones").val("");

                    $("#cmbProducto").focus();
                }
            } else {
                MensajeAlmacenSinProducto();
            }
        }
    });
}

ObtenerLotes = function(idProducto, idAlmacen) {
    var datos = { "idProducto": idProducto, "idAlmacen": idAlmacen };
    $.ajax({
        type: "POST",
        url: "SalidaVentaTraspaso.aspx/ObtenerLotes",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function(request) {
            ValidaMensajeError(request);
        },
        dataType: "json",
        success: function(data) {
            if (data.d) {
                var lotes = data.d;
                if (lotes != undefined && lotes.length > 0) {
                    $("#cmbLote").removeAttr("disabled");
                    $("#cmbLote").empty();
                    if (lotes.length == 1) {
                        $("#cmbLote").append("<option value='" + lotes[0].AlmacenInventarioLoteId + "' data-cantidad='" + lotes[0].Cantidad + "'>" + lotes[0].Lote + "</option>");
                        $("#txtKilogramos").val(accounting.formatNumber(lotes[0].Cantidad));
                        $("#txtPiezas").focus();
                    } else {
                        $("#cmbLote").append("<option value='0'>Seleccione</option>");
                        for (var i = 0; i < lotes.length; i++) {
                            var lote = lotes[i];
                            $("#cmbLote").append("<option value='" + lote.AlmacenInventarioLoteId + "' data-cantidad='" + lote.Cantidad + "'>" + lote.Lote + "</option>");
                        }
                    }

                    $("#txtPiezas").val("");
                    $("#txtObservaciones").val("");
                    $("#txtObservaciones").removeAttr("disabled");
                }
            } else {
                MensajeProductoSinLote();
            }
        }
    });
};

HabilitaPiezas = function(opcion) {
    if (opcion) {
        $('#txtPiezas').removeAttr("disabled");
    } else {

        $('#txtPiezas').attr("disabled", "disabled");
    }

};

BuscarFolios = function () {
    var resultado = true;
    var folio = 0;
    if ($("#txtFolioBuscar").val() != "") {
        folio = $("#txtFolioBuscar").val();
    }

    var datos = { "folioSalida": folio };

    $.ajax({
        type: "POST",
        url: "SalidaVentaTraspaso.aspx/ConsultaFoliosActivos",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            ValidaMensajeError(request);
            resultado = false;
        },
        dataType: "json",
        async:false,
        success: function (data) {
            if (data.d) {
                var salidas = data.d;
                if (salidas != null && salidas != undefined && salidas.length > 0) {
                    $("#tbBusqueda tbody").empty();
                    for (var j = 0; j < salidas.length; j++) {
                        var salida = salidas[j];
                        var row = $("<tr/>");

                        var radio = "<input type='radio' name='rbFolio' value='" + salida.FolioSalida + "' class='folios-resultado' data-movimiento='" + salida.DescripcionMovimiento + "'" +
                            "data-fecha='" + salida.FechaSalida + "' data-clave='" + salida.SalidaProductoId + "'/>";

                        row.append("<td class='span2'><div class='radio'><label style='padding-left:10px;'>&nbsp;" + radio + "" + salida.FolioSalida + "</label></div></td>");
                        row.append("<td class='span4'><label>" + salida.DescripcionMovimiento + "</label></td>");
                        if (salida.TipoMovimiento.TipoMovimientoID != $("#txtTipoMovimiento").val()) {
                            if (salida.OrganizacionDestino != null && salida.OrganizacionDestino.Descripcion != null && salida.OrganizacionDestino.Descripcion != undefined) {
                                row.append("<td class='span5'><label>" + salida.OrganizacionDestino.Descripcion + "</label></td>");
                            } else {
                                row.append("<td class='span5'><label></label></td>");
                            }
                            
                        } else {
                            if (salida.Cliente != null && salida.Cliente.Descripcion != null && salida.Cliente.Descripcion != undefined) {
                                row.append("<td class='span5'><label>" + salida.Cliente.Descripcion + "</label></td>");
                            } else {
                                row.append("<td class='span5'><label></label></td>");
                            }
                            
                        }
                        

                        $("#tbBusqueda").append(row);

                    }
                }
            } else {
                if ($("#txtFolioBuscar").val() != "") {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        $("#dlgBusquedaFolio").modal("hide");
                        bootbox.alert(msgFolioInvalido, function() {
                            msjAbierto = 0;
                            $("#dlgBusquedaFolio").modal("show");
                            $("#txtFolioBuscar").val("");
                            $("#txtFolioBuscar").focus();
                        });
                    }
                    resultado = false;
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        
                        bootbox.alert(msgSinFolios, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        }
    });

    return resultado;

}

//Validaciones
ValidaSubFamilia = function(subfamilia) {
    if (subfamilia != null && subfamilia != undefined) {

        if (subfamilia == $("#txtSubFamiliaForraje").val()) {
            HabilitaPiezas(true);
        } else {
            HabilitaPiezas(false);
        }
    }
}

ValidaDatosRequeridos = function() {
    var resultado = true;

    if ($("#txtFolio").val() == "") {
        MensajeIngresarFolio();
        resultado = false;
    } else if ($("#cmbAlmacen").val() == null || $("#cmbAlmacen").val() == "" || $("#cmbAlmacen").val() == 0) {
        MensajeIngresarAlmacen();
        resultado = false;
    } else if ($("#cmbProducto").val() == null || $("#cmbProducto").val() == "" || $("#cmbProducto").val() == 0) {
        MensajeIngresarProducto();
        resultado = false;
    } else if ($("#cmbLote").val() == null || $("#cmbLote").val() == "" || $("#cmbLote").val() == 0) {
        MensajeIngresarLote();
        resultado = false;
    } else if ((!$("#txtPiezas").prop("disabled")) && ($("#txtPiezas").val() == "" || $("#txtPiezas").val() == 0)) {
        MensajeIngresarPiezas();
        resultado = false;
    } else if ((!$("#txtPiezas").prop("disabled")) &&  !ValidarCantidadPiezas()) {
        resultado = false;
    }

    return resultado;
};

ValidarFolio = function () {
    if ($("#txtFolio").val() > 0) {

        var datos = { "folio": $("#txtFolio").val() };
        $.ajax({
            type: "POST",
            url: "SalidaVentaTraspaso.aspx/ObtenerFolioSalida",
            contentType: "application/json; charset=utf-8", 
            data: JSON.stringify(datos),
            error: function (request) {
                ValidaMensajeError(request);
            },
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    var salida = data.d;
                    ReiniciarFormulario();
                    if ((salida.Almacen == null || salida.Almacen.AlmacenID == 0) &&
                    (salida.AlmacenInventarioLote == null || salida.AlmacenInventarioLote.AlmacenInventarioLoteId == 0)) {
                        if (salida.DescripcionMovimiento != "") {
                            $("#txtSalida").val(salida.DescripcionMovimiento);
                        }
                        $("#txtClaveSalida").val(salida.SalidaProductoId);

                        var fecha = new Date(parseInt(salida.FechaSalida.substr(6)));
                        $("#txtFecha").val(ObtenerFecha(fecha));
                        $("#cmbAlmacen").removeAttr("disabled");
                        $("#DatosSalida").show();
                        folioValido = true;
                        $("#cmbAlmacen").focus();
                    } else {
                        folioValido = false;
                        MensajeSalidaSurtida();
                        ReiniciarFormulario();
                        $("#txtFolio").val("");
                        $("#DatosSalida").hide();
                        
                    }
                    
                } else {
                    folioValido = false;
                    MensajeFolioInvalido();
                    ReiniciarFormulario();
                    $("#DatosSalida").hide();
                }
            }
        });
    } else {

        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgIngresarFolio, function () {
                ReiniciarFormulario();
                $("#DatosSalida").hide();
                $("#txtFolio").focus();
                msjAbierto = 0;
            });
        }
    }
};

ValidaMensajeError = function(cadena) {
    var error = JSON.parse(cadena.responseText);
    var funcion = window[error.Message];
    if (typeof funcion == 'function') {
        funcion();
    } else {
        MensajeError(error.Message);
    }
}

ValidarCantidadPiezas = function () {
    var datos = { "idInventarioLote": $("#cmbLote").val() };
    var resultado = true;
    $.ajax({
        type: "POST",
        url: "SalidaVentaTraspaso.aspx/ObtenerCantidadPiezas",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            ValidaMensajeError(request);
        },
        dataType: "json",
        async:false,
        success: function (data) {
            if (data.d != null) {
                var resultadoData = data.d;
                var cantidad = resultadoData.Piezas;
                var cantidadCapturada = parseInt(QuitarFormato($("#txtPiezas").val()));

                if ( cantidadCapturada > cantidad) {
                    MensajeCantidadPiezas();
                    $("#txtPiezas").val("");
                    $("#txtPiezas").focus();
                    resultado = false;
                } else {
                    $("#txtPiezas").val(accounting.formatNumber($("#txtPiezas").val()));
                }
            }
        }
    });

    return resultado;
}


//Guardar
GuardarInformacion = function() {
    if (ValidaDatosRequeridos()) {

        var salidaProducto;

        salidaProducto = {
            "SalidaProductoId":$("#txtClaveSalida").val(),
            "Observaciones": ($("#txtObservaciones").val() == "" ? null : $("#txtObservaciones").val()),
            "Piezas": ($("#txtPiezas").val() == "" ? 0 : $("#txtPiezas").val()),
            "Almacen": { "AlmacenID": $("#cmbAlmacen").val() },
            "AlmacenInventarioLote": {
                         "AlmacenInventarioLoteID": $("#cmbLote").val()
            }   
        }
        
        var datos = { "salidaProducto":salidaProducto};

        $.ajax({
            type: "POST",
            url: "SalidaVentaTraspaso.aspx/ActualizaSalida",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                ValidaMensajeError(request);
            },
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    var resultado = data.d;
                    if (resultado) {
                        MensajeGuardarExito();
                    } else {
                        
                    }
                }
            }
        });
    }
}

LimpiarFormulario = function () {
    location.href = location.href;
};

//Mensajes
MostrarMensaje = function(mensaje,funcion) {
    if (msjAbierto == 0) {
        msjAbierto = 1;
        bootbox.alert(mensaje, function () {
            msjAbierto = 0;
            if (funcion != undefined) {
                funcion();
            }
        });
    }
}

MensajeIngresarFolio = function() {
    MostrarMensaje(msgIngresarFolio, function () {
        $("#txtFolio").focus();
    });
}

MensajeIngresarProducto = function() {
    MostrarMensaje(msgIngresarProducto, function () {
        $("#cmbProducto").focus();
    });
}

MensajeIngresarLote = function() {
    MostrarMensaje(msgIngresarLote, function () {
        $("#cmbLote").focus();
    });
}

MensajeIngresarAlmacen = function() {
    MostrarMensaje(msgIngresarAlmacen, function () {
        $("#cmbAlmacen").focus();
    });
}

MensajeIngresarPiezas = function() {
    MostrarMensaje(msgIngresarPiezas, function () {
        $("#txtPiezas").focus();
    })
}

MensajeFolioInvalido = function() {
    MostrarMensaje(msgFolioInvalido, function () {
        $("#txtFolio").val("");
        $("#txtFolio").focus();
    });
}

MensajeCantidadPiezas = function() {
    MostrarMensaje(msgCantidadPiezas,function() {
        $("#txtPiezas").focus();
    });
}

MensajeCancelar = function() {
    MostrarMensaje(msgCancelar);
}

MensajeGuardarExito = function() {
    MostrarMensaje("<img src='../Images/Correct.png'/>&nbsp;" + msgGuardarExito, function () {
        LimpiarFormulario();
    });
}

MensajeSeleccionarFolio = function () {
    bootbox.alert(msgSeleccionarFolio, function () {
        $("#dlgBusquedaFolio").modal("show");
        msjAbierto = 0;
    });
};

MensajeSalidaSurtida = function () {
    bootbox.alert(msgSalidaSurtida, function () {
        msjAbierto = 0;
        $("#txtFolio").focus();
    });
};

SesionExpirada = function () {
    bootbox.alert(msgSesionExpirada, function () {
        window.location.href = '../Seguridad/login.aspx';
    });
};


MensajeAlmacenSinProducto = function() {
    bootbox.alert(msgAlmacenSinProducto, function () {
        $("#cmbProducto").empty();
        $("#cmbProducto").attr("disabled", "disabled");
        $("#cmbLote").empty();
        $("#cmbLote").attr("disabled", "disabled");
        $("#txtKilogramos").val("");
        $("#txtPiezas").val("");
        $("#txtPiezas").attr("disabled", "disabled");
        $("#txtObservaciones").val("");
        $("#txtObservaciones").attr("disabled", "disabled");
        msjAbierto = 0;
    });
}

MensajeProductoSinLote = function () {
    bootbox.alert(msgProductoSinLote, function () {
        $("#cmbLote").empty();
        $("#cmbLote").attr("disabled", "disabled");
        $("#txtKilogramos").val("");
        $("#txtPiezas").val("");
        $("#txtPiezas").attr("disabled", "disabled");
        $("#txtObservaciones").val("");
        $("#txtObservaciones").attr("disabled", "disabled");
        msjAbierto = 0;
    });
}

MensajeSinAlmaceMateriaPrima = function () {
    bootbox.alert(msgSinAlmacenMateriaPrima, function() {
        msjAbierto = 0;
    });

}


MensajeSinAlmacePlantaAlimentos = function () {
    bootbox.alert(msgSinAlmacenPlantaAlimento, function () {
            msjAbierto = 0;
        });
}

MensajeSinAlmaceBodegaTercero = function () {
    
    bootbox.alert(msgSinAlmacenBodegaTerceros, function () {
        msjAbierto = 0;
    });
}

MensajeSinAlmacenBodegaExterna = function () {
    bootbox.alert(msgSinAlmacenBodegaExterna, function () {
        msjAbierto = 0;
    });
}

ObtenerFecha = function(fecha) {

    var dia = fecha.getDate();
    var mes = fecha.getMonth() + 1;
    var anio = fecha.getFullYear();
    if (dia < 10) {
        dia = "0" + dia;
    }
    if (mes < 10) {
        mes = "0" + mes;
    }
    
    var fechaFinal = dia + "/" + mes + "/" + anio;

    return fechaFinal;
};

ReiniciarFormulario = function() {
    $("#txtSalida").val("");
    $("#txtClaveSalida").val("");
    $("#txtFecha").val("");
    $("#txtKilogramos").val("");
    $("#cmbProducto").empty();
    $("#cmbLote").empty();
    $("#cmbAlmacen").val("0");
    $("#txtObservaciones").val("");
    $("#txtPiezas").val("");
    $("#cmbLote").attr("disabled", "disabled");
    $("#cmbProducto").attr("disabled", "disabled");
    $("#cmbAlmacen").attr("disabled", "disabled");
}

QuitarFormato = function (texto) {
    var resultado = "";

    if (texto.indexOf(",") > -1) {
        resultado = texto.replace(/,/g, "");
    } else {
        resultado = texto;
    }

    return resultado;
}