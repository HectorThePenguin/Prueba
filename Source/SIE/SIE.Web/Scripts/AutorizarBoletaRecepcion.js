/// <reference path="../Administracion/Notificaciones.aspx" />
/// <reference path="../Administracion/Notificaciones.aspx" />
/// <reference path="../Administracion/Notificaciones.aspx" />
/// <reference path="../Administracion/Notificaciones.aspx" />
/// <reference path="../Administracion/Notificaciones.aspx" />
//Variables Globales
var msjAbierto = 0;
var indicadores = 0;
var folio = 0;

//Funcionamiento controles
$(document).ready(function () {
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtTicket").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 9, "greedy": false });

    ObtenerFoliosPendientes();
    LimpiarIndicadores();

    //KeyPress
    $("#txtFolioBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtFolio").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            ObtenerFolio();
            e.preventDefault();
        } else if (code == 9) {
            ObtenerFolio();
        }
    });

    $("#txtTicket").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            $("#txtFolio").val("");
            ObtenerTicket();
            e.preventDefault();
        } else if (code == 9) {
            $("#txtFolio").val("");
            ObtenerTicket();
        }
    });

    //Click
    $("#btnAutorizar").click(function () {
        AutorizarEntrada();
    });

    $("#btnBuscar").click(function () {
        ObtenerFoliosPendientes();
        var registros = $("#tbBusqueda tbody tr");
        if (!(registros.length > 0)) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
                bootbox.alert(window.msgSinFolios, function () {
                    $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });

    $("#btnAgregarBuscar").click(function () {
        var renglones = $("input[class=indicadores]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtFolio").val($(this).attr("folio"));
            });
            ObtenerFolio();
            $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
            ObtenerFoliosPendientes();
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
                bootbox.alert(window.msgSeleccionarFolio, function () {
                    $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
                    msjAbierto = 0;
                });
            }
        }
    });

    $("#btnCancelarBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
    });

    if (parseInt($("#hdnFolioEntrada").val()) > 0) {
        obtenerFoliosAutorizados();
        $("#btnCancelar").text('Aceptar');
        $("#btnCancelar").click(function () {
            location.href = "Notificaciones.aspx";
        });
    } else {
        $("#btnCancelar").attr("href", "#dlgCancelar");
    }

    $("#btnDialogoSi").click(function() {
        location.href = location.href;
    });

    $("#btnRechazarSi").click(function () {
        RechazarEntrada();
    });

    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaFolioBoletaRecepcion").modal("hide");
        $("#txtFolioBuscar").val("");
        ObtenerFoliosPendientes();
    });

    $("#btnNoBuscar").click(function () {
        $("#dlgBusquedaFolioBoletaRecepcion").modal("show");
    });    
});

//Validaciones y consultas
//Obtiene los folios que se encuentran pendientes de autorizar
ObtenerFoliosPendientes = function () {
    var datos = {};
    if ($("#txtFolioBuscar").val() != "") {
        datos = { "folio": $("#txtFolioBuscar").val() };
    } else {
        datos = { "folio": 0 };
    }
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerEntradaProductosPendiente",
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
                var resultado = data.d;
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    var row = "<tr>" +
                        "<td class='span1'><input type='checkbox' class='indicadores' id='entrada" + resultado[i].EntradaProductoId + "' folio='" + resultado[i].Folio + "' onclick='SeleccionaUno(\"#entrada" + resultado[i].EntradaProductoId + "\");'/></td>" +
                        "<td class='span1' style='text-align:right;'>" + resultado[i].EntradaProductoId + "</td>" +
                        "<td class='span1' style='text-align:right;'>" + resultado[i].Folio + "</td>" +
                        "<td class='span2' style='text-align:right;'>" + resultado[i].Contrato.Folio + "</td>" +
                        "<td class='span3'>" + resultado[i].Producto.ProductoDescripcion + "</td>"+
                        "<td class='span3'>" + resultado[i].RegistroVigilancia.ProveedorMateriasPrimas.Descripcion + "</td>"+
                        "<td class='span1'>Pendiente por Autorizar</td>" +
                        "</tr>";
                    $("#tbBusqueda tbody").append(row);
                }
            }
        }
    });
};

