using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for CorralEdicion.xaml
    /// </summary>
    public partial class CorralEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CorralInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CorralInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CorralEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
            CargaTiposCorral();
        }

        /// <summary>
        /// Constructor para editar una entidad Corral Existente
        /// </summary>
        /// <param name="corralInfo"></param>
        public CorralEdicion(CorralInfo corralInfo)
        {
            InitializeComponent();
            corralInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = corralInfo;
            CargarAyudas();
            CargaTiposCorral();
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
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar la entrada de solo números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new CorralInfo
                           {
                               Organizacion = new OrganizacionInfo(),
                               TipoCorral = new TipoCorralInfo(),
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                    txtDescripcion.SelectAll();
                }
                else if (string.IsNullOrWhiteSpace(skAyudaOrganizacion.Clave) ||
                        string.IsNullOrWhiteSpace(skAyudaOrganizacion.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgOrganizacionRequerida;

                    txtDescripcion.Focus();
                    var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    txtDescripcion.MoveFocus(tRequest);
                }

                else if (cmbTipoCorral.SelectedItem == null || Contexto.TipoCorral.TipoCorralID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgTipoCorralRequerida;
                    cmbTipoCorral.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtCapacidad.Text) || txtCapacidad.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgCapacidadRequerida;
                    txtCapacidad.Focus();
                    txtCapacidad.SelectAll();
                }
                else if (string.IsNullOrWhiteSpace(txtSeccion.Text) || txtSeccion.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgSeccionRequerida;
                    txtSeccion.Focus();
                    txtSeccion.SelectAll();
                }
                else if (string.IsNullOrWhiteSpace(txtOrden.Text) || txtOrden.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralEdicion_MsgOrdenRequerida;
                    txtOrden.Focus();
                    txtOrden.SelectAll();
                }
                else
                {
                    var lotePL = new LotePL();
                    int corralId = Extensor.ValorEntero(txtCorralId.Text);
                    string descripcion = txtDescripcion.Text;
                    int organizacionID = Contexto.Organizacion.OrganizacionID;

                    var corralPL = new CorralPL();
                    //CorralInfo corral = corralPL.ValidaCorralConLoteConExistenciaActivo(Contexto.CorralID);
                    //if (corral != null)
                    //{
                    //    mensaje = Properties.Resources.Corral_MsgConExistencia;
                    //    resultado = false;
                    //}
                    //else

                    CorralInfo corral = corralPL.ObtenerPorDescripcionOrganizacion(descripcion, organizacionID);

                    if (corral != null && (corralId == 0 || corralId != corral.CorralID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CorralEdicion_MsgDescripcionExistente,
                                                corral.CorralID);
                    }
                    var loteFiltro = new LoteInfo
                        {
                            OrganizacionID = organizacionID,
                            CorralID = corralId
                        };
                    LoteInfo lote = lotePL.ObtenerPorCorralID(loteFiltro);
                    if (lote != null)
                    {
                        if (corral != null)
                        {
                            if (corral.TipoCorral.TipoCorralID != Contexto.TipoCorral.TipoCorralID)
                            {
                                resultado = false;
                                mensaje = string.Format(Properties.Resources.CorralEdicion_MsgCambiarTipoCorral,
                                               lote.Lote);

                            }
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
                    var corralPL = new CorralPL();
                    corralPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CorralID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Corral_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Corral_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los Roles
        /// </summary>
        private void CargaTiposCorral()
        {
            var tipoCorralPL = new TipoCorralPL();
            var tipoCorral = new TipoCorralInfo
            {
                TipoCorralID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoCorralInfo> listaTiposCorral = tipoCorralPL.ObtenerTodos(EstatusEnum.Activo);
            listaTiposCorral.Insert(0, tipoCorral);
            cmbTipoCorral.ItemsSource = listaTiposCorral;
            cmbTipoCorral.SelectedItem = tipoCorral;
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
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo()
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadDescripcionCatalogoAyuda", true, 50, false)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            //skAyudaOrigen.LlamadaMetodos += ValidaOrigenYdestino;
            //skAyudaOrigen.ObtenerDatos += ValidaOrganizacionesIguales;
            skAyudaOrganizacion.AsignaTabIndex(2);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }
        #endregion Métodos

    }
}

