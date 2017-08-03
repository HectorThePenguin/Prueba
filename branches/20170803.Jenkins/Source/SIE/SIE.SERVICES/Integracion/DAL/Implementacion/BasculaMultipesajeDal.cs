using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
   public class BasculaMultipesajeDAL : DALBase
    {
        /// <summary>
        /// Guarda un registro en la table BasculaMultipesaje
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <returns>regresa el  folio del registro que se acaba de registrar</returns>
        internal long InsertarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            long resultado = 0;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                AuxBasculaMultipesajeDAL.InsertarBasculaMultipesaje(basculaMultipesajeInfo);
                DataSet ds = Retrieve("BasculaMultipesaje_Crear", parameters);
                
                if (ValidateDataSet(ds))
                {
                    resultado = MapBasculaMultipesajeDAL.ObtenerFolioDespuesDeRegistrar(ds);
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
            return resultado;
        }

        /// <summary>
        /// Consulta por medio del folio un registro en BasculaMultipesaje
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>regresa los datos del folio consultado</returns>
        internal BasculaMultipesajeInfo ConsultarBasculaMultipesaje(long folio, int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBasculaMultipesajeDAL.ConsultaBasculaMultipesaje(folio, organizacionId);
                DataSet ds = Retrieve("BasculaMultipesaje_ObtenerPesaje", parameters);
                BasculaMultipesajeInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapBasculaMultipesajeDAL.ConsultarBasculaMultipesaje(ds);
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
        /// Obtiene un Folio por Id
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>regresa el folio consultado</returns>
        internal FolioMultipesajeInfo ObtenerFolioPorId(long folio, int organizacionId)
        {
            FolioMultipesajeInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBasculaMultipesajeDAL.ConsultaBasculaMultipesaje(folio, organizacionId);
                DataSet ds = Retrieve("BasculaMultipesaje_ObtenerPorFolio", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapBasculaMultipesajeDAL.ObtenerFolioPorId(ds);
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
        /// Obtiene los folios por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>una lista de folios del dia</returns>
        internal ResultadoInfo<FolioMultipesajeInfo> ObtenerPorPaginaFiltroFolios(PaginacionInfo pagina, FolioMultipesajeInfo filtro)
        {
            ResultadoInfo<FolioMultipesajeInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxBasculaMultipesajeDAL.ObtenerParametrosPorPaginaFiltroFolios(pagina, filtro);
                DataSet ds = Retrieve("BasculaMultipesaje_ObtenerFoliosPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapBasculaMultipesajeDAL.ObtenerPorPaginaCompleto(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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

            return lista;
        }

        /// <summary>
        /// Actualiza el registro del modulo BasculaMultipesaje
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        internal void ActualizarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBasculaMultipesajeDAL.ObtenerParametrosActualizar(basculaMultipesajeInfo);
                Update("BasculaMultipesaje_Actualizar", parameters);
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
