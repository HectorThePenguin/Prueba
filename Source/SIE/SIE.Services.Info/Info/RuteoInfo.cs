using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class RuteoInfo : BitacoraInfo,INotifyPropertyChanged
    {
        private int _ruteoID;
        private string _nombreRuteo;

        /// <summary>
        /// Identificación de la configuración
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveAdministracionRuteo", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        public int RuteoID 
        {
            get { return _ruteoID; }


            set
            {
                _ruteoID = value;
                NotifyPropertyChanged("RuteoID");
            } 
        }

        /// <summary>
        /// Organización origen de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }

        /// <summary>
        /// Organización destino de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }

        /// <summary>
        /// String concatenado de los origenes y destino rutas
        /// </summary>
        public string Rutas { get; set; }

        /// <summary>
        /// Nombre del ruteo
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionAdministracionRuteo", EncabezadoGrid = "Descripcion",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string NombreRuteo
        {
            get { return _nombreRuteo; }
            set
            {
                _nombreRuteo = value;
                NotifyPropertyChanged("NombreRuteo");
            }
        }

        /// <summary>
        /// Lista de detalles de rutas que pertenecen al ruteo
        /// </summary>
        public List<RuteoDetalleInfo> RuteoDetalle { get; set; }

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
