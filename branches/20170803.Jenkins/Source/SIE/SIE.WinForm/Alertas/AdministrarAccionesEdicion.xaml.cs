using System;
using System.Windows;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.Enum;
using System.ComponentModel;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Lógica de interacción para AdministrarAccionesEdicion.xaml
    /// </summary>
    public partial class AdministrarAccionesEdicion
    {

        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase se crea el contexto.
        /// </summary>
        private AdministrarAccionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AdministrarAccionInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        #endregion Propiedades
        
        #region constructores
        // Constructor por defecto
        public AdministrarAccionesEdicion()
        {

            InitializeComponent();
            Contexto = new AdministrarAccionInfo
                    {
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    };
        }

        /// <summary>
        /// Constructor para editar una Acción.
        /// </summary>
        public AdministrarAccionesEdicion(AdministrarAccionInfo accion)
        {
            InitializeComponent();
            accion.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = accion;
        }

        #endregion Contructores

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
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,MessageImage.Question);
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
        /// Metodo para guardar la acción</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }
        
        /// <summary>
        /// Metodo para cancelar el guardado la acción   
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
            Contexto = new AdministrarAccionInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Metodo de validar al guardar la edicion de una acción.
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                // si esta en blanco manda un mensaje que es requerido el campo descripcion.
                 if (string.IsNullOrWhiteSpace(txtDescripcion.Text) && txtDescripcion.Text.Trim()==string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.AccionEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.AccionEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    // se realiza la accion para la busqueda por descripcion para no editar y asignarle una descripcion igual a una ya existente.
                    //var AccionPL = new AccionPL();
                    AdministrarAccionInfo accion = AccionPL.ObtenerPorDescripcion(Contexto.Descripcion.Trim());
                    if (accion != null && (Contexto.AccionID == 0 || Contexto.AccionID != accion.AccionID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.AccionEdicion_MsgDescripcionExistente, accion.AccionID);
                    }
                    else if (Contexto.Activo == EstatusEnum.Inactivo)
                    {
                        bool validacion = AccionPL.ValidarAsignacionesAsignadasById(Contexto.AccionID);
                        if (validacion)
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.AccionEdicion_MsgAccionConfigurada);
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
                    var accionPL = new AccionPL();
                    accionPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExitoAccion, MessageBoxButton.OK, MessageImage.Correct);
                  
                    // valido si el id es diferente de cero cuando se edita la accion que se cierrre la ventana.
                    if (Contexto.AccionID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    txtDescripcion.Clear();
                    txtDescripcion.Focus();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Accion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Accion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos
    }
}
