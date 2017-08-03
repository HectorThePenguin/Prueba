using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TrampaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int trampaID;
        private string descripcion;
        private string hostName;
        /// <summary>
        /// Identificador TrampaID.
        /// </summary>
        public int TrampaID
        {
            get { return trampaID; }
            set
            {
                trampaID = value;
                NotifyPropertyChanged("TrampaID");
            }
        }
        /// <summary>
        /// Descripción.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Contenedor Organizacion.
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Tipo de Trampa.
        /// </summary>
        public char TipoTrampa { get; set; }

        /// <summary>
        /// Nombre del equipo.
        /// </summary>
        public string HostName
        {
            get { return hostName == null ? null : hostName.Trim(); }
            set
            {
                string valor = value;
                hostName = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("HostName");
            }
        }

        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }

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