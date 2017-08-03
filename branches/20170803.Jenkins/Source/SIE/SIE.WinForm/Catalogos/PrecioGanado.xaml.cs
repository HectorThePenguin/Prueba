using System;                      
using System.Collections.Generic;
using System.Reflection;           
using System.Windows;              
using System.Windows.Controls;
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
    /// Interaction logic for PrecioGanado.xaml
    /// </summary>
    public partial class PrecioGanado
    {
        #region Propiedades
        
        /// <summary>
        /// Contexto
        /// </summary>
        private PrecioGanadoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PrecioGanadoInfo)DataContext;
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
        public PrecioGanado()
        {
            InitializeComponent();
            InicializaContexto();
            CargaTiposGanado();
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
                ucPaginacion.DatosDelegado += ObtenerListaPrecioGanado;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
                skAyudaOrganizacion.AsignaTabIndex(0);
                skAyudaOrganizacion.AsignarFoco();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.PrecioGanado_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.PrecioGanado_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var precioGanadoEdicion = new PrecioGanadoEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.PrecioGanado_Nuevo_Titulo }
                };
                MostrarCentrado(precioGanadoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.PrecioGanado_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var precioGanadoInfoSelecionado = (PrecioGanadoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (precioGanadoInfoSelecionado != null)
                {
                    var precioGanadoEdicion = new PrecioGanadoEdicion(precioGanadoInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.PrecioGanado_Editar_Titulo }
                        };
                    MostrarCentrado(precioGanadoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.PrecioGanado_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PrecioGanadoInfo
                           {
                               Organizacion = new OrganizacionInfo(),
                               TipoGanado = new TipoGanadoInfo(),
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaPrecioGanado(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private PrecioGanadoInfo ObtenerFiltros()
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
        private void ObtenerListaPrecioGanado(int inicio, int limite)
        {
            try
            {
                var precioGanadoPL = new PrecioGanadoPL();
                PrecioGanadoInfo filtros = ObtenerFiltros();

                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};
                ResultadoInfo<PrecioGanadoInfo> resultadoInfo = precioGanadoPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<PrecioGanado>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.PrecioGanado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.PrecioGanado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los Tipos de Ganado
        /// </summary>
        private void CargaTiposGanado()
        {
            try
            {
                var tipoGanadoPL = new TipoGanadoPL();
                IList<TipoGanadoInfo> tipoGanado = tipoGanadoPL.ObtenerTodos(EstatusEnum.Activo)
                                                   ?? new List<TipoGanadoInfo>();
                
                tipoGanado.Insert(0, new TipoGanadoInfo { TipoGanadoID = 0, Descripcion = Properties.Resources.Seleccione_Todos });
                cmbTipoGanado.ItemsSource = tipoGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo { OrganizacionID = 0 }
                                                   , "PropiedadClaveProgramacionEmbarque"
                                                   , "PropiedadDescripcionProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
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
        /// Reinicia el valor del inicio de la página.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}

