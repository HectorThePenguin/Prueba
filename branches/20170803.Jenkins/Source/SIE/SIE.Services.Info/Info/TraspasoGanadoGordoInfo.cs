using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class TraspasoGanadoGordoInfo
    {
        public int OrganizacionId { get; set; }
        public OrganizacionInfo OrganizacionDestino { get; set; }
        public int FolioTraspaso { get; set; }
        public CorralInfo Corral { get; set; }
        public LoteInfo Lote { get; set; }
        public TipoGanadoInfo TipoGanadoId { get; set; }
        public int PesoProyectado { get; set; }
        public decimal GananciaDiaria { get; set; }
        public int DiasEngorda { get; set; }
        public FormulaInfo FormulaId { get; set; }
        public int DiasFormula { get; set; }
        public int Cabezas { get; set; }
        public DateTime FechaEnvio { get; set; }
        public EstatusEnum Activo { get; set; }
    }
}
