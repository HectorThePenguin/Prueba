using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class ChoferInfo : BitacoraInfo
    {
        public ChoferInfo()
        {
            Activo = Enums.EstatusEnum.Activo;
        }
        /// <summary>
        ///     Identificador del chofer .
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarque", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCaliadadMezcladoraFormulaAlimento", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaVentaTraspaso", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        public int ChoferID { get; set; }

        /// <summary>
        ///    Nombre del chofer
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        ///    Apellido paterno del chofer
        /// </summary>
        public string ApellidoPaterno { get; set; }

        /// <summary>
        ///    Apellido materno del chofer
        /// </summary>
        public string ApellidoMaterno { get; set; }

        /// <summary>
        ///    Nombre completo del chofer
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCaliadadMezcladoraFormulaAlimento", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerFormulaChoferPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspaso", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoIgnoraPropiedad]
        public string NombreCompleto
        {
            get { return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno , ApellidoMaterno).TrimEnd(' '); }
            set
            {
                if (value == string.Empty)
                {
                    Nombre = value;
                    ApellidoPaterno = value;
                    ApellidoMaterno = value;
                }
                else
                {
                    Nombre = value;
                    NotifyPropertyChanged("NombreCompleto");
                }
            }
        }
        public int ProveedorChoferID { get; set; }

        /// <summary>
        /// Indica si el chofer ha sido eliminado
        /// </summary>
        public bool Eliminado { get; set; }

        /// <summary>
        /// Indica si el chofer es boletinado
        /// </summary>
        public bool Boletinado { get; set; }

        /// <summary>
        /// Observaciones en caso de que el chofer sea boletinado
        /// </summary>
        public string Observaciones { get; set; }

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