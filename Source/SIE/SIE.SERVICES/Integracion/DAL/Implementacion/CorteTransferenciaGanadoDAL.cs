using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
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
    internal class CorteTransferenciaGanadoDAL : DALBase
    {
       
        /// <summary>
        /// Obtiene el permiso de la trampa
        /// </summary>
        /// <param name="trampaInfo"></param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPermisoTrampa(TrampaInfo trampaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCorteTransferenciaGanado.ObtenerParametrosObtenerTrampa(trampaInfo);
                var ds = Retrieve("CorteTransferenciaGanado_ObtenerPermisoTrampa", parameters);
                TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorteTransferenciaGanadoDAL.ObtenerPermisoTrampa(ds);
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
        /// Obtener entrada de ganado
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaGanado(AnimalInfo animalInfo)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxCorteTransferenciaGanado.ObtenerParametrosEntradaGanado(animalInfo);
                var ds = Retrieve("CorteTransferenciaGanado_ObtenerEntradaGanado", parameters);
                
                if (ValidateDataSet(ds))
                {
                    result = MapCorteTransferenciaGanadoDAL.ObtenerEntradaGanado(ds);
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
        /// Obtener totales
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal CorteTransferenciaTotalCabezasInfo ObtenerTotales(AnimalMovimientoInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var result = new CorteTransferenciaTotalCabezasInfo();
                var parameters = AuxCorteTransferenciaGanado.ObtenerParametrosTotales(animalInfo);
                var ds = Retrieve("CorteGanado_ObtenerTotalCabezasRecuperacion", parameters);
                
                if (ValidateDataSet(ds))
                {
                    result = MapCorteTransferenciaGanadoDAL.ObtenerCabezasCortadas(ds);
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
        /// Obtiene un listado de corrales por tipo
        /// </summary>
        /// <param name="tipoCorralInfo"></param>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerCorralesTipo(TipoCorralInfo tipoCorralInfo, int organizationID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorteTransferenciaGanado.ObtenerParametrosCorralesTipo(tipoCorralInfo, organizationID);
                DataSet ds = Retrieve("CorteTransferenciaGanado_ObtenerCorralesTipo", parametros);
                CorralInfo corrales = null;

                if (ValidateDataSet(ds))
                {
                    corrales = MapCorteTransferenciaGanadoDAL.ObtenerCorralesTipo(ds);
                }
                return corrales;
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
        ///  Obtiene un listado de corrales por tipo
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        internal List<TratamientoInfo> ObtenerTratamientosAnimal(AnimalInfo animalInfo, int tipoMovimiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorteTransferenciaGanado.ObtenerParametrosMovimientosAnimal(animalInfo, tipoMovimiento);
                DataSet ds = Retrieve("CorteTransferenciaGanado_ObtenerTratamientosAplicados", parametros);
                List<TratamientoInfo> tratamientos = null;

                if (ValidateDataSet(ds))
                {
                    tratamientos = MapCorteTransferenciaGanadoDAL.ObtenerTratamientosAplicados(ds);
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

    }
}
