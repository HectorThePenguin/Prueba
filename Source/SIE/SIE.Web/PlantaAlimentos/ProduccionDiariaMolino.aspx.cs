using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class ProduccionDiariaMolino : PageBase
    {
        private static List<int> productosForraje;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Value = DateTime.Now.Date.ToShortDateString();
            CargarProductosForrajes();
        }

        /// <summary>
        /// Carga los productos de forraje validos
        /// </summary>
        private void CargarProductosForrajes()
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametro =
                parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.ProductosForraje.ToString());
            if (parametro.Valor.Contains('|'))
            {
                productosForraje = (from tipos in parametro.Valor.Split('|')
                                    select Convert.ToInt32(tipos)).ToList();
            }
            else
            {
                int forraje = Convert.ToInt32(parametro.Valor);
                productosForraje.Add(forraje);
            }
        }

        /// <summary>
        /// Obtiene los Turnos de la produccion diaria molino
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<TurnoModel> ObtenerTurnos()
        {
            try
            {
                var turnos = new List<TurnoModel>
                {
                    new TurnoModel
                        {
                            TurnoID = 1,
                            Descripcion = "UNO"
                        },
                    new TurnoModel
                        {
                            TurnoID = 2,
                            Descripcion = "DOS"
                        }
                    ,
                    new TurnoModel
                        {
                            TurnoID = 3,
                            Descripcion = "TRES"
                        }
                };
                return turnos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Especificaciones de Forraje
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<EspecificacionForrajeModel> ObtenerEspecificacionForraje()
        {
            try
            {
                var turnos = new List<EspecificacionForrajeModel>
                {
                    new EspecificacionForrajeModel
                        {
                            EspecificacionForrajeID = 1,
                            Descripcion = "Pacas Grandes"
                        },
                    new EspecificacionForrajeModel
                        {
                            EspecificacionForrajeID = 2,
                            Descripcion = "Pacas Chicas"
                        }
                    ,
                    new EspecificacionForrajeModel
                        {
                            EspecificacionForrajeID = 3,
                            Descripcion = "Rollos"
                        }
                };
                return turnos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las Causas de Tiempo Muerto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<CausaTiempoMuertoInfo> ObtenerCausasTiempoMuerto()
        {
            try
            {
                var causaTiempoMuertoBL = new CausaTiempoMuertoBL();
                List<CausaTiempoMuertoInfo> listaCausas = causaTiempoMuertoBL.ObtenerPorTipoCausa(TipoCausa.ProduccionDiaria.GetHashCode()).ToList();
                return listaCausas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los Productos con la ayuda
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ProductoInfo> ObtenerProductos(ProductoInfo productoInfo)
        {
            try
            {
                var productoPL = new ProductoPL();
                productoInfo.SubfamiliaId = SubFamiliasEnum.Forrajes.GetHashCode();
                List<ProductoInfo> listaProductos = productoPL.ObtenerPorSubFamilia(productoInfo);

                List<ProductoInfo> productosValidos =
                    listaProductos.Where(pro => productosForraje.Contains(pro.ProductoId)).ToList();
                return productosValidos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los Tickets con la ayuda
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<FiltroTicketInfo> ObtenerTickets(FiltroTicketInfo filtroTicket)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroTicket.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var pesajeMateriaPrimaBL = new PesajeMateriaPrimaBL();
                List<FiltroTicketInfo> listaTickets = pesajeMateriaPrimaBL.ObtenerPorFiltro(filtroTicket);

                List<FiltroTicketInfo> listaTicketsValidos =
                    listaTickets.Where(ticket => productosForraje.Contains(ticket.ProductoID)).ToList();

                return listaTicketsValidos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los valores adicionales del producto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<FiltroProductoProduccionMolino> ObtenerValoresProduccionMolino(ProductoInfo productoInfo)
        {
            try
            {
                var productoPL = new ProductoPL();
                List<FiltroProductoProduccionMolino> valoresProduccion = productoPL.ObtenerValoresProduccionMolino(productoInfo);
                return valoresProduccion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los valores adicionales del ticket
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<FiltroTicketProduccionMolino> ObtenerValoresTicketProduccion(FiltroTicketInfo filtroTicket)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroTicket.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                filtroTicket.IndicadorID = IndicadoresEnum.Humedad.GetHashCode();
                var pesajeMateriaPrimaBL = new PesajeMateriaPrimaBL();
                List<FiltroTicketProduccionMolino> listaTickets = pesajeMateriaPrimaBL.ObtenerValoresTicketProduccion(filtroTicket);
                return listaTickets;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Valida el Usuario del Login de Supervisor
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static FiltroUsuarioInfo VerificarUsuario(string usuario, string contrasenia)
        {
            try
            {
                var usuarioPL = new UsuarioPL();
                FiltroUsuarioInfo usuarioActiveDirectory = usuarioPL.ValidarUsuarioCompleto(usuario.Trim(), contrasenia);
                return usuarioActiveDirectory;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para guardar 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void Guardar(ProduccionDiariaInfo produccionDiaria)
        {
            try
            {
                var produccionDiariaBL = new ProduccionDiariaBL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                produccionDiariaBL.Guardar(produccionDiaria, seguridad.Usuario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}