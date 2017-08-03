using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Servicios.BL;


namespace SIE.Services.Servicios.PL
{
    /// <summary>
    /// Clase para administrar la capa de presentacion para las opciones de Muerte del sistema
    /// </summary>
    public class MuertePL
    {
        /// <summary>
        /// Obtiene la lista de ganado detectado, recolectado e ingresado a necropsia para su salida
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<MuerteInfo> ObtenerGanadoParaSalidaPorMuerte(int organizacionId)
        {
            IList<MuerteInfo> retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerTodosMuertosParaNecropsia(organizacionId);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de problemas registrados para la salida por necropsia
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaInfo> ObtenerListaProblemasNecropsia()
        {
            IList<ProblemaInfo> retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerListaProblemasNecropsia();
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

            return retValue;
        }

        /// <summary>
        /// Obtiene la informacion del ganado muerto por arete
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        public MuerteInfo ObtenerGanadoMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerGanadoMuertoPorArete(organizacionId, numeroArete);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de movimientos(Muertes) a cancelar
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<MuerteInfo> ObtenerInformacionCancelarMovimiento(int organizacionId)
        {
            IList<MuerteInfo> retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerInformacionCancelarMovimiento(organizacionId);
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

            return retValue;
        }

        /// <summary>
        /// Guarda la salida por muerte de necropsia
        /// </summary>
        /// <param name="muerte"></param>
        /// <returns></returns>
        public int GuardarSalidaPorMuerteNecropsia(MuerteInfo muerte)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var bl = new MuerteBL();
                retValue = bl.GuardarSalidaPorMuerteNecropsia(muerte);
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
            return retValue;
        }
        
        /// <summary>
        /// Metodo para Guardar el muerteInfo
        /// </summary>
        /// <param name="muerteInfo"></param>
        public void CancelarMovimientoMuerte(MuerteInfo muerteInfo)
        {
            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                muerteBl.CancelarMovimientoMuerte(muerteInfo);
                //return result;
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
        /// Obtiene la lista de ganado muerto para recepcion en necropsia
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<MuerteInfo> ObtenerGanadoMuertoParaRecepcion(int organizacionId)
        {
            IList<MuerteInfo> retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerGanadoMuertoParaRecepcion(organizacionId);
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

            return retValue;
        }

        /// <summary>
        /// Guardar la lista de muertes recibidas en necropsia
        /// </summary>
        /// <param name="muertes">lista de identificadores de las muertes</param>
        /// <param name="operadorId">Identificador del operador de la recepcion</param>
        /// <returns></returns>
        public int GuardarRecepcionGanadoMuerto(IList<MuerteInfo> muertes, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var bl = new MuerteBL();
                retValue = bl.GuardarRecepcionGanadoMuerto(muertes, operadorId);
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
            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de aretes listos para recoleccion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<MuerteInfo> ObtenerAretesMuertosRecoleccion(int organizacionId)
        {
            IList<MuerteInfo> retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerAretesMuertosRecoleccion(organizacionId);
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

            return retValue;
        }

        /// <summary>
        /// Registra los aretes recolectados de ganado muerto
        /// </summary>
        /// <param name="muertes"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        public decimal GuardarRecoleccionGanadoMuerto(List<MuerteInfo> muertes, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var bl = new MuerteBL();
                retValue = bl.GuardarRecoleccionGanadoMuerto(muertes, operadorId);
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
            return retValue;
        }

        /// <summary>
        /// Guarda la informacion de una muerte
        /// </summary>
        /// <param name="muerte"></param>
        /// <param name="esCargaInicial"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        public int GuardarMuerte(MuerteInfo muerte, FlagCargaInicial esCargaInicial, AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                int resultado = muerteBl.GuardarMuerte(muerte, esCargaInicial, animal);
                return resultado;
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
        /// Obtiene la informacion del ganado muerto por arete para corrales de recepcion
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        public MuerteInfo ObtenerGanadoMuertoPorAreteRecepcion(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerGanadoMuertoPorAreteRecepcion(organizacionId, numeroArete);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene las muertes por Fecha Necropsia
        /// </summary>
        /// <param name="muerteInfo"></param>
        /// <returns></returns>
        public List<SalidaGanadoMuertoInfo> ObtenerMuertesFechaNecropsia(MuerteInfo muerteInfo)
        {
            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                return muerteBl.ObtenerMuertesFechaNecropsia(muerteInfo);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene la informacion del ganado muerto por arete
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        public MuerteInfo ObtenerMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;

            try
            {
                Logger.Info();
                var muerteBl = new MuerteBL();
                retValue = muerteBl.ObtenerMuertoPorArete(organizacionId, numeroArete);
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

            return retValue;
        }

    }
}
