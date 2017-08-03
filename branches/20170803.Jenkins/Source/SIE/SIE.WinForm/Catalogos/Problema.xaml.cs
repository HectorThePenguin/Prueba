using System;                      
using System.Collections.Generic;  
using System.Windows;              
using System.Windows.Controls;     
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
    /// Interaction logic for Problema.xaml
    /// </summary>
    public partial class Problema
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (ProblemaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Problema()
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
                ucPaginacion.DatosDelegado += ObtenerListaProblema;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
                ObtenerListaTiposProblema();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Problema_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Problema_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var problemaEdicion = new ProblemaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Problema_Nuevo_Titulo }
                };
                MostrarCentrado(problemaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Problema_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var problemaInfoSelecionado = (ProblemaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (problemaInfoSelecionado != null)
                {
                    using (var tipoProblemaBL = new TipoProblemaBL())
                    {
                        problemaInfoSelecionado.TipoProblema =
                            tipoProblemaBL.ObtenerPorID(problemaInfoSelecionado.TipoProblemaID);
                    }
                    var problemaEdicion = new ProblemaEdicion(problemaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Problema_Editar_Titulo }
                        };
                    MostrarCentrado(problemaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Problema_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
            Contexto = new ProblemaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoProblema = new TipoProblemaInfo()
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ucPaginacion.Inicio = 1;
            ObtenerListaProblema(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaProblema(int inicio, int limite)
        {
            try
            {
                using(var problemaBL = new ProblemaBL())
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
                    ResultadoInfo<ProblemaInfo> resultadoInfo = problemaBL.ObtenerPorPagina(pagina, Contexto);
                    if (resultadoInfo != null && resultadoInfo.Lista != null &&
                        resultadoInfo.Lista.Count > 0)
                    {
                        gridDatos.ItemsSource = resultadoInfo.Lista;
                        ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                        gridDatos.ItemsSource = new List<Problema>();
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Problema_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Problema_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene una lista de los tipo de problemas activos
        /// </summary>
        private void ObtenerListaTiposProblema()
        {
            using (var tipoProblemaBL = new TipoProblemaBL())
            {
                var tipoProblema = new TipoProblemaInfo
                {
                    TipoProblemaId = 0,
                    Descripcion = Properties.Resources.cbo_Seleccionar,
                };
                IList<TipoProblemaInfo> listaTipoProblema = tipoProblemaBL.ObtenerTodos(EstatusEnum.Activo);
                listaTipoProblema.Insert(0, tipoProblema);
                cmbTipoProblema.ItemsSource = listaTipoProblema;
                if (Contexto.TipoProblemaID == 0)
                {
                    cmbTipoProblema.SelectedItem = tipoProblema;
                    //cmbTipoProblema.DisplayMemberPath = "Descripcion";
                }
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

