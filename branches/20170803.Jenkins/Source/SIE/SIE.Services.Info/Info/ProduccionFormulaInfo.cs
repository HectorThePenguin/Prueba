using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ProduccionFormulaInfo : INotifyPropertyChanged
    {
        private int folioFormula;
        private string descripcionFormula;
        private long folioMovimiento;

        /// <summary>
        /// Identificador del registro de produccion formula
        /// </summary>
        [Atributos.AtributoInicializaPropiedad]
        public int ProduccionFormulaId { get; set; }

        /// <summary>
        /// Organizacion al que pertenece el registro
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Formula del registro de produccion formula
        /// </summary>
        public FormulaInfo Formula { get; set; }

        /// <summary>
        /// Cantidad producida de la formula
        /// </summary>
        public decimal CantidadProducida { get; set; }

        /// <summary>
        /// Fecha de produccion de la formula
        /// </summary>
        public DateTime FechaProduccion { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha en que se genero el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha en que se modifico el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Lista del detalle de la produccion de formula
        /// </summary>
        public List<ProduccionFormulaDetalleInfo> ProduccionFormulaDetalle { get; set; }

        /// <summary>
        /// Indica el Almacen al que se afectan los productos para la poliza de produccion de alimento
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Indica el ID del Almacen movimiento de entrada
        /// </summary>
        public long AlmacenMovimientoEntradaID { get; set; }

        /// <summary>
        /// Indica el ID del Almacen movimiento de salida
        /// </summary>
        public long AlmacenMovimientoSalidaID { get; set; }

        /// <summary>
        /// Indica el Folio de la produccion de formula
        /// </summary>
        public int FolioFormula
        {
            get { return folioFormula; }
            set
            {
                folioFormula = value;
                NotifyPropertyChanged("FolioFormula");
            }
        }

        /// <summary>
        /// Indica la descripcion de la formula producida
        /// </summary>
        public string DescripcionFormula
        {
            get { return descripcionFormula; }
            set
            {
                descripcionFormula = value;
                NotifyPropertyChanged("DescripcionFormula");
            }
        }

        /// <summary>
        /// Indica el mensaje que regresa la poliza si ocurre algun error
        /// </summary>
        public string MensajePolizas { get; set; }

        /// <summary>
        /// Indica el importe total de la formula
        /// </summary>
        public decimal ImporteFormula { get; set; }
        /// <summary>
        /// Indica en que RotoMix se llevó a cabo la mezcla
        /// </summary>
        public int RotoMixID { get; set; }
        /// <summary>
        /// Indica el número de batch en cual se llevó a cabo la mezcla
        /// </summary>
        public int Batch { get; set; }
        /// <summary>
        /// Hace referencia a la cantidad servida. Tomar este dato sumando las cantidad servida de la formula, TB RepartoDetalle campo CantidadServida.
        /// </summary>
        public decimal CantidadReparto { get; set; }

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

        /// <summary>
        /// Lista del detalle de la produccion de formula
        /// </summary>
        public List<ProduccionFormulaBatchInfo> ProduccionFormulaBatch { get; set; }

        /// <summary>
        /// Indica el Folio del movimiento generado
        /// </summary>
        public long FolioMovimiento
        {
            get { return folioMovimiento; }
            set
            {
                folioMovimiento = value;
                NotifyPropertyChanged("FolioMovimiento");
            }
        }
    }
}
