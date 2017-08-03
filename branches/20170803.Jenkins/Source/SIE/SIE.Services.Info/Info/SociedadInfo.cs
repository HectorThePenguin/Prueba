using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class SociedadInfo : INotifyPropertyChanged 
    {
        public  int SociedadID { get;set; }
        public  string Descripcion { get; set; }
        public int Pais { get; set; }
        public bool Activo  { get; set; }

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
