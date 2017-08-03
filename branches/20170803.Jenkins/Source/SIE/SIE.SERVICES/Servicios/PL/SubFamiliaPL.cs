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
    public class SubFamiliaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SubFamilia
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(SubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                int result = subFamiliaBL.Guardar(info);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> resultado;
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                resultado = subFamiliaBL.ObtenerPorPagina(pagina, filtro);
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

        /// <summary>
        ///     Obtiene un SubFamilia por Id
        /// </summary>
        /// <param name="subFamiliaId"></param>
        /// <returns></returns>
        public SubFamiliaInfo ObtenerPorID(int subFamiliaId)
        {
            SubFamiliaInfo subFamiliaInfo;
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                subFamiliaInfo = subFamiliaBL.ObtenerPorID(subFamiliaId);
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
            return subFamiliaInfo;
        }

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<SubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                IList<SubFamiliaInfo> result = subFamiliaBL.ObtenerTodos(estatus);
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
        public IList<SubFamiliaInfo> Centro_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                IList<SubFamiliaInfo> result = subFamiliaBL.Centros_ObtenerTodos(estatus);
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
        /// Obtiene una lista por la Familia ID
        /// </summary>
        /// <returns></returns>
        public IList<SubFamiliaInfo> ObtenerPorFamiliaID(int familiaID)
        {
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                IList<SubFamiliaInfo> result = subFamiliaBL.ObtenerPorFamiliaID(familiaID);
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
        /// <param name="familiaId"> </param>
        /// <returns></returns>
        public SubFamiliaInfo ObtenerPorDescripcion(string descripcion, int familiaId)
        {
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                SubFamiliaInfo result = subFamiliaBL.ObtenerPorDescripcion(descripcion, familiaId);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SubFamiliaInfo> ObtenerPorPaginaPorFamilia(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> resultado;
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                resultado = subFamiliaBL.ObtenerPorPaginaPorFamilia(pagina, filtro);
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

        /// <summary>
        ///     Obtiene un SubFamilia por familia
        /// </summary>
        /// <param name="subFamilia"></param>
        /// <returns></returns>
        public SubFamiliaInfo ObtenerPorIDPorFamilia(SubFamiliaInfo subFamilia)
        {
            SubFamiliaInfo subFamiliaInfo;
            try
            {
                Logger.Info();
                var subFamiliaBL = new SubFamiliaBL();
                subFamiliaInfo = subFamiliaBL.ObtenerPorIDPorFamilia(subFamilia);
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
            return subFamiliaInfo;
        }
    }
}