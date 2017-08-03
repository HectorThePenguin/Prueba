using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AlmacenInventarioBL
    {
        /// <summary>
        /// Obtiene el almacen en base al id
        /// </summary>
        /// <param name="almacenInventarioId"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerAlmacenInventarioPorId(int almacenInventarioId)
        {
            AlmacenInventarioInfo almacen = null;

            try
            {
                var almacenDAL = new AlmacenInventarioDAL();
                var almacenBl = new AlmacenBL();

                almacen = almacenDAL.ObtenerAlmacenInventarioPorId(almacenInventarioId);

                if (almacen != null)
                {
                    almacen.Almacen = almacenBl.ObtenerPorID(almacen.AlmacenID);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Obtiene un listado de almacen inventario por almacen id
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenInventarioInfo> ObtienePorAlmacenId(AlmacenInfo almacenInfo)
        {
            List<AlmacenInventarioInfo> almacen = null;

            try
            {
                var almacenDAL = new AlmacenInventarioDAL();
                almacen = almacenDAL.ObtenerPorAlmacenId(almacenInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Crea un registro en almacen inventario
        /// </summary>
        /// <returns></returns>
        internal int Crear(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioDal = new AlmacenInventarioDAL();
                int result = almacenInventarioDal.Crear(almacenInventarioInfo);
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
        /// Actualiza un registro
        /// </summary>
        /// <param name="almacenInventario"></param>
        internal void ActualizarPorProductoId(AlmacenInventarioInfo almacenInventario)
        {
            try
            {
                Logger.Info();
                var almacenInventarioDal = new AlmacenInventarioDAL();
                almacenInventarioDal.ActualizarPorProductoId(almacenInventario);
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
        internal void Actualizar(AlmacenInventarioInfo info)
        {
            try
            {
                Logger.Info();
                var almacenInventarioDal = new AlmacenInventarioDAL();
                almacenInventarioDal.Actualizar(info);
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
        /// Obtiene un listado de productos por almacen y obtiene los datos del producto
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioInfo> ObtienePorAlmacenIdLlenaProductoInfo(AlmacenInfo almacenInfo)
        {
            List<AlmacenInventarioInfo> almacen = null;

            try
            {
                var productoBl = new ProductoBL();
                var almacenDal = new AlmacenInventarioDAL();
                almacen = almacenDal.ObtenerPorAlmacenId(almacenInfo);
                if (almacen != null)
                {
                    foreach (var almacenInventarioInfo in almacen)
                    {
                        var productoInfo = new ProductoInfo()
                        {
                            ProductoId = almacenInventarioInfo.ProductoID
                        };
                        almacenInventarioInfo.Producto = productoBl.ObtenerPorID(productoInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Obtener inventario por almacenid, productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenDal = new AlmacenInventarioDAL();
                result = almacenDal.ObtenerPorAlmacenIdProductoId(almacenInventarioInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtener inventario por almacenid, productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenDal = new AlmacenInventarioDAL();
                result = almacenDal.ObtenerPorOrganizacionIdAlmacenIdProductoId(almacenInventarioInfo, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        

        /// <summary>
        /// Obtiene un listado de almaceninventario por almacenid
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal List<CierreDiaInventarioPAInfo> ObtenerDatosCierreDiaInventarioPlantaAlimentos(int organizacionID, int almacenID)
        {
            List<CierreDiaInventarioPAInfo> lista;
            try
            {
                var almacenDAL = new AlmacenInventarioDAL();
                lista = almacenDAL.ObtenerDatosCierreDiaInventarioPlantaAlimentos(organizacionID, almacenID);
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
        /// <param name="almacenId"></param>
        /// <param name="productos"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioInfo> ExistenciaPorProductos(int almacenId, IList<ProductoInfo> productos)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioDAL();
                IList<AlmacenInventarioInfo>  lista = almacenDAL.ExistenciaPorProductos(almacenId, productos);
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de AlmacenInventario 
        /// por sus almacenes ID
        /// </summary>
        /// <param name="almacenes"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioInfo> ObtenerPorAlmacenXML(List<AlmacenInfo> almacenes)
        {
            try
            {
                Logger.Info();
                var almacenDAL = new AlmacenInventarioDAL();
                IList<AlmacenInventarioInfo> lista = almacenDAL.ObtenerPorAlmacenXML(almacenes);
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtener inventario por almacenid, productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenDal = new AlmacenInventarioDAL();
                result = almacenDal.ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(almacenInventarioInfo, organizacionId);
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
