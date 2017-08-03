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
    /// Interaction logic for ParametroOrganizacionEdicion.xaml
    /// </summary>
    public partial class ParametroOrganizacionEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ParametroOrganizacionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ParametroOrganizacionInfo)DataContext;
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

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<ParametroInfo> skAyudaParametro;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ParametroOrganizacionEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaOrganizacion();
            AgregarAyudaParametro();
        }

        /// <summary>
        /// Constructor para editar una entidad ParametroOrganizacion Existente
        /// </summary>
        /// <param name="parametroOrganizacionInfo"></param>
        public ParametroOrganizacionEdicion(ParametroOrganizacionInfo parametroOrganizacionInfo)
        {
            InitializeComponent();
            parametroOrganizacionInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = parametroOrganizacionInfo;
            AgregarAyudaOrganizacion();
            AgregarAyudaParametro();
            skAyudaOrganizacion.IsEnabled = false;
            skAyudaParametro.IsEnabled = false;
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
            Contexto = new ParametroOrganizacionInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Parametro = new ParametroInfo
                    {
                        TipoParametro = new TipoParametroInfo()
                    },
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
                    mensaje = Properties.Resources.ParametroOrganizacionEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.Focus();
                }
                else if (Contexto.Parametro == null || Contexto.Parametro.ParametroID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroOrganizacionEdicion_MsgParametroIDRequerida;
                    skAyudaParametro.Focus();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.Valor))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroOrganizacionEdicion_MsgValorRequerida;
                    txtValor.Focus();
                }
                else
                {
                    int parametroOrganizacionId = Contexto.ParametroOrganizacionID;

                    var parametroOrganizacionPL = new ParametroOrganizacionPL();
                    ParametroOrganizacionInfo parametroOrganizacion = parametroOrganizacionPL.ObtenerPorParametroOrganizacionID(Contexto);

                    if (parametroOrganizacion != null && (parametroOrganizacionId == 0 || parametroOrganizacionId != parametroOrganizacion.ParametroOrganizacionID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ParametroOrganizacionEdicion_MsgDescripcionExistente, parametroOrganizacion.ParametroOrganizacionID);
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
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {

            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, Contexto.Organizacion
                                                                , "PropiedadClaveCatalogoParametroOrganizacion"
                                                                , "PropiedadDescripcionCatalogoParametroOrganizacion"
                                                                , true, true) { AyudaPL = new OrganizacionPL() };

            stpAyudaOrganizacion.Children.Clear();
            stpAyudaOrganizacion.Children.Add(skAyudaOrganizacion);

            skAyudaOrganizacion.AsignaTabIndex(1);

            skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrganizacion_Titulo;

           
        }


        /// <summary>
        /// Metodo para que la ayuda inicialize el campo Tipo de Parametro
        /// </summary>
        private void LlamadaMetodos()
        {
            Contexto.Parametro.TipoParametro = new TipoParametroInfo();
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaParametro()
        {

            skAyudaParametro = new SKAyuda<ParametroInfo>(160, false, Contexto.Parametro
                                                                , "PropiedadClaveCatalogoParametroOrganizacion"
                                                                , "PropiedadDescripcionCatalogoParametroOrganizacion"
                                                                , true, true) { AyudaPL = new ParametroPL() };

            stpAyudaParametro.Children.Clear();
            stpAyudaParametro.Children.Add(skAyudaParametro);

            skAyudaParametro.AsignaTabIndex(2);

            skAyudaParametro.MensajeClaveInexistente = Properties.Resources.Parametro_CodigoInvalido;
            skAyudaParametro.MensajeAgregar = Properties.Resources.Parametro_Seleccionar;
            skAyudaParametro.MensajeBusqueda = Properties.Resources.Parametro_Busqueda;
            skAyudaParametro.MensajeBusquedaCerrar = Properties.Resources.Parametro_SalirSinSeleccionar;
            skAyudaParametro.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaParametro;
            skAyudaParametro.TituloPantalla = Properties.Resources.BusquedaParametro_Titulo;

            skAyudaParametro.LlamadaMetodos += LlamadaMetodos;
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
                    var parametroOrganizacionPL = new ParametroOrganizacionPL();
                    parametroOrganizacionPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.ParametroOrganizacionID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.ParametroOrganizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ParametroOrganizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

    }
}

