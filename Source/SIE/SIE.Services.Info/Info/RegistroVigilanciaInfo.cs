using System;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class RegistroVigilanciaInfo : INotifyPropertyChanged
    {
        private int folioTurno;
        /// <summary>
        /// Identificador del registro de vigilancia
        /// </summary>
        public int RegistroVigilanciaId { get; set; }

        /// <summary>
        /// Organizacion a la que pertenece el registro de vigilancia
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Contrato del registro de vigilancia
        /// </summary>
        public ContratoInfo Contrato { get; set; }

        /// <summary>
        /// Proveedor de materias primas
        /// </summary>
        public ProveedorInfo ProveedorMateriasPrimas { get; set; }

        /// <summary>
        /// Chofer del proveedor
        /// </summary>
        public ProveedorChoferInfo ProveedorChofer { get; set; }

        /// <summary>
        /// Transportista
        /// </summary>
        public string Transportista { get; set; }

        /// <summary>
        /// Chofer
        /// </summary>
        public string Chofer { get; set; }

        /// <summary>
        /// Producto del registro de vigilancia
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Camion del registro de vigilancia
        /// </summary>
        public CamionInfo Camion { get; set; }

        /// <summary>
        /// Camion de la tabla registro vigilancia
        /// </summary>
        public string CamionCadena { get; set; }

        /// <summary>
        /// Marca del camion del registro de vigilancia
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Color del camion del registro de vigilancia
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Folio del turno
        /// </summary>
        public int FolioTurno
        {
            get { return folioTurno; }
            set 
            { 
                folioTurno = value;
                NotifyPropertyChanged("FolioTurno");
            }
        }

        /// <summary>
        /// Fecha de llegada
        /// </summary>
        public DateTime FechaLlegada { get; set; }

        /// <summary>
        /// Fecha de salida
        /// </summary>
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// TipoFolioID se utiliza para enviar el valor TipoFolio que sirve para genear folios en la tabla correspodiente
        /// </summary>
        public int TipoFolioID { get; set; }

        /// <summary>
        /// IDNulo
        /// </summary>
        public int IdNulo { get; set; }
        /// <summary>
        /// Rango de humedad minimo permitido para el producto
        /// </summary>
        public decimal porcentajePromedioMin { get; set; }
        /// <summary>
        /// Rango de humedad maximo permitido para el produxto
        /// </summary>
        public decimal porcentajePromedioMax { get; set; }

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
