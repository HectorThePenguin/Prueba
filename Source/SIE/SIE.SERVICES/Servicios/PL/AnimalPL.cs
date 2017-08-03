using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AnimalPL
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        public AnimalInfo GuardarAnimal(AnimalInfo animalInfo)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.GuardarAnimal(animalInfo);

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
        /// Metrodo Para obtener el peso
        /// </summary>
        public TrampaInfo ObtenerPeso(int organizacionID, int folioEntrada)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerPeso(organizacionID, folioEntrada);

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
        /// Metodo para obtener un listado de animales por Codigo de Corral
        /// </summary>
        public List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(CorralInfo corralInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesPorCodigoCorral(corralInfo);
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

        public List<AnimalInfo> ObtenerAnimalesPorCorral(CorralInfo corralInfo, int organizacionId)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesPorCorral(corralInfo, organizacionId);
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

        public AnimalMovimientoInfo ObtenerUltimoMovimientoAnimal(AnimalInfo animalInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerUltimoMovimientoAnimal(animalInfo);
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

        public AnimalInfo ObtenerAnimalPorArete(string arete, int organizacionId)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalPorArete(arete, organizacionId);
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
        /// Obtiene el peso proyectado de un animal desde su ultimo movimiento
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public int ObtenerPesoProyectado(string arete, int organizacionId)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                resultado = animalBL.ObtenerPesoProyectado(arete, organizacionId);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene si el animal se encuentra en AnimalSalida
        /// </summary>
        /// <param name="animalID"></param>
        /// <returns></returns>
        public int ObtenerExisteSalida(long animalID)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                resultado = animalBL.ObtenerExisteSalida(animalID);
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
            return resultado;
        }



        /// <summary>
        /// Guardar animal salida
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="animalMovimientoInfo"></param>
        public int GuardarCorralAnimalSalida(CorralInfo corralInfo, AnimalMovimientoInfo animalMovimientoInfo)
            {
                 try
                    {
                        Logger.Info();
                        var animalBl = new AnimalBL();
                        var animalInfo = animalBl.GuardarCorralAnimalSalida(corralInfo, animalMovimientoInfo);
                        return animalInfo;
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


        public List<AnimalSalidaInfo> ObtenerAnimalSalidaAnimalID(LoteInfo loteInfo)
        {
            List<AnimalSalidaInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalSalidaAnimalID(loteInfo);
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



        public string ObtenerExisteVentaDetalle(string arete, string areteMetalico)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                string areteVentaDetalle = animalBl.ObtenerExisteVentaDetalle(arete, areteMetalico);
                return areteVentaDetalle;
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

        public string obtenerExisteDeteccion(string arete)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                string areteDeteccion = animalBl.obtenerExisteDeteccion(arete);
                return areteDeteccion;
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
        public string obtenerExisteMuerte(string arete)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                string areteMuerte = animalBl.obtenerExisteMuerte(arete);
                return areteMuerte;
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
        /// Obtener animalaSalidaInfo por animalID
        /// </summary>
        /// <param name="AnimalID"></param>
        /// <returns></returns>
        public AnimalSalidaInfo ObtenerAnimalSalidaAnimalID(long AnimalID)
        {
            AnimalSalidaInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalSalidaAnimalID(AnimalID);
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

        public List<AnimalInfo> ObtenerAnimalesPorLoteID(LoteInfo loteInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesPorLoteID(loteInfo);
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
        /// Existe arete salida
        /// </summary>
        /// <param name="salida"></param>
        /// <param name="arete"></param>
        /// <returns></returns>
        public string ObtenerExisteAreteSalida(int salida, string arete)
        {
            string result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerExisteAreteSalida(salida, arete);
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

        public string obtenerExisteDeteccionTestigo(string areteTestigo)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                string areteDeteccion = animalBl.obtenerExisteDeteccionTestigo(areteTestigo);
                return areteDeteccion;
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

        public string obtenerExisteMuerteTestigo(string areteTestigo)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                string areteMuerte = animalBl.obtenerExisteMuerteTestigo(areteTestigo);
                return areteMuerte;
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

        public AnimalInfo ObtenerAnimalPorAreteTestigo(string areteTestigo, int organizacionID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalPorAreteTestigo(areteTestigo, organizacionID);
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
        /// Guardar animal salida lista
        /// </summary>
        /// <param name="animalSalida"></param>
        /// <returns></returns>
        public int GuardarCorralAnimalSalidaLista(List<AnimalSalidaInfo> animalSalida)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                var animalInfo = animalBl.GuardarCorralAnimalSalidaLista(animalSalida);
                return animalInfo;
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


        public int ObtenerLoteSalidaAnimal(string arete, string areteTestigo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                int lote = animalBl.ObtenerLoteSalidaAnimal(arete, areteTestigo, organizacionID);
                return lote;
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
        /// Obtiene los animales que han tenido
        /// salida por muerte
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        public ResultadoInfo<AnimalInfo> ObtenerAnimalesMuertosPorPagina(PaginacionInfo pagina, AnimalInfo animal)
        {
            ResultadoInfo<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesMuertosPorPagina(pagina, animal);
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
        /// Obtiene el animal que este muerto
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public AnimalInfo ObtenerAnimalesMuertosPorAnimal(AnimalInfo animal)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesMuertosPorAnimal(animal);
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
        /// Obtiene los aretes de un corral de recepcion
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public List<AnimalInfo> ObtenerAnimalesRecepcionPorCodigoCorral(CorralInfo corralInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesRecepcionPorCodigoCorral(corralInfo);
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
        /// Obtiene el animal que este muerto
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="deteccionGrabar"></param>
        /// <returns></returns>
        public void ReemplazarAretes(AnimalInfo animal, DeteccionInfo deteccionGrabar)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                animalBL.ReemplazarAretes(animal, deteccionGrabar);
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
        /// Metodo para actualizar el tipo de ganado de un animal
        /// </summary>
        /// <param name="animal"></param>
        public void ActializaTipoGanado(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                animalBL.ActializaTipoGanado(animal);
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
        /// Metodo para actualizar los aretes en el animal
        /// </summary>
        /// <param name="animal"></param>
        public void ActializaAretesEnAnimal(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                animalBL.ActializaAretesEnAnimal(animal);
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
        /// Obtiene la cantidad de dias despues de la primera entrada a enfermeria de la ultima deteccion para saber cuantos dias tiene en enfermeria
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public int ObtenerDiasUltimaDeteccion(AnimalInfo animal)
        {
            int dias = 0;

            try
            {
                var animalBl = new AnimalBL();
                dias = animalBl.ObtenerDiasUltimaDeteccion(animal);
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

            return dias;
        }
        /// <summary>
        /// Se actualiza clasificacion del animal
        /// </summary>
        /// <param name="animalInfo"></param>
        public void ActualizaClasificacionGanado(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                animalBL.ActualizaClasificacionGanado(animalInfo);
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

        public List<AnimalInfo> ObtenerAnimalesPorLote(int organizacionID, int loteID)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesPorLote(organizacionID, loteID);
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
        /// Metrodo que verifica si existe el nuevo arete, dejar en blanco el arete que no se requiera validar
        /// </summary>
        /// <param name="Arete">Arete capturado</param>
        /// <param name="AreteMetalico">Arete RFID</param>
        /// <returns>Verdadero en caso de que se encuentre el arete registro</returns>
        public Boolean VerificarExistenciaArete(string Arete, string AreteMetalico, int Organizacion)
        {
            Boolean result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.VerificarExistenciaArete(Arete, AreteMetalico, Organizacion);
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

        public void InactivarAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                animalBl.InactivarAnimal(animalInfo);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public void ActualizarArete(long animalId, string arete, int usuario)
        {
            try
            {
                Logger.Info();
                var animalBl = new AnimalBL();
                animalBl.ActualizarArete(animalId, arete, usuario);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Planchar arete a un animal en especifico
        /// </summary>
        /// <param name="Planchado_Arete_Request">Relacion de aretes/AnimalId´s en los cuales se realizara la operacion</param>
        /// <param name="usuarioId">Usaurio que realizo la operacion</param>
        /// <param name="organizacionId">determina bajo que contexto de planta se realizara el planchado</param>
        /// <returns>el numero del animal Id al cual se le realizo el planchado</returns>
        public List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_Arete_Request[] planchadoAretes, int usuarioID, int organizacionId)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                //return animalBL.PlancharAretes(animalesScp, animalID, loteID, usuarioID);
                return animalBL.PlancharAretes(planchadoAretes, usuarioID, organizacionId);
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
        /// Planchar arete a un animal que no esta programado en la orden de sacrificio
        /// </summary>
        /// <param name="Planchado_Arete_Lote_Request">Relacion de aretes/loteId´s en los cuales se realizara la operacion</param>
        /// <param name="usuarioId">Usaurio que realizo la operacion</param>
        /// <param name="organizacionId">determina bajo que contexto de planta se realizara el planchado</param>
        /// <returns>el numero del animal Id al cual se le realizo el planchado</returns>
        public List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_AreteLote_Request planchadoAretes, int usuarioID, int organizacionId)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                return animalBL.PlancharAretes(planchadoAretes, usuarioID, organizacionId);
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
        /// Metodo para obtener la trazabilidad del animal
        /// </summary>
        /// <param name="trazabilidadInfo"></param>
        /// <param name="busquedaDuplicado"></param>
        /// <returns></returns>
        public TrazabilidadAnimalInfo ObtenerTrazabilidadAnimal(TrazabilidadAnimalInfo trazabilidadInfo, bool busquedaDuplicado)
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                return animalBL.ObtenerTrazabilidadAnimal(trazabilidadInfo, busquedaDuplicado);
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
        /// Metrodo Para obtener los aretes del corral a detectar
        /// </summary>
        public List<AnimalInfo> ObtenerAnimalesPorCorralDeteccion(int corralID, bool esPartida)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalesPorCorralDeteccion(corralID, esPartida);
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
        /// Metodo para obtener el Animal mas antiguo del corral
        /// </summary>
        public AnimalInfo ObtenerAnimalAntiguoCorral(int corralID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                result = animalBL.ObtenerAnimalAntiguoCorral(corralID);
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

