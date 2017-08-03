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
    /// Interaction logic for SubFamiliaEdicion.xaml
    /// </summary>
    public partial class SubFamiliaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SubFamiliaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SubFamiliaInfo) DataContext;
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
        public SubFamiliaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargaFamilia();
        }

        /// <summary>
        /// Constructor para editar una entidad SubFamilia Existente
        /// </summary>
        /// <param name="subFamiliaInfo"></param>
        public SubFamiliaEdicion(SubFamiliaInfo subFamiliaInfo)
        {
            InitializeComponent();
            CargaFamilia();
            subFamiliaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = subFamiliaInfo;
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
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar,
                                                            MessageBoxButton.YesNo,
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
            e.Handled = Extensor.ValidarLetrasConAcentos(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números
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
            Contexto = 
                new SubFamiliaInfo
                           {
                               Familia = new FamiliaInfo(),
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
                if (string.IsNullOrWhiteSpace(txtSubFamiliaID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.SubFamiliaEdicion_MsgSubFamiliaIDRequerida;
                    txtSubFamiliaID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.SubFamiliaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbFamilia.SelectedItem == null || Contexto.Familia.FamiliaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.SubFamiliaEdicion_MsgFamiliaIDRequerida;
                    cmbFamilia.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.SubFamiliaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int subFamiliaId = Extensor.ValorEntero(txtSubFamiliaID.Text);
                    string descripcion = txtDescripcion.Text;
                    int familiaId = Contexto.Familia.FamiliaID;

                    var subFamiliaPL = new SubFamiliaPL();
                    SubFamiliaInfo subFamilia = subFamiliaPL.ObtenerPorDescripcion(descripcion, familiaId);

                    if (subFamilia != null && (subFamiliaId == 0 || subFamiliaId != subFamilia.SubFamiliaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.SubFamiliaEdicion_MsgDescripcionExistente,
                                                subFamilia.SubFamiliaID);
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
                    int  subFamiliaID = Contexto.SubFamiliaID;
                    var subFamiliaPL = new SubFamiliaPL();
                    subFamiliaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (subFamiliaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.SubFamilia_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.SubFamilia_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga Familias
        /// </summary>
        private void CargaFamilia()
        {
            var familiaPL = new FamiliaPL();
            var familia = new FamiliaInfo
                                  {
                                      FamiliaID = 0,
                                      Descripcion = Properties.Resources.cbo_Seleccione,
                                  };
            IList<FamiliaInfo> listaFamilias = familiaPL.ObtenerTodos(EstatusEnum.Activo);
            listaFamilias.Insert(0, familia);
            cmbFamilia.ItemsSource = listaFamilias;
            cmbFamilia.SelectedItem = familia;
        }

        #endregion Métodos

    }
}
