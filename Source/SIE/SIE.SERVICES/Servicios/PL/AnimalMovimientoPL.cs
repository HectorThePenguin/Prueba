using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AnimalMovimientoPL
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla AnimalMovimiento
        /// </summary>
        public AnimalMovimientoInfo GuardarAnimalMovimiento(AnimalMovimientoInfo animalMovimientoInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var animalMovimientoBL = new AnimalMovimientoBL();
                result = animalMovimientoBL.GuardarAnimalMovimiento(animalMovimientoInfo);

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

        /// <summary>
        /// Obtener los movimientos de un animal por su arete
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="arete"></param>
        /// <returns></returns>
        public List<AnimalMovimientoInfo> ObtenerMovimientosPorArete(int organizacionID, string arete)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoBL = new AnimalMovimientoBL();
                result = animalMovimientoBL.ObtenerMovimientosPorArete(organizacionID, arete);
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

        /// <summary>
        /// Obtiene una lista con los animales muertos a conciliar
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public IEnumerable<AnimalMovimientoInfo> ObtenerAnimalesMuertos(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoBL = new AnimalMovimientoBL();
                result = animalMovimientoBL.ObtenerAnimalesMuertos(organizacionID, fechaInicial, fechaFinal);
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

        /// <summary>
        /// Metrodo Para Guardar en en la tabla AnimalMovimiento
        /// </summary>
        public void GuardarAnimalMovimientoXML(List<AnimalMovimientoInfo> listaAnimalesMovimiento)
        {
            try
            {
                Logger.Info();
                var animalMovimientoBL = new AnimalMovimientoBL();
                animalMovimientoBL.GuardarAnimalMovimientoXML(listaAnimalesMovimiento);
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
        /// Obtiene el ultimo movimiento del animal
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        public List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimal(List<AnimalInfo> animales)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoBL = new AnimalMovimientoBL();
                result = animalMovimientoBL.ObtenerUltimoMovimientoAnimal(animales);
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
