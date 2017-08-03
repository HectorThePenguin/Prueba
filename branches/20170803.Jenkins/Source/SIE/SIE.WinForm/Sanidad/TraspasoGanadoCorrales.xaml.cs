using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Seguridad;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TipoCorral = SIE.Services.Info.Enums.TipoCorral;
using TipoMovimiento = SIE.Services.Info.Enums.TipoMovimiento;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para TraspasoGanadoCorrales.xaml
    /// </summary>
    public partial class TraspasoGanadoCorrales
    {

        #region Atributos
        private int organizacionID;
        private List<AnimalInfo> listaAnimales;
        private int usuario;
        private CorralInfo corralInfoDestino;
        private CorralInfo corralInfoOrigen;
        #endregion

        #region Constructor
        public TraspasoGanadoCorrales()
        {
            InitializeComponent();
            DeshabilitarControles();
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
        }
        #endregion

        #region Eventos
        //Evento cargar pantalla
        private void ucTitulo_Loaded(object sender, RoutedEventArgs e)
        {
            txtCorralOrigen.Focus();
        }

        //Evento KeyDown de txtCorralOrigen
        private void txtCorralOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    e.Handled = true;
                }
                else
                {
                    LimpiarPantalla(false);

                    //Verificar existencia del corral
                    var corralPl = new CorralPL();
                    var resultado = corralPl.ObtenerExistenciaCorral(organizacionID, txtCorralOrigen.Text);
                    if (resultado == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoCorral_CorralNoExiste,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        e.Handled = true;
                    }
                    else
                    {
                        //Verificar si corral es de enfermeria
                        var corralInfo = new CorralInfo
                        {
                            Codigo = txtCorralOrigen.Text,
                            GrupoCorral = (int)GrupoCorralEnum.Enfermeria,
                            Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                        };
                        corralInfo = corralPl.ObtenerPorCodigoGrupoCorral(corralInfo);
                        if (corralInfo == null)
                        {
                            //Verificar si corral es de recepcion
                            corralInfo = new CorralInfo
                            {
                                Codigo = txtCorralOrigen.Text,
                                GrupoCorral = (int)GrupoCorralEnum.Produccion,
                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                            };

                            corralInfo = corralPl.ObtenerPorCodigoGrupoCorral(corralInfo);
                            if (corralInfo == null)
                            {
                                corralInfo = new CorralInfo
                                {
                                    Codigo = txtCorralOrigen.Text,
                                    GrupoCorral = (int)GrupoCorralEnum.Recepcion,
                                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                                };

                                corralInfo = corralPl.ObtenerPorCodigoGrupoCorral(corralInfo);
                                if (corralInfo == null)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.TraspasoGanadoCorral_CorralNoEsEnfermeria,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                    e.Handled = true;
                                }
                                else
                                {
                                    corralInfoOrigen = corralInfo;
                                    corralInfoOrigen.OrganizacionId = organizacionID;
                                    var animalPL = new AnimalPL();
                                    listaAnimales = animalPL.ObtenerAnimalesRecepcionPorCodigoCorral(corralInfo);
                                    if (listaAnimales != null)
                                    {
                                        lisBoxCorralOrigen.ItemsSource = listaAnimales;
                                        txtCorralDestino.IsEnabled = true;
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.TraspasoGanadoCorral_CorralNoTieneLoteActivo,
                                            MessageBoxButton.OK,
                                            MessageImage.Warning);
                                        e.Handled = true;
                                    }
                                }
                            }
                            else
                            {
                                corralInfoOrigen = corralInfo;
                                corralInfoOrigen.OrganizacionId = organizacionID;
                                var animalPL = new AnimalPL();
                                listaAnimales = animalPL.ObtenerAnimalesPorCodigoCorral(corralInfo);
                                if (listaAnimales != null)
                                {
                                    lisBoxCorralOrigen.ItemsSource = listaAnimales;
                                    txtCorralDestino.IsEnabled = true;
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.TraspasoGanadoCorral_CorralNoTieneLoteActivo,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                    e.Handled = true;
                                }
                            }
                        }
                        else
                        {
                            if (corralInfo.TipoCorral.TipoCorralID == (int) TipoCorral.CronicoVentaMuerte)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.TraspasoGanadoCorral_CorralEsCronicoVentaMuerte,
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                e.Handled = true;
                            }
                            else
                            {
                                //Obtener animales para mostrar Arete en list
                                var animalPL = new AnimalPL();
                                listaAnimales = animalPL.ObtenerAnimalesPorCodigoCorral(corralInfo);
                                corralInfoOrigen = corralInfo;
                                corralInfoOrigen.OrganizacionId = organizacionID;
                                if (listaAnimales != null)
                                {
                                    lisBoxCorralOrigen.ItemsSource = listaAnimales;
                                    txtCorralDestino.IsEnabled = true;
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.TraspasoGanadoCorral_CorralNoTieneLoteActivo,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        //Evento KeyDown de txtCorralDestino
        private void txtCorralDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    e.Handled = true;
                }
                else
                {
                    //Corral destino debe ser diferente a corral origen
                    if (txtCorralDestino.Text.Equals(txtCorralOrigen.Text))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoCorral_CorralDestinoDiferenteOrigen,
                        MessageBoxButton.OK, MessageImage.Warning);
                        e.Handled = true;
                    }
                    else
                    {
                        //Verificar existencia del corral
                        var corralPl = new CorralPL();
                        var resultado = corralPl.ObtenerExistenciaCorral(organizacionID, txtCorralDestino.Text);
                        if (resultado == null)
                        {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoCorral_CorralNoExiste,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        e.Handled = true;
                        }
                        else
                        {
                            if (!ValidarCorralDestino())
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                HabilitarControles();
                                txtCorralDestino.IsEnabled = false;
                            }
                        }
                    }
                }
            }
        }

        //Evento click de btnTraspasoUnoDerecha
        private void btnTraspasoUnoDerecha_Click(object sender, RoutedEventArgs e)
        {
            //Verificar disponibilidad corral
            if (ValidarCapacidadCorralDestino(false))
            {

                var aretesSeleccionadosOrigen = lisBoxCorralOrigen.SelectedItems.Cast<AnimalInfo>().ToList();
                var aretesOrigen = lisBoxCorralOrigen.Items.Cast<AnimalInfo>().ToList();
                var aretesDestino = lisBoxCorralDestino.Items.Cast<AnimalInfo>().ToList();

                aretesDestino.AddRange(aretesSeleccionadosOrigen);

                foreach (var animal in aretesSeleccionadosOrigen)
                {
                    aretesOrigen.Remove(animal);
                }

                lisBoxCorralOrigen.ItemsSource = aretesOrigen;
                lisBoxCorralDestino.ItemsSource = aretesDestino;

                btnGuardar.IsEnabled = lisBoxCorralDestino.Items.Count > 0;
            }
        }

        //Evento click de btnTraspasoTodosDerecha
        private void btnTraspasoTodosDerecha_Click(object sender, RoutedEventArgs e)
        {
            //Verificar disponibilidad corral
            if (ValidarCapacidadCorralDestino(true))
            {
                var aretesOrigen = lisBoxCorralOrigen.Items.Cast<AnimalInfo>().ToList();
                var aretesDestino = lisBoxCorralDestino.Items.Cast<AnimalInfo>().ToList();

                aretesDestino.AddRange(aretesOrigen);

                lisBoxCorralOrigen.ItemsSource = null;
                lisBoxCorralDestino.ItemsSource = aretesDestino;

                    btnGuardar.IsEnabled = lisBoxCorralDestino.Items.Count > 0;
            }
        }

        //Evento click de btnTraspasoUnoIzquierda
        private void btnTraspasoUnoIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var aretesSeleccionadosDestino = lisBoxCorralDestino.SelectedItems.Cast<AnimalInfo>().ToList();
            var aretesOrigen = lisBoxCorralOrigen.Items.Cast<AnimalInfo>().ToList();
            var aretesDestino = lisBoxCorralDestino.Items.Cast<AnimalInfo>().ToList();

            aretesOrigen.AddRange(aretesSeleccionadosDestino);

            foreach (var animal in aretesSeleccionadosDestino)
            {
                aretesDestino.Remove(animal);
            }

            lisBoxCorralOrigen.ItemsSource = aretesOrigen;
            lisBoxCorralDestino.ItemsSource = aretesDestino;

                btnGuardar.IsEnabled = lisBoxCorralDestino.Items.Count > 0;
        }

        //Evento click de btnTraspasoTodosIzquierda
        private void btnTraspasoTodosIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var aretesOrigen = lisBoxCorralOrigen.Items.Cast<AnimalInfo>().ToList();
            var aretesDestino = lisBoxCorralDestino.Items.Cast<AnimalInfo>().ToList();

            aretesOrigen.AddRange(aretesDestino);

            lisBoxCorralOrigen.ItemsSource = aretesOrigen;
            lisBoxCorralDestino.ItemsSource = null;

                btnGuardar.IsEnabled = lisBoxCorralDestino.Items.Count > 0;
        }

        //Evento SelectionChanged de lisBoxCorralOrigen
        private void lisBoxCorralOrigen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aretesSeleccionados = lisBoxCorralOrigen.SelectedItems;
            if (aretesSeleccionados.Count > 0 && txtCorralDestino.Text.Trim() != String.Empty)
            {
                btnTraspasoUnoDerecha.IsEnabled = true;
            }
            else
            {
                btnTraspasoUnoDerecha.IsEnabled = false;
            }
        }

        //Evento SelectionChanged de lisBoxCorralDestino
        private void lisBoxCorralDestino_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnTraspasoUnoIzquierda.IsEnabled = lisBoxCorralDestino.SelectedItems.Count > 0;
        }

        private void Guardar()
        {
            try
            {
                if (this.ValidarParametroCorralDestino())
                {
                    if (this.ValidarValorParametroCorralDestino())
                    {
                        LimpiarPantalla(true);
                        DeshabilitarControles();
                        txtCorralOrigen.Focus();
                        return;
                    }
                }


                var traspasoGanadoCorralesPL = new TraspasoGanadoCorralesPL();
                var aretesTotal = lisBoxCorralDestino.Items.Cast<AnimalInfo>().ToList();
                var usuarioInfo = new UsuarioInfo
                {
                    UsuarioID = usuario,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                };
                if (corralInfoOrigen.GrupoCorral == GrupoCorralEnum.Recepcion.GetHashCode())
                {
                    if (traspasoGanadoCorralesPL.GuardarTraspasoGanadoCorralesRecepcion(corralInfoOrigen, corralInfoDestino,
                        usuarioInfo))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_SeguardoCorrectamenteElCorte,
                            MessageBoxButton.OK, MessageImage.Correct);
                        btnGuardar.IsEnabled = true;
                        imgloading.Visibility = Visibility.Hidden;
                        LimpiarPantalla(true);
                        DeshabilitarControles();
                        txtCorralOrigen.Focus();

                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                        btnGuardar.IsEnabled = true;
                        imgloading.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if (traspasoGanadoCorralesPL.GuardarTraspasoGanadoCorrales(aretesTotal, corralInfoDestino,
                        usuarioInfo))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_SeguardoCorrectamenteElCorte,
                            MessageBoxButton.OK, MessageImage.Correct);
                        btnGuardar.IsEnabled = true;
                        imgloading.Visibility = Visibility.Hidden;
                        LimpiarPantalla(true);
                        DeshabilitarControles();
                        txtCorralOrigen.Focus();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                        btnGuardar.IsEnabled = true;
                        imgloading.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Error(error);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], error.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        //Evento click de btnGuardar
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGuardar.IsEnabled = false;
                imgloading.Visibility = Visibility.Visible;
                Dispatcher.BeginInvoke((Action) (Guardar), DispatcherPriority.Background);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                btnGuardar.IsEnabled = true;
                imgloading.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                btnGuardar.IsEnabled = true;
                imgloading.Visibility = Visibility.Hidden;
            }
        }

        //Evento click de btnCancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Cancelarcaptura_CorteGanado,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarPantalla(true);
                DeshabilitarControles();
                txtCorralOrigen.Focus();
            }
        }

        //Evento KeyUp de txtCorralOrigen
        private void txtCorralOrigen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                LimpiarPantalla(false);
            }
        }

        //Evento PreviewTextInput de txtCorralOrigen
        private void txtCorralOrigen_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        //Evento PreviewTextInput de txtCorralDestino
        private void txtCorralDestino_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        #endregion

        #region Metodos
        //Metodo que deshabilita los controles
        private void DeshabilitarControles()
        {
            btnTraspasoUnoDerecha.IsEnabled = false;
            btnTraspasoUnoIzquierda.IsEnabled = false;
            btnTraspasoTodosDerecha.IsEnabled = false;
            btnTraspasoTodosIzquierda.IsEnabled = false;
            txtCorralDestino.IsEnabled = false;
            lisBoxCorralOrigen.IsEnabled = false;
            lisBoxCorralDestino.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        //Metodo que habilita los controles
        private void HabilitarControles()
        {
            txtCorralDestino.IsEnabled = true;
            lisBoxCorralOrigen.IsEnabled = true;
            lisBoxCorralDestino.IsEnabled = true;
            btnTraspasoTodosDerecha.IsEnabled = true;
            btnTraspasoTodosIzquierda.IsEnabled = true;
        }

        //Metodo que limpia la pantalla
        private void LimpiarPantalla(bool cambioNoIndividual)
        {
            if (cambioNoIndividual)
            {
               txtCorralOrigen.Text = String.Empty;
                corralInfoOrigen = new CorralInfo();
            }
            lisBoxCorralOrigen.ItemsSource = null;
            btnTraspasoUnoDerecha.IsEnabled = false;
            btnTraspasoTodosDerecha.IsEnabled = false;
            btnTraspasoUnoIzquierda.IsEnabled = false;
            btnTraspasoTodosIzquierda.IsEnabled = false;
            lisBoxCorralDestino.ItemsSource = null;
            txtCorralDestino.Text = String.Empty;
            txtCorralDestino.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        //Metodo que valida que corral destino tenga capacidad y no este cerrado
        private bool ValidarCorralDestino()
        {
            var corralValido = true;
            //Verificar que corral sea grupo enfermeria
            var corralInfo = new CorralInfo
            {
                Codigo = txtCorralDestino.Text,
                GrupoCorral = corralInfoOrigen.GrupoCorral,
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
            };
            var corralPL = new CorralPL();
            corralInfoDestino = corralPL.ObtenerPorCodigoGrupoCorral(corralInfo);
            if (corralInfoDestino != null)
            {
                if(corralInfoDestino.TipoCorral.TipoCorralID == TipoCorral.CronicoVentaMuerte.GetHashCode())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.TraspasoGanadoCorrales_CorralCronico,
                       MessageBoxButton.OK, MessageImage.Warning);
                    corralValido = false;
                }
                var lotePL = new LotePL();
                var resultadoLote = lotePL.ObtenerPorCorral(organizacionID, corralInfoDestino.CorralID);
                if (resultadoLote != null)
                {
                    //Verificar capacidad de corral destino
                    if (!(resultadoLote.Cabezas < corralInfoDestino.Capacidad))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                        MessageBoxButton.OK, MessageImage.Warning);
                        corralValido = false;
                    }
                }
                else
                {
                    var resultadoLoteCerrado = lotePL.ObtenerPorCorralCerrado(organizacionID, corralInfoDestino.CorralID);
                    if (resultadoLoteCerrado != null)
                    {
                        //Corral destino cerrado
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_CorralDestinoCerrado,
                        MessageBoxButton.OK, MessageImage.Warning);
                        corralValido = false;
                    }
                }
            }
            else
            {
                if (corralInfoOrigen.GrupoCorral == GrupoCorralEnum.Enfermeria.GetHashCode())
                {
                    //Corral no es de grupo enfermeria
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoCorral_CorralNoEsEnfermeria,
                        MessageBoxButton.OK, MessageImage.Warning);
                    corralValido = false;
                }
                else if (corralInfoOrigen.GrupoCorral == GrupoCorralEnum.Produccion.GetHashCode())
                {
                    //Corral no es de grupo produccion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoCorral_CorralNoEsProduccion,
                        MessageBoxButton.OK, MessageImage.Warning);
                    corralValido = false;
                }
                else if (corralInfoOrigen.GrupoCorral == GrupoCorralEnum.Recepcion.GetHashCode())
                {
                    //Corral no es de grupo recepcion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoCorral_CorralNoEsRecepcion,
                        MessageBoxButton.OK, MessageImage.Warning);
                    corralValido = false;
                }
            }
            return corralValido;
        }

        //Metodo que valida que corral destino tenga capacidad
        private bool ValidarCapacidadCorralDestino(bool todosDerecha)
        {
            var corralConCapacidad = true;
            int sumatoriaCabezas;
            var aretesSeleccionadosOrigen = lisBoxCorralOrigen.SelectedItems;
            var aretesTotalOrigen = lisBoxCorralOrigen.Items.Count;
            var aretesDestino = lisBoxCorralDestino.Items.Count;

            //Corral grupo enfermeria
            var lotePL = new LotePL();
            var resultadoLote = lotePL.ObtenerPorCorral(organizacionID, corralInfoDestino.CorralID);
            if (resultadoLote != null)
            {
                
                if (todosDerecha)
                {
                    sumatoriaCabezas = resultadoLote.Cabezas + aretesDestino + aretesTotalOrigen;
                    if (!((sumatoriaCabezas) <= corralInfoDestino.Capacidad))
                    {
                        //Corral no tiene capacidad
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                            MessageBoxButton.OK, MessageImage.Warning);
                        corralConCapacidad = false;
                    }
                }
                else
                {
                    sumatoriaCabezas = resultadoLote.Cabezas + aretesDestino + aretesSeleccionadosOrigen.Count;
                    if (!((sumatoriaCabezas) <= corralInfoDestino.Capacidad))
                    {
                        //Corral no tiene capacidad
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                            MessageBoxButton.OK, MessageImage.Warning);
                        corralConCapacidad = false;
                    }
                }
            }
            else
            {
                if (todosDerecha)
                {
                    sumatoriaCabezas = aretesDestino + aretesTotalOrigen;
                    if (!((sumatoriaCabezas) <= corralInfoDestino.Capacidad))
                    {
                        //Corral no tiene capacidad
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                            MessageBoxButton.OK, MessageImage.Warning);
                        corralConCapacidad = false;
                    }
                }
                else
                {
                    sumatoriaCabezas = aretesDestino + aretesSeleccionadosOrigen.Count;
                    if (!((sumatoriaCabezas) <= corralInfoDestino.Capacidad))
                    {
                        //Corral no tiene capacidad
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                            MessageBoxButton.OK, MessageImage.Warning);
                        corralConCapacidad = false;
                    }
                }
            }
            return corralConCapacidad;
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
                    if (tipoCorral == this.corralInfoOrigen.TipoCorral.TipoCorralID.ToString())
                    {
                        encontrado = true;
                    }
                }

                if (encontrado)
                {
                    imgloading.Visibility = Visibility.Hidden;
                    txtCorralDestino.Focus();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoGanadoCorrales_MsgCorralNoConfigurado, MessageBoxButton.OK,
                                       MessageImage.Stop);
                    txtCorralDestino.Clear();
                    txtCorralDestino.IsEnabled = true;
                    corralInfoDestino = new CorralInfo();

                    //
                    CorralPL corralpl = new CorralPL();
                    CorralInfo corral = corralpl.ObtenerCorralActivoPorCodigo(organizacionID, txtCorralOrigen.Text.Trim());
                    var corralInfo = new CorralInfo
                    {
                        Codigo = txtCorralOrigen.Text,
                        GrupoCorral = corral.GrupoCorral,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },

                    };
                    corralInfoOrigen = corralInfo;
                    var animalPL = new AnimalPL();
                    listaAnimales = animalPL.ObtenerAnimalesPorCodigoCorral(corralInfo);
                    if (listaAnimales != null)
                    {
                        lisBoxCorralDestino.ItemsSource = null;
                        lisBoxCorralOrigen.ItemsSource = listaAnimales;
                        txtCorralDestino.IsEnabled = true;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoCorral_CorralNoTieneLoteActivo,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                    }
                    btnGuardar.IsEnabled = false;
                    //

                }
                return encontrado;
            }
            return false;
        }

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

        #endregion
     }
}
