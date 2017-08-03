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
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Pregunta.xaml
    /// </summary>
    public partial class Pregunta
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PreguntaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (PreguntaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Pregunta()
        {
            InitializeComponent();
            InicializaContexto();
            CargaComboTipoPregunta();
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
                cmbTipoPregunta.Focus();
                if (Contexto.TipoPregunta == null && Contexto.TipoPreguntaID == 0)
                {
                    cmbTipoPregunta.SelectedIndex = 0;
                }
                ucPaginacion.Contexto = Contexto;
                ucPaginacion.DatosDelegado += ObtenerListaPregunta;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Pregunta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Pregunta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var preguntaEdicion = new PreguntaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Pregunta_Nuevo_Titulo }
                };
                MostrarCentrado(preguntaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Pregunta_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var preguntaInfoSelecionado = (PreguntaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (preguntaInfoSelecionado != null)
                {
                    var preguntaEdicion = new PreguntaEdicion(preguntaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Pregunta_Editar_Titulo }
                        };
                    MostrarCentrado(preguntaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Pregunta_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Carga los datos de la entidad Tipo Pregunta 
        /// </summary>
        private void CargaComboTipoPregunta()
        {
            var tipoPreguntaPL = new TipoPreguntaBL();
            var tipoPregunta = new TipoPreguntaInfo
            {
                TipoPreguntaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccionar,
            };
            IList<TipoPreguntaInfo> listaTipoPregunta = tipoPreguntaPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoPregunta.Insert(0, tipoPregunta);
            cmbTipoPregunta.ItemsSource = listaTipoPregunta;
            cmbTipoPregunta.SelectedItem = tipoPregunta;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PreguntaInfo
            {
                Estatus = EstatusEnum.Activo
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaPregunta(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private PreguntaInfo ObtenerFiltros()
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
        private void ObtenerListaPregunta(int inicio, int limite)
        {
            try
            {
                var preguntaPL = new PreguntaBL();
                PreguntaInfo filtros = ObtenerFiltros();
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
                ResultadoInfo<PreguntaInfo> resultadoInfo = preguntaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Pregunta>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Pregunta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Pregunta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
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

