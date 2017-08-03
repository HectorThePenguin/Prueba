using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CorralesPorOrganizacionInfo:BitacoraInfo, INotifyPropertyChanged
    {
        #region miembros de la clase

        private int parametroID;
        private int organizacionID;
        private int entradaGanadoTransitoID;
        private string descripcion;
        private string valor;
        private int corralID;
        private int loteID;
        private int cabezas;
        private int pesoPromedio;
        #endregion

        #region propiedades

        public int ParametroID
        {
            get{return parametroID;}
            set
            {
                parametroID = value;
                NotifyPropertyChanged("ParametroID");
            }
        }

        public int EntradaGanadoTransitoID
        {
            get
            {
                return entradaGanadoTransitoID;
            }
            set
            {
                entradaGanadoTransitoID = value;
                NotifyPropertyChanged("EntradaGanadoTransitoID");
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("Descripcion");
            }
        }

        public string Valor
        {
            get
            {
                return valor;
            }
            set
            {
                valor = value;
                NotifyPropertyChanged("Valor");
            }
        }

        public int CorralID
        {
            get
            {
                return corralID;
            }
            set
            {
                corralID = value;
                NotifyPropertyChanged("CorralID");
            }
        }
      
        public int LoteID
        {
            get
            {
                return loteID;
            }
            set
            {
                loteID = value;
                NotifyPropertyChanged("LoteID");
            }
        }

        public int Cabezas
        {
            get
            {
                return cabezas;
            }
            set
            {
                cabezas = value;
                NotifyPropertyChanged("Cabezas");
            }
        }

        public int PesoPromedio
        {
            get
            {
                return pesoPromedio;
            }
            set
            {
                pesoPromedio = value;
                NotifyPropertyChanged("PesoPromedio");
            }
        }

        public int OrganizacionID
        {
            get { return organizacionID; }
            set { organizacionID = value;NotifyPropertyChanged("OrganizacionID"); }
        }

        #endregion

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
