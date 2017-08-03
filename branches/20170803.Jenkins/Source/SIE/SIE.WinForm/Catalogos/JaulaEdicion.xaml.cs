using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
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
    /// Interaction logic for JaulaEdicion.xaml
    /// </summary>
    public partial class JaulaEdicion
    {
        #region Propiedades

        private JaulaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (JaulaInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<ProveedorInfo> skAyudaProveedor;

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Se utiliza para indentificar si existen marcas y
        /// no abrir la ventana de edicion al no existir
        /// </summary>
        public bool existenMarcas = false;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public JaulaEdicion(ProveedorInfo proveedor, MarcasInfo marca = null)
        {
            InitializeComponent();
            CargarAyudas();
            CargarComboMarcas();
            Contexto.Proveedor = proveedor;
            Contexto.Marca = marca;
        }

        /// <summary>
        /// Constructor para editar una entidad Jaula Existente
        /// </summary>
        /// <param name="jaulaInfo"></param>
        public JaulaEdicion(JaulaInfo jaulaInfo)
        {            
            InitializeComponent();
            jaulaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            CargarAyudas();
            CargarComboMarcas(jaulaInfo);
            Contexto = jaulaInfo;

            if (Contexto.Boletinado)
            {                
                cmbEstatus.IsEnabled = false;
                txtObservacionesRegistro.IsEnabled = true;
                lblObservacionesRequerido.Visibility = Visibility.Visible;
            }

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
            txtObservacionesRegistro.Clear();
            cmbMarca.SelectedIndex = 0;
            if (string.IsNullOrWhiteSpace(cmbMarca.SelectionBoxItem.ToString()))
            {
                confirmaSalir = false;
                Close();
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
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar la entrada de solo números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtDescripcionPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        /// <summary>
        /// Evento para controlar la propiedad Click del checkbox de observaciones
        /// </summary>
        /// <param name="sender">Objeto que implementa el método</param>
        /// <param name="e">Argumentos asociados</param>
        private void Boletinado_OnClick(object sender, RoutedEventArgs e)
        {
            if (chkBoletinado.IsChecked == true)
            {
                cmbEstatus.SelectedValue = EstatusEnum.Inactivo;
                cmbEstatus.IsEnabled = false;
                txtObservacionesRegistro.IsEnabled = true;
                txtObservacionesRegistro.Visibility = Visibility.Visible;
                lblObservacionesRequerido.Visibility = Visibility.Visible;
                txtObservacionesRegistro.Focus();
            }
            else if (chkBoletinado.IsChecked == false)
            {
                lblObservacionesRequerido.Visibility = Visibility.Hidden;
                cmbEstatus.SelectedValue = EstatusEnum.Activo;
                cmbEstatus.IsEnabled = true;
                txtObservacionesRegistro.IsEnabled = false;
                txtObservacionesRegistro.Text = string.Empty;
                txtObservacionesRegistro.Visibility = Visibility.Hidden;
                Contexto.Observaciones = string.Empty;        
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            ProveedorInfo proveedorInfo = (skAyudaProveedor != null && !skAyudaProveedor.IsEnabled) ? Contexto.Proveedor : new ProveedorInfo();            
            MarcasInfo marcasInfo = new MarcasInfo();

            Contexto = new JaulaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Proveedor = proveedorInfo,
                Marca = marcasInfo
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
                int numEconomico = 0;
                int modelo = 0;
                bool modeloEsNum = int.TryParse(txtModelo.Text, out modelo);
                bool numEconomicoEsNum = int.TryParse(txtNumEconomico.Text, out numEconomico);
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtNumEconomico.Text) || (numEconomicoEsNum && numEconomico == 0))
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgNumEconomicoRequerida;
                    txtNumEconomico.Focus();          
                }
                else if (Contexto.Marca == null || Contexto.Marca.MarcaId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgMarcaRequerida;
                    cmbMarca.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtModelo.Text) || (modeloEsNum && modelo == 0))
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgModeloRequerida;
                    txtModelo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(skAyudaProveedor.Clave) ||
                 string.IsNullOrWhiteSpace(skAyudaProveedor.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgProveedorNoValido;
                    skAyudaProveedor.AsignarFoco();
                }
                else if (chkBoletinado.IsChecked == true &&
                    (string.IsNullOrWhiteSpace(txtObservacionesRegistro.Text) && string.IsNullOrWhiteSpace(txtObservacionesConsulta.Text)))
                {
                    resultado = false;
                    mensaje = Properties.Resources.JaulaEdicion_MsgObservacionesRequeridas;
                    txtObservacionesRegistro.Focus();
                }                 
                else
                {
                    int jaulaId = Extensor.ValorEntero(txtJaulaId.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var jaulaPL = new JaulaPL();
                    JaulaInfo jaula = jaulaPL.ObtenerPorDescripcion(descripcion);

                    if (jaula != null && (jaulaId == 0 || jaulaId != jaula.JaulaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.JaulaEdicion_MsgDescripcionExistente,
                                                jaula.JaulaID);
                        txtDescripcion.Focus();
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
                    var jaulaPL = new JaulaPL();
                    jaulaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.JaulaID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                        cmbMarca.SelectedIndex = 0;
                    }                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Jaula_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Jaula_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaProveedor();
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Transportista
        /// </summary>
        private void AgregarAyudaProveedor()
        {
            skAyudaProveedor =
                new SKAyuda<ProveedorInfo>(240, false, Contexto.Proveedor
                                           , "PropiedadClaveCatalogo"
                                           , "PropiedadDescripcionCatalogo"
                                           , "PropiedadOcultaCatalogo"
                                           , true, 80, 10, true)
                    {
                        AyudaPL = new ProveedorPL(),
                        Info = new ProveedorInfo {Activo = EstatusEnum.Activo},
                        MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.Proveedor_SalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                        MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                        TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                        TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
                    };
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedor);
            skAyudaProveedor.AsignaTabIndex(4);
        }

        /// <summary>
        /// Método para deshabilitar el campo proveedor
        /// </summary>
        public void BloqueaProveedor(bool bloquear)
        {
            skAyudaProveedor.IsEnabled = bloquear;
        }


        /// <summary>
        /// Método para cargar el combo tipos de marcas
        /// Recibe una parametro jaula info opcional
        /// En caso de que jaula info sea null se mostrará
        /// La opción seleccione
        /// </summary>
        private void CargarComboMarcas(JaulaInfo jaulaInfo = null)
        {
            try
            {
                var marcasPL = new MarcasPL();
                var marcasInfo = new MarcasInfo();
                IList<MarcasInfo> listaTiposMarcas = marcasPL.ObtenerMarcas(EstatusEnum.Inactivo, EstatusEnum.Activo);
                if (listaTiposMarcas != null)
                {
                    this.existenMarcas = true;

                    if (jaulaInfo == null)
                    {
                        marcasInfo = new MarcasInfo
                        {
                            MarcaId = 0,
                            Descripcion = Properties.Resources.JaulaEdicion_SeleccioneMarca
                        };
                    }
                    else
                    {
                        marcasInfo = new MarcasInfo
                        {
                            MarcaId = jaulaInfo.Marca == null ? 0 : jaulaInfo.Marca.MarcaId,
                            Descripcion = string.IsNullOrEmpty(jaulaInfo.Marca.Descripcion) ? Properties.Resources.JaulaEdicion_SeleccioneMarca : jaulaInfo.Marca.Descripcion
                        };

                        // Habilita campo de observaciones en modo de edición
                        // en caso de que la jaula tenga la opcion boletinado
                        if (jaulaInfo.Boletinado)
                        {
                            txtObservacionesRegistro.IsEnabled = true;
                            lblObservacionesRequerido.Visibility = Visibility.Visible;
                        }
                    }
                    listaTiposMarcas.Insert(0, marcasInfo);
                    cmbMarca.ItemsSource = listaTiposMarcas;
                    cmbMarca.SelectedItem = marcasInfo;
                    if (Contexto.Marca == null)
                    {
                        Contexto.Marca = marcasInfo;
                    }
                    else
                    {
                        Contexto.Marca.MarcaId = marcasInfo.MarcaId;
                        Contexto.Marca.Descripcion = marcasInfo.Descripcion;
                    }
                    
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.JaulaEdicion_MsgMarcasActivas, MessageBoxButton.OK, MessageImage.Error);
                    this.existenMarcas = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.JaulaEdicion_ErrorObtenerMarcas, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Métodos
    }

}

