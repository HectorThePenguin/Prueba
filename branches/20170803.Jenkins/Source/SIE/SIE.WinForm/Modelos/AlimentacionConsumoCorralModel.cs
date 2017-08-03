using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using System;
using System.Linq;
using System.Windows;
using System.Text;
using SIE.Services.Integracion.DAL.Excepciones;
using SuKarne.Controls.MessageBox;


namespace SIE.WinForm.Modelos
{
    public class AlimentacionConsumoCorralModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public event EventHandler EnviarMensaje;

        private void notificarCambioPropiedad(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propiedad));
            }
        }

        public AlimentacionConsumoCorralModel()
        {
            reporte = new AlimentacionConsumoCorralReporte
                          {
                              Detalle = new ObservableCollection<AlimentacionConsumoCorralDetalle>(),
                              Totales = new ObservableCollection<AlimentacionConsumoCorralTotal>(),
                          };
        }

        #region Propiedades

        private OrganizacionInfo organizacion;

        public OrganizacionInfo Organizacion
        {
            get
            {
                return organizacion;
            }
            set
            {
                organizacion = value;
                notificarCambioPropiedad("Organizacion");
            }
        }

        CorralInfo corral;
        public CorralInfo Corral
        {
            get
            {
                return corral;
            }
            set
            {
                corral = value;
                Lote.CorralID = corral.CorralID;
                notificarCambioPropiedad("Corral");
            }
        }

        LoteInfo lote;
        public LoteInfo Lote
        {
            get
            {
                return lote;
            }
            set
            {
                lote = value;
                notificarCambioPropiedad("Lote");
            }
        }

        DateTime? fechaInicio;
        public DateTime? FechaInicio
        {
            get
            {
                return fechaInicio;
            }
            set
            {
                fechaInicio = value;
                notificarCambioPropiedad("FechaInicio");
            }
        }

        DateTime? fechaFin;
        public DateTime? FechaFin
        {
            get
            {
                return fechaFin;
            }
            set
            {
                fechaFin = value;
                notificarCambioPropiedad("FechaFin");
            }
        }

        bool estaOcupado;
        public bool EstaOcupado
        {
            get
            {
                return estaOcupado;
            }
            set
            {
                estaOcupado = value;
                notificarCambioPropiedad("EstaOcupado");
            }
        }

        bool puedeGenerarReporte;
        public bool PuedeGenerarReporte
        {
            get
            {
                return puedeGenerarReporte;
            }
            set
            {
                puedeGenerarReporte = value;
                notificarCambioPropiedad("PuedeGenerarReporte");
            }
        }

        AlimentacionConsumoCorralReporte reporte;
        public AlimentacionConsumoCorralReporte Reporte
        {
            get
            {
                return reporte;
            }
            set
            {
                reporte = value;
                if (reporte != null && reporte.Detalle.Count == 0)
                {
                    PuedeGenerarExcel = false;
                    MostrarBotones = Visibility.Hidden;
                }
                notificarCambioPropiedad("Reporte");
            }
        }

        private string rutaArchivo;
        public string RutaArchivo
        {
            get { return rutaArchivo; }
            set
            {
                rutaArchivo = value;
                notificarCambioPropiedad("RutaArchivo");
            }
        }

        private bool puedeGenerarExcel;
        public bool PuedeGenerarExcel
        {
            get { return puedeGenerarExcel; }
            set
            {
                puedeGenerarExcel = value;
                notificarCambioPropiedad("PuedeGenerarExcel");
            }
        }

        private Visibility mostrarBotones;

        public Visibility MostrarBotones
        {
            get { return mostrarBotones; }
            set
            {
                mostrarBotones = value;
                notificarCambioPropiedad("MostrarBotones");
            }
        }

        private DateTime? fechaInicioReporte;
        private DateTime? fechaFinaReporte;

        #endregion

        #region Metodos


        internal void TraerCorral(int OrganizacionID)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Corral.Codigo))
                    return;
                var corralPL = new SIE.Services.Servicios.PL.CorralPL();

                // Jose Angel Rodriguez Rodriguez 20/09/2014
                //La Primera ves k se mandaba llamar esto estava con un valor en 0, por eso se cambio a que desde la pantalla se tragera la OrganizacionID
                //var corral = corralPL.ObtenerCorralPorCodigo(Lote.OrganizacionID, Corral.Codigo);
                var corral = corralPL.ObtenerCorralPorCodigo(OrganizacionID, Corral.Codigo);

                if (corral == null)
                {
                    Corral = new CorralInfo();
                    Lote = new LoteInfo { OrganizacionID = Organizacion.OrganizacionID };
                    Lote.Corral = Corral;
                    FechaInicio = null;
                    FechaFin = null;
                    fechaFinaReporte = null;
                    fechaInicioReporte = null;
                    MostrarMensaje(Properties.Resources.Alimentacion_ConsumoCorral_MsgCorralNoEncontrado, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                Corral = corral;
                var lotePL = new SIE.Services.Servicios.PL.LotePL();
                Lote.Corral = Corral;
                Lote.CorralID = Corral.CorralID;
                Lote = lotePL.ObtenerLotePorCorral(Lote);
                if (Lote != null)
                {
                    TraerLote();
                }
                else
                {
                    Lote = new LoteInfo();
                    Lote.OrganizacionID = Organizacion.OrganizacionID;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MostrarMensaje(Properties.Resources.ReporteConsumoCorral_ErrorTraerCorral,
                                   MessageBoxButton.OK, MessageImage.Error);
            }
        }

        internal void TraerLote()
        {
            var lotePL = new SIE.Services.Servicios.PL.LotePL();
            Lote.Corral = Corral;
            this.FechaInicio = lote.FechaInicio;
            this.FechaFin = lotePL.ObtenerFechaUltimoConsumo(Lote.LoteID);

            fechaInicioReporte = FechaInicio;
            fechaFinaReporte = FechaFin;
            if (FechaFin != null)
            {
                PuedeGenerarReporte = true;
            }
        }
        internal void GenerarReporte(int organizacionId)
        {
            try
            {
                var bl = new ReporteConsumoCorralBL();
                bl.GenerarReporte(this.Lote.LoteID, this.FechaInicio.Value, this.FechaFin.Value, this.Reporte);
                if (reporte.Detalle == null || reporte.Detalle.Count == 0)
                {
                    MostrarMensaje(Properties.Resources.ReporteConsumoCorral_MsgSinInformacion,
                                   MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    ObservableCollection<AlimentacionConsumoCorralDetalle> rptDetalle = reporte.Detalle;
                    ObservableCollection<AlimentacionConsumoCorralTotal> rptTotales = reporte.Totales;
                    var rptCabeza = reporte;

                    List<AlimentacionConsumoDetalleInfo> datos;
                    List<AlimentacionConsumoCorralTotalInfo> datosFormula;

                    var organizacionPl = new OrganizacionPL();

                    var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                    var division = organizacion != null ? organizacion.Division : String.Empty;

                    string organizacionReporte = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + division + ")";


                    datos = (
                        from dato in rptDetalle
                        select new AlimentacionConsumoDetalleInfo
                        {
                            fecha = Convert.ToDateTime(dato.Fecha).Date,
                            formulaId = dato.FormulaId,
                            formula = dato.Formula ?? "",
                            cabezas = dato.Cabezas ?? 0,
                            kilosDia = dato.KilosDia,
                            servidosAcomulados = dato.ServidosAcomulados ?? 0,
                            diasAnimal = dato.DiasAnimal,
                            consumoDia = dato.ConsumoDia ?? 0,
                            promedioAcomulado = dato.PromedioAcomulado ?? 0,
                            precio = dato.Precio,
                            importe = dato.Importe
                        }).ToList();

                    datosFormula = (
                        from dato in rptTotales
                        group dato by dato.Formula into agrup
                        select new AlimentacionConsumoCorralTotalInfo()
                        {
                            formula = agrup.Key,
                            formulaID = agrup.First().FormulaId,
                            totalKilos = agrup.Sum(d=> d.TotalKilos ?? 0), //dato.TotalKilos ?? 0,
                            kilosTrans = agrup.Sum(d=> d.KilosTrans ?? 0),
                            kilosCorral = agrup.Sum(d=> d.KilosCorral ?? 0),
                            totalCosto = agrup.Sum(d=> d.TotalCosto ?? 0),
                            costoTrans = agrup.Sum(d=> d.CostoTrans ?? 0),
                            costoCorral = agrup.Sum(d=> d.CostoCorral ?? 0),
                            totalDiasAcomulado = agrup.Sum(d=> d.TotalDiasAcumulado ?? 0),
                            totalDiasAcomuladoTransferidos = agrup.Sum(d=> d.TotalDiasAcumuladoTransferidos ?? 0),
                            totalDiasAcumuladoCorral = agrup.Sum(d=> d.TotalDiasAcomuladoCorral ?? 0),
                            sumatoriaDiasAcumulados = agrup.Sum(d=> d.SumatoriaDiasAcumulados ?? 0),
                            sumatoriaDiasAcumuladosTransferidos = agrup.Sum(d=> d.SumatoriaDiasAcumuladosTransferidos),
                            sumatoriaKilos = agrup.Sum(d=> d.SumatoriaKilos ?? 0),
                            sumatoriaCosto = agrup.Sum(d=> d.SumatoriaCosto ?? 0),
                            sumatoriaKilosTransferencia = agrup.Sum(d=> d.SumatoriaKilosTransferencia ?? 0),
                            sumatoriaCostoTransferencia = agrup.Sum(d=> d.SumatoriaCostoTransferencia ?? 0),
                            sumatoriaCostoCorral = agrup.Sum(d=> d.SumatoriaCostoCorral ?? 0),
                            sumatoriaDiasAcumuladoCorral = agrup.Sum(d=> d.SumatoriaDiasAcumuladoCorral ?? 0),
                            sumatoriaKilosCorral = agrup.Sum(d=> d.SumatoriaKilosCorral ?? 0),
                        }).ToList();


                    //Agregar encabezado
                    foreach (var dato in datos)
                    {
                        dato.Titulo = Properties.Resources.AlimentoConsumoCorral_TituloReporte;
                        dato.FechaInicial = fechaInicio.Value;
                        dato.FechaFinal = fechaFin.Value;
                        dato.Organizacion = organizacionReporte;
                        dato.Corral = reporte.Corral;
                        dato.TipoGanado = reporte.TipoGanado ?? "";
                        dato.Proceso = reporte.Proceso ?? "";
                    }


                    var documento = new ReportDocument();
                    var strreporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\Rpt_AlimentacionConsumoCorral.rpt");
                    documento.Load(strreporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(datos);
                    documento.Subreports[0].SetDataSource(datosFormula);
                    documento.Refresh();
                    //documento.ExportToDisk(ExportFormatType.CharacterSeparatedValues,"C:\\Reporte.csv");

                    


                    var forma = new ReportViewer(documento, Properties.Resources.AlimentoConsumoCorral_TituloReporte);
                    forma.MostrarReporte();
                    forma.Show();

                    PuedeGenerarExcel = true;
                    MostrarBotones = Visibility.Visible;
                }
            }
            catch (Exception)
            {
                MostrarMensaje(Properties.Resources.RecepcionReporteEjecutivo_ErrorBuscar,
                                   MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        internal void ExportarReporte()
        {
            if (fechaInicioReporte == null && fechaFinaReporte == null)
            {
                PuedeGenerarExcel = false;
                MostrarBotones = Visibility.Hidden;
                MostrarMensaje(Properties.Resources.Reporte_MsgGeneradoConExito, MessageBoxButton.OK,
                                   MessageImage.Correct);
            }
            else
            {
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                string division =
                    AuxDivision.ObtenerDivision(organizacionID);
                var exportarPrincipal =
                    new ExportarExcel<AlimentacionConsumoCorralReporte>
                        {
                            Encabezados = ObtenerEncabezado(),
                            Datos = new List<AlimentacionConsumoCorralReporte> {Reporte},
                            TituloReporte = string.Format(Properties.Resources.Reporte_NombreEmpresa, division),
                            NombreReporte = Properties.Resources.ReporteConsumoCorral_Titulo,
                            SubTitulo =
                                string.Format(Properties.Resources.ReporteVentaMuerte_RangoFecha,
                                              fechaInicioReporte.Value.ToShortDateString(),
                                              fechaFinaReporte.Value.ToShortDateString()),
                            MostrarLogo = true,
                            NombreArchivo =
                                string.Format(Properties.Resources.ReporteConsumoCorral_NombreArchivo,
                                              DateTime.Now.ToShortDateString().Replace("/", "-"))
                        };

                var totalesDetalle = new List<AlimentacionConsumoCorralDetalle>();
                var totalDetalle = new AlimentacionConsumoCorralDetalle
                                       {
                                           DiasAnimal =
                                               (Reporte.Totales[0].SumatoriaDiasAcumulados.HasValue
                                                    ? Reporte.Totales[0].SumatoriaDiasAcumulados.Value
                                                    : 0),
                                           Importe =
                                               (Reporte.Totales[0].SumatoriaCosto.HasValue
                                                    ? Reporte.Totales[0].SumatoriaCosto.Value
                                                    : 0),
                                           KilosDia =
                                               (Reporte.Totales[0].SumatoriaKilos.HasValue
                                                    ? Reporte.Totales[0].SumatoriaKilos.Value
                                                    : 0),
                                           Fecha = Properties.Resources.Excel_LblTotal
                                       };
                totalesDetalle.Add(totalDetalle);
                var subtitulo = new StringBuilder();
                subtitulo.AppendFormat(Properties.Resources.ReporteVentaMuerte_RangoFecha,
                                       fechaInicioReporte.Value.ToShortDateString(),
                                       fechaFinaReporte.Value.ToShortDateString());
                subtitulo.AppendFormat("\r\n{0} {1}", Properties.Resources.ReporteConsumoCorral_Proveedor,
                                       Reporte.Proveedor);
                subtitulo.AppendFormat("\r\n{0} {1}", Properties.Resources.ReporteConsumoCorral_Corral,
                                       Reporte.Corral);
                subtitulo.AppendFormat("\r\n{0} {1}", Properties.Resources.ReporteConsumoCorral_TipoGanado,
                       Reporte.TipoGanado);
                subtitulo.AppendFormat("\r\n{0} {1}", Properties.Resources.ReporteConsumoCorral_Proceso,
                       Reporte.Proceso);
                var exportarDetalle =
                    new ExportarExcel<AlimentacionConsumoCorralDetalle>
                        {
                            Encabezados = ObtenerEncabezadoDetalle(),
                            Datos = Reporte.Detalle,
                            Totales = totalesDetalle,
                            TituloReporte = string.Format(Properties.Resources.Reporte_NombreEmpresa, division),
                            NombreReporte = Properties.Resources.ReporteConsumoCorral_Titulo,
                            SubTitulo = subtitulo.ToString(),
                            MostrarLogo = true,
                        };

                var totales = new List<AlimentacionConsumoCorralTotal>();
                var total = new AlimentacionConsumoCorralTotal
                                {
                                    TotalCosto = Reporte.Totales[0].SumatoriaCosto,
                                    TotalDiasAcumulado = Reporte.Totales[0].SumatoriaDiasAcumulados,
                                    TotalDiasAcumuladoTransferidos =
                                        reporte.Totales[0].SumatoriaDiasAcumuladosTransferidos,
                                    TotalKilos = Reporte.Totales[0].SumatoriaKilos,
                                    KilosTrans = Reporte.Totales[0].SumatoriaKilosTransferencia,
                                    CostoTrans = Reporte.Totales[0].SumatoriaCostoTransferencia,
                                    CostoCorral = Reporte.Totales[0].SumatoriaCostoCorral,
                                    KilosCorral = reporte.Totales[0].SumatoriaKilosCorral,
                                    TotalDiasAcomuladoCorral = reporte.Totales[0].SumatoriaDiasAcumuladoCorral,
                                    Formula = Properties.Resources.Excel_LblTotal,
                                };

                totales.Add(total);
                var exportarTotales =
                    new ExportarExcel<AlimentacionConsumoCorralTotal>
                        {
                            Encabezados = ObtenerEncabezadoTotales(),
                            Datos = Reporte.Totales,
                            Totales = totales,
                            TituloReporte = string.Format(Properties.Resources.Reporte_NombreEmpresa, division),
                            NombreReporte = Properties.Resources.ReporteConsumoCorral_Titulo,
                            SubTitulo = subtitulo.ToString(),
                            MostrarLogo = true,
                        };
                GeneraExcel(exportarPrincipal, exportarDetalle, exportarTotales);
            }
        }       

        internal void Cancelar()
        {
            MostrarMensaje(Properties.Resources.ReporteMedicamentosAplicados_Cancelar, MessageBoxButton.YesNo,
                           MessageImage.Warning);
        }

        internal void LimpiarCancelar()
        {
            PuedeGenerarExcel = false;
            MostrarBotones = Visibility.Hidden;
            Reporte = new AlimentacionConsumoCorralReporte();
            fechaFinaReporte = null;
            fechaInicioReporte = null;
            Limpiar();
        }

        internal void Limpiar()
        {
            this.Lote = new LoteInfo { Corral = new CorralInfo() };
            this.Lote.OrganizacionID = Organizacion != null ? Organizacion.OrganizacionID : 0;
            this.Corral = new CorralInfo();
            this.FechaFin = null;
            this.FechaInicio = null;
            this.PuedeGenerarReporte = false;
            this.RutaArchivo = string.Empty;
        }

        public void MostrarMensaje(string mensaje, MessageBoxButton boton, MessageImage imagen)
        {
            if (EnviarMensaje != null)
            {
                var msg = new
                              {
                                  mensaje,
                                  boton,
                                  imagen
                              };
                EnviarMensaje(msg, EventArgs.Empty);
            }
        }

        private void GeneraExcel(ExportarExcel<AlimentacionConsumoCorralReporte> exportar
                               , ExportarExcel<AlimentacionConsumoCorralDetalle> exportarDetalla
                               , ExportarExcel<AlimentacionConsumoCorralTotal> exportarTotales)
        {
            RutaArchivo = string.Empty;
            var thread = new Thread(delegate()
                {
                    try
                    {
                        RutaArchivo = exportar.GenerarReporte(exportarDetalla, exportarTotales);
                    }
                    catch (ExcepcionServicio ex)
                    {
                        Logger.Error(ex);
                        MostrarMensaje(ex.Message,
                                       MessageBoxButton.OK, MessageImage.Warning);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        MostrarMensaje(Properties.Resources.RecepcionReporteEjecutivo_MsgErrorExportarExcel,
                                       MessageBoxButton.OK, MessageImage.Error);

                    }
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (!string.IsNullOrWhiteSpace(RutaArchivo))
            {
                MostrarMensaje(Properties.Resources.Reporte_MsgGeneradoConExito, MessageBoxButton.OK,
                               MessageImage.Correct);
            }
        }

        /// <summary>
        /// Método para obtener el encabezado de las columnas
        /// </summary>
        /// <returns></returns>
        private string[] ObtenerEncabezado()
        {
            var encabezados =
                new[]
                    {
                        Properties.Resources.ReporteConsumoCorral_Proveedor,
                        Properties.Resources.ReporteConsumoCorral_Corral,
                        Properties.Resources.ReporteConsumoCorral_TipoGanado,
                        Properties.Resources.ReporteConsumoCorral_Proceso
                    };
            return encabezados;
        }

        private string[] ObtenerEncabezadoDetalle()
        {
            var encabezados =
                new[]
                    {
                        Properties.Resources.ReporteConsumoCorral_Fecha,
                        Properties.Resources.ReporteConsumoCorral_Formula,
                        Properties.Resources.ReporteConsumoCorral_Cabezas,
                        Properties.Resources.ReporteConsumoCorral_KilosDia,
                        Properties.Resources.ReporteConsumoCorral_ServidosAcumulados,
                        Properties.Resources.ReporteConsumoCorral_DiasAnimal,
                        Properties.Resources.ReporteConsumoCorral_ConsumoDia,
                        Properties.Resources.ReporteConsumoCorral_PromedioAcumulado,
                        Properties.Resources.ReporteConsumoCorral_Precio,
                        Properties.Resources.ReporteConsumoCorral_Importe
                    };
            return encabezados;
        }

        private string[] ObtenerEncabezadoTotales()
        {
            var encabezados =
                new[]
                    {
                        Properties.Resources.ReporteConsumoCorral_Formula,
                        Properties.Resources.ReporteConsumoCorral_Kilos,
                        Properties.Resources.ReporteConsumoCorral_KilosTransferencia,
                        Properties.Resources.ReporteConsumoCorral_KilosCorral,
                        Properties.Resources.ReporteConsumoCorral_TotalCosto,
                        Properties.Resources.ReporteConsumoCorral_CostoTransferencia,
                        Properties.Resources.ReporteConsumoCorral_CostoCorral,
                        Properties.Resources.ReporteConsumoCorral_TotalDiasAcumulado,
                        Properties.Resources.ReporteConsumoCorral_TotalDiasAcumuladoTransferidos,
                        Properties.Resources.ReporteConsumoCorral_TotalDiasAcumuladoCorral
                    };
            return encabezados;
        }

        #endregion
    }
}
