using System.Globalization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;
namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteSalidaConCostos.xaml
    /// </summary>
    public partial class ReporteSalidaConCostos
    {
        private OrganizacionInfo organizacionLocal;
        public SalidaConCostoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    inicializaContexto();
                }
                return (SalidaConCostoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        public ReporteSalidaConCostos()
        {
            InitializeComponent();
        }

        private void inicializaContexto()
        {
            Contexto = new SalidaConCostoInfo
            {
                
                Organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo
                            {
                                TipoProceso = new TipoProcesoInfo()
                            },
                            Iva = new IvaInfo
                                {
                                    CuentaPagar = new CuentaInfo
                                        {
                                            TipoCuenta = new TipoCuentaInfo()
                                        },
                                        CuentaRecuperar = new CuentaInfo
                                            {
                                                TipoCuenta = new TipoCuentaInfo()
                                            }
                                }
                    }
            };
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            inicializaContexto();
            inicializaCombos();
            inicializaAyudaOrganizacion();
            inicializaFechas();
            skAyudaOrganizacion.AsignarFoco();
            
        }

        private void inicializaFechas()
        {
            DtpFechaInicial.SelectedDate = DateTime.Now;
            DtpFechaFin.SelectedDate = DateTime.Now;
        }

        private void inicializaAyudaOrganizacion()
        {
            
            bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
            int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            OrganizacionPL organizacionPl = new OrganizacionPL();
            organizacionLocal = organizacionPl.ObtenerPorID(organizacionId);
            int tipoOrganizacion = organizacionLocal.TipoOrganizacion.TipoOrganizacionID;
            organizacionPl = new OrganizacionPL();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();

            if (usuarioCorporativo)
            {
                skAyudaOrganizacion.EsAyudaSimple = false;
                skAyudaOrganizacion.IsEnabled = true;

            }
            else
            {
                skAyudaOrganizacion.txtClave.Text = organizacionLocal.OrganizacionID.ToString();
                skAyudaOrganizacion.Id = organizacionLocal.OrganizacionID.ToString();
                skAyudaOrganizacion.Clave = organizacionLocal.OrganizacionID.ToString(CultureInfo.InvariantCulture);
                skAyudaOrganizacion.Descripcion = organizacionLocal.Descripcion;
                skAyudaOrganizacion.EsAyudaSimple = true;
                skAyudaOrganizacion.IsEnabled = false;
                skAyudaOrganizacion.UpdateLayout();
            }
            skAyudaOrganizacion.UpdateLayout();
        }

        private void inicializaCombos()
        {
            llenarComboTiposSalida();
            llenarComboTipoProceso();
            llenarComboVerCostos();
        }

        private void llenarComboTiposSalida()
        {
            TipoMovimientoPL movimientoPL = new TipoMovimientoPL();
            IList<TipoMovimientoInfo> lsTiposMovimiento = new List<TipoMovimientoInfo>();
            lsTiposMovimiento = movimientoPL.ObtenerTodos();

            lsTiposMovimiento = (from tipoMovimiento in lsTiposMovimiento
                                 where (tipoMovimiento.TipoMovimientoID == int.Parse(TipoMovimiento.SalidaPorVenta.GetHashCode().ToString()) ||
                                       tipoMovimiento.TipoMovimientoID == int.Parse(TipoMovimiento.Muerte.GetHashCode().ToString()) ||
                                       tipoMovimiento.TipoMovimientoID == int.Parse(TipoMovimiento.SalidaPorSacrificio.GetHashCode().ToString())
                                       ) && tipoMovimiento.EsGanado && tipoMovimiento.EsSalida
                                 select tipoMovimiento).ToList<TipoMovimientoInfo>();
            cmbTipoSalida.ItemsSource = lsTiposMovimiento;
        }

        private void llenarComboTipoProceso()
        {
            TipoProcesoPL procesoPL = new TipoProcesoPL();
            IList<TipoProcesoInfo> lsTiposProceso = new List<TipoProcesoInfo>();
            lsTiposProceso = procesoPL.ObtenerTodos();

            lsTiposProceso = (from proceso in lsTiposProceso
                              where proceso.Activo == EstatusEnum.Activo
                              select proceso).ToList<TipoProcesoInfo>();

            cmbTipoProceso.ItemsSource = lsTiposProceso;
            
        }

        private void llenarComboVerCostos()
        {
            cmbVerCostos.Items.Add("Agrupados");
            cmbVerCostos.Items.Add("Detallados");
            cmbVerCostos.SelectedIndex = 0;
        }

        private Boolean validarDatos()
        {
            Boolean bResultado = false;
            String sMensaje = String.Empty;
            DateTime dtFechaActual = new DateTime();
            TimeSpan? tsDiferencia = new TimeSpan();
            dtFechaActual = DateTime.Now;
            if (DtpFechaInicial.SelectedDate == null && DtpFechaFin.SelectedDate == null)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_MsgErrorFechasObligatorias;
            }
            else if(skAyudaOrganizacion.Descripcion.Trim().Length == 0)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_MsgOrganizacionObligatoria;
            }
            else if(cmbTipoSalida.SelectedIndex < 0)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_MsgTipoSalidaObligatoria;
            }
            else if(cmbTipoProceso.SelectedIndex < 0)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_MsgTipoProcesoObligatorio;
            }
            else if (DtpFechaFin.SelectedDate < DtpFechaInicial.SelectedDate)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_Msg_ErrorFechaFin;
            }
            else if (DtpFechaInicial.SelectedDate.Value.Date > dtFechaActual.Date)
            {
                sMensaje = Properties.Resources.ReporteSalidaConCosto_Msg_ErrorFechaInicial;
            }
            else
            {
                tsDiferencia = DtpFechaInicial.SelectedDate.Value.Date - dtFechaActual;
                if (tsDiferencia.Value.Days > 90)
                {
                    sMensaje = Properties.Resources.ReporteSalidaConCosto_Msg_RangoFechas;
                }
                else
                {
                    bResultado = true;
                }

            }
            
            if (!bResultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      sMensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
            }
            return bResultado;
        }

        private void LimpiarControles()
        {
            skAyudaOrganizacion.Clave = "0";
            inicializaFechas();
            inicializaAyudaOrganizacion();
            cmbTipoProceso.SelectedIndex = -1;
            cmbTipoSalida.SelectedIndex = -1;
            cmbVerCostos.SelectedIndex = 0;
            skAyudaOrganizacion.AsignarFoco();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (validarDatos())
            {
                generarReporte();
            }
            
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControles();
        }

        private void generarReporte()
        {
            DateTime dtFechaInicial = new DateTime(DtpFechaInicial.SelectedDate.Value.Year, DtpFechaInicial.SelectedDate.Value.Month, DtpFechaInicial.SelectedDate.Value.Day);
            DateTime dtFechaFinal = new DateTime(DtpFechaFin.SelectedDate.Value.Year, DtpFechaFin.SelectedDate.Value.Month,DtpFechaFin.SelectedDate.Value.Day);
            int iOrganizacionID = int.Parse(skAyudaOrganizacion.Clave);
            int iTipoSalida = int.Parse(cmbTipoSalida.SelectedValue.ToString());
            int iTipoProceso = int.Parse(cmbTipoProceso.SelectedValue.ToString());
            String sVistaCostos = cmbVerCostos.Text;
            ReporteSalidaConCostosBL salidaBL = new ReporteSalidaConCostosBL();
            Boolean bSalidaVentaMuerte = false;
            Boolean bDetallado = false;
            SalidaConCostoInfo salidas = new SalidaConCostoInfo();

            if (sVistaCostos.ToUpper().Trim() == "DETALLADOS")
            {
                bDetallado = true;
            }
            ReporteSalidasConCostoParametrosInfo parametros = new ReporteSalidasConCostoParametrosInfo
            {
                OrganizacionID = iOrganizacionID,
                FechaInicial = dtFechaInicial,
                FechaFinal = dtFechaFinal,
                TipoSalida = iTipoSalida,
                TipoProceso = iTipoProceso,
                EsDetallado = bDetallado
            };
            IList<ReporteSalidaConCostosInfo> resultadoInfo = salidaBL.obtenerReporte(parametros);

            if (resultadoInfo != null)
            {

                if (TipoMovimiento.Muerte.GetHashCode().ToString() == iTipoSalida.ToString() || TipoMovimiento.Muerte.GetHashCode().ToString() == iTipoSalida.ToString())
                {
                    bSalidaVentaMuerte = true;
                }



                foreach (ReporteSalidaConCostosInfo salida in resultadoInfo)
                {
                    salida.esSalidaVentaOMuerte = bSalidaVentaMuerte;
                    salida.esDetallado = bDetallado;
                    salida.Leyenda =  organizacionLocal.Descripcion;
                    salida.RangoFechas = "De " + dtFechaInicial.Day.ToString().PadLeft(2, '0') + "-" + dtFechaInicial.Month.ToString().PadLeft(2, '0') + dtFechaInicial.Year.ToString() +
                                        " hasta" + dtFechaFinal.Day.ToString().PadLeft(2, '0') + "-" + dtFechaFinal.Month.ToString().PadLeft(2, '0') + dtFechaFinal.Year.ToString();
                    salida.NombreReporte = Properties.Resources.ReporteSalidaConCosto_NombreReporte;

                }

                var encabezado = new ReporteEncabezadoInfo
                {
                    Titulo = Properties.Resources.ReporteSalidaConCosto_Titulo,
                    FechaInicio = dtFechaInicial,
                    FechaFin = dtFechaFinal,
                    Organizacion = Properties.Resources.ReporteSalidaConCosto_Titulo + " (" + organizacionLocal.Division + ")"
                };

                var documento = new ReportDocument();
                String directorioBase = String.Empty;
                if (AppDomain.CurrentDomain.BaseDirectory.Substring(AppDomain.CurrentDomain.BaseDirectory.Length - 1, 1) == "\\")
                {
                    directorioBase = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1);
                }
                else
                {
                    directorioBase = AppDomain.CurrentDomain.BaseDirectory.ToString();
                }
                var reporte = String.Format("{0}{1}", directorioBase, "\\Reporte\\RptReporteSalidaConCostosDetallado.rpt");
                documento.Load(reporte);

                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();

                var forma = new ReportViewer(documento, encabezado.Titulo);
                forma.MostrarReporte();
                forma.Show();
                for (int i = 0; i < resultadoInfo.Count; i++)
                {
                    GC.SuppressFinalize(resultadoInfo[i]);
                    
                }
                GC.SuppressFinalize(resultadoInfo);
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteSalidaConCosto_Msg_SinInformacion,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            
        }

    }
}
