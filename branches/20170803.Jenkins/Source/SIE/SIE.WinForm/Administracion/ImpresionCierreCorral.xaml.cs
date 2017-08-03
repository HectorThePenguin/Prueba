using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresioCierreCorral.xaml
    /// </summary>
    public partial class ImpresionCierreCorral
    {
        #region CONSTRUCTOR
        public ImpresionCierreCorral()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region EVENTOS
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            txtCorral.Text = string.Empty;
        }

        private void TxtCorral_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var corralPL = new CorralPL();
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();

                var corral = new CorralInfo
                {
                    Codigo = txtCorral.Text,
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = organizacionID
                    }
                };

                corral = corralPL.ObtenerPorCodigoGrupo(corral);

                if (corral == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ImpresionCierreCorral_CorralInvalido, MessageBoxButton.OK, MessageImage.Warning);
                    txtCorral.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ImpresionCierreCorral_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        private void txtCorral_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        #endregion EVENTOS

        #region METODOS
        private void Buscar()
        {
            try
            {
                var lotePL = new LotePL();
                var corralPL = new CorralPL();
                var checkListCorralPL = new CheckListCorralPL();
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();

                var corral = new CorralInfo
                {
                    Codigo = txtCorral.Text,
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = organizacionID
                    }
                };

                corral = corralPL.ObtenerPorCodigoGrupo(corral);

                if (corral == null)
                {
                    return;
                }

                LoteInfo lote = lotePL.ObtenerPorCorralCerrado(organizacionID, corral.CorralID);

                if (lote == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.ImpresionCierreCorral_SinLoteActivo, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                CheckListCorralInfo checkList = checkListCorralPL.ObtenerPorLote(organizacionID, lote.LoteID);

                if (checkList == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.ImpresionCierreCorral_SinCierre, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                GenerarArchivo(checkList);


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ImpresionCierreCorral_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        private void GenerarArchivo(CheckListCorralInfo checkList)
        {
            var file = new SaveFileDialog { FileName = "CheckList", Filter = @"Archivos PDF|*.pdf", Title = @"Guardar Archivo PDF" };
            var result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (file.FileName != "")
                {
                    if (File.Exists(file.FileName))
                    {
                        try
                        {
                            var archivoPDF = new FileStream(file.FileName, FileMode.Open);
                            archivoPDF.Close();
                        }
                        catch (IOException)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ImpresionCierreCorral_ArchivoAbierto, MessageBoxButton.OK, MessageImage.Warning);
                            //throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                        }
                    }
                    File.WriteAllBytes(file.FileName, checkList.PDF);
                    Process.Start(file.FileName);
                }
            }
        }
        #endregion METODOS
    }
}
