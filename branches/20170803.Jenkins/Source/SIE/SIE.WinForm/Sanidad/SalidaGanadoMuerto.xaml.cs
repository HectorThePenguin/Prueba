using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Filtros;
using SIE.WinForm.Controles;
using SIE.Services.Info.Info;
using System.Linq;
using SIE.Services.Info.Enums;


namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para SalidaGanadoMuerto.xaml
    /// </summary>
    public partial class SalidaGanadoMuerto 
    {
        public SalidaGanadoMuerto()
        {
            InitializeComponent();
            InicializaFormulario();
        }
        
        /// <summary>
        /// Inicializa el formulario
        /// </summary>
        private void InicializaFormulario()
        {
            dtFecha.SelectedDate = DateTime.Now;
            dtFecha.DisplayDateEnd = DateTime.Now;
            dtFecha.DisplayDateStart = DateTime.Now.AddYears(-1);
        }
        
        /// <summary>
        /// Ocurre cuando se presiona click sobre el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerar_OnClick(object sender, RoutedEventArgs e)
        {
            var muertesPl = new MuertePL();
            var salidaGanadoMuertePl = new SalidaGanadoMuertoPL();
            var muerteInfo = new MuerteInfo (){
                OrganizacionId = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]),
                FechaNecropsia = dtFecha.SelectedDate.Value
            };

            try
            {
                IList<SalidaGanadoMuertoInfo> listaMuertes = muertesPl.ObtenerMuertesFechaNecropsia(muerteInfo);
       
                if (listaMuertes != null && listaMuertes.Count > 0)
                {
                    int folio = 0;
                    IList<SalidaGanadoMuertoInfo> result = listaMuertes.Where(muerte => muerte.FolioSalida != 0).ToList();
                    if (result != null && result.Count > 0)
                    {
                        folio = result[0].FolioSalida;
                    }
                    else
                    {
                        var salidaGanadoMuerteInfo = new SalidaGanadoMuertoInfo()
                        {
                            OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]),
                            TipoFolio = (int)TipoFolio.SalidaGanadoMuerte
                        };
                        folio = salidaGanadoMuertePl.ObtenerFolio(salidaGanadoMuerteInfo);
                    }
                    var organizacionPl = new OrganizacionPL();
                    var organizacionInfo = new OrganizacionInfo();
                    organizacionInfo = organizacionPl.ObtenerPorID(muerteInfo.OrganizacionId);
                    var etiquetas = ObtenerEtiquetas();
                    etiquetas.Titulo = organizacionInfo.Descripcion;
                    etiquetas.Folio = folio.ToString().PadLeft(5,'0');
                    etiquetas.Fecha = dtFecha.Text;

                    listaMuertes[0].FolioSalida = folio;
                    listaMuertes[0].UsuarioModificacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

                    salidaGanadoMuertePl.AsignarFolioMuertes(listaMuertes);
                    try 
	                {	        
		                salidaGanadoMuertePl.CrearOrdenSalidaGandoMuerto(etiquetas, listaMuertes);
                        salidaGanadoMuertePl.MostrarReportePantalla();
	                }
	                catch
	                {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          Properties.Resources.OrdenSalidaMuerteGanado_msgArchivoAbierto,
                          MessageBoxButton.OK,
                          MessageImage.Stop);
	                }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          Properties.Resources.OrdenSalidaMuerteGanado_msgNoExistenMuertes,
                          MessageBoxButton.OK,
                          MessageImage.Stop);
                    dtFecha.SelectedDate = DateTime.Now;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
        }
        
        /// <summary>
        /// Ocurre cuando se presiona click sobre el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            dtFecha.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Obtiene las etiquetas para el reporte 
        /// </summary>
        /// <returns></returns>
        private ImpresionSalidaGanadoMuertoInfo ObtenerEtiquetas()
        {
            return new ImpresionSalidaGanadoMuertoInfo()
            {
                SubTitulo = Properties.Resources.OrdenSalidaMuerteGanado_SubTitulo,
                lblFolio = Properties.Resources.OrdenSalidaMuerteGanado_lblFolio,
                lblFecha = Properties.Resources.OrdenSalidaMuerteGanado_lblFecha,
                lblUnidad = Properties.Resources.OrdenSalidaMuerteGanado_lblUnidad,
                lblGerenteAdministrativo = Properties.Resources.OrdenSalidaMuerteGanado_lblGerenteAdministrativo,
                lblChofer = Properties.Resources.OrdenSalidaMuerteGanado_lblChofer,
                lblGerenteEngorda = Properties.Resources.OrdenSalidaMuerteGanado_lblGerenteEngorda,
                lblPlacas = Properties.Resources.OrdenSalidaMuerteGanado_lblPlacas,
                lblGerenteGeneral = Properties.Resources.OrdenSalidaMuerteGanado_lblGerenteGeneral,
                lblProteccionPatrimonial = Properties.Resources.OrdenSalidaMuerteGanado_lblProteccionPatrimonial,
                clmArete = Properties.Resources.OrdenSalidaMuerteGanado_clmArete,
                clmPeso = Properties.Resources.OrdenSalidaMuerteGanado_clmPeso,
                clmAreteTestigo = Properties.Resources.OrdenSalidaMuerteGanado_clmAreteTestigo,
                clmCausa = Properties.Resources.OrdenSalidaMuerteGanado_clmCausa,
                clmCorral = Properties.Resources.OrdenSalidaMuerteGanado_clmCorral,
                clmTipoGanado = Properties.Resources.OrdenSalidaMuerteGanado_clmTipoGanado,
                clmSexo = Properties.Resources.OrdenSalidaMuerteGanado_clmSexo
            };
        }

        private void DtFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dtFecha.SelectedDate == null)
                dtFecha.SelectedDate = DateTime.Now;
        }
    }
}
