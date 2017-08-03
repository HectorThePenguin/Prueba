using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SalidaGanadoInfo
    {
        /// <summary>
        /// Identificador De la salida del ganado .
        /// </summary>
        public int SalidaGanadoID { get; set; }
        /// <summary>
        /// Informacion de la organizacion que genera la salida
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Tipo de movimiento de la salida(Venta, Muerte, Sacrificio, etc.)
        /// </summary>
        public TipoMovimiento TipoMovimiento { get; set; }
        /// <summary>
        /// Fecha que se genera la salida
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Folio de la salida --> segenera mediante un foliador
        /// </summary>
        public int Folio { get; set; }
        /// <summary>
        /// Informacion de la venta cuando este proviene de una venta de ganado
        /// </summary>
        public VentaGanadoInfo VentaGanado { get; set; }
        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Usuario Creaciojn del registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public DateTime UsuarioModificacionID { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

    }
}
