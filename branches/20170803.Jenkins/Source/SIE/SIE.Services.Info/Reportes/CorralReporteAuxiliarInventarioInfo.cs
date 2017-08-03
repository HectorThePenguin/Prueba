using System;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class CorralReporteAuxiliarInventarioInfo : INotifyPropertyChanged
    {        
        private string lote;
        private DateTime fechaInicio;
        private string tipoGanado;
        private string clasificacion;
        private CorralInfo corralInfo;
        private LoteInfo loteInfo;

        /// <summary>
        /// Identificador del Corral
        /// </summary>
        public int CorralID { get; set; }
        /// <summary>
        /// Codigo del Corral
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Identificador de Lote
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Codigo del Lote
        /// </summary>
        public string Lote
        {
            get { return lote; }
            set
            {
                if (value != lote)
                {
                    lote = value;
                    NotifyPropertyChanged("Lote");
                }
            }
        }
        /// <summary>
        /// Fecha de Inicio del Lote
        /// </summary>
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                if (value != fechaInicio)
                {
                    fechaInicio = value;
                    NotifyPropertyChanged("FechaInicio");
                }
            }
        }
        /// <summary>
        /// Identificador del Tipo de Ganado
        /// </summary>
        public int TipoGanadoID { get; set; }
        /// <summary>
        /// Descripcion del Tipo de Ganado
        /// </summary>
        public string TipoGanado
        {
            get { return tipoGanado; }
            set
            {
                if (value != tipoGanado)
                {
                    tipoGanado = value;
                    NotifyPropertyChanged("TipoGanado");
                }
            }
        }
        /// <summary>
        /// Identificador de la Clasificacion del Ganado
        /// </summary>
        public int ClasificacionID { get; set; }
        /// <summary>
        /// Descripcion de la Clasificacion del Ganado
        /// </summary>
        public string Clasificacion
        {
            get { return clasificacion; }
            set
            {
                if (value != clasificacion)
                {
                    clasificacion = value;
                    NotifyPropertyChanged("Clasificacion");
                }
            }
        }

        /// <summary>
        /// Identificador del Tipo Corral
        /// </summary>
        public int TipoCorralID { get; set; }
        /// <summary>
        /// Tipo Corral
        /// </summary>
        public string TipoCorral { get; set; }
        /// <summary>
        /// Tipo Corral
        /// </summary>
        public CorralInfo CorralInfo
        {
            get { return corralInfo; }
            set
            {
                if (value != corralInfo)
                {
                    corralInfo = value;
                    NotifyPropertyChanged("CorralInfo");
                }
            }
        }
        /// <summary>
        /// Lote
        /// </summary>
        public LoteInfo LoteInfo
        {
            get { return loteInfo; }
            set
            {
                if (value != loteInfo)
                {
                    loteInfo = value;
                    NotifyPropertyChanged("LoteInfo");
                }
            }
        }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
