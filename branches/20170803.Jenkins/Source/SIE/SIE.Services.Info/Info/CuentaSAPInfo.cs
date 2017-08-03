using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("CuentaSAP")]
    public class CuentaSAPInfo : BitacoraInfo, INotifyPropertyChanged
    {
        /// <summary>
        /// Identificador de la cuenta sap
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private int cuentaSAPID;
        /// <summary>
        /// codigo sap de la cuenta
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string cuentaSAP;
        /// <summary>
        /// descripcion de la cuenta
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string descripcion;
        /// <summary>
        ///     Identificador de la cuenta Sap
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [AtributoAyuda(Nombre = "PropiedadOcultaCostoDistribucion", EstaEnContenedor = true, NombreContenedor = "CuentaSAP")]
        public int CuentaSAPID
        {
            get { return cuentaSAPID; }
            set
            {
                if (value != cuentaSAPID)
                {
                    cuentaSAPID = value;
                    NotifyPropertyChanged("CuentaSAPID");
                }
            }
        }

        /// <summary>
        ///   CuentaSAP
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveCosteoEntradaSAP", EncabezadoGrid = "Cuenta de Provision"
            , MetodoInvocacion = "ObtenerPorFiltro", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaVentaTraspaso", EncabezadoGrid = "Cuenta de Provision"
            , MetodoInvocacion = "ObtenerPorFiltroSinTipo", PopUp = false)]

        [AtributoAyuda(Nombre = "PropuedadClaveGastoInventario", EncabezadoGrid = "Cuenta"
           , MetodoInvocacion = "ObtenerPorFiltro", PopUp = true)]
        [AtributoAyuda(Nombre = "PropuedadClaveGastoInventario", EncabezadoGrid = "Cuenta"
           , MetodoInvocacion = "ObtenerPorFiltro", PopUp = false)]

        [AtributoAyuda(Nombre = "PropiedadClaveCostoDistribucion", EncabezadoGrid = "Código SAP", MetodoInvocacion = "ObtenerPorFiltro"
              , PopUp = false, EstaEnContenedor = true, NombreContenedor = "CuentaSAP")]

        [AtributoAyuda(Nombre = "PropiedadClaveCrearContrato", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorFiltro", PopUp = false)]
        public string CuentaSAP
        {

            get { return cuentaSAP; }
            set
            {
                if (value != cuentaSAP)
                {
                    cuentaSAP = value;
                    NotifyPropertyChanged("CuentaSAP");
                }
            }
        }

        /// <summary>
        ///    Descripción de la cuentaSAP
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoEntradaSAP", EncabezadoGrid = "Descripcion"
            , MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspaso", EncabezadoGrid = "Descripción"
            , MetodoInvocacion = "ObtenerPorPaginaCuentasSap", PopUp = true)]
        [AtributoAyuda(Nombre = "PropuedadDescripcionGastoInventario", EncabezadoGrid = "Descripción"
           , MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCrearContrato", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCostoDistribucion", EncabezadoGrid = "Descripción"
              , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
              , NombreContenedor = "CuentaSAP")]
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
        /// Tipo de cuenta
        /// </summary>
        public int TipoCuentaID { get; set; }

        /// <summary>
        ///    TipoCuenta
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public TipoCuentaInfo TipoCuenta { get; set; }

        /// <summary> 
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Lista de tipos de cuenta
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<TipoCuentaInfo> ListaTiposCuenta { get; set; }

        /// <summary>
        /// Sociedad de la cuenta para consultar en SAP
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string Sociedad { get; set; }

        /// <summary>
        /// Plan de la cuenta para consultar en SAP
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string PlanCuenta { get; set; }

        /// <summary>
        /// Indica si la cuenta esta bloqueada al consultarla en SAP
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool Bloqueada { get; set; }

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
