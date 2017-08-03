using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class EntradaDetalleInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private decimal importe;
        private decimal precioKilo;
        /// <summary>
        /// Identificador de la Tabla
        /// </summary>
        public int EntradaDetalleID { set; get; }
        /// <summary>
        /// Identificador de la Tabla Entrada Ganado
        /// </summary>
        public int EntradaGanadoCosteoID { set; get; }
        /// <summary>
        /// Identifica el Tipo de Ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado { set; get; }
        /// <summary>
        /// Propiedad para el Número de Cabezas
        /// </summary>
        public int Cabezas { set; get; }
        /// <summary>
        /// Peso de Origen del Ganado por Tipo de Ganado
        /// </summary>
        public decimal PesoOrigen { set; get; }
        /// <summary>
        /// Peso de Llegada del Ganado por Tipo de Ganado
        /// </summary>
        public decimal PesoLlegada { set; get; }

        /// <summary>
        /// Precio por Kilo del Ganado por Tipo de Ganado
        /// </summary>
        public decimal PrecioKilo
        {
            get { return precioKilo; }
            set
            {
                if (value != precioKilo)
                {
                    precioKilo = value;
                    NotifyPropertyChanged("PrecioKilo");
                }
            }
        }

        /// <summary>
        /// Importe Total del Tipo de Ganado (Cabezas * PrecioKilo)
        /// </summary>
        public decimal Importe
        {
            get { return importe; }
            set
            {
                if (value != importe)
                {
                    importe = value;
                    NotifyPropertyChanged("Importe");
                }
            }
        }

        /// <summary>
        /// Importe del origen
        /// </summary>
        public decimal ImporteOrigen { get; set; }

        /// <summary>
        /// Lista para poder Bindear el Combo Tipo de Ganado, dentro de un Grid, en la funcionalidad Costeo Entrada Ganado
        /// </summary>
        public List<TipoGanadoInfo> ListaTiposGanado { set; get; }

        /// <summary>
        /// Indica si el Registro se puede editar desde el Grid de la funcionalidad Entrada Ganado Costeo
        /// </summary>
        public bool Editar { set; get; }

        /// <summary>
        /// Peso de Llegada del Ganado por Tipo de Ganado
        /// </summary>
        public DateTime FechaSalidaInterface { set; get; }

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
 