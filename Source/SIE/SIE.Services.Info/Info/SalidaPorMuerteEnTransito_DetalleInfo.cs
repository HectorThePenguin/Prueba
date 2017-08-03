namespace SIE.Services.Info.Info
{
    public class SalidaGanadoEnTransitoDetalleInfo : BitacoraInfo
    {
        #region Atributos

        private int salidaGanadoTransitoDetalleId;
        private int salidaGanadoTransitoId;
        private int costoId;
        private decimal importeCosto;

        #endregion

        #region Propiedades 
        
        /// <summary>
        /// Id del detalle de salida de ganado en transito 
        /// </summary>
        public int SalidaGanadoTransitoDetalleId
        {
            get { return salidaGanadoTransitoDetalleId; }
            set { salidaGanadoTransitoDetalleId = value; }
        }

        /// <summary>
        /// Id de la salida de ganado en transito
        /// </summary>
        public int SalidaGanadoTransitoId
        {
            get { return salidaGanadoTransitoId; }
            set { salidaGanadoTransitoId = value; }
        }

        /// <summary>
        /// Id del costo del detalle de la salida de ganado en transito
        /// </summary>
        public int CostoId
        {
            get { return costoId; }
            set { costoId = value; }
        }

        /// <summary>
        /// Importe del detalle de la salida de ganado en transito
        /// </summary>
        public decimal ImporteCosto
        {
            get { return importeCosto; }
            set { importeCosto = value; }
        }

        #endregion
    }
}
