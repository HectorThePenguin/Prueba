using System;
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

namespace SIE.WinForm.Abasto
{
    public partial class TipoCreditoEdicion
    {
       #region Propiedades

        private TipoCreditoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TipoCreditoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private bool confirmaSalir = true;

        #endregion Propiedades

       #region Constructores

        public TipoCreditoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        public TipoCreditoEdicion(TipoCreditoInfo info)
        {
           InitializeComponent();
           info.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = info;
        }

        #endregion Constructores

       #region Eventos
       private void Window_Loaded(object sender, RoutedEventArgs e)
       {
            txtDescripcion.Focus();
       }

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

       private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

       private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
       private void TxtDescripcionPreviewTextInput(object sender, TextCompositionEventArgs e)
       {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
       }

       #endregion Eventos

       #region Métodos
       private void InicializaContexto()
        {
            Contexto = new TipoCreditoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Activo = EstatusEnum.Activo
            };
        }
        
       private bool ValidaGuardar()
       {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || txtDescripcion.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCreditoEdicion_DescripcionRequerida;
                    txtDescripcion.Focus();
                }       
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCreditoEdicion_ActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int id = Extensor.ValorEntero(txtID.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var pl = new TipoCreditoPL();
                    TipoCreditoInfo info = pl.TipoCredito_ObtenerPorDescripcion(descripcion);

                    if (info != null && (id == 0 || id != info.TipoCreditoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TipoCreditoEdicion_DescripcionExistente, info.TipoCreditoID);
                        txtDescripcion.Text = string.Empty;
                        txtDescripcion.Focus();
                    }
                    else {
                        var config = pl.TipoCredito_ValidarConfiguracion(id);
                        if (config.ConfiguracionCreditoID > 0)
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.TipoCreditoEdicion_TipoCreditoConfigurado, config.ConfiguracionCreditoID);
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
        
       private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var pl = new TipoCreditoPL();
                    pl.TipoCredito_Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TipoCreditoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.TipoCreditoEdicion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.TipoCreditoEdicion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
       
       #endregion Métodos
    }
}