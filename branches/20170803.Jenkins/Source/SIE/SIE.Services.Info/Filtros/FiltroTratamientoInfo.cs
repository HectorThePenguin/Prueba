
using System.ComponentModel;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroTratamientoInfo : INotifyPropertyChanged
    {
        private OrganizacionInfo organizacion;
        private int codigoTratamiento;
        private TipoTratamientoInfo tipoTratamiento;
        private EstatusEnum estatus;

        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                if (value != organizacion)
                {
                    organizacion = value;
                    NotifyPropertyChanged("Organizacion");
                }
            }
        }
        public int CodigoTratamiento
        {
            get { return codigoTratamiento; }
            set
            {
                if (value != codigoTratamiento)
                {
                    codigoTratamiento = value;
                    NotifyPropertyChanged("CodigoTratamiento");
                }
            }
        }
        public TipoTratamientoInfo TipoTratamiento
        {
            get { return tipoTratamiento; }
            set
            {
                if (value != tipoTratamiento)
                {
                    tipoTratamiento = value;
                    NotifyPropertyChanged("TipoTratamiento");
                }
            }
        }
        public EstatusEnum Estatus
        {
            get { return estatus; }
            set
            {
                if (value != estatus)
                {
                    estatus = value;
                    NotifyPropertyChanged("Estatus");
                }
            }
        }


        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

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
