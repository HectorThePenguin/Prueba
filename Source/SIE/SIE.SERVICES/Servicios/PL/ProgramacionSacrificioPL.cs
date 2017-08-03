using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
namespace SIE.Services.Servicios.PL
{
    public class ProgramacionSacrificioPL
    {

        /// Metodo para obtener animal en caso de existir
        public AnimalInfo ObtenerExistenciaAnimal(AnimalInfo animalInfo, int loteID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var programacionSacrificio = new ProgramacionSacrificioBL();
                result = programacionSacrificio.ObtenerExistenciaAnimal(animalInfo, loteID);
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

        /// Metodo para obtener animal en caso de existir
        public int GuardarAnimalSalida(List<AnimalInfo> listaAnimales, ProgramacionSacrificioGuardadoInfo programacionSacrificioGuardadoInfo)
        {
            int result;
            try
            {
                Logger.Info();
                var programacionSacrificio = new ProgramacionSacrificioBL();
                result = programacionSacrificio.GuardarAnimalSalida(listaAnimales, programacionSacrificioGuardadoInfo);
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
