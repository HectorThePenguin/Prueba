using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CamionEdicion.xaml
    /// </summary>
    public partial class CamionEdicion
    {
        #region Propiedades

        private CamionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CamionInfo) DataContext;
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

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CamionEdicion(ProveedorInfo proveedor)
        {
            InitializeComponent();
            Contexto.Proveedor = proveedor;
            CargarComboMarcasSinAsignar();
            CargarAyudas();
            HabilitarComboObservaciones();
        }

        /// <summary>
        /// Constructor para editar una entidad Camion Existente
        /// </summary>
        /// <param name="camionInfo"></param>
        public CamionEdicion(CamionInfo camionInfo)
        {
            InitializeComponent();
            CargarAyudas();
            camionInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            CargarComboMarcas(camionInfo);
            Contexto = camionInfo;
            if (camionInfo.MarcaID == null && camionInfo.MarcaDescripcion == string.Empty)
            {
                Contexto.MarcaID = 0;
                Contexto.MarcaDescripcion = Properties.Resources.Camion_MarcaSeleccionar;   
            }
            HabilitarComboObservaciones();
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


        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                txtObservacionesEnviar.IsEnabled = Contexto.Boletinado;
                if (Contexto.Boletinado)
                {
                    cmbEstatus.SelectedItem = EstatusEnum.Inactivo;
                    Contexto.Activo = EstatusEnum.Inactivo;
                    cmbEstatus.IsEnabled = false;
                    NoVacio.Visibility = Visibility.Visible;
                    txtObservacionesEnviar.Visibility = Visibility.Visible;
                    txtObservacionesEnviar.Focus();
                }
                else
                {
                    cmbEstatus.SelectedItem = EstatusEnum.Activo;
                    Contexto.Activo = EstatusEnum.Activo;
                    cmbEstatus.IsEnabled = true;
                    NoVacio.Visibility = Visibility.Hidden;
                    txtObservacionesEnviar.Text = string.Empty;
                    txtObservacionesEnviar.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Condicion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarLetrasConAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar numeros
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetras(e.Text);
        }
        #endregion Eventos

        #region Métodos

        private void HabilitarComboObservaciones()
        {
            txtObservacionesEnviar.IsEnabled = Contexto.Boletinado;
            if (Contexto.Boletinado)
            {
                cmbEstatus.IsEnabled = false;
                NoVacio.Visibility = Visibility.Visible;
            }
            else
            {
                cmbEstatus.IsEnabled = true;
                NoVacio.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarComboMarcasSinAsignar()
        {
            try
            {
                var tipoMarcasPL = new MarcasPL();
                IList<MarcasInfo> listaMarcas = tipoMarcasPL.ObtenerMarcas(EstatusEnum.Activo, EstatusEnum.Activo);
                if (listaMarcas != null)
                {
                    var tipoMarca = new MarcasInfo
                    {
                        MarcaId = 0,
                        Descripcion = Properties.Resources.Camion_MarcaSeleccionar
                    };
                    listaMarcas.Insert(0, tipoMarca);
                    cmbMarca.ItemsSource = listaMarcas;
                    cmbMarca.SelectedItem = tipoMarca;
                    Contexto.MarcaID = tipoMarca.MarcaId;
                    Contexto.MarcaDescripcion = tipoMarca.Descripcion;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Camion_MarcasVacias, MessageBoxButton.OK, MessageImage.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Camion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarComboMarcas(CamionInfo camionInfo)
        {
            try{
                var tipoMarcasPL = new MarcasPL();
                IList<MarcasInfo> listaMarcas = tipoMarcasPL.ObtenerMarcas(EstatusEnum.Activo, EstatusEnum.Activo);
                if (listaMarcas != null)
                {
                    var tipoMarca = new MarcasInfo
                    {
                        MarcaId = (camionInfo.MarcaID == null) ? 0 : (int) camionInfo.MarcaID,
                        Descripcion = camionInfo.MarcaDescripcion,
                    };
                    if (tipoMarca.MarcaId == 0)
                    {
                        tipoMarca.Descripcion = Properties.Resources.Camion_MarcaSeleccionar;
                        listaMarcas.Insert(0, tipoMarca);
                    }

                    cmbMarca.ItemsSource = listaMarcas;
                    cmbMarca.SelectedItem = tipoMarca;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Camion_MarcasVacias, MessageBoxButton.OK, MessageImage.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Camion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            ProveedorInfo proveedorInfo = (skAyudaProveedor != null && !skAyudaProveedor.IsEnabled) ? Contexto.Proveedor : new ProveedorInfo();            
            Contexto = new CamionInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Proveedor = proveedorInfo
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
                
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtEconomico.Text) || txtEconomico.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgEconomicoNoValido;
                    txtEconomico.Focus();
                }
                else if (Contexto.MarcaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgMarcaNoValido;
                    cmbMarca.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtModelo.Text) || txtModelo.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgModeloNoValido;
                    txtModelo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(skAyudaProveedor.Clave) || string.IsNullOrWhiteSpace(skAyudaProveedor.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgProveedorNoValido;
                    skAyudaProveedor.AsignarFoco();
                } 
                else if (string.IsNullOrWhiteSpace(txtColor.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionEdicion_MsgColorNoValido;
                    txtColor.Focus();
                }
                else if (Contexto.Boletinado && string.IsNullOrWhiteSpace(Contexto.ObservacionesEnviar))
                {
                    if (string.IsNullOrWhiteSpace(Contexto.ObservacionesObtener))
                    {
                        resultado = false;
                        mensaje = Properties.Resources.CamionEdicion_MsgObservacionesNoValido;
                        txtObservacionesEnviar.Focus();
                    }
                }
                
               
                else
                {
                    int camionId = Extensor.ValorEntero(txtCamionId.Text);
                    string descripcion = txtDescripcion.Text;

                    var camionPL = new CamionPL();
                    CamionInfo camionBusqueda = camionPL.ObtenerPorDescripcion(descripcion);

                    if (camionBusqueda != null && (camionId == 0 || camionId != camionBusqueda.CamionID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CamionEdicion_MsgDescripcionExistente,
                                                       camionBusqueda.CamionID);
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
                    var camionPL = new CamionPL();
                    camionPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if(Contexto.CamionID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Camion_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Camion_ErrorGuardar, MessageBoxButton.OK,
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
                    Info = new ProveedorInfo { Activo = EstatusEnum.Activo },
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

        #endregion Métodos

    }
}

