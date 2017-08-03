using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Manejo
{
    public partial class RevisionImplantes : System.Web.UI.Page
    {

        private static CorralInfo CorralSeleccionado;
        private static LoteInfo LoteSeleccionado;
        private static AnimalInfo AreteSeleccionado;
        private static ResultadoInfo<AreaRevisionInfo> Lugares;
        private static ResultadoInfo<CausaRevisionImplanteInfo> Causas;
        private static List<RevisionImplanteInfo> Revisiones;


        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitUserData();
        }

        /// <summary>
        /// The init user data.
        /// </summary>
        private void InitUserData()
        {
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    if (seguridad.Usuario.Operador != null)
                    {
                        txtFecha.Text = DateTime.Now.ToShortDateString();
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
            }
        }

        /// <summary>
        /// Obtiene los lugares de validacion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ResultadoInfo<AreaRevisionInfo> ObtenerLugaresValidacion()
        {
            try
            {
                var revisionPl = new RevisionImplantesPL();
                Lugares = revisionPl.ObtenerLugaresValidacion();
                return Lugares;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The obtener causas.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoInfo<CausaRevisionImplanteInfo> ObtenerCausas()
        {
            try
            {
                var revisionPl = new RevisionImplantesPL();
                Causas = revisionPl.ObtenerCausas();
                return Causas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Se valida el corral.
        /// </summary>
        /// <param name="codigoCorral">
        /// The codigo corral.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion ValidarCorral(string codigoCorral)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();

                var revisionImplantePl = new RevisionImplantesPL();
                var corral =
                    new CorralInfo
                    {
                        Codigo = codigoCorral,
                        OrganizacionId = seguridad.Usuario.OrganizacionID
                    };
                ResultadoValidacion resultado = revisionImplantePl.ValidarCorral(corral);

                if (!resultado.Resultado)
                {
                    return resultado;
                }
                var revisionActual = (RevisionImplanteInfo)resultado.Control;
                LoteSeleccionado = revisionActual.Lote;
                CorralSeleccionado = revisionActual.Corral;
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Valida el arete proporcionado
        /// </summary>
        /// <param name="codigoArete">
        /// The codigo arete.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion ValidarArete(string codigoArete)
        {
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();

                var revisionImplantePl = new RevisionImplantesPL();
                var animal =
                    new AnimalInfo
                    {
                        Arete = codigoArete,
                        OrganizacionIDEntrada = seguridad.Usuario.OrganizacionID
                    };
                ResultadoValidacion resultado = revisionImplantePl.ValidarArete(animal, CorralSeleccionado);
                AreteSeleccionado = (AnimalInfo)resultado.Control;
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene la sessión  de la seguridad
        /// </summary>
        /// <returns></returns>
        public static object ObtenerSeguridad()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"];
                return seguridad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The agregar arete.
        /// </summary>
        /// <param name="revision">
        /// The revision.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion AgregarArete(RevisionImplanteInfo revision)
        {
            try
            {
                var resultado = new ResultadoValidacion();
                revision.Fecha = DateTime.Now;
                var revisionImplantePl = new RevisionImplantesPL();

                if (Revisiones == null)
                {
                    Revisiones = new List<RevisionImplanteInfo>();
                }

                resultado = ValidarCorral(revision.Corral.Codigo);
                if (!resultado.Resultado)
                {
                    return resultado;
                }

                var revisionTemporal = (RevisionImplanteInfo)resultado.Control;
                revision.Corral = revisionTemporal.Corral;
                revision.Lote = revisionTemporal.Lote;

                resultado = ValidarArete(revision.Animal.Arete);
                if (!resultado.Resultado)
                {
                    return resultado;
                }

                revision.Animal = (AnimalInfo)resultado.Control;

                resultado = revisionImplantePl.ValidarAgregarArete(revision, Revisiones);

                if (!resultado.Resultado)
                {
                    return resultado;
                }

                foreach (var lugar in Lugares.Lista.Where(lugar => lugar.AreaRevisionId == revision.LugarValidacion.AreaRevisionId))
                {
                    revision.LugarValidacion = lugar;
                    break;
                }

                foreach (var causa in Causas.Lista.Where(lugar => lugar.CausaId == revision.Causa.CausaId))
                {
                    revision.Causa = causa;
                    break;
                }

                Revisiones.Add(revision);

                resultado.Control = Revisiones;
                
                //ResultadoValidacion resultado = new ResultadoValidacion();// = revisionImplantePl.ValidarAgregarArete(animal, CorralSeleccionado);
                //AreteSeleccionado = (AnimalInfo)resultado.Control;
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The eliminar registro.
        /// </summary>
        /// <param name="arete">
        /// The arete.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion EliminarRegistro(string codigoArete)
        {
            try
            {
                var resultado = new ResultadoValidacion{Resultado = true};
                List<RevisionImplanteInfo> RevisionesTemporal = new List<RevisionImplanteInfo>();
                foreach (var revisionImplanteInfo in Revisiones)
                {
                    if (revisionImplanteInfo.Animal.Arete != codigoArete)
                    {
                        RevisionesTemporal.Add(revisionImplanteInfo);
                    }
                }
                Revisiones = RevisionesTemporal;
                resultado.Control = RevisionesTemporal;
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// The cancelar.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion Cancelar()
        {
            try
            {
                var resultado = new ResultadoValidacion { Resultado = true };
                CorralSeleccionado = null;
                AreteSeleccionado = null;
                Revisiones = new List<RevisionImplanteInfo>();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// The guardar revision.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        [WebMethod]
        public static ResultadoValidacion GuardarRevision()
        {
            try
            {
                var revisionImplantePl = new RevisionImplantesPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                return revisionImplantePl.GuardarRevision(Revisiones, seguridad.Usuario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}