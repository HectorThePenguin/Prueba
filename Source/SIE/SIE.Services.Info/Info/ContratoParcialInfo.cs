using System;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ContratoParcialInfo
    {
        private bool _seleccionado;
        /// <summary>
        /// ID del registro
        /// </summary>
        public int ContratoParcialId { set; get; }

        /// <summary>
        /// Contrato id al que pertenece
        /// </summary>
        public int ContratoId { set; get; }

        /// <summary>
        /// Cantidad de la compra parcial
        /// </summary>
        public decimal Cantidad { set; get; }

        /// <summary>
        /// Importe de la compra parcial
        /// </summary>
        public decimal Importe { set; get; }

        /// <summary>
        /// Importe con el tipo de cambio
        /// </summary>
        public decimal ImporteConvertido { get; set; }

        /// <summary>
        /// Estatus del contrato parcial
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha creacion del contrato parcial
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario que creo el contrato parcial
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Tipo cambio guardado para el contrato parcial
        /// </summary>
        public TipoCambioInfo TipoCambio { get; set; }
        /// <summary>
        /// Usuario que modifica el contrato parcial
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Indica si el contrato parcial fue guardado
        /// </summary>
        public bool Guardado { get; set; }

        /// <summary>
        /// Entidad de contrato
        /// </summary>
        public ContratoInfo Contrato { get; set; }

        /// <summary>
        /// Indica si el contrato parcial fue seleccionado para darle entrada
        /// </summary>
        public bool Seleccionado {
            get { return _seleccionado; }
            set
            {
                _seleccionado = value;
                if (!_seleccionado)
                {
                    CantidadEntrante = 0;
                    NotifyPropertyChanged("CantidadEntrante");
                }
            } }

        /// <summary>
        /// Cantidad faltante para terminar la parcialidad
        /// </summary>
        public decimal CantidadRestante { get; set; }

        /// <summary>
        /// Cantidad que se dara entrada en la parcialidad
        /// </summary>
        public decimal CantidadEntrante { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
