using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapAlmacenInventarioDAL
    {
        /// <summary>
        /// Obtiene la entidad del almacen inventario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioInfo ObtenerAlmacenInventarioId(DataSet ds)
        {
            AlmacenInventarioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioInfo
                         {
                              AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                              AlmacenID = info.Field<int>("AlmacenID"),
                              ProductoID = info.Field<int>("ProductoID"),
                              Minimo = info.Field<int>("Minimo"),
                              Maximo =info.Field<int>("Maximo"),
                              PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                              Cantidad = info.Field<decimal>("Cantidad"),
                              Importe = info.Field<decimal>("Importe")
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene un listado de almacenes por almacenid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioInfo> ObtenerPorAlmacenId(DataSet ds)
        {
            List<AlmacenInventarioInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioInfo
                         {
                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Minimo = info.Field<int>("Minimo"),
                             Maximo = info.Field<int>("Maximo"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene un listado de almacenes por almacenid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CierreDiaInventarioPAInfo> ObtenerDatosCierreDiaInventarioPlantaAlimentos(DataSet ds)
        {
            List<CierreDiaInventarioPAInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                lista = (from info in dt.AsEnumerable()
                         select new CierreDiaInventarioPAInfo
                         {
                             ProductoID = info.Field<int>("ProductoID"),
                             Producto = info.Field<string>("Producto"),
                             UnidadMedicion = info.Field<string>("UnidadMedida"),
                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                             ListaCierreDiaInventarioPADetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                  where info.Field<int>("ProductoID") == detalle.Field<int>("ProductoID")
                                                                      select new CierreDiaInventarioPADetalleInfo
                                                                          {
                                                                              ProductoID = detalle.Field<int>("ProductoID"),
                                                                              Producto = detalle.Field<string>("Producto"),
                                                                              ManejaLote = detalle.Field<bool>("ManejaLote"),
                                                                              AlmacenInventarioLoteID = detalle.Field<int>("AlmacenInventarioLoteID"),
                                                                              Lote = detalle.Field<int>("Lote"),
                                                                              CostoUnitario = detalle.Field<decimal>("CostoUnitario"),
                                                                              TamanioLote = detalle.Field<int>("TamanioLote"),
                                                                              InventarioTeorico = detalle.Field<int>("InventarioTeorico"),
                                                                              SubFamiliaID = detalle.Field<int>("SubFamiliaID"),
                                                                              PiezasTeoricas = detalle.Field<int>("PiezasTeoricas")
                                                                          }).ToList()
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de AlmacenInventario 
        /// por un conjunto de productos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioInfo> ObtenerExistenciaPorProductos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInventarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInventarioInfo
                             {
                                 AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                                 AlmacenID =  info.Field<int>("AlmacenID"),
                                 Almacen =
                                     new AlmacenInfo
                                         {
                                             AlmacenID = info.Field<int>("AlmacenID"),
                                             TipoAlmacen = new TipoAlmacenInfo
                                                 {
                                                     TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                                     Descripcion = info.Field<string>("TipoAlmacen")
                                                 }
                                             //Descripcion = info.Field<string>("Almacen")
                                         },
                                 ProductoID = info.Field<int>("ProductoID"),
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             SubFamilia = new SubFamiliaInfo
                                                 {
                                                     SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                                     Descripcion = info.Field<string>("SubFamilia")
                                                 },
                                                 UnidadMedicion = new UnidadMedicionInfo
                                                     {
                                                         UnidadID = info.Field<int>("UnidadID"),
                                                         Descripcion = info.Field<string>("Unidad"),
                                                         ClaveUnidad = info.Field<string>("ClaveUnidad")
                                                     }
                                             //Descripcion = info.Field<string>("Producto")
                                         },
                                 Minimo = info.Field<int>("Minimo"),
                                 Maximo = info.Field<int>("Maximo"),
                                 PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                 Cantidad = info.Field<decimal>("Cantidad"),
                                 Importe = info.Field<decimal>("Importe"),
                                 //Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
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
        /// <param name="ds"></param>
        /// <returns></returns>
        public static int ObtenerAutorizacionMateriaPrimaID(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = Convert.ToInt32(dr["AutorizacionMateriaPrimaID"]);
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
        /// Obtiene un listado de almacenes por almacenid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioInfo> ObtenerPorAlmacenXML(DataSet ds)
        {
            List<AlmacenInventarioInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioInfo
                         {
                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Minimo = info.Field<int>("Minimo"),
                             Maximo = info.Field<int>("Maximo"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                             DiasReorden = info.Field<int>("DiasReorden"),
                             CapacidadAlmacenaje = info.Field<int>("CapacidadAlmacenaje")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
