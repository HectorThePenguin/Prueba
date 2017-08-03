using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
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
    /// Interaction logic for OperadorEdicion.xaml
    /// </summary>
    public partial class OperadorEdicion
    {
        #region Propiedades

        private OperadorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (OperadorInfo) DataContext;
            }
            set { DataContext = value; }
        }
       
        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Control para la ayuda de Usuario
        /// </summary>
        private SKAyuda<UsuarioInfo> skAyudaUsuario;

        /// <summary>
        /// Control para la ayuda de Empleados
        /// </summary>
        private SKAyuda<EmpleadoInfo> skAyudaEmpleado;

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        private bool esEdicion = false;
        
        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public OperadorEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargaRoles();
            CargarAyudas();
        }

        /// <summary>
        /// Constructor para editar una entidad Operador Existente
        /// </summary>
        /// <param name="operadorInfo"></param>
        public OperadorEdicion(OperadorInfo operadorInfo)
        {
            InitializeComponent();
            CargaRoles();
            operadorInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            operadorInfo.Usuario = operadorInfo.Usuario ?? new UsuarioInfo();
            esEdicion = true;
            Contexto = operadorInfo;
            CargarAyudas();
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
            txtNombre.Focus();
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new OperadorInfo
                {
                    Nombre = string.Empty,
                    Organizacion = new OrganizacionInfo(),
                    Usuario = new UsuarioInfo(),
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Activo = EstatusEnum.Activo,
                    Empleado = new EmpleadoInfo(), 
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
                if (txtNombre.Text == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.OperadorEdicion_MsgNombreRequerida;
                    txtNombre.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtApellidoPaterno.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.OperadorEdicion_MsgApellidoPaternoRequerida;
                    txtApellidoPaterno.Focus();
                }
                else if ((!esEdicion && (string.IsNullOrWhiteSpace(skAyudaEmpleado.Clave) || string.IsNullOrWhiteSpace(skAyudaEmpleado.Descripcion))) || (esEdicion && string.IsNullOrWhiteSpace(txtCodigoSAP.Text)))
                {
                    resultado = false;
                    mensaje = Properties.Resources.OperadorEdicion_MsgCodigoSAPRequerida;
                    skAyudaEmpleado.AsignarFoco();
                }
                else if (cmbRol.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.OperadorEdicion_MsgRolRequerida;
                    cmbRol.Focus();
                }
                else if (string.IsNullOrWhiteSpace(skAyudaOrganizacion.Clave) ||
                       string.IsNullOrWhiteSpace(skAyudaOrganizacion.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.OperadorEdicion_MsgOrganizacionRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }

                int operadorId = Extensor.ValorEntero(txtOperadorId.Text);
                string descripcion = string.Format("{0} {1} {2}", txtNombre.Text, txtApellidoPaterno.Text,
                                                   txtApellidoMaterno.Text);

                var operadorPL = new OperadorPL();
                OperadorInfo operador = operadorPL.ObtenerPorDescripcion(descripcion);

                if (operador != null && (operadorId == 0 || operadorId != operador.OperadorID))
                {
                    resultado = false;
                    mensaje = string.Format(Properties.Resources.OperadorEdicion_MsgDescripcionExistente,
                                                   operador.OperadorID);
                    
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if(!string.IsNullOrWhiteSpace(mensaje))
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
                    var operadorPL = new OperadorPL();
                    Contexto.ApellidoMaterno = Contexto.ApellidoMaterno ?? string.Empty;
                    Contexto.Usuario = Contexto.Usuario.UsuarioID == 0
                                           ? null
                                           : Contexto.Usuario;

                    operadorPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.OperadorID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                        skAyudaOrganizacion.AsignarFoco();
                    }
                          
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Operador_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Operador_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los Roles
        /// </summary>
        private void CargaRoles()
        {
            var rolPL = new RolPL();
            cmbRol.ItemsSource = rolPL.ObtenerTodos(EstatusEnum.Activo);

        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
            AgregarAyudaUsuario();
            AgregarAyudaEmpleado();
        }
        
        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo()
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
                TabIndex = 2
            };
            skAyudaOrganizacion.AsignaTabIndex(6);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaUsuario()
        {
            skAyudaUsuario = new SKAyuda<UsuarioInfo>(200, false, new UsuarioInfo()
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadNombreCatalogoAyuda", true, 50, true)
            {
                AyudaPL = new UsuarioPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaUsuario_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaUsuario_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaUsuario_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaUsuario_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaUsuario_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaUsuario_Busqueda_Titulo,
                TabIndex = 3
            };
            skAyudaUsuario.AsignaTabIndex(1);
            SplAyudaUsuario.Children.Clear();
            SplAyudaUsuario.Children.Add(skAyudaUsuario);
        }

        /// <summary>
        /// Método para agregar el control ayuda empleado
        /// </summary>
        private void AgregarAyudaEmpleado()
        {
            skAyudaEmpleado = new SKAyuda<EmpleadoInfo>(180, false, new EmpleadoInfo()
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadNombreCatalogoAyuda", true, 70, true)
            {
                AyudaPL = new EmpleadoPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaEmpleado_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaEmpleado_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaEmpleado_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaEmpleado_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaEmpleado_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaEmpleado_Busqueda_Titulo,
                TabIndex = 1
            };

            skAyudaEmpleado.AyudaConDatos += (sender, args) =>
            {
                var empleadoSeleccionado = (EmpleadoInfo)skAyudaEmpleado.Info;
                txtNombre.Text = empleadoSeleccionado.Nombre;
                txtApellidoPaterno.Text = empleadoSeleccionado.Paterno;
                txtApellidoMaterno.Text = empleadoSeleccionado.Materno;
                txtCodigoSAP.Text = empleadoSeleccionado.EmpleadoID.ToString();
            };

            if (esEdicion)
            {
                skAyudaEmpleado.Visibility = System.Windows.Visibility.Hidden;
                txtCodigoSAP.Visibility = System.Windows.Visibility.Visible;
            }

            skAyudaEmpleado.AsignaTabIndex(1);
            SplAyudaEmpleado.Children.Clear();
            SplAyudaEmpleado.Children.Add(skAyudaEmpleado);
            skAyudaEmpleado.AsignarFoco();
            SplAyudaEmpleado.Focus();
        }

        #endregion Métodos
    }
}