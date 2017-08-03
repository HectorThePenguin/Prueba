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
    public class TipoCorralPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoCorral
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoCorralInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                int result = tipoCorralBL.Guardar(info);
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
        public ResultadoInfo<TipoCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                ResultadoInfo<TipoCorralInfo> result = tipoCorralBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                IList<TipoCorralInfo> result = tipoCorralBL.ObtenerTodos();
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
        public IList<TipoCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                IList<TipoCorralInfo> result = tipoCorralBL.ObtenerTodos(estatus);
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
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        public TipoCorralInfo ObtenerPorID(int tipoCorralID)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                TipoCorralInfo result = tipoCorralBL.ObtenerPorID(tipoCorralID);
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
        public TipoCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                TipoCorralInfo result = tipoCorralBL.ObtenerPorDescripcion(descripcion);
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
        /// Valida si el Grupo Corral Pertenece a un Tipo Corral
        /// </summary>
        /// <param name="grupoCorralID"></param>
        /// <returns></returns>
        public bool TieneAsignadoGruposCorral(int grupoCorralID)
        {
            try
            {
                Logger.Info();
                var tipoCorralBL = new TipoCorralBL();
                bool result = tipoCorralBL.TieneAsignadoGruposCorral(grupoCorralID);
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

