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
    public class TipoEmbarquePL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoEmbarque
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoEmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                int result = tipoEmbarqueBL.Guardar(info);
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
        public ResultadoInfo<TipoEmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                ResultadoInfo<TipoEmbarqueInfo> result = tipoEmbarqueBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoEmbarqueInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                IList<TipoEmbarqueInfo> result = tipoEmbarqueBL.ObtenerTodos();
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
        public IList<TipoEmbarqueInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                IList<TipoEmbarqueInfo> result = tipoEmbarqueBL.ObtenerTodos(estatus);
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
        /// <param name="tipoEmbarqueID"></param>
        /// <returns></returns>
        public TipoEmbarqueInfo ObtenerPorID(int tipoEmbarqueID)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                TipoEmbarqueInfo result = tipoEmbarqueBL.ObtenerPorID(tipoEmbarqueID);
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
        public TipoEmbarqueInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoEmbarqueBL = new TipoEmbarqueBL();
                TipoEmbarqueInfo result = tipoEmbarqueBL.ObtenerPorDescripcion(descripcion);
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

