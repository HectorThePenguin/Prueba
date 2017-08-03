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
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaGanadoMuerteDAL : DALBase
    {
        /// <summary>
        /// Obtiene un registro de EntradaGanadoTransito
        /// </summary>
        /// <param name="entradaGanadoID">Identificador de la entradaGanadoID</param>
        /// <returns></returns>
        internal List<EntradaGanadoMuerteInfo> ObtenerMuertesEnTransitoPorEntradaGanadoID(int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@EntradaGanadoID", entradaGanadoID } };
                var ds = Retrieve("EntradaGanadoMuerte_ObtenerPorEntradaGanadoID", parametros);
                List<EntradaGanadoMuerteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoMuerteDAL.ObtenerMuertesEnTransitoPorEntradaGanadoID(ds);
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
        /// Actualiza datos de entrada ganado muerte
        /// </summary>
        /// <param name="list"></param>
        internal void Actualizar(List<EntradaGanadoMuerteInfo> entradasGanadoMuertes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEntradaGanadoMuerteDAL.ObtenerParametrosActualizarMuertesXML(entradasGanadoMuertes);
                Update("EntradaGanadoMuerte_Actualizar", parametros);
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
