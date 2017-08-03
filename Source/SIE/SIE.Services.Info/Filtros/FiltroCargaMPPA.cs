using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroCargaMPPA : INotifyPropertyChanged
    {
        private OrganizacionInfo organizacion;
        private string ruta;

        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        public string Ruta
        {
            get { return ruta; }
            set
            {
                ruta = value;
                NotifyPropertyChanged("Ruta");
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
