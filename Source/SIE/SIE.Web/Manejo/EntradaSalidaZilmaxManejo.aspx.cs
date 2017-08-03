using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SIE.Base.Exepciones;
using SIE.Base.Vista;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Manejo
{
    public partial class EntradaSalidaZilmaxManejo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            datepicker.Text = String.Concat(DateTime.Now.Day.ToString().PadLeft(2, '0'), "/",
                                            DateTime.Now.Month.ToString().PadLeft(2, '0'), "/",
                                            DateTime.Now.Year.ToString());
        }

        /// <summary>
        /// Método para consultar Entradas a Zilmax
        /// </summary>
        /// <returns>Regresa una lista de los corrales entrantes a Zilmax</returns>
        [WebMethod]
        public static IList<ReporteEntradaSalidaZilmaxInfo> TraerEntradaSalidaZilmaxManejo()
        {
            IList<ReporteEntradaSalidaZilmaxInfo> entradaZilmax = null;
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var entradaSalidaZilmaxPL = new EntradaSalidaZilmaxPL();
                    var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    entradaZilmax = entradaSalidaZilmaxPL.ObtenerEntrantesSalidaZilmaxTodos(organizacionId);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return entradaZilmax;
        }

        /// <summary>
        /// Actualiza la fecha de entrada/salida a zilmax
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GuardarEntradaSalidaZilmaxManejo(List<LoteInfo> lotesZilmax)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var lotePL = new LotePL();
                    lotesZilmax.ForEach(usu => usu.UsuarioModificacionID = seguridad.Usuario.UsuarioID);
                    lotePL.GuardarEntradaSalidaZilmax(lotesZilmax);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return "Ok";
        }
    }
}
