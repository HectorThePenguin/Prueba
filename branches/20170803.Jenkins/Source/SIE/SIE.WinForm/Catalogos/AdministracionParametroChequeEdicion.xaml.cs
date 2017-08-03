using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using SIE.Base.Exepciones;
using System;
using System.Windows;
using System.Reflection;
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
    /// Interaction logic for ParametroChequeEdicion.xaml
    /// </summary>
    public partial class AdministracionParametroChequeEdicion
    {

        #region Propiedades

        /// <summary>
        /// Bandera para indicar si se puede cerrar el dialogo
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Propiedad que contiene el contexto de la aplicación
        /// </summary>
        private CatParametroBancoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CatParametroBancoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor principal
        /// </summary>
        public AdministracionParametroChequeEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Contructor para la edición
        /// </summary>
        /// <param name="catParametro"></param>
        public AdministracionParametroChequeEdicion(CatParametroBancoInfo catParametro)
        {
            InitializeComponent();
            InicializaContexto();
            Contexto = catParametro;
            Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CatParametroBancoInfo { Activo = EstatusEnum.Activo, UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(), UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado() };
            confirmaSalir = true;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarParametroCheque();
        }

        /// <summary>
        /// Evento del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        /// Evento para validar que solo se puedan introducir solo letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSoloLetrasyNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Evento para validar que solo se puedan introducir solo letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSoloLetrasAcentoyNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasConAcentosGuion(e.Text);
        }

        #endregion

        #region Métodos

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
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroChequeEdicion_ValidarDescripcion;
                    txtDescripcion.Focus();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Contexto.Clave))
                    {
                        resultado = false;
                        mensaje = Properties.Resources.ParametroChequeEdicion_ValidarClave;
                        txtClave.Focus();
                    }

                    else
                    {
                        if (Contexto.TipoParametroID.GetHashCode() <= 0)
                        {
                            resultado = false;
                            mensaje = Properties.Resources.ParametroChequeEdicion_ValidarTipoParametro;
                            cmbTipoParametro.Focus();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(Contexto.Valor))
                            {
                                resultado = false;
                                mensaje = Properties.Resources.ParametroChequeEdicion_ValidarValor;
                                txtValor.Focus();
                            }
                            else
                            {
                                if (Contexto.Activo.GetHashCode() < 0)
                                {
                                    resultado = false;
                                    mensaje = Properties.Resources.ParametroChequeEdicion_ValidarEstatus;
                                    cmbActivo.Focus();
                                }
                                else
                                {
                                    int idExistente = this.DescripcionExiste();
                                    if(idExistente>0)
                                    {
                                        resultado = false;
                                        mensaje = Properties.Resources.AdministracionParametroBanco_ValidacionDescripcion.Replace("{ID}", idExistente.ToString());
                                        txtDescripcion.Focus();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
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
        private void GuardarParametroCheque()
        {
            try
            {
                if (ValidaGuardar())
                {
                    int resultado = 0;
                    bool cerrar = false;
                    AdministracionParametroChequePL administracionParametroChequePL = new AdministracionParametroChequePL();
                    List<CatParametroBancoInfo> lista = new List<CatParametroBancoInfo>();
                    lista.Add(Contexto);
                    string mensage = string.Empty;

                    if (Contexto.ParametroID > 0)
                    {
                        resultado = administracionParametroChequePL.EditarParametroCheque(lista);
                        mensage = Properties.Resources.ParametroChequeEdicion_MsgEditado;
                        cerrar = true;
                    }
                    else
                    {
                        resultado = administracionParametroChequePL.GuardarParametroCheque(lista);
                        mensage = Properties.Resources.ParametroChequeEdicion_MsgGuardado;
                        cerrar = false;
                    }
                    
                    if (resultado > 0)
                    {
                        SkMessageBox.Show(this, mensage, MessageBoxButton.OK, MessageImage.Correct);
                        confirmaSalir = false;
                        if (cerrar)
                        {
                            Close();
                        }
                        else
                        {
                            InicializaContexto();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.AdministracionParametroChequeEdicion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Metodo que valida si la descripcion ya existe
        /// </summary>
        /// <returns>boolean</returns>
        private int DescripcionExiste()
        {
            try
            {
                AdministracionParametroChequePL administracionParametroChequePL = new AdministracionParametroChequePL();
                CatParametroBancoInfo resultado = administracionParametroChequePL.ObtenerParametroChequePorDescipcion(Contexto);
                if (resultado != null)
                {
                    if (Contexto.ParametroID > 0)
                    {
                        if (Contexto.ParametroID != resultado.ParametroID)
                        {
                            return resultado.ParametroID;
                        }
                    }
                    else
                    {
                        return resultado.ParametroID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.AdministracionParametroChequeEdicion_ErrorValidarExistente, MessageBoxButton.OK, MessageImage.Warning);
            }
            return 0;
        }

        #endregion Métodos 
        
    }
}
