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
    internal class ProveedorDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un Proveedor
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(ProveedorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosCrear(info);
                Create("Proveedor_Crear", parameters);
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
        ///     Metodo que actualiza un Proveedor
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(ProveedorInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosActualizar(info);
                Update("Proveedor_Actualizar", parameters);

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
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPaginaCompleto(ds);
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
        /// Obtiene una lista de  proveedores de tipo ganaderas 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedorGanadera(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaGanaderas(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaTipoProveedorGanadera", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPaginaCompleto(ds);
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
        ///     Obtiene una lista de todos los Proveedores
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerTodos()
        {
            List<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Proveedor_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerTodos(ds);
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
        ///  Obtiene una lista de Proveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Proveedor_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerTodos(ds);
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
        /// Obtiene un  proveedor   ganadera por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal ProveedorInfo proveedorIdGanadera(int id)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("[dbo].[ProveedorGanadera_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorID(ds);
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
        ///     Obtiene un Proveedor por Id
        /// </summary>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorID(int id)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("[dbo].[Proveedor_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorID(ds);
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
        ///     Obtiene un Proveedor por Id
        /// </summary>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorIDConCorreo(int id)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("[dbo].[Proveedor_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorIDconCorreo(ds);
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
        ///     Obtiene un Proveedor por Id
        /// </summary>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorCodigoSAP(ProveedorInfo info)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametroPorCodigoSAP(info);
                DataSet ds = Retrieve("[dbo].[Proveedor_ObtenerPorCodigoSAP]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorCodigoSAP(ds);
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
        /// Obtiene un registro de Proveedor
        /// </summary>
        /// <param name="descripcion">Descripción de la Proveedor</param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Proveedor_ObtenerPorDescripcion", parameters);
                ProveedorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de Proveedor de un lote
        /// </summary>
        /// <param name="lote">Descripción de la Proveedor</param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosObtenerPorLote(lote);
                DataSet ds = Retrieve("Proveedor_ObtenerLote", parameters);
                ProveedorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorID(ds);
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
        ///     Obtiene una lista de proveedores por tipo proveedor id
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerPorTipoProveedorID(int estatus, int tipoProveedorID)
        {
            List<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosObtenerPorTipoProveedorID(estatus, tipoProveedorID);
                DataSet ds = Retrieve("Proveedor_ObtenerPorTipoProveedorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorTipoProveedorID(ds);
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
        /// Obtiene proveedores por tipo proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTipoProveedor(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaTipoProveedor(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaFiltroTipoProveedor", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene proveedores por tipo proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProveedorInfo> ObtenerPorPaginaFiltros(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaFiltros(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaFiltroTipoProveedor", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPaginaFiltros(ds);
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
        /// Obtiene los proveedores por una lista de tipos de proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedores(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaTiposProveedores(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorFiltro", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene los proveedores por una lista de tipos de proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaTiposProveedoresAlmacen(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaTiposProveedores(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorFiltroAlmacen", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPagina(ds);
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
        /// Consulta los proveedores que tiene asignado un producto en la tabla fletes internos
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerProveedoresEnFletesInternos(int productoId, int organizacionId)
        {
            List<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxProveedorDAL.ObtenerParametrosObtenerProveedoresEnFletesInternos(productoId, organizacionId);
                DataSet ds = Retrieve("Proveedor_ObtenerProveedoresEnFletesInternos", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerTodos(ds);
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
        ///     Obtiene un lista paginada de proveedores por fletes internos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerFoliosPorPaginaFletesInterno(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPaginaFletesInternos(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaFletesInternos", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorProductoContratoCodigoSAP(ProveedorInfo filtro)
        {
            ProveedorInfo proveedor = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorContratoCodigoSAP(filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorProductoContratoCodigoSAP", parameters);
                if (ValidateDataSet(ds))
                {
                    proveedor = MapProveedorDAL.ObtenerPorProductoContratoCodigoSAP(ds);
                }
                return proveedor;
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
        /// Obtiene los proveedores que tienen contrato
        /// con el producto seleccionado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorProductoContrato(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxProveedorDAL.ObtenerParametrosPorPaginaProductoContrato(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaProductoContrato", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPaginaProductoContrato(ds);
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
        /// Obtiene un  proveedor   ganadera por Id
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal ProveedorInfo proveedorIdGanadera(ProveedorInfo info)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametroPorIDGanaderas(info);
                DataSet ds = Retrieve("[dbo].[ProveedorGanadera_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorIDGanadera(ds);
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
        /// Obtiene el proveedor del Embarque
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ProveedorInfo ObtenerPorCodigoSAPEmbarque(ProveedorInfo filtro)
        {
            ProveedorInfo proveedor = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosCodigoSAPEmbarque(filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorCodigoSAPEmbarque", parameters);
                if (ValidateDataSet(ds))
                {
                    proveedor = MapProveedorDAL.ObtenerPorCodigoSAPEmbarque(ds);
                }
                return proveedor;
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
        /// Obtiene los proveedores del Embarque
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProveedorInfo> ObtenerPorPaginaEmbarque(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ProveedorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxProveedorDAL.ObtenerParametrosPorPaginaEmbarque(pagina, filtro);
                DataSet ds = Retrieve("Proveedor_ObtenerPorPaginaEmbarque", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProveedorDAL.ObtenerPorPaginaEmbarque(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        //internal ResultadoInfo<ProveedorInfo> ObtenerFleteroPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        //{
        //    ResultadoInfo<ProveedorInfo> lista = null;
        //    try
        //    {
        //        Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosPorPagina(pagina, filtro);
        //        DataSet ds = Retrieve("Proveedor_ObtenerFleteroPorPagina", parameters);
        //        if (ValidateDataSet(ds))
        //        {
        //            lista = MapProveedorDAL.ObtenerPorPaginaCompleto(ds);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    catch (DataException ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    return lista;
        //}

        /// <summary>
        ///  Obtiene un Proveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerProveedorActivo(EstatusEnum estatus)
        {
            List<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Proveedor_ObtenerProveedorActivo", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerTodos(ds);
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
        ///     Obtiene un Proveedor que cuenta con Origen-Destino Configurado
        /// </summary>
        /// <returns></returns>
        internal ProveedorInfo ObtenerProveedorConfiguradoOrigenDestino(EmbarqueDetalleInfo embarque)
        {
            ProveedorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerProveedorConfiguradoOrigenDestino(embarque);
                DataSet ds = Retrieve("[dbo].[Proveedor_ObtenerProveedorPorConfiguracionOrigenDestino]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerProveedorConfiguradoOrigenDestino(ds);
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
        ///     Obtiene una lista de proveedores por ruta
        /// </summary>
        /// <returns></returns>
        internal List<ProveedorInfo> ObtenerProveedoresPorRuta(int estatus, int tipoProveedorID, int ConfiguracionEmbarqueDetalleID)
        {
            List<ProveedorInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorDAL.ObtenerParametrosObtenerProveedoresPorRuta(estatus, tipoProveedorID, ConfiguracionEmbarqueDetalleID);
                DataSet ds = Retrieve("Proveedor_ObtenerProvedoresPorRuta", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorDAL.ObtenerPorTipoProveedorID(ds);
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
    }
}
