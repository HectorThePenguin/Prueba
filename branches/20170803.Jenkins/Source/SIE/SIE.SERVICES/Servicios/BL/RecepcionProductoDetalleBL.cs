
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class RecepcionProductoDetalleBL
    {
        /// <summary>
        /// Guarda el detalle de una recepcion
        /// </summary>
        /// <param name="listaRecepcionProductoDetalle"></param>
        internal void Guardar(List<RecepcionProductoDetalleInfo> listaRecepcionProductoDetalle)
        {
            try
            {
                Logger.Info();
                var recepcionProductoDetalleDal = new RecepcionProductoDetalleDAL();
                recepcionProductoDetalleDal.Guardar(listaRecepcionProductoDetalle);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
