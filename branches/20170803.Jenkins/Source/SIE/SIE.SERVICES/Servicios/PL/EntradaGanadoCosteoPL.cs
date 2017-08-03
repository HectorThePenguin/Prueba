using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaGanadoCosteoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una Entrada Ganado Costeo
        /// </summary>
        /// <param name="contenedorCosteoEntradaGanadoInfo"></param>
        public MemoryStream Guardar(ContenedorCosteoEntradaGanadoInfo contenedorCosteoEntradaGanadoInfo)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                MemoryStream stream = entradaGanadoCosteoBL.Guardar(contenedorCosteoEntradaGanadoInfo);
                return stream;
            }
            catch (ExcepcionServicio)
            {
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
        ///     Obtiene una Entrada Ganado Costeo por su Id
        /// </summary>
        /// <param name="entradaGanadoCosteoID"></param>
        /// <returns></returns>
        public EntradaGanadoCosteoInfo ObtenerPorID(int entradaGanadoCosteoID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                EntradaGanadoCosteoInfo result = entradaGanadoCosteoBL.ObtenerPorID(entradaGanadoCosteoID);

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
        ///     Obtiene una Entrada Ganado Costeo por su Entrada Id
        /// </summary>
        /// <param name="entradaID"></param>
        /// <returns></returns>
        public EntradaGanadoCosteoInfo ObtenerPorEntradaID(int entradaID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                EntradaGanadoCosteoInfo result = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaID);

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
        /// Metodo para Guardar/Modificar una Entrada Ganado Costeo
        /// </summary>
        /// <param name="filtroCalificacionGanadoInfo"></param>
        public void GuardarCalidadGanado(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                entradaGanadoCosteoBL.GuardarCalidadGanado(filtroCalificacionGanadoInfo);
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
        /// Obtiene una lista de entradas ganado costeo
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<EntradaGanadoCosteoInfo> ObtenerEntradasPorFechasConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                List<EntradaGanadoCosteoInfo> result =
                    entradaGanadoCosteoBL.ObtenerEntradasPorFechasConciliacion(organizacionID, fechaInicial, fechaFinal);
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
    }
}
