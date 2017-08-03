using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using System.Transactions;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Globalization;

namespace SIE.Services.Servicios.BL
{
    public class EntradaProductoBL 
    {
        /// <summary>
        /// Obtiene la lista de entrada de productos
        /// </summary>
        /// <returns>Lista de EntradaProductoInfo</returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodos(int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDAL.ObtenerEntradaProductosTodos(organizacionId);

                if (listaEntradaProducto != null)
                {
                    foreach (var entradaProducto in listaEntradaProducto)
                    {
                        if (entradaProducto.Organizacion.OrganizacionID > 0)
                        {
                            var organizacionBl = new OrganizacionBL();
                            entradaProducto.Organizacion = organizacionBl.ObtenerPorID(entradaProducto.Organizacion.OrganizacionID);
                        }

                        if (entradaProducto.Contrato.ContratoId > 0)
                        {
                            var contratoBl = new ContratoBL();
                            entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                        }

                        if (entradaProducto.RegistroVigilancia != null)
                        {
                            var registroVigilanciaBl = new RegistroVigilanciaBL();
                            entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.RegistroVigilancia =
                                registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);
                        }

                        if (entradaProducto.Producto.ProductoId > 0)
                        {
                            var productoBl = new ProductoBL();
                            entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);
                        }

