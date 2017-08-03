using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class AlmacenInventarioLoteBL
    {
        /// <summary>
        /// Obtiene el almacen en base al id
        /// </summary>
        /// <param name="almacenInventarioLoteId"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorId(int almacenInventarioLoteId)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorId(almacenInventarioLoteId);

                if (almacenInventarioLote != null)
                {
                    var almacenInventarioBl = new AlmacenInventarioBL();
                    almacenInventarioLote.AlmacenInventario =
                        almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                            almacenInventarioLote.AlmacenInventario.AlmacenInventarioID);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Crea un registro en almacen inventario lote.
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal int Crear(AlmacenInventarioLoteInfo almacenInventarioLoteInfo, AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioDal = new AlmacenInventarioLoteDAL();
                int result = almacenInventarioDal.Crear(almacenInventarioLoteInfo, almacenInventarioInfo);
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
        /// Obtiene los almacenes en base a la organizacion, el tipo de almacen y el producto
        /// </summary>
        /// <param name="datosLotes"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo datosLotes)
        {
            List<AlmacenInventarioLoteInfo> almacenInventarioLote = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(datosLotes);

                if (almacenInventarioLote != null)
                {
                    foreach (var almacenInventario in almacenInventarioLote)
                    {
                        if (almacenInventario.AlmacenInventario.AlmacenInventarioID > 0)
                        {
                            var almacenInventarioBl = new AlmacenInventarioBL();
                            almacenInventario.AlmacenInventario =
                                almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                    almacenInventario.AlmacenInventario.AlmacenInventarioID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Actualiza los datos del lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal void Actualizar(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLoteDAL.Actualizar(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenInventarioID(AlmacenInventarioInfo almacenInventario)
        {
            List<AlmacenInventarioLoteInfo> almacenInventarioLote = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerPorAlmacenInventarioID(almacenInventario);

                if (almacenInventarioLote != null)
                {
                    foreach (var almacenInventarioInfo in almacenInventarioLote)
                    {
                        if (almacenInventarioInfo.AlmacenInventario.AlmacenInventarioID > 0)
                        {
                            var almacenInventarioBl = new AlmacenInventarioBL();
                            almacenInventarioInfo.AlmacenInventario =
                                almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                    almacenInventarioInfo.AlmacenInventario.AlmacenInventarioID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }


        /// <summary>
        /// Obtiene el almacen en base a la organizacion, el tipo de almacen , el producto y el numero de lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="datosLotes"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerPorLoteTipoAlmacenProducto(int lote,ParametrosOrganizacionTipoAlmacenProductoActivo datosLotes)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;

            try
            {
                var listaInventariosLote = ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(datosLotes);
                if (listaInventariosLote != null && listaInventariosLote.Count > 0)
                {
                    var listaAlmacenInventarioLote = (from inventarioLote in listaInventariosLote
                                            where inventarioLote.Lote == lote
                                            select inventarioLote);

                    if (listaAlmacenInventarioLote != null && listaAlmacenInventarioLote.Count() > 0)
                    {
                        almacenInventarioLote = listaAlmacenInventarioLote.First();
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }


        /// <summary>
        /// Obtiene los lotes en los que se encuentra un producto.
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProducto(AlmacenInfo almacen,ProductoInfo producto)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();

                listaLotes = almacenDAL.ObtenerPorAlmacenProducto(almacen, producto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }

        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                result = almacenInventarioLoteDal.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(pagina, almacenInventarioLote);
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
        /// Desactiva un lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        internal void DesactivarLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLoteDAL.DesactivarLote(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene almaceninventariolote por folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorLote(almacenInventarioLote);

                if (almacenInventarioLote != null)
                {
                    if (almacenInventarioLote.AlmacenInventario.AlmacenInventarioID > 0)
                    {
                        var almacenInventarioBl = new AlmacenInventarioBL();
                        almacenInventarioLote.AlmacenInventario =
                            almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                almacenInventarioLote.AlmacenInventario.AlmacenInventarioID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Crea el nuevo lote para la organizacion producto tipo almacen seleccionados
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo CrearLotePorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo parametro)
        {
            try
            {
                var almacenBl = new AlmacenBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();

                AlmacenInventarioLoteInfo almacenInventarioLote = null;

                var almacenInventario =
                    almacenBl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(new ParametrosOrganizacionTipoAlmacenProductoActivo
                    {
                        OrganizacionId = parametro.OrganizacionId,
                        TipoAlmacenId = parametro.TipoAlmacenId,
                        Activo = parametro.Activo,
                        ProductoId = parametro.ProductoId
                    });

                // Si el producto no se encuentra en el almacen inventario, lo insertamos
                if (almacenInventario == null)
                {
                    var listaAlmacenOrganizacion = almacenBl.ObtenerAlmacenPorOrganizacion(parametro.OrganizacionId);
                    if (listaAlmacenOrganizacion != null)
                    {
                        // Obtenemos el almacen y validamos que sea del mismo tipo Almacen
                        foreach (AlmacenInfo almacenInfo in listaAlmacenOrganizacion)
                        {
                            // Aqui se valida que el almacen sea del tipo seleccionado
                            if (almacenInfo.TipoAlmacen.TipoAlmacenID == parametro.TipoAlmacenId)
                            {
                                almacenInventario = new AlmacenInventarioInfo
                                {
                                    AlmacenInventarioID =
                                        almacenInventarioBl.Crear(new AlmacenInventarioInfo
                                        {
                                            AlmacenID = almacenInfo.AlmacenID,
                                            ProductoID = parametro.ProductoId,
                                            UsuarioCreacionID = parametro.UsuarioId
                                        }),
                                    AlmacenID = almacenInfo.AlmacenID
                                };
                                break;
                            }
                        }
                    }
                }

                if (almacenInventario != null)
                {
                    int loteIdCreado = almacenInventarioLoteBl.Crear(new AlmacenInventarioLoteInfo
                    {
                        AlmacenInventarioLoteId = 0,
                        AlmacenInventario =
                            new AlmacenInventarioInfo
                            {
                                AlmacenInventarioID = almacenInventario.AlmacenInventarioID
                            },
                        Cantidad = 0,
                        PrecioPromedio = 0,
                        Piezas = 0,
                        Importe = 0,
                        Activo = (EstatusEnum) parametro.Activo,
                        UsuarioCreacionId = parametro.UsuarioId,
                    }, new AlmacenInventarioInfo
                    {
                        AlmacenID = almacenInventario.AlmacenID,
                        ProductoID = parametro.ProductoId
                    });

                    almacenInventarioLote =
                        almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(loteIdCreado);
                }

                return almacenInventarioLote;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoConMovimientos(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();

                listaLotes = almacenDAL.ObtenerPorAlmacenProductoConMovimientos(almacenInfo, productoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }
        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote por producto organizacion tipo almacen y que tenga cantidad mayor a cero
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                result = almacenInventarioLoteDal.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado(pagina, almacenInventarioLote);
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
        /// Obtiene una lista de Almacen Inventario Lote
        /// </summary>
        /// <param name="filtroAyudaLote"></param>
        /// <returns></returns>
        public IList<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLotePorLote(FiltroAyudaLotes filtroAyudaLote)
        {
            IList<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                result = almacenInventarioLoteDal.ObtenerAlmacenInventarioLotePorLote(filtroAyudaLote);
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
        /// Obtiene almaceninventariolote por folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorFolioLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorFolioLote(almacenInventarioLote);

                if (almacenInventarioLote != null)
                {
                    if (almacenInventarioLote.AlmacenInventario.AlmacenInventarioID > 0)
                    {
                        var almacenInventarioBl = new AlmacenInventarioBL();
                        almacenInventarioLote.AlmacenInventario =
                            almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                almacenInventarioLote.AlmacenInventario.AlmacenInventarioID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoEnCeros(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();

                listaLotes = almacenDAL.ObtenerPorAlmacenProductoEnCeros(almacenInfo, productoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }

        internal IList<AlmacenInventarioLoteInfo> ObtenerLotesUso(AlmacenInventarioLoteInfo datosLote)
        {
            IList<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();

                listaLotes = almacenDAL.ObtenerLotesUso(datosLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        
        public void GuardarAutorizacionMateriaPrima(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                almacenInventarioLoteDal.GuardarAutorizacionMateriaPrima(autorizacionMateriaPrimaInfo);
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
        /// 
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        /// <param name="programacionMateriaPrimaInfo"></param>
        public void GuardarAutorizacionMateriaPrimaProgramacion(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo, ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                var autorizacionMateriaPrimaID = almacenInventarioLoteDal.GuardarAutorizacionMateriaPrimaProgramacion(autorizacionMateriaPrimaInfo, programacionMateriaPrimaInfo);

                almacenInventarioLoteDal.GuardarProgramacionMateria(programacionMateriaPrimaInfo, autorizacionMateriaPrimaID);

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
        /// Obtiene almaceninventariolote por folio
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorContratoID(ContratoInfo contrato)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;
            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorContratoID(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene almaceninventariolote por folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteAlmacenCodigo(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLoteAlmacenCodigo(almacenInventarioLote);

                if (almacenInventarioLote != null)
                {
                    if (almacenInventarioLote.AlmacenInventario.AlmacenInventarioID > 0)
                    {
                        var almacenInventarioBl = new AlmacenInventarioBL();
                        almacenInventarioLote.AlmacenInventario =
                            almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                almacenInventarioLote.AlmacenInventario.AlmacenInventarioID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote por producto organizacion tipo almacen y que tenga cantidad mayor a cero
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLoteAlmacenPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteDal = new AlmacenInventarioLoteDAL();
                result = almacenInventarioLoteDal.ObtenerAlmacenInventarioLoteAlmacenPaginado(pagina, almacenInventarioLote);
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
        /// Se obtiene lotes 
        /// </summary>
        /// <param name="almacenesInventario"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioLoteInfo> ObtenerLotesPorAlmacenInventarioXML(List<AlmacenInventarioInfo> almacenesInventario)
        {
            IList<AlmacenInventarioLoteInfo> almacenInventarioLote;

            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerLotesPorAlmacenInventarioXML(almacenesInventario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Crea un registro en almacen inventario lote.
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <returns></returns>
        internal int CrearConTodosParametros(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioDal = new AlmacenInventarioLoteDAL();
                int result = almacenInventarioDal.CrearConTodosParametros(almacenInventarioLoteInfo);
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
        /// Obtiene una lista de almaceninventariolote
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenInventarioIDLigero(AlmacenInventarioInfo almacenInventario)
        {
            List<AlmacenInventarioLoteInfo> almacenInventarioLote;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteDAL();
                almacenInventarioLote = almacenDAL.ObtenerPorAlmacenInventarioID(almacenInventario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }


        /// <summary>
        ///  Actualiza la infomracion del inventario - lote cuando se genera un envio de alimento
        /// </summary>
        /// <param name="almacenInventarioLoteInfo">Información del almácen lote - inventari</param>
        internal void ActualizarEnvioAlimento(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                new AlmacenInventarioLoteDAL().ActualizarInventarioLoteEnvioMercancia(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
