using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using System;
using System.Windows;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using SIE.Base.Log;
using System.Text;
using System.Collections.ObjectModel;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ConciliacionSAP.xaml
    /// </summary>
    public partial class ConciliacionSAP
    {
        #region PROPIEDADES

        private ConciliacionSAPModel Contexto
        {
            get { return (ConciliacionSAPModel) DataContext; }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public ConciliacionSAP()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtRuta.Focus();
        }

        private void btnBuscarClick(object sender, RoutedEventArgs e)
        {
            var file = new System.Windows.Forms.OpenFileDialog
                           {
                               Filter = "Hojas de Calculo|*.xls|All Files (*.*)|*.*",
                               Title = "Buscar Archivo Conciliación SAP"
                           };
            var result = file.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(file.FileName))
                {
                    if (File.Exists(file.FileName))
                    {
                        Contexto.Ruta = file.FileName;
                    }
                }
            }
        }

        private void btnLimpiarClick(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            txtRuta.Focus();
        }

        private void btnConciliarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool archivoValido = ValidarNombreArchivo();
                if (archivoValido)
                {
                    List<PolizaSAPInfo> polizasSAP = LeerArchivoConciliacion();
                    if (polizasSAP != null && polizasSAP.Any())
                    {
                        List<DateTime> fechas =
                            polizasSAP.Select(
                                x =>
                                new DateTime(Convert.ToInt32(x.FechaDocumento.Split('.')[2]),
                                             Convert.ToInt32(x.FechaDocumento.Split('.')[1]),
                                             Convert.ToInt32(x.FechaDocumento.Split('.')[0]))).ToList();
                        DateTime fechaInicial = fechas.Min(x => x);
                        DateTime fechaFinal = fechas.Max(x => x);
                        List<PolizaInfo> polizasSIAP = ObtenerPolizas(fechaInicial, fechaFinal);
                        Contexto.PolizasCompletas.AddRange(polizasSIAP);
                        if (polizasSIAP == null || !polizasSIAP.Any())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.ConciliacionSAP_NoExistenMovimientos,
                                              MessageBoxButton.OK,
                                              MessageImage.Warning);
                            LimpiarArbol();
                        }
                        else
                        {
                            polizasSIAP = polizasSIAP.Where(soc => !"300".Equals(soc.Sociedad)).ToList();
                            GenerarConciliacion(polizasSAP, polizasSIAP);
                            if (Contexto.Polizas != null && Contexto.Polizas.Any())
                            {
                                GenerarArbolConFaltantes(Contexto.Polizas);
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.ConciliacionSAP_NoExistenPendietesPorEnviar,
                                                  MessageBoxButton.OK,
                                                  MessageImage.Warning);
                                LimpiarArbol();
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ConciliacionSAP_ArchivoVacio, MessageBoxButton.OK,
                                          MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ConciliacionSAP_ArchivoInvalido, MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtnGuardar(object sender, RoutedEventArgs e)
        {
            List<PolizaInfo> polizasPorReenviar = Contexto.Polizas.Where(sel => sel.Generar).ToList();
            if (polizasPorReenviar != null && polizasPorReenviar.Any())
            {
                Guardar(polizasPorReenviar);
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_SeleccionarPoliza, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConciliacionSAPModel
                           {
                               Ruta = string.Empty,
                               Polizas = new ObservableCollection<PolizaInfo>(),
                               PolizasCompletas = new List<PolizaInfo>(),
                               Ganado = true
                           };
            LimpiarArbol();
        }

        /// <summary>
        /// Reestablece los valores del arbol
        /// </summary>
        private void LimpiarArbol()
        {
            treePolizas.Items.Clear();
            GenerarArbolConFaltantes(new ObservableCollection<PolizaInfo>());
        }

        /// <summary>
        /// Valida la extension del archivo
        /// </summary>
        /// <returns></returns>
        private bool ValidarNombreArchivo()
        {
            var nombreCorrecto = true;
            var extensiones = new List<string>
                                  {
                                      ".xls",
                                      ".xlsx"
                                  };
            string extension = Path.GetExtension(Contexto.Ruta);
            if (!extensiones.Contains(extension))
            {
                nombreCorrecto = false;
            }
            return nombreCorrecto;
        }

        /// <summary>
        /// Obtiene las polizas de la base de datos
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerPolizas(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var polizaPL = new PolizaPL();
                string claseDocumento = Contexto.Ganado ? "GF" : "AG";
                IEnumerable<PolizaInfo> polizas =
                    polizaPL.ObtenerPolizasConciliacion(AuxConfiguracion.ObtenerOrganizacionUsuario(), fechaInicial,
                                                        fechaFinal, claseDocumento);
                return polizas.ToList();
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_ErrorObtenerPolizas, MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
        }

        /// <summary>
        /// Lee el archivo con los datos a conciliar
        /// </summary>
        private List<PolizaSAPInfo> LeerArchivoConciliacion()
        {
            var polizasSAP = new List<PolizaSAPInfo>();
            try
            {
                string con = string.Format("{0}{1}{2}", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", Contexto.Ruta,
                                           ";Extended Properties='Excel 8.0;HDR=Yes;'");
                using (var connection = new OleDbConnection(con))
                {
                    connection.Open();
                    var command = new OleDbCommand("SELECT * FROM [conciliacion$]", connection);
                    using (var dr = command.ExecuteReader())
                    {
                        PolizaSAPInfo polizaSAP;
                        while (dr.Read())
                        {
                            polizaSAP = new PolizaSAPInfo
                            {
                                Concepto = Convert.ToString(dr["Concepto"]),
                                Importe = Convert.ToString(dr["Importe"]),
                                Sociedad = Convert.ToString(dr["Sociedad"]),
                                Division = Convert.ToString(dr["Division"]),
                                Cuenta = Convert.ToString(dr["Cuenta"]),
                                Asignacion = Convert.ToString(dr["Asignacion"]),
                                NumeroDocumento = Convert.ToString(dr["NumeroDocumento"]),
                                ClaseDocumento = Convert.ToString(dr["ClaseDocumento"]),
                                FechaContable = Convert.ToString(dr["FechaContable"]),
                                FechaDocumento = Convert.ToString(dr["FechaDocumento"]),
                                Moneda = Convert.ToString(dr["Moneda"]),
                                Ref3 = Convert.ToString(dr["Ref3"]),
                                Periodo = Convert.ToString(dr["Periodo"]),
                            };
                            polizasSAP.Add(polizaSAP);
                        }
                    }
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_ErrorLeerArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
            string claseDocumento = Contexto.Ganado ? "GF" : "AG";
            return polizasSAP.Where(clase => clase.ClaseDocumento.Equals(claseDocumento)).ToList();
        }

        /// <summary>
        /// Genera la conciliacion de SAP contra SIAP
        /// </summary>
        /// <param name="polizasSAP"></param>
        /// <param name="polizasSIAP"></param>
        private void GenerarConciliacion(List<PolizaSAPInfo> polizasSAP, List<PolizaInfo> polizasSIAP)
        {
            OrganizacionInfo organizacion = ObtenerOrganizacion();
            PolizaInfo polizaSIAP;
            StringBuilder sb;
            List<PolizaSAPInfo> polizasFaltantes;
            var conjuntoPolizas = new HashSet<string>();
            HashSet<string> prefijosCuenta;
            switch (Contexto.TipoCuenta)
            {
                case 1:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1151",
                                             "2104",
                                             "4001",
                                             "5001",
                                         };
                    break;
                case 2:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1154",
                                         };
                    break;
                default:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1153",
                                             "1156",
                                         };
                    break;
            }

            polizasSIAP =
                polizasSIAP.Where(x => x.Cuenta.Length > 0 && prefijosCuenta.Contains(x.Cuenta.Substring(0, 4))).ToList();
            for (var indexPolizaSIAP = 0; indexPolizaSIAP < polizasSIAP.Count; indexPolizaSIAP++)
            {
                polizaSIAP = polizasSIAP[indexPolizaSIAP];
                sb = new StringBuilder();
                sb.AppendFormat("{0}.{1}.{2}", polizaSIAP.FechaDocumento.Substring(6, 2),
                                polizaSIAP.FechaDocumento.Substring(4, 2), polizaSIAP.FechaDocumento.Substring(0, 4));
                polizasFaltantes = polizasSAP.Where(
                    sap => sap.Cuenta.Trim().Equals(polizaSIAP.Cuenta.Trim())
                           && !sap.Cuenta.Trim().Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta)
                           && sap.ClaseDocumento.Trim().Equals(polizaSIAP.ClaseDocumento.Trim())
                           && polizaSIAP.Concepto.Trim().Contains(sap.Concepto.Trim())
                           && polizaSIAP.Referencia3.Trim().Contains(sap.Ref3.Trim())
                           && sap.Sociedad.Trim().Equals(organizacion.Sociedad.Trim())
                           && sap.Division.Trim().Equals(organizacion.Division.Trim())
                           && sap.FechaDocumento.Trim().Equals(sb.ToString())).ToList();
                if (polizasFaltantes == null || !polizasFaltantes.Any())
                {
                    if (conjuntoPolizas.Contains(polizaSIAP.Referencia3))
                    {
                        continue;
                    }
                    Contexto.Polizas.Add(polizaSIAP);
                }
                conjuntoPolizas.Add(polizaSIAP.Referencia3);
            }
        }

        /// <summary>
        /// Obtiene la organizacion
        /// </summary>
        /// <returns></returns>
        private OrganizacionInfo ObtenerOrganizacion()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                OrganizacionInfo organizacion = organizacionPL.ObtenerPorIdConIva(AuxConfiguracion.ObtenerOrganizacionUsuario());
                return organizacion;
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_ErrorObtenerOrganizacion, MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
        }

        /// <summary>
        /// Genera un treeview con los movimientos faltantes por enviar a SAP
        /// </summary>
        /// <param name="polizasFaltantesPorEnviar"></param>
        private void GenerarArbolConFaltantes(ObservableCollection<PolizaInfo> polizasFaltantesPorEnviar)
        {
            System.Windows.Controls.StackPanel stack = GenerarEncabezado();
            var treeHeader = new System.Windows.Controls.TreeViewItem
                                 {
                                     Header = stack,
                                 };
            if (!polizasFaltantesPorEnviar.Any())
            {
                treePolizas.Items.Add(treeHeader);
            }

            List<string> cuentas =
                polizasFaltantesPorEnviar.Where(x => !string.IsNullOrWhiteSpace(x.Cuenta)).OrderBy(x => x.Cuenta).Select
                    (x => x.Cuenta).Distinct().ToList();
            if (cuentas.Any())
            {
                List<PolizaInfo> polizasAgrupadas;
                IList<TipoPolizaInfo> tiposPoliza = ObtenerTiposPoliza();
                for (var indexCuentas = 0; indexCuentas < cuentas.Count; indexCuentas++)
                {
                    stack = new System.Windows.Controls.StackPanel
                                {
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    Width = 1050,
                                };
                    var lbl = new System.Windows.Controls.Label
                                  {
                                      Content = cuentas[indexCuentas],
                                  };
                    stack.Children.Add(lbl);
                    treeHeader = new System.Windows.Controls.TreeViewItem
                                     {
                                         Header = stack,
                                     };
                    treePolizas.Items.Add(treeHeader);
                    polizasAgrupadas =
                        polizasFaltantesPorEnviar.Where(x => x.Cuenta.Equals(cuentas[indexCuentas])).ToList();
                    if (polizasAgrupadas.Any())
                    {
                        for (var indexAgrupado = 0; indexAgrupado < polizasAgrupadas.Count; indexAgrupado++)
                        {
                            stack = ObtenerPanelItem(indexCuentas, polizasAgrupadas[indexAgrupado], tiposPoliza);
                            var treeItem = new System.Windows.Controls.TreeViewItem { Header = stack };
                            treeHeader.Items.Add(treeItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un StackPanel que para mostrar las
        /// cuentas
        /// </summary>
        /// <param name="indexFormularios"></param>
        /// <param name="poliza"></param>
        /// <param name="tiposPoliza"> </param>
        /// <returns></returns>
        private System.Windows.Controls.StackPanel ObtenerPanelItem(int indexFormularios, PolizaInfo poliza, IList<TipoPolizaInfo> tiposPoliza)
        {
            Brush colorItem;
            if (indexFormularios % 2 == 0)
            {
                colorItem = new SolidColorBrush(Colors.LavenderBlush);
            }
            else
            {
                colorItem = new SolidColorBrush(Colors.White);
            }

            var grdDetalles = new System.Windows.Controls.Grid();
            grdDetalles.SetValue(System.Windows.Controls.Grid.BackgroundProperty, colorItem);
            var columnaDescripcion = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(300) };
            var columnaLectura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(435) };
            var columnaEscritura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(50) };

            grdDetalles.ColumnDefinitions.Add(columnaDescripcion);
            grdDetalles.ColumnDefinitions.Add(columnaLectura);
            grdDetalles.ColumnDefinitions.Add(columnaEscritura);

            var row = new System.Windows.Controls.RowDefinition { Height = new GridLength(20) };
            grdDetalles.RowDefinitions.Add(row);

            TipoPolizaInfo tipoPoliza = tiposPoliza.FirstOrDefault(x => x.TipoPolizaID == poliza.TipoPolizaID);
            if (tipoPoliza == null)
            {
                tipoPoliza = new TipoPolizaInfo();
            }

            var lbl = new System.Windows.Controls.Label
                          {
                              Content =
                                  string.Format("{0}\t\t{1}\t{2}\t{3}", poliza.FechaDocumento, tipoPoliza.Descripcion,
                                                poliza.Concepto,
                                                Math.Abs(Convert.ToDecimal(poliza.Importe)).ToString("C2"))
                          };
            System.Windows.Controls.Grid.SetColumn(lbl, 0);
            System.Windows.Controls.Grid.SetColumnSpan(lbl, 2);
            System.Windows.Controls.Grid.SetRow(lbl, 0);

            var chkGenera = new System.Windows.Controls.CheckBox
                                {
                                    HorizontalAlignment = HorizontalAlignment.Right,
                                };
            var bindGenera = new Binding("Generar")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = poliza,
            };
            chkGenera.SetBinding(System.Windows.Controls.CheckBox.IsCheckedProperty, bindGenera);

            System.Windows.Controls.Grid.SetColumn(chkGenera, 2);
            System.Windows.Controls.Grid.SetRow(chkGenera, 0);

            grdDetalles.Children.Add(lbl);
            grdDetalles.Children.Add(chkGenera);

            var stack = new System.Windows.Controls.StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 1050,
            };
            stack.Children.Add(grdDetalles);

            return stack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private System.Windows.Controls.StackPanel GenerarEncabezado()
        {
            var stack = new System.Windows.Controls.StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 1070
            };
            var grdEncabezado = new System.Windows.Controls.Grid();
            var columnaDescripcion = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(470) };
            var columnaLectura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(300) };
            var columnaEscritura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(300) };

            grdEncabezado.ColumnDefinitions.Add(columnaDescripcion);
            grdEncabezado.ColumnDefinitions.Add(columnaLectura);
            grdEncabezado.ColumnDefinitions.Add(columnaEscritura);

            grdEncabezado.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());

            var lblPantalla = new System.Windows.Controls.Label
                                  {
                                      Content = Properties.Resources.ConciliacionSAP_lblCuenta,
                                      Background =
                                          (LinearGradientBrush)
                                          Application.Current.FindResource("headerDataGridColor"),
                                      Foreground =
                                          new LinearGradientBrush(Colors.White, Colors.White, 0),
                                      Style = (Style) Application.Current.FindResource("etiquetaBold")
                                  };
            grdEncabezado.Children.Add(lblPantalla);
            System.Windows.Controls.Grid.SetColumn(lblPantalla, 0);
            System.Windows.Controls.Grid.SetRow(lblPantalla, 0);

            var lblLectura = new System.Windows.Controls.Label
                                 {
                                     Background =
                                         (LinearGradientBrush)
                                         Application.Current.FindResource("headerDataGridColor"),
                                     Foreground =
                                         new LinearGradientBrush(Colors.White, Colors.White, 0),
                                     Style = (Style) Application.Current.FindResource("etiquetaBold")
                                 };
            grdEncabezado.Children.Add(lblLectura);
            System.Windows.Controls.Grid.SetColumn(lblLectura, 1);
            System.Windows.Controls.Grid.SetRow(lblLectura, 0);

            var lblEscritura = new System.Windows.Controls.Label
                                   {
                                       Background =
                                           (LinearGradientBrush)
                                           Application.Current.FindResource("headerDataGridColor"),
                                       Foreground =
                                           new LinearGradientBrush(Colors.White, Colors.White, 0),
                                       Style = (Style) Application.Current.FindResource("etiquetaBold"),
                                       Content = Properties.Resources.ConciliacionSAP_TituloGenerar
                                   };
            grdEncabezado.Children.Add(lblEscritura);
            System.Windows.Controls.Grid.SetColumn(lblEscritura, 2);
            System.Windows.Controls.Grid.SetRow(lblEscritura, 0);

            stack.Children.Add(grdEncabezado);

            return stack;
        }

        /// <summary>
        /// Obtiene los tipos de poliza
        /// </summary>
        /// <returns></returns>
        private IList<TipoPolizaInfo> ObtenerTiposPoliza()
        {
            var tipoPolizaPL = new TipoPolizaPL();
            IList<TipoPolizaInfo> tiposPoliza = tipoPolizaPL.ObtenerTodos();
            return tiposPoliza;
        }

        /// <summary>
        /// Metodo para Guardar
        /// </summary>
        private void Guardar(List<PolizaInfo> polizasReenviar)
        {
            try
            {
                var polizaPL = new PolizaPL();
                polizasReenviar.ForEach(datos =>
                                            {
                                                datos.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                                                datos.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                                            });
                polizasReenviar =
                    Contexto.PolizasCompletas.Join(polizasReenviar, comp => comp.Referencia3, ree => ree.Referencia3,
                                                   (comp, ree) => comp).ToList();
                polizaPL.GuardarConciliacion(polizasReenviar, polizasReenviar);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                  MessageImage.Correct);
                InicializaContexto();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorGuardar_ConciliacionSAP, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        #endregion METODOS
    }
}
