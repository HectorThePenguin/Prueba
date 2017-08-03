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
    internal class TipoPolizaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoPoliza
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoPolizaInfo info)
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                int result = info.TipoPolizaID;
                if (info.TipoPolizaID == 0)
                {
                    result = tipoPolizaDAL.Crear(info);
                }
                else
                {
                    tipoPolizaDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoPolizaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPolizaInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                ResultadoInfo<TipoPolizaInfo> result = tipoPolizaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoPoliza
        /// </summary>
        /// <returns></returns>
        internal IList<TipoPolizaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                IList<TipoPolizaInfo> result = tipoPolizaDAL.ObtenerTodos();
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
        internal IList<TipoPolizaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                IList<TipoPolizaInfo> result = tipoPolizaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoPoliza por su Id
        /// </summary>
        /// <param name="tipoPolizaID">Obtiene una entidad TipoPoliza por su Id</param>
        /// <returns></returns>
        internal TipoPolizaInfo ObtenerPorID(int tipoPolizaID)
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                TipoPolizaInfo result = tipoPolizaDAL.ObtenerPorID(tipoPolizaID);
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
        /// Obtiene una entidad TipoPoliza por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoPolizaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoPolizaDAL = new TipoPolizaDAL();
                TipoPolizaInfo result = tipoPolizaDAL.ObtenerPorDescripcion(descripcion);
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

