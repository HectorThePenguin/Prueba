using System;
using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.ComponentModel;
using SIE.Services.Info.Enums;
namespace SIE.Services.Info.Info
{
    public class CostoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int costoID;
        private string claveContable = string.Empty;
        private string descripcion = string.Empty;
        private bool tieneCuenta;

        public CostoInfo()
        {
            ListaRetencion = new List<RetencionInfo>();
            ListaTipoCostos = new List<TipoCostoInfo>();
            ListaTipoProrrateo = new List<TipoProrrateoInfo>();
            ListaTipoCostoCentro = new List<TipoCostoCentroInfo>();
            TipoCosto = new TipoCostoInfo();
            TipoProrrateo = new TipoProrrateoInfo();
            TipoCostoCentro = new TipoCostoCentroInfo();
            Retencion = new RetencionInfo();
            AbonoA = AbonoA.AMBOS;
        }

        /// <summary>
        ///     Identificador del costo
        /// </summary>        
        ///         
        [AtributoAyuda(Nombre = "PropiedadClaveRegistrarProgramacionInternaFletes", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorIDFiltroTipoCosto", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadOcultaCosteoEntrada", EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadOcultaCosteoPremezcla", EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadOcultaProgramacionEmbarqueCostos")]
        [AtributoAyuda(Nombre = "PropiedadOcultaAyudaCatalogoAyuda", EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaTipoCosto",
        EncabezadoGrid = "Clave",
        MetodoInvocacion = "ObtenerPorIDFiltroTipoCosto",
        PopUp = false,
        EstaEnContenedor = true,
        NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadClaveGastoInventario", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerCostoPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCuentaGastoGanado", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDFiltroTipoCosto", PopUp = false, EstaEnContenedor = true,
                NombreContenedor = "Costos")]
        [AtributoInicializaPropiedad]
        public int CostoID
        {
            set
            {
                if (value != costoID)
                {
                    costoID = value;
                    NotifyPropertyChanged("CostoID");
                }
            }
            get { return costoID; }
        }

