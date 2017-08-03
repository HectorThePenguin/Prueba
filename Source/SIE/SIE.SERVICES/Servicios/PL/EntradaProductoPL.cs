using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaProductoPL 
    {
        /// <summary>
        /// Obtiene la lista de entradas de producto
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductosTodos(int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductosTodos(organizacionId);
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

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene la lista de entradas de producto por el estatusID
        /// </summary>
        /// <param name="estatusId"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductosTodosPorEstatusId(int estatusId, int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductosTodosPorEstatusId(estatusId, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene la lista de entradas de producto por el estatusID
        /// </summary>
        /// <param name="estatusId"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(EntradaProductoInfo entrada, int folio)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(entrada, folio);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene una entrada en base al folio seleccionado
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public EntradaProductoInfo ObtenerEntradaProductoPorFolio(int folio, int organizacionId)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                return entradaProductoBl.ObtenerEntradaProductosPorFolio(organizacionId, folio);
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
        /// Obtiene una entrada en base al folio seleccionado
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public EntradaProductoInfo ObtenerEntradaProductoPorRegistroVigilanciaID(int folio, int organizacionId)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                return entradaProductoBl.ObtenerEntradaProductoPorRegistroVigilanciaID(organizacionId, folio);
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
        /// Actualiza la entrada de producto la primera vez que llega
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public bool ActualizarEntradaProductoLlegada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                return entradaProductoBl.ActualizarEntradaProductoLlegada(entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            
            return false;
        }

        /// <summary>
        /// Guarda una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        public EntradaProductoInfo GuardarEntradaProducto(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            EntradaProductoInfo entradaProductoNuevo = null;

            try
            {
                var entradaProductoBL = new EntradaProductoBL();
                entradaProductoNuevo = entradaProductoBL.GuardarEntradaProducto(entradaProducto, tipoFolio);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return entradaProductoNuevo;
        }

        /// <summary>
        /// Actualiza un registro de entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        public bool AutorizaEntrada(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                var entradaProductoBL = new EntradaProductoBL();
                retorno = entradaProductoBL.AutorizaEntrada(entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return retorno;
        }

        /// <summary>
        /// Obtiene las entradas de producto validos filtrados por folio.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// /// <param name="folio"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductoValido(int organizacionId, int folio)
        {
            List<EntradaProductoInfo> entradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                entradaProducto = entradaProductoBl.ObtenerEntradaProductoValido(organizacionId, folio);
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

            return entradaProducto;
        }

        /// <summary>
        /// Obtiene las entradas de producto validos filtrados por folio.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductoValidoAyuda(EntradaProductoInfo entrada)
        {
            List<EntradaProductoInfo> entradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                entradaProducto = entradaProductoBl.ObtenerEntradaProductoValidoAyuda(entrada);
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

            return entradaProducto;
        }

        /// <summary>
        ///     Obtiene un lista paginada de los folios de entrada ganado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoBL = new EntradaProductoBL();
                costoLista = entradaProductoBL.ObtenerFoliosPorPaginaParaEntradaMateriaPrima(pagina, filtro);
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un lista paginada de los folios para cancelar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> listaEntradasCancelacion;
            try
            {
                Logger.Info();
                var entradaProductoBL = new EntradaProductoBL();
                listaEntradasCancelacion = entradaProductoBL.ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(pagina, filtro);
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
            return listaEntradasCancelacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de los folios para cancelar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> listaEntradasCancelacion;
            try
            {
                Logger.Info();
                var entradaProductoBL = new EntradaProductoBL();
                listaEntradasCancelacion = entradaProductoBL.ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(pagina, filtro);
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
            return listaEntradasCancelacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de los folios de entrada ganado por Estatus 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoBL = new EntradaProductoBL();
                costoLista = entradaProductoBL.ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(pagina, filtro);
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
            return costoLista;
        }

        /// <summary>
        /// Obtiene una lista paginada de los folios por producto de entrada ganado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaProductoInfo> ObtenerPorProductoEntradaMateriaPrima(PaginacionInfo pagina,
            EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoBL = new EntradaProductoBL();
                costoLista = entradaProductoBL.ObtenerPorProductoEntradaMateriaPrima(pagina, filtro);
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
            return costoLista;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public EntradaProductoInfo ObtenerPorFolioPorEntradaMateriaPrima(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo entradaMateriaPrima;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaProductoBL();
                entradaMateriaPrima = entradaGanadoBL.ObtenerPorFolioEntradaMateriaPrima(filtro);
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
            return entradaMateriaPrima;
        }

        /// <summary>
        /// OBtiene una entrada de producto sin validar los costos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public EntradaProductoInfo ObtenerPorFolioPorEntradaCancelacion(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo entradaMateriaPrima;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaProductoBL();
                entradaMateriaPrima = entradaGanadoBL.ObtenerPorFolioEntradaCancelacion(filtro);
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
            return entradaMateriaPrima;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public EntradaProductoInfo ObtenerPorFolioPorEntradaMateriaPrimaEstatus(EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> listaEntradaProductoInfo;
            EntradaProductoInfo entradaProductoInfo = null;
            try
            {
                Logger.Info();
                PaginacionInfo pagina = new PaginacionInfo();
                pagina.Inicio = 1;
                pagina.Limite = 1;
                var entradaGanadoBL = new EntradaProductoBL();
                listaEntradaProductoInfo = entradaGanadoBL.ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(pagina,filtro);
                if (listaEntradaProductoInfo != null)
                {
                    entradaProductoInfo = listaEntradaProductoInfo.Lista[0];
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
            return entradaProductoInfo;
        }

        /// <summary>
        /// Obtiene la entrada del producto segun el registro de vigilancia.
        /// </summary>
        /// <param name="registroVigilancia"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        public EntradaProductoInfo ObtenerEntradaProductoPorRegistroVigilancia(RegistroVigilanciaInfo registroVigilancia)
        {
            EntradaProductoInfo entradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                int organizacionId = registroVigilancia.Organizacion.OrganizacionID;
                int registroVigilanciaId = registroVigilancia.RegistroVigilanciaId;

                entradaProducto = entradaProductoBl.ObtenerEntradaProductoPorRegistroVigilancia(organizacionId, registroVigilanciaId);
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

            return entradaProducto;
        }

        /// <summary>
        /// Guarda una entrada de producto con todo su detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        public EntradaProductoInfo GuardarEntradaProductoConDetalle(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            try
            {
                var entradaProductoBL = new EntradaProductoBL();
                return entradaProductoBL.GuardarEntradaProductoConDetalle(entradaProducto,tipoFolio);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return null;
        }

        /// <summary>
        /// Actualizar una entrada de producto con todo su detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        public bool ActualizarEntradaProductoConDetalle(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                var entradaProductoBL = new EntradaProductoBL();
                retorno = entradaProductoBL.ActualizarEntradaProductoConDetalle(entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return retorno;
        }

        /// <summary>
        /// Metodo para actualizar el lote en patio y las piezas en caso de que sea forraje
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        public bool ActualizaLoteEnPatio(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                retorno = entradaProductoBl.ActualizaLoteEnPatio(entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }

        /// <summary>
        /// Método para actualizar la fecha de inicio y fin de descarga
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public string ActualizaFechaDescargaPiezasEnPatio(EntradaProductoInfo entrada)
        {
            string retorno = "";

            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                retorno = entradaProductoBl.ActualizaFechaDescargaPiezasEnPatio(entrada);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }
        /// <summary>
        /// Obtiene la entrada producto por su identificador
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        public EntradaProductoInfo ObtenerEntradaProductoPorId(int entradaProductoId)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                return entradaProductoBl.ObtenerEntradaProductosPorId(entradaProductoId);
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
        /// Obtiene el listado de productos sin el detalle para mostrarlos en la ayuda
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductosAyuda(int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductosAyuda(organizacionId);
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

            return listaEntradaProducto;
        }

        public List<DiferenciasIndicadoresMuestraContrato> ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(int entradaProductoId)
        {
            List<DiferenciasIndicadoresMuestraContrato> listaDiferenciasIndicadores;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaDiferenciasIndicadores = entradaProductoBl.ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(entradaProductoId);
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

            return listaDiferenciasIndicadores;
        }

        /// <summary>
        /// Obtiene entradas de producto por contrato
        /// </summary>
        /// <returns></returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductoPorContratoId(EntradaProductoInfo entradaProducto)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            try
            {
                Logger.Info();
                var entradaProductoBl = new EntradaProductoBL();
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductoPorContratoId(entradaProducto);
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
            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene una lista de entradas de producto por contrato
        /// Esta funcion solo consulta EntradaProducto
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        public List<EntradaProductoInfo> ObtenerEntradaProductoPorContrato(ContratoInfo contratoInfo)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            var entradaProductoBl = new EntradaProductoBL();

            try
            {
                listaEntradaProducto = entradaProductoBl.ObtenerEntradaProductoPorContrato(contratoInfo);
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

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene una lista con las entradas de producto
        /// </summary>
        /// <param name="movimientosEntrada"></param>
        /// <returns></returns>
        public List<EntradaProductoInfo> ObtenerEntradasPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> movimientosEntrada)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                listaEntradaProducto = entradaProductoBl.ObtenerEntradasPorAlmacenMovimientoXML(movimientosEntrada);
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
            return listaEntradaProducto;
        }

        /// <summary>
        /// Metodo para actualizar el operador y la fecha de inicio de descarga.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        public bool ActualizaOperadorFechaDescargaEnPatio(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoBL();
                retorno = entradaProductoDal.ActualizaOperadorFechaDescargaEnPatio(entradaProducto);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return retorno;
        }

        /// <summary>
        /// Obtiene la cantidad de notificaciones autorizadas
        /// por autorizar
        /// </summary>
        /// <returns></returns>
        public int ObtenerCantidadNotificacionesAutorizadas(int organizacionID)
        {
            int cantidadNotificacionesAutorizadas;
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                cantidadNotificacionesAutorizadas = entradaProductoBl.ObtenerCantidadNotificacionesAutorizadas(organizacionID);
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
            return cantidadNotificacionesAutorizadas;
        }

        /// <summary>
        /// Obtiene las notificaciones autorizadas
        /// </summary>
        /// <returns></returns>
        public List<EntradaProductoInfo> ObtenerNotificacionesAutorizadas(int organizacionID)
        {
            List<EntradaProductoInfo> notificacionesAutorizadas;
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                notificacionesAutorizadas = entradaProductoBl.ObtenerNotificacionesAutorizadas(organizacionID);
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
            return notificacionesAutorizadas;
        }

        /// <summary>
        /// Actualiza bandera de revisado por generente de Planta de Alimentos
        /// </summary>
        /// <param name="entradaProductoId"></param>
        public void ActualizaRevisionGerente(int entradaProductoId)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                entradaProductoBl.ActualizaRevisionGerente(entradaProductoId);
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

        public decimal ObtenerHumedadOrigen(EntradaProductoInfo entradaProducto)
        {
            decimal result;
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                result = entradaProductoBl.ObtenerHumedadOrigen(entradaProducto);
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

        public void ActualizarDescuentoEntradaProductoMuestra(EntradaProductoInfo entradaProducto, decimal descuento)
        {
            try
            {
                var entradaProductoBl = new EntradaProductoBL();
                entradaProductoBl.ActualizarDescuentoEntradaProductoMuestra(entradaProducto, descuento);
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
