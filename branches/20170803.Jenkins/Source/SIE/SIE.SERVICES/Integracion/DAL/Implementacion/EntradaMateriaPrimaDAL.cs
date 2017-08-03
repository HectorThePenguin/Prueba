
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
    internal class EntradaMateriaPrimaDAL : DALBase
    {

        /// <summary>
        /// Obtiene los cotos de los fletes
        /// </summary>
        /// <returns></returns>
        internal List<CostoEntradaMateriaPrimaInfo> ObtenerCostosFletes(ContratoInfo contrato, EntradaProductoInfo entradadProducto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxEntradaMateriaPrimaDAL.ObtenerParametrosCostosFletes(contrato, entradadProducto);
                var ds = Retrieve("EntradaMateriaPrima_ObtenerFletesDetallePorContrato", parameters);
                List<CostoEntradaMateriaPrimaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaMateriaPrima.ObtenerCostosFletes(ds);
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
        /// Metodo para guardar los costos de una entrada de materia prima
        /// </summary>
        /// <param name="entradaMateriaPrima">Continen todos los datos necesarios para guardar</param>
        /// <returns>Regresa el resultado de la operacion</returns>
        internal bool GuardarEntradaMateriaPrima(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima)
        {
            try
            {
                Logger.Info();
                var parameters = AuxEntradaMateriaPrimaDAL.ObtenerParametrosPorPaginaTiposProveedores(entradaMateriaPrima);
                Create("EntradaMateriaPrima_Guardar", parameters);
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
        /// Obtiene la merma permitida por folio de entrada
        /// </summary>
        /// <param name="entradadProducto"></param>
        /// <returns></returns>
        internal CostoEntradaMateriaPrimaInfo ObtenerMermaPermitida(EntradaProductoInfo entradadProducto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxEntradaMateriaPrimaDAL.ObtenerParametrosMermaPermitida( entradadProducto);
                var ds = Retrieve("EntradaMateriaPrima_ObtenerMermaPermitida", parameters);
                CostoEntradaMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaMateriaPrima.ObtenerMermaPermitida(ds);
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
