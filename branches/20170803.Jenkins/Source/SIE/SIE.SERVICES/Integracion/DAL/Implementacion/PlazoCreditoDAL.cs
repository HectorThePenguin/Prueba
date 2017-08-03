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
    internal class PlazoCreditoDAL : DALBase
    {
        internal ResultadoInfo<PlazoCreditoInfo> PlazoCredito_ObtenerPlazosCreditoPorFiltro(PaginacionInfo pagina, PlazoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPlazoCreditoDAL.PlazoCredito_ObtenerPlazosCreditoPorFiltro(pagina, filtro);
                DataSet ds = Retrieve("PlazoCredito_ObtenerPlazoCreditoPorPagina", parameters);
                ResultadoInfo<PlazoCreditoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPlazoCreditoDAL.PlazoCredito_ObtenerPlazosCreditoPorFiltro(ds);
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

        internal List<PlazoCreditoInfo> PlazoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("PlazoCredito_ObtenerTodos");
                List<PlazoCreditoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPlazoCreditoDAL.PlazoCredito_ObtenerTodos(ds);
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

        internal PlazoCreditoInfo PlazoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPlazoCreditoDAL.PlazoCredito_ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("PlazoCredito_ObtenerPorDescripcion", parameters);
                PlazoCreditoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPlazoCreditoDAL.PlazoCredito_ObtenerPorDescripcion(ds);
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

        internal int PlazoCredito_Crear(PlazoCreditoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPlazoCreditoDAL.PlazoCredito_ObtenerParametrosCrear(info);
                infoId = Create("[dbo].[PlazoCredito_Crear]", parameters);
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

            return infoId;
        }

        internal void PlazoCredito_Actualizar(PlazoCreditoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPlazoCreditoDAL.PlazoCredito_ObtenerParametrosActualizar(info);
                Update("[dbo].[PlazoCredito_Actualizar]", parameters);
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

        internal ConfiguracionCreditoInfo PlazoCredito_ValidarConfiguracion(int plazoCredito)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPlazoCreditoDAL.PlazoCredito_ObtenerParametrosValidarConfiguracion(plazoCredito);
                DataSet ds = Retrieve("ConfiguracionCredito_ObtenerPorPlazoCredito", parameters);
                var result = new ConfiguracionCreditoInfo();
                if (ValidateDataSet(ds))
                {
                    result = MapPlazoCreditoDAL.PlazoCredito_ObtenerValidarConfiguracion(ds);
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
