using System;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SuKarne.Controls.Enum;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.PL;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RegistrarAutoFacturacionDeCompra.xaml
    /// </summary>
    public partial class RegistrarAutoFacturacionDeCompra : Window
    {
        #region VARIABLES
        private AutoFacturacionInfo imagenInfo;
        #endregion VARIABLES

        #region CONSTRUCTOR

        public RegistrarAutoFacturacionDeCompra(AutoFacturacionInfo info)
        {
            InitializeComponent();
            InicializaPantalla(info);
        }

        #endregion CONSTRUCTOR

        #region METODOS

        public void InicializaPantalla(AutoFacturacionInfo info)
        {
            imagenInfo = info;
            txtOrganizacion.Text = info.Organizacion;
            dtpFechaCompra.SelectedDate = info.FechaCompra.Date;
            txtFormaPago.Text = info.FormasPago;
            txtFactura.Text = info.Factura;
            txtProveedor.Text = info.Proveedor;
            txtFolioCompra.Text = info.FolioCompra.ToString();
            txtFolio.Text = info.Folio.ToString();
            ckbFacturado.IsChecked = info.Facturado;
            dtpFechaFactura.SelectedDate = info.FechaFactura;
            txtProducto.Text = info.ProductoCabezas.ToString();
            txtImporte.Text = info.Importe.ToString();
            imagenInfo.UsuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();

            if(imagenInfo.EstatusId == 2)
            {
                btnGuardar.IsEnabled = false;
            }

            txtFactura.Focus();
        }

        private void Guardar()
        {
            try
            {
                var pl = new AutoFacturacionPL();
                var result = pl.Guardar(imagenInfo);
                if(result == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.RegistrarAutoFacturacion_ExitoGuardar, result), MessageBoxButton.OK, MessageImage.Correct);
                    Close();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private bool Validar()
        {
            var result = false;
            try
            {
                if(txtFactura.Text.Trim() != string.Empty)
                {
                    imagenInfo.Factura = txtFactura.Text.Trim();
                    var esFacturado = ckbFacturado.IsChecked != null && (bool)ckbFacturado.IsChecked;
                    if(esFacturado)
                    {
                        result = true;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_MsjValidarEsFacturado, MessageBoxButton.OK, MessageImage.Warning);
                        ckbFacturado.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_MsjValidarFactura, MessageBoxButton.OK, MessageImage.Warning);
                    txtFactura.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
            }
            return result;
        }

        #endregion METODOS
        
        #region EVENTOS
        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDoc_Click(object sender, RoutedEventArgs e)
        {
            var form = new AutoRefacturacionDocumentos(imagenInfo);
            form.Left = (ActualWidth - form.Width) / 2;
            form.Top=((ActualHeight - form.Height) / 2);
            form.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
            form.ShowDialog();
        }

        private void btnINE_Click(object sender, RoutedEventArgs e)
        {
            var form = new AutoRefacturacionImagenes(imagenInfo);
            form.Left = (ActualWidth - form.Width) / 2;
            form.Top = ((ActualHeight - form.Height) / 2);
            form.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
            form.ShowDialog();
        }

        private void btnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                Guardar();
            }
        }
        
        #endregion

    }
}
