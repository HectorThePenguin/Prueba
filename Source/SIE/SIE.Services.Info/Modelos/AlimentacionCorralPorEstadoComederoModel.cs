using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionCorralPorEstadoComederoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlimentacionCorralPorEstadoComederoModel()
        {
            EstadoComederoDescripcion = string.Empty;
        }

        void notificarCambioColeccion(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        int estadoComederoID;
        public int EstadoComederoID
        {
            get
            {
                return estadoComederoID;
            }
            set
            {
                estadoComederoID = value;
                notificarCambioColeccion("EstadoComederoID");
            }
        }
        string estadoComederoDescripcion;
        public string EstadoComederoDescripcion
        {
            get
            {
                return estadoComederoDescripcion;
            }
            set
            {
                estadoComederoDescripcion = value;
                notificarCambioColeccion("EstadoComederoDescripcion");
            }
        }
        int totalCorrales;
        public int TotalCorrales
        {
            get
            {
                return totalCorrales;
            }
            set
            {
                totalCorrales = value;
                notificarCambioColeccion("TotalCorrales");
            }
        }
    }
}
