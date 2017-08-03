using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class RegistroVigilanciaHumedadInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private decimal humedad;
        private RegistroVigilanciaInfo registroVigilancia;
        private int numeroMuestra;
        private DateTime fechaMuestra;

		/// <summary> 
		///	RegistroVigilanciaHumedadID  
		/// </summary> 
		public int RegistroVigilanciaHumedadID { get; set; }

        /// <summary> 
        ///	RegistroVigilanciaID  
        /// </summary> 
        public RegistroVigilanciaInfo RegistroVigilancia
        {
            get { return registroVigilancia; }
            set
            {
                registroVigilancia = value;
                NotifyPropertyChanged("RegistroVigilancia");
            }
        }

        /// <summary> 
        ///	Humedad  
        /// </summary> 
        public decimal Humedad
        {
            get { return humedad; }
            set 
            { 
                humedad = value;
                NotifyPropertyChanged("Humedad");
            }
        }

        /// <summary> 
        ///	NumeroMuestra  
        /// </summary> 
        public int NumeroMuestra
        {
            get { return numeroMuestra; }
            set
            {
                numeroMuestra = value;
                NotifyPropertyChanged("NumeroMuestra");
            }
        }

        /// <summary> 
        ///	FechaMuestra  
        /// </summary> 
        public DateTime FechaMuestra
        {
            get { return fechaMuestra; }
            set
            {
                fechaMuestra = value;
                NotifyPropertyChanged("FechaMuestra");
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
