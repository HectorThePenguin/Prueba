using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPedidoGanadoDAL
    {
        /// <summary>
        ///     Metodo para obtener los parametros para guardar un pedido de ganado
        /// </summary>
        /// <param name="pedidoGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearPedidoGanado(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", pedidoGanadoInfo.Organizacion.OrganizacionID},
                            {"@FechaInicio", pedidoGanadoInfo.FechaInicio},
                            {"@CabezasPromedio", pedidoGanadoInfo.CabezasPromedio},
                            {"@Lunes", pedidoGanadoInfo.Lunes},
                            {"@Martes", pedidoGanadoInfo.Martes},
                            {"@Miercoles", pedidoGanadoInfo.Miercoles},
                            {"@Jueves", pedidoGanadoInfo.Jueves},
                            {"@Viernes", pedidoGanadoInfo.Viernes},
                            {"@Sabado", pedidoGanadoInfo.Sabado},
                            {"@Domingo", pedidoGanadoInfo.Domingo},
                            {"@UsuarioCreacionID", pedidoGanadoInfo.UsuarioCreacionID}
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
        ///     Metodo para obtener los parametros para guardar un pedido de ganado
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPedidoGanado(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PedidoGanadoID", pedidoGanadoEspejoInfo.PedidoGanado.PedidoGanadoID},
                            {"@CabezasPromedio", pedidoGanadoEspejoInfo.CabezasPromedio},
                            {"@Lunes", pedidoGanadoEspejoInfo.Lunes},
                            {"@Martes", pedidoGanadoEspejoInfo.Martes},
                            {"@Miercoles", pedidoGanadoEspejoInfo.Miercoles},
                            {"@Jueves", pedidoGanadoEspejoInfo.Jueves},
                            {"@Viernes", pedidoGanadoEspejoInfo.Viernes},
                            {"@Sabado", pedidoGanadoEspejoInfo.Sabado},
                            {"@Domingo", pedidoGanadoEspejoInfo.Domingo},
                            {"@UsuarioModificacionID", pedidoGanadoEspejoInfo.UsuarioModificacionID}
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
        /// Obtener pedido ganado semanal
        /// </summary>
        /// <param name="pedidoGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidoGanadoSemanal(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", pedidoGanadoInfo.Organizacion.OrganizacionID},
                                {"@FechaInicio", pedidoGanadoInfo.FechaInicio}
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
        /// ObtenerPedidoGanadoEspejo del pedidoganado
        /// </summary>
        /// <param name="pedidoGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidoGanadoEspejo(PedidoGanadoInfo pedidoGanadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@PedidoGanadoID", pedidoGanadoInfo.PedidoGanadoID},
                                     {"@Activo",EstatusEnum.Activo}
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
        ///     Metodo para obtener los parametros para guardar un pedido de ganado espejo
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearPedidoGanadoEspejo(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", pedidoGanadoEspejoInfo.Organizacion.OrganizacionID},
                            {"@FechaInicio", pedidoGanadoEspejoInfo.FechaInicio},
                            {"@CabezasPromedio", pedidoGanadoEspejoInfo.CabezasPromedio},
                            {"@Lunes", pedidoGanadoEspejoInfo.Lunes},
                            {"@Martes", pedidoGanadoEspejoInfo.Martes},
                            {"@Miercoles", pedidoGanadoEspejoInfo.Miercoles},
                            {"@Jueves", pedidoGanadoEspejoInfo.Jueves},
                            {"@Viernes", pedidoGanadoEspejoInfo.Viernes},
                            {"@Sabado", pedidoGanadoEspejoInfo.Sabado},
                            {"@Domingo", pedidoGanadoEspejoInfo.Domingo},
                            {"@UsuarioCreacionID", pedidoGanadoEspejoInfo.UsuarioCreacionID},
                            {"@UsuarioSolicitanteID", pedidoGanadoEspejoInfo.UsuarioSolicitanteID},
                            {"@Justificacion", pedidoGanadoEspejoInfo.Justificacion},
                            {"@Estatus", pedidoGanadoEspejoInfo.Estatus},
                            {"@PedidoGanadoID", pedidoGanadoEspejoInfo.PedidoGanado.PedidoGanadoID},

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
        /// Metodo para obtener los parametros para actualziar el estatus
        /// de un pedido de ganado espejo
        /// </summary>
        /// <param name="pedidoGanadoEspejoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPedidoGanadoEspejoEstatus(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PedidoGanadoEspejoID", pedidoGanadoEspejoInfo.PedidoGanadoEspejoID},
                            {"@UsuarioModificacionID", pedidoGanadoEspejoInfo.UsuarioCreacionID},
                            {"@UsuarioAproboID", pedidoGanadoEspejoInfo.UsuarioAproboID},
                            {"@Estatus", pedidoGanadoEspejoInfo.Estatus},
                            {"@Activo", pedidoGanadoEspejoInfo.Activo},

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
