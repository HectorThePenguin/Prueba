using System;
using System.ComponentModel;
using System.Xml.Serialization;
namespace SIE.Services.Info.Info
{
    public class PolizaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private bool generar;
        /// <summary>
        /// Regerencia del Documento
        /// </summary>
        [XmlElement(ElementName = "noref")]
        public string NumeroReferencia { get; set; }

        /// <summary>
        /// Fecha del Documento
        /// </summary>
        [XmlElement(ElementName = "fecha_doc")]
        public string FechaDocumento { get; set; }

        /// <summary>
        /// Fecha Contabilidad
        /// </summary>
        [XmlElement(ElementName = "fecha_cont")]
        public string FechaContabilidad { get; set; }

        /// <summary>
        /// Clase del Documento
        /// </summary>
        [XmlElement(ElementName = "clase_doc")]
        public string ClaseDocumento { get; set; }

        /// <summary>
        /// Sociedad
        /// </summary>
        [XmlElement(ElementName = "sociedad")]
        public string Sociedad { get; set; }

        /// <summary>
        /// Clave Moneda
        /// </summary>
        [XmlElement(ElementName = "moneda")]
        public string Moneda { get; set; }

        /// <summary>
        /// Tipo de Cambio
        /// </summary>
        [XmlElement(ElementName = "tipocambio")]
        public decimal TipoCambio { get; set; }

        /// <summary>
        /// Texto del Documento
        /// </summary>
        [XmlElement(ElementName = "texto_doc")]
        public string TextoDocumento { get; set; }

        /// <summary>
        /// Mes del Documento
        /// </summary>
        [XmlElement(ElementName = "mes")]
        public string Mes { get; set; }

        /// <summary>
        /// Cuenta a la que se
        /// aplicara el movimiento
        /// </summary>
        [XmlElement(ElementName = "cuenta")]
        public string Cuenta { get; set; }

        /// <summary>
        /// Proveedor al que se le aplica
        /// el movimiento
        /// </summary>
        [XmlElement(ElementName = "proveedor")]
        public string Proveedor { get; set; }

        /// <summary>
        /// Cliente al que se le aplica
        /// el movimiento
        /// </summary>
        [XmlElement(ElementName = "cliente")]
        public string Cliente { get; set; }

        /// <summary>
        /// Indica CME
        /// </summary>
        [XmlElement(ElementName = "indica_cme")]
        public string IndicaCme { get; set; }

        /// <summary>
        /// Importe del Movimiento
        /// </summary>
        [XmlElement(ElementName = "importe")]
        public string Importe { get; set; }

        /// <summary>
        /// Indica el Impuesto
        /// </summary>
        [XmlElement(ElementName = "indica_imp")]
        public string IndicaImp { get; set; }

        /// <summary>
        /// Centro de Costo
        /// </summary>
        [XmlElement(ElementName = "centro_cto")]
        public string CentroCosto { get; set; }

        /// <summary>
        /// Orden de Int
        /// </summary>
        [XmlElement(ElementName = "orden_int")]
        public string OrdenInt { get; set; }

        /// <summary>
        /// Centro Beneficio
        /// </summary>
        [XmlElement(ElementName = "centro_ben")]
        public string CentroBeneficio { get; set; }

        /// <summary>
        /// Texto Asignado
        /// </summary>
        [XmlElement(ElementName = "texto_asig")]
        public string TextoAsignado { get; set; }

        /// <summary>
        /// Concepto
        /// </summary>
        [XmlElement(ElementName = "concepto")]
        public string Concepto { get; set; }

        /// <summary>
        /// Division
        /// </summary>
        [XmlElement(ElementName = "division")]
        public string Division { get; set; }

        /// <summary>
        /// Clase de Movimiento
        /// </summary>
        [XmlElement(ElementName = "clase_movt")]
        public string ClaseMovimiento { get; set; }

        /// <summary>
        /// BusAct
        /// </summary>
        [XmlElement(ElementName = "bus_act")]
        public string BusAct { get; set; }

