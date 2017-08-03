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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for MermaSuperavit.xaml
    /// </summary>
    public partial class MermaSuperavit
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private MermaSuperavitInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (MermaSuperavitInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MermaSuperavit()
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
                InicializaContexto();
                CargarOrganizaciones();
                ucPaginacion.DatosDelegado += ObtenerListaMermaSuperavit;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
                skAyudaAlmacen.AyudaConDatos += (o, args) =>
                                                    {

                                                        Contexto.Almacen.FiltroTipoAlmacen =
                                                            string.Format("{0}|{1}|{2}|{3}",
                                                                          TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode(),
                                                                          TipoAlmacenEnum.MateriasPrimas.GetHashCode(),
                                                                          TipoAlmacenEnum.BodegaDeTerceros.GetHashCode(),
                                                                          TipoAlmacenEnum.EnTránsito.GetHashCode());
                                                        Contexto.Almacen.ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                                                                {
                                                                                                    new TipoAlmacenInfo
                                                                                                        {
                                                                                                            TipoAlmacenID=TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode()
                                                                                                        },
                                                                                                    new TipoAlmacenInfo
                                                                                                        {
                                                                                                            TipoAlmacenID=TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                                                                                        },
                                                                                                    new TipoAlmacenInfo
                                                                                                        {
                                                                                                            TipoAlmacenID=TipoAlmacenEnum.BodegaDeTerceros.GetHashCode()
                                                                                                        },
                                                                                                    new TipoAlmacenInfo
                                                                                                        {
                                                                                                            TipoAlmacenID=TipoAlmacenEnum.EnTránsito.GetHashCode()
                                                                                                        },
                                                                                                };
                                                    };

                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var mermaSuperavitEdicion = new MermaSuperavitEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.MermaSuperavit_Nuevo_Titulo }
                };
                MostrarCentrado(mermaSuperavitEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var mermaSuperavitInfoSelecionado = (MermaSuperavitInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (mermaSuperavitInfoSelecionado != null)
                {
                    //int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                    //mermaSuperavitInfoSelecionado.Almacen.Organizacion = new OrganizacionInfo
                    //                                                         {
                    //                                                             OrganizacionID = organizacionID
                    //                                                         };
                    mermaSuperavitInfoSelecionado.Almacen.FiltroTipoAlmacen =
                        string.Format("{0}|{1}|{2}|{3}",
                                      TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode(),
                                      TipoAlmacenEnum.MateriasPrimas.GetHashCode(),
                                      TipoAlmacenEnum.BodegaDeTerceros.GetHashCode(),
                                      TipoAlmacenEnum.BodegaExterna.GetHashCode());
                    mermaSuperavitInfoSelecionado.Almacen.ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                                                 {
                                                                                     new TipoAlmacenInfo
                                                                                         {
                                                                                             TipoAlmacenID = TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode()
                                                                                         },
                                                                                     new TipoAlmacenInfo
                                                                                         {
                                                                                             TipoAlmacenID = TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                                                                         },
                                                                                     new TipoAlmacenInfo
                                                                                         {
                                                                                             TipoAlmacenID = TipoAlmacenEnum.BodegaDeTerceros.GetHashCode()
                                                                                         },
                                                                                     new TipoAlmacenInfo
                                                                                         {
                                                                                             TipoAlmacenID = TipoAlmacenEnum.EnTránsito.GetHashCode()
                                                                                         },
                                                                                 };
                    mermaSuperavitInfoSelecionado.Producto.AlmacenID = mermaSuperavitInfoSelecionado.Almacen.AlmacenID;
                    var mermaSuperavitEdicion = new MermaSuperavitEdicion(mermaSuperavitInfoSelecionado)
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.MermaSuperavit_Editar_Titulo }
                        };
                    MostrarCentrado(mermaSuperavitEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new MermaSuperavitInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Almacen = new AlmacenInfo
                                             {
                                                 Organizacion = new OrganizacionInfo(),
                                                 FiltroTipoAlmacen =
                                                     string.Format("{0}|{1}|{2}|{3}",
                                                                   TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode(),
                                                                   TipoAlmacenEnum.MateriasPrimas.GetHashCode(),
                                                                   TipoAlmacenEnum.BodegaDeTerceros.GetHashCode(),
                                                                   TipoAlmacenEnum.EnTránsito.GetHashCode()),
                                                 ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                                        {
                                                                            new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID = TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode()
                                                                                },
                                                                            new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID = TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                                                                },
                                                                                 new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID = TipoAlmacenEnum.BodegaDeTerceros.GetHashCode()
                                                                                },
                                                                                 new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID = TipoAlmacenEnum.EnTránsito.GetHashCode()
                                                                                },
                                                                        }
                                             },
                               Producto = new ProductoInfo(),
                               Activo = EstatusEnum.Activo
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaMermaSuperavit(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaMermaSuperavit(int inicio, int limite)
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
                var mermaSuperavitPL = new MermaSuperavitPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<MermaSuperavitInfo> resultadoInfo = mermaSuperavitPL.ObtenerPorPagina(pagina, Contexto);
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
                    gridDatos.ItemsSource = new List<MermaSuperavit>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaSuperavit_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                //int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = organizacionId,
                            Descripcion = nombreOrganizacion
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    //cmbOrganizacion.SelectedIndex = 0;
                    Contexto.Almacen.Organizacion.OrganizacionID = organizacionId;
                    cmbOrganizacion.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarOrganizaciones,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }

        #endregion
    }
}

