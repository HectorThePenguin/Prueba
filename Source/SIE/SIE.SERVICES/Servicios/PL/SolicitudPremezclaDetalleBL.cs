
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.PL
{
    public class SolicitudPremezclaDetalleBL
    {
        /// <summary>
        /// Guarda el detalle de una solicitud
        /// </summary>
        /// <param name="listaSolicitudPremezclaDetalle"></param>
        internal void Guardar(List<SolicitudPremezclaDetalleInfo> listaSolicitudPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                var solicitudPremezclaDetalleDal = new SolicitudPremezclaDetalleDAL();
                solicitudPremezclaDetalleDal.Guardar(listaSolicitudPremezclaDetalle);
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
