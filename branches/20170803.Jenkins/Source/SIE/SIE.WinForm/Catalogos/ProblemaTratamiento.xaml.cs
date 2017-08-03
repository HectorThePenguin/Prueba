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
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.PL; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ProblemaTratamiento.xaml
    /// </summary>
    public partial class ProblemaTratamiento
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaTratamientoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (ProblemaTratamientoInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProblemaTratamiento()
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
                skAyudaProblema.ObjetoNegocio = new ProblemaBL();
                skAyudaProblema.AyudaConDatos += (o, args) =>
                {
                    var contextoAyuda = skAyudaProblema.Contexto as ProblemaInfo;
                    contextoAyuda.TipoProblema = new TipoProblemaInfo();
                };
                ucPaginacion.DatosDelegado += ObtenerListaProblemaTratamiento;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacion.AyudaConDatos += (o, args) =>
                {
                    var contextoAyuda = skAyudaOrganizacion.Contexto as OrganizacionInfo;
                    contextoAyuda.TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = 2};
                    skAyudaOrganizacion.DataContext = contextoAyuda;
                };
                
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ProblemaTratamiento_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ProblemaTratamiento_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var problemaTratamientoEdicion = new ProblemaTratamientoEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.ProblemaTratamiento_Nuevo_Titulo }
                };
                MostrarCentrado(problemaTratamientoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ProblemaTratamiento_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var problemaTratamientoInfoSelecionado = (ProblemaTratamientoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (problemaTratamientoInfoSelecionado != null)
                {
                    var problemaTratamientoEdicion = new ProblemaTratamientoEdicion(problemaTratamientoInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.ProblemaTratamiento_Editar_Titulo }
                        };
                    MostrarCentrado(problemaTratamientoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ProblemaTratamiento_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProblemaTratamientoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Problema = new ProblemaInfo(),
                Tratamiento = new TratamientoInfo(),
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = SIE.Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode() }
                }
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaProblemaTratamiento(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ProblemaTratamientoInfo ObtenerFiltros()
        {
            try
            {
                if (ituTratamiento.Value == null || ituTratamiento.Value.Value == 0)
                {
                    Contexto.Tratamiento.CodigoTratamiento = 0;
                }
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
        private void ObtenerListaProblemaTratamiento(int inicio, int limite)
        {
            try
            {
                var problemaTratamientoBL = new ProblemaTratamientoBL();
                ProblemaTratamientoInfo filtros = ObtenerFiltros();
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

                ResultadoInfo<ProblemaTratamientoInfo> resultadoInfo = problemaTratamientoBL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ProblemaTratamiento>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProblemaTratamiento_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProblemaTratamiento_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
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

