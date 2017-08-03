using SIE.Services.Info.Atributos;
using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class VigilanciaProveedorInfo: BitacoraInfo, INotifyPropertyChanged
    {
        private int organizacionId;
        private string descripcion;
        private string direccion;

        /// <summary>
        /// Identificador de la organización
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorEmbarqueTipoOrganizacion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveProgramacionEmbarque", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarque", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarqueAyudaOrigen", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorDependencia", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarqueAyudaDestino", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorDependenciasOrigenID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaOrigen", 
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorID", 
                PopUp = false, 
                EstaEnContenedor = true, 
                NombreContenedor = "OrganizacionOrigen")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaDestino", 
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorID", 
                PopUp = false, 
                EstaEnContenedor = true, 
                NombreContenedor = "OrganizacionDestino")]

        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyuda",
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorID",
                PopUp = false,
                EstaEnContenedor = true,
                NombreContenedor = "Organizacion")]

        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoTratamiento",
              EncabezadoGrid = "Clave",
              MetodoInvocacion = "ObtenerPorDependencia",
              PopUp = false,
              EstaEnContenedor = true,
              NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoConfiguracionSemana",
              EncabezadoGrid = "Clave",
              MetodoInvocacion = "ObtenerPorID",
              PopUp = false,
              EstaEnContenedor = true,
              NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoParametroOrganizacion",
            EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorID",
            PopUp = false,
            EstaEnContenedor = true,
            NombreContenedor = "Organizacion")]
        [AtributoInicializaPropiedad]
        public int OrganizacionID
        {
            get { return organizacionId; }
            set
            {
                if (value != organizacionId)
                {
                    organizacionId = value;
                    NotifyPropertyChanged("OrganizacionID");
                }
            }
        }

        //PropiedadClaveCatalogoAyudaOrigen
        //PropiedadDescripcionCatalogoAyudaOrigen


        /// <summary>
        /// Tipo de Organización
        /// </summary>
        public TipoOrganizacionInfo TipoOrganizacion { get; set; }

        /// <summary>
        /// Descripcion de la organización
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorEmbarqueTipoOrganizacionPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProgramacionEmbarque", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarqueAyudaOrigen", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorDependencia", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarqueAyudaDestino", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorPaginaOrigenID", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaOrigen", EncabezadoGrid = "Descripción"
                    , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
                    , NombreContenedor = "OrganizacionOrigen")]

        
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaDestino", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
            , NombreContenedor = "OrganizacionDestino")]

        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyuda",
                EncabezadoGrid = "Descripción",
                MetodoInvocacion = "ObtenerPorPagina",
                PopUp = true,
                EstaEnContenedor = true,
                NombreContenedor = "Organizacion")]


        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoTratamiento", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorDependencia", PopUp = true, EstaEnContenedor = true
            , NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoConfiguracionSemana", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
            , NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoParametroOrganizacion", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
            , NombreContenedor = "Organizacion")]
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
        /// Dirección de la Organizacion
        /// </summary>
        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (value != direccion)
                {
                    direccion = value;
                    NotifyPropertyChanged("Direccion");
                }
            }
        }

        /// <summary>
        /// RFC de la Organizacion
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Iva que se aplica en la Organizacion
        /// </summary>
        public IvaInfo Iva { get; set; }
 
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