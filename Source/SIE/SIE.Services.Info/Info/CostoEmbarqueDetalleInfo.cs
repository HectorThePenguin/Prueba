namespace SIE.Services.Info.Info
{
    public class CostoEmbarqueDetalleInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador del Costo Embarque Detalle
        /// </summary>
        public int CostoEmbarqueDetalleID { set; get; }

        /// <summary>
        ///     Identificador de Programacion Embarque Detalle del Costo Embarque Detalle
        /// </summary>
        public int EmbarqueDetalleID { set; get; }

        /// <summary>
        ///     Costo del Costo Embarque Detalle
        /// </summary>
        public CostoInfo Costo { set; get; }

        /// <summary>
        ///     Renglon equivalente al Orden de la Escala
        /// </summary>
        public int Orden { set; get; }

        /// <summary>
        ///     Importe del Costo Embarque Detalle
        /// </summary>
        public decimal Importe { set; get; }
    }
}
