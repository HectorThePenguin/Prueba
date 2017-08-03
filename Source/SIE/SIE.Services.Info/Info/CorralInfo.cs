using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Corral")]
    public class CorralInfo : INotifyPropertyChanged
    {
        private int corralId;
        private string codigo;

        public CorralInfo()
        {
            Activo = Enums.EstatusEnum.Activo;
        }
        /// <summary>
        /// Clave de Corral
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveCaliadadMezcladoraFormulaAlimento", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerFormulaCorralPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogo", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveDetector", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveAuxiliarInventario", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveGastoAlInventario", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveCorralDestino", EncabezadoGrid = "Clave")]
        [AtributoInicializaPropiedad]
        public int CorralID
        {
            get { return corralId; }
            set
            {
                if (value != corralId)
                {
                    corralId = value;
                    NotifyPropertyChanged("CorralID");
                }
            }
        }

        /// <summary>
        /// Organizacion a la que pertenece el Corral
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Identificador de Corral
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorDependencia", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", MetodoInvocacion = "ObtenerPorDependencia",
            PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCalidadMezcladoraFormulaAlimento", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerFormulaPorDependencia", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCalidadMezcladoraFormulaAlimento",
            MetodoInvocacion = "ObtenerFormulaPorDependencia", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogo", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoMezclado", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionDetector", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorCodicoOrganizacionCorral", PopUp = false, EstaEnContenedor = true,
            NombreContenedor = "Corral")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionDetector", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorDependencia", PopUp = true, EstaEnContenedor = true,
            NombreContenedor = "Corral")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogo", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorDependencia", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionAuxiliarInventario", MetodoInvocacion = "ObtenerPorGrupoCorral",
            PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionAuxiliarInventario",
            MetodoInvocacion = "ObtenerPorPaginaGrupoCorral", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadCodigoGastoAlInventario", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorPaginaGruposCorrales", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadCodigoGastoAlInventario", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorCodicoOrganizacionCorral", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCorralDestino", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorCodigoCorralDestinoReimplante", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCorralDestino", EncabezadoGrid = "Código",
            MetodoInvocacion = "ObtenerPorPaginaCorralDestinoReimplante", PopUp = true)]
        public string Codigo
        {
            get { return string.IsNullOrWhiteSpace(codigo) ? string.Empty : codigo.Trim(); }
            set
            {
                codigo = value;
                NotifyPropertyChanged("Codigo");
            }
        }

        /// <summary>
        /// Identificador de CodigoOrigen
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string CodigoOrigen { get; set; }
        /// <summary>
        /// Clave del Tipo de Corral
        /// </summary>
        public TipoCorralInfo TipoCorral { get; set; }

        /// <summary>
        /// Capacidad del Corral
        /// </summary>
        public int Capacidad { get; set; }

        /// <summary>
        /// Metros de Largo del Corral
        /// </summary>
        public int MetrosLargo { get; set; }

        /// <summary>
        /// Metros de Ancho del Corral
        /// </summary>
        public long MetrosAncho { get; set; }

        /// <summary>
        /// Seccion en la que se encuentra localizado el Corral
        /// </summary>
        public int Seccion { get; set; }

        /// <summary>
        /// Orden del Corral
        /// </summary>
        public int Orden { get; set; }

        /// <summary>
        /// Estatus del Corral
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public bool PuntaChica { get; set; }

        /// <summary>
        /// Fecha de Creacion del Corral
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que Creo el Corral
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha de Modificacion del Corral
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TipoCorralInfo> ListaTipoCorral { get; set; }

        /// <summary>
        /// Tipo de corral al que pertenece el corral
        /// </summary>
        public int TipoCorralId { get; set; }

        /// <summary>
        /// Organizacion a la que pertenece el corral
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Operador encargado del corral
        /// </summary>
        public OperadorInfo Operador { get; set; }
        
        /// <summary>
        /// Flag para saber si el corral pertenece a la enfermeria seleccionada.
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        public bool perteneceAEnfermeria { get; set; }

        [BLToolkit.DataAccess.SqlIgnore]
        public int TotalCabezas { get; set; }

        /// <summary>
        /// Usuario que Modifico el Corral
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Grupo al que pertenece el corral
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        public int GrupoCorral { get; set; }

        /// <summary>
        /// Grupos al que pertenece el corral
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        public List<GrupoCorralInfo> ListaGrupoCorral { get; set; }

        /// <summary> 
        ///	CorralDetectorID  
        /// </summary> 
        [BLToolkit.DataAccess.SqlIgnore]
        public int CorralDetectorID { get; set; }

        ///<summary>
        /// Grupo al que pertenece el corral
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        public GrupoCorralInfo GrupoCorralInfo { get; set; }

        public override string ToString()
        {
            return Codigo;
        }
        public FormulaInfo FormulaInfo { get; set; }
      
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
 