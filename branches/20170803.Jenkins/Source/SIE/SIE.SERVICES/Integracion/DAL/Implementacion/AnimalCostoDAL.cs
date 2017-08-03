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
    internal class AnimalCostoDAL : DALBase
    {
        /// <summary>
        /// Metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public bool ValdiarTieneCostoGanadoAnimal(AnimalInfo animalInactivo, int costoGanado)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalCostoDAL.ObtenerParametrosValdiarTieneCostoGanadoAnimal(animalInactivo, costoGanado);
                var respuesta = RetrieveValue<int>("AnimalCosto_ValdiarTieneCostoGanadoAnimal", parameters);
                
                bool result = respuesta > 0;

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
        /// Metodo para enviar AnimalCosto A historico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EnviarAnimalCostoAHistorico(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalCostoDAL.ObtenerParametrosAnimalCostoID(animalInactivo);
                Create("AnimalCosto_EnviarAnimalCostoAHistorico", parameters);              
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
        /// Metodo elimina el animal de AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EliminarAnimalCosto(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalCostoDAL.ObtenerParametrosAnimalCostoID(animalInactivo);
                Delete("AnimalCosto_EliminarAnimalCosto", parameters);
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
        /// Obtiene los costos por animal
        /// </summary>
        /// <param name="animalesGenerarPoliza"></param>
        /// <returns></returns>
        internal List<AnimalCostoInfo> ObtenerCostosAnimal(List<AnimalInfo> animalesGenerarPoliza)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalCostoDAL.ObtenerParametrosCostosAnimal(animalesGenerarPoliza);
                DataSet ds = Retrieve("AnimalCosto_ObtenerPorAnimalXML", parameters);
                List<AnimalCostoInfo> costosAnimal = MapAnimalCostoDAL.ObtenerCostosAnimal(ds);
                return costosAnimal;
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
