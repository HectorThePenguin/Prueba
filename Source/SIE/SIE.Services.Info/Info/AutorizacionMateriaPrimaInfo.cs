using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AutorizacionMateriaPrimaInfo
    {
        /// <summary>
        /// Identificador de autorizacion de materia prima
        /// </summary>
        public int AutorizacionMateriaPrimaID { get; set; }
        /// <summary>
        /// Identificador de la organizacion de usuario
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Tipo de autorizacion
        /// </summary>
        public int TipoAutorizacionID { get; set; }
        /// <summary>
        /// Folio movimiento
        /// </summary>
        public long Folio { get; set; }
        /// <summary>
        /// Justificacion para la autorizacion
        /// </summary>
        public string Justificacion { get; set; }
        /// <summary>
        /// Identificador del lote
        /// </summary>
        public int Lote { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductoID { get; set; }
        /// <summary>
        /// Identificador del almacen
        /// </summary>
        public int AlmacenID { get; set; }
        /// <summary>
        /// Identificador del estatus
        /// </summary>
        public int EstatusID { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacion { get; set; }
        /// <summary>
        /// Estatus
        /// </summary>
        public int Activo { get; set; }
        /// <summary>
        /// Info de la programacion materia
        /// </summary>
        public ProgramacionMateriaPrimaInfo Programacion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal CantidadProgramada { get; set; }
    }
}
