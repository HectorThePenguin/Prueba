using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class MetodoPagoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad MetodoPago
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(MetodoPagoInfo info)
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                int result = info.MetodoPagoID;
                if (info.MetodoPagoID == 0)
                {
                    result = metodoPagoDAL.Crear(info);
                }
                else
                {
                    metodoPagoDAL.Actualizar(info);
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

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<MetodoPagoInfo> ObtenerPorPagina(PaginacionInfo pagina, MetodoPagoInfo filtro)
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                ResultadoInfo<MetodoPagoInfo> result = metodoPagoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de MetodoPago
        /// </summary>
        /// <returns></returns>
        public IList<MetodoPagoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                IList<MetodoPagoInfo> result = metodoPagoDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<MetodoPagoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                IList<MetodoPagoInfo> result = metodoPagoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad MetodoPago por su Id
        /// </summary>
        /// <param name="metodoPagoID">Obtiene una entidad MetodoPago por su Id</param>
        /// <returns></returns>
        public MetodoPagoInfo ObtenerPorID(int metodoPagoID)
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                MetodoPagoInfo result = metodoPagoDAL.ObtenerPorID(metodoPagoID);
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
        /// Obtiene una entidad MetodoPago por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public MetodoPagoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var metodoPagoDAL = new MetodoPagoDAL();
                MetodoPagoInfo result = metodoPagoDAL.ObtenerPorDescripcion(descripcion);
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

