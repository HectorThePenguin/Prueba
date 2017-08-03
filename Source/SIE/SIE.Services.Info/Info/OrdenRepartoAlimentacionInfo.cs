
using System;

namespace SIE.Services.Info.Info
{
    public class OrdenRepartoAlimentacionInfo
    {
        /// <summary>
        /// Reparto
        /// </summary>
        public RepartoInfo Reparto { get; set; }
       
        /// <summary>
        /// Corral que se generara la orden de reparto
        /// </summary>
        public CorralInfo Corral { get; set; }
        /// <summary>
        /// Lote del corral
        /// </summary>
        public LoteInfo Lote { get; set; }

        /// <summary>
        /// Peso de llegada
        /// </summary>
        public int PesoLlegada { get; set; }
        /// <summary>
        /// Total de animales a los cuales se realizo la suma del peso
        /// </summary>
        public int TotalAnimalesPeso { get; set; }

        /// <summary>
        /// Peso tomado en el ultimo movimiento realizado al animal
        /// </summary>
        public int PesoActual { get; set; }

        /// <summary>
        /// Proyeccion del lote
        /// </summary>
        public LoteProyeccionInfo Proyeccion { get; set; }
        /// <summary>
        /// Ganancia corral
        /// </summary>
        public decimal GananciaCorral { get; set; }
        /// <summary>
        /// Lote reimplante
        /// </summary>
        public LoteReimplanteInfo LoteReimplante { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Identificador del usuario que se encuentra logueado
        /// </summary>
        public int UsuarioID { get; set; }
        /// <summary>
        /// Detalle de la orden de reparto
        /// </summary>
        public  RepartoDetalleInfo DetalleOrdenReparto { get; set; }
        /// <summary>
        /// Tipo de Servicio
        /// </summary>
        public int TipoServicioID { get; set; }

        /// <summary>
        /// Seccion de la que se va a generar la Orden de Reparto
        /// </summary>
        public int Seccion { get; set; }

        /// <summary>
        /// Fecha del Reparto
        /// </summary>
        public DateTime FechaReparto { get; set; }

        public override string ToString()
        {
            return Reparto.ToString();
        }
    }
}