//Selecciona solo un checkbox
SeleccionaUno = function (Id) {
    var listaCheckBox = $(".indicadores");
    var checkbox = $(Id);
    if (checkbox.is(":checked")) {
        listaCheckBox.each(function () {
            this.checked = false;
        });
        checkbox.attr("checked", true);
    }
};

obtenerFoliosAutorizados = function () {
    $("#txtFolio").val($("#hdnFolioEntrada").val());
    var datos = { "folio": $("#hdnFolioEntrada").val() };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerFoliosAutorizados",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request, ajaxOptions, thrownError) {
            App.desbloquearContenedor($(".container-fluid"));
            var respuesta = $.parseJSON(request.responseText);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(respuesta.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            App.desbloquearContenedor($(".container-fluid"));
            if (data.d) {
                var entradaProducto = data.d.EntradaProducto;
                    $("#txtTicket").val(entradaProducto.RegistroVigilancia.FolioTurno);
                    $("#cmbDestino").val(entradaProducto.TipoContrato.TipoContratoId);
                    $("#cmbProducto").append("<option value='" + entradaProducto.RegistroVigilancia.Producto.ProductoId + "' selected>" + entradaProducto.RegistroVigilancia.Producto.ProductoDescripcion + "</option>");
                    if (entradaProducto.Contrato.TipoFlete.TipoFleteId == $('#txtPagoEnGanadera').val()) {
                        ObtenerContratos(entradaProducto.Contrato.ContratoId, entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID, entradaProducto.RegistroVigilancia.Producto.ProductoId, entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.RegistroVigilanciaId);
                        ObtenerListaChofer(entradaProducto.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProducto.RegistroVigilancia.ProveedorChofer.Chofer.ChoferID);
                        ObtenerListaPlacas(entradaProducto.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProducto.RegistroVigilancia.Camion.CamionID);
                    }
                    else {
                        $('#cmbContrato').append("<option value='" + entradaProducto.Contrato.ContratoId + "' selected>" + entradaProducto.Contrato.Folio + "</option>");
                        $('#cmbProveedor').append("<option selected>" + entradaProducto.RegistroVigilancia.Transportista + "</option>");
                        $('#cmbChofer').append("<option selected>" + entradaProducto.RegistroVigilancia.Chofer + "</option>");
                        $('#cmbPlacas').append("<option selected>" + entradaProducto.RegistroVigilancia.CamionCadena + "</option>");
                    }
                $("#txtJustificacion").val(entradaProducto.Justificacion);
                $("#tbIndicadores tbody tr").html('');
                ObtenerIndicadores(entradaProducto.EntradaProductoId);
                actualizarRevisionGerenteEngorda(entradaProducto.EntradaProductoId);

                $("#dvJustificacion").css("display", "block");
                $("#txtJustificacion").attr("disabled", "disabled");
                $("#txtFolio").attr("disabled", "disabled");
                $("#lblBuscar").removeAttr("href");
                $("#btnAutorizar").hide();
                $("#btnRechazar").hide();
            }
        }
    });
};

