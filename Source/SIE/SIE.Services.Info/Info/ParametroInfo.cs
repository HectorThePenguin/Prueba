using SIE.Services.Info.Atributos;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ParametroInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int parametroID;
        private string descripcion = string.Empty;
        private string clave = string.Empty;
        
        /// <summary> 
        ///	Id del parametro
        /// </summary> 
        [AtributoAyuda(Nombre = "ClaveAyudaCatalogoParametro", EncabezadoGrid = "Clave"
                     , MetodoInvocacion = "ObtenerPorParametroTipoParametro", PopUp = false
                     , EstaEnContenedor = true, NombreContenedor = "Parametro")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoParametroOrganizacion", EncabezadoGrid = "Clave"
                     , MetodoInvocacion = "ObtenerPorID", PopUp = false
                     , EstaEnContenedor = true, NombreContenedor = "Parametro")]
        [AtributoInicializaPropiedad]
        public int ParametroID
        {
            get { return parametroID; }
            set
            {
                if (value != parametroID)
                {
                    parametroID = value;
                    NotifyPropertyChanged("ParametroID");
                }
            }
        }

		/// <summary> 
		///	TipoParametroID  
		/// </summary> 
		public TipoParametroInfo TipoParametro { get; set; }

        /// <summary> 
        ///	Descripcion  
        /// </summary> 
        [AtributoAyuda(Nombre = "DescripcionAyudaCatalogoParametro", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true
            , EstaEnContenedor = true, NombreContenedor = "Parametro")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoParametroOrganizacion", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true
            , EstaEnContenedor = true, NombreContenedor = "Parametro")]
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
		///	Clave  
		/// </summary> 
        public string Clave
        {
            get { return clave == null ? null : clave.Trim(); }
            set
            {
                if (value != clave)
                {
                    string valor = value;
                    clave = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Clave");
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
