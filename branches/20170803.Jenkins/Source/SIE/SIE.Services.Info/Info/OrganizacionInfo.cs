using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class OrganizacionInfo: BitacoraInfo, INotifyPropertyChanged
    {
        private int organizacionId;
        private string descripcion = string.Empty;
        private string direccion = string.Empty;
        private string rfc = string.Empty;
        private string division = string.Empty;
        

        /// <summary>
        /// Identificador de la organización
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogo", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID"
                     , PopUp = false, EstaEnContenedor = true, NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogo", EncabezadoGrid = "Clave"
                     , MetodoInvocacion = "ObtenerPorID", PopUp = true, EstaEnContenedor = true
                     , NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadOcultaCatalogo", EstaEnContenedor = true, NombreContenedor = "Organizacion")]
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

        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaTipoOrnanizacion",
        EncabezadoGrid = "Clave",
        MetodoInvocacion = "ObtenerPorIDFiltroTipoOrganizacion",
        PopUp = false,
        EstaEnContenedor = true,
        NombreContenedor = "Organizacion")]
        [AtributoAyuda(Nombre = "PropiedadIdentificadorCatalogoAyudaTipoOrnanizacion",
        EncabezadoGrid = "Identificador",
        MetodoInvocacion = "ObtenerPorIDFiltroTipoOrganizacion",
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

        [AtributoAyuda(Nombre = "PropiedadClaveSalidaVentaTraspado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorPaginaPorTiposOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaVentaTraspado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveSolicitudPremezclaOrganizacion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadClaveSolicitudPremezclaOrganizacion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveConfiguracionPremezclas", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDFiltroTipoOrganizacion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistrarProgramacionFleteInterna", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdFiltroTiposOrganizacion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveGanadoGordo", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDFiltroTipoOrganizacion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCrearContrato", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDFiltroTipoOrganizacion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveGastosMateriaPrima", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdFiltroTiposOrganizacion", PopUp = false)]

        [AtributoAyuda(Nombre = "PropiedadClaveReporteLlegadaLogistica", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadClaveReporteLlegadaLogistica", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
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
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaTipoOrganizacion",
        EncabezadoGrid = "Descripción",
        MetodoInvocacion = "ObtenerPorPaginaTipoOrganizacion",
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
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspado", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaPorTiposOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspado", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerSoloGanaderaPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSolicitudPremezcla", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerSoloGanaderaPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSolicitudPremezcla", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaPremezcla", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionConfiguracionPremezclas", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistrarProgramacionFleteInterna", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaPorFiltroTipoOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionTraspasoGanadoGordo", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerSoloGanaderaPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionTraspasoGanadoGordo", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCrearContrato", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoOrganizacion", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionGastosMateriaPrima", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaPorFiltroTipoOrganizacion", PopUp = true)]


        [AtributoAyuda(Nombre = "PropiedadDescripcionReporteLlegadaLogistica", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
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
        /// Dirección de la Organizacion
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadOcultaCatalogo", EstaEnContenedor = true, NombreContenedor = "Organizacion")]
        public string Direccion
        {
            get { return direccion == null ? null : direccion.Trim(); }
            set
            {
                if (value != direccion)
                {
                    string valor = value;
                    direccion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Direccion");
                }
            }
        }

        /// <summary>
        /// RFC de la Organizacion
        /// </summary>
        public string RFC
        {
            get { return rfc == null ? null : rfc.Trim(); }
            set
            {
                if (value != rfc)
                {
                    string valor = value;
                    rfc = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("RFC");
                }
            }
        }

        /// <summary>
        /// Iva que se aplica en la Organizacion
        /// </summary>
        public IvaInfo Iva { get; set; }

        public string Sociedad { get; set; }
        public string Division { get; set; }
        public ZonaInfo Zona { get; set; }
        public string TituloPoliza { get; set; }
        public string Moneda { get; set; }

        /// <summary>
        /// Listado de tipos organizacion para filtrar por medio de esta
        /// </summary>
        public List<TipoOrganizacionInfo> ListaTiposOrganizacion { get; set; }
 
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
