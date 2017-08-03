using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class PaisPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PaisInfo> ObtenerPorPagina(PaginacionInfo pagina, PaisInfo filtro)
        {
            ResultadoInfo<PaisInfo> resultado;
            try
            {
                Logger.Info();
                var paisBL = new PaisBL();
                resultado = paisBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un Pais por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public PaisInfo ObtenerPorID(int paisID)
        {
            PaisInfo paisInfo;
            try
            {
                Logger.Info();
                var paisBL = new PaisBL();
                paisInfo = paisBL.ObtenerPorID(paisID);
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
            return paisInfo;
        }

        /// <summary>
        /// Obtiene un pais por Id
        /// </summary>
        /// <param name="paisInfo"></param>
        /// <returns></returns>
        public PaisInfo ObtenerPorID(PaisInfo paisInfo)
        {
            PaisInfo pasInfo;
            try
            {
                Logger.Info();
                var paisBL = new PaisBL();
                pasInfo = paisBL.ObtenerPorID(paisInfo);
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
            return pasInfo;
        }

    }
}
