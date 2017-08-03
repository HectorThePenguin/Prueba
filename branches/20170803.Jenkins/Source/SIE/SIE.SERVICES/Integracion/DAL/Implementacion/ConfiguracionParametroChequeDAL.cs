using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ConfiguracionParametroChequeDAL : DALBase
    {

        /// <summary>
        /// Metodo para Crear un registro de configuracion parametro cheque
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        /// <summary>
        /// Obtiene diferencias de inventario
        /// </summary>
        /// <returns>Lista de configuracion de parametros de cheque</returns>
        /// <param name="usuario">Identificador del usuario loggeado</param>
        internal List<CatParametroConfiguracionBancoInfo> ObtenerConfiguracionParametroPorBanco(CatParametroConfiguracionBancoInfo info, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionParametroChequeDAL.ObtenerParametroConfiguracionParametroChequePorBanco(info, usuario);
                DataSet ds = Retrieve("CatConfiguracionParametroBanco_ObtenerPorBanco", parameters);
                List<CatParametroConfiguracionBancoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionParametroChequeDAL.ObtenerConfiguracionParametroBanco(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Actauliza la configuracion de parametros de cheque
        /// </summary>
        /// <param name="listaConfiguracionParametros">Lista de configuracion de parametros de cheque</param>
        /// <param name="usuarioInfo">Usuario que realizala modificacion</param>
        /// <returns>Numero de registros actualizados</returns>
        internal int ActualizarConfiguracionParametros(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametros, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionParametroChequeDAL.ObtenerParametroConfiguracionParametroActualizar(listaConfiguracionParametros, usuarioInfo);
                int resultado = Create("CatParametroConfigBanco_EditarXML", parameters);
                return resultado;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
