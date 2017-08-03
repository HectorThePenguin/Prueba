using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para BitacoraErrores.xaml
    /// </summary>
    public partial class BitacoraErrores
    {
        public BitacoraErrores()
        {
            InitializeComponent();
        }

        private void BtnDescargar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var rutaEjecutable = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\", StringComparison.Ordinal));
                string archivo = string.Format("{0}\\{1}", rutaEjecutable, Properties.Resources.BitacoraErrores_NombreLog);
                if (File.Exists(archivo))
                {
                    var file = new SaveFileDialog { FileName = archivo, Filter = @"Archivos txt|*.txt", Title = @"Guardar Archivo txt" };
                    var result = file.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        if (file.FileName != "")
                        {
                            if (File.Exists(file.FileName))
                            {
                                try
                                {
                                    var archivoTxt = new FileStream(file.FileName, FileMode.Open);
                                    archivoTxt.Close();
                                }
                                catch (IOException)
                                {
                                    throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                                }
                            }
                            try
                            {
                                File.Copy(archivo, file.FileName, true);
                                Process.Start(file.FileName);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.BitacoraErrores_ErrorDescargarArchivo, MessageBoxButton.OK,
                                                  MessageImage.Error);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.BitacoraErrores_ErrorDescargarArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }

        private void BtnEnviar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtDestinatarios.Text))
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.BitacoraErrores_SinDestinatarios, MessageBoxButton.OK,
                               MessageImage.Warning);
                    return;
                }

                var rutaEjecutable = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\", StringComparison.Ordinal));
                string archivo = string.Format("{0}\\{1}", rutaEjecutable, Properties.Resources.BitacoraErrores_NombreLog);
                if (File.Exists(archivo))
                {
                    string archivoCopiado = string.Format("{0}\\{1}", Path.GetTempPath(),
                                                          Properties.Resources.BitacoraErrores_NombreLog);
                    File.Copy(archivo, archivoCopiado,true);
                    var correopl = new CorreoPL();

                    if(!File.Exists(archivoCopiado))
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.BitacoraErrores_SinBitacora, MessageBoxButton.OK,
                              MessageImage.Warning);
                        return;
                    }

                    List<string> destinatarios = (from cor in txtDestinatarios.Text.Split(';')
                                                  select cor).ToList();

                    var correo = new CorreoInfo
                                            {
                                                Asunto = "Bitacora Errores SIAP",
                                                Mensaje = "Se adjunta archivo de Errores",
                                                NombreOrigen = "Sukarne",
                                                Correos = destinatarios,
                                                ArchivoAdjunto = archivoCopiado
                                            };

                    correopl.EnviarCorreo(
                        new OrganizacionInfo {OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()},
                        correo);
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.BitacoraErrores_CorreoEnviado, MessageBoxButton.OK,
                                MessageImage.Correct);
                    txtDestinatarios.Text = string.Empty;
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.BitacoraErrores_SinBitacora, MessageBoxButton.OK,
                              MessageImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.BitacoraErrores_ErrorDescargarArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }
    }
}
