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
using SIE.Services.Servicios.PL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class AutorizarCierreDiaInventarioPA : PageBase
    {
        private static readonly List<int> AlmacenesValidos = new List<int> { TipoAlmacenEnum.MateriasPrimas.GetHashCode(), TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode() };
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obtiene los Almacenes de Planta de Alimentos y Materia Prima
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AlmacenesCierreDiaInventarioPAModel> ObtenerAlmacenes()
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                var almacenPL = new AlmacenPL();
                List<AlmacenesCierreDiaInventarioPAModel> listaAlmacen =
                    almacenPL.ObtenerAlmacenesOrganizacion(seguridad.Usuario.Organizacion.OrganizacionID);
                List<AlmacenesCierreDiaInventarioPAModel> listaFiltrada =
                    listaAlmacen.Where(tipo => AlmacenesValidos.Contains(tipo.TipoAlmacenID)).ToList();
                return listaFiltrada;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los Movimientos pendientes por autorizar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<MovimientosAutorizarCierreDiaPAModel> ObtenerMovimientosPendientesAutorizar(
            FiltrosAutorizarCierreDiaInventarioPA filtrosAutorizarCierreDia)
        {
            try
            {
                var almacenMovimientoPL = new AlmacenMovimientoPL();
                List<MovimientosAutorizarCierreDiaPAModel> listaPendientesAutorizar =
                    almacenMovimientoPL.ObtenerMovimientosPendientesAutorizar(filtrosAutorizarCierreDia);
                return listaPendientesAutorizar;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Guarda los Movimientos Autorizados, o cancelados dependiendo si se seleccionaron los registros para autorizar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void GuardarAutorizarCierreDia(
            CierreDiaInventarioPAInfo cierreDiaInventarioPa)
        {
            try
            {
                var almacenMovimientoPL = new AlmacenMovimientoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                cierreDiaInventarioPa.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                cierreDiaInventarioPa.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                almacenMovimientoPL.GuardarAutorizarCierreDiaInventarioPA(cierreDiaInventarioPa);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}