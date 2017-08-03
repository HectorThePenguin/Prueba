using System;
using System.ComponentModel;

namespace SIE.Services.Info.Filtros
{
    public class FiltroFechasInfo : INotifyPropertyChanged
    {
        private int organizacionId;
        private DateTime? fechaInicial;
        private DateTime? fechaFinal;
        private bool valido;

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public int OrgazizacionId
        {
            get { return organizacionId; }
            set
            {
                organizacionId = value;
                NotifyPropertyChanged("organizacionId");
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
                var fechaIni = new DateTime();
                var fechaFin = new DateTime();
                DateTime fechaActual =Convert.ToDateTime(DateTime.Today.ToShortDateString());
                if (fechaInicial.HasValue)
                {
                    fechaIni = Convert.ToDateTime(fechaInicial.Value.ToShortDateString());
                }
                if (fechaFinal.HasValue)
                {
                    fechaFin = Convert.ToDateTime(fechaFinal.Value.ToShortDateString());
                }
                if (fechaInicial == null && fechaFinal == null)
                {
                    valido = false;
                }
                else if (fechaInicial == null)
                {
                    valido = false;
                }
                else if (fechaFinal == null)
                {
                    valido = false;
                }
                else if (fechaIni > fechaActual)
                {
                    valido = false;
                }
                else if (fechaFin < fechaIni)
                {
                    valido = false;
                }
                else
                {
                    valido = value;
                }
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
