using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class AnimalConsumoBL
    {
        /// <summary>
        /// Envia el consumo del animal al historico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EnviarAnimalConsumoAHistorico(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var animalConsumoDAL = new AnimalConsumoDAL();
                animalConsumoDAL.EnviarAnimalConsumoAHistorico(animalInactivo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        public void EliminarAnimalConsumo(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var animalConsumoDAL = new AnimalConsumoDAL();
                animalConsumoDAL.EliminarAnimalConsumo(animalInactivo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
                var animalConsumoDAL = new AnimalConsumoDAL();
                animalConsumoDAL.CrearTablasTemporalesConsumo();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
