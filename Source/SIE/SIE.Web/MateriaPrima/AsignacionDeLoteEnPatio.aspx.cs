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
    public partial class AsignacionDeLoteEnPatio : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.AlmacenUsuario != null)
                {
                    txtAlmacenUsuario.Value = seguridad.Usuario.AlmacenUsuario.Almacen.TipoAlmacenID.ToString();
                }

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
        /// Método para consultar el lote tecleado.
        /// </summary>
        /// <param name="parametroLoteMateriaPrima"></param>
        /// <returns></returns>
        [WebMethod]
        public static RespuestaInventarioLoteInfo ObtenerLotePorTipoAlmacen(ParametroRecepcionMateriaPrimaLote parametroLoteMateriaPrima)
        {
            try
            {
                RespuestaInventarioLoteInfo listadoLotes = null;
                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0, tipoAlmacen = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                switch (parametroLoteMateriaPrima.TipoAlmacen)
                {
                    case "rbLoteAlmacen":
                        tipoAlmacen = (int)TipoAlmacenEnum.MateriasPrimas;
                        break;
                    case "rbLoteProceso":
                        tipoAlmacen = (int)TipoAlmacenEnum.PlantaDeAlimentos;
                        break;
                    case "rbBodegaExterna":
                        tipoAlmacen = (int)TipoAlmacenEnum.BodegaExterna;
                        break;
                }

                var almacenInventarioLote = almacenInventarioLotePl
                    .ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(
                        new ParametrosOrganizacionTipoAlmacenProductoActivo
                        {
                            OrganizacionId = organizacionId,
                            ProductoId = parametroLoteMateriaPrima.ProductoId,
                            TipoAlmacenId = tipoAlmacen
                        });

                if (almacenInventarioLote != null)
                {
                    foreach (var almacenInventario in almacenInventarioLote.Where(almacen => almacen.Lote == parametroLoteMateriaPrima.Lote))
                    {
                        var almacenPl = new AlmacenPL();
                        var almacen = almacenPl.ObtenerPorID(almacenInventario.AlmacenInventario.AlmacenID);

                        listadoLotes = new RespuestaInventarioLoteInfo
                        {
                            AlmacenInventarioLoteId = almacenInventario.AlmacenInventarioLoteId,
                            AlmacenId = almacenInventario.AlmacenInventario.AlmacenID,
                            CodigoAlmacen = almacen.CodigoAlmacen,
                            Lote = almacenInventario.Lote,
                            Cantidad = almacenInventario.Cantidad
                        };
                    }
                }
                return listadoLotes;
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
        public static bool ActualizarLoteEntradaProducto(EntradaProductoInfo entrada)
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
                return entradaProductoPl.ActualizaLoteEnPatio(entrada);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para obtener los lotes por el tipo de almacen
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="tipoalmacen"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<RespuestaInventarioLoteInfo> ObtenerLotesPorTipoAlmacen(int productoId, string tipoalmacen)
        {
            try
            {
                List<RespuestaInventarioLoteInfo> listadoLotes = null;
                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0, tipoAlmacen = 0;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                switch (tipoalmacen)
                {
                    case "rbLoteAlmacen":
                        tipoAlmacen = (int)TipoAlmacenEnum.MateriasPrimas;
                        break;
                    case "rbLoteProceso":
                        tipoAlmacen = (int)TipoAlmacenEnum.PlantaDeAlimentos;
                        break;
                    case "rbBodegaExterna":
                        tipoAlmacen = (int)TipoAlmacenEnum.BodegaExterna;
                        break;
                }

                var almacenInventarioLote = almacenInventarioLotePl.ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(
                new ParametrosOrganizacionTipoAlmacenProductoActivo
                {
                    OrganizacionId = organizacionId,
                    ProductoId = productoId,
                    TipoAlmacenId = tipoAlmacen
                });

                if (almacenInventarioLote != null)
                {
                    listadoLotes = new List<RespuestaInventarioLoteInfo>();
                    foreach (var almacenInventario in almacenInventarioLote)
                    {
                        var almacenPl = new AlmacenPL();
                        var almacen = almacenPl.ObtenerPorID(almacenInventario.AlmacenInventario.AlmacenID);

                        listadoLotes.Add(new RespuestaInventarioLoteInfo
                        {
                            AlmacenInventarioLoteId = almacenInventario.AlmacenInventarioLoteId,
                            AlmacenId = almacenInventario.AlmacenInventario.AlmacenID,
                            CodigoAlmacen = almacen.CodigoAlmacen,
                            Lote = almacenInventario.Lote,
                            Cantidad = almacenInventario.Cantidad
                        });
                    }
                }
                return listadoLotes;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
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
        /// Metodo para generar un lote nuevo dependiendo del tipo de lote seleccionado.
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="tipoalmacen"></param>
        /// <returns></returns>
        [WebMethod]
        public static AlmacenInventarioLoteInfo ObtenerNuevoLoteMateriaPrima(int productoId, string tipoalmacen)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionId = 0, usuarioId = 0, tipoAlmacen = 0;

                AlmacenInventarioLoteInfo almacenInventarioLote = null;

                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    usuarioId = seguridad.Usuario.UsuarioID;
                }

                switch (tipoalmacen)
                {
                    case "rbLoteAlmacen":
                        tipoAlmacen = (int)TipoAlmacenEnum.MateriasPrimas;
                        break;
                    case "rbLoteProceso":
                        tipoAlmacen = (int)TipoAlmacenEnum.PlantaDeAlimentos;
                        break;
                    case "rbBodegaExterna":
                        tipoAlmacen = (int)TipoAlmacenEnum.BodegaExterna;
                        break;
                }

                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                var almacenPl = new AlmacenPL();
                var almacenInventarioPl = new AlmacenInventarioPL();

                var almacenInventario =
                    almacenPl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(new ParametrosOrganizacionTipoAlmacenProductoActivo
                    {
                        OrganizacionId = organizacionId,
                        TipoAlmacenId = tipoAlmacen,
                        Activo = (int)EstatusEnum.Activo,
                        ProductoId = productoId
                    });

                // Si el producto no se encuentra en el almacen inventario, lo insertamos
                if (almacenInventario == null)
                {
                    var listaAlmacenOrganizacion = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionId);
                    if (listaAlmacenOrganizacion != null)
                    {
                        // Obtenemos el almacen y validamos que sea del mismo tipo Almacen
                        foreach (AlmacenInfo almacenInfo in listaAlmacenOrganizacion)
                        {
                            // Aqui se valida que el almacen sea del tipo seleccionado en pantalla
                            if (almacenInfo.TipoAlmacen.TipoAlmacenID == tipoAlmacen)
                            {
                                almacenInventario = new AlmacenInventarioInfo
                                {
                                    AlmacenInventarioID =
                                        almacenInventarioPl.Crear(new AlmacenInventarioInfo
                                        {
                                            AlmacenID = almacenInfo.AlmacenID,
                                            ProductoID = productoId,
                                            UsuarioCreacionID = usuarioId
                                        }),
                                    AlmacenID = almacenInfo.AlmacenID
                                };
                                break;
                            }
                        }
                    }
                }

                if (almacenInventario != null)
                {
                    int loteIdCreado = almacenInventarioLotePl.Crear(new AlmacenInventarioLoteInfo
                    {
                        AlmacenInventarioLoteId = 0,
                        AlmacenInventario =
                            new AlmacenInventarioInfo { AlmacenInventarioID = almacenInventario.AlmacenInventarioID },
                        Cantidad = 0,
                        PrecioPromedio = 0,
                        Piezas = 0,
                        Importe = 0,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionId = usuarioId,
                    }, new AlmacenInventarioInfo
                    {
                        AlmacenID = almacenInventario.AlmacenID,
                        ProductoID = productoId
                    });

                    almacenInventarioLote = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(loteIdCreado);

                }

                return almacenInventarioLote;
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
                                entrada.Activo == EstatusEnum.Activo && entrada.PesoBruto > 0 && entrada.FechaFinDescarga.Year == 1 && entrada.AlmacenInventarioLote.AlmacenInventarioLoteId == 0).ToList();
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
    }
}