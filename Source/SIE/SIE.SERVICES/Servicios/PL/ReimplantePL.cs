using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReimplantePL
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        public AnimalInfo ReasignarAreteMetalico(AnimalInfo animalInfo, int banderaGuardar)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ReasignarAreteMetalico(animalInfo, banderaGuardar);

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
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        public DatosCompra ObtenerDatosCompra(AnimalInfo animalInfo)
        {
            DatosCompra result;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ObtenerDatosCompra(animalInfo);

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
        /// Metrodo Para Verificar existencia de reimplante
        /// </summary>
        public bool ExisteProgramacionReimplate(int organizacionID)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ExisteProgramacionReimplate(organizacionID);
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
        /// Metodo Para Obtener lo el animal de reimplante
        /// </summary>
        public ReimplanteInfo ObtenerAreteIndividual(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ObtenerAreteIndividual(animalInfo, corte);

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

        public ReimplanteInfo ObtenerAreteMetalico(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ObtenerAreteMetalico(animalInfo, corte);

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
        /// Metodo para validar el corral destino
        /// </summary>
        public int ValidarCorralDestinio(string corralOrigen, string corralDestino, int idOrganizacion)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ValidarCorralDestino(corralOrigen, corralDestino, idOrganizacion);

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
        /// Metrodo Para Validar Si el arete ya fue reimplantado
        /// </summary>
        public bool ValidarReimplate(AnimalInfo animal)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ValidarReimplate(animal);
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

        public bool ValidarReimplatePorAreteMetalico(AnimalInfo animal)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ValidarReimplatePorAreteMetalico(animal);
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
        /// Obtiene el numero de cabezas de enfermeria para reimplante
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        public int ObtenerCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int tipoMovimiento)
        {
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                var result = reimplanteBL.ObtenerCabezasEnEnfermeria(ganadoEnfermeria, tipoMovimiento);

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
        /// Obtiene el numero de cabezas reimplantadas
        /// </summary>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        public List<CabezasCortadas> ObtenerCabezasReimplantadas(CabezasCortadas cabezas)
        {
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                var result = reimplanteBL.ObtenerCabezasReimplantadas(cabezas);

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
        /// Obtiene el numero total de cabezas muertas de un lote para reimplante
        /// </summary>
        /// <param name="cabezaMuertas"></param>
        /// <returns></returns>
        public int ObtenerCabezasMuertas(CabezasCortadas cabezaMuertas)
        {
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                var result = reimplanteBL.ObtenerCabezasMuertas(cabezaMuertas);

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

        public ReimplanteInfo ObtenerAreteIndividualReimplantar(LoteInfo lote)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ObtenerAreteIndividualReimplantar(lote);

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
        /// Metodo para generar la proyeccion de reimplante
        /// </summary>
        public void GenerarProyeccionReimplante()
        {
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                reimplanteBL.GenerarProyeccionReimplante();
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
        /// Se valida corral destino si tiene punta chica
        /// </summary>
        /// <param name="corralOrigen"></param>
        /// <param name="corralDestino"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public CorralInfo ValidarCorralDestinoPuntaChica(string corralOrigen, string corralDestino, int organizacionId)
        {
            CorralInfo result = null;
            try
            {
                Logger.Info();
                var reimplanteBL = new ReimplanteBL();
                result = reimplanteBL.ValidarCorralDestinoPuntaChica(corralOrigen, corralDestino, organizacionId);

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
