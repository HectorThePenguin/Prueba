using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Drawing;
using SIE.Services.Properties;


namespace SIE.WinForm.CargaInicial
{
    /// <summary>
    /// Lógica de interacción para MigrarInventarioAnimales.xaml
    /// </summary>
    public partial class MigrarInventarioAnimales 
    {

        private List<ResumenInfo> listaResumen = new List<ResumenInfo>();
        private readonly Dictionary<int, string> encabezadosColumna = new Dictionary<int, string>();
        private const int RenglonEncabezados = 1;
        private List<MigracionCifrasControlInfo> cifrasControl;

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroCargaMPPA Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroCargaMPPA)DataContext;
            }
            set { DataContext = value; }
        }

        public MigrarInventarioAnimales()
        {
            InitializeComponent();
            InicializaContexto();
            CargarEncabezadosColumna();
            HabilitarControlesMigracion(1);
        }

        private void CargarEncabezadosColumna()
        {
            // FEC_INI	FEC_READY	CORRAL	LOTE	TIPGANADO	CABEZAS	DIASENG	GDIARIA	PESOORI	PESOPROY	FORM_ACT	CONS_PROM	GANADO	ALIMENTO	COMISIONES	FLETES	IMPPRED	GUIAS_TTO	SEG_CORRAL	SEG_TRANSP	OPENGORDA	OPABASTO	OPPTALIM	BAO	ALIMCTROS	URZ	MEDICTROS	RENTA	SEGENFEXOT	MED_ENFER	MED_IMPLA	MED_REIMPL	PERMISOS	SGANCENACO	MEDPRADE	ALIMCADIS	MEDICADIS	OPPTALIME
            encabezadosColumna.Add(1, Properties.Resources.MigracionInventarioSIAP_FEC_INI);
            encabezadosColumna.Add(2, Properties.Resources.MigracionInventarioSIAP_FEC_READY);
            encabezadosColumna.Add(3, Properties.Resources.MigracionInventarioSIAP_CORRAL);
            encabezadosColumna.Add(4, Properties.Resources.MigracionInventarioSIAP_LOTED);
            encabezadosColumna.Add(5, Properties.Resources.MigracionInventarioSIAP_TIPGANADO);
            encabezadosColumna.Add(6, Properties.Resources.MigracionInventarioSIAP_CABEZAS);
            encabezadosColumna.Add(7, Properties.Resources.MigracionInventarioSIAP_DIASENG);
            encabezadosColumna.Add(8, Properties.Resources.MigracionInventarioSIAP_GDIARIA);
            encabezadosColumna.Add(9, Properties.Resources.MigracionInventarioSIAP_PESOORI);
            encabezadosColumna.Add(10, Properties.Resources.MigracionInventarioSIAP_PESOPROY);
            encabezadosColumna.Add(11, Properties.Resources.MigracionInventarioSIAP_FORM_ACT);
            encabezadosColumna.Add(12, Properties.Resources.MigracionInventarioSIAP_CONS_PROM);
            encabezadosColumna.Add(13, Properties.Resources.MigracionInventarioSIAP_GANADO);
            encabezadosColumna.Add(14, Properties.Resources.MigracionInventarioSIAP_ALIMENTO);
            encabezadosColumna.Add(15, Properties.Resources.MigracionInventarioSIAP_COMISIONES);
            encabezadosColumna.Add(16, Properties.Resources.MigracionInventarioSIAP_FLETES);
            encabezadosColumna.Add(17, Properties.Resources.MigracionInventarioSIAP_IMPPRED);
            encabezadosColumna.Add(18, Properties.Resources.MigracionInventarioSIAP_GUIAS_TTO);
            encabezadosColumna.Add(19, Properties.Resources.MigracionInventarioSIAP_SEG_CORRAL);
            encabezadosColumna.Add(20, Properties.Resources.MigracionInventarioSIAP_SEG_TRANSP);
            encabezadosColumna.Add(21, Properties.Resources.MigracionInventarioSIAP_OPENGORDA);
            encabezadosColumna.Add(22, Properties.Resources.MigracionInventarioSIAP_OPABASTO);
            encabezadosColumna.Add(23, Properties.Resources.MigracionInventarioSIAP_OPPTALIM);
            encabezadosColumna.Add(24, Properties.Resources.MigracionInventarioSIAP_BAO);
            encabezadosColumna.Add(25, Properties.Resources.MigracionInventarioSIAP_ALIMCTROS);
            encabezadosColumna.Add(26, Properties.Resources.MigracionInventarioSIAP_URZ);
            encabezadosColumna.Add(27, Properties.Resources.MigracionInventarioSIAP_MEDICTROS);
            encabezadosColumna.Add(28, Properties.Resources.MigracionInventarioSIAP_RENTA);
            encabezadosColumna.Add(29, Properties.Resources.MigracionInventarioSIAP_SEGENFEXOT);
            encabezadosColumna.Add(30, Properties.Resources.MigracionInventarioSIAP_MED_ENFER);
            encabezadosColumna.Add(31, Properties.Resources.MigracionInventarioSIAP_MED_IMPLA);
            encabezadosColumna.Add(32, Properties.Resources.MigracionInventarioSIAP_MED_REIMPL);
            encabezadosColumna.Add(33, Properties.Resources.MigracionInventarioSIAP_PERMISOS);
            encabezadosColumna.Add(34, Properties.Resources.MigracionInventarioSIAP_SGANCENACO);
            encabezadosColumna.Add(35, Properties.Resources.MigracionInventarioSIAP_MEDPRADE);
            encabezadosColumna.Add(36, Properties.Resources.MigracionInventarioSIAP_ALIMCADIS);
            encabezadosColumna.Add(37, Properties.Resources.MigracionInventarioSIAP_MEDICADIS);
            encabezadosColumna.Add(38, Properties.Resources.MigracionInventarioSIAP_OPPTALIME);

            encabezadosColumna.Add(39, Properties.Resources.MigracionInventarioSIAP_GASTOS_FINANCIEROS);
            encabezadosColumna.Add(40, Properties.Resources.MigracionInventarioSIAP_GASTOS_PRADERAS_FIJOS);
            encabezadosColumna.Add(41, Properties.Resources.MigracionInventarioSIAP_SEGURO_ALTA_MORTANDAD_PRADERA);
            encabezadosColumna.Add(42, Properties.Resources.MigracionInventarioSIAP_ALIMENTO_EN_DESCANSO);
            encabezadosColumna.Add(43, Properties.Resources.MigracionInventarioSIAP_MEDICAMENTO_EN_DESCANSO);
            encabezadosColumna.Add(44, Properties.Resources.MigracionInventarioSIAP_MANEJO_DE_GANADO);
            encabezadosColumna.Add(45, Properties.Resources.MigracionInventarioSIAP_COSTO_DE_PRADERA);
            encabezadosColumna.Add(46, Properties.Resources.MigracionInventarioSIAP_DEMORAS);
            encabezadosColumna.Add(47, Properties.Resources.MigracionInventarioSIAP_MANIOBRAS);
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroCargaMPPA
            {
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo
                    {
                        TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                    },
                    ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                                {
                                    new TipoOrganizacionInfo
                                        {
                                            TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()   
                                        }
                                }
                }
            };
        }

        private void MigrarInventarioAnimales_OnLoaded(object sender, RoutedEventArgs e)
        {
            //btnGuardar.IsEnabled = false;
            skAyudaOrganizacion.AsignarFoco();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaOrganizacion.AyudaConDatos += (o, args) =>
            {
                Contexto.Organizacion.TipoOrganizacion = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                };
                Contexto.Organizacion.ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                {
                    new TipoOrganizacionInfo
                        {
                            TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                        }
                };
            };
        }

        private void Examinar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialogo = new OpenFileDialog
                {
                    DefaultExt = ".xls",
                    //Filter = "EXCEL Files (*.xls)|*.xls"
                    Filter = Properties.Resources.CargaMPPA_FiltroExcel
                };
                DialogResult resultado = dialogo.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dialogo.FileName))
                    {
                        if (File.Exists(dialogo.FileName))
                        {
                            Contexto.Ruta = dialogo.FileName;
                            HabilitarControlesMigracion(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Validar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    gridDatos.ItemsSource = null;
                    CargarArchivoImportar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CargaMPPA_ErrorValidar, MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CargaMPPA_SeleccioneOrganizacion, MessageBoxButton.OK,
                                     MessageImage.Warning);
                skAyudaOrganizacion.AsignarFoco();
                return false;
            }
            if (string.IsNullOrWhiteSpace(Contexto.Ruta))
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CargaMPPA_SeleccioneRuta, MessageBoxButton.OK,
                                  MessageImage.Warning);
                return false;
            }
            return true;
        }

        private void CargarArchivoImportar()
        {
            try
            {

                var archivoCarga = new FileInfo(Contexto.Ruta);
                listaResumen = new List<ResumenInfo>();
                // Open and read the XlSX file.
                using (var excel = new ExcelPackage(archivoCarga))
                {
                    ExcelWorkbook libro = excel.Workbook;
                    if (libro == null || libro.Worksheets.Count == 0)
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                                          Properties.Resources.CargaMPPA_ArchivoSinDatos, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    // Get the first worksheet
                    ExcelWorksheet hojaExcel = libro.Worksheets.First();

                    if (!hojaExcel.Name.ToUpper().Equals(Properties.Resources.MigracionInventarioSIAP_NombreHoja.ToUpper(), 
                                                         StringComparison.InvariantCultureIgnoreCase))
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.MigracionInventarioSIAP_NombreIncorrectoHoja, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }

                    if (!ValidarEncabezado(hojaExcel))
                    {
                        return;
                    }
                    for (int renglon = RenglonEncabezados + 1; renglon <= hojaExcel.Dimension.End.Row; renglon++)
                    {
                        var carga = new ResumenInfo();

                        object columnaVacia = hojaExcel.Cells[renglon, 1].Value;
                        if (columnaVacia == null || string.IsNullOrWhiteSpace(columnaVacia.ToString()))
                        {
                            continue;
                        }

                        #region AsignarPropiedades

                        var propiedades = carga.GetType().GetProperties();
                        foreach (var propInfo in propiedades)
                        {
                            dynamic customAttributes = carga.GetType().GetProperty(propInfo.Name).GetCustomAttributes(typeof(AtributoCargaMPPA), true);
                            if (customAttributes.Length > 0)
                            {
                                for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                                {
                                    var atributos = (AtributoCargaMPPA)customAttributes[indexAtributos];
                                    int celdaArchivo = atributos.Celda;
                                    TypeCode tipoDato = atributos.TipoDato;
                                    bool aceptaVacio = atributos.AceptaVacio;

                                    object dato = hojaExcel.Cells[renglon, celdaArchivo].Value;

                                    switch (tipoDato)
                                    {
                                        case TypeCode.String:
                                            string valorString;
                                            valorString = dato == null ? "" : dato.ToString();
                                            if (valorString == "")
                                            {
                                                if (aceptaVacio)
                                                {
                                                    propInfo.SetValue(carga, "", null);
                                                }
                                                else
                                                {
                                                    carga.MensajeAlerta =
                                                        string.Format(Properties.Resources.CargaMPPA_ErrorColumna,
                                                                      renglon, propInfo.Name);
                                                    break;
                                                }
                                            }
                                            propInfo.SetValue(carga, valorString, null);
                                            break;
                                        case TypeCode.Int32:
                                            int valorInt;
                                            int.TryParse(dato == null ? "" : dato.ToString(), out valorInt);
                                            if (valorInt == 0)
                                            {
                                                if (aceptaVacio)
                                                {
                                                    propInfo.SetValue(carga, 0, null);
                                                }
                                                else
                                                {
                                                    carga.MensajeAlerta =
                                                        string.Format(Properties.Resources.CargaMPPA_ErrorColumna,
                                                                      renglon, propInfo.Name);
                                                    break;
                                                }
                                            }
                                            propInfo.SetValue(carga, valorInt, null);
                                            break;
                                        case TypeCode.Decimal:
                                            decimal valorDecimal;
                                            decimal.TryParse(dato == null ? "" : dato.ToString(), out valorDecimal);
                                            if (valorDecimal == 0)
                                            {
                                                if (aceptaVacio)
                                                {
                                                    propInfo.SetValue(carga, 0, null);
                                                }
                                                else
                                                {
                                                    carga.MensajeAlerta =
                                                        string.Format(Properties.Resources.CargaMPPA_ErrorColumna,
                                                                      renglon, propInfo.Name);
                                                    break;
                                                }
                                            }
                                            propInfo.SetValue(carga, valorDecimal, null);
                                            break;
                                        case TypeCode.Double:
                                            Double valorDouble;
                                            Double.TryParse(dato == null ? "" : dato.ToString(), out valorDouble);
                                            if (valorDouble == 0)
                                            {
                                                if (aceptaVacio)
                                                {
                                                    propInfo.SetValue(carga, 0, null);
                                                }
                                                else
                                                {
                                                    carga.MensajeAlerta =
                                                        string.Format(Properties.Resources.CargaMPPA_ErrorColumna,
                                                                      renglon, propInfo.Name);
                                                    break;
                                                }
                                            }
                                            propInfo.SetValue(carga, valorDouble, null);
                                            break;
                                        case TypeCode.DateTime:
                                            DateTime valorFecha;
                                            DateTime.TryParse(dato == null ? "" : dato.ToString(), out valorFecha);
                                            if (valorFecha == DateTime.MinValue)
                                            {
                                                if (aceptaVacio)
                                                {
                                                    propInfo.SetValue(carga, DateTime.MinValue, null);
                                                }
                                                else
                                                {
                                                    carga.MensajeAlerta =
                                                        string.Format(Properties.Resources.CargaMPPA_ErrorColumna,
                                                                      renglon, propInfo.Name);
                                                    break;
                                                }
                                            }
                                            propInfo.SetValue(carga, valorFecha, null);
                                            break;
                                        default:
                                            propInfo.SetValue(carga, null, null);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion AsignarPropiedades

                        listaResumen.Add(carga);                        
                    }

                    if (listaResumen != null && listaResumen.Any())
                    {
                        var migracionBL = new MigracionBL();
                        // Insertar la lista de resumen en la tabla de RESUMEN
                        MigracionCifrasControlInfo cifras = migracionBL.GuardarResumen(listaResumen, Contexto.Organizacion.OrganizacionID);
                        if (cifras != null)
                        {
                            cifrasControl = new List<MigracionCifrasControlInfo> { cifras };
                            gridDatos.ItemsSource = null;
                            gridDatos.ItemsSource = cifrasControl;
                            HabilitarControlesMigracion(3);
                            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format(Properties.Resources.CargaMPPA_RegistroSinProblemas, listaResumen.Count),
                                MessageBoxButton.OK,
                                MessageImage.Correct);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            string.Format(Properties.Resources.MigracionInventarioSIAP_NoContieneRegistro, listaResumen.Count),
                            MessageBoxButton.OK,
                            MessageImage.Correct);
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(
                    System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.CargaMPPA_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
        }

        protected void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "Migrar Inventario de Animales SIAP",
                    DefaultExt = ".xlsx",
                    Filter = "Excel files (.xlsx)|*.xlsx"
                };

                // Show save file dialog box
                var result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result != true) return;

                // Save document
                string nombreArchivo = dlg.FileName;

                ExportarArchivo(nombreArchivo);

                //Guardado correctamente
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.MigracionInventarioSIAP_LayOutExportado,
                    MessageBoxButton.OK, MessageImage.Correct);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfigurarFormula_ExportarError,
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
        }

        private void ExportarArchivo(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                try
                {
                    File.Delete(nombreArchivo);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(Properties.Resources.MigracionInventarioSIAP_ArchivoEnUso);
                }

            }
            var newFile = new FileInfo(nombreArchivo);

            using (var pck = new ExcelPackage(newFile))
            {
                // get the handle to the existing worksheet
                var wsData = pck.Workbook.Worksheets.Add(Properties.Resources.MigracionInventarioSIAP_TituloExcel);

                if (encabezadosColumna != null && encabezadosColumna.Count >= 0)
                {
                    int i = 1;
                    foreach (var columna in encabezadosColumna)
                    {
                        wsData.Cells[1, i].Value = columna.Value;
                        i++;
                    }


                    using (ExcelRange r = wsData.Cells[1,1,1, encabezadosColumna.Count()])
                    {
                        //r.Merge = true;
                        r.Style.Font.SetFromFont(new Font("Arial Bold", 14, System.Drawing.FontStyle.Regular));
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 178, 34, 34));
                    }

                    ExcelRangeBase dataRange;
                    dataRange = wsData.Cells["A1:K1"];
                    dataRange.AutoFitColumns();

                    pck.Save();

                }
                
            }

        }

        private bool ValidarEncabezado(ExcelWorksheet hojaExcel)
        {
            foreach (var encabezado in encabezadosColumna)
            {
                object columna = hojaExcel.Cells[RenglonEncabezados, encabezado.Key].Value;
                if (columna == null)
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.CargaMPPA_ColumnaFalta, encabezado.Value), MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                if (!encabezado.Value.ToUpper().Equals(columna.ToString().ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.CargaMPPA_ColumnaFalta, encabezado.Value), MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }

            return true;
        }

        private void CargaInicial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var migracionBL = new MigracionBL();
                MigracionCifrasControlInfo cifras = migracionBL.CrearCargaInicialAnimales(Contexto.Organizacion.OrganizacionID);
                if (cifras != null)
                {
                    if (cifrasControl != null)
                    {
                        cifrasControl.Add(cifras);
                    }
                    else
                    {
                        cifrasControl = new List<MigracionCifrasControlInfo> {cifras};
                    }
                    gridDatos.ItemsSource = null;
                    gridDatos.ItemsSource = cifrasControl;
                    HabilitarControlesMigracion(4);
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MigracionInventarioSIAP_OKCargaInicial,
                        MessageBoxButton.OK,MessageImage.Correct);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(
                    System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.MigracionInventarioSIAP_ErrorCargaInicial, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
            
        }

        private void Migrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.MigracionInventarioSIAP_SeguroMigrar, MessageBoxButton.YesNo,
                    MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var migracionBL = new MigracionBL();
                    MigracionCifrasControlInfo cifras = migracionBL.GuardarAnimalesSIAP(Contexto.Organizacion.OrganizacionID);
                    if (cifras != null)
                    {
                        if (cifrasControl != null)
                        {
                            cifrasControl.Add(cifras);
                        }
                        else
                        {
                            cifrasControl = new List<MigracionCifrasControlInfo> { cifras };
                        }

                        gridDatos.ItemsSource = null;
                        gridDatos.ItemsSource = cifrasControl;
                        HabilitarControlesMigracion(5);
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.MigracionInventarioSIAP_OKMigracion,
                            MessageBoxButton.OK, MessageImage.Correct);
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(
                    System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.MigracionInventarioSIAP_ErrorMigracion, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
        }

        private void HabilitarControlesMigracion(int paso)
        {
            
            btnExaminar.IsEnabled = false;
            btnValidar.IsEnabled = false;
            btnCargaInicial.IsEnabled = false;
            btnMigrar.IsEnabled = false;

            switch (paso)
            {
                case 1:
                    btnExaminar.IsEnabled = true;
                    break;
                case 2:
                    btnValidar.IsEnabled = true;
                    break;
                case 3:
                    btnCargaInicial.IsEnabled = true;
                    break;
                case 4:
                    btnMigrar.IsEnabled = true;
                    break;
            }

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.MigracionInventarioSIAP_CerrarSinGuardar, MessageBoxButton.YesNo,
                    MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                InicializaContexto();
                HabilitarControlesMigracion(1);
                gridDatos.ItemsSource = null;
            }
        }
    }
}
