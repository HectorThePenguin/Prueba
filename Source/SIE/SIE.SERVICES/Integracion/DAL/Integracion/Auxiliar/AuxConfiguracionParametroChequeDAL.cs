using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxConfiguracionParametroChequeDAL
    {
        /// <summary>
        /// Obtiene los parametros para mandar almacenar la configuracion del parametro del cheque
        /// </summary>
        /// <param name="listaConfiguracionParametroCheque"></param>
        /// <returns>Diccionario con parametros</returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarConfiguracionParametroCheque(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametroCheque)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaConfiguracionParametroCheque
                                 select new XElement("ParametroBanco",
                                        new XElement("ParametroID", detalle.ParametroID.ParametroID),
                                        new XElement("X", detalle.X),
                                        new XElement("Y", detalle.Y),
                                        new XElement("Width", detalle.Width),
                                        new XElement("Estatus", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@xmlParametroConfiguracionBanco", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary>
        /// Obtiene los prametros para mandar modificar la configuracion del parametro del cheque
        /// </summary>
        /// <param name="listaConfiguracionParametroCheque"></param>
        /// <returns>Diccionario con parametros</returns>
        internal static Dictionary<string, object> ObtenerParametroEditarConfiguracionParametroCheque(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametroCheque)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaConfiguracionParametroCheque
                                 select new XElement("ParametroBanco",
                                        new XElement("CatParametroConfigBancoID", detalle.ParametroID.ParametroID),
                                        new XElement("X", detalle.X),
                                        new XElement("Y", detalle.Y),
                                        new XElement("Width", detalle.Width),
                                        new XElement("Estatus", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioModificacionID", detalle.UsuarioModificacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@xmlParametroConfiguracionBanco", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para obtener la configuracion de parametros de cheques por banco
        /// </summary>
        /// <param name="info">Informacion para el filtro</param>
        /// <param name="usuario">Identificador del usuario loggeado</param>
        /// <returns>Diccionario con parametros</returns>
        internal static Dictionary<string, object> ObtenerParametroConfiguracionParametroChequePorBanco(CatParametroConfiguracionBancoInfo info, UsuarioInfo usuario)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
 
                parametros = new Dictionary<string, object>
                    {
                        {"@BancoID", info.BancoID.BancoID},
                        {"@Activo", info.Activo.GetHashCode()},
                        {"@Usuario", usuario.UsuarioID }
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para actualizar la configuracion de parametro cheque
        /// </summary>
        /// <param name="listaConfiguracionParametro">Lista de configuracion parametro</param>
        /// <param name="usuarioInfo">Usuario que modifica</param>
        /// <returns>Diccionario con parametros</returns>
        internal static Dictionary<string, object> ObtenerParametroConfiguracionParametroActualizar(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametro, UsuarioInfo usuarioInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaConfiguracionParametro
                                 select new XElement("ConfiguracionParametroBancoEditar",
                                        new XElement("CatParametroConfigBancoID", detalle.CatParametroConfiguracionBancoID),
                                        new XElement("X", detalle.X),
                                        new XElement("Y", detalle.Y),
                                        new XElement("Width", detalle.Width),
                                        new XElement("Estatus", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioModificacionID", usuarioInfo.UsuarioID)
                                     ));

                parametros = new Dictionary<string, object>
                    {
                        {"@xmlParametroConfiguracionBanco", xml.ToString()} 
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }
    }
}
