using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class SeleccionInfoModelo<T> : INotifyPropertyChanged where T : class
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void notificar(string nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
            }
        }

        public SeleccionInfoModelo()
        {

        }

        T elemento;
        public T Elemento
        {
            get
            {
                return elemento;
            }
            set
            {
                elemento = value;
                notificar("Elemento");
            }
        }

        bool marcado;
        public bool Marcado
        {
            get
            {
                return marcado;
            }
            set
            {
                marcado = value;
                notificar("Marcado");
            }
        }

    }
}
