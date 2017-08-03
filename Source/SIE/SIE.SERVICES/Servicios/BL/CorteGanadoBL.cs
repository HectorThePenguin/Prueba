using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;


namespace SIE.Services.Servicios.BL
{
    internal class CorteGanadoBL
    {
        internal AnimalInfo ExisteAreteMetalicoEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ExisteAreteMetalicoEnPartida(animalInfo);
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
        ///    Obtiene una Cantidad de Animales que se encuentran en enfermeria
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="noTipoCorral"></param>
        /// <returns></returns>
        internal int ObtenerCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int noTipoCorral)
        {
            int result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerCabezasEnEnfermeria(ganadoEnfermeria, noTipoCorral);
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
        ///    Se obtiene el proveedor
        /// </summary>
        /// <param name="noEmbarqueID"></param>
        /// <returns></returns>
        internal String ObtenerNombreProveedor(int noEmbarqueID)
        {
            String result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerProveedor(noEmbarqueID);
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
        ///    Se valida que existan programacion corte de ganado
        /// </summary>
        internal bool ExisteProgramacionCorteGanado(int organizacionID)
        {
            bool result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ExisteProgramacionCorteGanado(organizacionID);
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
        /// Metrodo Verificar si existen Partidas programadas
        /// </summary>
        internal AnimalInfo ExisteAreteEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ExisteAreteEnPartida(animalInfo);
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
        /// Metrodo Para Registrar los movimientos en el almacen
        /// </summary>
        internal bool RegistrarMovimientosAlmacen(MovimientosAlmacen movimientosAlmacen)
        {
            bool result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.RegistrarMovimientosAlmacen(movimientosAlmacen);
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
        /// Metrodo Para obtener el total de cabezas cortadas
        /// </summary>
        internal int ObtenerCabezasCortadas(CabezasCortadas cabezasCortadas)
        {
            int result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerCabezasCortadas(cabezasCortadas);
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
        /// Metrodo Para obtener el total de cabezas Muertas
        /// </summary>
        internal int ObtenerCabezasMuertas(CabezasCortadas cabezasMuertas)
        {
            int result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerCabezasMuertas(cabezasMuertas);
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
        ///  Metrodo Para Obtener pesos origen y llegada
        /// </summary>
        internal bool ObtenerPesosOrigenLlegada(int orgnizacionID, int corralOrigenID, int loteOrigenID)
        {
            bool result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerPesosOrigenLlegada(orgnizacionID, corralOrigenID, loteOrigenID);
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
        /// Obtener las cabezas sobrantes cortadas
        /// </summary>
        /// <param name="paramCabezaSobrantesCortadas"></param>
        /// <returns></returns>
        internal IList<CabezasSobrantesPorEntradaInfo> ObtenerCabezasSobrantes(CabezasCortadas paramCabezaSobrantesCortadas)
        {
            IList<CabezasSobrantesPorEntradaInfo> result;
            try
            {
                Logger.Info();
                var corteGanadoDAL = new CorteGanadoDAL();
                result = corteGanadoDAL.ObtenerCabezasSobrantes(paramCabezaSobrantesCortadas);
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
