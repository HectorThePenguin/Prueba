using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Services.Servicios.BL;
using SIE.Base.Vista;

namespace SIE.Web.Manejo
{
    public partial class ControlSacrificio : PageBase
    {
        static SeguridadInfo sesion = new SeguridadInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
         HttpContext.Current.Session["Seguridad"] = new SeguridadInfo()
            {
                Usuario = new UsuarioInfo()
                {
                    Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = 5,
                        Descripcion = "Parque AgroIndustrial SuKarne Durango"
                    },
                    OrganizacionID = 5
                }
            };

         var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
#else
            //var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
            var seguridad = (SeguridadInfo)ObtenerSeguridad();
            Session["ErrorCheckList"] = null;

            if (seguridad != null)
            {
                var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
            }
            else
            {
                Response.Redirect("../Seguridad/login.aspx", true);
            }
#endif

        }


        public static string Ruta(string ruta)
        {
            return VirtualPathUtility.ToAbsolute(ruta);
        }

        [WebMethod]
        public static Respuesta Transferencia(Peticion<Transferencia_Request> req)
        {
            var feedBack = new Respuesta { Resultado = false, Mensaje = string.Empty };

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                
                var transferenciaBO = new Services.Servicios.BL.TransferenciaGanadoIndividualBL();
                transferenciaBO.GuardarTransferenciaGanado(req.Datos.AnimalID, req.Datos.LoteID);

                var sincronizacion = new ControlSacrificioBL();
                var informacionSIAP = new List<ControlSacrificioInfo.SincronizacionSIAP>();
                informacionSIAP.Add(new ControlSacrificioInfo.SincronizacionSIAP
                {
                    AnimalID = req.Datos.AnimalID,
                    Corral = req.Datos.Corral
                });

                //sincronizacion.SyncResumenSacrificio_transferencia(informacionSIAP, seguridad.Usuario.UsuarioID, seguridad.Usuario.Organizacion.OrganizacionID);

                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                feedBack.Mensaje = ex.Message;
            }

            return feedBack;
        }

        /// <summary>
        /// Regresa una lista de aretes del lote especificado
        /// </summary>
        /// <param name="req">Recibe el LoteID</param>
        /// <returns></returns>
        [WebMethod]
        public static Respuesta<ObtenerAretes_Corral_Model[]> Obtener_Aretes_Corral(Peticion<int> req)
        {
            var feedBack = new Respuesta<ObtenerAretes_Corral_Model[]> {Resultado = false, Mensaje = string.Empty};
            try
            {
                var pl = new LotePL();
                var result = pl.ObtenerAretesCorralPorLoteId(req.Datos);
                if (result.Any())
                {
                    var listModel = new List<ObtenerAretes_Corral_Model>();
                    foreach (var animalInfo in result)
                    {
                        var model = new ObtenerAretes_Corral_Model
                                        {AnimalID = animalInfo.AnimalID, Arete = animalInfo.Arete};
                        listModel.Add(model);
                    }
                    feedBack.Resultado = true;
                    feedBack.Datos = listModel.ToArray();
                }
                else
                {
                    feedBack.Mensaje = String.Format("El lote no tiene ligado ningún arete, favor de verificar.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al obtener aretes para el lote especificado.");
            }
            return feedBack;
        }

        /// <summary>
        /// Regresa una lista de corrales con animales disponibles para transferir
        /// </summary>
        /// <param name="req">Recibe la OrganizacionID</param>
        /// <returns></returns>
        [WebMethod]
        public static Respuesta<Obtener_Corrales_Model[]> Obtener_Corrales(Peticion<int> req)
        {
            var feedBack = new Respuesta<Obtener_Corrales_Model[]> { Resultado = false, Mensaje = string.Empty };
            try
            {
                var pl = new LotePL();
                var result = pl.ObtenerLotesConAnimalesDisponiblesPorOrganizacionId(req.Datos);
                if (result.Any())
                {
                    var listModel = new List<Obtener_Corrales_Model>();
                    foreach (var lote in result)
                    {
                        var model = new Obtener_Corrales_Model { LoteID = lote.LoteID, Corral = lote.Corral.Codigo };
                        listModel.Add(model);
                    }
                    feedBack.Resultado = true;
                    feedBack.Datos = listModel.ToArray();
                }
                else
                {
                    feedBack.Mensaje = String.Format("No se encontraron corrales con animales disponible, favor de verificar.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al obtener aretes para el lote especificado.");
            }
            return feedBack;
        }

        [WebMethod]
        public static Respuesta Reactiva_Animal(Peticion<long> req)
        {
            var feedBack = new Respuesta { Mensaje = string.Empty, Resultado = false };
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var pl = new AnimalPL();
                var info = new AnimalInfo
                {
                    Activo = true,
                    AnimalID = req.Datos,
                    UsuarioModificacionID = seguridad.Usuario.UsuarioID
                };

                pl.InactivarAnimal(info);
                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al activar el animal.");
            }

            return feedBack;
        }

        [WebMethod]
        public static Respuesta Inactivar_Animal(Peticion<long> req)
        {
            var feedBack = new Respuesta{Mensaje = string.Empty, Resultado = false};
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var pl = new AnimalPL();
                var info = new AnimalInfo
                               {
                                   Activo = false,
                                   AnimalID = req.Datos,
                                   UsuarioModificacionID = seguridad.Usuario.UsuarioID
                               };

                pl.InactivarAnimal(info);
                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al inactivar el animal.");
            }

            return feedBack;
        }

        [WebMethod]
        public static Respuesta<List<ControlSacrificioInfo.SincronizacionSIAP>> Planchado_Arete(Peticion<ControlSacrificioInfo.Planchado_Arete_Request[]> req)
        {
            var feedBack = new Respuesta<List<ControlSacrificioInfo.SincronizacionSIAP>> { Mensaje = string.Empty, Resultado = false };
            try
            {
                var pl = new AnimalPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //pl.ActualizarArete(req.Datos.AnimalID,req.Datos.Arete,seguridad.Usuario.UsuarioID);
                feedBack.Datos = pl.PlancharAretes(req.Datos, seguridad.Usuario.UsuarioID, seguridad.Usuario.OrganizacionID);

                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al planchar arete."+ ex.Message);
                feedBack.Resultado = false;
            }

            return feedBack;
        }

        [WebMethod]
        public static Respuesta<List<ControlSacrificioInfo.SincronizacionSIAP>> Planchado_AreteLote(Peticion<ControlSacrificioInfo.Planchado_AreteLote_Request> req)
        {
            var feedBack = new Respuesta<List<ControlSacrificioInfo.SincronizacionSIAP>> { Mensaje = string.Empty, Resultado = false };
            try
            {
                var pl = new AnimalPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //pl.ActualizarArete(req.Datos.AnimalID,req.Datos.Arete,seguridad.Usuario.UsuarioID);
                feedBack.Datos = pl.PlancharAretes(req.Datos, seguridad.Usuario.UsuarioID, seguridad.Usuario.OrganizacionID);

                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al planchar arete." + ex.Message);
                feedBack.Resultado = false;
            }

            return feedBack;
        }
        

        [WebMethod]
        public static Respuesta<Reemplazo_Arete_Model> Reemplazo_Arete(Peticion<Reemplazo_Arete_Request> req)
        {
            var feedBack = new Respuesta<Reemplazo_Arete_Model> { Mensaje = string.Empty, Resultado = false};
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var fechaSacrificio = String.Format("{0:u}", req.Datos.FechaSacrificio);
                var pl = new LotePL();
                var animalPl = new AnimalPL();
                var loteInfo = pl.ObtenerPorId(req.Datos.LoteID);

                if(loteInfo != null)
                {
                    var aretesScp = pl.ObtenerLoteConAnimalesScp(seguridad.Usuario.OrganizacionID, loteInfo.Lote, loteInfo.Corral.Codigo, fechaSacrificio);
                    if (aretesScp.Any())
                    {
                        if(!pl.ExistenAretesRepetidos(aretesScp))
                        {
                            //var animalId = animalPl.PlancharAretes(aretesScp,req.Datos.AnimalID,req.Datos.LoteID,seguridad.Usuario.UsuarioID);
                            //feedBack.Datos.AnimalID = animalId;
                            feedBack.Resultado = true;
                        }
                        else
                        {
                            feedBack.Mensaje = string.Format("Existen aretes duplicados en el Control de Piso para el corral {0}, lote {1}, fecha de producción {2}.", loteInfo.Corral.Codigo, loteInfo.Lote, fechaSacrificio);
                        }
                    }
                    else
                    {
                        feedBack.Mensaje = string.Format("No se encontraron aretes en el Control de Piso para el corral {0}, lote {1}, fecha de producción {2}, por lo tanto no se puede realizar el reemplazo.", loteInfo.Corral.Codigo, loteInfo.Lote, fechaSacrificio);
                    }
                }
                else
                {
                    feedBack.Mensaje = string.Format("No se encontró el lote ni el corral para obtener los aretes del Control de Piso.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al reemplazar el arete.");
            }

            return feedBack;
        }

        [WebMethod]
        public static Respuesta Valida_Lote_Activo(Peticion<int> req)
        {
            var feedBack = new Respuesta {Mensaje = string.Empty, Resultado = false};
            try
            {
                var pl = new LotePL();
                var info = pl.ObtenerEstatusPorLoteId(req.Datos);
                if(info != null)
                {
                    if (info.Activo.GetHashCode() == 1)
                    {
                        feedBack.Resultado = true;
                    }    
                }
                else
                {
                    feedBack.Mensaje = String.Format("El lote no existe, favor de verificar.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al validar el estatus del lote.");
            }

            return feedBack;
        }

        [WebMethod]
        public static Respuesta Valida_Corral_Completo(Peticion<Valida_Corral_Completo_Request> req)
        {
            var feedBack = new Respuesta { Mensaje = string.Empty, Resultado = false };
            try
            {
                var pl = new LotePL();
                var loteInfo = pl.ValidarCorralCompletoParaSacrificio(req.Datos.LoteID);
                var fecha = String.Format("{0:u}", req.Datos.FechaProduccion);
                var cabezasScp = pl.ValidarCorralCompletoParaSacrificioScp(fecha.Substring(0, 10), loteInfo.Lote, loteInfo.Corral.Codigo, loteInfo.OrganizacionID);
                if (loteInfo.Cabezas == cabezasScp)
                {
                    feedBack.Resultado = true;
                }
                else
                {
                    feedBack.Mensaje = String.Format("Corral incompleto, existen diferencias en cabezas entre el SIAP y el SCP.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al validar si el corral esta completo.");
            }
            return feedBack;
        }

        [WebMethod]
        public static Respuesta Activar_Lote(Peticion<int> req)
        {
            var feedBack = new Respuesta { Mensaje = string.Empty, Resultado = false };
            try
            {
                var pl = new LotePL();
                var info = new LoteInfo {LoteID = req.Datos, Activo = EstatusEnum.Activo};
                pl.ActualizaActivoEnLote(info);
                feedBack.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al activar el lote.");
            }

            return feedBack;
        }

        public void GenerarSesion()
        {
            HttpContext.Current.Session["Seguridad"] = new SeguridadInfo()
            {
                Usuario = new UsuarioInfo()
                {
                    Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = 5,
                        Descripcion = "Parque AgroIndustrial SuKarne Durango"
                    },
                    OrganizacionID = 5
                }
            };
        }

        [WebMethod(EnableSession=true)]
        public static Respuesta<Obtener_Sacrificio_Model> Obtener_Sacrificio(Peticion<Obtener_Sacrificio_Request> req)
        {
            var feedBack = new Respuesta<Obtener_Sacrificio_Model> { Mensaje = string.Empty, Resultado = false, Datos = new Obtener_Sacrificio_Model() };

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var fechaSacrificio = req.Datos.FechaSacrificio.ToString("yyyy-MM-dd");
                var pl = new OrdenSacrificioPL();
                var resultScp = pl.ObtenerResumenSacrificioScp(fechaSacrificio, seguridad.Usuario.OrganizacionID);
                resultScp.ForEach(e => e.OrganizacionID = seguridad.Usuario.OrganizacionID);
                if (resultScp.Any())
                {
                    var resultSiap = pl.ObtenerResumenSacrificioSiap(resultScp);
                    feedBack.Datos.TotalCabezas = resultScp.Count;
                    feedBack.Datos.Organizacion = seguridad.Usuario.Organizacion.Descripcion;
                    feedBack.Datos.Sacrificio = resultSiap.ToArray();
                    feedBack.Resultado = true;
                }
                else
                {
                    feedBack.Mensaje = string.Format("No se encontraron registros para la fecha de sacrifico especificada {0}.", req.Datos.FechaSacrificio.ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                feedBack.Mensaje = string.Format("Ocurrió un error al obtener sacrificio.: " + ex.Message);
            }

            return feedBack;

        }
    }

    public class Peticion
    {
        public int UsuarioID { get; set; }
        public int OrganizacionID { get; set; }
        public string Token { get; set; }
    }

    public class Peticion<T> : Peticion
    {
        public T Datos { get; set; }
    }

    public class Respuesta
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
    }

    public class Respuesta<T> : Respuesta
    {
        public T Datos { get; set; }
    }

    public class Transferencia_Request
    {
        public int AnimalID { get; set; }
        public int LoteID { get; set; }
        public string Corral { get; set; }
    }

    //public class Planchado_Arete_Request
    //{
    //    public int AnimalID { get; set; }
    //    public string Arete { get; set; }
    //}

    public class Reemplazo_Arete_Request
    {
        public long AnimalID { get; set; }
        public int LoteID { get; set; }
        public DateTime FechaSacrificio { get; set; }
    }

    public class Obtener_Sacrificio_Request
    {
        public int OrganizacionID { get; set; }
        public DateTime FechaSacrificio { get; set; }
    }

    public class ObtenerAretes_Corral_Model
    {
        public string Arete { get; set; }
        public long AnimalID { get; set; }
    }

    public class Obtener_Corrales_Model
    {
        public string Corral { get; set; }
        public int LoteID { get; set; }
    }

    public class Reemplazo_Arete_Model
    {
        public long AnimalID { get; set; }
    }

    public class Obtener_Sacrificio_Model
    {
        public int TotalCabezas { get; set; }
        public string Organizacion { get; set; }
        public int Procesadas { get; set; }
        public ControlSacrificioInfo[] Sacrificio { get; set; }
    }

    public class Sacrificio_Model
    {

        //SIAP
        public string CodigoCorral { get; set; }
        public int LoteIdSiap { get; set; }
        
        //SCP
        public int Consecutivo { get; set; }
        public string Arete { get; set; }
        public DateTime FechaSacrificio { get; set; }
        public string NumeroCorral { get; set; }
        public string NumeroProceso { get; set; }
        public string TipoGanado { get; set; }
        public int LoteId { get; set; }
        public long AnimalId { get; set; }
        public string CorralInnova { get; set; }
        public string Po { get; set; }
        public bool Noqueo { get; set; }
        public bool PielSangre { get; set; }
        public bool PielDescarnada { get; set; }
        public bool Viscera { get; set; }
        public bool Inspeccion { get; set; }
        public bool CanalCompleta { get; set; }
        public bool CanalCaliente { get; set; }

    }

    public class Valida_Corral_Completo_Request
    {
        public int LoteID { get; set; }
        public DateTime FechaProduccion { get; set; }
    }
}