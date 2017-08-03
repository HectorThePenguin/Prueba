using System;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionConsumoCorralDetalle : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void notificarCambioPropiedad(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propiedad));
            }
        }
        string fecha;
        public string Fecha
        {
            get
            {
                return fecha;
            }
            set
            {
                fecha = value;
                notificarCambioPropiedad("Fecha");
            }
        }
        int formulaId;
        [Atributos.AtributoIgnorarColumnaExcel]
        public int FormulaId
        {
            get
            {
                return formulaId;
            }
            set
            {
                formulaId = value;
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
        int? cabezas;
        public int? Cabezas
        {
            get
            {
                return cabezas;
            }
            set
            {
                cabezas = value;
                notificarCambioPropiedad("Cabezas");
            }
        }
        int kilosDia;
        public int KilosDia
        {
            get
            {
                return kilosDia;
            }
            set
            {
                kilosDia = value;
                notificarCambioPropiedad("KilosDia");
            }
        }
        int? servidosAcomulados;
        public int? ServidosAcomulados
        {
            get
            {
                return servidosAcomulados;
            }
            set
            {
                servidosAcomulados = value;
                notificarCambioPropiedad("ServidosAcomulados");
            }
        }
        int diasAnimal;
        public int DiasAnimal
        {
            get
            {
                return diasAnimal;
            }
            set
            {
                diasAnimal = value;
                notificarCambioPropiedad("DiasAnimal");
            }
        }
        decimal? consumoDia;
        public decimal? ConsumoDia
        {
            get
            {
                return consumoDia;
            }
            set
            {
                consumoDia = value;
                notificarCambioPropiedad("ConsumoDia");
            }
        }
        //public decimal? PromedioAcomulado
        //{
        //    get
        //    {
        //        var retValue = Decimal.Zero;
        //        if (ServidosAcomulados != null && ServidosAcomulados > 0 && DiasAnimal > 0)
        //        {
        //            retValue = (decimal)ServidosAcomulados / DiasAnimal;
        //        }
                    

        //        return retValue;
        //    }
            
        //}
        int cabezasAcumulados;
        public int CabezasAcumulados
        {
            get
            {
                return cabezasAcumulados;
            }
            set
            {
                cabezasAcumulados = value;
                notificarCambioPropiedad("DiasAnimal");
            }
        }
        //public decimal? PromedioAcomulado
        //{
        //    get
        //    {
        //        var retValue = Decimal.Zero;
        //        if ( KilosDia > 0 && DiasAnimal > 0)
        //        {
        //            retValue = (decimal)KilosDia / DiasAnimal;
        //        }


        //        return retValue;
        //    }

        //}

        public decimal? PromedioAcomulado
        {
            get
            {
                var retValue = Decimal.Zero;
                if (ServidosAcomulados != null && ServidosAcomulados > 0 && DiasAnimal > 0)
                {
                    retValue = (decimal)ServidosAcomulados / CabezasAcumulados;
                }
                return retValue;
            }
        }

        decimal precio;
        public decimal Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
                notificarCambioPropiedad("Precio");
            }
        }
        decimal importe;
        public decimal Importe
        {
            get
            {
                return importe;
            }
            set
            {
                importe = value;
                notificarCambioPropiedad("Importe");
            }
        }
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
    }
}
