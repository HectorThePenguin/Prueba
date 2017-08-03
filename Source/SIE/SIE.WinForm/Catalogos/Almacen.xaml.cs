using System;                      
using System.Collections.Generic;  
using System.Reflection;           
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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Almacen.xaml
    /// </summary>
    public partial class Almacen
    {

        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private AlmacenInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (AlmacenInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Almacen()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
            CargaTiposAlmacen();
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
                ucPaginacion.DatosDelegado += ObtenerListaAlmacen;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Almacen_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Almacen_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var almacenEdicion = new AlmacenEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Almacen_Nuevo_Titulo }
                };
                MostrarCentrado(almacenEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Almacen_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var almacenInfoSelecionado = (AlmacenInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (almacenInfoSelecionado != null)
                {
                    var almacenEdicion = new AlmacenEdicion(almacenInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Almacen_Editar_Titulo }
                        };
                    MostrarCentrado(almacenEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Almacen_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        #endregion


        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }



        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new AlmacenInfo
                {
                    Descripcion = string.Empty,
                    Organizacion = new OrganizacionInfo(),
                    TipoAlmacen = new TipoAlmacenInfo(),
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaAlmacen(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private AlmacenInfo ObtenerFiltros()
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
        private void ObtenerListaAlmacen(int inicio, int limite)
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
                var almacenPL = new AlmacenPL();
                AlmacenInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};
                ResultadoInfo<AlmacenInfo> resultadoInfo = almacenPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Almacen>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Almacen_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Almacen_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los Roles
        /// </summary>
        private void CargaTiposAlmacen()
        {
            var tipoAlmacenPL = new TipoAlmacenPL();
            var tipoAlmacen = new TipoAlmacenInfo
            {
                TipoAlmacenID = 0,
                Descripcion = Properties.Resources.Seleccione_Todos,
            };
            IList<TipoAlmacenInfo> listaTipoAlmacen = tipoAlmacenPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoAlmacen.Insert(0, tipoAlmacen);
            cmbTipoAlmacen.ItemsSource = listaTipoAlmacen;
            cmbTipoAlmacen.SelectedItem = tipoAlmacen;
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo {OrganizacionID = 0}
                                                                , "PropiedadClaveCatalogoAyuda"
                                                                , "PropiedadDescripcionCatalogoAyuda", true)
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
            skAyudaOrganizacion.AsignaTabIndex(1);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        #endregion
    }
}

