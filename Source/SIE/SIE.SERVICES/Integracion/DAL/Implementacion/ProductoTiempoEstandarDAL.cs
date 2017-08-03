using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ProductoTiempoEstandarDAL : DALBase
    {
        public ResultadoInfo<ProductoTiempoEstandarInfo> ObtenerPorPagina (PaginacionInfo pagina, ProductoTiempoEstandarInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoTiempoEstandarDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProductoTiempoEstandar_ObtenerPorPagina", parameters);
                ResultadoInfo<ProductoTiempoEstandarInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoTiempoEstandarDAL.ObtenerPorPagina(ds);
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

        public bool GuardarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoTiempoEstandarDAL.ObtenerParametrosGuardar(productoTiempoEstandarInfo);
                DataSet ds = Retrieve("ProductoTiempoEstandar_Guardar", parameters);
                bool result = false;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoTiempoEstandarDAL.GuardarProductoTiempoEstandar(ds);
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

        public bool ActualizarProductoTiempoEstandar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoTiempoEstandarDAL.ObtenerParametrosActualizar(productoTiempoEstandarInfo);
                DataSet ds = Retrieve("ProductoTiempoEstandar_Actualizar", parameters);
                bool result = false;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoTiempoEstandarDAL.ActualizarProductoTiempoEstandar(ds);
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

        public ProductoTiempoEstandarInfo ObtenerPorProductoID(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoTiempoEstandarDAL.ObtenerParametrosObtenerPorProductoID(productoTiempoEstandarInfo);
                DataSet ds = Retrieve("ProductoTiempoEstandar_ObtenerPorProductoId", parameters);
                ProductoTiempoEstandarInfo productoTiempoEstandarInfoRespuesta = new ProductoTiempoEstandarInfo();
                if (ValidateDataSet(ds))
                {
                    productoTiempoEstandarInfoRespuesta = MapProductoTiempoEstandarDAL.ObtenerPorProductoID(ds);
                    
                }

                return productoTiempoEstandarInfoRespuesta;
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
