using System;
using System.Web;
using SIE.Base.Vista;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using System.Web.Services;
using SIE.Base.Infos;

namespace SIE.Web.Calidad
{
    public partial class CalidadPaseProceso : PageBase
    {
        #region PAGE LOAD
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion PAGE LOAD

        #region METODOS WEB

        /// <summary>
        /// Obtiene un Pedido por su folio
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoInfo ObtenerFolioPedido(int folioPedido)
        {
            var pedidoPL = new PedidosPL();
            PedidoInfo pedido = null;
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                pedido = pedidoPL.ObtenerPedidosProgramadosPorFolioPedido(folioPedido,
                                                                          usuario.Usuario.Organizacion.OrganizacionID);
            }
            return pedido;
        }

        /// <summary>
        /// Obtiene el producto para su pedido
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="productoID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ProductoInfo ObtenerProducto(int pedidoID, int productoID)
        {
            var productoPL = new ProductoPL();
            ProductoInfo producto = productoPL.ObtenerPorPedidoID(pedidoID, productoID);
            return producto;
        }

        /// <summary>
        /// Obtiene las formulas
        /// </summary>
        /// <param name="formulaID"></param>
        /// <returns></returns>
        [WebMethod]
        public static FormulaInfo ObtenerFormula(int formulaID)
        {
            var formulaPL = new FormulaPL();
            FormulaInfo formula = formulaPL.ObtenerPorFormulaIDCalidadPaseProceso(formulaID);
            return formula;
        }

        /// <summary>
        /// Obtiene los indicadores por pedido y producto
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="productoID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ObtenerIndicadores(int pedidoID, int productoID)
        {
            var indicadorObjetoPL = new IndicadorObjetivoBL();
            int organizacionID = 0;
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
            }
            string indicadorObjetivoSemaforo = indicadorObjetoPL.ObtenerSemaforo(pedidoID, productoID, organizacionID);
            return indicadorObjetivoSemaforo;
        }

        /// <summary>
        /// Valida el indicador, para pintar el semaforo
        /// </summary>
        /// <param name="valorIndicador"></param>
        /// <param name="descripcionIndicador"></param>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ValidarIndicadores(string valorIndicador, string descripcionIndicador, string indicadores)
        {
            var indicadorObjetoPL = new IndicadorObjetivoBL();
            string indicadorObjetivoSemaforo = indicadorObjetoPL.ValidarIndicadores(Convert.ToDecimal(valorIndicador), descripcionIndicador,
                                                                                    indicadores);
            return indicadorObjetivoSemaforo;
        }

        /// <summary>
        /// Guarda los indicadores para la materia prima
        /// </summary>
        /// <param name="indicadoresMateriaPrima"></param>
        /// <param name="observaciones"></param>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Guardar(string indicadoresMateriaPrima, string observaciones, int movimiento)
        {
            var calidadMateriaPrimaBL = new CalidadMateriaPrimaBL();
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int usuarioID = 0;
            if (usuario != null)
            {
                usuarioID = usuario.Usuario.UsuarioID;
            }
            calidadMateriaPrimaBL.GuardarCalidadPaseProceso(indicadoresMateriaPrima, observaciones, movimiento,
                                                            usuarioID);
            return "OK";
        }

        /// <summary>
        /// Obtiene los productos para su ayuda
        /// </summary>
        /// <param name="productoDescripcion"></param>
        /// <param name="pedidoID"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<ProductoInfo> ObtenerProductos(string productoDescripcion, int pedidoID)
        {
            var pagina = new PaginacionInfo
                             {
                                 Inicio = 1,
                                 Limite = 100000
                             };
            var producto = new ProductoInfo
                               {
                                   ProductoDescripcion = productoDescripcion
                               };
            var productoPL = new ProductoPL();
            ResultadoInfo<ProductoInfo> productos = productoPL.ObtenerPorPedidoPaginado(pagina, producto, pedidoID);
            var resultado = new List<ProductoInfo>();
            if (productos != null)
            {
                resultado.AddRange(productos.Lista);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los folios para la ayuda
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<PedidoInfo> ObtenerFolios(string almacen)
        {            
            var pagina = new PaginacionInfo
            {
                Inicio = 1,
                Limite = 100000
            };
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionID = 0;
            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
            }
            var pedido = new PedidoInfo
                             {
                                 Almacen = new AlmacenInfo
                                               {
                                                   Descripcion = almacen,
                                               },
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = organizacionID
                                                    },
                                 Activo = EstatusEnum.Activo
                             };
            var pedidosPL = new PedidosPL();
            ResultadoInfo<PedidoInfo> folios = pedidosPL.ObtenerPorFolioProgramadoPaginado(pagina, pedido);
            var resultado = new List<PedidoInfo>();
            if (folios != null)
            {
                resultado.AddRange(folios.Lista);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las formulas para la ayuda
        /// </summary>
        /// <param name="descripcionFormula"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<FormulaInfo> ObtenerFormulaPagina(string descripcionFormula)
        {
            var pagina = new PaginacionInfo
            {
                Inicio = 1,
                Limite = 100000
            };
            var formulaPL = new FormulaPL();
            var formula = new FormulaInfo
                              {
                                  Descripcion = descripcionFormula,
                                  Activo = EstatusEnum.Activo
                              };
            ResultadoInfo<FormulaInfo> formulas = formulaPL.ObtenerPorPaseCalidadPaginado(pagina, formula);
            var resultado = new List<FormulaInfo>();
            if (formulas != null)
            {
                resultado.AddRange(formulas.Lista);
            }
            return resultado;
        }
        
        #endregion METODOS WEB
    }
}
