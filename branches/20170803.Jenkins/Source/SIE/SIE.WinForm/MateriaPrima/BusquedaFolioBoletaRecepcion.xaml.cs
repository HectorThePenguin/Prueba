using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para BusquedaFolioBoletaRecepcion.xaml
    /// </summary>
    public partial class BusquedaFolioBoletaRecepcion : Window
    {
        private int organizacionId;

        public bool Seleccionado { get; set; }
        public EntradaProductoInfo EntradaProducto { get; set; }

        public BusquedaFolioBoletaRecepcion()
        {
            InitializeComponent();
            Seleccionado = false;
        }

        private void DataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }

        /// <summary>
        /// Asigna los valores seleccionados en el grid
        /// </summary>
        private void AsignarValoresSeleccionadosGrid()
        {
            var renglonSeleccionado = dgFoliosEntradaProducto.SelectedItem as EntradaProductoInfo;
            if (renglonSeleccionado != null)
            {
                EntradaProducto = renglonSeleccionado;
                Seleccionado = true;
                Close();
            }
        }

        private void ConsultarFoliosValidos(int folio)
        {
            try
            {
                var entradaProductoValido = new EntradaProductoPL();
                List<EntradaProductoInfo> entradas = entradaProductoValido.ObtenerEntradaProductoValidoAyuda(new EntradaProductoInfo
                { 
                    Organizacion = new OrganizacionInfo
                    { 
                        OrganizacionID = organizacionId
                    },
                    Folio = folio,
                    TipoContrato = new TipoContratoInfo
                    {
                        TipoContratoId = (int)TipoContratoEnum.BodegaExterna
                    }
                });

                dgFoliosEntradaProducto.ItemsSource = entradas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BusquedaFolioBoletaRecepcion_MsgErrorConsulta,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());

            ConsultarFoliosValidos(0);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            int folio = 0;
            try
            {
                if (txtFolio.Text.Trim() != string.Empty)
                {
                    folio = int.Parse(txtFolio.Text.Trim());
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgFolioFueraDeRango, MessageBoxButton.OK, MessageImage.Warning);
                txtFolio.Text = string.Empty;
                txtFolio.Focus();
            }
            ConsultarFoliosValidos(folio);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }

        private void txtFolio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtFolio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
    }
}
