using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ConciliacionMovimientosSIAPModel : INotifyPropertyChanged
    {
        private string ruta;
        private bool ganado;
        private bool materiaPrima;
        private bool almacen;
        private ObservableCollection<PolizaInfo> polizas;
        private ObservableCollection<ConciliacionPolizasMovimientosModel> polizasMovimientos;
        
        /// <summary>
        /// Ruta del archivo a conciliar
        /// </summary>
        public string Ruta
        {
            get { return ruta; }
            set
            {
                ruta = value;
                NotifyPropertyChanged("Ruta");
            }
        }

        /// <summary>
        /// Obtiene las polizas a regenerar
        /// </summary>
        public ObservableCollection<PolizaInfo> Polizas
        {
            get { return polizas; }
            set
            {
                polizas = value;
                NotifyPropertyChanged("Polizas");
                NotifyPropertyChanged("Guardar");
            }
        }

        /// <summary>
        /// Obtiene las polizas a regenerar
        /// </summary>
        public ObservableCollection<ConciliacionPolizasMovimientosModel> PolizasMovimientos
        {
            get { return polizasMovimientos; }
            set
            {
                polizasMovimientos = value;
                NotifyPropertyChanged("PolizasMovimientos");
            }
        }

        /// <summary>
        /// Lista con los pases a proceso generados
        /// </summary>
        public List<PolizaPaseProcesoModel> PasesProceso { get; set; }

        /// <summary>
        /// Lista con los pases a proceso generados
        /// </summary>
        public List<ContenedorEntradaMateriaPrimaInfo> ContenedorEntradasMateriasPrima { get; set; }

        /// <summary>
        /// Lista de Movimientos de Almacen
        /// </summary>
        public ConciliacionMovimientosAlmacenModel AlmacenesMovimiento { get; set; }

        /// <summary>
        /// Lista con los movimientos de consumo de alimento
        /// </summary>
        public List<PolizaConsumoAlimentoModel> ConsumosAlimento { get; set; }

        /// <summary>
        /// Lista con los movimientos de consumo de producto
        /// </summary>
        public List<ContenedorAlmacenMovimientoCierreDia> ConsumosProducto { get; set; }

        /// <summary>
        /// Lista de otros costos
        /// </summary>
        public List<PolizaContratoModel> PolizasContrato { get; set; }

        /// <summary>
        /// Lista de recepcion de productos en almacen
        /// </summary>
        public List<RecepcionProductoInfo> RecepcionProductos { get; set; }

        /// <summary>
        /// Obtiene una lista de Distribucion de Ingredientes
        /// </summary>
        public List<DistribucionDeIngredientesInfo> DistribucionIngredientes { get; set; }

        /// <summary>
        /// Son todas las polizas obtenidas en la consulta
        /// </summary>
        public List<PolizaInfo> PolizasCompletas { get; set; }

        /// <summary>
        /// Lista de Solicitudes de producto
        /// </summary>
        public List<SolicitudProductoInfo> SolicitudProductos { get; set; }

        /// <summary>
        /// Obtiene una lista de entradas por ajuste
        /// </summary>
        public List<PolizaEntradaSalidaPorAjusteModel> EntradasPorAjuste { get; set; }

        /// <summary>
        /// Obtiene una lista de entradas por ajuste
        /// </summary>
        public List<PolizaEntradaSalidaPorAjusteModel> SalidasPorAjuste { get; set; }

        /// <summary>
        /// Obtiene una lista de entradas de materia prima
        /// </summary>
        public List<ContenedorEntradaMateriaPrimaInfo> SubProductos { get; set; }

        /// <summary>
        /// Obtiene una coleccion de produccion de formula
        /// </summary>
        public List<ProduccionFormulaInfo> ProduccionesFormula { get; set; }

        /// <summary>
        /// Obtiene una lista de las salidas de venta traspaso de producto
        /// </summary>
        public List<SalidaProductoInfo> SalidasVentaProductos { get; set; }

        /// <summary>
        /// Obtiene una lista de gastos de materia prima
        /// </summary>
        public List<GastoMateriaPrimaInfo> GastosMateriaPrima { get; set; }

        /// <summary>
        /// Obtiene una lista de las ventas de ganado
        /// </summary>
        public List<ContenedorVentaGanado> VentasGanado{ get; set; }

        /// <summary>
        /// Obtiene una lista de costos
        /// </summary>
        public List<AnimalCostoInfo> AnimalesCosto { get; set; }

        /// <summary>
        /// Obtiene los datos de los sacrificios
        /// </summary>
        public List<PolizaSacrificioModel> LotesSacrificio { get; set; }

        /// <summary>
        /// Obtiene los datos de las entradas de ganado
        /// </summary>
        public List<ContenedorCosteoEntradaGanadoInfo> EntradasGanado { get; set; }

        /// <summary>
        /// Obtiene una poliza de gastos al inventario
        /// </summary>
        public List<GastoInventarioInfo> GastosInventario { get; set; }

        /// <summary>
        /// Obtiene los traspasos de ganado gordo
        /// </summary>
        public List<InterfaceSalidaTraspasoInfo> InterfaceSalidasTraspasos { get; set; }

        /// <summary>
        /// Indica si se conciliara Ganado
        /// </summary>
        public bool Ganado
        {
            get { return ganado; }
            set
            {
                ganado = value;
                NotifyPropertyChanged("Ganado");
            }
        }

        /// <summary>
        /// Indica si se conciliara Materia Prima
        /// </summary>
        public bool MateriaPrima
        {
            get { return materiaPrima; }
            set
            {
                materiaPrima = value;
                NotifyPropertyChanged("MateriaPrima");
            }
        }

        /// <summary>
        /// Indica si se conciliara Almacen
        /// </summary>
        public bool Almacen
        {
            get { return almacen; }
            set
            {
                almacen = value;
                NotifyPropertyChanged("Almacen");
            }
        }

        /// <summary>
        /// Obtiene el tipo de cuenta a buscar
        /// </summary>
        public int TipoCuenta
        {
            get
            {
                var tipo = 0;
                if (ganado)
                {
                    tipo = 1;
                }
                if (almacen)
                {
                    tipo = 3;
                }
                if (materiaPrima)
                {
                    tipo = 2;
                }
                return tipo;
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
