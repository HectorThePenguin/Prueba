using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para AyudaContrato.xaml
    /// </summary>
    public partial class AyudaContrato : Window
    {
        public int Folio;
        private int OrganizacionID;
        private bool confirmaSalir = true;
        internal bool Cancelado { get; set; }
        public AyudaContrato()
        {
            InitializeComponent();
            ConsultaContratos(1, 15);
            Folio = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="organizacionId"></param>
        public AyudaContrato(int organizacionId)
        {
            this.OrganizacionID = organizacionId;
            InitializeComponent();
            //ConsultaContratos(1, 15);
            Folio = 0;
        }
        /// <summary>
        /// Consulta contratos
        /// </summary>
        /// <param name="Inicio"></param>
        /// <param name="Final"></param>
        private void ConsultaContratos(int Inicio, int Final)
        {
            ContratoPL contratoPl = new ContratoPL();
            IList<ContratoInfo> listaContratos = null;

            try
            {
                var pagina = new PaginacionInfo {Inicio = Inicio, Limite = Final};
                var contrato = new ContratoInfo();
                if (txtFolio.Text != "")
                {
                    contrato.Folio = Convert.ToInt32(txtFolio.Text);
                }
                contrato.Organizacion = new OrganizacionInfo() { OrganizacionID = OrganizacionID };
                contrato.TipoFlete = new TipoFleteInfo() { TipoFleteId = (int)TipoFleteEnum.PagoenGanadera };
                contrato.Activo = EstatusEnum.Activo;
                listaContratos = contratoPl.ObtenerPorPaginaSinProgramacion(pagina,contrato).Lista;

                if (listaContratos != null)
                {
                    dgContrato.ItemsSource = listaContratos;
                    if (txtFolio.Text == "")
                    {
                        List<ContratoInfo> listaContratosContador = contratoPl.ObtenerTodos().ToList();
                        if (listaContratosContador.Count > 0)
                        {
                            List<ContratoInfo> list = new List<ContratoInfo>();
                            foreach (ContratoInfo registro in listaContratosContador)
                            {
                                if (registro.TipoFlete.TipoFleteId == (int) TipoFleteEnum.PagoenGanadera)
                                    list.Add(registro);
                            }
                            PaginacionContrato.TotalRegistros = list.Count;
                        }
                        else
                        {
                            PaginacionContrato.TotalRegistros = 0;
                        }
                    }
                    else
                    {
                        PaginacionContrato.TotalRegistros =
                            contratoPl.ObtenerTodos()
                                .Where(
                                    registro =>
                                        registro.Folio == Convert.ToInt32(txtFolio.Text) &&
                                        registro.Organizacion.OrganizacionID == OrganizacionID &&
                                        registro.TipoFlete.TipoFleteId == (int) TipoFleteEnum.PagoenGanadera)
                                .ToList()
                                .Count;
                    }
                }
                else
                {
                    PaginacionContrato.TotalRegistros = 0;
                    PaginacionContrato.AsignarValoresIniciales();
                    dgContrato.ItemsSource = new List<ContratoInfo>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento de carga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AyudaContrato_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PaginacionContrato.DatosDelegado += ConsultaContratos;
                PaginacionContrato.AsignarValoresIniciales();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento clic del boton agregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }
        /// <summary>
        /// Evento clicl del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelado = true;
            confirmaSalir = true;
            this.Close();
            
        }
        /// <summary>
        /// Asigna los valores al grid
        /// </summary>
        private void AsignarValoresSeleccionadosGrid()
        {
            var renglonSeleccionado = dgContrato.SelectedItem as ContratoInfo;
            if (renglonSeleccionado != null)
            {
                Folio = renglonSeleccionado.Folio;
                confirmaSalir = false;
                this.Close();
            }
        }
        /// <summary>
        /// Doble clic en el grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgContrato_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }
        /// <summary>
        /// Evento clic del boton buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ConsultaContratos(PaginacionContrato.Inicio, PaginacionContrato.Limite);
        }
        /// <summary>
        /// Preview tex del folio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFolio_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Validacion de cierre del formulario
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.AyudaContrato_btnCancelarSalir, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Cancelado = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            ConsultaContratos(1,15);
        }
    }
}
