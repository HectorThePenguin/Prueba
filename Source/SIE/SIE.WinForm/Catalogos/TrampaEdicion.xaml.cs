using System;
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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TrampaEdicion.xaml
    /// </summary>
    public partial class TrampaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TrampaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TrampaInfo) DataContext;
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

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TrampaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargarAyudas();
        }

        /// <summary>
        /// Constructor para editar una entidad Trampa Existente
        /// </summary>
        /// <param name="trampaInfo"></param>
        public TrampaEdicion(TrampaInfo trampaInfo)
        {
           InitializeComponent();
           trampaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = trampaInfo;
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
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TrampaInfo
            {
                Organizacion = new OrganizacionInfo(),
                TipoTrampa = (char)TipoTrampa.Enfermeria,
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
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
                if (string.IsNullOrWhiteSpace(txtTrampaID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgTrampaIDRequerida;
                    txtTrampaID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (cmbTipoTrampa.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgTipoTrampaRequerida;
                    cmbTipoTrampa.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtHostName.Text) || Contexto.HostName == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgHostNameRequerida;
                    txtHostName.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.TrampaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int trampaId = Extensor.ValorEntero(txtTrampaID.Text);
                    string descripcion = txtDescripcion.Text.TrimEnd();

                    var trampaPL = new TrampaPL();
                    TrampaInfo trampa = trampaPL.ObtenerPorDescripcion(descripcion);

                    if (trampa != null && (trampaId == 0 || trampaId != trampa.TrampaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TrampaEdicion_MsgDescripcionExistente, trampa.TrampaID);
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
                    var trampaPL = new TrampaPL();
                    trampaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TrampaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Trampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Trampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
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
            skAyudaOrganizacion.AsignaTabIndex(2);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        #endregion Métodos

    }
}

