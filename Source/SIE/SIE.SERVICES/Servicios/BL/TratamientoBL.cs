using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class TratamientoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Tratamiento
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(TratamientoInfo info)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    Logger.Info();
                    var tratamientoDAL = new TratamientoDAL();
                    int tratamientoID = info.TratamientoID;
                    if (tratamientoID == 0)
                    {
                        tratamientoID = tratamientoDAL.Crear(info);
                    }
                    else
                    {
                        tratamientoDAL.Actualizar(info);
                    }
                    tratamientoDAL.GuardarTratamientoProducto(info, tratamientoID);
                    info.TratamientoID = tratamientoID;
                    transaction.Complete();
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
        /// Metodo para Guardar/Modificar una entidad Tratamiento
        /// </summary>
        /// <param name="info"></param>
        internal void Centros_Guardar(TratamientoInfo info)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    Logger.Info();
                    var tratamientoDAL = new TratamientoDAL();
                    int tratamientoID = info.TratamientoID;
                    int organizacionID = info.Organizacion.OrganizacionID;
                    if (tratamientoID == 0)
                    {
                        tratamientoID = tratamientoDAL.Centros_Crear(info);
                    }
                    else
                    {
                        tratamientoDAL.Centros_Actualizar(info);
                    }
                    tratamientoDAL.Centros_GuardarTratamientoProducto(info, tratamientoID, organizacionID);
                    info.TratamientoID = tratamientoID;
                    transaction.Complete();
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                ResultadoInfo<TratamientoInfo> result = tratamientoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Tratamiento
        /// </summary>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                IList<TratamientoInfo> result = tratamientoDAL.ObtenerTodos();
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
        internal IList<TratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                IList<TratamientoInfo> result = tratamientoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Tratamiento por su Id
        /// </summary>
        /// <param name="tratamientoID">Obtiene una entidad Tratamiento por su Id</param>
        /// <returns></returns>
        internal TratamientoInfo ObtenerPorID(int tratamientoID)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                TratamientoInfo result = tratamientoDAL.ObtenerPorID(tratamientoID);
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
        /// Obtiene una entidad Tratamiento por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Tratamiento por su Id</param>
        /// <returns></returns>
        internal TratamientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                TratamientoInfo result = tratamientoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene la lista de tratamientos especificada para corte
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <param name="bMetafilaxia"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTipoTratamientosCorte(TratamientoInfo tratamientoInfo, Metafilaxia bMetafilaxia)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                IList<TratamientoInfo> lista = null;
                IList<TratamientoInfo> resultTrataamientos =
                    tratamientoDAL.ObtenerTratamientosCorte(tratamientoInfo, bMetafilaxia);

                if (resultTrataamientos != null)
                {
                    //Eliminar los tratamientos repetidos
                    lista = EliminarTratamientosRepetidos(resultTrataamientos);

                    var productoDal = new ProductoDAL();
                    ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamientoPorXML(lista.ToList());
                    if (result != null && result.Lista != null && result.Lista.Any())
                    {
                        lista.ForEach(prod =>
                        {
                            prod.Productos =
                                result.Lista.Where(id => id.TratamientoID == prod.TratamientoID).
                                    Select(p => p).ToList();
                        });
                    }
                }
                return lista;
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
        /// Obtiene los tratamientos por tipo de tratamiento de reimplante
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTratamientosPorTipoReimplante(TratamientoInfo tratamientoInfo)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                IList<TratamientoInfo> tratamientos = tratamientoDAL.ObtenerTratamientosPorTipoReimplante(tratamientoInfo);
                if (tratamientos != null)
                {
                    var productoDal = new ProductoDAL();
                    ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamientoPorXML(tratamientos.ToList());
                    if (result != null && result.Lista != null && result.Lista.Any())
                    {
                        tratamientos.ForEach(prod =>
                        {
                            prod.Productos =
                                result.Lista.Where(id => id.TratamientoID == prod.TratamientoID).
                                    Select(p => p).ToList();
                        });
                    }
                }
                return tratamientos;
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
        /// Obtiene para cada elemento de la lista de tratamientos los productos. El tratamiento debe estar marcado como seleccionado
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerProductosPorTratamientoSeleccionado(IList<TratamientoInfo> tratamientos)
        {
            var contador = 0;
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                foreach (var item in tratamientos)
                {
                    if (!item.Seleccionado) continue;

                    if (item.Productos != null) continue;

                    ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamiento(item);
                    item.Productos = new List<ProductoInfo>();
                    foreach (var producto in result.Lista)
                    {
                        producto.Renglon = ++contador;
                        item.Productos.Add(producto);

                    }
                }

                return tratamientos;
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
        /// Obtiene los productos para una lista completa de tratamientos especificada
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <returns></returns>
        internal IList<ProductoInfo> ObtenerProductosPorTratamiento(IList<TratamientoInfo> tratamientos)
        {
            IList<ProductoInfo> productos = new List<ProductoInfo>();

            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                foreach (var item in tratamientos)
                {
                    ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamiento(item);
                    foreach (var producto in result.Lista)
                    {
                        productos.Add(producto);
                    }
                }

                return productos;
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
        internal ResultadoInfo<TratamientoInfo> ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                ResultadoInfo<TratamientoInfo> result = tratamientoDAL.ObtenerTratamientosPorFiltro(pagina, filtro);
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
        internal ResultadoInfo<TratamientoInfo> Centros_ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                ResultadoInfo<TratamientoInfo> result = tratamientoDAL.Centros_ObtenerTratamientosPorFiltro(pagina, filtro);
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
        /// Valida si el código del tratamiento ya existe para esa organización
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal bool ValidarExisteTratamiento(TratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                bool result = tratamientoDAL.ValidarExisteTratamiento(filtro);
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
        /// Obtiene la lista de tratamientos por problema
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <param name="listaProblemas"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTratamientosPorProblemas(TratamientoInfo tratamientoInfo, List<int> listaProblemas)
        {
            try
            {
                Logger.Info();
                var tratamientoDAL = new TratamientoDAL();
                var resultTratamientos = tratamientoDAL.ObtenerTratamientosPorProblemas(tratamientoInfo, listaProblemas);
                List<TratamientoInfo> lista = null;
                //Eliminar los tratamientos repetidos
                if (resultTratamientos != null)
                {
                    lista = EliminarTratamientosRepetidos(resultTratamientos);
                }

                /* Se obtendran los tratamiento de Aretes sin ligarlos al problema */
                tratamientoInfo.TipoTratamiento = (int)TipoTratamiento.Arete;
                var resultTrataamientosArete = tratamientoDAL.ObtenerTratamientosPorTipo(tratamientoInfo);
                if (lista == null)
                {
                    lista = new List<TratamientoInfo>();
                }
                if (resultTrataamientosArete != null)
                {
                    lista.AddRange(resultTrataamientosArete);
                }

                if (lista != null)
                {
                    var productoDal = new ProductoDAL();
                    ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamientoPorXML(lista);
                    if (result != null && result.Lista != null && result.Lista.Any())
                    {
                        lista.ForEach(prod =>
                                          {
                                              prod.Productos =
                                                  result.Lista.Where(id => id.TratamientoID == prod.TratamientoID).
                                                      Select(p => p).ToList();
                                          });
                    }
                }
                return lista;
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
        /// Eliminar tratamientos repetidos
        /// </summary>
        /// <param name="resultTratamientos"></param>
        /// <returns></returns>
        private List<TratamientoInfo> EliminarTratamientosRepetidos(IList<TratamientoInfo> resultTratamientos)
        {
            var lista = new List<TratamientoInfo>();
            foreach (var tratamiento in resultTratamientos)
            {
                var existe = true;
                foreach (var tratamientoInf in lista)
                {
                    if (tratamiento.TratamientoID == tratamientoInf.TratamientoID)
                    {
                        existe = false;
                    }
                }
                if (existe)
                {
                    lista.Add(tratamiento);
                }
            }
            return lista;
        }

        /// <summary>
        /// Se valida la existencia de los productos en el inventario
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal ResultadoValidacion ComprobarExistenciaTratamientos(IList<TratamientoInfo> tratamientos, int almacenID)
        {
            var resultado = new ResultadoValidacion
            {
                Resultado = true,
                Mensaje = "OK",
                TipoResultadoValidacion = TipoResultadoValidacion.Default
            };

            //Se obtiene la lista de tratamientos seleccionados
            IList<TratamientoInfo> listaTratamientosSeleccionados =
                tratamientos.Where(item => item.Seleccionado && item.Habilitado).ToList();

            var almacenInventario = new AlmacenBL();
            if (listaTratamientosSeleccionados.Count > 0)
            {
                foreach (var item in listaTratamientosSeleccionados)
                {
                    if (item.Productos == null)
                    {
                        item.Productos = ObtenerProductosDelTratamiento(item);
                    }
                    foreach (var itemProducto in item.Productos)
                    {
                        //Se obtiene la cantida de inventario del producto en el almacen
                        var productoAlmacen = almacenInventario.ObtenerCantidadProductoEnInventario(
                            itemProducto,
                            almacenID);

                        if (productoAlmacen == null)
                        {
                            //No existe inventario del producto
                            resultado.Resultado = false;
                            resultado.TipoResultadoValidacion = TipoResultadoValidacion.ProductoInexistente;
                            resultado.Mensaje = itemProducto.ProductoDescripcion.Trim();
                            return resultado;
                        }
                        if (!(productoAlmacen.Cantidad < itemProducto.Dosis)) continue;
                        //No existe suficiente inventario del producto
                        resultado.Resultado = false;
                        resultado.TipoResultadoValidacion = TipoResultadoValidacion.InventarioInsuficiente;
                        resultado.Mensaje = itemProducto.ProductoDescripcion.Trim();
                        return resultado;
                    }
                }
            }
            else
            {
                //No han seleccionado tratamientos
                resultado.Resultado = false;
                resultado.TipoResultadoValidacion = TipoResultadoValidacion.TratamientosNoSeleccionados;
                return resultado;
            }
            return resultado;
        }


        /// <summary>
        /// Obtiene solo la lista de tratamientos seleccionados y sus productos
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerProductosDelTratamiento(TratamientoInfo tratamientos)
        {
            var contador = 0;
            var listaProductos = new List<ProductoInfo>();
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDal.ObtenerProductosPorTratamiento(tratamientos);

                foreach (var producto in result.Lista)
                {
                    producto.Renglon = ++contador;
                    listaProductos.Add(producto);

                }

                return listaProductos;
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
