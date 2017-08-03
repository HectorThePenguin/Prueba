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
    internal class FamiliaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Familia
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(FamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                int result = info.FamiliaID;
                if (info.FamiliaID == 0)
                {
                    result = familiaDAL.Crear(info);
                }
                else
                {
                    familiaDAL.Actualizar(info);
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
        ///     Obtiene un lista paginada de Familia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, FamiliaInfo filtro)
        {
            ResultadoInfo<FamiliaInfo> result;
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                result = familiaDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene una Familia por Id
        /// </summary>
        /// <param name="familiaId"></param>
        /// <returns></returns>
        internal FamiliaInfo ObtenerPorID(int familiaId)
        {
            FamiliaInfo familiaInfo;
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                familiaInfo = familiaDAL.ObtenerPorID(familiaId);
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
            return familiaInfo;
        }

        /// <summary>
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<FamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                IList<FamiliaInfo> result = familiaDAL.ObtenerTodos(estatus);
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
        internal IList<FamiliaInfo> Centros_ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                IList<FamiliaInfo> result = familiaDAL.Centros_ObtenerTodos(estatus);
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
        /// Obtiene una entidad Familia por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal FamiliaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var familiaDAL = new FamiliaDAL();
                FamiliaInfo result = familiaDAL.ObtenerPorDescripcion(descripcion);
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