//Obtiene el folio tecleado
ObtenerFolio = function () {
    if ($("#txtFolio").val() != "" && (folio != $("#txtFolio").val() || folio == 0)) {
        App.bloquearContenedor($(".container-fluid"));
        var datos = { "folio": $("#txtFolio").val() };
        $.ajax({
            type: "POST",
            url: "AutorizarBoletaRecepcion.aspx/ObtenerFolio",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request, ajaxOptions, thrownError) {
                App.desbloquearContenedor($(".container-fluid"));
                var respuesta = $.parseJSON(request.responseText);
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(respuesta.Message, function () {
                        msjAbierto = 0;
                    });
                }
            },
            dataType: "json",
            success: function (data) {
                App.desbloquearContenedor($(".container-fluid"));
                if (data.d) {
                    var entradaProducto = data.d;
                    if (entradaProducto.Estatus.EstatusId != $("#txtAprobado").val() && entradaProducto.Estatus.EstatusId != $("#txtRechazado").val() && entradaProducto.Estatus.EstatusId != $("#txtAutorizado").val()) {
                        $("#txtTicket").val(entradaProducto.RegistroVigilancia.FolioTurno);
                        $("#cmbDestino").val(entradaProducto.TipoContrato.TipoContratoId);
                        $("#cmbProducto").append("<option value='" + entradaProducto.RegistroVigilancia.Producto.ProductoId + "' selected>" + entradaProducto.RegistroVigilancia.Producto.ProductoDescripcion + "</option>");
                        if (entradaProducto.Contrato.TipoFlete.TipoFleteId == $('#txtPagoEnGanadera').val()) {
                            ObtenerContratos(entradaProducto.Contrato.ContratoId, entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID, entradaProducto.RegistroVigilancia.Producto.ProductoId, entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.RegistroVigilanciaId);
                            ObtenerListaChofer(entradaProducto.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProducto.RegistroVigilancia.ProveedorChofer.Chofer.ChoferID);
                            ObtenerListaPlacas(entradaProducto.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID, entradaProducto.RegistroVigilancia.Camion.CamionID);
                        }
                        else {
                            $('#cmbContrato').append("<option value='" + entradaProducto.Contrato.ContratoId + "' selected>" + entradaProducto.Contrato.Folio + "</option>");
                            $('#cmbProveedor').append("<option selected>" + entradaProducto.RegistroVigilancia.Transportista + "</option>");
                            $('#cmbChofer').append("<option selected>" + entradaProducto.RegistroVigilancia.Chofer + "</option>");
                            $('#cmbPlacas').append("<option selected>" + entradaProducto.RegistroVigilancia.CamionCadena + "</option>");
                        } 
                        $("#tbIndicadores tbody tr").html('');
                        ObtenerIndicadores(entradaProducto.EntradaProductoId);

                        $("#dvJustificacion").css("display", "block");
                    } else {
                        if (entradaProducto.Estatus.EstatusId == $("#txtAprobado").val()) {
                            LimpiarFormulario();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioAprobado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (entradaProducto.Estatus.EstatusId == $("#txtRechazado").val()) {
                            LimpiarFormulario();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioRechazado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        } else if (entradaProducto.Estatus.EstatusId == $("#txtAutorizado").val()) {
                            LimpiarFormulario();
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(window.msgFolioAutorizado, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                    }
                } else {
                    LimpiarFormulario();
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgFolioInvalido, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        });
    }
};

//Obtiene los proveedores
ObtenerProveedores = function (ProveedorID) {
    App.bloquearContenedor($(".container-fluid"));
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerProveedores",
        contentType: "application/json; charset=utf-8",
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
                $("#cmbProveedor").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ProveedorID == datos[i].ProveedorID) {
                        $("#cmbProveedor").append("<option value='" + datos[i].ProveedorID + "'selected>" + datos[i].Descripcion + "</option>");
                    } else {
                        $("#cmbProveedor").append("<option value='" + datos[i].ProveedorID + "'>" + datos[i].Descripcion + "</option>");
                    }
                }
            } else {
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgSinProveedores, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

//Obtiene los productos del proveedor
ObtenerProductosProveedor = function (ProveedorID, ProductoID) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerProductosProveedor",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
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
                $("#cmbProducto").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ProductoID == datos[i].ProductoId) {
                        $("#cmbProducto").append("<option value='" + datos[i].ProductoId + "' selected>" + datos[i].ProductoDescripcion + "</option>");
                    } else {
                        $("#cmbProducto").append("<option value='" + datos[i].ProductoId + "'>" + datos[i].ProductoDescripcion + "</option>");
                    }
                }
            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinProducto, function () {
                        msjAbierto = 0;
                    });
                }
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
            }
        }
    });
};

