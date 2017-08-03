using System;                      
using System.Collections.Generic;
using System.Windows;              
using System.Windows.Controls;     
using SIE.Base.Exepciones;         
using SIE.Base.Infos;              
using SIE.Base.Log;                
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CamionReparto.xaml
    /// </summary>
    public partial class CamionReparto
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CamionRepartoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CamionRepartoInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CamionReparto()
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
                ucPaginacion.DatosDelegado += ObtenerListaCamionReparto;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                Buscar();
                skAyudaOrganizacion.AsignarFoco();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CamionReparto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CamionReparto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var camionRepartoEdicion = new CamionRepartoEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.CamionReparto_Nuevo_Titulo }
                };
                MostrarCentrado(camionRepartoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CamionReparto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var camionRepartoInfoSelecionado = (CamionRepartoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (camionRepartoInfoSelecionado != null)
                {
                    var organizacionPL = new OrganizacionPL();
                    camionRepartoInfoSelecionado.Organizacion =
                        organizacionPL.ObtenerPorID(camionRepartoInfoSelecionado.OrganizacionID);
                    var camionRepartoEdicion = new CamionRepartoEdicion(camionRepartoInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.CamionReparto_Editar_Titulo }
                        };
                    MostrarCentrado(camionRepartoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.CamionReparto_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
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
            Contexto = new CamionRepartoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo
                            {
                                TipoProceso = new TipoProcesoInfo()
                            },
                            Iva = new IvaInfo
                                {
                                    CuentaPagar = new CuentaInfo
                                        {
                                            TipoCuenta = new TipoCuentaInfo()
                                        },
                                        CuentaRecuperar = new CuentaInfo
                                            {
                                                TipoCuenta = new TipoCuentaInfo()
                                            }
                                }
                    }
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaCamionReparto(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCamionReparto(int inicio, int limite)
        {
            try
            {
                using(var camionRepartoBL = new CamionRepartoBL())
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
                    ResultadoInfo<CamionRepartoInfo> resultadoInfo = camionRepartoBL.ObtenerPorPagina(pagina, Contexto);
                    if (resultadoInfo != null && resultadoInfo.Lista != null &&
                        resultadoInfo.Lista.Count > 0)
                    {
                        gridDatos.ItemsSource = resultadoInfo.Lista;
                        ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                        gridDatos.ItemsSource = new List<CamionReparto>();
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CamionReparto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CamionReparto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

