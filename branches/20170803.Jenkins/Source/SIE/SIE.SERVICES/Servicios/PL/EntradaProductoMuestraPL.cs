
using System;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaProductoMuestraPL 
    {
        /// <summary>
        /// Guarda la actualizacion de las muestras
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        public bool GuardarActualizacionProductos(Info.Info.EntradaProductoDetalleInfo indicadores)
        {
            try
            {
                var entradaProductoMuestraBl = new EntradaProductoMuestraBL();
                return entradaProductoMuestraBl.GuardarActualizacionProductos(indicadores);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
