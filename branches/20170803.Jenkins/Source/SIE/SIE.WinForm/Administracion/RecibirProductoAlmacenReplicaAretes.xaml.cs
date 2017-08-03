using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SIE.Services.Info.Enums;
using SuKarne.Controls.MessageBox;
using System.Linq;
using System.ComponentModel;
using System.Windows.Threading;

using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacenReplicaAretes.xaml
    /// </summary>
    public partial class RecibirProductoAlmacenReplicaAretes : Window
    {
        #region VARIABLES
        
        public bool _confirmar = true;
        public List<RecibirProductoAlmacenReplicaAretesInfo> ListAretes;
        private int organizacionIdActual = 0;
        private int consecutivo = 1;
        private decimal _totalAretes;
        private int _esSukarne;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public RecibirProductoAlmacenReplicaAretes(int organizacionId, decimal totalAretes, int esSukarne)
        {
            InitializeComponent();
            ListAretes = new List<RecibirProductoAlmacenReplicaAretesInfo>();
            organizacionIdActual = organizacionId;
            _totalAretes = totalAretes;
            _esSukarne = esSukarne;
        }

        #endregion CONSTRUCTOR

        #region METODOS

        private void Limpiar()
        {
            ListAretes = new List<RecibirProductoAlmacenReplicaAretesInfo>();
            gridDatos.Items.Clear();
            txtArete.Text = string.Empty;
            consecutivo = 1;
            txtArete.Focus();
        }

        private void Salir()
        {
            MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
            Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                        MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ListAretes.Clear();
                _confirmar = false;
                Close();
            }
        }

        private void AgregarArete(RecibirProductoAlmacenReplicaAretesInfo areteInfo)
        {
            try
            {
                if (areteInfo.NumeroAreteSukarne != string.Empty)
                {
                    var existeArete =
                    ListAretes.Where(a => a.NumeroAreteSukarne.Equals(areteInfo.NumeroAreteSukarne) && a.OrganizacionId == areteInfo.OrganizacionId);
                    if (existeArete.Any())
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(
                                                Properties.Resources.RecibirProductoAlmacenReplicaAretes_AreteDuplicado, areteInfo.NumeroAreteSukarne), MessageBoxButton.OK,
                                                                  MessageImage.Warning);
                    }
                    else
                    {
                        ListAretes.Add(areteInfo);
                        gridDatos.Items.Add(areteInfo);
                        consecutivo++;
                    }
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecibirProductoAlmacenReplicaAretes_CapturarArete, MessageBoxButton.OK,
                                                              MessageImage.Warning);
                }
                txtArete.Text = string.Empty;
                txtArete.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AgregarAreteMasivo(IEnumerable<string> listArete)
        {
            try
            {
                foreach (var cadena in listArete)
                {
                    if(!ListAretes.Where(la => la.NumeroAreteSukarne.Equals(cadena)).ToList().Any())
                    {
                        var arete = new RecibirProductoAlmacenReplicaAretesInfo()
                        {
                            NumeroAreteSukarne = cadena,
                            ConsecutivoId = consecutivo,
                            OrganizacionId = organizacionIdActual,
                            UsuarioCreacionId = 1
                        };
                        ListAretes.Add(arete);
                        gridDatos.Items.Add(arete);
                        consecutivo++;
                    }
                }
                lblMensaje.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                lblMensaje.Visibility = Visibility.Hidden;
                throw;
            }
        }

        private bool ValidarCantidaAretes()
        {
            var result = false;
            if(ListAretes.Count > 0)
            {
                if (ListAretes.Count > _totalAretes)
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        string.Format(Properties.Resources.RecibirProductoAlmacenReplicaAretes_ValidarCantidad, _totalAretes, ListAretes.Count),
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.RecibirProductoAlmacenReplicaAretes_ValidarCapturaAretes,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                txtArete.Focus();
            }
            
            return result;
        }

        private bool Guardar()
        {
            var result = false;

            try
            {
                var pl = new RecibirProductoAlmacenReplicaPL();
                var aretes = ListAretes.Select(a => a.NumeroAreteSukarne).ToList();
                var nuevoListado = pl.ConsultarAretes(aretes, organizacionIdActual);
                if (nuevoListado.Any())
                {
                    var msj = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_AretesExistentesBD, MessageBoxButton.YesNo,
                                MessageImage.Warning);

                    if (msj == MessageBoxResult.Yes)
                    {
                        Limpiar();
                        AgregarAreteMasivo(nuevoListado);
                    }
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_AgregarExito, MessageBoxButton.OK,
                                MessageImage.Correct);
                    _confirmar = false;
                    Close();
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_GuardarError, MessageBoxButton.OK,
                                MessageImage.Error);
            }            

            return result;
        }

        private DataTable CargarInformacionADataTable(string sRuta)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection())
                {

                    var dt = new DataTable();
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sRuta + ";" + "Extended Properties='Excel 8.0;HDR=No;IMEX=1;MaxScanRows=0;ImportMixedTypes=Text;'";
                    using (var comm = new OleDbCommand())
                    {
                        comm.CommandText = "Select * From [Hoja1$]";
                        comm.Connection = conn;
                        using (var da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = comm;
                            da.Fill(dt);
                        }
                    }
                    return dt;
                }
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al leer archivo de Excel.");
            }
        }

        private bool ValidarRegistros(DataTable datos)
        {
            var result = false;
            try
            {
                if (datos.Rows.Count > 1)
                {
                    if (datos.Rows[datos.Rows.Count - 1][0].ToString() == string.Empty)
                    {
                        var row = datos.Rows[datos.Rows.Count - 1];
                        datos.Rows.Remove(row);
                    }
                    result = true;
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.RecibirProductoAlmacenReplicaAretes_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al validar registros del archivo de Excel.");
            }
            return result;
        }

        private List<String> ConvertirDataTableLista(DataTable datos)
        {
            try
            {
                datos.Rows.RemoveAt(0);
                return (from DataRow row in datos.Rows select row[0].ToString()).ToList();
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al convertir datos de Excel.");
            }
        }

        private bool ValidarSiEsNumero(string arete)
        {
            var result = true;
            
            long numero = 0;
            var correcto = long.TryParse(arete, out numero);
            if (!correcto)
                result = false;

            return result;
        }

        private bool ValidaEstructuraAreteMasivo(IEnumerable<string> aretes)
        {
            var result = false;
            var correctoLongitud = true;
            var correctoDigitos = true;

            try
            {
                if (_esSukarne == 1)
                {
                    var logic = new ParametroGeneralPL();
                    var paramLongitud = logic.ObtenerPorClaveParametro(ParametrosEnum.LongitudAreteSK.ToString());
                    if (paramLongitud != null)
                    {
                        foreach (string arete in aretes)
                        {
                            if (int.Parse(paramLongitud.Valor) != 0 && arete.Length != int.Parse(paramLongitud.Valor))
                            {
                                correctoLongitud = false;
                            }
                        }
                    }

                    if (correctoLongitud)
                    {
                        var paramDigito = logic.ObtenerPorClaveParametro(ParametrosEnum.DigitosIniAreteSK.ToString());
                        if (paramDigito != null)
                        {
                            foreach (string arete in aretes)
                            {
                                if (paramDigito.Valor.Length > arete.Length || !paramDigito.Valor.Trim().Equals(arete.Substring(0, 4)) && !string.IsNullOrEmpty(paramDigito.Valor))
                                {
                                    correctoDigitos = false;
                                }
                                else
                                {
                                    if (!ValidarSiEsNumero(arete))
                                    { 
                                        correctoDigitos = false;
                                    }
                                }
                            }
                        }
                    }

                    if (correctoLongitud && correctoDigitos)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                    foreach (string arete in aretes)
                    {
                        if (!ValidarSiEsNumero(arete))
                        {
                            result = false;
                        }
                    } 
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_ErrorValEstructArete, MessageBoxButton.OK,
                                MessageImage.Error);
            }

            return result;
        }

        private bool ValidaEstructuraArete(string arete)
        {
            var result = false;
            var correctoLongitud = true;
            var correctoDigitos = true;

            try
            {
                if (_esSukarne == 1)
                {
                    var logic = new ParametroGeneralPL();
                    var paramLongitud = logic.ObtenerPorClaveParametro(ParametrosEnum.LongitudAreteSK.ToString());
                    if (paramLongitud != null)
                    {
                        if (int.Parse(paramLongitud.Valor) != 0 && arete.Length != int.Parse(paramLongitud.Valor))
                        {
                            correctoLongitud = false;
                        }
                    }

                    if (correctoLongitud)
                    {
                        var paramDigito = logic.ObtenerPorClaveParametro(ParametrosEnum.DigitosIniAreteSK.ToString());
                        if (paramDigito != null)
                        {
                            if (paramDigito.Valor.Length > arete.Length || !paramDigito.Valor.Trim().Equals(arete.Substring(0, 4)) && !string.IsNullOrEmpty(paramDigito.Valor))
                            {
                                correctoDigitos = false;
                            }
                        }
                    }

                    if (correctoLongitud && correctoDigitos)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_ErrorValEstructArete, MessageBoxButton.OK,
                                MessageImage.Error);
            }

            return result;
        }

        #endregion METODOS

        #region EVENTOS

        private void BtnRango_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pagoEdicion = new RecibirProductoAlmacenReplicaRango(_esSukarne);
                pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                pagoEdicion.Owner = System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal];
                pagoEdicion.ShowDialog();
                if (pagoEdicion.Aretes.Any())
                {
                    if (pagoEdicion._guardar)
                    {
                        lblMensaje.Content = string.Format(Properties.Resources.RecibirProductoAlmacenReplicaAretes_Espera, pagoEdicion.Aretes.Count());
                        lblMensaje.Visibility = Visibility.Visible;
                        Dispatcher.BeginInvoke((Action) (() => AgregarAreteMasivo(pagoEdicion.Aretes)),
                                               DispatcherPriority.Background);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void BtnImportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnImportar.IsEnabled = false;
                var dialogo = new OpenFileDialog
                {
                    DefaultExt = ".xls",
                    Filter = Properties.Resources.RecibirProductoAlmacenReplicaAretes_FiltroExcel
                };
                var resultado = dialogo.ShowDialog();

                if (resultado == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dialogo.FileName))
                    {
                        if (File.Exists(dialogo.FileName))
                        {
                            var datos = CargarInformacionADataTable(dialogo.FileName.Trim());
                            if (ValidarRegistros(datos))
                            {
                                var aretes = ConvertirDataTableLista(datos);
                                if (ValidaEstructuraAreteMasivo(aretes))
                                {
                                    AgregarAreteMasivo(aretes);                             
                                }     
                                else{
                                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.RecibirProductoAlmacenReplicaAretes_AretesNoValidos, MessageBoxButton.OK,
                                            MessageImage.Warning);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(ex.Message, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
            finally
            {
                btnImportar.IsEnabled = true;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var arete = new RecibirProductoAlmacenReplicaAretesInfo()
                                {
                                    NumeroAreteSukarne = txtArete.Text.Trim(),
                                    ConsecutivoId = consecutivo,
                                    OrganizacionId = organizacionIdActual,
                                    UsuarioCreacionId = 1
                                };
                if (ValidaEstructuraArete(arete.NumeroAreteSukarne.Trim()))
                {
                    AgregarArete(arete);
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RecibirProductoAlmacenReplicaAretes_ValEstructArete, MessageBoxButton.OK,
                                MessageImage.Warning);
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCantidaAretes())
            {
                Guardar();
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Salir();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_confirmar)
            {
                MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else {
                    ListAretes.Clear();
                }
            }
        }

        private void txtArete_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64 valor = 0;
                bool result = Int64.TryParse(txtArete.Text, out valor);
                if (!result)
                {
                    txtArete.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                txtArete.Text = string.Empty;
                txtArete.Focus();
            }
        }

        #endregion EVENTOS

    }
}
