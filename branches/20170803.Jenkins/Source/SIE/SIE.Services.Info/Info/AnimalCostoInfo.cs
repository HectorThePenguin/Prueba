using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AnimalCostoInfo
    {
        /// <summary>
        /// Identificador del AnimalCosto
        /// </summary>
        public long AnimalCostoID { get; set; }
        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Fecha Registro del Costo
        /// </summary>
        public DateTime FechaCosto { get; set; }
        /// <summary>
        /// CostoID 
        /// </summary>
        public int CostoID { get; set; }

        /// <summary>
        /// Costo 
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        /// Folio de referencia
        /// </summary>
        public TipoReferenciaAnimalCosto TipoReferencia { get; set; }
        /// <summary>
        /// Folio de referencia
        /// </summary>
        public long FolioReferencia { get; set; }
        /// <summary>
        /// Importe del costo
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Clave de Organizacion 
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Arete del animal al que pertenece el Costo
        /// </summary>
        public string Arete { get; set; }
    }
}
