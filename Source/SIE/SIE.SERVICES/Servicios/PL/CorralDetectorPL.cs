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
    public class CorralDetectorPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CorralDetector
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(CorralDetectorInfo info)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                int result = corralDetectorBL.Guardar(info);
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
        public ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralDetectorInfo filtro)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                ResultadoInfo<OperadorInfo> result = corralDetectorBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<CorralDetectorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                IList<CorralDetectorInfo> result = corralDetectorBL.ObtenerTodos();
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
        public CorralDetectorInfo ObtenerTodosPorDetector(int detectorID)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                CorralDetectorInfo result = corralDetectorBL.ObtenerTodosPorDetector(detectorID);
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
        public IList<CorralDetectorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                IList<CorralDetectorInfo> result = corralDetectorBL.ObtenerTodos(estatus);
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
        /// <param name="corralDetectorID"></param>
        /// <returns></returns>
        public CorralDetectorInfo ObtenerPorID(int corralDetectorID)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                CorralDetectorInfo result = corralDetectorBL.ObtenerPorID(corralDetectorID);
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
        public CorralDetectorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                CorralDetectorInfo result = corralDetectorBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un registro de CorralDetector
        /// </summary>
        /// <param name="operadorID">Clave del Operador</param>
        /// <param name="corralID">Clave del COrral</param>
        /// <returns></returns>
        public CorralDetectorInfo ObtenerPorOperadorCorral(int operadorID, int corralID)
        {
            try
            {
                Logger.Info();
                var corralDetectorBL = new CorralDetectorBL();
                CorralDetectorInfo result = corralDetectorBL.ObtenerPorOperadorCorral(operadorID, corralID);
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
