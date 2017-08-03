using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoMuerteInfo : BitacoraInfo
    {
        /// <summary> 
        ///	EntradaGanadoMuerteID  
        /// </summary> 
        public int EntradaGanadoMuerteID { get; set; }

        /// <summary> 
        ///	EntradaGanadoID
        /// </summary> 
        public EntradaGanadoInfo EntradaGanado { get; set; }

        /// <summary> 
        ///	Animal  
        /// </summary> 
        public AnimalInfo Animal { get; set; }

        /// <summary> 
        ///	FolioMuerte  
        /// </summary> 
        public long FolioMuerte { get; set; }

        /// <summary> 
        ///	Fecha  
        /// </summary> 
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Peso de la muerte en transito
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Lista con el detalle de la 
        /// muerte
        /// </summary>
        public List<EntradaGanadoMuerteDetalleInfo> EntradaGanadMuerteDetalle { get; set; }

        public ProveedorInfo ProveedorFletes { get; set; }

        public ClienteInfo Cliente { get; set; }

        public int OrganizacionDestinoID { get; set; }
    }
}
