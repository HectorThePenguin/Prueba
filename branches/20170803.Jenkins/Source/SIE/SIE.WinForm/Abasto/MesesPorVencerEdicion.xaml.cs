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
    public partial class MesesPorVencerEdicion
    {
       #region Propiedades

        private PlazoCreditoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PlazoCreditoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private bool confirmaSalir = true;

        #endregion Propiedades

       #region Constructores

        public MesesPorVencerEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        public MesesPorVencerEdicion(PlazoCreditoInfo info)
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
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
       }

       #endregion Eventos

       #region Métodos
       private void InicializaContexto()
        {
            Contexto = new PlazoCreditoInfo
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
                    mensaje = Properties.Resources.MesesPorVencer_DescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else
                {
                    int mesTemporal = 0;
                    bool valido = int.TryParse(txtDescripcion.Text.Trim(), out mesTemporal);
                    if (valido == false || (valido && mesTemporal <= 0))
                    {
                        resultado = false;
                        mensaje = Properties.Resources.MesesPorVencer_DescripcionNoEntero;
                        txtDescripcion.Focus();
                    }
                    else if (cmbActivo.SelectedItem == null)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.MesesPorVencer_ActivoRequerida;
                        cmbActivo.Focus();
                    }
                    else
                    {
                        int id = Extensor.ValorEntero(txtID.Text);
                        string descripcion = txtDescripcion.Text.Trim();

                        var pl = new PlazoCreditoPL();
                        PlazoCreditoInfo info = pl.PlazoCredito_ObtenerPorDescripcion(Convert.ToInt32(descripcion).ToString());

                        if (info != null && (id == 0 || id != info.PlazoCreditoID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.MesesPorVencer_DescripcionExistente, info.PlazoCreditoID);
                            txtDescripcion.Text = string.Empty;
                            txtDescripcion.Focus();
                        }
                        else
                        {
                            var config = pl.PlazoCredito_ValidarConfiguracion(id);
                            if (config.ConfiguracionCreditoID > 0)
                            {
                                resultado = false;
                                mensaje = string.Format(Properties.Resources.MesesPorVencer_MesConfigurado, config.ConfiguracionCreditoID);
                            }
                            else {
                                Contexto.Descripcion = Convert.ToInt32(descripcion).ToString();
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
        
       private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var pl = new PlazoCreditoPL();
                    pl.PlazoCredito_Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.PlazoCreditoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.MesesPorVencer_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.MesesPorVencer_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
       
       #endregion Métodos

    }
}
