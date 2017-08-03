using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
//using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Reflection;
using System.Drawing;
using MessageBox = System.Windows.MessageBox;

//using HorizontalAlignment = System.Windows.HorizontalAlignment;
//using Label = System.Windows.Controls.Label;
//using System.Collections.ObjectModel;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ConcialiacionPolizasSIAP_SAP.xaml
    /// </summary>
    public partial class ConcialiacionPolizasSIAP_SAP
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConcialiacionPolizasSIAP_SAP()
        {
            InitializeComponent();
            polizasFaltantes = new List<ConciliacionPolizasSiapSapModel>();
            listCuentas.ItemsSource = polizasFaltantes.OrderBy(x => x.Cuenta).ToList();
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// 
        /// </summary>
        private List<ConciliacionPolizasSiapSapModel> polizasFaltantes;
        #endregion

        #region Eventos

        /// <summary>
        /// Evento del boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Limpiar();

            var file = new OpenFileDialog
            {
                Filter = "Documento de Excel|*.xlsx",
                Title = Properties.Resources.ConciliacionPolizasSIAPSAP_lblHeaderFileDialog
            };

            var result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                
                if (!string.IsNullOrWhiteSpace(file.FileName))
                {
                    if (File.Exists(file.FileName))
                    {
                        txtRuta.Text = file.FileName;
                    }
                }
            }
        }

        /// <summary>
        /// Evento del boton conciliar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConciliarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                desahabilitarControles(false,false);

                
                    if (validarDatosEntrada())
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format(Properties.Resources.ConciliacionPolizasSIAPSAP_ConciliacionMSJ, DtpFechaInicial.SelectedDate.Value.ToShortDateString(), DtpFechafinal.SelectedDate.Value.ToShortDateString(), cmbTipoCuenta.SelectedValue.ToString()),
                                MessageBoxButton.OKCancel, MessageImage.Warning) == MessageBoxResult.OK)
                        {
                            var prefijo = cmbTipoCuenta.SelectedValue.ToString();
                            List<PolizaSAPInfo> polizasSap = LeerArchivoExcel();
                            if (polizasSap != null && polizasSap.Any())
                            {
                                var polizasSiap = ObtenerPolizas(polizasSap, prefijo);

                                if (polizasSiap == null || !polizasSiap.Any())
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeNoExistenMovimientos,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                    desahabilitarControles(true, false);
                                }
                                else
                                {
                                    GenerarConciliacion(polizasSap, polizasSiap);
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeNoExistenMovimientosExcel,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                desahabilitarControles(true, false);
                            }
                        }
                        else
                        {
                            desahabilitarControles(true, false);
                        }
                    }
                    else
                    {
                        desahabilitarControles(true,false);
                    }
                


                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// Limpiar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiarClick(object sender, RoutedEventArgs e)
        {
            Limpiar();
            
        }

        private void Limpiar()
        {
            listCuentas.ItemsSource = new List<ConciliacionPolizasSiapSapModel>();
            txtRuta.Text = string.Empty;
            txtCuenta.Text = string.Empty;
            desahabilitarControles(true, false);
            DtpFechafinal.SelectedDate = null;
            DtpFechaInicial.SelectedDate = null;
            btnFiltro.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            txtCuenta.IsEnabled = false;
            cmbTipoCuenta.SelectedIndex = -1;
            
        }

        /// <summary>
        /// Realizar filtro por cuenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltro_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtCuenta.Text != string.Empty)
            {
                if (polizasFaltantes.Any())
                {
                    var listaFiltro = polizasFaltantes.Where(c => c.Cuenta.Equals(txtCuenta.Text)).ToList();
                    if (listaFiltro.Any())
                    {
                        listCuentas.ItemsSource = new List<ConciliacionPolizasSiapSapModel>();
                        listCuentas.ItemsSource = listaFiltro.OrderBy(x => x.Cuenta).ToList();
                        var view = (CollectionView)CollectionViewSource.GetDefaultView(listCuentas.ItemsSource);
                        var groupDescription = new PropertyGroupDescription("Cuenta");
                        if (view.GroupDescriptions != null) view.GroupDescriptions.Add(groupDescription);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConciliacionPolizasSIAPSAP_BtnFiltroNoResultados, MessageBoxButton.OK,
                                 MessageImage.Warning);
                    }

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_BtnFiltroNoDiferencias, MessageBoxButton.OK,
                                  MessageImage.Warning);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_BtnFiltroCapturaCuenta, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }

        }


        /// <summary>
        /// Cancelar filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            txtCuenta.Text = string.Empty;
            listCuentas.ItemsSource = new List<ConciliacionPolizasSiapSapModel>();
            listCuentas.ItemsSource = polizasFaltantes.OrderBy(x => x.Cuenta).ToList();
            var view = (CollectionView)CollectionViewSource.GetDefaultView(listCuentas.ItemsSource);
            var groupDescription = new PropertyGroupDescription("Cuenta");
            if (view.GroupDescriptions != null) view.GroupDescriptions.Add(groupDescription);
        }

        /// <summary>
        /// Evento para exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtExportar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                desahabilitarControles(false, false);
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConciliacionPolizasSIAPSAP_ExportarMej,
                                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                        {
                            if (polizasFaltantes.Any())
                            {
                                
                                string[] cabezeras = new string[14];

                                cabezeras[1] = "Cuenta";
                                cabezeras[2] = "Fecha";
                                cabezeras[3] = "Fecha_Contable";
                                cabezeras[4] = "Folio";
                                cabezeras[5] = "Cargo-SAP";
                                cabezeras[6] = "Abono-SAP";
                                cabezeras[7] = "Cargo-SIAP";
                                cabezeras[8] = "Abono-SIAP";
                                cabezeras[9] = "Diferencia";
                                cabezeras[10] = "Póliza SAP";
                                cabezeras[11] = "Póliza SIAP";
                                cabezeras[12] = "Referencia3";
                                cabezeras[13] = "Concepto";
                                cabezeras[0] = "Total";

                                var datos = polizasFaltantes.OrderBy(x => x.Cuenta).ToList();
                                Generar(datos, cabezeras);
                             }
                        }
                else
                {
                    desahabilitarControles(true,true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConciliacionPolizasSIAPSAP_errorExportarExcel, MessageBoxButton.OK,
                    MessageImage.Error);
            }

        }

        private void desahabilitarControles(bool seleccion, bool conciliacion)
        {
            btnBuscar.IsEnabled = seleccion;
            btnLimpiar.IsEnabled = seleccion;
            btnConciliar.IsEnabled = seleccion;
            btnExportar.IsEnabled = conciliacion;
            btnMostrarVista.IsEnabled = conciliacion;
            DtpFechaInicial.IsEnabled = seleccion;
            DtpFechafinal.IsEnabled = seleccion;
            cmbTipoCuenta.IsEnabled = seleccion;
            txtRuta.IsEnabled = seleccion;
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Método que genera el arbol con los datos de la conciliación
        /// </summary>
        /// <param name="polizasSap">Listado de pólizas del SAP</param>
        /// <param name="polizasSiap">Listado de pólizas del SIAP</param>
        private void GenerarConciliacion(List<PolizaSAPInfo> polizasSap, List<PolizaInfo> polizasSiap)
        {
            try
            {
                listCuentas.ItemsSource = new List<ConciliacionPolizasSiapSapModel>();

                var sapLista = polizasSap.Select(x => new
                {
                    x.Cuenta,
                    FechaDocumento = string.Format("{2}/{1}/{0}", x.FechaDocumento.Split('.')[0].PadLeft(2, '0'), x.FechaDocumento.Split('.')[1].PadLeft(2, '0'), x.FechaDocumento.Split('.')[2].PadLeft(2, '0')),
                    FechaContable = string.Format("{2}/{1}/{0}", x.FechaContable.Split('.')[0].PadLeft(2, '0'), x.FechaContable.Split('.')[1].PadLeft(2, '0'), x.FechaContable.Split('.')[2].PadLeft(2, '0')),
                    Referencia = x.Ref3,
                    x.Referencia3,
                    Concepto = x.Concepto.ToUpper(),
                    Importe = decimal.Parse(x.Importe, CultureInfo.InvariantCulture),
                    FolioMovimiento = x.Asignacion,
                    PolizaSap = x.NumeroDocumento,
                    PolizaSiap =string.Empty
                }).ToList();

                var siapLista = polizasSiap.Select(x => new
                {
                    x.Cuenta,
                    FechaDocumento = string.Format("{0}/{1}/{2}", x.FechaDocumento.Substring(6, 2), x.FechaDocumento.Substring(4, 2), x.FechaDocumento.Substring(0, 4)),
                    FechaContable = string.Format("{0}/{1}/{2}", x.FechaContabilidad.Substring(6, 2), x.FechaContabilidad.Substring(4, 2), x.FechaContabilidad.Substring(0, 4)),
                    Referencia = x.Referencia3.Replace(" ", ""),
                    x.Referencia3,
                    Concepto = x.Concepto.ToUpper(),
                    Importe = decimal.Parse(x.Importe, CultureInfo.InvariantCulture),
                    FolioMovimiento = x.NumeroReferencia,
                    PolizaSap = String.Empty,
                    PolizaSiap = x.DocumentoSAP
                    
                }).ToList();

                for (int i = 0; i < polizasSap.Count; i++)
                {
                    GC.SuppressFinalize(polizasSap[i]);
                }

                for (int i = 0; i < polizasSiap.Count; i++)
                {
                    GC.SuppressFinalize(polizasSiap[i]);
                }

                // Obtenemos las coincidencias de las dos listas.
                var igualesSAP = (from sap in sapLista
                                  join siap in siapLista
                                 on new
                                 {
                                     a = sap.FechaDocumento,
                                     b = sap.Referencia,
                                     c = sap.Cuenta,
                                     d = sap.Concepto,
                                     e = sap.Importe
                                 }
                                 equals new
                                 {
                                     a = siap.FechaDocumento,
                                     b = siap.Referencia,
                                     c = siap.Cuenta,
                                     d = siap.Concepto,
                                     e = siap.Importe
                                 }
                                  select sap).ToList();

                // Eliminamos de los listados las coincidencias.

                // Eliminamos de los listados las coincidencias de la lista de SAP.

                // Obtenemos las coincidencias de las dos listas.
                var igualesSIAP = (from sap in sapLista
                                   join siap in siapLista
                                  on new
                                  {
                                      a = sap.FechaDocumento,
                                      b = sap.Referencia,
                                      c = sap.Cuenta,
                                      d = sap.Concepto,
                                      e = sap.Importe
                                  }
                                  equals new
                                  {
                                      a = siap.FechaDocumento,
                                      b = siap.Referencia,
                                      c = siap.Cuenta,
                                      d = siap.Concepto,
                                      e = siap.Importe
                                  }
                                   select siap).ToList();

                for (int i = 0; i < igualesSAP.Count; i++)
                {
                    sapLista.Remove(igualesSAP[i]);
                    GC.SuppressFinalize(igualesSAP[i]);
                }
                igualesSAP.Clear();


                for (int i = 0; i < igualesSIAP.Count; i++)
                {
                    siapLista.Remove(igualesSIAP[i]);
                    GC.SuppressFinalize(igualesSIAP[i]);
                }

                igualesSIAP.Clear();
                // Se recorre la lista para generar la lista con las diferencias de las polizas del sap.
                polizasFaltantes = (from sap in sapLista
                                    let d = siapLista.Where(x => x.Referencia == sap.Referencia && x.Concepto == sap.Concepto).Select(x => x.Importe).FirstOrDefault<decimal>()
                                    let diferencia = d != 0 ? d - sap.Importe : sap.Importe
                                    select new ConciliacionPolizasSiapSapModel
                                    {
                                        Concepto = sap.Concepto,
                                        Cuenta = sap.Cuenta,
                                        FechaDocumento = sap.FechaDocumento,
                                        FechaContable = sap.FechaContable,
                                        FolioMovimiento = sap.FolioMovimiento,
                                        SapCargo = sap.Importe > 0 ? sap.Importe.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                        SapAbono = sap.Importe < 0 ? sap.Importe.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                        SiapCargo = d > 0 ? d.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                        SiapAbono = d < 0 ? d.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                        Diferencia = diferencia.ToString(CultureInfo.InvariantCulture),
                                        Ref3 = sap.Referencia3,
                                        PolizaSap = sap.PolizaSap,
                                        PolizaSiap = string.Empty
                                    }).ToList();

                // Se recorre la lista para generar la lista con las diferencias de las polizas del siap.
                polizasFaltantes.AddRange(from siap in siapLista
                                          let d = sapLista.Where(x => x.Referencia == siap.Referencia && x.Concepto == siap.Concepto).Select(x => x.Importe).FirstOrDefault<decimal>()
                                          let diferencia = d != 0 ? d - siap.Importe : siap.Importe
                                          select new ConciliacionPolizasSiapSapModel
                                          {
                                              Concepto = siap.Concepto,
                                              Cuenta = siap.Cuenta,
                                              FechaDocumento = siap.FechaDocumento,
                                              FechaContable = siap.FechaContable,
                                              FolioMovimiento = siap.FolioMovimiento,
                                              SapCargo = d > 0 ? d.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                              SapAbono = d < 0 ? d.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                              SiapCargo = siap.Importe > 0 ? siap.Importe.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                              SiapAbono = siap.Importe < 0 ? siap.Importe.ToString(CultureInfo.InvariantCulture) : string.Empty,
                                              Diferencia = diferencia.ToString(CultureInfo.InvariantCulture),
                                              Ref3 = siap.Referencia3,
                                              PolizaSap = string.Empty,
                                              PolizaSiap = siap.PolizaSiap
                                          });


                for (int i = 0; i < siapLista.Count; i++)
                {
                    GC.SuppressFinalize(siapLista[i]);
                }
                siapLista.Clear();

                for (int i = 0; i < sapLista.Count; i++)
                {
                    GC.SuppressFinalize(sapLista[i]);
                }
                sapLista.Clear();

                if (polizasFaltantes.Any())
                {
                    // Se realiza la suma de las cantidades, para agregar un renglon mas a cada grupo de datos.
                    var lista = (from r in polizasFaltantes
                        group r by new
                        {
                            r.Cuenta
                        }
                        into g
                        select new ConciliacionPolizasSiapSapModel
                        {
                            Cuenta = g.Key.Cuenta,
                            SapAbono =
                                g.Sum(x => x.SapAbono == string.Empty ? 0 : decimal.Parse(x.SapAbono, CultureInfo.InvariantCulture))
                                    .ToString(CultureInfo.InvariantCulture),
                            SapCargo =
                                g.Sum(x => x.SapCargo == string.Empty ? 0 : decimal.Parse(x.SapCargo, CultureInfo.InvariantCulture))
                                    .ToString(CultureInfo.InvariantCulture),
                            SiapAbono =
                                g.Sum(x => x.SiapAbono == string.Empty ? 0 : decimal.Parse(x.SiapAbono, CultureInfo.InvariantCulture))
                                    .ToString(CultureInfo.InvariantCulture),
                            SiapCargo =
                                g.Sum(x => x.SiapCargo == string.Empty ? 0 : decimal.Parse(x.SiapCargo, CultureInfo.InvariantCulture))
                                    .ToString(CultureInfo.InvariantCulture),
                            Diferencia =
                                g.Sum(x => x.Diferencia == string.Empty ? 0 : decimal.Parse(x.Diferencia, CultureInfo.InvariantCulture))
                                    .ToString(CultureInfo.InvariantCulture),
                            Total = Properties.Resources.ConciliacionPolizasSIAPSAP_lblTotalPorCuenta
                        }).ToList();

                    polizasFaltantes.AddRange(lista);

                    desahabilitarControles(true,true);

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_NoDiferencias, MessageBoxButton.OK,
                                  MessageImage.Correct);
                    desahabilitarControles(true,false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_errorGenerarConciliacion, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Método que lee el archivo de Excel
        /// </summary>
        /// <returns>Regresa una lista con el contenido del excel</returns>
        private List<PolizaSAPInfo> LeerArchivoExcel()
        {
            var polizasSap = new List<PolizaSAPInfo>();
            string prefijo = string.Empty;
            FileInfo nombreArchivo = new FileInfo(txtRuta.Text.ToString());
            const int renglonInicio = 2;
            try
            {

                if (validarDatosEntrada())
                {

                    prefijo = cmbTipoCuenta.SelectedValue.ToString();

                    using (var package = new ExcelPackage(nombreArchivo))
                    {
                        ExcelWorkbook workbook = package.Workbook;
                        if (workbook != null && workbook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet currentworksheet = workbook.Worksheets.First();

                            for (int renglon = renglonInicio + 1; renglon < currentworksheet.Dimension.End.Row; renglon++)
                            {
                                object oSociedad = currentworksheet.Cells[renglon, 2].Value;
                                object oDivision = currentworksheet.Cells[renglon, 3].Value;
                                string oCuenta = currentworksheet.Cells[renglon, 4].Value.ToString();
                                object oAsignacion = currentworksheet.Cells[renglon, 5].Value;
                                object oNumeroDocumento = currentworksheet.Cells[renglon, 7].Value;
                                object oClaseDocumento = currentworksheet.Cells[renglon, 8].Value;
                                object oFechaContable = currentworksheet.Cells[renglon, 9].Value;
                                object oFechaDocumento = currentworksheet.Cells[renglon, 10].Value;
                                object oImporte = currentworksheet.Cells[renglon, 12].Value;
                                object oMoneda = currentworksheet.Cells[renglon, 13].Value;
                                object oRef3 = currentworksheet.Cells[renglon, 1].Value;
                                object oConcepto = currentworksheet.Cells[renglon, 16].Value;

                                try
                                {
                                    if (oDivision != null)
                                    {
                                        if (!string.IsNullOrEmpty(oCuenta)
                                            && oCuenta != string.Empty
                                            && oCuenta.Trim().Length >= prefijo.Length
                                            && oCuenta.Substring(0, prefijo.Length) == prefijo)
                                        {
                                            polizasSap.Add(new PolizaSAPInfo
                                            {
                                                Sociedad = oSociedad == null ? string.Empty : oSociedad.ToString(),
                                                Division = string.IsNullOrEmpty(oDivision.ToString())  ? string.Empty : oDivision.ToString(),
                                                Cuenta = oCuenta,
                                                Asignacion = oAsignacion == null ? string.Empty : oAsignacion.ToString(),
                                                NumeroDocumento = oNumeroDocumento == null ? string.Empty : oNumeroDocumento.ToString(),
                                                ClaseDocumento = oClaseDocumento == null ? string.Empty : oClaseDocumento.ToString(),
                                                FechaContable = ((DateTime)oFechaContable).Year + "." + ((DateTime)oFechaContable).Month + "." + ((DateTime)oFechaContable).Day,
                                                FechaDocumento = ((DateTime)oFechaDocumento).Year + "." + ((DateTime)oFechaDocumento).Month + "." + ((DateTime)oFechaDocumento).Day,
                                                Importe = oImporte == null ? string.Empty : ((double)oImporte).ToString(CultureInfo.InvariantCulture),
                                                Moneda = oMoneda == null ? string.Empty : oMoneda.ToString(),
                                                Ref3 = oRef3 == null ? string.Empty : oRef3.ToString().Replace(" ", ""),
                                                Referencia3 = oRef3 == null ? string.Empty : oRef3.ToString(),
                                                Concepto = oConcepto == null ? "  " : oConcepto.ToString().Replace("\"", ""),
                                                ClavePoliza = oConcepto == null ? "  " : oConcepto.ToString().Replace("\"", "").Split('-')[0].ToUpper(),
                                                // Periodo = Convert.ToString(dr["Periodo"]),
                                            });
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    e.ToString();
                                    throw;
                                }
                            }
                        }
                        
                    }
                   

                    if (polizasSap.Any())
                    {
                        polizasSap =
                            polizasSap.Where(
                                p =>
                                    Convert.ToDateTime(p.FechaDocumento) >= DtpFechaInicial.SelectedDate &&
                                    Convert.ToDateTime(p.FechaDocumento) <= DtpFechafinal.SelectedDate).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeErrorAlLeerArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
                desahabilitarControles(true,false);
                throw;

            }
            return polizasSap;
        }

        /// <summary>
        /// Método que obtiene el listado de pólizas generadas en el SIAP
        /// </summary>
        /// <param name="polizasSap">El el listado de pólizas obtenenidas del documento, de este se obtiene la fecha mínima y máxima.</param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerPolizas(List<PolizaSAPInfo> polizasSap, string prefijo)
        {
            try
            {
                
                // Se obtienen las diferentes divisiones del documento
                var divisiones = polizasSap.GroupBy(o => o.Division).Select(x => x.FirstOrDefault()).Select(x => x.Division).ToList();


                // Se obtienen la fecha incial y final del documento para limitar el filtrado
                var fechaInicial = DtpFechaInicial.SelectedDate.Value;
                var fechaFinal = DtpFechafinal.SelectedDate.Value;

                var cociliacionParametros = new ConciliaciionParametros
                {
                    diviciones = divisiones,
                    Organizacion = new OrganizacionInfo { OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario() },
                    fechaInicio = fechaInicial,
                    fechaFin = fechaFinal,
                    Prefijo = prefijo
                    //    Cuentas = cuentas

                };

                var polizaPl = new PolizaPL();
                return polizaPl.ObtenerPolizasConciliacionSapSiap(cociliacionParametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeErrorAlConsultarPolizas, MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
        }

        /// <summary>
        /// Generar el archivo excel
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="encabezados"></param>
        public void Generar(List<ConciliacionPolizasSiapSapModel> datos
            , string[] encabezados)
        {
            try
            {


                using (ExcelPackage p = new ExcelPackage())
                {

                    //Here setting some document properties

                    p.Workbook.Properties.Author = "Sukarne";

                    p.Workbook.Properties.Title = "Conciliación SAP vs SIAP";



                    //Create a sheet

                    p.Workbook.Worksheets.Add("Conciliacion");

                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    ws.Name = "Conciliación"; //Setting Sheet's name

                    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet

                    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                    ws.Cells[1, 1].Value = "Conciliación SAP vs SIAP";

                    ws.Cells[1, 1, 1, encabezados.Count()].Merge = true;

                    ws.Cells[1, 1, 1, encabezados.Count()].Style.Font.Bold = true;

                    ws.Cells[1, 1, 1, encabezados.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;

                    int rowIndex = 2;

                    foreach (string encabezado in encabezados) //Creating Headings
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        //Setting the background color of header cells to Gray

                        var fill = cell.Style.Fill;

                        fill.PatternType = ExcelFillStyle.Solid;

                        fill.BackgroundColor.SetColor(Color.Gray);
                        //Setting Top/left,right/bottom borders.

                        var border = cell.Style.Border;

                        border.Bottom.Style =

                            border.Top.Style =

                                border.Left.Style =

                                    border.Right.Style = ExcelBorderStyle.Thin;
                        //Setting Value in cell

                        cell.Value = encabezado;
                        colIndex++;
                    }
                    var propiedades = ObtenerPropiedades();
                    //var valores = new object[datos.Count, propiedades.Length];
                    for (int j = 0; j < datos.Count; j++)
                    {
                        colIndex = 1;
                        rowIndex++;
                        ConciliacionPolizasSiapSapModel item = datos[j];
                        for (int i = 0; i < propiedades.Length; i++)
                        {

                            //    border.Right.Style = ExcelBorderStyle.Thin;
                            if (string.IsNullOrEmpty(datos[j].Total))
                            {
                                ws.Cells[rowIndex, 2].Value = datos[j].Cuenta;
                                ws.Cells[rowIndex, 3].Value = datos[j].FechaDocumento;
                                ws.Cells[rowIndex, 4].Value = datos[j].FechaContable;
                                ws.Cells[rowIndex, 5].Value = datos[j].FolioMovimiento;
                                ws.Cells[rowIndex, 6].Value = datos[j].SapCargo;
                                ws.Cells[rowIndex, 7].Value = datos[j].SapAbono;
                                ws.Cells[rowIndex, 8].Value = datos[j].SiapCargo;
                                ws.Cells[rowIndex, 9].Value = datos[j].SiapAbono;
                                ws.Cells[rowIndex, 10].Value = datos[j].Diferencia;
                                ws.Cells[rowIndex, 11].Value = datos[j].PolizaSap;
                                ws.Cells[rowIndex, 12].Value = datos[j].PolizaSiap;
                                ws.Cells[rowIndex, 13].Value = datos[j].Ref3;
                                ws.Cells[rowIndex, 14].Value = datos[j].Concepto;
                            }
                            else
                            {
                                ws.Cells[rowIndex, 1].Value = "TOTAL";
                                ws.Cells[rowIndex, 6].Value = datos[j].SapCargo;
                                ws.Cells[rowIndex, 7].Value = datos[j].SapAbono;
                                ws.Cells[rowIndex, 8].Value = datos[j].SiapCargo;
                                ws.Cells[rowIndex, 9].Value = datos[j].SiapAbono;
                                ws.Cells[rowIndex, 10].Value = datos[j].Diferencia;
                            }

                            colIndex++;

                            //valores[j, i] = (y == null) ? "" : y;
                        }
                    }

                    Byte[] bin = p.GetAsByteArray();

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.FileName = "Conciliacion" + DateTime.Now.Year +
                                               DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                               DateTime.Now.Day.ToString().PadLeft(2, '0') + "_" +
                                               DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                                               DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                                               DateTime.Now.Second.ToString().PadLeft(2, '0') + ".xlsx";
                    //saveFileDialog1.Filter = "Excel File|(*.xlsx)";
                    saveFileDialog1.Title = "Save an Excel File";
                    saveFileDialog1.ShowDialog();

                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog1.FileName != "")
                    {
                        string file = saveFileDialog1.FileName;
                        File.WriteAllBytes(file, bin);
                    }



                }
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConciliacionPolizasSIAPSAP_ExitoExportar, MessageBoxButton.OK,
                    MessageImage.Correct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConciliacionPolizasSIAPSAP_errorExportarExcel, MessageBoxButton.OK,
                    MessageImage.Error);
            }
            finally
            {
                desahabilitarControles(true,true);
            }

        }
        /// <summary>
        /// Se obtiene las propiedades
        /// </summary>
        /// <returns></returns>
        protected object[] ObtenerPropiedades()
        {
            Type ignorable = typeof(SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel);
            PropertyInfo[] propiedades = typeof(ConciliacionPolizasSiapSapModel).GetProperties().Where(e => !e.GetCustomAttributes(ignorable, true).Any()).ToArray();
            return propiedades.Select(t => t.Name).Cast<object>().ToArray();
        }


        /// <summary>
        /// Valida la extension del archivo
        /// </summary>
        /// <returns></returns>
        private bool ValidarNombreArchivo()
        {
            var extensiones = new List<string>
                                  {
                                      ".xls",
                                      ".xlsx"
                                  };
            if (!extensiones.Contains(Path.GetExtension(txtRuta.Text)))
            {
                return false;
            }
            return true;
        }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaInicial_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ValidarFechas();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechafinal_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ValidarFechas();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ValidarFechas()
        {
            if (DtpFechaInicial.SelectedDate.HasValue && DtpFechafinal.SelectedDate.HasValue)
            {
                if (DtpFechaInicial.SelectedDate > DtpFechafinal.SelectedDate)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionPolizasSIAPSAP_fechas, MessageBoxButton.OK,
                                  MessageImage.Warning);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMostrarVista_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (polizasFaltantes != null)
            {
                if (polizasFaltantes.Any())
                {
                    desahabilitarControles(false,false);
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConciliacionPolizasSIAPSAP_DatosGridMej,
                                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                        {
                            listCuentas.ItemsSource = polizasFaltantes.OrderBy(x => x.Cuenta).ToList();
                            // Se crea la agrupación para la vista
                            var view = (CollectionView)CollectionViewSource.GetDefaultView(listCuentas.ItemsSource);
                            var groupDescription = new PropertyGroupDescription("Cuenta");
                            if (view.GroupDescriptions != null) view.GroupDescriptions.Add(groupDescription);
                            btnFiltro.IsEnabled = true;
                            btnCancelar.IsEnabled = true;
                            txtCuenta.IsEnabled = true;
                            desahabilitarControles(true, true);
                        }
                    else
                    {
                        desahabilitarControles(true,true);
                    }
                }
            }
            
        }


        private void ConcialiacionPolizasSIAP_SAP_OnLoaded(object sender, RoutedEventArgs e)
        {

            obtenerTiposCuentaConciliacion();
        }

        /// <summary>
        /// 
        /// </summary>
        private void obtenerTiposCuentaConciliacion()
        {
            PolizaPL polizaPl = new PolizaPL();

            try
            {
                var lsTiposCuenta = polizaPl.ObtenerTiposCuentaConciliacion();
                if (lsTiposCuenta.Any())
                {
                    cmbTipoCuenta.ItemsSource = lsTiposCuenta;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeErrorSinCuentasConfiguradas, MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeErrorAlObtenerTiposCuenta, MessageBoxButton.OK,
                                     MessageImage.Error);
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Resultado de la validacion</returns>
        private bool validarDatosEntrada()
        {
            bool resultado = false;

            if (ValidarNombreArchivo())
            {
                if (DtpFechaInicial.SelectedDate.HasValue && DtpFechafinal.SelectedDate.HasValue)
                {
                    if (cmbTipoCuenta.SelectedValue != null)
                    {
                        resultado = true;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConciliacionPolizasSIAPSAP_TipoCuentaRequerido, MessageBoxButton.OK,
                                MessageImage.Warning);
                        btnConciliar.IsEnabled = true;
                    }

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConciliacionPolizasSIAPSAP_FechasIncorrectas, MessageBoxButton.OK,
                                MessageImage.Warning);
                    btnConciliar.IsEnabled = true;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConciliacionPolizasSIAPSAP_MensajeSinArchivoSeleccionado, MessageBoxButton.OK,
                                MessageImage.Warning);
                btnConciliar.IsEnabled = true;
                
            }

            return resultado;
        }
    }
}
