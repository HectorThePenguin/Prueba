using System;
using System.Windows;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Drawing;
using System.IO;
using Application = System.Windows.Application;
using SIE.Base.Log;
using Image = System.Drawing.Image;


namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para AutoRefacturacionImagenes.xaml
    /// </summary>
    public partial class AutoRefacturacionImagenes : Window
    {
        #region VARIABLES
        private AutoFacturacionInfo _imagen;
        private AutoFacturacionInfo _info;
        #endregion VARIABLES

        #region CONSTRUCTOR
        public AutoRefacturacionImagenes(AutoFacturacionInfo info)
        {
            InitializeComponent();
            InicializaPantalla(info);
            _info = info;
        }
        #endregion

        #region METODOS
        private void InicializaPantalla(AutoFacturacionInfo info)
        {
            try
            {
                var autoFacturacionDeCompraImagenesPL = new AutoFacturacionDeCompraImagenesPL();
                var imagen = autoFacturacionDeCompraImagenesPL.ObtenerImagenIneCurp(info);
                if (imagen != null)
                {
                    _imagen = imagen;
                    _imagen.ProveedorId = info.ProveedorId;
                    _imagen.OrganizacionId = info.OrganizacionId;
                    if ((imagen.ImgINE != null) || (imagen.ImgCURP != null))
                    {
                        if (imagen.ImgINE != null)
                        {
                            var imgIne = byteArrayToImage(imagen.ImgINE);
                            ImgINE.Image = imgIne;
                            btnDescargar.Visibility = Visibility.Visible;
                            btnGirar.Visibility = Visibility.Visible;
                            btnSiguiente.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_INE, MessageBoxButton.OK, MessageImage.Warning);
                        }

                        if (imagen.ImgCURP != null)
                        {
                            var imgCurp = byteArrayToImage(imagen.ImgCURP);
                            ImgCURP.Image = imgCurp;
                            btnDescargarCurp.Visibility = Visibility.Visible;
                            btnGirarCurp.Visibility = Visibility.Visible;
                            btnSiguienteCurp.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_CURP, MessageBoxButton.OK, MessageImage.Warning);
                        }
                        
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_ValidaImagenes, MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_ValidaImagenes, MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorImagen, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void BuscarSiguienteImagen(AutoFacturacionInfo info, bool esCurp)
        {
            try
            {
                var autoFacturacionDeCompraImagenesPL = new AutoFacturacionDeCompraImagenesPL();
                var imagen = autoFacturacionDeCompraImagenesPL.ObtenerImagenIneCurp(_imagen);
                if (imagen != null)
                {
                    if(esCurp)
                    {
                        if (imagen.ImgCURP != null)
                        {
                            var imgCurp = byteArrayToImage(imagen.ImgCURP);
                            ImgCURP.Image = imgCurp;
                            _imagen.ImgCURPId = imagen.ImgCURPId;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_CURP, MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                    else
                    {
                        if (imagen.ImgINE != null)
                        {
                            var imgIne = byteArrayToImage(imagen.ImgINE);
                            ImgINE.Image = imgIne;
                            _imagen.ImgINEId = imagen.ImgINEId;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_INE, MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_ValidaImagenes, MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorImagen, MessageBoxButton.OK, MessageImage.Warning);
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

        private void GirarImagen(bool esCurp)
        {
            try
            {
                if(esCurp)
                {
                    var x = ImgCURP.Image;
                    x.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    ImgCURP.Image = x;
                }
                else
                {
                    var x = ImgINE.Image;
                    x.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    ImgINE.Image = x;
                }
              
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_ErrorGirarImagen, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void DescargarImagen(bool esCurp)
        {
            try
            {
                if (esCurp)
                {
                    var file = new System.Windows.Forms.SaveFileDialog { FileName = string.Format("CURP {0} de la Organización {1} del proveedor {2}", _imagen.ImgCURPId, _info.OrganizacionId, _info.ProveedorId), Filter = @"Archivos JPG|*.jpg", Title = @"Archivos JPG" };
                    var result = file.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        if (file.FileName != "")
                        {
                            if (!File.Exists(file.FileName))
                            {
                                byteArrayToImage(_imagen.ImgCURP).Save(file.FileName);
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_DescargarImagen, MessageBoxButton.OK, MessageImage.Correct);
                            }
                        }
                    }
                }
                else
                {
                    var file = new System.Windows.Forms.SaveFileDialog { FileName = string.Format("INE {0} de la Organización {1} del proveedor {2}", _imagen.ImgINEId, _info.OrganizacionId, _info.ProveedorId), Filter = @"Archivos JPG|*.jpg", Title = @"Archivos JPG" };
                    var result = file.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        if (file.FileName != "")
                        {
                            if (!File.Exists(file.FileName))
                            {
                                byteArrayToImage(_imagen.ImgINE).Save(file.FileName);
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_DescargarImagen, MessageBoxButton.OK, MessageImage.Correct);
                            }
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
        #endregion METODOS

        #region EVENTOS
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnDescargar_OnClick(object sender, RoutedEventArgs e)
        {
            DescargarImagen(false);
        }

        private void BtnGirar_OnClick(object sender, RoutedEventArgs e)
        {
            GirarImagen(false);
        }

        private void BtnSiguiente_OnClick(object sender, RoutedEventArgs e)
        {
            if (_imagen.ImgINECount <= 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_SinImagenes, MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                if(_imagen.ImgINEId >= _imagen.ImgINEMax)
                {
                    _imagen.ImgINEId = 0;
                }
                btnSiguiente.IsEnabled = false;
                BuscarSiguienteImagen(_imagen,false);
                btnSiguiente.IsEnabled = true;
            }
        }

        private void BtnDescargarCurp_OnClick(object sender, RoutedEventArgs e)
        {
            DescargarImagen(true);
        }

        private void BtnSiguienteCurp_OnClick(object sender, RoutedEventArgs e)
        {
            if (_imagen.ImgCURPCount <= 1)
            {
                btnSiguienteCurp.IsEnabled = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionImagen_SinImagenes, MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                if (_imagen.ImgCURPId >= _imagen.ImgCURPMax)
                {
                    _imagen.ImgCURPId = 0;
                }
                btnSiguienteCurp.IsEnabled = false;
                BuscarSiguienteImagen(_imagen,true);
                btnSiguienteCurp.IsEnabled = true;
            }
        }

        private void BtnGirarCurp_OnClick(object sender, RoutedEventArgs e)
        {
            GirarImagen(true);
        }
        #endregion EVENTOS

    }
}
