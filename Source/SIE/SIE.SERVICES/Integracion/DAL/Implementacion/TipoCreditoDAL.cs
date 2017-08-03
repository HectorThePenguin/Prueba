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
    internal class TipoCreditoDAL : DALBase
    {
        internal ResultadoInfo<TipoCreditoInfo> TipoCredito_ObtenerTiposCreditoPorFiltro(PaginacionInfo pagina, TipoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCreditoDAL.TipoCredito_ObtenerTiposCreditoPorFiltro(pagina, filtro);
                DataSet ds = Retrieve("TipoCredito_ObtenerTipoCreditoPorPagina", parameters);
                ResultadoInfo<TipoCreditoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCreditoDAL.TipoCredito_ObtenerTiposCreditoPorFiltro(ds);
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

        internal TipoCreditoInfo TipoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCreditoDAL.TipoCredito_ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoCredito_ObtenerPorDescripcion", parameters);
                TipoCreditoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCreditoDAL.TipoCredito_ObtenerPorDescripcion(ds);
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

        internal int TipoCredito_Crear(TipoCreditoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCreditoDAL.TipoCredito_ObtenerParametrosCrear(info);
                infoId = Create("[dbo].[TipoCredito_Crear]", parameters);
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

        internal void TipoCredito_Actualizar(TipoCreditoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoCreditoDAL.TipoCredito_ObtenerParametrosActualizar(info);
                Update("[dbo].[TipoCredito_Actualizar]", parameters);
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

        internal List<TipoCreditoInfo> TipoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoCredito_ObtenerTodos");
                List<TipoCreditoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCreditoDAL.TipoCredito_ObtenerTodos(ds);
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

        internal ConfiguracionCreditoInfo TipoCredito_ValidarConfiguracion(int tipoCredito)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCreditoDAL.PlazoCredito_ObtenerParametrosValidarConfiguracion(tipoCredito);
                DataSet ds = Retrieve("ConfiguracionCredito_ObtenerPorTipoCredito", parameters);
                var result = new ConfiguracionCreditoInfo();
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCreditoDAL.TipoCredito_ObtenerValidarConfiguracion(ds);
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
