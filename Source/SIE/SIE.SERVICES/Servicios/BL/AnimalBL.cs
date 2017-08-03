using System;
using System.Transactions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class AnimalBL
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal AnimalInfo GuardarAnimal(AnimalInfo animalInfo)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.GuardarAnimal(animalInfo);

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
        /// Metrodo que verifica si existe el arete, dejar en blanco el arete que no se requiera validar
        /// </summary>
        /// <param name="Arete">Arete capturado</param>
        /// <param name="AreteMetalico">Arete RFID</param>
        /// <returns>Verdadero en caso de que se encuentre el arete registro</returns>
        internal Boolean VerificarExistenciaArete(string Arete, string AreteMetalico, int Organizacion)
        {
            Boolean result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.VerificarExistenciaArete(Arete, AreteMetalico, Organizacion);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal TrampaInfo ObtenerPeso(int organizacionID, int folioEntrada)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerPeso(organizacionID, folioEntrada);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal List<AnimalMovimientoInfo> ObtenerFechasUltimoMovimiento(int organizacionID, List<int> listaLotes)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerFechasUltimoMovimiento(organizacionID, listaLotes);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal List<AnimalInfo> ObtenerAnimalesPorLote(int organizacionID, int loteID)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalesPorLote(organizacionID, loteID);
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
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteDisponibilidad(List<DisponibilidadLoteInfo> lotesDisponibles , int organizacionId)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalesPorLoteDisponibilidad(lotesDisponibles, organizacionId);
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
        /// Obtiene los Animales por Codigo de Corral
        /// </summary>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(CorralInfo corralInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesPorCodigoCorral(corralInfo);
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
        /// Se crea el registro en Animal Costo
        /// </summary>
        /// <param name="animalCosto"></param>
        internal int GuardarAnimalCosto(AnimalCostoInfo animalCosto)
        {
            int result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.GuardarAnimalCosto(animalCosto);
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
        /// Se crea el registro en Animal Costo
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        internal int GuardarAnimalCostoXML(List<AnimalCostoInfo> listaAnimalCosto)
        {
            int result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.GuardarAnimalCostoXML(listaAnimalCosto);
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

        internal List<AnimalInfo> ObtenerAnimalesPorCorral(CorralInfo corralInfo, int organizacionId)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesPorCorral(corralInfo, organizacionId);
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

        internal AnimalMovimientoInfo ObtenerUltimoMovimientoAnimal(AnimalInfo animalInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerUltimoMovimientoAnimal(animalInfo);
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

        internal AnimalInfo ObtenerAnimalPorArete(string arete, int organizacionId)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                var animalMovimientoBL = new AnimalMovimientoBL();
                result = animalDAL.ObtenerAnimalPorArete(arete,organizacionId);
                if (result != null)
                {
                    result.CargaInicial = animalMovimientoBL.ObtenerEsCargaInicialAnimal(result.AnimalID);
                }
                

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

        internal List<AnimalInfo> ObtenerAnimalPorAretes(AnimalInfo arete, int organizacionId)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalPorAretes(arete, organizacionId);

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

        internal int ObtenerPesoProyectado(string arete, int organizacionId)
        {
            int resultado;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                resultado = animalDAL.ObtenerPesoProyectado(arete, organizacionId);
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

        internal int ObtenerExisteSalida(long animalID)
        {
            int resultado;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                resultado = animalDAL.ObtenerExisteSalida(animalID);
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
        internal int GuardarCorralAnimalSalida(CorralInfo corralInfo, AnimalMovimientoInfo animalMovimientoInfo)
        {
            var animalDal = new AnimalDAL();
            try
            {
                var animalInfo = animalDal.GuardarCorralAnimalSalida(corralInfo, animalMovimientoInfo);
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


        internal List<AnimalSalidaInfo> ObtenerAnimalSalidaAnimalID(LoteInfo loteInfo)
        {
            List<AnimalSalidaInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalSalidaAnimalID(loteInfo);
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




        internal string ObtenerExisteVentaDetalle(string arete, string areteMetalico)
        {
            var animalDal = new AnimalDAL();
            try
            {
                string areteVentaDetalle = animalDal.ObtenerExisteVentaDetalle(arete, areteMetalico);
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

        internal string obtenerExisteDeteccion(string arete)
        {
            var animalDal = new AnimalDAL();
            try
            {
                string areteDeteccion = animalDal.obtenerExisteDeteccion(arete);
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

        internal string obtenerExisteMuerte(string arete)
        {
            var animalDal = new AnimalDAL();
            try
            {
                string areteMuerte = animalDal.obtenerExisteMuerte(arete);
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

        internal AnimalSalidaInfo ObtenerAnimalSalidaAnimalID(long animalID)
        {
            AnimalSalidaInfo result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalSalidaAnimalID(animalID);
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
        /// Obtiene los Animales por LoteID
        /// </summary>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteID(LoteInfo loteInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesPorLoteID(loteInfo);
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

        internal string ObtenerExisteAreteSalida(int salida, string arete)
        {
            string result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerExisteAreteSalida(salida, arete);
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

        internal string obtenerExisteDeteccionTestigo(string areteTestigo)
        {
            var animalDal = new AnimalDAL();
            try
            {
                string areteDeteccion = animalDal.obtenerExisteDeteccionTestigo(areteTestigo);
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

        internal string obtenerExisteMuerteTestigo(string areteTestigo)
        {
            var animalDal = new AnimalDAL();
            try
            {
                string areteMuerte = animalDal.obtenerExisteMuerteTestigo(areteTestigo);
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

        internal AnimalInfo ObtenerAnimalPorAreteTestigo(string areteTestigo, int organizacionID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalPorAreteTestigo(areteTestigo, organizacionID);
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
        /// Se guarda lista de salida de recuperacion por corral
        /// </summary>
        /// <param name="animalSalida"></param>
        /// <returns></returns>
        internal int GuardarCorralAnimalSalidaLista(List<AnimalSalidaInfo> animalSalida)
        {
            var animalDal = new AnimalDAL();
            try
            {
                var animalInfo = animalDal.GuardarCorralAnimalSalidaLista(animalSalida);
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

        internal int ObtenerLoteSalidaAnimal(string arete, string areteTestigo, int organizacionID)
        {
            var animalDal = new AnimalDAL();
            try
            {
                int lote = animalDal.ObtenerLoteSalidaAnimal(arete, areteTestigo, organizacionID);
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
        /// Obtener los animales del inventario para cada folioEntrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<AnimalInfo> ObtenerInventarioAnimalesPorFolioEntrada(string folioEntrada, int organizacionID)
        {
            IList<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerInventarioAnimalesPorFolioEntrada(folioEntrada, organizacionID);
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
        /// Metodo para enviar animales A las tablas historicas
        /// </summary>
        /// <param name="listaAnimalesInactivos"></param>
        internal void EnviarAHistorico(List<AnimalInfo> listaAnimalesInactivos)
        {
            try
            {
                var animalCostoBL = new AnimalCostoBL();
                var salidaGanadoBL = new SalidaGanadoBL();
                var animalMovimientoBL = new AnimalMovimientoBL();
                var animalConsumoBL = new AnimalConsumoBL();

                List<AnimalInfo> animalesAgrupados =
                    listaAnimalesInactivos.GroupBy(id => id.AnimalID)
                        .Select(ani => new AnimalInfo
                                           {
                                               AnimalID = ani.Key,
                                               OrganizacionIDEntrada =
                                                   ani.Select(org => org.OrganizacionIDEntrada).FirstOrDefault(),
                                               UsuarioCreacionID =
                                                   ani.Select(usu => usu.UsuarioCreacionID).FirstOrDefault()
                                           }).ToList();
                foreach (var animalInactivo in animalesAgrupados)
                {
                    //Validar Si tiene Costo el animal
                    var tieneCosto =
                        animalCostoBL.ValdiarTieneCostoGanadoAnimal(animalInactivo, (int)Costo.CostoGanado);
                    if (tieneCosto)
                    {
                        //Guardar Salida Ganado
                        salidaGanadoBL.GuardarSalidaGanado(animalInactivo);
                        //Enviar Costo
                        animalCostoBL.EnviarAnimalCostoAHistorico(animalInactivo);
                        //Enviar Movimiento
                        animalMovimientoBL.EnviarAnimalMovimientoAHistorico(animalInactivo);
                        //Enviar consumo
                        animalConsumoBL.EnviarAnimalConsumoAHistorico(animalInactivo);
                        animalConsumoBL.EliminarAnimalConsumo(animalInactivo);
                        //Enviar Animal
                        EnviarAnimalAHistorico(animalInactivo);
                    }
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
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
        /// Enviar animal a Animal Historico
        /// </summary>
        /// <param name="animalInactivo"></param>
        private bool EnviarAnimalAHistorico(AnimalInfo animalInactivo)
        {
            var envioAnimal = false;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();

                //Se envia el animal de Animal a AnimalHistorico
                animalDAL.EnviarAnimalAHistorico(animalInactivo);
                //Se elimina el animal de Animal
                animalDAL.EliminarAnimal(animalInactivo);

                envioAnimal = true;
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
            return envioAnimal;
        }

        /// <summary>
        /// Consulta un animal por AnimalID
        /// </summary>
        /// <param name="animalID"></param>
        /// <returns>AnimalInfo</returns>
        internal AnimalInfo ObtenerAnimalAnimalID(long animalID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalAnimalID(animalID);

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
        /// Metodo para inactivar animal del inventario
        /// </summary>
        /// <param name="animalInfo"></param>
        internal void InactivarAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.InactivarAnimal(animalInfo);

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

         internal void ActualizarArete(long animalId, string arete, int usuario)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.ActualizarArete(animalId, arete, usuario);

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
        /// Eliminar el animal de animal Salida
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="loteId"></param>
        internal void EliminarAnimalSalida(long animalId, int loteId)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.EliminarAnimalSalida(animalId, loteId);
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
        internal ResultadoInfo<AnimalInfo> ObtenerAnimalesMuertosPorPagina(PaginacionInfo pagina, AnimalInfo animal)
        {
            ResultadoInfo<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesMuertosPorPagina(pagina, animal);
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
        internal AnimalInfo ObtenerAnimalesMuertosPorAnimal(AnimalInfo animal)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesMuertosPorAnimal(animal);
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
        /// Metodo para reemplazar aretes cuando encuentras un animal en diferente corral
        /// y es de carga inicial
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="deteccionGrabar"></param>
        internal void ReemplazarAretes(AnimalInfo animal, DeteccionInfo deteccionGrabar)
        {
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                animalDAL.ReemplazarAretes(animal, deteccionGrabar);
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
        /// OBtiene los animales de un corral de recepcion
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesRecepcionPorCodigoCorral(CorralInfo corralInfo)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerAnimalesRecepcionPorCodigoCorral(corralInfo);
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
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        internal void GuardarAnimalConsumoXml(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.GuardarAnimalConsumoXml(listaAnimalConsumo);
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
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerAnimalesPorLoteReimplante(List<DisponibilidadLoteInfo> lotesDisponibles, int organizacionId)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalesPorLoteReimplante(lotesDisponibles, organizacionId);
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
        /// Metodo para actualizar el tipo de ganado de un animal
        /// </summary>
        /// <param name="animal"></param>
        public void ActializaTipoGanado(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.ActializaTipoGanado(animal);
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
                var animaloDAL = new AnimalDAL();
                animaloDAL.ActializaAretesEnAnimal(animal);
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
        /// Obtiene la cantidad de dias que tiene en enfermeria el animal despues de la ultima deteccion
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal int ObtenerDiasUltimaDeteccion(AnimalInfo animal)
        {
            int dias = 0;

            try
            {
                var animalDal = new AnimalDAL();
                dias = animalDal.ObtenerDiasUltimaDeteccion(animal);
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
        /// Se remplaza el arete por un arete del mismo corral
        /// </summary>
        /// <param name="animal"></param>
        internal void ReemplazarAreteMismoCorral(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                animalDAL.ReemplazarAreteMismoCorral(animal);
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
        /// Se actualiza clasificacion del ganado
        /// </summary>
        /// <param name="animalInfo"></param>
        internal void ActualizaClasificacionGanado(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.ActualizaClasificacionGanado(animalInfo);
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
        /// Metodo para actualizar el peso compra de un animal
        /// </summary>
        /// <param name="animal"></param>
        internal void ActualizaPesoCompra(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.ActualizaPesoCompra(animal);
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

        internal List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimalXML(List<AnimalInfo> animales, int organizacionID)
        {
            List<AnimalMovimientoInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerUltimoMovimientoAnimalXML(animales, organizacionID);
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
        /// Se crea el registro en Animal Costo
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        internal int GuardarAnimalCostoXMLManual(List<AnimalCostoInfo> listaAnimalCosto)
        {
            int result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.GuardarAnimalCostoXMLManual(listaAnimalCosto);
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
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        /// <returns></returns>
        internal void GuardarAnimalConsumoXmlManual(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.GuardarAnimalConsumoXmlManual(listaAnimalConsumo);
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
        /// Obtener Animales por Lote
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorLoteXML(List<LoteInfo> lotes)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerMovimientosPorLoteXML(lotes);
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

        internal List<AnimalInfo> ObtenerMovimientosSacrificioPorLoteXML(List<LoteInfo> lotes)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerMovimientosSacrificioPorLoteXML(lotes);
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
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteXML(List<LoteInfo> lotes, int organizacionId)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalesPorLoteXML(lotes, organizacionId);
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
        /// Obtiene una lista de animales que han
        /// sido sacrificados para los lotes
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorLoteSacrificadosXML(List<LoteInfo> lotes)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerMovimientosPorLoteSacrificadosXML(lotes);
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
        /// Genera animales a partir de un XML
        /// </summary>
        /// <param name="animales"></param>
        internal void GuardarAnimal(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.GuardarAnimal(animales);
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
        /// Obtiene animales por arete y organizacion
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerPorArete(List<AnimalInfo> animales)
        {
            List<AnimalInfo> animalesPorArete;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animalesPorArete = animaloDAL.ObtenerPorArete(animales);
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
            return animalesPorArete;
        }

        /// <summary>
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        internal void GuardarAnimalConsumoBulkCopy(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.GuardarAnimalConsumoBulkCopy(listaAnimalConsumo);
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
        /// Se crea el registro en Animal Costo
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        internal int GuardarAnimalCostoBulkCopy(List<AnimalCostoInfo> listaAnimalCosto)
        {
            int result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.GuardarAnimalCostoBulkCopy(listaAnimalCosto);
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
        /// Mueve de la Tabla temporal AnimalConsumoTemporal a la tabla AnimalConsumo
        /// </summary>
        internal void EnviarAnimalConsumoDeTemporal()
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.EnviarAnimalConsumoDeTemporal();
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
        /// Mueve de la tabla AnimalCostoTemporal a la tabla AnimalCosto
        /// </summary>
        internal void EnviarAnimalCostoDeTemporal()
        {
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                animaloDAL.EnviarAnimalCostoDeTemporal();
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
        /// Obtener Animales por XML
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorXML(List<AnimalInfo> animales)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                result = animalDAL.ObtenerMovimientosPorXML(animales);
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

        //public long PlancharAretes(List<AnimalInfo> animalesScp, long animalID, int loteID, int usuarioID)
        public List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_Arete_Request[] plancharArete, int usuarioID, int organizacionId)
        {
            List<ControlSacrificioInfo.SincronizacionSIAP> relacion = new List<ControlSacrificioInfo.SincronizacionSIAP>();
            
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();
                using (var transaction = new TransactionScope())
                {
                    relacion = animalDAL.PlancharAretes(plancharArete, usuarioID);
                    //transaction.Complete();
                }

                //var OrdenSacrificio = new ControlSacrificioDAL();
                //var informacionSIAP = new List<ControlSacrificioInfo.SincronizacionSIAP>();
                //plancharArete.Select(pa => new { pa.AnimalId, pa.Arete }).ToList().ForEach(pa => {
                //    informacionSIAP.Add(new ControlSacrificioInfo.SincronizacionSIAP 
                //                        { 
                //                            AnimalID = pa.AnimalId, 
                //                            Arete = pa.Arete
                //                        }
                //        );
                //});

                //OrdenSacrificio.SyncResumenSacrificio_planchado(informacionSIAP, usuarioID, organizacionId);

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
            return relacion;
        }

        public List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_AreteLote_Request plancharArete, int usuarioID, int organizacionId)
        {
            List<ControlSacrificioInfo.SincronizacionSIAP> relacion = new List<ControlSacrificioInfo.SincronizacionSIAP>();
            try
            {
                Logger.Info();
                var animalDAL = new AnimalDAL();

                relacion = animalDAL.PlancharAretes(plancharArete, usuarioID);

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
            return relacion;
        }

        /// <summary>
        /// Funcion que procesa los costos de los animales en la lista
        /// Obtiene los costos de la entrada de Interfaces y EntradaGanado
        /// Prorratea y asigna por animal
        /// Genera los registros en las tablas de control para el costeo
        /// Genera los costos en AnimalCosto
        /// </summary>
        /// <param name="listaAnimales"></param>
        /// <returns>Lista de Animales con sus costos</returns>
        internal List<AnimalInfo> ProcesoGenerarCostos(List<AnimalInfo> listaAnimales)
        {

            try
            {
                EntradaGanadoBL entradaGanadoBl = new EntradaGanadoBL();
                EntradaGanadoInfo entradaGanado = new EntradaGanadoInfo();

                InterfaceSalidaAnimalBL interfaceSalidaAnimalBl = new InterfaceSalidaAnimalBL();
                List<InterfaceSalidaAnimalInfo> interfaceSalidaAnimal = null;
                InterfaceSalidaCostoBL interfaceSalidaCostoBl = new InterfaceSalidaCostoBL();
                IList<InterfaceSalidaCostoInfo> listaInterfaceSalidaCosto = new List<InterfaceSalidaCostoInfo>();
                List<ControlEntradaGanadoInfo> listaControlEntrada = new List<ControlEntradaGanadoInfo>();

                EntradaGanadoCosteoBL entradaGanadoCosteoBl = new EntradaGanadoCosteoBL();
                EntradaGanadoCosteoInfo entradaGanadoCosteo = new EntradaGanadoCosteoInfo();

                ControlEntradaGanadoBL controlEntradaGanadoBl = new ControlEntradaGanadoBL();

                decimal importeCosto = 0;
                foreach(AnimalInfo animal in listaAnimales.OrderBy(registro => registro.FolioEntrada))
                {
                    if (entradaGanado.FolioEntrada != animal.FolioEntrada)
                    {
                        entradaGanado = entradaGanadoBl.ObtenerPorFolioEntradaOrganizacion(animal.FolioEntrada, animal.OrganizacionIDEntrada);
                        listaInterfaceSalidaCosto = null;
                        entradaGanadoCosteo = null;
                        interfaceSalidaAnimal = null;
                        listaControlEntrada = null;
                    }

                    if (entradaGanado != null && entradaGanado.FolioEntrada > 0)
                    {
                        //Obtiene los costos ya prorrateados de las tablas de control
                        if (listaControlEntrada == null)
                        {
                            listaControlEntrada = controlEntradaGanadoBl.ObtenerControlEntradaGanadoPorID(0, entradaGanado.EntradaGanadoID);
                        }

                        if (entradaGanado.FolioOrigen > 0)
                        {
                            if (interfaceSalidaAnimal == null)
                            {
                                interfaceSalidaAnimal =
                                    interfaceSalidaAnimalBl.ObtenerInterfazSalidaAnimal(entradaGanado.FolioOrigen,
                                                                                  entradaGanado.OrganizacionOrigenID);
                            }


                            //Obtiene los costos de la interfaz para el animal
                            if (listaInterfaceSalidaCosto == null)
                            {
                                listaInterfaceSalidaCosto = interfaceSalidaCostoBl.ObtenerCostoAnimales(entradaGanado.FolioOrigen, entradaGanado.OrganizacionOrigenID);
                            }
                            animal.ListaCostosAnimal = new List<AnimalCostoInfo>();

                            if (listaInterfaceSalidaCosto != null)
                            {
                                foreach (InterfaceSalidaCostoInfo interfaceSalidaCosto in listaInterfaceSalidaCosto.Where(registro => registro.Arete == animal.Arete))
                                {
                                    animal.ListaCostosAnimal.Add(new AnimalCostoInfo()
                                                                            {
                                                                                AnimalID = animal.AnimalID,
                                                                                CostoID = interfaceSalidaCosto.Costo.CostoID,
                                                                                Importe = interfaceSalidaCosto.Importe,
                                                                                FechaCosto = interfaceSalidaCosto.FechaCompra,
                                                                                TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                                                                                UsuarioCreacionID = animal.UsuarioCreacionID
                                                                            });
                                }
                            }
                            

                            //Obtiene los costos de la entrada para prorratear
                            if (entradaGanadoCosteo == null)
                            {
                                entradaGanadoCosteo = entradaGanadoCosteoBl.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID);
                            }

                            if (entradaGanadoCosteo != null)
                            {
                                var listaCostos = new List<InterfaceSalidaCostoInfo>();
                                if (listaInterfaceSalidaCosto != null)
                                if (listaInterfaceSalidaCosto.Any(registro => registro.Arete == animal.Arete))
                                {
                                    listaCostos =
                                        listaInterfaceSalidaCosto.Where(registro => registro.Arete == animal.Arete).ToList();
                                }

                                foreach (EntradaGanadoCostoInfo entradaGanadoCosto in entradaGanadoCosteo.ListaCostoEntrada)
                                {
                                    if (listaCostos.Count > 0)
                                    if (listaCostos.Any(registro => registro.Costo.CostoID == entradaGanadoCosto.Costo.CostoID))
                                    {
                                        continue;
                                    }
                                    
                                    importeCosto = (entradaGanadoCosto.Importe) / entradaGanado.CabezasRecibidas;
                                    
                                    animal.ListaCostosAnimal.Add(new AnimalCostoInfo()
                                    {
                                        AnimalID = animal.AnimalID,
                                        CostoID = entradaGanadoCosto.Costo.CostoID,
                                        Importe = importeCosto,
                                        FechaCosto = entradaGanadoCosto.Costo.FechaCosto,
                                        TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                                        UsuarioCreacionID = animal.UsuarioCreacionID
                                    });
                                }
                            }
                        }
                        else
                        {
                            //Obtiene los costos de la entrada para prorratear
                            if (entradaGanadoCosteo == null)
                            {
                                entradaGanadoCosteo = entradaGanadoCosteoBl.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID);
                            }

                            animal.ListaCostosAnimal = new List<AnimalCostoInfo>();

                            foreach (EntradaGanadoCostoInfo entradaGanadoCosto in entradaGanadoCosteo.ListaCostoEntrada)
                            {
                                importeCosto = (entradaGanadoCosto.Importe) / entradaGanado.CabezasRecibidas;

                                animal.ListaCostosAnimal.Add(new AnimalCostoInfo()
                                {
                                    AnimalID = animal.AnimalID,
                                    CostoID = entradaGanadoCosto.Costo.CostoID,
                                    Importe = importeCosto,
                                    FechaCosto = entradaGanadoCosto.Costo.FechaCosto,
                                    TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                                    UsuarioCreacionID = animal.UsuarioCreacionID
                                });
                            }
                        }

                        //Guarda la lista de costos
                        GuardarAnimalCostoXMLManual(animal.ListaCostosAnimal);

                        //Funcion de guardar en las tablas de control
                        ControlEntradaGanadoInfo controlEntradaGanadoInfo = new ControlEntradaGanadoInfo();
                        controlEntradaGanadoInfo.Animal = animal;
                        controlEntradaGanadoInfo.EntradaGanado = entradaGanado;
                        controlEntradaGanadoInfo.Activo = EstatusEnum.Activo;
                        controlEntradaGanadoInfo.UsuarioCreacionID = animal.UsuarioCreacionID;
                        controlEntradaGanadoInfo.ListaControlEntradaGanadoDetalle = new List<ControlEntradaGanadoDetalleInfo>();

                        foreach (AnimalCostoInfo animalCosto in animal.ListaCostosAnimal)
                        {
                            ControlEntradaGanadoDetalleInfo controlEntradaGanadoDetalle = new ControlEntradaGanadoDetalleInfo();

                            controlEntradaGanadoDetalle.Costo = new CostoInfo() { CostoID = animalCosto.CostoID };
                            controlEntradaGanadoDetalle.Importe = animalCosto.Importe;
                            controlEntradaGanadoDetalle.Activo = EstatusEnum.Activo;
                            controlEntradaGanadoDetalle.UsuarioCreacionID = animal.UsuarioCreacionID;

                            controlEntradaGanadoInfo.ListaControlEntradaGanadoDetalle.Add(controlEntradaGanadoDetalle);
                        }

                        controlEntradaGanadoBl.GuardarControlEntradaGanado(controlEntradaGanadoInfo);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida("No se encontro el folio");
                    }
                }
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
            return listaAnimales;
        }

        /// <summary>
        /// Metodo para obtener la trazabilidad del animal
        /// </summary>
        /// <param name="trazabilidadAnimalInfo"></param>
        /// <param name="busquedaDuplicado"></param>
        /// <returns></returns>
        public TrazabilidadAnimalInfo ObtenerTrazabilidadAnimal(TrazabilidadAnimalInfo trazabilidadAnimalInfo, bool busquedaDuplicado)
        {
            try
            {
                Logger.Info();
                {
                    var animalMovimientoBl = new AnimalMovimientoBL();
                    if (busquedaDuplicado)
                    {
                        //Se busca por arete inventario normal
                        trazabilidadAnimalInfo.AnimalesDuplicados = ObtenerAnimalPorAretes(
                            trazabilidadAnimalInfo.Animal,
                            trazabilidadAnimalInfo.Organizacion.OrganizacionID);

                        if (trazabilidadAnimalInfo.AnimalesDuplicados != null)
                        {
                            foreach (AnimalInfo animal in trazabilidadAnimalInfo.AnimalesDuplicados)
                            {
                                if (string.IsNullOrEmpty(animal.Origen))
                                {
                                    EntradaGanadoBL entradaBl = new EntradaGanadoBL();
                                    EntradaGanadoInfo entrada = entradaBl.ObtenerPorFolioEntradaOrganizacion(animal.FolioEntrada,
                                trazabilidadAnimalInfo.Organizacion.OrganizacionID);
                                    if (entrada.EntradaGanadoID != 0)
                                    {
                                        animal.Origen = entrada.OrganizacionOrigen;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        trazabilidadAnimalInfo.AnimalesDuplicados = new List<AnimalInfo>
                        {
                            trazabilidadAnimalInfo.Animal
                        };
                    }

                    if (trazabilidadAnimalInfo.AnimalesDuplicados != null &&
                        trazabilidadAnimalInfo.AnimalesDuplicados.Any())
                    {
                        switch (trazabilidadAnimalInfo.AnimalesDuplicados.Count())
                        {
                            case 1: // Encontro un Arete
                                trazabilidadAnimalInfo.Animal = trazabilidadAnimalInfo.AnimalesDuplicados.FirstOrDefault();
                                trazabilidadAnimalInfo.Animal =
                                    animalMovimientoBl.ObtenerTrazabilidadAnimalMovimiento(trazabilidadAnimalInfo.Animal);
                                break;
                            default : // Se encontro arete duplicado
                                trazabilidadAnimalInfo.ExisteDuplicados = true;
                                return trazabilidadAnimalInfo;
                        }
                    }
                }
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
            return trazabilidadAnimalInfo;
        }

        /// <summary>
        /// Metrodo Para obtener los aretes del corral a detectar
        /// </summary>
        internal List<AnimalInfo> ObtenerAnimalesPorCorralDeteccion(int corralID, bool esPartida)
        {
            List<AnimalInfo> result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalesPorCorralDeteccion(corralID, esPartida);
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
        internal AnimalInfo ObtenerAnimalAntiguoCorral(int corralID)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var animaloDAL = new AnimalDAL();
                result = animaloDAL.ObtenerAnimalAntiguoCorral(corralID);
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

