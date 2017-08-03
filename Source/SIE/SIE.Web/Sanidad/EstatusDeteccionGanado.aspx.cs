using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class EstatusDeteccionGanado : PageBase
    {
        private DataTable dtDetecciones;

        protected void Page_Load(object sender, EventArgs e)
        {
            int rolIdUsuario = 0, operadorId = 0;

            lblHoraInicio.Text = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            lblFecha.Text = DateTime.Now.ToShortDateString();

            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
                    rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;
                    operadorId = seguridad.Usuario.Operador.OperadorID;

                    lblSupervisor.Text = seguridad.Usuario.Nombre;

                    if (ConsultarEnfermerias(operadorId))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "LlenarConceptosDeteccion();", true);
                    }
                    else
                    {
                        var localResourceObject = GetLocalResourceObject("mensajeNoTieneEnfermeriasAsignadas");
                        if (localResourceObject != null)
                            EnviarMensajeUsuario(localResourceObject.ToString());
                    }
                }
                else
                {
                    var recursolocal = GetLocalResourceObject("mensajeDeRolSupervisor");
                    if (recursolocal != null)
                        EnviarMensajeUsuario(recursolocal.ToString());
                }

                /* Se elimina esta validacion para dejarla por permisos de sistema 
                if (rolIdUsuario == (int)Roles.SupervisorSanidad)
                {
                    lblSupervisor.Text = seguridad.Usuario.Nombre;

                    if (ConsultarEnfermerias(operadorId))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "LlenarConceptosDeteccion();", true);
                    }
                    else
                    {
                        var localResourceObject = GetLocalResourceObject("mensajeNoTieneEnfermeriasAsignadas");
                        if (localResourceObject != null)
                            EnviarMensajeUsuario(localResourceObject.ToString());
                    }
                }
                else
                {
                    var recursolocal = GetLocalResourceObject("mensajeDeRolSupervisor");
                    if (recursolocal != null)
                        EnviarMensajeUsuario(recursolocal.ToString());
                }*/
            }
            else
            {
                var localResourceObject = GetLocalResourceObject("mensajeErrorSession");
                if (localResourceObject != null)
                    EnviarMensajeUsuarioErrorSession(localResourceObject.ToString());
            }
        }
        /// <summary>
        /// Metodo que consulta las enfermerias
        /// </summary>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        private bool ConsultarEnfermerias(int operadorId)
        {            
            bool tieneEnfermerias = false;

            var enfermeriaPl = new EnfermeriaPL();
            List<EnfermeriaInfo> result = enfermeriaPl.ObtenerEnfermeriasPorOperadorId(operadorId);

            StringBuilder enfermerias;
            if (result != null && result.Count > 0)
            {
                tieneEnfermerias = true;
                cmbEnfermeria.DataSource = result;
                
                cmbEnfermeria.DataTextField = "Descripcion";
                cmbEnfermeria.DataValueField = "EnfermeriaID";
                cmbEnfermeria.DataBind();
                cmbEnfermeria.SelectedIndex = -1;
                
                string[] descripciones;
                var descripcionEnfermerias = new List<string>();
                foreach (EnfermeriaInfo enfermeria in result)
                {
                    enfermerias = new StringBuilder();

                    descripciones = enfermeria.Descripcion.Split(' ');
                    if (descripciones[0].Length > 3)
                    {
                        enfermerias.Append(descripciones[0].Substring(0, 3));
                    }
                    else
                    {
                        enfermerias.Append(descripciones[0]);
                    }
                    if (descripciones.Length > 1)
                    {
                        enfermerias.Append(" ").Append(descripciones[1]);
                    }
                    descripcionEnfermerias.Add(enfermerias.ToString());
                }
                lblEnfermeria.Text = string.Join(",", descripcionEnfermerias.ToArray());
            }

            return tieneEnfermerias;
        }

        /// <summary>
        /// ValidaEsAnimalDeCargaInicial 
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static AnimalInfo ValidaEsAnimalDeCargaInicial(AnimalInfo animalInfo)
        {
            AnimalInfo animal = null;
            try{
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var animalPl = new AnimalPL();

                /* Validar Si el arete existe en el inventario */
                animal = animalPl.ObtenerAnimalPorArete(animalInfo.Arete, organizacionId);
                if (!(animal != null && animal.CargaInicial))
                {
                    animal = null;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //throw;
            }
            return animal;
        }

        /// <summary>
        /// Metodo web que consulta los datos del corral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="enfermeria"></param>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ObtenerDatosDelCorral(CorralInfo corralInfo, int enfermeria)
        {
            try{
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var corralPL = new CorralPL();
                var operadorPL = new OperadorPL();
                
                var corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corralInfo.Codigo);

                if (corral != null)
                {
                    if (corralPL.ValidarCorralPorEnfermeria(corral.Codigo, enfermeria, organizacionId))
                    {
                        corral.Operador = operadorPL.ObtenerPorCodigoCorral(corral.Codigo, organizacionId);
                        corral.perteneceAEnfermeria = true;
                    }
                    else
                    {
                        corral = new CorralInfo();
                        corral.perteneceAEnfermeria = false;
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

        private void EnviarMensajeUsuario(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario('" + mensaje + "');", true);
        }

        private void EnviarMensajeUsuarioErrorSession(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuarioErrorSession('" + mensaje + "');", true);
        }

        /// <summary>
        /// Metodo para obtener todos los animales que tiene el corral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(CorralInfo corralInfo)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                bool validarSalida = true;

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();
                var entrada = new EntradaGanadoPL();

                List<AnimalInfo> listaanimales = null;

                var corralIn = corralPl.ObtenerCorralPorCodigo(organizacionId, corralInfo.Codigo);

                if (corralIn != null)
                {
                    var lotePl = new LotePL();
                    var lote = lotePl.ObtenerLotesActivos(organizacionId, corralIn.CorralID);

                    if (lote != null)
                    {
                        if (corralIn.GrupoCorral == (int)GrupoCorralEnum.Enfermeria ||
                            corralIn.GrupoCorral == (int)GrupoCorralEnum.Produccion)
                        {
                            listaanimales = animalPl.ObtenerAnimalesPorCorral(corralInfo, organizacionId);
                        }
                        else if (corralIn.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                        {
                            var datosEntrada = entrada.ObtenerEntradaPorLote(lote);

                            if (datosEntrada != null)
                            {
                                if (datosEntrada.TipoOrganizacionOrigenId == (int) TipoOrganizacion.CompraDirecta)
                                {
                                    validarSalida = false;
                                }
                            }

                            AnimalInfo animal;

                            if (validarSalida)
                            {
                                listaanimales = new List<AnimalInfo>();
                                var interfaz = new InterfaceSalidaAnimalPL();

                                var anim = interfaz.ObtenerAretesInterfazSalidaAnimal(corralInfo.Codigo, organizacionId);
                                if (anim != null)
                                {
                                    foreach (InterfaceSalidaAnimalInfo interfaceAnimal in anim)
                                    {
                                        animal = new AnimalInfo();
                                        animal.Arete = interfaceAnimal.Arete;
                                        listaanimales.Add(animal);
                                    }
                                }
                                else
                                {
                                    animal = new AnimalInfo {AnimalID = -1};
                                    listaanimales.Add(animal);
                                }
                            }
                        }
                    }
                }
                return listaanimales;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que obtiene el listado de los conceptos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ConceptoInfo> ObtenerListadoDeConceptos()
        {
            try
            {
                var conceptoPl = new ConceptoPL();
                return conceptoPl.ObtenerListadoDeConceptos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para obtener la hora del servidor.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string ObtenerHoraServidor()
        {
            return DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Metodo para guardar los aretes detectados
        /// </summary>
        /// <param name="supervisionParam"></param>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarEstatusDeteccion(List<SupervisionGanadoInfo> supervisionParam)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var supervisionPl = new SupervisionGanadoPL();
                return supervisionPl.GuardarEstatusDeteccion(supervisionParam, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que valida si los aretes ya fueron detectados en el dia
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="areteTestigo"></param>
        /// <returns></returns>
        [WebMethod]
        public static int ValidarAretesDetectado(string arete, string areteTestigo)
        {
            try
            {
                if (arete == "" && areteTestigo == "")
                {
                    return 0;
                }
                else
                {
                    var supervisionPl = new SupervisionGanadoPL();
                    return supervisionPl.ValidarAreteDetectado(arete, areteTestigo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}