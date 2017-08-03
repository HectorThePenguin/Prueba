using System;                      
using System.Collections.Generic;  
using System.Reflection;           
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
using System.Linq;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ProblemaSintoma.xaml
    /// </summary>
    public partial class ProblemaSintoma
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaSintomaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProblemaSintomaInfo) DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProblemaSintoma()
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
                                                         var tipoProblema =
                                                             cmbTipoProblema.SelectedItem as TipoProblemaInfo;
                                                         var contextoAyuda = skAyudaProblema.Contexto as ProblemaInfo;
                                                         contextoAyuda.TipoProblema = tipoProblema;
                                                     };
                CargaComboTipoProblema();
                ucPaginacion.DatosDelegado += ObtenerListaProblemaSintoma;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorCargar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorCargar, MessageBoxButton.OK,
                                  MessageImage.Error);
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
                var problemaSintomaEdicion = new ProblemaSintomaEdicion
                                                 {
                                                     ucTitulo =
                                                         {
                                                             TextoTitulo =
                                                                 Properties.Resources.ProblemaSintoma_Nuevo_Titulo
                                                         }
                                                 };
                MostrarCentrado(problemaSintomaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorNuevo, MessageBoxButton.OK,
                                  MessageImage.Error);
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
                var problemaSintomaInfoSelecionado =
                    (ProblemaSintomaInfo) Extensor.ClonarInfo(botonEditar.CommandParameter);

                if (problemaSintomaInfoSelecionado != null)
                {
                    ObtenerListaSintomas(problemaSintomaInfoSelecionado);
                    var problemaSintomaEdicion = new ProblemaSintomaEdicion(problemaSintomaInfoSelecionado)
                                                     {
                                                         ucTitulo =
                                                             {
                                                                 TextoTitulo =
                                                                     Properties.Resources.ProblemaSintoma_Editar_Titulo
                                                             }
                                                     };
                    MostrarCentrado(problemaSintomaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorEditar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void ObtenerListaSintomas(ProblemaSintomaInfo problemaSintomaSeleccionado)
        {
            var problemaSintomaBL = new ProblemaSintomaBL();
            var listaProblemasSintoma = problemaSintomaBL.ObtenerProblemasSintomaTodos();
            var sintomas = (from sinto in listaProblemasSintoma
                            where sinto.ProblemaID == problemaSintomaSeleccionado.ProblemaID
                            select new SintomaInfo
                                       {
                                           SintomaID = sinto.SintomaID,
                                           Descripcion = sinto.Sintoma.Descripcion,
                                           Activo = sinto.Activo,
                                           ProblemaSintomaID = sinto.ProblemaSintomaID,
                                           UsuarioCreacionID = sinto.UsuarioCreacionID,
                                           UsuarioModificacionID = sinto.UsuarioModificacionID
                                       }).ToList();

            problemaSintomaSeleccionado.ListaSintomas = sintomas;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProblemaSintomaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Problema = new ProblemaInfo
                                              {
                                                  TipoProblema = new TipoProblemaInfo()
                                              }
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaProblemaSintoma(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ProblemaSintomaInfo ObtenerFiltros()
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
        private void ObtenerListaProblemaSintoma(int inicio, int limite)
        {
            try
            {
                var problemaSintomaBL = new ProblemaSintomaBL();
                ProblemaSintomaInfo filtros = ObtenerFiltros();
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};
                ResultadoInfo<ProblemaSintomaInfo> resultadoInfo = problemaSintomaBL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ProblemaSintoma>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProblemaSintoma_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Problema 
        /// </summary>
        private void CargaComboTipoProblema()
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
                if (Contexto.ProblemaID == 0)
                {
                    cmbTipoProblema.SelectedIndex = 0;
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

