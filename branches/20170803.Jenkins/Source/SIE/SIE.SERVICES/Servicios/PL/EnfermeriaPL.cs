using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EnfermeriaPL
    {
        /// <summary>
        /// Obtiene los corrales en los cuales se detectaron ganado enfermo
        /// </summary>
        /// <param name="organizacionId">Organizacion de la cual se buscaran los corrales con ganado enfermi</param>
        /// <param name="pagina">Configuracion de paginacion</param>
        /// <returns>Lista con los corrales con ganado enfermo</returns>
        public ResultadoInfo<EnfermeriaInfo> ObtenerCorralesConGanadoDetectadoEnfermo(int organizacionId, PaginacionInfo pagina)
        {
            ResultadoInfo<EnfermeriaInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerCorralesConGanadoDetectadoEnfermo(organizacionId, pagina);

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
        /// Obtener la deteccion de un animal enfermos
        /// </summary>
        /// <param name="animal">Animal a verificar</param>
        /// <returns></returns>
        public AnimalDeteccionInfo ObtenerAnimalDetectadoPorArete(AnimalInfo animal)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerAnimalDetectadoPorArete(animal);

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
        /// Obtiene el ultimo movimiento de enfermeria
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public AnimalMovimientoInfo ObtenerUltimoMovimientoEnfermeria(AnimalInfo animal)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerUltimoMovimientoEnfermeria(animal);
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
        /// obtiene la lista de problemas
        /// </summary>
        /// <returns></returns>
        public List<ProblemaInfo> ObtenerProblemas()
        {
            List<ProblemaInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerProblemas();
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
        /// obtiene la lista de problemas
        /// </summary>
        /// <returns></returns>
        public IList<GradoInfo> ObtenerGrados()
        {
            IList<GradoInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerGrados();
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
        /// Obtiene el ultimo movimiento de recuperacion
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        public AnimalMovimientoInfo ObtenerUltimoMovimientoRecuperacion(AnimalInfo animalInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerUltimoMovimientoRecuperacion(animalInfo);
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
        /// Obtener la deteccion de un animal enfermos
        /// </summary>
        /// <param name="animal">Animal a verificar</param>
        /// <returns></returns>
        public IList<HistorialClinicoInfo> ObtenerHistorialClinico(AnimalInfo animal)
        {
            IList<HistorialClinicoInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerHistorialClinico(animal);
                ObtenerCostos(result, animal.OrganizacionIDEntrada);

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
        /// Obtiene el consto del historial
        /// </summary>
        /// <param name="historial"></param>
        /// <param name="organizacionId"></param>
        private void ObtenerCostos(IList<HistorialClinicoInfo> historial, int organizacionId)
        {

            var enfermeriaDal = new EnfermeriaPL();
            var listaCostos = new List<TratamientoInfo>();

            if (historial != null)
            {

                foreach (var historia in historial)
                {
                    historia.Costo = 0;
                    var animalMovimiento = new AnimalMovimientoInfo
                    {
                        OrganizacionID = organizacionId,
                        AnimalMovimientoID = historia.AnimalMovimientoId,
                        
                    };
                    if (historia.ListaProblemas != null)
                    {
                        foreach (var problema in historia.ListaProblemas)
                        {
                            if (problema.isCheked)
                            {
                                if (problema.Tratamientos != null)
                                {
                                    if (!String.IsNullOrEmpty(historia.Tratamiento.Trim()))
                                    {
                                        //Se separan los tratamientos
                                        string[] listaTratamiento = historia.Tratamiento.Trim().Split(new Char[] { ',' });
                                        TratamientoInfo tratamientoInfo = null;
                                        foreach (string tratamiento in listaTratamiento)
                                        {
                                            if (tratamiento.Trim() != "")
                                            {
                                                tratamientoInfo = new TratamientoInfo { TratamientoID = int.Parse(tratamiento.Trim()) };
                                                historia.Costo += enfermeriaDal.ObtenerCosto(animalMovimiento,
                                                                tratamientoInfo.TratamientoID);
                                                listaCostos.Add(tratamientoInfo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if(!String.IsNullOrEmpty(historia.Tratamiento.Trim()))
                    {
                        //Se separan los tratamientos
                        string[] listaTratamiento = historia.Tratamiento.Trim().Split(new Char[] { ',' });
                        TratamientoInfo tratamientoInfo = null;
                        foreach (string tratamiento in listaTratamiento)
                        {
                            if (tratamiento.Trim() != "")
                            {
                                tratamientoInfo = new TratamientoInfo {TratamientoID = int.Parse(tratamiento.Trim())};
                                historia.Costo += enfermeriaDal.ObtenerCosto(animalMovimiento,
                                                tratamientoInfo.TratamientoID);
                                listaCostos.Add(tratamientoInfo);
                            }
                        }
                        //Se elimina el ultimo caracter si este es una coma(',')
                        if (historia.Tratamiento.Trim().Substring(historia.Tratamiento.Length-2) == ",")
                        {
                            historia.Tratamiento = historia.Tratamiento.Trim()
                                .Substring(0,historia.Tratamiento.Length - 2);
                        }
                        //Se elimina el ultimo caracter si este es una coma(',')
                        if (historia.CodigoTratamiento.Trim().Substring(historia.CodigoTratamiento.Length - 2) == ",")
                        {
                            historia.CodigoTratamiento = historia.CodigoTratamiento.Trim()
                                .Substring(0, historia.CodigoTratamiento.Length - 2);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Obtiene el costo total
        /// </summary>
        /// <param name="historial"></param>
        /// <returns></returns>
        public decimal ObtenerCostoTotal(IList<HistorialClinicoInfo> historial)
        {
            decimal total = 0;
            if (historial != null)
            {
                total = historial.Sum(historia => historia.Costo);
            }
            return total;
        }

        /// <summary>
        /// Obtiene el costo de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamientoId">Identificador del producto</param>
        /// <returns></returns>
        public decimal ObtenerCosto(AnimalMovimientoInfo movimiento, int tratamientoId)
        {
            decimal result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerCosto(movimiento, tratamientoId);

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
        /// Obtiene el costo de un producto de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamiento">Identificador del producto</param>
        /// <returns></returns>
        public decimal ObtenerCostoProducto(AnimalMovimientoInfo movimiento, TratamientoProductoInfo tratamiento)
        {
            decimal result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerCostoProducto(movimiento, tratamiento);

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
        /// Elimina la deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <returns></returns>
        public int EliminarDeteccion(DeteccionInfo deteccion)
        {
            try
            {
                Logger.Info();
                var programacionCorteBl = new EnfermeriaBL();
                return programacionCorteBl.EliminarDeteccion(deteccion);
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
        /// Elimina la deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <param name="problemasDetectados"></param>
        /// <returns></returns>
        public int GurdarDeteccion(AnimalDeteccionInfo deteccion, IList<ProblemaInfo> problemasDetectados)
        {
            try
            {
                Logger.Info();
                var programacionCorteBl = new EnfermeriaBL();
                return programacionCorteBl.GurdarDeteccion(deteccion, problemasDetectados);
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
        /// Guarda la entrada a enfermeria
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        public EntradaGanadoEnfermeriaInfo GurdarEntradaEnfermeria(EntradaGanadoEnfermeriaInfo entradaGanadoInfo)
        {
            try
            {
                Logger.Info();
                var programacionCorteBl = new EnfermeriaBL();
                return programacionCorteBl.GuardarEntradaEnfermeria(entradaGanadoInfo);
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
        /// Consulta la salida de un animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        public AnimalSalidaInfo AnimalSalidaEnfermeria(AnimalInfo animalInfo)
        {
            AnimalSalidaInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.AnimalSalidaEnfermeria(animalInfo);
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
        /// Método para consultar las enfermerias que le pertenecen al operador
        /// </summary>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        public List<EnfermeriaInfo> ObtenerEnfermeriasPorOperadorId(int operadorId)
        {
            List<EnfermeriaInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerEnfermeriasPorOperadorID(operadorId);

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
        /// Obtiene los datos de la compra
        /// </summary>
        /// <param name="folioEntrada">Folio entrada</param>
        /// <param name="organicacionId">Organicacion</param>
        /// <returns></returns>
        public DatosCompra ObtenerDatosCompra(int folioEntrada, int organicacionId)
        {
            DatosCompra result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerDatosCompra(folioEntrada, organicacionId);

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
            return result;
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EnfermeriaInfo> ObtenerPorPagina(PaginacionInfo pagina, EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new EnfermeriaBL();
                ResultadoInfo<EnfermeriaInfo> result = cuentaBL.ObtenerPorPagina(pagina, filtro);

                return result;
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
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="enfermeriaInfo"></param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorID(EnfermeriaInfo enfermeriaInfo)
        {
            EnfermeriaInfo info;
            try
            {
                Logger.Info();
                var enfermeriaBL = new EnfermeriaBL();
                info = enfermeriaBL.ObtenerPorID(enfermeriaInfo);
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
            return info;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Enfermeria
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(EnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                var enfermeriaBL = new EnfermeriaBL();
                int result = enfermeriaBL.Guardar(info);
                return result;
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorDescripcion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var enfermeriaBL = new EnfermeriaBL();
                EnfermeriaInfo result = enfermeriaBL.ObtenerPorDescripcion(descripcion, organizacionID);
                return result;
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

        public AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteSinActivo(AnimalInfo animalEnfermo)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerAnimalDetectadoPorAreteSinActivo(animalEnfermo);

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
        /// Metodo para obtener la ultima deteccion que sele realizo al animal
        /// </summary>
        /// <param name="animalEnfermo"></param>
        /// <returns></returns>
        public AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteUltimaDeteccion(AnimalInfo animalEnfermo)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaBl = new EnfermeriaBL();
                result = enfermeriaBl.ObtenerAnimalDetectadoPorAreteUltimaDeteccion(animalEnfermo);

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
