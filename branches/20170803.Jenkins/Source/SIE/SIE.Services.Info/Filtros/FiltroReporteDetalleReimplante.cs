using System;
using System.ComponentModel;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroReporteDetalleReimplante : INotifyPropertyChanged
    {
        private OrganizacionInfo organizacion;
        private DateTime? fecha;
        
        private bool valido;

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                if (value != organizacion)
                {
                    organizacion = value;
                    NotifyPropertyChanged("Organizacion");
                    Valido = true;
                }
                else
                {
                    Valido = false;
                }
            }
        }

        public DateTime? Fecha
        {
            get { return fecha; }
            set
            {
                if (value != fecha)
                {
                    fecha = value;
                    NotifyPropertyChanged("Fecha");
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

                if (organizacion != null && organizacion.OrganizacionID== 0)
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
                else if (fecha > DateTime.Today)
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
