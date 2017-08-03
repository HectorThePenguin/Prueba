using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Modelos
{
    public class ImpresionSupervisionDetectoresModel
    {
        /// <summary>
        /// Nomenclatura del archivo
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Id de la supervision
        /// </summary>
        public int SupervisionDetectoresID { get; set; }
        /// <summary>
        /// Organizacion donde se hace la supervision
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Nombre del Detector
        /// </summary>
        public string Detector { get; set; }
        /// <summary>
        /// Fecha de la supervision
        /// </summary>
        public DateTime FechaSupervision { get; set; }
        /// <summary>
        /// Criterio de la Supervision
        /// </summary>
        public string CriterioSupervision { get; set; }
        /// <summary>
        /// Valor inicial del criterio de supervision
        /// </summary>
        public int ValorInicial { get; set; }
        /// <summary>
        /// Valor Final del criterio de supervision
        /// </summary>
        public int ValorFinal { get; set; }
        /// <summary>
        /// Codigo de color del criterio
        /// </summary>
        public string CodigoColor { get; set; }
        /// <summary>
        /// Observaciones de la supervision
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Calificacion en puntos de las respuestas de la supervision
        /// </summary>
        public int TotalSupervision { get; set; }

        #region Valores Preguntas
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 18)]
        public int CertificacionCrb { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 19)]
        public int SignosEnfermedad { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 20)]
        public int IdentificacionCrb { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 21)]
        public int SignosClinicos { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 22)]
        public int EntradaCalmada { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 23)]
        public int RegistrarAnimales { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 24)]
        public int SacaAnimales { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 25)]
        public int ManejoSinEstres { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 26)]
        public int AnimalesSinTratamiento { get; set; }
        [Atributos.AtributoImpresionSupervisionDetectores(PreguntaID = 27)]
        public int RevisionCorrales { get; set; }
        #endregion Valores Preguntas
        public List<ImpresionSupervisionDetectoresDetalleModel> Detalles { get; set; }
    }
}

