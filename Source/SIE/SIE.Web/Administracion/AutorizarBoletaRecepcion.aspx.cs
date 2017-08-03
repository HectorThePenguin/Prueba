using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.MateriaPrima
{
    public partial class AutorizarBoletaRecepcion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtAprobado.Text = Estatus.Aprobado.GetHashCode().ToString();
            txtRechazado.Text = Estatus.Rechazado.GetHashCode().ToString();
            txtAutorizado.Text = Estatus.Autorizado.GetHashCode().ToString();
            txtPagoEnGanadera.Text = TipoFleteEnum.PagoenGanadera.GetHashCode().ToString();
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (seguridad != null && seguridad.Usuario.Operador != null)
            {
                txtOperadorID.Text = seguridad.Usuario.Operador.OperadorID.ToString();
            }
            LlenaComboDestino();
            hdnFolioEntrada.Value = string.Empty;
            string folioEntrada = Request["folioEntrada"];
            if (!string.IsNullOrWhiteSpace(folioEntrada))
            {
                hdnFolioEntrada.Value = folioEntrada;
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
            List<ProveedorInfo> listaProveedores = new List<ProveedorInfo>();

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
                    EntradaProductoInfo entrada = new EntradaProductoInfo { Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }, Estatus = new EstatusInfo { EstatusId = (int)Estatus.PendienteAutorizar } };
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
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaEntradaProductoPendiente;
        }

        [WebMethod]
        public static dynamic ObtenerFoliosAutorizados(int folio)
        {
            dynamic resultado = new {};
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var entradaProductoPL = new EntradaProductoPL();
                    int organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                    EntradaProductoInfo entradaProducto = entradaProductoPL.ObtenerEntradaProductoPorFolio(folio,
                                                                                                           organizacionID);
                    if (entradaProducto != null)
                    {
                        resultado = new {EntradaProducto = entradaProducto};
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
            return resultado;
        }

        [WebMethod]
        public static void ActualizarRevisionGerente(int entradaProductoId)
        {
            try
            {
                var entradaProductoPL = new EntradaProductoPL();
                entradaProductoPL.ActualizaRevisionGerente(entradaProductoId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
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
                        if (entradaProducto.Activo != EstatusEnum.Activo)
                        {
                            entradaProducto = null;
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
        /// Obtiene la diferencias de indicadores por el ID de Entrada
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<DiferenciasIndicadoresMuestraContrato> ObtenerDiferenciasIndicadores(int entradaProductoId)
        {
            var entradaProductoPL = new EntradaProductoPL();
            List<DiferenciasIndicadoresMuestraContrato> diferenciasIndicadores;

            try
            {
                diferenciasIndicadores = entradaProductoPL.ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(entradaProductoId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }

            return diferenciasIndicadores;
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
            List<ContratoInfo> listaContratos;

            var contratoPL = new ContratoPL();
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    listaContratos = contratoPL.ObtenerContratosPorProveedorId(proveedorId,
                        seguridad.Usuario.Organizacion.OrganizacionID);

                    if (listaContratos != null)
                    {
                        if (listaContratos.Count > 0)
                        {
                            if (listaContratos.Count > 0)
                                if (listaContratos.Select(contrato => contrato.Producto).ToList().Count > 0)
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
                        if (listaContratos.Count > 0)
                            listaContratos =
                                listaContratos.Where(
                                    registro =>
                                        registro.Producto.ProductoId == productoId).ToList();
                        else
                            listaContratos = null;
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
                    contrato = new ContratoInfo();
                    contrato.ContratoId = contratoId;
                    contrato.Organizacion = new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };
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
        /// <param name="estatusId"> </param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo AutorizarRechazarEntrada(EntradaProductoInfo entradaProducto, int estatusId)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    entradaProducto.Organizacion = new OrganizacionInfo { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };
                    entradaProducto.Estatus = new EstatusInfo { EstatusId = estatusId };
                    entradaProducto.UsuarioModificacionID = seguridad.Usuario.UsuarioID;
                    entradaProducto.OperadorAutoriza = new OperadorInfo { OperadorID = seguridad.Usuario.Operador.OperadorID };

                    var entradaProductoPl = new EntradaProductoPL();
                    if (entradaProductoPl.AutorizaEntrada(entradaProducto))
                    {
                        return entradaProducto;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }
    }
}