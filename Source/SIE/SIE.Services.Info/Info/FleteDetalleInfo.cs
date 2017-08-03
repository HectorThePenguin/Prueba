using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class FleteDetalleInfo
    {
        /// <summary>
        /// Identificador del flete detalle info
        /// </summary>
        public int FleteDetalleID { get; set; }
        /// <summary>
        /// Flete ID
        /// </summary>
        public int FleteID { get; set; }
        /// <summary>
        /// Costo id
        /// </summary>
        public int CostoID { get; set; }
        /// <summary>
        /// Tarifa
        /// </summary>
        public decimal Tarifa { get; set; }
        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacion { get; set; }
        /// <summary>
        /// Costo descripcion
        /// </summary>
        public string Costo { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public  DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario Modificacion
        /// </summary>
        public  int UsuarioModificacion { get; set; }
        /// <summary>
        /// Opcion
        /// </summary>
        public int Opcion { get; set; }
    }
}
