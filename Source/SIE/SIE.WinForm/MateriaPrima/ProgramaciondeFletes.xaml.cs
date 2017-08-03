using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para ProgramaciondeFletes.xaml
    /// </summary>
    public partial class ProgramaciondeFletes
    {
        #region Atributos
        /// <summary>
        /// Contexto
        /// </summary>
        private ContratoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContratoInfo)DataContext;
            }
            set { DataContext = value; }
        }
        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ContratoInfo
            {
                Activo = EstatusEnum.Activo
            };
        }
        private List<ContratoInfo> datosGridProgramacionFletes;
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        #endregion  

        #region Constructor
        public ProgramaciondeFletes()
        {
            datosGridProgramacionFletes = new List<ContratoInfo>();
            InitializeComponent();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Loaded de programacion de fletes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramaciondeFletes_OnLoaded(object sender, RoutedEventArgs e)
        {
            ucPaginacion.DatosDelegado += ObtenerListaContratos;
            ucPaginacion.AsignarValoresIniciales();
            ucPaginacion.Contexto = Contexto;
            CargarAyudas();
            Buscar();
            txtContrato.Focus();

        }
        /// <summary>
        /// Realiza la busqueda
        /// </summary>
        private void Buscar()
        {
            ObtenerListaContratos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }
        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaContratos(int inicio, int limite)
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

                var programacionFletesPl = new ProgramaciondeFletesPL();
                ContratoInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ContratoInfo> resultadoInfo = programacionFletesPl.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatosProgramacionFletes.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatosProgramacionFletes.ItemsSource = new List<ContratoInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramaciondeFlete_SeleccioneFiltro,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CalidadGanado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CalidadGanado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Obtiene el filtro necesario para obtener los fletes
        /// </summary>
        /// <returns></returns>
        private ContratoInfo ObtenerFiltros()
        {
            ContratoInfo resultado = null;
            try
            {
                
                int folioOrganizacion = string.IsNullOrEmpty(skAyudaOrganizacion.Clave)
                    ? 0
                    : int.Parse(skAyudaOrganizacion.Clave);
                int folio = 0;

                if (!int.TryParse(txtContrato.Text, out folio))
                {
                    folio = 0;
                }

                resultado =  new ContratoInfo
                {
                    TipoFlete = new TipoFleteInfo
                    {
                        TipoFleteId = (int) TipoFleteEnum.PagoenGanadera
                    },
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = folioOrganizacion
                    },
                    Folio = folio,
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }
       /// <summary>
       /// Key down de txt contrato
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void TxtContrato_OnKeyDown(object sender, KeyEventArgs e)
        {
           if (e.Key == Key.Enter || e.Key == Key.Tab)
           {
               Buscar();
               
            }
        }
        
        /// <summary>
        /// Buscar en filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Limpiar campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
        /// <summary>
        /// Limpiar
        /// </summary>
        private void LimpiarCampos()
        {
            txtContrato.Clear();
            CargarGridProgramacionFletes();
            CargarAyudas();
            txtContrato.Focus();
        }

        /// <summary>
        /// OnClick de boton editar de grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var contratoSeleccionado = (ContratoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);

                if (contratoSeleccionado != null)
                {
                    contratoSeleccionado.Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = contratoSeleccionado.Organizacion.OrganizacionID,
                        Descripcion = contratoSeleccionado.Organizacion.Descripcion,
                    };
                    var registratProgramacionFlete = new RegistrarProgramaciondeFletes(contratoSeleccionado);
                    
                    MostrarCentrado(registratProgramacionFlete);
                    CargarGridProgramacionFletes();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionFletes_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// OnClick del boton nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var registratProgramacionFlete = new RegistrarProgramaciondeFletes();
                MostrarCentrado(registratProgramacionFlete);
                CargarGridProgramacionFletes();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Preview text input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtContrato_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        #endregion  

        #region Metodos

        

        /// <summary>
        /// Cargar datos del grid programacion de fletes
        /// </summary>
        private void CargarGridProgramacionFletes()
        {
            try
            {
                var programacionFletesPl = new ProgramaciondeFletesPL();
                datosGridProgramacionFletes = programacionFletesPl.ObtenerContratosPorTipo((int)EstatusEnum.Activo, (int)TipoFleteEnum.PagoenGanadera);
                    if (datosGridProgramacionFletes != null)
                    {
                        gridDatosProgramacionFletes.ItemsSource = datosGridProgramacionFletes;
                    }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo { TipoOrganizacion = new TipoOrganizacionInfo(){ TipoOrganizacionID = (int)TipoOrganizacion.Ganadera}}
                                                          , "PropiedadIdentificadorCatalogoAyudaTipoOrnanizacion"
                                                          , "PropiedadDescripcionCatalogoAyudaTipoOrganizacion", true, 50, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaGanadera_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaGanadera_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaGanadera_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaGanadera_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaGanadera_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaGanadera_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

       
        #endregion

        
    }
}
