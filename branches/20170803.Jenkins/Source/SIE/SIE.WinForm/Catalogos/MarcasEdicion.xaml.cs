using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para MarcasEdicion.xaml
    /// </summary>
    public partial class MarcasEdicion
    {
        #region Propiedades
        /// <summary>
        /// Variable para la confirmación de salida de la ventana
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Contexto de la interfaz
        /// </summary>
        private MarcasInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (MarcasInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor simple de clase
        /// </summary>
        public MarcasEdicion()
        {
            InitializeComponent();
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Constructor para inicializar la pantalla con los datos de la marca a editar
        /// </summary>
        /// <param name="marcasInfo"> Información de las marcas a cargar en pantalla </param>
        public MarcasEdicion(MarcasInfo marcasInfo)
        {
            InitializeComponent();
            Contexto.Activo = marcasInfo.Activo;
            Contexto.MarcaId = marcasInfo.MarcaId;
            Contexto.Descripcion = marcasInfo.Descripcion;
            rbTracto.IsChecked = Convert.ToBoolean(marcasInfo.EsTracto);
            rbJaula.IsChecked = !Convert.ToBoolean(marcasInfo.EsTracto);
            txtDescripcion.Focus();
        }


        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta al cerrar la ventana
        /// </summary>
        /// <param name="e"> Parámetro estandar e </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
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
        /// Evento que se ejectua al seleccionar el botón cancelar.
        /// </summary>
        /// <param name="sender"> Parámetro estandar sender</param>
        /// <param name="e"> Parámetro estandar e </param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta al seleccionar el botón guardar.
        /// </summary>
        /// <param name="sender">Parámetro estandar sender</param>
        /// <param name="e">Parámetro estandar e</param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Verificar())
                {
                    var marcasInfo = new MarcasInfo()
                    {
                        MarcaId = Convert.ToInt32(txtMarcaID.Text),
                        Descripcion = txtDescripcion.Text.Trim(),
                        Activo = Convert.ToBoolean(cboEstatus.SelectedIndex) ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        EsTracto = Convert.ToBoolean(rbTracto.IsChecked) ? TractoEnum.Tracto : TractoEnum.Jaula
                    };
                    var marcasPL = new MarcasPL();
                    MarcasInfo verificado = marcasPL.VerificaExistenciMarca(marcasInfo);
                    if (!(verificado.MarcaId > 0) || verificado.MarcaId == marcasInfo.MarcaId)
                    {
                        marcasInfo = marcasPL.GuardarMarca(marcasInfo);
                        if (marcasInfo.MarcaId != 0)
                        {
                            SkMessageBox.Show(this, Properties.Resources.MarcasEdicion_Mensaje_Exito, MessageBoxButton.OK, MessageImage.Correct);
                            if (marcasInfo.FechaModificacion == new DateTime())
                            {
                                LimpiarCampos();
                            }
                            else 
                            {
                                confirmaSalir = false;
                                Close();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MarcasEdicion_Mensaje_Error, MessageBoxButton.OK, MessageImage.Error);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MarcasEdicion_Mensaje_MarcaRegistrada + verificado.MarcaId, MessageBoxButton.OK, MessageImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
            
        }

        /// <summary>
        /// Evento que se ejecuta al cargarse la interfaz
        /// </summary>
        /// <param name="sender"> Parametro estandar sender </param>
        /// <param name="e"> Parametro estandar e </param>
        private void MarcasEdicion_OnLoaded(object sender, RoutedEventArgs e)
        {
            CargarCboEstatus();
            txtDescripcion.Focus();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método que verifica que los campos no se encuentren vacíos
        /// </summary>
        /// <returns> Variable booleana para validación </returns>
        private bool Verificar()
        {
            var respuesta = true;
            if (rbJaula.IsChecked == false && rbTracto.IsChecked == false)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MarcasEdicion_Jaula_Obligatorio, MessageBoxButton.OK, MessageImage.Warning);
                respuesta = false;
            }
            else if (txtDescripcion.Text.Trim() == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MarcasEdicion_Descripcion_Obligatorio, MessageBoxButton.OK, MessageImage.Warning);
                txtDescripcion.Focus();
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Método que limpia los campos de la pantalla
        /// </summary>
        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtMarcaID.Text = "0";
            cboEstatus.SelectedItem = EstatusEnum.Activo;
            rbTracto.IsChecked = false;
            rbJaula.IsChecked = false;
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Método que carga el combo de estados para mostrar
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                txtMarcaID.Text = Contexto.MarcaId.ToString();
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = Contexto.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que inicializa el contexto de la interfaz
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new MarcasInfo();
        }

        private void SoloLetrasYNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        #endregion
    }
}
