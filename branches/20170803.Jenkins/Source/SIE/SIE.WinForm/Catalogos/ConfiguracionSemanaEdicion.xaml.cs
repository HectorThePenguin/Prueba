using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ConfiguracionSemanaEdicion.xaml
    /// </summary>
    public partial class ConfiguracionSemanaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ConfiguracionSemanaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ConfiguracionSemanaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ConfiguracionSemanaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Constructor para editar una entidad ConfiguracionSemana Existente
        /// </summary>
        /// <param name="configuracionSemanaInfo"></param>
        public ConfiguracionSemanaEdicion(ConfiguracionSemanaInfo configuracionSemanaInfo)
        {
            InitializeComponent();
            configuracionSemanaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = configuracionSemanaInfo;
            AgregarAyudaOrganizacion();
            skAyudaOrganizacion.IsEnabled = false;
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConfiguracionSemanaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo()
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
                if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionSemanaEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.InicioSemana == Contexto.FinSemana)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionSemanaEdicion_MsgDiasIguales;
                    cmbInicioSemana.Focus();
                }
                else
                {
                    if (Contexto.Organizacion != null)
                    {
                        int configuracionSemanaId = Contexto.ConfiguracionSemanaID;
                        int organizacionId = Contexto.Organizacion.OrganizacionID;

                        var configuracionSemanaPL = new ConfiguracionSemanaPL();
                        ConfiguracionSemanaInfo configuracionSemana = configuracionSemanaPL.ObtenerPorOrganizacion(organizacionId);

                        if (configuracionSemana != null && (configuracionSemanaId == 0 || configuracionSemanaId != configuracionSemana.ConfiguracionSemanaID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.ConfiguracionSemanaEdicion_MsgDescripcionExistente, configuracionSemana.ConfiguracionSemanaID);
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
                    var configuracionSemanaPL = new ConfiguracionSemanaPL();
                    configuracionSemanaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.ConfiguracionSemanaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.ConfiguracionSemana_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ConfiguracionSemana_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {

            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, Contexto.Organizacion
                                                                  , "PropiedadClaveCatalogoConfiguracionSemana"
                                                                , "PropiedadDescripcionCatalogoConfiguracionSemana"
                                                                , true, true) { AyudaPL = new OrganizacionPL() };

            stpAyudaOrganizacion.Children.Clear();
            stpAyudaOrganizacion.Children.Add(skAyudaOrganizacion);

            skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrganizacion_Titulo;
        }
        #endregion Métodos

        
    }
}

