using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoMovimientoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;
        /// <summary>
        ///     Identificador tipo de póliza
        /// </summary>
        public int TipoMovimientoID { get; set; }

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
        public bool EsGanado { get; set; }

        /// <summary>
        ///     Clave de la póliza
        /// </summary>
        public bool EsProducto { get; set; }

        /// <summary>
        ///     Clave de la póliza
        /// </summary>
        public bool EsEntrada { get; set; }

        /// <summary>
        ///     Clave de la póliza
        /// </summary>
        public bool EsSalida { get; set; }

        /// <summary>
        ///     Clave de la póliza
        /// </summary>
        public string ClaveCodigo { get; set; }

        /// <summary>
        ///     Clave de la póliza
        /// </summary>
        public TipoPolizaInfo TipoPoliza { get; set; }

        public override string ToString()
        {
            return descripcion;
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
