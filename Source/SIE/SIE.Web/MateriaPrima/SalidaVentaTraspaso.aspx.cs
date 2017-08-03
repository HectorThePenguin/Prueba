using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Windows.Documents;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;

namespace SIE.Web.MateriaPrima
{
    public partial class SalidaVentaTraspaso : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString().ToString(CultureInfo.InvariantCulture);
            txtSubFamiliaForraje.Value = Convert.ToString((int)SubFamiliasEnum.Forrajes);
            txtTipoMovimiento.Value = Convert.ToString((int) TipoMovimiento.ProductoSalidaVenta);
            txtAlmacenMateriaPrima.Value = Convert.ToString((int) TipoAlmacenEnum.MateriasPrimas);
            txtAlmacenPlantaAlimento.Value = Convert.ToString((int) TipoAlmacenEnum.PlantaDeAlimentos);
            txtAlmacenBodegaExterno.Value = Convert.ToString((int) TipoAlmacenEnum.BodegaDeTerceros);
            txtAlmacenBodegaTercero.Value = Convert.ToString((int) TipoAlmacenEnum.BodegaExterna);
        }

        /// <summary>
        /// Obtiene una salida de producto por folio
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static SalidaProductoInfo ObtenerFolioSalida(int folio)
        {
            SalidaProductoInfo salidaProducto = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var salidaProductoPl = new SalidaProductoPL();
                    var salidaProductoConsulta = new SalidaProductoInfo();
                    salidaProductoConsulta.FolioSalida = folio;
                    salidaProductoConsulta.Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID
                    };
                    salidaProductoConsulta.Activo = EstatusEnum.Activo;

                    salidaProducto = salidaProductoPl.ObtenerPorFolioSalida(salidaProductoConsulta);

                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return salidaProducto;
        }

        /// <summary>
        /// Obtiene los almacenes.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AlmacenInfo> ObtenerAlmacenes()
        {
            List<AlmacenInfo> listaAlmacen = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var almacenPl = new AlmacenPL();

                   List<TipoAlmacenEnum> tiposAlmacen = new List<TipoAlmacenEnum>();
                    tiposAlmacen.Add(TipoAlmacenEnum.MateriasPrimas);
                    tiposAlmacen.Add(TipoAlmacenEnum.BodegaDeTerceros);
                    tiposAlmacen.Add(TipoAlmacenEnum.PlantaDeAlimentos);
                    tiposAlmacen.Add(TipoAlmacenEnum.BodegaExterna);

                    OrganizacionInfo organizacion = seguridad.Usuario.Organizacion;

                    listaAlmacen = almacenPl.ObtenerAlmacenPorTiposAlmacen(tiposAlmacen, organizacion);

                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return listaAlmacen;
        }


        /// <summary>
        /// Obtiene los productos disponibles en un almacen.
        /// </summary>
        /// <param name="idAlmacen"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<AlmacenInventarioInfo> ObtenerProductosPorAlmacen(int idAlmacen)
        {
            IList<AlmacenInventarioInfo> listaAlmacenInventario = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var almacenInventarioPl = new AlmacenInventarioPL();
                    var almacenPl = new AlmacenPL();

                    AlmacenInfo almacen = almacenPl.ObtenerPorID(idAlmacen);
                    
                    OrganizacionInfo organizacion = seguridad.Usuario.Organizacion;

                    listaAlmacenInventario = almacenInventarioPl.ObtienePorAlmacenIdLlenaProductoInfo(almacen);

                    if (listaAlmacenInventario != null && listaAlmacenInventario.Count > 0)
                    {

                        listaAlmacenInventario = (from productos in listaAlmacenInventario
                                                  where productos.Producto != null 
                                                   orderby productos.Producto.Descripcion
                                                   select productos).ToList();

                        if (almacen.TipoAlmacen.TipoAlmacenID == (int)TipoAlmacenEnum.PlantaDeAlimentos)
                        {
                            listaAlmacenInventario = (from productos in listaAlmacenInventario
                                                    where productos.Producto.SubFamilia.SubFamiliaID != (int) SubFamiliasEnum.AlimentoFormulado 
                                                    orderby productos.Producto.Descripcion
                                                    select productos).ToList();
                        }
                        
                    }
                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return listaAlmacenInventario;

        }

        /// <summary>
        /// Obtiene los lotes en los que se localiza un producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idAlmacen"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<AlmacenInventarioLoteInfo> ObtenerLotes(int idProducto,int idAlmacen)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var almacenPl = new AlmacenInventarioLotePL();

                    AlmacenInfo almacen = new AlmacenInfo() { AlmacenID = idAlmacen };
                    ProductoInfo producto = new ProductoInfo(){ProductoId = idProducto};

                    listaLotes = almacenPl.ObtenerPorAlmacenProducto(almacen, producto);

                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return listaLotes;

        }

        /// <summary>
        /// Obtiene la cantidad de piezas existentes en lotes para validar la cantidad que se entrega.
        /// </summary>
        /// <param name="idInventarioLote"></param>
        /// <returns></returns>
        [WebMethod]
        public static AlmacenInventarioLoteInfo ObtenerCantidadPiezas(int idInventarioLote)
        {
            AlmacenInventarioLoteInfo lote = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var almacenPl = new AlmacenInventarioLotePL();

                    lote = almacenPl.ObtenerAlmacenInventarioLotePorId(idInventarioLote);

                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return lote;
        }

        /// <summary>
        /// Actualiza en salida producto los datos de almacen, lote y piezas.
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ActualizaSalida(SalidaProductoInfo salidaProducto)
        {
            bool resultado = true;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var salidaProductoPl = new SalidaProductoPL();
                    salidaProductoPl.ActualizarAlmacenInventarioLote(salidaProducto);
                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return resultado;
        }

        [WebMethod]
        public static List<SalidaProductoInfo> ConsultaFoliosActivos(int folioSalida)
        {
            List<SalidaProductoInfo> listaFolios = null;

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var salidaProductoPl = new SalidaProductoPL();
                    var salida = new SalidaProductoInfo()
                    {
                        FolioSalida = folioSalida,
                        Activo = EstatusEnum.Activo,
                        Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = seguridad.Usuario.OrganizacionID
                        }
                    };

                    listaFolios = salidaProductoPl.ObtenerTraspasoFoliosActivos(salida);
                }
                else
                {
                    throw new ExcepcionServicio("SesionExpirada");
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

            return listaFolios;
        } 


    }
}