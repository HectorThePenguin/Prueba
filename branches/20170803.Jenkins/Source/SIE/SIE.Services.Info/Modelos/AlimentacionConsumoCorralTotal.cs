
namespace SIE.Services.Info.Modelos
{
    public class AlimentacionConsumoCorralTotal : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void notificarCambioPropiedad(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propiedad));
            }
        }
        int formulaID;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int FormulaId
        {
            get
            {
                return formulaID;
            }
            set
            {
                formulaID = value;
                notificarCambioPropiedad("FormulaId");
            }
        }
        string formula;
        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
                notificarCambioPropiedad("Formula");
            }
        }
        int? totalKilos;
        public int? TotalKilos
        {
            get
            {
                return totalKilos;
            }
            set
            {
                totalKilos = value;
                notificarCambioPropiedad("TotalKilos");
            }
        }
        int? kilosTrans;
        public int? KilosTrans
        {
            get
            {
                return kilosTrans;
            }
            set
            {
                kilosTrans = value;
                notificarCambioPropiedad("KilosTrans");
            }
        }

        private int? kilosCorral;
        public int? KilosCorral
        {
            
            get
            {
                int kilosTransferencia = kilosTrans ?? 0;
                return totalKilos - kilosTransferencia < 0 ? totalKilos - kilosTransferencia * -1 : totalKilos - kilosTransferencia;
            }
            set
            {
                kilosCorral = value;
                notificarCambioPropiedad("KilosCorral");
            }
        }

        //decimal
        //int? totalCosto;
        //public int? TotalCosto
        //{
        //    get
        //    {
        //        return totalCosto;
        //    }
        //    set
        //    {
        //        totalCosto = value;
        //        notificarCambioPropiedad("TotalCosto");
        //    }
        //}
        //int? costoTrans;
        //public int? CostoTrans
        //{
        //    get
        //    {
        //        return costoTrans;
        //    }
        //    set
        //    {
        //        costoTrans = value;
        //        notificarCambioPropiedad("CostoTrans");
        //    }
        //}
        decimal? costoTrans;
        public decimal? CostoTrans
        {
            get
            {
                return costoTrans;
            }
            set
            {
                costoTrans = value;
                notificarCambioPropiedad("CostoTrans");
            }
        }
        //agregado por jose angel rodriguez
        long? repartoID;
        public long? RepartoID
        {
            get
            {
                return repartoID;
            }
            set
            {
                repartoID = value;
                notificarCambioPropiedad("CostoTrans");
            }
        }
        decimal? totalCosto;
        public decimal? TotalCosto
        {
            get
            {
                return totalCosto;
            }
            set
            {
                totalCosto = value;
                notificarCambioPropiedad("TotalCosto");
            }
        }
        decimal? sumatoriaCosto;
        [Atributos.AtributoIgnorarColumnaExcel]
        public decimal? SumatoriaCosto
        {
            get { return sumatoriaCosto; }
            set
            {
                sumatoriaCosto = value;
                notificarCambioPropiedad("SumatoriaCosto");
            }
        }
        //private int? costoCorral;
        //public int? CostoCorral
        //{
        //    get { return (totalCosto.HasValue ? totalCosto.Value : 0) - (costoTrans.HasValue ? costoTrans.Value : 0); }
        //    set
        //    {
        //        costoCorral = value;
        //        notificarCambioPropiedad("CostoCorral");
        //    }
        //}
        private decimal? costoCorral;
        public decimal? CostoCorral
        {
            get { return (totalCosto.HasValue ? totalCosto.Value : 0) - (costoTrans.HasValue ? costoTrans.Value : 0); }
            set
            {
                costoCorral = value;
                notificarCambioPropiedad("CostoCorral");
            }
        }
        int? totalDiasAcomulado;
        public int? TotalDiasAcumulado
        {
            get
            {
                return totalDiasAcomulado;
            }
            set
            {
                totalDiasAcomulado = value;
                notificarCambioPropiedad("TotalDiasAcumulado");
            }
        }
        int? totalDiasAcomuladoTransferidos;
        public int? TotalDiasAcumuladoTransferidos
        {
            get { return (totalDiasAcomuladoTransferidos.HasValue ? totalDiasAcomuladoTransferidos.Value : 0); }
            set
            {
                totalDiasAcomuladoTransferidos = value;
                notificarCambioPropiedad("TotalDiasAcumuladoTransferidos");
            }
        }

        private int? totalDiasAcumuladoCorral;
        public int? TotalDiasAcomuladoCorral
        {
            get
            {
                return (totalDiasAcomulado.HasValue ? totalDiasAcomulado.Value : 0)
                       - (totalDiasAcomuladoTransferidos.HasValue ? totalDiasAcomuladoTransferidos.Value : 0);
            }
            set
            {
                totalDiasAcumuladoCorral = value;
                notificarCambioPropiedad("TotalDiasAcomuladoCorral");
            }
        }

        int? sumatoriaDiasAcumulados;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaDiasAcumulados
        {
            get { return sumatoriaDiasAcumulados; }
            set
            {
                sumatoriaDiasAcumulados = value;
                notificarCambioPropiedad("SumatoriaDiasAcumulados");
            }
        }

        int sumatoriaDiasAcumuladosTransferidos;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int SumatoriaDiasAcumuladosTransferidos
        {
            get { return sumatoriaDiasAcumuladosTransferidos; }
            set
            {
                sumatoriaDiasAcumuladosTransferidos = value;
                notificarCambioPropiedad("SumatoriaDiasAcumuladosTransferidos");
            }
        }

        int? sumatoriaKilos;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaKilos
        {
            get { return sumatoriaKilos; }
            set
            {
                sumatoriaKilos = value;
                notificarCambioPropiedad("SumatoriaKilos");
            }
        }
        //int? sumatoriaCosto;
        //[Atributos.AtributoIgnorarColumnaExcel]
        //public int? SumatoriaCosto
        //{
        //    get { return sumatoriaCosto; }
        //    set
        //    {
        //        sumatoriaCosto = value;
        //        notificarCambioPropiedad("SumatoriaCosto");
        //    }
        //}
        

        int? sumatoriaKilosTransferencia;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaKilosTransferencia
        {
            get { return sumatoriaKilosTransferencia; }
            set
            {
                sumatoriaKilosTransferencia = value;
                notificarCambioPropiedad("SumatoriaKilosTransferencia");
            }
        }

        int? sumatoriaCostoTransferencia;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaCostoTransferencia
        {
            get { return sumatoriaCostoTransferencia; }
            set
            {
                sumatoriaCostoTransferencia = value;
                notificarCambioPropiedad("SumatoriaCostoTransferencia");
            }
        }

        int? sumatoriaCostoCorral;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaCostoCorral
        {
            get { return sumatoriaCostoCorral; }
            set
            {
                sumatoriaCostoCorral = value;
                notificarCambioPropiedad("SumatoriaCostoCorral");
            }
        }

        private int? sumatoriaDiasAcumuladoCorral;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaDiasAcumuladoCorral
        {
            get { return sumatoriaDiasAcumuladoCorral; }
            set
            {
                sumatoriaDiasAcumuladoCorral = value;
                notificarCambioPropiedad("SumatoriaDiasAcumuladoCorral");
            }
        }

        private int? sumatoriaKilosCorral;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int? SumatoriaKilosCorral
        {
            get { return sumatoriaKilosCorral; }
            set
            {
                sumatoriaKilosCorral = value;
                notificarCambioPropiedad("SumatoriaKilosCorral");
            }
        }
    }
}
