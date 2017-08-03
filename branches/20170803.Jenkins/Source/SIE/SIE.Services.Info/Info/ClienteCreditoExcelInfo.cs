using System.ComponentModel;
using System;

namespace SIE.Services.Info.Info
{
    public class ClienteCreditoExcelInfo : INotifyPropertyChanged
    {
        private int clienteID;
        private int creditoID;
        private string  nombre;

        public int ClienteID
        {
            get { return clienteID; }
            set
            {
                clienteID = value;
                NotifyPropertyChanged("ClienteID");
            }
        }
       
        public int CreditoID
        {
            get { return creditoID; }
            set
            {
                creditoID = value;
                NotifyPropertyChanged("CreditoID");
            }
        }
  
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                NotifyPropertyChanged("Nombre");
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
