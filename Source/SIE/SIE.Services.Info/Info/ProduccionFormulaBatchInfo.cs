using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ProduccionFormulaBatchInfo
    {
        //
        /// <summary>
        /// Identificador del registro de Produccion Formula Batch
        /// </summary>
        public int ProduccionFormulaBatchID	{ get; set; }

        /// <summary>
        /// Identificador del registro de Produccion Formula
        /// </summary>
        public int ProduccionFormulaID	{ get; set; }

        /// <summary>
        /// Identificador de Organizacion ID
        /// </summary>
        public int OrganizacionID	{ get; set; }

        /// <summary>
        /// Identificador del Producto ID
        /// </summary>
        public int ProductoID	{ get; set; }

        /// <summary>
        /// Identificador de la Formula ID
        /// </summary>
        public int FormulaID	{ get; set; }

        /// <summary>
        /// Identificador del Rotomix ID
        /// </summary>
        public int RotomixID	{ get; set; }

        /// <summary>
        /// Identificador del Batch
        /// </summary>
        public int Batch	{ get; set; }

        /// <summary>
        /// Cantidad Programada del Registro
        /// </summary>
        public int CantidadProgramada	{ get; set; }

        /// <summary>
        /// Cantidad Real del Registro
        /// </summary>
        public int CantidadReal	{ get; set; }

        /// <summary>
        /// Identificador si el registro esta Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Identificador de la Fecha de Creacion del registro 
        /// </summary>
        public DateTime FechaCreacion	{ get; set; }

        /// <summary>
        /// Identificador del Usuario de Creacion del registro 
        /// </summary>
        public int UsuarioCreacionID	{ get; set; }

        /// <summary>
        /// Identificador de la Fecha de Modificacion del registro 
        /// </summary>
        public DateTime FechaModificacion	{ get; set; }

        /// <summary>
        /// Identificador del Usuario de Modificacion del registro 
        /// </summary>
        public int UsuarioModificacionID	{ get; set; }
        /// <summary>
        /// Identificador de la Fecha de Modificacion del registro 
        /// </summary>
        public DateTime FechaProduccion { get; set; }
    }
}
