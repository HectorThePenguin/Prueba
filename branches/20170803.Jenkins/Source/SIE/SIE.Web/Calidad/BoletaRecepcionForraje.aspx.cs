using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System.Globalization;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Web.Configuration;

namespace SIE.Web.Calidad 
{
    public partial class BoletaRecepcionForraje : PageBase
    {
        List<ContratoInfo> contratos = null;
        decimal porcentajePromedioMin;
        decimal porcentajePromedioMax;
        int calidadOrigen = -1;
        /// <summary>
        /// Inicializa los componentes de la pagina una vez que se carga.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            EstableceDatosAnalista();
            LlenaComboDestino();
            txtAutorizado.Value = Convert.ToString(Estatus.Autorizado.GetHashCode());
            txtAprobado.Value = Convert.ToString(Estatus.Aprobado.GetHashCode());
            txtRechazado.Value = Convert.ToString(Estatus.Rechazado.GetHashCode());
            txtPendienteAutorizar.Value = Convert.ToString(Estatus.PendienteAutorizar.GetHashCode());
            if (txtCalidadOrigenID.Text != "")
            {
                validarCalidadOrigen(Convert.ToInt32(txtCalidadOrigenID.Text));
            }
            else
            {
                txtFolioOrigen.Enabled = false;
                txtFechaOrigen.Enabled = false;
                txtHumedadOrigen.Enabled = false;
            }
        }

        /// <summary>
        /// Ayuda a ejecutar el metodo que valida el ticket desde una 
        /// funcion javascript
        /// </summary>
        /// <param name="arg"></param>
        public void RaisePostBackEvent(string arg)
        {
            if (txtTicket.ID == arg)
            {
                txtTicket_TextChanged(txtTicket, null);
            }
        }

        /// <summary>
        /// Ejecuta la funcion que evaluara si el ticket capturado es valido.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtTicket_TextChanged(object sender, EventArgs e)
        {
            ValidaFolio();
        }

