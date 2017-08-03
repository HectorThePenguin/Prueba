using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Enums;

namespace SIE.Web.Manejo
{
    public partial class ProgramacionSacrificio : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            InitUserData();
            ObtenerOrdenesSacrificio();
            paginado.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Inicializacion de datos de usuario
        /// </summary>
        private void InitUserData()
        {
            int rolIdUsuario = 0;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    if (seguridad.Usuario.Operador != null)
                    {
                        rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;
                        txtFecha.Text = DateTime.Now.ToShortDateString();
                        txtHora.Text = DateTime.Now.ToShortTimeString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeUsuario();", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// Metodo para obtener las ordenes de sacrificio
        [WebMethod]
        public void ObtenerOrdenesSacrificio()
        {
            try
            {
                var ordenSacrificio = new OrdenSacrificioPL();
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    var ordenSacrificioDia = (new OrdenSacrificioInfo
                    {
                        OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID,
                        FechaOrden = DateTime.Today,
                        EstatusID = (int)Estatus.OrdenSacrificioPendiente
                    });

                    var orden = ordenSacrificio.ObtenerOrdenSacrificioDelDia(ordenSacrificioDia);

                    if (orden != null)
                    {
                        var listaBusqueda = (from item in orden.DetalleOrden
                                             select new
                                             {
                                                 item.Corraleta.Codigo,
                                                 item.Lote.Lote,
                                                 item.Cabezas,
                                                 item.CabezasASacrificar,
                                                 FechaCreacion = DateTime.Today.Date,
                                                 item.Usuario.Nombre,
                                                 item.Lote.LoteID,
                                                 item.Corraleta.CorralID,
                                                 item.OrdenSacrificioDetalleID
                                             }).ToList();
                        gvBusqueda.DataSource = listaBusqueda;
                        gvBusqueda.DataBind();
                    }
                    else
                    {
                        gvBusqueda.DataSource = new List<OrdenSacrificioInfo>();
                        gvBusqueda.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
               
            }
        }

        //Metodo para obtener la existencia del animal
        [WebMethod]
        public static AnimalInfo ObtenerExistenciaAnimal(string arete, int loteID)
        {
            var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
            var programacionSacrificioPL = new ProgramacionSacrificioPL();
            var animalInfo = new AnimalInfo{
                Arete = arete,
                OrganizacionIDEntrada = seguridad.Usuario.Organizacion.OrganizacionID
            };

            var resultado = programacionSacrificioPL.ObtenerExistenciaAnimal(animalInfo, loteID);
            if (resultado==null)
            {
                var animalPl = new AnimalPL();
                /* Validar Si el arete existe en el inventario Como Carga Inicial*/
                var animalCargaInicial = animalPl.ObtenerAnimalPorArete(arete, seguridad.Usuario.Organizacion.OrganizacionID);
                if (animalCargaInicial != null && animalCargaInicial.CargaInicial)
                {
                    resultado = animalCargaInicial;
                }
            }

            return resultado;
        }

        //Metodo para guardar las salidas
        [WebMethod]
        public static int GuardarSalida(List<AnimalInfo> animalInfo, int loteID, int corraletaID, int ordenSacrificioDetalleID)
        {
            var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
            var organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
            var usuarioID = seguridad.Usuario.UsuarioID;
            var programacionSacrificioGuardadoInfo = new ProgramacionSacrificioGuardadoInfo
            {
                OrganizacionID = organizacionID,
                TipoMovimiento = (int)TipoMovimiento.SalidaPorSacrificio,
                UsuarioID = usuarioID,
                LoteID = loteID,
                CorraletaID = corraletaID,
                OrdenSacrificioDetalleID = ordenSacrificioDetalleID
            };
            var programacionSacrificioPL = new ProgramacionSacrificioPL();
            return programacionSacrificioPL.GuardarAnimalSalida(animalInfo, programacionSacrificioGuardadoInfo);
        }

        /// <summary>
        /// Metodo para obtener los aretes disponibles
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AnimalInfo> ObtenerAnimalesPorLoteID(int loteID)
        {
            List<AnimalInfo> lista = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var animalPl = new AnimalPL();

                var loteInfo = new LoteInfo
                {
                    LoteID = loteID,
                    OrganizacionID = organizacionID
                };

                lista = animalPl.ObtenerAnimalesPorLoteID(loteInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
            return lista;
        }

        //Metodo RowDataBound de gvBusqueda
        protected void gvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ;
        }

        //Metodo PageIndexChanging de gvBusqueda
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
        }

        //Metodo PreRender de gvBusqueda
        protected void gvBusqueda_PreRender(object sender, EventArgs e)
        {
            try
            {
                gvBusqueda.UseAccessibleHeader = true;
                gvBusqueda.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}