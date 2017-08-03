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
using SIE.Services.Servicios.PL;   
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Enfermeria.xaml
    /// </summary>
    public partial class Enfermeria
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private EnfermeriaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (EnfermeriaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Enfermeria()
        {
            InitializeComponent();
            InicializaContexto();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
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
                ucPaginacion.DatosDelegado += ObtenerListaEnfermeria;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Enfermeria_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Enfermeria_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var enfermeriaEdicion = new EnfermeriaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.Enfermeria_Nuevo_Titulo }
                };
                MostrarCentrado(enfermeriaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Enfermeria_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var enfermeriaInfoSelecionado = (EnfermeriaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (enfermeriaInfoSelecionado != null)
                {
                    var organizacionPL = new OrganizacionPL();
                    var organizacionID = Convert.ToInt32(enfermeriaInfoSelecionado.Organizacion);
                    enfermeriaInfoSelecionado.OrganizacionInfo = organizacionPL.ObtenerPorID(organizacionID);
                    var enfermeriaEdicion = new EnfermeriaEdicion(enfermeriaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Enfermeria_Editar_Titulo }
                        };
                    MostrarCentrado(enfermeriaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Enfermeria_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtDescripcionPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new EnfermeriaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Corral = new CorralInfo
                                            {
                                                Operador = new OperadorInfo
                                                               {
                                                                   Organizacion = new OrganizacionInfo
                                                                                      {
                                                                                          TipoOrganizacion =
                                                                                              new TipoOrganizacionInfo()
                                                                                      }
                                                               },
                                                Organizacion = new OrganizacionInfo
                                                                   {
                                                                       TipoOrganizacion = new TipoOrganizacionInfo()
                                                                   },
                                                TipoCorral = new TipoCorralInfo
                                                                 {
                                                                     GrupoCorral = new GrupoCorralInfo()
                                                                 },
                                            },
                               OrganizacionInfo = new OrganizacionInfo
                                                      {
                                                          TipoOrganizacion = new TipoOrganizacionInfo()
                                                      },
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaEnfermeria(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaEnfermeria(int inicio, int limite)
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
                var enfermeriaPL = new EnfermeriaPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<EnfermeriaInfo> resultadoInfo = enfermeriaPL.ObtenerPorPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Enfermeria>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Enfermeria_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Enfermeria_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

    }
}
