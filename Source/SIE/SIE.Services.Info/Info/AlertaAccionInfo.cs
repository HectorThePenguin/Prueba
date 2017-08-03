using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AlertaAccionInfo : BitacoraInfo
    {
        private int alertaAccionId;
        private int alertaId;
        private int accionId;
        private string descripcion;
        private bool nuevo;

        public int AlertaAccionId
        {
            get { return alertaAccionId; }
            set
            {
                alertaAccionId = value;
            }
        }

        public int AlertaId
        {
            get { return alertaId; }
            set
            {
                alertaId = value;
            }
        }

        public int AccionId
        {
            get { return accionId; }
            set
            {
                accionId = value;
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
            }
        }

        public bool Nuevo
        {
            get { return nuevo; }
            set
            {
                nuevo = value;
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
