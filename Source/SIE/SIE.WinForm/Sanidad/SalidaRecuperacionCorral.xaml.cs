using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TipoCorral = SIE.Services.Info.Enums.TipoCorral;
using TipoMovimiento = SIE.Services.Info.Enums.TipoMovimiento;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para SalidaRecupecionCorral.xaml
    /// </summary>
    public partial class SalidaRecuperacionCorral
    {
        #region Atributos

        private readonly int organizacionId;
        private List<AnimalInfo> animales;
        private readonly int usuario;
        private int corralIdDestino;
        private bool banderaRetrocesoOrigen;
        private bool banderaRetrocesoDestino;
        private int totalOrigen;
        private int totalDestino;
        private   List<AnimalInfo> listaAnimalesCorral;
        private List<AnimalInfo> listaAnimalesCorraleta;
        private List<AnimalInfo> ListaTotal;
        private int corralIdOrigen;

        #endregion


        #region Constructor
        public SalidaRecuperacionCorral()
        {
            ListaTotal = new List<AnimalInfo>();
            listaAnimalesCorraleta=new List<AnimalInfo>();
            listaAnimalesCorral = new List<AnimalInfo>();
            totalDestino = 0;
            totalOrigen = 0;
            banderaRetrocesoOrigen = false;
            banderaRetrocesoDestino = false;
            corralIdDestino = 0;
            InitializeComponent();
            
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            EstablecerControles(false);
        }
        #endregion

        #region Metodos
       
        /// <summary>
        /// Validar Corral
        /// </summary>
        /// <param name="e"></param>
        public void ValidarCorral(KeyEventArgs e)
        {
           if (ExisteCorralOrigen())
            {
                //Validar si el corral origen es de tipo enfermeria
                banderaRetrocesoOrigen = true;
                LimpiarCaptura();
                banderaRetrocesoOrigen = false;
                var corralPl = new CorralPL();
                var bandera = false;
                var corralInfo = new CorralInfo
                {
                    Codigo = txtCorralOrigen.Text,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    GrupoCorral = (int)GrupoCorralEnum.Enfermeria
                };
                if (lisBoxCorralOrigen.Items.Count > 0)
                {
                  lisBoxCorralOrigen.ItemsSource=null;
                  listaAnimalesCorral = new List<AnimalInfo>();
                }
                corralInfo = corralPl.ValidarCorralEnfermeria(corralInfo);
                if (corralInfo != null)
                {
                    if (corralInfo.TipoCorral.TipoCorralID != (int) TipoCorral.CronicoVentaMuerte)
                    {
                        corralIdOrigen = corralInfo.CorralID;
                        corralInfo = new CorralInfo
                                         {
                                             Codigo = txtCorralOrigen.Text,
                                             Organizacion = new OrganizacionInfo {OrganizacionID = organizacionId},
                                             GrupoCorral = (int) GrupoCorralEnum.Enfermeria
                                         };
                        var animalPl = new AnimalPL();
                        animales = animalPl.ObtenerAnimalesPorCodigoCorral(corralInfo);
                        if (animales != null)
                        {
                            listaAnimalesCorral.AddRange(animales);
                            LlenarAretesOrigen(listaAnimalesCorral);
                            ListaTotal.AddRange(listaAnimalesCorral);
                            bandera = true;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.SalidaRecuperacionCorral_CorralOrigenNoAretes,
                                              MessageBoxButton.OK,
                                              MessageImage.Warning);
                            EstablecerControles(false);
                            LimpiarCaptura();
                            e.Handled = true;
                        }

                        if (bandera)
                        {
                            txtCorralDestino.IsEnabled = true;
                        }
                        else
                        {
                            btnGuardar.IsEnabled = false;
                            btnTraspasoTodosDerecha.IsEnabled = false;
                            btnTraspasoTodosIzquierda.IsEnabled = false;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.SalidaRecuperacionCorral_CorralTipoCronicoVenta,
                                          MessageBoxButton.OK,
                                          MessageImage.Warning);

                        LimpiarCaptura();
                        EstablecerControles(false);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaRecuperacionCorral_CorralOrigenNoEnfermeria,
                        MessageBoxButton.OK,
                        MessageImage.Warning);

                    LimpiarCaptura();
                    EstablecerControles(false);

                    e.Handled = true;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                 Properties.Resources.SalidaRecuperacionCorral_CorralOrigenNoExiste,
                 MessageBoxButton.OK,
                 MessageImage.Warning);

                LimpiarCaptura();
                EstablecerControles(false);

                e.Handled = true;
            }
        }

        /// <summary>
        /// Validar corral destino
        /// </summary>
        /// <param name="e"></param>
        private void ValidarCorralDestino(KeyEventArgs e)
        {
            var corralInfo = ExisteCorralDestino();
            if (corralInfo != null)
            {
                corralIdDestino = corralInfo.CorralID;
                if (corralInfo.TipoCorral.TipoCorralID == (int) TipoCorral.CorraletaRecuperado)
                {
                    BtnTraspasos();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.SalidaRecuperacionCorral_CorralNoCorraletaManejo,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                    e.Handled = true;
                    txtCorralDestino.Clear();
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SalidaRecuperacionCorral_CorraletaDestinoNoExiste,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
                txtCorralDestino.Clear();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Traspasar aretes corral
        /// </summary>
        public void BtnTraspasos()
        {
            btnTraspasoTodosDerecha.IsEnabled = lisBoxCorralOrigen.Items.Count > 0;
            btnTraspasoTodosIzquierda.IsEnabled = lisBoxCorralDestino.Items.Count > 0;
            if (lisBoxCorralDestino.Items.Count == totalDestino && lisBoxCorralOrigen.Items.Count == totalOrigen)
            {
                btnGuardar.IsEnabled = false;
            }
            else
            {
                btnGuardar.IsEnabled = true;
            }
        }

        /// <summary>
        /// Validar si existe corral destino
        /// </summary>
        /// <returns></returns>
        private CorralInfo ExisteCorralDestino()
        {
            var corralPl = new CorralPL();
            var corralInfo = corralPl.ObtenerExistenciaCorral(organizacionId, txtCorralDestino.Text);
            return corralInfo;
        }

        /// <summary>
        /// Validar si existe corral origen
        /// </summary>
        /// <returns></returns>
        private bool ExisteCorralOrigen()
        {
            var corralPl = new CorralPL();
            var corralInfo = corralPl.ObtenerExistenciaCorral(organizacionId, txtCorralOrigen.Text);
            return corralInfo != null;
        }

        /// <summary>
        /// Guardar la salida por recuperacion
        /// </summary>
        /// <returns></returns>
        private bool GuardarSalidaRecuperacion()
        {
            var regreso = false;
            if (lisBoxCorralDestino.Items.Count > 0)
            {
                var lotePL = new LotePL();
                var lote = new LoteInfo
                               {
                                   Corral = new CorralInfo
                                                {
                                                    CorralID = corralIdOrigen
                                                },
                                   CorralID = corralIdOrigen,
                                   OrganizacionID = organizacionId
                               };
                lote = lotePL.ObtenerPorCorralID(lote);
                var animalMovimientoPL = new AnimalMovimientoPL();
                List<AnimalMovimientoInfo> movimientosAnimal = animalMovimientoPL.ObtenerUltimoMovimientoAnimal(animales);
                if (movimientosAnimal != null)
                {
                    var loteDestino = new LoteInfo
                                          {
                                              Corral = new CorralInfo
                                                           {
                                                               CorralID = corralIdDestino
                                                           },
                                              CorralID = corralIdDestino,
                                              OrganizacionID = organizacionId
                                          };
                    loteDestino = lotePL.ObtenerPorCorralID(loteDestino);
                    movimientosAnimal.ForEach(datos =>
                                                  {
                                                      datos.LoteID = loteDestino.LoteID;
                                                      datos.CorralID = corralIdDestino;
                                                      datos.TipoMovimientoID =
                                                          TipoMovimiento.SalidaEnfermeria.GetHashCode();
                                                      datos.Activo = EstatusEnum.Activo;
                                                      datos.UsuarioCreacionID = usuario;
                                                      datos.UsuarioModificacionID = usuario;
                                                  });
                    lote.Cabezas -= movimientosAnimal.Count;
                    loteDestino.Cabezas += movimientosAnimal.Count;
                    using (var scope = new TransactionScope())
                    {
                        animalMovimientoPL.GuardarAnimalMovimientoXML(movimientosAnimal);
                        var filtro = new FiltroActualizarCabezasLote
                        {
                            CabezasProcesadas = movimientosAnimal.Count,
                            LoteIDDestino = loteDestino.LoteID,
                            LoteIDOrigen = lote.LoteID,
                            UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                        };

                        lotePL.ActualizarCabezasProcesadas(filtro);
                        scope.Complete();
                    }
                    regreso = true;
                }
            }
            return regreso;
        }

        /// <summary>
        /// Limpiar datos de captura
        /// </summary>
        private void LimpiarCaptura()
        {
            if (!banderaRetrocesoOrigen)
            {
                txtCorralOrigen.Clear();
                txtCorralDestino.Clear();
            }
            if (!banderaRetrocesoDestino)
            {
                txtCorralDestino.Clear();
            }
            lisBoxCorralDestino.ItemsSource=null;
            lisBoxCorralOrigen.ItemsSource=null;

            txtCorralOrigen.Focus();
            listaAnimalesCorral = new List<AnimalInfo>();
            listaAnimalesCorraleta = new List<AnimalInfo>();
            ListaTotal = new List<AnimalInfo>();
        }

        /// <summary>
        /// Habilitar o deshabilitar controles del formulario
        /// </summary>
        private void EstablecerControles(bool habilitar)
        {
            txtCorralDestino.IsEnabled = false;
            txtCorralOrigen.IsEnabled = true;
            lisBoxCorralOrigen.IsEnabled = true;
            lisBoxCorralDestino.IsEnabled = true;
            btnTraspasoTodosDerecha.IsEnabled = habilitar;
            btnTraspasoTodosIzquierda.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            totalDestino = 0;
            totalOrigen = 0;
        }

        /// <summary>
        /// Cargar lista de aretes origen
        /// </summary>
        /// <param name="animales"></param>
        private void LlenarAretesOrigen(List<AnimalInfo> animales)
        {
            lisBoxCorralOrigen.ItemsSource = animales;
            totalOrigen = lisBoxCorralOrigen.Items.Count;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Bloque de copyPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BloqueoCopyPage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Enviar aretes a la lista derecha de corral destino
        /// </summary>
        private void EnviarDerecha()
        {
            lisBoxCorralDestino.ItemsSource = ListaTotal;
            lisBoxCorralOrigen.ItemsSource = null;
            BtnTraspasos();
            btnGuardar.Focus();
        }

        /// <summary>
        /// Enviar a la izquierda al corral origen
        /// </summary>
        private void EnviarIzquierda()
        {
            lisBoxCorralOrigen.ItemsSource = ListaTotal;
            lisBoxCorralDestino.ItemsSource = null;
            BtnTraspasos();
            btnGuardar.Focus();
        }

        /// <summary>
        /// Enviar aretes a la derecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTraspasoTodosDerecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnviarDerecha();
            }
        }

        /// <summary>
        /// Handler del clic del toton a la derecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTraspasoTodosDerecha_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = true;
            EnviarDerecha();
        }

        /// <summary>
        /// Hadler del clic del boton a la izquierda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTraspasoTodosIzquierda_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = true;
            EnviarIzquierda();
        }

        /// <summary>
        /// Regresar aretes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTraspasoTodosIzquierda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               EnviarIzquierda();
            }
        }

        /// <summary>
        /// Keydown de corral origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaRecuperacionCorral_CorralOrigenVacio,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                    txtCorralOrigen.Focus();
                    e.Handled = true;
                }
                else
                {
                    lisBoxCorralDestino.ItemsSource = null;
                    ValidarCorral(e);
                }
            }
        }

        /// <summary>
        /// Keydown corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrWhiteSpace(txtCorralDestino.Text))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.SalidaRecuperacionCorral_CorraletaDestinoVacio,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                    txtCorralDestino.Focus();
                    e.Handled = true;
                }
                else
                {
                    lisBoxCorralDestino.ItemsSource = null;
                    ValidarCorralDestino(e);
                }
            }
        }

        /// <summary>
        /// Handler del clic del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaRecuperacionCorral_Cancelar,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    LimpiarCaptura();
                    EstablecerControles(false);
                    txtCorralOrigen.IsEnabled = true;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Handler del clic del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lisBoxCorralOrigen.Items != null)
                {
                    if (GuardarSalidaRecuperacion())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaRecuperacionCorral_GuardarExito,
                        MessageBoxButton.OK, MessageImage.Correct);

                        EstablecerControles(false);
                        txtCorralOrigen.IsEnabled = true;
                        LimpiarCaptura();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaRecuperacionCorral_GuardarError,
                        MessageBoxButton.OK, MessageImage.Error);

                        EstablecerControles(false);
                        txtCorralOrigen.IsEnabled = true;
                        LimpiarCaptura();
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Handler para validar letras y numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralOrigen_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Handler para validar letras y numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        /// <summary>
        /// limpiar al dar clic en retroceso y suprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralOrigen_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                banderaRetrocesoOrigen = true;
                LimpiarCaptura();
                EstablecerControles(false);
                banderaRetrocesoOrigen = false;
            }
        }
        /// <summary>
        /// teclear retroseso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                banderaRetrocesoDestino = true;
                ListaTotal = new List<AnimalInfo>();

                lisBoxCorralDestino.ItemsSource=null;
                ValidarCorral(e);
                btnTraspasoTodosDerecha.IsEnabled = false;
                btnTraspasoTodosIzquierda.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                banderaRetrocesoDestino = false;
                txtCorralDestino.Focus();
            }
        }
        /// <summary>
        /// Bloque Copy Page en corral destino.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralOrigen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            BloqueoCopyPage(sender, e);
        }
        /// <summary>
        /// Bloqueo Copy Page en corraleta destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            BloqueoCopyPage(sender, e);
        }

        /// <summary>
        /// Evento inicio 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtCorralOrigen.Focus();
        }

        /// <summary>
        /// Limpiar captura al cambiar el texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralOrigen_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            banderaRetrocesoOrigen = true;
            LimpiarCaptura();
            banderaRetrocesoOrigen = false;
        }
        #endregion
    }
}
