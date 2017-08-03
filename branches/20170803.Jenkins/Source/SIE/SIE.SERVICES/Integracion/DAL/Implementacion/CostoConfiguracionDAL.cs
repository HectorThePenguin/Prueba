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
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CostoOrganizacionDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>ObtenerPorOrganizacion
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoOrganizacionInfo filtro)
        {
            ResultadoInfo<CostoOrganizacionInfo> costoOrganizacionLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CostoOrganizacion_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    costoOrganizacionLista = MapCostoOrganizacionDAL.ObtenerPorPagina(ds);
                }
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
            return costoOrganizacionLista;
        }

        /// <summary>
        ///     Metodo que actualiza un CostoOrganizacion
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CostoOrganizacionInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[CostoOrganizacion_Actualizar]", parameters);
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
        ///     Obtiene un CostoOrganizacionInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal CostoOrganizacionInfo ObtenerPorID(int infoId)
        {
            CostoOrganizacionInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[CostoOrganizacion_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoOrganizacionDAL.ObtenerPorID(ds);
                }
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
            return result;
        }

        /// <summary>
        ///     Obtiene un CostoOrganizacionInfo por Id
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        internal List<CostoOrganizacionInfo> ObtenerPorOrganizacion(EntradaGanadoInfo entradaGanadoInfo)
        {
            List<CostoOrganizacionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerParametroPorOrganizacion(entradaGanadoInfo);
                DataSet ds = Retrieve("[dbo].[CostoOrganizacion_ObtenerPorOganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoOrganizacionDAL.ObtenerPorOrganizacion(ds);
                }
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
            return result;
        }

        /// <summary>
        ///     Metodo que crear un CostoOrganizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(CostoOrganizacionInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerParametrosGuardado(info);
                infoId = Create("[dbo].[CostoOrganizacion_Crear]", parameters);
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

        /// <summary>
        ///  Obtiene una lista de CostoOrganizacions filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CostoOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoOrganizacionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CostoOrganizacion_ObtenerTodos", parameters);
                IList<CostoOrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoOrganizacionDAL.ObtenerTodos(ds);
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
        ///  Obtiene un CostoOrganizacions por tipo organizacion
        ///  y costo
        /// </summary>
        /// <returns></returns>
        internal CostoOrganizacionInfo ObtenerPorTipoOrganizacionCosto(CostoOrganizacionInfo costoOrganizacion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCostoOrganizacionDAL.ObtenerParametroPorTipoOrganizacionCosto(costoOrganizacion);
                DataSet ds = Retrieve("CostoOrganizacion_ObtenerPorTipoOrganizacionCosto", parameters);
                CostoOrganizacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoOrganizacionDAL.PorTipoOrganizacionCosto(ds);
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
