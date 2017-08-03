using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CostoPromedioEdicion.xaml
    /// </summary>
    public partial class CostoPromedioEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CostoPromedioInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CostoPromedioInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCosto;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CostoPromedioEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargarAyudas();
        }

        /// <summary>
        /// Constructor para editar una entidad CostoPromedio Existente
        /// </summary>
        /// <param name="costoPromedioInfo"></param>
        public CostoPromedioEdicion(CostoPromedioInfo costoPromedioInfo)
        {
           InitializeComponent();
           InicializaContexto();
           costoPromedioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = costoPromedioInfo;
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
             //txtDescripcion.Focus();
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
        /// utilizaremos el evento PreviewTextInput para validar números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void DtuControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                //c.Focus();
                return;
            }

            string valorControl = ((DecimalUpDown)sender).Text ?? string.Empty;

            if (e.Key == Key.Decimal && valorControl.IndexOf('.') > 0)
            {
                e.Handled = true;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CostoPromedioInfo
            {
                Organizacion = new OrganizacionInfo(),
                Costo = new CostoInfo(),
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
                if (string.IsNullOrWhiteSpace(txtCostoPromedioID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CostoPromedioEdicion_MsgcostopromedioidRequerida;
                    txtCostoPromedioID.Focus();
                }
                else if(Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CostoPromedioEdicion_MsgorganizacionidRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if(Contexto.Costo == null || Contexto.Costo.CostoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CostoPromedioEdicion_MsgcostoidRequerida;
                    skAyudaCosto.AsignarFoco();
                }
                else if (!dtuImporte.Value.HasValue || Contexto.Importe == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CostoPromedioEdicion_MsgimporteRequerida;
                    dtuImporte.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CostoPromedioEdicion_MsgactivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int costoPromedioId = Extensor.ValorEntero(txtCostoPromedioID.Text);
                    int organizacionId = Contexto.Organizacion.OrganizacionID;
                    int costoId = Contexto.Costo.CostoID;

                    var costoPromedioPL = new CostoPromedioPL();
                    CostoPromedioInfo costoPromedio = costoPromedioPL.ObtenerPorOrganizacionCosto(organizacionId, costoId);

                    if (costoPromedio != null && (costoPromedioId == 0 || costoPromedioId != costoPromedio.CostoPromedioID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CostoPromedioEdicion_MsgDescripcionExistente, costoPromedio.CostoPromedioID);
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
                    var costoPromedioPL = new CostoPromedioPL();
                    costoPromedioPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CostoPromedioID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CostoPromedio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.CostoPromedio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
            AgregarAyudaCosto();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
           skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, Contexto.Organizacion
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadDescripcionCatalogoAyuda", true, 50, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente =
                    Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar =
                    Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }
        
        /// <summary>
        /// Agrega una ayuda de costo
        /// </summary>
        private void AgregarAyudaCosto()
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(200, false, Contexto.Costo
                                                , "PropiedadClaveCosteoEntradaSinDependencia"
                                                , "PropiedadDescripcionCosteoEntradaSinDependencia"
                                                , "PropiedadOcultaAyudaCatalogoAyuda", true, 50, 3, false)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };

            skAyudaCosto.AsignaTabIndex(1);
            skAyudaCosto.IsTabStop = false;
            SplAyudaCosto.Children.Clear();
            SplAyudaCosto.Children.Add(skAyudaCosto);
        }

        #endregion Métodos
    }
}

