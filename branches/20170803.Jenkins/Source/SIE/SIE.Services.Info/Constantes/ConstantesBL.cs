namespace SIE.Services.Info.Constantes
{
    public class ConstantesBL
    {
        public static readonly string[] CalidadEnLinea = new[] { "1", "1.5", "2", "3", "3.5" };
        public static readonly string[] CalidadProduccion = new[] { "3.5L", "3.5P", "3.5VT2", "3.5 PR" };
        public static readonly string[] CalidadVenta = new[] { "4", "5", "6", "7" };
        public static readonly string EnLinea = "EnLinea";
        public static readonly string Produccion = "Produccion";
        public static readonly string Venta = "Venta";
        public static readonly int[] PermiteDiasDisponibilidad = new[] { 6, 7 };
        public static readonly string TotalGeneral = "Total General";
        public static readonly string CronicosFilter = "Crnico";
        public static readonly string Cronicos = "Crónicos > 30 Días";
        public static readonly string CronicosEnRecuperacion = "Crónicos en Recuperación";
        public static readonly string CronicoVentaMuerte = "Crónico, Venta o Muerte";
        public static readonly string Recaidos = "Recaidos";
        public static readonly string Total = "Total";
        public static readonly string Seleccione = "Seleccione";
        public static readonly string Banio = "Baño";
        public static readonly string MensajeUsuarioNoExiste = "El usuario y/o contraseña incorrectos. Favor de verificar.";
        public static readonly string MensajeUsuarioRol = "Usuario no cuenta con rol del supervisor en la planta de alimentos.";
        public static readonly string MensajeUsuarioNoExisteSistema = "Usuario no se encuentra dado de alta en el sistema.";
        public static readonly string MovimientoAplicado = "InvAplicad";
        public static readonly string MovimientoCancelado = "InvCancela";
        public static readonly string MovimientoNuevo = "InvNuevo";
        public static readonly string MovimientoPendiente = "InvPendien";
        public static readonly string MovimientoAutorizado = "InvAutori";
        public static readonly string MensajeArchivoNoExiste = "No se encontró archivo en la ruta configurada.";
        public static readonly string MensajeSinParametro = "No se encuentra configurado el parámetro {0}. Favor de verificar.";
        public static readonly string MensajeSinDatosDataLink = "No se encontró información en el archivo, para el operador, fecha, servicio y camión.";

    }
}
