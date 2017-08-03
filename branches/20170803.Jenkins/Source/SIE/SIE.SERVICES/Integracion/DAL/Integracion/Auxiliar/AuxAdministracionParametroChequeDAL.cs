using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAdministracionParametroChequeDAL
    {

        /// <summary>
        /// Obtiene los prametros para mandar almacenar el detalle de almacen movimiento
        /// </summary>
        /// <param name="listaParametroCheque"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarParametroCheque(List<CatParametroBancoInfo> listaParametroCheque)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaParametroCheque
                                 select new XElement("ParametroBanco",
                                        new XElement("Descripcion", detalle.Descripcion),
                                        new XElement("Clave", detalle.Clave),
                                        new XElement("TipoParametro", detalle.TipoParametroID.GetHashCode()),
                                        new XElement("Valor", detalle.Valor),
                                        new XElement("Estatus", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@xmlParametroBanco", xml.ToString()},
                        {"@x" , 0},
                        {"@y" , 0},
                        {"@with" , 0},
                        {"@activoConfiguracion", EstatusEnum.Activo.GetHashCode()}
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
        /// Obtiene los prametros para mandar almacenar el detalle de almacen movimiento
        /// </summary>
        /// <param name="listaParametroCheque"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroEditarParametroCheque(List<CatParametroBancoInfo> listaParametroCheque)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaParametroCheque
                                 select new XElement("ParametroBanco",
                                        new XElement("CatParametroBancoID", detalle.ParametroID),
                                        new XElement("Descripcion", detalle.Descripcion),
                                        new XElement("Clave", detalle.Clave),
                                        new XElement("TipoParametro", detalle.TipoParametroID.GetHashCode()),
                                        new XElement("Valor", detalle.Valor),
                                        new XElement("Estatus", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioModificacionID", detalle.UsuarioModificacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@xmlParametroBanco", xml.ToString()}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina">Informacion de paginacion usada en la consulta</param>
        /// <param name="filtro">filtro de busqueda que se usara</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCatParametroBanco(PaginacionInfo pagina, CatParametroBancoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
                            {"@TipoParametro", filtro.TipoParametroID.GetHashCode()},
                            {"@Activo", filtro.Activo.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesario
        /// </summary>
        /// <param name="filtro">Objeto con los datos</param>
        /// <returns>Diccionario con los parametros necesarios</returns>
        internal static Dictionary<string, object> ObtenerCatParametroBancoPorDescripcion(CatParametroBancoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
                            {"@Activo", filtro.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
