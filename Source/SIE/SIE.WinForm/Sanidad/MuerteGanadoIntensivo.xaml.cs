using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Base.Log;


namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para MuerteGanadoIntensivo.xaml
    /// </summary>
    public partial class MuerteGanadoIntensivo
    {
        #region Atributos

        private int organiazcionID;

        /// <summary>
        /// Contenedor de la clase
        /// </summary>
        private GanadoIntensivoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (GanadoIntensivoInfo)DataContext;
            }
            set { DataContext = value; }
        }

       #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        public MuerteGanadoIntensivo()
        {
            organiazcionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            InitializeComponent();
            InicializaContexto();
            cmbFechaMuerteGanado.SelectedDate = DateTime.Now;
            cmbFechaMuerteGanado.IsEnabled = false;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            var listaTipoCorral = new List<TipoCorralInfo>
            {
                new TipoCorralInfo() {TipoCorralID = TipoCorral.Intensivo.GetHashCode()},
                new TipoCorralInfo() {TipoCorralID = TipoCorral.Maquila.GetHashCode()}
            };

            Contexto = new GanadoIntensivoInfo
            {
                 UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                 Organizacion =  new OrganizacionInfo
                 {
                     OrganizacionID =  organiazcionID
                 },
                 Activo =  EstatusEnum.Activo,
                 Corral =  new CorralInfo
                 {
                     ListaTipoCorral = listaTipoCorral,
                     Organizacion = new OrganizacionInfo
                     {
                         OrganizacionID = organiazcionID
                     },
                     Activo = EstatusEnum.Activo
                 },
                 TotalCabezas = 0,
                 Importe = 0,
                 Observaciones = string.Empty,
                 TipoMovimientoID = TipoMovimiento.MuerteGanadoIntensivo,
                 TipoFolio = TipoFolio.MuerteGanadoIntensivo

            };
            skAyudaCorral.ObjetoNegocio = new CorralPL();
            skAyudaCorral.AyudaConDatos += (sender, args) =>
            {
                Contexto.Corral.ListaTipoCorral = listaTipoCorral;
            };
        }


        /// <summary>
        /// Validar Guardado
        /// </summary>
        /// <returns></returns>
        public bool ValidarGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if(Contexto.Corral.Codigo == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.MuerteGanadoIntensivo_msgCorralRequerido;
                }
                else if (Contexto.TotalCabezas == 0 )
                { 
                    resultado = false;
                    mensaje = Properties.Resources.MuerteGanadoIntensivo_msgTotalCabezas;
                }
                else if (Contexto.Cabezas == 0 || string.IsNullOrEmpty(txtCabezas.Text))
                {
                    resultado = false;
                    Contexto.CabezasText = "0";
                    mensaje = Properties.Resources.MuerteGanadoIntensivo_msgCabezasRequerido;
                }
                else if (string.IsNullOrEmpty(Contexto.Observaciones))
                {
                    resultado = false;
                    mensaje = Properties.Resources.MuerteGanadoIntensivo_msgObservacionesRequerido;
                }
              
            }
                
            catch (Exception ex)
            {

                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }
        /// <summary>
        /// Obtener muerte ganado intensivo
        /// </summary>
        /// <returns></returns>
        private GanadoIntensivoInfo ObtenerDatosMuerteGanadoIntensivo()
        {

            GanadoIntensivoInfo ganadoIntensivoInfo = null;
            try
            {
                var muerteGanadoIntensivoPL = new GanadoIntensivoPL();
                ganadoIntensivoInfo =
                muerteGanadoIntensivoPL.ObtenerMuerteGanadoIntensivo(Contexto.Corral);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarUsuarioOperador, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            return ganadoIntensivoInfo;
        }
        /// <summary>
        /// Metodo para calcular costos de cabezas
        /// </summary>
        private void CalcularCostosDeCabezas()
        {
                GanadoIntensivoInfo muerteGanadoIntensivoInfo = null;
                try
                {
                    if (!string.IsNullOrEmpty(txtCabezas.Text) || Contexto.Cabezas > 0)
                    {
                        if (ValidarCabezas())
                        {
                            var muerteGanadoIntensivoPL = new GanadoIntensivoPL();
                            muerteGanadoIntensivoInfo =
                                muerteGanadoIntensivoPL.CalcularCostosDeCabezas(Contexto);
                            //InicializaContexto();
                            Contexto.Importe = muerteGanadoIntensivoInfo.Importe;
                            Contexto.ListaGanadoIntensivoCosto = muerteGanadoIntensivoInfo.ListaGanadoIntensivoCosto;
                        }
                        else
                        {
                            SkMessageBox.Show(Properties.Resources.MuerteGanadoIntensivo_msgNumCabezasInvalido,
                                MessageBoxButton.OK, MessageImage.Warning);
                            Contexto.CabezasText = "0";
                        }
                    }
                    else
                    {
                        Contexto.CabezasText = "0";
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_ErrorConsultarUsuarioOperador, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
        }

        /// <summary>
        /// Valida total de cabezas del lote
        /// </summary>
        /// <returns></returns>
        private bool ValidarCabezas()
        {
            return Contexto.TotalCabezas >= Contexto.Cabezas;
        }
       
        #endregion

        #region Eventos

        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCabezas_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Valida los espacios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCabezas_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarGuardar())
            {
                var ganadoIntensivo = new GanadoIntensivoPL();
                var resultado = ganadoIntensivo.MuerteGanadoIntensivo_Guardar(Contexto);

                if (resultado.stream != null)
                {
                    var exportarPoliza = new ExportarPoliza();
                    exportarPoliza.ImprimirPoliza(resultado.stream,
                                                  string.Format("{0} {1}", "Poliza Salida Muerte Ganado Intensivo Folio No.",
                                                                resultado.FolioTicket));
                }

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], String.Format(Properties.Resources.MuerteGanadoIntensivo_msgGuardadoConExito,resultado.FolioTicket),// folio.ToString(CultureInfo.InvariantCulture)),
                                    MessageBoxButton.OK, MessageImage.Correct);
                InicializaContexto();
            }

            
        }

        /// <summary>
        /// Evento cuando la ayuda corral pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkAyudaCorral_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Contexto.Corral.CorralID != 0)
            {
                GanadoIntensivoInfo ganadoIntensivoInfo = ObtenerDatosMuerteGanadoIntensivo();
                if (ganadoIntensivoInfo != null)
                {
                    if (ganadoIntensivoInfo.Lote != null)
                    {
                        if (ganadoIntensivoInfo.EntradaGanado != null)
                        {
                            Contexto.Importe = 0;
                            Contexto.Observaciones = string.Empty;
                            Contexto.CabezasText="0";
                            Contexto.EntradaGanado = ganadoIntensivoInfo.EntradaGanado;
                            Contexto.EntradaGanadoCosteo = ganadoIntensivoInfo.EntradaGanadoCosteo;
                            Contexto.Lote = ganadoIntensivoInfo.Lote;
                            Contexto.TotalCabezas = ganadoIntensivoInfo.TotalCabezas;
                        }
                        else
                        {
                            SkMessageBox.Show(Properties.Resources.MuerteGanadoIntensivo_MsjNoTieneEntradaGanado,
                               MessageBoxButton.OK, MessageImage.Warning);
                            InicializaContexto();
                            skAyudaCorral.Focus();
                        }
                        

                    }
                    else
                    {
                        SkMessageBox.Show(Properties.Resources.MuerteGanadoIntensivo_MsjCorralSinLote,
                                    MessageBoxButton.OK, MessageImage.Warning);
                        InicializaContexto();
                        skAyudaCorral.Focus();
                    }
                    
                   
                }
                else
                {
                    
                    InicializaContexto();
                    skAyudaCorral.Focus();
                }
            }
            else
            {
                InicializaContexto();
                skAyudaCorral.Focus();
            }
           
        }

        /// <summary>
        /// Evento boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.MuerteGanadoIntensivo_msgCancelarCaptura,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializaContexto();
            }

        }
        #endregion

        private void TxtCabezas_OnLostFocus(object sender, RoutedEventArgs e)
        {
            
            CalcularCostosDeCabezas();
        }
    }
}
