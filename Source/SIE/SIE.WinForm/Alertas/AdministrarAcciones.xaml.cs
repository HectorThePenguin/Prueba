using SIE.WinForm.Auxiliar;
using SIE.Services.Info.Info;
using SIE.Services.Info.Constantes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Base.Infos;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.Enum;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Lógica de interacción para AdministrarAcciones.xaml
    /// </summary>
    public partial class AdministrarAcciones
    {
        public AdministrarAcciones()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de Carga al entrar a Administrar Acciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaAcciones;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Accion_ErrorCargar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Accion_ErrorCargar, 
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        //Se inicializa el contexto..
        /// <summary>
        /// Contenedor de la clase
        /// </summary>
        private AdministrarAccionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AdministrarAccionInfo)DataContext;
            }
            set { DataContext = value; }
        }
        
        /// <summary>
        /// Se toma el usuario el cual creo y se encuentra logeado.
        /// </summary>
        public void InicializaContexto()
        {

            Contexto = new AdministrarAccionInfo
           {
               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
           };

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
                var accionNueva = new AdministrarAccionesEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.RegistroAccion_titulo }
                };
                MostrarCentrado(accionNueva);
                Buscar();
            }

            catch (Exception ex)
            {
                Logger.Error(ex);

            }
        }
        
        /// <summary>
        /// Buscar la accion dentro de las registrados para evitar duplicados.
        /// </summary>
        public void Buscar()
        {
            ObtenerListaAcciones(ucPaginacion.Inicio, ucPaginacion.Limite);
        }
        
        /// <summary>
        /// Ontiene la lista
        /// </summary>
        /// <param name="inicio">posicion del primer registro que se mostrara(el valor inicial es 1)</param>
        /// <param name="limite">posicion del ultimo registro que se mostrara en esta pagina</param>
        private void ObtenerListaAcciones(int inicio, int limite)
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
                // se crean los filtros para le paginado  para posterior mandar llamar el SP.
                //var AccionPL = new AccionPL();
                AdministrarAccionInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<AdministrarAccionInfo> resultadoInfo = AccionPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<AdministrarAcciones>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Accion_ErrorBuscar, 
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Accion_ErrorBuscar,
                    MessageBoxButton.OK, MessageImage.Error);
            }

     }
        
        /// <summary>
        /// metodo que retorna context
        /// </summary>
        /// <returns></returns>
        private AdministrarAccionInfo ObtenerFiltros()
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
        /// buscaremos el registro en base a descripcion y Estatus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();

        }
        
        /// <summary>
        /// metodo para la edición de las acciones. Tomando los datos del girdDatos pasandolos al pantalla de edción 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var accionSeleccionada = (AdministrarAccionInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (accionSeleccionada != null)
                {
                    var accionEdicion = new AdministrarAccionesEdicion(accionSeleccionada)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.EdicionAccion_titulo }
                    };
                    MostrarCentrado(accionEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Accion_ErrorEditar, 
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
        /// <summary>
        /// reiniciar valores de paginado
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }
    }
}
