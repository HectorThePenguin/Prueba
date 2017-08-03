﻿namespace SIE.Services.Info.Enums
{
    /// <summary>
    /// Enumerador que representa los estatus en la tabla de Estatus 
    /// </summary>
    public enum Estatus
    {
        Pendiente = 1,
        Recibido = 2,
        Cancelado = 3,
        Detectado = 4,
        Recolectado = 5,
        Necropsia = 6,
        MuerteCancelado = 7,
        RecepcionNecropsia = 8,
        Generado = 9,
        Aplicado = 11,
        OrdenSacrificioPendiente = 12,
        Correcto = 13,
        Monton = 14,
        Espacio = 15,
        NoExiste = 16,
        Separacion = 17,
        Desperdicio = 18,
        Barrido = 19,
        PendienteInv = 20,
        AplicadoInv = 21,
        CanceladoInv = 22,
        Nuevo = 23,
        Autorizado = 24,
        Rechazado = 25,
        Aprobado = 26,
        PendienteAutorizar = 27,
        PedidoSolicitado = 28,
        PedidoProgramado = 29,
        PedidoParcial = 30,
        PedidoRecibido = 31,
        PedidoPendiente = 32,
        PedidoCompletado = 33,
        SolicitudProductoPendiente = 34,
        SolicitudProductoAutorizado = 35,
        SolicitudProductoCancelado = 36,
        SolicitudProductoEntregado = 37,
        SolicitudProductoRecibido = 38,
        DifInvAplicado = 39,
        DifInvAutorizado = 40,
        DifInvPendiente = 41,
        DifInvRechazado = 42,
        DifInvCancelado = 43,
        AutorizadoInv = 44,
        ConActivo = 45,
        ConCerrado = 46,
        ConCancela = 47,
        AMPAutoriz = 48,
        AMPRechaza = 49,
        AMPPendien = 50,
        CanceladoEntPro = 51,
        CanceladoPesaje = 52,
        NuevaAlert = 55,
        RegisAlert = 56,
        RechaAlert = 57,
        VenciAlert = 58,
        CerrarAler = 59
    }
}