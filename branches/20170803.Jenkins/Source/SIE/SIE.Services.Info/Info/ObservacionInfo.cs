using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ObservacionInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion = string.Empty;
        /// <summary>
        /// Identificador de Observación .
        /// </summary>
        public int ObservacionID { get; set; }

        /// <summary>
        /// Identificador de Observación .
        /// </summary>
        public int TipoObservacionID { get; set; }
        /// <summary>
        /// Descripción de la Observación .
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }
        
        public TipoObservacionInfo TipoObservacion { get; set; }

        /// <summary>
        /// Fecha Creacion de Observación .
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        ///    Acceso FechaCitaDescargaString
        /// </summary>
        public string FechaCreacionString
        {
            get
            {
                string regreso = String.Empty;
                if (FechaCreacion.Year != 1900)
                    regreso = FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                return regreso;
            }
        }

        /// <summary>
        /// Nombre de quien genero la Observación .
        /// </summary>
        public string NombreUsuario { get; set; }


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
