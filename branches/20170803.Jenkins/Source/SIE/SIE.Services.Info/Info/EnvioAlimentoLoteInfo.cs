using BLToolkit.Common;
using System.Data;

namespace SIE.Services.Info.Info
{
    public class EnvioAlimentoLoteInfo : BitacoraInfo
    {
        private int _loteID;
        private decimal _cantidad;
        private decimal _precioPromedio;
        private int almacenInventarioLoteID;

        private long _piezas;

        /// <summary>
        /// Identificador del lote
        /// </summary>
        public int LoteID
        {
            get { return _loteID; }
            set { _loteID = value; }
        }
       
        /// <summary>
        /// Cantidad en inventario del lote
        /// </summary>
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
       
        /// <summary>
        /// Precio promedio del articulo
        /// </summary>
        public decimal PrecioPromedio
        {
            get { return _precioPromedio; }
            set { _precioPromedio = value; }
        }



        /// <summary>
        /// Importe del prodcuto 
        /// </summary>
        public decimal Importe {
            get { return this._precioPromedio * this._cantidad; }
        }

        /// <summary>
        /// Id del AlmacenInventarioLote
        /// </summary>
        public int AlmacenInventarioLoteID
        {
            get {
                return almacenInventarioLoteID;     
            }
            set {
                almacenInventarioLoteID = value;
            }
        }
        public long Piezas
        {
            get { return _piezas; }
            set { _piezas = BLToolkit.Common.Convert.ToInt64(_cantidad) / _piezas; }
        }
    }
}
