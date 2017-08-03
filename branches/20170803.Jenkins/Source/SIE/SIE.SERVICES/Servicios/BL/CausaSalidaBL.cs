using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class CausaSalidaBL
    {
        internal CausaSalidaInfo ObtenerPorTipoMovimiento(int tipoMovimientoEnum)
        {
            try
            {
                Logger.Info();
                var causaSalida = new CausaSalidaDAL();
                CausaSalidaInfo resultadoCorral = causaSalida.ObtenerPorTipoMovimiento(tipoMovimientoEnum);
                return resultadoCorral;
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

        internal List<CausaSalidaInfo> ObtenerPorTipoMovimientoLista(int tipoMovimientoEnum)
        {
            try
            {
                Logger.Info();
                var causaSalida = new CausaSalidaDAL();
                List<CausaSalidaInfo> resultadoCorral = causaSalida.ObtenerPorTipoMovimientoLista(tipoMovimientoEnum);
                return resultadoCorral;
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
        /// Metodo para Guardar/Modificar una entidad CausaSalida
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CausaSalidaInfo info)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                int result = info.CausaSalidaID;
                if (info.CausaSalidaID == 0)
                {
                    result = causaSalidaDAL.Crear(info);
                }
                else
                {
                    causaSalidaDAL.Actualizar(info);
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
        internal ResultadoInfo<CausaSalidaInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaSalidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                ResultadoInfo<CausaSalidaInfo> result = causaSalidaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CausaSalida
        /// </summary>
        /// <returns></returns>
        internal IList<CausaSalidaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                IList<CausaSalidaInfo> result = causaSalidaDAL.ObtenerTodos();
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
        internal IList<CausaSalidaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                IList<CausaSalidaInfo> result = causaSalidaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CausaSalida por su Id
        /// </summary>
        /// <param name="causaSalidaID">Obtiene una entidad CausaSalida por su Id</param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorID(int causaSalidaID)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                CausaSalidaInfo result = causaSalidaDAL.ObtenerPorID(causaSalidaID);
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
        /// Obtiene una entidad CausaSalida por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                CausaSalidaInfo result = causaSalidaDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una Causa Salida por su Descripción y Tipo de Movimiento
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorTipoMovimientoDescripcion(CausaSalidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaSalidaDAL = new CausaSalidaDAL();
                CausaSalidaInfo result = causaSalidaDAL.ObtenerPorTipoMovimientoDescripcion(filtro);
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
