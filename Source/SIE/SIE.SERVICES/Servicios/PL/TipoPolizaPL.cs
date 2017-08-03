using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoPolizaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoPoliza
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoPolizaInfo info)
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                int result = tipoPolizaBL.Guardar(info);
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
        public ResultadoInfo<TipoPolizaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPolizaInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                ResultadoInfo<TipoPolizaInfo> result = tipoPolizaBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<TipoPolizaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                IList<TipoPolizaInfo> result = tipoPolizaBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TipoPolizaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                IList<TipoPolizaInfo> result = tipoPolizaBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="tipoPolizaID"></param>
        /// <returns></returns>
        public TipoPolizaInfo ObtenerPorID(int tipoPolizaID)
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                TipoPolizaInfo result = tipoPolizaBL.ObtenerPorID(tipoPolizaID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoPolizaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoPolizaBL = new TipoPolizaBL();
                TipoPolizaInfo result = tipoPolizaBL.ObtenerPorDescripcion(descripcion);
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

