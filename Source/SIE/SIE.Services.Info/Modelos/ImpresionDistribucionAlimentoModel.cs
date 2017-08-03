using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ImpresionDistribucionAlimentoModel
    {
        /// <summary>
        /// Nomenclatura del archivo
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Fecha de la distribucion
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Nombre del que realiza la distribucion
        /// </summary>
        public string Lector { get; set; }
        /// <summary>
        /// Corral al que se le distribuyo el alimento
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Camion que repartio el alimento
        /// </summary>
        public string Camion { get; set; }
        /// <summary>
        /// Estatus de la distribucion
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripcion Corta del Estatus
        /// </summary>
        public string DescripcionCorta { get; set; }
        /// <summary>
        /// indica el tipo de servicio
        /// </summary>
        public TipoServicioInfo TipoServicio { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo S(Separacion)
        /// </summary>
        public int TotalTipoS { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo OK(Correcto)
        /// </summary>
        public int TotalTipoOK { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo M(Monton)
        /// </summary>
        public int TotalTipoM { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo E(Espacio)
        /// </summary>
        public int TotalTipoE { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo N/A(No Existe)
        /// </summary>
        public int TotalTipoNA { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo D(Desperdicio)
        /// </summary>
        public int TotalTipoD { get; set; }
        /// <summary>
        /// Total de corrales encontratos de tipo B(Barrido)
        /// </summary>
        public int TotalTipoB { get; set; }
        /// <summary>
        /// Total de corrales
        /// </summary>
        public int TotalCorrales { get; set; }
    }
}
