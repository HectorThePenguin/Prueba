
using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RegistroVigilanciaPL
    {
        /// <summary>
        /// Obtiene un registro de vigilancia
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        public RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorId(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaBl = new RegistroVigilanciaBL();
                registroVigilancia = registroVigilanciaBl.ObtenerRegistroVigilanciaPorId(registroVigilanciaInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene un registro de vigilancia por folio turno que se encuentre activo.
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        public RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorFolioTurno(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaBl = new RegistroVigilanciaBL();
                registroVigilancia = registroVigilanciaBl.ObtenerRegistroVigilanciaPorFolioTurno(registroVigilanciaInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene un registro de vigilancia por folio turno idenpendientemente de si esta activo o no.
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        public RegistroVigilanciaInfo ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaBl = new RegistroVigilanciaBL();
                registroVigilancia = registroVigilanciaBl.ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(registroVigilanciaInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene un registro de vigilancia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        public ResultadoInfo<RegistroVigilanciaInfo> ObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaInfo filtro)
        {
            ResultadoInfo<RegistroVigilanciaInfo> registroVigilancia;
            try
            {
                Logger.Info();
                var registroVigilanciaBl = new RegistroVigilanciaBL();
                registroVigilancia = registroVigilanciaBl.ObtenerPorPagina(pagina, filtro);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return registroVigilancia;
        }
        /// <summary>
        /// Valida que el las placas del camion ingresado no esté activo y tenga fecha de salida
        /// </summary>
        /// <param name="camion"></param>
        /// <returns></returns>
        public bool ObtenerDisponibilidadCamion(CamionInfo camion)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var registroVigilaciaBl = new RegistroVigilanciaBL();
                resultado = registroVigilaciaBl.ObtenerDisponibilidadCamion(camion);
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
    }
}
