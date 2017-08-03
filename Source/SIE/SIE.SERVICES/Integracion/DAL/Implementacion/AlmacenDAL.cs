using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AlmacenDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un Almacen por su Id
        /// </summary>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorID(int almacenID)
        {
            AlmacenInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametroPorID(almacenID);
                using (IDataReader reader = RetrieveReader("Almacen_ObtenerPorID", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        result = MapAlmacenDAL.ObtenerPorID(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
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
            return result;
        }

        /// <summary>
        /// Validar que se tenga inventario para el medicamento
        /// </summary>
        /// <param name="itemProducto"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerCantidadProductoEnInventario(ProductoInfo itemProducto, int almacenID)
        {
            AlmacenInventarioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenDAL.ObtenerParametroObtenerCantidadProductoEnInventario(itemProducto, almacenID);
                DataSet ds = Retrieve("Almacen_ObtenerCantidadProducto", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerCantidadProductoEnInventario(ds);
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
        /// Guarda almacen Movimento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo GuardarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            AlmacenMovimientoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenDAL.ObtenerParametroGuardarAlmacenMovimiento(almacenMovimientoInfo);
                DataSet ds = Retrieve("Almacen_GuardarAlmacenMovimiento", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimiento(ds);
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
        /// Se almacena el detalle de almacen Movimiento
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle"></param>
        /// <param name="almacenID"></param>
        internal decimal GuardarAlmacenMovimientoDetalle(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle,
                                                       int almacenID)
        {
            decimal result = 0;
            try
            {
                Logger.Info();

                List<AlmacenMovimientoDetalle> lista = listaAlmacenMovimientoDetalle.Where(
                    almacenMovimientoDetalle => almacenMovimientoDetalle.TratamientoID == 0).ToList();
                Dictionary<string, object> parameters = null;
                if (lista != null && lista.Count > 0)
                {
                    parameters = AuxAlmacenDAL.ObtenerParametroGuardarAlmacenMovimientoDetalleTratamientoNulo(listaAlmacenMovimientoDetalle, almacenID);
                }
                else
                {
                    parameters = AuxAlmacenDAL.ObtenerParametroGuardarAlmacenMovimientoDetalle(listaAlmacenMovimientoDetalle, almacenID);
                }
                DataSet ds = Retrieve("Almacen_GuardarAlmacenMovimientoDetalle", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerGuardarAlmacenMovimientoDetalle(ds);
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
        ///  Validar que no queden ajustes pendientes por aplicar para el almacen(Diferencias de inventario)
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal bool ExistenAjustesPendientesParaAlmacen(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            var result = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenDAL.ObtenerParametroExistenAjustesPendientesParaAlmacen(
                        almacenMovimientoInfo);
                DataSet ds = Retrieve("Almacen_ExistenAjustesPendientesParaAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerExistenAjustesPendientesParaAlmacen(ds);
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
        /// Metodo para Crear un registro de Almacen
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosCrear(info);
                int result = Create("Almacen_Crear", parameters);
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
        /// Metodo para actualizar un registro de Almacen
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosActualizar(info);
                Update("Almacen_Actualizar", parameters);
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
        internal ResultadoInfo<AlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Almacen_ObtenerPorPagina", parameters);
                ResultadoInfo<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Almacen
        /// </summary>
        /// <returns></returns>
        internal IList<AlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Almacen_ObtenerTodos");
                IList<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerTodos(ds);
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
        internal IList<AlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Almacen_ObtenerTodos", parameters);
                IList<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Almacen
        /// </summary>
        /// <param name="descripcion">Descripción de la Almacen</param>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Almacen_ObtenerPorDescripcion", parameters);
                AlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de AlmacenMovimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo">Descripción de la Almacen</param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerAlmacenMovimiento(almacenMovimientoInfo);
                DataSet ds = Retrieve("Almacen_ObtenerAlmacenMovimiento", parameters);
                AlmacenMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimientoPorID(ds);
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
        /// Obtiene los almacenes por organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<AlmacenInfo> ObtenerAlmacenPorOrganizacion( int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerAlmacenPorOrganizacion(organizacionId);
                using (IDataReader reader = RetrieveReader("Almacen_ObtenerPorOrganizacion", parameters))
                {
                    IList<AlmacenInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapAlmacenDAL.ObtenerPorOrganizacion(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }

        public AlmacenCierreDiaInventarioInfo ObtenerDatosAlmacenInventario(AlmacenCierreDiaInventarioInfo cierreInventarioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerObtenerDatosAlmacenInventario(cierreInventarioInfo);
                DataSet ds = Retrieve("Almacen_ObtenerFolioAlmacen", parameters);
                AlmacenCierreDiaInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerCierreAlmacenMovimientoInfo(ds);
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
        /// Obtener productos almacen
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<AlmacenCierreDiaInventarioInfo> ObtenerProductosAlamcen(int almacenId, int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerObtenerDatosAlmacenProductos(almacenId, organizacionId);
                DataSet ds = Retrieve("Almacen_ObtenerProductosPorAlmacenID", parameters);
                IList<AlmacenCierreDiaInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerProductosAlamcen(ds);
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
        ///     Metodo que actualiza en almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        internal void ActualizarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosActualizarAlmacenMovimiento(almacenMovimientoInfo);
                Update("Almacen_ActualizarAlmacenMovimiento", parameters);
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
        ///     Metodo que actualiza en almacen inventario
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        internal void ActualizarAlmacenInventario(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosActualizarAlmacenInventario(almacenInventarioInfo);
                Update("Almacen_ActualizarAlmacenInventario", parameters);
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
        ///     Metodo que actualiza en almacen inventario
        /// </summary>
        /// <param name="almacenMovimientoDetalle"></param>
        internal void EliminarAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenMovimientoDetalle)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosEliminarAlmacenMovimientoDetalle(almacenMovimientoDetalle);
                Delete("Almacen_EliminarAlmacenMovimientoDetalle", parameters);
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
        /// Obtener folio almacen
        /// </summary>
        /// <param name="almacenCierreFolio"></param>

        public void obtenerFolioAlmacen(AlmacenCierreDiaInventarioInfo almacenCierreFolio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosobtenerFolioAlmacen(almacenCierreFolio);
                Update("FolioAlmacen_Obtener", parameters);
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
        /// obtiene almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public IList<AlmacenMovimientoInfo> ObtenerListaAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo, int activo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerobtenerListaAlmacenMovimiento(almacenMovimientoInfo, activo);
                DataSet ds = Retrieve("Almacen_ObtenerListaAlmacenMovimiento", parameters);
                IList<AlmacenMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerListaAlmacenMovimiento(ds);
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

        internal List<AlmacenInventarioInfo> ObtenerProductosAlmacenInventario(AlmacenInfo almacen, OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerProductosAlmacenInventario(almacen, organizacion);
                DataSet ds = Retrieve("AlmacenInventario_ConsultarPorAlmacenID", parameters);
                List<AlmacenInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerProductosAlmacenInventario(ds);
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

        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorAlmacenID(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosObtenerAlmacenMovimientoPorAlmacenID(almacen);
                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorAlmacenID", parameters);
                List<AlmacenMovimientoDetalle> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimientoPorAlmacenID(ds);
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

        internal int GuardarConsumoAlimento(List<AlmacenInventarioInfo> listaActualizadaProductos, OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosGuardarConsumoAlimento(listaActualizadaProductos, organizacion);
                int result = Create("AlmacenInventario_ActualizaInventarioProductos", parameters);
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
        /// Se almacena el detalle de almacen Movimiento
        /// </summary>
        /// <param name="almacenMovimientoDetalle"></param>
        internal AlmacenMovimientoDetalle GuardarAlmacenMovimientoDetalleProducto(AlmacenMovimientoDetalle almacenMovimientoDetalle)
        {
            AlmacenMovimientoDetalle result = null;
            try
            {
                Logger.Info();

                var parameters = AuxAlmacenDAL.ObtenerParametrosGuardarAlmacenMovimientoDetalleProducto(almacenMovimientoDetalle);
                DataSet ds = Retrieve("Almacen_GuardarAlmacenMovimientoDetalleProducto", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimientoDetalleProducto(ds);
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
        /// Obtiene los movimientos del almacen por contrato
        /// </summary>
        /// <param name="contrato">Contrato del cual se ontendran los movimientos</param>
        /// <returns>Lista de movimientos</returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorContrato(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosAlmacenMovimientoPorContrato(contrato);
                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorContrato", parameters);
                List<AlmacenMovimientoDetalle> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimientoPorContrato(ds);
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
        /// Obtiene el almacen inventario por organizacion, tipo de almacen y producto
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(ParametrosOrganizacionTipoAlmacenProductoActivo datos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacenProducto(datos);
                DataSet ds = Retrieve("Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto", parameters);
                AlmacenInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacenProducto(ds);
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
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal int ObtenerFolioAlmacenConsulta(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosFolioAlmacenConsulta(filtros);
                int result = Create("FolioAlmacen_ObtenerFolio", parameters);
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
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<AlmacenesCierreDiaInventarioPAModel> ObtenerAlmacenesOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosAlmacenesOrganizacion(organizacionID);
                DataSet ds = Retrieve("Almacen_ObtenerAlmacenPorOrganizacion", parameters);
                List<AlmacenesCierreDiaInventarioPAModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenesOrganizacion(ds);
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
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal void ActualizarFolioAlmacen(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosActualizarFolioAlmacen(filtros);
                Update("FolioAlmacen_ActualizarFolio", parameters);
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

        internal ResultadoInfo<AlmacenInfo> ObtenerPorPaginaPoliza(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosPorPaginaPoliza(pagina, filtro);
                DataSet ds = Retrieve("Almacen_ObtenerPorPaginaPoliza", parameters);
                ResultadoInfo<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorPaginaPoliza(ds);
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
        /// Obtiene un almacen
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorAlmacenPoliza(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerPorAlmacenPoliza(almacen);
                DataSet ds = Retrieve("Almacen_ObtenerPorAlmacen", parameters);
                AlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenPoliza(ds);
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
        /// Obtiene un lista paginada
        /// filtrando por varios tipos de almacen.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInfo> ObtenerPorOrganizacionTipoAlmacenPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosPorOrganizacionTipoAlmacenPagina(pagina, filtro);
                DataSet ds = Retrieve("Almacen_ObtenerPorOrganizacionTipoAlmacenPagina", parameters);
                ResultadoInfo<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorOrganizacionTipoAlmacenPagina(ds);
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
        /// Obtiene un listado de almacenes por tipo almacen
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInfo> ObtenerPorPaginaTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            ResultadoInfo<AlmacenInfo> almacenLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosPorPaginaTipoAlmacen(pagina, filtro);
                DataSet ds = Retrieve("Almacen_ObtenerPorPaginaTipoAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    almacenLista = MapAlmacenDAL.ObtenerPorPaginaTipoAlmacen(ds);
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
            return almacenLista;
        }

        /// <summary>
        /// Obtiene un almacen por id y tipo almacen
        /// </summary>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorIdFiltroTipoAlmacen(AlmacenInfo almacenInfo)
        {
            AlmacenInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametroObtenerPorIdFiltroTipoAlmacen(almacenInfo);
                DataSet ds = Retrieve("Almacen_ObtenerPorIDFiltroTipoAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorIdFiltroTipoAlmacen(ds);
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
        ///  Validar si el almacen cuenta con al menos un producto con existencia
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        internal bool ValidarProductosEnAlmacen(AlmacenInfo almacenInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ValidarProductosEnAlmacen(almacenInfo);
                DataSet ds = Retrieve("Almacen_ValidarProductoAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    return true;
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
            return false;
        }

        /// <summary>
        ///  Validar si el producto tiene existencias en algun Almacen
        /// </summary>
        /// <param name="productoID"></param>
        /// <returns></returns>
        internal bool ValidarExistenciasProductoEnAlmacen(int productoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosValidarExistenciasProductoEnAlmacen(productoID);
                DataSet ds = Retrieve("Almacen_ValidarProductoExistenciaAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    return true;
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
            return false;
        }

        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<AlmacenInfo> ObtenerAlmacenesPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParametrosAlmacenesOrganizacion(organizacionID);
                DataSet ds = Retrieve("Almacen_ObtenerAlmacenPorOrganizacion", parameters);
                List<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenesPorOrganizacion(ds);
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
        /// Guarda almacen Movimento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo GuardarAlmacenMovimientoConFecha(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            AlmacenMovimientoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenDAL.ObtenerParametroGuardarAlmacenMovimientoConFecha(almacenMovimientoInfo);
                DataSet ds = Retrieve("Almacen_GuardarAlmacenMovimientoConFecha", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenMovimientoConFecha(ds);
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
        ///     Obtiene un almacen por su id y su organizacion
        /// </summary>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorIDOrganizacion(AlmacenInfo info)
        {
            AlmacenInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenDAL.ObtenerParametroPorIDOrganizacion(info);
                DataSet ds = Retrieve("Almacen_ObtenerPorIDOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerPorIDOrganizacion(ds);
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

        internal List<AlmacenInfo> ObtenerAlamcenPorProducto(FiltroAlmacenProductoEnvio filtroEnvio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenDAL.ObtenerParmetrosPorProducto(filtroEnvio);
                DataSet ds = Retrieve("AbastoEnvioAlimento_ObtenerAlmacenProducto", parameters);
                List<AlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenDAL.ObtenerAlmacenesPorProducto(ds);
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