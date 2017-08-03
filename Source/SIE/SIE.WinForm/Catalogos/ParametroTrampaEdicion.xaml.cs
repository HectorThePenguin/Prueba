using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
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
using System.Windows.Controls;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ParametroTrampaEdicion.xaml
    /// </summary>
    public partial class ParametroTrampaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ParametroTrampaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ParametroTrampaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Ayuda de Parametro
        /// </summary>
        private SKAyuda<ParametroInfo> skAyudaParametro;

        /// <summary>
        /// Indica si viene de edicion
        /// </summary>
        private bool edicion;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ParametroTrampaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerTiposParametro();
            ObtenerTrampas();
            AgregarAyudaParametro();
        }

        /// <summary>
        /// Constructor para editar una entidad ParametroTrampa Existente
        /// </summary>
        /// <param name="parametroTrampaInfo"></param>
        public ParametroTrampaEdicion(ParametroTrampaInfo parametroTrampaInfo)
        {
            InitializeComponent();
            parametroTrampaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = parametroTrampaInfo;
            edicion = true;
            ObtenerTiposParametro();
            ObtenerTrampas();
            AgregarAyudaParametro();
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
            cmbTipoParametro.Focus();
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
        private void TxtValidarNumerosLetrasConAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasConAcentos(e.Text);
        }

        /// <summary>
        /// Se manda llamar al cambiar el tipo de parametro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipoParametroChaged(object sender, SelectionChangedEventArgs e)
        {
            if (Contexto.Parametro.ParametroID > 0 && !edicion)
            {
                skAyudaParametro.LimpiarCampos();
            }
            edicion = false;
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ParametroTrampaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Trampa = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID =
                            AuxConfiguracion.ObtenerOrganizacionUsuario()
                    }
                },
                Parametro = new ParametroInfo
                {
                    TipoParametro = new TipoParametroInfo()
                }
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
                if (Contexto.Parametro.TipoParametro.TipoParametroID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgTipoParametroRequerido;
                    cmbTipoParametro.Focus();
                }else if (Contexto.Parametro.ParametroID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgParametroIDRequerida;
                    skAyudaParametro.AsignarFoco();
                }
                else if (Contexto.Trampa.TrampaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgTrampaIDRequerida;
                    cmbTrampa.Focus();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.Valor))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgValorRequerida;
                    txtValor.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int parametroTrampaId = Contexto.ParametroTrampaID;
                    int parametroID = Contexto.Parametro.ParametroID;
                    int trampaID = Contexto.Trampa.TrampaID;

                    var parametroTrampaPL = new ParametroTrampaPL();
                    ParametroTrampaInfo parametroTrampa = parametroTrampaPL.ObtenerPorParametroTrampa(parametroID, trampaID);

                    if (parametroTrampa != null && (parametroTrampaId == 0 || parametroTrampaId != parametroTrampa.ParametroTrampaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ParametroTrampaEdicion_MsgParametroYTrampaExistente, parametroTrampa.ParametroTrampaID);
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
                    var parametroTrampaPL = new ParametroTrampaPL();
                    parametroTrampaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.ParametroTrampaID == 0)
                    {
                        InicializaContexto();
                        cmbTipoParametro.Focus();
                    }
                    else
                    {
                        confirmaSalir = false;
                        Close();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ParametroTrampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ParametroTrampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Obtiene las trampas
        /// </summary>
        private void ObtenerTrampas()
        {
            try
            {
                var trampaPL = new TrampaPL();
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                IList<TrampaInfo> trampas = trampaPL.ObtenerPorOrganizacion(organizacionID);
                if (trampas == null)
                {
                    trampas = new List<TrampaInfo>();
                }
                var trampaSeleccione = new TrampaInfo
                {
                    Descripcion = Properties.Resources.cbo_Seleccione
                };
                trampas.Insert(0, trampaSeleccione);
                cmbTrampa.ItemsSource = trampas;
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega una ayuda de parametro
        /// </summary>
        private void AgregarAyudaParametro()
        {
            skAyudaParametro = new SKAyuda<ParametroInfo>(300, false, Contexto.Parametro, "ClaveAyudaCatalogoParametro",
                                                          "DescripcionAyudaCatalogoParametro", true, true)
                                   {
                                       AyudaPL = new ParametroPL(),
                                       MensajeClaveInexistente = Properties.Resources.Parametro_CodigoInvalido,
                                       MensajeBusquedaCerrar = Properties.Resources.Parametro_SalirSinSeleccionar,
                                       MensajeBusqueda = Properties.Resources.Parametro_Busqueda,
                                       MensajeAgregar = Properties.Resources.Parametro_Seleccionar,
                                       TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaParametro,
                                       TituloPantalla = Properties.Resources.BusquedaParametro_Titulo,
                                   };
            skAyudaParametro.LlamadaMetodos += InicializaTipoParametro;
            skAyudaParametro.AsignaTabIndex(1);
            stpParametro.Children.Clear();
            stpParametro.Children.Add(skAyudaParametro);
        }

        /// <summary>
        /// Inicializa el tipo de parametro
        /// </summary>
        private void InicializaTipoParametro()
        {
            if (cmbTipoParametro.SelectedIndex == 0)
            {
                Contexto.Parametro.TipoParametro = new TipoParametroInfo();
            }
        }

        /// <summary>
        /// Obtiene los tipos parametro
        /// </summary>
        private void ObtenerTiposParametro()
        {
            try
            {
                var tipoParametroPL = new TipoParametroPL();
                IList<TipoParametroInfo> tiposParametros = tipoParametroPL.ObtenerTodos(EstatusEnum.Activo);
                if (tiposParametros == null)
                {
                    tiposParametros = new List<TipoParametroInfo>();
                }
                var tipoParametroSeleccione = new TipoParametroInfo
                {
                    Descripcion = Properties.Resources.cbo_Seleccione
                };
                tiposParametros.Insert(0, tipoParametroSeleccione);
                cmbTipoParametro.ItemsSource = tiposParametros;
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Métodos
    }
}
