using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class EvaluacionCorralInfo : BitacoraInfo
    {
        /// <summary>
        ///     Acceso a OrganizacionID
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Acceso a OrganizacionID
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }

        /// <summary>
        ///     Acceso a OrganizacionOrigen Agrupadas
        /// </summary>
        public string OrganizacionOrigenAgrupadas { get; set; }

        /// <summary>
        ///     Acceso a Partidas Agrupadas
        /// </summary>
        public string PartidasAgrupadas { get; set; }

        /// <summary>
        ///     Acceso a EvaluacionID 
        /// </summary>
        public int EvaluacionID { get; set; }

        /// <summary>
        ///     Acceso a Corral
        /// </summary>
        public CorralInfo  Corral { get; set; }

        /// <summary>
        ///     Acceso LoteID
        /// </summary>
        public LoteInfo Lote { get; set; }

        /// <summary>
        ///     Acceso FechaEvaluacion
        /// </summary>
        public DateTime FechaEvaluacion { get; set; }

        /// <summary>
        ///     Acceso FechaEvaluacion
        /// </summary>
        public string HoraEvaluacion { get; set; }

        /// <summary>
        ///     Acceso FechaEvaluacionString
        /// </summary>
        public string FechaEvaluacionString
        {
            get
            {
                string regreso = String.Empty;
                if (FechaEvaluacion.Year != 1900)
                    regreso = FechaEvaluacion.ToShortDateString();
                return regreso;
            }
        }

        /// <summary>
        ///  Acceso   Cabezas
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        ///    Acceso EsMetafilaxia
        /// </summary>
        public bool EsMetafilaxia { get; set; }

        /// <summary>
        ///    Acceso MetafilaxiaAutorizada
        /// </summary>
        public bool MetafilaxiaAutorizada { get; set; }
        
        /// <summary>
        ///    Acceso OperadorID
        /// </summary>
        //public int OperadorID { get; set; }
        public OperadorInfo Operador { get; set; }

        /// <summary>
        ///    Acceso ConGarrapata
        /// </summary>
        public NivelGarrapata NivelGarrapata { get; set; }

        /// <summary>
        ///    Acceso Autorizado
        /// </summary>
        public int Autorizado { get; set; }

        /// <summary>
        ///    Acceso Justificacion
        /// </summary>
        public string Justificacion { get; set; }

        /// <summary>
        ///    Acceso FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        ///    Acceso FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        ///    Acceso FechaRecepcion
        /// </summary>
        public DateTime FechaRecepcion { get; set; }

        /// <summary>
        ///    Acceso FechaRecepcionString
        /// </summary>
        public string FechaRecepcionString
        {
            get
            {
                string regreso = String.Empty;
                if (FechaRecepcion.Year != 1900)
                    regreso = FechaRecepcion.ToShortDateString();
                return regreso;
            }
        }

        /// <summary>
        ///    Acceso FolioEvaluacion
        /// </summary>
        public int FolioEvaluacion { get; set; }

        /// <summary>
        ///    Acceso FolioEvaluacion
        /// </summary>
        public decimal PesoLlegada { get; set; }

        /// <summary>
        ///    Acceso FolioEvaluacion
        /// </summary>
        public int FolioOrigen { get; set; }

        /// <summary>
        ///    Acceso al Detalle de La Evaluacion de Riesgo de Corral
        /// </summary>
        public IList<EvaluacionCorralDetalleInfo> PreguntasEvaluacionCorral { get; set; }
    }
}
