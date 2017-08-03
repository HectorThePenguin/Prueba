using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class AlmacenInventarioDAL:DALBase
    {
        /// <summary>
        /// Obtiene el almacen inventario indicado
        /// </summary>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerAlmacenInventarioPorId(int almacenInventarioId)
        {
            AlmacenInventarioInfo almacen = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioDAL.ObtenerParametrosObtenerAlmacenInventarioId(almacenInventarioId);
                DataSet ds = Retrieve("AlmacenInventario_ObtenerAlmacenPorId", parametros);
                if (ValidateDataSet(ds))
                {
                    almacen = MapAlmacenInventarioDAL.ObtenerAlmacenInventarioId(ds);
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
        /// Obtiene un listado de almaceninventario por almacenid
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioInfo> ObtenerPorAlmacenId(AlmacenInfo almacenInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosObtenerPorAlmacenId(almacenInfo);
                var ds = Retrieve("AlmacenInventario_ObtenerPorAlmacenID", parameters);
                List<AlmacenInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerPorAlmacenId(ds);
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
        /// Se inserta un registro en almacenInventario
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal int Crear(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenInventarioDAL.ObtenerParametrosCrear(almacenInventarioInfo);
                int result = Create("AlmacenInventario_Crear", parameters);
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
        /// Actualiza el inventario de un producto
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal void ActualizarPorProductoId(AlmacenInventarioInfo almacenInventario)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenInventarioDAL.ObtenerParametrosActualizarPorProductoId(almacenInventario);
                Update("AlmacenInventario_ActualizarPorProductoID", parameters);
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
        /// Actualiza un registro de almacen inventario
        /// </summary>
        /// <returns></returns>
        internal void Actualizar(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenInventarioDAL.ObtenerParametrosActualizar(almacenInventarioInfo);
                Update("AlmacenInventario_Actualizar", parameters);
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
        /// Obtiene inventario por almacen y producto
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosObtenerPorAlmacenIdProductoId(almacenInventarioInfo);
                var ds = Retrieve("AlmacenInventario_ObtenerPorAlmacenIDProductoID", parameters);
                AlmacenInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerAlmacenInventarioId(ds);
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
        /// Obtiene inventario por almacen y producto
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosObtenerPorOrganizacionIDAlmacenIdProductoId(almacenInventarioInfo, organizacionId);
                var ds = Retrieve("AlmacenInventario_ObtenerPorOrganizacionIdAlmacenIDProductoID", parameters);
                AlmacenInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerAlmacenInventarioId(ds);
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
        /// Obtiene un listado de almaceninventario por almacenid
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal List<CierreDiaInventarioPAInfo> ObtenerDatosCierreDiaInventarioPlantaAlimentos(int organizacionID, int almacenID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosDatosCierreDiaInventarioPlantaAlimentos(organizacionID, almacenID);
                var ds = Retrieve("CierreDiaInventarioPA_ObtenerDatos", parameters);
                List<CierreDiaInventarioPAInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerDatosCierreDiaInventarioPlantaAlimentos(ds);
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
        /// Actualiza un registro de almacen inventario
        /// </summary>
        /// <returns></returns>
        internal void AjustarAlmacenInventario(List<AlmacenInventarioInfo> listaAlmacenInventario)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenInventarioDAL.ObtenerParametrosAjustarAlmacenInventario(listaAlmacenInventario);
                Update("AlmacenInventario_DescontarCierreDiaInventario", parameters);
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
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosExistenciaPorProductos(almacenId, productos);
                var ds = Retrieve("AlmacenInventario_ExistenciaPorProductos", parameters);
                List<AlmacenInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerExistenciaPorProductos(ds);
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
        /// Actualiza un conjunto de registros de almacen inventario
        /// </summary>
        /// <param name="listaAlmacenInventario"></param>
        /// <returns></returns>
        internal void Actualizar(IList<AlmacenInventarioInfo> listaAlmacenInventario)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosActualizarPorProductos(listaAlmacenInventario);
                Update("AlmacenInventario_ActualizarPorProductos", parameters);
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
        /// Actualiza los datos del inventario despues de haber guardado la entrada del producto.
        /// </summary>
        /// <returns></returns>
        internal void DescontarAlmacenInventarioProduccionDiaria(ProduccionDiariaInfo produccionDiaria)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioDAL.ObtenerParametrosDescontarAlmacenInventarioProduccionDiaria(produccionDiaria);
                Update("AlmacenInventario_DescontarProduccionDiaria", parametros);
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
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosPorAlmacenXML(almacenes);
                var ds = Retrieve("AlmacenInventario_ObtenerPorAlmacenXML", parameters);
                List<AlmacenInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerPorAlmacenXML(ds);
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
        /// Obtiene inventario por almacen y producto
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal AlmacenInventarioInfo ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(AlmacenInventarioInfo almacenInventarioInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlmacenInventarioDAL.ObtenerParametrosObtenerPorOrganizacionIDAlmacenIdProductoId(almacenInventarioInfo, organizacionId);
                var ds = Retrieve("AlmacenInventario_ObtenerPorOrganizacionIdAlmacenIDProductoIDCentroCadisDesc", parameters);
                AlmacenInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerAlmacenInventarioId(ds);
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
    }
}
