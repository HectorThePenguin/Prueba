using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionParametroChequePL
    {

        /// <summary>
        /// Obtiena la configuracion de paramatros de cheque por banco
        /// </summary>
        /// <param name="info">Filtro con el banco requerido</param>
        /// <returns>Lista de configuracion de parametros cheque</returns>
        /// <param name="usuario">Identificador del usuario loggeado</param>
        public List<CatParametroConfiguracionBancoInfo> ObtenerPorBanco(CatParametroConfiguracionBancoInfo info, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();

                List<CatParametroConfiguracionBancoInfo> result = ConfiguracionParametroChequeBL.ObtenerParametroConfiguracionCheque(info, usuario);
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
        /// Actualiza la configuracion de parametros cheque
        /// </summary>
        /// <param name="listaConfiguracionParametro">Lista de configuracion de parametros cheque</param>
        /// <param name="usuarioInfo">Usuario que realiza la modificacion</param>
        /// <returns>Numero de registros afectados</returns>
        public int ActualizarConfiguracionParametroCheque(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametro, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();

                int result = ConfiguracionParametroChequeBL.ActualizarConfiguracionParametroCheque(listaConfiguracionParametro, usuarioInfo);
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
