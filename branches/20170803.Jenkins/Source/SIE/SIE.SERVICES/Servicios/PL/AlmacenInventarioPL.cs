using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenInventarioPL
    {
        /// <summary>
        /// Obtiene un listado de almacen inventario por almacen id
        /// </summary>
        /// <returns></returns>
        public List<AlmacenInventarioInfo> ObtienePorAlmacenId(AlmacenInfo almacenInfo)
        {
            List<AlmacenInventarioInfo> almacen;

            try
            {
                var almacenBL = new AlmacenInventarioBL();
                almacen = almacenBL.ObtienePorAlmacenId(almacenInfo);
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
        public int Crear(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioBl = new AlmacenInventarioBL();
                int result = almacenInventarioBl.Crear(almacenInventarioInfo);
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
        /// Obtiene un listado de productos por almacen y asigna obtiene los datos del producto
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        public List<AlmacenInventarioInfo> ObtienePorAlmacenIdLlenaProductoInfo(AlmacenInfo almacenInfo)
        {
            List<AlmacenInventarioInfo> almacen;

            try
            {
                var almacenBl = new AlmacenInventarioBL();
                almacen = almacenBl.ObtienePorAlmacenIdLlenaProductoInfo(almacenInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Obtiene un listado de almaceninventario por almacenid
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        public List<CierreDiaInventarioPAInfo> ObtenerDatosCierreDiaInventarioPlantaAlimentos(int organizacionID, int almacenID)
        {
            List<CierreDiaInventarioPAInfo> lista;

            try
            {
                var almacenBl = new AlmacenInventarioBL();
                lista = almacenBl.ObtenerDatosCierreDiaInventarioPlantaAlimentos(organizacionID, almacenID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene un listado de almaceninventario 
        /// por un conjunto de productos
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="productos"></param>
        /// <returns></returns>
        public IList<AlmacenInventarioInfo> ExistenciaPorProductos(int almacenId, IList<ProductoInfo> productos)
        {
            try
            {
                var almacenBl = new AlmacenInventarioBL();
                IList<AlmacenInventarioInfo> lista = almacenBl.ExistenciaPorProductos(almacenId, productos);
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
        public IList<AlmacenInventarioInfo> ObtenerPorAlmacenXML(List<AlmacenInfo> almacenes)
        {
            try
            {
                var almacenBl = new AlmacenInventarioBL();
                IList<AlmacenInventarioInfo> lista = almacenBl.ObtenerPorAlmacenXML(almacenes);
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
        public  AlmacenInventarioInfo ObtenerPorAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenInventarioBL = new AlmacenInventarioBL();
                result = almacenInventarioBL.ObtenerPorAlmacenIdProductoId(almacenInventarioInfo);
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
        public AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenInventarioBL = new AlmacenInventarioBL();
                result = almacenInventarioBL.ObtenerPorOrganizacionIdAlmacenIdProductoId(almacenInventarioInfo, organizacionId);
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
        public AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            AlmacenInventarioInfo result;

            try
            {
                var almacenInventarioBL = new AlmacenInventarioBL();
                result = almacenInventarioBL.ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(almacenInventarioInfo, organizacionId);
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