//Obtiene los choferes del proveedor
ObtenerContratos = function (ContratoID,ProveedorID, ProductoID, RegistroVigilanciaID) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID, "productoId": ProductoID };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerProveedorContratos",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
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
                $("#cmbContrato").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ContratoID == datos[i].ContratoId) {
                        $("#cmbContrato").append("<option value='" + datos[i].ContratoId + "' selected>" + datos[i].Folio + "</option>");
                    } else {
                        $("#cmbContrato").append("<option value='" + datos[i].ContratoId + "'>" + datos[i].Folio + "</option>");
                    }
                }

                if (datos.length > 0) {
                    ObtenerProveedores(ProveedorID);
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgProveedorSinContrato, function () {
                            msjAbierto = 0;
                        });
                    }
                    $("#cmbContrato").html("");
                    $("#cmbProducto").html("");
                    $("#cmbChofer").html("");
                    $("#cmbPlacas").html("");
                    $("#cmbProveedor").html("");
                }
            } else {
                $("#cmbContrato").html("");
                $("#cmbProducto").html("");
                $("#cmbChofer").html("");
                $("#cmbPlacas").html("");
                $("#cmbProveedor").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinContrato, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

//Obtiene los choferes del proveedor
ObtenerListaChofer = function (ProveedorID, ChoferID) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerProveedorChofer",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
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
                $("#cmbChofer").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (ChoferID == datos[i].Chofer.ChoferID) {
                        $("#cmbChofer").append("<option value='" + datos[i].Chofer.ChoferID + "' selected>" + datos[i].Chofer.NombreCompleto + "</option>");
                    } else {
                        $("#cmbChofer").append("<option value='" + datos[i].Chofer.ChoferID + "'>" + datos[i].Chofer.NombreCompleto + "</option>");
                    }
                }
            } else {
                $("#cmbChofer").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinChofer, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

//Obtiene las placas del proveedor
ObtenerListaPlacas = function (ProveedorID, PlacaCamion) {
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "proveedorId": ProveedorID };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerProveedorPlacas",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
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
                $("#cmbPlacas").html("");
                for (var i = 0; i < datos.length; i++) {
                    if (PlacaCamion == datos[i].CamionID) {
                        $("#cmbPlacas").append("<option value='" + datos[i].CamionID + "' selected>" + datos[i].PlacaCamion + "</option>");
                    } else {
                        $("#cmbPlacas").append("<option value='" + datos[i].CamionID + "'>" + datos[i].PlacaCamion + "</option>");
                    }
                }
            } else {
                $("#cmbPlacas").html("");
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgProveedorSinPlacas, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });
};

actualizarRevisionGerenteEngorda = function (entradaProductoId) {
    var datos = { "entradaProductoId": entradaProductoId };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ActualizarRevisionGerente",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request, ajaxOptions, thrownError) {
            App.desbloquearContenedor($(".container-fluid"));
            var respuesta = $.parseJSON(request.responseText);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(respuesta.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
        }
    });
};


//Obtiene los indicadores de la entrada
ObtenerIndicadores = function (EntradaProductoID) {
    LimpiarIndicadores();
    var datos = { "entradaProductoId": EntradaProductoID };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/ObtenerDiferenciasIndicadores",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request, ajaxOptions, thrownError) {
            App.desbloquearContenedor($(".container-fluid"));
            var respuesta = $.parseJSON(request.responseText);
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(respuesta.Message, function () {
                    msjAbierto = 0;
                });
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                if (data.d.length > 0) {
                    var indicadores = data.d;
                    var renglones = $("#tbIndicadores tbody tr");
                    for (var i = 0; i < indicadores.length; i++) {
                        if (i >= 13) {
                            $("#tbIndicadores tbody").append("<tr>" +
                                                            "<td class='span5'>&nbsp;</td>" +
                                                            "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                            "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                                            "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                            "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                                            "</tr>");
                        }
                        renglones[i].id = indicadores[i].IndicadorId;
                        renglones[i].cells[0].innerHTML = indicadores[i].Descripcion;
                        renglones[i].cells[3].innerHTML = parseFloat(indicadores[i].PorcentajeMuestra.toFixed(2)).toFixed(2);
                        renglones[i].cells[4].innerHTML = parseFloat(indicadores[i].PorcentajeContrato.toFixed(2)).toFixed(2);
                        if (indicadores[i].PorcentajeMuestra != indicadores[i].PorcentajeContrato) {
                            $('#tbIndicadores tbody tr:eq(' + i + ') td').css("background-color", "rgb(248, 141, 141)");
                        }
                    }
                }
            }
        }
    });
};

