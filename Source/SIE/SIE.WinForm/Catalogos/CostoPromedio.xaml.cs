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
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CostoPromedio.xaml
    /// </summary>
    public partial class CostoPromedio
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CostoPromedioInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CostoPromedioInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCosto;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CostoPromedio()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
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
                ucPaginacion.DatosDelegado += ObtenerListaCostoPromedio;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CostoPromedio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CostoPromedio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var costoPromedioEdicion = new CostoPromedioEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.CostoPromedio_Nuevo_Titulo }
                };
                MostrarCentrado(costoPromedioEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CostoPromedio_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var costoPromedioInfoSelecionado = (CostoPromedioInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (costoPromedioInfoSelecionado != null)
                {
                    var costoPromedioEdicion = new CostoPromedioEdicion(costoPromedioInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.CostoPromedio_Editar_Titulo }
                        };
                    MostrarCentrado(costoPromedioEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CostoPromedio_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new CostoPromedioInfo
                    {
                        Organizacion = new OrganizacionInfo(),
                        Costo = new CostoInfo(),
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    };
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaCostoPromedio(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private CostoPromedioInfo ObtenerFiltros()
        {
            try
            {
                Contexto.Organizacion =
                     new OrganizacionInfo
                     {
                         OrganizacionID = Extensor.ValorEntero(skAyudaOrganizacion.Clave)
                     };
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCostoPromedio(int inicio, int limite)
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
                var costoPromedioPL = new CostoPromedioPL();
                CostoPromedioInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CostoPromedioInfo> resultadoInfo = costoPromedioPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<CostoPromedio>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CostoPromedio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CostoPromedio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
            AgregarAyudaCosto();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, Contexto.Organizacion
                                                                , "PropiedadClaveCatalogoAyuda"
                                                                , "PropiedadDescripcionCatalogoAyuda", true, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente =
                    Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar =
                    Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }
        
        /// <summary>
        /// Agrega una ayuda de costo
        /// </summary>
        private void AgregarAyudaCosto()
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(200, false, Contexto.Costo
                                                , "PropiedadClaveCosteoEntradaSinDependencia"
                                                , "PropiedadDescripcionCosteoEntradaSinDependencia"
                                                , "PropiedadOcultaAyudaCatalogoAyuda", true, true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };

            skAyudaCosto.AsignaTabIndex(1);
            skAyudaCosto.IsTabStop = false;
            SplAyudaCosto.Children.Clear();
            SplAyudaCosto.Children.Add(skAyudaCosto);
        }

        #endregion
    }
}
