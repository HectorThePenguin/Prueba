using System;                      
using System.Collections.Generic;  
using System.Windows;              
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;         
using SIE.Base.Infos;              
using SIE.Base.Log;                
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TipoServicio.xaml
    /// </summary>
    public partial class TipoServicio
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoServicioInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoServicioInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TipoServicio()
        {
            InitializeComponent();
            InicializaContexto();
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
                ucPaginacion.DatosDelegado += ObtenerListaTipoServicio;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoServicio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoServicio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var tipoServicioEdicion = new TipoServicioEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.TipoServicio_Nuevo_Titulo }
                };
                MostrarCentrado(tipoServicioEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoServicio_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var tipoServicioInfoSelecionado = (TipoServicioInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (tipoServicioInfoSelecionado != null)
                {
                    var tipoServicioEdicion = new TipoServicioEdicion(tipoServicioInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.TipoServicio_Editar_Titulo }
                        };
                    MostrarCentrado(tipoServicioEdicion);
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoServicio_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarLetrasConAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TipoServicioInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Lote = new LoteInfo
                                          {
                                              Corral = new CorralInfo
                                                           {
                                                               Operador = new OperadorInfo
                                                                              {
                                                                                  Organizacion = new OrganizacionInfo
                                                                                                     {
                                                                                                         TipoOrganizacion
                                                                                                             =
                                                                                                             new TipoOrganizacionInfo
                                                                                                                 {
                                                                                                                     TipoProceso
                                                                                                                         =
                                                                                                                         new TipoProcesoInfo
                                                                                                                         ()
                                                                                                                 }
                                                                                                     },
                                                                                  Rol = new RolInfo()
                                                                              },
                                                               TipoCorral = new TipoCorralInfo
                                                                                {
                                                                                    GrupoCorral = new GrupoCorralInfo()
                                                                                },
                                                               Organizacion = new OrganizacionInfo
                                                                                  {
                                                                                      TipoOrganizacion
                                                                                          =
                                                                                          new TipoOrganizacionInfo
                                                                                              {
                                                                                                  TipoProceso
                                                                                                      =
                                                                                                      new TipoProcesoInfo
                                                                                                      ()
                                                                                              }
                                                                                  },
                                                           },
                                          },
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaTipoServicio(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaTipoServicio(int inicio, int limite)
        {
            try
            {
                using(var tipoServicioBL = new TipoServicioBL())
                {
                    var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                    ResultadoInfo<TipoServicioInfo> resultadoInfo = tipoServicioBL.ObtenerPorPagina(pagina, Contexto);
                    if (resultadoInfo != null && resultadoInfo.Lista != null &&
                        resultadoInfo.Lista.Count > 0)
                    {
                        gridDatos.ItemsSource = resultadoInfo.Lista;
                        ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                        gridDatos.ItemsSource = new List<TipoServicio>();
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoServicio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoServicio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

