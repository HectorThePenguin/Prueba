using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    class AlertaDAL:DALBase
    {
        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion que se usara para la consulta</param>
        /// <param name="filtro">filtros de busqueda para la busqueda</param>
        /// <returns>Regresa una lista de alertas que concuerden con los filtros de busqueda proporcionados</returns>
        internal ResultadoInfo<AlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, AlertaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlertaDAL.ObtenerParametrosPorPagina(pagina,filtro);
                DataSet ds = Retrieve("Alerta_ObtenerPorPagina", parameters);
                ResultadoInfo<AlertaInfo> result ;

                if (ValidateDataSet(ds))
                {
                    result = MapAlertaDAL.ObtenerPorPagina(ds);
                }
                else
                {
                    result = new ResultadoInfo<AlertaInfo>();
                    result.Lista = new List<AlertaInfo>();
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
        /// Metodo para Crear un registro de Alerta
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(AlertaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlertaDAL.ObtenerParametrosCrear(info);
                int result = Create("Alerta_Crear", parameters);
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
        /// Metodo para actualizar un registro de Alerta
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(AlertaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlertaDAL.ObtenerParametrosActualizar(info);
                Update("Alerta_actualizar", parameters);
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
        /// consulta si se encuentra registrada una alerta con una descripcion y modulo especificos
        /// </summary>
        /// <param name="alerta">descripcion y modulo de la alerta que se verificara existencia</param>
        /// <returns>Regresa true si existe la alerta proporcionada</returns>
        internal bool AlertaExiste(AlertaInfo alerta)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAlertaDAL.ObtenerParametrosAlertaValida(alerta);
                var result= RetrieveValue<int>("Alertas_AlertaValida", parameters);
                return result==1;
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
