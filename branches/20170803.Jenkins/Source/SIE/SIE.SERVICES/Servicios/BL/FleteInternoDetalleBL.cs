using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class FleteInternoDetalleBL
    {
        /// <summary>
        /// Crea un flete interno detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoDetalleDal = new FleteInternoDetalleDAL();
                int result = fleteInternoDetalleDal.Crear(fleteInternoDetalleInfo);
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
        internal void Actualizar(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoDetalleDal = new FleteInternoDetalleDAL();
                fleteInternoDetalleDal.Actualizar(fleteInternoDetalleInfo);
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
        /// Obtiene un listado de flete interno detalle por flete interno id
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoDetalleInfo> ObtenerPorFleteInternoId(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var fleteInternoDetalleDal = new FleteInternoDetalleDAL();
                List<FleteInternoDetalleInfo> result = fleteInternoDetalleDal.ObtenerPorFleteInternoId(fleteInternoInfo);
                if (result != null)
                {
                    foreach (var fleteInternoDetalle in result)
                    {
                        //Obtener costos de detalle
                        if (fleteInternoDetalle.FleteInternoDetalleId > 0)
                        {
                            var fleteInternoCostoBl = new FleteInternoCostoBL();
                            fleteInternoDetalle.ListadoFleteInternoCosto = fleteInternoCostoBl.ObtenerPorFleteInternoDetalleId(fleteInternoDetalle);
                        }
                    }
                }
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
