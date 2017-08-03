using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SalidaGanadoEnTransitoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        #region Atributos
        private int salidaGanadoTransitoID;
        private int organizacionID;
        private int folio;
        private int loteID;
        private int numCabezas;
        private bool venta;
        private bool muerte;
        private DateTime fecha;
        private decimal importe;
        private ClienteInfo cliente;
        private int kilos;
        private String serie;
        private int folioFactura;
        private int polizaId;
        private string observaciones;
        private DateTime fechaModificacion;
        private DateTime fechacreacion;
        private List<SalidaGanadoEnTransitoDetalleInfo> detalles;
        private IList<CostoCorralInfo> costos;
        private int entradaGanadoTransito;
        private decimal totalCabezasDuranteMuerte;//calculado en caso de muerte
        private int CorralId;

        #endregion

        #region Constructores

        public SalidaGanadoEnTransitoInfo()
        {
            cliente = new ClienteInfo();
            fecha = DateTime.Today;
            detalles = new List<SalidaGanadoEnTransitoDetalleInfo>();

        }

        #endregion

        #region Propiedades

        /// <summary>
        /// identificador de la salida de ganado en transito
        /// </summary>
        public int SalidaGanadoTransitoID
        {
            get
            {
                return salidaGanadoTransitoID;
            }
            set
            {
                salidaGanadoTransitoID = value;
                foreach (var detalle in detalles)
                {
                    detalle.SalidaGanadoTransitoId = salidaGanadoTransitoID;
                }
                NotifyPropertyChanged("SalidaGanadoTransitoID");
            }
        }

        /// <summary>
        /// Organizacion a la que se registra la salida
        /// </summary>
        public int OrganizacionID
        {
            get
            {
                return organizacionID;
            }
            set
            {
                organizacionID = value;
                NotifyPropertyChanged("OrganizacionID");
            }
        }

        /// <summary>
        /// Folio de salida de ganado en transito por venta
        /// </summary>
        public int Folio
        {
            get
            {
                return folio;
            }
            set
            {
                folio = value;
                NotifyPropertyChanged("Folio");
            }
        }

        /// <summary>
        /// lote al cual se le registra la salida
        /// </summary>
        public int LoteID
        {
            get
            {
                return loteID;
            }
            set
            {
                loteID = value;
                NotifyPropertyChanged("LoteID");
            }
        }

        /// <summary>
        /// Numero de cabezas al que se les registra la salida
        /// </summary>
        public int NumCabezas
        {
            get
            {
                return numCabezas;
            }
            set
            {
                numCabezas = value;
                NotifyPropertyChanged("NumCabezas");
            }
        }

        /// <summary>
        /// Indica si la salida es por venta
        /// </summary>
        public bool Venta
        {
            get
            {
                return venta;
            }
            set
            {
                venta = value;
                NotifyPropertyChanged("Venta");
            }
        }

        /// <summary>
        /// Indica si la salida es por muerte del ganado
        /// </summary>
        public bool Muerte
        {
            get
            {
                return muerte;
            }
            set
            {
                muerte = value;
                NotifyPropertyChanged("Muerte");
            }
        }

        /// <summary>
        /// Fecha de registro de la salida
        /// </summary>
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; NotifyPropertyChanged("Fecha"); }
        }

        /// <summary>
        /// Importe por el que se registra la salida en caso de venta
        /// </summary>
        public decimal Importe
        {
            get { return importe; }
            set { importe = value; NotifyPropertyChanged("Importe"); }
        }

        /// <summary>
        /// Cliente al que se le registra la venta
        /// </summary>
        public ClienteInfo Cliente
        {
            get { return cliente; }
            set { cliente = value; NotifyPropertyChanged("Cliente"); }
        }

        /// <summary>
        /// Kilos que se calcula que se estan dando salida
        /// </summary>
        public int Kilos
        {
            get { return kilos; }
            set { kilos = value; NotifyPropertyChanged("Kilos"); }
        }

        /// <summary>
        /// Serie de la factura en caso de venta
        /// </summary>
        public string Serie
        {
            get { return serie; }
            set { serie = value; NotifyPropertyChanged("Serie"); }
        }

        /// <summary>
        /// Folio de la factura de la salida que se registra en caso de venta
        /// </summary>
        public int FolioFactura
        {
            get { return folioFactura; }
            set { folioFactura = value; NotifyPropertyChanged("FolioFactura"); }
        }

        /// <summary>
        /// Identificador de la poliza que se genera por la salida de ganado
        /// </summary>
        public int PolizaID
        {
            get { return polizaId; }
            set { polizaId = value; NotifyPropertyChanged("PolizaID"); }
        }

        /// <summary>
        /// Observaciones de la salida por venta o muerte
        /// </summary>
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; NotifyPropertyChanged("Observaciones"); }
        }

        /// <summary>
        /// Fecha en la que se modifica la salida
        /// </summary>
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set
            {
                fechaModificacion = value;
                NotifyPropertyChanged("FechaModificacion");
            }
        }

        /// <summary>
        /// Fecha en la que se registra la salida
        /// </summary>
        public DateTime FechaCreacion
        {
            get { return fechacreacion; }
            set
            {
                fechacreacion = value;
                NotifyPropertyChanged("FechaCreacion");
            }
        }

        /// <summary>
        /// Cantidad de cabezas del lote correspondiente antes de realizar la salida de ganado en transito
        /// </summary>
        public decimal TotalCabezasDuranteMuerte
        {
            get { return totalCabezasDuranteMuerte; }
            set { totalCabezasDuranteMuerte = value; }
        }

        /// <summary>
        /// Identificador de la entrada de ganado en transito a la cual le corresponde la salida
        /// </summary>
        public int EntradaGanadoTransitoID
        {
            get { return entradaGanadoTransito; }
            set
            {
                entradaGanadoTransito = value;
                NotifyPropertyChanged("EntradaGanadoTransitoID");
            }
        }

        /// <summary>
        ///Costos del corral al que se realiza la salida
        /// </summary>
        public IList<CostoCorralInfo> Costos
        {
            get { return costos; }
            set
            {
                costos = value;
                NotifyPropertyChanged("Costos");
            }
        }

        /// <summary>
        /// Detalles de la salida de ganado(por cada costo)
        /// </summary>
        public List<SalidaGanadoEnTransitoDetalleInfo> DetallesSalida
        {
            get { return detalles; }
            set { detalles = value; }
        }

        public int CorralID
        {
            get { return CorralId; }
            set { CorralId = value; }
        }
        #endregion

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
