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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class LoteDistribucionAlimentoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de LoteDistribucionAlimento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(LoteDistribucionAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDistribucionAlimentoDAL.ObtenerParametrosCrear(info);
                int result = Create("LoteDistribucionAlimento_Crear", parameters);
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
        /// Metodo para actualizar un registro de LoteDistribucionAlimento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(LoteDistribucionAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDistribucionAlimentoDAL.ObtenerParametrosActualizar(info);
                Update("LoteDistribucionAlimento_Actualizar", parameters);
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
        /// Obtiene una lista de LoteDistribucionAlimento
        /// </summary>
        /// <returns></returns>
        internal IList<LoteDistribucionAlimentoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("LoteDistribucionAlimento_ObtenerTodos");
                IList<LoteDistribucionAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDistribucionAlimentoDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<LoteDistribucionAlimentoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDistribucionAlimentoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("LoteDistribucionAlimento_ObtenerTodos", parameters);
                IList<LoteDistribucionAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDistribucionAlimentoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de LoteDistribucionAlimento
        /// </summary>
        /// <param name="loteDistribucionAlimentoID">Identificador de la LoteDistribucionAlimento</param>
        /// <returns></returns>
        internal LoteDistribucionAlimentoInfo ObtenerPorID(int loteDistribucionAlimentoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDistribucionAlimentoDAL.ObtenerParametrosPorID(loteDistribucionAlimentoID);
                DataSet ds = Retrieve("LoteDistribucionAlimento_ObtenerPorID", parameters);
                LoteDistribucionAlimentoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDistribucionAlimentoDAL.ObtenerPorID(ds);
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
        /// Obtiene una lista para la impresion de la distribucion de alimento
        /// </summary>
        /// <returns></returns>
        internal IList<ImpresionDistribucionAlimentoModel> ObtenerImpresionDistribucionAlimento(FiltroImpresionDistribucionAlimento filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDistribucionAlimentoDAL.ObtenerParametrosImpresionDistribucionAlimento(filtro);
                DataSet ds = Retrieve("LoteDistribucionAlimento_ObtenerImpresion", parameters);
                IList<ImpresionDistribucionAlimentoModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDistribucionAlimentoDAL.ObtenerImpresionDistribucionAlimento(ds);
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

