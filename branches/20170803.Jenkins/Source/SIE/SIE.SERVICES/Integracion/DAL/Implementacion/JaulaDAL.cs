using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class JaulaDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Jaula
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(JaulaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametrosCrear(info);
                Create("Jaula_Crear", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Metodo que actualiza un Jaula
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(JaulaInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametrosActualizar(info);
                Update("Jaula_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro)
        {
            ResultadoInfo<JaulaInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Jaula_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapJaulaDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de todos los Jaulas
        /// </summary>
        /// <returns></returns>
        internal List<JaulaInfo> ObtenerTodos()
        {
            List<JaulaInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Jaula_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene una lista de jaulas filtrandas por su estatus
        /// </summary>
        /// <returns></returns>
        internal List<JaulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Jaula_ObtenerTodos", parameters);
                List<JaulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerTodos(ds);
                }

                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Obtiene un Jaula por Id
        /// </summary>
        /// <returns></returns>
        internal JaulaInfo ObtenerPorID(int id)
        {
            JaulaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("Jaula_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene las Jaulas de un Proveedor
        /// </summary>
        /// <returns></returns>
        internal List<JaulaInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<JaulaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametroPorProveedorId(proveedorId);
                DataSet ds = Retrieve("Jaula_ObtenerPorProveedorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerPorProveedorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una entidad de JaulaInfo
        /// </summary>
        /// <param name="jaula"></param>
        /// <returns></returns>
        internal JaulaInfo ObtenerJaula(JaulaInfo jaula)
        {
            JaulaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametroPorJaula(jaula);
                DataSet ds = Retrieve("Jaula_ObtenerPorPlacaJaula", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un registro de Jaula
        /// </summary>
        /// <param name="descripcion">Descripción de la Jaula</param>
        /// <returns></returns>
        internal JaulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Jaula_ObtenerPorDescripcion", parameters);
                JaulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de JaulaInfo
        /// </summary>
        /// <param name="jaula"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal JaulaInfo ObtenerJaula(JaulaInfo jaula, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            JaulaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametroPorJaula(jaula, Dependencias);
                DataSet ds = Retrieve("Jaula_ObtenerPorPlacaJaula", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapJaulaDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<JaulaInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxJaulaDAL.ObtenerParametrosPorPagina(pagina, filtro, Dependencias);
                DataSet ds = Retrieve("Jaula_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapJaulaDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;

        }
    }
}
