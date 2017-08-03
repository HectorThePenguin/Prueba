using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.ComponentModel;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Lógica de interacción para RegistrarAlerta.xaml
    /// </summary>
    public partial class AlertaEdicion
    {
        #region Propiedades

        /// <summary>
        /// alerta que se enlaza a la interfaz
        /// </summary>
        private AlertaInfo Contexto
        {
            get
            {
                if (DataContext == null)//si no se recibio una alerta se inicializa una nueva
                {
                    InicializaContexto();
                }
                return (AlertaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private AlertaInfo ContextoSinEditar;//contexto original
        
        /// <summary>
        /// Se utiliza para identificar la confirmación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// indica si se cerrara sin pedir confirmacion del usuario
        /// </summary>
        private bool ForzarCierre ;

        /// <summary>
        /// indica si se cerrara por cancelacion del usuario
        /// </summary>
        private bool Cancelar;

        private enum Operation {Registrar,Editar};

        /// <summary>
        /// funcion que realizara la interfaz sobre las alertas
        /// </summary>
        private Operation operacion;

        #endregion

        #region Constructores

        public AlertaEdicion()
        {
            InitializeComponent();
            operacion = Operation.Registrar;//si no se recibio la informacion de una alerta se indica que la operacion es registrar
            InicializaContexto();//inicializa la informacion de la alerta
           
            CargaComboModulos();
        }

        public AlertaEdicion(AlertaInfo alertaInfo)
        {
            InitializeComponent();
            CargaComboModulos();
            ContextoSinEditar = (AlertaInfo)Extensor.ClonarInfo(alertaInfo);
            Contexto = alertaInfo;//la ventana recibe la alerta que se editara
            Contexto.UsuarioCreacionID = 0;//si la operacion es edicion se borra el parametro de id del usuario que lo registra
            Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();//el registro que se editara recibira este valor para registrarlo como el usuario que edito dicho registro
            operacion = Operation.Editar;  
     
        }
  
        #endregion

        #region Eventos

        /// <summary>
        /// controla que solo se proporcionen numeros en el campo Horas Respuesta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHorasRespuesta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = Extensor.ValidarSoloNumeros(e.Text);
            }
            catch (Exception ex)
            { 
                Logger.Error(ex);
                e.Handled = false;//si ocurrio un error al convertir, se indica que no es valido el texto ingresado 
            }
        }

        /// <summary>
        /// Guarda la alerta 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarCambios();
        }

        /// <summary>
        /// Confirma o valida si el usuario desea cerrar la ventana antes de proseguir 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (Cancelar)//despliega el mensaje de confirmacion de cancelar el registro
            {
                MessageBoxResult result;
                if (operacion == Operation.Registrar)
                {
                    result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Msg_AlertaCancelarRegistro, MessageBoxButton.YesNo, MessageImage.Question);

                }
                else //si operacion es editar
                {
                    result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Msg_AlertaCancelarEdicion, MessageBoxButton.YesNo, MessageImage.Question);
                }
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                    InitializeComponent();
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (ForzarCierre)
            {
                Contexto = null;
                InitializeComponent();
            }

            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo, MessageImage.Question);
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
        /// borra informacion de la alerta de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Cancelar = true;
            confirmaSalir = false;
            Close();
        }

        /// <summary>
        /// evento que valida que solo se ingresen numeros al campo Horas Respuesta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtHorasRespuesta_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = Extensor.ValidarSoloNumeros(txtHorasRespuesta.Text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                e.Handled = false;//si ocurrio un error al convertir, se indica que no es valido el texto ingresado 
            }
        }

        //al recibir la el contexto el checkbox se selecciono o deselecciona segun el valor del campo "TerminadoAutomatico" 
        private void CbTerminadoAUtomatico_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                cbTerminadoAUtomatico.IsChecked = (((AlertaInfo)e.NewValue).TerminadoAutomatico == EstatusEnum.Activo);
            }

        }

        //si el usuario da clic en el checkbox el contexto actualiza su valor en la propiedad "TerminadoAutomatico" hacia el nuevo valor segun este activado o no el checkbox
        private void CbTerminadoAUtomatico_OnClick(object sender, RoutedEventArgs e)
        {
            Contexto.TerminadoAutomatico = cbTerminadoAUtomatico.IsChecked == true ? EstatusEnum.Activo : EstatusEnum.Inactivo;
        }

        //al cargar la alerta se selecciona por default el campo descripcion
        private void AlertaEdicion_OnLoaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Carga los modulos activos a la interfaz
        /// </summary>
        private void CargaComboModulos()
        {
            try
            {
                var moduloBL = new ModuloBL();
                    var moduloInfo = new ModuloInfo
                    {
                        ModuloID = 0,
                        Descripcion = Properties.Resources.cbo_Seleccione
                    };
                    IList<ModuloInfo> listaModulos = moduloBL.ObtenerTodosAsList();
                    if(listaModulos ==null)//si no se encontraron modulos activos
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlertaModulo_ErrorBusqueda, 
                            MessageBoxButton.OK, MessageImage.Error);
                        confirmaSalir = false;
                        ForzarCierre = true;
                        Close();
                    }
                    else
                    {
                        //carga los modulos en el combobox de la interfaz
                        listaModulos.Insert(0, moduloInfo);
                        cmbModulo.ItemsSource = listaModulos;
                    }
                cmbModulo.SelectedItem = Contexto.Modulo;//inicializa el modulo seleccionado al modulo de la alerta recibida
                if (Contexto.Modulo.ModuloID == 0)
                {
                    cmbModulo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlertaModulo_ErrorBusqueda, 
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa la alerta que se enlazara a la interfaz
        /// </summary>
        private void InicializaContexto()
        {
            if (operacion == Operation.Registrar)
            {
                Contexto = new AlertaInfo
                {
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    TerminadoAutomatico = EstatusEnum.Inactivo,
                    Modulo = new ModuloInfo
                    {
                        ModuloID = 0
                    }
                };

                ContextoSinEditar = new AlertaInfo();
            }
            else
            {
                Contexto = new AlertaInfo
                { 
                    UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Modulo = new ModuloInfo
                    {
                        ModuloID = 0
                    }
                };
            }
        }
        
        /// <summary>
        /// Guarda o actualiza los datos de la alerta
        /// </summary>
        private void GuardarCambios()
        {
            try
            {
                if (ValidaGuardar())//si se proporcionaron los datos necesarios para guardar o editar los datos de la alerta
                {
                    if (ExisteAlerta(Contexto))//si la alerta ya esta registrada se manda un mensaje de error y no deja proseguir al usuario 
                    {
                        string error =string.Format(Properties.Resources.Msg_AlertaExistente,Contexto.Modulo.Descripcion);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],error, MessageBoxButton.OK, MessageImage.Error);
                    }
                    else
                    {
                        //procede a guardar los cambios o el registro nuevo
                        AlertaPL.Guardar(Contexto);//registra o actualiza los datos de la entrada
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.GuardadoConExito,
                            MessageBoxButton.OK, MessageImage.Correct);
                        if (operacion == Operation.Editar)//si la operacion fue registro de alerta se cierra la ventana
                        {
                            ForzarCierre = true;
                            confirmaSalir = false;
                            Close();
                        }
                        confirmaSalir = false;//si la operacion fue edicion de alerta no se cierra, solo se borran los datos de la interfaz:
                        InicializaContexto();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);//registra el error en el "log" de sucesos del sistema
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAlerta_ErrorGuardar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// valida que se hayan proporcionado los campos necesarios para el guardado o modificacion
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            if (Contexto.Modulo.ModuloID == 0)//valida que se haya proporcionado un modulo antes de guardar
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAlerta_CapturaModulo,
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Contexto.Descripcion) || txtDescripcion.Text.Trim() == string.Empty)//valida que se haya proporcionado una descripcion antes de guardar
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAlerta_CapturaDescripcion, 
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (txtHorasRespuesta.Text.Trim() != Contexto.HorasRespuesta.ToString()|| Contexto.HorasRespuesta == 0 )//valida que se haya proporcionado las horas de respuesta validas para la alerta que se desea guardar
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAlerta_CapturaHorasRespuesta, 
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            if (operacion == Operation.Editar && !Validar_DesactivacionAlerta(Contexto.AlertaID) && (ContextoSinEditar.Activo==EstatusEnum.Activo & Contexto.Activo== EstatusEnum.Inactivo))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlertaEdicion_MsgAlertaConfigurada, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// valida si la ya hay una alerta registrada al modulo especificado
        /// </summary>
        /// <param name="filtro">alerta cuya descripcion y modulo asignado se validaran</param>
        /// <returns></returns>
        private bool ExisteAlerta(AlertaInfo filtro)
        {
            try
            {
                if ( filtro.Modulo.ModuloID == ContextoSinEditar.Modulo.ModuloID &&
                    filtro.Descripcion == ContextoSinEditar.Descripcion)//si el registro no se modifico en el modulo ni descripcion
                {
                   return false;
                }
                
                var filtrosAlertas =(AlertaInfo)Extensor.ClonarInfo(filtro);

                bool result = AlertaPL.ExisteAlerta(filtrosAlertas);

                return result; //regresa true si se encontro la alerta

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorBuscar, 
                    MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorBuscar, 
                    MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
          }

        ///<summary>
        /// Metodo que valida que una alerta no tenga una configuracion
        /// </summary>
        /// <returns>Regresa true si no esta configurada esta alerta para poder desactivarla</returns>
        private bool Validar_DesactivacionAlerta(int alertaId)
        {

            try
            {
                var configAlertaPl = new ConfiguracionAlertasPL();
                AlertaInfo alerta = new AlertaInfo() { AlertaID = alertaId };
                AlertaInfo alerta_result = configAlertaPl.ObtenerAlertaPorId(alerta);

                return (alerta_result == null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }

        }

        #endregion

        
    }
}
