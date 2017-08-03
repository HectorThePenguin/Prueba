using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaTraspasoDetalleInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int interfaceSalidaTraspasoDetalleID;
        private int interfaceSalidaTraspasoID;
        private LoteInfo lote;
        private CorralInfo corral;
        private TipoGanadoInfo tipoGanado;
        private int pesoProyectado;
        private decimal gananciaDiaria;
        private int diasEngorda;
        private FormulaInfo formula;
        private int diasFormula;
        private int cabezas;
        private bool registrado;
        private OrganizacionInfo organizacionDestino;
        private bool traspasoGanado;
        private bool sacrificioGanado;
        private int cabezasSacrificadas;

        /// <summary>
        /// Id de la tabla
        /// </summary>
        public int InterfaceSalidaTraspasoDetalleID
        {
            get { return interfaceSalidaTraspasoDetalleID; }
            set
            {
                interfaceSalidaTraspasoDetalleID = value;
                NotifyPropertyChanged("InterfaceSalidaTraspasoDetalleID");
            }
        }
        /// <summary>
        /// Id de la tabla InterfaceSalidaTraspaso
        /// </summary>
        public int InterfaceSalidaTraspasoID
        {
            get { return interfaceSalidaTraspasoID; }
            set
            {
                interfaceSalidaTraspasoID = value;
                NotifyPropertyChanged("InterfaceSalidaTraspasoID");
            }
        }
        /// <summary>
        /// Lote del Envio
        /// </summary>
        public LoteInfo Lote
        {
            get { return lote; }
            set
            {
                lote = value;
                NotifyPropertyChanged("Lote");
            }
        }
        /// <summary>
        /// Corral del Envio
        /// </summary>
        public CorralInfo Corral
        {
            get { return corral; }
            set
            {
                corral = value;
                NotifyPropertyChanged("Corral");
            }
        }
        /// <summary>
        /// Tipo de Ganado a Enviar
        /// </summary>
        public TipoGanadoInfo TipoGanado
        {
            get { return tipoGanado; }
            set
            {
                tipoGanado = value;
                NotifyPropertyChanged("TipoGanado");
            }
        }
        /// <summary>
        /// Peso Proyectado del Lote
        /// </summary>
        public int PesoProyectado
        {
            get { return pesoProyectado; }
            set
            {
                pesoProyectado = value;
                NotifyPropertyChanged("PesoProyectado");
            }
        }
        /// <summary>
        /// Ganancia diaria del Lote
        /// </summary>
        public decimal GananciaDiaria
        {
            get { return gananciaDiaria; }
            set
            {
                gananciaDiaria = value;
                NotifyPropertyChanged("GananciaDiaria");
            }
        }
        /// <summary>
        /// Dias Engorda del Lote
        /// </summary>
        public int DiasEngorda
        {
            get { return diasEngorda; }
            set
            {
                diasEngorda = value;
                NotifyPropertyChanged("DiasEngorda");
            }
        }
        /// <summary>
        /// Formula en la que se encuentra el Lote
        /// </summary>
        public FormulaInfo Formula
        {
            get { return formula; }
            set
            {
                formula = value;
                NotifyPropertyChanged("Formula");
            }
        }
        /// <summary>
        /// Dias que lleva el lote en la formula
        /// </summary>
        public int DiasFormula
        {
            get { return diasFormula; }
            set
            {
                diasFormula = value;
                NotifyPropertyChanged("DiasFormula");
            }
        }
        /// <summary>
        /// Cabezas a Traspasar
        /// </summary>
        public int Cabezas
        {
            get { return cabezas; }
            set
            {
                cabezas = value;
                NotifyPropertyChanged("Cabezas");
            }
        }

        /// <summary>
        /// Indica si ya fue registrado
        /// </summary>
        public bool Registrado
        {
            get { return registrado; }
            set
            {
                registrado = value;
                NotifyPropertyChanged("Registrado");
            }
        }

        /// <summary>
        /// Organizacion Seleccionada
        /// </summary>
        public OrganizacionInfo OrganizacionDestino
        {
            get { return organizacionDestino; }
            set
            {
                organizacionDestino = value;
                NotifyPropertyChanged("OrganizacionDestino");
            }
        }

        /// <summary>
        /// Indica si es traspaso de ganado
        /// </summary>
        public bool TraspasoGanado
        {
            get { return traspasoGanado; }
            set
            {
                traspasoGanado = value;
                NotifyPropertyChanged("TraspasoGanado");
            }
        }

        /// <summary>
        /// Indica si es sacrificio de ganado
        /// </summary>
        public bool SacrificioGanado
        {
            get { return sacrificioGanado; }
            set
            {
                sacrificioGanado = value;
                NotifyPropertyChanged("SacrificioGanado");
            }
        }

        /// <summary>
        /// Cabezas que se han sacrificado
        /// </summary>
        public int CabezasSacrificadas
        {
            get { return cabezasSacrificadas; }
            set
            {
                cabezasSacrificadas = value;
                NotifyPropertyChanged("CabezasSacrificadas");
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
