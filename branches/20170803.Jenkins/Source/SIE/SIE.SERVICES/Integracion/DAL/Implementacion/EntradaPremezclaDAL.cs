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
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaPremezclaDAL : DALBase
    {
        /// <summary>
        /// Metodo para almacenar los movimientos que genera la premezcla
        /// </summary>
        /// <param name="entradaPremezclaInfo"></param>
        internal int Guardar(EntradaPremezclaInfo entradaPremezclaInfo)
        {
            int respuesta;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaPremezclaDAL.ObtenerParametrosGuardado(entradaPremezclaInfo);
                respuesta = Create("EntradaPremezcla_Guardar", parametros);
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
            return respuesta;
        }

        /// <summary>
        /// Obtiene una lista de entrada premezcla por movimiento de entrada
        /// </summary>
        /// <param name="entradaPremezclaInfo"></param>
        /// <returns></returns>
        internal List<EntradaPremezclaInfo> ObtenerPorMovimientoEntrada(AlmacenMovimientoInfo almacenMovimiento)
        {
            List<EntradaPremezclaInfo> listaEntradaPremezcla = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaPremezclaDAL.ObtenerParametrosObtenerPorMovimientoEntrada(almacenMovimiento);
                DataSet ds = Retrieve("EntradaProducto_ObtenerFolioPorPaginaEstatus", parameters);
                if (ValidateDataSet(ds))
                {
                    listaEntradaPremezcla = MapEntradaPremezclaDAL.ObtenerPorMovimientoEntrada(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return listaEntradaPremezcla;
        }
    }
}
