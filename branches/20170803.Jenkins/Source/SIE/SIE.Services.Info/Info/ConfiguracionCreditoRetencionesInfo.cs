using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ConfiguracionCreditoRetencionesInfo : INotifyPropertyChanged
    {
        private int configuracionCreditoID;
        private int numeroMes;
        private int porcentajeRetencion;
        private int usuarioCreacionID;
        private int usuarioModificacionID;

        public int ConfiguracionCreditoID
        {
            get { return configuracionCreditoID; }
            set
            {
                configuracionCreditoID = value;
                NotifyPropertyChanged("ConfiguracionCreditoID");
            }
        }

        public int NumeroMes
        {
            get { return numeroMes; }
            set
            {
                numeroMes = value;
                NotifyPropertyChanged("NumeroMes");
            }
        }

        public int PorcentajeRetencion
        {
            get { return porcentajeRetencion; }
            set
            {
                porcentajeRetencion = value;
                NotifyPropertyChanged("PorcentajeRetencion");
            }
        }

        public int UsuarioCreacionID
        {
            get { return usuarioCreacionID; }
            set
            {
                usuarioCreacionID = value;
                NotifyPropertyChanged("UsuarioCreacionID");
            }
        }

        public int UsuarioModificacionID
        {
            get { return usuarioModificacionID; }
            set
            {
                usuarioModificacionID = value;
                NotifyPropertyChanged("UsuarioModificacionID");
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