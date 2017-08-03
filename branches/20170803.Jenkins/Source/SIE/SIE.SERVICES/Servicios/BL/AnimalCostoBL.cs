using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class AnimalCostoBL
    {
        /// <summary>
        /// Metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public bool ValdiarTieneCostoGanadoAnimal(AnimalInfo animalInactivo, int costoGanado)
        {
            var tieneCosto = false;
            try
            {
                Logger.Info();
                var animalCostoDAL = new AnimalCostoDAL();
                tieneCosto = animalCostoDAL.ValdiarTieneCostoGanadoAnimal(animalInactivo, costoGanado);
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
            return tieneCosto;
        }

        /// <summary>
        /// Metodo para enviar animal de AnimalCosto a AnimalCostoHistorico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public bool EnviarAnimalCostoAHistorico(AnimalInfo animalInactivo)
        {
            var envioCosto = false;
            try
            {
                Logger.Info();
                var animalCostoDAL = new AnimalCostoDAL();

                //Se envia el animal de AnimalCosto a AnimalCostoHistorico
                animalCostoDAL.EnviarAnimalCostoAHistorico(animalInactivo);
                //Se elimina el animal de AnimalCosto
                animalCostoDAL.EliminarAnimalCosto(animalInactivo);

                envioCosto = true;
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
            return envioCosto;
        }

        /// <summary>
        /// Obtiene los costos por animal
        /// </summary>
        /// <param name="animalesGenerarPoliza"></param>
        /// <returns></returns>
        public List<AnimalCostoInfo> ObtenerCostosAnimal(List<AnimalInfo> animalesGenerarPoliza)
        {
            List<AnimalCostoInfo> result;
            try
            {
                Logger.Info();
                var animalCostoDAL = new AnimalCostoDAL();
                result = animalCostoDAL.ObtenerCostosAnimal(animalesGenerarPoliza);
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
            return result;
        }
    }
}
