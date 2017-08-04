using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrystalDecisions.Shared.Json;
using SIE.Base.Log;
using System.Globalization;
using System.Web;
using System.Web.Services;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;

namespace SIE.Web.Recepcion
{
    public partial class ProgramacionEmbarque : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Valida que se cumplan las precondiciones
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ValidarPreCondiciones()
        {
            int result = 0;
            try
            {
                var precondicionesPL  = new TipoAutorizacionPL();
                result = precondicionesPL.ValidarPreCondiciones();
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene la lista de Organizaciones de Tipo Ganadera, Cadis y Descanso
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacion(int organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesTipoGanadera = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesTipoGanadera = (List<OrganizacionInfo>)organizacionesPl.ObtenerTipoGanaderasCadisDescanso();

                    if (organizacion > 0 && listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.OrganizacionID == organizacion).ToList();
                    }
                    else
                    {
                        listaOrganizacionesTipoGanadera = null;
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesTipoGanadera;
        }

        /// <summary>
        /// Obtiene la lista de Organizaciones Origen
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacionOrigen(int organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesOrigen = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesOrigen = (List<OrganizacionInfo>)organizacionesPl.ObtenerOrganizacionesOrigen();

                    if (organizacion > 0 && listaOrganizacionesOrigen != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesOrigen = listaOrganizacionesOrigen.Where(
                                registro => registro.OrganizacionID == organizacion).ToList();
                    }
                    else
                    {
                        listaOrganizacionesOrigen = null;
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesOrigen;
        }

        /// <summary>
        /// Obtiene la lista de Organizaciones de Tipo Ganadera, Cadis y Descanso
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacionesTipoGanaderaCadisDescanso(string organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesTipoGanadera = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesTipoGanadera = (List<OrganizacionInfo>)organizacionesPl.ObtenerTipoGanaderasCadisDescanso();

                    if (organizacion.Length > 0 && listaOrganizacionesTipoGanadera != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesTipoGanadera = listaOrganizacionesTipoGanadera.Where(
                                registro => registro.Descripcion.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(organizacion.ToString(CultureInfo.InvariantCulture).ToUpper())).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesTipoGanadera;
        }

        /// <summary>
        /// Obtiene la lista de Organizaciones Origen
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OrganizacionInfo> ObtenerOrganizacionOrigenPorDescripcion(string organizacion)
        {
            List<OrganizacionInfo> listaOrganizacionesOrigen = null;

            try
            {
                var organizacionesPl = new OrganizacionPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaOrganizacionesOrigen = (List<OrganizacionInfo>)organizacionesPl.ObtenerOrganizacionesOrigen();

                    if (organizacion.Length > 0 && listaOrganizacionesOrigen != null)
                    {
                        //Filtra todos las organizaciones que contengan lo capturado
                        listaOrganizacionesOrigen = listaOrganizacionesOrigen.Where(
                                registro => registro.Descripcion.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(organizacion.ToString(CultureInfo.InvariantCulture).ToUpper())).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaOrganizacionesOrigen;
        }

        /// <summary>
        /// Método web que obtiene los datos de las programaciones de embarque de acuerdo a la organizacion ingresada
        /// </summary>
        /// <param name="organizacionId"> ID de la organizacion para filtrar </param>
        /// <returns> Lista con los embarques encontradas </returns>
        [WebMethod]
        public static List<EmbarqueInfo> ObtenerProgramacionPorOrganizacionId(int organizacionId)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var programacionEmbarques = programacionEmbarquePL.ObtenerProgramacionPorOrganizacionId(organizacionId);
                if (programacionEmbarques == null)
                {
                    return null;
                }
                return programacionEmbarques;
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método web que obtiene los  tipos de embarque segun el tipo de estatus.
        /// </summary>
        /// <param name="organizacionId"> Estatus de los tipos de embarques.</param>
        /// <returns> Lista con los tipos de embarque encontradas </returns>
        [WebMethod]
        public static List<TipoEmbarqueInfo> ObtenerTiposEmbarque(EstatusEnum Activo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var programacionEmbarques = programacionEmbarquePL.ObtenerTiposEmbarque(Activo);
                if (programacionEmbarques == null)
                {
                    return null;
                }
                return programacionEmbarques;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método web que obtiene los ruteos asignados al origen y destino seleccionados.
        /// </summary>
        /// <param name="ruteoInfo"> Objeto que contiene las organizaciones origen y destino </param>
        /// <returns> Lista con los ruteos seleccionados </returns>
        [WebMethod]
        public static List<RuteoInfo> ObtenerRuteosPorOrigenyDestino(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<RuteoInfo> listaRuteos = programacionEmbarquePL.ObtenerRuteosPorOrigenyDestino(ruteoInfo);
                return listaRuteos;
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
        /// Método web que obtiene los detalles del ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto con la información de ruteo </param>
        /// <returns> Lista con los detalles del ruteo seleccionado </returns>
        [WebMethod]
        public static List<RuteoDetalleInfo> ObtenerRuteoDetallesPorRuteoID(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<RuteoDetalleInfo> listaDetalles = programacionEmbarquePL.ObtenerRuteoDetallePorRuteoID(embarqueInfo);
                return listaDetalles;
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
        /// Metodo para Consultar las jaulas solicitadas
        /// </summary>
        /// <param name="pedidoGanadoInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoGanadoInfo ObtenerJaulasSolicitadas(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                var pedidoGanadoPL = new PedidoGanadoPL();
                var pedidoGanado = pedidoGanadoPL.ObtenerPedidoSemanal(pedidoGanadoInfo);
                if (pedidoGanado == null)
                {
                    pedidoGanado = new PedidoGanadoInfo();
                }
                if (pedidoGanado.ListaSolicitudes == null)
                {
                    pedidoGanado.ListaSolicitudes = new List<PedidoGanadoEspejoInfo>();
                }
                return pedidoGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar dias habiles
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ParametroSemanaInfo ObtenerDiasHabiles()
        {
            try
            {
                var parametroSemanaInfo = new ParametroSemanaInfo();
                parametroSemanaInfo.Descripcion = "Pedido";
                var parametroSemanaPL = new ParametroSemanaPL();
                var parametroSemana = parametroSemanaPL.ObtenerParametroSemanaPorDescripcion(parametroSemanaInfo);
                if (parametroSemana == null)
                {
                    parametroSemana = new ParametroSemanaInfo();
                }
                return parametroSemana;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar las jaulas programadas
        /// </summary>
        /// <param name="embarqueInfo">filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns></returns>
        [WebMethod]
        public static List<EmbarqueInfo> ObtenerJaulasProgramadas(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var jaulasProgramadas = programacionEmbarquePL.ObtenerJaulasProgramadas(embarqueInfo);
                if (jaulasProgramadas == null)
                {
                    return null;
                }
                return jaulasProgramadas;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Metodo para Eliminar la informacion de la programacion del embarque 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void Eliminar(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                int usuarioId = seguridad.Usuario.UsuarioID;
                embarqueInfo.UsuarioModificacionID = usuarioId;
                programacionEmbarquePL.Eliminar(embarqueInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Guardar la informacion de la programacion del embarque 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EmbarqueInfo Guardar(EmbarqueInfo embarqueInfo, TipoGuardadoProgramacionEmbarqueEnum seccion)
        {
            try
            {
                var resultadoEmbarqueInfo = new EmbarqueInfo();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                int usuarioId = seguridad.Usuario.UsuarioID;
                embarqueInfo.CitaCarga = DateTime.Parse(embarqueInfo.CitaCarga.ToString());
                embarqueInfo.CitaDescarga = DateTime.Parse(embarqueInfo.CitaDescarga.ToString());
                                      
               if (embarqueInfo.UsuarioCreacionID == -1)
                {
                    // Edicion
                    embarqueInfo.UsuarioModificacionID = usuarioId;
                }
                else
                {
                    //Guardado
                    embarqueInfo.UsuarioModificacionID = usuarioId;
                    embarqueInfo.UsuarioCreacionID = usuarioId;    
                }
                
                if (seccion == TipoGuardadoProgramacionEmbarqueEnum.Datos)
                {
                    // Guardado para seccion de datos
                    // Regresa embarque con su folio de embaque registrado
                    resultadoEmbarqueInfo = programacionEmbarquePL.GuardarDatos(embarqueInfo);
                }
                else
                {
                    programacionEmbarquePL.Guardar(embarqueInfo, seccion);
                }
                
                return resultadoEmbarqueInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene la sessión  de la seguridad
        /// </summary>
        /// <returns></returns>
        public static object ObtenerSeguridad()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"];
                return seguridad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar el Numero de semana
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerNumeroSemana(DateTime fechaCalcular)
        {
            try
            {
                var parametroSemanaPL = new ParametroSemanaPL();
                var numeroSemana = parametroSemanaPL.ObtenerNumeroSemana(fechaCalcular);
                return numeroSemana;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene todos ruteos registrados y activos
        /// </summary>
        /// <returns> Lista con los ruteos obtenidos </returns>
        [WebMethod]
        public static List<RuteoInfo> ObtenerRuteosActivos()
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var ruteosActivos = programacionEmbarquePL.ObtenerRuteosActivos();
                return ruteosActivos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los detalles del embarque ruteo a editar.
        /// </summary>
        /// <param name="embarqueInfo"> Información del embarque seleccionado </param>
        /// <returns> Lista de los detalles del ruteo. </returns>
        [WebMethod]
        public static List<EmbarqueRuteoInfo> ObtenerDetallesEmbarqueRuteo(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<EmbarqueRuteoInfo> listaDetalles = programacionEmbarquePL.ObtenerDetallesEmbarqueRuteo(embarqueInfo);
                return listaDetalles;
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
        /// Metodo para consultar la informacion si cuenta con folio embarque 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EmbarqueInfo CuentaConFolioEmbarque(int embarqueId)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                EmbarqueInfo embarque = programacionEmbarquePL.CuentaConFolioEmbarque(embarqueId);

                return embarque;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar el flete 
        /// </summary>
        /// <returns></returns>
        
        [WebMethod]
        public static bool ObtenerFleteOrigenDestino(RuteoInfo ruteoInfo)
        {
            try
            {
                Logger.Info();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                bool existeTarifa = programacionEmbarquePL.ObtenerFleteOrigenDestino(ruteoInfo);
                return existeTarifa;
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
        /// Obtiene la lista de Proveedores Fletes
        /// </summary>
        /// <param name="codigoSAP"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorInfo> ObtenerProveedorDescripcion(string codigoSAP, int ConfiguracionEmbarqueDetalleID)
        {
            List<ProveedorInfo> listaProveedorDescripcion = null;

            try
            {
                var proveedorPL = new ProveedorPL();
                var activo = EstatusEnum.Activo;
                var tipoProveedor = TipoProveedorEnum.ProveedoresFletes;
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaProveedorDescripcion = (List<ProveedorInfo>)proveedorPL.ObtenerProveedoresPorRuta(activo.GetHashCode(), tipoProveedor.GetHashCode(), ConfiguracionEmbarqueDetalleID);

                    if (codigoSAP.Length > 0 && listaProveedorDescripcion != null)
                    {
                        //Filtra todos los proveedores que contengan lo capturado
                        listaProveedorDescripcion = listaProveedorDescripcion.Where(
                                registro => registro.CodigoSAP.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(codigoSAP.ToString(CultureInfo.InvariantCulture).ToUpper())).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaProveedorDescripcion;
        }

        /// <summary>
        /// Metodo para consultar si el proveedor cuenta con correo
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ProveedorInfo ObtenerPorIDConCorreo(int proveedorId)
        {
            try
            {
                var proveedorPL = new ProveedorPL();
                ProveedorInfo proveedor = proveedorPL.ObtenerPorIDConCorreo(proveedorId);
                return proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los datos de la pestaña de transporte.
        /// </summary>
        /// <param name="organizacionId"> Id de la organización principal. </param>
        /// <returns> Listado con los embarques obtenidos </returns>
        [WebMethod]
        public static List<EmbarqueInfo> ObtenerProgramacionTransporte(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var programacionEmbarques = programacionEmbarquePL.ObtenerProgramacionTransporte(embarqueInfo);
                if (programacionEmbarques == null)
                {
                    return null;
                }
                return programacionEmbarques;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar si el proveedor cuenta con chofer
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorChoferInfo> ObtenerProveedorChoferPorProveedorId(int proveedorId)
        {
            try
            {
                var proveedorChoferPL = new ProveedorChoferPL();
                List<ProveedorChoferInfo> proveedor = proveedorChoferPL.ObtenerProveedorChoferPorProveedorId(proveedorId);
                return proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar si el proveedor cuenta con jaula
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<JaulaInfo> ObtenerJaulaPorProveedorID(int proveedorId)
        {
            try
            {
                var jaulaPL = new JaulaPL();
                List<JaulaInfo> proveedor = jaulaPL.ObtenerPorProveedorID(proveedorId);
                return proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar si el proveedor cuenta con Tractos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CamionInfo> ObtenerTractoPorProveedorID(int proveedorId)
        {
            try
            {
                var camionPL = new CamionPL();
                List<CamionInfo> proveedor = camionPL.ObtenerPorProveedorID(proveedorId);
                return proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar si el proveedor cuenta con Origen-Destino Configurado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ProveedorInfo ObtenerProveedorConfiguradoOrigenDestino(EmbarqueDetalleInfo embarque)
        {
            try
            {
                var proveedorPL = new ProveedorPL();
                ProveedorInfo proveedor = proveedorPL.ObtenerProveedorConfiguradoOrigenDestino(embarque);
                return proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que elimina la informacion del embarque registrada en la seccion de datos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void EliminarInformacionDatos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                int usuarioId = seguridad.Usuario.UsuarioID;
                embarqueInfo.CitaCarga = DateTime.Parse(embarqueInfo.CitaCarga.ToString());
                embarqueInfo.CitaDescarga = DateTime.Parse(embarqueInfo.CitaDescarga.ToString());
                embarqueInfo.UsuarioCreacionID = usuarioId;
                embarqueInfo.UsuarioModificacionID = usuarioId;
                programacionEmbarquePL.EliminarInformacionDatos(embarqueInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los datos requeridos para la pestaña de datos.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto con la información necesaria para la consulta. </param>
        /// <returns> Listado de embarques encontrado </returns>
        [WebMethod]
        public static List<EmbarqueInfo> ObtenerProgramacionDatos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<EmbarqueInfo> programacionEmbarques = programacionEmbarquePL.ObtenerProgramacionDatos(embarqueInfo);
                return programacionEmbarques;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene la lista de las Rutas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorId(ConfiguracionEmbarqueInfo embarque)
        {
            List<ConfiguracionEmbarqueDetalleInfo> listaRutasPorId = null;

            try
            {
                var configuracionEmbarquePL = new ConfiguracionEmbarquePL();
                embarque.Activo = EstatusEnum.Activo;
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaRutasPorId = (List<ConfiguracionEmbarqueDetalleInfo>)configuracionEmbarquePL.ObtenerRutasPorId(embarque);

                    if (embarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID > 0 && listaRutasPorId != null)
                    {
                        //Filtra todas las rutas que contengan lo capturado
                        listaRutasPorId = listaRutasPorId.Where(
                                registro => registro.ConfiguracionEmbarqueDetalleID == embarque.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaRutasPorId;
        }

        /// <summary>
        /// Obtiene la lista de las Rutas
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorDescripcion(string descripcion, ConfiguracionEmbarqueInfo embarque)
        {
            List<ConfiguracionEmbarqueDetalleInfo> listaRutasDescripcion = null;

            try
            {
                var configuracionEmbarquePL = new ConfiguracionEmbarquePL();
                embarque.Activo = EstatusEnum.Activo;
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaRutasDescripcion = (List<ConfiguracionEmbarqueDetalleInfo>)configuracionEmbarquePL.ObtenerRutasPorDescripcion(embarque);

                    if (descripcion.Length > 0 && listaRutasDescripcion != null)
                    {
                        //Filtra todas las rutas que contengan lo capturado
                        listaRutasDescripcion = listaRutasDescripcion.Where(
                                registro => registro.Descripcion.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(descripcion.ToString(CultureInfo.InvariantCulture).ToUpper())).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaRutasDescripcion;
        }

        /// <summary>
        /// Método para enviar correos
        /// </summary>
        /// <param name="organizacionID">Identificador de la organizacion a la que se realiza la solicitud</param>
        [WebMethod]
        public static int EnviarCorreo(EmbarqueInfo embarqueInfo, int organizacionID)
        {

            var programacionEmbarquePL = new ProgramacionEmbarquePL();
            embarqueInfo.Organizacion = new OrganizacionInfo()
            {
                OrganizacionID = organizacionID
            };
            try
            {

                string formatoCorreo =
                    HttpContext.GetLocalResourceObject("~/Recepcion/ProgramacionEmbarque.aspx",
                        "formatoCorreoNuevo.Text").ToString();
                var correo = programacionEmbarquePL.EnviarCorreo(embarqueInfo, formatoCorreo);
                return correo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            
        }

        /// <summary>
        /// Obtiene la lista de Operadores 1
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorChoferInfo> ObtenerChofer(int proveedorId, int choferId)
        {
            List<ProveedorChoferInfo> listaChofer = null;
            try
            {
                
                var proveedorChoferPL = new ProveedorChoferPL();
                listaChofer = (List<ProveedorChoferInfo>)proveedorChoferPL.ObtenerProveedorChoferPorProveedorId(proveedorId);

                if (choferId > 0 && listaChofer != null)
                {
                    //Filtra todos los operadores que contengan lo capturado
                    listaChofer = listaChofer.Where(
                        registro => registro.Chofer.ChoferID == choferId 
                        && registro.Chofer.Activo == EstatusEnum.Activo).ToList();
                }
                else
                {
                    listaChofer = null;
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaChofer;
        }

        /// <summary>
        /// Obtiene la lista de Operadores 1
        /// </summary>
        /// <param name="codigoSAP"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorChoferInfo> ObtenerProveedorOperador(string nombre, int proveedorId)
        {
            List<ProveedorChoferInfo> listaProveedorDescripcion = null;

            try
            {
                var proveedorChoferPL = new ProveedorChoferPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    listaProveedorDescripcion = (List<ProveedorChoferInfo>)proveedorChoferPL.ObtenerProveedorChoferPorProveedorId(proveedorId);

                    if (nombre.Length > 0 || listaProveedorDescripcion != null)
                    {
                        //Filtra todos los proveedores que contengan lo capturado
                        listaProveedorDescripcion = listaProveedorDescripcion.Where(
                                registro => registro.Chofer.Nombre.ToString(CultureInfo.InvariantCulture).ToUpper()
                                        .Contains(nombre.ToString(CultureInfo.InvariantCulture).ToUpper()) 
                                        && registro.Chofer.Activo == EstatusEnum.Activo).ToList();
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaProveedorDescripcion;
        }

        /// <summary>
        /// Método que obtiene los tractos de acuerdo al proveedor seleccionado.
        /// </summary>
        /// <param name="camionInfo"> Objeto que contiene el proveedor seleccionado </param>
        /// <returns>Listado de los camiones encontrados </returns>
        [WebMethod]
        public static List<CamionInfo> ObtenerTractosPorProveedorID(CamionInfo camionInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<CamionInfo> listadoCamiones = programacionEmbarquePL.ObtenerTractosPorProveedorID(camionInfo);
                return listadoCamiones;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los tractos de acuerdo por placa.
        /// </summary>
        /// <param name="camionInfo"> Objeto de tipo camión contenedor de la placa a buscar </param>
        /// <returns></returns>
        [WebMethod]
        public static CamionInfo ObtenerTractosPorDescripcion(CamionInfo camionInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                CamionInfo camionResultado = programacionEmbarquePL.ObtenerTractosPorDescripcion(camionInfo);
                if (camionResultado != null && camionResultado.Proveedor.ProveedorID != camionInfo.Proveedor.ProveedorID)
                {
                    camionResultado = new CamionInfo();
                }
                return camionResultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene las jaulas de acuerdo al proveedor seleccionado.
        /// </summary>
        /// <param name="jaulaInfo"> Objeto que contiene el proveedor seleccionado </param>
        /// <returns> Listado de jaulas encontradas </returns>
        [WebMethod]
        public static List<JaulaInfo> ObtenerJaulasPorProveedorID(JaulaInfo jaulaInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                List<JaulaInfo> listadoJaulas = programacionEmbarquePL.ObtenerJaulasPorProveedorID(jaulaInfo);
                return listadoJaulas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene las jaulas de acuerdo por placa.
        /// </summary>
        /// <param name="jaulaInfo"> Objeto de tipo jaula contenedor de la placa a buscar </param>
        /// <returns></returns>
        [WebMethod]
        public static JaulaInfo ObtenerJaulasPorDescripcion(JaulaInfo jaulaInfo)
        {
            try
            {
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                JaulaInfo jaulaResultado = programacionEmbarquePL.ObtenerJaulasPorDescripcion(jaulaInfo);
                if (jaulaResultado != null && jaulaResultado.Proveedor.ProveedorID != jaulaInfo.Proveedor.ProveedorID)
                {
                    jaulaResultado = new JaulaInfo();
                }
                return jaulaResultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar el costo del flete
        /// </summary>
        /// <param name="embarqueTarifa"></param>
        /// <returns></returns>
        [WebMethod]
        public static EmbarqueTarifaInfo ObtenerCostoFlete(EmbarqueTarifaInfo embarqueTarifa)
        {
            try
            {
                embarqueTarifa.Activo = EstatusEnum.Activo;
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                EmbarqueTarifaInfo EmbarqueTarifa = programacionEmbarquePL.ObtenerCostoFlete(embarqueTarifa);
                return EmbarqueTarifa;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que obtiene los gastos fijos de acuerdo a la ruta seleccionada.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto de tipo embarque contenedor de la ruta seleccionada </param>
        /// <returns> Objeto que contiene la suma de los gastos fijos de acuerdo a la ruta seleccionada. </returns>
        [WebMethod]
        public static AdministracionDeGastosFijosInfo ObtenerGastosFijos(EmbarqueInfo embarqueInfo)
        {
            try
            {
                embarqueInfo.Activo = EstatusEnum.Activo;
                var programacionEmbarquePL = new ProgramacionEmbarquePL();
                AdministracionDeGastosFijosInfo gastosFijosRespuesta = programacionEmbarquePL.ObtenerGastosFijos(embarqueInfo);
                return gastosFijosRespuesta;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para obtener usuario.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RolInfo ObtenerRol()
        {
            var rol = new RolInfo();
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                int rolId = seguridad.Usuario.Operador.Rol.RolID;

                if (rolId == OperadorEmum.EmbarqueProgramacion.GetHashCode())
                {
                    rol.Descripcion = "EMBARQUE PROGRAMACION";
                }
                else if (rolId == OperadorEmum.EmbarqueTransporteDatos.GetHashCode())
                {
                    rol.Descripcion = "EMBARQUE TRANSPORTE Y DATOS";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return rol;
        }

        /// <summary>
        /// Metodo para Validar el estatus de la informacion de la programacion del embarque 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EmbarqueInfo ValidarEstatus(EmbarqueInfo embarqueInfo)
        {
            try
            {
                var resultadoEmbarqueInfo = new EmbarqueInfo();
                var programacionEmbarquePL = new ProgramacionEmbarquePL();

                resultadoEmbarqueInfo = programacionEmbarquePL.ValidarEstatus(embarqueInfo);

                return resultadoEmbarqueInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

    }
}
