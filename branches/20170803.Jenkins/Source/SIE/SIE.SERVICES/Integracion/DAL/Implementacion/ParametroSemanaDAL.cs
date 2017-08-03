using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ParametroSemanaDAL:DALBase
    {
        /// <summary>
        /// Metodo para obtener pedido ganado semanal
        /// </summary>
        internal ParametroSemanaInfo ObtenerParametroSemanaPorDescripcion(ParametroSemanaInfo parametroSemanaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxParametroSemanaDAL.ObtenerParametrosObtenerParametroSemanaPorDescripcion(parametroSemanaInfo);
                var ds = Retrieve("ParametroSemana_ObtenerPorDescripcion", parameters);
                ParametroSemanaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroSemanaDAL.ObtenerParametroSemana(ds);
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
        /// Metodo para obtener numero de semana
        /// </summary>
        internal int ObtenerNumeroSemana(DateTime fechaCalcular)
        {
            try
            {
                Logger.Info();
                var parameters = AuxParametroSemanaDAL.ObtenerParametrosObtenerNumeroSemana(fechaCalcular);
                var ds = Retrieve("PedidoGanado_ObtenerNumeroSemana", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroSemanaDAL.ObtenerNumeroSemana(ds);
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
