using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CausaPrecioEdicion.xaml
    /// </summary>
    public partial class CausaPrecioEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CausaPrecioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CausaPrecioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Se utiliza para almacenar las Causas Salidas, y llenar el combo sin estar llendo a BD
        /// </summary>
        private IList<CausaSalidaInfo> listaCausaSalida;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CausaPrecioEdicion()
        {
            InitializeComponent();
            Contexto =
                new CausaPrecioInfo
                    {
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                            }
                    };

        }

        /// <summary>
        /// Constructor para editar una entidad CausaPrecio Existente
        /// </summary>
        /// <param name="causaPrecioInfo"></param>
        public CausaPrecioEdicion(CausaPrecioInfo causaPrecioInfo)
        {
            InitializeComponent();
            causaPrecioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = causaPrecioInfo;
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
            //txtDescripcion.Focus();
            cmbTipoMovimiento.Focus();
            CargarCausasSalidas();
            CargarTiposMovimientos();
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
        /// Evento que guarda los datos de la entidad
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
            Contexto = new CausaPrecioInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                CausaSalida = new CausaSalidaInfo
                    {
                        TipoMovimiento = new TipoMovimientoInfo()
                    },
                    Organizacion = new OrganizacionInfo()
                        {
                            OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                        }
            };
        }

        private void CargarCausasSalidas()
        {
            var causaSalidaPL = new CausaSalidaPL();
            listaCausaSalida = causaSalidaPL.ObtenerTodos(EstatusEnum.Activo);
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
                if (cmbTipoMovimiento.SelectedItem == null || cmbTipoMovimiento.SelectedIndex == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaPrecioEdicion_MsgCausaTipoMovimientoRequerido;
                    cmbCausaSalida.Focus();
                }
                else if (cmbCausaSalida.SelectedItem == null || Contexto.CausaSalida.CausaSalidaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaPrecioEdicion_MsgCausaSaldiaIDRequerida;
                    cmbCausaSalida.Focus();
                }
                else if ((!dtuPrecio.Value.HasValue || dtuPrecio.Value.Value ==0) || Contexto.Precio == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaPrecioEdicion_MsgPrecioGanadoIDRequerida;
                }
                else
                {
                    int causaPrecioId = Contexto.CausaPrecioID;

                    var causaPrecioPL = new CausaPrecioPL();
                    var causaPrecio = causaPrecioPL.ObtenerPorCausaSalidaPrecioGanado(Contexto);

                    if (causaPrecio != null && (causaPrecioId == 0 || causaPrecioId != causaPrecio.CausaPrecioID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CausaPrecioEdicion_MsgCausaPrecioExistente,
                                                causaPrecio.CausaPrecioID);
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
                    var causaPrecioPL = new CausaPrecioPL();
                    causaPrecioPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    //confirmaSalir = false;
                    LimpiarPantalla();
                    //Close();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.CausaPrecio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CausaPrecio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        private void LimpiarPantalla()
        {
            InicializaContexto();
            cmbTipoMovimiento.SelectedIndex = 0;
            cmbTipoMovimiento.Focus();
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private void CargarTiposMovimientos()
        {
            try
            {
                var tipoMovimientoDefault = new TipoMovimientoInfo
                {
                    TipoMovimientoID = 0,
                    EsGanado = true,
                    Descripcion = Properties.Resources.cbo_Seleccione
                };

                var tipoMovimientoPL = new TipoMovimientoPL();
                var tiposMovimiento = tipoMovimientoPL.ObtenerTodos(EstatusEnum.Activo);
                tiposMovimiento.Insert(0, tipoMovimientoDefault);
                cmbTipoMovimiento.ItemsSource = tiposMovimiento.Where(tipo => tipo.EsGanado);
                if (Contexto.CausaSalida == null || Contexto.CausaSalida.CausaSalidaID == 0)
                {
                    cmbTipoMovimiento.SelectedIndex = 0;
                    CargarCausaSalidaDefault();
                }
                else
                {
                    cmbTipoMovimiento.SelectedValue = Contexto.CausaSalida.TipoMovimiento.TipoMovimientoID;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para cargar la Causa de Salida, por Default por Seleccione.
        /// </summary>
        private void CargarCausaSalidaDefault()
        {
            IList<CausaSalidaInfo> listaDefault = new List<CausaSalidaInfo>();
            var causaSalidaDefault = new CausaSalidaInfo
            {
                CausaSalidaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione
            };
            listaDefault.Insert(0, causaSalidaDefault);
            cmbCausaSalida.ItemsSource = listaDefault;
            cmbCausaSalida.SelectedIndex = 0;
        }

        #endregion Métodos

        private void cmbTipoMovimiento_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tipoMovimientoSeleccionado = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
            if (tipoMovimientoSeleccionado == null || tipoMovimientoSeleccionado.TipoMovimientoID == 0)
            {
                CargarCausaSalidaDefault();
                return;
            }
            var causaSalidaSeleccione = new CausaSalidaInfo
            {
                CausaSalidaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione
            };
            var listaOrdenada = listaCausaSalida.Where(causa => causa.TipoMovimiento.TipoMovimientoID == tipoMovimientoSeleccionado.TipoMovimientoID).OrderBy(causa => causa.Descripcion).ToList();
            listaOrdenada.Insert(0, causaSalidaSeleccione);

            cmbCausaSalida.ItemsSource = listaOrdenada;
            if (Contexto.CausaSalida == null)
            {
                cmbCausaSalida.SelectedIndex = 0;
            }
            if (listaOrdenada.Count <= 1)
            {
                SkMessageBox.Show(this, Properties.Resources.CausaPrecioEdicion_MsgSinCausas, MessageBoxButton.OK, MessageImage.Warning);
                cmbTipoMovimiento.SelectedIndex = 0;
            }
        }
    }
}

