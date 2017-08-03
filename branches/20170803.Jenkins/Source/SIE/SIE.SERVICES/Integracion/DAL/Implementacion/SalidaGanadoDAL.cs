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

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SalidaGanadoDAL : DALBase
    {
        /// <summary>
        /// Metodo para validar si existe la venta en la salida Ganado
        /// </summary>
        /// <param name="salidaGanadoInfo"></param>
        /// <returns></returns>
        internal SalidaGanadoInfo ExisteVentaEnSalidaGanado(SalidaGanadoInfo salidaGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSalidaGanadoDAL.ObtenerParametrosExisteVentaEnSalidaGanado(salidaGanadoInfo);
                var ds = Retrieve("SalidaGanado_ExisteVentaEnSalidaGanado", parameters);
                SalidaGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaGanadoDAL.ObtenerSalidaGanado(ds);
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
        /// Metodo para guardar la salida ganado
        /// </summary>
        /// <param name="salidaGanadoInfo"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal SalidaGanadoInfo GuardarSalidaGanado(SalidaGanadoInfo salidaGanadoInfo, int tipoFolio)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSalidaGanadoDAL.ObtenerParametrosSalidaGanado(salidaGanadoInfo, tipoFolio);
                var ds = Retrieve("SalidaGanado_Guardar", parameters);
                SalidaGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaGanadoDAL.ObtenerSalidaGanado(ds);
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
