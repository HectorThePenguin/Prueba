using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ImpresionCalidadGanadoModel
    {
        /// <summary>
        /// Nomenclatura del formato
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Id de la entrada de ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }
        /// <summary>
        /// Folio de la entrada de ganado
        /// </summary>
        public string FolioEntrada { get; set; }
        /// <summary>
        /// Proveedor de la entrada de ganado
        /// </summary>
        public string Proveedor { get; set; }
        /// <summary>
        /// Corral de la entrada de ganado
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Vaquero que califico el ganado
        /// </summary>
        public string VaqueroCalificador { get; set; }
        /// <summary>
        /// Fecha de Calificacion de la entrada de ganado
        /// </summary>
        public DateTime FechaCalificacion { get; set; }
        /// <summary>
        /// Total de Cabezas Machos
        /// </summary>
        public int TotalMachos { get; set; }
        /// <summary>
        /// Total de Cabezas Hembras
        /// </summary>
        public int TotalHembras { get; set; }
        /// <summary>
        /// Total de Cabezas en Linea Machos
        /// </summary>
        public int TotalLineaMachos { get; set; }
        /// <summary>
        /// Total de Cabezas en Linea Hembras
        /// </summary>
        public int TotalLineaHembras { get; set; }
        /// <summary>
        /// Total de Cabezas de Produccion Machos
        /// </summary>
        public int TotalProduccionMachos { get; set; }
        /// <summary>
        /// Total de Cabezas de Produccion Hembras
        /// </summary>
        public int TotalProduccionHembras { get; set; }
        /// <summary>
        /// Total de Cabezas de Venta Machos
        /// </summary>
        public int TotalVentaMachos { get; set; }
        /// <summary>
        /// Total de Cabezas de Venta Hembras
        /// </summary>
        public int TotalVentaHembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 1 Macho
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 1)]
        public int Calidad1Macho { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 1 Hembra
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 14)]
        public int Calidad1Hembra { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 1.5 Macho
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 2)]
        public int Calidad15Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 1.5 Hembra
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 15)]
        public int Calidad15Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 2 Macho
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 3)]
        public int Calidad2Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 2 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 16)]
        public int Calidad2Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 4)]
        public int Calidad3Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 17)]
        public int Calidad3Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 5)]
        public int Calidad35Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 18)]
        public int Calidad35Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5L Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 6)]
        public int Calidad35LMachos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5L Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 19)]
        public int Calidad35LHembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5P Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 7)]
        public int Calidad35PMachos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5P Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 20)]
        public int Calidad35PHembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5PVT2 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 8)]
        public int Calidad35Vt2Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5PVT2 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 21)]
        public int Calidad35Vt2Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5PR Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 9)]
        public int Calidad35PrMachos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 3.5PR Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 22)]
        public int Calidad35PrHembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 4 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 10)]
        public int Calidad4Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 4 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 23)]
        public int Calidad4Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 5 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 11)]
        public int Calidad5Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 5 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 24)]
        public int Calidad5Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 6 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 12)]
        public int Calidad6Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 6 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 25)]
        public int Calidad6Hembras { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 7 Machos
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 13)]
        public int Calidad7Machos { get; set; }
        /// <summary>
        /// Total de Cabezas calificacion 7 Hembras
        /// </summary>
        [Atributos.AtributoImpresionCalidad(CalidadGanadoID = 26)]
        public int Calidad7Hembras { get; set; }

        /// <summary>
        ///     Detalle de la calidad del ganado no usar en Reporte
        /// </summary>
        public List<EntradaGanadoCalidadInfo> ListaCalidadGanado { set; get; }

    }
}
