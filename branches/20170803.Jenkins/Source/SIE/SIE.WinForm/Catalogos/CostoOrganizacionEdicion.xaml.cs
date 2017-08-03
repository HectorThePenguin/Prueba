using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CostoOrganizacionEdicion.xaml
    /// </summary>
    public partial class CostoOrganizacionEdicion
    {
        #region PROPIEDADES

        private CostoOrganizacionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CostoOrganizacionInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public CostoOrganizacionEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaCosto();
        }

        public CostoOrganizacionEdicion(CostoOrganizacionInfo costoOrganizacion, IList<TipoOrganizacionInfo> tiposOrganizacion)
        {
            InitializeComponent();
            InicializaContexto();
            Contexto = costoOrganizacion;
            TiposOrganizacion = tiposOrganizacion;
            AgregarAyudaCosto();
            CambiarLeyendaCombo();
        }

        #endregion CONSTRUCTORES

        #region VARIABLES

        private IList<TipoOrganizacionInfo> TiposOrganizacion;
        private SKAyuda<CostoInfo> skAyudaCosto;

        #endregion VARIABLES

        #region EVENTOS

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboTipoOrganizacion.ItemsSource = TiposOrganizacion;
            cboTipoOrganizacion.Focus();
        }

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
            else if (e.Key == Key.Escape)
            {
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
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar
                                                          , MessageBoxButton.YesNo,
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
        /// Guarda un nuevo Costo para la Organizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_OnClick(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Cierra la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Iniciliaza el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CostoOrganizacionInfo();
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Agrega una ayuda de costo
        /// </summary>
        private void AgregarAyudaCosto()
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(160, false, Contexto.Costo
                                                , "PropiedadClaveCosteoEntradaSinDependencia"
                                                , "PropiedadDescripcionCosteoEntradaSinDependencia"
                                                , "PropiedadOcultaProgramacionEmbarqueCostos", true, 50, 3, false)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };

            skAyudaCosto.AsignaTabIndex(2);
            skAyudaCosto.IsTabStop = false;

            stpCosto.Children.Clear();
            stpCosto.Children.Add(skAyudaCosto);
        }

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            TipoOrganizacionInfo tipoOrganizacion =
                TiposOrganizacion.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccionar));
            if (tipoOrganizacion != null)
            {
                tipoOrganizacion.Descripcion = Properties.Resources.cbo_Seleccione;
            }
        }

        /// <summary>
        /// Guarda un Costo Organizacion
        /// </summary>
        private void Guardar()
        {
            try
            {
                bool guardar = ValidaGuardar();
                if (guardar)
                {
                    var costoOrganizacionPL = new CostoOrganizacionPL();
                    costoOrganizacionPL.Crear(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if ( Contexto.CostoOrganizacionID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();              
                        
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.Producto_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.Producto_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida los campos a guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool guardar = true;

            var mensaje = string.Empty;
            if (Contexto.TipoOrganizacion == null || Contexto.TipoOrganizacion.TipoOrganizacionID == 0)
            {
                guardar = false;
                cboTipoOrganizacion.Focus();
                mensaje = Properties.Resources.CostoOrganizacionEdicion_MsgTipoOrganizacionIDRequerida;
            }
            else
            {
                if (Contexto.Costo == null || Contexto.Costo.CostoID == 0)
                {
                    guardar = false;
                    skAyudaCosto.AsignarFoco();
                    mensaje = Properties.Resources.CostoOrganizacionEdicion_MsgCostoIDRequerida;
                }
            }
            if (guardar)
            {
                var costoOrganizacionPL = new CostoOrganizacionPL();
                CostoOrganizacionInfo costoOrganizacion = costoOrganizacionPL.ObtenerPorTipoOrganizacionCosto(Contexto);
                if (costoOrganizacion != null && costoOrganizacion.CostoOrganizacionID != Contexto.CostoOrganizacionID)
                {
                    guardar = false;
                    mensaje = string.Format(Properties.Resources.CostoOrganizacion_TipoOrganizacion_Costo_Existente,
                                            Contexto.TipoOrganizacion.Descripcion, Contexto.Costo.Descripcion);
                    cboTipoOrganizacion.Focus();
                    skAyudaCosto.LimpiarCampos();
                }
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }

            return guardar;
        }
        #endregion METODOS
    }
}
