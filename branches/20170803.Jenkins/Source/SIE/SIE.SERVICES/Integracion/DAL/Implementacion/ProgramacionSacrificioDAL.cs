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
    internal class ProgramacionSacrificioDAL : DALBase
    {

        /// Obtiene un animal en casi de que exista
        internal AnimalInfo ObtenerExistenciaAnimal(AnimalInfo animalInfo, int loteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxProgramacionSacrificio.ObtenerParametrosObtenerExistenciaAnimal(animalInfo, loteID);
                DataSet ds = Retrieve("ProgramacionSacrificio_ObtenerExistenciaAnimal", parametros);
                AnimalInfo result = null;

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionSacrificioDAL.ObtenerExistenciaAnimal(ds);
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

        internal int GuardarAnimalSalida(List<AnimalInfo> listaAnimales, ProgramacionSacrificioGuardadoInfo programacionSacrificioGuardadoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionSacrificio.ObtenerParametrosGuardarAnimalSalida(listaAnimales, programacionSacrificioGuardadoInfo);
                int result = Create("ProgramacionSacrificio_GuardarAnimalSalida", parameters);
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
