using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoPolizaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int tipoPolizaID;
        private string descripcion;

        /// <summary>
        ///     Identificador tipo de póliza
        /// </summary>
        public int TipoPolizaID
        {
            get { return tipoPolizaID; }
            set
            {
                tipoPolizaID = value;
                NotifyPropertyChanged("TipoPolizaID");
            }
        }

        /// <summary>
        ///     Descripción del tipo de embarque
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
        ///     Clave de la póliza
        /// </summary>
        public string ClavePoliza { get; set; }
        /// <summary>
        /// Postfijo que llevara Ref3
        /// </summary>
        public string PostFijoRef3 { get; set; }
        /// <summary>
        /// Texto que llevara el Tipo de Documento
        /// </summary>
        public string TextoDocumento { get; set; }
        /// <summary>
        /// Indica si se imprimira poliza ó no
        /// </summary>
        public bool ImprimePoliza { get; set; }

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
