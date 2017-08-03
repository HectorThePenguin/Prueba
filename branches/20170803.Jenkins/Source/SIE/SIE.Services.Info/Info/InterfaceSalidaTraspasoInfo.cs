using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaTraspasoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private bool traspasoGanado;
        private bool sacrificioGanado;
        private int cabezasEnvio;
        private long folioTraspaso;
        private OrganizacionInfo organizacionDestino;
        private string corral;
        private string lote;
        private int pesoProyectado;
        private decimal gananciaDiaria;
        private int diasEngorda;
        private int diasFormula;
        private int cabezas;
        private DateTime fechaEnvio;
        private bool registrado;
        private List<InterfaceSalidaTraspasoDetalleInfo> listaInterfaceSalidaTraspasoDetalle;
        private int pesoTara;
        private int pesoBruto;
        private int diasZilmax;
        private DateTime fechaEnvioTraspaso;

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int InterfaceSalidaTraspasoId { get; set; }

        /// <summary>
        /// Identificador de la organizacion del usuario
        /// </summary>
        public int OrganizacionId { get; set; }

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
        /// Folio del traspaso
        /// </summary>
        public long FolioTraspaso
        {
            get { return folioTraspaso; }
            set
            {
                folioTraspaso = value;
                NotifyPropertyChanged("FolioTraspaso");
            }
        }

        /// <summary>
        /// Codigo del corral
        /// </summary>
        public string Corral
        {
            get { return corral; }
            set
            {
                corral = value;
                NotifyPropertyChanged("Corral");
            }
        }

        /// <summary>
        /// Codigo del Lote
        /// </summary>
        public string Lote
        {
            get { return lote; }
            set
            {
                lote = value;
                NotifyPropertyChanged("Lote");
            }
        }

        private LoteInfo loteInfo;
        /// <summary>
        /// Codigo del Lote
        /// </summary>
        public LoteInfo LoteInfo
        {
            get { return loteInfo; }
            set
            {
                loteInfo = value;
                NotifyPropertyChanged("LoteInfo");
            }
        }

        private CorralInfo corralInfo;
        /// <summary>
        /// Codigo del Corral
        /// </summary>
        public CorralInfo CorralInfo
        {
            get { return corralInfo; }
            set
            {
                corralInfo = value;
                NotifyPropertyChanged("CorralInfo");
            }
        }

        private TipoGanadoInfo tipoGanado;
        /// <summary>
        /// Tipo de Ganado Seleccionado
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
        /// Peso proyectado
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
        /// Ganancia diaria
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
        /// Dias que pasa en engorda
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

        private FormulaInfo formula;
        /// <summary>
        /// Formula seleccionada
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
        /// Dias que pasa tomando la formula
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
        /// Cabezas
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
        /// Fecha de envio
        /// </summary>
        public DateTime FechaEnvio
        {
            get { return fechaEnvio; }
            set
            {
                fechaEnvio = value;
                NotifyPropertyChanged("FechaEnvio");
            }
        }

        /// <summary>
        /// Fecha de envio
        /// </summary>
        public DateTime FechaEnvioTraspaso
        {
            get { return fechaEnvioTraspaso; }
            set
            {
                fechaEnvioTraspaso = value;
                NotifyPropertyChanged("FechaEnvioTraspaso");
            }
        }

        private Sexo sexo;

        /// <summary>
        /// Sexo del ganado
        /// </summary>
        public Sexo Sexo
        {
            get { return sexo; }
            set
            {
                sexo = value;
                NotifyPropertyChanged("Sexo");
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
        /// Cabezas de envio
        /// </summary>
        public int CabezasEnvio
        {
            get { return cabezasEnvio; }
            set
            {
                cabezasEnvio = value;
                NotifyPropertyChanged("CabezasEnvio");
            }
        }

        private LoteProyeccionInfo loteProyeccion;

        public LoteProyeccionInfo LoteProyecion
        {
            get { return loteProyeccion; }
            set
            {
                loteProyeccion = value;
                NotifyPropertyChanged("LoteProyecion");
            }
        }

        public List<InterfaceSalidaTraspasoDetalleInfo> ListaInterfaceSalidaTraspasoDetalle
        {
            get { return listaInterfaceSalidaTraspasoDetalle; }
            set
            {
                listaInterfaceSalidaTraspasoDetalle = value;
                NotifyPropertyChanged("ListaInterfaceSalidaTraspasoDetalle");
            }
        }

        public int PesoTara
        {
            get { return pesoTara; }
            set
            {
                pesoTara = value;
                NotifyPropertyChanged("PesoTara");
            }
        }

        public int PesoBruto
        {
            get { return pesoBruto; }
            set
            {
                pesoBruto = value;
                NotifyPropertyChanged("PesoBruto");
            }
        }

        public int DiasZilmax
        {
            get { return diasZilmax; }
            set
            {
                diasZilmax = value;
                NotifyPropertyChanged("DiasZilmax");
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
