using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class CorralDetectorBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CorralDetector
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CorralDetectorInfo info)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();

                List<CorralInfo> corralesMarcados = (from corral in info.Corrales
                    .Where(corral => !(corral.Elemento.CorralDetectorID == 0 && !corral.Marcado))
                        select new CorralInfo
                        {
                            CorralID = corral.Elemento.CorralID,
                            CorralDetectorID = corral.Elemento.CorralDetectorID,
                            Activo = corral.Marcado ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                        }).ToList();
                int result = 0;
                if (corralesMarcados != null && corralesMarcados.Count>0)
                {
                    result = corralDetectorDAL.Crear(info,corralesMarcados);
                }
                

                /*
                if (info.CorralDetectorID == 0)
                {
                    result = corralDetectorDAL.Crear(info);
                }
                else
                {
                    corralDetectorDAL.Actualizar(info);
                }*/
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
        internal ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralDetectorInfo filtro)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                ResultadoInfo<OperadorInfo> result = corralDetectorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CorralDetector
        /// </summary>
        /// <returns></returns>
        internal IList<CorralDetectorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                IList<CorralDetectorInfo> result = corralDetectorDAL.ObtenerTodos();
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
        internal IList<CorralDetectorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                IList<CorralDetectorInfo> result = corralDetectorDAL.ObtenerTodos(estatus);
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
        /// Obtiene una lista filtrando por el detector
        /// </summary>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerTodosPorDetector(int detectorID)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                CorralDetectorInfo result = corralDetectorDAL.ObtenerTodosPorDetector(detectorID);
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
        /// Obtiene una entidad CorralDetector por su Id
        /// </summary>
        /// <param name="corralDetectorID">Obtiene una entidad CorralDetector por su Id</param>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerPorID(int corralDetectorID)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                CorralDetectorInfo result = corralDetectorDAL.ObtenerPorID(corralDetectorID);
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
        /// Obtiene una entidad CorralDetector por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                CorralDetectorInfo result = corralDetectorDAL.ObtenerPorDescripcion(descripcion);
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
        internal CorralDetectorInfo ObtenerPorOperadorCorral(int operadorID, int corralID)
        {
            try
            {
                Logger.Info();
                var corralDetectorDAL = new CorralDetectorDAL();
                CorralDetectorInfo result = corralDetectorDAL.ObtenerPorOperadorCorral(operadorID, corralID);
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

