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
    public class RotomixPL
    {
        /// <summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        public RotoMixInfo ObtenerRotoMixXOrganizacionYDescripcion(int organizacionId,string Descripcion)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                RotoMixInfo result = rotomixBL.ObtenerRotoMixXOrganizacionYDescripcion(organizacionId, Descripcion);
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
        /// Metodo para Guardar/Modificar una entidad Rotomix
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(RotoMixInfo info)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                int result = rotomixBL.Guardar(info);
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
        public ResultadoInfo<RotoMixInfo> ObtenerPorPagina(PaginacionInfo pagina, RotoMixInfo filtro)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                ResultadoInfo<RotoMixInfo> result = rotomixBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<RotoMixInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                IList<RotoMixInfo> result = rotomixBL.ObtenerTodos();
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
        public IList<RotoMixInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                IList<RotoMixInfo> result = rotomixBL.ObtenerTodos(estatus);
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
        /// <param name="rotomixID"></param>
        /// <returns></returns>
        public RotoMixInfo ObtenerPorID(int rotomixID)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                RotoMixInfo result = rotomixBL.ObtenerPorID(rotomixID);
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
        public RotoMixInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var rotomixBL = new RotomixBL();
                RotoMixInfo result = rotomixBL.ObtenerPorDescripcion(descripcion);
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
