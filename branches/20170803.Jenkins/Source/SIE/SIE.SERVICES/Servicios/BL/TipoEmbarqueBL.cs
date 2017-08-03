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
    internal class TipoEmbarqueBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoEmbarque
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoEmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                int result = info.TipoEmbarqueID;
                if (info.TipoEmbarqueID == 0)
                {
                    result = tipoEmbarqueDAL.Crear(info);
                }
                else
                {
                    tipoEmbarqueDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoEmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                ResultadoInfo<TipoEmbarqueInfo> result = tipoEmbarqueDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoEmbarque
        /// </summary>
        /// <returns></returns>
        internal IList<TipoEmbarqueInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                IList<TipoEmbarqueInfo> result = tipoEmbarqueDAL.ObtenerTodos();
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
        internal IList<TipoEmbarqueInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                IList<TipoEmbarqueInfo> result = tipoEmbarqueDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoEmbarque por su Id
        /// </summary>
        /// <param name="tipoEmbarqueID">Obtiene una entidad TipoEmbarque por su Id</param>
        /// <returns></returns>
        internal TipoEmbarqueInfo ObtenerPorID(int tipoEmbarqueID)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                TipoEmbarqueInfo result = tipoEmbarqueDAL.ObtenerPorID(tipoEmbarqueID);
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
        /// Obtiene una entidad TipoEmbarque por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoEmbarqueInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueDAL = new TipoEmbarqueDAL();
                TipoEmbarqueInfo result = tipoEmbarqueDAL.ObtenerPorDescripcion(descripcion);
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

