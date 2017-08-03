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
    internal class SubFamiliaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SubFamilia
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(SubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                int result = info.SubFamiliaID;
                if (info.SubFamiliaID == 0)
                {
                    result = subFamiliaDAL.Crear(info);
                }
                else
                {
                    subFamiliaDAL.Actualizar(info);
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
        ///     Obtiene un lista paginada de SubFamilia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> result;
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                result = subFamiliaDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene una SubFamilia por Id
        /// </summary>
        /// <param name="subFamiliaId"></param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorID(int subFamiliaId)
        {
            SubFamiliaInfo subFamiliaInfo;
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                subFamiliaInfo = subFamiliaDAL.ObtenerPorID(subFamiliaId);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<SubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                IList<SubFamiliaInfo> result = subFamiliaDAL.ObtenerTodos(estatus);
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
        internal IList<SubFamiliaInfo> Centros_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                IList<SubFamiliaInfo> result = subFamiliaDAL.Centros_ObtenerTodos(estatus);
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
        internal IList<SubFamiliaInfo> ObtenerPorFamiliaID(int familiaID)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                IList<SubFamiliaInfo> result = subFamiliaDAL.ObtenerPorFamiliaID(familiaID);
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
        /// Obtiene una entidad SubFamilia por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="familiaId"> </param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorDescripcion(string descripcion, int familiaId)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                SubFamiliaInfo result = subFamiliaDAL.ObtenerPorDescripcion(descripcion, familiaId);
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
        internal ResultadoInfo<SubFamiliaInfo> ObtenerPorPaginaPorFamilia(PaginacionInfo pagina, SubFamiliaInfo filtro)
        {
            ResultadoInfo<SubFamiliaInfo> result;
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                result = subFamiliaDAL.ObtenerPorPaginaPorFamilia(pagina, filtro);
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
        ///     Obtiene un SubFamilia por familia
        /// </summary>
        /// <param name="subFamilia"></param>
        /// <returns></returns>
        internal SubFamiliaInfo ObtenerPorIDPorFamilia(SubFamiliaInfo subFamilia)
        {
            try
            {
                Logger.Info();
                var subFamiliaDAL = new SubFamiliaDAL();
                SubFamiliaInfo result = subFamiliaDAL.ObtenerPorIDPorFamilia(subFamilia);
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
