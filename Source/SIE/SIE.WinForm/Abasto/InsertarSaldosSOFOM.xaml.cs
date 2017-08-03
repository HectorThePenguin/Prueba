using System;
using System.IO;
using System.Linq;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Windows.Forms;
using SIE.Services.Servicios.PL;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Abasto
{
    /// <summary>
    /// Lógica de interacción para InsertarSaldosSOFOM.xaml
    /// </summary>
    public partial class InsertarSaldosSOFOM
    {
        #region VARIABLES
        private readonly Dictionary<int, string> _encabezadosColumna = new Dictionary<int, string>();
        private const int RenglonEncabezados = 0;
        DataTable procesar = new DataTable();
        #endregion

        #region CONSTRUCTOR
        public InsertarSaldosSOFOM()
        {
            InitializeComponent();
            CargarEncabezadosColumna();
        }
        #endregion

        #region EVENTOS
        private void ucTitulo_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CargarArchivo(object sender, RoutedEventArgs e)
        {
            try
            {
                Nuevo();
                btnImportarExcel.IsEnabled = false;
                var dialogo = new OpenFileDialog
                {
                    DefaultExt = ".xls",
                    Filter = Properties.Resources.ImportarSaldosSOFOM_FiltroExcel
                };
                var resultado = dialogo.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dialogo.FileName))
                    {
                        if (File.Exists(dialogo.FileName))
                        {
                            var datos = CargarInformacionADataTable(dialogo.FileName.Trim());
                            if (ValidarEncabezado(datos))
                            {
                                if (ValidarRegistros(datos))
                                {
                                    var saldos = ConvertirDataTableLista(procesar);
                                    if(saldos.Any())
                                    {
                                        var listRegs = Guardar(saldos);
                                        MostrarDatos(saldos, listRegs);
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.ImportarSaldosSOFOM_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Nuevo();
                SkMessageBox.Show(ex.Message, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
            finally
            {
                btnImportarExcel.IsEnabled = true;
            }
        }
        #endregion

        #region METODOS
        private void CargarEncabezadosColumna()
        {
            _encabezadosColumna.Add(0, Properties.Resources.ImportarSaldosSOFOM_Layout_Credito);
            _encabezadosColumna.Add(1, Properties.Resources.ImportarSaldosSOFOM_Layout_Nombre);
            _encabezadosColumna.Add(2, Properties.Resources.ImportarSaldosSOFOM_Layout_TipoCredito);
            _encabezadosColumna.Add(3, Properties.Resources.ImportarSaldosSOFOM_Layout_FechaAlta);
            _encabezadosColumna.Add(4, Properties.Resources.ImportarSaldosSOFOM_Layout_FechaVencimiento);
            _encabezadosColumna.Add(5, Properties.Resources.ImportarSaldosSOFOM_Layout_Saldo);
        }

        private bool ValidarRegistros(DataTable datos)
        {
            var result = false;
            try
            {
                if (datos.Rows.Count > 1)
                {
                    procesar = datos;
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

                    result = true;
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.ImportarSaldosSOFOM_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al validar registros del archivo de Excel.");
            }
            return result;
        }

        private bool ValidarEncabezado(DataTable datos)
        {
            var result = false;
            try
            {
                if (datos != null)
                {
                    if (datos.Rows.Count > 0)
                    {
                        var cabecero = datos.Rows[RenglonEncabezados];
                        result = true;
                        foreach (var encabezado in _encabezadosColumna)
                        {
                            if (result)
                            {
                                switch (encabezado.Key)
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                        if (cabecero[encabezado.Key].ToString().Trim().ToUpper() != encabezado.Value.ToUpper())
                                        {
                                            result = false;
                                            SkMessageBox.Show(
                                                System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                string.Format(Properties.Resources.ImportarSaldosSOFOM_ColumnaFalta,
                                                              encabezado.Value), MessageBoxButton.OK, MessageImage.Warning);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.ImportarSaldosSOFOM_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.ImportarSaldosSOFOM_ArchivoVacio), MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al validar encabezados del archivo de Excel.");
            }

            return result;
        }

        private void MostrarDatos(IEnumerable<ImportarSaldosSOFOMInfo> saldos, List<ImportarSaldosSOFOMInfo> proveedor)
        {
            try
            {
                gridDatos.ItemsSource = saldos;
                gridDatos2.ItemsSource = proveedor.Where(e => e.Existe);
                gridDatos3.ItemsSource = proveedor.Where(e => e.Existe == false);
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al mostrar información en patalla.");
            }
        }

        private void Nuevo()
        {
            gridDatos.ItemsSource = new List<ImportarSaldosSOFOMInfo>();
            gridDatos2.ItemsSource = new List<ImportarSaldosSOFOMInfo>();
            gridDatos3.ItemsSource = new List<ImportarSaldosSOFOMInfo>();
        }

        private List<ImportarSaldosSOFOMInfo> Guardar(List<ImportarSaldosSOFOMInfo> saldos)
        {
            try
            {
                var saldosPL = new ImportarSaldosSOFOMPL();
                return saldosPL.Guardar(saldos);
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al guardar información.");
            }
        }

        private List<ImportarSaldosSOFOMInfo> ConvertirDataTableLista(DataTable datos)
        {
            try
            {
                //Antes de crear la lista, eliminamos el cabecero de la tabla
                datos.Rows.RemoveAt(0);
                return (from DataRow row in datos.Rows
                        select new ImportarSaldosSOFOMInfo
                        {
                            UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                            CreditoID = Convert.ToInt32(row[0].ToString()),
                            Nombre = row[1].ToString(),
                            TipoCredito = new TipoCreditoInfo { TipoCreditoID = 0, Descripcion = row[2].ToString() },
                            FechaAlta = string.Format("{0:s}",row[3].ToString().Substring(0, 10)),
                            FechaVencimiento = string.Format("{0:s}", row[4].ToString().Substring(0, 10)),
                            Saldo = Convert.ToDouble(row[5].ToString())
                        }).ToList();
            }
            catch (Exception)
            {
                throw new ExcepcionServicio("Error al convertir datos de Excel.");
            }

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

        #endregion
    }
}
