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
    public class GradoPL
    {

        public IList<GradoInfo> ObtenerGrados()
        {
            IList<GradoInfo> result;
            try
            {
                Logger.Info();
                var grado = new GradoBL();
                result = grado.ObtenerGrados();
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
        /// Obtiene una lista paginada de Grados
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<GradoInfo> ObtenerPorPagina(PaginacionInfo pagina, GradoInfo filtro)
        {
            try
            {
                Logger.Info();
                var gradoBL = new GradoBL();
                ResultadoInfo<GradoInfo> result = gradoBL.ObtenerPorPagina(pagina, filtro);
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
        /// Guarda un Grado
        /// </summary>
        /// <param name="grado"></param>
        public int Guardar(GradoInfo grado)
        {
            try
            {
                Logger.Info();
                var gradoBL = new GradoBL();
                int result = gradoBL.Guardar(grado);
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
        /// Obtiene un Grado por su Descripcion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public GradoInfo ObtenerPorDescripcion(string descripcion)
        {
            GradoInfo result;
            try
            {
                Logger.Info();
                var grado = new GradoBL();
                result = grado.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="gradoID"></param>
        /// <returns></returns>
        public GradoInfo ObtenerPorID(int gradoID)
        {
            try
            {
                Logger.Info();
                var gradoBL = new GradoBL();
                GradoInfo result = gradoBL.ObtenerPorID(gradoID);
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
        public IList<GradoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var gradoBL = new GradoBL();
                IList<GradoInfo> result = gradoBL.ObtenerTodos();
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
        public IList<GradoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var gradoBL = new GradoBL();
                IList<GradoInfo> result = gradoBL.ObtenerTodos(estatus);
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