//Borrar Indicadores grid
LimpiarIndicadores = function () {
    $("#tbIndicadores tbody").html('');
    for (var i = 0; i < 14; i++) {
        $("#tbIndicadores tbody").append("<tr>" +
                                        "<td class='span5'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td style='width: 0px;display: none;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "<td class='span3' style='text-align:right;'>&nbsp;</td>" +
                                        "</tr>");
    }
}

//Limpia la pantalla
LimpiarFormulario = function () {
    $("#txtTicket").val('');
    $("#cmbContrato").html("");
    $("#cmbProducto").html("");
    $("#cmbChofer").html("");
    $("#cmbPlacas").html("");
    $("#cmbProveedor").html("");
    LimpiarIndicadores();
}

//Autoriza la entrada
AutorizarEntrada = function () {

    if ($("#txtFolio").val() != "") {
        if ($("#txtJustificacion").val() == "") {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgDatosBlanco, function () {
                    msjAbierto = 0;
                    $('#txtJustificacion').focus();
                });
            }
            return false;
        }
        if ($("#txtOperadorID").val() == "") {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgOperadorRequerido, function () {
                    msjAbierto = 0;
                });
            }
            return false;
        }

        var entradaProducto = { "Folio": $("#txtFolio").val(), "Justificacion": $("#txtJustificacion").val() };
                
        App.bloquearContenedor($(".container-fluid"));
        var datos = { "entradaProducto": entradaProducto, "estatusId": $('#txtAutorizado').val() };
        $.ajax({
            type: "POST",
            url: "AutorizarBoletaRecepcion.aspx/AutorizarRechazarEntrada",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
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
                var datos = data.d;
                if (datos.Estatus.EstatusId == 27) { //Pendiente de autorizar
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert('<table>' +
                                        '<tr>' +
                                            '<td>' +
                                                '<img class="span1 no-left-margin" src="../Images/Correct.png"></img>' +
                                            '</td>' +
                                            '<td>' +
                                                '<label class="span4" style="word-wrap:true;">'+ window.msgSolicitaAutorizacion + '</label>' +
                                            '</td>' +
                                        '</tr>' +
                                    '</table>',
                                            function () {
                                                location.href = location.href;
                                                msjAbierto = 0;
                                            });

                    }
                } else if (datos.Estatus.EstatusId >= 24 && datos.Estatus.EstatusId < 27) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert("<img src='../Images/Correct.png'/>" + window.msgDatosGuardados, function () {
                            location.href = location.href;
                            msjAbierto = 0;
                        });
                    }
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(window.msgErrorGuardar, function () {
                            msjAbierto = 0;
                        });
                    }
                }
            }
        });
    } else {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgFolioAutorizacionRequerido, function () {
                msjAbierto = 0;
                $('#txtFolio').focus();
            });
        }
    }
};

//Rechaza la entrada
RechazarEntrada = function () {
    if ($("#txtFolio").val() <= 0) {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgFolioRechazoRequerido, function () {
                msjAbierto = 0;
                $("#txtFolio").focus();
            });
        }
        return false;
    }
    if ($("#txtJustificacion").val() == "") {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgDatosBlanco, function () {
                msjAbierto = 0;
                $("#txtJustificacion").focus();
            });
        }
        return false;
    }
    if ($("#txtOperadorID").val() == "") {
        if (msjAbierto == 0) {
            msjAbierto = 1;
            bootbox.alert(window.msgOperadorRequerido, function () {
                msjAbierto = 0;
            });
        }
        return false;
    }
    var entradaProducto = { "Folio": $("#txtFolio").val(), "Justificacion": $("#txtJustificacion").val() };
    App.bloquearContenedor($(".container-fluid"));
    var datos = { "entradaProducto": entradaProducto, "estatusId": $('#txtRechazado').val() };
    $.ajax({
        type: "POST",
        url: "AutorizarBoletaRecepcion.aspx/AutorizarRechazarEntrada",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
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
            var datos = data.d;
            if (datos.Estatus.EstatusId >= 24 && datos.Estatus.EstatusId < 27) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert("<img src='../Images/Correct.png'/>" + window.msgDatosGuardados, function () {
                        location.href = location.href;
                        msjAbierto = 0;
                    });
                }
            } else {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgErrorRechazar, function () {
                        msjAbierto = 0;
                    });
                }
            }
        }
    });

}