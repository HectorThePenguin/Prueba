using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class LoteProyeccionInfo : BitacoraInfo
    {
        ///<summary>
        /// Identificador lote proyeccion
        /// </summary>
        public int LoteProyeccionID { get; set; }

        /// <summary>
        /// Identifica el lote 
        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        /// Identifica la organización
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Identifica el valor de Frame
        /// </summary>
        public decimal Frame { get; set; }

        /// <summary>
        /// Identifica la ganancia diaria
        /// </summary>
        public decimal GananciaDiaria { get; set; }

        /// <summary>
        /// Identifica el consumo base humeda
        /// </summary>
        public decimal ConsumoBaseHumeda { get; set; }

        /// <summary>
        /// Identifica la conversión
        /// </summary>
        public decimal Conversion { get; set; }

        /// <summary>
        /// Identifica el peso maduro
        /// </summary>
        public int PesoMaduro { get; set; }

        /// <summary>
        /// Identifica el peso de sacrificio
        /// </summary>
        public int PesoSacrificio { get; set; }

        /// <summary>
        /// Identifica los dias de engorda
        /// </summary>
        public int DiasEngorda { get; set; }

        /// <summary>
        /// Identifica la fecha de entrada Zilmax
        /// </summary>
        public DateTime FechaEntradaZilmax { get; set; }

        /// <summary>
        /// Identifica la fecha de sacrificio, que se genera la proyección
        /// </summary>
        public DateTime FechaSacrificio { get; set; }

        public IList<LoteReimplanteInfo> ListaReimplantes { get; set; }

        public CheckListProyeccionInfo Proyeccion { get; set; }
        
        /// <summary>
        /// Numero de reimplante
        /// </summary>
        public int NumeroReimplante { get; set; }
        /// <summary>
        /// Indica si el corral sera visto en la opcion de disponibilidad
        /// </summary>
        public bool RequiereRevision { get; set; }

        /// <summary>
        /// Indica si el registro se va a guardar en la Bitacora de LoteProyeccionBitacora
        /// </summary>
        public bool AplicaBitacora { get; set; }

    }
}
