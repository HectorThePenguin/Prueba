using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AlmacenBL
    {
        /// <summary>
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        internal AlmacenInfo ObtenerPorID(int almacenID)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ObtenerPorID(almacenID);
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
            return info;
        }

        /// <summary>
        /// Valida si existe Inventario en el almacen para el producto o medicamento
        /// </summary>
        /// <param name="itemProducto"></param>
        /// <param name="almacenID"></param>
        internal AlmacenInventarioInfo ObtenerCantidadProductoEnInventario(ProductoInfo itemProducto, int almacenID)
        {

            AlmacenInventarioInfo info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ObtenerCantidadProductoEnInventario(itemProducto, almacenID);
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
            return info;
        }

        /// <summary>
        /// Funcion para guardar los tratamientos en almacen movimiento y descontarlos del inventario
        /// </summary>
        /// <param name="listaTratamientos"></param>
        /// <param name="almacenMovimientoInfo"></param>
        internal int GuardarDescontarTratamientos(List<TratamientoInfo> listaTratamientos,
                                                AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            var info = 0;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();

                var resp = almacenDAL.GuardarAlmacenMovimiento(almacenMovimientoInfo);
                //Almacenar el MovimientoDetalle
                if (resp != null)
                {
                    almacenMovimientoInfo.AlmacenMovimientoID = resp.AlmacenMovimientoID;
                    almacenMovimientoInfo.FolioMovimiento = resp.FolioMovimiento;
                    almacenMovimientoInfo.FechaCreacion = resp.FechaCreacion;
                    almacenMovimientoInfo.FechaMovimiento = resp.FechaMovimiento;
                    //Se obtiene la lista de productos y tratamientos a insertar
                    var listaAlmacenMovimientoDetalle = (
                                        from tratamiento in listaTratamientos
                                        from producto in tratamiento.Productos
                                        select new AlmacenMovimientoDetalle
                                            {
                                                TratamientoID = tratamiento.TratamientoID,
                                                ProductoID = producto.ProductoId,
                                                Precio = 0,
                                                Cantidad = producto.Dosis,
                                                Importe = 0,
                                                AlmacenMovimientoID =
                                                    almacenMovimientoInfo.AlmacenMovimientoID,
                                                UsuarioCreacionID =
                                                    almacenMovimientoInfo.UsuarioCreacionID
                                            }).ToList();

                    var lista = new List<AlmacenMovimientoDetalle>();
                    foreach (var almacenMovimintoDetalle in listaAlmacenMovimientoDetalle)
                    {
                        var existe = true;
                        foreach (var almacenMovimintoDetalleInfo in lista)
                        {
                            if (almacenMovimintoDetalle.ProductoID == almacenMovimintoDetalleInfo.ProductoID)
                            {
                                existe = false;
                            }
                        }
                        if (existe)
                        {
                            lista.Add(almacenMovimintoDetalle);
                        }
                    }

                    //Se almacena el detalle del movimiento
                    var costoTotal = almacenDAL.GuardarAlmacenMovimientoDetalle(lista,
                                                                                almacenMovimientoInfo.AlmacenID);

                    var animalCosto = new AnimalCostoInfo
                                          {
                                              AnimalID = almacenMovimientoInfo.AnimalID,
                                              CostoID = almacenMovimientoInfo.CostoID,
                                              FolioReferencia = almacenMovimientoInfo.AlmacenMovimientoID,
                                              Importe = costoTotal,
                                              UsuarioCreacionID = almacenMovimientoInfo.UsuarioCreacionID
                                          };

                    //Almacenr el costo en Animal Costo
                    var animalBL = new AnimalBL();
                    info = animalBL.GuardarAnimalCosto(animalCosto);

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
            return info;
        }

        /// <summary>
        /// Validar que no queden ajustes pendientes por aplicar para el almacen(Diferencias de inventario)
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal bool ExistenAjustesPendientesParaAlmacen(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ExistenAjustesPendientesParaAlmacen(almacenMovimientoInfo);
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
            return info;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Almacen
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                int result = info.AlmacenID;
                bool esModificacion = result > 0;
                using (var scope = new TransactionScope())
                {
                    if (info.AlmacenID == 0)
                    {
                        result = almacenDAL.Crear(info);
                    }
                    else
                    {
                        almacenDAL.Actualizar(info);
                    }
                    if (info.Proveedor != null && info.Proveedor.ProveedorID > 0)
                    {
                        var proveedorAlmacenDAL = new ProveedorAlmacenDAL();
                        var proveedorAlmacen = new ProveedorAlmacenInfo
                        {
                            Proveedor = info.Proveedor,
                            Almacen = new AlmacenInfo
                            {
                                AlmacenID = result
                            }
                        };
                        if(esModificacion)
                        {
                            ProveedorAlmacenInfo proveedorAlmacenExiste =
                                    proveedorAlmacenDAL.ObtenerPorAlmacenID(result);
                            if(info.Activo == EstatusEnum.Inactivo)
                            {
                                if(proveedorAlmacenExiste != null)
                                {
                                    proveedorAlmacenExiste.Activo = EstatusEnum.Inactivo;
                                    proveedorAlmacenExiste.UsuarioModificacionID = info.UsuarioModificacionID;
                                    proveedorAlmacenDAL.Actualizar(proveedorAlmacenExiste);
                                    scope.Complete();
                                    return result;
                                }
                            }

                            if (proveedorAlmacenExiste != null)
                            {
                                proveedorAlmacenExiste.UsuarioModificacionID = info.UsuarioModificacionID.HasValue ? info.UsuarioModificacionID.Value : 0;
                                proveedorAlmacenExiste.Proveedor = info.Proveedor;
                                proveedorAlmacenDAL.Actualizar(proveedorAlmacenExiste);
                                scope.Complete();
                                return result;
                            }
                        }
                        else
                        {
                            proveedorAlmacen.UsuarioCreacionID = info.UsuarioCreacionID;
                        }
                        proveedorAlmacenDAL.Crear(proveedorAlmacen);
                    }
                    scope.Complete();
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
        internal ResultadoInfo<AlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                ResultadoInfo<AlmacenInfo> result = almacenDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Almacen
        /// </summary>
        /// <returns></returns>
        internal IList<AlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenInfo> result = almacenDAL.ObtenerTodos();
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
        internal IList<AlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenInfo> result = almacenDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Almacen por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenInfo result = almacenDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad Almacen por su descripción
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public AlmacenMovimientoInfo ObtenerAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenMovimientoInfo result = almacenDAL.ObtenerAlmacenMovimiento(almacenMovimientoInfo);

                if(result != null){
                    AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    result.ListaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>{ almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoID(result.AlmacenMovimientoID)};

                    AlmacenMovimientoCostoBL almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                    result.ListaAlmacenMovimientoCosto = almacenMovimientoCostoBl.ObtenerPorAlmacenMovimientoId(result);
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
        /// Obtiene los almacenes por organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<AlmacenInfo> ObtenerAlmacenPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenInfo> result = almacenDAL.ObtenerAlmacenPorOrganizacion(organizacionId);
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
        /// ObtenerDatosAlmacenInventario
        /// </summary>
        /// <param name="cierreInventarioInfo"></param>
        /// <returns></returns>
        public AlmacenCierreDiaInventarioInfo ObtenerDatosAlmacenInventario(AlmacenCierreDiaInventarioInfo cierreInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenCierreDiaInventarioInfo result = almacenDAL.ObtenerDatosAlmacenInventario(cierreInventarioInfo);
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
        /// Obtener productos almacen
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<AlmacenCierreDiaInventarioInfo> ObtenerProductosAlamcen(int almacenId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenCierreDiaInventarioInfo> result = almacenDAL.ObtenerProductosAlamcen(almacenId, organizacionId);
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
        /// Metodo que actualiza un registro en AlmacenMovimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        public void ActualizarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                almacenDAL.ActualizarAlmacenMovimiento(almacenMovimientoInfo);
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
        /// Metodo que actualiza un registro en AlmacenInventario
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        public void ActualizarAlmacenInventario(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                almacenDAL.ActualizarAlmacenInventario(almacenInventarioInfo);
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
        /// Metodo que elimina en AlmacenMovimientoDetalle
        /// </summary>
        /// <param name="almacenInventarioDetalle"></param>
        public void EliminaAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenInventarioDetalle)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                almacenDAL.EliminarAlmacenMovimientoDetalle(almacenInventarioDetalle);
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
        /// obtiene el almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public IList<AlmacenMovimientoInfo> ObtenerListaAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo, int activo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenMovimientoInfo> resultado = new List<AlmacenMovimientoInfo>();
                IList<AlmacenMovimientoInfo> result = almacenDAL.ObtenerListaAlmacenMovimiento(almacenMovimientoInfo, activo);
                if (result != null)
                {
                    foreach (var movimientoInfo in result)
                    {
                        if (movimientoInfo.Status == (int)EstatusInventario.Pendiente)
                        {
                            resultado.Add(movimientoInfo);
                        }
                    }
                }
                return resultado;
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

        internal List<AlmacenInventarioInfo> ObtenerProductosAlmacenInventario(AlmacenInfo almacen, OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                List<AlmacenInventarioInfo> resultado = almacenDAL.ObtenerProductosAlmacenInventario(almacen, organizacion);
                return resultado;
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
        /// Obtiene la lista de movimientos por almacenid
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns>Lista AlmacenMovimientoDetalle</returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorAlmacenID(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                List<AlmacenMovimientoDetalle> resultado = almacenDAL.ObtenerAlmacenMovimientoPorAlmacenID(almacen);
                return resultado;
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

        internal int GuardarConsumoAlimento(List<AlmacenInventarioInfo> listaActualizadaProductos, OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                int resultado = almacenDAL.GuardarConsumoAlimento(listaActualizadaProductos, organizacion);
                return resultado;
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
        /// Guarda el almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoNuevo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo GuardarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoNuevo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenMovimientoInfo resultado = almacenDAL.GuardarAlmacenMovimiento(almacenMovimientoNuevo);
                return resultado;
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

        internal decimal GuardarAlmacenMovimientoDetalle(List<AlmacenMovimientoDetalle> almacenDetalleLista, int almacenID)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                decimal resultado = almacenDAL.GuardarAlmacenMovimientoDetalle(almacenDetalleLista, almacenID);
                return resultado;
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
        /// Se almacena el detalle de almacen Movimiento
        /// </summary>
        /// <param name="almacenDetalle"></param>
        /// <returns></returns>
        public AlmacenMovimientoDetalle GuardarAlmacenMovimientoDetalleProducto(AlmacenMovimientoDetalle almacenDetalle)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                return almacenDAL.GuardarAlmacenMovimientoDetalleProducto(almacenDetalle);
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
        /// Obtiene los movimeintos por contrato
        /// (resc)
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorContrato(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                List<AlmacenMovimientoDetalle> resultado = almacenDAL.ObtenerAlmacenMovimientoPorContrato(contrato);
                return resultado;
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
        /// Obtiene los almacenes por organizacion y tipo de almacen
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public AlmacenInventarioInfo ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(ParametrosOrganizacionTipoAlmacenProductoActivo datos)
        {
            try
            {
                Logger.Info();
                var almacenDal = new AlmacenDAL();
                return almacenDal.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(datos);
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
        /// Obtiene los almacenes por organizacion y tipo de almacen
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorOrganizacionId(int organizacionId)
        {
            try
            {
                Logger.Info();
                using (var almacenDal = new Integracion.DAL.ORM.AlmacenDAL())
                {
                    return almacenDal.ObtenerTodos().FirstOrDefault(a => a.OrganizacionID == organizacionId);
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
        }

        /// <summary>
        /// Obtiene todos los almacenes por los tipo de almacen y organizacion.
        /// </summary>
        /// <param name="tiposAlmacen"></param>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<AlmacenInfo> ObtenerAlmacenPorTiposAlmacen(List<TipoAlmacenEnum> tiposAlmacen, OrganizacionInfo organizacion)
        {
            List<AlmacenInfo> listaAlmacen = null;
            try
            {
                if (tiposAlmacen != null && tiposAlmacen.Count > 0)
                {
                    IList<AlmacenInfo> listaAlmacenes = ObtenerAlmacenPorOrganizacion(organizacion.OrganizacionID);

                    if (listaAlmacenes != null && listaAlmacenes.Count > 0)
                    {
                        listaAlmacen = new List<AlmacenInfo>();
                        foreach (var tipoAlmacen in tiposAlmacen)
                        {
                            listaAlmacen.AddRange((from almacen in listaAlmacenes
                                                   where almacen.TipoAlmacen.TipoAlmacenID == (int)tipoAlmacen
                                                   select almacen));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaAlmacen;
        }

        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal int ObtenerFolioAlmacenConsulta(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                var almacenDal = new AlmacenDAL();
                return almacenDal.ObtenerFolioAlmacenConsulta(filtros);
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
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<AlmacenesCierreDiaInventarioPAModel> ObtenerAlmacenesOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var almacenDal = new AlmacenDAL();
                return almacenDal.ObtenerAlmacenesOrganizacion(organizacionID);
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
        /// Obtiene un almacen por su clave
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorAlmacenPoliza(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var almacenDal = new AlmacenDAL();
                return almacenDal.ObtenerPorAlmacenPoliza(almacen);
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
        /// Obtiene un objeto con los datos de almacen paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInfo> ObtenerPorPaginaPoliza(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                ResultadoInfo<AlmacenInfo> result = almacenDAL.ObtenerPorPaginaPoliza(pagina, filtro);
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
        /// filtrando por varios tipos de almacen.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInfo> ObtenerPorOrganizacionTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                ResultadoInfo<AlmacenInfo> result = almacenDAL.ObtenerPorOrganizacionTipoAlmacenPagina(pagina, filtro);
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
        internal ResultadoInfo<AlmacenInfo> ObtenerPorPaginaTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                ResultadoInfo<AlmacenInfo> result = almacenDAL.ObtenerPorPaginaTipoAlmacen(pagina, filtro);
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
        /// Obtiene un almacen por id y tipo almacen
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal AlmacenInfo ObtenerPorIdFiltroTipoAlmacen(AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenInfo result = almacenDAL.ObtenerPorIdFiltroTipoAlmacen(filtro);
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
        /// Validar que si tiene por lo menos algun producto con existencia en el inventario
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        internal bool ValidarProductosEnAlmacen(AlmacenInfo almacenInfo)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ValidarProductosEnAlmacen(almacenInfo);
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
            return info;
        }

        /// <summary>
        ///  Validar si el producto tiene existencias en algun Almacen
        /// </summary>
        /// <param name="productoID"></param>
        /// <returns></returns>
        internal bool ValidarExistenciasProductoEnAlmacen(int productoID)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ValidarExistenciasProductoEnAlmacen(productoID);
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
            return info;
        }

        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<AlmacenInfo> ObtenerAlmacenesPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var almacenDal = new AlmacenDAL();
                return almacenDal.ObtenerAlmacenesPorOrganizacion(organizacionID);
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
        /// Guarda el almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoNuevo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo GuardarAlmacenMovimientoConFecha(AlmacenMovimientoInfo almacenMovimientoNuevo)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                AlmacenMovimientoInfo resultado = almacenDAL.GuardarAlmacenMovimientoConFecha(almacenMovimientoNuevo);
                return resultado;
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
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        internal AlmacenInfo ObtenerPorIDOrganizacion(AlmacenInfo almacenInfo)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenDAL();
                info = almacenDAL.ObtenerPorIDOrganizacion(almacenInfo);
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
            return info;
        }

        /// <summary>
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        internal List<AlmacenInfo> ObtenerAlamcenPorProducto(FiltroAlmacenProductoEnvio filtroEnvio)
        {
            try
            {
                Logger.Info();
                return new AlmacenDAL().ObtenerAlamcenPorProducto(filtroEnvio);
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
