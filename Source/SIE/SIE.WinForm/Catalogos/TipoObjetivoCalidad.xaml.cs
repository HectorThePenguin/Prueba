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
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TipoObjetivoCalidad.xaml
    /// </summary>
    public partial class TipoObjetivoCalidad
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoObjetivoCalidadInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoObjetivoCalidadInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TipoObjetivoCalidad()
        {
            InitializeComponent();
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
                ucPaginacion.DatosDelegado += ObtenerListaTipoObjetivoCalidad;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoObjetivoCalidad_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoObjetivoCalidad_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var tipoObjetivoCalidadEdicion = new TipoObjetivoCalidadEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.TipoObjetivoCalidad_Nuevo_Titulo }
                };
                MostrarCentrado(tipoObjetivoCalidadEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoObjetivoCalidad_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var tipoObjetivoCalidadInfoSelecionado = (TipoObjetivoCalidadInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (tipoObjetivoCalidadInfoSelecionado != null)
                {
                    var tipoObjetivoCalidadEdicion = new TipoObjetivoCalidadEdicion(tipoObjetivoCalidadInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.TipoObjetivoCalidad_Editar_Titulo }
                        };
                    MostrarCentrado(tipoObjetivoCalidadEdicion);
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoObjetivoCalidad_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TipoObjetivoCalidadInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaTipoObjetivoCalidad(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private TipoObjetivoCalidadInfo ObtenerFiltros()
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
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaTipoObjetivoCalidad(int inicio, int limite)
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
                var tipoObjetivoCalidadBL = new TipoObjetivoCalidadBL();
                TipoObjetivoCalidadInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<TipoObjetivoCalidadInfo> resultadoInfo = tipoObjetivoCalidadBL.ObtenerPorPagina(pagina, filtros);
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
                    gridDatos.ItemsSource = new List<TipoObjetivoCalidad>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoObjetivoCalidad_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoObjetivoCalidad_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

