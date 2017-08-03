using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProduccionDiariaDetalleDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ProduccionDiariaDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProduccionDiariaDetalleID", filtro.ProduccionDiariaDetalleID},
                            {"@Activo", filtro.Activo},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(ProduccionDiariaDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Activo", info.Activo},
							{"@ProduccionDiariaDetalleID", info.ProduccionDiariaDetalleID},
							{"@ProduccionDiariaID", info.ProduccionDiaria.ProduccionDiariaID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@PesajeMateriaPrimaID", info.PesajeMateriaPrima.PesajeMateriaPrimaID},
							{"@EspecificacionForraje", info.EspecificacionForraje},
							{"@HoraInicial", info.HoraInicial},
							{"@HoraFinal", info.HoraFinal},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(ProduccionDiariaDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Activo", info.Activo},
							{"@ProduccionDiariaDetalleID", info.ProduccionDiariaDetalleID},
							{"@ProduccionDiariaID", info.ProduccionDiaria.ProduccionDiariaID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@PesajeMateriaPrimaID", info.PesajeMateriaPrima.PesajeMateriaPrimaID},
							{"@EspecificacionForraje", info.EspecificacionForraje},
							{"@HoraInicial", info.HoraInicial},
							{"@HoraFinal", info.HoraFinal},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="produccionDiariaDetalleID">Identificador de la entidad ProduccionDiariaDetalle</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int produccionDiariaDetalleID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@ProduccionDiariaDetalleID", produccionDiariaDetalleID}
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@ProduccionDiariaDetalleID", descripcion}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="produccionDiaria">Valores de la entidad</param>
        ///  <param name="produccionDiariaID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerGuardarProduccionDiariaDetalle(ProduccionDiariaInfo produccionDiaria, int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in produccionDiaria.ListaProduccionDiariaDetalle
                                              select
                                                  new XElement("ProduccionDiariaDetalle",
                                                               new XElement("ProduccionDiariaDetalleID", info.ProduccionDiariaDetalleID),
                                                               new XElement("ProduccionDiariaID", produccionDiariaID),
                                                               new XElement("ProductoID", info.ProductoID),
                                                               new XElement("PesajeMateriaPrimaID", info.PesajeMateriaPrimaID),
                                                               new XElement("EspecificacionForraje", info.EspecificacionForraje),
                                                               new XElement("HoraInicial", info.HoraInicial),
                                                               new XElement("HoraFinal", info.HoraFinal),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlProduccionDiariaDetalle", xml.ToString()}
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

