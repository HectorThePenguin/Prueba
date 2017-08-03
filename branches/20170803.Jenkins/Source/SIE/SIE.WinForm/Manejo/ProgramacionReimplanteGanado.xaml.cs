using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NPOI.SS.Formula.Functions;
using SIE.Services.Info.Constantes;
using SIE.WinForm.Auxiliar;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Info;
using System.Data;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.ComponentModel;

namespace SIE.WinForm.Manejo
{
 
    /// <summary>
    /// Interaction logic for ProgramacionReimplanteGanado.xaml
    /// </summary>
    public partial class ProgramacionReimplanteGanado : UserControl, INotifyPropertyChanged
    {
        int _organizacionId = -1;
        IList<OrdenReimplanteInfo> _lotesDisponibles;
        int _totalCabezas = 0;
        private string _nombreImpresora;
        //private string _rutaReportes;
        private int _idUsuario;
        private bool programacionRealizada;
        public event PropertyChangedEventHandler PropertyChanged;

        public ProgramacionReimplanteGanado()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa los datos de usuario del formulario
        /// </summary>
        private void InicializarDatosUsuario()
        {
            
            _organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            _idUsuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

            /* Obtener la impresora de la configuracion xml */
            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            _nombreImpresora = configuracion.ImpresoraRecepcionGanado;
            

            programacionRealizada = false;
            dtfFecha.SelectedDate = DateTime.Now.AddDays(1);
            //dtfFecha.IsEnabled = false;

            CargarCorralesParaReimplante();
        }

