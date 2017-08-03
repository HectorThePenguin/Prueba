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
    internal class PrecioGanadoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<PrecioGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioGanadoInfo filtro)
        {
            ResultadoInfo<PrecioGanadoInfo> precioGanadoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("PrecioGanado_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    precioGanadoLista = MapPrecioGanadoDAL.ObtenerPorPagina(ds);
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
            return precioGanadoLista;
        }

        /// <summary>
        ///     Metodo que crear un PrecioGanado
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(PrecioGanadoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerParametrosCrear(info);
                infoId = Create("[dbo].[PrecioGanado_Crear]", parameters);
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
        ///     Metodo que actualiza un PrecioGanado
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(PrecioGanadoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[PrecioGanado_Actualizar]", parameters);
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
        ///     Obtiene un PrecioGanadoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal PrecioGanadoInfo ObtenerPorID(int infoId)
        {
            PrecioGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[PrecioGanado_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPrecioGanadoDAL.ObtenerPorID(ds);
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
        ///  Obtiene una lista de PrecioGanados filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<PrecioGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("PrecioGanado_ObtenerTodos", parameters);
                IList<PrecioGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPrecioGanadoDAL.ObtenerTodos(ds);
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
        ///     Obtiene un PrecioGanadoInfo por Id
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal PrecioGanadoInfo ObtenerPorOrganizacionTipoGanado(PrecioGanadoInfo info)
        {
            PrecioGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPrecioGanadoDAL.ObtenerPorOrganizacionTipoGanado(info);
                DataSet ds = Retrieve("[dbo].[PrecioGanado_ObtenerPorOrganizacionTipoGanado]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPrecioGanadoDAL.ObtenerPorOrganizacionTipoGanado(ds);
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
       
    }
}
