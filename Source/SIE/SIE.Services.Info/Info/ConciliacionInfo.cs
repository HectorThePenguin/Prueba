using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ConciliacionInfo 
    {

        ////PolizasIncorrectas
        //public int OrganizacionID {get;set;}
        //public string FolioMovto  {get; set;}
        //public string FechaDocto {get; set;}
        //public string Concepto {get; set;}
        //public string Ref3 {get; set;}
        //public decimal Cargos {get; set;}
        //public decimal Abonos {get; set;}
        //public string DocumentoSAP {get; set;}
        //public bool Procesada {get; set;}
        //public string Mensaje {get;set;}
        //public int PolizaID {get; set;}
        //public int TipoPolizaID {get; set;}



        //CONCILIACION

        public string NoRef { get; set; }
        public string FechaDocto {get; set;}
        public string FechaCont {get; set;}
        public string ClaseDoc {get; set;}
        public string Sociedad {get; set;}
        public string Moneda {get; set;}
        public string TipoCambio {get;set;}
        public string TextoDocto {get; set;}
        public string Mes {get; set;}
        public string Cuenta {get; set;}
        public string Proveedor {get; set;}
        public string Cliente {get;set;}
        public string Importe {get; set;}
        public string Concepto {get; set;}
        public string Division {get; set; }
        public string NoLinea {get; set;}
        public string Ref3 {get; set;}
        public string ArchivoFolio {get; set;}
        public string DocumentoSAP {get; set;}
        public string DocumentoCancelacionSAP {get; set;}
        public string Segmento {get; set;}
        public int  OrganizacionID {get; set;}
        public bool Conciliada {get; set;}
        public bool Procesada {get; set;}
        public bool Cancelada {get; set;}
        public string Mensaje {get; set;}
        public int PolizaID {get;set;}
        public int TipoPolizaID {get;set;}
        public string TipoPoliza { get; set; }
        
    }
}
