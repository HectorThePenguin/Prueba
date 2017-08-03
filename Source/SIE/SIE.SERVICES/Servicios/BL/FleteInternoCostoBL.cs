using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    class FleteInternoCostoBL
    {
        /// <summary>
        /// Crea un flete interno detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(List<FleteInternoCostoInfo> listaFleteInternoCosto, FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoCostoDal = new FleteInternoCostoDAL();
                int result = fleteInternoCostoDal.Crear(listaFleteInternoCosto, fleteInternoDetalleInfo);
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
        /// Actualizar flete interno detalle
        /// </summary>
        /// <returns></returns>
        internal void Actualizar(List<FleteInternoCostoInfo> listaFleteInternoCosto)
        {
            try
            {
                Logger.Info();
                var fleteInternoDCostoDal = new FleteInternoCostoDAL();
                fleteInternoDCostoDal.Actualizar(listaFleteInternoCosto);
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
        /// Obtener un listado de flete interno costo 
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoCostoInfo> ObtenerPorFleteInternoDetalleId(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoCostoDal = new FleteInternoCostoDAL();
                List<FleteInternoCostoInfo> result = fleteInternoCostoDal.ObtenerPorFleteInternoDetalleId(fleteInternoDetalleInfo);
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
