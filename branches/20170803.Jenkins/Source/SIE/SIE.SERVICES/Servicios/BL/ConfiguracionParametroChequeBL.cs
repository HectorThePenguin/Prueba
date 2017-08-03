using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ConfiguracionParametroChequeBL
    {

        /// <summary>
        /// Obtiene la lista de configuracion de parametros
        /// </summary>
        /// <param name="catConfiguracionParametroBancoInfo">Filtro de busqueda</param>
        /// <returns>Lista de parametros configuracion</returns>
        /// <param name="usuario">Identificador del usuario loggeado</param>
        internal static List<CatParametroConfiguracionBancoInfo> ObtenerParametroConfiguracionCheque(CatParametroConfiguracionBancoInfo catConfiguracionParametroBancoInfo, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                var configuracionParametroChequeDAL = new ConfiguracionParametroChequeDAL();
                List<CatParametroConfiguracionBancoInfo> result = configuracionParametroChequeDAL.ObtenerConfiguracionParametroPorBanco(catConfiguracionParametroBancoInfo, usuario);
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
        /// Actualiza la configuracion de parametros de cheque
        /// </summary>
        /// <param name="listaConfiguracionParametroBanco">Lista de configuracion de parametros de cheque</param>
        /// <param name="usuarioInfo">Usuario que modifica</param>
        /// <returns>Numero de registros afectados</returns>
        internal static int ActualizarConfiguracionParametroCheque(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametroBanco, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var configuracionParametroChequeDAL = new ConfiguracionParametroChequeDAL();
                int result = configuracionParametroChequeDAL.ActualizarConfiguracionParametros(listaConfiguracionParametroBanco, usuarioInfo);
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
