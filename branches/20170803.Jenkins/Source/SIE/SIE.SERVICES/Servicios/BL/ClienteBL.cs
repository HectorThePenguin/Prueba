using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.ServicioValidaCreditoClientes;
using System.ServiceModel;
using System.Configuration;

namespace SIE.Services.Servicios.BL
{
    internal class ClienteBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Cliente
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ClienteInfo info)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                int result = info.ClienteID;
                if (info.ClienteID == 0)
                {
                    result = clienteDAL.Crear(info);
                }
                else
                {
                    clienteDAL.Actualizar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ClienteInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                ResultadoInfo<ClienteInfo> result = clienteDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Cliente
        /// </summary>
        /// <returns></returns>
        internal IList<ClienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                IList<ClienteInfo> result = clienteDAL.ObtenerTodos();
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
        internal IList<ClienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                IList<ClienteInfo> result = clienteDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Cliente por su Id
        /// </summary>
        /// <param name="clienteID">Obtiene una entidad Cliente por su Id</param>
        /// <returns>Regresa la informacion del cliente encontrado</returns>
        internal ClienteInfo ObtenerPorID(int clienteID)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                ClienteInfo result = clienteDAL.ObtenerPorID(clienteID);
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
        /// Obtiene una entidad Cliente por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ClienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                ClienteInfo result = clienteDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene el listado de clientes.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ResultadoInfo<ClienteInfo> ObtenerClientesPorPagina(PaginacionInfo pagina, string descripcion)
        {
            ResultadoInfo<ClienteInfo> result;
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                result = clienteDAL.ObtenerClientesPorPagina(pagina, descripcion);
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
        /// Método que consulta un cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        internal ClienteInfo ObtenerClientePorCliente(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                result = clienteDAL.ObtenerClientePorCliente(cliente);
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
        /// Método que consulta un cliente en SAP
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        internal ClienteInfo ObtenerClienteSAP(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var interfaceSAPBL = new InterfaceSAPBL();
                result = interfaceSAPBL.ObtenerClienteSAP(cliente);
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

        internal ResultadoInfo<ClienteInfo> ObtenerActivosPorDescripcion(PaginacionInfo pagina,ClienteInfo filtro)
        {
            ResultadoInfo<ClienteInfo> result;
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                result = clienteDAL.ObtenerActivosPorDescripcion( pagina, filtro);
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

        public ClienteInfo Obtener_Nombre_CodigoSAP_PorID(int clienteId)
        {
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                ClienteInfo result = clienteDAL.Obtener_Nombre_CodigoSAP_PorID(clienteId);
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

        public ClienteInfo Obtener_ActivoPorCodigoSAP(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                result = clienteDAL.Obtener_ActivoPorCodigoSAP(cliente);
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
        /// Realiza la validacion de limite de credito de un cliente
        /// </summary>
        /// <param name="datosValidacion"></param>
        /// <returns>Objeto con el resultado de la validacion</returns>
        public ResultadoValidacion validarLimiteCredito(ParametrosValidacionLimiteCredito datosValidacion)
        {
            ResultadoValidacion resultado = new ResultadoValidacion();
            ParametroGeneralBL parametroGen = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral;
            SI_OS_SBX_to_ECC_ValidaCreditoClientesRequest request;
            SI_OS_SBX_to_ECC_ValidaCreditoClientesResponse response;

            SI_OS_SBX_to_ECC_ValidaCreditoClientesClient servicio;
            try
            {
                parametroGeneral = parametroGen.ObtenerPorClaveParametro(ParametrosEnum.FlagValidarLimiteCredito.ToString());
                if (parametroGeneral.Valor.Trim().CompareTo("1") == 0)
                {
                    servicio = obtenerInstanciaClienteValidaCredito();
                    request = new SI_OS_SBX_to_ECC_ValidaCreditoClientesRequest();
                    response = new SI_OS_SBX_to_ECC_ValidaCreditoClientesResponse();
                    
                    request.MT_SBK_ValidaCreditoClientes_Request = new DT_SBK_ValidaCreditoClientes_Request
                    {
                        Cliente = datosValidacion.CodigoSAP,
                        Importe = datosValidacion.Importe.ToString(),
                        Sociedad = datosValidacion.Sociedad,
                        Moneda = datosValidacion.Moneda
                    };

                    response.MT_SBK_ValidaCreditoClientes_Response = servicio.SI_OS_SBX_to_ECC_ValidaCreditoClientes(request.MT_SBK_ValidaCreditoClientes_Request);
                    servicio.Close();
                    
                    if (response.MT_SBK_ValidaCreditoClientes_Response != null)
                    {
                        resultado.TipoResultadoValidacion = TipoResultadoValidacion.Default;
                        if (response.MT_SBK_ValidaCreditoClientes_Response.Resultado.Trim().CompareTo("0") == 0)
                        {
                            resultado.Resultado = false;
                            resultado.Mensaje = response.MT_SBK_ValidaCreditoClientes_Response.Mensaje;
                        }
                        else if (response.MT_SBK_ValidaCreditoClientes_Response.Resultado.Trim().CompareTo("1") == 0)
                        {
                            resultado.Resultado = true;
                            resultado.Mensaje = response.MT_SBK_ValidaCreditoClientes_Response.Mensaje;
                        }
                        else
                        {
                            resultado.Resultado = false;
                            resultado.Mensaje = Properties.ResourceServices.validarLimiteCredito_RespuestaServicioDesconocida.ToString().Trim(); 
                        }
                    }
                    else
                    {
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.ResourceServices.validarLimiteCredito_SinRespuestaServicio.ToString().Trim(); 
                    }
                    GC.SuppressFinalize(servicio);
                    GC.SuppressFinalize(request);
                    GC.SuppressFinalize(response);
                }
                else
                {
                    resultado.Resultado = true;
                }
                
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                resultado.Resultado = false;
                resultado.Mensaje = Properties.ResourceServices.validarLimiteCredito_ErrorConsumoServicio.ToString().Trim(); 
                //throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Genera una instancia configurada en base a parametros generales para el consumo del  webservice de limite de credito
        /// </summary>
        /// <returns></returns>
        public SI_OS_SBX_to_ECC_ValidaCreditoClientesClient obtenerInstanciaClienteValidaCredito()
        {
            SI_OS_SBX_to_ECC_ValidaCreditoClientesClient cliente = null;
            ParametroGeneralBL parametroGen = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral;
            EndpointAddress remoteAddress;
            BasicHttpBinding binding;
            string endpoint = string.Empty;
            string user = string.Empty;
            string pass = string.Empty;
            int timeOut = 0;

            parametroGeneral = parametroGen.ObtenerPorClaveParametro(ParametrosEnum.URLvalidacionLimiteCredito.ToString());
            endpoint = parametroGeneral.Valor.Trim();

            parametroGeneral = parametroGen.ObtenerPorClaveParametro(ParametrosEnum.UsuarioWebServiceLimiteCredito.ToString());
            user = parametroGeneral.Valor.Trim();

            parametroGeneral = parametroGen.ObtenerPorClaveParametro(ParametrosEnum.PassWebServiceLimiteCredito.ToString());
            pass = parametroGeneral.Valor.Trim();

            timeOut = ConfigurationManager.AppSettings.Get("timeOutLimiteCredito") != null ? int.Parse(ConfigurationManager.AppSettings.Get("timeOutLimiteCredito").ToString()) : 0;
            remoteAddress = new EndpointAddress(new Uri(endpoint));

            binding = new BasicHttpBinding
            {
                Name = "HTTP_Port",
                Security =
                {
                    Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                    Transport =
                    {
                        ClientCredentialType = HttpClientCredentialType.Basic,
                        ProxyCredentialType = HttpProxyCredentialType.None,
                    },
                    Message = new BasicHttpMessageSecurity { ClientCredentialType = BasicHttpMessageCredentialType.UserName}
                }
            };

            cliente = new SI_OS_SBX_to_ECC_ValidaCreditoClientesClient(binding, remoteAddress);
            cliente.ClientCredentials.UserName.UserName = user;
            cliente.ClientCredentials.UserName.Password = pass;
            if (timeOut > 0)
            {
                cliente.InnerChannel.OperationTimeout = new TimeSpan(0, 0, timeOut);
            }
            return cliente;
        }

        public int ObtenerTotalClientesActivos()
        {
            int result;
            try
            {
                Logger.Info();
                var clienteDAL = new ClienteDAL();
                result = clienteDAL.ObtenerTotalClientesActivos();
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
    }
}
