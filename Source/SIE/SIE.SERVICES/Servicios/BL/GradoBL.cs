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
    internal class GradoBL
    {
        internal IList<GradoInfo> ObtenerGrados()
        {
            IList<GradoInfo> result;
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                result = gradoDAL.ObtenerGrados();
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
            return result;
        }

        /// <summary>
        /// Obtiene un Grado por su Descripcion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal GradoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                GradoInfo result = gradoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Metodo para Guardar/Modificar una entidad Grado
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(GradoInfo info)
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                int result = info.GradoID;
                if (info.GradoID == 0)
                {
                    result = gradoDAL.Crear(info);
                }
                else
                {
                    gradoDAL.Actualizar(info);
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
        internal ResultadoInfo<GradoInfo> ObtenerPorPagina(PaginacionInfo pagina, GradoInfo filtro)
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                ResultadoInfo<GradoInfo> result = gradoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una entidad Grado por su Id
        /// </summary>
        /// <param name="gradoID">Obtiene una entidad Grado por su Id</param>
        /// <returns></returns>
        internal GradoInfo ObtenerPorID(int gradoID)
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                GradoInfo result = gradoDAL.ObtenerPorID(gradoID);
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
        internal IList<GradoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                IList<GradoInfo> result = gradoDAL.ObtenerTodos(estatus);
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
        /// Obtiene un lista de Grado
        /// </summary>
        /// <returns></returns>
        internal IList<GradoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var gradoDAL = new GradoDAL();
                IList<GradoInfo> result = gradoDAL.ObtenerTodos();
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
