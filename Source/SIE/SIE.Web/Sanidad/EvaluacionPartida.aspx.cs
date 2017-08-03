using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using Entidad;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Web.Services;
using System.Web.UI.WebControls;
using SIE.Base.Log;
using utilerias;

namespace SIE.Web.Sanidad
{
    public partial class EvaluacionPartida : PageBase
    {
        #region Eventos
        /// <summary>
        /// Inicia la carga de las preguntas y busqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LimpiarControles();
            ObtenerCorrales();

            ObtenerPreguntasEnfermeria();
            txtFechaEvaluacion.Text = DateTime.Now.ToShortDateString();
            txtHrEvalacion.Text = DateTime.Now.ToShortTimeString();
            msgErrorRolEvaluador.Value = GetLocalResourceObject("msgErrorRolEvaluador").ToString();

            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if (usuario != null)
            {
                if (usuario.Usuario.Operador != null)
                {
                    txtEvaluador.Text = usuario.Usuario.Operador.NombreCompleto;
                }
            }
        }

        /// <summary>
        /// Validar el rol del usuario logueado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<List<PreguntaInfo>> ValidarRolUsuario()
        {
            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (usuario != null)
                {
                    if (usuario.Usuario.Operador == null)
                    {
                        return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(false,
                                                                                                   "NOPREGUNTAS");
                    }
                    return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(true,
                       "NOPREGUNTAS");
                }
                else
                {
                    return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(false,
                        "NOPREGUNTAS");
                }
                
            }
            catch (Exception ex)
            {
                return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(false,
                            ex.Message);
            }
        }


        private void LimpiarControles()
        {
            txtCorral.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtCabezas.Text = string.Empty;
            txtFechaEvaluacion.Text = string.Empty;
            txtPartidas.Text = string.Empty;
            txtHrEvalacion.Text = string.Empty;
            txtInventarioInicial.Text = string.Empty;
            txtFechaLlegada.Text = string.Empty;
            corralId.Value = string.Empty;
            kgsOrigen.Value = string.Empty;
            fechaSalida.Value = string.Empty;
            Horas.Value = string.Empty;
            txttotalPreguntas.Value = string.Empty;
            txttotalAyuda.Value = string.Empty;
            rdnMetafilaxiaAutorizada.Checked = false;
            txtGarrapata.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtPorcentajeCC3.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtGuardar.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            paginado.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            evaluadores.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtMerma.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtPesoPromedio.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
            txtHoras.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Evento para pintar los renglones cuando la fecha de entrada es mayor a 3 dias.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double dias = CalcularDiasTranscurridas(DateTime.Parse(e.Row.Cells[6].Text));
                if (dias > Numeros.ValorTres.GetHashCode())
                {
                    e.Row.Cells[1].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[2].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[3].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[4].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[5].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[6].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[7].BackColor = Color.FromArgb(237, 176, 176);
                    e.Row.Cells[8].BackColor = Color.FromArgb(237, 176, 176);
                }
            }
        }
        /// <summary>
        /// Evento de cambio de pagina del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            ObtenerCorrales();
            paginado.Value = Numeros.ValorUno.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Metodo para obtener corrales con sus lotes activos que no cuentan con fecha de evalauacion
        /// </summary>
        private void ObtenerCorrales()
        {
            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionID = 0;
                if (usuario != null)
                {
                    organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                }
                var entradaPl = new EntradaGanadoPL();
                var resultadoBusqueda = entradaPl.ObtenerPorIDOrganizacion(organizacionID,
                                                                           TipoCorral.Recepcion.GetHashCode());
                if (resultadoBusqueda != null && resultadoBusqueda.Lista != null &&
                    resultadoBusqueda.Lista.Count > 0)
                {
                    var listaBusqueda = (from item in resultadoBusqueda.Lista
                                         select new
                                         {
                                             Horas = CalcularHorasTranscurridas(item.FechaEntrada, item.FechaSalida),
                                             item.FolioEntrada,
                                             item.CodigoCorral,
                                             item.Lote.LoteID,
                                             item.Lote.Lote,
                                             item.CabezasRecibidas,
                                             item.PesoTara,
                                             PesoLlegada = String.Format("{0:#,#}", (int)(item.PesoBruto - item.PesoTara)),
                                             item.FechaEntrada,
                                             item.OrganizacionOrigen,
                                             item.FolioOrigen,
                                             item.CorralID,
                                             item.PesoBruto,
                                             item.FechaSalida,
                                             item.PesoOrigen,
                                             HoraLlegada = item.FechaEntrada.ToShortTimeString()
                                         }).ToList();
                    gvBusqueda.DataSource = listaBusqueda;
                    gvBusqueda.DataBind();
                    txttotalAyuda.Value = Numeros.ValorUno.GetHashCode().ToString();
                }
                else
                {
                    gvBusqueda.DataSource = new List<EntradaGanadoInfo>();
                    gvBusqueda.DataBind();
                    txttotalAyuda.Value = Numeros.ValorCero.GetHashCode().ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Metodo para calcular las horas transcurridas del viaje
        /// </summary>
        private double CalcularHorasTranscurridas(DateTime fechaEntrada, DateTime salida)
        {
            TimeSpan horasTranscurridas = TimeSpan.MinValue;
            horasTranscurridas = fechaEntrada - salida;
            return horasTranscurridas.TotalHours;
        }
        /// <summary>
        /// Metodo para calcular las dias transcurridas.
        /// </summary>
        private double CalcularDiasTranscurridas(DateTime fechaEntrada)
        {
            TimeSpan dias = TimeSpan.MinValue;
            dias = DateTime.Now - fechaEntrada;
            return dias.Days;
        }
                
        /// <summary>
        /// Metodo para obtener las preguntas de enfermeria
        /// </summary>
        private void ObtenerPreguntasEnfermeria()
        {
            try
            {
                var preguntaPl = new PreguntaPL();
                ResultadoInfo<PreguntaInfo> resultadoPreguntas =
                    preguntaPl.ObtenerPorFormularioID(TipoPreguntas.DatosEnfermeria.GetHashCode());
                if (resultadoPreguntas != null && resultadoPreguntas.Lista != null &&
                    resultadoPreguntas.Lista.Count > 0)
                {
                    var listaBusqueda = (from item in resultadoPreguntas.Lista
                                         select new PreguntaInfo
                                         {
                                             Descripcion =  item.Descripcion,
                                             Valor = "number",
                                             PreguntaID = item.PreguntaID,
                                             Activo = false
                                         }).ToList();
                    int i = 0;
                    while (i < listaBusqueda.Count)
                    {
                        if (listaBusqueda[i].Descripcion.Contains(GetLocalResourceObject("CC3").ToString()) ||
                            listaBusqueda[i].Descripcion.Contains(GetLocalResourceObject("CC3OMENOS").ToString()) ||
                            listaBusqueda[i].Descripcion.Contains(GetLocalResourceObject("ENFERMOSGRADO1").ToString()) ||
                            listaBusqueda[i].Descripcion.Contains(GetLocalResourceObject("ENFERMOSGRADO1A").ToString()) ||
                            listaBusqueda[i].Descripcion.Contains(GetLocalResourceObject("TASAMORABILIDAD").ToString()))
                        {
                            listaBusqueda[i].Activo = true;
                            listaBusqueda[i].Valor = "text";
                        }
                        
                        i++;
                    }
                    dgvDatosEnfermeria.DataSource = listaBusqueda;
                    dgvDatosEnfermeria.DataBind();
                }
                else
                {
                    txttotalPreguntas.Value = Numeros.ValorCero.GetHashCode().ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        [WebMethod]
        public static string CargarDatos()
        {
            var prueba = new EvaluacionPartida();
            prueba.ObtenerCorrales();
            return "ok";
        }
        /// <summary>
        /// Metodo para guardar la informacion en EvaluacionCorral, EvaluacionCorralDetalle
        /// </summary>
        [WebMethod]
        public static Response<EvaluacionCorralInfo> GuardarEvaluacion(EvaluacionCorralInfo evaluacionCorralInfo)
        {
            try
            {
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionID = 0;
                int usuarioID = 0;
                if (usuario != null)
                {
                    organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                    usuarioID = usuario.Usuario.UsuarioID;
                }
                var guardarPl = new EvaluacionCorralPL();
                var listaGuardarEvaluacion = new EvaluacionCorralInfo
                                                 {
                                                     Organizacion =
                                                         new OrganizacionInfo {OrganizacionID = organizacionID},
                                                     Corral =
                                                         new CorralInfo
                                                             {CorralID = evaluacionCorralInfo.Corral.CorralID},
                                                     Lote = new LoteInfo {LoteID = evaluacionCorralInfo.Lote.LoteID},
                                                     Cabezas = evaluacionCorralInfo.Cabezas,
                                                     EsMetafilaxia = evaluacionCorralInfo.EsMetafilaxia,
                                                     MetafilaxiaAutorizada = evaluacionCorralInfo.MetafilaxiaAutorizada,
                                                     Operador =
                                                         new OperadorInfo
                                                             {OperadorID = usuario.Usuario.Operador.OperadorID},
                                                     NivelGarrapata = evaluacionCorralInfo.NivelGarrapata,
                                                     Justificacion = evaluacionCorralInfo.Justificacion,
                                                     UsuarioCreacionID = usuarioID,
                                                     UsuarioModificacionID = usuarioID,
                                                     PreguntasEvaluacionCorral =
                                                         evaluacionCorralInfo.PreguntasEvaluacionCorral
                                                 };
                listaGuardarEvaluacion.PreguntasEvaluacionCorral.ToList().ForEach(clave =>
                                                                                      {
                                                                                          clave.UsuarioCreacion =
                                                                                              usuarioID;
                                                                                          clave.UsuarioModificacion =
                                                                                              usuarioID;
                                                                                      });
                int respuestaMetodo = guardarPl.GuardarEvaluacionCorral(listaGuardarEvaluacion,(int)TipoFolio.EvaluacionPartida);
                if (respuestaMetodo != 0)
                {
                    return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(true,
                        "GuardadoExito");
                }
                return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(false,
                    "ErrorGuardar");
            }
            catch (Exception ex)
            {
                return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(false, ex.InnerException.Message);
            }
        }
        
        /// <summary>
        /// Metodo para validar si existe el corral
        /// </summary>
        [WebMethod]
        public static Response<ServicioAlimentoInfo> ValidarCodigoCorral(string value)
        {
            try
            {
                var values = Utilerias.Deserializar<ServicioAlimentoInfo>(value);

                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                int organizacionID = 0;
                if (usuario != null)
                {
                    organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                }

                var corralPL = new CorralPL();
                CorralInfo corral = corralPL.ObtenerValidacionCorral(organizacionID, values.CodigoCorral);
                if (corral != null)
                {
                    return Response<ServicioAlimentoInfo>.CrearResponseVacio<ServicioAlimentoInfo>(true,
                                                                                                   "EXISTE");
                }
                return Response<ServicioAlimentoInfo>.CrearResponseVacio<ServicioAlimentoInfo>(false,
                                                                                               "NOEXISTE");
            }
            catch (Exception ex)
            {
                return Response<ServicioAlimentoInfo>.CrearResponseVacio<ServicioAlimentoInfo>(false, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para consultar las preguntas del formulario entrada de ganado
        /// </summary>
        [WebMethod]
        public static Response<List<PreguntaInfo>> ConsultarPreguntas()
        {
            try
            {
                var preguntaPl = new PreguntaPL();
                ResultadoInfo<PreguntaInfo> resultadoPreguntas =
                    preguntaPl.ObtenerPorFormularioID(TipoPreguntas.EvaluacionRiesgo.GetHashCode());
                if (resultadoPreguntas != null && resultadoPreguntas.Lista != null &&
                    resultadoPreguntas.Lista.Count > 0)
                {
                    var listaBusqueda = (from item in resultadoPreguntas.Lista
                                         select new PreguntaInfo
                                         {
                                             Descripcion = item.Descripcion,
                                             Activo = false,
                                             Valor = item.Valor,
                                             PreguntaID = item.PreguntaID
                                         }).ToList();
                    return Response<List<PreguntaInfo>>.CrearResponse<List<PreguntaInfo>>(true, listaBusqueda);

                }
                return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(false,
                        "NOPREGUNTAS");
            }
            catch (Exception ex)
            {
                return Response<List<PreguntaInfo>>.CrearResponseVacio<List<PreguntaInfo>>(false,
                            ex.Message);
            }
        }
        /// <summary>
        /// Metodo para obtener corrales con sus lotes activos que no cuentan con fecha de evalauacion
        /// </summary>
        [WebMethod]
        public static Response<List<EntradaGanadoInfo>> ObtenerCorralesSinEvaluar()
        {
            try
            {
                int organizacionID = 0;
                var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                if (usuario != null)
                {
                    organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                }
                var entradaPl = new EntradaGanadoPL();
                var resultadoBusqueda = entradaPl.TraerGanadoPorOrganizacionId(organizacionID,
                    (int)TipoCorral.Recepcion);

                if (resultadoBusqueda != null)
                {
                    var listaBusqueda = (from item in resultadoBusqueda
                                         select new EntradaGanadoInfo
                                                    {
                                                        Horas = item.Horas,
                                                        FolioEntrada = item.FolioEntrada,
                                                        CodigoCorral = item.CodigoCorral,
                                                        LoteID = item.LoteID,
                                                        CodigoLote = item.CodigoLote,
                                                        CabezasRecibidas = item.CabezasRecibidas,
                                                        PesoLlegada = (int) (item.PesoBruto - item.PesoTara),
                                                        PesoTara = item.PesoTara,
                                                        FechaEntrada = item.FechaEntrada,
                                                        OrganizacionOrigen = item.OrganizacionOrigen,
                                                        FolioOrigen = item.FolioOrigen,
                                                        CorralID = item.CorralID,
                                                        PesoBruto = item.PesoBruto,
                                                        FechaSalida = item.FechaSalida,
                                                        PesoOrigen = item.PesoOrigen
                                                    }).ToList();
                    return Response<List<EntradaGanadoInfo>>.CrearResponse(true, listaBusqueda);
                }
                return Response<List<EntradaGanadoInfo>>.CrearResponseVacio<List<EntradaGanadoInfo>>(false,
                        "Ocurrio un error al consultar");
            }
            catch (Exception ex)
            {
                return Response<List<EntradaGanadoInfo>>.CrearResponseVacio<List<EntradaGanadoInfo>>(false,
                            ex.Message);
            }
        }
        #endregion

        [WebMethod]
        public static string ObtenerFecha()
        {
            return DateTime.Now.ToShortDateString();
        }

        [WebMethod]
        public static string obtenerHora()
        {
            return DateTime.Now.ToShortTimeString();
        }

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            ObtenerCorrales();
        }
    }
}
