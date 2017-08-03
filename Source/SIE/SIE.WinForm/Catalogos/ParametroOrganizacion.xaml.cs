using System;                      
using System.Collections.Generic;  
using System.Windows;              
using System.Windows.Controls;     
using SIE.Base.Exepciones;         
using SIE.Base.Infos;              
using SIE.Base.Log;                
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;      
using SIE.Services.Servicios.PL;   
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ParametroOrganizacion.xaml
    /// </summary>
    public partial class ParametroOrganizacion
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ParametroOrganizacionInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (ParametroOrganizacionInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        //private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        #endregion Propiedades

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ParametroOrganizacion()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaOrganizacion();
        }
        #endregion 

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaParametro.ObjetoNegocio = new ParametroPL();
                skAyudaParametro.AyudaConDatos += (o, args) =>
                                                  {
                                                      Contexto.Parametro.TipoParametro = new TipoParametroInfo
                                                                                         {
                                                                                             TipoParametroID = 0
                                                                                         };
                                                  };
                ucPaginacion.DatosDelegado += ObtenerListaParametroOrganizacion;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ParametroOrganizacion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ParametroOrganizacion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
          Buscar();
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parametroOrganizacionEdicion = new ParametroOrganizacionEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.ParametroOrganizacion_Nuevo_Titulo }
                };
                MostrarCentrado(parametroOrganizacionEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ParametroOrganizacion_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button) e.Source;
            try
            {
                var parametroOrganizacionInfoSelecionado = (ParametroOrganizacionInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (parametroOrganizacionInfoSelecionado != null)
                {
                    var parametroOrganizacionEdicion = new ParametroOrganizacionEdicion(parametroOrganizacionInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.ParametroOrganizacion_Editar_Titulo }
                        };
                    MostrarCentrado(parametroOrganizacionEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ParametroOrganizacion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
      
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ParametroOrganizacionInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               OrganizacionFiltro = new OrganizacionInfo
                                                        {
                                                            TipoOrganizacion = new TipoOrganizacionInfo()
                                                        },
                               Parametro = new ParametroInfo
                                               {
                                                   TipoParametro = new TipoParametroInfo()
                                               }
                           };
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {

            //skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, Contexto.OrganizacionFiltro
            //                                                    , "PropiedadClaveCatalogoParametroOrganizacion"
            //                                                    , "PropiedadDescripcionCatalogoParametroOrganizacion"
            //                                                    , true, true) { AyudaPL = new OrganizacionPL() };

            //stpAyudaOrganizacion.Children.Clear();
            //stpAyudaOrganizacion.Children.Add(skAyudaOrganizacion);

            //skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            //skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            //skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            //skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            //skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            //skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrganizacion_Titulo;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaParametroOrganizacion(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaParametroOrganizacion(int inicio, int limite)
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
                var parametroOrganizacionPL = new ParametroOrganizacionPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ParametroOrganizacionInfo> resultadoInfo = parametroOrganizacionPL.ObtenerPorFiltroPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ParametroOrganizacion>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroOrganizacion_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ParametroOrganizacion_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

