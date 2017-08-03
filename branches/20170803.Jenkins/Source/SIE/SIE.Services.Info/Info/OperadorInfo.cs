using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class OperadorInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int operadorId;
        private string nombre = string.Empty;
        private string apellidoPaterno = string.Empty;
        private string apellidoMaterno = string.Empty;
        private string codigoSAP = string.Empty;
        /// <summary>
        /// Identificador del operador
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorOperador", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveDetector", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorOperadorOrganizacion"
            , PopUp = false, EstaEnContenedor = true, NombreContenedor = "Operador")]
        [AtributoInicializaPropiedad]
        public int OperadorID
        {
            get { return operadorId; }
            set
            {
                if (value != operadorId)
                {
                    operadorId = value;
                    NotifyPropertyChanged("OperadorID");
                }
            }
        }

        /// <summary>
        /// Organizacion a la que pertenece el Operador
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Nombre del operador
        /// </summary>
        public string Nombre
        {
            get { return nombre == null ? string.Empty : nombre.Trim(); }
            set
            {
                if (value != nombre)
                {
                    string valor = value;
                    nombre = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Nombre");
                }
            }
        }

        /// <summary>
        /// Apellido paterno del operador
        /// </summary>
        public string ApellidoPaterno
        {
            get { return apellidoPaterno == null ? string.Empty : apellidoPaterno.Trim(); }
            set
            {
                if (value != apellidoPaterno)
                {
                    string valor = value;
                    apellidoPaterno = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("ApellidoPaterno");
                }
            }
        }

        /// <summary>
        /// Apellido Materno del operador
        /// </summary>
        public string ApellidoMaterno
        {
            get { return apellidoMaterno == null ? string.Empty : apellidoMaterno.Trim(); }
            set
            {
                if (value != apellidoMaterno)
                {
                    string valor = value;
                    apellidoMaterno = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("ApellidoMaterno");
                }
            }
        }

        /// <summary>
        /// Codigo con el que esta dado de alta en sap 
        /// </summary>
        public string CodigoSAP
        {
            get { return codigoSAP == null ? string.Empty : codigoSAP.Trim(); }
            set
            {
                if (value != codigoSAP)
                {
                    string valor = value;
                    codigoSAP = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("CodigoSAP");
                }
            }
        }

        /// <summary>
        /// Rol del operador 
        /// </summary>
        public RolInfo Rol { get; set; }

        /// <summary>
        /// Usuario que esta relaciona do al operador
        /// </summary>
        public UsuarioInfo Usuario { get; set; }

        /// <summary>
        /// Empleado que esta relaciona do al operador
        /// </summary>
        public EmpleadoInfo Empleado { get; set; }

        [AtributoIgnoraPropiedad]
        public EstatusEnum EstatusUsuario { get; set; }
        /// <summary>
        ///    Nombre completo del chofer
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Nombre", MetodoInvocacion = "ObtenerPorOperadorPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionDetector", EncabezadoGrid = "Nombre"
                     , MetodoInvocacion = "ObtenerPorOperadorPaginado", PopUp = true
                     , EstaEnContenedor = true, NombreContenedor = "Operador")]
        [AtributoIgnoraPropiedad]
        public string NombreCompleto
        {
            get { return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno).Trim(); }
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
                    ApellidoPaterno = string.Empty;
                    ApellidoMaterno = string.Empty;
                    Nombre = value;
                    NotifyPropertyChanged("NombreCompleto");
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