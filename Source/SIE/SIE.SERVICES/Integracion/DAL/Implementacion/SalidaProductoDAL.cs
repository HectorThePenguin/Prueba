using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SalidaProductoDAL : DALBase
    {
        /// <summary>
        /// Obtiene salida de producto por id
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerPorSalidaProductoId(SalidaProductoInfo salidaProducto)
        {
            SalidaProductoInfo producto = null;
            try
            {
                Dictionary<string, object> parameters = AuxSalidaProductoDAL.ObtenerParametrosPorId(salidaProducto);
                DataSet ds = Retrieve("SalidaProducto_ObtenerPorId", parameters);
                if (ValidateDataSet(ds))
                {
                    producto = MapSalidaProductoDAL.ObtenerFoliosPorId(ds);
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
            return producto;
        }
        /// <summary>
        /// Obtiene una salida de producto por folio de salida
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo salidaProducto)
        {
            SalidaProductoInfo producto = null;
            try
            {
                Dictionary<string, object> parameters = AuxSalidaProductoDAL.ObtenerParametrosPorFolioSalida(salidaProducto);
                DataSet ds = Retrieve("SalidaProducto_ObtenerPorFolioSalida", parameters);
                if (ValidateDataSet(ds))
                {
                    producto = MapSalidaProductoDAL.ObtenerFoliosPorFolioSalida(ds);
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
            return producto;
        }
        
        /// <summary>
        /// Obtiene la lista por pagina de las salidas de productos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaSalidaProducto(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosPorPaginaSalidaProducto(pagina, filtro);
                DataSet ds = Retrieve("SalidaProducto_ObtenerFolioPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapSalidaProductoDAL.ObtenerFoliosPorPaginaParaSalidaProducto(ds);
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
            return foliosLista;
        }
        /// <summary>
        /// Guarda el primer pesaje de la salida del producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal int GuardarPrimerPesajeSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosGuardarPrimerPesajeSalida(salida);
                int result = Create("SalidaProducto_CrearPrimerPeso", parameters);
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
        /// Guarda el primer pesaje de la salida del producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal int GuardarSegundoPesajeSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosGuardarSegundoPesajeSalida(salida);
                int result = Create("SalidaProducto_ActualizarSegundoPesaje", parameters);
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
        /// Termina la salida del producto
        /// </summary>
        /// <param name="salida"></param>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        internal int TerminarSalidaProducto(SalidaProductoInfo salida, AlmacenMovimientoInfo movimiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosTerminarSalida(salida, movimiento);
                int result = Create("SalidaProducto_TerminarSalidaProducto", parameters);
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
        /// Actualiza el almacen, almacen inventario lote y piezas.
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal void ActualizarAlmacenInventarioLote(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosActualizaAlmacenInventarioLote(salida);
                Update("SalidaProducto_ActualizaAlmacenInventario", parameters);
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
        /// Consulta los folios activos con el peso tara capturado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<SalidaProductoInfo> ObtenerTraspasoFoliosActivos(SalidaProductoInfo filtro)
        {
            List<SalidaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosTraspasoFoliosActivos(filtro);
                DataSet ds = Retrieve("SalidaProducto_BusquedaTraspasoFoliosActivos", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapSalidaProductoDAL.ObtenerTraspasoFoliosActivos(ds);
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
            return foliosLista;
        }
        /// <summary>
        /// Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal FacturaInfo ObtenerDatosFacturaPorFolioSalida(SalidaProductoInfo salidaProducto)
        {
            FacturaInfo producto = null;
            try
            {
                Dictionary<string, object> parameters = AuxSalidaProductoDAL.ObtenerParametrosObtenerDatosFacturaPorFolioSalida(salidaProducto);
                DataSet ds = Retrieve("SalidaProducto_ObtenerDatosFacturaPorFolioSalida", parameters);
                if (ValidateDataSet(ds))
                {
                    producto = MapSalidaProductoDAL.ObtenerDatosFacturaPorFolioSalida(ds);
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
            return producto;
        }

        /// <summary>
        /// Obtiene un lista paginada de los folios de salida
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SalidaProductoInfo> ObtenerFolioPorPaginaReimpresion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosPorPaginaReimpresion(pagina, filtro);
                using (IDataReader reader = RetrieveReader("SalidaProducto_ObtenerFolioPorPaginaReimpresion", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        foliosLista = MapSalidaProductoDAL.ObtenerFoliosPorPaginaReimpresion(reader);
                    }
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return foliosLista;
        }

        /// <summary>
        /// Obtiene un folio de salida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerFolioPorReimpresion(SalidaProductoInfo filtro)
        {
            SalidaProductoInfo folio = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosPorReimpresion(filtro);
                using (IDataReader reader = RetrieveReader("SalidaProducto_ObtenerFolioReimpresion", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        folio = MapSalidaProductoDAL.ObtenerFoliosPorReimpresion(reader);
                    }
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return folio;
        }

        /// <summary>
        /// Obtiene una lista de salidas de producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal IEnumerable<SalidaProductoInfo> ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosPorAlmacenMovimientoXML(almacenesMovimiento);
                IMapBuilderContext<SalidaProductoInfo> mapeo = MapSalidaProductoDAL.ObtenerPolizasConciliacion();
                IEnumerable<SalidaProductoInfo> foliosFaltantes = GetDatabase().ExecuteSprocAccessor
                    <SalidaProductoInfo>(
                        "SalidaProducto_ObtenerConciliacionMovimientosSIAP", mapeo.Build(),
                        new[] { parameters["@XmlAlmacenMovimiento"] });
                return foliosFaltantes;
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

        internal bool Cancelar(SalidaProductoInfo salidaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosCancelar(salidaProducto);
                Update("SalidaProducto_Cancelar", parameters);
                return true;
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

        internal ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaCancelacion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxSalidaProductoDAL.ObtenerParametrosObtenerFoliosPorPaginaParaCancelacion(pagina, filtro);
                DataSet ds = Retrieve("SalidaProducto_ObtenerCancelacionPorDescripcionProducto", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapSalidaProductoDAL.ObtenerFoliosPorPaginaParaSalidaProducto(ds);
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
            return foliosLista;
        }   
    }
}
