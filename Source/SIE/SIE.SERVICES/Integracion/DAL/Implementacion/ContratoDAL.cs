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
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ContratoDAL : DALBase
    {
    /// <summary>
    /// Metodo para obtener contrato todos
    /// </summary>
    /// <returns></returns>
    internal List<ContratoInfo> ObtenerPorEstado(EstatusEnum estatus)
    {
        try
        {
            Logger.Info();
            var parameters = AuxContratoDAL.ObtenerParametrosObtenerContratosPorEstado(estatus);
            var ds = Retrieve("Contrato_ObtenerPorEstado", parameters);
            List<ContratoInfo> result = null;
            if (ValidateDataSet(ds))
            {
                result = MapContratoDAL.ObtenerContratosPorEstado(ds);
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
    /// Metodo para obtener todos los contratos (activos o inactivos)
    /// </summary>
    /// <returns></returns>
    internal List<ContratoInfo> ObtenerTodos()
    {
        try
        {
            Logger.Info();
            var parameters = new Dictionary<string, object>();
            var ds = Retrieve("Contrato_ObtenerPorEstado", parameters);
            List<ContratoInfo> result = null;
            if (ValidateDataSet(ds))
            {
                result = MapContratoDAL.ObtenerContratosPorEstado(ds);
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
        /// Obtiene un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxContratoDAL.ObtenerParametrosObtenerPorId(contratoInfo);
                var ds = Retrieve("Contrato_ObtenerPorID", parameters);
                ContratoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerPorId(ds);
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
        /// Obtiene un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorFolio(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxContratoDAL.ObtenerParametrosObtenerPorFolio(contratoInfo);
                var ds = Retrieve("Contrato_ObtenerPorFolio", parameters);
                ContratoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerPorFolio(ds);
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
        /// Obtiene una lista de contratos del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ContratoInfo> ObtenerContratosPorProveedorId(int proveedorId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxContratoDAL.ObtenerContratosPorProveedorId(proveedorId,organizacionId);
                var ds = Retrieve("Contrato_ObtenerPorProveedorID", parameters);
                List<ContratoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerContratosPorProveedorId(ds);
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
        /// Crea un nuevo contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal ContratoInfo Crear(ContratoInfo contratoInfo, int tipoFolio)
        {
            ContratoInfo result = null; 
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosCrearContrato(contratoInfo, tipoFolio);
                DataSet ds = Retrieve("Contrato_Crear", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerPorId(ds);
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
        /// Actualiza un registro de contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal void ActualizarEstado(ContratoInfo contratoInfo, EstatusEnum estatus)
        {
            try
            {
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosActualizarEstado(contratoInfo, estatus);
                Update("Contrato_ActualizarEstado", parameters);
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
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            ResultadoInfo<ContratoInfo> contratoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Contrato_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    contratoLista = MapContratoDAL.ObtenerPorPagina(ds);
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
            return contratoLista;
        }

        /// <summary>
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorContenedor(ContratoInfo contenedor)
        {
            ContratoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosPorContenedor(contenedor);
                DataSet ds = Retrieve("Contrato_ObtenerPorFolioContenedor", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerPorContenedor(ds);
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
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="pagina"> </param>
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorContenedorPaginado(PaginacionInfo pagina, ContratoInfo contenedor)
        {
            ResultadoInfo<ContratoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosPorContenedorPaginado(pagina, contenedor);
                DataSet ds = Retrieve("Contrato_ObtenerPorFolioPaginadoContenedor", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDAL.ObtenerPorContenedorPaginado(ds);
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
        /// Obtiene una lista de contratos por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorPaginaSinProgramacion(PaginacionInfo pagina, ContratoInfo filtro)
        {
            ResultadoInfo<ContratoInfo> contratoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Contrato_ObtenerPorPaginaSinProgramacion", parameters);
                if (ValidateDataSet(ds))
                {
                    contratoLista = MapContratoDAL.ObtenerPorPagina(ds);
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
            return contratoLista;
        }

        /// <summary>
        /// Obtiene una lista de contratos por XML
        /// </summary>
        /// <param name="contratosId"></param>
        /// <returns></returns>
        internal IEnumerable<ContratoInfo> ObtenerPorXML(List<int> contratosId)
        {
            try
            {
                string parametro = AuxContratoDAL.ObtenerParametrosPorXML(contratosId);
                IMapBuilderContext<ContratoInfo> mapeo = MapContratoDAL.ObtenerPorXML();
                IEnumerable<ContratoInfo> almacenMovimientoCostoPorAlmacenMovimiento = GetDatabase().
                    ExecuteSprocAccessor<ContratoInfo>("Contrato_ObtenerPorXML", mapeo.Build(), new object[] {parametro});
                return almacenMovimientoCostoPorAlmacenMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de contratos por fechas
        /// </summary>
        /// <returns></returns>
        internal List<ContratoInfo> ObtenerPorFechasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Logger.Info();
                List<ContratoInfo> result = null;
                Dictionary<string, object> parameters =
                    AuxContratoDAL.ObtenerParametrosPorFechasConciliacion(organizacionID, fechaInicio, fechaFin);
                using (IDataReader reader = RetrieveReader("Contrato_ObtenerConciliacionMovimientosSIAP", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        result = MapContratoDAL.ObtenerContratosPorFechasConciliacion(reader);
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

        /// <summary>
        /// Metodo que actualiza un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal void ActualizarContrato(ContratoInfo contratoInfo, EstatusEnum estatus)
        {
            try
            {
                Dictionary<string, object> parameters = AuxContratoDAL.ObtenerParametrosActualizarContrato(contratoInfo, estatus);
                Update("Contrato_Actualizar", parameters);
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
