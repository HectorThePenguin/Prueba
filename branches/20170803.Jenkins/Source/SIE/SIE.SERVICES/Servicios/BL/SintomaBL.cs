using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class SintomaBL : IDisposable
    {
        SintomaDAL sintomaDAL;

        public SintomaBL()
        {
            sintomaDAL = new SintomaDAL();
        }

        public void Dispose()
        {
            sintomaDAL.Disposed += (s, e) =>
            {
                sintomaDAL = null;
            };
            sintomaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de Sintoma
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SintomaInfo> ObtenerPorPagina(PaginacionInfo pagina, SintomaInfo filtro)
        {
            try
            {
                Logger.Info();
                return sintomaDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Sintoma
        /// </summary>
        /// <returns></returns>
        public IList<SintomaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return sintomaDAL.ObtenerTodos().ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Sintoma filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<SintomaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return sintomaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Sintoma por su Id
        /// </summary>
        /// <param name="sintomaId">Obtiene una entidad Sintoma por su Id</param>
        /// <returns></returns>
        public SintomaInfo ObtenerPorID(int sintomaId)
        {
            try
            {
                Logger.Info();
                return sintomaDAL.ObtenerTodos().FirstOrDefault(e => e.SintomaID == sintomaId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Sintoma por su Id
        /// </summary>
        /// <param name="sintoma">Obtiene una entidad Sintoma por su Id</param>
        /// <returns></returns>
        public SintomaInfo ObtenerPorID(SintomaInfo sintoma)
        {
            try
            {
                Logger.Info();
                return sintomaDAL.ObtenerTodos().FirstOrDefault(e => e.SintomaID == sintoma.SintomaID);
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
        /// Obtiene una entidad de Sintoma por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Sintoma por su descripción</param>
        /// <returns></returns>
        public SintomaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Sintoma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SintomaInfo info)
        {
            try
            {
                Logger.Info();
                return sintomaDAL.Guardar(info);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de sintomas por problema.
        /// </summary>
        /// <param name="problema"></param>
        /// <returns></returns>
        public IList<SintomaInfo> ObtenerPorProblema(int problema)
        {
            IList<SintomaInfo> resultado;
            try
            {
                Logger.Info();
                resultado = sintomaDAL.ObtenerPorProblema(problema);
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
            return resultado;
        }
    }
}
