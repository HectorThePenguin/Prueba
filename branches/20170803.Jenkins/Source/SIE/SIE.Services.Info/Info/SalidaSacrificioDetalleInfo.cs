namespace SIE.Services.Info.Info
{
    public class SalidaSacrificioDetalleInfo : BitacoraInfo
    {
        /// <summary> 
        ///	Lote Id
        /// </summary> 
        public int LoteId { get; set; }

        /// <summary> 
        ///	Identificador del Animal
        /// </summary> 
        public long AnimalId { get; set; }

        /// <summary> 
        ///	Arete del animal
        /// </summary> 
        public string Arete { get; set; }

        /// <summary> 
        ///	Folio de la orden de Sacrificio
        /// </summary> 
        public int FolioSalida { get; set; }
    }
}
