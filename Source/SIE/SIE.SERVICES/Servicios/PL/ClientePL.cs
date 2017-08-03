using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ClientePL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Cliente
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ClienteInfo info)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                int result = clienteBL.Guardar(info);
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
        public ResultadoInfo<ClienteInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                ResultadoInfo<ClienteInfo> result = clienteBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<ClienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                IList<ClienteInfo> result = clienteBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ClienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                IList<ClienteInfo> result = clienteBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        public ClienteInfo ObtenerPorID(int clienteID)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                ClienteInfo result = clienteBL.ObtenerPorID(clienteID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ClienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                ClienteInfo result = clienteBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<ClienteInfo> ObtenerClientesPorPagina(PaginacionInfo pagina, string descripcion)
        {
            ResultadoInfo<ClienteInfo> result;
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                result = clienteBL.ObtenerClientesPorPagina(pagina, descripcion);
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
        public ClienteInfo ObtenerClientePorCliente(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                result = clienteBL.ObtenerClientePorCliente(cliente);
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
        public ClienteInfo ObtenerClienteSAP(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                result = clienteBL.ObtenerClienteSAP(cliente);
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

        public ResultadoInfo<ClienteInfo> ObtenerActivosPorDescripcion(PaginacionInfo pagina, ClienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                ResultadoInfo<ClienteInfo> result = clienteBL.ObtenerActivosPorDescripcion(pagina,filtro);
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

        public ClienteInfo Obtener_Nombre_CodigoSAP_PorID(ClienteInfo cliente)
        {
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                result = clienteBL.Obtener_Nombre_CodigoSAP_PorID(cliente.ClienteID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public ClienteInfo Obtener_ActivoPorCodigoSAP(ClienteInfo cliente)
        {
            
            ClienteInfo result;
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                result = clienteBL.Obtener_ActivoPorCodigoSAP(cliente);
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

        public ResultadoValidacion validarLimiteCredito(ParametrosValidacionLimiteCredito datosValidacion)
        {
            ResultadoValidacion resultado = new ResultadoValidacion();
            ClienteBL clienteBL = new ClienteBL();
         
            try
            {
                Logger.Info();
                resultado = clienteBL.validarLimiteCredito(datosValidacion);
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

            return resultado;
        }

        public int ObtenerTotalClientesActivos()
        {
            int resultado = 0;
            ClienteBL clienteBL = new ClienteBL();

            try
            {
                Logger.Info();
                resultado = clienteBL.ObtenerTotalClientesActivos();
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

            return resultado;
        }

        public IList<ClienteInfo> ObtenerTodosActivos()
        {
            try
            {
                Logger.Info();
                var clienteBL = new ClienteBL();
                IList<ClienteInfo> result = clienteBL.ObtenerTodos(EstatusEnum.Activo);
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
