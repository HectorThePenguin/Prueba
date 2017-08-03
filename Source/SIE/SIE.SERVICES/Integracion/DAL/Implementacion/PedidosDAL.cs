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
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PedidosDAL: DALBase
    {
        /// <summary>
        /// Obtiene los pedidos con estatus programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosProgramadosYParciales(PedidoInfo pedido)
        {
            List<PedidoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPedidoDAL.ObtenerParametrosPedidosProgramadosYParciales(pedido);
                DataSet ds = Retrieve("Pedido_ObtenerPedidosPorDosEstatus", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPedidosDAL.ObtenerPedidosProgramadosYParciales(ds);
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
        /// Obtiene todos los pedidos activos o inactivos.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosTodos(int organizacionId,EstatusEnum estatus)
        {
            List<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerTodos(organizacionId,estatus);

                DataSet ds = Retrieve("Pedido_ObtenerTodos", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosTodos(ds);
                }

            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Obtiene los pedidos paginados que coincidan con el folio pedido. 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFolioPaginado(PaginacionInfo pagina,PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerPedidosPorFolioPaginado(pagina, pedido);

                DataSet ds = Retrieve("Pedido_ObtenerFoliosPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosPorFolioPaginado(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Obtiene el pedido por folio pedido.
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidoPorFolio(int folioPedido,int organizacionId)
        {
            PedidoInfo pedido = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerPorFolio(folioPedido,organizacionId);

                DataSet ds = Retrieve("Pedido_ObtenerPorFolio", parametros);
                if (ValidateDataSet(ds))
                {
                    pedido = MapPedidosDAL.ObtenerPedidoPorFolio(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return pedido;
        }

        /// <summary>
        /// Obtiene los pedidos programados para 
        /// la funcionalidad de Calidad Pase a Proceso
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosProgramadosPaginado(PaginacionInfo paginacion, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosPedidosProgramadosPaginado(paginacion, pedido);
                DataSet ds = Retrieve("Pedido_ObtenerFolioPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosProgramadosPaginado(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los pedidos programados para 
        /// la funcionalidad de Calidad Pase a Proceso
        /// </summary>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidosProgramadosPorFolioPedido(int folioPedido, int organizacionID)
        {
            PedidoInfo resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerPedidosProgramadosPorFolioPedido(folioPedido, organizacionID);
                DataSet ds = Retrieve("Pedido_ObtenerFolioPorFolioPedido", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosProgramadosPorFolioPedido(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal ResultadoInfo<PedidoInfo> ObtenerPorFolioProgramadoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosPorFolioPaginado(pagina, pedido);
                DataSet ds = Retrieve("Pedido_ObtenerFolioPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosProgramadosPaginado(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el pedido por folio pedido.
        /// </summary>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidoPorFolio(PedidoInfo pedidoInfo)
        {
            PedidoInfo pedido = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerPorFolio(pedidoInfo);

                DataSet ds = Retrieve("Pedido_ObtenerPorFolio", parametros);
                if (ValidateDataSet(ds))
                {
                    pedido = MapPedidosDAL.ObtenerPedidoPorFolio(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return pedido;
        }

        /// <summary>
        /// Metodo que obtiene pedidos 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltro(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerPedidosPorFiltro(pagina, pedido);

                DataSet ds = Retrieve("Pedido_ObtenerPorFiltro", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosPorFolioPaginado(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene pedidos 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltroCancelacion(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPedidoDAL.ObtenerParametrosObtenerPedidosPorFiltroCancelacion(pagina, pedido);

                DataSet ds = Retrieve("Pedido_ObtenerPorFiltroCancelacion", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosPorFolioPaginado(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }
        

        /// <summary>
        /// Metodo que actualiza estatus de un pedido
        /// </summary>
        internal void ActualizarEstatusPedido(PedidoInfo pedidoInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPedidoDAL.ObtenerParametrosActualizarEstatusPedido(pedidoInfo);
                Update("Pedido_ActualizarEstatusPedido", parameters);
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
        /// Obtiene un registro por ticket
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidoPorTicketPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoDAL.ObtenerParametrosObtenerPedidoPorTicketPesaje(pesajeMateriaPrimaInfo, organizacionId);
                var ds = Retrieve("Pedido_ObtenerPedidoPorTicket", parameters);
                PedidoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidosDAL.ObtenerPedidoPorTicketPesaje(ds);
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
        /// Crea un registro para la tabla pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal PedidoInfo Crear(PedidoInfo pedido, TipoFolio tipoFolio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPedidoDAL.ObtenerParametrosCrear(pedido,tipoFolio);
                var ds = Retrieve("Pedido_Crear", parameters);
                PedidoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidosDAL.Crear(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene pedidos por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosCompletoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPedidoDAL.ObtenerParametrosPedidoCompletoPaginado(pagina,
                                                                                                             pedido);
                DataSet ds = Retrieve("Pedido_ObtenerPorPaginaRecibido", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidoCompletoPaginado(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un pedido por folio
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidosCompleto(PedidoInfo pedido)
        {
            PedidoInfo resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPedidoDAL.ObtenerParametrosPedidoCompleto(pedido);
                DataSet ds = Retrieve("Pedido_ObtenerRecibido", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidoCompleto(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un pedido por folio
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosPendientesPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPedidoDAL.ObtenerPedidosPendientesPorLote(almacenInventarioLoteID);
                DataSet ds = Retrieve("EntradaMateriaPrima_ObtenerPedidoPendienteLote", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosPendientesPorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un pedido por folio
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosProgramadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPedidoDAL.ObtenerPedidosProgramadosPorLote(almacenInventarioLoteID);
                DataSet ds = Retrieve("ProgramacionMateriaPrima_ObtenerProgramacionLote", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosProgramadosPorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los Pedidos que tenga pendiente el Lote
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosEntregadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPedidoDAL.ObtenerPedidosEntregadosPorLote(almacenInventarioLoteID);
                DataSet ds = Retrieve("Pedido_ObtenerCantidadEntregadaLote", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPedidosDAL.ObtenerPedidosEntregadosPorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public int ObtenerPedidosProgramadosPorLoteCantidadProgramada(int almacenInventarioLoteId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPedidoDAL.ObtenerParametrosObtenerPedidosProgramadosPorLoteCantidadProgramada(almacenInventarioLoteId);
                int result = Create("AlmacenInventarioLote_ObtenerLotesCantidadProgramada", parameters);
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
