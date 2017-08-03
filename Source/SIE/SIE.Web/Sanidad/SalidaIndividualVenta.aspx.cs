using System;
using System.Linq;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace SIE.Web.Sanidad
{
    public partial class SalidaIndividualVenta : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
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

        private void LlenarcomboCausa()
        {
            try
            {
                int tipoMovimientoEnum = (int)TipoMovimiento.SalidaPorVenta;
                var causaSalidaPL = new CausaSalidaPL();
                List<CausaSalidaInfo> causaSalida = causaSalidaPL.ObtenerPorTipoMovimientoLista(tipoMovimientoEnum);
                if (causaSalida != null)
                {
                    cmbCausa.DataSource = causaSalida;
                    cmbCausa.DataTextField = "Descripcion";
                    cmbCausa.DataValueField = "CausaSalidaID";
                    cmbCausa.DataBind();

                    ListItem itemCombo = new ListItem();
                    itemCombo.Text = "Seleccione";
                    itemCombo.Value = Numeros.ValorCero.GetHashCode().ToString();
                    cmbCausa.Items.Insert(0, itemCombo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para llenar el combo de precios
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CausaPrecioInfo> LlenarcomboPrecio(int causa)
        {
            List<CausaPrecioInfo> causaPrecio = null;
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionID = 0;
                if (seguridad != null)
                {
                    organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                }
                var causaPrecioPL = new CausaPrecioPL();
                causaPrecio = causaPrecioPL.ObtenerPorPrecioPorCausaSalida(causa, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return causaPrecio;
        }

        /// <summary>
        /// Metodo para Consultar si existe el ticket
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static VentaGanadoInfo ValidarTicket(int ticket, int tipoVenta)
        {
            VentaGanadoInfo ventaGanado = null;
            TicketInfo ticketInfo = new TicketInfo();

            ticketInfo.FolioTicket = ticket;
            
            if (tipoVenta == TipoVentaEnum.Propio.GetHashCode())
            {
                ticketInfo.TipoVenta = TipoVentaEnum.Propio;
            }
            else
            {
                ticketInfo.TipoVenta = TipoVentaEnum.Externo;
            }
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionID = 0;
                if (seguridad != null)
                {
                    organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                }
                var ventaGanadoPL = new VentaGanadoPL();
                ticketInfo.Organizacion = organizacionID;
                ventaGanado = ventaGanadoPL.ObtenerVentaGanadoPorTicket(ticketInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return ventaGanado;
        }

        /// <summary>
        /// Metodo para Consultar el lote
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static LoteInfo ObtenerLote(string corralCodigo)
        {
            LoteInfo lote = null;

            try
            {
                CorralInfo corral = null;
                var lotePL = new LotePL();
                var corralPL = new CorralPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                
                if(corral != null)
                    lote = lotePL.ObtenerPorCorralCerrado(organizacionId, corral.CorralID);
                    //lote = lotePL.ObtenerPorCorral(organizacionId, corral.CorralID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return lote;
        }

        /// <summary>
        /// Metodo para Consultar la informacion de un corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerCorral(string corralCodigo, int TipoVenta)
        {
            CorralInfo corral;

            corral = null;
            var pl = new CorralPL();
            int retorno = 0;

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    corral = pl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);

                    if (corral != null)
                    {

                        if ((TipoVenta == TipoVentaEnum.Propio.GetHashCode() && corral.TipoCorral.TipoCorralID == (int)TipoCorral.CronicoVentaMuerte)
                           || (TipoVenta == TipoVentaEnum.Externo.GetHashCode() && (corral.TipoCorral.TipoCorralID == (int)TipoCorral.Maquila || corral.TipoCorral.TipoCorralID == (int)TipoCorral.Intensivo)))
                        {
                            retorno = 1;
                        }
                        else
                        {
                            retorno = 2;
                        }
                    }
                    else
                    {
                        retorno = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                retorno = 0;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo para obtener los aretes disponibles
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(string corralCodigo)
        {
            List<AnimalInfo> lista = null;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();

                CorralInfo corral = null;

                corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                corral.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };

                return animalPl.ObtenerAnimalesPorCorral(corral, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
            return lista;
        }

        /// <summary>
        /// Metodo para obtener si existe el arete, o es de un corral de recepcion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static AnimalInfo ObtenerExisteArete(string corralCodigo, string arete, string areteRFID)
        {
            AnimalInfo animalAgregado = new AnimalInfo();
          
            int retorno = 0;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                IList<AnimalInfo> animales = null;
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                string areteVentaDetalle = "";

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();

                CorralInfo corral = null;

                corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);

                areteVentaDetalle = animalPl.ObtenerExisteVentaDetalle(arete, areteRFID);

                if (areteVentaDetalle == "")
                {
                    if (corral != null)
                    {
                        corral.Organizacion = new OrganizacionInfo {OrganizacionID = organizacionId};


                        animales = animalPl.ObtenerAnimalesPorCorral(corral, organizacionId);

                        if (animales != null)
                        {
                            if (arete.Trim() != string.Empty && animales.Any(t => arete == t.Arete))
                            {
                                animalAgregado = (from animal in animales where animal.Arete == arete select animal).FirstOrDefault();
                                retorno = 1; //El arete es valido
                            }
                            else if (areteRFID.Trim() != string.Empty && animales.Any(t => areteRFID == t.AreteMetalico))
                            {
                                animalAgregado = (from animal in animales where animal.AreteMetalico == areteRFID select animal).FirstOrDefault();
                                retorno = 1; //El arete es valido
                            }
                            else
                            {
                                if (corral.GrupoCorral == (int)GrupoCorralEnum.Enfermeria ||
                                    corral.GrupoCorral == (int)GrupoCorralEnum.Produccion)
                                {
                                    /* Validar Si el arete existe en el inventario */
                                    var animal = animalPl.ObtenerAnimalPorArete(arete, organizacionId);
                                    if (animal != null && animal.CargaInicial)
                                    {
                                        animalAgregado = animal;
                                        retorno = 3; //El arete es valido
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    retorno = 2;//El arete ya tiene una salida
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            animalAgregado.TipoGanadoID = retorno;
            return animalAgregado;
        }

        /// <summary>
        /// Guarda los cambios en las tablas VentaGanado y VentaGanadoDetalle
        /// </summary>
        /// <param name="codigoCorral">Codigo de Corral</param>
        /// <param name="causaPrecioId">Codigo de Tarida</param>
        /// <param name="ventaGanado">Datos generales de la venta</param>
        /// <param name="ventaGanadoDetalle">Detalle de la venta(Aretes)</param>
        /// <param name="tipoVenta">Tipo Venta (Propio o Externo)</param>
        /// <param name="totalCabezas">Total de cabezas</param>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarVentaDetalle(string codigoCorral, int causaPrecioId, VentaGanadoInfo ventaGanado, List<VentaGanadoDetalleInfo> ventaGanadoDetalle, int tipoVenta, int totalCabezas)
        {
            int valorRetorno = 0;
            var datosVenta = new GrupoVentaGanadoInfo();
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                datosVenta.OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                datosVenta.UsuarioID = seguridad.Usuario.UsuarioID;
                datosVenta.TotalCabezas = totalCabezas;
                datosVenta.TipoVenta = (TipoVentaEnum)tipoVenta;
                datosVenta.CausaPrecioID = causaPrecioId;
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    var ventaGanadoDetallePl = new VentaGanadoDetallePL();
                    ventaGanado.UsuarioModificacionID = datosVenta.UsuarioID;
                    for (var i = 0; i < ventaGanadoDetalle.Count; i++)
                    {
                        ventaGanadoDetalle[i].FotoVenta = TipoFoto.Venta.ToString() + '/' + ventaGanadoDetalle[i].FotoVenta;
                    }
                    datosVenta.CodigoCorral = codigoCorral;
                    datosVenta.VentaGanado = ventaGanado;
                    datosVenta.VentaGandadoDetalle = ventaGanadoDetalle;
                    valorRetorno = ventaGanadoDetallePl.GuardarDetalle(datosVenta);
                }
            }
            catch (Exception ex)
            {
                valorRetorno = -1;
            }

            return valorRetorno;
        }
    }
}