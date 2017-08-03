using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Servicios.BL
{
    internal class PremezclaDetalleBL
    {
        /// <summary>
        /// Metodo que guarda premezclas detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(List<PremezclaDetalleInfo> listaPremezclaDetalle, PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var premezclaDetalleDal = new PremezclaDetalleDAL();
                int result = premezclaDetalleDal.Crear(listaPremezclaDetalle, premezclaInfo);
                return result;
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

        /// <summary>
        /// Actualizar premezcla detalle
        /// </summary>
        /// <param name="listaPremezclaDetalle"></param>
        /// <returns></returns>
        internal void Actualizar(List<PremezclaDetalleInfo> listaPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                var premezclaDetalleDal = new PremezclaDetalleDAL();
                premezclaDetalleDal.Actualizar(listaPremezclaDetalle);
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

        /// <summary>
        /// Obtiene un listado de premezcla detalle por premezclaid
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal List<PremezclaDetalleInfo> ObtenerPremezclaDetallePorPremezclaId(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var premezclaDetalleDal = new PremezclaDetalleDAL();
                List<PremezclaDetalleInfo> result = premezclaDetalleDal.ObtenerPremezclaDetallePorPremezclaId(premezclaInfo);
                return result;
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
