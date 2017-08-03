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
    /// Lógica de interacción para GastosMateriasPrimasAretes.xaml
    /// </summary>
    public partial class GastosMateriasPrimasAretes : Window
    {
        #region VARIABLES
        
        public bool _confirmar = true;
        public List<AreteInfo> ListAretes = new List<AreteInfo>();
        private int organizacionIdActual = 0;
        private int totalAretes = 0;
        private bool esAreteSukarne = false;
        private DataTable procesar = new DataTable();

        #endregion VARIABLES

        #region CONSTRUCTOR

        public GastosMateriasPrimasAretes(int organizacionId, int cantidad, bool esSukarne)
        {
            InitializeComponent();
            ListAretes = new List<AreteInfo>();
            organizacionIdActual = organizacionId;
            totalAretes = cantidad;
            esAreteSukarne = esSukarne;
        }

        #endregion CONSTRUCTOR

        #region METODOS

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

        private void AgregarAreteMasivo(IEnumerable<string> listArete)
        {
            try
            {
                var consecutivo = 1;
                lblMensaje.Visibility = Visibility.Visible;  
                foreach (var cadena in listArete)
                {
                    if (!ListAretes.Where(la => la.Arete.Equals(cadena)).ToList().Any())
                    {
                        var arete = new AreteInfo()
                        {
                            Arete = cadena,
                            Consecutivo = consecutivo
                        };
                        ListAretes.Add(arete);
                        gridDatos.Items.Add(arete);
                        consecutivo++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                lblMensaje.Visibility = Visibility.Hidden;            
            }
        }

        private bool ValidarCantidaAretes()
        {
            var result = false;
            if(ListAretes.Count > 0)
            {
                /*if (ListAretes.Count != totalAretes)
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                       string.Format(Properties.Resources.GastosMateriasPrimasAretes_ValidarCantidad, totalAretes, ListAretes.Count),
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                }
                else
                {
                    result = true;
                }*/
                result = true;
            }
            else
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.GastosMateriasPrimasAretes_ValidarCapturaAretes,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                txtRuta.Focus();
            }
            
            return result;
        }

        private bool Guardar()
        {
            var result = false;

            try
            {
                result = true;
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.GastosMateriasPrimasAretes_AgregarExito, MessageBoxButton.OK, MessageImage.Correct);
                _confirmar = false;
                Close();
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.GastosMateriasPrimasAretes_GuardarError, MessageBoxButton.OK,
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
                if (datos.Rows.Count >= 1)
                {
                    datos.Rows.RemoveAt(0);
                    procesar = datos;
                    if (procesar.Rows.Count >= 1)
                    {
                        var contador = datos.Rows.Count;
                        for (int i = 0; i < contador; i++)
                        {
                            var row = datos.Rows[i];
                            if (row[0].ToString().Trim() == string.Empty)
                            {
                                procesar.Rows.RemoveAt(i);
                                i--;
                                contador--;
                            }
                        }

                        if (procesar.Rows.Count < 1)
                        {
                            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.GastosMateriasPrimasAretes_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else {
                            result = true;
                        }                        
                    }
                    else {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.GastosMateriasPrimasAretes_ArchivoSoloCabecero), MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.GastosMateriasPrimasAretes_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.GastosMateriasPrimasAretes_ValidarRegistrosExcel), MessageBoxButton.OK, MessageImage.Warning);
            }
            return result;
        }

        private List<String> ConvertirDataTableLista(DataTable datos)
        {
            try
            {
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
                if (esAreteSukarne)
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
                           Properties.Resources.GastosMateriasPrimasAretes_ErrorValEstructArete, MessageBoxButton.OK,
                                MessageImage.Error);
            }

            return result;
        }

        #endregion METODOS

        #region EVENTOS
    
        private void BtnImportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnImportar.IsEnabled = false;
                txtRuta.Text = string.Empty;

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
                            txtRuta.Text = dialogo.FileName.Trim();
                            ListAretes = new List<AreteInfo>();
                            gridDatos.Items.Clear();
                            var datos = CargarInformacionADataTable(dialogo.FileName.Trim());
                            if (ValidarRegistros(datos))
                            {
                                var aretes = ConvertirDataTableLista(datos);
                                if (ValidaEstructuraAreteMasivo(aretes))
                                {
                                    AgregarAreteMasivo(aretes);
                                }
                                else
                                {
                                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.GastosMateriasPrimasAretes_AretesNoValidos, MessageBoxButton.OK,
                                            MessageImage.Warning);
                                    txtRuta.Text = string.Empty;
                                }
                            }
                            else
                            {
                                txtRuta.Text = string.Empty;
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

        #endregion EVENTOS

    }
}
