using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;


namespace SIE.Services.Servicios.PL
{
    public class CorteGanadoPL
    {
        /// <summary>
        /// Verifica si existe el arete metalico(RFID) en la partida
        /// </summary>
        /// <param name="animalInfo"Datos del animal></param>
        /// <returns></returns>
        public AnimalInfo ExisteAreteMetalicoEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result = null;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ExisteAreteMetalicoEnPartida(animalInfo);
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
        public int ObtenerCabezasEnEnfermeria( EntradaGanadoInfo ganadoEnfermeria, int noTipoCorral)
        {
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                var result = corteGanadoBL.ObtenerCabezasEnEnfermeria(ganadoEnfermeria, noTipoCorral);

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
        ///    Se obtiene el proveedor
        /// </summary>
        /// <param name="noEmbarqueID"></param>
        /// <returns></returns>
        public String ObtenerProveedor(int noEmbarqueID)
        {
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                var result = corteGanadoBL.ObtenerNombreProveedor(noEmbarqueID);

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
        ///    Se valida que existan programacion corte de ganado
        /// </summary>
        public bool ExisteProgramacionCorteGanado(int organizacionID)
        {
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                var result = corteGanadoBL.ExisteProgramacionCorteGanado(organizacionID);

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
        /// Metrodo Verificar si existen Partidas programadas
        /// </summary>
        public AnimalInfo ExisteAreteEnPartida(AnimalInfo animalInfo)
        {
            AnimalInfo result = null;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ExisteAreteEnPartida(animalInfo);

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
        public bool RegistrarMovimientosAlmacen(MovimientosAlmacen movimientosAlmacen)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.RegistrarMovimientosAlmacen(movimientosAlmacen);

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
        public int ObtenerCabezasCortadas(CabezasCortadas cabezasCortadas)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ObtenerCabezasCortadas(cabezasCortadas);

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
        public int ObtenerCabezasMuertas(CabezasCortadas cabezasMuertas)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ObtenerCabezasMuertas(cabezasMuertas);

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
        public bool ObtenerPesosOrigenLlegada(int orgnizacionID, int corralOrigenID, int loteOrigenID)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ObtenerPesosOrigenLlegada(orgnizacionID, corralOrigenID, loteOrigenID);

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
        /// Metodo para obtener el numero de cabezar sobrantes cortadas
        /// </summary>
        /// <param name="paramCabezaSobrantesCortadas"></param>
        /// <returns></returns>
        public IList<CabezasSobrantesPorEntradaInfo> ObtenerCabezasSobrantes(CabezasCortadas paramCabezaSobrantesCortadas)
        {
            IList<CabezasSobrantesPorEntradaInfo> result = null;
            try
            {
                Logger.Info();
                var corteGanadoBL = new CorteGanadoBL();
                result = corteGanadoBL.ObtenerCabezasSobrantes(paramCabezaSobrantesCortadas);

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

