using System;
using System.ComponentModel;
using SIE.Services.Info.Info;
namespace SIE.Services.Info.Filtros
{
    public class FiltroParametrosKardexGanado
    {
        private int organizacionId;
        private int tipoproceso;
        private DateTime? fechaFin;
        private bool valido;
        private DateTime? fechaInicio;

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


        public int TipoProceso
        {
            get { return tipoproceso; }
            set
            {
                tipoproceso = value;
                NotifyPropertyChanged("tipoproceso");
            }
        }

        public DateTime? FechaFin
        {
            get { return fechaFin; }
            set
            {
                if (value != fechaFin)
                {
                    fechaFin = value;
                    NotifyPropertyChanged("FechaFin");
                    Valido = true;
                }
                else
                {
                    Valido = false;
                }
            }
        }

        public DateTime? FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                if (value != fechaInicio)
                {
                    fechaInicio = value;
                    NotifyPropertyChanged("FechaInicio");
                    Valido = true;
                }
                else
                {
                    Valido = false;
                }
            }
        }

      

        public bool Valido
        {
            get { return valido; }
            set
            {

                if (fechaFin == DateTime.Now)
                {
                    valido = true;
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
