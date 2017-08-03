using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SIE.Base.Log;
using SIE.WinForm.Auxiliar;
using System.Reflection;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Controles
{
    /// <summary>
    /// Lógica de interacción para ControlAyuda.xaml
    /// </summary>
    public partial class ControlAyuda : IDisposable
    {
        #region CONSTRUCTORES

        public ControlAyuda()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTORES

        #region VARIABLES

        private bool existenDatos;
        private bool ventanaBusqueda;
        private bool camposEnBlanco = true;

        public event EventHandler AyudaLimpia;
        public event EventHandler AyudaConDatos;

        #endregion VARIABLES 
        
        #region PROPIEDADES

        /// <summary>
        /// Objeto de negocio que se
        /// estara invocando
        /// </summary>
        public object ObjetoNegocio { get; set; }

        /// <summary>
        /// Contexto al cual estara ligado
        /// el control
        /// </summary>
        public object Contexto
        {
            get { return DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Campo de Info que representara
        /// la clave en el control
        /// </summary>
        public string CampoClave
        {
            get
            {
                return (string)GetValue(ClaveProperty);
            }
            set
            {
                SetValue(ClaveProperty, value);
            }
        }

        /// <summary>
        /// Campo de Info que representara
        /// la descripcion en el control
        /// </summary>
        public string CampoDescripcion
        {
            get { return (string)GetValue(DescripcionProperty); }
            set
            {
                SetValue(DescripcionProperty, value);
            }
        }

        /// <summary>
        /// Indica si es ayuda simple
        /// (Solo mostrara una caja de texto)
        /// </summary>
        public bool EsAyudaSimple
        {
            get { return (bool)GetValue(EsAyudaSimpleProperty); }
            set
            {
                SetValue(EsAyudaSimpleProperty, value);
            }
        }

        /// <summary>
        /// Ancho que tomara la caja
        /// descripcion
        /// </summary>
        public int AnchoDescripcion
        {
            get { return (int)GetValue(AnchoDescripcionProperty); }
            set
            {
                SetValue(AnchoDescripcionProperty, value);
            }
        }

        /// <summary>
        /// ANcho que tomara la caja clave
        /// </summary>
        public int AnchoClave
        {
            get { return (int)GetValue(AnchoClaveProperty); }
            set
            {
                SetValue(AnchoClaveProperty, value);
            }
        }

        /// <summary>
        /// Indica si estara ligado al contexto
        /// </summary>
        public bool EsBindeable
        {
            get { return (bool)GetValue(EsBindeableProperty); }
            set
            {
                SetValue(EsBindeableProperty, value);
            }
        }

        /// <summary>
        /// Campo Info que representa la clave,
        /// en caso de que ocupen mostrarse campos
        /// que no son el identificador
        /// </summary>
        public string CampoLlaveOcultaClave
        {
            get { return (string)GetValue(LlaveOcultaProperty); }
            set
            {
                SetValue(LlaveOcultaProperty, value);
            }
        }

        /// <summary>
        /// Mostrara la descripcion
        /// </summary>
        public string EtiquetaDescripcion
        {
            get { return (string)GetValue(EtiquetaDescripcionProperty); }
            set
            {
                SetValue(EtiquetaDescripcionProperty, value);
            }
        }

        /// <summary>
        /// Metodo que se invocara desde la ayuda
        /// al presionar enter ó perder el foco
        /// </summary>
        public string MetodoInvocacion
        {
            get { return (string)GetValue(MetodoInvocacionProperty); }
            set
            {
                SetValue(MetodoInvocacionProperty, value);
            }
        }

        /// <summary>
        /// Indica si solo sera posible caputar numeros
        /// </summary>
        public bool AceptaSoloNumeros
        {
            get { return (bool)GetValue(SoloNumerosProperty); }
            set
            {
                SetValue(SoloNumerosProperty, value);
            }
        }

        /// <summary>
        /// Indica el indice que tendra el control
        /// </summary>
        public int ControlIndex
        {
            get { return (int)GetValue(ControlIndexProperty); }
            set
            {
                SetValue(ControlIndexProperty, value);
            }
        }

        /// <summary>
        /// Metodo que sera invocado desde la pantalla de Busqueda
        /// </summary>
        public string MetodoInvocacionBusqueda
        {
            get { return (string)GetValue(MetodoInvocacionBusquedaProperty); }
            set
            {
                SetValue(MetodoInvocacionBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Titulo de la pantalla de busqueda
        /// </summary>
        public string TituloBusqueda
        {
            get { return (string)GetValue(TituloBusquedaProperty); }
            set
            {
                SetValue(TituloBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Descripcion que se mostrara en la etiqueta
        /// de la pantalla de busqueda
        /// </summary>
        public string ConceptoBusqueda
        {
            get { return (string)GetValue(ConceptoBusquedaProperty); }
            set
            {
                SetValue(ConceptoBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Encabezado del grid para el campo clave en pantalla
        /// de busqueda
        /// </summary>
        public string EncabezadoClaveBusqueda
        {
            get { return (string)GetValue(EncabezadoClaveBusquedaProperty); }
            set
            {
                SetValue(EncabezadoClaveBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Encabezado del grid para el campo descripcion
        /// </summary>
        public string EncabezadoDescripcionBusqueda
        {
            get { return (string)GetValue(EncabezadoDescripcionBusquedaProperty); }
            set
            {
                SetValue(EncabezadoDescripcionBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Mensaje que se mostrara cuando se presione el boton
        /// agregar en la pantalla de busqueda sin tener un registro
        /// seleccionado
        /// </summary>
        public string MensajeAgregarBusqueda
        {
            get { return (string)GetValue(MensajeAgregarBusquedaProperty); }
            set
            {
                SetValue(MensajeAgregarBusquedaProperty, value);
            }
        }

        /// <summary>
        /// Mensaje que se mostrara cuando se quiera salir de la pantalla
        /// de busqueda
        /// </summary>
        public string MensajeCerrarBusqueda
        {
            get { return (string) GetValue(MensajeCerrarBusquedaProperty); }
            set { SetValue(MensajeCerrarBusquedaProperty, value); }
        }

        /// <summary>
        /// Mensaje que se mostrara cuando se ingrese una clave
        /// en el campo de ayuda y no se encuentre registrado
        /// en base de datos
        /// </summary>
        public string MensajeClaveInexistenteBusqueda
        {
            get { return (string)GetValue(MensajeClaveInexistenteBusquedaProperty); }
            set { SetValue(MensajeClaveInexistenteBusquedaProperty, value); }
        }

        /// <summary>
        /// Maximo numero de caracteres permitidos
        /// </summary>
        public int MaximoCaracteres
        {
            get { return (int)GetValue(MaximoCaracteresProperty); }
            set { SetValue(MaximoCaracteresProperty, value); }
        }

        /// <summary>
        /// Clave del Catalogo
        /// </summary>
        public string Clave
        {
            get { return txtClave.Text; }
            set { txtClave.Text = value; }
        }

        /// <summary>
        /// Valor de Descripcion del Catalogo
        /// </summary>
        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        /// <summary>
        /// Identificador Unico del Catalogo
        /// </summary>
        public string Id
        {
            get { return txtOculto.Text; }
            set { txtOculto.Text = value; }
        }

        /// <summary>
        /// Campo por el cual tendra dependencia
        /// </summary>
        public string CampoDependencia
        {
            get { return (string) GetValue(CampoDependenciaProperty); }
            set { SetValue(CampoDependenciaProperty, value); }
        }

        /// <summary>
        /// Mensaje que se mostrara si puede realizar la busqueda
        /// acorde a una condicion
        /// </summary>
        public string MensajeNoPuedeBuscar
        {
            get { return (string)GetValue(MensajeNoPuedeBuscarProperty); }
            set { SetValue(MensajeNoPuedeBuscarProperty, value); }
        }

        /// <summary>
        /// Accion que se ejecuta cuando sea busqueda vacia
        /// </summary>
        public Action BusquedaVacia { get; set; }

        /// <summary>
        /// Indica si puede realizar la busqueda acorde
        /// a una condicion
        /// </summary>
        public Func<bool> PuedeBuscar { get; set; }

        /// <summary>
        /// Indica si sera visible el boton de busqueda
        /// </summary>
        public bool MostrarBotonBusqueda
        {
            get { return (bool)GetValue(MostrarBotonBusquedaProperty); }
            set { SetValue(MostrarBotonBusquedaProperty, value); }
        }

        public bool BusquedaActivada { get; set; }

        #endregion PROPIEDADES

        #region PROPIEDADES DEPENDENCIA

        // Using a DependencyProperty as the backing store for Clave.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClaveProperty =
            DependencyProperty.Register("CampoClave", typeof(string), typeof(ControlAyuda), new PropertyMetadata(string.Empty));
        // Using a DependencyProperty as the backing store for Descripcion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescripcionProperty =
            DependencyProperty.Register("CampoDescripcion", typeof(string), typeof(ControlAyuda), new PropertyMetadata(string.Empty));
        // Using a DependencyProperty as the backing store for EsAyudaSimple.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EsAyudaSimpleProperty =
            DependencyProperty.Register("EsAyudaSimple", typeof(bool), typeof(ControlAyuda), new PropertyMetadata(false));
        // Using a DependencyProperty as the backing store for AnchoDescripcion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnchoDescripcionProperty =
            DependencyProperty.Register("AnchoDescripcion", typeof(int), typeof(ControlAyuda), new PropertyMetadata(160));
        public static readonly DependencyProperty AnchoClaveProperty =
            DependencyProperty.Register("AnchoClave", typeof(int), typeof(ControlAyuda), new PropertyMetadata(50));
        // Using a DependencyProperty as the backing store for EsBindeable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EsBindeableProperty =
            DependencyProperty.Register("EsBindeable", typeof(bool), typeof(ControlAyuda), new PropertyMetadata(false));

        public static readonly DependencyProperty LlaveOcultaProperty =
            DependencyProperty.Register("CampoLlaveClave", typeof (string), typeof (ControlAyuda), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty EtiquetaDescripcionProperty =
            DependencyProperty.Register("EtiquetaDescripcion", typeof(string), typeof(ControlAyuda), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty MetodoInvocacionProperty =
            DependencyProperty.Register("MetodoInvocacion", typeof(string), typeof(ControlAyuda), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SoloNumerosProperty =
            DependencyProperty.Register("AceptaSoloNumeros", typeof (bool), typeof (ControlAyuda),
                                        new PropertyMetadata(false));
        public static readonly DependencyProperty ControlIndexProperty =
            DependencyProperty.Register("ControlIndex", typeof (int), typeof (ControlAyuda),
                                        new PropertyMetadata(0));

        public static readonly DependencyProperty MetodoInvocacionBusquedaProperty =
            DependencyProperty.Register("MetodoInvocacionBusqueda", typeof (string), typeof (ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TituloBusquedaProperty =
            DependencyProperty.Register("TituloBusqueda", typeof(string), typeof(ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ConceptoBusquedaProperty =
            DependencyProperty.Register("ConceptoBusqueda", typeof(string), typeof(ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty EncabezadoClaveBusquedaProperty =
           DependencyProperty.Register("EncabezadoClaveBusqueda", typeof(string), typeof(ControlAyuda),
                                       new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty EncabezadoDescripcionBusquedaProperty =
           DependencyProperty.Register("EncabezadoDescripcionBusqueda", typeof(string), typeof(ControlAyuda),
                                       new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MensajeAgregarBusquedaProperty =
           DependencyProperty.Register("MensajeAgregarBusqueda", typeof(string), typeof(ControlAyuda),
                                       new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MensajeCerrarBusquedaProperty =
            DependencyProperty.Register("MensajeCerrarBusqueda", typeof(string), typeof(ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MensajeClaveInexistenteBusquedaProperty =
            DependencyProperty.Register("MensajeClaveInexistenteBusqueda", typeof(string), typeof(ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MaximoCaracteresProperty =
            DependencyProperty.Register("MaximoCaracteres", typeof(int), typeof(ControlAyuda),
                                        new PropertyMetadata(20));
        
        public static readonly DependencyProperty CampoDependenciaProperty =
            DependencyProperty.Register("CampoDependencia", typeof(string), typeof(ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MensajeNoPuedeBuscarProperty =
            DependencyProperty.Register("MensajeNoPuedeBuscar", typeof (string), typeof (ControlAyuda),
                                        new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MostrarBotonBusquedaProperty =
            DependencyProperty.Register("MostrarBotonBusqueda", typeof(bool), typeof(ControlAyuda),
                                        new PropertyMetadata(true));


        #endregion PROPIEDADES DEPENDENCIA

        #region METODOS

        /// <summary>
        /// Habilita los controles acorde
        /// al tipo de ayuda que se implementara
        /// </summary>
        private void HabilitarControles()
        {
            if (EsAyudaSimple)
            {
                txtClave.Visibility = Visibility.Hidden;
                txtClave.IsTabStop = false;
                colClave.Width = new GridLength(0);
                txtDescripcion.TabIndex = ControlIndex;
                txtDescripcion.MaxLength = MaximoCaracteres;
            }
            else
            {
                txtClave.TabIndex = ControlIndex;
                txtClave.MaxLength = MaximoCaracteres;
                txtDescripcion.IsReadOnly = true;
                txtDescripcion.IsTabStop = false;
            }

            if (!MostrarBotonBusqueda)
            {
                btnBusqueda.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Liga los campos con el contexto
        /// </summary>
        private void BindearCampos()
        {
            var bind = new Binding(CampoClave)
            {
                TargetNullValue = string.Empty,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Mode = BindingMode.Default
            };
            txtClave.SetBinding(TextBox.TextProperty, bind);

            bind = new Binding(CampoDescripcion)
            {
                TargetNullValue = string.Empty,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Mode = BindingMode.Default
            };
            txtDescripcion.SetBinding(TextBox.TextProperty, bind);

            if (!string.IsNullOrWhiteSpace(CampoLlaveOcultaClave))
            {
                bind = new Binding(CampoLlaveOcultaClave)
                {
                    TargetNullValue = string.Empty,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Mode = BindingMode.TwoWay
                };
                txtOculto.SetBinding(TextBox.TextProperty, bind);
            }
        }

        /// <summary>
        /// Inicializa las propiedades
        /// de los controles de ayuda
        /// </summary>
        private void InicializaControl()
        {
            txtDescripcion.Width = AnchoDescripcion;
            txtClave.Width = AnchoClave;
        }        

        /// <summary>
        /// Valida si se puede ó no Consultar
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="descripcion"></param>
        private void ValidaConsultarValores(string valor, string descripcion)
        {
            if (!string.IsNullOrWhiteSpace(valor) 
                && (string.IsNullOrWhiteSpace(descripcion) || string.Compare(descripcion, "0", StringComparison.CurrentCultureIgnoreCase) == 0)
                && string.Compare(valor, "0", StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                camposEnBlanco = false;
                if (EsBindeable)
                {
                    ConsultarValores(null);
                }
                else
                {
                    ConsultarValores(valor);
                }
                if (existenDatos)
                {
                    if (AyudaConDatos != null)
                    {
                        AyudaConDatos(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Consulta los valores de ayuda
        /// </summary>
        /// <param name="valorParametro"></param>
        private void ConsultarValores(object valorParametro)
        {
            if (ValidarSiPuedeBuscar())
            {
                try
                {
                    existenDatos = false;

                    PropertyInfo property = EsAyudaSimple
                                                ? Contexto.GetType().GetProperty(CampoDescripcion)
                                                : Contexto.GetType().GetProperty(CampoClave);
                    if (property != null)
                    {
                        var tiposParametros = new[] { Contexto.GetType() };
                        var metodo = ObjetoNegocio.GetType().GetMethod(MetodoInvocacion, tiposParametros);
                        if (metodo != null)
                        {
                            if (valorParametro == null)
                            {
                                object valorPropiedad = property.GetValue(Contexto, null);
                                property.SetValue(Contexto, valorPropiedad, null);
                            }
                            else
                            {
                                try
                                {
                                    property.SetValue(Contexto, valorParametro, null);
                                }
                                catch (Exception)
                                {
                                    property.SetValue(Contexto, Convert.ToInt32(valorParametro), null);
                                }
                            }
                            var valoresParametros = new[] { Contexto };
                            object resultadoInvocacion = metodo.Invoke(ObjetoNegocio, valoresParametros);
                            if (resultadoInvocacion != null)
                            {
                                existenDatos = true;
                                AsiganaValores(resultadoInvocacion);
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[1], MensajeClaveInexistenteBusqueda, MessageBoxButton.OK,
                                                  MessageImage.Warning);
                                LimpiarCampos();
                                if (BusquedaVacia != null)
                                {
                                    BusquedaVacia();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[1],
                                      SuKarne.Controls.Properties.Resources.Ayuda_MensajeError,
                                      MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Asigna los valores obtenidos
        /// en la consulta a los controles
        /// </summary>
        /// <param name="resultado"></param>
        private void AsiganaValores(object resultado)
        {
            if (EsBindeable)
            {
                AuxAyuda.AsignaValoresInfo(Contexto, resultado);
            }
            else
            {
                txtClave.Text = Convert.ToString(resultado.GetType().GetProperty(CampoClave).GetValue(resultado, null));
                txtDescripcion.Text =
                    Convert.ToString(resultado.GetType().GetProperty(CampoDescripcion).GetValue(resultado, null));
                if (!string.IsNullOrWhiteSpace(CampoLlaveOcultaClave))
                {
                    txtOculto.Text =
                        Convert.ToString(resultado.GetType().GetProperty(CampoLlaveOcultaClave).GetValue(resultado,
                                                                                                         null));
                }
            }
        }

        /// <summary>
        /// Valida la entrada de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidatePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = AceptaSoloNumeros
                            ? Extensor.ValidarSoloNumeros(e.Text)
                            : Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
        }

        /// <summary>
        /// Inicializa la propiedad entera
        /// </summary>
        private void LimpiaPropieadEntera()
        {
            if (Contexto != null)
            {
                PropertyInfo propiedad = Contexto.GetType().GetProperty(CampoClave);
                if (propiedad != null)
                {
                    try
                    {
                        propiedad.SetValue(Contexto, string.Empty, null);
                    }
                    catch (Exception)
                    {
                        propiedad.SetValue(Contexto, -1, null);
                        propiedad.SetValue(Contexto, 0, null);
                    }                    
                }

                if (!string.IsNullOrWhiteSpace(CampoLlaveOcultaClave))
                {
                    propiedad = Contexto.GetType().GetProperty(CampoLlaveOcultaClave);
                    if (propiedad != null)
                    {
                        propiedad.SetValue(Contexto, 0, null);
                    }
                }
                if (AyudaLimpia != null)
                {
                    AyudaLimpia(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Limpia la propiedad de descripción
        /// </summary>
        private void LimpiaPropieadDescripcion()
        {
            if (Contexto != null)
            {
                PropertyInfo propiedad = Contexto.GetType().GetProperty(CampoDescripcion);
                if (propiedad != null)
                {
                    propiedad.SetValue(Contexto, string.Empty, null);
                }
            }
        }

        /// <summary>
        /// Asigna el estilo al control de busqueda
        /// </summary>
        private void InicializaControlBusqueda()
        {
            btnBusqueda.Style = (Style)Application.Current.FindResource("BuscarIcono");
            var imgBuscar = new Image
            {
                Source = new BitmapImage(new Uri("../Imagenes/buscar.ico", UriKind.Relative)),
            };
            btnBusqueda.Content = imgBuscar;
        }

        /// <summary>
        /// Limpia los campos del control
        /// </summary>
        public void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtClave.Text = string.Empty;
            LimpiaPropieadEntera();
        }

        /// <summary>
        /// Asigna el foco al control
        /// </summary>
        public void AsignarFoco()
        {
            if (EsAyudaSimple)
            {
                txtDescripcion.Focus();
            }
            else
            {
                txtClave.Focus();
            }    
        }

        /// <summary>
        /// Llama a la pantalla de busqueda
        /// </summary>
        private void LlamarPantallaBusqueda()
        {
            if (ValidarSiPuedeBuscar())
            {
                BusquedaActivada = true;
                dynamic contextoActual = Extensor.ClonarInfo(Contexto);
                LimpiaPropieadEntera();
                LimpiaPropieadDescripcion();
                dynamic contextoBusqueda = Extensor.ClonarInfo(Contexto);
                var ayudaBusqueda = new ControlAyudaBusqueda
                                        {
                                            ConceptoBusqueda = ConceptoBusqueda,
                                            TituloBusqueda = TituloBusqueda,
                                            CampoDescripcion = CampoDescripcion,
                                            Contexto = contextoBusqueda,
                                            MetodoInvocacion = MetodoInvocacionBusqueda,
                                            ObjetoNegocio = ObjetoNegocio,
                                            CampoClave = CampoClave,
                                            EncabezadoClaveGrid = EncabezadoClaveBusqueda,
                                            EncabezadoDescripcionGrid = EncabezadoDescripcionBusqueda,
                                            MensajeAgregar = MensajeAgregarBusqueda,
                                            MensajeCerrar = MensajeCerrarBusqueda
                                        };
                ayudaBusqueda.Owner = Application.Current.Windows[1];
                ayudaBusqueda.ShowDialog();
                ventanaBusqueda = true;
                AsignaResultadoPantallaBusqueda(ayudaBusqueda.Contexto, contextoActual, ayudaBusqueda.Cancelado);
                ventanaBusqueda = false;
            }
            BusquedaActivada = false;
        }

        /// <summary>
        /// Asiga el resultado de la busqueda al Contexto
        /// de la ayuda
        /// </summary>
        /// <param name="contextoBusqueda"></param>
        /// <param name="contextoActual"></param>
        /// <param name="cancelado"></param>
        private void AsignaResultadoPantallaBusqueda(dynamic contextoBusqueda, dynamic contextoActual, bool cancelado)
        {
            if (cancelado)
            {
                if (EsBindeable)
                {
                    AuxAyuda.AsignaValoresInfo(Contexto, contextoActual);
                    VerificaLimpiarCampos(Contexto);
                }
            }
            else
            {
                AuxAyuda.AsignaValoresInfo(Contexto, contextoBusqueda);
                if (!EsBindeable)
                {
                    AsignarValoresBusqueda(contextoBusqueda);
                }
                AsignarFoco();
                var keyboardFocus = Keyboard.FocusedElement as UIElement;
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                if (AyudaConDatos != null)
                {
                    BusquedaActivada = false;
                    AyudaConDatos(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Verifica si se limpiaran los campos ó no
        /// </summary>
        /// <param name="contexto"></param>
        private void VerificaLimpiarCampos(object contexto)
        {
            PropertyInfo property;
            if (EsAyudaSimple)
            {
                property = contexto.GetType().GetProperty(CampoClave);
            }
            else
            {
                property = contexto.GetType().GetProperty(CampoDescripcion);
            }
            if (property != null)
            {
                string valor = Convert.ToString(property.GetValue(contexto, null));
                if (string.IsNullOrWhiteSpace(valor)
                    || string.Compare(valor, "0", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    LimpiarCampos();
                }
            }
        }

        /// <summary>
        /// Asigna los valores de busqueda a los controles
        /// en caso de que no este ligada la ayuda al contexto
        /// </summary>
        /// <param name="contextoBusqueda"></param>
        private void AsignarValoresBusqueda(object contextoBusqueda)
        {
            txtClave.Text =
                Convert.ToString(contextoBusqueda.GetType().GetProperty(CampoClave).GetValue(contextoBusqueda, null));
            txtDescripcion.Text =
                Convert.ToString(contextoBusqueda.GetType().GetProperty(CampoDescripcion).GetValue(contextoBusqueda,
                                                                                                   null));
            if (!string.IsNullOrWhiteSpace(CampoLlaveOcultaClave))
            {
                txtOculto.Text =
                    Convert.ToString(
                        contextoBusqueda.GetType().GetProperty(CampoLlaveOcultaClave).GetValue(contextoBusqueda, null));
            }
        }

        /// <summary>
        /// Ejecuta las validaciones para ver si puede realizar la busqueda de elementos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarSiPuedeBuscar()
        {
            if (PuedeBuscar != null)
            {
                if (!PuedeBuscar())
                {
                    if (!string.IsNullOrWhiteSpace(MensajeNoPuedeBuscar))
                    {
                        SkMessageBox.Show(Application.Current.Windows[1], MensajeNoPuedeBuscar, MessageBoxButton.OK,
                                  MessageImage.Warning);
                    }
                    txtOculto.Clear();
                    txtClave.Clear();
                    txtDescripcion.Clear();
                    return false;
                }
            }
            return true;
        }

        #endregion METODOS
        
        #region EVENTOS

        /// <summary>
        /// Se ejecuta al cargarse el control de ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            InicializaControl();
            HabilitarControles();
            if (EsBindeable)
            {
                BindearCampos();
            }
            InicializaControlBusqueda();
        }

        /// <summary>
        /// Se dispara cuando se obtiene el foco del control, realiza la seleccion del texto existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClaveGotFocus(object sender, RoutedEventArgs e)
        {
            txtClave.SelectAll();
        }

        /// <summary>
        /// Se ejecuta cuando el campo de clave pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClaveLostFocus(object sender, RoutedEventArgs e)
        { 
            ValidaConsultarValores(txtClave.Text, txtDescripcion.Text);
            if (string.IsNullOrWhiteSpace(txtClave.Text) && EsBindeable)
            {
                LimpiaPropieadEntera();
            }
        }

        /// <summary>
        /// Se ejecuta cuando el campo clave se le da de entrada
        /// un Enter, Tab, F1, BackSpace ó Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClaveKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Tab:
                    ClaveLostFocus(sender, new RoutedEventArgs());
                    if (!existenDatos && !camposEnBlanco)
                    {
                        AsignarFoco();
                        e.Handled = true;
                    }
                    break;
                case Key.F1:
                    LlamarPantallaBusqueda();
                    break;
                case Key.Delete:
                case Key.Back:
                    txtDescripcion.Text = string.Empty;
                    break;
            }
            camposEnBlanco = true;
            existenDatos = false;
        }

        /// <summary>
        /// Se ejecuta cuando cambia el texto del campo Clave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClaveTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsEnabled)
            {
                if (!ventanaBusqueda)
                {
                    if (!existenDatos)
                    {
                        existenDatos = false;
                        if (txtDescripcion.IsReadOnly && txtDescripcion.Text.Trim().Length > 0)
                        {
                            txtDescripcion.Text = string.Empty;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Se ejecuta cuando el campo Descripción pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescripcionLostFocus(object sender, RoutedEventArgs e)
        {
            ValidaConsultarValores(txtDescripcion.Text, txtClave.Text);
        }

        /// <summary>
        /// Se ejecuta cuando el campo Descripcion se le da de entrada
        /// un Enter, Tab, F1, BackSpace ó Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescripcionKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                ValidaConsultarValores(txtDescripcion.Text, txtClave.Text);
                if (!existenDatos && !camposEnBlanco)
                {
                    AsignarFoco();
                    e.Handled = true;
                }
            }
            else
            {
                if (e.Key == Key.F1)
                {
                    LlamarPantallaBusqueda();
                }
                else
                {
                    if (e.Key == Key.Delete || e.Key == Key.Back)
                    {
                        txtClave.Text = string.Empty;
                    }
                }
            }
            camposEnBlanco = true;
            existenDatos = false;
        }

        /// <summary>
        /// Se ejecuta cuando el texto del campo Descripción cambia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescripcionTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsEnabled)
            {
                if (!ventanaBusqueda)
                {
                    if (!existenDatos)
                    {
                        existenDatos = false;
                        if (!txtDescripcion.IsReadOnly)
                        {
                            txtClave.Text = string.Empty;
                            if (EsBindeable)
                            {
                                LimpiaPropieadEntera();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Se ejecuta al presionar el boton de busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BusquedaClick(object sender, RoutedEventArgs e)
        {
            LlamarPantallaBusqueda();
        }

        #endregion EVENTOS

        public void Dispose()
        {
            IDisposable dispose = ObjetoNegocio as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
