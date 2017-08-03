using System;
using System.Collections.Generic;
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
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para ConsultarFactores.xaml
    /// </summary>
    public partial class ConsultarFactores : Window
    {
        /// <summary>
        /// Factoer seleccionado
        /// </summary>
        private int factorSeleccionado;
        /// <summary>
        /// Usuario id
        /// </summary>
        private int usuarioID;
        /// <summary>
        /// lista de calidad mezclado
        /// </summary>
        private List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="usuarioId"></param>
        public ConsultarFactores(int usuarioId)
        {
            usuarioID = usuarioId;
            factorSeleccionado = 0;
            listaCalidadMezcladoInfos = new List<CalidadMezcladoFactorInfo>();
            InitializeComponent();
            txtParticulasEsperadas.IsReadOnly = true;
            cargarGrid();
        }
        /// <summary>
        /// Cargar grid de inicio
        /// </summary>
        private void cargarGrid()
        {
            var mecladoraPl = new MezcladoraPL();
            
            listaCalidadMezcladoInfos = mecladoraPl.ObtenerCalidadMezcladoFactor();
            dgConsultarFactores.ItemsSource = listaCalidadMezcladoInfos;
        }

        /// <summary>
        /// Editar grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var calidadMezcladoFactorInfo in listaCalidadMezcladoInfos)
            {
                calidadMezcladoFactorInfo.PesoBHHabilitado = false;
                calidadMezcladoFactorInfo.PesoSHHabilitado = false;
            }
            if (dgConsultarFactores.SelectedIndex == 3)
            {
                txtParticulasEsperadas.IsEnabled = false;
                txtParticulasEsperadas.Text = string.Empty;
                return;
            }
            factorSeleccionado = dgConsultarFactores.SelectedIndex;
            listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].PesoBHHabilitado = true;
            listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].PesoSHHabilitado = true;
            listaCalidadMezcladoInfos[3].PesoBHHabilitado = true;
            listaCalidadMezcladoInfos[3].PesoSHHabilitado = true;
            txtParticulasEsperadas.Text =
                Convert.ToString(listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].Factor);
            txtParticulasEsperadas.IsEnabled = true;
            dgConsultarFactores.ItemsSource = new List<CalidadMezcladoFactorInfo>();
            dgConsultarFactores.ItemsSource = listaCalidadMezcladoInfos;
        }
        /// <summary>
        /// Genera calculos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBH_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var text = (sender as TextBox);
                if (text != null)
                {
                    if (text.Text != "")
                    {
                        var peso = text.Text.Replace(" ", string.Empty);
                        listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].PesoBH = Convert.ToInt32(peso);
                        listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].UsuarioModifica=usuarioID;
                        //private List<CalidadMezcladoFactorInfo> CalcularPesos(List<CalidadMezcladoFactorInfo> lista)
                        listaCalidadMezcladoInfos[3].PesoBHHabilitado = true;
                        listaCalidadMezcladoInfos[3].PesoSHHabilitado = true;
                        var listacalculada = calcularPesos(listaCalidadMezcladoInfos);
                        listaCalidadMezcladoInfos = new List<CalidadMezcladoFactorInfo>();
                        listaCalidadMezcladoInfos = listacalculada;
                        dgConsultarFactores.ItemsSource = listaCalidadMezcladoInfos;
                    }
                }
            }
        }
        /// <summary>
        /// Calcular peso
        /// </summary>
        /// <param name="calidadMezcladoFactorInfos"></param>
        /// <returns></returns>
        private List<CalidadMezcladoFactorInfo> calcularPesos(List<CalidadMezcladoFactorInfo> calidadMezcladoFactorInfos)
        {
            var mezcladoraPl = new MezcladoraPL();
            var listacalculada = mezcladoraPl.CalcularPesos(calidadMezcladoFactorInfos);
            return listacalculada;
        }
        /// <summary>
        /// Validad solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBH_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoSH_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Calcula datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoSH_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var text = (sender as TextBox);
                if (text != null)
                {
                    if (text.Text != "")
                    {
                        var peso = text.Text.Replace(" ", string.Empty);
                        listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].PesoBS = Convert.ToInt32(peso);
                        listaCalidadMezcladoInfos[dgConsultarFactores.SelectedIndex].UsuarioModifica = usuarioID;
                        listaCalidadMezcladoInfos[3].PesoBHHabilitado = true;
                        listaCalidadMezcladoInfos[3].PesoSHHabilitado = true;
                        //private List<CalidadMezcladoFactorInfo> CalcularPesos(List<CalidadMezcladoFactorInfo> lista)
                        var listacalculada = calcularPesos(listaCalidadMezcladoInfos);
                        listaCalidadMezcladoInfos = new List<CalidadMezcladoFactorInfo>();
                        listaCalidadMezcladoInfos = listacalculada;
                        dgConsultarFactores.ItemsSource = listaCalidadMezcladoInfos;
                    }
                }
            }
        }
        /// <summary>
        /// Calcula particulas esperadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtParticulasEsperadas_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var text = (sender as TextBox);
                if (text != null)
                {
                    if (text.Text != "")
                    {
                        var particulas = text.Text.Replace(" ", string.Empty);
                        if (ValidarDecimal(particulas))
                        {
                            listaCalidadMezcladoInfos[factorSeleccionado].Factor = Convert.ToDecimal(particulas);
                            listaCalidadMezcladoInfos[factorSeleccionado].UsuarioModifica = usuarioID;
                            listaCalidadMezcladoInfos[3].PesoBHHabilitado = true;
                            listaCalidadMezcladoInfos[3].PesoSHHabilitado = true;
                            dgConsultarFactores.ItemsSource = listaCalidadMezcladoInfos;
                            btnActualizar.Focus();
                            e.Handled = true;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.ConsultarFactores_CantidadParticulas,
                               MessageBoxButton.OK, MessageImage.Warning);
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// Validar formato decimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool ValidarDecimal(string text)
        {
            try
            {
                var dato = text.Split('.');
                if (dato.Count() > 2)
                {
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                Logger.Error(Ex);
                return false;
            }
        }
        /// <summary>
        /// Valida numeros con punto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtParticulasEsperadas_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
        }

        /// <summary>
        /// Actualiza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActualizar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mezcladoraPl = new MezcladoraPL();
                List<CalidadMezcladoFactorInfo> listaGuardar = listaCalidadMezcladoInfos.Where(calidadMezcladoFactorInfo => calidadMezcladoFactorInfo.UsuarioModifica != 0).ToList();
                if (listaGuardar.Count > 0)
                {
                    mezcladoraPl.GuardarConsultaFactor(listaGuardar);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_mgsGuardarExito,
                    MessageBoxButton.OK, MessageImage.Correct);
                    txtParticulasEsperadas.Text = string.Empty;
                    cargarGrid();
                    txtParticulasEsperadas.Focus();
                }


              
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError + ex.Message,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Click cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCanceñar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConsultarFactores_mgsCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
            
        }
    }
}
