using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxProgramacionEmbarqueDAL
    {
        /// <summary>
        /// Método que obtiene los parametros necesarios para obtener los embarques de acuerdo a una organizacion.
        /// </summary>
        /// <param name="organizacionId"> ID de la organización para filtrar </param>
        /// <returns> Parametros necesarios. </returns>
        internal static Dictionary<string, object> ObtenerProgramacionPorOrganizacionId(int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId}
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
        /// Método para parametrizar el id de la organización.
        /// </summary>
        /// <param name="ruteoInfo"> Objeto con la información del ruteo </param>
        /// <returns> Parametro creado </returns>
        internal static Dictionary<string, object> ObtenerRuteosPorOrganizacionId(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionOrigenID", ruteoInfo.OrganizacionOrigen.OrganizacionID},
                            {"@OrganizacionDestinoID", ruteoInfo.OrganizacionDestino.OrganizacionID}
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
        /// Método para inactivar un registro en la tabla de embarque
        /// </summary>
        /// <param name="embarqueInfo"> Objeto con la información del embarque</param>
        /// <returns> Parametro creado </returns>
        internal static Dictionary<string, object> Eliminar(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EmbarqueID", embarqueInfo.EmbarqueID},
                            {"@EstausCancelado", 3},
                            {"@UsuarioModificacionID", embarqueInfo.UsuarioModificacionID}
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
        /// Método que obtiene los parametros para obtener los detalles del ruteo
        /// </summary>
        /// <param name="embarqueInfo"> Información del embarque </param>
        /// <returns> Parameteos creados </returns>
        internal static Dictionary<string, object> ObtenerRuteoDetallesPorRuteoId(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RuteoID", embarqueInfo.Ruteo.RuteoID},
                            {"@CitaCarga", embarqueInfo.CitaCarga.Date},
                            {"@HoraCarga", embarqueInfo.CitaCarga.Hour+Convert.ToDecimal((decimal)embarqueInfo.CitaCarga.Minute/60)}
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
        /// Método para obtener los parametros para obtener los tipos de embarque
        /// </summary>
        /// <returns> diccionarion con los parametros </returns>
        internal static Dictionary<string, object> ObtenerTiposEmbarque(EstatusEnum Activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
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
        ///  Obtiene parametros para actualizar la informacion del embarque con los datos de la programación
        /// </summary>
        /// <param name="info">info con la informacion del objeto a guardar</param>
        /// <returns>parametris para guardado de la programacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearProgramacionEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@CitaCarga", info.CitaCarga},
                                     {"@CitaDescarga", info.CitaDescarga},
                                     {"@Estatus", 1},
                                     {"@HorasTransito",	info.HorasTransito},                                    
                                     {"@Observacion",info.Observaciones == null ? string.Empty : info.Observaciones},
                                     {"@OrganizacionDestinoID", info.Ruteo.OrganizacionDestino.OrganizacionID},
                                     {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                     {"@OrganizacionOrigenID", info.Ruteo.OrganizacionOrigen.OrganizacionID},
                                     {"@ResponsableEmbarque",info.ResponsableEmbarque}, 
                                     {"@TipoEmbarqueID", info.TipoEmbarque.TipoEmbarqueID},
                                     {"@UsuarioCreacionID", info.UsuarioModificacionID }
                                    
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
        ///  Obtiene parametros para actualizar la informacion del embarque con los datos de la programación
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarProgramacionEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CitaCarga", info.CitaCarga},
                                     {"@CitaDescarga", info.CitaDescarga},
                                     {"@ResponsableEmbarque", info.ResponsableEmbarque},
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@HorasTransito",	info.HorasTransito},
                                     {"@OrganizacionDestinoID", info.Ruteo.OrganizacionDestino.OrganizacionID},
                                     {"@OrganizacionOrigenID", info.Ruteo.OrganizacionOrigen.OrganizacionID},
                                     {"@TipoEmbarqueID", info.TipoEmbarque.TipoEmbarqueID},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID }
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
        /// Método que obtiene los parametros necesarios para obtener las jaulas programadas.
        /// </summary>
        /// <param name="embarqueInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns> Parametros necesarios. </returns>
        internal static Dictionary<string, object> ObtenerJaulasProgramadas(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", embarqueInfo.Organizacion.OrganizacionID},
                            {"@FechaInicio", embarqueInfo.FechaInicio}
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
        ///  Obtiene parametros para actualizazr las observaciones de la programación
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarObservaciones(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@Observacion", info.Observaciones},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID}
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
        /// Obtiene parametros para actualizar el embarque 
        /// con la información de la programacion
        /// cuando el tipo de embarque es 'Ruteo' 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarProgramacionEmbarqueRuteo(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                XElement xml = new XElement("ListaRuteoDetalle", 
                info.EmbarqueRuteoDetalle.Select(x => 
                    new XElement("RuteoDetalle", 
                    new XElement("Horas", x.HorasString), 
                    new XElement("Kilometros", x.Kilometros), 
                    new XElement("FechaProgramada", x.Fecha),
                    new XElement("EmbarqueRuteoID", x.EmbarqueRuteoID)
                  )
                ));
                string listaRuteoDetalle = xml.ToString();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlRuteoDetalle", listaRuteoDetalle},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID}
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
        /// Método que obtiene parametros para obtener los detalles del embarque ruteo.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto contenedor de los parametros. </param>
        /// <returns> Lista con los parametros. </returns>
        internal static Dictionary<string, object> ObtenerDetallesEmbarqueRuteo(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EmbarqueID", embarqueInfo.EmbarqueID}
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
        /// Metodo para consultar la informacion si cuenta con folio embarque
        /// </summary>
        /// <param name="embarqueId">Información del embarque </param>
        /// <returns> Lista con los parametros. </returns>
        internal static Dictionary<string, object> ObtenerFolioEmbarque(int embarqueId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EmbarqueID", embarqueId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerProgramacionTransporte(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", embarqueInfo.Organizacion.OrganizacionID},
                            {"@EmbarqueID", embarqueInfo.EmbarqueID}
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
        ///  Obtiene parametros para actualizar la informacion del embarque con los datos de la programación
        /// </summary>
        /// <param name="info">info con la informacion del objeto a guardar</param>
        /// <returns>parametris para guardado de la programacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearTransporteEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CitaDescarga", info.CitaDescarga},
                                     {"@DobleTransportista", info.DobleTransportista},
                                     {"@EmbarqueID",info.EmbarqueID}, 
                                     {"@ImporteDemora",	info.Costos[2].Importe},
                                     {"@ImporteFlete", info.Costos[0].Importe},
                                     {"@ImporteGastoVariable", info.Costos[1].Importe},
                                     {"@Kilometros", info.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.Kilometros},
                                     {"@Observacion", info.Observaciones},
                                     {"@RutaID", info.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID},
                                     {"@Transportista", info.Proveedor.ProveedorID},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID }
                                    
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
        /// Metodo que elimina la informacion del embarque registrada en la seccion de datos
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEliminarInformacionDatos(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@EmbarqueID",info.EmbarqueID}, 
                    {"@UsuarioModificacionID", info.UsuarioModificacionID }
                                    
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerProgramacionDatos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", embarqueInfo.Organizacion.OrganizacionID},
                            {"@EmbarqueID", embarqueInfo.EmbarqueID}
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
        ///  Obtiene parametros para actualizar la informacion del embarque con los datos de la transoorte
        /// </summary>
        /// <param name="info">info con la informacion del objeto a guardar</param>
        /// <returns>parametris para guardado de la programacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarTransporteEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
               
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CitaDescarga", info.CitaDescarga},
                                     {"@DobleTransportista", info.DobleTransportista},
                                     {"@EmbarqueID",info.EmbarqueID}, 
                                     {"@ImporteDemora",	info.Costos[2].Importe},
                                     {"@ImporteFlete", info.Costos[0].Importe},
                                     {"@ImporteGastoVariable", info.Costos[1].Importe},
                                     {"@Kilometros", info.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.Kilometros},
                                     {"@Observacion", info.Observaciones},
                                     {"@RutaID", info.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID},
                                     {"@Transportista", info.Proveedor.ProveedorID},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID }
                                    
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
        ///  Obtiene parametros para guardar la informacion del embarque en la pestaña de datos
        /// </summary>
        /// <param name="info">info con la informacion del objeto a guardar</param>
        /// <returns>parametros para guardado de la programacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearDatosEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@ChoferID1", info.Operador1.ChoferID},
                                     {"@ChoferID2", info.Operador2.ChoferID}, 
                                     {"@PlacaJaula", info.Jaula.PlacaJaula},
                                     {"@PlacaCamion", info.Tracto.PlacaCamion},
                                     {"@Observacion", info.Observaciones},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID }
                                    
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
        ///  Obtiene parametros para actualizar la informacion del embarque en la pestaña de datos
        /// </summary>
        /// <param name="info">info con la informacion del objeto a guardar</param>
        /// <returns>parametros para guardado de la programacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarDatosEmbarque(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@ChoferID1", info.Operador1.ChoferID},
                                     {"@ChoferID2", info.Operador2.ChoferID}, 
                                     {"@PlacaJaula", info.Jaula.PlacaJaula},
                                     {"@PlacaCamion", info.Tracto.PlacaCamion},
                                     {"@Observacion", info.Observaciones},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID }
                                    
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
        /// Metodo para consultar el costo del flete
        /// </summary>
        /// <param name="embarqueTarifa">Información del embarque </param>
        /// <returns> Lista con los parametros. </returns>
        internal static Dictionary<string, object> ObtenerCostoFlete(EmbarqueTarifaInfo embarqueTarifa)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ConfiguracionEmbarqueDetalleID", embarqueTarifa.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID},
                            {"@ProveedorID", embarqueTarifa.Proveedor.ProveedorID},
                            {"@Activo", embarqueTarifa.Activo.GetHashCode()}
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
        /// Método que obtiene lo parametros para obtener los gastos fijos.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto contenedor de los parámetros </param>
        /// <returns> Objeto contenedor de los parametros. </returns>
        internal static Dictionary<string, object> ObtenerGastosFijos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ConfiguracionEmbarqueDetalleID", embarqueInfo.ConfiguracionEmbarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID},
                            {"@ProveedorID", embarqueInfo.Proveedor.ProveedorID},
                            {"@Activo", embarqueInfo.Activo}
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
        /// Obtiene parametros para guardar el embarque 
        /// con la información de la programacion
        /// cuando el tipo de embarque es 'Ruteo'
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearProgramacionEmbarqueTipoRuteo(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                RuteoDetalleInfo organizacionOrigen = info.Ruteo.RuteoDetalle.First();
                organizacionOrigen.Fecha = DateTime.Parse(organizacionOrigen.Fecha.ToShortDateString());
                info.Ruteo.RuteoDetalle[0] = organizacionOrigen;

                XElement xml = new XElement("ListaRuteoDetalle",
                    info.Ruteo.RuteoDetalle.Select(x =>
                        new XElement("RuteoDetalle",
                            new XElement("Horas", x.HorasString),
                            new XElement("Kilometros", x.Kilometros),
                            new XElement("OrganizacionID", x.OrganizacionOrigen.OrganizacionID),
                            new XElement("FechaProgramada",  x.Fecha.AddHours(Int32.Parse(x.HorasString.Length == 4 ? x.HorasString.Substring(0, 1) : x.HorasString.Substring(0, 2)))),
                            new XElement("RuteoID", x.Ruteo.RuteoID)
                        )
                    )
                );
                string listaRuteoDetalle = xml.ToString();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlRuteoDetalle", listaRuteoDetalle},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@CitaCarga", info.CitaCarga},
                                     {"@CitaDescarga", info.CitaDescarga},
                                     {"@Estatus", 1},                                    
                                     {"@HorasTransito",	info.HorasTransito},
                                     {"@Observacion", info.Observaciones == null ? string.Empty : info.Observaciones},
                                     {"@OrganizacionDestinoID", info.Ruteo.OrganizacionDestino.OrganizacionID},
                                     {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                     {"@OrganizacionOrigenID", info.Ruteo.OrganizacionOrigen.OrganizacionID},
                                     {"@Recibido", 1},
                                     {"@ResponsableEmbarque",info.ResponsableEmbarque},
                                     {"@TipoEmbarqueID", info.TipoEmbarque.TipoEmbarqueID},
                                     {"@UsuarioCreacionID", info.UsuarioModificacionID }
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
        ///  Obtiene parametros para actualizazr las observaciones de la programación
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearObservaciones(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@EmbarqueID", info.EmbarqueID},
                                     {"@Observacion", info.Observaciones},
                                     {"@UsuarioCreacionID", info.UsuarioModificacionID}

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
        /// Método que obtiene lo parametros para obtener la información a enviar en el correo.
        /// </summary>
        /// <param name="info"> Objeto contenedor de los parámetros </param>
        /// <returns> Parámetros encontrados </returns>
        internal static Dictionary<string, object> ObtenerInformacionCorreo(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", info.EmbarqueID}
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
        ///  Obtiene parametros para consultar el estatus del embarque
        /// </summary>
        /// <param name="info">info con la informacion del objeto para consultar</param>
        /// <returns>EmbarqueID</returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEmbarqueId(EmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", info.EmbarqueID}
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