        //Metodos para establecer los datos en pantalla
        /// <summary>
        /// Funcion que llena el combo de destino
        /// </summary>
        private void LlenaComboDestino()
        {
            try
            {
                var tipoContratoPl = new TipoContratoPL();
                var listaTipoContrato = tipoContratoPl.ObtenerTodos();
                cmbDestino.DataSource = listaTipoContrato;
                cmbDestino.DataTextField = "Descripcion";
                cmbDestino.DataValueField = "TipoContratoId";
                cmbDestino.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Funcion que llena el combo de los forrajes
        /// </summary>
        /// <param name="registroVigilancia"></param>
        /// <returns></returns>
        private bool LlenaComboForrajes(RegistroVigilanciaInfo registroVigilancia, int tipoflete)
        {
            List<ProductoInfo> forrajes = null;

            forrajes = ObtenerForrajes();
            
            if (forrajes != null && forrajes.Count > 0)
            {
                forrajes = forrajes.Where(registro => !registro.Descripcion.ToUpper().Contains("SILO")).ToList();

                if(forrajes.Count > 0)
                forrajes = forrajes.GroupBy(producto => producto.ProductoId)
                               .Select(productos => productos.First())
                               .ToList();

                if (forrajes.Count > 0)
                {
                    cmbForrajes.DataSource = forrajes;
                    cmbForrajes.DataTextField = "ProductoDescripcion";
                    cmbForrajes.DataValueField = "ProductoID";
                    cmbForrajes.DataBind();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Establece los datos del analista
        /// </summary>
        private void EstableceDatosAnalista()
        {
            SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (seguridad != null && seguridad.Usuario != null)
            {
                txtAnalista.Text = seguridad.Usuario.Nombre;
            }
        }

        /// <summary>
        /// Establece  los datos del camion en pantalla.
        /// </summary>
        /// <param name="camionInfo"></param>
        protected bool EstableceDatosCamion(CamionInfo camionInfo, int tipoflete, RegistroVigilanciaInfo registroVigilancia)
        {
            if (camionInfo != null)
            {
                if (tipoflete == 1)
                {
                    List<RegistroVigilanciaInfo> camiones = new List<RegistroVigilanciaInfo>();
                    camiones.Add(registroVigilancia);

                    cmbPlacas.DataSource = camiones;
                    cmbPlacas.DataTextField = "CamionCadena";
                    cmbPlacas.DataValueField = "IdNulo";
                    cmbPlacas.DataBind();
                    return true;
                }
                else
                {
                    List<CamionInfo> camiones = new List<CamionInfo>();
                    camiones.Add(camionInfo);

                    cmbPlacas.DataSource = camiones;
                    cmbPlacas.DataTextField = "PlacaCamion";
                    cmbPlacas.DataValueField = "CamionID";
                    cmbPlacas.DataBind();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Establece los datos del proveedor en la pantalla
        /// </summary>
        /// <param name="proveedorInfo"></param>
        protected bool EstableceDatosProveedor(ProveedorInfo proveedorInfo, int tipoflete, RegistroVigilanciaInfo registroVigilancia)
        {
            
            if (proveedorInfo != null)
            {
                var boletaRecepcionForrajePl = new BoletaRecepcionForrajePL();
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                registroVigilancia.Organizacion = seguridad.Usuario.Organizacion;
                var rangos = boletaRecepcionForrajePl.ObtenerRangos(registroVigilancia, IndicadoresEnum.Humedad);
                if (rangos == null)
                {
                    return false;
                }

                hdPorcentajePermitidoMin.Value = Convert.ToString(rangos.porcentajePromedioMin);
                porcentajePromedioMin = rangos.porcentajePromedioMin;
                hdPorcentajePermitidoMax.Value = Convert.ToString(rangos.porcentajePromedioMax);
                porcentajePromedioMax = rangos.porcentajePromedioMax;

                if (tipoflete == 1)
                {
                    List<RegistroVigilanciaInfo> proveedores = new List<RegistroVigilanciaInfo>();
                    proveedores.Add(registroVigilancia);

                    cmbProveedor.DataSource = proveedores;
                    cmbProveedor.DataTextField = "Transportista";
                    cmbProveedor.DataValueField = "IdNulo";
                    cmbProveedor.DataBind();
                    return true;
                }
                else
                {
                    List<ProveedorInfo> proveedores = new List<ProveedorInfo>();
                    proveedores.Add(proveedorInfo);

                    cmbProveedor.DataSource = proveedores;
                    cmbProveedor.DataTextField = "Descripcion";
                    cmbProveedor.DataValueField = "ProveedorID";
                    cmbProveedor.DataBind();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Establece los datos del chofer en la pantalla
        /// </summary>
        /// <param name="choferInfo"></param>
        protected bool EstableceDatosChofer(ChoferInfo choferInfo, int tipoflete, RegistroVigilanciaInfo registroVigilancia)
        {
            if (choferInfo != null)
            {
                if (tipoflete == 1)
                {
                    List<RegistroVigilanciaInfo> choferes = new List<RegistroVigilanciaInfo>();
                    choferes.Add(registroVigilancia);

                    cmbChofer.DataSource = choferes;
                    cmbChofer.DataTextField = "Chofer";
                    cmbChofer.DataValueField = "IdNulo";
                    cmbChofer.DataBind();
                    return true;
                }
                else
                {
                    List<ChoferInfo> choferes = new List<ChoferInfo>();
                    choferes.Add(choferInfo);

                    cmbChofer.DataSource = choferes;
                    cmbChofer.DataTextField = "NombreCompleto";
                    cmbChofer.DataValueField = "ChoferID";
                    cmbChofer.DataBind();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Establece los datos de los contratos que pertenecen al proveedor.
        /// </summary>
        private bool EstablecerDatosContrato(RegistroVigilanciaInfo registroVigilancia, int tipoflete)
        {
            contratos = new List<ContratoInfo>();
            contratos.Add(registroVigilancia.Contrato);

            if (contratos != null && contratos.Count > 0)
            {
                cmbContrato.DataSource = contratos;
                cmbContrato.DataTextField = "Folio";
                cmbContrato.DataValueField = "ContratoID";
                cmbContrato.DataBind();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Establece en pantalla todos los datos encontrados en el registro de vigilancia.
        /// </summary>
        /// <param name="registroVigilancia"></param>
        private void EstablecerDatosGenerales(RegistroVigilanciaInfo registroVigilancia)
        {
            int tipoflete;
            tipoflete = registroVigilancia.Contrato.TipoFlete.TipoFleteId;
            validarCalidadOrigen(registroVigilancia.Contrato.CalidadOrigen);
            txtCalidadOrigenID.Text = Convert.ToString(registroVigilancia.Contrato.CalidadOrigen);
            txtPorcentajePermitido.Text =
                Convert.ToString(registroVigilancia.Contrato.ListaContratoDetalleInfo[0].PorcentajePermitido);
            if (EstableceDatosProveedor(registroVigilancia.ProveedorMateriasPrimas, tipoflete, registroVigilancia))

            {
                calidadOrigen = registroVigilancia.Contrato.CalidadOrigen;
            if (EstablecerDatosContrato(registroVigilancia, tipoflete))
            {
                if (LlenaComboForrajes(registroVigilancia, tipoflete))
                {
                    if (registroVigilancia.ProveedorChofer != null)
                    {
                        if (EstableceDatosChofer(registroVigilancia.ProveedorChofer.Chofer, tipoflete,
                            registroVigilancia))
                        {
                            if (!EstableceDatosCamion(registroVigilancia.Camion, tipoflete, registroVigilancia))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "placas", "MensajeChoferSinCamion()",
                                    true);
                            }

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "chofer", "MensajeProveedorSinChofer()",
                                true);
                        }
                    }
                    else
                    {
                        if (tipoflete == TipoFleteEnum.LibreAbordo.GetHashCode())
                        {
                            List<ChoferInfo> choferes = new List<ChoferInfo>();
                            choferes.Add(new ChoferInfo() {NombreCompleto = registroVigilancia.Chofer});

                            cmbChofer.DataSource = choferes;
                            cmbChofer.DataTextField = "NombreCompleto";
                            cmbChofer.DataValueField = "ChoferID";
                            cmbChofer.DataBind();

                            List<CamionInfo> camiones = new List<CamionInfo>();
                            camiones.Add(new CamionInfo() {PlacaCamion = registroVigilancia.CamionCadena});

                            cmbPlacas.DataSource = camiones;
                            cmbPlacas.DataTextField = "PlacaCamion";
                            cmbPlacas.DataValueField = "CamionID";
                            cmbPlacas.DataBind();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "chofer", "MensajeProveedorSinChofer()",
                                true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "productos", "MensajeProveedorSinProductos()",
                        true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "contratos", "MensajeProveedorSinContrato()", true);
            }
        }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "contratos", "MensajeProductoErrorRangos()", true);
            }
    }

        private void validarCalidadOrigen(int calidad)
        {
            if (calidad == 1)
            {
                txtFolioOrigen.Enabled = true;
                txtFechaOrigen.Enabled = true;
                txtHumedadOrigen.Enabled = true;
            }
            else
            {
                txtFolioOrigen.Enabled = false;
                txtFechaOrigen.Enabled = false;
                txtHumedadOrigen.Enabled = false;
            }
        }

        /// <summary>
        /// Obtiene el ticket de un registro de vigilancia
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        protected RegistroVigilanciaInfo ObtenerTicket(int ticket,int organizacionId)
        {
            RegistroVigilanciaInfo registroVigilancia = null;
            var registroVigilanciaPL = new RegistroVigilanciaPL();
            try
            {
                registroVigilancia = new RegistroVigilanciaInfo { FolioTurno = ticket, Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionId } };
                registroVigilancia = registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(registroVigilancia);
                
                if (registroVigilancia != null)
                {
                    
                    var entradaProductoPl = new EntradaProductoPL();
                    EntradaProductoInfo entradaProducto = entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(registroVigilancia.RegistroVigilanciaId, registroVigilancia.Organizacion.OrganizacionID);

                    if (entradaProducto != null)
                    {
                        registroVigilancia = null;
                    }
                    else
                    {
                        var productoPl = new ProductoPL();
                        //Valida que el registro de vigilancia sea de un producto forraje configurado en la TB ParametroGeneral
                        registroVigilancia.Producto = productoPl.ObtenerProductoForraje(registroVigilancia.Producto);
                        //
                        if (registroVigilancia.Producto == null)
                        {
                            registroVigilancia = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return registroVigilancia;
        }

        /// <summary>
        /// Obtiene los contratos que el proveedor tiene activos con el producto registrado en
        /// el modulo de registro vigilancia.
        /// </summary>
        /// <param name="registroVigilancia"></param>
        /// <returns></returns>
        private List<ContratoInfo> ObtenerContratos(RegistroVigilanciaInfo registroVigilancia)
        {
            List<ContratoInfo> listaContratos;
            var contratoPL = new ContratoPL();

            int proveedorId = registroVigilancia.ProveedorMateriasPrimas.ProveedorID;
            int organizacionId = registroVigilancia.Organizacion.OrganizacionID;

            contratos = null;
            listaContratos = contratoPL.ObtenerContratosPorProveedorId(proveedorId, organizacionId);
            listaContratos =
                listaContratos.Where(
                    registro => registro.Producto.ProductoId == registroVigilancia.Producto.ProductoId).ToList();

            if (listaContratos.Count > 0)
            {
                var productoPl = new ProductoPL();
                var listaProductosValidos = productoPl.ObtenerProductosValidosForraje();

                listaContratos = (from contrato in listaContratos
                    from producto in listaProductosValidos
                    where contrato.Producto.ProductoId == producto.ProductoId
                    select contrato).ToList();

                if (listaContratos.Count == 0)
                {
                    listaContratos = null;
                }
            }
            else
            {
                listaContratos = null;
            }
            return listaContratos;
        } 

        /// <summary>
        /// Obtiene los tipos de forrajes que van a evaluar.
        /// </summary>
        /// <returns></returns>
        private List<ProductoInfo> ObtenerForrajes()
        {
            List<ProductoInfo> listaProductos = null;
            try
            {
                
                if (contratos != null)
                {
                    if (contratos.Select(contrato => contrato.Producto).ToList().Count > 0)
                    {
                        listaProductos = new List<ProductoInfo>();
                        listaProductos.AddRange(contratos.Select(contrato => contrato.Producto));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaProductos;
        }

        /// <summary>
        /// Consulta los productos que ya tengan la muestra 1 capturada y que se encuentren
        /// con el estatus pendiente de autorizar.
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<EntradaProductoInfo> ObtenerEntradaProductosConMuestra(int folio)
        {
            List<EntradaProductoInfo> listaEntradaProductoAutorizado = null;
            try
            {
                var entradaProductoPl = new EntradaProductoPL();
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    EntradaProductoInfo entrada = new EntradaProductoInfo { Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }, Estatus = new EstatusInfo { EstatusId = (int)Estatus.Autorizado } };
                    listaEntradaProductoAutorizado = entradaProductoPl.ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(entrada, folio);

                    entrada = new EntradaProductoInfo { Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }, Estatus = new EstatusInfo { EstatusId = (int)Estatus.Aprobado } };
                    var listaProductoAprobado =
                        entradaProductoPl.ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(entrada, folio);

                    if (listaProductoAprobado != null)
                    {
                        listaEntradaProductoAutorizado.AddRange(listaProductoAprobado);
                    }
                    entrada = new EntradaProductoInfo { Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }, Estatus = new EstatusInfo { EstatusId = (int)Estatus.PendienteAutorizar } };

                    var lisProductoPendienteAutorizacion = entradaProductoPl.ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(entrada,folio);
                    if (lisProductoPendienteAutorizacion != null)
                    {
                        listaEntradaProductoAutorizado.AddRange(lisProductoPendienteAutorizacion);
                    }
                    
                    if (folio > 0 && listaEntradaProductoAutorizado != null)
                    {
                        //Filtra todos los folios que contengan el numero de folio capturado
                        listaEntradaProductoAutorizado =
                            listaEntradaProductoAutorizado.Where(
                                registro =>
                                    registro.Folio.ToString(CultureInfo.InvariantCulture)
                                        .Contains(folio.ToString(CultureInfo.InvariantCulture))).ToList();
                    }

                    if (listaEntradaProductoAutorizado != null)
                    {
                        var productoPl = new ProductoPL();
                        var listaProductosValidos = productoPl.ObtenerProductosValidosForraje();

                        listaEntradaProductoAutorizado = (from contrato in listaEntradaProductoAutorizado
                                          from producto in listaProductosValidos
                                          where contrato.Producto.ProductoId == producto.ProductoId
                                          select contrato).ToList();

                        if (listaEntradaProductoAutorizado.Count == 0)
                        {
                            listaEntradaProductoAutorizado = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaEntradaProductoAutorizado;
        }

        /// <summary>
        /// Guarda una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo GuardarEntradaProducto(EntradaProductoInfo entradaProducto, int Bandera, decimal PorcentajePermitidoMin, decimal PorcentajePermitidoMax)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var entradaProductoPL = new EntradaProductoPL();
                    entradaProducto.Organizacion = new OrganizacionInfo() { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };

                    if (AnalizaMuestras(entradaProducto, PorcentajePermitidoMin, PorcentajePermitidoMax))
                    {

                        var registroVigilanciaPL = new RegistroVigilanciaPL();
                        var contratoPl = new ContratoPL();
                        var contrato = contratoPl.ObtenerPorFolio(entradaProducto.Contrato);
                        var registroVigilancia = new RegistroVigilanciaInfo { FolioTurno = entradaProducto.RegistroVigilancia.FolioTurno, Organizacion = new OrganizacionInfo() { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID } };
                        entradaProducto.RegistroVigilancia = registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurno(registroVigilancia);


                        if (entradaProducto.Folio == 0)
                        {
                            entradaProducto.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                            entradaProducto.OperadorAnalista = new OperadorInfo() { OperadorID = seguridad.Usuario.Operador.OperadorID };

                            if (Bandera == 1)
                            {
                                entradaProducto.Estatus.EstatusId = (int)Estatus.Aprobado;
                            }
                            else
                            {
                                entradaProducto.Estatus.EstatusId = (int)Estatus.PendienteAutorizar;
                            }
                            EntradaProductoInfo ProductoNuevoTemporal = new EntradaProductoInfo();
                            entradaProducto.ProductoDetalle[0].Indicador.IndicadorId = IndicadoresEnum.Humedad.GetHashCode();
                            ProductoNuevoTemporal = entradaProductoPL.GuardarEntradaProductoConDetalle(entradaProducto, (int)TipoFolio.EntradaProducto);
                            if (contrato.CalidadOrigen == 1)
                            {
                                AgregarRegitroOrigen(entradaProducto);
                            }
                            ActualizarCampoDescuento(entradaProducto, contrato);
                            entradaProducto = ProductoNuevoTemporal;

                            return entradaProducto;
                        }
                        else
                        {
                            entradaProducto.Organizacion = new OrganizacionInfo() { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };
                            entradaProducto.ProductoDetalle[0].ProductoMuestras.Average(registro => registro.Porcentaje);
                            entradaProducto.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                            entradaProducto.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                            entradaProducto.OperadorAnalista = new OperadorInfo() { OperadorID = seguridad.Usuario.Operador.OperadorID };

                            if (entradaProductoPL.ActualizarEntradaProductoConDetalle(entradaProducto))
                            {
                                ActualizarCampoDescuento(entradaProducto, contrato);
                                return entradaProducto;
                            }
                            
                        }
                    }else{

                        throw new ExcepcionServicio("MensajeNoValidoMuestras");  
                    }
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new Exception(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Guarda una entrada de producto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerLimiteMuestras()
        {
            int cantidadMuestras = 0;
            string limite = WebConfigurationManager.AppSettings["LimiteMuestrasForraje"];
            if(!String.IsNullOrEmpty(limite)){
                cantidadMuestras = int.Parse(limite);
            }
            return cantidadMuestras;
        }

        /// <summary>
        /// Valida si el numero de ticket capturado sea valido, verifica que exista en 
        /// registro vigilancia y tenga entrada de producto.
        /// </summary>
        private void ValidaTicket()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    if (!txtTicket.Text.Equals(String.Empty))
                    {
                        int ticket = int.Parse(txtTicket.Text);
                        int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                        RegistroVigilanciaInfo registroVigilancia = ObtenerTicket(ticket, organizacionId);
                        if (registroVigilancia != null)
                        {
                            var entradaProductoPl = new EntradaProductoPL();
                            var entradaProducto = entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(registroVigilancia.RegistroVigilanciaId, registroVigilancia.Organizacion.OrganizacionID);
                            if (entradaProducto == null)
                            {
                                EstablecerDatosGenerales(registroVigilancia);
                            }
                            else
                            {
                                if (entradaProducto.Estatus.EstatusId == (int)Estatus.Rechazado)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "rechazo",
                                        "MensajeProductoPrevioRechazo()", true);
                                }
                                else if (entradaProducto.Estatus.EstatusId == (int)Estatus.PendienteAutorizar)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "pendiente",
                                        "MensajeProductoPrevioPendiente()", true);
                                }
                                else if (entradaProducto.Estatus.EstatusId == (int)Estatus.Autorizado ||
                                         entradaProducto.Estatus.EstatusId == (int)Estatus.Aprobado)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "aprobado",
                                        "MensajeProductoPrevioAprobado()", true);
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeTicketNoExiste()", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "SesionExpirada()", true);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeError(msgErrorInterno)", true);
            }
        }

        /// <summary>
        /// Devuelve las muestras que tenga capturadas en el detalle de la entrada del producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private static List<EntradaProductoMuestraInfo> ObtenerMuestras(EntradaProductoInfo entradaProducto)
        {
            List<EntradaProductoMuestraInfo> resultadoMuestras = null;
            if (entradaProducto != null)
            {
                resultadoMuestras = (from entradaProductoIndicador in entradaProducto.ProductoDetalle
                                     from entradaProductoMuestras in entradaProductoIndicador.ProductoMuestras
                                     where entradaProductoMuestras.EntradaProductoDetalleId == entradaProductoIndicador.EntradaProductoDetalleId
                                     select entradaProductoMuestras).ToList();
            }
            return resultadoMuestras;
        }

        /// <summary>
        /// Obtiene el promedio de las resultadoMuestras capturadas.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private static double ObtenerPromedioPorcentaje(EntradaProductoInfo entradaProducto)
        {
            double resultado = 0;
            var muestras = ObtenerMuestras(entradaProducto);

            if (muestras != null)
            {
                resultado = (double)(from detalleMuestras in muestras
                            select detalleMuestras.Porcentaje).Average();
            }
            return resultado;
        }

        /// <summary>
        /// Rechaza las resultadoMuestras que no pasaron el rango permitido segun el contrato.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="contratoDetalle"></param>
        private static bool RechazaProductos(EntradaProductoInfo entradaProducto, ContratoDetalleInfo contratoDetalle, decimal PorcentajePermitido)
        {
            bool resultado = true;
            try
            {
                if (entradaProducto != null)
                {
                    var muestras = ObtenerMuestras(entradaProducto);

                    if (muestras != null && muestras.Count > 0)
                    {
                        int cantidadMuestras = 0;
                        foreach (var muestra in muestras)
                        {
                            if (muestra.Porcentaje < PorcentajePermitido)
                            {
                                muestra.Rechazo = EstatusEnum.Activo;
                            }
                            cantidadMuestras++;
                        }
                    }
                }

            }catch(Exception ex){
                resultado = false;
                Logger.Error(ex);
            }
            return resultado;           
        }

        /// <summary>
        /// Obtiene las Muestras que se van a grabar en el detalle, si vienen las Muestras 1 previamente
        /// capturadas cuando se genero el folio se eliminaran para dejar las Muestras 2 para el grabado
        /// final de las Muestras.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private static List<EntradaProductoMuestraInfo> ObtenerMuestrasFinales(EntradaProductoInfo entradaProducto)
        {
            List<EntradaProductoMuestraInfo> resultadoMuestras = null;
            if(entradaProducto != null)
            {
                resultadoMuestras = ObtenerMuestras(entradaProducto);

                resultadoMuestras = (from muestra in resultadoMuestras
                                     where muestra.EntradaProductoMuestraId == 0
                                     select muestra).ToList();
            }
            return resultadoMuestras;
        }

        /// <summary>
        /// Verifica que las muestras capturadas tengan el porcentaje permitido, segun lo especificado en el indicador
        /// del contrato, si no cumplen con el rango rechazara la muestra, al finalizar de validar las muestras, obtendra
        /// el promedio de los porcentajes capturados, si no cumple con el rango rechazara el forraje de lo contrario lo dejara 
        /// en Pendiente de Autorizar o Aprobado segun sea el caso.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private static bool AnalizaMuestras(EntradaProductoInfo entradaProducto, decimal PorcentajePermitidoMin, decimal PorcentajePermitidoMax)
        {
            bool resultado = true;
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var contratoPL = new ContratoPL();
                    var contrato = new ContratoInfo
                    {
                        ContratoId = entradaProducto.Contrato.ContratoId,
                        Organizacion =
                            new OrganizacionInfo() {OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID}
                    };
                    contrato = contratoPL.ObtenerPorId(contrato);
                    entradaProducto.Contrato = contrato;

                    var contratoDetalleInfo = (ContratoDetalleInfo)(from contratoDetalle in contrato.ListaContratoDetalleInfo
                                                                    select contratoDetalle).First();

                    double promedio = ObtenerPromedioPorcentaje(entradaProducto);
                    if (RechazaProductos(entradaProducto, contratoDetalleInfo, PorcentajePermitidoMax))
                    {
                        entradaProducto.Estatus = new EstatusInfo() { EstatusId = (int)Estatus.Aprobado };

                        //Si el promedio no cumple con el rango establecido en el contrato se manda  pendiente, falta autorizar
                        if (promedio< (double)PorcentajePermitidoMin || promedio > (double)PorcentajePermitidoMax)
                        {
                            entradaProducto.Estatus.EstatusId = (int)Estatus.PendienteAutorizar;
                        }
                        else
                        {
                            if (entradaProducto.Folio == 0)
                            {
                                entradaProducto.Estatus.EstatusId = (int)Estatus.Rechazado;
                            }
                        }

                        var productoDetalle = (EntradaProductoDetalleInfo) (from entradaDetalle in entradaProducto.ProductoDetalle
                                               select entradaDetalle).First();

                        //Eliminamos las muestras1 que ya fueron capturadas.
                        productoDetalle.ProductoMuestras = ObtenerMuestrasFinales(entradaProducto);
                    }
               }
            }catch(Exception ex){
                resultado = false;
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo ObtenerEntradaProducto(EntradaProductoInfo entradaProducto)
        {
            var entradaProductoPL = new EntradaProductoPL();
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    entradaProducto = entradaProductoPL.ObtenerEntradaProductoPorFolio(entradaProducto.Folio,
                        seguridad.Usuario.Organizacion.OrganizacionID);

                    if (entradaProducto != null)
                    {
                        //Filtra en caso de existir el registro 16 (registro adicional cuando EsOrigen= 1)
                        entradaProducto.ProductoDetalle[0].ProductoMuestras =
                            entradaProducto.ProductoDetalle[0].ProductoMuestras.Where(elemento => elemento.EsOrigen == EsOrigenEnum.Destino).ToList();

                        if (entradaProducto.datosOrigen.folioOrigen != 0)
                        {
                            var humedadOrigen = entradaProductoPL.ObtenerHumedadOrigen(entradaProducto);
                            entradaProducto.datosOrigen.humedadOrigen = humedadOrigen;
                            var fecha = Convert.ToString(entradaProducto.datosOrigen.fechaOrigen);
                            fecha = fecha.Substring(0, 10);
                            entradaProducto.datosOrigen.fechaFormateada = fecha;
                        }
                        var productoPl = new ProductoPL();
                        var producto = productoPl.ObtenerProductoForraje(entradaProducto.Producto);
                        if (producto == null)
                        {
                            entradaProducto = null;
                        }
                        else
                        {
                            if (entradaProducto.Activo != EstatusEnum.Activo)
                            {
                                entradaProducto = null;
                            }
                            else
                            {
                                var boletaRecepcionForrajePl = new BoletaRecepcionForrajePL();
                                entradaProducto.RegistroVigilancia.Organizacion = seguridad.Usuario.Organizacion;
                                var rangos = boletaRecepcionForrajePl.ObtenerRangos(entradaProducto.RegistroVigilancia, IndicadoresEnum.Humedad);
                                entradaProducto.RegistroVigilancia.porcentajePromedioMin = rangos.porcentajePromedioMin;
                                entradaProducto.RegistroVigilancia.porcentajePromedioMax = rangos.porcentajePromedioMax;
                                
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
            return entradaProducto;
        }

        /// <summary>
        /// Obtiene una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo ObtenerEntradaProductoConsulta(int folio)
        {
            var entradaProductoPL = new EntradaProductoPL();
            EntradaProductoInfo entradaProducto = null;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    entradaProducto = entradaProductoPL.ObtenerEntradaProductoPorFolio(folio,
                        seguridad.Usuario.Organizacion.OrganizacionID);

                    if (entradaProducto != null)
                    {
                        //Filtra en caso de existir el registro 16 (registro adicional cuando EsOrigen= 1)
                        entradaProducto.ProductoDetalle[0].ProductoMuestras =
                            entradaProducto.ProductoDetalle[0].ProductoMuestras.Where(elemento => elemento.EsOrigen == EsOrigenEnum.Destino).ToList();

                        if (entradaProducto.datosOrigen.folioOrigen != 0)
                        {
                            var humedadOrigen = entradaProductoPL.ObtenerHumedadOrigen(entradaProducto);
                            entradaProducto.datosOrigen.humedadOrigen = humedadOrigen;
                            var fecha = Convert.ToString(entradaProducto.datosOrigen.fechaOrigen);
                            fecha = fecha.Substring(0, 10);
                            entradaProducto.datosOrigen.fechaFormateada = fecha;
                        }
                        var productoPl = new ProductoPL();
                        var producto = productoPl.ObtenerProductoForraje(entradaProducto.Producto);
                        if (producto == null)
                        {
                            entradaProducto = null;
                        }
                        else
                        {
                            if (entradaProducto.Activo != EstatusEnum.Activo)
                            {
                                entradaProducto = null;
                            }
                            else
                            {
                                var boletaRecepcionForrajePl = new BoletaRecepcionForrajePL();
                                entradaProducto.RegistroVigilancia.Organizacion = seguridad.Usuario.Organizacion;
                                var rangos = boletaRecepcionForrajePl.ObtenerRangos(entradaProducto.RegistroVigilancia, IndicadoresEnum.Humedad);
                                entradaProducto.RegistroVigilancia.porcentajePromedioMin = rangos.porcentajePromedioMin;
                                entradaProducto.RegistroVigilancia.porcentajePromedioMax = rangos.porcentajePromedioMax;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
            return entradaProducto;
        }

        /// <summary>
        /// Valida si existe el Folio
        /// </summary>
        private void ValidaFolio()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    if (!txtTicket.Text.Equals(String.Empty))
                    {
                        int ticket = int.Parse(txtTicket.Text);
                        int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                        var registroVigilancia = new RegistroVigilanciaInfo { FolioTurno = ticket, Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionId } };
                        var registroVigilanciaPL = new RegistroVigilanciaPL(); 
                        var productoPl = new ProductoPL();
                        registroVigilancia = registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(registroVigilancia);

                        if (registroVigilancia != null)
                        {
                            if (registroVigilancia.Producto.SubFamilia.SubFamiliaID != SubFamiliasEnum.Forrajes.GetHashCode())
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeTicketNoExiste()", true);
                                return;
                            }
                            if (registroVigilancia.Producto == null) 
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeTicketNoExiste()", true);
                                return;
                            }
                            if (registroVigilancia.FechaSalida != new DateTime(1900, 01, 01))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeFolioFechaSalida()", true);
                                return;
                            }
                            else
                            {
                                //valida si el producto es un forraje para darle entrada
                                if (registroVigilancia.Producto != null)
                                {
                                    var entradaProductoPl = new EntradaProductoPL();
                                    registroVigilancia.Producto = productoPl.ObtenerProductoForraje(registroVigilancia.Producto);
                                    EntradaProductoInfo entradaProducto;
                                    if (registroVigilancia.Producto != null)
                                    {
                                         entradaProducto = entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(registroVigilancia.RegistroVigilanciaId, registroVigilancia.Organizacion.OrganizacionID);
                                         if (entradaProducto == null)
                                         {
                                             EstablecerDatosGenerales(registroVigilancia);
                                         }
                                         else
                                         {
                                             ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeFolioYaRegistrado()", true);
                                         }
                                    }
                                    else 
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeTicketNoExiste()", true);
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeTicketNoExiste()", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "SesionExpirada()", true);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e);
                ClientScript.RegisterStartupScript(this.GetType(), "noExiste", "MensajeError(msgErrorInterno)", true);
            }
        }

        /// <summary>
        /// Método que verifica si el promedio de humedad de las primeras 15 muestras está por enciam de
        /// el promedio permitido en el contrato. Si esta por encima, se calcula el descuento a aplicar
        /// </summary>
        [WebMethod]
        public static decimal CalcularDescuento(int folio, decimal promedioHumedad, decimal humedadOrigen)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    if (folio > 0)
                    {
                        int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                        var registroVigilancia = new RegistroVigilanciaInfo
                        {
                            FolioTurno = folio,
                            Organizacion = new OrganizacionInfo() {OrganizacionID = organizacionId}
                        };

                        var registroVigilanciaPL = new RegistroVigilanciaPL();
                        registroVigilancia =
                            registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurno(registroVigilancia);

                        if (registroVigilancia != null)
                        {
                            decimal porcentajePermitidoContrato =
                                registroVigilancia.Contrato.ListaContratoDetalleInfo[0].PorcentajePermitido;
                            if (registroVigilancia.Contrato.CalidadOrigen == 1)
                            {
                                if (promedioHumedad > porcentajePermitidoContrato)
                                {
                                    return (promedioHumedad - porcentajePermitidoContrato);
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                if (humedadOrigen > porcentajePermitidoContrato)
                                {
                                    return (humedadOrigen - porcentajePermitidoContrato);
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
                return 0;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Método que agrega un registro a la tabla "EntradaProductoMuestra" 
        /// Se agrega un solo registro por tratarse de un folio que cuya CalidadOrigen= 1
        /// </summary>
        private static void AgregarRegitroOrigen(EntradaProductoInfo entradaProducto)
        {
            try
            {
                var boletaRecepcionForrajePl = new BoletaRecepcionForrajePL();
                boletaRecepcionForrajePl.AgregarNuevoRegistro(entradaProducto);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new Exception(ex.Message);
            }
        }
       
        /// <summary>
        /// Método que actualiza los 15 0 30 registros de la tabla "EntradaProductoMuestra" para el ticket que esta en pantalla
        /// </summary>
        private static void ActualizarCampoDescuento(EntradaProductoInfo entradaProducto, ContratoInfo contrato)
        {
            decimal descuento = 0;
            try
            {
                
                if (contrato.CalidadOrigen == 1)
                {
                    if (contrato.ListaContratoDetalleInfo[0].PorcentajePermitido <
                        entradaProducto.datosOrigen.promedioMuestras)
                    {
                        if (entradaProducto.datosOrigen.humedadOrigen >
                            contrato.ListaContratoDetalleInfo[0].PorcentajePermitido)
                        {
                            descuento = entradaProducto.datosOrigen.humedadOrigen -
                                        contrato.ListaContratoDetalleInfo[0].PorcentajePermitido;
                        }
                    }
                }
                else
                {
                    if (contrato.ListaContratoDetalleInfo[0].PorcentajePermitido <
                        entradaProducto.datosOrigen.promedioMuestras)
                    {
                        if (entradaProducto.datosOrigen.promedioMuestras >
                            contrato.ListaContratoDetalleInfo[0].PorcentajePermitido)
                        {
                            descuento = entradaProducto.datosOrigen.promedioMuestras -
                                        contrato.ListaContratoDetalleInfo[0].PorcentajePermitido;
                        }
                    }
                }

                var entradaProductoPl = new EntradaProductoPL();
                entradaProductoPl.ActualizarDescuentoEntradaProductoMuestra(entradaProducto, descuento);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new Exception(ex.Message);
            }
      }

    }
}