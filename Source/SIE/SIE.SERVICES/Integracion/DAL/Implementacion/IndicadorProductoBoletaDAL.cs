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
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class IndicadorProductoBoletaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de IndicadorProductoBoleta
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(IndicadorProductoBoletaInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                         {"@IndicadorProductoID", info.IndicadorProducto.IndicadorProductoId},
                                         {"@RangoMaximo", info.RangoMaximo},
                                         {"@RangoMinimo", info.RangoMinimo},
                                         {"@Activo", info.Activo},
                                         {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                     };
                int result = Create("IndicadorProductoBoleta_Crear", parameters);
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
        /// Metodo para actualizar un registro de IndicadorProductoBoleta
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(IndicadorProductoBoletaInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@IndicadorProductoBoletaID", info.IndicadorProductoBoletaID},
                                         {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                         {"@IndicadorProductoID", info.IndicadorProducto.IndicadorProductoId},
                                         {"@Activo", info.Activo},
                                         {"@RangoMaximo", info.RangoMaximo},
                                         {"@RangoMinimo", info.RangoMinimo},
                                         {"@UsuarioModificacionID", info.UsuarioModificacionID},
                                     };
                Update("IndicadorProductoBoleta_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IndicadorProductoBoletaInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorProductoBoletaInfo filtro)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@Inicio", pagina.Inicio},
                                         {"@Limite", pagina.Limite},
                                         {"@Activo", filtro.Activo},
                                         {"@IndicadorID", filtro.IndicadorProducto.IndicadorInfo.IndicadorId},
                                         {"@ProductoID", filtro.IndicadorProducto.Producto.ProductoId}
                                     };
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerPorPagina", parameters);
                ResultadoInfo<IndicadorProductoBoletaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de IndicadorProductoBoleta
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoBoletaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerTodos");
                IList<IndicadorProductoBoletaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoBoletaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = null;
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerTodos", parameters);
                IList<IndicadorProductoBoletaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de IndicadorProductoBoleta
        /// </summary>
        /// <param name="indicadorProductoBoletaID">Identificador de la IndicadorProductoBoleta</param>
        /// <returns></returns>
        public IndicadorProductoBoletaInfo ObtenerPorID(int indicadorProductoBoletaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = null;
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerPorID", parameters);
                IndicadorProductoBoletaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerPorID(ds);
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
        /// Obtiene una indicador producto boleta por
        /// indicador producto y organizacion
        /// </summary>
        /// <param name="indicadorProductoID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal IndicadorProductoBoletaInfo ObtenerPorIndicadorProductoOrganizacion(int indicadorProductoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@IndicadorProductoID", indicadorProductoID},
                                         {"@OrganizacionID", organizacionID}
                                     };
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerPorIndicadorProductoOrganizacion", parameters);
                IndicadorProductoBoletaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerPorIndicadorProductoOrganizacion(ds);
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
        /// Obtiene una indicador producto boleta por
        /// indicador producto y organizacion
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        internal List<IndicadorProductoBoletaInfo> ObtenerPorProductoOrganizacion(int productoID, int organizacionID, EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@ProductoID", productoID},
                                         {"@OrganizacionID", organizacionID},
                                         {"@Activo", activo.GetHashCode()}
                                     };
                DataSet ds = Retrieve("IndicadorProductoBoleta_ObtenerPorProductoOrganizacion", parameters);
                List<IndicadorProductoBoletaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoBoletaDAL.ObtenerPorProductoOrganizacion(ds);
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
    }
}
