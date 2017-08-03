using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class FolioMultipesajeInfo : INotifyPropertyChanged
    {
        private long folio;
        private string producto;
        private string chofer;
        private int organizacionId;

        public long Folio
        {
            get { return folio; }
            set
            {
                folio = value;
                NotifyPropertyChanged("Folio");
            }
        }

        public string Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged("Producto");
            }
        }

        public string Chofer { 
            get { return chofer; }
            set
            {
                chofer = value;
                NotifyPropertyChanged("Chofer");
            } 
        }

        public int OrganizacionId
        {
            get { return organizacionId; }
            set
            {
                organizacionId = value;
                NotifyPropertyChanged("OrganizacionId");
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
