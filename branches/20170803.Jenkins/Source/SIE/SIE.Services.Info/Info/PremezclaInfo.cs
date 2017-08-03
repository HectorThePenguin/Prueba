using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PremezclaInfo
    {
        /// <summary>
        /// Identificador de la premezcla
        /// </summary>
        public int PremezclaId { get; set; }

        /// <summary>
        /// Organizacion a la que pertenece la premezcla
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Descripcion de la premezcla
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Producto de la premezcla
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        /// <summary>
        /// Listado de premezcla detalle
        /// </summary>
        public List<PremezclaDetalleInfo> ListaPremezclaDetalleInfos { get; set; }
        /// <summary>
        /// Permite editar el campo de texto en un grid
        /// </summary>
        public bool Habilitado { get; set; }

        /// <summary>
        /// Indica si la premezcla fue guardada
        /// </summary>
        public bool Guardado { get; set; }

        /// <summary>
        /// Cantidad en el grid
        /// </summary>
        public int Cantidad { get; set; }        
    }
}
