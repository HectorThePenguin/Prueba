using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProgramacionCorteDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="programacionCorte"></param>
        internal int GuardarProgramacionCorte(IList<ProgramacionCorteInfo> programacionCorte)
        {

            try
            {
                Logger.Info();
                var parametros = AuxProgramacionCorte.ObtenerParametrosGuardado(programacionCorte);
                return Create("ProgramacionCorte_Crear", parametros);
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
        internal int EliminarProgramacionCorte(ProgramacionCorte programacionCorte, int organizacionID)
        {

            try
            {
                Logger.Info();
                var parametros = AuxProgramacionCorte.ObtenerParametrosEliminarProgramacionCorte(programacionCorte, organizacionID);
                return Create("ProgramacionCorte_EliminarProgramacionCorte", parametros);
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
        /// Se actualiza la fecha inicio de la programacion corte
        /// </summary>
        /// <param name="programacionCorte"></param>
        /// <returns></returns>
        public void ActualizarFechaInicioProgramacionCorte(ProgramacionCorte programacionCorte)
        {
            try
            {
                Logger.Info();
                var parametros = AuxProgramacionCorte.ObtenerParametrosActualizarFechaInicioProgramacionCorte(programacionCorte);
                Update("ProgramacionCorte_ActualizarFechaInicio", parametros);
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
