using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
    public class ProgramaciondeFletesDAL : DALBase
    {
        /// <summary>
        /// Obtiene los contratos por tipo
        /// </summary>
        /// <param name="activo"></param>
        /// <param name="tipoFlete"></param>
        /// <returns></returns>
        public List<ContratoInfo> ObtenerContratosPorTipo(int activo, int tipoFlete)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxProgramaciondeFletesDAL.ObtenerParametrosObtenerContratosPorTipo(activo, tipoFlete);
                DataSet ds = Retrieve("ProgramacionFletes_ObtenerContratoPorTipo", parametros);
                List<ContratoInfo> contratoInfos = null;

                if (ValidateDataSet(ds))
                {
                    contratoInfos = MapProgramaciondeFletesDAL.ObtenerProgramacionFletes(ds);
                }
                return contratoInfos;
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
        /// ObtenerFletes
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        public List<ProgramaciondeFletesInfo> ObtenerFletes(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxProgramaciondeFletesDAL.ObtenerParametrosObtenerFletes(contratoInfo);
                DataSet ds = Retrieve("ProgramacionFletes_ObtenerFletes", parametros);
                List<ProgramaciondeFletesInfo> tratamientos = null;

                if (ValidateDataSet(ds))
                {
                    tratamientos = MapProgramaciondeFletesDAL.ObtenerProgramacionFletesDetalle(ds);
                }
                return tratamientos;
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
        /// Guardar programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        public void GuardarProgramaciondeFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramaciondeFletesDAL.GuardarProgramaciondeFlete(listaProgramaciondeFletesInfos);
                Update("ProgramacionFletes_GuardarProgramacionFlete", parameters);
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
        /// Guardar programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <param name="usuarioModificacionId"></param>
        public bool EliminarProveedorFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos,
            int usuarioModificacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramaciondeFletesDAL.ObtenerEliminarProveedorFlete(listaProgramaciondeFletesInfos,
                        usuarioModificacionId);
                Update("ProgramacionFletes_EstatusProgramacionFlete", parameters);
                return true;
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
        /// Guardar programacion de fletes
        /// </summary>
        /// <param name="listaFletesDetalle"></param>
        /// <param name="usuarioModificacionId"></param>
        public bool EliminarFleteDetalle(List<FleteDetalleInfo> listaFletesDetalle, int usuarioModificacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramaciondeFletesDAL.ObtenerEliminarFleteDetalle(listaFletesDetalle, usuarioModificacionId);
                Update("ProgramacionFletes_EstatusProgramacionFleteDetalle", parameters);
                return true;
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
        /// Guardar programacion flete detalle
        /// </summary>
        /// <param name="regresoCostosFletes"></param>
        public bool GuardarProgramaciondeFleteDetalle(List<FleteDetalleInfo> regresoCostosFletes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramaciondeFletesDAL.GuardarProgramaciondeFleteDetalle(regresoCostosFletes);
                Update("ProgramacionFletes_GuardarProgramacionFleteDetalle", parameters);
                return true;
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
        /// Cancela la programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        internal bool CancelarProgramacionFlete(ProgramaciondeFletesInfo listaProgramaciondeFletesInfos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramaciondeFletesDAL.CancelarProgramacionFlete(listaProgramaciondeFletesInfos);
                Update("Flete_CancelarFleteID", parameters);
                return true;
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
        /// Obtine los contratos por tipo y por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ContratoInfo> ObtenerPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxProgramaciondeFletesDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProgramacionFletes_ObtenerContratoPorTipoPorPagina", parametros);
                ResultadoInfo<ContratoInfo> contratoInfos = null;

                if (ValidateDataSet(ds))
                {
                    contratoInfos = MapProgramaciondeFletesDAL.ObtenerProgramacionFletesPorPagina(ds);
                }
                return contratoInfos;
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

