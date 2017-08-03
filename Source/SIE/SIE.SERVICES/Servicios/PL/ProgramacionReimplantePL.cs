using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Infos;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Servicios.PL
{
    public class ProgramacionReimplantePL
    {
        
        /// <summary>
        /// Obtiene la lista de corrales disponibles para traspaso en programacion de reimplante
        /// </summary>
        /// <param name="organizacionId">Id de la Organizacion</param>
        /// <returns></returns>
        public List<CorralInfo> ObtenerCorralesParaTraspaso(int organizacionId)
        {
            var lista = new List<CorralInfo>();

            return lista;
        }

        /// <summary>
        /// Obtiene la lista de lotes disponibles para programar el reimplante
        /// </summary>
        /// <param name="organizacionId">Id de la Organizacion</param>
        /// <returns></returns>
        public ResultadoInfo<OrdenReimplanteInfo> ObtenerLotesDisponiblesReimplante(int organizacionId)
        {
            ResultadoInfo<OrdenReimplanteInfo> lista = null;
            try
            {
                Logger.Info();
                var pReimplanteBl = new ProgramacionReimplanteBL();
                lista = pReimplanteBl.ObtenerLotesDisponiblesReimplante(organizacionId);
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


            return lista;
        }

        /// <summary>
        /// Almacena la orden de reimplante programada
        /// </summary>
        /// <param name="orden">Orden de reimplante</param>
        /// <returns>Retorna true cuando se ha ejecutado correctamente el proceso</returns>
        public bool GuardarProgramacionReimplante(IList<OrdenReimplanteInfo> orden)
        {
            bool retValue = true; 
            try
            {
                Logger.Info();
                var pReimplanteBl = new ProgramacionReimplanteBL();
                retValue = pReimplanteBl.GuardarProgramacionReimplante(orden);
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
        /// descuenta la cantidad de ganado 
        /// </summary>
        /// <param name="folioProgReimplante">Folio del Reimplante Programado</param>
        /// <returns>Retorna true cuando ejecuta la eliminacion del reimplante programado</returns>
        public bool EliminarProgramacionReimplante(int folioProgReimplante)
        {
            bool respuesta = false;
            try
            {
                Logger.Info();
                var reimplannte = new ProgramacionReimplanteBL();
                respuesta = reimplannte.EliminarProgramacionReimplante(folioProgReimplante);
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
            return respuesta;
        }

        public bool GuardarFechaReal(String fechaReal, LoteReimplanteInfo loteReimplante)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var reimplannte = new ProgramacionReimplanteBL();
                regresa = reimplannte.GuardarFechaReal(fechaReal, loteReimplante);
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

            return regresa;
        }


        public bool ExisteProgramacionReimplante(int organizacionId, DateTime selectedDate)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var reimplannte = new ProgramacionReimplanteBL();
                regresa = reimplannte.ExisteProgramacionReimplante(organizacionId, selectedDate);
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

            return regresa;
        }

        public List<ProgramacionReinplanteInfo> ObtenerProgramacionReimplantePorLoteID(LoteInfo lote)
        {
            List<ProgramacionReinplanteInfo> lista = null;
            try
            {
                Logger.Info();
                var pReimplanteBl = new ProgramacionReimplanteBL();
                lista = pReimplanteBl.ObtenerProgramacionReimplantePorLoteID(lote);
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


            return lista;
        }

        /// <summary>
        /// Validar que el reimplante sea de corral a corral
        /// </summary>
        /// <param name="loteIDOrigen"></param>
        /// <param name="corralIDDestino"></param>
        /// <returns></returns>
        public bool ValidarReimplanteCorralACorral(int loteIDOrigen, int corralIDDestino)
        {
            var regresa = false;
            try
            {
                Logger.Info();
                var reimplante = new ProgramacionReimplanteBL();
                regresa = reimplante.ValidarReimplanteCorralACorral(loteIDOrigen, corralIDDestino);
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

            return regresa;
        }
    }
}
