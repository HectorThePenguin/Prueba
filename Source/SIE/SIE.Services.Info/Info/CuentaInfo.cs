using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public partial class CuentaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int cuentaID;
        private string descripcion;
        /// <summary>
        ///     Identificador Cuenta
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadOculta")]
        [AtributoAyuda(Nombre = "PropiedadOcultaGastosMateriaPrima", EncabezadoGrid = "ID", MetodoInvocacion = "ObtenerPorIDGastosMateriasPrimas", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadCuentaGastoGanado", EncabezadoGrid = "ID", MetodoInvocacion = "ObtenerPorIDGastosMateriasPrimas", PopUp = false, EstaEnContenedor = true,
                NombreContenedor = "Cuenta")]
        public int CuentaID
        {
            get { return cuentaID; }
            set
            {
                if (value != cuentaID)
                {
                    cuentaID = value;
                    NotifyPropertyChanged("CuentaID");
                }
            }
        }

        /// <summary>
        ///     Cuenta Description.
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadCuentaGastoGanadoDescripcion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true,
                NombreContenedor = "Cuenta")]
        public string Descripcion
        {
            get { return descripcion; }
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
        ///     Tipo de cuenta
        /// </summary>
        public TipoCuentaInfo TipoCuenta { get; set; }

        /// <summary>
        ///     Cuenta Description.
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClave", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = true)]
        public string ClaveCuenta { get; set; }

        /// <summary>
        /// Organizacion a la que pertenece la cuenta
        /// </summary>
        public int OrganizacionId;

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
