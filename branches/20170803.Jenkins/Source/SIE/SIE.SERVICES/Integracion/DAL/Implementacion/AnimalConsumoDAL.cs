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

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AnimalConsumoDAL : DALBase
    {
        /// <summary>
        /// Envia el consumo del animal al historico
        /// </summary>
        /// <param name="animalInactivo"></param>
        internal void EnviarAnimalConsumoAHistorico(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> {{"@AnimalID", animalInactivo.AnimalID}};
                Create("AnimalConsumo_EnviarAnimalConsumoAHistorico", parameters);
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
        /// Elimina el consumo de tabla
        /// </summary>
        /// <param name="animalInactivo"></param>
        internal void EliminarAnimalConsumo(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> { { "@AnimalID", animalInactivo.AnimalID } };
                Delete("AnimalConsumo_EliminarAnimalConsumo", parameters);
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
        /// Crea las tablas Temporales para la ejecucion del Consumo
        /// </summary>
        internal void CrearTablasTemporalesConsumo()
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>();
                Create("GenerarTablasTemporalesConsumo", parameters);
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
