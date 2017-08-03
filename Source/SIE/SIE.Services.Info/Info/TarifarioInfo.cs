using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TarifarioInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int provedorId;
        private int embarqueId;

        /// <summary>
        /// Identificador de proveedor
        /// </summary>
        public int ProveedorID
        {
            get { return provedorId; }

            set
            {
                provedorId = value;
                NotifyPropertyChanged("ProveedorID");
            }
        }

        /// <summary>
        /// Identificador de proveedor
        /// </summary>
        public int EmbarqueID
        {
            get { return embarqueId; }

            set
            {
                embarqueId = value;
                NotifyPropertyChanged("EmbarqueID");
            }
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public string NombreProveedor { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public string Organizaciones { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public string Ruta { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public decimal Kilometros { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        public decimal Tarifa { get; set; }

        /// <summary>
        /// Provedor de la configuración
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Organización origen de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }

        /// <summary>
        /// Organización destino de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }
        
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
