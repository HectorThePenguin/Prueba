using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alimentacion
{
    public partial class CapturaManualDeConsumo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                idUsuario.Value = seguridad.Usuario.UsuarioID.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Consulta las formulas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<FormulaInfo> ConsultarFormulas()
        {
            try
            {
                var formulasPl = new FormulaPL();
                var formulas = formulasPl.ObtenerPorTipoFormulaID((int)TipoFormula.Forraje);
                var formulasInicio = formulasPl.ObtenerPorTipoFormulaID((int)TipoFormula.Inicio);
                formulasInicio = formulasInicio.Where(item => item.Descripcion.Trim() == "F1").ToList();
                if (formulasInicio.Count > 0)
                    formulas.Add(formulasInicio[0]);
                return formulas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Consulta los tipos de corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<GrupoCorralInfo> ConsultarTipoDeCorral()
        {
            try
            {
                var grupoCorralPl = new GrupoCorralPL();
                IList<GrupoCorralInfo> listaGrupo = grupoCorralPl.ObtenerTodos(EstatusEnum.Activo);
                if (listaGrupo != null)
                {
                    listaGrupo = listaGrupo.Where(i => i.GrupoCorralID != (int) GrupoCorralEnum.Corraleta).ToList();
                }
                return listaGrupo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el listado de corrales dependiendo del Grupo Corral al que pertenecen
        /// </summary>
        /// <param name="grupoCorralId">Es el id del grupo de corral seleccionado.</param>
        /// <returns></returns>
        [WebMethod]
        public static IList<CorralInfo> ConsultarCorralesPorGrupoCorral(int grupoCorralId)
        {
            try
            {
                IList<CorralInfo> listaCorrales = null;
                var corralPl = new CorralPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (seguridad != null)
                {
                    var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                    if (grupoCorralId == (int) GrupoCorralEnum.Enfermeria)
                    {
                        listaCorrales = corralPl.ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(new GrupoCorralInfo() { GrupoCorralID = grupoCorralId }, organizacionId);
                    }
                    else
                    {
                        listaCorrales = corralPl.ObtenerCorralesPorGrupo(new GrupoCorralInfo() {GrupoCorralID = grupoCorralId}, organizacionId);
                    }
                }

                return listaCorrales;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Método que valida el corral ingresado por el usuario.
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ValidarCorral(CorralInfo corralInfo)
        {
            var corral = new CorralInfo();
            var lotePl = new LotePL();

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                    var corralPL = new CorralPL();

                    corral = corralPL.ObtenerCorralActivoPorCodigo(organizacionId, corralInfo.Codigo);

                    if (corral != null)
                    {
                        LoteInfo lote = lotePl.ObtenerPorCorralCerrado(organizacionId, corral.CorralID);

                        if (lote != null)
                        {
                            if (lote.Activo == EstatusEnum.Inactivo)
                            {
                                corral = new CorralInfo();
                                corral.CorralID = -3; //Corral no tiene lote activo
                            }
                            else if (corral.GrupoCorral != (int) GrupoCorralEnum.Produccion)
                            {
                                corral = new CorralInfo();
                                corral.CorralID = -1; //Corral no es de produccion
                            }
                        }
                        else
                        {
                            corral = new CorralInfo();
                            corral.CorralID = -3; //Corral no tiene lote activo
                        }
                    }
                }
                else
                {
                    corral = new CorralInfo();
                    corral.CorralID = -2; //Error de session
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                corral = null;
            }

            return corral;
        }

        /// <summary>
        /// Obtiene el total de cabezas de todos los corrales seleccionados.
        /// </summary>
        /// <param name="corrales"></param>
        /// <param name="parametroConsumo"></param>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoOperacion GenerarOrdenRepartoManual(List<CorralInfo> corrales, ParametroCapturaManualConsumoInfo parametroConsumo)
        {
            var repartoPl = new RepartoPL();
            var respuesta = new ResultadoOperacion();

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    parametroConsumo.UsuarioId = seguridad.Usuario.UsuarioID;
                    parametroConsumo.OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    respuesta = repartoPl.GenerarOrdenRepartoManual(corrales, parametroConsumo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                respuesta.Resultado = false;
            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene el avance del reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoAvanceInfo ObtenerAvanceReparto()
        {
            RepartoAvanceInfo respuesta = null;

            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var repartoPl = new RepartoPL();

                    respuesta = repartoPl.ObtenerAvanceReparto(seguridad.Usuario.UsuarioID);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return respuesta;
        }

        /// <summary>
        /// Valida si la formula seleccionada tiene existencia en almacen inventario por lote
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static AlmacenInventarioLoteInfo ObtenerExistenciaInventario(int almacenInventarioLoteId)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            AlmacenInventarioLoteInfo almacenInventarioLoteInfo = null;
            var almaceninventariolotepl = new AlmacenInventarioLotePL();
            
            if (seguridad != null)
            {
                almacenInventarioLoteInfo = almaceninventariolotepl.ObtenerAlmacenInventarioLotePorId(almacenInventarioLoteId);
            }

            return almacenInventarioLoteInfo;
        }

        /// <summary>
        /// Valida si la formula tiene enxistencia en almacen inventario
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static AlmacenInventarioInfo ObtenerExistenciaInventarioFormula(int formulaId)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            AlmacenInventarioInfo almacenInventarioInfo = null;
            //var almaceninventarioPl = new AlmacenInventarioPL();

            if (seguridad != null)
            {
                FormulaInfo formula = new FormulaInfo();
                FormulaPL formulaPl = new FormulaPL();
                formula = formulaPl.ObtenerPorID(formulaId);
                //AlmacenInfo almacenInfo = new AlmacenInfo();
                AlmacenPL almacenPl = new AlmacenPL();
                almacenInventarioInfo = almacenPl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(new ParametrosOrganizacionTipoAlmacenProductoActivo(){
                    Activo = 1,
                    OrganizacionId = seguridad.Usuario.OrganizacionID,
                    ProductoId = formula.Producto.ProductoId,
                    TipoAlmacenId = (int)TipoAlmacenEnum.PlantaDeAlimentos
                 });
            }

            return almacenInventarioInfo;
        }

        /// <summary>
        /// Obtiene el listado de los lotes en los que esta el producto seleccionado del almacen de materia prima
        /// </summary>
        /// <param name="formulaId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<AlmacenInventarioLoteInfo> ObtenerLotesDelProducto(int formulaId)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            if (seguridad != null)
            {
                var formulaPl = new FormulaPL();
                FormulaInfo formulaInfo = formulaPl.ObtenerPorID(formulaId);

                if (formulaInfo.Descripcion.Trim() != "F1")
                {
                    var almaceninventariolotepl = new AlmacenInventarioLotePL();
                    listaLotes = almaceninventariolotepl.ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(
                        new ParametrosOrganizacionTipoAlmacenProductoActivo
                        {
                            Activo = 1,
                            OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID,
                            ProductoId = formulaInfo.Producto.ProductoId,
                            TipoAlmacenId = (int) TipoAlmacenEnum.MateriasPrimas,
                            UsuarioId = seguridad.Usuario.UsuarioID
                        });
                }
            }

            return listaLotes;
        }
    }
}