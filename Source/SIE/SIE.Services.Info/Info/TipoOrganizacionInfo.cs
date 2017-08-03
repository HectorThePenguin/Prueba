using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoOrganizacionInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;
        /// <summary>
        ///     Identificador del tipo de organizacion
        /// </summary>
        public int TipoOrganizacionID { get; set; }

        /// <summary>
        ///    Tipo de proceso 
        /// </summary>
        public TipoProcesoInfo TipoProceso { get; set; }

        /// <summary>
        /// Descripcion del tipo de Proceso
        /// </summary>
        public string DescripcionTipoProceso { get; set; }

        /// <summary>
        ///     Descipción del tipo de organización
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