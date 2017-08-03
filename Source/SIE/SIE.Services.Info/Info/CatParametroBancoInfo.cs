using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;


namespace SIE.Services.Info.Info
{   
    public class CatParametroBancoInfo : BitacoraInfo, INotifyPropertyChanged
    {

        private int parametroID;
        private string descripcion = string.Empty;
        private string clave = string.Empty;
        private string valor = string.Empty;
        private TipoParametroBancoEnum tipoParametroID;


        public int ParametroID
        {
            get { return parametroID; }
            set
            {
                parametroID = value;
                NotifyPropertyChanged("ParametroID");
            }
        }

        public string Clave
        {
            get { return clave; }
            set
            {
                clave = value;
                NotifyPropertyChanged("Clave");
            }
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

        public string Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                NotifyPropertyChanged("Valor");
            }
        }

        public TipoParametroBancoEnum TipoParametroID
        {
            get { return tipoParametroID; }
            set
            {
                tipoParametroID = value;
                NotifyPropertyChanged("TipoParametroID");
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
