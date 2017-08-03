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
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CorteGanadoDAL : DALBase
    {
        internal AnimalInfo ExisteAreteMetalicoEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result = null;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosExisteAreteMetalicoEnPartida(animalInfo);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ExisteAreteMetalicoEnPartida]", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapCorteGanadoDAL.ObtenerExisteExisteAreteEnPartida(ds);
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
        ///    Obtiene una Cantidad de Animales que se encuentran en enfermeria
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="noTipoCorral"></param>
        /// <returns></returns>
        internal int ObtenerCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int noTipoCorral)
        {
            var resp = 0;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosCabezasEnEnfermeria(ganadoEnfermeria, noTipoCorral);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria]", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapCorteGanadoDAL.ObtenerCabezasEnEnfermeria(ds);
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
            return resp;
        }

        /// <summary>
        ///    Se obtiene el proveedor
        /// </summary>
        /// <param name="noEmbarqueid"></param>
        /// <returns></returns>
        internal String ObtenerProveedor(int noEmbarqueid)
        {
            var resp = "";
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosNombreProveedor(noEmbarqueid);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerNombreProveedor]", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapCorteGanadoDAL.ObtenerNombreProveedor(ds);
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
            return resp;
        }

        /// <summary>
        ///    Se valida que existan programacion corte de ganado
        /// </summary>
        internal bool ExisteProgramacionCorteGanado(int organizacionID)
        {
            var resp = false;
            try
            {
                var parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID}
                                 };
                DataSet ds = Retrieve("CorteGanado_ExisteProgramacionCorteGanado", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapCorteGanadoDAL.ObtenerExisteProgramacionCorteGanado(ds);
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
            return resp;
        }

        /// <summary>
        /// Metrodo Verificar si existen Partidas programadas
        /// </summary>
        internal AnimalInfo ExisteAreteEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result = null;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosExisteAreteEnPartida(animalInfo);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ExisteAreteEnPartida]", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapCorteGanadoDAL.ObtenerExisteExisteAreteEnPartida(ds);
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
        /// Metrodo Para Registrar los movimientos en el almacen
        /// </summary>
        internal bool RegistrarMovimientosAlmacen(MovimientosAlmacen movimientosAlmacen)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosRegistrarMovimientosAlmacen(movimientosAlmacen);
                DataSet ds = Retrieve("[dbo].[usp_AlmMovimientos_Registrar]", parametros);
                if (ValidateDataSet(ds))
                {
                    //result = MapCorteGanadoDAL.ObteneRegistrarMovimientosAlmacen(ds);
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
        /// Metrodo Para Obtener pesos origen y llegada
        /// </summary>
        internal bool ObtenerPesosOrigenLlegada(int orgnizacionID, int corralOrigenID, int loteOrigenID)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosObteenerPesosOrigenLlegada(orgnizacionID, corralOrigenID, loteOrigenID);
                Update("CorteGanado_CierrePartidaPesoOrigenLLegada", parametros);
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
        /// Metrodo Para obtener el total de cabezas cortadas
        /// </summary>
        internal int ObtenerCabezasCortadas(CabezasCortadas cabezasCortadas)
        {
            int result = 0;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosObtenerCabezasCortadas(cabezasCortadas);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerCabezasCortadas]", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapCorteGanadoDAL.ObtenerCabezasCortadas(ds);
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
        /// Metrodo Para obtener el total de cabezas Muertas
        /// </summary>
        internal int ObtenerCabezasMuertas(CabezasCortadas cabezasMuertas)
        {
            int result = 0;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosObtenerCabezasMuertas(cabezasMuertas);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerTotalGanadoMuerto]", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapCorteGanadoDAL.ObtenerCabezasCortadas(ds);
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
        /// Obtener las partidas cortadas y sus sobrantes que lleva cortados
        /// </summary>
        /// <param name="paramCabezaSobrantesCortadas"></param>
        /// <returns></returns>
        internal IList<CabezasSobrantesPorEntradaInfo> ObtenerCabezasSobrantes(CabezasCortadas paramCabezaSobrantesCortadas)
        {
            IList<CabezasSobrantesPorEntradaInfo> result = null;
            try
            {
                Dictionary<string, object> parametros = AuxCorteGanadoDAL.ObtenerParametrosObtenerCabezasSobrantesCortadas(paramCabezaSobrantesCortadas);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado]", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapCorteGanadoDAL.ObtenerCabezasSobrantesCortadas(ds);
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

      
    }
}
