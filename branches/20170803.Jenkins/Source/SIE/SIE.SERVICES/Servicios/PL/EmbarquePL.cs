using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EmbarquePL
    {
        /// <summary>
        /// Obtiene una lista de embarques pendiente de recibir 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaEmbarqueInfo> ObtenerEmbarquesPedientesPorPagina(PaginacionInfo pagina,
                                                                                     FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EntradaEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                result = embarqueBL.ObtenerEmbarquesPedientesPorPagina(pagina, filtro);
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
        ///     Metodo que guarda un Embarque
        /// </summary>
        /// <param name="embarqueInfo"></param>
        public int GuardarEmbarque(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                return embarqueBL.GuardarEmbarque(embarqueInfo);
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
        ///     Obtiene un lista de los Embarquees
        /// </summary>
        /// <returns> </returns>
        public IList<EmbarqueInfo> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///      Obtiene una lista pagina del embarques
        /// </summary>
        /// <returns> </returns>
        public ResultadoInfo<EmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EmbarqueInfo> result;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                result = embarqueBL.ObtenerEntradasActivasPorPagina(pagina, filtro);
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
        ///      Obtiene una lista pagina del embarques
        /// </summary>
        /// <returns> </returns>
        public ResultadoInfo<EmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, EmbarqueInfo filtro)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///      Obtiene un ProgramacionEmbarque por su Id
        /// </summary>
        /// <returns> </returns>
        public EmbarqueInfo ObtenerPorID(int embarqueId)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                result = embarqueBL.ObtenerPorID(embarqueId);
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
        ///      Obtiene un Embarque por su folio
        /// </summary>
        /// <returns> </returns>
        public EmbarqueInfo ObtenerPorFolioEmbarque(int folioEmbarqueId)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                result = embarqueBL.ObtenerPorFolioEmbarque(folioEmbarqueId);
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
        ///  Obtiene la lista de las escalas por EmbarqueID
        /// </summary>
        /// <returns> </returns>
        public IList<EmbarqueDetalleInfo> ObtenerEscalasPorEmbarqueID(int embarqueId)
        {
            IList<EmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                result = embarqueBL.ObtenerEscalasPorEmbarqueID(embarqueId);
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
        ///     Obtiene registro del embarque por su Id
        /// </summary>
        /// <param name="folioEmbarque"> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        public EmbarqueInfo ObtenerPorFolioEmbarqueOrganizacion(int folioEmbarque, int organizacionId)
        {
            EmbarqueInfo info;
            try
            {
                Logger.Info();
                var embarqueBL = new EmbarqueBL();
                info = embarqueBL.ObtenerPorFolioEmbarqueOrganizacion(folioEmbarque, organizacionId);
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
    }
}