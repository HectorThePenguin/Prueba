using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;
using System.Transactions;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para PaseGanadoIndividual.xaml
    /// </summary>
    public partial class TrasferenciaGanadoIndividual
    {
        #region Constructor

        private int organizacionID;
        private int usuario;
        private AnimalInfo animal;
        private CorralInfo corralOrigen;
        private CorralInfo corralDestino;

        public TrasferenciaGanadoIndividual()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void TrasferenciaGanadoIndividual_Loaded(object sender, RoutedEventArgs e)
        {
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
        }

        /// <summary>
        /// Evento del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValdiarCamposObligatoriosEnBlanco())
                {
                    if (ValidarValorParametroCorralDestino())
                    {
                        return;
                    }

                    var transferenciaPL = new TransferenciaGanadoIndividualPL();

                    if (ckbCompensacion.IsChecked != null && ckbCompensacion.IsChecked.Value)
                    {
                        // Se guarda una compensacion
                        if (dgAretesDelCorral.SelectedItem != null)
                        {
                            var animalCompensado = (AnimalInfo)dgAretesDelCorral.SelectedItem;
                            // Guarda una transferencia de ganado
                            using (var scope = new TransactionScope())
                            {
                                // como es intercambio no se decrementan las cabezas
                                transferenciaPL.GuardarTransferenciaGanadoCompensacion(animal, corralDestino,
                                                                                       animalCompensado, corralOrigen,
                                                                                       usuario, false);
                                scope.Complete();
                            }
                        }
                        else
                        {
                            // no se encuentra seleccionado el arete para realizar la compensacion
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.TrasferenciaGanadoIndividual_SeleccionarAreteGrid,
                               MessageBoxButton.OK, MessageImage.Warning);
                            dgAretesDelCorral.Focus();
                            return;
                        }

                    }
                    else
                    {
                        // Guarda una transferencia de ganado
                        using (var scope = new TransactionScope())
                        {
                            transferenciaPL.GuardarTransferenciaGanado(animal, corralDestino, usuario, true);
                            scope.Complete();
                        }
                    }
                    // Se limpia pantalla y se muestra mensaje de éxito 
                    Limpiar(true);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteGanado_SeguardoCorrectamenteElCorte,
                                MessageBoxButton.OK, MessageImage.Correct);
                    txtArete.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.TrasferenciaGanadoIndividual_ErrorGuardar,
                MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento click del boton cancenlar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.TrasferenciaGanadoIndividual_Cancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Limpiar(true);
                txtArete.Focus();
            }

        }

        /// <summary>
        /// Evento para cuando pierde el focus el txtArete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtArete_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtArete.Text))
            {
                if (!ValidarArete())
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtArete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!String.IsNullOrEmpty(txtArete.Text))
                {
                    if (!ValidarArete())
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_CapturarArete,
                        MessageBoxButton.OK, MessageImage.Warning);

                    txtArete.Focusable = true;
                    txtArete.Focus();
                    txtArete.Text = String.Empty;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento del corral destino KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!String.IsNullOrEmpty(txtCorralDestino.Text))
                {
                    if (!ValidarCorralDestino())
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_CapturarCorral,
                        MessageBoxButton.OK, MessageImage.Warning);

                    txtCorralDestino.Focusable = true;
                    txtCorralDestino.Focus();
                    txtCorralDestino.Text = String.Empty;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento para cuando se pierde el focus el txtCorralDestino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCorralDestino.Text))
            {
                if (!ValidarCorralDestino())
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento del check para cuando se chekea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbCompensacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ValdiarCamposObligatoriosEnBlanco())
            {
                //Obtener animales del corral destino
                var animalPl = new AnimalPL();
                var animales = animalPl.ObtenerAnimalesPorCorral(corralDestino, organizacionID);
                if (animales != null)
                {
                    dgAretesDelCorral.ItemsSource = animales;
                }
                grbAretesCorral.Visibility = Visibility.Visible;
                // dgAretesDelCorral
            }
            else
            {
                ckbCompensacion.IsChecked = false;
            }
        }

        /// <summary>
        /// Evento del check para cuando se deschekea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbCompensacion_Unchecked(object sender, RoutedEventArgs e)
        {
            ckbCompensacion.IsChecked = false;
            grbAretesCorral.Visibility = Visibility.Hidden;

        }

        #endregion

        #region Metodos

        /// <summary>
        /// Valida la el parametro y la configuracion del parametro de tipoCorralDestino
        /// </summary>
        /// <returns>Regresa true si el parametro esta registrado y esta configurado como activo para la organizacion del usuario logueado.</returns>
        private bool ValidarParametroCorralDestino()
        {
            try
            {
                Logger.Info();
                bool parametroConfigurado = true;
                ParametroPL parametroPl = new ParametroPL();
                ParametroOrganizacionPL parametroOrganizacionPl = new ParametroOrganizacionPL();
                List<ParametroInfo> parametros = parametroPl.ObtenerTodos(EstatusEnum.Activo).ToList();
                ParametroInfo parametroCorralDestino = null;
                if (parametros != null)
                {
                    parametroCorralDestino = parametros.Where(parametro => parametro.Clave == ParametrosEnum.CORRALDESTINOORGANIZACION.ToString()).FirstOrDefault();
                }
                if (parametroCorralDestino == null)
                {
                    parametroConfigurado = false;
                }
                else
                {
                    ParametroOrganizacionInfo parametroValor = parametroOrganizacionPl.ObtenerPorOrganizacionIDClaveParametro(int.Parse(Application.Current.Properties["OrganizacionID"].ToString()), ParametrosEnum.CORRALDESTINOORGANIZACION.ToString());
                    if (parametroValor == null || parametroValor.Activo == EstatusEnum.Inactivo)
                    {
                        parametroConfigurado = false;
                    }
                    else
                    {
                        parametroConfigurado = true;
                    }
                }
                return parametroConfigurado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
        }
     
        /// <summary>
        /// Valida si el tipo de corral destino esta configurado como invalido.
        /// </summary>
        /// <returns>Regresa True si el tipoCorralID esta configurado como invalido en el parametro</returns>
        private bool ValidarValorParametroCorralDestino() 
        {
            if (ValidarParametroCorralDestino()) 
            {
                ParametroOrganizacionInfo parametroValor = new ParametroOrganizacionPL().ObtenerPorOrganizacionIDClaveParametro(int.Parse(Application.Current.Properties["OrganizacionID"].ToString()), ParametrosEnum.CORRALDESTINOORGANIZACION.ToString());
               
                string[] TiposCorralConfigurados = parametroValor.Valor.Split('|');
                bool encontrado = false;
                foreach (string tipoCorral in TiposCorralConfigurados)
                {
                    if (tipoCorral == corralOrigen.TipoCorral.TipoCorralID.ToString())
                    {
                        encontrado = true;
                    }
                }

                if (encontrado)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoGanadoCorrales_MsgCorralNoConfigurado, MessageBoxButton.OK,
                                       MessageImage.Stop);
                    txtCorralDestino.Clear();
                    this.corralDestino = new CorralInfo();
                    txtCorralDestino.Focus();
                    BtnGuardar.IsEnabled = true;
                }
                return encontrado;
            }
            return false;

        }

        /// <summary>
        /// Metodo para hacer las validaciones del arete
        /// </summary>
        /// <returns></returns>
        private bool ValidarArete()
        {
            try
            {
                var animalInfo = new AnimalInfo
                {
                    Arete = txtArete.Text,
                    OrganizacionIDEntrada = organizacionID
                };
                //Buscar el animal en el inventario
                var corteGanadoPl = new CorteGanadoPL();
                animalInfo = corteGanadoPl.ExisteAreteEnPartida(animalInfo);
                if (animalInfo != null)
                {
                    //Se obtiene el ultimo movimiento
                    var animalPL = new AnimalPL();
                    var ultimoMovimiento = animalPL.ObtenerUltimoMovimientoAnimal(animalInfo);
                    if (ultimoMovimiento != null)
                    {
                        //Se obtiene el corral en el que se encuentra
                        var corralPL = new CorralPL();
                        var corralInfo = corralPL.ObtenerPorId(ultimoMovimiento.CorralID);
                        if (corralInfo != null)
                        {
                            //Se obtiene el Corral Origen
                            txtCorralOrigen.Text = corralInfo.Codigo;

                            var lotePL = new LotePL();
                            //var loteOrigen = new LoteInfo
                            //                 {
                            //                     CorralID = corralInfo.CorralID,
                            //                     OrganizacionID = organizacionID
                            //                 };
                            LoteInfo loteOrigen = lotePL.ObtenerPorId(ultimoMovimiento.LoteID);
                            if (loteOrigen == null || loteOrigen.Activo == EstatusEnum.Inactivo)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.TrasferenciaGanadoIndividual_LoteInactivo,
                                                    MessageBoxButton.OK, MessageImage.Warning);
                                txtArete.Focusable = true;
                                txtArete.Focus();
                                txtArete.Text = String.Empty;
                                return false;
                            }
                        }
                        //Si existe se obtienen los datos del animal
                        txtPesoOrigen.Text = animalInfo.PesoCompra.ToString(CultureInfo.InvariantCulture);
                        var tipoGanadoPl = new TipoGanadoPL();
                        var tipoGanado = tipoGanadoPl.ObtenerPorID(animalInfo.TipoGanadoID);
                        if (tipoGanado != null)
                        {
                            //Se obtiene el tipo de ganado y sexo
                            txtSexo.Text = tipoGanado.Sexo.ToString();
                        }
                        //Se inicializa la info del animal de forma global
                        animal = animalInfo;
                        corralOrigen = corralInfo;
                        txtAreteMetalico.Text = animal.AreteMetalico;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_AreteNoExiste,
                        MessageBoxButton.OK, MessageImage.Warning);
                        txtArete.Focusable = true;
                        txtArete.Focus();
                        txtArete.Text = String.Empty;
                        return false;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TrasferenciaGanadoIndividual_AreteNoExiste,
                    MessageBoxButton.OK, MessageImage.Warning);
                    txtArete.Focusable = true;
                    txtArete.Focus();
                    txtArete.Text = String.Empty;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return true;
        }
        /// <summary>
        /// Metodo para validar el corral destino
        /// </summary>
        private bool ValidarCorralDestino()
        {
            try
            {
                //Buscar el corral en el inventario
                var corralPL = new CorralPL();
                var corralInfo = corralPL.ObtenerCorralPorCodigo(organizacionID, txtCorralDestino.Text);
                if (corralInfo != null)
                {
                    if (corralInfo.GrupoCorral == (int)GrupoCorralEnum.Enfermeria ||
                        corralInfo.GrupoCorral == (int)GrupoCorralEnum.Produccion)
                    {
                        // Obtener el lote del corral
                        var lotePL = new LotePL();
                        var lote = lotePL.ObtenerPorCorralCerrado(organizacionID, corralInfo.CorralID);

                        if (lote != null)
                        {
                            //Si existe se obtienen los datos del corral
                            corralDestino = corralInfo;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TrasferenciaGanadoIndividual_GrupoCorralSinLote,
                            MessageBoxButton.OK, MessageImage.Warning);
                            txtCorralDestino.Text = String.Empty;
                            txtCorralDestino.Focusable = true;
                            txtCorralDestino.Focus();
                            //e.Handled = true;
                            return false;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_GrupoCorralInvalido,
                        MessageBoxButton.OK, MessageImage.Warning);
                        txtCorralDestino.Focusable = true;
                        txtCorralDestino.Focus();
                        txtCorralDestino.Text = String.Empty;
                        //e.Handled = true;
                        return false;
                    }


                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_CorralNoExiste,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtCorralDestino.Focusable = true;
                    txtCorralDestino.Focus();
                    txtCorralDestino.Text = String.Empty;
                    //e.Handled = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return true;
        }

        /// <summary>
        /// Metodo para validar los campos obligatorios en blanco
        /// </summary>
        /// <returns></returns>
        private bool ValdiarCamposObligatoriosEnBlanco()
        {
            // Valdiar si se capturo el arete
            if (String.IsNullOrEmpty(txtArete.Text) && String.IsNullOrEmpty(txtAreteMetalico.Text))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TrasferenciaGanadoIndividual_CapturarArete,
                    MessageBoxButton.OK, MessageImage.Warning);
                txtArete.Focus();
                return false;
            }

            // Valdiar si se capturo el corral destino
            if (String.IsNullOrEmpty(txtCorralDestino.Text) || corralDestino == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TrasferenciaGanadoIndividual_CapturarCorral,
                    MessageBoxButton.OK, MessageImage.Warning);
                txtCorralDestino.Text = String.Empty;
                txtCorralDestino.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo para limpiar pantalla
        /// </summary>
        private void Limpiar(bool completo)
        {
            txtArete.Text = String.Empty;
            txtAreteMetalico.Text = String.Empty;

            txtPesoOrigen.Text = String.Empty;
            txtSexo.Text = String.Empty;
            txtCorralDestino.Text = String.Empty;
            txtCorralOrigen.Text = String.Empty;

            dgAretesDelCorral.ItemsSource = null;

            ckbCompensacion.IsChecked = false;
            grbAretesCorral.Visibility = Visibility.Hidden;

            animal = null;
            corralOrigen = null;
            corralDestino = null;

        }

        /// <summary>
        /// Evento para cuando se elimina el arete capturado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtArete_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                try
                {
                    Limpiar(false);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }

        /// <summary>
        /// Evento para cuando de borra el corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                try
                {
                    txtCorralDestino.Focusable = true;
                    txtCorralDestino.Focus();
                    //txtCorralDestino.Text = String.Empty;
                    ckbCompensacion.IsChecked = false;
                    grbAretesCorral.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }

        #endregion

        private void txtAreteMetalico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!String.IsNullOrEmpty(txtAreteMetalico.Text))
                {
                    if (!ValidarAreteMetalico())
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_CapturarArete,
                        MessageBoxButton.OK, MessageImage.Warning);

                    txtAreteMetalico.Focusable = true;
                    txtAreteMetalico.Focus();
                    txtAreteMetalico.Text = String.Empty;
                    e.Handled = true;
                }
            }
        }

        private void txtAreteMetalico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                try
                {
                    Limpiar(false);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }

        private void txtAreteMetalico_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtAreteMetalico.Text))
            {
                if (!ValidarAreteMetalico())
                {
                    e.Handled = true;
                }
            }
        }

        private bool ValidarAreteMetalico()
        {
            try
            {
                var animalInfo = new AnimalInfo
                {
                    AreteMetalico = txtAreteMetalico.Text,
                    OrganizacionIDEntrada = organizacionID
                };
                //Buscar el animal en el inventario
                var corteGanadoPl = new CorteGanadoPL();
                animalInfo = corteGanadoPl.ExisteAreteMetalicoEnPartida(animalInfo);
                if (animalInfo != null)
                {
                    //Se obtiene el ultimo movimiento
                    var animalPL = new AnimalPL();
                    var ultimoMovimiento = animalPL.ObtenerUltimoMovimientoAnimal(animalInfo);
                    if (ultimoMovimiento != null)
                    {
                        //Se obtiene el corral en el que se encuentra
                        var corralPL = new CorralPL();
                        var corralInfo = corralPL.ObtenerPorId(ultimoMovimiento.CorralID);
                        if (corralInfo != null)
                        {
                            //Se obtiene el Corral Origen
                            txtCorralOrigen.Text = corralInfo.Codigo;
                            var lotePL = new LotePL();
                            //var loteOrigen = new LoteInfo
                            //                 {
                            //                     CorralID = corralInfo.CorralID,
                            //                     OrganizacionID = organizacionID
                            //                 };
                            LoteInfo loteOrigen = lotePL.ObtenerPorId(ultimoMovimiento.LoteID);
                            if (loteOrigen == null || loteOrigen.Activo == EstatusEnum.Inactivo)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.TrasferenciaGanadoIndividual_LoteInactivo,
                                                    MessageBoxButton.OK, MessageImage.Warning);
                                txtAreteMetalico.Focusable = true;
                                txtAreteMetalico.Focus();
                                txtAreteMetalico.Clear();
                                return false;
                            }
                        }

                        //Si existe se obtienen los datos del animal
                        txtPesoOrigen.Text = animalInfo.PesoCompra.ToString(CultureInfo.InvariantCulture);
                        var tipoGanadoPl = new TipoGanadoPL();
                        var tipoGanado = tipoGanadoPl.ObtenerPorID(animalInfo.TipoGanadoID);
                        if (tipoGanado != null)
                        {
                            //Se obtiene el tipo de ganado y sexo
                            txtSexo.Text = tipoGanado.Sexo.ToString();
                        }
                        //Se inicializa la info del animal de forma global
                        animal = animalInfo;
                        corralOrigen = corralInfo;
                        txtArete.Text = animal.Arete;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrasferenciaGanadoIndividual_AreteNoExiste,
                        MessageBoxButton.OK, MessageImage.Warning);
                        txtAreteMetalico.Focusable = true;
                        txtAreteMetalico.Focus();
                        txtAreteMetalico.Clear();
                        return false;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TrasferenciaGanadoIndividual_AreteNoExiste,
                    MessageBoxButton.OK, MessageImage.Warning);
                    txtAreteMetalico.Focusable = true;
                    txtAreteMetalico.Focus();
                    txtAreteMetalico.Clear();
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return true;
        }
    }
}
