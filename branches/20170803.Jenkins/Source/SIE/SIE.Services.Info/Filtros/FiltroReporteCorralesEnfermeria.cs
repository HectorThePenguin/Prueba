using System;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroReporteCorralesEnfermeria : INotifyPropertyChanged
    {
        private EnfermeriaInfo enfermeria;
        private DateTime? fecha;
        private bool valido;

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;


        // set
        //{
        //    organizacionID = value;
        //    notificarCambioColeccion("OrganizacionID");
        //}


        public EnfermeriaInfo Enfermeria
        {
            get { return enfermeria; }
            set
            {
                enfermeria = value;
                NotifyPropertyChanged("Enfermeria");
                ValidarCatura();
            }
        }

        public DateTime? Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                NotifyPropertyChanged("Fecha");
                ValidarCatura();
            }
        }
        
        public bool Valido
        {
            get { return valido; }
            set
            {
                valido = value;
                NotifyPropertyChanged("Valido");
               
            }
        }

        private void ValidarCatura()
        {
            if (fecha == null)
            {
                valido = false;
            }

            else if (fecha > DateTime.Today)
            {
                valido = false;
            }
            //else if (enfermeria == null)
            //{
            //    valido = false;
            //}
            //else if (enfermeria.EnfermeriaID == 0)
            //{
            //    valido = false;
            //}
            else
            {
                valido = true;
            }
            NotifyPropertyChanged("Valido");
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
