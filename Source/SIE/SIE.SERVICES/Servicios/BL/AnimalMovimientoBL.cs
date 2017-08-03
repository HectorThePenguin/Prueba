using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AnimalMovimientoBL
    {
        /// <summary>
        /// Metodo para enviar animal de AnimalCosto a AnimalCostoHistorico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public bool EnviarAnimalMovimientoAHistorico(AnimalInfo animalInactivo)
        {
            var envioCosto = false;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();

                //Se envia el animal de AnimalMovimiento a AnimalMovimientoHistorico
                animalMovimientoDAL.EnviarAnimalMovimientoAHistorico(animalInactivo);
                //Se elimina el animal de AnimalMovimiento
                animalMovimientoDAL.EliminarAnimalMovimiento(animalInactivo);

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
        /// Metrodo Para Guardar en en la tabla AnimalMovimiento
        /// </summary>
        internal AnimalMovimientoInfo GuardarAnimalMovimiento(AnimalMovimientoInfo animalMovimientoInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.GuardarAnimalMovimiento(animalMovimientoInfo);
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
        /// Obtiene el ultimo movimiento del animal
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimal(List<AnimalInfo> animales)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerUltimoMovimientoAnimal(animales);
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
        /// Obtener los animales q no fueron reimplantados
        /// </summary>
        /// <param name="loteId"></param>
        /// <param name="OrganizacionID"></param>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantados(int loteId, int OrganizacionID)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerAnimalesNoReimplantados(loteId, OrganizacionID);
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
        /// Validar si el animal es carga inicial
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        internal bool ObtenerEsCargaInicialAnimal(long animalId)
        {
            bool result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerEsCargaInicialAnimal(animalId);
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
        internal List<AnimalMovimientoInfo> ObtenerMovimientosPorArete(int organizacionID, string arete)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerMovimientosPorArete(organizacionID, arete);
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
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<AnimalMovimientoInfo> ObtenerAnimalesMuertos(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerAnimalesMuertos(organizacionID, fechaInicial, fechaFinal);
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
        internal void GuardarAnimalMovimientoXML(List<AnimalMovimientoInfo> listaAnimalesMovimiento)
        {
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                animalMovimientoDAL.GuardarAnimalMovimientoXML(listaAnimalesMovimiento);
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
        /// Obtiene los animales reimplantados por XML
        /// </summary>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantadosXML(int organizacionID, List<LoteInfo> lotes)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerAnimalesNoReimplantadosXML(organizacionID, lotes);
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
        /// Metodo para obtener la trazabilidad de los movimientos de un animal
        /// </summary>
        /// <param name="animal"></param>
        public AnimalInfo ObtenerTrazabilidadAnimalMovimiento(AnimalInfo animal)
        {
            var result = new AnimalInfo();
            try
            {
                Logger.Info();
                var animalMovimientoDAL = new AnimalMovimientoDAL();
                result = animalMovimientoDAL.ObtenerTrazabilidadAnimalMovimiento(animal);
                
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
