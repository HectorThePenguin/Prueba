using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System.Configuration;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using CheckBox = System.Windows.Controls.CheckBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for ProgramacionCorteGanado.xaml
    /// </summary>
    public partial class ProgramacionCorteGanado
    {
        
        #region Constructor
        private int organizacionID;
        private ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;

        public ProgramacionCorteGanado()
        {
            InitializeComponent();
            organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
        }
        
        #endregion

        #region Eventos

        /// <summary>
        /// Evento cargar Load del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramacionCorteGanado_OnLoaded(object sender, RoutedEventArgs e)
        {
            LlenaDatosGrid(); //Obtenemos los corrales disponibles para programacion de ganado
           
        }

        ///<summary>
        /// Selecciona todos los checkbox del grid
        /// </summary>   
        /// <param name="sender"></param>   
        /// <param name="e"></param>   
        private void ChkTodos_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox) sender).IsChecked;
            SetCheckbox(isChecked != null && isChecked.Value);
        }

        /// <summary>
        /// Evento click para Programar a Corte y envia la pantalla imprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgramarCorte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                var programacionCorteLista = new List<ProgramacionCorteInfo>();

                foreach (var t in dgProgramacionCorteGanado.Items)
                {
                    var tmpEntradaGanado = (EntradaGanadoInfo)t;
                    if (!tmpEntradaGanado.Manejado) continue;

                    foreach (var entradaGanado in listaEntradaGanado.Lista)
                    {
                        if (entradaGanado.CorralID == tmpEntradaGanado.CorralID)
                        {
                            var programacionCorte = new ProgramacionCorteInfo();

                            programacionCorte.Activo = EstatusEnum.Activo;
                            /*
                                programacionCorte.FechaCreacion = DateTime.Now;
                                programacionCorte.FechaProgramacion = DateTime.Now;
                                programacionCorte.FechaModificacion = DateTime.Now;
                                programacionCorte.FechaFinCorte = DateTime.Now;
                                programacionCorte.FechaInicioCorte = DateTime.Now;
                            */
                            //programacionCorte.FolioEntradaID = entradaGanado.EntradaGanadoID;
                            programacionCorte.FolioEntradaID = entradaGanado.FolioEntrada;
                            programacionCorte.Merma = ((entradaGanado.PesoOrigen - entradaGanado.PesoLlegada) / (float)entradaGanado.PesoOrigen) * 100;
                            programacionCorte.Merma = (float)Math.Round(programacionCorte.Merma, 2);
                            programacionCorte.OrganizacionID = organizacionID;
                            programacionCorte.UsuarioCreacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                            programacionCorte.FechaEntrada = entradaGanado.FechaEntrada.ToShortDateString();
                            programacionCorte.OrganizacionID = entradaGanado.OrganizacionID;
                            programacionCorte.CabezasRecibidas = entradaGanado.CabezasRecibidas;
                            programacionCorte.CodigoCorral = entradaGanado.CodigoCorral;
                            programacionCorte.Hembras = entradaGanado.Hembras;
                            programacionCorte.Machos = entradaGanado.Machos;
                            programacionCorte.LeyendaNivelGarrapata = entradaGanado.LeyendaNivelGarrapata;
                            programacionCorte.Evaluacion = entradaGanado.Evaluacion;
                            
                            programacionCorte.Rechazos = Convert.ToInt32(entradaGanado.Rechazos);
                            //programacionCorte.FechaInicioCorte = entradaGanado.FechaEntrada;
                            DateTime now = DateTime.Now;
                            var ts = DateTime.ParseExact(now.ToShortDateString(), "dd/MM/yyyy", null) - DateTime.ParseExact(entradaGanado.FechaEntrada.ToShortDateString(), "dd/MM/yyyy", null);
                    		programacionCorte.Dias = ts.Days;
                            programacionCorte.OrganizacionNombre = entradaGanado.OrganizacionOrigen;
                            programacionCorteLista.Add(programacionCorte);
                        }
                    }
                    
                }
                if (programacionCorteLista.Count > 0)
                {

					var programacionCorteGanadoDialogo =
                        new ProgramacionCorteGanadoDialogo(programacionCorteLista);

                    programacionCorteGanadoDialogo.Left = (ActualWidth - programacionCorteGanadoDialogo.Width) / 2;
                    programacionCorteGanadoDialogo.Top = ((ActualHeight - programacionCorteGanadoDialogo.Height) / 2) + 132;
                    programacionCorteGanadoDialogo.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                    programacionCorteGanadoDialogo.ShowDialog();

                    if (programacionCorteGanadoDialogo.guardado)
                    {
                        LlenaDatosGrid();
                    }
                    
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                                        Properties.Resources.ProgramacionCorteGanado_ErrorNoPartidas, 
                                        MessageBoxButton.OK, 
                                        MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionCorteGanado_ErrorAlGuardar,
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionCorteGanado_ErrorAlGuardar, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
        }

        #endregion

        #region Metodos
        
        /// <summary>
        /// Metodo para Checar los checkbox del Grid
        /// </summary>
        /// <param name="value"></param>
        private void SetCheckbox(bool value)
        {
            IList<EntradaGanadoInfo> entradaGanadoLista = new List<EntradaGanadoInfo>();
            foreach (var entradaGanado in dgProgramacionCorteGanado.Items.Cast<EntradaGanadoInfo>())
            {
                entradaGanado.Manejado = value;
                entradaGanadoLista.Add(entradaGanado);
            }
            dgProgramacionCorteGanado.ItemsSource = entradaGanadoLista;
        }
        
        /// <summary>
        /// Funcion que permite generar la lista de corrales listos para ser programados
        /// </summary>
        private void LlenaDatosGrid()
        {
            try
            {
                var entradaGanadoPl = new EntradaGanadoPL();
                listaEntradaGanado = entradaGanadoPl.ObtenerPorTipoCorral(organizacionID, (int)TipoCorral.Recepcion);
                if (listaEntradaGanado != null)
                {
                    dgProgramacionCorteGanado.ItemsSource = agruparLista(listaEntradaGanado.Lista);
                }
                else
                {
                    dgProgramacionCorteGanado.ItemsSource = null;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.ProgramacionCorteGanado_ErrorSinDatos, 
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionCorteGanado_ErrorObtenerPartidas, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ProgramacionCorteGanado_ErrorObtenerPartidas, 
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Funcion que agrupa los resultados del grid dgProgramacionCorteGanado
        /// </summary>
        private IList<EntradaGanadoInfo> agruparLista(IList<EntradaGanadoInfo> listaEntrada)
        {
            IList<EntradaGanadoInfo> listaResultado = new List<EntradaGanadoInfo>();
            bool encontrado;
            foreach (var entradaGanadoInfo in listaEntrada)
            {
                encontrado = false;
                foreach (var tmpEntradaGanadoInfo in listaResultado)
                {
                    if (entradaGanadoInfo.CodigoCorral == tmpEntradaGanadoInfo.CodigoCorral)
                    {
                        encontrado = true;

                        tmpEntradaGanadoInfo.FolioEntradaAgrupado += " | " + entradaGanadoInfo.FolioEntrada;
                        tmpEntradaGanadoInfo.OrganizacionOrigenAgrupado += " | " + entradaGanadoInfo.OrganizacionOrigen;

                        tmpEntradaGanadoInfo.CabezasRecibidasAgrupadas += entradaGanadoInfo.CabezasRecibidas;
                    }
                }
                if (encontrado == false)
                {
                    entradaGanadoInfo.FolioEntradaAgrupado = entradaGanadoInfo.FolioEntrada.ToString();
                    entradaGanadoInfo.OrganizacionOrigenAgrupado = entradaGanadoInfo.OrganizacionOrigen;
                    entradaGanadoInfo.CabezasRecibidasAgrupadas = entradaGanadoInfo.CabezasRecibidas;
                    listaResultado.Add(entradaGanadoInfo);
                }

            }

            return listaResultado;
        }

        #endregion
        /// <summary>
        /// Event Handler que permite validar el tiempo transcurrido del corral para efectos de cambio de color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProgramacionCorteGanado_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            try
            {
                //TODO: Revisar la implementacion de lectura de valores de configuracion
                var numDiasEntradaParaCorte = int.Parse(ConfigurationManager.AppSettings["numDiasEntradaParaCorte"]);
                var valor = (EntradaGanadoInfo)e.Row.Item;
                var tiempo = DateTime.Now - valor.FechaEntrada;
                //Todos los renglones donde la fecha de entrada sea >= 3 dias con la actual se pinta en rojo
                if (tiempo.Days > numDiasEntradaParaCorte)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
               
            }
            catch
            {
                ;
            } 
        }
    }
}
