using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para TratamientoEdicion.xaml
    /// </summary>
    public partial class TratamientoEdicion
    {
        #region CONSTRUCTORES
        /// <summary>
        /// Constructor por default
        /// </summary>
        public TratamientoEdicion()
        {
            try
            {
                InitializeComponent();
                InicializaContexto();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        public TratamientoEdicion(TratamientoInfo tratamiento)
        {
            try
            {
                TratamientoInfo = tratamiento;
                if (TratamientoInfo.ListaTratamientoProducto != null && TratamientoInfo.ListaTratamientoProducto.Any())
                {
                     int ordenSecuencia = 0;
                    TratamientoInfo.ListaTratamientoProducto.ForEach(productos =>
                        {
                            ordenSecuencia = ordenSecuencia + 1;
                            productos.Orden = ordenSecuencia;
                        });
                }
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Propiedad para manejar el resultado de los productos
        /// </summary>
        private ResultadoInfo<TratamientoProductoInfo> resultadoInfo;

        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private TratamientoInfo TratamientoInfo
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TratamientoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        #endregion PROPIEDADES

        #region VARIABLES
        /// <summary>
        /// Vairiable para manejar el Usuario Logueado
        /// </summary>
        private int usuarioLogueadoID;

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        #endregion VARIABLES

        #region EVENTOS
        /// <summary>
        /// Evento que se ejecuta cuando inicia la pantalla
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AgregarAyudaOrganizacion();
                CargarComboTipoTratamiento();
                CargarComboTipoOrganizacion();
                ucPaginacionProducto.DatosDelegado += CargarGridProductos;
                ucPaginacionProducto.AsignarValoresIniciales();
                CargarGridProductos(ucPaginacionProducto.Inicio, ucPaginacionProducto.Limite);
                usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
                cboTipoOrganizacion.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Guardar
        /// </summary>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Cancelar
        /// </summary>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Nuevo Producto
        /// </summary>
        private void BotonNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var producto = new TratamientoProductoInfo
                {
                    Producto = new ProductoInfo
                        {
                            SubFamilia = new SubFamiliaInfo(),
                            Familia = new FamiliaInfo()
                        },
                    Tratamiento = new TratamientoInfo(),
                    HabilitaEdicion = true
                };
                var tratamientoEdicionProducto =
                    new TratamientoEdicionProducto(producto)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.TratamientoEdicionProducto_Nuevo }
                    };
                MostrarCentrado(tratamientoEdicionProducto);
                if (tratamientoEdicionProducto.ConfirmaSalir)
                {
                    return;
                }
                if (producto.Producto.ProductoId != 0 && producto.Dosis != 0)
                {
                    producto.Tratamiento.TratamientoID = TratamientoInfo.TratamientoID;
                    var productoRepetido =
                        TratamientoInfo.ListaTratamientoProducto.FirstOrDefault(
                            pro => pro.Producto.ProductoId == producto.Producto.ProductoId);
                    if (productoRepetido != null)
                    {
                        SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ProductoRepetido, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    int ordenMaximo = 0;
                    if (TratamientoInfo.ListaTratamientoProducto.Any())
                    {
                        ordenMaximo = TratamientoInfo.ListaTratamientoProducto.Max(prod => prod.Orden);
                    }
                    producto.Orden = ordenMaximo + 1;
                    TratamientoInfo.ListaTratamientoProducto.Add(producto);
                    resultadoInfo.Lista.Add(producto);

                    gridDatosProducto.ItemsSource = null;
                    gridDatosProducto.ItemsSource = resultadoInfo.Lista;
                    resultadoInfo.TotalRegistros = TratamientoInfo.ListaTratamientoProducto.Count;
                    ucPaginacionProducto.TotalRegistros = resultadoInfo.TotalRegistros;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Editar Producto
        /// </summary>
        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var tratamientoProductoEditar = (TratamientoProductoInfo)btn.CommandParameter;
                var tratamientoProductoOriginal = tratamientoProductoEditar.Clone();
                var tratamientoEdicionProducto =
                    new TratamientoEdicionProducto(tratamientoProductoEditar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.TratamientoEdicionProducto_Edicion }
                    };
                MostrarCentrado(tratamientoEdicionProducto);
                if (tratamientoEdicionProducto.ConfirmaSalir)
                {
                    var tratamientoModificado =
                        TratamientoInfo.ListaTratamientoProducto.FirstOrDefault(
                            pro => pro.TratamientoProductoID == tratamientoProductoOriginal.TratamientoProductoID);
                    if (tratamientoModificado == null)
                    {
                        return;
                    }
                    tratamientoModificado.Dosis = tratamientoProductoOriginal.Dosis;
                    tratamientoModificado.Activo = tratamientoProductoOriginal.Activo;
                    gridDatosProducto.ItemsSource = null;
                    gridDatosProducto.ItemsSource = resultadoInfo.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamiendoEdicion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando te captura una tecla en los TextBox para manejar solamente números
        /// </summary>
        private void TextBox_ValidarSoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento que se ejecuta cuando te captura una tecla en la pantalla
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia el valor de Tipo de Organización
        /// </summary>
        private void CboTipoOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion, cboTipoOrganizacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
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
                    TratamientoInfo = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion EVENTOS

        #region METODOS

        private void Guardar()
        {
            if (!ValidarGuardar())
            {
                return;
            }
            bool nuevo = false;
            if (TratamientoInfo.TratamientoID == 0)
            {
                TratamientoInfo.UsuarioCreacionID = usuarioLogueadoID;
                nuevo = true;
            }
            else
            {
                TratamientoInfo.UsuarioModificacionID = usuarioLogueadoID;
            }
            TratamientoInfo.ListaTratamientoProducto.ForEach(prod =>
            {
                if (prod.TratamientoProductoID == 0)
                {
                    prod.UsuarioCreacionID = usuarioLogueadoID;
                }
                else
                {
                    prod.UsuarioModificacionID = usuarioLogueadoID;
                }
            });
            var tratamientoPL = new TratamientoPL();
            tratamientoPL.Guardar(TratamientoInfo);
            confirmaSalir = false;
            SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_GuardadoExitoso, MessageBoxButton.OK, MessageImage.Correct);
            if (!nuevo)
            {
                Close();
            }
            InicializaContexto();
            ucPaginacionProducto.AsignarValoresIniciales();
            CargarGridProductos(ucPaginacionProducto.Inicio, ucPaginacionProducto.Limite);
        }

        /// <summary>
        /// Metodo que se utiliza para validar que los campos requeridos hayan sido capturados
        /// </summary>
        private bool ValidarGuardar()
        {
            if (TratamientoInfo.Organizacion.TipoOrganizacion == null || TratamientoInfo.Organizacion.TipoOrganizacion.TipoOrganizacionID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_CampoTipoOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (TratamientoInfo.Organizacion.OrganizacionID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_CampoOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (TratamientoInfo.TipoTratamientoInfo.TipoTratamientoID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratemientoEdicion_CampoTipoTratamiento, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (TratamientoInfo.CodigoTratamiento == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratemientoEdicion_CampoCodigoTratamiento, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (TratamientoInfo.RangoInicial == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_CampoRangoInicial, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (TratamientoInfo.RangoFinal == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_CampoRangoFinal, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if(TratamientoInfo.RangoInicial > TratamientoInfo.RangoFinal)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_RangosValidos, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }            
            if (TratamientoInfo.TratamientoID == 0)
            {
                var tratamientoPL = new TratamientoPL();
                if (tratamientoPL.ValidarExisteTratamiento(TratamientoInfo))
                {
                    SkMessageBox.Show(this, Properties.Resources.TratamientoEdicion_TratamientoRepetido,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            TratamientoInfo = new TratamientoInfo
            {
                Organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    },
                TipoTratamientoInfo = new TipoTratamientoInfo(),
                ListaTratamientoProducto = new List<TratamientoProductoInfo>(),
                Sexo = Sexo.Hembra
            };
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboTipoTratamiento()
        {
            var tipoTratamientoPL = new TipoTratamientoPL();
            IEnumerable<TipoTratamientoInfo> tiposTratamientos = tipoTratamientoPL.ObtenerTodos(EstatusEnum.Activo);
            var listaOrdenada = tiposTratamientos.OrderBy(tipo => tipo.Descripcion).ToList();
            cboTipoTratamiento.ItemsSource = listaOrdenada;
            if(TratamientoInfo.TipoTratamientoInfo.TipoTratamientoID == 0)
            {
                cboTipoTratamiento.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Metodo que carga la información de los productos
        /// </summary>
        private void CargarGridProductos(int inicio, int limite)
        {
            resultadoInfo = new ResultadoInfo<TratamientoProductoInfo>
                {
                    TotalRegistros = TratamientoInfo.ListaTratamientoProducto.Count,
                    Lista = TratamientoInfo.ListaTratamientoProducto.Where(prod => prod.Orden >= inicio && prod.Orden <= limite).ToList()
                };
            gridDatosProducto.ItemsSource = null;
            if (resultadoInfo != null && resultadoInfo.Lista != null &&
                     resultadoInfo.Lista.Count > 0)
            {
                gridDatosProducto.ItemsSource = resultadoInfo.Lista;
                ucPaginacionProducto.TotalRegistros = resultadoInfo.TotalRegistros;
            }
            else
            {
                ucPaginacionProducto.TotalRegistros = 0;
                ucPaginacionProducto.AsignarValoresIniciales();
                resultadoInfo = new ResultadoInfo<TratamientoProductoInfo>
                    {
                        TotalRegistros = 0,
                        Lista = new List<TratamientoProductoInfo>()
                    };
                gridDatosProducto.ItemsSource = new List<TratamientoInfo>();
            }

        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {

            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, TratamientoInfo.Organizacion
                                                                , "PropiedadClaveCatalogoTratamiento"
                                                                , "PropiedadDescripcionCatalogoTratamiento"
                                                                , true, true) {AyudaPL = new OrganizacionPL()};

            stpAyudaOrganizacion.Children.Clear();
            stpAyudaOrganizacion.Children.Add(skAyudaOrganizacion);

            skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrganizacion_Titulo;

            skAyudaOrganizacion.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("TipoOrganizacionID",
                                    Properties.Resources.RegistroProgramacionEmbarques_SeleccionarTipoOrganizacion);
            skAyudaOrganizacion.MensajeDependencias = mensajeDependencias;
            skAyudaOrganizacion.AsignaTabIndex(1);

            AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion, cboTipoOrganizacion);
        }

        /// <summary>
        /// Metodo para agregar las dependencias a las ayudas de Organización Origen y Destino
        /// </summary>
        private void AsignaDependenciasAyudaOrganizacion(SKAyuda<OrganizacionInfo> controlAyuda, ComboBox combo)
        {
            controlAyuda.Dependencias = null;

            IList<IDictionary<IList<String>, Object>> dependencias = new List<IDictionary<IList<String>, Object>>();
            IDictionary<IList<String>, Object> dependecia = new Dictionary<IList<String>, Object>();

            var dependenciasGanado = new EntradaGanadoInfo();
            IList<String> camposDependientes = new List<String>();
            camposDependientes.Add("EmbarqueID");
            dependecia.Add(camposDependientes, dependenciasGanado);
            dependencias.Add(dependecia);

            dependecia = new Dictionary<IList<String>, Object>();
            camposDependientes = new List<String> { "TipoOrganizacionID" };
            dependecia.Add(camposDependientes, combo.SelectedItem);
            dependencias.Add(dependecia);

            controlAyuda.Dependencias = dependencias;
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboTipoOrganizacion()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            IEnumerable<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
            var listaOrdenada = tiposOrganizacion.OrderBy(tipo => tipo.Descripcion).ToList();
            cboTipoOrganizacion.ItemsSource = listaOrdenada;
            if (TratamientoInfo.Organizacion.OrganizacionID == 0)
            {
                cboTipoOrganizacion.SelectedIndex = 0;
            }
        }
        #endregion METODOS
    }
}
