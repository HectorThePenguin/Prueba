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

namespace SIE.Web.MateriaPrima
{
    public partial class RecepcionMateriaPrimaEnPatio : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                //if (seguridad.Usuario.AlmacenUsuario != null)
                //{
                //    txtAlmacenUsuario.Value = seguridad.Usuario.AlmacenUsuario.Almacen.TipoAlmacenID.ToString();
                //}

                if (seguridad.Usuario.Operador == null)
                {
                    var recursolocal = GetLocalResourceObject("mensajeNoTienesRol");
                    if (recursolocal != null)
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario('" + recursolocal.ToString() + "');", true);
                }
            }
            else
            {
                var localResourceObject = GetLocalResourceObject("mensajeErrorSession");
                if (localResourceObject != null)
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario('" + localResourceObject.ToString() + "');", true);
            }
        }

        /// <summary>
        /// Metodo para obtener los datos del folio capturado
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static EntradaProductoInfo ConsultarEntradaProducto(int folio)
        {
            try
            {
                var entradaProductoPl = new EntradaProductoPL();
                var productoPl = new ProductoPL();

                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                var organizacionId = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                var entradaProducto = entradaProductoPl.ObtenerEntradaProductoPorFolio(folio, organizacionId);
                if (entradaProducto != null)
                {
                    var productoForraje = productoPl.ObtenerProductoForraje(entradaProducto.Producto);
                    if (productoForraje != null)
                    {
                        entradaProducto.Producto.Forraje = true;
                    }
                }

                return entradaProducto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Método que consulta el listado de los productos.
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<EntradaProductoInfo> ConsultarListadoEntradaProducto(int folio)
        {
            try
            {
                var entradaProductoPl = new EntradaProductoPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                var organizacionId = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }
                    
                List<EntradaProductoInfo> listadoFolios = entradaProductoPl.ObtenerEntradaProductosAyuda(organizacionId);
                if (listadoFolios != null)
                {
                    listadoFolios =
                        listadoFolios.Where(
                            entrada =>
                                entrada.Activo == EstatusEnum.Activo && entrada.PesoBruto > 0 && entrada.FechaFinDescarga.Year == 1).ToList();
                    if (listadoFolios.Count == 0)
                    {
                        listadoFolios = null;
                    }
                }

                return listadoFolios;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Método que actualiza el lote, operador almacen y las piezas en caso de que sea forraje.
        /// </summary>
        /// <param name="entrada"></param>
        [WebMethod]
        public static bool ActualizaOperadorFechaDescargaEnPatio(EntradaProductoInfo entrada)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int operadorId = 0;

                if (seguridad != null)
                {
                    operadorId = seguridad.Usuario.Operador.OperadorID;
                }

                entrada.OperadorAlmacen = new OperadorInfo { OperadorID = operadorId };
                var entradaProductoPl = new EntradaProductoPL();
                return entradaProductoPl.ActualizaOperadorFechaDescargaEnPatio(entrada);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Método para actualizar la fecha de inicio y fin de descarga
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <param name="productoId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ActualizaFechaDescargaEnPatio(EntradaProductoInfo entrada)
        {
            try
            {
                bool guardarFecha = true;

                var entradaProductoPl = new EntradaProductoPL();
                var productoPl = new ProductoPL();
                var productoForraje = productoPl.ObtenerProductoForraje(entrada.Producto);
                if (productoForraje != null)
                {
                    var entradaproducto = entradaProductoPl.ObtenerEntradaProductoPorId(entrada.EntradaProductoId);
                    if (entradaproducto != null)
                    {
                        if (entradaproducto.ProductoDetalle != null)
                        {
                            foreach (var entradaDetalle in entradaproducto.ProductoDetalle)
                            {
                                if (entradaDetalle.ProductoMuestras != null)
                                {
                                    if (entradaDetalle.ProductoMuestras.Count < 30)
                                    {
                                        guardarFecha = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (guardarFecha)
                {
                    return entradaProductoPl.ActualizaFechaDescargaPiezasEnPatio(entrada);
                }
                else
                {
                    return "ErrorForraje";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }
    }
}