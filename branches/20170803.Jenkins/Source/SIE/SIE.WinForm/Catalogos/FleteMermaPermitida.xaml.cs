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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for FleteMermaPermitida.xaml
    /// </summary>
    public partial class FleteMermaPermitida
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FleteMermaPermitidaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (FleteMermaPermitidaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FleteMermaPermitida()
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
                ucPaginacion.DatosDelegado += ObtenerListaFleteMermaPermitida;
                ucPaginacion.AsignarValoresIniciales();
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.FleteMermaPermitida_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.FleteMermaPermitida_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var fleteMermaPermitidaEdicion = new FleteMermaPermitidaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.FleteMermaPermitida_Nuevo_Titulo }
                };
                MostrarCentrado(fleteMermaPermitidaEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.FleteMermaPermitida_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var fleteMermaPermitidaInfoSelecionado = (FleteMermaPermitidaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (fleteMermaPermitidaInfoSelecionado != null)
                {
                    fleteMermaPermitidaInfoSelecionado.SubFamilia.Familias = new List<FamiliaInfo>
                                                                                 {
                                                                                     new FamiliaInfo
                                                                                         {
                                                                                             FamiliaID =
                                                                                                 FamiliasEnum.
                                                                                                 MateriaPrimas.
                                                                                                 GetHashCode()
                                                                                         },
                                                                                     new FamiliaInfo
                                                                                         {
                                                                                             FamiliaID =
                                                                                                 FamiliasEnum.Premezclas
                                                                                                 .
                                                                                                 GetHashCode()
                                                                                         }
                                                                                 };
                    var fleteMermaPermitidaEdicion = new FleteMermaPermitidaEdicion(fleteMermaPermitidaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.FleteMermaPermitida_Editar_Titulo }
                        };                    
                    MostrarCentrado(fleteMermaPermitidaEdicion);
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.FleteMermaPermitida_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FleteMermaPermitidaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo
                                   {
                                       TipoOrganizacion = new TipoOrganizacionInfo
                                                              {
                                                                  TipoProceso = new TipoProcesoInfo()
                                                              }
                                   },
                SubFamilia = new SubFamiliaInfo()
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaFleteMermaPermitida(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaFleteMermaPermitida(int inicio, int limite)
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
                var fleteMermaPermitidaBL = new FleteMermaPermitidaBL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<FleteMermaPermitidaInfo> resultadoInfo = fleteMermaPermitidaBL.ObtenerPorPagina(pagina,
                                                                                                              Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<FleteMermaPermitida>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.FleteMermaPermitida_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.FleteMermaPermitida_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}
