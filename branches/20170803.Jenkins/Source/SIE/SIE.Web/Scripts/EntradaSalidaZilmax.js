/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

$(document).ready(function () {

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

    InicializarFecha = function () {
        var fecha = new Date();
        fechaSeleccionada = FechaFormateada(fecha);
        $("#datepicker").val(fechaSeleccionada);
    };

    CargarDatos = function () {
        var tcbzZ = 0;
        App.bloquearContenedor($(".container-fluid"));
        var datePicker = ToDate($("#datepicker").val());
        var fecha = { "fechaZilmax": datePicker };
        $.ajax({
            type: "POST",
            url: "EntradaSalidaZilmax.aspx/TraerEntradaSalidaZilmax",
            datatype: "Json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(fecha),
            success: function (data) {
                if (data.d == null || data.d.length == 0)
                {
                    App.desbloquearContenedor($(".container-fluid"));
                    bootbox.alert("No exiten corrales disponibles para entrar a zilmax o salida a zilmax");
                }
                else
                {
                    for (var i = 0; i < data.d.length; i++)
                    {
                        if (data.d[i].Tipo == 'entrada')
                        {
                            if (data.d[i].Estatus == 'ya debe entrar a zilmax')
                            {
                                $("#tbEntradaZilmax tbody").append("<tr>" +
                                  "<td style='background-color:red; text-align: center;'>&nbsp;</td>" +
                                  "<td style='background-color:red; text-align: center;'>&nbsp;</td>" +
                                  "</tr>");
                            }
                            else
                            {
                                $("#tbEntradaZilmax tbody").append("<tr>" +
                                 "<td style='text-align: center;'>&nbsp;</td>" +
                                 "<td style='text-align: center;'>&nbsp;</td>" +
                                 "</tr>");
                            }
                        }
                        else
                        {
                            if (data.d[i].Tipo == 'salida')
                            {

                                if (data.d[i].Estatus == 'por salida a zilmax pero no se les a servido formula de retiro')
                                {
                                    $("#tbSalidaZilmax tbody").append("<tr>" +
                                      "<td style='background-color:red;text-align: center;'>&nbsp;</td>" +
                                      "<td style='background-color:red;text-align: center;'>&nbsp;</td>" +
                                      "</tr>");
                                }
                                else
                                {
                                    $("#tbSalidaZilmax tbody").append("<tr>" +
                                      "<td style='text-align: center;'>&nbsp;</td>" +
                                      "<td style='text-align: center;'>&nbsp;</td>" +
                                      "</tr>");
                                }
                            }
                        }
                    }
                    var renglonesEZ = $("#tbEntradaZilmax tbody tr");
                    var renglonesSZ = $("#tbSalidaZilmax tbody tr");
                    var j = 0
                    if (renglonesEZ.length == 0) {
                        App.desbloquearContenedor($(".container-fluid"));
                        bootbox.alert("No exiten corrales disponibles para entrar a zilmax");
                        App.bloquearContenedor($(".container-fluid"));
                    }
                    else
                    {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].Tipo == 'entrada') {
                                renglonesEZ[j].cells[0].innerHTML = data.d[i].Corral;
                                renglonesEZ[j].cells[1].innerHTML = data.d[i].Formula;
                                tcbzZ = tcbzZ + data.d[i].Cbz;
                                j++;
                            }
                        }
                    }
                    $("#txtCbzEntrada").val(tcbzZ)
                    $("#TxtCorralesEntrada").val(j)
                    $("#txtCbzEntrada").val(accounting.formatNumber($("#txtCbzEntrada").val().replace(/,/g, '').replace(/_/g, ''), 0, ","));
                    $("#TxtCorralesEntrada").val(accounting.formatNumber($("#TxtCorralesEntrada").val().replace(/,/g, '').replace(/_/g, ''), 0, ","));
                    
                    j = 0; tcbzZ = 0;
                    if (renglonesSZ.length == 0) {
                        App.desbloquearContenedor($(".container-fluid"));
                        bootbox.alert("No exiten corrales disponibles con salida a zilmax");
                        App.bloquearContenedor($(".container-fluid"));
                    }
                    else
                    {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].Tipo == 'salida') {
                                renglonesSZ[j].cells[0].innerHTML = data.d[i].Corral;
                                renglonesSZ[j].cells[1].innerHTML = data.d[i].Formula;
                                tcbzZ = tcbzZ + data.d[i].Cbz;
                                j++;
                            }
                        }
                    }
                    $("#txtCbzSalida").val(tcbzZ)
                    $("#TxtCorralesSalida").val(j)
                    $("#txtCbzSalida").val(accounting.formatNumber($("#txtCbzSalida").val().replace(/,/g, '').replace(/_/g, ''), 0, ","));
                    $("#TxtCorralesSalida").val(accounting.formatNumber($("#TxtCorralesSalida").val().replace(/,/g, '').replace(/_/g, ''), 0, ","));
                }
                App.desbloquearContenedor($(".container-fluid"));
            },
            error: function (xhr, ajaxOptions, error)
            {
                App.desbloquearContenedor($(".container-fluid"));
                bootbox.dialog({
                    message: 'Ocurrió un error al consultar la información.',
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {

                            }
                        }
                    }
                });
            }
        }); 
    };

    $("#btnSalir").click(function () {
        $("#dlgCancelarBuscar").modal("show");
    });

    $("#btnSiBuscar").click(function () {
        window.location.href = "../Principal.aspx";
    });
 
    $("#txtCbzEntrada").val("");
    $("#TxtCorralesEntrada").val("");
    $("#txtCbzSalida").val("");
    $("#TxtCorralesSalida").val("");
    $("#txtCbzEntrada").attr("disabled", true);
    $("#TxtCorralesEntrada").attr("disabled", true);
    $("#txtCbzSalida").attr("disabled", true);
    $("#TxtCorralesSalida").attr("disabled", true);
    $('#txtCbzEntrada').css('text-align', 'right');
    $('#TxtCorralesEntrada').css('text-align', 'right');
    $('#txtCbzSalida').css('text-align', 'right');
    $('#TxtCorralesSalida').css('text-align', 'right');
    $("#datepicker").css('text-align', 'right');

    InicializarFecha();

    CargarDatos();
});

