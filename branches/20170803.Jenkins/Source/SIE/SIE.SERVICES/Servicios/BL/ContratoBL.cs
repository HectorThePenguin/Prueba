using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ContratoBL
    {

        /// <summary>
        /// Metodo que guarda o actualiza un contrato
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal ContratoInfo Guardar(ContratoInfo info, int tipoFolio)
        {
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                ContratoInfo result = contratoDal.Crear(info, tipoFolio);
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
        /// Metodo que actualiza el estado de un contrato
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstado(ContratoInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                contratoDal.ActualizarEstado(info, estatus);
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
        /// Metrodo para obtener un listado de contratos por estado
        /// </summary>
        internal List<ContratoInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                result = contratoDAL.ObtenerPorEstado(estatus);
                if (result != null)
                {
                    foreach (var contratoInfo in result)
                    {
                        if (contratoInfo.Organizacion.OrganizacionID > 0)
                        {
                            var organizacionBl = new OrganizacionBL();
                            contratoInfo.Organizacion =
                                organizacionBl.ObtenerPorID(contratoInfo.Organizacion.OrganizacionID);
                        }

                        if (contratoInfo.Producto.ProductoId > 0)
                        {
                            var productoBl = new ProductoBL();
                            contratoInfo.Producto = productoBl.ObtenerPorID(contratoInfo.Producto);
                        }

                        if (contratoInfo.Proveedor.ProveedorID > 0)
                        {
                            var proveedorBl = new ProveedorBL();
                            contratoInfo.Proveedor =
                                proveedorBl.ObtenerPorID(contratoInfo.Proveedor.ProveedorID);
                        }

                        if (contratoInfo.TipoContrato.TipoContratoId > 0)
                        {
                            var tipoContratoBl = new TipoContratoBL();
                            contratoInfo.TipoContrato =
                                tipoContratoBl.ObtenerPorId(contratoInfo.TipoContrato.TipoContratoId);
                        }

                        if (contratoInfo.TipoFlete.TipoFleteId > 0)
                        {
                            var tipoFleteBl = new TipoFleteBL();
                            contratoInfo.TipoFlete =
                                tipoFleteBl.ObtenerPorId(contratoInfo.TipoFlete.TipoFleteId);
                        }

                        if (contratoInfo.ContratoId <= 0) continue;
                        var contratoDetalleBl = new ContratoDetalleBL();
                        contratoInfo.ListaContratoDetalleInfo = contratoDetalleBl.ObtenerPorContratoId(contratoInfo);
                    }
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

        /// <summary>
        /// Obtiene el total de contratos (activos e inactivos)
        /// </summary>
        /// <returns></returns>
        internal List<ContratoInfo> ObtenerTodos()
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                result = contratoDal.ObtenerTodos();
                if (result != null)
                {
                    foreach (var contratoInfo in result)
                    {
                        if (contratoInfo.Organizacion.OrganizacionID > 0)
                        {
                            var organizacionBl = new OrganizacionBL();
                            contratoInfo.Organizacion = organizacionBl.ObtenerPorID(contratoInfo.Organizacion.OrganizacionID);
                        }

                        if (contratoInfo.Producto.ProductoId > 0)
                        {
                            var productoBl = new ProductoBL();
                            contratoInfo.Producto = productoBl.ObtenerPorID(contratoInfo.Producto);
                        }

                        if (contratoInfo.Proveedor.ProveedorID > 0)
                        {
                            var proveedorBl = new ProveedorBL();
                            contratoInfo.Proveedor =
                                proveedorBl.ObtenerPorID(contratoInfo.Proveedor.ProveedorID);
                        }

                        if (contratoInfo.TipoContrato.TipoContratoId > 0)
                        {
                            var tipoContratoBl = new TipoContratoBL();
                            contratoInfo.TipoContrato =
                                tipoContratoBl.ObtenerPorId(contratoInfo.TipoContrato.TipoContratoId);
                        }

                        if (contratoInfo.TipoFlete.TipoFleteId > 0)
                        {
                            var tipoFleteBl = new TipoFleteBL();
                            contratoInfo.TipoFlete =
                                tipoFleteBl.ObtenerPorId(contratoInfo.TipoFlete.TipoFleteId);
                        }

                        if (contratoInfo.ContratoId > 0)
                        {
                            var contratoDetalleBl = new ContratoDetalleBL();
                            contratoInfo.ListaContratoDetalleInfo = contratoDetalleBl.ObtenerPorContratoId(contratoInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un contrato por id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorId(ContratoInfo contratoInfo)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                contrato = contratoDAL.ObtenerPorId(contratoInfo);

                if (contrato != null)
                {
                    if (contrato.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        contrato.Organizacion = organizacionBl.ObtenerPorID(contrato.Organizacion.OrganizacionID);
                    }

                    if (contrato.Producto.ProductoId > 0)
                    {
                        var productoBl = new ProductoBL();
                        contrato.Producto = productoBl.ObtenerPorID(contrato.Producto);
                    }

                    if (contrato.Proveedor.ProveedorID > 0)
                    {
                        var proveedorBl = new ProveedorBL();
                        contrato.Proveedor =
                            proveedorBl.ObtenerPorID(contrato.Proveedor.ProveedorID);
                    }

                    if (contrato.TipoContrato.TipoContratoId > 0)
                    {
                        var tipoContratoBl = new TipoContratoBL();
                        contrato.TipoContrato =
                            tipoContratoBl.ObtenerPorId(contrato.TipoContrato.TipoContratoId);
                        contrato.tipocontratodescripcion = contrato.TipoContrato.Descripcion.ToString();
                    }

                    if (contrato.TipoFlete != null && contrato.TipoFlete.TipoFleteId > 0)
                    {
                        var tipoFleteBl = new TipoFleteBL();
                        contrato.TipoFlete =
                            tipoFleteBl.ObtenerPorId(contrato.TipoFlete.TipoFleteId);
                    }

                    if (contrato.TipoCambio != null && contrato.TipoCambio.TipoCambioId > 0)
                    {
                        var tipoCambioBl = new TipoCambioBL();
                        contrato.TipoCambio =
                            tipoCambioBl.ObtenerPorId(contrato.TipoCambio.TipoCambioId);
                    }

                    if (contrato.ContratoId > 0)
                    {
                        var contratoDetalleBl = new ContratoDetalleBL();
                        contrato.ListaContratoDetalleInfo = contratoDetalleBl.ObtenerPorContratoId(contrato);
                    }

                    //Obtener precio y cantidad por tonelada
                    contrato.CantidadToneladas = (int) (contrato.Cantidad / 1000);
                    if (contrato.TipoCambio != null)
                    {
                        if (contrato.TipoCambio.Descripcion == Properties.ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula)
                        {
                            contrato.PrecioToneladas = (contrato.Precio * 1000) / contrato.TipoCambio.Cambio;
                        }
                        else
                        {
                            contrato.PrecioToneladas = contrato.Precio * 1000;
                        }
                    }

                    if (contrato.Cuenta != null)
                    {
                        if (contrato.Cuenta.CuentaSAPID > 0)
                        {
                            CuentaSAPBL cuentaSapBl = new CuentaSAPBL();
                            contrato.Cuenta.Activo = EstatusEnum.Activo;
                            contrato.Cuenta = cuentaSapBl.ObtenerPorFiltroSinTipo(contrato.Cuenta);
                        }
                    }
                    //
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
            return contrato;
        }

        /// <summary>
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorFolio(ContratoInfo contratoInfo)
        {
            ContratoInfo result;
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                result = contratoDAL.ObtenerPorFolio(contratoInfo);
                result = ObtenerPorId(result);
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
        /// Obtiene una lista de contratos del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ContratoInfo> ObtenerContratosPorProveedorId(int proveedorId, int organizacionId)
        {
            List<ContratoInfo> listaContratosCompleta = new List<ContratoInfo>();
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();

                var contrato = new ContratoInfo();
                List<ContratoInfo> listaContratos = contratoDAL.ObtenerContratosPorProveedorId(proveedorId, organizacionId);

                if (listaContratos != null)
                {
                    foreach (var contratoInfo in listaContratos)
                    {
                        contrato = ObtenerPorId(contratoInfo);
                        listaContratosCompleta.Add(contrato);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaContratosCompleta;
        }

        /// <summary>
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {

            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                ResultadoInfo<ContratoInfo> contratoLista = contratoDAL.ObtenerPorPagina(pagina, filtro);
                if (contratoLista != null)
                {
                    foreach (var contrato in contratoLista.Lista)
                    {
                        //Obtener precio y cantidad por tonelada
                        contrato.CantidadToneladas = contrato.Cantidad / 1000;
                        contrato.ToneladasSurtidas = (decimal)contrato.CantidadSurtida/1000;
                        if (contrato.TipoCambio == null) continue;
                        if (contrato.TipoCambio.Descripcion.ToUpper() == Properties.ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula)
                        {
                            contrato.PrecioToneladas = (contrato.Precio * 1000) / contrato.TipoCambio.Cambio;
                        }
                        else
                        {
                            contrato.PrecioToneladas = contrato.Precio * 1000;
                        }

                        //Obtener detalle de contrato
                        if (contrato.ContratoId <= 0) continue;
                        var contratoDetalleBl = new ContratoDetalleBL();
                        contrato.ListaContratoDetalleInfo = contratoDetalleBl.ObtenerPorContratoId(contrato);
                    }
                }
                return contratoLista;
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

        public List<ContratoInfo> ObtenerContratoTipoFlete()
        {
            List<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoDal = new ContratoBL();
                result = contratoDal.ObtenerTodos();
                if (result != null)
                {
                    var resultado = result.Where(dato => dato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera).ToList();
                    return resultado;
                }

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
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        internal ContratoInfo ObtenerPorContenedor(ContratoInfo contenedor)
        {
            ContratoInfo result;
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                result = contratoDAL.ObtenerPorContenedor(contenedor);
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
        /// Obtiene un contrato por folio
        /// </summary>
        /// <param name="pagina"> </param>
        /// <param name="contenedor"></param>
        /// <returns>ContratoInfo</returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorContenedorPaginado(PaginacionInfo pagina, ContratoInfo contenedor)
        {
            ResultadoInfo<ContratoInfo> result;
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                result = contratoDAL.ObtenerPorContenedorPaginado(pagina, contenedor);
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
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ContratoInfo> ObtenerPorPaginaSinProgramacion(PaginacionInfo pagina, ContratoInfo filtro)
        {
            try
            {
                Logger.Info();
                var contratoDAL = new ContratoDAL();
                ResultadoInfo<ContratoInfo> contratoLista = contratoDAL.ObtenerPorPaginaSinProgramacion(pagina, filtro);
                if (contratoLista != null)
                {
                    foreach (var contrato in contratoLista.Lista)
                    {
                        //Obtener precio y cantidad por tonelada
                        contrato.CantidadToneladas = (int) (contrato.Cantidad / 1000);
                        if (contrato.TipoCambio == null) continue;
                        if (contrato.TipoCambio.Descripcion.ToUpper() == Properties.ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula)
                        {
                            contrato.PrecioToneladas = (contrato.Precio * 1000) / contrato.TipoCambio.Cambio;
                        }
                        else
                        {
                            contrato.PrecioToneladas = contrato.Precio * 1000;
                        }

                        //Obtener detalle de contrato
                        if (contrato.ContratoId <= 0) continue;
                        var contratoDetalleBl = new ContratoDetalleBL();
                        contrato.ListaContratoDetalleInfo = contratoDetalleBl.ObtenerPorContratoId(contrato);
                    }
                }
                return contratoLista;
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
        /// Obtiene una lista de contratos por XML
        /// </summary>
        /// <param name="contratosId"></param>
        /// <returns></returns>
        internal IEnumerable<ContratoInfo> ObtenerPorXML(List<int> contratosId)
        {
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                IEnumerable<ContratoInfo> result = contratoDal.ObtenerPorXML(contratosId);
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
        /// Obtiene una lista de contratos por fechas
        /// </summary>
        /// <returns></returns>
        internal List<ContratoInfo> ObtenerPorFechasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                List<ContratoInfo> result = contratoDal.ObtenerPorFechasConciliacion(organizacionID, fechaInicio, fechaFin);
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
        /// Metodo que actualiza un contrato
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        internal void ActualizarContrato(ContratoInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoDal = new ContratoDAL();
                contratoDal.ActualizarContrato(info, estatus);
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
