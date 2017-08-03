using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class PolizasIncorrectasInfo
    {
        //PolizasIncorrectas
        public int OrganizacionID { get; set; }
        public string FolioMovto { get; set; }
        public string FechaDocto { get; set; }
        public string Concepto { get; set; }
        public string Ref3 { get; set; }
        public decimal Cargos { get; set; }
        public decimal Abonos { get; set; }
        public string DocumentoSAP { get; set; }
        public bool Procesada { get; set; }
        public string Mensaje { get; set; }
        public int PolizaID { get; set; }
        public int TipoPolizaID { get; set; }
        public string TipoPoliza {get; set;}
            
    }
}
