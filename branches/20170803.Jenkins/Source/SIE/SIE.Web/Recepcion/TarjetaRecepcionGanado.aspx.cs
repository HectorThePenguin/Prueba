using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Services.Servicios.BL;

namespace SIE.Web.Recepcion
{
    public partial class TarjetaRecepcionGanado : PageBase
    {
        public class TarjetaRecepcionGanadoInfo
        {
            public EntradaGanadoInfo EntradaGanado { set; get; }
            public OperadorInfo Operador { set; get; }
            public ChoferInfo Chofer { set; get; }
            public CorralInfo Corral { set; get; }
            public JaulaInfo Jaula { set; get; }
            public bool ConteoCapturado { set; get; }

        }

        /// <summary>
        /// Método para consultar el folio de entrada
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static TarjetaRecepcionGanadoInfo TraerEntradaGanado(EntradaGanadoInfo entradaGanadoInfo)
        {
            try
            {
                TarjetaRecepcionGanadoInfo result = null;
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                var entradaPl = new EntradaGanadoPL();
                var entradaGanadoCalidadPl = new EntradaGanadoCalidadPL();
                var operadorPL = new OperadorPL();
                var choferPL = new ChoferPL();

                var jaulaPL = new JaulaPL();

                int folioEntrada = entradaGanadoInfo.FolioEntrada;
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID; //entradaGanadoInfo.OrganizacionID;

                entradaGanadoInfo = entradaPl.ObtenerPorFolioEntrada(folioEntrada, organizacionId);
                
                if(entradaGanadoInfo != null)
                {
                    var entradaGanadoCalidad =
                        entradaGanadoCalidadPl.ObtenerEntradaGanadoId(entradaGanadoInfo.EntradaGanadoID);

                    if(entradaGanadoCalidad == null)
                    {
                        result = new TarjetaRecepcionGanadoInfo
                        {
                            ConteoCapturado = false
                        };
                        return result;
                    }
                    if (entradaGanadoCalidad.All(ent => ent.EntradaGanadoID == 0))
                    {
                        result = new TarjetaRecepcionGanadoInfo
                        {
                            ConteoCapturado = false
                        };
                        return result;
                    }

                    int embarqueId = entradaGanadoInfo.EmbarqueID;
                    var embarquePL = new EmbarquePL();
                    
                    var embarque = embarquePL.ObtenerPorID(embarqueId);

                    CorralInfo corral = TraerCorralPorEmbarque(embarque.FolioEmbarque);
                    
                    result = new TarjetaRecepcionGanadoInfo
                    {
                        EntradaGanado = entradaGanadoInfo,
                        Operador = operadorPL.ObtenerPorID(entradaGanadoInfo.OperadorID),
                        Chofer = choferPL.ObtenerPorID(entradaGanadoInfo.ChoferID),
                        Jaula = jaulaPL.ObtenerPorID(entradaGanadoInfo.JaulaID),
                        Corral = corral,
                        ConteoCapturado = true
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para consultar los corrales.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo TraerCorral(CorralInfo corralInfo, int embarqueId)
        {
            try
            {
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID; //corralInfo.Organizacion.OrganizacionID;

                var corralPL = new CorralPL();
                corralInfo.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };
                corralInfo.TipoCorral = new TipoCorralInfo { TipoCorralID = 1 }; //TipoCorral = 1 Recepción

                CorralInfo corral = corralPL.ObtenerPorCodigoCorral(corralInfo);

                if(corral != null)
                {
                    int corralId = corral.CorralID;
                    int lotesActivo  = TraerLoteObtenerActivosPorCorral(corralId);
                    if (lotesActivo > 0)
                    {
                        //Obtener programaciones por corral
                        var entradaGanadoPL = new EntradaGanadoPL();
                        int total = entradaGanadoPL.ObtenerPorCorralDisponible(organizacionId, corralId, embarqueId);
                        if(total > 0)
                        {
                            //si la condición se cumple el corral ya tiene un lote asignado
                            corral = new CorralInfo();
                        }
                    }
                }
                
                return corral;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para traer un corral por su Id
        /// </summary>
        /// <param name="corralId"></param>
        /// <returns></returns>
        [WebMethod]
        private static CorralInfo TraerCorralPorId(int corralId)
        {
            try
            {
                var corralPL = new CorralPL();
                CorralInfo corral = corralPL.ObtenerPorId(corralId);
                return corral;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para traer un corral por embarque y organización
        /// </summary>
        /// <param name="embarqueId"></param>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo TraerCorralPorEmbarque(int embarqueId)
        {
            try
            {
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var corralPL = new CorralPL();
                CorralInfo corral = corralPL.ObtenerPorEmbarqueRuteo(embarqueId, organizacionId);
                return corral;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para traer un corral por su organización y corralId
        /// </summary>
        /// <returns></returns>
        /// <param name="organizacionId"></param>
        /// <param name="corralID"></param>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        [WebMethod]
        public static LoteInfo ObtenerLoteCorralRuteo(int organizacionId, int corralID, int embarqueID)
        {
            try
            {
                //var seguridad = (SeguridadInfo) ObtenerSeguridad();
                //int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                //LoteInfo lote = lotePL.ObtenerPorCorral(organizacionId, corralID);

                var lotePL = new LotePL();
                LoteInfo lote = lotePL.ObtenerPorIdOrganizacionId(organizacionId,corralID, embarqueID);


                return lote;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para obtener si un corral tiene un lote activo
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        [WebMethod]
        public static int TraerLoteObtenerActivosPorCorral(int corralID)
        {
            try
            {
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID; 

                var lotePL = new LotePL();
                int activo = lotePL.ObtenerActivosPorCorral(organizacionId, corralID);
                return activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        
        /// <summary>
        /// Método para consultar los condiciones de ganado.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CondicionInfo> TraerCondiciones()
        {
            try
            {
                var condicionPL = new CondicionPL();
                var condiciones = condicionPL.ObtenerTodos(EstatusEnum.Activo);
                condiciones = condiciones ?? new List<CondicionInfo>();
                return condiciones.OrderBy(x => x.CondicionID).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        
        /// <summary>
        /// Genera un Objeto de Tipo Lote
        /// </summary>
        private static TipoOrganizacionInfo TraerTipoOrganizacion(int tipoOrganizacionId)
        {
            try
            {
                var tipoOrganizacionPl = new TipoOrganizacionPL();
                TipoOrganizacionInfo tipoOrganizacion = tipoOrganizacionPl.ObtenerPorID(tipoOrganizacionId);
                return tipoOrganizacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método para guardar los datos de la captura
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Guardar(EntradaGanadoInfo entradaGanadoInfo)
        {
            try
            {
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID; 

                int folioEntrada = entradaGanadoInfo.FolioEntrada;
                int corralId = entradaGanadoInfo.CorralID;
                int usuarioId = seguridad.Usuario.UsuarioID;

                var entradaGanadoPL = new EntradaGanadoPL();
                EntradaGanadoInfo infoGrabar = entradaGanadoPL.ObtenerPorFolioEntrada(folioEntrada, organizacionId);
                
                int embarqueId = infoGrabar.EmbarqueID;
                foreach (var condicion in entradaGanadoInfo.ListaCondicionGanado)
                {
                    condicion.UsuarioID = usuarioId;
                }

                infoGrabar.CabezasRecibidas = entradaGanadoInfo.CabezasRecibidas;
                infoGrabar.ListaCondicionGanado = entradaGanadoInfo.ListaCondicionGanado;
                infoGrabar.UsuarioModificacionID = usuarioId;
                infoGrabar.CorralID = entradaGanadoInfo.CorralID;
                infoGrabar.ManejoSinEstres = entradaGanadoInfo.ManejoSinEstres;

                LoteInfo loteInfo = ObtenerLoteCorralRuteo(organizacionId, corralId, embarqueId);

                if (loteInfo == null)
                {
                    loteInfo = infoGrabar.Lote;
                    TipoOrganizacionInfo tipoOrganizacion = TraerTipoOrganizacion(infoGrabar.TipoOrigen);

                    loteInfo.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                    loteInfo.OrganizacionID = organizacionId;
                    loteInfo.UsuarioCreacionID = usuarioId;
                    if (corralId > 0)
                    {
                        CorralInfo corral = TraerCorralPorId(corralId);
                        if (corral != null)
                        {
                            loteInfo.CorralID = corralId;
                            loteInfo.TipoCorralID = corral.TipoCorral.TipoCorralID;
                        }
                    }
                    loteInfo.Activo = EstatusEnum.Activo;
                    loteInfo.DisponibilidadManual = false;
                    loteInfo.Cabezas = infoGrabar.CabezasRecibidas;
                    loteInfo.CabezasInicio = infoGrabar.CabezasOrigen;
                }
                else
                {
                    //Obtener programaciones por corral
                    int total = entradaGanadoPL.ObtenerPorCorralDisponible(organizacionId, corralId, embarqueId);
                    if (total > 0)
                    {
                        return "ERROR";
                    }
                    loteInfo.Cabezas = infoGrabar.CabezasRecibidas;
                    loteInfo.CabezasInicio = infoGrabar.CabezasRecibidas;
                    loteInfo.UsuarioModificacionID = usuarioId;
                    infoGrabar.Lote = loteInfo;
                }
                infoGrabar.CondicionJaula = entradaGanadoInfo.CondicionJaula;

                entradaGanadoPL.GuardarEntradaCondicion(infoGrabar);

                return "OK";
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene un listado de condiciones jaula
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<CondicionJaulaInfo> ConcultarCondicionesJaula()
        {
            try
            {
                var condicionJaulaBL = new CondicionJaulaBL();
                var condicionesJaula = condicionJaulaBL.ObtenerTodos(EstatusEnum.Activo);
                return condicionesJaula;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
 