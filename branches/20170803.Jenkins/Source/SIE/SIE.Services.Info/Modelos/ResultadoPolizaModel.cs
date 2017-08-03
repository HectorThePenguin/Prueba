using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.IO;

namespace SIE.Services.Info.Modelos
{
    public class ResultadoPolizaModel
    {
        public string NomenclaturaArchivo { get; set; }
        public IList<PolizaInfo> Polizas { get; set; }
        public MemoryStream PDF { get; set; }
        public Dictionary<TipoPoliza, MemoryStream> PDFs { get; set; }
        public OrganizacionInfo Organizacion { get; set; }
    }
}
