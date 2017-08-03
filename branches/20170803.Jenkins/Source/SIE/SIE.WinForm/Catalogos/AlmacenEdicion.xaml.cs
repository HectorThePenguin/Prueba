using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for AlmacenEdicion.xaml
    /// </summary>
    public partial class AlmacenEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private AlmacenInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AlmacenInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Control para la ayuda de organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        #endregion Propiedades

        #region Variables

        private List<TipoAlmacenInfo> listaTiposAlmacenProveedor = new List<TipoAlmacenInfo>();

        private bool validaProveedor = false;

        #endregion Variables

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlmacenEdicion()
        {
            InitializeComponent();
            CargarParametrosTiposAlmacenProveedor();
            InicializaContexto();
            CargarAyudas();
            CargaTiposAlmacen();
        }

        /// <summary>
        /// Constructor para editar una entidad Almacen Existente
        /// </summary>
        /// <param name="almacenInfo"></param>
        public AlmacenEdicion(AlmacenInfo almacenInfo)
        {
            InitializeComponent();
            CargarParametrosTiposAlmacenProveedor();
            almacenInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = almacenInfo;
            CargarAyudas();
            CargaTiposAlmacen();
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
            skAyudaProveedor.ObjetoNegocio = new ProveedorPL();
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
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarSoloLetrasYNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void CargarParametrosTiposAlmacenProveedor()
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            string claveParametro = ParametrosEnum.TiposAlmacenProveedor.ToString();
            ParametroGeneralInfo parametro = parametroGeneralBL.ObtenerPorClaveParametro(claveParametro);
            if (parametro == null)
            {
                return;
            }
            if (parametro.Valor.Contains('|'))
            {
                listaTiposAlmacenProveedor = (from tipos in parametro.Valor.Split('|')
                                              select new TipoAlmacenInfo
                                                  {
                                                      TipoAlmacenID = Convert.ToInt32(tipos)
                                                  }).ToList();
            }
            else
            {
                var tipoAlmacen = new TipoAlmacenInfo
                    {
                        TipoAlmacenID = Convert.ToInt32(parametro.Valor)
                    };
                listaTiposAlmacenProveedor.Add(tipoAlmacen);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo(),
                        TipoAlmacen = new TipoAlmacenInfo(),
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        Proveedor = new ProveedorInfo()
                    };
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
                if (string.IsNullOrWhiteSpace(txtAlmacenID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgAlmacenIDRequerida;
                    txtAlmacenID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.CodigoAlmacen))
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgCodigoAlmacenRequerida;
                    txtCodigoAlmacen.Focus();
                }
                else if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }

                else if (Contexto.TipoAlmacen.TipoAlmacenID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgTipoAlmacenIDRequerida;
                    cmbTipoAlmacen.Focus();
                }
                else if (validaProveedor && (Contexto.Proveedor == null || Contexto.Proveedor.ProveedorID == 0))
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgProveedorRequerido;
                    skAyudaProveedor.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else if (validaProveedor)
                {
                    if (Contexto.AlmacenID == 0 && Contexto.Proveedor != null && Contexto.Proveedor.ProveedorID > 0)
                    {
                        var proveedorAlmacenPL = new ProveedorAlmacenPL();
                        var proveedorAlmacenInfo = new ProveedorAlmacenInfo
                        {
                            Proveedor = Contexto.Proveedor,
                            Almacen = new AlmacenInfo
                            {
                                TipoAlmacenID = Contexto.TipoAlmacen.TipoAlmacenID
                            },
                            Activo = EstatusEnum.Activo
                        };
                        ProveedorAlmacenInfo proveedorAlmacen =
                            proveedorAlmacenPL.ObtenerPorProveedorTipoAlmacen(proveedorAlmacenInfo);
                        if (proveedorAlmacen != null)
                        {
                            resultado = false;
                            mensaje = Properties.Resources.AlmacenEdicion_MsgProveedorAlmacenExistente;
                            skAyudaProveedor.AsignarFoco();
                        }
                    }
                }
                else
                {
                    int almacenId = Extensor.ValorEntero(txtAlmacenID.Text);
                    string descripcion = txtDescripcion.Text;

                    var almacenPL = new AlmacenPL();
                    AlmacenInfo almacen = almacenPL.ObtenerPorDescripcion(descripcion);

                    if (almacen == null)
                    {
                        return true;
                    }

                    if (almacen.TipoAlmacen.TipoAlmacenID != Contexto.TipoAlmacen.TipoAlmacenID && almacen.Organizacion.OrganizacionID == Contexto.Organizacion.OrganizacionID)
                    {
                        bool tieneProductos = almacenPL.ValidarProductosEnAlmacen(almacen);
                        if (tieneProductos)
                        {
                            mensaje = Properties.Resources.AlmacenEdicion_MsgTipoAlmacenCambio;
                            resultado = false;
                        }
                    }

                    if ((almacenId == 0 || almacenId != almacen.AlmacenID) && almacen.Organizacion.OrganizacionID == Contexto.Organizacion.OrganizacionID)
                    {
                        resultado = false;
                        mensaje = string.Format(
                            Properties.Resources.AlmacenEdicion_MsgDescripcionExistente, almacen.AlmacenID);
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
                    var almacenPL = new AlmacenPL();
                    almacenPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.AlmacenID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Almacen_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Almacen_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Carga tipos de almacén
        /// </summary>
        private void CargaTiposAlmacen()
        {
            var tipoAlmacenPL = new TipoAlmacenPL();
            var tipoAlmacen = new TipoAlmacenInfo
            {
                TipoAlmacenID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoAlmacenInfo> listaTipoAlmacen = tipoAlmacenPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoAlmacen.Insert(0, tipoAlmacen);
            cmbTipoAlmacen.ItemsSource = listaTipoAlmacen;
            cmbTipoAlmacen.SelectedItem = tipoAlmacen;
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, Contexto.Organizacion
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadDescripcionCatalogoAyuda", true, 50, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(4);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }
        #endregion Métodos

        private void CmbTipoAlmacen_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoAlmacen.SelectedItem == null)
            {
                return;
            }
            var tipoAlmacen = (TipoAlmacenInfo)cmbTipoAlmacen.SelectedItem;
            if (listaTiposAlmacenProveedor.Any(tipo => tipo.TipoAlmacenID == tipoAlmacen.TipoAlmacenID))
            {
                skAyudaProveedor.IsEnabled = true;
                validaProveedor = true;
                lblProveedorRequerido.Visibility = Visibility.Visible;
            }
            else
            {
                skAyudaProveedor.IsEnabled = false;
                validaProveedor = false;
                lblProveedorRequerido.Visibility = Visibility.Hidden;
            }
        }
    }
}

