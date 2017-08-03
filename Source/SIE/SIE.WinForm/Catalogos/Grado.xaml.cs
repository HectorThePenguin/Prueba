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
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox;
using System.Windows.Input; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Grado.xaml
    /// </summary>
    public partial class Grado
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private GradoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (GradoInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Grado()
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
                ucPaginacion.DatosDelegado += ObtenerListaGrado;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Grado_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Grado_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var gradoEdicion = new GradoEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Grado_Nuevo_Titulo }
                };
                MostrarCentrado(gradoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Grado_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var gradoInfoSelecionado = (GradoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (gradoInfoSelecionado != null)
                {
                    var gradoEdicion = new GradoEdicion(gradoInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Grado_Editar_Titulo }
                        };
                    MostrarCentrado(gradoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Grado_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
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
            Contexto = new GradoInfo
            {
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
            ucPaginacion.Inicio = 1;
            ObtenerListaGrado(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaGrado(int inicio, int limite)
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
                var gradoPL = new GradoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<GradoInfo> resultadoInfo = gradoPL.ObtenerPorPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Grado>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Grado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Grado_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

