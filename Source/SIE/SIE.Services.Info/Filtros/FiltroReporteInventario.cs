using System;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroReporteInventario : INotifyPropertyChanged
    {
        private TipoProcesoInfo tipoProceso;
        private DateTime? fechaInicial;
        private DateTime? fechaFinal;
        private bool valido;

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public TipoProcesoInfo TipoProceso
        {
            get { return tipoProceso; }
            set
            {
                if (value != tipoProceso)
                {
                    tipoProceso = value;
                    NotifyPropertyChanged("TipoProceso");
                    Valido = true;
                }
                else
                {
                    Valido = false;
                }
            }
        }

        public DateTime? FechaInicial
        {
            get { return fechaInicial; }
            set
            {
                if (value != fechaInicial)
                {
                    fechaInicial = value;
                    NotifyPropertyChanged("FechaInicial");
                    Valido = true;
                }
                else
                {
                    Valido = false;
                }
            }
        }

        public DateTime? FechaFinal
        {
            get { return fechaFinal; }
            set
            {
                if (value != fechaFinal)
                {
                    fechaFinal = value;
                    NotifyPropertyChanged("FechaFinal");
                    Valido = true;
                }
            }
        }

        public bool Valido
        {
            get { return valido; }
            set
            {

                if (tipoProceso != null && tipoProceso.TipoProcesoID < 0)
                {
                    valido = false;
                }
                //else if (fechaInicial == null)
                //{
                //    valido = false;
                //}
                //else if (fechaInicial == null && fechaFinal == null)
                //{
                //    valido = false;
                //}
                else if (fechaInicial > DateTime.Today)
                {
                    valido = false;
                }
                else
                {
                    valido = value;
                }
                //else if (fechaFinal == null)
                //{
                //    valido = false;
                //}
                //else if (fechaFinal < fechaInicial)
                //{
                //    valido = false;
                //}
                
                NotifyPropertyChanged("Valido");
            }
        }

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
