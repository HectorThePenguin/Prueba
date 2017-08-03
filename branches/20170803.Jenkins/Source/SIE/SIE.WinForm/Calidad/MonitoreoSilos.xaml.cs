using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Windows.Media;

using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;
using Application = System.Windows.Application;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para MonitoreoSilos.xaml
    /// </summary>
    /// 
    public partial class MonitoreoSilos
    {
        #region Atributos

        private int usuarioID;
        private int organizacionId;
        private int tempMaximaGrid;
        private SKAyuda<ProductoInfo> skAyudaProducto;
        private List<MonitoreoSiloInfo> listaGridDatos;

        #endregion

        public MonitoreoSilos()
        {
            InitializeComponent();
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            AgregarAyudaProducto();
            CargarCboSilos();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ObtenerDatosGridTemperaturaMax();
            InicializaFormulario();
        }

        #region Metodos

        //Inicializa el Formulario
        private void InicializaFormulario()
        {
            CrearGrid();
            txtTemperaturaAmbiente.Value = 0;
            txtHR.Value = 0;
            cmbSilo.SelectedIndex = 0;
            txtObservaciones.Clear();
            skAyudaProducto.LimpiarCampos();
            txtTemperaturaAmbiente.Focus();
        }

        private void ObtenerDatosGridTemperaturaMax()
        {
            try
            {
                var gridPL = new MonitoreoSiloPL();
                listaGridDatos = gridPL.ObtenerOrdenAlturaMonitoreoGrid(AuxConfiguracion.ObtenerOrganizacionUsuario());

                var tempPL = new MonitoreoSiloPL();
                tempMaximaGrid = tempPL.ObtenerTemperaturaMax(AuxConfiguracion.ObtenerOrganizacionUsuario(), Properties.Resources.MonitoreoSilos_Descripcion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// Se genera la estructura del Grid
        private void CrearGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MTS");

                var ubicacion = listaGridDatos.GroupBy(column => column.UbicacionSensor)
                   .Select(grp => grp.First())
                   .OrderBy(column => column.OrdenSensor)
                   .ToList();

                foreach (MonitoreoSiloInfo ubicacionDG in ubicacion)
                {
                    dt.Columns.Add(ubicacionDG.UbicacionSensor.ToString());
                }

                DataRow renglon = dt.NewRow();

                var alturas = listaGridDatos.GroupBy(row => row.AlturaSilo)
                   .Select(grp => grp.First())
                   .OrderBy(row => row.AlturaSilo)
                   .ToList();

               foreach (MonitoreoSiloInfo alturaDG in alturas)
                {
                    renglon = dt.NewRow();
                    renglon[0]=(alturaDG.AlturaSilo.ToString());
                    dt.Rows.Add(renglon);
                }
                gridMonitoreoSilos.ItemsSource = dt.DefaultView;
                gridMonitoreoSilos.Style = null;
                dt.Columns[0].ReadOnly = true;
                gridMonitoreoSilos.ScrollIntoView(gridMonitoreoSilos.Items[0]);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// Agrega la ayuda para la busqueda de producto
        private void AgregarAyudaProducto()
        {
            try
            {
                skAyudaProducto = new SKAyuda<ProductoInfo>(200,
                    false,
                    new ProductoInfo()
                    {
                        ProductoId = 0,
                        FamiliaId  = (int) FamiliasEnum.MateriaPrimas,
                        SubfamiliaId = (int) SubFamiliasEnum.Granos,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadProductoMonitoreoSilosID",
                    "PropiedadProductoMonitoreoSilosDescripcion",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new ProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.MonitoreoSilos_MsgProductoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.MonitoreoSilos_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.MonitoreoSilos_Busqueda,
                    MensajeAgregar = Properties.Resources.MonitoreoSilos_Seleccionar,
                    TituloEtiqueta = Properties.Resources.MonitoreoSilos_LeyandaBuscar,
                    TituloPantalla = Properties.Resources.MonitoreoSilos_Busqueda_Titulo,
                    MetodoPorDescripcion = "ObtenerPorDescripcionSubFamilia"
                };

                skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;

                splAyudaProducto.Children.Clear();
                splAyudaProducto.Children.Add(skAyudaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        // Obtiene los datos del producto seleccionado
        private void ObtenerDatosProducto(string filtro)
        {
            try
            {
                var productoPl = new ProductoPL();
                ProductoInfo productoActual = skAyudaProducto.Info;
                productoActual = productoPl.ObtenerPorIDSinActivo(productoActual);

                if (productoActual != null)
                {
                    if (productoActual.Familia.FamiliaID == (int)FamiliasEnum.MateriaPrimas ||
                        productoActual.Familia.FamiliaID == (int)FamiliasEnum.Premezclas)
                    {
                        if (productoActual.Activo == EstatusEnum.Activo)
                        {
                            skAyudaProducto.Info = productoActual;
                            skAyudaProducto.Info.Familias = new List<FamiliaInfo>()
                            {
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                            };
                        }
                        else
                        {
                            skAyudaProducto.LimpiarCampos();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.MonitoreoSilos_ProductoInactivo,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaProducto.Info = new ProductoInfo()
                            {
                                ProductoId = 0,
                                Familia = new FamiliaInfo() { FamiliaID = (int)FamiliasEnum.MateriaPrimas },
                                Familias = new List<FamiliaInfo>()
                                {
                                    new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                    new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                                },
                                Activo = EstatusEnum.Activo
                            };
                        }
                    }
                    else
                    {
                        skAyudaProducto.LimpiarCampos();
                        skAyudaProducto.Info = new ProductoInfo()
                        {
                            ProductoId = 0,
                            Familia = new FamiliaInfo() { FamiliaID = (int)FamiliasEnum.MateriaPrimas },
                            Familias = new List<FamiliaInfo>()
                            {
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                            },
                            Activo = EstatusEnum.Activo

                        };
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MonitoreoSilos_MsgProductoInvalido,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    }
                }
                else
                {
                    skAyudaProducto.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MonitoreoSilos_MsgProductoInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    skAyudaProducto.Info = new ProductoInfo()
                    {
                        ProductoId = 0,
                        Familia = new FamiliaInfo() { FamiliaID = (int)FamiliasEnum.MateriaPrimas },
                        Familias = new List<FamiliaInfo>()
                        {
                            new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                            new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                        },
                        Activo = EstatusEnum.Activo
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// Carga los valores del combo de silos
        private void CargarCboSilos()
        {
            try
            {
                var silosPL = new MonitoreoSiloPL();
                var listaSilos = silosPL.ObtenerSilosParaMonitoreo(AuxConfiguracion.ObtenerOrganizacionUsuario());
                var siloSeleccione = new MonitoreoSiloInfo
                {
                    SiloDescripcion = Properties.Resources.cbo_Seleccione
                };
                listaSilos.Insert(0, siloSeleccione);
                cmbSilo.ItemsSource = listaSilos;
                cmbSilo.DisplayMemberPath = "SiloDescripcion";
                cmbSilo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region Eventos

        //Evento que se activa cuando se pierde el foco del control
        private void TxtDecimal_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var txtEditado = (DecimalUpDown)sender;
            if (txtEditado.Value == null) { txtEditado.Value = 0; }
        }

        //Evento que se activa cuando se pierde el foco del textbox Temperatura Ambiente
        private void TxtTempAmbiente_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var txtEditado = (DecimalUpDown)sender;
            if (txtEditado.Value == null) { txtEditado.Value = 0; }
            txtHR.Focus();
        }

        //Ocurre cuando se obtiene el foco en el control
        private void CmbSilo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skAyudaProducto.AsignarFoco();
            skAyudaProducto.Focus();
            //skAyudaProducto.AsignaTabIndex(1);
        }

        //Ocurre cuando se terminar de editar una celda del Grid
        private void gridMonitoreoSilos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var tempCap = (System.Windows.Controls.TextBox)e.EditingElement;

            if (tempCap.Text != String.Empty)
            {
                int tempCapCelda = Convert.ToInt16(tempCap.Text);

                if (tempCapCelda > 0)
                {
                    DataGridRow row = (DataGridRow)gridMonitoreoSilos.ItemContainerGenerator.ContainerFromIndex(gridMonitoreoSilos.SelectedIndex);
                    System.Windows.Controls.DataGridCell celda = gridMonitoreoSilos.Columns[e.Column.DisplayIndex].GetCellContent(row).Parent as System.Windows.Controls.DataGridCell;

                    if (tempMaximaGrid < tempCapCelda)
                    {
                        celda.Background = Brushes.Red;

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.MonitoreoSilos_MsgTemperaturaMaxima,
                                            MessageBoxButton.OK, MessageImage.Warning);

                        gridMonitoreoSilos.Focus();
                    }
                    else
                    {
                        celda.Background = Brushes.Green;
                    }
                }
            }
            else
            {
                DataGridRow row = (DataGridRow)gridMonitoreoSilos.ItemContainerGenerator.ContainerFromIndex(gridMonitoreoSilos.SelectedIndex);
                System.Windows.Controls.DataGridCell celda = gridMonitoreoSilos.Columns[e.Column.DisplayIndex].GetCellContent(row).Parent as System.Windows.Controls.DataGridCell;
                celda.ClearValue(System.Windows.Controls.DataGridCell.BackgroundProperty);
            }
        }

        //Ocurre cada que se presiona una tecla dentro del grid
        private void gridMonitoreoSilos_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) || //Sólo numeros
                Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt) || //No permite combinación con tecla Alt
                Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) || //No permite combinación con tecla Shift
                Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) //No permite combinación con tecla Ctrl
            {
                e.Handled = true;
            }
        }

        //Se activa cuando dan click sobre el boton
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            bool datosCompletos = false;
            bool guardoMonitoreo = false;

            if (txtTemperaturaAmbiente.Value != 0)
            {
                if (txtHR.Value != 0)
                {
                    if (cmbSilo.SelectedIndex > 0)
                    {
                        if (skAyudaProducto.Descripcion != String.Empty)
                        {
                            datosCompletos = true;

                            DataGridRow row = new DataGridRow();

                            var datosGridPL = new MonitoreoSiloPL();
                            List<MonitoreoSiloInfo> listaMonitoreoSilo = new List<MonitoreoSiloInfo>();

                            for (int i = 0; i < gridMonitoreoSilos.Items.Count; i++) //Renglones
                            {
                                gridMonitoreoSilos.ScrollIntoView(gridMonitoreoSilos.Items[i]);

                                row = (DataGridRow)gridMonitoreoSilos.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock altura = (System.Windows.Controls.TextBlock)gridMonitoreoSilos.Columns[0].GetCellContent(row);

                                for (int j = 1; j < gridMonitoreoSilos.Columns.Count; j++) //Columnas
                                {
                                    TextBlock tempCap = (System.Windows.Controls.TextBlock)gridMonitoreoSilos.Columns[j].GetCellContent(row);

                                    if (tempCap.Text != String.Empty)
                                    {
                                        string selectedColumnHeader = (string)gridMonitoreoSilos.SelectedCells[j].Column.Header;

                                        var datosGridInfo = new MonitoreoSiloInfo();
                                        datosGridInfo.TemperaturaCelda = Convert.ToInt16(tempCap.Text);
                                        datosGridInfo.AlturaSilo = Convert.ToInt16(altura.Text);
                                        datosGridInfo.UbicacionSensor = Convert.ToInt16(selectedColumnHeader);
                                        listaMonitoreoSilo.Add(datosGridInfo);
                                    }
                                }
                            }

                            var silosPL = new MonitoreoSiloPL();
                            var silosInfo = new MonitoreoSiloInfo
                            {
                                TemperaturaAmbiente = (decimal)txtTemperaturaAmbiente.Value,
                                SiloDescripcion = cmbSilo.Text,
                                HR = (decimal)txtHR.Value,
                                ProductoID = skAyudaProducto.Info.ProductoId,
                                Observaciones = txtObservaciones.Text,
                                UsuarioCreacionId = usuarioID
                            };

                            guardoMonitoreo = silosPL.Guardar(listaMonitoreoSilo, silosInfo, organizacionId);
                            if (guardoMonitoreo)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.MonitoreoSilos_MsgGuardado,
                                    MessageBoxButton.OK, MessageImage.Correct);
                                gridMonitoreoSilos.ItemsSource = null;
                                InicializaFormulario();
                            }
                        }
                        else
                        {
                            skAyudaProducto.AsignarFoco();
                            skAyudaProducto.Focus();
                            skAyudaProducto.AsignaTabIndex(1);
                        }
                    }
                    else
                    {
                        cmbSilo.Focus();
                    }
                }
                else
                {
                    txtHR.Focus();
                }
            }
            else
            {
                txtTemperaturaAmbiente.Focus();
            }

            if (!datosCompletos)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.MonitoreoSilos_MsgCamposObligatorios,
                MessageBoxButton.OK,
                MessageImage.Stop);
            }
        }

        //Se activa cuando dan click sobre el boton
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.MonitoreoSilos_MensajeCancelar,
               MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                gridMonitoreoSilos.ItemsSource = null;
                InicializaFormulario();
            }
        }

        #endregion

        private void gridMonitoreoSilos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Style _style = new Style(typeof(System.Windows.Controls.TextBox));

            _style.Setters.Add(new Setter(System.Windows.Controls.TextBox.MaxLengthProperty, 4));
            (e.Column as DataGridTextColumn).EditingElementStyle = _style;
        }
    }
}
