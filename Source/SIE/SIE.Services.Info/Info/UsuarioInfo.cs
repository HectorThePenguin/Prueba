using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Usuario")]
    public class UsuarioInfo : INotifyPropertyChanged
    {
        private int usuarioID;
        private string nombre;

        public UsuarioInfo()
        {
            Activo = EstatusEnum.Activo;
            Organizacion = new OrganizacionInfo();
            Operador = new OperadorInfo
                           {
                               Nombre = string.Empty,
                               ApellidoMaterno = string.Empty,
                               ApellidoPaterno = string.Empty,
                               NombreCompleto = string.Empty
                           };
        }
        /// <summary>
        /// Id de Usuario
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyuda",
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorID",
                PopUp = false,
                EstaEnContenedor = true,
                NombreContenedor = "Usuario")]
        public int UsuarioID
        {
            get { return usuarioID; }
            set
            {
                usuarioID = value;
                NotifyPropertyChanged("UsuarioID");
            }
        }

        /// <summary>
        /// Nombre de Usuario
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadNombreCatalogoAyuda",
            EncabezadoGrid = "Nombre",
            MetodoInvocacion = "ObtenerPorPagina",
            PopUp = true,
            EstaEnContenedor = true,
            NombreContenedor = "Usuario")]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                NotifyPropertyChanged("Nombre");
            }
        }

        /// <summary>
        /// Organización a la que Pertenece el Usuario
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Organizacion a la que Pertenece el Usuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Nombre de Usuario con el Cual Inicia Sesion por Active Directory
        /// </summary>
        public string UsuarioActiveDirectory { get; set; }

        /// <summary>
        /// Si el usuario es operador contendra los datos de operador
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public OperadorInfo Operador { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public AlmacenUsuarioInfo AlmacenUsuario { get; set; }

        /// <summary>
        /// Grupos al que pertenece el usuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<UsuarioGrupoInfo> UsuarioGrupo { get; set; }


        /// <summary>
        /// Usario que modifica el registro .
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int? UsuarioCreacionID { get; set; }

        /// <summary>
        /// Usario que modifica el registro .
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int? UsuarioModificacionID { get; set; }

        /// <summary>
        /// Indica si el Usuario es corporativo
        /// </summary>
        public bool Corporativo { get; set; }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Indica si el registro tiene cambios pantalla AlmacenUsuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool TieneCambios { get; set; }

        /// <summary>
        /// Indica el nivel de acceso que tiene el Usuario
        /// </summary>
        public NivelAccesoEnum NivelAcceso { get; set; }
        

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public UsuarioInfo Clone()
        {
            var usuarioClone = new UsuarioInfo
            {
                UsuarioID = UsuarioID,
                Nombre = Nombre,
                Organizacion = Organizacion,
                Operador = Operador,
                UsuarioActiveDirectory = UsuarioActiveDirectory,
                Activo = Activo,
                UsuarioCreacionID = UsuarioCreacionID,
                UsuarioModificacionID = UsuarioModificacionID
            };
            return usuarioClone;
        }

        #endregion
    }
}