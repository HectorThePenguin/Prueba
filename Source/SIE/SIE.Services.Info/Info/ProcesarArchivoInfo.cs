using System;

namespace SIE.Services.Info.Info
{
    public class ProcesarArchivoInfo
    {
        public int batch { get; set; }
        public string Formula { get; set; }
        public string Codigo { get; set; }
        public int Meta { get; set; }
        public int Real { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Marca { get; set; }
        public string Rotomix { get; set; }

        public int OrganizacionID { get; set; }
        public int ProductoID { get; set; }
        public int FormulaID { get; set; }
        public int RotoMixID { get; set; }
        public DateTime FechaProduccion { get; set; }
    }
}
