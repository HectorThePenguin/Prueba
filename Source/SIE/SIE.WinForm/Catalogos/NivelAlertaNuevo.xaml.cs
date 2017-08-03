using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para NivelAlertaNuevo.xaml
    /// </summary>
    public partial class NivelAlertaNuevo
    {
        public EstatusEnum AuxInactivo;
        /// <summary>
        /// Contenerdor de la clase se crea el contexto.
        /// </summary>
        private NivelAlertaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (NivelAlertaInfo)DataContext;
            }
            set { DataContext = value; }
        }
        
        /// <summary>
        /// cargo al usuario que esta logeado.
        /// </summary>
        public NivelAlertaNuevo()
        {
            InitializeComponent();

            Contexto = new NivelAlertaInfo
                    {
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    };
        }
        
        /// <summary>
        /// Constructor para editar un nivel.
        /// </summary>
        public NivelAlertaNuevo(NivelAlertaInfo Nivel)
        {
            InitializeComponent();
            Nivel.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            AuxInactivo = Nivel.Activo;
            Contexto = Nivel;
        }   

        #region Propiedades      

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades
        
        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
        }
        
        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo, MessageImage.Question);
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
        /// Evento para nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new NivelAlertaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        //Metodo de validar al guardar el nivel de alerta.
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                // si esta en blanco manda un mensaje que es requerido el campo descripcion.
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion) && txtDescripcion.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.NivelAlertaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.NivelAlertaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {

                    // se realiza la busqueda por descripcion para no editar y asignarle una descripcion igual a una ya existente.
                    int NivelAlertaID = Contexto.NivelAlertaId;
                    var NivelAlertaPL = new NivelAlertaPL();
                    NivelAlertaInfo NivelALerta = NivelAlertaPL.ObtenerPorDescripcion(Contexto.Descripcion.Trim());

                    //Obtiene un contador de los niveles que se encuentran deshabilitados
                    int nivelesAlertaDesactivados = NivelAlertaPL.NivelesAlertaDesactivados();
                    
                    //validacion si la descripcion del nivel registrado ya existe
                    if (NivelALerta != null && (NivelAlertaID == 0 || NivelAlertaID != NivelALerta.NivelAlertaId))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.NivelAlertaEdicion_MsgDescripcionExistente, NivelALerta.NivelAlertaId);
                    }
                    //validacion para cuando se crea un nuevo registro activo pero hay un registro previo desactivado
                    else if (nivelesAlertaDesactivados > 0 && Contexto.Activo == EstatusEnum.Activo && Contexto.NivelAlertaId == 0)
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.NivelAlertaCreacion_MsgInactivos);
                    }
                    //validacion para cuando editas un nivel alerta
                    else if (Contexto.Activo == EstatusEnum.Inactivo && Contexto.NivelAlertaId != 0)
                    {
                        //validacion para cuando editas un nivel alerta pero solo modificas su descripcion
                        //(AuxInactivo guarda en valor original en cuanto se cargan los datos del nivel a editar
                        if (AuxInactivo != Contexto.Activo)
                        {
                            int verificarAsignacionNivelAlerta = NivelAlertaPL.VerificarAsignacionNivelAlerta(NivelAlertaID);
                            //verifica si el nivel seleccionado no a sido asignado
                            if (verificarAsignacionNivelAlerta > 0)
                            {
                                resultado = false;
                                mensaje = string.Format(Properties.Resources.NivelAlertaEdicion_MsgAsignado);
                            }    
                        }
                    }
                    //validacion cuando un inactivo quiere pasar a activo...
                    else if (AuxInactivo != EstatusEnum.Activo && Contexto.NivelAlertaId != 0)
                    {
                        int habilitarPrimerRegistro = NivelAlertaPL.NivelAlerta_ActivarPrimerNivelDesactivado(NivelAlertaID);
                        //Si regresa 0 no es el primero deshabilitado si regresa > 0 es el primero deshabilitado
                        if (habilitarPrimerRegistro == 0)
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.NivelAlertaActivacion_MsgActivar);
                        }
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
                    var nivelAlertaPL = new NivelAlertaPL();
                    nivelAlertaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExitoNivelAlerta, MessageBoxButton.OK, MessageImage.Correct);
                    confirmaSalir = false;

                    // valido si el id es diferente de 0 cuando se edita el nivel para que se cierrre la ventana.
                    if (Contexto.NivelAlertaId != 0)
                    {
                        Close();
                    }

                    txtDescripcion.Clear();
                    txtDescripcion.Focus();

                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.NivelAlerta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.NivelAlerta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos
    }
}

