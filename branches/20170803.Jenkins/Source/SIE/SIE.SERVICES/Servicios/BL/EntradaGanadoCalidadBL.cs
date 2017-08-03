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
    internal class EntradaGanadoCalidadBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad EntradaGanadoCalidad
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(EntradaGanadoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                int result = info.EntradaGanadoCalidadID;
                if (info.EntradaGanadoCalidadID == 0)
                {
                    result = entradaGanadoCalidadDAL.Crear(info);
                }
                else
                {
                    entradaGanadoCalidadDAL.Actualizar(info);
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
        internal ResultadoInfo<EntradaGanadoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoCalidadInfo filtro)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                ResultadoInfo<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de EntradaGanadoCalidad
        /// </summary>
        /// <returns></returns>
        internal IList<EntradaGanadoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                IList<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadDAL.ObtenerTodos();
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
        internal IList<EntradaGanadoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                IList<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad EntradaGanadoCalidad por su Id
        /// </summary>
        /// <param name="entradaGanadoCalidadID">Obtiene una entidad EntradaGanadoCalidad por su Id</param>
        /// <returns></returns>
        internal EntradaGanadoCalidadInfo ObtenerPorID(int entradaGanadoCalidadID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                EntradaGanadoCalidadInfo result = entradaGanadoCalidadDAL.ObtenerPorID(entradaGanadoCalidadID);
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
        /// Obtiene una entidad EntradaGanadoCalidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal EntradaGanadoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                EntradaGanadoCalidadInfo result = entradaGanadoCalidadDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una lista filtrando por Entrada Ganado Id
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoCalidadInfo> ObtenerPorEntradaGanadoId(int entradaGanadoId)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadDAL = new EntradaGanadoCalidadDAL();
                List<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadDAL.ObtenerPorEntradaGanadoId(entradaGanadoId);
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

