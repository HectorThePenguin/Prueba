using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System.Web.Services;
using utilerias;

namespace SIE.Web.Alimentacion
{

    public partial class ServicioAlimetoCorrales : PageBase
    {
        #region Eventos

        private static int organizacionID;
        private static int usuarioID;

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
            usuarioID = usuario.Usuario.UsuarioID;
            if (!Page.IsPostBack)
            {
                CargarFormulas();
            }
        }
       /* protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigoCorral.Text = string.Empty;
            txtKgsProgramados.Text = string.Empty;
            ddlFormula.SelectedIndex = -1;
        }*/
        #endregion
        #region Metodos

        private void CargarFormulas()
        {
            FormulaPL formulaPL = new FormulaPL();
            var formulas = formulaPL.ObtenerTodos();
            if (formulas != null)
            {
                formulas = formulas.Where(registro => registro.TipoFormula.TipoFormulaID == TipoFormula.Inicio.GetHashCode() ||
                                           registro.TipoFormula.TipoFormulaID == TipoFormula.Finalizacion.GetHashCode() ||
                                           registro.TipoFormula.TipoFormulaID == TipoFormula.Retiro.GetHashCode()).ToList();
                ddlFormula.DataSource = formulas;
                ddlFormula.DataBind();
            }
        }
        /// <summary>
        /// Metodo para obtener información diaria del servicio alimento.
        /// </summary>
        [WebMethod(EnableSession = true)]
        public static Response<List<ServicioAlimentoInfo>> ObtenerInformacionDiariaAlimento()
        {
            try
            {
                //TODO: OrganizacionID pendiente
                var servicioAlimento = new ServicioAlimentoPL();
                IList<ServicioAlimentoInfo> servicioAlimentos =
                    servicioAlimento.ObtenerInformacionDiariaAlimento(organizacionID);
                HttpContext.Current.Session.Add("DatosImprimir", servicioAlimentos);
                if (servicioAlimentos != null)
                {
                    return Response<List<ServicioAlimentoInfo>>.CrearResponse(true, servicioAlimentos.ToList());
                }
                return Response<List<ServicioAlimentoInfo>>.CrearResponseVacio<List<ServicioAlimentoInfo>>(false,
                    "NOEXISTE");
            }
            catch (Exception ex)
            {
                return Response<List<ServicioAlimentoInfo>>.CrearResponseVacio<List<ServicioAlimentoInfo>>(false, ex.Message);
            }
        }
        /// <summary>
        /// Metodo para validar si existe el corral
        /// </summary>
        [WebMethod]
        public static Response<CorralInfo> ValidarCodigoCorral(string value)
        {
            try
            {
                var values = Utilerias.Deserializar<ServicioAlimentoInfo>(value);
                //TODO: OrganizacionID pendiente
                CorralPL corralPL = new CorralPL();
                CorralInfo corral = corralPL.ObtenerValidacionCorral(organizacionID, values.CodigoCorral);
                if (corral != null)
                {
                    return Response<CorralInfo>.CrearResponse(true, corral, "EXITO");
                }
                return Response<CorralInfo>.CrearResponseVacio<CorralInfo>(false,
                    "NOEXISTE");
            }
            catch (Exception ex)
            {
                return Response<CorralInfo>.CrearResponseVacio<CorralInfo>(false, ex.Message);
            }
        }
        /// <summary>
        /// Metodo para guardar informacion alimentos.
        /// </summary>
        [WebMethod]
        public static Response<List<ServicioAlimentoInfo>> GuardarInformacionServicioAlimento(string value)
        {
            try
            {
                var values = Utilerias.Deserializar<ServicioAlimentoInfo>(value);
                var servicioAlimentoPL = new ServicioAlimentoPL();
                
                foreach (var t in values.ListaServicioAlimentoInfos)
                {
                    t.OrganizacionID = organizacionID;
                    t.UsuarioCreacionID = usuarioID;
                    t.UsuarioModificacionID = usuarioID;
                }
                servicioAlimentoPL.Guardar(values.ListaServicioAlimentoInfos);
                return Response<List<ServicioAlimentoInfo>>.CrearResponseVacio<List<ServicioAlimentoInfo>>(true,
                    "GuardadoExito");
            }
            catch (Exception ex)
            {
                return Response<List<ServicioAlimentoInfo>>.CrearResponseVacio<List<ServicioAlimentoInfo>>(false, ex.Message);
            }
        }
        #endregion
    }
}