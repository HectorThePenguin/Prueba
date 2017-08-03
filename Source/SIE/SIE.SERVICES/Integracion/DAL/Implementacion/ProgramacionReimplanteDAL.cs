using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProgramacionReimplanteDAL : DALBase
    {
        /// <summary>
        /// Retorna de la base de datos la lista con los lotes disponibles para programar reimplante
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion</param>
        /// <returns>Lista con la informacion de lotes disponibles para reimplante</returns>
        internal ResultadoInfo<OrdenReimplanteInfo> ObtenerLotesDisponiblesReimplante(int organizacionId)
        {
            var retValue = new ResultadoInfo<OrdenReimplanteInfo>();

            try
            {
                Dictionary<string, object> parameters = 
                    AuxProgramacionReimplanteDAL.ObtenerParametrosPorOrganizacionId(organizacionId);
                DataSet ds = Retrieve("[dbo].[OrdenReimplante_ObtenerCorralesProgramar]", parameters);
                if (ValidateDataSet(ds))
                {
                    retValue = MapProgramacionReimplanteDAL.ObtenerLotesDisponiblesReimplante(ds);
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

            return retValue;
        }

        /// <summary>
        /// Almacena una orden de programacion de reimplante
        /// </summary>
        /// <param name="orden"></param>
        /// <returns></returns>
        internal int GuardarOrdenReimplante(OrdenReimplanteInfo orden)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var parametros = AuxProgramacionReimplanteDAL.ObtenerParametrosGuardado(orden);
                retValue = Create("ProgramacionReimplante_Crear", parametros);

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

            return retValue;
        }
        internal bool EliminarProgramacionReimplante(int folioProgReimplante)
        {
            bool regresa = false;

            try
            {
                Logger.Info();
                var parametros = AuxProgramacionReimplanteDAL.ObtenerParametrosProgramacionReimplante(folioProgReimplante);
                if (Create("ProgramacionReimplante_EliminarProgramacionReimplante", parametros) > 0)
                {
                    return true;
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

            return regresa;
        }

        internal bool GuardarFechaReal(String fechaReal, LoteReimplanteInfo loteReimplante)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var parametros = AuxProgramacionReimplanteDAL.ObtenerParametrosFechaReal(fechaReal, loteReimplante);
                if (Create("ReimplanteGanado_ActualizaFechaReal", parametros) > 0)
                {
                    return true;
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

            return regresa;
        }

        internal bool ExisteProgramacionReimplante(int organizacionId, DateTime selectedDate)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var parametros = AuxProgramacionReimplanteDAL.ObtenerParametrosValidacionProgramacion(selectedDate, organizacionId);
                DataSet ds = Retrieve("ProgramacionReimplante_ValidarProgramacion", parametros);
                if (ValidateDataSet(ds))
                {
                    return true;
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

            return regresa;
        }

        /// <summary>
        /// Retorna de la base de datos la lista de programacion reimplante por LoteID
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion</param>
        /// <returns>Lista con la informacion de la tabla ProgramacionReinplante</returns>
        internal List<ProgramacionReinplanteInfo> ObtenerProgramacionReimplantePorLoteID(LoteInfo lote)
        {
            var retValue = new List<ProgramacionReinplanteInfo>();

            try
            {
                Dictionary<string, object> parameters =
                    AuxProgramacionReimplanteDAL.ObtenerParametrosProgramacionReimplantePorLoteID(lote);
                DataSet ds = Retrieve("[dbo].[ProgramacionReimplante_ObtenerPorLoteID]", parameters);
                if (ValidateDataSet(ds))
                {
                    retValue = MapProgramacionReimplanteDAL.ObtenerProgramacionReimplantePorLoteID(ds);
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

            return retValue;
        }

        /// <summary>
        /// Validar que el reimplante sea de corral a corral
        /// </summary>
        /// <param name="loteIDOrigen"></param>
        /// <param name="corralIDDestino"></param>
        /// <returns></returns>
        internal bool ValidarReimplanteCorralACorral(int loteIDOrigen, int corralIDDestino)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@LoteIDOrigen", loteIDOrigen }, { "@CorralIDDestino", corralIDDestino } };
                var resp = RetrieveValue<int>("ProgramacionReimplante_ValidarReimplanteCorralACorral", parametros);
                if (resp>0)
                {
                    return true;
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

            return regresa;
        }

        /// <summary>
        /// Elimina la programacion de reimplante
        /// </summary>
        /// <param name="corralesProgramados"></param>
        internal void EliminarProgramacionReimplanteXML(List<ProgramacionReinplanteInfo> corralesProgramados)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros 
                    = AuxProgramacionReimplanteDAL.ObtenerParametrosProgramacionReimplanteXML(corralesProgramados);
                Update("ProgramacionReimplante_EliminarProgramacionReimplanteXML", parametros);
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
        /// Cierra las programaciones de Reimplante pendientes
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="usuarioID"></param>
        internal void CerrarProgramacionReimplante(int organizacionID, int usuarioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros
                    = AuxProgramacionReimplanteDAL.ObtenerParametrosCerrarProgramacionReimplante(organizacionID, usuarioID);
                Update("ReimplanteGanado_CierreReimplante", parametros);
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
