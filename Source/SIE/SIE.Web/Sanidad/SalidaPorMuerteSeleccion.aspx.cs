using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Base.Log;

using System.Web.Services;
using utilerias;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace SIE.Web.Sanidad
{
    public partial class SalidaPorMuerteSeleccion : PageBase
    {
        private string numeroArete;
        private int muerteId;
        private CorralInfo corralInfo;
        /// <summary>
        /// Carga de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 InitUserData();
            }
           
        }

        /// <summary>
        /// Inicializa los datos de usuario
        /// </summary>
        private void InitUserData()
        {
            //Obtenermos el numero de arete presentado
            muerteId = Convert.ToInt32(Request.QueryString["muerteId"].Trim());
            numeroArete = Request.QueryString["arete"].Trim();
            corralInfo = new CorralInfo {Codigo = Request.QueryString["corral"].Trim()};

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                }
                //Llenamos el combo de problemas
                LlenarComboProblemas();
                LlenarDatosArete();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //throw;
            }

        }

        /// <summary>
        /// Carga la informacion del arete en pantalla
        /// </summary>
        private void LlenarDatosArete()
        {

            MuerteInfo areteMuerto = null;
            var pl = new MuertePL();
            var animalPL = new AnimalPL();
            var corralPL = new CorralPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                var organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                if (seguridad != null)
                {
                    //Se obtiene el Corral para ver Si es de Recepcion
                    corralInfo = corralPL.ObtenerCorralPorCodigo(organizacionID, corralInfo.Codigo);

                    if (corralInfo.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                    {
                        /* Si el corral es de Recepcion */
                        areteMuerto = pl.ObtenerGanadoMuertoPorAreteRecepcion(organizacionID, numeroArete);
                    }
                    else
                    {
                        /* Si el corral es de Enfermeria o produccion */
                        areteMuerto = pl.ObtenerGanadoMuertoPorArete(organizacionID, numeroArete);
                        if (areteMuerto != null && areteMuerto.AnimalId > 0)
                        {
                            areteMuerto.Peso = animalPL.ObtenerUltimoMovimientoAnimal(new AnimalInfo(){AnimalID = areteMuerto.AnimalId,OrganizacionIDEntrada = organizacionID}).Peso;
                        }
                        else
                        {
                            if (areteMuerto==null){
                                areteMuerto = new MuerteInfo {Arete = numeroArete, CorralCodigo = corralInfo.Codigo};
                            }
                            var lote = new LoteInfo();
                            var lotePl = new LotePL();
                            lote = lotePl.ObtenerPorCorral(organizacionID, corralInfo.CorralID);
                            if (lote != null)
                            {
                                var listaAnimales = animalPL.ObtenerAnimalesPorLoteID(lote);
                                if (listaAnimales != null)
                                {
                                    var listaMovimientosActivos = (from animal in listaAnimales
                                        let movimiento = new AnimalMovimientoInfo()
                                        select animalPL.ObtenerUltimoMovimientoAnimal(animal)).ToList();

                                    if (listaMovimientosActivos.Count > 0)
                                    {
                                        areteMuerto.Peso = listaMovimientosActivos.Sum(registro => registro.Peso)/
                                                           listaMovimientosActivos.Count;
                                    }
                                }
                            }
                        }
                    }
                    
                    if (areteMuerto != null)
                    {
                        hdMuerteId.Value = areteMuerto.MuerteId.ToString();
                        hdAnimalId.Value = areteMuerto.AnimalId.ToString();
                        hdCorralId.Value = areteMuerto.CorralId.ToString();
                        hdLoteId.Value = areteMuerto.LoteId.ToString();

                        if (areteMuerto.MuerteId == 0)
                        {
                            hdMuerteId.Value = muerteId.ToString();
                        }

                        txtCorral.Text = areteMuerto.CorralCodigo;
                        txtPeso.Text = areteMuerto.Peso.ToString();
                        txtAreteMetalico.Text = areteMuerto.AreteMetalico;
                        txtNumeroArete.Text = areteMuerto.Arete;
                        txtNombreResponsable.Text = seguridad.Usuario.Nombre;
                        lblFechaSalida.Text += " " + DateTime.Now.ToShortDateString();
                        lblHoraSalida.Text += " " + DateTime.Now.ToShortTimeString();

                        var parametros = ObtenerParametrosRutaImagenes(seguridad.Usuario.Organizacion.OrganizacionID);
                        imgFotoDeteccion.Visible = false;
                        if (parametros != null && areteMuerto.FotoDeteccion != string.Empty)
                        {
                            imgFotoDeteccion.Visible = true;
                            imgFotoDeteccion.ImageUrl = parametros.Valor + areteMuerto.FotoDeteccion;
                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "MostrarFalloCargarDatos();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Obtiene la configuracion de los parametros de la fotos de deteccion
        /// </summary>
        /// <param name="organizacionid"></param>
        /// <returns></returns>
        private ConfiguracionParametrosInfo ObtenerParametrosRutaImagenes(int organizacionid)
        {
            ConfiguracionParametrosInfo retVal = null;
            var pl = new ConfiguracionParametrosPL();
            try
            {
                retVal = pl.ObtenerPorOrganizacionTipoParametroClave(new ConfiguracionParametrosInfo()
                {
                    Clave = ParametrosEnum.ubicacionFotos.ToString(),
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    OrganizacionID = organizacionid
                });


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retVal;
        }
       
        /// <summary>
        /// Se agrega el seleccione al combo de problemas
        /// </summary>
        /// <param name="combo"></param>
        private void AnexarItemSeleccione(ref DropDownList combo)
        {
            var itemCombo = new ListItem();
            var localResourceObject = GetLocalResourceObject("Seleccione");

            if (localResourceObject != null)
                itemCombo.Text = localResourceObject.ToString();
            itemCombo.Value = Numeros.ValorCero.GetHashCode().ToString();
            combo.Items.Insert(0, itemCombo);
        }

        /// <summary>
        /// Metodo para llenar el combo de evaluador operador tipo 3
        /// </summary>
        private void LlenarComboProblemas()
        {
            try
            {

                IList<ProblemaInfo> problemas = ObtenerListaProblemas();
                if (problemas != null)
                {
                    var listaProblemas = from item in problemas
                                           select new
                                           {
                                               item.Descripcion,
                                               item.ProblemaID
                                           };

                    

                    cmbProblemas.DataSource = listaProblemas;
                    cmbProblemas.DataTextField = "Descripcion";
                    cmbProblemas.DataValueField = "ProblemaID";
                    cmbProblemas.DataBind();
                    AnexarItemSeleccione(ref cmbProblemas);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<ProblemaInfo> ObtenerListaProblemas()
        {
            IList<ProblemaInfo> retValue = null;
            var pl = new MuertePL();
            try
            {
                retValue = pl.ObtenerListaProblemasNecropsia();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Metodo guardar la salida por necropsia
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<MuerteInfo> GuardarSalida(string value)
        {
            Response<MuerteInfo> retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    var values = Utilerias.Deserializar<MuerteInfo>(value);
                    var guardarPl = new MuertePL();

                    var muerte = new MuerteInfo()
                    {
                        MuerteId = values.MuerteId,
                        AnimalId = values.AnimalId,
                        LoteId = values.LoteId,
                        CorralId = values.CorralId,
                        OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID,
                        ProblemaId = values.ProblemaId,
                        FotoNecropsia = values.FotoNecropsia,
                        Observaciones = values.Observaciones,
                        Arete = values.Arete,
                        AreteMetalico = values.AreteMetalico,
                        OperadorNecropsiaId = seguridad.Usuario.Operador.OperadorID,
                        Peso = values.Peso,
                        UsuarioCreacionID = seguridad.Usuario.UsuarioID,
                        EstatusId = (int)EstatusMuertes.Necropsia,
                        CorralCodigo = values.CorralCodigo
                    };
                    if(muerte != null){
                        muerte.FotoNecropsia = TipoFoto.Necropsia.ToString() + '/' + muerte.FotoNecropsia;
                    }

                    var resultado = guardarPl.GuardarSalidaPorMuerteNecropsia(muerte);

                    if (resultado == 1)
                    {
                        retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(true, "OK");
                    }

                }
                else
                {
                    retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(false, "Fallo al guardar salida. Su sesión a expirado, por favor ingrese de nuevo");
                }
                
            }
            catch (Exception ex)
            {
                retValue = Response<MuerteInfo>.CrearResponseVacio<MuerteInfo>(false, "Error inesperado: " + ex.InnerException.Message);
            }

            return retValue;
        }


    }

}