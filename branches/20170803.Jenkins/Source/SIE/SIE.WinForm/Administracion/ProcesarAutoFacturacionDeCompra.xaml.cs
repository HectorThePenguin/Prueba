using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;
using System.IO.Compression;
using System.Drawing;
using System.IO;
using Image = System.Drawing.Image;
using Ionic.Zip; 

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ProcesarAutoFacturacionDeCompra.xaml
    /// </summary>
    public partial class ProcesarAutoFacturacionDeCompra
    {
        #region VARIABLES

        private AutoFacturacionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AutoFacturacionInfo) DataContext;
            }
            set { DataContext = value; }
        }
        private readonly AutoFacturacionPL _plAutoFacturacion = new AutoFacturacionPL();
        
        #endregion VARIABLES

        #region CONSTRUCTOR

        public ProcesarAutoFacturacionDeCompra()
        {
            InitializeComponent();
            InicializaContexto();
            skAyudaCentroAcopio.ObjetoNegocio = new OrganizacionPL();
            LlenarComboFormasPago();
            LlenarComboEstatus();
            cmbFormaPago.SelectedIndex = 0;
            cmbEstatus.SelectedIndex = 0;
            ucPaginacion.Inicio = 1;
            dpFechaInicio.SelectedDate = DateTime.Now.Date;
            dpFechaFin.SelectedDate = DateTime.Now.Date;

            ucPaginacion.DatosDelegado += LlenarGrid;
            ucPaginacion.AsignarValoresIniciales();
            ucPaginacion.Contexto = Contexto;
            skAyudaCentroAcopio.AyudaConDatos += (o, args) => AsignarFiltroAyuda();
            LlenarGrid(ucPaginacion.Inicio, ucPaginacion.Limite);
            skAyudaCentroAcopio.AsignarFoco();
        }

        #endregion CONSTRUCTOR

        #region METODOS

        private void InicializaContexto()
        {
            Contexto = new AutoFacturacionInfo()
                           {
                               CentroAcopio = new OrganizacionInfo()
                                                  {
                                                      TipoOrganizacion = new TipoOrganizacionInfo(),
                                                      ListaTiposOrganizacion = new List<TipoOrganizacionInfo>()
                                                  },
                               ListFormasPago = new List<FormaPagoInfo>(),
                               ListEstatus = new List<EstatusInfo>(),
                               Estatus = new EstatusInfo(),
                               FormaPago = new FormaPagoInfo(),
                               FolioCompra = 0,
                               ChequeId = 0,
                               FechaInicio = DateTime.Now.Date,
                               FechaFin = DateTime.Now.Date
                           };

            AsignarFiltroAyuda();

        }

        private void AsignarFiltroAyuda()
        {
            Contexto.CentroAcopio = new OrganizacionInfo()
                                        {
                                            TipoOrganizacion = new TipoOrganizacionInfo(),
                                            ListaTiposOrganizacion = new List<TipoOrganizacionInfo>()
                                        };

            if(skAyudaCentroAcopio.Clave != string.Empty)
            {
                Contexto.OrganizacionId = int.Parse(skAyudaCentroAcopio.Clave);
            }
            else
            {
                Contexto.OrganizacionId = 0;
            }

            if (txtFolio.Text.Trim() != string.Empty)
            {
                Contexto.FolioCompra = int.Parse(txtFolio.Text);
            }
            else
            {
                Contexto.FolioCompra = 0;
            }
        }

        private void AgregarElementoInicialEstatus(IList<EstatusInfo> listEstatus)
        {
            var listInicial = new EstatusInfo { EstatusId = 0, Descripcion = Properties.Resources.ProcesarAutoFacturacion_Todos };
            if (!listEstatus.Contains(listInicial))
            {
                listEstatus.Insert(0, listInicial);
            }
        }

        private void AgregarElementoInicialFormasPago(IList<FormaPagoInfo> listFormaPago)
        {
            var listInicial = new FormaPagoInfo { FormaPagoId = 0, Descripcion = Properties.Resources.ProcesarAutoFacturacion_Todos };
            if (!listFormaPago.Contains(listInicial))
            {
                listFormaPago.Insert(0, listInicial);
            }
        }

        private void LlenarComboEstatus()
        {
            try
            {
                var pl = new EstatusPL();
                var lista = pl.ObtenerEstatusTipoEstatus(TipoEstatusEnum.ProcesarAutoFacturacion.GetHashCode());

                if (lista != null && lista.Any())
                {
                    AgregarElementoInicialEstatus(lista);
                    Contexto.ListEstatus = lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarAutoFacturacion_ErrorComboEstatus, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void LlenarComboFormasPago()
        {
            try
            {
                var pl = new FormaPagoPL();
                var lista = pl.ObtenerTodos();

                if (lista != null && lista.Any())
                {
                    AgregarElementoInicialFormasPago(lista);
                    Contexto.ListFormasPago = lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarAutoFacturacion_ErrorComboFormasPago, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Limpiar()
        {
            skAyudaCentroAcopio.LimpiarCampos();
            txtFolio.Text = string.Empty;
            cmbEstatus.SelectedIndex = 0;
            txtCheque.Text = string.Empty;
            cmbFormaPago.SelectedIndex = 0;
            dpFechaInicio.SelectedDate = DateTime.Now.Date;
            dpFechaFin.SelectedDate = DateTime.Now.Date;
            gridDatos.ItemsSource = new List<AutoFacturacionInfo>();
            LlenarGrid(ucPaginacion.Inicio, ucPaginacion.Limite);
            skAyudaCentroAcopio.AsignarFoco();
        }

        private void LlenarGrid(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                if (skAyudaCentroAcopio.Clave != null && skAyudaCentroAcopio.Clave.Trim() != string.Empty)
                {
                    Contexto.OrganizacionId = Convert.ToInt32(skAyudaCentroAcopio.Clave);
                }
                else
                {
                    Contexto.OrganizacionId = 0;
                }

                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                Contexto.FechaInicio = dpFechaInicio.SelectedDate.Value;
                Contexto.FechaFin = dpFechaFin.SelectedDate.Value;
                var result = _plAutoFacturacion.ObtenerPorFiltro(pagina, Contexto);

                if (result != null && result.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = result.Lista;
                    ucPaginacion.TotalRegistros = result.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<PagoTransferenciaInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AutoRefacturacionDocumentos_SinInformacion, MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarAutoFacturacion_ErrorMostrarDatos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void OcultarControles(bool habilitar)
        {
            btnBuscar.IsEnabled = habilitar;
            btnLimpiar.IsEnabled = habilitar;
            btnExportar.IsEnabled = habilitar;
            if (habilitar)
            {
                lblMensaje1.Visibility = Visibility.Hidden;
                lblMensaje2.Visibility = Visibility.Hidden;
            }
            else
            {
                lblMensaje1.Visibility = Visibility.Visible;
                lblMensaje2.Visibility = Visibility.Visible;
            }           
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
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

        private void GuardarImagenes(string guardarEn)
        {
            try
            {
                var pl = new AutoFacturacionPL();
                var ruta = string.Format("{0}\\{1}", Properties.Resources.ProcesarAutoFacturacion_CarpetaPrimaria, Properties.Resources.ProcesarAutoFacturacion_CarpetaSecundaria);
                var result = pl.ObtenerImagenes(Contexto);
                if (result.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        string.Format(Properties.Resources.ProcesarAutoFacturacion_MsgCantidadImagenes,result.Count),
                                            MessageBoxButton.OK,
                                                MessageImage.Warning);
                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);                        
                    }
                    else{
                        Directory.Delete(ruta, true);
                    }

                    var contador = 1;
                    var orgIdAnterior = 0;
                    var folioIdAnterior = 0;
                    foreach(var img in result.OrderBy(item => item.OrganizacionId).ThenBy(item => item.FolioCompra))
                    {
                        var nuevaCarpeta = string.Format("{0}\\{1}",ruta,img.Organizacion);
                        if (!Directory.Exists(nuevaCarpeta))
                        {
                            Directory.CreateDirectory(nuevaCarpeta);
                        }

                        var nuevaCarpetaFolio = string.Format("{0}\\{1}", nuevaCarpeta, img.FolioCompra);
                        if (!Directory.Exists(nuevaCarpetaFolio))
                        {
                            Directory.CreateDirectory(nuevaCarpetaFolio);
                        }

                        if (orgIdAnterior == img.OrganizacionId)
                        {
                            if (folioIdAnterior == img.FolioCompra)
                            {
                                contador++;
                            }
                            else {
                                contador = 1;
                            }
                        }
                        else {
                            contador = 1;
                        }

                        if (!File.Exists(string.Format("{0}\\Imagen{1}.jpg", nuevaCarpetaFolio, img.FolioCompra)))
                        {
                            var miImagen = byteArrayToImage(img.ImgDocmento);
                            miImagen.Save(string.Format("{0}\\Imagen{1}.jpg", nuevaCarpetaFolio, contador));
                        }

                        orgIdAnterior = img.OrganizacionId;
                        folioIdAnterior = img.FolioCompra;
                    }

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ProcesarAutoFacturacion_MsgZip,
                        MessageBoxButton.OK,
                            MessageImage.Warning);

                    ZipFile zip = new ZipFile();
                    zip.AddDirectory(ruta);
                    zip.Save(guardarEn);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ProcesarAutoFacturacion_MsgSinImagenes,
                        MessageBoxButton.OK,
                            MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ProcesarAutoFacturacion_MsgErrorExportar, 
                                        MessageBoxButton.OK, 
                                            MessageImage.Error);
            }
            finally
            {
                if (Directory.Exists(Properties.Resources.ProcesarAutoFacturacion_CarpetaPrimaria))
                {
                    Directory.Delete(Properties.Resources.ProcesarAutoFacturacion_CarpetaPrimaria, true);                
                }
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void btnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnEditar_OnClick(object sender, RoutedEventArgs e)
        {
             var botonEditar = (Button)e.Source;
             try
             {
                 var infoSelecionado = (AutoFacturacionInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                 var info = new AutoFacturacionInfo
                 {
                     OrganizacionId = infoSelecionado.OrganizacionId,
                     Organizacion = infoSelecionado.Organizacion,
                     FechaCompra = infoSelecionado.FechaCompra,
                     FormasPago = infoSelecionado.FormasPago,

                     Factura = infoSelecionado.Factura,
                     Proveedor = infoSelecionado.Proveedor,
                     ProveedorId = infoSelecionado.ProveedorId,
                     FolioCompra = infoSelecionado.FolioCompra,
                     Folio = infoSelecionado.Folio,
                     Facturado = false,
                     FechaFactura = DateTime.Now.Date,
                     ProductoCabezas = infoSelecionado.ProductoCabezas,
                     Importe = infoSelecionado.Importe,
                     TipoCompra = infoSelecionado.TipoCompra,
                     EstatusId = infoSelecionado.EstatusId
                 };
                 
                var form = new RegistrarAutoFacturacionDeCompra(info);
                form.Left = (ActualWidth - form.Width) / 2;
                form.Top = ((ActualHeight - form.Height) / 2);
                form.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                form.ShowDialog();
                Limpiar();
             }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarAutoFacturacion_AbrirFormulario, MessageBoxButton.OK, MessageImage.Error);
            }
          
        }

        private void btnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            
            if (dpFechaFin.SelectedDate >= dpFechaInicio.SelectedDate)
            {
                LlenarGrid(ucPaginacion.Inicio, ucPaginacion.Limite);
            }
            else {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ProcesarAutoFacturacion_MsgRangoFechasNoValido, 
                                        MessageBoxButton.OK, 
                                            MessageImage.Stop);
            }            
        }

        private void txtFolio_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "D1":
                case "D2":
                case "D3":
                case "D4":
                case "D5":
                case "D6":
                case "D7":
                case "D8":
                case "D9":
                case "D0":
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
        
        private void txtCheque_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "D1":
                case "D2":
                case "D3":
                case "D4":
                case "D5":
                case "D6":
                case "D7":
                case "D8":
                case "D9":
                case "D0":
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void txtFolio_TextChanged(object sender, TextChangedEventArgs e)
        {
            int valor = 0;
            bool result = Int32.TryParse(txtFolio.Text, out valor);
            if (!result)
            {
                txtFolio.Text = string.Empty;
                Contexto.FolioCompra = 0;
            }
            else
            {
                Contexto.FolioCompra = int.Parse(txtFolio.Text);
            }
        }

        private void txtCheque_TextChanged(object sender, TextChangedEventArgs e)
        {
            int valor = 0;
            bool result = Int32.TryParse(txtCheque.Text, out valor);
            if (!result)
            {
                txtCheque.Text = string.Empty;
                Contexto.ChequeId = 0;
            }
            else
            {
                Contexto.ChequeId = int.Parse(txtCheque.Text);
            }
        }

        private void CmbFormaPago_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFormaPago.SelectedIndex > 0)
            {
                var fp = (FormaPagoInfo)cmbFormaPago.SelectedItem;
                Contexto.FormaPagoId = fp.FormaPagoId;
            }
            else
            {
                Contexto.FormaPagoId = 0;
            }
        }

        private void CmbEstatus_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEstatus.SelectedIndex <= 0)
            {
                Contexto.EstatusId = 0;
            }
            else
            {
                if (cmbEstatus.SelectedIndex == 1)
                {
                    Contexto.EstatusId = 2;
                }
                else
                {
                    Contexto.EstatusId = 1;
                }
            }
        }

        private void btnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnLimpiar.IsEnabled = false;
                btnBuscar.IsEnabled = false;
                btnExportar.IsEnabled = false;
                lblMensaje1.Visibility = Visibility.Visible;
                lblMensaje2.Visibility = Visibility.Visible;
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Filter = "Zip File|*.zip";
                saveFileDialog1.Title = "Guardar archivo Zip";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    if (skAyudaCentroAcopio.Clave != null && skAyudaCentroAcopio.Clave.Trim() != string.Empty)
                    {
                        Contexto.OrganizacionId = Convert.ToInt32(skAyudaCentroAcopio.Clave);
                    }
                    else
                    {
                        Contexto.OrganizacionId = 0;
                    }

                    Contexto.FechaInicio = dpFechaInicio.SelectedDate.Value;
                    Contexto.FechaFin = dpFechaFin.SelectedDate.Value;
                    GuardarImagenes(saveFileDialog1.FileName);

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ProcesarAutoFacturacion_MsgExportarExito,
                        MessageBoxButton.OK,
                            MessageImage.Correct);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ProcesarAutoFacturacion_MsgErrorExportar,
                                        MessageBoxButton.OK,
                                            MessageImage.Error);
            }
            finally
            {
                btnLimpiar.IsEnabled = true;
                btnBuscar.IsEnabled = true;
                btnExportar.IsEnabled = true;
                lblMensaje1.Visibility = Visibility.Hidden;
                lblMensaje2.Visibility = Visibility.Hidden;
            }
        }

        #endregion EVENTOS
       
    }
}