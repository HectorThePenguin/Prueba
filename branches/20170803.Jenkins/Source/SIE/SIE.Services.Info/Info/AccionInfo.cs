using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AccionInfo : INotifyPropertyChanged
    {
        private int accionID;
        private string descripcion;
        private EstatusEnum activo;
        /// <summary>
        /// Lista de las Observaciones por Acción
        /// </summary>
        public List<ObservacionInfo> ListaObservaciones { get; set; }

        /// <summary>
        /// Observación de la Acción
        /// </summary>
        private string observacion;

        /// <summary>
        /// Acciones Tomadas
        /// </summary>
        private string accionesTomadas;


        /// <summary>
        /// ID de la Acción
        /// </summary>
        public int AccionID {
            get { return accionID; } 
            set{
                accionID = value;
                NotifyPropertyChanged("AccionID");
                }
        }

        /// <summary>
        /// ID de la Acción
        /// </summary>
        public EstatusEnum Activo
        {
            get { return activo; }
            set
            {
                activo = value;
                NotifyPropertyChanged("Activo");
            }
        }

        /// <summary>
        /// Descripción de la Acción
        /// </summary>
        public string Observacion
        {
            get { return observacion; }
            set
            {
                observacion = value;
                NotifyPropertyChanged("Observacion");
            }
        }

        public string AccionesTomadas
        {
            get { return accionesTomadas; }
            set
            {
                accionesTomadas = value;
                NotifyPropertyChanged("AccionesTomadas");
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("Descripcion");
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
