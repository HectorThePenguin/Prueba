using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class GanadoIntensivoInfo : INotifyPropertyChanged 
    {

        private CorralInfo corral;
        private int totalCabezas;
        private int cabezasAnterior;
        private decimal importe;
        private string observaciones;
        private OrganizacionInfo organizacion;
        private TipoMovimiento tipoMovimiento;
        private TipoFolio tipoFolio;
        private LoteInfo lote;
        private EntradaGanadoCosteoInfo entradaGanadoCosteo;
        private EntradaGanadoInfo entradaGanado;
        private string cabezasText;
        private int folioTicket;
        private decimal pesoBruto;

        public string CabezasText
        {
            get { return cabezasText; }
            set
            {
                cabezasText = value;
                NotifyPropertyChanged("CabezasText");
            }
        }

        /// <summary>
        /// Folio Ticket
        /// </summary>
        public int FolioTicket
        {
            get { return folioTicket; }
            set
            {
                folioTicket = value;
                NotifyPropertyChanged("FolioTicket");
            }
        }

        /// <summary>
        /// Peso Bruto
        /// </summary>
        public decimal PesoBruto
        {
            get { return pesoBruto; }
            set
            {
                pesoBruto = value;
                NotifyPropertyChanged("PesoBruto");
            }
        }

        /// <summary>
        /// Corral
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
        /// Total Cabezas
        /// </summary>
        public int TotalCabezas
        {
            get { return totalCabezas; }
            set
            {
                totalCabezas = value;
                NotifyPropertyChanged("TotalCabezas");
            }
        }


        /// <summary>
        /// Folio
        /// </summary>
        public TipoFolio TipoFolio
        {
            get { return tipoFolio; }
            set
            {
                tipoFolio = value;
                NotifyPropertyChanged("TipoFolio");
            }
        }


        /// <summary>
        /// Cabezas Anterior
        /// </summary>
        public int CabezasAnterior
        {
            get { return cabezasAnterior;  }
            set
            {
                cabezasAnterior = value;
                NotifyPropertyChanged("CabezasAnterior");
            }
        }

        /// <summary>
        /// Cabezas
        /// </summary>
        public int Cabezas
        {
            get
            {
                int val;
                if (int.TryParse(CabezasText, out val))
                {
                    return Convert.ToInt32(CabezasText);
                }

                return 0;
            }
        }

        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe
        {
            get { return importe; }
            set
            {
                importe = value;
                NotifyPropertyChanged("Importe");
            }
        }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones
        {
            get { return observaciones; }
            set
            {
                observaciones = value;
                NotifyPropertyChanged("Observaciones");
            }
        }

        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Tipo Movimiento ID
        /// </summary>
        public TipoMovimiento TipoMovimientoID
        {
            get { return tipoMovimiento; }
            set
            {
                tipoMovimiento = value;
                NotifyPropertyChanged("TipoMovimiento");
            }
        }

        /// <summary>
        /// Lote
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
        /// Entrada Ganado Costeo
        /// </summary>
        public EntradaGanadoCosteoInfo EntradaGanadoCosteo
        {
            get { return entradaGanadoCosteo; }
            set
            {
                entradaGanadoCosteo = value;
                NotifyPropertyChanged("EntradaGandoCosteo");
            }
        }

        /// <summary>
        /// Entrada ganado 
        /// </summary>
        public EntradaGanadoInfo EntradaGanado
        {
            get { return entradaGanado; }
            set
            {
                entradaGanado = value;
                NotifyPropertyChanged("EntradaGanado");
            }
        }

        /// <summary>
        /// Lista ganado intensivo costo
        /// </summary>
        public List<GanadoIntensivoCostoInfo> ListaGanadoIntensivoCosto { get; set; }

        /// <summary>
        ///      Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        ///     Usario que creo el registro.
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        ///     Usario que modifica el registro .
        /// </summary>
        public int? UsuarioModificacionID { get; set; }


        public MemoryStream stream { get; set; }

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
