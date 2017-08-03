using System;
using System.Collections.Generic;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TipoMovimientoEdicion.xaml
    /// </summary>
    public partial class TipoMovimientoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoMovimientoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TipoMovimientoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TipoMovimientoEdicion()
        {
            InitializeComponent();
            CargaTipoPoliza();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad TipoMovimiento Existente
        /// </summary>
        /// <param name="tipoMovimientoInfo"></param>
        public TipoMovimientoEdicion(TipoMovimientoInfo tipoMovimientoInfo)
        {
            InitializeComponent();
            CargaTipoPoliza();
            tipoMovimientoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = tipoMovimientoInfo;
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
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new TipoMovimientoInfo
            {
                TipoPoliza = new TipoPolizaInfo(),
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoMovimientoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }                
                else if (string.IsNullOrWhiteSpace(txtClaveCodigo.Text) || Contexto.ClaveCodigo == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoMovimientoEdicion_MsgClaveCodigoRequerida;
                    txtClaveCodigo.Focus();
                }
                else if (cmbTipoPoliza.SelectedItem == null || Contexto.TipoPoliza.TipoPolizaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoMovimientoEdicion_MsgTipoPolizaIDRequerida;
                    cmbTipoPoliza.Focus();
                }                
                else
                {
                    int tipoMovimientoId = Extensor.ValorEntero(txtTipoMovimientoID.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var tipoMovimientoPL = new TipoMovimientoPL();
                    TipoMovimientoInfo tipoMovimiento = tipoMovimientoPL.ObtenerPorDescripcion(descripcion);

                    if (tipoMovimiento != null && (tipoMovimientoId == 0 || tipoMovimientoId != tipoMovimiento.TipoMovimientoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TipoMovimientoEdicion_MsgDescripcionExistente, tipoMovimiento.TipoMovimientoID);
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
                    var tipoMovimientoPL = new TipoMovimientoPL();
                    tipoMovimientoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TipoMovimientoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.TipoMovimiento_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.TipoMovimiento_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        private void CargaTipoPoliza()
        {
            var tipoPolizaPL = new TipoPolizaPL();
            var tipoPolizaInfo = new TipoPolizaInfo
            {
                TipoPolizaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoPolizaInfo> listaTipoPoliza = tipoPolizaPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoPoliza.Insert(0, tipoPolizaInfo);
            cmbTipoPoliza.ItemsSource = listaTipoPoliza;
            cmbTipoPoliza.SelectedItem = tipoPolizaInfo;
        }
        #endregion Métodos

    }
}

