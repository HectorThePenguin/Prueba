using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxBasculaMultipesajeDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado BasculaMultipesaje_Crear
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> InsertarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    { "@Chofer", basculaMultipesajeInfo.Chofer },
                    { "@Placas", basculaMultipesajeInfo.Placas },
                    { "@PesoBruto", basculaMultipesajeInfo.PesoBruto},
                    { "@PesoTara", basculaMultipesajeInfo.PesoTara },
                    { "@FechaPesoTara", basculaMultipesajeInfo.FechaPesoTara },
                    { "@FechaPesoBruto", basculaMultipesajeInfo.FechaPesoBruto },
                    { "@UsuarioCreacionID", basculaMultipesajeInfo.UsuarioCreacion },
                    { "@Producto", basculaMultipesajeInfo.Producto },
                    { "@OrganizacionID", basculaMultipesajeInfo.OrganizacionInfo.OrganizacionID },
                    { "@TipoFolio", Convert.ToInt32(TipoFolio.BasculaMultipesaje) },
                    { "@Observacion", basculaMultipesajeInfo.Observacion },
                    { "@EnvioSAP", basculaMultipesajeInfo.EnvioSAP },
                    { "@OperadorID", basculaMultipesajeInfo.QuienRecibe.OperadorID }
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado BasculaMultipesaje_ObtenerPesaje
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ConsultaBasculaMultipesaje(long folio, int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Folio", folio},
                    {"@OrganizacionId", organizacionId}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado BasculaMultipesaje_ObtenerFoliosPorPagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroFolios(PaginacionInfo pagina, FolioMultipesajeInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Fecha", DateTime.Now},
                    {"@Inicio", pagina.Inicio},
                    {"@Limite", pagina.Limite},
                    {"@OrganizacionId", filtro.OrganizacionId},
                    {"@Descripcion", filtro.Chofer.Trim()},
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado BasculaMultipesaje_Actualizar
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    { "@Folio", basculaMultipesajeInfo.FolioMultipesaje.Folio },
                    { "@PesoBruto", basculaMultipesajeInfo.PesoBruto },
                    { "@PesoTara", basculaMultipesajeInfo.PesoTara },
                    { "@FechaPesoTara", basculaMultipesajeInfo.FechaPesoTara },
                    { "@FechaPesoBruto", basculaMultipesajeInfo.FechaPesoBruto },
                    { "@UsuarioModificacionID", basculaMultipesajeInfo.UsuarioCreacion },
                    { "@Observacion", basculaMultipesajeInfo.Observacion }
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
