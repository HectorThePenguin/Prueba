using System.Collections.Generic;
using System.ComponentModel;
using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ReimpresionPolizaModel : INotifyPropertyChanged
    {
        private IList<TipoPolizaInfo> tiposPoliza;
        private VentaGanadoInfo ventaGanado;
        private EntradaGanadoInfo entradaGanado;
        private AnimalInfo animal;
        private AlmacenInfo almacen;
        private ContenedorEntradaMateriaPrimaInfo contenedorEntradaMateriaPrima;
        private DateTime fecha;
        private PedidoInfo pedido;
        private FolioSolicitudInfo solicitudProducto;
        private ProduccionFormulaInfo produccionFormula;
        private SalidaProductoInfo salidaProducto;

        /// <summary>
        /// Tipo de Poliza que se Reimprimira
        /// </summary>
        public IList<TipoPolizaInfo> TiposPoliza
        {
            get { return tiposPoliza; }
            set
            {
                tiposPoliza = value;
                NotifyPropertyChanged("TiposPoliza");
            }
        }

        /// <summary>
        /// Venta de ganado
        /// </summary>
        public VentaGanadoInfo VentaGanado
        {
            get { return ventaGanado; }
            set { ventaGanado = value; NotifyPropertyChanged("VentaGanado"); }
        }

        /// <summary>
        /// Entrada de ganado
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
        /// Animal
        /// </summary>
        public AnimalInfo Animal
        {
            get { return animal; }
            set
            {
                animal = value;
                NotifyPropertyChanged("Animal");
            }
        }

        /// <summary>
        /// Almacen
        /// </summary>
        public AlmacenInfo Almacen
        {
            get { return almacen; }
            set
            {
                almacen = value;
                NotifyPropertyChanged("Almacen");
            }
        }

        public ContenedorEntradaMateriaPrimaInfo ContenedorEntradaMateriaPrima
        {
            get { return contenedorEntradaMateriaPrima; }
            set
            {
                contenedorEntradaMateriaPrima = value;
                NotifyPropertyChanged("ContenedorEntradaMateriaPrima");
            }
        }

        public PedidoInfo Pedido
        {
            get { return pedido; }
            set
            {
                pedido = value;
                NotifyPropertyChanged("Pedido");
            }
        }

        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                NotifyPropertyChanged("Fecha");
            }
        }

        /// <summary>
        /// Entidad de Solicitud Producto
        /// </summary>
        public FolioSolicitudInfo SolicitudProducto
        {
            get { return solicitudProducto; }
            set
            {
                solicitudProducto = value;
                NotifyPropertyChanged("SolicitudProducto");
            }
        }

        /// <summary>
        /// Entidad de la Produccion de Formula
        /// </summary>
        public ProduccionFormulaInfo ProduccionFormula
        {
            get { return produccionFormula; }
            set
            {
                produccionFormula = value;
                NotifyPropertyChanged("ProduccionFormula");
            }
        }

        /// <summary>
        /// Entidad de Salida de Producto
        /// </summary>
        public SalidaProductoInfo SalidaProducto
        {
            get { return salidaProducto; }
            set
            {
                salidaProducto = value;
                NotifyPropertyChanged("SalidaProducto");
            }
        }
        /// <summary>
        /// Tipo de Poliza Seleccionada
        /// </summary>
        public TipoPolizaInfo TipoPoliza { get; set; }

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
