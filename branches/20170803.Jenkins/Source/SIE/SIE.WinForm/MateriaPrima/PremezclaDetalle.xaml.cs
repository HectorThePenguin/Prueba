using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Info;


namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para PremezclaDetalle.xaml
    /// </summary>
    public partial class PremezclaDetalle
    {
        public List<PremezclaDetalleInfo> ListaDetallePremezcla = null;
        public PremezclaDetalle()
        {
            InitializeComponent();
        }

        public PremezclaDetalle(List<PremezclaDetalleInfo> detallePremezcla,
            decimal pesoNeto)
        {
            InitializeComponent();

            foreach (var premezclaDetalleInfo in detallePremezcla)
            {
                premezclaDetalleInfo.Kilogramos = (pesoNeto * premezclaDetalleInfo.Porcentaje)/100;
            }

            gridPremezcla.ItemsSource = detallePremezcla;
            ListaDetallePremezcla = detallePremezcla;
        }

        /// <summary>
        /// Evento que se activa cuando se modifica el combo de lotes para asignarselo a la organizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var combo = (ComboBox)sender;
                var ingredientePremezcla = (PremezclaDetalleInfo)gridPremezcla.SelectedItem;
                ingredientePremezcla.Lote = (AlmacenInventarioLoteInfo)combo.SelectedItem;
                /*
                if (ValidarCapacidadLote(ingredientePremezcla))
                {
                    //gridPremezcla.ItemsSource = null;
                    gridPremezcla.ItemsSource = gridPremezcla.ItemsSource;
                }
                */
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void gridPremezcla_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var ingredientePremezcla = (PremezclaDetalleInfo)e.Row.Item;
                if(ValidarCapacidadLote(ingredientePremezcla))
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private bool ValidarCapacidadLote(PremezclaDetalleInfo ingredientePremezcla)
        {
            try
            {
                if (ingredientePremezcla != null && ingredientePremezcla.Lote != null)
                {
                    if (ingredientePremezcla.Lote.AlmacenInventarioLoteId > 0)
                        if (ingredientePremezcla.Lote.Cantidad < ingredientePremezcla.Kilogramos)
                        {
                            return true;
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
