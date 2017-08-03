using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Auxiliar;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ControlEntradaGanadoDAL : DALBase
    {
        /// <summary>
        /// Metodo para obtener Contro entrada ganado Por AnimalID o EntradaGanadoID
        /// </summary>
        
        internal List<ControlEntradaGanadoInfo> ObtenerControlEntradaGanadoPorID(int animalID, int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxControlEntradaGanadoDAL.ObtenerParametrosObtenerControlEntradaGanadoPorID(animalID, entradaGanadoID);
                var ds = Retrieve("ControlEntradaGanado_ObtenerPorAnimalIDOEntradaGanadoID", parameters);
                List<ControlEntradaGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapControlEntradaGanadoDAL.ObtenerControlEntradaGanado(ds);
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
        /// Guarda Control entrada ganado
        /// </summary>
        /// <param name="controlEntradaGanado"></param>
        /// <returns></returns>
        internal bool GuardarControlEntradaGanado(ControlEntradaGanadoInfo controlEntradaGanado)
        {
            try
            {
                Logger.Info();
                var parameters = AuxControlEntradaGanadoDAL.ObtenerParametrosGuardarControlEntradaGanado(controlEntradaGanado);
                Create("ControlEntradaGanado_GuardaControlEntradaGanado", parameters);
                //var result = 1;

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
        /// Elimina control entrada de ganado por EntradaGanadoID
        /// </summary>
        /// <param name="EntradaGanadoID"></param>
        /// <returns></returns>
        internal bool EliminaControlEntradaGanado(int EntradaGanadoID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxControlEntradaGanadoDAL.ObtenerParametrosEliminaControlEntradaGanado(EntradaGanadoID);
                Delete("ControlEntradaGanado_EliminaPorEntradaGanadoID", parameters);

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
    }
}
