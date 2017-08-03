using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using utilerias;

namespace SIE.Web.Sanidad
{
    public partial class SupervisionTecnicaDetectores : PageBase
    {
        /// <summary>
        /// Cargado de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitUserData();
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

                        //if (rolIdUsuario == (int) Roles.SupervisorSanidad){
                        CargarComboDetectores(seguridad.Usuario.Organizacion.OrganizacionID,
                            seguridad.Usuario.Operador.OperadorID);

                        lblFechaExpuesta.Text = DateTime.Now.ToShortDateString();
                        lblOrganizacion.Text = seguridad.Usuario.Organizacion.Descripcion;
                        /*}else{
                            ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();",
                                true);
                        }*/
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();",
                                true);
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
        /// Carga los datos del combo detectores
        /// </summary>
        private void CargarComboDetectores(int organizacionid, int operadorId)
        {
            try
            {

                IList<OperadorInfo> operadores = ObtenerListaDetectores(organizacionid, operadorId);
                if (operadores != null)
                {
                    var listaOperadores = from item in operadores
                                         select new
                                         {
                                             item.NombreCompleto,
                                             item.OperadorID
                                         };



                    cmbDetectores.DataSource = operadores;
                    cmbDetectores.DataTextField = "NombreCompleto";
                    cmbDetectores.DataValueField = "OperadorID";
                    cmbDetectores.DataBind();
                    AnexarItemSeleccione(ref cmbDetectores);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
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
            else
            {
                itemCombo.Text = @"Undefined";
            }
            itemCombo.Value = Numeros.ValorCero.GetHashCode().ToString();
            combo.Items.Insert(0, itemCombo);
        }


        #region WebMethods
        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<OperadorInfo> ObtenerListaDetectores(int organizacionid, int operadorId)
        {
            IList<OperadorInfo> retValue = null;
            var pl = new OperadorPL();
            try
            {
                retValue = pl.ObtenerPorIdRolDetector(organizacionid, operadorId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<PreguntaInfo> ObtenerPreguntas()
        {
            IList<PreguntaInfo> retValue = null;
            var pl = new PreguntaPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int tipo = (int)TipoPreguntas.SupervicionTecnicaDetencion;
                if (seguridad != null)
                {
                    ResultadoInfo<PreguntaInfo> resultado = pl.ObtenerPorFormularioID(tipo);

                    if (resultado != null)
                        retValue = resultado.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<CriterioSupervisionInfo> ObtenerCriteriosEvaluacion()
        {
            IList<CriterioSupervisionInfo> retValue = null;
            var pl = new PreguntaPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    ResultadoInfo<CriterioSupervisionInfo> resultado = pl.ObtenerCriteriosSupervision();

                    if (resultado != null)
                        retValue = resultado.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Metodo para obtener las evaluaciones anteriores de un operador
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<SupervisionDetectoresInfo> ObtenerEvaluacionesAnteriores(string value)
        {
            IList<SupervisionDetectoresInfo> retValue = null;
            var pl = new PreguntaPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    var operadorId = Utilerias.Deserializar<int>(value);

                    ResultadoInfo<SupervisionDetectoresInfo> resultado = 
                            pl.ObtenerSupervisionesAnteriores(seguridad.Usuario.Organizacion.OrganizacionID, operadorId);

                    if (resultado != null)
                        retValue = resultado.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }

        /// <summary>
        /// Obtiene la informacion del periodo de evaluacion del sitema
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ConfiguracionParametrosInfo ObtenerConfiguracionPeriodo()
        {
            ConfiguracionParametrosInfo retValue = null;
            var pl = new ConfiguracionParametrosPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    var parametro = new ConfiguracionParametrosInfo()
                    {
                      Clave = ParametrosEnum.diasPeriodo.ToString(),
                      OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID,
                      TipoParametro = (int)TiposParametrosEnum.CheckListTecnicaDeteccion
                      
                    };

                   retValue = 
                            pl.ObtenerPorOrganizacionTipoParametroClave(parametro);

                   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retValue;
        }


        /// <summary>
        /// Metodo guardar los aretes recibidos en necropsia
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<SupervisionDetectoresInfo> GuardarSupervision(string value)
        {
            Response<SupervisionDetectoresInfo> retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    var values = Utilerias.Deserializar<SupervisionDetectoresInfo>(value);
                    var guardarPl = new PreguntaPL();

                    values.OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    values.UsuarioCreacionId = seguridad.Usuario.UsuarioID;

                    var resultado = guardarPl.GuardarSupervisionDeteccionTecnica(values);

                    if (resultado > 0)
                    {
                        retValue = Response<SupervisionDetectoresInfo>.CrearResponseVacio<SupervisionDetectoresInfo>(true, "OK");
                    }

                }
                else
                {
                    retValue = Response<SupervisionDetectoresInfo>.CrearResponseVacio<SupervisionDetectoresInfo>(false, "Fallo al guardar supervision. Su sesión a expirado, por favor ingrese de nuevo");
                }

            }
            catch (Exception ex)
            {
                retValue = Response<SupervisionDetectoresInfo>.CrearResponseVacio<SupervisionDetectoresInfo>(false, "Error inesperado: " + ex.InnerException.Message);
            }

            return retValue;
        }
        #endregion
    }
}