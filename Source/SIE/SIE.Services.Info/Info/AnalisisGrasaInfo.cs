using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AnalisisGrasaInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int AnalisisGrasaID { get; set; }
        
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public EntradaProductoInfo EntradaProdructo { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public string TipoMuestra { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public decimal PesoMuestra { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public decimal PesoTuboSeco { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public decimal PesoTuboMuestra { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public decimal Impurezas { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Fecha en que se creo la solicitud
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo la solicitud
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha en que se modifico la solicitud
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica la solicitud
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

    }
}
