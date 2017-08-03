using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class VentaGanadoInfo : INotifyPropertyChanged
    {
        private int ventaGanadoID;
        private int folioTicket;
        private string nombreCliente;

        public int VentaGanadoID
        {
            get { return ventaGanadoID; }
            set
            {
                ventaGanadoID = value;
                NotifyPropertyChanged("VentaGanadoID");
            }
        }

        public int FolioTicket
        {
            get { return folioTicket; }
            set
            {
                folioTicket = value;
                NotifyPropertyChanged("FolioTicket");
            }
        }

        public string FolioFactura { get; set; }
        public int ClienteID { get; set; }
        public string CodigoSAP { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal PesoTara { get; set; }
        public decimal PesoBruto { get; set; }
        public int LoteID { get; set; }
        public string CodigoCorral { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreacionID { get; set; }
        public int UsuarioModificacionID { get; set; }

        public string NombreCliente
        {
            get { return nombreCliente == null ? string.Empty : nombreCliente; }
            set
            {
                nombreCliente = value;
                NotifyPropertyChanged("NombreCliente");
            }
        }

        public LoteInfo Lote { get; set; }

        /// <summary>
        /// Tipo de Venta
        /// </summary>
        public Enums.TipoVentaEnum TipoVenta { get; set; }

        /// <summary>
        /// Total de cabezas, solo sera usado cuando el tipo de venta sea Externo, debido a que no se capturan aretes sino solo el total
        /// </summary>
        public int TotalCabezas { get; set; }

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
