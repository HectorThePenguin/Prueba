using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class NivelAlertaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de NivelAlerta
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(NivelAlertaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.ObtenerParametrosCrear(info);
                int result = Create("NivelAlerta_Crear", parameters);
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
        /// Metodo para actualizar un registro de Cliente
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(NivelAlertaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.ObtenerParametrosActualizar(info);
                Update("NivelAlerta_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<NivelAlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, NivelAlertaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("NivelAlerta_ObtenerPorPagina", parameters);
                ResultadoInfo<NivelAlertaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapNivelAlertaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene registros por descripcion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        internal NivelAlertaInfo ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.ObtenerPorDescripcion(Descripcion);
                DataSet ds = Retrieve("NivelAlerta_ObtenerPoDescripcion", parameters);
                NivelAlertaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapNivelAlertaDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// verifica si el nivel seleccionado no a sido asignado
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns></returns>
        internal int VerificarAsignacionNivelAlerta(int nivelAlertaId)
        {
            try
            {
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.VerificarAsignacionNivelAlerta(nivelAlertaId);
                int ds = Create("NivelAlerta_VerificarAsignacionNivelAlerta", parameters);

                return ds;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un contador de los niveles que se encuentran deshabilitados
        /// </summary>
        /// <returns></returns>
        internal int NivelesAlertaDesactivados()
        {
            try
            {
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.NivelesAlertaDesactivados();
                int ds = Create("NivelAlerta_VerificarNivelesDesactivados", parameters);

                return ds;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// SP que devuelve el primer campo inactivo de la tabla NivelAlerta 
        /// y verifica si es el mismo que se le envio.
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns>Si regresa 0 no es el primero deshabilitado si regresa > 0 es el primero deshabilitado</returns>
        internal int NivelAlerta_ActivarPrimerNivelDesactivado(int nivelAlertaId)
        {
            try
            {
                Dictionary<string, object> parameters = AuxNivelAlertaDAL.NivelAlerta_ActivarPrimerNivelDesactivado(nivelAlertaId);
                int ds = Create("NivelAlerta_ActivarPrimerNivelDesactivado", parameters);

                return ds;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}