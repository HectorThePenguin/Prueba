using System.Globalization;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SIE.Services.Info.Enums;

namespace SIE.Web.Alimentacion
{
    public partial class ConfiguracionAjustes : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarEstadosComedero();
            CargarFormulas();
        }

        /// <summary>
        /// Funcion para llenar los datos del combo de estados de comedero
        /// </summary>
        private void CargarEstadosComedero()
        {
            try
            {
                Logger.Info();
                EstadoComederoPL estadoComederoPL = new EstadoComederoPL();
                var listaEstados = estadoComederoPL.ObtenerTodos();

                cmbEstadoComedero.DataSource = listaEstados;
                cmbEstadoComedero.DataTextField = "EstadoComederoID";
                cmbEstadoComedero.DataValueField = "EstadoComederoID";
                cmbEstadoComedero.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Funcion que llena los datos de los combos de formulas
        /// </summary>
        private void CargarFormulas()
        {
            try
            {
                Logger.Info();
                FormulaPL formulaPL = new FormulaPL();
                IList<FormulaInfo> listaFormulas = formulaPL.ObtenerTodos();

                cmbFormulaManiana.DataSource = listaFormulas;
                cmbFormulaManiana.DataTextField = "Descripcion";
                cmbFormulaManiana.DataValueField = "FormulaId";
                cmbFormulaManiana.DataBind();

                cmbFormulaTarde.DataSource = listaFormulas;
                cmbFormulaTarde.DataTextField = "Descripcion";
                cmbFormulaTarde.DataValueField = "FormulaId";
                cmbFormulaTarde.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar la informacion de un corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ObtenerCorral(string corralCodigo)
        {
            CorralInfo corral;

            corral = null;
            var corralPL = new CorralPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return corral;
        }

        /// <summary>
        /// Metodo para Consultar el lote activo del corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static LoteInfo ObtenerLotesCorral(int corralID)
        {
            LoteInfo lote;
            CorralInfo corral;

            lote = null;
            corral = null;
            var lotePL = new LotePL();
            var corralPL = new CorralPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    corral = corralPL.ObtenerPorId(corralID);

                    if (corral != null)
                    {
                        lote = lotePL.DeteccionObtenerPorCorral(organizacionId, corralID);
                        if (lote == null)
                        {
                            lote = new LoteInfo();
                        }
                        lote.CorralID = corralID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return lote;
        }

        /// <summary>
        /// Metodo para Consultar la orden de reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoInfo ObtenerOrdenReparto(int loteID, int corralID, string fechaReparto)
        {
            RepartoInfo reparto = null;
            LoteInfo lote = null;
            RepartoPL repartoPL = new RepartoPL();
            LotePL lotePL = new LotePL();
            var corralPL = new CorralPL();
            var fecha = DateTime.Parse(fechaReparto);

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];

                if (seguridad != null)
                {
                    lote = lotePL.ObtenerPorId(loteID);
                    CorralInfo corral = corralPL.ObtenerPorId(corralID);
                    //if (lote != null)
                    //{
                    if (lote != null)
                    {
                        reparto = repartoPL.ObtnerPorLote(lote, fecha);
                    }
                    else
                    {
                        reparto = repartoPL.ObtnerPorFechaCorral(corral, fecha);
                    }

                    if (lote != null && reparto == null)
                    {
                        reparto = repartoPL.ObtnerPorFechaCorral(corral, fecha);
                        if (reparto == null || reparto.LoteID == 0 || reparto.LoteID != lote.LoteID)
                        {
                            reparto = null;
                        }
                    }

                    if (reparto != null && reparto.DetalleReparto != null/* &&
                            reparto.DetalleReparto.Count ==
                                reparto.DetalleReparto.Where(dato => dato.Servido == true).ToList().Count()*/)
                    {
                        var matutinosServidos = 0;
                        var vespertinoServidos = 0;

                        matutinosServidos =
                           reparto.DetalleReparto.Count(
                               tmpDetalleReparto =>
                                   tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Matutino &&
                                   tmpDetalleReparto.Servido);
                        if (matutinosServidos > 0)
                        {
                            reparto.TotalRepartos = 1;
                        }
                        vespertinoServidos =
                            reparto.DetalleReparto.Count(
                                tmpDetalleReparto =>
                                    tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Vespertino &&
                                    tmpDetalleReparto.Servido);

                        reparto.CantidadPedido = reparto.DetalleReparto.Where(
                            tmpDetalleReparto =>
                                tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Vespertino ||
                                tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                                .ToList().Sum(tmpDetalleReparto => tmpDetalleReparto.CantidadProgramada);

                        if (vespertinoServidos > 0)
                        {
                            reparto.TotalRepartos = 2;
                        }
                        if (reparto.TotalRepartos == 0)
                        {
                            reparto.TotalRepartos = 3;
                        }
                        RepartoDetalleInfo repartoMatutino =  reparto.DetalleReparto.FirstOrDefault(
                               tmpDetalleReparto =>
                                   tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Matutino);
                        if(repartoMatutino != null)
                        {
                            reparto.CantidadProgramadaManiana = repartoMatutino.CantidadProgramada;
                        }
                        

                    }
                    else
                    {
                        reparto = new RepartoInfo { Fecha = fecha, TotalRepartos = 0 };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return reparto;
        }

        /// <summary>
        /// Metodo para Consultar el tipo proceso del lote
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static TipoProcesoInfo ObtenerTipoProceso(int tipoProcesoID)
        {
            TipoProcesoInfo tipoProceso;
            TipoProcesoPL tipoProcesoPL = new TipoProcesoPL();

            tipoProceso = null;

            try
            {
                tipoProceso = tipoProcesoPL.ObtenerPorID(tipoProcesoID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return tipoProceso;
        }

        /// <summary>
        /// Metodo para Consultar el detalle de la orden de reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoDetalleInfo ObtenerOrdenRepartoDetalleManiana(int loteId, int corralID, string fechaReparto)
        {
            IList<RepartoDetalleInfo> repartoDetalleLista = null;
            LoteInfo lote = new LoteInfo();
            RepartoDetalleInfo repartoDetalle = null;
            RepartoInfo reparto = new RepartoInfo();
            RepartoPL repartoPL = new RepartoPL();
            LotePL lotePL = new LotePL();
            var corralPL = new CorralPL();

            try
            {
                var fecha = DateTime.Parse(fechaReparto.ToString(CultureInfo.InvariantCulture));
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];

                if (seguridad != null)
                {
                    CorralInfo corral = corralPL.ObtenerPorId(corralID);
                    lote = lotePL.ObtenerPorId(loteId);
                    if (lote != null)
                    {
                        reparto = repartoPL.ObtnerPorLote(lote, fecha);
                    }
                    else
                    {
                        reparto = repartoPL.ObtnerPorFechaCorral(corral, fecha);
                    }

                    if (reparto != null)
                    {
                        if (reparto.DetalleReparto != null)
                        {
                            for (int i = 0; i < reparto.DetalleReparto.Count; i++)
                            {
                                if (reparto.DetalleReparto[i].TipoServicioID == (int)TipoServicioEnum.Matutino)
                                {
                                    repartoDetalle = reparto.DetalleReparto[i];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return repartoDetalle;
        }

        /// <summary>
        /// Metodo para Consultar el detalle de la orden de reparto
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static RepartoDetalleInfo ObtenerOrdenRepartoDetalleTarde(int loteID, int repartoID, int corralID, string fechaReparto)
        {
            IList<RepartoDetalleInfo> repartoDetalleLista = null;
            RepartoDetalleInfo repartoDetalle = null;
            LoteInfo lote = null;
            RepartoInfo reparto = new RepartoInfo();
            RepartoPL repartoPL = new RepartoPL();
            LotePL lotePL = new LotePL();
            var corralPL = new CorralPL();
            try
            {
                var fecha = DateTime.Parse(fechaReparto.ToString(CultureInfo.InvariantCulture));

                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];

                if (seguridad != null)
                {
                    lote = lotePL.ObtenerPorId(loteID);
                    CorralInfo corral = corralPL.ObtenerPorId(corralID);
                    /*DateTime fecha = new DateTime();
                    fecha = DateTime.Now;
                    fecha = fecha.AddDays(-1);*/
                    if (lote != null)
                    {
                        reparto = repartoPL.ObtnerPorLote(lote, fecha);
                    }
                    else
                    {
                        reparto = repartoPL.ObtnerPorFechaCorral(corral, fecha);
                    }

                    if (reparto != null)
                    {
                        repartoDetalleLista = repartoPL.ObtenerDetalle(reparto);

                        if (repartoDetalleLista != null)
                        {
                            for (int i = 0; i < repartoDetalleLista.Count; i++)
                            {
                                if (repartoDetalleLista[i].TipoServicioID == (int)TipoServicioEnum.Vespertino)
                                {
                                    repartoDetalle = repartoDetalleLista[i];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return repartoDetalle;
        }

        /// <summary>
        /// Metodo para Consultar si el corral está programado para sacrificio, para reimplante o va a zilmax
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerHabilitarEstadoComedero(int loteID)
        {
            int resultado = 0;
            ProgramacionReimplantePL programacionReinplantePL = new ProgramacionReimplantePL();
            OrdenSacrificioPL ordenSacrificioPL = new OrdenSacrificioPL();
            LotePL lotePL = new LotePL();

            LoteInfo lote = new LoteInfo();
            OrdenSacrificioInfo orden = new OrdenSacrificioInfo();

            try
            {

                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    lote = lotePL.ObtenerPorId(loteID);

                    if (lote != null)
                    {
                        //Se verifica si el corral va a Zilmax
                        if (lote.FechaDisponibilidad.ToShortDateString() != DateTime.Now.ToShortDateString())
                        {
                            resultado = 1;
                        }

                        //Se verifica si el corral va a reimplante
                        if (resultado == 1)
                        {
                            List<ProgramacionReinplanteInfo> programacionReinplante = null;
                            programacionReinplante = programacionReinplantePL.ObtenerProgramacionReimplantePorLoteID(lote);

                            if (programacionReinplante.Count > 0)
                            {
                                if (programacionReinplante.First().Fecha.ToShortDateString() == DateTime.Now.ToShortDateString())
                                {
                                    resultado = 0;
                                }
                            }
                        }

                        //Se verifica si el corral va a sacrificio
                        if (resultado == 1)
                        {
                            orden.FechaOrden = DateTime.Now;
                            orden.OrganizacionID = organizacionId;
                            orden.EstatusID = (int)Estatus.OrdenSacrificioPendiente;
                            orden = ordenSacrificioPL.ObtenerOrdenSacrificioDelDia(orden);

                            if (orden != null)
                            {
                                for (var i = 0; i < orden.DetalleOrden.Count; i++)
                                {
                                    if (orden.DetalleOrden[i].Lote.LoteID == lote.LoteID)
                                    {
                                        resultado = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return resultado;
        }

        /// <summary>
        /// Metodo para Consultar los kilogramos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static LectorRegistroInfo ObtenerKilogramosProgramados(int loteID)
        {
            LectorRegistroInfo lector = null;
            LectorRegistroPL lectorRegistroPL = new LectorRegistroPL();

            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    LoteInfo lote = new LoteInfo();
                    lote.LoteID = loteID;
                    lote.OrganizacionID = organizacionId;
                    lector = lectorRegistroPL.ObtenerLectorRegistro(lote, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return lector;
        }

        /// <summary>
        /// Metodo para Consultar los kilogramos calculados
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerKilogramosCalculados(int estadoComederoID, int kilogramosProgramados)
        {
            int kilogramosCalculados = 0;
            EstadoComederoPL estadoComederoPL = new EstadoComederoPL();
            EstadoComederoInfo estadoComedero = new EstadoComederoInfo();

            try
            {
                estadoComedero = estadoComederoPL.ObtenerPorID(estadoComederoID);

                kilogramosCalculados = estadoComederoPL.ObtenerKilogramosCalculados(estadoComedero, kilogramosProgramados);
            }
            catch (Exception ex)
            {
                kilogramosCalculados = -1;
            }

            return kilogramosCalculados;
        }

        ///<sumary>
        /// Se ultiliza para
        /// </sumary>
        /// <returns>EstadoComedero</returns>
        [WebMethod]
        public static EstadoComederoInfo ObtenerEstadoPorKilogramos(int kilogramos, int kilogramosProgramados)
        {
            List<EstadoComederoInfo> estadoComederoLista = null;
            EstadoComederoPL estadoComederoPL = new EstadoComederoPL();
            EstadoComederoInfo EstadoComedero = new EstadoComederoInfo();
            int kilogramosCalculados = 0;

            try
            {
                estadoComederoLista = estadoComederoPL.ObtenerTodos().ToList();

                foreach (var estado in estadoComederoLista)
                {
                    kilogramosCalculados = estadoComederoPL.ObtenerKilogramosCalculados(estado, kilogramosProgramados);
                    estado.KilogramosCalculados = kilogramosCalculados;
                }

                EstadoComedero =
                    estadoComederoLista.FirstOrDefault(
                        registro => (registro.KilogramosCalculados < kilogramos && registro.Tendencia == Tendencia.Mayor)
                            || (registro.KilogramosCalculados > kilogramos && registro.Tendencia == Tendencia.Menor)
                            || (registro.KilogramosCalculados == kilogramos && registro.Tendencia == Tendencia.Igual));
            }
            catch (Exception)
            {
                EstadoComedero = null;
            }

            return EstadoComedero;
        }

        /// <summary>
        /// Se ultiliza para consultar una configuracion de formula por el tipo de ganado del corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ConfiguracionFormulaInfo> ConsultarConfiguracionFormula(int LoteID)
        {
            List<ConfiguracionFormulaInfo> resultado = null;
            LoteInfo lote = null;
            TipoGanadoInfo tipoGanado = null;
            List<AnimalInfo> animales = null;
            CorralInfo corral = null;

            LotePL lotePL = new LotePL();
            AnimalPL animalPL = new AnimalPL();
            ConfiguracionFormulaPL configuracionFormulaPL = new ConfiguracionFormulaPL();

            try
            {
                lote = lotePL.ObtenerPorId(LoteID);
                if (lote != null)
                {
                    animales = animalPL.ObtenerAnimalesPorLoteID(lote);

                    if (animales != null)
                    {
                        tipoGanado = lotePL.ObtenerSoloTipoGanadoCorral(animales, lote);

                        if (tipoGanado != null)
                        {
                            resultado = configuracionFormulaPL.ObtenerFormulaPorTipoGanado(tipoGanado, lote.OrganizacionID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = null;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que actualiza las formulas de los repartos de la mañana y tarde.
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <param name="fechaReparto"></param>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarSoloFormulas(List<CambiosReporteInfo> cambiosDetalle, string fechaReparto)
        {
            int valorRetorno = 0;

            try
            {
                RepartoPL reparto = new RepartoPL();

                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                int usuario = seguridad.Usuario.UsuarioID;
                var fecha = DateTime.Parse(fechaReparto.ToString(CultureInfo.InvariantCulture));

                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    for (int i = 0; i < cambiosDetalle.Count; i++)
                    {
                        cambiosDetalle[i].OrganizacionID = organizacionId;
                        cambiosDetalle[i].UsuarioModificacionID = usuario;
                        cambiosDetalle[i].FechaReparto = fecha;
                    }
                    valorRetorno = reparto.GuardarFormulasRepartoDetalle(cambiosDetalle);
                }
            }
            catch (Exception ex)
            {
                valorRetorno = -1;
            }
            return valorRetorno;
        }

        /// <summary>
        /// Inserta el reparto
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarReparto(List<CambiosReporteInfo> cambiosDetalle, string fechaReparto)
        {
            int valorRetorno = 0;
            try
            {
                var repartoPl = new RepartoPL();
                var corralPL = new CorralPL();
                var lotePL = new LotePL();

                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    int usuario = seguridad.Usuario.UsuarioID;
                    var fecha = DateTime.Parse(fechaReparto.ToString(CultureInfo.InvariantCulture));

                    List<string> codigosCorral = cambiosDetalle.Select(det => det.CorralInfo.Codigo).Distinct().ToList();

                    List<CorralInfo> corralesOrganizacion = corralPL.ObtenerCorralesPorCodigosCorral(codigosCorral,
                                                                                                     organizacionId);

                    List<CambiosReporteInfo> repartosInactivar = new List<CambiosReporteInfo>();

                    int cantidadManiana = 0;
                    bool bandera = false;

                    foreach (CambiosReporteInfo t in cambiosDetalle)
                    {

                        t.OrganizacionID = organizacionId;
                        t.UsuarioModificacionID = usuario;
                        t.FechaReparto = fecha;

                        #region Eliminar Duplicados
                        if (t.RepartoID == 0)
                        {
                            CorralInfo corralCompleto =
                                corralesOrganizacion.FirstOrDefault(
                                    cor =>
                                    cor.Codigo.Trim().Equals(t.CorralInfo.Codigo.Trim(),
                                                             StringComparison.InvariantCultureIgnoreCase));
                            if (corralCompleto == null)
                            {
                                continue;
                            }

                            var corral = new CorralInfo
                                {
                                    Organizacion = new OrganizacionInfo
                                        {
                                            OrganizacionID = organizacionId
                                        },
                                    CorralID = corralCompleto.CorralID
                                };

                            RepartoInfo repartoCorral = repartoPl.ObtnerPorFechaCorral(corral, fecha);
                            if (repartoCorral != null)
                            {
                                RepartoDetalleInfo repartoDetalleMatutino =
                                    repartoCorral.DetalleReparto.FirstOrDefault(
                                        rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());

                                RepartoDetalleInfo repartoDetalleVespertino =
                                    repartoCorral.DetalleReparto.FirstOrDefault(
                                        rep => rep.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());

                                LoteInfo lote = lotePL.ObtenerLotesActivos(organizacionId, corralCompleto.CorralID);

                                if (t.TipoServicioID == (int)TipoServicioEnum.Matutino)
                                //&& t.Servido == 1)
                                {
                                    if (repartoDetalleMatutino != null)
                                    {
                                        //cantidadManiana = t.CantidadServida;
                                        //if(repartoCorral.Fecha.Date == DateTime.Now.Date)
                                        //{
                                        //    t.NoModificar = true;
                                        //}
                                        cantidadManiana = repartoDetalleMatutino.CantidadProgramada;
                                        bandera = true;
                                    }

                                }

                                if (repartoDetalleMatutino != null)
                                {
                                    if (repartoCorral.LoteID == 0 || !repartoDetalleMatutino.Servido)
                                    {
                                        t.Lote = lote.Lote;
                                        t.Cabezas = lote.Cabezas;
                                        t.CambiarLote = true;
                                        t.RepartoID = repartoCorral.RepartoID;
                                        //t.RepartoDetalleIdManiana = repartoDetalleMatutino.RepartoDetalleID;
                                        if (repartoDetalleVespertino != null)
                                            t.RepartoDetalleIdTarde = repartoDetalleVespertino.RepartoDetalleID;
                                    }
                                    else
                                        if (repartoCorral.LoteID != lote.LoteID)
                                        {
                                            if (t.TipoServicioID == (int)TipoServicioEnum.Vespertino)
                                            {
                                                if (repartoDetalleVespertino != null)
                                                {
                                                    var repartoInactivar = new CambiosReporteInfo
                                                        {
                                                            RepartoDetalleIdTarde = repartoDetalleVespertino.RepartoDetalleID,
                                                            InactivarDetalle = true,
                                                            CorralInfo = new CorralInfo(),
                                                            UsuarioModificacionID = usuario,
                                                            FechaReparto = fecha
                                                        };
                                                    repartosInactivar.Add(repartoInactivar);
                                                }
                                                t.Lote = lote.Lote;
                                            }
                                            else
                                            {
                                                t.RepartoDetalleIdManiana = repartoDetalleMatutino.RepartoDetalleID;
                                            }
                                        }
                                }
                            }
                        }
                        #endregion Eliminar Duplicados

                        var repartoDetalleManiana = new RepartoDetalleInfo();
                        if (t.RepartoID > 0)
                        {
                            IList<RepartoDetalleInfo> detalleRepartos =
                                repartoPl.ObtenerDetalle(new RepartoInfo {RepartoID = t.RepartoID});

                            if(detalleRepartos != null && detalleRepartos.Any())
                            {
                                repartoDetalleManiana =
                                    detalleRepartos.FirstOrDefault(
                                        det => det.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());
                            }

                        }
                        if (t.TipoServicioID == (int)TipoServicioEnum.Matutino)
                        //&& t.Servido == 1)
                        {
                            if (repartoDetalleManiana != null)
                            {
                                //cantidadManiana = t.CantidadServida;
                                if (t.FechaReparto.Date == DateTime.Now.Date && t.Servido == 1)
                                {
                                    t.NoModificar = true;
                                }
                                if (t.Servido == 1)
                                {
                                    cantidadManiana = repartoDetalleManiana.CantidadServida;
                                }
                                else
                                {
                                    cantidadManiana = repartoDetalleManiana.CantidadProgramada;
                                }
                                bandera = true;
                            }

                        }

                        if (bandera && t.TipoServicioID == (int)TipoServicioEnum.Vespertino)
                        {
                            if(t.CantidadProgramada == 0)
                            {
                                t.CantidadProgramada = 0;
                            }
                            else
                            {
                                t.CantidadProgramada = t.CantidadProgramada - cantidadManiana;
                            }

                            if(cantidadManiana == 0)
                            {
                                t.ValidaPorcentaje = 0;    
                            }
                            else
                            {
                                t.ValidaPorcentaje = 1;    
                            }
                            
                            bandera = false;
                            cantidadManiana = 0;
                        }
                    }
                    cambiosDetalle.AddRange(repartosInactivar);
                    valorRetorno = repartoPl.GenerarOrdenRepartoConfiguracionAjustes(cambiosDetalle.Where(cam=> cam.NoModificar == false).ToList());
                }
            }
            catch (Exception ex)
            {
                valorRetorno = -1;
            }
            return valorRetorno;
        }
    }
}