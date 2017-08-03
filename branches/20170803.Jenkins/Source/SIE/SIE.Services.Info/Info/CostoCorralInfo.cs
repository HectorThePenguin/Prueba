using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;

namespace SIE.Services.Info.Info
{
    public class CostoCorralInfo : INotifyPropertyChanged
    {
        private int costoId;
        private string descripcion;
        private decimal importe;

        public int CostoID
        {
          get { return costoId; }
            set { costoId = value; NotifyPropertyChanged("CostoID"); }
        }

        public string Descripcion
        {
            get { return descripcion; } 
            set 
            {
                descripcion = value; 
                NotifyPropertyChanged("Descripcion");
            }
        }

        public decimal Importe
        {
            get { return importe; } 
            set { importe = value;NotifyPropertyChanged("Importe"); }
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
