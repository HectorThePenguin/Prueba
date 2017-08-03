using System;
using System.Drawing;
using System.Windows;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.IO;
using Image = System.Drawing.Image;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para AutoRefacturacionDocumentos.xaml
    /// </summary>
    public partial class AutoRefacturacionDocumentos : Window
    {
        #region VARIABLES
        private AutoFacturacionInfo _imagen;
        private AutoFacturacionInfo _info;
        #endregion VARIABLES

        #region CONSTRUCTOR
        public AutoRefacturacionDocumentos(AutoFacturacionInfo info)
        {
            InitializeComponent();
            InicializaPantalla(info);
            _info = info;
        }
        #endregion CONSTRUCTOR

        #region METODOS
        private void InicializaPantalla(AutoFacturacionInfo info)
        {
            try
            {
                var autoFacturacionDeCompraImagenesPL = new AutoFacturacionDeCompraImagenesPL();
                _imagen = autoFacturacionDeCompraImagenesPL.ObtenerImagenDocumento(info);
                if (_imagen != null)
                {
                    var IMG = byteArrayToImage(_imagen.ImgDocmento);
                    ImgDocumento.Image = IMG;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ValidarImagen, MessageBoxButton.OK, MessageImage.Error);
                    btnDescargar.IsEnabled = false;
                    btnGirar.IsEnabled = false;
                }

            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorImagen, MessageBoxButton.OK, MessageImage.Error);
                btnDescargar.IsEnabled = false;
            }
        }

        /// <summary>
        /// Método para convertir un arreglo de byte en imagen
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public Image byteArrayToImage(byte[] byteArrayIn)
        {

            try
            {
                MemoryStream stream = new MemoryStream(byteArrayIn);
                Image returnImage = null;
                returnImage = Image.FromStream(stream);
                return returnImage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void DescargarImagen()
        {
            try
            {
                var file = new System.Windows.Forms.SaveFileDialog { FileName = string.Format("Documento de Compra {0} de la Organización {1}", _info.FolioCompra, _info.OrganizacionId), Filter = @"Archivos JPG|*.jpg", Title = @"Archivos JPG" };
                var result = file.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (file.FileName != "")
                    {
                        if (!File.Exists(file.FileName))
                        {
                            byteArrayToImage(_imagen.ImgDocmento).Save(file.FileName);
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_DescargarImagen, MessageBoxButton.OK, MessageImage.Correct);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorDescargarImagen, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void GirarImagen()
        {
            try
            {
                var x = ImgDocumento.Image;
                x.RotateFlip(RotateFlipType.Rotate90FlipNone);
                ImgDocumento.Image = x;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorGirarImagen, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion METODOS

        #region EVENTOS
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            DescargarImagen();
        }

        private void btnGirar_Click(object sender, RoutedEventArgs e)
        {
            GirarImagen();
        }
        #endregion EVENTOS
    }
}
