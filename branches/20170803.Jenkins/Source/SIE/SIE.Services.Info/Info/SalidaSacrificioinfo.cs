using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class SalidaSacrificioInfo : BitacoraInfo
    {
        /// <summary> 
        ///	Fecha de Socrificio  
        /// </summary> 		
        public string FEC_SACR { get; set; }

        /// <summary> 
        ///	Folio de la orden de sacrificio
        /// </summary> 
        public string NUM_SALI { get; set; }

        /// <summary> 
        ///	Código de corral  
        /// </summary> 
        public string NUM_CORR { get; set; }

        /// <summary> 
        ///	Codigo de producción
        /// </summary> 		
        public string NUM_PRO { get; set; }

        /// <summary> 
        ///	Fecha salida del corral
        /// </summary> 
        public string FEC_SALC { get; set; }

        /// <summary> 
        ///	Hora de salida  
        /// </summary> 
        public string HORA_SAL { get; set; }

        /// <summary> 
        ///	Estado de comedero  
        /// </summary> 
        public string EDO_COME { get; set; }

        /// <summary> 
        ///	Número de cabezas en el lote  
        /// </summary> 
        public int? NUM_CAB { get; set; }

        /// <summary> 
        ///	Tipo animal  
        /// </summary> 
        public string TIP_ANI { get; set; }

        /// <summary> 
        ///	Kilogramos de salida  
        /// </summary> 
        public int? KGS_SAL { get; set; }

        /// <summary> 
        ///	Precio  
        /// </summary> 
        public int? PRECIO { get; set; }

        /// <summary> 
        ///	Origen  
        /// </summary> 
        public string ORIGEN { get; set; }

        /// <summary> 
        ///	Cuenta  
        /// </summary> 
        public string CTA_PROVIN { get; set; }

        /// <summary> 
        ///	
        /// </summary> 
        public int? PRE_EST { get; set; }

        /// <summary> 
        ///	Identificador de la salida a sacrificio. 
        /// </summary>
        public int ID_SalidaSacrificio { get; set; }

        /// <summary> 
        ///	Tipo de venta  
        /// </summary> 
        public string VENTA_PARA { get; set; }

        /// <summary> 
        ///	Código de proveedor  
        /// </summary> 
        public string COD_PROVEEDOR { get; set; }

        /// <summary> 
        ///	Notas  
        /// </summary> 
        public string NOTAS { get; set; }

        /// <summary> 
        ///	Costo por cabeza 
        /// </summary> 
        public string COSTO_CABEZA { get; set; }

        /// <summary> 
        ///	Cabezas procesadas  
        /// </summary> 
        public int CABEZAS_PROCESADAS { get; set; }

        /// <summary> 
        ///	Fecha inicio  
        /// </summary> 
        public int FICHA_INICIO { get; set; }

        /// <summary> 
        ///	Costo Corral  
        /// </summary> 
        public string COSTO_CORRAL { get; set; }

        /// <summary> 
        ///	Unidades entradas  
        /// </summary> 
        public string UNI_ENT { get; set; }

        /// <summary> 
        ///	Unidades de salida
        /// </summary> 
        public string UNI_SAL { get; set; }

        /// <summary> 
        ///	Sync
        /// </summary> 
        public string SYNC { get; set; }

        /// <summary> 
        ///	Identificador de salida sacrificio
        /// </summary> 
        public int? ID_S { get; set; }

        /// <summary> 
        ///	Sexo
        /// </summary> 
        public int? SEXO { get; set; }

        /// <summary> 
        ///	Dias de engorda  
        /// </summary> 
        public string DIAS_ENG { get; set; }

        /// <summary> 
        ///	Folio entrada 
        /// </summary> 
        public string FOLIO_ENTRADA_I { get; set; }

        /// <summary> 
        ///	Origen ganado
        /// </summary> 
        public string ORIGEN_GANADO { get; set; }

        /// <summary> 
        ///	Tipo salida
        /// </summary> 
        public string TIPO_SALIDA { get; set; }

        /// <summary> 
        ///	Organización a la que pertenece 
        /// la salida de sacrificio
        /// </summary> 		
        public int OrganizacionID { get; set; }

        /// <summary> 
        ///	Identificador del Corral
        /// </summary> 
        public int CorralID { get; set; }

        /// <summary> 
        ///	Identificador del Lote
        /// </summary> 
        public int LoteID { get; set; }

        /// <summary> 
        ///	Clasificación del ganado
        /// </summary> 
        public string Clasificacion { get; set; }

        public int? OrdenSacrificioID { get; set; }

        /// <summary> 
        ///	Detalle de la orden de sacrificio para guardar en la salida de sacrificio del SCP
        /// </summary> 
        public List<SalidaSacrificioDetalleInfo> Detalle { get; set; }

        /// <summary> 
        ///	Variable creada para almacenar cantidades muy grandes, no forma parte de la entidad como tal
        /// </summary> 
        public long AuxiliarId { get; set; }

        /// <summary> 
        ///	Variable creada para almacenar los posibles estatus de de la salida en marel
        /// </summary> 
        public int Estatus { get; set; }

        /// <summary> 
        ///	Variable creada para almacenar el lote de un loteID
        /// </summary> 
        public string Lote { get; set; }
    }
}
