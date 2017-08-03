using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionLoteDetalleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlimentacionLoteDetalleModel()
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

        int corralID;
        public int CorralID
        {
            get
            {
                return corralID;
            }
            set
            {
                corralID = value;
                notificarCambioColeccion("CorralID");
            }
        }

        int organizacionID;
        public int OrganizacionID
        {
            get
            {
                return organizacionID;
            }
            set
            {
                organizacionID = value;
                notificarCambioColeccion("OrganizacionID");
            }
        }

        string codigo;
        public string Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                codigo = value;
                notificarCambioColeccion("Codigo");
            }
        }

        int cabezas;
        public int Cabezas
        {
            get
            {
                return cabezas;
            }
            set
            {
                cabezas = value;
                notificarCambioColeccion("Cabezas");
            }
        }

        string sexo;
        public string Sexo
        {
            get
            {
                return sexo;
            }
            set
            {
                sexo = value;
                notificarCambioColeccion("Sexo");
            }
        }

        double pesoLote;
        public double PesoLote
        {
            get
            {
                return pesoLote;
            }
            set
            {
                pesoLote = value;
                notificarCambioColeccion("PesoLote");
            }
        }

        int tipoGanadoID;
        public int TipoGanadoID
        {
            get
            {
                return tipoGanadoID;
            }
            set
            {
                tipoGanadoID = value;
                notificarCambioColeccion("TipoGanadoID");
            }
        }

        int diasEngordaEntrada;
        public int DiasEngordaEntrada
        {
            get
            {
                return diasEngordaEntrada;
            }
            set
            {
                diasEngordaEntrada = value;
                notificarCambioColeccion("DiasEngordaEntrada");
            }
        }

        int diasEngordaInicio;
        public int DiasEngordaInicio
        {
            get
            {
                return diasEngordaInicio;
            }
            set
            {
                diasEngordaInicio = value;
                notificarCambioColeccion("DiasEngordaInicio");
            }
        }

        int ganaciaCorral;
        public int GananciaCorral
        {
            get
            {
                return ganaciaCorral;
            }
            set
            {
                ganaciaCorral = value;
                notificarCambioColeccion("GananciaCorral");
            }
        }

        int ultimaFormulaID;
        public int UltimaFormulaID
        {
            get
            {
                return ultimaFormulaID;
            }
            set
            {
                ultimaFormulaID = value;
                notificarCambioColeccion("UltimaFormulaID");
            }
        }

        int diasUltimaFormula;
        public int DiasUltimaFormula
        {
            get
            {
                return diasUltimaFormula;
            }
            set
            {
                diasUltimaFormula = value;
                notificarCambioColeccion("DiasUltimaFormula");
            }
        }
    }
}
