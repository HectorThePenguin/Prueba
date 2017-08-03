using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionRepartoDetalleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlimentacionRepartoDetalleModel()
        {

        }

        void notificarCambioColeccion(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        int loteID;
        public int LoteID
        {
            get
            {
                return loteID;
            }
            set
            {
                loteID = value;
                notificarCambioColeccion("LoteID");
            }
        }

        DateTime fecha;
        public DateTime Fecha
        {
            get
            {
                return fecha;
            }
            set
            {
                fecha = value;
                notificarCambioColeccion("Fecha");
            }
        }

        SIE.Services.Info.Enums.TipoServicioEnum tipoServicioID;
        public SIE.Services.Info.Enums.TipoServicioEnum TipoServicioID
        {
            get
            {
                return tipoServicioID;
            }
            set
            {
                tipoServicioID = value;
                notificarCambioColeccion("TipoServicioID");
            }
        }

        int cantidadProgramada;
        public int CantidadProgramada
        {
            get
            {
                return cantidadProgramada;
            }
            set
            {
                cantidadProgramada = value;
                notificarCambioColeccion("CantidadProgramada");
            }
        }

        int formulaIDProgramada;
        public int FormulaIDProgramada
        {
            get
            {
                return formulaIDProgramada;
            }
            set
            {
                formulaIDProgramada = value;
                notificarCambioColeccion("FormulaIDProgramada");
            }
        }

        int cantidadServida;
        public int CantidadServida
        {
            get
            {
                return cantidadServida;
            }
            set
            {
                cantidadServida = value;
                notificarCambioColeccion("CantidadServida");
            }
        }

        int formulaIDServida;
        public int FormulaIDServida
        {
            get
            {
                return formulaIDServida;
            }
            set
            {
                formulaIDServida = value;
                notificarCambioColeccion("FormulaIDServida");
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

        int dias;
        public int Dias
        {
            get
            {
                return dias;
            }
            set
            {
                dias = value;
                notificarCambioColeccion("Dias");
            }
        }

    }
}
