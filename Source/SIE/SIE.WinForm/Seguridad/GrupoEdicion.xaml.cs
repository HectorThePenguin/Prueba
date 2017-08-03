using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Seguridad
{
    /// <summary>
    /// Interaction logic for GrupoEdicion.xaml
    /// </summary>
    public partial class GrupoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private GrupoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (GrupoInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GrupoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Grupo Existente
        /// </summary>
        /// <param name="grupoInfo"></param>
        public GrupoEdicion(GrupoInfo grupoInfo)
        {
           InitializeComponent();
           grupoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = grupoInfo;
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
            GenerarTreeView();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Valida que solo se puedadn capturar letras y numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarNumerosYLetrasConAcento(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasConAcentos(e.Text);
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new GrupoInfo
                           {
                               Descripcion = string.Empty,
                               Activo = EstatusEnum.Activo,
                               GrupoID = 0,
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               GrupoFormularioInfo = new GrupoFormularioInfo
                                                         {
                                                             Acceso = new AccesoInfo(),
                                                             Formulario = new FormularioInfo
                                                                              {
                                                                                  Modulo = new ModuloInfo(),
                                                                                  Acceso = new AccesoInfo()
                                                                              },
                                                             ListaFormulario = new List<FormularioInfo>()
                                                         }
                           };
            GenerarTreeView();
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtGrupoID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.GrupoEdicion_MsgGrupoIDRequerida;
                    txtGrupoID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.GrupoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.GrupoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int grupoId = Extensor.ValorEntero(txtGrupoID.Text);
                    string descripcion = txtDescripcion.Text;

                    var grupoPL = new GrupoPL();
                    GrupoInfo grupo = grupoPL.ObtenerPorDescripcion(descripcion);

                    if (grupo != null && (grupoId == 0 || grupoId != grupo.GrupoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.GrupoEdicion_MsgDescripcionExistente, grupo.GrupoID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                      SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }

        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var grupoPL = new GrupoPL();
                    int grupoID = Contexto.GrupoID;
                    grupoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);

                    if (grupoID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Grupo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Grupo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Genera el Arbol con las pantallas
        /// </summary>
        private void GenerarTreeView()
        {
            treeGrupos.Items.Clear();
            var grupoFormularioPL = new GrupoFormularioPL();
            Contexto.GrupoFormularioInfo = grupoFormularioPL.ObtenerPorGrupoID(Contexto.GrupoID);

            var modulos = (from mod in Contexto.GrupoFormularioInfo.ListaFormulario
                           orderby mod.Modulo.Descripcion
                           select new
                           {
                               mod.Modulo.ModuloID,
                               mod.Modulo.Descripcion,
                           }).Distinct().ToList();

            StackPanel stack = GenerarEncabezado();
            var treeHeader = new TreeViewItem
            {
                Header = stack,
            };
            treeGrupos.Items.Add(treeHeader);

            modulos.ForEach(desc =>
            {
                stack = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 560,
                };
                var lbl = new Label
                {
                    Content = desc.Descripcion,
                };
                stack.Children.Add(lbl);
                treeHeader = new TreeViewItem
                {
                    Header = stack,
                };
                treeGrupos.Items.Add(treeHeader);

                List<FormularioInfo> formularios =
                    Contexto.GrupoFormularioInfo.ListaFormulario.Where(
                        pantalla => pantalla.Modulo.ModuloID == desc.ModuloID).Select(
                            formulario => formulario).ToList();

                for (var indexFormularios = 0
                     ;
                     indexFormularios < formularios.Count;
                     indexFormularios++)
                {
                    stack = ObtenerPanelItem(indexFormularios,
                                             formularios[indexFormularios].Descripcion,
                                             formularios[indexFormularios].Acceso);

                    var treeItem = new TreeViewItem { Header = stack };
                    treeHeader.Items.Add(treeItem);
                }
            });
        }

        /// <summary>
        /// Obtiene un StackPanel que para mostrar las
        /// pantallas pertenencientes al Catalogo
        /// </summary>
        /// <param name="indexFormularios"></param>
        /// <param name="descripcion"></param>
        /// <param name="acceso"></param>
        /// <returns></returns>
        private StackPanel ObtenerPanelItem(int indexFormularios, string descripcion, AccesoInfo acceso)
        {
            var chkLectura = new CheckBox { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center};
            var chkEscritura = new CheckBox { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
            var bindLectura = new Binding("AccesoId")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = acceso,
                Converter = new LecturaValueConverter(),
                ConverterParameter = 0
            };
            chkLectura.SetBinding(CheckBox.IsCheckedProperty, bindLectura);
            var bindEscritura = new Binding("AccesoId")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = acceso,
                Converter = new EscrituraValueConverter(),
                ConverterParameter = 0
            };
            chkEscritura.SetBinding(CheckBox.IsCheckedProperty, bindEscritura);

            Brush colorItem;
            if (indexFormularios % 2 == 0)
            {
                colorItem = new SolidColorBrush(Colors.LavenderBlush);
            }
            else
            {
                colorItem = new SolidColorBrush(Colors.White);
            }

            var grdDetalles = new Grid();
            grdDetalles.SetValue(Grid.BackgroundProperty, colorItem);
            var columnaDescripcion = new ColumnDefinition { Width = new GridLength(300) };
            var columnaLectura = new ColumnDefinition { Width = new GridLength(150) };
            var columnaEscritura = new ColumnDefinition { Width = new GridLength(150) };

            grdDetalles.ColumnDefinitions.Add(columnaDescripcion);
            grdDetalles.ColumnDefinitions.Add(columnaLectura);
            grdDetalles.ColumnDefinitions.Add(columnaEscritura);

            var row = new RowDefinition { Height = new GridLength(20) };
            grdDetalles.RowDefinitions.Add(row);

            var lbl = new Label
            {
                Content = descripcion,
            };

            Grid.SetColumn(lbl, 0);
            Grid.SetRow(lbl, 0);

            Grid.SetColumn(chkLectura, 1);
            Grid.SetRow(chkLectura, 0);

            Grid.SetColumn(chkEscritura, 2);
            Grid.SetRow(chkEscritura, 0);

            grdDetalles.Children.Add(lbl);
            grdDetalles.Children.Add(chkLectura);
            grdDetalles.Children.Add(chkEscritura);

            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 540,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
            };
            stack.Children.Add(grdDetalles);

            return stack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StackPanel GenerarEncabezado()
        {
            var grdEncabezado = new Grid();
            var columnaDescripcion = new ColumnDefinition { Width = new GridLength(300) };
            var columnaLectura = new ColumnDefinition { Width = new GridLength(130) };
            var columnaEscritura = new ColumnDefinition { Width = new GridLength(130) };

            grdEncabezado.ColumnDefinitions.Add(columnaDescripcion);
            grdEncabezado.ColumnDefinitions.Add(columnaLectura);
            grdEncabezado.ColumnDefinitions.Add(columnaEscritura);

            grdEncabezado.RowDefinitions.Add(new RowDefinition());

            var lblPantalla = new Label
            {
                Content = Properties.Resources.GrupoFormularioEdicion_lblPantalla,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblPantalla);
            Grid.SetColumn(lblPantalla, 0);
            Grid.SetRow(lblPantalla, 0);

            var lblLectura = new Label
            {
                Content = Properties.Resources.GrupoFormularioEdicion_lblLectura,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblLectura);
            Grid.SetColumn(lblLectura, 1);
            Grid.SetRow(lblLectura, 0);

            var lblEscritura = new Label
            {
                Content =
                    string.Format("{0, 15}",
                                  Properties.Resources.GrupoFormularioEdicion_lblEscritura),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblEscritura);
            Grid.SetColumn(lblEscritura, 2);
            Grid.SetRow(lblEscritura, 0);

            var stack = new StackPanel
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Width = 560,
                            };
            stack.Children.Add(grdEncabezado);

            return stack;
        }

        #endregion Métodos
    }
}

