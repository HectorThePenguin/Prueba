using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Windows;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.BL.Auxiliar;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.MessageBox;

namespace SIE.Web.Calidad
{
    public partial class BoletaRecepcion : PageBase
    {
        private static bool aplicaCapturaHumedad;
        private static List<int> ProductosHumedad;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtAprobado.Text = Estatus.Aprobado.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtRechazado.Text = Estatus.Rechazado.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtPendienteAutorizar.Text = Estatus.PendienteAutorizar.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtAutorizado.Text = "0";
            hdIditificadorHumedad.Value = Convert.ToString((int)IndicadoresEnum.Humedad);
            LlenaComboDestino();
            ValidarParametroHumedad();
        }

        

        private void ValidarParametroHumedad()
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            if (seguridad != null)
            {
                ParametroOrganizacionInfo parametro =
                    parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(
                        seguridad.Usuario.Organizacion.OrganizacionID, ParametrosEnum.MuestraHumedad.ToString());
                if (parametro != null)
                {
                    int valor;
                    int.TryParse(parametro.Valor, out valor);
                    if (valor == 1)
                    {
                        aplicaCapturaHumedad = true;
                        hfAplicaHumedad.Value = parametro.Valor;
                    }

                    ParametroOrganizacionInfo parametroProductos = parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(
                        seguridad.Usuario.Organizacion.OrganizacionID, ParametrosEnum.ProductosMuestraHumedad.ToString());
                    if (parametroProductos != null)
                    {
                        if (parametroProductos.Valor.Contains('|'))
                        {
                            ProductosHumedad = (from tipos in parametroProductos.Valor.Split('|')
                                                select Convert.ToInt32(tipos)).ToList();
                        }
                        else
                        {
                            int producto = Convert.ToInt32(parametroProductos.Valor);
                            ProductosHumedad.Add(producto);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Funcion que obtiene los proveedores de tipo materias primas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorInfo> ObtenerProveedores()
        {
            var proveedorPL = new ProveedorPL();
            var listaProveedores = new List<ProveedorInfo>();

            try
            {
                listaProveedores = proveedorPL.ObtenerTodos().ToList();

                if (listaProveedores.Count > 0)
                {
                    listaProveedores = listaProveedores.Where(
                                            registro =>
                                                registro.TipoProveedor.TipoProveedorID == (int)TipoProveedorEnum.ProveedoresDeMateriaPrima).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaProveedores;
        }

        /// <summary>
        /// Funcion que llena el combo de destino
        /// </summary>
        private void LlenaComboDestino()
        {
            var tipoContratoPl = new TipoContratoPL();

            try
            {
                List<TipoContratoInfo> listaTipoContrato = tipoContratoPl.ObtenerTodos();

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
        /// Obtiene la lista de Entradas pendientes
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        [WebMethod]
        public static List<EntradaProductoInfo> ObtenerEntradaProductosPendiente(int folio)
        {
            List<EntradaProductoInfo> listaEntradaProductoPendiente = null;

            try
            {
                var entradaProductoPl = new EntradaProductoPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    var entrada = new EntradaProductoInfo { Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }, Estatus = new EstatusInfo { EstatusId = (int)Estatus.PendienteAutorizar } };
                    listaEntradaProductoPendiente =
                        entradaProductoPl.ObtenerEntradaProductosTodosPorEstatusIdAyudaForraje(entrada, folio);

                    if (folio > 0 && listaEntradaProductoPendiente != null)
                    {
                        //Filtra todos los folios que contengan el numero de folio capturado
                        listaEntradaProductoPendiente =
                            listaEntradaProductoPendiente.Where(
                                registro =>
                                    registro.Folio.ToString(CultureInfo.InvariantCulture)
                                        .Contains(folio.ToString(CultureInfo.InvariantCulture))).ToList();

                    }

                    if (listaEntradaProductoPendiente != null)
                    {
                        var productoPl = new ProductoPL();
                        var listaProductosInvalidos = productoPl.ObtenerProductosValidosForraje();

                        listaEntradaProductoPendiente = (from contrato in listaEntradaProductoPendiente
                                                         where
                                                             !(from productos in listaProductosInvalidos select productos.ProductoId).ToList()
                                                                 .Contains(contrato.Producto.ProductoId)
                                                         select contrato).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaEntradaProductoPendiente;
        }

        /// <summary>
        /// Obtiene un registro de entrada producto por folio
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo ObtenerFolio(int folio)
        {
            var entradaProductoPL = new EntradaProductoPL();
            EntradaProductoInfo entradaProducto = null;

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    entradaProducto = entradaProductoPL.ObtenerEntradaProductoPorFolio(folio,
                        seguridad.Usuario.Organizacion.OrganizacionID);
                    if (entradaProducto != null)
                    {
                        var productoPl = new ProductoPL();
                        var producto = productoPl.ObtenerProductoForraje(entradaProducto.Producto);
                        if (producto != null)
                        {
                            entradaProducto = null;
                        }
                        else
                        {
                            if (entradaProducto.Activo != EstatusEnum.Activo)
                            {
                                entradaProducto = null;
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
        /// Obtiene el ticket de un registro de vigilancia
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [WebMethod]
        public static RegistroVigilanciaInfo ObtenerTicket(int ticket)
        {
            RegistroVigilanciaInfo registroVigilancia = null;
            var registroVigilanciaPL = new RegistroVigilanciaPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    registroVigilancia = new RegistroVigilanciaInfo { FolioTurno = ticket, Organizacion = new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID } };
                    registroVigilancia = registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurnoActivoInactivo(registroVigilancia);

                    if (registroVigilancia != null)
                    {
                        bool tieneFechaSalida = (registroVigilancia.FechaSalida != new DateTime(1900, 01, 01));

                        var entradaProductoPl = new EntradaProductoPL();
                        //valida que el folio ingresado del registro de vigilancia sea de un producto "MicroIngrediente"
                        EntradaProductoInfo entradaProducto = entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(registroVigilancia.RegistroVigilanciaId, registroVigilancia.Organizacion.OrganizacionID);
                        //
                        if (entradaProducto == null)
                        {
                            var productoPl = new ProductoPL();
                            //Valida que el registro de vigilancia sea de un producto forraje configurado en la TB ParametroGeneral
                            var producto = productoPl.ObtenerProductoForraje(registroVigilancia.Producto);
                            if (producto != null)//si es forraje:
                            {
                                registroVigilancia = null;
                            }
                            //
                        }
                        else
                        {
                            registroVigilancia = null;
                        }

                        if (tieneFechaSalida) 
                        {
                            return registroVigilancia;
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
        /// 
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ProductoInfo> ObtenerProductosProveedor(int proveedorId)
        {
            var listaProductos = new List<ProductoInfo>();

            var contratoPL = new ContratoPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    List<ContratoInfo> listaContratos = contratoPL.ObtenerContratosPorProveedorId(proveedorId,
                                                                                                  seguridad.Usuario.Organizacion.OrganizacionID);

                    if (listaContratos != null)
                    {
                        listaContratos =
                            listaContratos.Where(
                                registro => registro.Producto.Familia.FamiliaID == (int)FamiliasEnum.MateriaPrimas)
                                .ToList();

                        if (listaContratos.Count > 0)
                        {
                            var productoPl = new ProductoPL();
                            var listaProductosInvalidos = productoPl.ObtenerProductosValidosForraje();

                            listaContratos = (from contrato in listaContratos
                                              where
                                                  !(from productos in listaProductosInvalidos select productos.ProductoId).ToList()
                                                      .Contains(contrato.Producto.ProductoId)
                                              select contrato).ToList();

                            if (listaContratos.Count > 0)
                                if (listaContratos.Select(contrato => contrato.Producto).Any())
                                {
                                    listaProductos.AddRange(listaContratos.Select(contrato => contrato.Producto));
                                }
                                else
                                {
                                    listaProductos = null;
                                }
                        }
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
        /// Obtiene la lista de contratos del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="productoId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ContratoInfo> ObtenerProveedorContratos(int proveedorId, int productoId)
        {
            List<ContratoInfo> listaContratos = null;

            var contratoPL = new ContratoPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    listaContratos = contratoPL.ObtenerContratosPorProveedorId(proveedorId, seguridad.Usuario.Organizacion.OrganizacionID);
                    if (listaContratos != null)
                    {
                        var productoPl = new ProductoPL();
                        var listaProductosInvalidos = productoPl.ObtenerProductosValidosForraje();

                        listaContratos = (from contrato in listaContratos
                                          where !(from productos in listaProductosInvalidos select productos.ProductoId).ToList().Contains(contrato.Producto.ProductoId)
                                          select contrato).ToList();

                        if (listaContratos.Count > 0)
                        {
                            listaContratos =
                                listaContratos.Where(
                                    registro =>
                                        registro.Producto.ProductoId == productoId).ToList();
                            //Tipos de contrato Normal,Terceros, Transito
                            listaContratos =
                                listaContratos.Where(
                                    registro =>
                                        registro.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaNormal ||
                                        registro.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero ||
                                        registro.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito).ToList();
                        }
                        else
                        {
                            listaContratos = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaContratos;
        }

        /// <summary>
        /// Obtiene los indicadores de un contrato
        /// </summary>
        /// <param name="contratoId"></param>
        /// <returns></returns>
        [WebMethod]
        public static ContratoInfo ObtenerIndicadoresContrato(int contratoId)
        {
            ContratoInfo contrato = null;
            var contratoPL = new ContratoPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    contrato = new ContratoInfo
                                   {
                                       ContratoId = contratoId,
                                       Organizacion =
                                           new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID }
                                   };
                    contrato = contratoPL.ObtenerPorId(contrato);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return contrato;
        }

        /// <summary>
        /// Obtiene la lista de choferes del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorChoferInfo> ObtenerProveedorChofer(int proveedorId)
        {
            List<ProveedorChoferInfo> listaProveedorChofer = null;

            var proveedorChoferPL = new ProveedorChoferPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    listaProveedorChofer = proveedorChoferPL.ObtenerProveedorChoferPorProveedorId(proveedorId);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaProveedorChofer;
        }

        /// <summary>
        /// Obtiene la lista de placas del proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<CamionInfo> ObtenerProveedorPlacas(int proveedorId)
        {
            List<CamionInfo> listaCamion = null;

            var camionPL = new CamionPL();
            try
            {
                listaCamion = camionPL.ObtenerPorProveedorID(proveedorId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaCamion;
        }


        /// <summary>
        /// Guarda una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="Bandera"></param>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoValidacion GuardarEntradaProducto(EntradaProductoInfo entradaProducto, int Bandera)
        {
            try
            {
                var productoPl = new ProductoPL();
                var resultado = new ResultadoValidacion();
                var boletaRecepcionPl = new BoletaRecepcionForrajePL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var entradaProductoPl = new EntradaProductoPL();
                    if (entradaProducto.Justificacion == null)
                    {
                        entradaProducto.Organizacion = new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };

                        entradaProducto.Producto = productoPl.ObtenerPorID(entradaProducto.Producto);
                        var tmpRegistro = new RegistroVigilanciaInfo
                                              {
                                                  Producto = entradaProducto.Producto,
                                                  Organizacion = entradaProducto.Organizacion
                                              };

                        foreach (EntradaProductoDetalleInfo tmpDetalle in entradaProducto.ProductoDetalle)
                        {
                            var enumIndicador = (IndicadoresEnum)tmpDetalle.Indicador.IndicadorId;

                            foreach (var tmpIndicador in tmpDetalle.ProductoMuestras.Where(mue => mue.EsOrigen == EsOrigenEnum.Destino))
                            {
                                RegistroVigilanciaInfo registroVigelancia = boletaRecepcionPl.ObtenerRangos(tmpRegistro, enumIndicador);
                                if (registroVigelancia == null)
                                {
                                    resultado.Resultado = false;
                                    resultado.Mensaje = boletaRecepcionPl.ObtenerMensajeRango() + " " + entradaProducto.Producto.ProductoDescripcion;

                                    return resultado;
                                }
                                if (registroVigelancia.porcentajePromedioMin > tmpIndicador.Porcentaje || registroVigelancia.porcentajePromedioMax < tmpIndicador.Porcentaje)
                                {
                                    Bandera = 2;
                                }

                            }

                        }

                        //Bandera = 1.- Aprobada:2.- Pendiente por Autorizar;3.- Autorizado:
                        entradaProducto.Estatus = new EstatusInfo();

                        if (Bandera == 1)
                        {
                            entradaProducto.Estatus.EstatusId = (int)Estatus.Aprobado;
                        }
                        else
                        {
                            entradaProducto.Estatus.EstatusId = (int)Estatus.PendienteAutorizar;
                        }
                        entradaProducto.PesoBonificacion = 0;
                        var registroVigilanciaPl = new RegistroVigilanciaPL();
                        var registroVigilancia = new RegistroVigilanciaInfo
                        {
                            FolioTurno = entradaProducto.RegistroVigilancia.FolioTurno,
                            Organizacion =
                                new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID }
                        };
                        entradaProducto.RegistroVigilancia = registroVigilanciaPl.ObtenerRegistroVigilanciaPorFolioTurno(registroVigilancia);
                        entradaProducto.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                        entradaProducto.OperadorAnalista = new OperadorInfo { OperadorID = seguridad.Usuario.Operador.OperadorID };
                        entradaProducto = entradaProductoPl.GuardarEntradaProducto(entradaProducto, (int)TipoFolio.EntradaProducto);

                        resultado.Resultado = true;
                        resultado.Control = entradaProducto;
                        return resultado;
                    }
                    entradaProducto.Organizacion = new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };
                    entradaProducto.Estatus = new EstatusInfo { EstatusId = (int)Estatus.Autorizado };
                    entradaProducto.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                    entradaProducto.OperadorAutoriza = new OperadorInfo { OperadorID = seguridad.Usuario.Operador.OperadorID };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        /// <summary>
        /// Guarda la actualizacion de los descuentos en las tablas
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool GuardarActualizacionProductos(EntradaProductoDetalleInfo indicadores)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var entradaProductoMuestrasPl = new EntradaProductoMuestraPL();
                    foreach (var muestra in indicadores.ProductoMuestras)
                    {
                        muestra.UsuarioModificacion = seguridad.Usuario;
                    }
                    return entradaProductoMuestrasPl.GuardarActualizacionProductos(indicadores);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// The obtener indicador producto.
        /// </summary>
        /// <param name="IdProducto">
        /// The id producto.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion ObtenerIndicadorProducto(int IdProducto)
        {
            var resultado = new ResultadoValidacion();

            try
            {
                var indicadorProductoBoletaBL = new IndicadorProductoBoletaBL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    List<IndicadorProductoBoletaInfo> indicadoresBoleta =
                        indicadorProductoBoletaBL.ObtenerPorProductoOrganizacion(IdProducto,
                                                                                 seguridad.Usuario.Organizacion.
                                                                                     OrganizacionID, EstatusEnum.Activo);

                    if (indicadoresBoleta != null)
                    {
                        List<IndicadorProductoInfo> indicadores = (from ind in indicadoresBoleta
                                                                   select ind.IndicadorProducto).ToList();
                        resultado.Resultado = true;
                        resultado.Control = indicadores;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return resultado;
        }

        /// <summary>
        /// The obtener grado porcentual.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion ObtenerGradoPorcentual()
        {
            var resultado = new ResultadoValidacion();

            try
            {
                var parametroPl = new ParametroGeneralPL();

                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    ParametroGeneralInfo parametro = parametroPl.ObtenerPorClaveParametro(ParametrosEnum.GradoPorcentualPerdidaHumedad.ToString());
                    resultado.Resultado = true;
                    resultado.Control = parametro;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el ticket de un registro de vigilancia
        /// </summary>
        /// <param name="registroVigilanciaID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoValidacion ObtenerHumedad(int registroVigilanciaID)
        {
            if (aplicaCapturaHumedad)
            {
                var resultado = new ResultadoValidacion();
                RegistroVigilanciaHumedadInfo registroVigilanciaHumedad = null;

                var registroVigilanciaPL = new RegistroVigilanciaPL();
                try
                {
                    var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                    if (seguridad != null)
                    {
                        var registroVigilancia = new RegistroVigilanciaInfo
                                                                        {
                                                                            RegistroVigilanciaId = registroVigilanciaID
                                                                        };

                        registroVigilancia = registroVigilanciaPL.ObtenerRegistroVigilanciaPorId(registroVigilancia);

                        if (registroVigilancia != null)
                        {
                            if(ProductosHumedad == null || !ProductosHumedad.Any())
                            {
                                resultado.Resultado = false;
                                resultado.CodigoMensaje = 1;
                                return resultado;
                            }


                            if (ProductosHumedad.Contains(registroVigilancia.Producto.ProductoId))
                            {
                                var registroVigilanciaHumedadBL = new RegistroVigilanciaHumedadBL();

                                registroVigilanciaHumedad =
                                    registroVigilanciaHumedadBL.ObtenerPorRegistroVigilanciaID(
                                        registroVigilanciaID);
                                
                                if(registroVigilanciaHumedad == null)
                                {
                                    resultado.Resultado = false;
                                    resultado.CodigoMensaje = 2;
                                    return resultado;
                                }
                                resultado.Resultado = true;
                                resultado.Control = registroVigilanciaHumedad;
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                return resultado;
            }
            return null;
        }
    }
}           