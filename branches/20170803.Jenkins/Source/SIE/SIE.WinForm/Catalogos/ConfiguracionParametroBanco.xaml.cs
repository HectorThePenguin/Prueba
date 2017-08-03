using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionParametroBanco.xaml
    /// </summary>
    public partial class ConfiguracionParametroBanco 
    {

        #region Propiedades

        private bool Recargar;

        /// <summary>
        /// Contexto de la aplicación
        /// </summary>
        private CatParametroConfiguracionBancoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CatParametroConfiguracionBancoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametroSinEdicion;

        #endregion

        #region Constructores

        /// <summary>
        /// Carga de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Recargar)
                {
                    InicializaContexto();

                    skAyudaBanco.ObjetoNegocio = new BancoPL();
                    skAyudaBanco.Contexto = Contexto.BancoID;
                    skAyudaBanco.txtClave.Focus();
                    skAyudaBanco.AyudaConDatos += skAyudaBanco_AyudaConDatos;
                    skAyudaBanco.AyudaLimpia += skAyudaBanco_AyudaLimpia;
                    btnGuardar.IsEnabled = false;
                    Recargar = false;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Constructor principal
        /// </summary>
        public ConfiguracionParametroBanco()
        {
            InitializeComponent();
            InicializaContexto();
            Recargar = true;
        }

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CatParametroConfiguracionBancoInfo { Activo = EstatusEnum.Activo, BancoID = new BancoInfo() };
        }

        #endregion

        #region Eventos


        /// <summary>
        /// Funcion que no permite espacios en blanco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            KeyEventArgs ke = e;
            if (ke != null && ke.Key == Key.Space)
            {
                ke.Handled = true;
            }
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaBanco_AyudaLimpia(object sender, EventArgs e)
        {
            gridConfiguracionParametros.ItemsSource = new List<CatParametroConfiguracionBancoInfo>();        
            skAyudaBanco.AsignarFoco();
        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaBanco_AyudaConDatos(object sender, EventArgs e)
        {
            gridConfiguracionParametros.ItemsSource = new List<CatParametroConfiguracionBancoInfo>();
        }

        /// <summary>
        /// Evento del boton de guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarConfiguracionParametro();
        }

        /// <summary>
        /// Evento del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        /// <summary>
        /// Evento del boton buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarParametrosPorBanco();
        }

        /// <summary>
        /// Pemitir o no ingresar caracteres
        /// </summary>
        /// <param name="sender">Objeto que se envia</param>
        /// <param name="e">Evento del campo modificado</param>
        private void ValidacionTextBox(object sender, TextCompositionEventArgs e)
        {
            var texto = e.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                e.Handled = false;
            }
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
            
        }

        /// <summary>
        /// Permite que solo se puedan pegar caracteres numericos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!Extensor.ValidarNumeros(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Actualiza la configuracion de los parametros
        /// </summary>
        private void ActualizarConfiguracionParametro()
        {
            try
            {
                List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametroGrid = (List<CatParametroConfiguracionBancoInfo>)gridConfiguracionParametros.ItemsSource;

                List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametro = ObtenerModificados(listaConfiguracionParametroGrid);

                if (listaConfiguracionParametro != null)
                {
                    if (listaConfiguracionParametro.Count > 0)
                    {
                        if (ValidarConfiguracionParametro(listaConfiguracionParametro))
                        {
                            var configuracionParametroChequePL = new ConfiguracionParametroChequePL();
                            UsuarioInfo usuario = new UsuarioInfo
                            {
                                UsuarioID = AuxConfiguracion.ObtenerUsuarioLogueado()
                            };
                            int resultado = configuracionParametroChequePL.ActualizarConfiguracionParametroCheque(listaConfiguracionParametro, usuario);

                            if (resultado > 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroCheque_MsgGuardado, MessageBoxButton.OK, MessageImage.Correct);
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorActualizar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorActualizar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Buscar parametros por banco
        /// </summary>
        private void BuscarParametrosPorBanco()
        {
            try
            {
                if (skAyudaBanco.Contexto != null)
                {
                    var banco = (BancoInfo)skAyudaBanco.Contexto;
                    if (banco.BancoID > 0)
                    {
                        UsuarioInfo usuario = new UsuarioInfo
                        {
                            UsuarioID = AuxConfiguracion.ObtenerUsuarioLogueado()
                        };
                        ObtenerConfiguracionParametroBanco(banco, usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorObtenerPorBanco, MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }
            
        }

        /// <summary>
        /// Limpiar los campos ayuda grid y desactiva el boton guardar
        /// </summary>
        private void LimpiarCampos()
        {
            skAyudaBanco.LimpiarCampos();
            gridConfiguracionParametros.ItemsSource = new List<CatParametroConfiguracionBancoInfo>();
            btnGuardar.IsEnabled = false;
        }
        /// <summary>
        /// Realiza la validacion de los campos requeridos
        /// </summary>
        /// <param name="listaConfiguracionParametro">Lista de repositorios</param>
        /// <returns></returns>
        private bool ValidarConfiguracionParametro(List<CatParametroConfiguracionBancoInfo> listaConfiguracionParametro)
        {
            bool resultado = false;
            try
            {
                foreach (var catParametroConfiguracionBancoInfo in listaConfiguracionParametro)
                {
                    if (catParametroConfiguracionBancoInfo.X < 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroCheque_MsgValidarX, MessageBoxButton.OK, MessageImage.Error);
                    }
                    else
                    {
                        if (catParametroConfiguracionBancoInfo.Y < 0)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroCheque_MsgValidarY, MessageBoxButton.OK, MessageImage.Error);
                        }
                        else
                        {
                            if (catParametroConfiguracionBancoInfo.Width < 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroCheque_MsgValidarAncho, MessageBoxButton.OK, MessageImage.Error);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }

        /// <summary>
        /// Obtien y llena el grid con la configuracion del parametro de cheque por banco
        /// </summary>
        /// <param name="banco">Informacion del banco</param>
        /// <param name="usuario">Identificador del usuario loggeado</param>
        private void ObtenerConfiguracionParametroBanco(BancoInfo banco, UsuarioInfo usuario)
        {
            try
            {

                var configuracionParametroChequePL = new ConfiguracionParametroChequePL();
                
                CatParametroConfiguracionBancoInfo info = new CatParametroConfiguracionBancoInfo
                {
                    BancoID = banco,
                    Activo = EstatusEnum.Activo
                };
                List<CatParametroConfiguracionBancoInfo> resultadoInfo = configuracionParametroChequePL.ObtenerPorBanco(info, usuario);
                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {
                    gridConfiguracionParametros.ItemsSource = resultadoInfo;
                    ClonarLista(resultadoInfo);
                    btnGuardar.IsEnabled = true;
                }
                else
                {
                    gridConfiguracionParametros.ItemsSource = new List<CatParametroConfiguracionBancoInfo>();
                    btnGuardar.IsEnabled = false;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorObtenerPorBanco, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionParametroBanco_ErrorObtenerPorBanco, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Realiza la funcionalida del cancelar
        /// </summary>
        private void Cancelar()
        {
            MessageBoxResult result = SkMessageBox.Show(Properties.Resources.ConfiguracionParametroBanco_MsgCancelar, MessageBoxButton.YesNo, MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                gridConfiguracionParametros.ItemsSource = new List<CatParametroConfiguracionBancoInfo>();
                skAyudaBanco.LimpiarCampos();
                btnGuardar.IsEnabled = false;
            }
        }

        /// <summary>
        /// Clona la lista
        /// </summary>
        /// <param name="tmpLista"></param>
        private void ClonarLista(List<CatParametroConfiguracionBancoInfo> tmpLista)
        {
            try
            {
                listaConfiguracionParametroSinEdicion = new List<CatParametroConfiguracionBancoInfo>();

                tmpLista.ForEach((item) => listaConfiguracionParametroSinEdicion.Add((CatParametroConfiguracionBancoInfo)item.Clone()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
            
        }

        /// <summary>
        /// Obtiene los registros modificados
        /// </summary>
        /// <param name="listaGrid">Datos del grid</param>
        /// <returns></returns>
        private List<CatParametroConfiguracionBancoInfo> ObtenerModificados(List<CatParametroConfiguracionBancoInfo> listaGrid)
        {
            List<CatParametroConfiguracionBancoInfo> listaModificada = new List<CatParametroConfiguracionBancoInfo>();
            try
            {
                foreach (var parametroSinModificacion in listaConfiguracionParametroSinEdicion)
                {
                    listaModificada.AddRange(listaGrid.Where(parametroModificado => parametroModificado.CatParametroConfiguracionBancoID == parametroSinModificacion.CatParametroConfiguracionBancoID).Where(parametroModificado => parametroModificado.ToString() != parametroSinModificacion.ToString()));
                }
                return listaModificada;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        #endregion

    }
}
