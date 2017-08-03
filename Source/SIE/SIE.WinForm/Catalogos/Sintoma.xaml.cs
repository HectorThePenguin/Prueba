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
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Sintoma.xaml
    /// </summary>
    public partial class Sintoma
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SintomaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (SintomaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        private string descripcionAnterior = string.Empty;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Sintoma()
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
                ucPaginacion.DatosDelegado += ObtenerListaSintoma;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Sintoma_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Sintoma_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var sintomaEdicion = new SintomaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Sintoma_Nuevo_Titulo }
                };
                MostrarCentrado(sintomaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Sintoma_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var sintomaInfoSelecionado = (SintomaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (sintomaInfoSelecionado != null)
                {
                    var sintomaEdicion = new SintomaEdicion(sintomaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Sintoma_Editar_Titulo }
                        };
                    MostrarCentrado(sintomaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Sintoma_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new SintomaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaSintoma(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private SintomaInfo ObtenerFiltros()
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
        private void ObtenerListaSintoma(int inicio, int limite)
        {
            try
            {
                var sintomaBL = new SintomaBL();
                if (!string.IsNullOrWhiteSpace(Contexto.Descripcion)
                       || !string.IsNullOrWhiteSpace(descripcionAnterior))
                {
                    if (string.Compare(Contexto.Descripcion, descripcionAnterior, StringComparison.CurrentCultureIgnoreCase) != 0)
                    {
                        inicio = 1;
                        ucPaginacion.Inicio = 1;
                    }
                }
                descripcionAnterior = Contexto.Descripcion;
                SintomaInfo filtros = ObtenerFiltros();
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<SintomaInfo> resultadoInfo = sintomaBL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Sintoma>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Sintoma_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Sintoma_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
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

