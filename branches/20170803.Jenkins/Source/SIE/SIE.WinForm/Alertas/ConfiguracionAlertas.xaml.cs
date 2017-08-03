using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Enums;
using Application = System.Windows.Application;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Interaction logic for ConfiguracionAlertas.xaml
    /// </summary>
    public partial class ConfiguracionAlertas
    {
        #region CONSTRUCTOR
        public ConfiguracionAlertas()
        {
            InitializeComponent();
            InicializaContexto();
        }
        #endregion

        #region PROPIEDADES
        private ConfiguracionAlertasGeneraInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }

                return (ConfiguracionAlertasGeneraInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion

        #region METODOS
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConfiguracionAlertasGeneraInfo
            {
                AlertaInfo = new AlertaInfo
                {
                    ListaAccionInfo = new List<AccionInfo>(),
                    ConfiguracionAlerta = new ConfiguracionAlertasInfo
                    {
                        Activo = EstatusEnum.Activo
                    },
                    Descripcion = "",
                },
                ListaAlertaAccionInfo = new List<AlertaAccionInfo>()
            };
            skAyudaAlertas.ObjetoNegocio = new ConfiguracionAlertasPL();
        }

        /// <summary>
        /// Metodo para buscar por el filtro seleccionado
        /// </summary>
        private void Buscar()
        {
            ObtenerListaConfguracionAlertas(ucPaginacion.Inicio, ucPaginacion.Limite);
        }
        #endregion

        #region EVENTOS
        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
            Contexto.AlertaInfo.AlertaID = 0;
            Contexto.AlertaInfo.Descripcion = string.Empty;
        }
        #endregion


        /// <summary>
        /// Obtiene la lista de Configuracion de Alertas
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaConfguracionAlertas(int inicio, int limite)
        {
            try
            {
                var configAlertasPL = new ConfiguracionAlertasPL();
                ConfiguracionAlertasGeneraInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                filtros.AlertaInfo.ConfiguracionAlerta.Activo = (EstatusEnum) cmbActivo.SelectedValue;
                ResultadoInfo<ConfiguracionAlertasGeneraInfo> resultadoInfo = configAlertasPL.ConsultaConfiguracionAlertas(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<ConfiguracionAlertasInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_ErrorCargarConfiguracionesAlertas,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_ErrorCargarConfiguracionesAlertas,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ConfiguracionAlertasGeneraInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Evento que llama la ventana donde se cargaran los datos de la configuracion 
        /// para editar la información
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var configuracionAlertaSelec = (ConfiguracionAlertasGeneraInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (configuracionAlertaSelec != null)
                {
                    int alertaid = configuracionAlertaSelec.AlertaInfo.AlertaID;

                    var listaAcciones = new ConfiguracionAlertasPL();
                    List<AlertaAccionInfo> filtros = listaAcciones.ObtenerListaAcciones(alertaid);

                    configuracionAlertaSelec.ListaAlertaAccionInfo = filtros;
                    configuracionAlertaSelec.ConfiguracionAlertas = configuracionAlertaSelec.AlertaInfo.ConfiguracionAlerta;
                    configuracionAlertaSelec.AccionInfo = configuracionAlertaSelec.AccionInfo;
                    configuracionAlertaSelec.AlertaInfo = configuracionAlertaSelec.AlertaInfo;

                    var configuracionAlertaEdicion = new ConfiguracionAlertasEdicion(configuracionAlertaSelec)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.ConfiguracionAlertaEdicion_Nuevo_Titulo }
                    };
                    MostrarCentrado(configuracionAlertaEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_ErrorAlCargarLaVentanaDeEdicion,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento de carga de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaConfguracionAlertas;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiiguracionAlerta_ErrorAlCargarLaVentana,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiiguracionAlerta_ErrorAlCargarLaVentana, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que llama la ventana donde se creara una nueva configuracion 
        /// para registrar en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var configuracionAlertaSelec = new ConfiguracionAlertasGeneraInfo();
                var configuracionAlertaEdicion = new ConfiguracionAlertasEdicion(configuracionAlertaSelec)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.ConfiguracionAlertaEdicion_Nuevo_Titulo }
                };
                MostrarCentrado(configuracionAlertaEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_ErrorAlCargarLaVentanaDeNuevo,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }
    }
}
