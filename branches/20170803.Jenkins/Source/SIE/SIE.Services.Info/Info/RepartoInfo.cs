using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Reparto")]
    public class RepartoInfo
    {
        /// <summary>
        /// Identificador del reparto
        /// </summary>
        public long RepartoID { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Identificador del lote
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Fecha de aplicacion
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Peso inicio
        /// </summary>
        public int PesoInicio { get; set; }
        /// <summary>
        /// Peso proyectado
        /// </summary>
        public int PesoProyectado { get; set; }
        /// <summary>
        /// Dias de engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Peso repeso
        /// </summary>
        public int PesoRepeso { get; set; }
        /// <summary>
        /// Detalle del reparto
        /// </summary>
        public IList<RepartoDetalleInfo> DetalleReparto { get; set; }
        /// <summary>
        /// Corral que se tiene que revisar.
        /// </summary>
        public CorralInfo Corral { get; set; }
        /// <summary>
        /// Numero Total de Repartos.
        /// </summary>
        public int TotalRepartos { get; set; }
        /// <summary>
        /// Numero Total de Repartos leidos.
        /// </summary>
        public int TotalRepartosLeidos { get; set; }

        /// <summary>
        /// Id del Usuario de Creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Numero Total de Repartos leidos.
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int CantidadPedido  { get; set; }

        /// <summary>
        /// Cantidad Programada en la maniana
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int CantidadProgramadaManiana { get; set; }

        /// <summary>
        /// Id del Usuario de Creacion
        /// </summary>
        public EstatusEnum Activo { get; set; }

        public override string ToString()
        {
            return RepartoID.ToString();
        }
    }
}
