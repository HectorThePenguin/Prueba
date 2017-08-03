
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class GastoMateriaPrimaDAL:DALBase
    {
        internal int Guardar(GastoMateriaPrimaInfo gasto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxGastoMateriaPrimaDAL.ObtenerParametrosGuardar(gasto);
                int result = Create("GastoMateriaPrima_Crear", parameters);
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
        /// Obtiene un registro de GastoMateriaPrima
        /// </summary>
        /// <param name="gastoMateriaPrimaID">Identificador de la GastoMateriaPrima</param>
        /// <returns></returns>
        public GastoMateriaPrimaInfo ObtenerPorID(int gastoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGastoMateriaPrimaDAL.ObtenerParametrosPorID(gastoMateriaPrimaID);
                DataSet ds = Retrieve("GastoMateriaPrima_ObtenerPorID", parameters);
                GastoMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGastoMateriaPrimaDAL.ObtenerPorID(ds);
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
        /// Obtiene una lista de gastos de materia prima
        /// por sus movimientos de almacen
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal IEnumerable<GastoMateriaPrimaInfo> ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxGastoMateriaPrimaDAL.ObtenerParametrosPorAlmacenMovimientoXML(almacenesMovimiento);
                IMapBuilderContext<GastoMateriaPrimaInfo> mapeo = MapGastoMateriaPrimaDAL.ObtenerPolizasConciliacion();
                IEnumerable<GastoMateriaPrimaInfo> foliosFaltantes = GetDatabase().ExecuteSprocAccessor
                    <GastoMateriaPrimaInfo>(
                        "GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP", mapeo.Build(),
                        new[] { parameters["@XmlAlmacenMovimiento"] });
                return foliosFaltantes;
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

        internal IEnumerable<AreteInfo> ConsultarAretes(List<AreteInfo> aretes, int organizacionID, bool esAreteSukarne, bool esEntradaAlmacen)
        {
            try
            {
                Logger.Info();
                IEnumerable<AreteInfo> result = new List<AreteInfo>();
                var parameters = AuxGastoMateriaPrimaDAL.ObtenerParametrosValidarAretes(aretes, organizacionID, esAreteSukarne, esEntradaAlmacen);
                var ds = Retrieve("GastosMateriasPrimas_ValidarAretes", parameters);
                if (ValidateDataSet(ds))
                    result = MapGastoMateriaPrimaDAL.MapeoAretes(ds);

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

        internal void GuardarAretes(GastoMateriaPrimaInfo gasto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxGastoMateriaPrimaDAL.ObtenerParametrosGuardarAretes(gasto);
                var ds = Create("GastosMateriasPrimas_GuardarAretes", parameters);
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
