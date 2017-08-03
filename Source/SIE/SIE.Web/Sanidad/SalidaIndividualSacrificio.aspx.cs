using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace SIE.Web.Sanidad
{
    public partial class SalidaIndividualSacrificio : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
                    LlenarcomboSalida();
                    LlenarcomboCausa();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
            }
        }
        private void LlenarcomboSalida()
        {
            try
            {
                int tipoMovimientoEnum = (int)TipoMovimiento.SalidaPorSacrificio;
                var tipoMovimiento = new TipoMovimientoPL();
                TipoMovimientoInfo tipoMovimientoInfo = tipoMovimiento.ObtenerSoloTipoMovimiento(tipoMovimientoEnum);
                if (tipoMovimientoInfo != null)
                {
                    ListItem itemCombo = new ListItem();
                    itemCombo.Text = tipoMovimientoInfo.Descripcion;
                    itemCombo.Value = tipoMovimientoInfo.TipoMovimientoID.ToString("N");
                    cmbSalida.Items.Insert(0, itemCombo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LlenarcomboCausa()
        {
            try
            {
                int tipoMovimientoEnum = (int)TipoMovimiento.SalidaPorSacrificio;
                var tipoMovimiento = new CausaSalidaPL();
                CausaSalidaInfo tipoMovimientoInfo = tipoMovimiento.ObtenerPorTipoMovimiento(tipoMovimientoEnum);
                if (tipoMovimientoInfo != null)
                {
                    ListItem itemCombo = new ListItem();
                    itemCombo.Text = tipoMovimientoInfo.Descripcion;
                    itemCombo.Value = tipoMovimientoInfo.CausaSalidaID.ToString("N");
                    cmbCausa.Items.Insert(0, itemCombo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar si existe el arete
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static AnimalInfo ObtenerExisteArete(string arete)
        {
            AnimalInfo animal = null;

            try
            {
                var animalPL = new AnimalPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                int pesoProyectado = 0;
                animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);
                
                pesoProyectado = animalPL.ObtenerPesoProyectado(arete,organizacionId);

                animal.PesoAlCorte = pesoProyectado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return animal;
        }

        /// <summary>
        /// Metodo para Consultar las corraletas de sacrificio configuradas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoInfo<CorralInfo> ExisteCorraletaDeSacrificioConfigurada(string arete)
        {
            ResultadoInfo<CorralInfo> corraletas = null;

            try
            {
                var animalPL = new AnimalPL();
                var tipoGanadoPL = new TipoGanadoPL();
                var parametroOrg = new ParametroOrganizacionPL();
                var corralPL = new CorralPL();

                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                // Se obtiene el animal
                var animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                if (animal != null)
                {
                    // Obtener el tipo de ganado
                    animal.TipoGanado = tipoGanadoPL.ObtenerPorID(animal.TipoGanadoID);
                    // Se obtiene el tipo de corraletaSacrificio a Buscar
                    var paramTipoCorraletaSacrificio = animal.TipoGanado.Sexo == Sexo.Macho ? 
                        ParametrosEnum.CorraletaSacrificioMacho : ParametrosEnum.CorraletaSacrificioHembra;
                    var parametroOrganizacionInfo = 
                        parametroOrg.ObtenerPorOrganizacionIDClaveParametro(organizacionId, paramTipoCorraletaSacrificio.ToString());
                    // Si se tiene configurado el parametro
                    if (parametroOrganizacionInfo != null)
                    {
                        //Obtener info de las corraletas configuradas
                        corraletas =
                            corralPL.ObtenerInformacionCorraletasDisponiblesSacrificio(organizacionId, parametroOrganizacionInfo.Valor);
                        if (corraletas != null && corraletas.Lista.Count>0)
                        {
                            var corral = new CorralInfo {Codigo = "Seleccione", CorralID = 0};
                            corraletas.Lista.Insert(0,corral);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return corraletas;
        }

        /// <summary>
        /// Metodo para obtener el ultimo movimiento
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerUltimoMovimiento(string arete)
        {
            AnimalInfo animal = null;
            AnimalMovimientoInfo animalMovimiento = null;
            CorralInfo corral = null;
            List<TratamientoInfo> tratamientos = null;

            int respuesta = 0;
            try
            {
                var animalPL = new AnimalPL();
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                animalMovimiento = animalPL.ObtenerUltimoMovimientoAnimal(animal);

                int animalSalida = animalPL.ObtenerExisteSalida(animal.AnimalID);

                var tratamiento = new CorteTransferenciaGanadoPL();

                tratamientos = tratamiento.ObtenerTratamientosAplicados(animal, -1);
                if (tratamientos != null)
                {
                    tratamientos =
                        tratamientos.Where(
                            tipo =>
                            tipo.TipoTratamientoInfo.TipoTratamientoID == TipoTratamiento.Enfermeria.GetHashCode() ||
                            tipo.TipoTratamientoInfo.TipoTratamientoID ==
                            TipoTratamiento.EnfermeriaAlCorte.GetHashCode()).
                            ToList();
                    for (var i = 0; i < tratamientos.Count; i++)
                    {
                        TimeSpan dias = DateTime.Now - tratamientos[i].FechaAplicacion;
                        if (dias.TotalDays < 30)
                        {
                            respuesta = 4; //No han pasado los treinta dias despues de ultimo tratamiento
                        }
                    }
                }

                if (respuesta == 0)
                {
                    if (animalSalida == 0)
                    {
                        //if (animalMovimiento.TipoMovimientoID == (int) TipoMovimiento.EntradaEnfermeria)
                        //{
                            corral = corralPL.ObtenerPorId(animalMovimiento.CorralID);
                            if (corral.TipoCorral.TipoCorralID == (int) TipoCorral.Enfermeria ||
                                corral.TipoCorral.TipoCorralID == (int) TipoCorral.CronicoRecuperacion ||
                                corral.TipoCorral.TipoCorralID == (int) TipoCorral.CronicoVentaMuerte)
                            {
                                if (corral.TipoCorral.TipoCorralID != (int) TipoCorral.CronicoVentaMuerte)
                                {
                                    respuesta = 1; //El arete es valido
                                }
                                else
                                {
                                    respuesta = 2; //El arete es de tipo cronico
                                }
                            }
                            else
                            {
                                respuesta = 0; //El arete no se encuentra en enfermeria
                            }
                        //}
                        //else
                        //{
                        //    respuesta = 0; //El arete no se encuentra en enfermeria
                        //}
                    }
                    else
                    {
                        respuesta = 3; //El arete ya tiene una salida
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo para obtener el corral actual del animal
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ObtenerCorral(string arete)
        {
            AnimalInfo animal = null;
            AnimalMovimientoInfo animalMovimiento = null;
            CorralInfo corral = null;
            int respuesta = 0;
            try
            {
                var animalPL = new AnimalPL();
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                animalMovimiento = animalPL.ObtenerUltimoMovimientoAnimal(animal);

                corral = corralPL.ObtenerPorId(animalMovimiento.CorralID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return corral;
        }

        ///
        /// Metodo para validar la corraleta
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ValidarCorraleta(string corraleta)
        {
            CorralInfo corral = null;
            int respuesta = 0;
            try
            {
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corraleta);

                if (corral != null)
                {
                    if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                    {
                        respuesta = 1;
                    }
                    else
                    {
                        respuesta = 2;
                    }
                }
                else
                {
                    respuesta = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return respuesta;
        }

        ///
        /// Metodo para validar la capacidad de la corraleta
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ValidarCapacidadCorraleta(int corraletaID)
        {
            CorralInfo corral = null;
            int respuesta = 0;
            try
            {
                var corralPL = new CorralPL();
                var lotePL = new LotePL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                corral = corralPL.ObtenerPorId(corraletaID);

                if (corral != null)
                {                    
                    var lote = lotePL.ObtenerPorCorralID(new LoteInfo{CorralID = corral.CorralID, OrganizacionID = organizacionId});
                    if (lote!=null)
                    {
                        if (corral.Capacidad > lote.Cabezas)
                        {
                            respuesta = 1; // Corral OK
                        }
                        else
                        {
                            respuesta = 2; // Corral No tiene capacidad
                        }
                    }
                    else
                    {
                        respuesta = 1; // Corral OK   ----->>>   respuesta = 3;// no tiene lote activo
                    }
                }
                else
                {
                    respuesta = 0; // Corral no existe
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return respuesta;
        }

        ///
        /// Metodo para obtener la corraleta
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoInfo<CorralInfo> ObtenerCorraleta()
        {
            ResultadoInfo<CorralInfo> corral = null;
            CorralInfo corralDatos = null;
            int respuesta = 0;
            try
            {
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                corralDatos = new CorralInfo();
                corralDatos.Organizacion = new OrganizacionInfo();
                corralDatos.Organizacion.OrganizacionID = organizacionId;
                corralDatos.GrupoCorral = (int)GrupoCorralEnum.Corraleta;

                corral = corralPL.ObtenerCorralesPorTipoCorraletaSacrificio(corralDatos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return corral;
        }

        ///
        /// Metodo para guardar la salida individual de sacrificio
        /// 
        /// <returns></returns>
        [WebMethod]
        public static int Guardar(string arete, string codigoCorral, int corraletaID, int tipoMovimiento)
        {
            int retorno = 0;

            try
            {
                var salidaIndividualPL = new SalidaIndividualPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                int usuario = seguridad.Usuario.UsuarioID;
                int operador = seguridad.Usuario.Operador.OperadorID;

                ////Se cambia el Movimiento, para que no genere el de Salida Por Sacrificio
                //if (tipoMovimiento == TipoMovimiento.SalidaPorSacrificio.GetHashCode())
                //{
                    tipoMovimiento = TipoMovimiento.TraspasoDeGanado.GetHashCode();
                //}

                retorno = salidaIndividualPL.Guardar(arete, organizacionId, codigoCorral, corraletaID,
                    usuario, tipoMovimiento, operador);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }
    }
}