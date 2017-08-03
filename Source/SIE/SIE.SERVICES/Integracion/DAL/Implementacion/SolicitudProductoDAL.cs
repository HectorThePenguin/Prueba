using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Transactions;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class SolicitudProductoDAL : DALBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="cadena"></param>
        public SolicitudProductoDAL(string cadena = "")
        {
            if(!cadena.Equals(string.Empty))
                ConnectionString = cadena;
        }

        /// <summary>
        /// Metodo para Crear un registro de SolicitudProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDAL.ObtenerParametrosCrear(info);
                int result = Create("SolicitudProducto_Crear", parameters);
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
        /// Metodo para actualizar un registro de SolicitudProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDAL.ObtenerParametrosActualizar(info);
                Update("SolicitudProducto_Actualizar", parameters);
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
        /// Obtiene una lista de SolicitudProducto
        /// </summary>
        /// <returns></returns>
        public IList<SolicitudProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("SolicitudProducto_ObtenerTodos");
                IList<SolicitudProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDAL.ObtenerTodos(ds);
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
        public IList<SolicitudProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("SolicitudProducto_ObtenerTodos", parameters);
                IList<SolicitudProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de SolicitudProducto
        /// </summary>
        /// <param name="solicitudProductoID">Identificador de la SolicitudProducto</param>
        /// <returns></returns>
        public SolicitudProductoInfo ObtenerPorID(int solicitudProductoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDAL.ObtenerParametrosPorID(solicitudProductoID);
                DataSet ds = Retrieve("SolicitudProducto_ObtenerPorID", parameters);
                SolicitudProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDAL.ObtenerPorID(ds);
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
        /// Obtener los precios promedios de los productos
        /// </summary>
        /// <param name="folioSolicitud"></param>
        public List<ProductoINFORInfo> ObtenerPrecioPromedioProductorINFOR(long folioSolicitud)
        {
            List<ProductoINFORInfo> listaProductoINFORInfo = null;
            try
            {
                Logger.Info();
                
                
                string cadena =
                       string.Format("{0} {1} {2} '{3}'",
                        " SELECT TRA_CODE, TRA_ADVICE, TRL_QTY, TRL_PRICE, PAR_CODE, PAR_DESC, TRL_COSTCODE, PAR_UDFCHAR02 ",
                        " FROM vw_Solicitud_SIAP  ",
                        " WHERE TRA_ADVICE = ",
                        folioSolicitud);
                var ds = new DataSet();
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var command = new SqlCommand(cadena, connection);
                    command.Connection.Open();
                    var adapter = new SqlDataAdapter(command);

                    adapter.Fill(ds);
                }
                if (ValidateDataSet(ds))
                {
                    listaProductoINFORInfo = MapSolicitudProductoDAL.ObtenerPrecioPromedioProductoINFOR(ds);
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
            return listaProductoINFORInfo;
        }

        /// <summary>
        /// Metodo para valdiar si el producto existe como codigo parte
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoCodigoParteInfo ObtenerCodigoParteDeProducto(ProductoInfo producto)
        {
            ProductoCodigoParteInfo result = null;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@ProductoID", producto.ProductoId } };
                var ds = Retrieve("SolicitudProducto_ExisteCodigoParteDeproducto", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDAL.ObtenerCodigoParteDeProducto(ds);
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
        /// Obtiene una lista de solicitud de productos
        /// </summary>
        /// <param name="almacenMovimientos"></param>
        /// <returns></returns>
        internal List<SolicitudProductoInfo> ObtenerConciliacionPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenMovimientos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDAL.ObtenerConciliacionPorAlmacenXML(almacenMovimientos);
                List<SolicitudProductoInfo> result = null;
                using (IDataReader reader = RetrieveReader("SolicitudProducto_ObtenerConciliacionMovimientosSIAP", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        result = MapSolicitudProductoDAL.ObtenerConciliacionPorAlmacenMovimientoXML(reader);
                    }
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

        public bool GuardarInformacionCentros(string numeroDocumento, int usuarioId, int organizacionId, List<AreteInfo> aretes)
        {

            var result = false;
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    var parameters = AuxSolicitudProductoDAL.ObtenerParametrosGuardarAretes(numeroDocumento, usuarioId, organizacionId, aretes);
                    DataSet ds = Retrieve("CacRecepcionProducto_Insertar", parameters);

                    if (ValidateDataSet(ds))
                    {
                        result = MapSolicitudProductoDAL.ObtenerRespuestaNumeroDocumento(ds);
                    }
                    transaction.Complete();
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

        public string ValidarAretesDuplicados(int organizacionId, List<AreteInfo> aretes)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudProductoDAL.ObtenerParametrosValidarAretesDuplicados(organizacionId, aretes);
                var ds = Retrieve("CacRecepcionProducto_ValidarAretes", parameters);
                var result = string.Empty;
                if (ValidateDataSet(ds))
                    result = MapSolicitudProductoDAL.ObtenerRespuestaArete(ds);

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
