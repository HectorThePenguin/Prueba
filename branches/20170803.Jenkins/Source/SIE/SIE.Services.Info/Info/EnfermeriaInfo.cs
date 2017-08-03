using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class EnfermeriaInfo : BitacoraInfo, INotifyPropertyChanged
    {

        public EnfermeriaInfo()
        {
            Corrales = new List<CorralInfo>();
        }

        private int enfermeriaID;
        private string descripcion = string.Empty;
        private string organizacion = string.Empty;
        private string tipoOrganizacion = string.Empty;
        private string descripcionGanado = string.Empty;

        /// <summary>
        /// Identificador de la enfermeria
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyuda", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorID", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Enfermeria")]
        public int EnfermeriaID
        {
            get { return enfermeriaID; }
            set
            {
                if (value != enfermeriaID)
                {
                    enfermeriaID = value;
                    NotifyPropertyChanged("EnfermeriaID");
                }
            }
        }

        /// <summary>
        /// Descripión de la enfermería.
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyuda", EncabezadoGrid = "Descripción",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true,
            NombreContenedor = "Enfermeria")]
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                if (value != descripcion)
                {
                    string valor = value;
                    descripcion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        /// <summary>
        /// Corral de enfermeria .
        /// </summary>
        public CorralInfo Corral { get; set; }

        /// <summary>
        /// Numero de partida 
        /// </summary>
        public int FolioEntrada { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public string Organizacion
        {
            get { return organizacion == null ? null : organizacion.Trim(); }
            set
            {
                if (value != organizacion)
                {
                    string valor = value;
                    organizacion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Organizacion");
                }
            }
        }

        /// <summary>
        /// Tipo de Origen
        /// </summary>
        public int TipoOrigen { get; set; }

        /// <summary>
        /// Tipo de organizacion
        /// </summary>
        public string TipoOrganizacion
        {
            get { return tipoOrganizacion == null ? null : tipoOrganizacion.Trim(); }
            set
            {
                if (value != tipoOrganizacion)
                {
                    string valor = value;
                    tipoOrganizacion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("TipoOrganizacion");
                }
            }
        }
        /// <summary>
        /// Total de cabezas enfermas
        /// </summary>
        public int TotalCabezas { get; set; }

        /// <summary>
        /// Descripcion del ganado
        /// </summary>
        public string DescripcionGanado
        {
            get { return descripcionGanado == null ? null : descripcionGanado.Trim(); }
            set
            {
                if (value != descripcionGanado)
                {
                    string valor = value;
                    descripcionGanado = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("DescripcionGanado");
                }
            }
        }

        /// <summary>
        /// Lista de animales enfermos
        /// </summary>
        public IList<AnimalDeteccionInfo> ListaAnimales { get; set; }

        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo OrganizacionInfo { get; set; }

        public List<CorralInfo> Corrales { get; set; }

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
