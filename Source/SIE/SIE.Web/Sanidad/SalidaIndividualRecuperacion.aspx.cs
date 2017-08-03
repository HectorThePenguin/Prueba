using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class SalidaIndividualRecuperacion : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();

            int rolIdUsuario = 0;
            SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
                    rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;

                    LlenarcomboSalida();

                    LlenarcomboCausa();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }

                /* Se elimina esta validacion para dejarla por permisos de sistema 
                if (rolIdUsuario == (int)Roles.JefeSanidad || rolIdUsuario == (int)Roles.SupervisorSanidad)
                {
                    LlenarcomboSalida();

                    LlenarcomboCausa();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }*/
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
                int tipoMovimientoEnum = (int)TipoMovimiento.SalidaPorRecuperacion;
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
                int tipoMovimientoEnum = (int)TipoMovimiento.SalidaPorRecuperacion;
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
                var muertePL = new MuertePL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                if (animal != null)
                {
                    MuerteInfo muerto = muertePL.ObtenerMuertoPorArete(organizacionId, arete);
                    if (muerto != null)
                    {
                        animal.AnimalID = -1;
                    }
                }

                //ObtenerCorralDestinoAnimal(arete);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return animal;
        }

        /// <summary>
        /// Metodo para Consultar si existe el arete
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CorralInfo> ObtenerCorralDestinoAnimal(string arete)
        {
            var corrales = new List<CorralInfo>();

            try
            {
                //var animalPL = new AnimalPL();
                var animalMovimientoPL = new AnimalMovimientoPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                //animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                List<AnimalMovimientoInfo> movimientosArete =
                    animalMovimientoPL.ObtenerMovimientosPorArete(organizacionId, arete);

                var movimientoEnfermeria = new AnimalMovimientoInfo();
                var movimientoCorralOrigen = new AnimalMovimientoInfo();
                var parametroOrganizacionPL = new ParametroOrganizacionPL();
                var deteccionAnimalBL = new DeteccionAnimalBL();
                var corralPL = new CorralPL();
                double diasEnfermeria = 0;
                if (movimientosArete != null && movimientosArete.Any())
                {
                    movimientosArete = movimientosArete.OrderByDescending(mov => mov.AnimalMovimientoID).ToList();
                    foreach (var animalMovimientoInfo in movimientosArete)
                    {
                        if (animalMovimientoInfo.GrupoCorralID == GrupoCorralEnum.Enfermeria.GetHashCode())
                        {
                            var movimientosDiferenteEnfermeria =
                                movimientosArete.Any(
                                    mov => mov.GrupoCorralID != GrupoCorralEnum.Enfermeria.GetHashCode());

                            if (!movimientosDiferenteEnfermeria)
                            {
                                var primerMovimientoEnfermeria = movimientosArete.LastOrDefault();

                                if (primerMovimientoEnfermeria != null)
                                {
                                    movimientoEnfermeria = primerMovimientoEnfermeria;

                                    DeteccionAnimalInfo deteccionAnimal =
                                        deteccionAnimalBL.ObtenerPorAnimalMovimientoID(
                                            movimientoEnfermeria.AnimalMovimientoID);

                                    if (deteccionAnimal != null)
                                    {
                                        animalMovimientoInfo.GrupoCorralEnum = (GrupoCorralEnum)deteccionAnimal.GrupoCorralID;
                                        movimientoCorralOrigen = animalMovimientoInfo;
                                        TimeSpan diferencia = DateTime.Now - movimientoEnfermeria.FechaMovimiento;
                                        diasEnfermeria = diferencia.TotalDays;
                                        break;
                                    }
                                }
                            }
                            movimientoEnfermeria = animalMovimientoInfo;
                            continue;
                        }
                        if (animalMovimientoInfo.GrupoCorralID != GrupoCorralEnum.Enfermeria.GetHashCode())
                        {
                            //Validar que el corral no sea el de Sobrantes
                            ParametroOrganizacionInfo parametroOrganizacionSobrante =
                                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                    ParametrosEnum.CORRALSOBRANTE.ToString());

                            if (parametroOrganizacionSobrante != null &&
                                !string.IsNullOrWhiteSpace(parametroOrganizacionSobrante.Valor))
                            {
                                var corralSobrante = new CorralInfo
                                                     {
                                                         Codigo = parametroOrganizacionSobrante.Valor,
                                                         Organizacion = new OrganizacionInfo
                                                                        {
                                                                            OrganizacionID = organizacionId
                                                                        }
                                                     };

                                corralSobrante = corralPL.ObtenerPorCodicoOrganizacionCorral(corralSobrante);
                                var lotePL = new LotePL();
                                if (corralSobrante != null)
                                {
                                    LoteInfo loteOrigenSobrante = lotePL.ObtenerPorCorral(organizacionId,
                                        corralSobrante.CorralID);
                                    TimeSpan diferencia;
                                    if (loteOrigenSobrante != null)
                                    {
                                        if (loteOrigenSobrante.LoteID == animalMovimientoInfo.LoteID)
                                        {
                                            DeteccionAnimalInfo deteccionAnimal =
                                                deteccionAnimalBL.ObtenerPorAnimalMovimientoID(
                                                    movimientoEnfermeria.AnimalMovimientoID);

                                            if (deteccionAnimal != null)
                                            {
                                                animalMovimientoInfo.GrupoCorralEnum =
                                                    (GrupoCorralEnum)animalMovimientoInfo.GrupoCorralID;
                                                movimientoCorralOrigen = animalMovimientoInfo;
                                                movimientoCorralOrigen.LoteID = deteccionAnimal.Lote.LoteID;
                                                diferencia = DateTime.Now - movimientoEnfermeria.FechaMovimiento;
                                                diasEnfermeria = diferencia.TotalDays;
                                                break;
                                            }
                                        }
                                    }

                                    animalMovimientoInfo.GrupoCorralEnum =
                                        (GrupoCorralEnum)animalMovimientoInfo.GrupoCorralID;
                                    movimientoCorralOrigen = animalMovimientoInfo;
                                    diferencia = DateTime.Now - movimientoEnfermeria.FechaMovimiento;
                                    diasEnfermeria = diferencia.TotalDays;
                                    break;
                                }
                            }
                        }
                    }
                }
                int diasRecuperacion = 0;



                ParametroOrganizacionInfo parametroOrganizacion =
                    parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                                                                   ParametrosEnum.DiasRecuperacion.
                                                                                       ToString());

                if (parametroOrganizacion != null)
                {
                    int.TryParse(parametroOrganizacion.Valor, out diasRecuperacion);
                }

                if (diasRecuperacion == 0)
                {
                    diasRecuperacion = 7; //Valor por default, si no se encuentra el parámetro de la organización
                }



                if (movimientoCorralOrigen.TipoCorralID == TipoCorral.CorraletaRecuperado.GetHashCode()
                    || movimientoCorralOrigen.TipoCorralID == TipoCorral.CorraletaRecuperadosPartida.GetHashCode())
                {
                    var tipoCorralRecuperado = new TipoCorralInfo
                    {
                        TipoCorralID = TipoCorral.CorraletaRecuperado.GetHashCode()
                    };
                    List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperado, organizacionId);
                    if (corralesRecuperados != null && corralesRecuperados.Any())
                    {
                        corrales.AddRange(corralesRecuperados);
                        return corrales;
                    }
                }
                switch (movimientoCorralOrigen.GrupoCorralEnum)
                {
                    case GrupoCorralEnum.Produccion:
                        if (diasEnfermeria < diasRecuperacion)
                        {
                            var lotePL = new LotePL();
                            //var corralPL = new CorralPL();
                            LoteInfo loteOriginal = lotePL.ObtenerPorId(movimientoCorralOrigen.LoteID);
                            if (loteOriginal != null && loteOriginal.Activo == EstatusEnum.Activo)
                            {
                                corrales.Add(loteOriginal.Corral);
                            }
                            else
                            {
                                var tipoCorralRecuperado = new TipoCorralInfo
                                    {
                                        TipoCorralID = TipoCorral.CorraletaRecuperado.GetHashCode()
                                    };
                                List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperado, organizacionId);
                                if (corralesRecuperados != null && corralesRecuperados.Any())
                                {
                                    corrales.AddRange(corralesRecuperados);
                                }

                            }
                        }
                        else
                        {
                            //var corralPL = new CorralPL();
                            var tipoCorralRecuperado = new TipoCorralInfo
                            {
                                TipoCorralID = TipoCorral.CorraletaRecuperado.GetHashCode()
                            };
                            List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperado, organizacionId);
                            if (corralesRecuperados != null && corralesRecuperados.Any())
                            {
                                corrales.AddRange(corralesRecuperados);
                            }
                        }
                        break;
                    case GrupoCorralEnum.Recepcion:
                        if (diasEnfermeria < diasRecuperacion)
                        {
                            //var corralPL = new CorralPL();

                            var tipoCorralRecuperadoPartida = new TipoCorralInfo
                            {
                                TipoCorralID = TipoCorral.CorraletaRecuperadosPartida.GetHashCode()
                            };
                            List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperadoPartida, organizacionId);
                            if (corralesRecuperados != null && corralesRecuperados.Any())
                            {
                                corrales.AddRange(corralesRecuperados);
                            }
                        }
                        else
                        {
                            //var corralPL = new CorralPL();
                            var tipoCorralRecuperado = new TipoCorralInfo
                            {
                                TipoCorralID = TipoCorral.CorraletaRecuperado.GetHashCode()
                            };
                            List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperado, organizacionId);
                            if (corralesRecuperados != null && corralesRecuperados.Any())
                            {
                                corrales.AddRange(corralesRecuperados);
                            }
                        }
                        break;

                }

                if (!corrales.Any())
                {
                    var tipoCorralRecuperado = new TipoCorralInfo
                    {
                        TipoCorralID = TipoCorral.CorraletaRecuperado.GetHashCode()
                    };
                    List<CorralInfo> corralesRecuperados = corralPL.ObtenerCorralesPorTipoCorral(tipoCorralRecuperado, organizacionId);
                    if (corralesRecuperados != null && corralesRecuperados.Any())
                    {
                        corrales.AddRange(corralesRecuperados);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return corrales;
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
            int respuesta = 0;
            try
            {
                var animalPL = new AnimalPL();
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                animal = animalPL.ObtenerAnimalPorArete(arete, organizacionId);

                animalMovimiento = animalPL.ObtenerUltimoMovimientoAnimal(animal);

                if (animalMovimiento == null)
                {
                    //No existe en el inventario
                    return -1;
                }

                //int animalSalida = animalPL.ObtenerExisteSalida(animal.AnimalID);

                //if (animalSalida == 0)
                //{
                corral = corralPL.ObtenerPorId(animalMovimiento.CorralID);
                //if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.Enfermeria || corral.TipoCorral.TipoCorralID == (int)TipoCorral.CronicoRecuperacion)
                //{
                if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.Enfermeria ||
                    corral.TipoCorral.TipoCorralID == (int)TipoCorral.CronicoRecuperacion ||
                    corral.TipoCorral.TipoCorralID == (int)TipoCorral.CronicoVentaMuerte)
                {
                    if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.CronicoVentaMuerte)
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
                //}
                //else
                //{
                //    respuesta = 0;
                //}
                //}
                //else
                //{
                //    respuesta = 3;
                //}
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

        /////
        ///// Metodo para validar la corraleta
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public static int ValidarCorraleta(string arete, string corraleta)
        //{
        //    CorralInfo corral = null;
        //    int respuesta = 0;
        //    try
        //    {
        //        var corralPL = new CorralPL();
        //        var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
        //        int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

        //        corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corraleta);
        //        var animalPl = new AnimalPL();
        //        var animal = animalPl.ObtenerAnimalPorArete(arete, organizacionId);
        //        if (animal != null)
        //        {
        //            int dias = animalPl.ObtenerDiasUltimaDeteccion(animal);
        //            if (dias <= 7)
        //            {
        //                if (corral != null)
        //                {
        //                    respuesta = corral.TipoCorral.TipoCorralID == (int)TipoCorral.Produccion ? 1 : 2;
        //                }
        //            }
        //            else
        //            {
        //                if (corral != null)
        //                {
        //                    respuesta = corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaManejo ? 1 : 3;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }

        //    return respuesta;
        //}

        ///
        /// Metodo para guardar la salida individual de recuperacion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int Guardar(string arete, string codigoCorral, string codigoCorraleta, int tipoMovimiento)
        {
            int retorno = 0;

            try
            {
                var salidaIndividualPL = new SalidaIndividualPL();
                var corralPl = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                int usuario = seguridad.Usuario.UsuarioID;
                int operador = seguridad.Usuario.Operador.OperadorID;

                var corraleta = corralPl.ObtenerCorralPorCodigo(organizacionId, codigoCorraleta);

                retorno = salidaIndividualPL.Guardar(arete, organizacionId, codigoCorral, corraleta.CorralID,
                    usuario, (int)TipoMovimiento.SalidaPorRecuperacion, operador);
            }
            catch (Exception ex)
            {
                retorno = -1;
                Logger.Error(ex);
            }

            return retorno;
        }
    }
}