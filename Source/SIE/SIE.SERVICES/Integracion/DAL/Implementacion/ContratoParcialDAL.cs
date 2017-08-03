using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ContratoParcialDAL : DALBase
    {
        /// <summary>
        /// Crea un contrato detalle
        /// </summary>
        /// <returns></returns>
        internal int CrearContratoParcial(List<ContratoParcialInfo> listaContratoParcialInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxContratoParcialDAL.ObtenerParametrosCrearContratoParcial(listaContratoParcialInfo);
                int result = Create("ContratoParcial_CrearContratosParciales", parameters);
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
        /// Obtiene una lista de parcialidades del contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<ContratoParcialInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoParcialDAL.ObtenerPorContratoId(contrato);
                DataSet ds = Retrieve("ContratoParcial_ObtenerPorContratoID", parameters);
                List<ContratoParcialInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapContratoParcialDAL.ObtenerPorContratoId(ds);
                }
                return lista;
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
        /// Actualiza el estado del un contrato parcial
        /// </summary>
        /// <param name="contratoParcialInfo"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstado(ContratoParcialInfo contratoParcialInfo, EstatusEnum estatus)
        {
            try
            {
                Dictionary<string, object> parameters = AuxContratoParcialDAL.ObtenerParametrosActualizarEstado(contratoParcialInfo, estatus);
                Update("ContratoParcial_ActualizarEstado", parameters);
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
        /// 
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal List<ContratoParcialInfo> ObtenerFaltantePorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoParcialDAL.ObtenerPorContratoId(contratoInfo);
                DataSet ds = Retrieve("ContratoParcial_ObtenerFaltantePorContratoID", parameters);
                List<ContratoParcialInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapContratoParcialDAL.ObtenerFaltantePorContratoId(ds);
                }
                return lista;
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