        /// <summary>
        /// Periodo
        /// </summary>
        [XmlElement(ElementName = "periodo")]
        public string Periodo { get; set; }

        /// <summary>
        /// Numero de Linea
        /// </summary>
        [XmlElement(ElementName = "nolinea")]
        public string NumeroLinea { get; set; }

        /// <summary>
        /// Referencia 1
        /// </summary>
        [XmlElement(ElementName = "ref1")]
        public string Referencia1 { get; set; }

        /// <summary>
        /// Referencia 2
        /// </summary>
        [XmlElement(ElementName = "ref2")]
        public string Referencia2 { get; set; }

        /// <summary>
        /// Referencia 3
        /// </summary>
        [XmlElement(ElementName = "ref3")]
        public string Referencia3 { get; set; }

        /// <summary>
        /// Fecha Impuesto
        /// </summary>
        [XmlElement(ElementName = "fecha_imto")]
        public string FechaImpuesto { get; set; }

        /// <summary>
        /// Condicion Impuesto
        /// </summary>
        [XmlElement(ElementName = "cond_imto")]
        public string CondicionImpuesto { get; set; }

        /// <summary>
        /// Clave Impuesto
        /// </summary>
        [XmlElement(ElementName = "clave_imto")]
        public string ClaveImpuesto { get; set; }

        /// <summary>
        /// Tipo Retencion
        /// </summary>
        [XmlElement(ElementName = "tipo_ret")]
        public string TipoRetencion { get; set; }

        /// <summary>
        /// Codigo Retencion
        /// </summary>
        [XmlElement(ElementName = "cod_ret")]
        public string CodigoRetencion { get; set; }

        /// <summary>
        /// Impuesto Retencion
        /// </summary>
        [XmlElement(ElementName = "imp_ret")]
        public int ImpuestoRetencion { get; set; }

        /// <summary>
        /// Impuesto Iva
        /// </summary>
        [XmlElement(ElementName = "imp_iva")]
        public string ImpuestoIva { get; set; }

        /// <summary>
        /// Archivo Folio
        /// </summary>
        [XmlElement(ElementName = "archifolio")]
        public string ArchivoFolio { get; set; }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        [XmlIgnore]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha Modificacion
        /// </summary>
        [XmlIgnore]
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Organizacion que Genera la Poliza
        /// </summary>
        [XmlIgnore]
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        [XmlIgnore]
        public string Observaciones { get; set; }

        /// <summary>
        /// Descripcion del Costo
        /// </summary>
        [XmlIgnore]
        public string Descripcion { get; set; }

        /// <summary>
        /// Descripcion del Costo
        /// </summary>
        [XmlIgnore]
        public string DescripcionProducto { get; set; }

        /// <summary>
        /// Indentificador de registro en poliza
        /// </summary>
        [XmlIgnore]
        public int PolizaID { get; set; }
        /// <summary>
        /// Tipo de Poliza que se estara generando
        /// </summary>
        [XmlIgnore]
        public int TipoPolizaID { get; set; }

        /// <summary>
        /// Indica si se generara de nuevo la poliza
        /// </summary>
        [XmlIgnore]
        public bool Generar
        {
            get { return generar; }
            set
            {
                generar = value;
                NotifyPropertyChanged("Generar");
            }
        }

        /// <summary>
        /// Indica si la poliza ha sido conciliada
        /// </summary>
        [XmlElement(ElementName = "conciliada")]
        public bool Conciliada { get; set; }

        /// <summary>
        /// Indica si se habilitara el check de envio
        /// </summary>
        [XmlIgnore]
        public bool HabilitarCheck { get; set; }

        /// <summary>
        /// Indica si tiene inconcistencia el movimiento
        /// </summary>
        [XmlIgnore]
        public bool Inconcistencia { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public bool Faltante { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public int ArchivoEnviadoServidor { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public string DocumentoSAP { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public string DocumentoCancelacionSAP { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public string Segmento { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public string Corral { get; set; }

        /// <summary>
        /// Indica si falta
        /// </summary>
        [XmlIgnore]
        public int Procesada { get; set; }

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
