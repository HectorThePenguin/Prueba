using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using  System.Windows.Controls;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Lógica de interacción para AdministrarAlertas.xaml
    /// </summary>
    public partial class AdministrarAlertas
    {
        /// <summary>
        /// Contenedor de la clase
        /// </summary>
        private AlertaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AlertaInfo)DataContext;
            }

            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AdministrarAlertas()
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
                ucPaginacion.DatosDelegado += ObtenerListaAlerta;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorCargar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorCargar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Nuevo();
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
                 var alertaInfoSelecionado = (AlertaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);//recupera la informacion de la alerta que invoco la ventana de edicion
                 if (alertaInfoSelecionado != null)//si se pudo cargar la informacion correctamente
                 {
                     var alertaEdicion = new AlertaEdicion(alertaInfoSelecionado)//se invoca la ventana de registro/edicion de alertas enviandole la informacion de la alerta que se editara
                     {
                         ucTitulo = { TextoTitulo = Properties.Resources.Alerta_Editar_Titulo }
                     };
                     MostrarCentrado(alertaEdicion);//muestra la ventana de registro / edicion de alertas
                     ReiniciarValoresPaginador();//una vez cerrada la ventana de registro / edicion reinicia la paginacion usada a los valores iniciales
                     Buscar();//una vez cerrada la ventana de registro / edicion, busca y muestra las alertas en el datagrid
                 }
             }
             catch (Exception ex)
             {
                 Logger.Error(ex);
                 SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorEditar,
                     MessageBoxButton.OK, MessageImage.Error);
             }
         }

        #endregion

        #region Métodos

        /// <summary>
        /// reiniciar paginado
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new AlertaInfo();
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaAlerta(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista de alertas para mostrar en el grid
        /// </summary>
        private void ObtenerListaAlerta(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)//revisa si se tenian datos anteriores de alertas
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);//consulta si la informacion de las alertas es la misma que la anterior
                    if (!contextosIguales)//si la informacion de las alertas no es la misma (si se agregaron alertas o se editaron) reinicia la informacion de paginacion
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                
                //var alertaPL = new AlertaPL();
                AlertaInfo filtrosAlertas = ObtenerFiltrosAlertas();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                //consulta las alertas por los datos proporcionados y aplicando la paginacion actual:
                ResultadoInfo<AlertaInfo> resultadoInfo = AlertaPL.ObtenerPorPagina(pagina, filtrosAlertas);//consulta las alertas por los datos proporcionados y aplicando la paginacion actual
               
                if ( resultadoInfo.Lista != null && resultadoInfo.Lista.Count > 0)//si se encontraron alertas:
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else //en caso de no encontrarse alertas:
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<AdministrarAlertas>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorBuscar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Alerta_ErrorBuscar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private AlertaInfo ObtenerFiltrosAlertas()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Abre una ventana en blanco para registro de alertas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// muestra la ventana de registro y edicion de alertas
        /// </summary>
        void Nuevo()
        {
            try
            {
                var frmAlerta = new AlertaEdicion();
                MostrarCentrado(frmAlerta);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        
        #endregion
    }
}
