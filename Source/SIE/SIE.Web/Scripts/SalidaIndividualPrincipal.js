$(document).ready(function () {
    $("#rdSalidaRecuperacion").click(function () {
        location.href = "SalidaIndividualRecuperacion.aspx";
    });

    $("#rdSalidaSacrificio").click(function () {
        location.href = "SalidaIndividualSacrificio.aspx";
    });

    $("#rdSalidaVenta").click(function () {
        location.href = "SalidaIndividualVenta.aspx";
    });
});
//Validar que el usuario tenga permisos suficientes
EnviarMensajeUsuario = function() {
    bootbox.alert(msgNoTienePermiso, function () {
        location.href = "../Principal.aspx";
        return false;
    });
}