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
    /// Interaction logic for CausaRechazo.xaml
    /// </summary>
    public partial class CausaRechazo
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CausaRechazoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CausaRechazoInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CausaRechazo()
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
                ucPaginacion.Contexto = Contexto;
                ucPaginacion.DatosDelegado += ObtenerListaCausaRechazo;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CausaRechazo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CausaRechazo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var causaRechazoEdicion = new CausaRechazoEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.CausaRechazo_Nuevo_Titulo }
                };
                MostrarCentrado(causaRechazoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CausaRechazo_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var causaRechazoInfoSelecionado = (CausaRechazoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (causaRechazoInfoSelecionado != null)
                {
                    var causaRechazoEdicion = new CausaRechazoEdicion(causaRechazoInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.CausaRechazo_Editar_Titulo }
                        };
                    MostrarCentrado(causaRechazoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CausaRechazo_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
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


        #endregion Eventos

        #region Métodos

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CausaRechazoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaCausaRechazo(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCausaRechazo(int inicio, int limite)
        {
            try
            {
                using(var causaRechazoBL = new CausaRechazoBL())
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

                    var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                    ResultadoInfo<CausaRechazoInfo> resultadoInfo = causaRechazoBL.ObtenerPorPagina(pagina, Contexto);
                    if (resultadoInfo != null && resultadoInfo.Lista != null &&
                        resultadoInfo.Lista.Count > 0)
                    {
                        gridDatos.ItemsSource = resultadoInfo.Lista;
                        ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                        gridDatos.ItemsSource = new List<CausaRechazo>();
                    }   
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaRechazo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaRechazo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

