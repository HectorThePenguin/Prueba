using System;
using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class ServicioAlimentoInfo : BitacoraInfo
    {
        /// <summary>
        /// Acceso OrganizacionID
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Acceso ServicioID
        /// </summary>
        public int ServicioID { get; set; }


        /// <summary>
        /// Acceso CorralID
        /// </summary>
        public int CorralID { get; set; }


        /// <summary>
        /// Acceso Fecha
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Acceso FormulaID
        /// </summary>
        public int FormulaID { get; set; }

        /// <summary>
        /// Acceso kilosProgramados
        /// </summary>
        public int KilosProgramados { get; set; }

        /// <summary>
        /// Acceso Comentarios
        /// </summary>
        public string Comentarios { get; set; }

        /// <summary>
        /// Acceso FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Acceso FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Acceso DescripcionFormula
        /// </summary>
        public string DescripcionFormula { get; set; }

        /// <summary>
        /// Acceso CodigoCorral
        /// </summary>
        public string  CodigoCorral { get; set; }

        /// <summary>
        /// Acceso Activo
        /// </summary>
        public int Estatus { get; set; }

        /// <summary>
        /// Acceso FechaFormateada
        /// </summary>
        public string FechaRegistro { get; set; }

        public IList<ServicioAlimentoInfo> ListaServicioAlimentoInfos { get; set; }
    }
}