                        if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                        {
                            var almacenBl = new AlmacenInventarioLoteBL();
                            entradaProducto.AlmacenInventarioLote =
                                almacenBl.ObtenerAlmacenInventarioLotePorId(
                                    entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                        }

                        if (entradaProducto.OperadorAnalista.OperadorID > 0)
                        {
                            var operadorBl = new OperadorBL();
                            entradaProducto.OperadorAnalista =
                                operadorBl.ObtenerPorID(entradaProducto.OperadorAnalista.OperadorID);
                        }

                        if (entradaProducto.OperadorBascula.OperadorID > 0)
                        {
                            var operadorBl = new OperadorBL();
                            entradaProducto.OperadorBascula =
                                operadorBl.ObtenerPorID(entradaProducto.OperadorBascula.OperadorID);
                        }

                        if (entradaProducto.OperadorAlmacen.OperadorID > 0)
                        {
                            var operadorBl = new OperadorBL();
                            entradaProducto.OperadorAlmacen =
                                operadorBl.ObtenerPorID(entradaProducto.OperadorAlmacen.OperadorID);
                        }

                        if (entradaProducto.OperadorAutoriza.OperadorID > 0)
                        {
                            var operadorBl = new OperadorBL();
                            entradaProducto.OperadorAutoriza =
                                operadorBl.ObtenerPorID(entradaProducto.OperadorAutoriza.OperadorID);
                        }

                        var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                        entradaProducto.ProductoDetalle =
                            entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                                entradaProducto.EntradaProductoId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene la lista de entradas de producto por estatusID
        /// </summary>
        /// <param name="estatusId"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodosPorEstatusId(int estatusId, int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDAL.ObtenerEntradaProductosTodosPorEstatusId(estatusId, organizacionId);

                if (listaEntradaProducto != null)
                {
                    foreach (var entradaProducto in listaEntradaProducto)
                    {
                        if (entradaProducto.Contrato.ContratoId > 0)
                        {
                            var contratoBl = new ContratoBL();
                            entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                        }

                        if (entradaProducto.RegistroVigilancia != null)
                        {
                            var registroVigilanciaBl = new RegistroVigilanciaBL();
                            entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.RegistroVigilancia =
                                registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);
                        }

                        if (entradaProducto.Producto.ProductoId > 0)
                        {
                            var productoBl = new ProductoBL();
                            entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);
                        }

                        if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                        {
                            var almacenBl = new AlmacenInventarioLoteBL();
                            entradaProducto.AlmacenInventarioLote =
                                almacenBl.ObtenerAlmacenInventarioLotePorId(
                                    entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                        }

                        var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                        entradaProducto.ProductoDetalle =
                            entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                                entradaProducto.EntradaProductoId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene la lista de entradas de producto por estatusID
        /// </summary>
        /// <param name="estatusId"></param>
        /// <param name="organizacionId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(EntradaProductoInfo entrada, int folio)
        {
            List<EntradaProductoInfo> listaEntradaProducto;


            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDAL.ObtenerEntradaProductosTodosPorEstatusId(entrada.Estatus.EstatusId, entrada.Organizacion.OrganizacionID);

                if (folio > 0 && listaEntradaProducto != null)
                {
                    //Filtra todos los folios que contengan el numero de folio capturado
                    listaEntradaProducto =
                        listaEntradaProducto.Where(
                            registro =>
                                registro.Folio.ToString(CultureInfo.InvariantCulture)
                                    .Contains(folio.ToString(CultureInfo.InvariantCulture))).ToList();

                }

                if (listaEntradaProducto != null)
                {
                    foreach (var entradaProducto in listaEntradaProducto)
                    {
                        if (entradaProducto.RegistroVigilancia != null)
                        {
                            var registroVigilanciaBl = new RegistroVigilanciaBL();
                            entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = entrada.Organizacion.OrganizacionID
                            };
                            entradaProducto.RegistroVigilancia =
                                registroVigilanciaBl.ObtenerRegistroVigilanciaPorIdAyudaForraje(entradaProducto.RegistroVigilancia);

                            if (string.IsNullOrEmpty(entradaProducto.Producto.Descripcion))
                            {
                                ProductoBL productoBl = new ProductoBL();
                                entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);
                            }
                            if (entradaProducto.Contrato.Folio==0)
                            {
                                ContratoBL contratoBl = new ContratoBL();
                                entradaProducto.Contrato.Organizacion = entradaProducto.Organizacion;
                                entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Actualiza la entrada de producto la primera vez que llega
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal bool ActualizarEntradaProductoLlegada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                return entradaProductoDal.ActualizarEntradaProductoLlegada(entradaProducto);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo GuardarEntradaProducto(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            EntradaProductoInfo entradaProductoNuevo = null;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                entradaProductoNuevo = entradaProductoDal.GuardarEntradaProducto(entradaProducto, tipoFolio);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return entradaProductoNuevo;
        }

        /// <summary>
        /// Actualiza una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool AutorizaEntrada(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                retorno = entradaProductoDal.AutorizaEntrada(entradaProducto);
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
        /// Obtiene las entradas producto por organizacion y folio para el filtro
        /// </summary>
        /// <param name="organizacionId"></param>
        /// /// <param name="folio"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoValido( int organizacionId, int folio)
        {
            List<EntradaProductoInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDal = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDal.ObtenerEntradaProductoValido(organizacionId, folio);

                if (listaEntradaProducto != null)
                {
                    foreach (var entradaProducto in listaEntradaProducto)
                    {
                        if (entradaProducto.Contrato.ContratoId > 0)
                        {
                            var contratoBl = new ContratoBL();
                            entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                        }

                        if (entradaProducto.RegistroVigilancia != null)
                        {
                            var registroVigilanciaBl = new RegistroVigilanciaBL();
                            entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                            entradaProducto.RegistroVigilancia =
                                registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);
                        }

                        if (entradaProducto.Producto.ProductoId > 0)
                        {
                            var productoBl = new ProductoBL();
                            entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);
                        }

                        if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                        {
                            var almacenBl = new AlmacenInventarioLoteBL();
                            entradaProducto.AlmacenInventarioLote =
                                almacenBl.ObtenerAlmacenInventarioLotePorId(
                                    entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                        }

                        var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                        entradaProducto.ProductoDetalle =
                            entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                                entradaProducto.EntradaProductoId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene las entradas producto por organizacion y folio para el filtro
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoValidoAyuda(EntradaProductoInfo entrada)
        {
            List<EntradaProductoInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDal = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDal.ObtenerEntradaProductoValidoAyuda(entrada);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Obtiene una entrada en base al registro de vigilancia
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="registroVigilanciaID"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductoPorRegistroVigilancia(int organizacionId,int registroVigilanciaID)
        {
            EntradaProductoInfo entradaProducto = null;

            try
            {
                List<EntradaProductoInfo> listaEntradaProducto = ObtenerEntradaProductosTodos(organizacionId);

                if (listaEntradaProducto != null)
                {
                    foreach (var entrada in listaEntradaProducto.Where(entrada => entrada.RegistroVigilancia.RegistroVigilanciaId == registroVigilanciaID))
                    {
                        entradaProducto = entrada;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }
        
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                costoLista = entradaProductoDAL.ObtenerFoliosPorPaginaParaEntradaMateriaPrima(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                costoLista = entradaProductoDAL.ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(pagina, filtro);
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
        /// Obtiene la entrada de materia prima por folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerPorFolioEntradaMateriaPrima(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo entradaProducto;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                entradaProducto = entradaProductoDAL.ObtenerPorFolioEntradaMateriaPrima(filtro);
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
        /// Obtiene la entrada de materia prima por folio para cancelacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerPorFolioEntradaCancelacion(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo entradaProducto;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                entradaProducto = entradaProductoDAL.ObtenerPorFolioEntradaCancelacion(filtro);
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
        /// Almacena la entrada de producto con su detalle y las muestras.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        internal EntradaProductoInfo GuardarEntradaProductoSinDetalle(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var entradaProductoDal = new EntradaProductoDAL();
                    EntradaProductoInfo entradaProductoNuevo = null;

                    entradaProductoNuevo = entradaProductoDal.GuardarEntradaProductoSinDetalle(entradaProducto, tipoFolio);

                    if (entradaProductoNuevo != null)
                    {
                        transaction.Complete();
                        return entradaProductoNuevo;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }

        /// <summary>
        /// Almacena la entrada de producto con su detalle y las muestras.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        internal EntradaProductoInfo GuardarEntradaProductoConDetalle(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var entradaProductoDal = new EntradaProductoDAL();
                    EntradaProductoInfo entradaProductoNuevo = null;

                    entradaProductoNuevo = entradaProductoDal.GuardarEntradaProductoSinDetalle(entradaProducto, tipoFolio);

                    if (entradaProductoNuevo != null)
                    {
                        if (entradaProductoNuevo.EntradaProductoId > 0)
                        {
                            entradaProducto.EntradaProductoId = entradaProductoNuevo.EntradaProductoId;

                            int detalleId = 0;
                            var productoDetalleBl = new EntradaProductoDetalleBL();
                            detalleId = productoDetalleBl.GuardarProductoDetalle(entradaProducto);

                            if (detalleId > 0)
                            {

                                var productoDetalle =
                                    (EntradaProductoDetalleInfo)
                                        (from entradaProductoDetalle in entradaProducto.ProductoDetalle
                                            select entradaProductoDetalle).First();

                                productoDetalle.EntradaProductoDetalleId = detalleId;
                                List<EntradaProductoDetalleInfo> productosDetalle =
                                    new List<EntradaProductoDetalleInfo>();
                                productosDetalle.Add(productoDetalle);

                                entradaProducto.ProductoDetalle = productosDetalle;

                                var productoMuestraBl = new EntradaProductoMuestraBL();
                                productoMuestraBl.GuardarEntradaProductoMuestra(entradaProducto);
                          }
                            
                        }
                        transaction.Complete();
                        return entradaProductoNuevo;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }

        /// <summary>
        /// Actualiza la entrada de producto con su detalle y las muestras.
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal bool ActualizarEntradaProductoConDetalle(EntradaProductoInfo entradaProducto)
        {
            bool resultado = true;
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var entradaProductoDal = new EntradaProductoDAL();

                    entradaProductoDal.ActualizarEntradaProductoSinDetalle(entradaProducto);

                    var productoDetalleBl = new EntradaProductoDetalleBL();
                    var entradaProductoDetalle = productoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(entradaProducto.EntradaProductoId);

                    foreach (var entradaDetalle in entradaProductoDetalle)
                    {
                        entradaDetalle.ProductoMuestras = (from productoDetalle in entradaProducto.ProductoDetalle
                                                           from productoMuestra in productoDetalle.ProductoMuestras
                                                           where productoDetalle.EntradaProductoDetalleId == productoMuestra.EntradaProductoDetalleId
                                                           select productoMuestra).ToList();
                    }

                    entradaProducto.ProductoDetalle = entradaProductoDetalle;

                    productoDetalleBl.ActualizarProductoDetalle(entradaProducto);

                    var productoMuestraBl = new EntradaProductoMuestraBL();
                    productoMuestraBl.GuardarEntradaProductoMuestra(entradaProducto);

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para actualizar el lote en patio y las piezas en caso de que sea forraje
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool ActualizaLoteEnPatio(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                retorno = entradaProductoDal.ActualizaLoteEnPatio(entradaProducto);
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
        /// Método para actualizar la fecha de inicio y fin de descarga
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        internal string ActualizaFechaDescargaPiezasEnPatio(EntradaProductoInfo entrada)
        {
            string retorno = "";

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                retorno = entradaProductoDal.ActualizaFechaDescargaPiezasEnPatio(entrada);
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
        ///     Obtiene un lista paginada de los folios de entrada ganado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerPorProductoEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                costoLista = entradaProductoDAL.ObtenerPorProductoEntradaMateriaPrima(pagina, filtro);
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
        /// Actualiza el movimiento de almacen
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="almacenMovimientoInfo"></param>
        internal bool ActualizarAlmacenMovimiento(EntradaProductoInfo entradaProducto,AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            bool retorno = false;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                retorno = entradaProductoDal.ActualizarAlmacenMovimiento(entradaProducto, almacenMovimientoInfo);
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
        /// Obtiene una lista donde coincidan los folio
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductosPorFolio(int organizacionId, int folio)
        {
            EntradaProductoInfo entradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                entradaProducto = entradaProductoDAL.ObtenerEntradaProductoPorFolio(organizacionId, folio);

                if (entradaProducto != null)
                {
                    if (entradaProducto.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        entradaProducto.Organizacion =
                            organizacionBl.ObtenerPorID(entradaProducto.Organizacion.OrganizacionID);
                    }

                    if (entradaProducto.Contrato.ContratoId > 0)
                    {
                        var contratoBl = new ContratoBL();
                        entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = organizacionId
                        };
                        entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                    }
                    
                    
                    if (entradaProducto.RegistroVigilancia != null)
                    {
                        var registroVigilanciaBl = new RegistroVigilanciaBL();
                        entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = organizacionId
                        };
                        entradaProducto.RegistroVigilancia =
                            registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);

                        if (entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID > 0 && entradaProducto.Contrato != null)
                        {
                            var proveedorBl = new ProveedorBL();
                            entradaProducto.Contrato.Proveedor =
                                proveedorBl.ObtenerPorID(entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID);
                            entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                        }

                    }

                    if (entradaProducto.Producto.ProductoId > 0)
                    {
                        var productoBl = new ProductoBL();
                        entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);

                        //Validar si es una premezcla y obtener sus datos
                        if (entradaProducto.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                        {
                            var premezclaBL = new PremezclaBL();
                            var premezclaInfo = new PremezclaInfo
                            {
                                Producto = entradaProducto.Producto,
                                Organizacion = new OrganizacionInfo{OrganizacionID = organizacionId},
                                Activo = EstatusEnum.Activo
                            };
                            //Se obtiene la configuracion de la premezcla
                            entradaProducto.PremezclaInfo = premezclaBL.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
                            //Se se encuentra la premezcla configurada obtener los lotes de los productos q la componen
                            if (entradaProducto.PremezclaInfo != null &&
                                entradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos != null)
                            {
                                var inventarioLoteBl = new AlmacenInventarioLoteBL();
                                foreach (var premezclaDetalle in entradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos)
                                {
                                    premezclaDetalle.LotesDisponibles = inventarioLoteBl
                                        .ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto
                                        (new ParametrosOrganizacionTipoAlmacenProductoActivo
                                        {
                                            Activo = 1,
                                            OrganizacionId = entradaProducto.Organizacion.OrganizacionID,
                                            ProductoId = premezclaDetalle.Producto.ProductoId,
                                            TipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas
                                        }) ?? new List<AlmacenInventarioLoteInfo>();

                                    premezclaDetalle.LotesDisponibles.Insert(0,
                                        new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = 0, Lote = 0 });
                                }
                            }

                        }

                    }

                    if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                    {
                        var almacenBl = new AlmacenInventarioLoteBL();
                        entradaProducto.AlmacenInventarioLote =
                            almacenBl.ObtenerAlmacenInventarioLotePorId(
                                entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    }

                    if (entradaProducto.OperadorAnalista.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAnalista =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAnalista.OperadorID);
                    }

                    if (entradaProducto.OperadorBascula.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorBascula =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorBascula.OperadorID);
                    }

                    if (entradaProducto.OperadorAlmacen.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAlmacen =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAlmacen.OperadorID);
                    }

                    if (entradaProducto.OperadorAutoriza.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAutoriza =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAutoriza.OperadorID);
                    }

                    var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                    entradaProducto.ProductoDetalle =
                        entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                            entradaProducto.EntradaProductoId);

                    if (entradaProducto.AlmacenMovimientoSalida != null)
                    {
                        AlmacenMovimientoBL almacenMovimientoBl = new AlmacenMovimientoBL();
                        entradaProducto.AlmacenMovimientoSalida = almacenMovimientoBl.ObtenerPorId(entradaProducto.AlmacenMovimientoSalida.AlmacenMovimientoID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }

        /// <summary>
        /// Obtiene una lista donde coincidan los folio del Registro Vigilancia
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductoPorRegistroVigilanciaID(int organizacionId, int folio)
        {
            EntradaProductoInfo entradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                entradaProducto = entradaProductoDAL.ObtenerEntradaProductoPorRegistroVigilanciaID(organizacionId, folio);

                if (entradaProducto != null)
                {
                    if (entradaProducto.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        entradaProducto.Organizacion =
                            organizacionBl.ObtenerPorID(entradaProducto.Organizacion.OrganizacionID);
                    }

                    if (entradaProducto.Contrato.ContratoId > 0)
                    {
                        var contratoBl = new ContratoBL();
                        entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = organizacionId
                        };
                        entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                    }


                    if (entradaProducto.RegistroVigilancia != null)
                    {
                        var registroVigilanciaBl = new RegistroVigilanciaBL();
                        entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = organizacionId
                        };
                        entradaProducto.RegistroVigilancia =
                            registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);

                        if (entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID > 0)
                        {
                            var proveedorBl = new ProveedorBL();
                            entradaProducto.Contrato.Proveedor =
                                proveedorBl.ObtenerPorID(entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.ProveedorID);
                            entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = organizacionId
                            };
                        }

                    }

                    if (entradaProducto.Producto.ProductoId > 0)
                    {
                        var productoBl = new ProductoBL();
                        entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);

                        //Validar si es una premezcla y obtener sus datos
                        if (entradaProducto.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                        {
                            var premezclaBL = new PremezclaBL();
                            var premezclaInfo = new PremezclaInfo
                            {
                                Producto = entradaProducto.Producto,
                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                                Activo = EstatusEnum.Activo
                            };
                            //Se obtiene la configuracion de la premezcla
                            entradaProducto.PremezclaInfo = premezclaBL.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
                            //Se se encuentra la premezcla configurada obtener los lotes de los productos q la componen
                            if (entradaProducto.PremezclaInfo != null &&
                                entradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos != null)
                            {
                                var inventarioLoteBl = new AlmacenInventarioLoteBL();
                                foreach (var premezclaDetalle in entradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos)
                                {
                                    premezclaDetalle.LotesDisponibles = inventarioLoteBl
                                        .ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto
                                        (new ParametrosOrganizacionTipoAlmacenProductoActivo
                                        {
                                            Activo = 1,
                                            OrganizacionId = entradaProducto.Organizacion.OrganizacionID,
                                            ProductoId = premezclaDetalle.Producto.ProductoId,
                                            TipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas
                                        }) ?? new List<AlmacenInventarioLoteInfo>();

                                    premezclaDetalle.LotesDisponibles.Insert(0,
                                        new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = 0, Lote = 0 });
                                }
                            }

                        }

                    }

                    if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                    {
                        var almacenBl = new AlmacenInventarioLoteBL();
                        entradaProducto.AlmacenInventarioLote =
                            almacenBl.ObtenerAlmacenInventarioLotePorId(
                                entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    }

                    if (entradaProducto.OperadorAnalista.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAnalista =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAnalista.OperadorID);
                    }

                    if (entradaProducto.OperadorBascula.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorBascula =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorBascula.OperadorID);
                    }

                    if (entradaProducto.OperadorAlmacen.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAlmacen =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAlmacen.OperadorID);
                    }

                    if (entradaProducto.OperadorAutoriza.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAutoriza =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAutoriza.OperadorID);
                    }

                    var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                    entradaProducto.ProductoDetalle =
                        entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                            entradaProducto.EntradaProductoId);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }

        /// <summary>
        /// Obtiene la entrada produrcto por su identificador
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductosPorId(int entradaProductoId)
        {
            EntradaProductoInfo entradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                entradaProducto = entradaProductoDAL.ObtenerEntradaProductoPorId(entradaProductoId);

                if (entradaProducto != null)
                {
                    if (entradaProducto.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        entradaProducto.Organizacion =
                            organizacionBl.ObtenerPorID(entradaProducto.Organizacion.OrganizacionID);
                    }

                    if (entradaProducto.Contrato.ContratoId > 0)
                    {
                        var contratoBl = new ContratoBL();
                        entradaProducto.Contrato.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = entradaProducto.Organizacion.OrganizacionID
                        };
                        entradaProducto.Contrato = contratoBl.ObtenerPorId(entradaProducto.Contrato);
                    }

                    if (entradaProducto.RegistroVigilancia != null)
                    {
                        var registroVigilanciaBl = new RegistroVigilanciaBL();
                        entradaProducto.RegistroVigilancia.Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = entradaProducto.Organizacion.OrganizacionID
                        };
                        entradaProducto.RegistroVigilancia =
                            registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(entradaProducto.RegistroVigilancia);
                    }

                    if (entradaProducto.Producto.ProductoId > 0)
                    {
                        var productoBl = new ProductoBL();
                        entradaProducto.Producto = productoBl.ObtenerPorID(entradaProducto.Producto);
                    }

                    if (entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                    {
                        var almacenBl = new AlmacenInventarioLoteBL();
                        entradaProducto.AlmacenInventarioLote =
                            almacenBl.ObtenerAlmacenInventarioLotePorId(
                                entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    }

                    if (entradaProducto.OperadorAnalista.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAnalista =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAnalista.OperadorID);
                    }

                    if (entradaProducto.OperadorBascula.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorBascula =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorBascula.OperadorID);
                    }

                    if (entradaProducto.OperadorAlmacen.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAlmacen =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAlmacen.OperadorID);
                    }

                    if (entradaProducto.OperadorAutoriza.OperadorID > 0)
                    {
                        var operadorBl = new OperadorBL();
                        entradaProducto.OperadorAutoriza =
                            operadorBl.ObtenerPorID(entradaProducto.OperadorAutoriza.OperadorID);
                    }

                    var entradaProductoDetalleBl = new EntradaProductoDetalleBL();
                    entradaProducto.ProductoDetalle =
                        entradaProductoDetalleBl.ObtenerDetalleEntradaProductosPorIdEntrada(
                            entradaProducto.EntradaProductoId);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }
        /// <summary>
        /// Obtiene el listado de productos sin el detalle para mostrarlos en la ayuda
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosAyuda(int organizacionId)
        {
            try
            {
                var entradaProductoDAL = new EntradaProductoDAL();
                return entradaProductoDAL.ObtenerEntradaProductosAyuda(organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene contrato por organizacion
        /// </summary>
        /// <param name="contratoID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerPorContratoOrganizacion(int contratoID, int organizacionID)
        {
            List<EntradaProductoInfo> resultado = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                resultado = entradaProductoDal.ObtenerPorContratoOrganizacion(contratoID, organizacionID);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un contenedor de Entrada de Materia Prima
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="contratoId"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ContenedorEntradaMateriaPrimaInfo ObtenerPorFolioEntradaContrato(int folioEntrada, int contratoId, int organizacionID)
        {
            ContenedorEntradaMateriaPrimaInfo resultado = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                resultado = entradaProductoDal.ObtenerPorFolioEntradaContrato(folioEntrada, contratoId, organizacionID);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        internal List<DiferenciasIndicadoresMuestraContrato> ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(int entradaProductoId)
        {
            List<DiferenciasIndicadoresMuestraContrato> resultado = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                resultado = entradaProductoDal.ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(entradaProductoId);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene entradas de producto por folio
        /// </summary>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoPorContratoId(EntradaProductoInfo entradaProducto)
        {
            List<EntradaProductoInfo> listaEntradaProducto;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                listaEntradaProducto = entradaProductoDal.ObtenerEntradaProductoPorContratoId(entradaProducto);
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
        internal List<EntradaProductoInfo> ObtenerEntradaProductoPorContrato(ContratoInfo contratoInfo)
        {
            List<EntradaProductoInfo> resultado = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                resultado = entradaProductoDal.ObtenerEntradaProductoPorContrato(contratoInfo);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con las entradas de producto
        /// </summary>
        /// <param name="movimientosEntrada"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradasPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> movimientosEntrada)
        {
            IEnumerable<EntradaProductoInfo> resultado = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                resultado = entradaProductoDal.ObtenerEntradasPorAlmacenMovimientoXML(movimientosEntrada);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado.ToList();
        }

		/// <summary>
        /// Funcion que consulta los folios que se pueden cancelar del tipo de entrada por compra
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> listaEntradaCompra;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                listaEntradaCompra = entradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(pagina, filtro);
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
            return listaEntradaCompra;
        }

        /// <summary>
        /// Funcion que consulta los folios que se pueden cancelar del tipo de entrada por compra
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> listaEntradaCompra;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new EntradaProductoDAL();
                listaEntradaCompra = entradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(pagina, filtro);
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
            return listaEntradaCompra;
        }

        /// <summary>
        /// Obtiene la cantidad de notificaciones autorizadas
        /// por autorizar
        /// </summary>
        /// <returns></returns>
        internal int ObtenerCantidadNotificacionesAutorizadas(int organizacionID)
        {
            int cantidadNotificacionesAutorizadas = 0;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                cantidadNotificacionesAutorizadas = entradaProductoDal.ObtenerCantidadNotificacionesAutorizadas(organizacionID);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return cantidadNotificacionesAutorizadas;
        }

        /// <summary>
        /// Obtiene las notificaciones autorizadas
        /// </summary>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerNotificacionesAutorizadas(int organizacionID)
        {
            List<EntradaProductoInfo> notificacionesAutorizadas = null;
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                notificacionesAutorizadas = entradaProductoDal.ObtenerNotificacionesAutorizadas(organizacionID);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return notificacionesAutorizadas;
        }

        /// <summary>
        /// Metodo para actualizar el operador y la fecha de inicio de descarga.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool ActualizaOperadorFechaDescargaEnPatio(EntradaProductoInfo entradaProducto)
        {
            bool retorno = false;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
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
        /// Actualiza bandera de revisado por generente de Planta de Alimentos
        /// </summary>
        /// <param name="entradaProductoId"></param>
        internal void ActualizaRevisionGerente(int entradaProductoId)
        {
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                entradaProductoDal.ActualizaRevisionGerente(entradaProductoId);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public decimal ObtenerHumedadOrigen(EntradaProductoInfo entradaProducto)
        {
            decimal retorno = 0;

            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                retorno = entradaProductoDal.ObtenerHumedadOrigen(entradaProducto);
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

        public void ActualizarDescuentoEntradaProductoMuestra(EntradaProductoInfo entradaProducto, decimal descuento)
        {
            try
            {
                Logger.Info();
                var entradaProductoDal = new EntradaProductoDAL();
                entradaProductoDal.ActualizarDescuentoEntradaProductoMuestra(entradaProducto, descuento);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
