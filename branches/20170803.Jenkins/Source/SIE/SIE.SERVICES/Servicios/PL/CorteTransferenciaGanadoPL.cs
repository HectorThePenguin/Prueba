using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
namespace SIE.Services.Servicios.PL
{
    public class CorteTransferenciaGanadoPL
    {
        /// <summary>
        /// Obtiene permiso trampa
        /// </summary>
        /// <param name="trampaInfo"></param>
        /// <returns></returns>
        public TrampaInfo ObtenerPermisoTrampa(TrampaInfo trampaInfo)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var permisoBL = new CorteTransferenciaGanadoBL();
                result = permisoBL.ObtenerPermisoTrampa(trampaInfo);

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
        /// Obtener entrada de ganado
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradaGanado(AnimalInfo animalInfo)
        {
            EntradaGanadoInfo result;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new CorteTransferenciaGanadoBL();
                result = entradaGanadoBL.ObtenerEntradaGanado(animalInfo);
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
        /// Metodo para obetener los totales de corte por transferencia
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public CorteTransferenciaTotalCabezasInfo ObtenerTotales(AnimalMovimientoInfo animalInfo)
        {
            CorteTransferenciaTotalCabezasInfo result;
            try
            {
                Logger.Info();
                var objCorteTransferencia = new CorteTransferenciaGanadoBL();
                result = objCorteTransferencia.ObtenerTotales(animalInfo);
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
        /// Metodo para obetener los totales de corte por transferencia
        /// </summary>
        /// <param name="tipoCorralInfo"></param>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorralesTipo(TipoCorralInfo tipoCorralInfo, int organizationID)
        {
            CorralInfo result;
            try
            {
                Logger.Info();
                var corteTransferenciaGanado = new CorteTransferenciaGanadoBL();
                result = corteTransferenciaGanado.ObtenerCorralesTipo(tipoCorralInfo, organizationID);
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
        /// Metodo para obtener tratamientos aplicados por animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        public List<TratamientoInfo> ObtenerTratamientosAplicados(AnimalInfo animalInfo, int tipoMovimiento)
        {
            List<TratamientoInfo> result;
            try
            {
                Logger.Info();
                var corteTransferenciaGanado = new CorteTransferenciaGanadoBL();
                result = corteTransferenciaGanado.ObtenerTratamientosAplicados(animalInfo, tipoMovimiento);
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

        public CorteTransferenciaGanadoGuardarInfo GuardarCorteTransferencia(CorteTransferenciaGanadoGuardarInfo guardarAnimalCorte, IList<TratamientoInfo> listaTratamientos)
        {
            CorteTransferenciaGanadoGuardarInfo result;
            try
            {
                Logger.Info();
                var corteTransferenciaBL = new CorteTransferenciaGanadoBL();
                result = corteTransferenciaBL.GuardarCorteTransferencia(guardarAnimalCorte, listaTratamientos);
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
