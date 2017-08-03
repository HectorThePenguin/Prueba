using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using OfficeOpenXml;
using SIE.Base.Log;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.CargaInicial
{
    /// <summary>
    /// Lógica de interacción para CargaMPPA.xaml
    /// </summary>
    public partial class CargaMPPA
    {
        private List<CargaMPPAModel> listaInventariosValidos = new List<CargaMPPAModel>();
        private List<CargaMPPAModel> listaInventariosInvalidos = new List<CargaMPPAModel>();
        private readonly Dictionary<int, string> encabezadosColumna = new Dictionary<int, string>();
        private const int RenglonEncabezados = 1;

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

        public CargaMPPA()
        {
            InitializeComponent();
            InicializaContexto();
            CargarEncabezadosColumna();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CargaMPPA_ErrorGuardar, MessageBoxButton.OK,
                                     MessageImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                           MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                InicializaContexto();
            }

        }

        private void Validar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    gridDatos.ItemsSource = null;
                    listaInventariosInvalidos = new List<CargaMPPAModel>();
                    listaInventariosValidos = new List<CargaMPPAModel>();
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

        private void Examinar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialogo = new OpenFileDialog
                                  {
                                      DefaultExt = ".xls",
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CargaMPPA_OnLoaded(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = false;
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

        private void CargarArchivoImportar()
        {
            try
            {
                var almacenPL = new AlmacenPL();
                var almacenInventarioPL = new AlmacenInventarioPL();
                var almacenInventarioLotePL = new AlmacenInventarioLotePL();
                var productoPL = new ProductoPL();

                List<AlmacenInfo> almacenesOrganizacion =
                    almacenPL.ObtenerAlmacenesPorOrganizacion(Contexto.Organizacion.OrganizacionID);

                List<ProductoInfo> productos = productoPL.ObtenerPorEstados(EstatusEnum.Activo);


                if (almacenesOrganizacion == null)
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CargaMPPA_SinAlmacenes, MessageBoxButton.OK,
                                      MessageImage.Warning);
                    return;
                }

                IList<AlmacenInventarioInfo> almacenesInventario =
                    almacenInventarioPL.ObtenerPorAlmacenXML(almacenesOrganizacion) ?? new List<AlmacenInventarioInfo>();

                IList<AlmacenInventarioLoteInfo> almacenesInventarioLote =
                    almacenInventarioLotePL.ObtenerLotesPorAlmacenInventarioXML(almacenesInventario.ToList()) ??
                    new List<AlmacenInventarioLoteInfo>();


                var archivoCarga = new FileInfo(Contexto.Ruta);
                // Open and read the XlSX file.
                using (var excel = new ExcelPackage(archivoCarga))
                {
                    ExcelWorkbook libro = excel.Workbook;
                    if (libro == null || libro.Worksheets.Count == 0)
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CargaMPPA_ArchivoSinDatos, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    // Get the first worksheet
                    ExcelWorksheet hojaExcel = libro.Worksheets.First();

                    if (!hojaExcel.Name.ToUpper().Equals(Properties.Resources.CargaMPPA_NombreHoja.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CargaMPPA_NombreIncorrectoHoja, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }

                    if (!ValidarEncabezado(hojaExcel))
                    {
                        return;
                    }
                    for (int renglon = RenglonEncabezados + 1; renglon <= hojaExcel.Dimension.End.Row; renglon++)
                    {
                        var carga = new CargaMPPAModel();

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
                        #region Validaciones

                        AlmacenInfo almacenCarga = almacenesOrganizacion.FirstOrDefault(alm => alm.AlmacenID == carga.AlmacenID);
                        ProductoInfo producto = productos.FirstOrDefault(pro => pro.ProductoId == carga.ProductoID);
                        AlmacenInventarioInfo almacenInventario =
                            almacenesInventario.FirstOrDefault(
                                ai => ai.AlmacenID == carga.AlmacenID && ai.ProductoID == carga.ProductoID);

                        if (almacenCarga == null)
                        {
                            carga.MensajeAlerta = string.Format(Properties.Resources.CargaMPPA_NoExisteAlmacen,
                                                                carga.AlmacenID, renglon);
                        }
                        carga.Almacen = almacenCarga;
                        if (producto == null)
                        {
                            carga.MensajeAlerta = string.Format(Properties.Resources.CargaMPPA_NoExisteProducto,
                                                                carga.ProductoID, renglon);
                        }
                        carga.Producto = producto;
                        if (almacenInventario != null && almacenInventario.Cantidad > 0)
                        {
                            carga.MensajeAlerta = string.Format(Properties.Resources.CargaMPPA_ExisteInventario,
                                                                carga.ProductoID, carga.AlmacenID, renglon);
                        }
                        carga.AlmacenInventario = almacenInventario;

                        if (carga.AlmacenInventario != null && carga.AlmacenInventario.AlmacenInventarioID > 0)
                        {
                            List<AlmacenInventarioLoteInfo> lotesInventario =
                                almacenesInventarioLote.Where(
                                    ail =>
                                    ail.AlmacenInventario.AlmacenInventarioID ==
                                    carga.AlmacenInventario.AlmacenInventarioID).ToList();

                            if (lotesInventario.Any())
                            {
                                AlmacenInventarioLoteInfo lote =
                                    lotesInventario.FirstOrDefault(ail => ail.Lote == carga.Lote);

                                if (lote != null)
                                {
                                    carga.MensajeAlerta = string.Format(Properties.Resources.CargaMPPA_ExisteLote,
                                                                carga.Lote, carga.AlmacenID, renglon);
                                }
                            }
                        }
                        CargaMPPAModel cargaRepetida =
                            listaInventariosValidos.FirstOrDefault(car => car.ProductoID == carga.ProductoID &&
                                                                          car.AlmacenID == carga.AlmacenID &&
                                                                          car.Lote == carga.Lote);

                        if (cargaRepetida != null)
                        {
                            carga.MensajeAlerta = string.Format(Properties.Resources.CargaMPPA_ExisteRenglonRepetido, renglon);
                        }


                        if (!string.IsNullOrWhiteSpace(carga.MensajeAlerta))
                        {
                            listaInventariosInvalidos.Add(carga);
                        }
                        else
                        {
                            listaInventariosValidos.Add(carga);
                        }
                        #endregion Validaciones
                    }
                }
                if (listaInventariosInvalidos.Any())
                {
                    gridDatos.ItemsSource = listaInventariosInvalidos;
                    btnGuardar.IsEnabled = false;
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.CargaMPPA_RegistroProblemas, listaInventariosInvalidos.Count),
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                    return;
                }

                if (listaInventariosValidos.Any())
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.CargaMPPA_RegistroSinProblemas, listaInventariosValidos.Count),
                        MessageBoxButton.OK,
                        MessageImage.Correct);
                }

                btnGuardar.IsEnabled = true;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CargaMPPA_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
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

        private void CargarEncabezadosColumna()
        {
            encabezadosColumna.Add(1, Properties.Resources.CargaMPPA_AlmacenID);
            encabezadosColumna.Add(2, Properties.Resources.CargaMPPA_ProductoID);
            encabezadosColumna.Add(3, Properties.Resources.CargaMPPA_Lote);
            encabezadosColumna.Add(4, Properties.Resources.CargaMPPA_CantidadInicial);
            encabezadosColumna.Add(5, Properties.Resources.CargaMPPA_ImporteInicial);
            encabezadosColumna.Add(6, Properties.Resources.CargaMPPA_CantidadActual);
            encabezadosColumna.Add(7, Properties.Resources.CargaMPPA_ImporteActual);
            encabezadosColumna.Add(8, Properties.Resources.CargaMPPA_Piezas);
            encabezadosColumna.Add(9, Properties.Resources.CargaMPPA_FechaInicioLote);
        }

        private void Guardar()
        {
            var cargaInicialInventarioBL = new CargaInicialInventariosBL();
            cargaInicialInventarioBL.GenerarCargaInicial(listaInventariosValidos);
            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.CargaMPPA_GuardadoExito, MessageBoxButton.OK,
                                   MessageImage.Correct);
            InicializaContexto();
        }
    }
}
