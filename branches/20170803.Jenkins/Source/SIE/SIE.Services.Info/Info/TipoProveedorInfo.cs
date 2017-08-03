using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoProveedorInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int tipoProveedorID;
        private string descripcion;
        private string codigoGrupoSap;

        /// <summary>
        ///     Idntificador del Tipo de Proveedor
        /// </summary>
        public int TipoProveedorID
        {
            get
            {
                return tipoProveedorID;
            }
            set
            {
                if (value != tipoProveedorID)
                {
                    tipoProveedorID = value;
                    NotifyPropertyChanged("TipoProveedorID");
                }
            }
        }

        /// <summary>
        ///     Descripción del tipo de proveedor
        /// </summary>
        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        /// <summary>
        ///     Código de Grupo en SAP
        /// </summary>
        public string CodigoGrupoSap
        {
            get
            {
                return codigoGrupoSap;
            }
            set
            {
                if (value != codigoGrupoSap)
                {
                    codigoGrupoSap = value;
                    NotifyPropertyChanged("CodigoGrupoSap");
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