using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

using System.IO;
using Image = System.Drawing.Image;
using ImageFormat = System.Drawing.Imaging.ImageFormat;


namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionTarjetaRecepcion.xaml
    /// </summary>
    public partial class ImpresionTarjetaRecepcion
    {
        private List<ImpresionTarjetaRecepcionModel> impresionTarjeta;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroImpresionTarjetaRecepcion Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroImpresionTarjetaRecepcion)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroImpresionTarjetaRecepcion
                {
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        },
                    EntradaGanado = new ImpresionTarjetaRecepcionModel()
                };
        }

        public ImpresionTarjetaRecepcion()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            cmbFolioEntrada.ItemsSource = null;
            gridDatos.ItemsSource = null;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            if(impresionTarjeta == null || !impresionTarjeta.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ImpresionTarjetaRecepcion_FechaInvalida, MessageBoxButton.OK,
                             MessageImage.Warning);
                return;
            }
            CargarEntradasImprimir();
        }



        //Esta es la función que realiza la conversión de de una imagen a un arreglo de bytes
        public static byte[] ConversionImagen(string nombrearchivo){ 
            //Declaramos fs para poder abrir la imagen.
            FileStream fs =new FileStream(nombrearchivo, FileMode.Open); 
            // Declaramos un lector binario para pasar la imagen 
            // a bytes
            BinaryReader br =new BinaryReader(fs); 
             //BinaryReader(fs); 
            byte[] imagen =new byte[(int)fs.Length];
            br.Read(imagen,0,(int)fs.Length);
            br.Close();
            fs.Close(); 
            return imagen;
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var boton = e.Source as Button;
                if (boton != null)
                {
                    var elemento = boton.CommandParameter as ImpresionTarjetaRecepcionModel;
                    if (elemento!=null)
                    {
                        elemento.Vigilante = elemento.Vigilante.ToUpper();
                        elemento.UsuarioRecibio = elemento.UsuarioRecibio.ToUpper();
                        elemento.Operador = elemento.Operador.ToUpper();

                        var origenImagen = String.Format("{0}{1}",AppDomain.CurrentDomain.BaseDirectory,"Administracion\\Impresiones\\");
                        // Manejo sin estres
                        var imagen = elemento.ManejoSinEstres ? "OK.jpg" : "NO.jpg";
                        var dirImagen = String.Format("{0}{1}", origenImagen, imagen);
                        elemento.ManejoSinEstresImg = ConversionImagen(dirImagen);
                        //Guia
                        imagen = elemento.Guia ? "OK.jpg" : "NO.jpg";
                        dirImagen = String.Format("{0}{1}", origenImagen, imagen);
                        elemento.GuiaImg = ConversionImagen(dirImagen);
                        //Factura
                        imagen = elemento.Factura ? "OK.jpg" : "NO.jpg";
                        dirImagen = String.Format("{0}{1}", origenImagen, imagen);
                        elemento.FacturaImg = ConversionImagen(dirImagen);
                        //Poliza
                        imagen = elemento.Poliza ? "OK.jpg" : "NO.jpg";
                        dirImagen = String.Format("{0}{1}", origenImagen, imagen);
                        elemento.PolizaImg = ConversionImagen(dirImagen);
                        //HojaE
                        imagen = elemento.HojaEmbarque ? "OK.jpg" : "NO.jpg";
                        dirImagen = String.Format("{0}{1}", origenImagen, imagen);
                        elemento.HojaEmbarqueImg = ConversionImagen(dirImagen);
                        

                        var documento = new ReportDocument();
                        var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, 
                                                    "\\Administracion\\Impresiones\\rptImpresionRecepcionDeTarjeta.rpt");
                        documento.Load(reporte);
                        documento.DataSourceConnections.Clear();
                        documento.SetDataSource(new List<ImpresionTarjetaRecepcionModel> { elemento });
                        documento.Refresh();

                        ExportOptions rptExportOption;
                        var rptFileDestOption = new DiskFileDestinationOptions();
                        var rptFormatOption = new PdfRtfWordFormatOptions();
                        string reportFileName = string.Format("{0}{1}_{2}.pdf", 
                                        AppDomain.CurrentDomain.BaseDirectory,
                                        @"\ImpresionRecepcionDeTarjeta",
                                        Contexto.Fecha.ToString("ddMMyyyy"));
                        if (File.Exists(reportFileName))
                        {
                            try
                            {
                                File.Delete(reportFileName);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                                throw new ExcepcionDesconocida(Properties.Resources.ImpresionTarjetaRecepcion_ArchivoEnUso);
                            }

                        }

                        rptFileDestOption.DiskFileName = reportFileName;
                        rptExportOption = documento.ExportOptions;
                        {
                            rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                            rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rptExportOption.ExportDestinationOptions = rptFileDestOption;
                            rptExportOption.ExportFormatOptions = rptFormatOption;
                        }
                        documento.Export();
                        Process.Start(reportFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : Properties.Resources.ImpresionTarjetaRecepcion_ErrorImprimirReporte),              
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void CargarEntradasImprimir()
        {
            try
            {
                if(Contexto.EntradaGanado.EntradaGanadoID == 0)
                {
                    gridDatos.ItemsSource = impresionTarjeta.Where(impre=> !string.IsNullOrEmpty(impre.Descripcion));
                }
                else
                {
                    List<ImpresionTarjetaRecepcionModel> entradasFiltradas =
                        impresionTarjeta.Where(
                            impre => impre.EntradaGanadoID == Contexto.EntradaGanado.EntradaGanadoID && !string.IsNullOrEmpty(impre.Descripcion)).ToList();

                    if(entradasFiltradas.Any())
                    {
                        gridDatos.ItemsSource = entradasFiltradas;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionTarjetaRecepcion_ErrorConsultarEntradas, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ObtenerEntradasGanado()
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                if(!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue )
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionTarjetaRecepcion_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                Contexto.Fecha = dtpFecha.SelectedDate.Value;
                impresionTarjeta =
                    entradaGanadoPL.ObtenerEntradasImpresionTarjetaRecepcion(Contexto);
                if (impresionTarjeta != null && impresionTarjeta.Any())
                {
                    impresionTarjeta.ForEach(impre =>
                        {
                            impre.Descripcion =
                                string.Format(Properties.Resources.ImpresionTarjetaRecepcion_NomenclaturaFormato,
                                              impre.FolioEntrada);
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionTarjetaRecepcion_ErrorConsultarEntradas, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            {
                return;
            }
            if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionTarjetaRecepcion_FechaMayor, MessageBoxButton.OK,
                                MessageImage.Warning);
                dtpFecha.SelectedDate = null;
                return;
            }
            cmbFolioEntrada.ItemsSource = null;
            gridDatos.ItemsSource = null;
            ObtenerEntradasGanado();
            if(impresionTarjeta == null || !impresionTarjeta.Any())
            {
                 SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionTarjetaRecepcion_SinEntradas, MessageBoxButton.OK,
                                MessageImage.Warning);
                return;
            }
            var impresionTodos = new ImpresionTarjetaRecepcionModel
                {
                    FolioEntrada = Properties.Resources.cbo_Seleccionar,
                    EntradaGanadoID = 0
                };
            impresionTarjeta.Insert(0,impresionTodos);
            cmbFolioEntrada.ItemsSource = impresionTarjeta;
            cmbFolioEntrada.SelectedValue = impresionTodos.EntradaGanadoID;
            cmbFolioEntrada.SelectedIndex = 0;
        }
    }
}
