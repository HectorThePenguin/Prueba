using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxMarcasDAL
    {
        /// <summary>
        /// Método para obtener los parametros para guardar marca
        /// </summary>
        /// <param name="marcaInfo"> Objeto con los parametros </param>
        /// <returns> lista de los parametros </returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarMarca(MarcasInfo marcaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", marcaInfo.Descripcion},
                            {"@Activo", marcaInfo.Activo},
                            {"@Tracto", marcaInfo.EsTracto},
                            {"@UsuarioCreacionId", marcaInfo.UsuarioCreacionID},
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
        /// Método para obtener los parametros para guardar marca
        /// </summary>
        /// <returns> lista de los parametros </returns>
        internal static Dictionary<string, object> ObtenerMarcas(EstatusEnum Tipo, EstatusEnum Activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Tipo", Tipo},
                            {"@Activo", Activo}
                            
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
        /// Obtener parametros para actualizar la marca
        /// </summary>
        /// <param name="marcaInfo"> Objeto que contiene los parametros </param>
        /// <returns> Lista de parametros </returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarMarca(MarcasInfo marcaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@MarcaId", marcaInfo.MarcaId},
                            {"@Descripcion", marcaInfo.Descripcion},
                            {"@Activo", marcaInfo.Activo},
                            {"@Tracto", marcaInfo.EsTracto},
                            {"@UsuarioModificacionId", marcaInfo.UsuarioCreacionID},
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
        /// Método que obtiene los parametros para obtener las marcas por paginador.
        /// </summary>
        /// <param name="pagina"> Objeto con los parametros del paginador </param>
        /// <param name="filtro"> Objeto con los parametros de la marca </param>
        /// <returns> Lista de parametros </returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, MarcasInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                {
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        /// Obtiene los parametros para el metodo de VerificaExistenciaMarca
        /// </summary>
        /// <param name="marcaInfo"> Objeto con los parametros </param>
        /// <returns> Lista de parametros </returns>
        internal static Dictionary<string, object> VerificaExistenciaMarca(MarcasInfo marcaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", marcaInfo.Descripcion},
                            {"@Tracto", marcaInfo.EsTracto}
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