        /// <summary>
        /// Handler del evento loaded para el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programacionReimplanteGanado_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarDatosUsuario();
            contarChekeados();
        }

       
        /// <summary>
        /// Carga los datos de corrales disponibles para reimplate
        /// </summary>
        private void CargarCorralesParaReimplante()
        {
            //Validamos que no exista programacion para el dia seleccionado

            try
            {
                
                var progReimplantePl = new ProgramacionReimplantePL();
                //validar si para la fecha seleccionada existe programacion de reimplante registrada
                var existeProgramacion = progReimplantePl.ExisteProgramacionReimplante(_organizacionId, dtfFecha.SelectedDate.Value);
                if (existeProgramacion && ! programacionRealizada)
                {
                    programacionRealizada = true;
                    MessageBoxResult confirmar = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                      Properties.Resources.OrdenReimplante_ImprimirProgramacionCerrada, 
                      MessageBoxButton.OK,
                      MessageImage.Warning);
                }
                else
                {
                    var lotes = progReimplantePl.ObtenerLotesDisponiblesReimplante(_organizacionId);
                    _lotesDisponibles = lotes.Lista;
                    if (_lotesDisponibles != null)
                    {
                        dgCorrales.ItemsSource = _lotesDisponibles;
                    }
                    else
                    {
                        dgCorrales.ItemsSource = null;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                            Properties.Resources.OrdenReimplante_NoExisteCorralesDisponibles,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionReimplante_ErrorObtenerCorrales, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionReimplante_ErrorObtenerCorrales, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Realiza la impresion del formulario ISO de la programacion de reimplantes
        /// </summary>
        private void ImprimirFormulario()
        {
            var tablaMachos = CrearColumnasImpresion("M");
            tablaMachos.TableName = "tablaMachos";
            var tablaHembras = CrearColumnasImpresion("H");
            tablaHembras.TableName = "tablaHembras";
            var tablaTotales = CrearColumnasTotales();
            tablaTotales.TableName = "tablaTotales";
            int totalMachos, totalReimplante;
            int totalHembras = totalMachos = totalReimplante = 0;
            const string idOrden = "ordenProgramacion";
            int idM;
            int idH = idM = 1;

            try
            {
                foreach (OrdenReimplanteInfo orden in dgCorrales.Items)
                {
                    if (orden.Seleccionado)
                    {
                        //Establecemos para las ordenes de reimplante seleccionadas el usuario de creacion
                        orden.UsuarioCreacion = _idUsuario;

                        if (dtfFecha.SelectedDate != null)
                            orden.FechaReimplanteSeleccionado = dtfFecha.SelectedDate.Value;

                        DataRow row = null;
                        if (orden.TipoGanado.Sexo == Services.Info.Enums.Sexo.Hembra)
                        {
                                //agregamos a la table de hembras
                                row = tablaHembras.NewRow();
                                    row[0] = idOrden;
                                    row[1] = idH;
                                    //Reimplante
                                    row[2] = orden.ProductoSeleccionado.ProductoDescripcion;
                                    //corral
                                    row[3] = orden.CorralOrigen.Codigo;
                                    //corralDestino
                                    var corralesDestinos = "";
                                    foreach (var corralDestino in orden.CorralesDestino)
                                    {
                                        corralesDestinos = String.Format("{0}{1},", corralesDestinos, corralDestino.Codigo);
                                    }
                                    if (!String.IsNullOrEmpty(corralesDestinos))
                                    {
                                        corralesDestinos = corralesDestinos.Substring(0, corralesDestinos.Length - 1);
                                    }
                                    row[4] = corralesDestinos;
                                    //kilogramos
                                    row[5] = orden.KilosProyectados;
                                    //cabezas
                                    row[6] = orden.Cabezas;
                                tablaHembras.Rows.Add(row);
                            totalHembras += orden.Cabezas;
                            idH++;
                        }
                        else
                        {
                            //Agregamos a la tabla de machos
                            row = tablaMachos.NewRow();
                            row[0] = idOrden;
                            row[1] = idM;
                            //Reimplante
                            row[2] = orden.ProductoSeleccionado.ProductoDescripcion;
                            //corral
                            row[3] = orden.CorralOrigen.Codigo;
                            //corralDestino
                            var corralesDestinos = "";
                            foreach (var corralDestino in orden.CorralesDestino)
                            {
                                corralesDestinos = String.Format("{0}{1},", corralesDestinos, corralDestino.Codigo);
                            }
                            if (!String.IsNullOrEmpty(corralesDestinos))
                            {
                                corralesDestinos = corralesDestinos.Substring(0, corralesDestinos.Length - 1);
                            }
                            row[4] = corralesDestinos;
                            //kilogramos
                            row[5] = orden.KilosProyectados;
                            //cabezas
                            row[6] = orden.Cabezas;

                            totalMachos += orden.Cabezas;

                            tablaMachos.Rows.Add(row);
                        }
                    }
                }

                totalReimplante = totalMachos + totalHembras;

                DateTime fecha = DateTime.Now;
                int organizacionIdu = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionIdu);
                //string Titulo = Properties.Resources.ReporteConsumoProgramadovsServido_TituloReporte;
                var nombreOrganizacion = organizacion.Descripcion;

                DataRow rowTotales = tablaTotales.NewRow();
                rowTotales[0] = idOrden;
                rowTotales[1] = totalHembras;
                rowTotales[2] = totalMachos;
                rowTotales[3] = totalReimplante;
                rowTotales[4] = nombreOrganizacion;
                rowTotales[5] = fecha.ToShortDateString();

                tablaTotales.Rows.Add(rowTotales);

                var ds = new DataSet();
                ds.DataSetName = "programacionReimplante";

                ds.Tables.Add(tablaTotales);
                ds.Tables.Add(tablaMachos);
                ds.Tables.Add(tablaHembras);

                PrintReport(ds);
            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.OrdenReimplante_ErrorImpresora, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }

        }

        private void PrintReport(DataSet ds)
        {

            var documento = new ReportDocument();
            var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\programacionReimplanteImpresion.rpt");

            try
            {
                documento.Load(reporte);
                documento.DataSourceConnections.Clear();
                documento.SetDataSource(ds);
                documento.Refresh();

                var forma = new ReportViewer(documento, Properties.Resources.ProgramacionReimplanteGanado_lblTitulo);
                forma.MostrarReporte();
                forma.Show();

            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.OrdenReimplante_ErrorImpresora, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
           
        }

        /// <summary>
        /// Handler del Check de Tratamiento cuando es checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTratamiento_checked(object sender, RoutedEventArgs e)
        {
            var dato = (OrdenReimplanteInfo)dgCorrales.SelectedItem;
            if (dato != null && dato.EsEditable && dato.Seleccionado == false)
            {
                
                dato.Seleccionado = true;
                contarChekeados();
            }

        }

        /// <summary>
        /// Handler del check de tratamiento cuando es unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTratamiento_unchecked(object sender, RoutedEventArgs e)
        {
            var dato = (OrdenReimplanteInfo)dgCorrales.SelectedItem;
            if (dato != null && dato.EsEditable && dato.Seleccionado)
            {
                dato.Seleccionado = false;
                contarChekeados();
                dgCorrales.SelectedItem = null;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void checkSeleccionado_Click_1(object sender, RoutedEventArgs e)
        {
            CheckBox Check = (CheckBox)sender;
            var dato = (OrdenReimplanteInfo)dgCorrales.SelectedItem;
            if (dato != null && dato.EsEditable)
            {
                if (Check.IsChecked.Value)
                {
                    dato.Seleccionado = true;
                }
                else
                {
                    dato.Seleccionado = false;
                }
                contarChekeados();
                dgCorrales.Items.Refresh();
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handler del evento para cargar los renglones del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCorrales_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
                var valor = (OrdenReimplanteInfo)e.Row.Item;
                if (valor != null)
                {
                    if (valor.Seleccionado)
                        // _totalCabezas = valor.Cabezas;
                        //_totalCabezas = 0;
                    //lblTotalCabezas.Content = _totalCabezas;

                    //Regla de seleccion
                    if (valor.Seleccionado && !valor.EsEditable)
                    {
                        e.Row.Background = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            catch
            {
                ;
            } 
        }

        /// <summary>
        /// Imprime el reporte de salida de la programacion de reimplante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            //Preparamos los datos del informe
            //¿Está seguro de imprimir la programación?
            if (programacionRealizada)
            {
                MessageBoxResult confirmar = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.OrdenReimplante_ImprimirProgramacionCerrada, MessageBoxButton.OK,
                   MessageImage.Warning);

                return;
            }

            if (ValidarProgramacion())
            {

                MessageBoxResult confirmar = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenReimplante_ImprimirProgramacion, MessageBoxButton.OKCancel,
                    MessageImage.Question);

                if (confirmar == MessageBoxResult.OK)
                {
                    ImprimirFormulario();

                    MessageBoxResult resultado =
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.OrdenReimplante_ImpresionCorrecta, 
                        MessageBoxButton.YesNo,
                        MessageImage.Question);

                    if (resultado == MessageBoxResult.Yes)
                    {
                        programacionRealizada = true;
                        GuardarProgramacion();
                    }
                }
            }

        }

        /// <summary>
        /// Valida la seleccion de las ordenes de reimplante
        /// </summary>
        /// <returns></returns>
        private bool ValidarProgramacion()
        {
            bool retValue = true;
            bool unoSeleccionado = false;
            foreach (OrdenReimplanteInfo orden in dgCorrales.Items)
            {
                if (orden.Seleccionado)
                {
                    unoSeleccionado = true;
                    if (orden.ProductoSeleccionado == null )
                    {

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                            Properties.Resources.OrdenReimplante_SeleccionarProducto, 
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        return false;
                    }

                    if (orden.CorralesDestino == null)
                    {
                        //SkMessageBox.Show(Properties.Resources.OrdenReimplante_SeleccionarCorralDestino, MessageBoxButton.OK,
                        //    MessageImage.Warning);
                        //retValue = false;
                        orden.CorralesDestino = new List<CorralInfo> {orden.CorralOrigen};
                    }
                }

            }
            //Validamos que haya seleccionado al menos uno
            if (!unoSeleccionado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.OrdenReimplante_SeleccionarUnaOrden, 
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                retValue = false;
            }

            return retValue;
        }

        /// <summary>
        /// Guarda la programacion de reimplante
        /// </summary>
        private void GuardarProgramacion()
        {
            try
            {
                var programacionReimplantePl = new ProgramacionReimplantePL();
                var resultado = programacionReimplantePl.GuardarProgramacionReimplante(_lotesDisponibles);
                if (resultado)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.ProgramacionReimplante_ExitoGuardarProgramacion,
                        MessageBoxButton.OK,
                        MessageImage.Correct);

                    CargarCorralesParaReimplante();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.ProgramacionReimplante_ErrorGuardarProgramacion,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionReimplante_ErrorGuardarProgramacion, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionReimplante_ErrorGuardarProgramacion, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Crear las culumnas para la tabla de totales
        /// </summary>
        /// <returns></returns>
        public static DataTable CrearColumnasTotales()
        {
            var dt = new DataTable();

            var column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "idOrden";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "TotalHembras";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "TotalMachos";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "TotalReimplantar";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Organizacion";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Fecha";
            dt.Columns.Add(column);

            return dt;
        }


        private static DataTable CrearColumnasImpresion(string prefijo)
        {
            var dt = new DataTable();

            var column = new DataColumn();

            column.DataType = Type.GetType("System.String");
            column.ColumnName = "idOrden";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_Reimplante";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_Corral";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_CorralDestino";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_Kilogramos";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = prefijo + "_Cabezas";
            dt.Columns.Add(column);

            return dt;
        }

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }

        private void contarChekeados()
        {
            _totalCabezas = 0;
            foreach (OrdenReimplanteInfo orden in dgCorrales.Items)
            {
                if (orden.Seleccionado)
                {
                    _totalCabezas += orden.Cabezas;
                    
                }
            }
            lblTotalCabezas.Content = _totalCabezas;
        }

        private void dgCorrales_Sorting(object sender, DataGridSortingEventArgs e)
        {
            dgCorrales.SelectedItem  = null;
        }

        /// <summary>
        /// Metodo que crea la ventana Otros Costos
        /// </summary>
        /// <param name="ventana"></param>
        protected void MostrarCentrado(Window ventana)
        {
            ventana.Left = (ActualWidth - ventana.Width) / 2;
            ventana.Top = ((ActualHeight - ventana.Height) / 2) + 132;
            ventana.Owner = Application.Current.Windows[1];
            ventana.ShowDialog();
        }

        /// <summary>
        /// Boton buscar corrales destinos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscarDestino_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var corralInfoSelecionado = (OrdenReimplanteInfo)btn.CommandParameter;

            try
            {
                if (corralInfoSelecionado != null)
                {
                    var corralDestino = new CorralDestino(corralInfoSelecionado.CorralOrigen,
                                                          corralInfoSelecionado.CorralesDestino);
                    MostrarCentrado(corralDestino);
                    corralInfoSelecionado.CorralesDestino = corralDestino.listaCorrales;
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
        }

        private void DtfFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            if (dtfFecha.SelectedDate.Value < DateTime.Today)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ProgramacionReimplante_FechaMenorActual,
                        MessageBoxButton.OK,
                        MessageImage.Stop);

                dtfFecha.SelectedDate = DateTime.Now;
            }
        }

        
    }
}
