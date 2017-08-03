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
    internal class ClienteDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Cliente
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ClienteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosCrear(info);
                int result = Create("Cliente_Crear", parameters);
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
        /// Metodo para actualizar un registro de Cliente
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ClienteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosActualizar(info);
                Update("Cliente_Actualizar", parameters);
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
        internal ResultadoInfo<ClienteInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Cliente_ObtenerPorPagina", parameters);
                ResultadoInfo<ClienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Cliente
        /// </summary>
        /// <returns></returns>
        internal IList<ClienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Cliente_ObtenerTodos");
                IList<ClienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerTodos(ds);
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
        internal IList<ClienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Cliente_ObtenerTodos", parameters);
                IList<ClienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Cliente
        /// </summary>
        /// <param name="clienteID">Identificador de la Cliente</param>
        /// <returns></returns>
        internal ClienteInfo ObtenerPorID(int clienteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosPorID(clienteID);
                DataSet ds = Retrieve("Cliente_ObtenerPorID", parameters);
                ClienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Cliente
        /// </summary>
        /// <param name="descripcion">Descripción de la Cliente</param>
        /// <returns></returns>
        internal ClienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Cliente_ObtenerPorDescripcion", parameters);
                ClienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerPorDescripcion(ds);
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

        internal ResultadoInfo<ClienteInfo> ObtenerClientesPorPagina(PaginacionInfo pagina, string descripcion)
        {
            ResultadoInfo<ClienteInfo> listaEmbarque = null;
            try
            {
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosClientePorPagina(descripcion, pagina);
                DataSet ds = Retrieve("Cliente_ObtenerClientesPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    listaEmbarque = MapClienteDAL.ObtenerTodosClientes(ds);
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
            return listaEmbarque;
        }
        /// <summary>
        /// Obtiene los datos del cliente por codigo sap
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        internal ClienteInfo ObtenerClientePorCliente(ClienteInfo cliente)
        {
            ClienteInfo result = null;
            try
            {
                try
                {
                    Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosClientePorCodigoSAP(cliente);
                    DataSet ds = Retrieve("Cliente_ObtenerClientePorCodigoSAP", parameters);
                    if (ValidateDataSet(ds))
                    {
                        result = MapClienteDAL.ObtenerPorClienteInfo(ds);
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

        public ResultadoInfo<ClienteInfo> ObtenerActivosPorDescripcion(PaginacionInfo pagina,ClienteInfo filtro)
        {
            ResultadoInfo<ClienteInfo> result = null;
            try
            {
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosClientePorPagina(filtro.Descripcion,pagina);
                DataSet ds = Retrieve("SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerClientesAyudaPorPagina(ds);
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

        public ClienteInfo Obtener_Nombre_CodigoSAP_PorID(int clienteId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosPorID(clienteId);
                DataSet ds = Retrieve("SalidaGanadoTransito_ObtenerClienteByID", parameters);
                ClienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.Obtener_Nombre_CodigoSAP_PorID(ds);
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

        public ClienteInfo Obtener_ActivoPorCodigoSAP(ClienteInfo cliente)
        {
               ClienteInfo result = null;
            
                try
                {
                    Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosClientePorCodigoSAP(cliente);
                    DataSet ds = Retrieve("Cliente_ObtenerClientePorCodigoSAPActivos", parameters);
                    if (ValidateDataSet(ds))
                    {
                        result = MapClienteDAL.ObtenerPorClienteInfo(ds);
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

        public int ObtenerTotalClientesActivos()
        {
            int result = 0;

            try
            {
               // Dictionary<string, object> parameters = AuxClienteDAL.ObtenerParametrosClientePorCodigoSAP(cliente);
                DataSet ds = Retrieve("Cliente_ObtenerTotalClientesActivos");
                if (ValidateDataSet(ds))
                {
                    result = MapClienteDAL.ObtenerTotalClientesActivos(ds);
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
