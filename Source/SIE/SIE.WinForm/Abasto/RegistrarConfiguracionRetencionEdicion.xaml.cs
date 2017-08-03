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
using System.Collections.Generic;
using System.Linq;

namespace SIE.WinForm.Abasto
{
    public partial class RegistrarConfiguracionRetencionEdicion
    {
       #region Propiedades

        private ConfiguracionCreditoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ConfiguracionCreditoInfo)DataContext;
            }
            set { DataContext = value; }
        }
        private bool esEdicion = false;
        private bool confirmaSalir = true;
        #endregion Propiedades

       #region Constructores

        public RegistrarConfiguracionRetencionEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarComboEstatus();
            CargarComboTipoCredito();
            CargarComboMesesPorVencer();
            cmbCredito.Focus();
        }

        public RegistrarConfiguracionRetencionEdicion(ConfiguracionCreditoInfo info)
        {
           InitializeComponent();
           CargarComboEstatus();
           CargarComboTipoCredito();
           CargarComboMesesPorVencer();
           info.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = info;
           esEdicion = true;
           cmbCredito.IsEnabled = false;
           cmbMeses.Focus();
        }

        #endregion Constructores

       #region Eventos
       private void Window_Loaded(object sender, RoutedEventArgs e)
       {
           if (esEdicion)
           {
               gridDatos.ItemsSource = ObtenerRetenciones();
           }

           if (cmbCredito.Items.Count <= 1)
           {
               SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_SinTiposCredito, MessageBoxButton.OK, MessageImage.Error);
               HabilitarControles(false);
           }

           if (cmbMeses.Items.Count <= 1)
           {
               SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_SinMesesPorVencer, MessageBoxButton.OK, MessageImage.Error);
               HabilitarControles(false);
           }
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
            if (ValidaGuardar())
            {
                Guardar();
            } 
        }

       private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       private void Generar_Click(object sender, RoutedEventArgs e)
       {
           var seleccion = (PlazoCreditoInfo)cmbMeses.SelectedItem;
            if (seleccion.PlazoCreditoID > 0)
            {
                CalcularRenglones();
            }
            else
            {
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ValidarMesesPorVencer, MessageBoxButton.OK, MessageImage.Warning);
                cmbMeses.Focus();
            }          
       }
        
       private void cmbMeses_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
       {
           Contexto.Retenciones = new List<ConfiguracionCreditoRetencionesInfo>();   
       }
       
       #endregion Eventos

       #region Métodos
       private void InicializaContexto()
        {
            Contexto = new ConfiguracionCreditoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Activo = EstatusEnum.Activo,
                TipoCredito = new TipoCreditoInfo(),
                PlazoCredito = new PlazoCreditoInfo(),
                Retenciones = new List<ConfiguracionCreditoRetencionesInfo>()
            };
        }

       private void CargarComboEstatus()
       {
           IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
           cmbEstatus.ItemsSource = estatusEnums;
           cmbEstatus.SelectedItem = EstatusEnum.Activo;
       }

       private void CargarComboTipoCredito()
       {
           var pl = new TipoCreditoPL();
           var tipoCreditoInicial = new TipoCreditoInfo { 
                                                            Activo = EstatusEnum.Activo,
                                                            Descripcion = String.Format(" Seleccione"),
                                                            TipoCreditoID = 0 
                                                        };
           var tiposCredito = pl.TipoCredito_ObtenerTodos();
           if (tiposCredito == null)
           {
               tiposCredito = new List<TipoCreditoInfo>();
           }
           
           tiposCredito.Add(tipoCreditoInicial);
           cmbCredito.ItemsSource = tiposCredito.Where(item => item.Activo == EstatusEnum.Activo).OrderBy(item => item.Descripcion);
           cmbCredito.SelectedItem = tipoCreditoInicial;
       }

       private void CargarComboMesesPorVencer()
       {
           var pl = new PlazoCreditoPL();
           var plazoCreditoInicial = new PlazoCreditoInfo
           {
               Activo = EstatusEnum.Activo,
               Descripcion = String.Format(" Seleccione"),
               PlazoCreditoID = 0
           };
           var plazoCredito = pl.PlazoCredito_ObtenerTodos();
           if (plazoCredito == null)
           {
               plazoCredito = new List<PlazoCreditoInfo>();
           }
           plazoCredito.Add(plazoCreditoInicial);
           cmbMeses.ItemsSource = plazoCredito.Where(item => item.Activo == EstatusEnum.Activo).OrderBy(item => item.Descripcion);
           cmbMeses.SelectedItem = plazoCreditoInicial;  
       }

       private bool ValidaGuardar()
       {
            bool resultado = false;

            var seleccion = (TipoCreditoInfo)cmbCredito.SelectedItem;
            var seleccionMes = (PlazoCreditoInfo)cmbMeses.SelectedItem;
            if (seleccion.TipoCreditoID > 0)
            {
                if (seleccionMes.PlazoCreditoID > 0)
                {
                    if (gridDatos.Items.Count > 0)
                    {
                        if(Contexto.Retenciones.Where(item => item.PorcentajeRetencion < 0 || item.PorcentajeRetencion > 100).Any())
                        {
                            SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ValidarPorcentaje, MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else
                        {
                            if (Contexto.Retenciones.Where(item => item.PorcentajeRetencion == 0).ToList().Count > 1)
                            {
                                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_DobleCero, MessageBoxButton.OK, MessageImage.Warning);
                            }
                            else
                            {
                                if (ValidarConfiguracionExistente())
                                {
                                    resultado = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ValidarRetenciones, MessageBoxButton.OK, MessageImage.Warning);
                        btnGenerar.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ValidarMesesPorVencer, MessageBoxButton.OK, MessageImage.Warning);
                    cmbMeses.Focus();
                }
            }
            else
            {
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ValidarTipoCredito, MessageBoxButton.OK, MessageImage.Warning);
                cmbCredito.Focus();
            }    
           
            return resultado;
       }
        
       private void Guardar()
       {
            try
            {
                var pl = new ConfiguracionCreditoPL();                
                if (pl.ConfiguracionCredito_Guardar(Contexto))
                {
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    confirmaSalir = false;
                    Close();    
                }                   
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
       
        private void CalcularRenglones()
        {
            var datos = new List<ConfiguracionCreditoRetencionesInfo>();
            for (int i = Convert.ToInt32(cmbMeses.Text); i > 0; i--)
            {
                var retencion = new ConfiguracionCreditoRetencionesInfo { NumeroMes = i, PorcentajeRetencion = 0};
                datos.Add(retencion);
            }
            Contexto.Retenciones = datos;
        }

        private bool ValidarConfiguracionExistente()
        {
            var resultado = false;
            try
            {
                if (Contexto.ConfiguracionCreditoID == 0)
                {
                    var pl = new ConfiguracionCreditoPL();
                    var id = pl.ConfiguracionCredito_ObtenerPorTipoCreditoYMes(Contexto);
                    if (id > 0)
                    {
                        resultado = false;
                        SkMessageBox.Show(this, string.Format(Properties.Resources.ConfigurarRetencion_ConfiguracionExistente, id), MessageBoxButton.OK, MessageImage.Warning);
                    }
                    else
                    {
                        resultado = true;
                    }
                }
                else {
                    resultado = true;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorValidarConfiguracion, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorValidarConfiguracion, MessageBoxButton.OK, MessageImage.Error);
            }

            return resultado;
        }

        private List<ConfiguracionCreditoRetencionesInfo> ObtenerRetenciones()
        {
            var resultado = new List<ConfiguracionCreditoRetencionesInfo>();
            try
            {
                var pl = new ConfiguracionCreditoPL();
                resultado = pl.ConfiguracionCredito_ObtenerRetencionesPorID(Contexto.ConfiguracionCreditoID);
                if (!resultado.Any())
                {
                    SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorObtenerRetenciones, MessageBoxButton.OK, MessageImage.Error);
                }                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorObtenerRetenciones, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfigurarRetencion_ErrorObtenerRetenciones, MessageBoxButton.OK, MessageImage.Error);
            }

            return resultado;
        }
    
        private void HabilitarControles(bool habilitar)
        {
            cmbCredito.IsEnabled = habilitar;
            cmbEstatus.IsEnabled = habilitar;
            cmbMeses.IsEnabled = habilitar;
            btnGenerar.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            confirmaSalir = habilitar;
            gridDatos.IsEnabled = habilitar;
        }

       #endregion Métodos

    }
}