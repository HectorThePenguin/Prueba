using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class FleteInfo
    {
        /// <summary>
        /// Flete id
        /// </summary>
        public int FleteID { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Contrato id
        /// </summary>
        public int ContratoID { get; set; }
        /// <summary>
        /// Proveedor chofer info
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }
        /// <summary>
        /// Merma permitida
        /// </summary>
        public decimal MermaPermitida { get; set; }
        /// <summary>
        /// Tipo tarifa del flete
        /// </summary>
        public TipoTarifaInfo TipoTarifa { get; set; }
        /// <summary>
        /// Descripcion del flete
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Estatus del flete
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha creacion del flete
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario creacionid
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Fecha Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario modificacionid
        /// </summary>
        public int UsuarioModificacionID { get; set; }
        /// <summary>
        /// Flete detalle info
        /// </summary>
        public FleteDetalleInfo FleteDetalle { get; set; }
        /// <summary>
        /// lista Flete detalle info
        /// </summary>
        public List<FleteDetalleInfo> LisaFleteDetalleInfo { get; set; } 
        /// <summary>
        /// Opcion
        /// </summary>
        public int Opcion { get; set; }
        /// <summary>
        /// Indica si el registro fue guardado
        /// </summary>
        public bool Guardado { get; set; }
    }
}