        /// <summary>
        ///     Clave Contable del costo
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveOtrosCostos", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorClaveContableTipoCosto", PopUp = false)]

        [AtributoAyuda(Nombre = "PropiedadIDCatalogoAyudaOtrosCosto",
                        EncabezadoGrid = "Costo",
                        MetodoInvocacion = "ObtenerPorIDPaginaTipoCosto",
                        PopUp = true,
                        EstaEnContenedor = true,
                        NombreContenedor = "Costo")]


        [AtributoAyuda(Nombre = "PropiedadClaveProgramacionEmbarqueCostos", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorClaveContableTipoCosto", PopUp = false)]
        
        [AtributoAyuda(Nombre = "PropiedadClaveCosteoEntrada", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorClaveContableTipoCosto", PopUp = false, EstaEnContenedor = true,
            NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadClaveCosteoEntradaSinDependencia", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorClaveContable", PopUp = false, EstaEnContenedor = true,
            NombreContenedor = "Costo")]

        [AtributoAyuda(Nombre = "PropiedadClaveCosteoPremezcla", EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorClaveContableTipoCosto", PopUp = false, EstaEnContenedor = true,
            NombreContenedor = "Costo")]

        [AtributoAyuda(Nombre = "ClaveContable", EncabezadoGrid = "Clave")]
        public string ClaveContable
        {
            set
            {
                if (value != claveContable)
                {
                    string valor = value;
                    claveContable = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("ClaveContable");
                }
            }
            get { return claveContable == null ? null : claveContable.Trim(); }
        }

        /// <summary>
        ///     Descripcion del costo
        /// </summary>
        ///         
        [AtributoAyuda(Nombre = "PropiedadDescripcionOtrosCostos", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoCosto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaCosto",
                        EncabezadoGrid = "Costo",
                        MetodoInvocacion = "ObtenerPorPaginaTipoCosto",
                        PopUp = true,
                        EstaEnContenedor = true,
                        NombreContenedor = "Costo")]
       [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaTipoCosto",
                        EncabezadoGrid = "Costo",
                        MetodoInvocacion = "ObtenerPorIDPaginaTipoCosto",
                        PopUp = true,
                        EstaEnContenedor = true,
                        NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProgramacionEmbarqueCostos", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoCosto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoEntrada", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoCosto", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoPremezcla", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoCosto", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoEntradaSinDependencia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaClaveContable", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Costo")]
        [AtributoAyuda(Nombre = "Descripcion", EncabezadoGrid = "Descripción")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionGastoInventario", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaCostoPorTiposGasto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistrarProgramacionInternaFletes", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorPaginaTipoCosto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadCuentaGastoGanadoDescripcion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDPaginaTipoCosto", PopUp = true, EstaEnContenedor = true,
                NombreContenedor = "Costos")]
        public string Descripcion
        {
            set
            {
                if (value != descripcion)
                {
                    string valor = value;
                    descripcion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Descripcion");
                }
            }
            get { return descripcion == null ? null : descripcion.Trim(); }
        }

        /// <summary>
        ///     Tipo de Costo del costo
        /// </summary>
        public TipoCostoInfo TipoCosto { set; get; }

        /// <summary>
        ///     Tipo de Prorrateo del costo
        /// </summary>
        public TipoProrrateoInfo TipoProrrateo { set; get; }

        /// <summary>
        ///     Tipo de Retencion
        /// </summary>
        public RetencionInfo Retencion { get; set; }

        /// <summary>
        /// Indica a quien se le puede asignar un costo, a un Proveedor a una Cuenta o a Ambos
        /// </summary>
        public AbonoA AbonoA { get; set; }

        /// <summary>
        /// Cuenta sap perteneciente al costo
        /// </summary>
        public CuentaSAPInfo CuentaSap { get; set; }

        /// <summary>
        /// Proveedor perteneciente al costo
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Indica si el costo se aplicara a cuenta, de lo contrario se aplica a proveedor
        /// </summary>
        public bool TieneCuenta
        {
            get { return tieneCuenta; }
            set
            {
                if (value != tieneCuenta)
                {
                    tieneCuenta = value;
                    NotifyPropertyChanged("TieneCuenta");
                }
            }
        }

        /// <summary>
        /// Lista para manejar el parámetro de los Tipos de Costo
        /// </summary>
        public IList<TipoCostoInfo> ListaTipoCostos { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los Tipos de Prorrateo
        /// </summary>
        public IList<TipoProrrateoInfo> ListaTipoProrrateo { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los Tipos de Prorrateo
        /// </summary>
        public IList<RetencionInfo> ListaRetencion { set; get; }

        /// <summary>
        /// Toneladas que se guardaran para el costo
        /// </summary>
        public decimal ToneladasCosto { set; get; }

        /// <summary>
        /// Importe que se guardara para el costo
        /// </summary>
        public decimal ImporteCosto { set; get; }

        /// <summary>
        /// Indica si el costo tiene IVA
        /// </summary>
        public bool AplicaIva { set; get; }

        /// <summary>
        /// Indica si el costo tiene retencion
        /// </summary>
        public bool AplicaRetencion { set; get; }

        /// <summary>
        /// Indica descripcion de cuenta o proveedor segun sea el caso
        /// </summary>
        public string CuentaProveedor
        {
            get
            {
                if (TieneCuenta)
                {
                    if (CuentaSap != null)
                    {
                        return CuentaSap.Descripcion;
                    }
                }
                else
                {
                    if (Proveedor != null)
                    {
                        return Proveedor.Descripcion;
                    }
                }
                return "";
            }
        }

        public DateTime FechaCosto { get; set; }
        /// <summary>
        /// Compra individual
        /// </summary>
        public bool CompraIndividual { get; set; }
        /// <summary>
        /// Compra
        /// </summary>
        public bool Compra { get; set; }
        /// <summary>
        /// Recepcion
        /// </summary>
        public bool Recepcion { get; set; }
        /// <summary>
        /// Gasto
        /// </summary>
        public bool Gasto { get; set; }
        /// <summary>
        /// Costo
        /// </summary>
        public bool Costo { get; set; }

        public TipoCostoCentroInfo TipoCostoCentro { get; set; }

        /// <summary>
        /// Lista de Tipo costo de centro
        /// </summary>
        public List<TipoCostoCentroInfo> ListaTipoCostoCentro { get; set; }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Indica si esta guardado el costo
        /// </summary>
        public bool Editable { set; get; }
        #endregion
    }
}
