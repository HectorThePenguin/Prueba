using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class ProduccionFormulas : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargarRotoMix();
            CargarFormulas();
        }
        /// <summary>
        /// Llena el combo de rotomix
        /// </summary>
        private void CargarRotoMix()
        {
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                var formulaPl = new FormulaPL();
                IList<RotoMixInfo> listaRotoMix = formulaPl.ObtenerRotoMixConfiguradas(seguridad.Usuario.Organizacion.OrganizacionID);
                if (listaRotoMix != null && listaRotoMix.Count > 0)
                {
                    listaRotoMix.Add(new RotoMixInfo() { RotoMixId = 0, Descripcion = "Seleccione" });
                    cmbRotoMix.DataSource = listaRotoMix;
                    cmbRotoMix.DataTextField = "Descripcion";
                    cmbRotoMix.DataValueField = "RotoMixId";
                    cmbRotoMix.DataBind();
                    cmbRotoMix.SelectedValue = "0";
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
        }
        /// <summary>
        /// Llena el combo de formulas
        /// </summary>
        private void CargarFormulas()
        {
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                var formulaPl = new FormulaPL();
                IList<FormulaInfo> listaFormula = formulaPl.ObtenerFormulasConfiguradas(EstatusEnum.Activo);

                if (listaFormula != null)
                {
                    if (listaFormula.Count > 0)
                    {
                        listaFormula.Add(new FormulaInfo() { FormulaId = 0, Descripcion = "Seleccione" });
                        cmbFormula.DataSource = listaFormula;
                        cmbFormula.DataTextField = "Descripcion";
                        cmbFormula.DataValueField = "FormulaID";
                        cmbFormula.DataBind();
                        cmbFormula.SelectedValue = "0";
                    }
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
        }
        /// <summary>
        /// Obtiene la lista de ingredientes de una formula
        /// </summary>
        /// <param name="formulaId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IngredienteInfo> ObtenerIngredientesFormula(int formulaId)
        {
            List<IngredienteInfo> listaIngredientes = new List<IngredienteInfo>();

            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var ingredientePl = new IngredientePL();
                    IngredienteInfo ingrediente = new IngredienteInfo();
                    ingrediente.Formula = new FormulaInfo() { FormulaId = formulaId };
                    ingrediente.Organizacion = new OrganizacionInfo() { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID };
                    listaIngredientes = ingredientePl.ObtenerPorFormula(ingrediente, EstatusEnum.Activo);
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

            return listaIngredientes;
        }
        /// <summary>
        /// Guarda la produccion de una formula
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        [WebMethod]
        public static ProduccionFormulaInfo GuardarProduccionFormula(string fecha, int FormulaId, decimal CantidadProducida, List<ProduccionFormulaDetalleInfo> ProduccionFormulaDetalle, int RotoMixID, int Batch)
        {
            ProduccionFormulaInfo retorno = null;
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    ProduccionFormulaInfo produccionFormula = new ProduccionFormulaInfo()
                    {
                        FechaProduccion = Convert.ToDateTime(fecha),
                        Organizacion = new OrganizacionInfo() { OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID },
                        UsuarioCreacionId = seguridad.Usuario.UsuarioID,
                        Formula = new FormulaInfo() { FormulaId = FormulaId },
                        ProduccionFormulaDetalle = ProduccionFormulaDetalle,
                        RotoMixID = RotoMixID,
                        Batch = Batch,
                        CantidadProducida = CantidadProducida
                    };
                    var produccionFormulaPl = new ProduccionFormulaPL();
                    retorno = produccionFormulaPl.GuardarProduccionFormula(produccionFormula);
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }
        /// <summary>
        /// Obtiene los lotes utilizando la ayuda
        /// </summary>
        /// <param name="filtroLote"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<AlmacenInventarioLoteInfo> ObtenerLotes(FiltroAyudaLotes filtroLote)
        {
            IList<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = null;
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var almacenPL = new AlmacenPL();
                    List<AlmacenInfo> almacenesOrganizacion = almacenPL.ObtenerAlmacenesPorOrganizacion(seguridad.Usuario.Organizacion.OrganizacionID);
                    AlmacenInfo almacenPlantaAlimentos =
                        almacenesOrganizacion.FirstOrDefault(
                            alm => alm.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode());
                    if (almacenPlantaAlimentos != null)
                    {
                        filtroLote.AlmacenID = almacenPlantaAlimentos.AlmacenID;
                    }
                    filtroLote.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;

                    var almacenInventarioLotePL = new AlmacenInventarioLotePL();

                    listaAlmacenInventarioLote =
                        almacenInventarioLotePL.ObtenerAlmacenInventarioLotePorLote(filtroLote);

                    return listaAlmacenInventarioLote;
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaAlmacenInventarioLote;
        }
        /// <summary>
        /// Obtiene el número de batch que deberá mostrarse en el texbox "txtBatch"
        /// Este dato se inicializado en 1, por rotomix y por día.
        /// </summary>
        [WebMethod]
        public static int DeterminaNumeroBatch(int rotoMix)
        {
            int resultado = 0;
            try
            {
                int organizacionid = 0;
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                organizacionid = seguridad.Usuario.Organizacion.OrganizacionID;
                var formulaPl = new FormulaPL();
                resultado = formulaPl.CantidadBatch(organizacionid, rotoMix)+1;
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene de la formula ingresada, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        [WebMethod]
        public static ProduccionFormulaInfo ConsultarFormulaId(int formulaId)
        {

            ProduccionFormulaInfo resultado = null;
            try
            {
                int organizacionid = 0;
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                organizacionid = seguridad.Usuario.Organizacion.OrganizacionID;
                var produccionFormulaResumenPL = new ProduccionFormulaResumenPL();
                ProduccionFormulaResumenInfo produccionFormulaResumenInfo = new ProduccionFormulaResumenInfo();
                produccionFormulaResumenInfo.OrganizacionID = organizacionid;
                produccionFormulaResumenInfo.FormulaID = formulaId;
                produccionFormulaResumenInfo.TipoAlmacenID = Convert.ToInt16(TipoAlmacenEnum.PlantaDeAlimentos);
                resultado = produccionFormulaResumenPL.ConsultarFormulaId(produccionFormulaResumenInfo);

            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

    }
}