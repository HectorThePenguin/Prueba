using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PlazoCreditoInfo : INotifyPropertyChanged
    {
        private int plazoCreditoID;
        private string descripcion;
        private EstatusEnum activo;
        private int usuarioCreacionID;
        private int usuarioModificacionID;

        public int PlazoCreditoID
        {
            get { return plazoCreditoID; }
            set
            {
                plazoCreditoID = value;
                NotifyPropertyChanged("PlazoCreditoID");
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

        public EstatusEnum Activo
        {
            get { return activo; }
            set
            {
                activo = value;
                NotifyPropertyChanged("Activo");
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
