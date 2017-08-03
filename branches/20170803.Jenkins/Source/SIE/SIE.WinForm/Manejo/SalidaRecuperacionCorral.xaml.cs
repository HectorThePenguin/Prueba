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
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para SalidaRecupecionCorral.xaml
    /// </summary>
    public partial class SalidaRecuperacionCorral
    {
        #region Atributos

        private int _organizacionId;
        private List<AnimalInfo> _Animales;
        #endregion

        #region Constructor
        public SalidaRecuperacionCorral()
        {

            InitializeComponent();
            EstablecerControlesFalse();
            _organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
        }
        #endregion

        #region Eventos
        private void txtCorralOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    txtCorralOrigen.Focus();
                    e.Handled = true;
                }
                else
                {
                    //Validar si el corral origen es de tipo enfermeria
                    var corralPl = new CorralPL();
                    var corralInfo = new CorralInfo
                    {
                        Codigo = txtCorralOrigen.Text,
                        TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Enfermeria, },
                        Organizacion = new OrganizacionInfo { OrganizacionID = _organizacionId }
                    };
                    corralInfo = corralPl.ObtenerPorCodigoCorral(corralInfo);
                    if (corralInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoCorral_CorralInvalido,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        txtCorralOrigen.Focus();
                        return;
                    }
                    else
                    {
                        corralPl = new CorralPL();
                        corralInfo = new CorralInfo
                        {
                            Codigo = txtCorralOrigen.Text,
                            Organizacion = new OrganizacionInfo { OrganizacionID = _organizacionId },
                            TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Enfermeria, }
                        };
                        var animalPL = new AnimalPL();
                        _Animales = animalPL.ObtenerAnimalesPorCodigoCorral(corralInfo);
                        if (_Animales != null)
                        {
                            LlenarAretesOrigen(_Animales);
                            txtCorralDestino.IsEnabled = true;
                            txtCorralDestino.Focus();    
                        }
                        else
                        {
                            
                        }
                        
                    }
                }
            }
        }

        private void txtCorralDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    txtCorralOrigen.Focus();
                    e.Handled = true;
                }
                else
                {
                    //Validar si el corral destino es de tipo enfermeria
                    var corralPl = new CorralPL();
                    var corralInfo = new CorralInfo
                    {
                        Codigo = txtCorralDestino.Text,
                        TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Enfermeria, },
                        Organizacion = new OrganizacionInfo { OrganizacionID = _organizacionId }
                    };
                    corralInfo = corralPl.ObtenerPorCodigoCorral(corralInfo);
                    if (corralInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoCorral_CorralInvalido,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        txtCorralDestino.Focus();
                        return;
                    }
                    else
                    {
                        corralPl = new CorralPL();
                        corralInfo = new CorralInfo
                        {
                            Codigo = txtCorralDestino.Text,
                            Organizacion = new OrganizacionInfo { OrganizacionID = _organizacionId },
                            TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Enfermeria, }
                        };
                        var animalPL = new AnimalPL();
                        _Animales = animalPL.ObtenerAnimalesPorCodigoCorral(corralInfo);
                        LlenarAretesDestino(_Animales);
                        EstablecerControlesTrue();
                        lisBoxCorralOrigen.SelectionMode = SelectionMode.Multiple;
                        lisBoxCorralDestino.SelectionMode = SelectionMode.Multiple;
                        txtCorralDestino.IsEnabled = false;
                    }
                }
            }
        }

        private void btnTraspasoUnoDerecha_Click(object sender, RoutedEventArgs e)
        {
            var aretesSeleccionados = lisBoxCorralOrigen.SelectedItems.Cast<String>().ToList();
            var aretesTotal = lisBoxCorralOrigen.Items;
            foreach (var animal in aretesSeleccionados)
            {
                lisBoxCorralDestino.Items.Add(animal.ToString());
            }
            foreach (var animal in aretesSeleccionados)
                lisBoxCorralOrigen.Items.Remove(animal);
        }

        private void btnTraspasoTodosDerecha_Click(object sender, RoutedEventArgs e)
        {
            var aretesTotal = lisBoxCorralOrigen.Items;
            foreach (var animal in aretesTotal)
            {
                lisBoxCorralDestino.Items.Add(animal.ToString());
            }
            lisBoxCorralOrigen.Items.Clear();
        }

        private void btnTraspasoUnoIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var aretesSeleccionados = lisBoxCorralDestino.SelectedItems.Cast<String>().ToList();
            foreach (var animal in aretesSeleccionados)
            {
                lisBoxCorralOrigen.Items.Add(animal.ToString());
            }
            foreach (var animal in aretesSeleccionados)
                lisBoxCorralDestino.Items.Remove(animal);
        }

        private void btnTraspasoTodosIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var aretesSeleccionados = lisBoxCorralDestino.Items;
            {
                foreach (var animal in aretesSeleccionados)
                    lisBoxCorralOrigen.Items.Add(animal.ToString());
            }
            lisBoxCorralDestino.Items.Clear();
        }





        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CosteoEntradaGanado_Cancelar,
                                      MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes)
                {
                    
                }
                else
                {
                    limpiarCaptura();
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void limpiarCaptura()
        {
            txtCorralDestino.Clear();
            txtCorralOrigen.Clear();
            lisBoxCorralDestino.Items.Clear();
            lisBoxCorralOrigen.Items.Clear();
            txtCorralOrigen.Focus();
        }

        #endregion


        #region Metodos
        private void EstablecerControlesFalse()
        {

            btnTraspasoTodosDerecha.IsEnabled = false;
            btnTraspasoTodosIzquierda.IsEnabled = false;
            txtCorralDestino.IsEnabled = false;
            lisBoxCorralOrigen.IsEnabled = false;
            lisBoxCorralDestino.IsEnabled = false;
        }

        private void EstablecerControlesTrue()
        {
            txtCorralDestino.IsEnabled = true;
            lisBoxCorralOrigen.IsEnabled = true;
            lisBoxCorralDestino.IsEnabled = true;
            btnTraspasoTodosDerecha.IsEnabled = true;
            btnTraspasoTodosIzquierda.IsEnabled = true;
        }

        

        private void LlenarAretesOrigen(List<AnimalInfo> animales)
        {
            foreach (var animal in animales)
            {
                lisBoxCorralOrigen.Items.Add(animal.Arete);
            }
        }

        private void LlenarAretesDestino(List<AnimalInfo> animales)
        {
            foreach (var animal in animales)
            {
                lisBoxCorralDestino.Items.Add(animal.Arete);
            }
        }
        #endregion
    }
}
