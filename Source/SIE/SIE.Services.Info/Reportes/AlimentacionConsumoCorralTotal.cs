namespace SIE.Services.Info.Reportes
{
    public class AlimentacionConsumoCorralTotalInfo
    {
        public int formulaID{get;set;}
        public string formula{get;set;}
        public int totalKilos{get;set;}
        public int kilosTrans{get;set;}
        public int kilosCorral{get;set;}
        //public int totalCosto{get;set;}
        public decimal totalCosto { get; set; }
        //public int costoTrans{get;set;}
        public decimal costoTrans { get; set; }
        //public int costoCorral{get;set;}
        public decimal costoCorral { get; set; }
        public int totalDiasAcomulado{get;set;}
        public int totalDiasAcomuladoTransferidos{get;set;}
        public int totalDiasAcumuladoCorral{get;set;}
        public int sumatoriaDiasAcumulados{get;set;}
        public int sumatoriaDiasAcumuladosTransferidos{get;set;}
        public int sumatoriaKilos{get;set;}
        //public int sumatoriaCosto{get;set;}
        public decimal sumatoriaCosto { get; set; }
        public int sumatoriaKilosTransferencia{get;set;}
        public int sumatoriaCostoTransferencia{get;set;}
        public int sumatoriaCostoCorral{get;set;}
        public int sumatoriaDiasAcumuladoCorral{get;set;}
        public int sumatoriaKilosCorral{get;set;}
    }
}
